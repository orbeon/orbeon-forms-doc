# Microsoft Azure

## Availability

- [SINCE Orbeon Forms 2024.1]
- This is an [Orbeon Forms PE](https://www.orbeon.com/download) feature.

## Overview

This guide walks you through deploying Orbeon Forms on Microsoft Azure using:

- [Entra ID](https://learn.microsoft.com/en-us/entra/identity/) for user/group management
- [Azure Storage](https://learn.microsoft.com/en-us/azure/storage/) for configuration files
- [PostgreSQL](https://learn.microsoft.com/en-us/azure/postgresql/) for the database
- [Kubernetes](https://learn.microsoft.com/en-us/azure/aks/) for container orchestration
- [Azure Container Registry](https://learn.microsoft.com/en-us/azure/container-registry/) for custom Docker images (optional)

We will use a self-signed certificate and a single-node Kubernetes cluster for demonstration purposes. In production, you would likely use a certificate signed by a trusted certificate authority (CA) and a multi-node cluster.

The users/groups will be accessed via the OpenID Connect (OIDC) protocol and WildFly's native OIDC support implementation. Entra ID groups will be mapped to WildFly roles, which will be used to control access to Orbeon Forms.

We will create two groups/roles:

 - `orbeon-user`
 - `orbeon-admin`

As well as two test users:

 - `testuser1` (member of `orbeon-user`)
 - `testuser2` (member of `orbeon-user` and `orbeon-admin`)

Orbeon Forms will be accessible only to users from the `orbeon-user` group (i.e. to both `testuser1` and `testuser2`). Form Builder and Forms Admin pages will be accessible only to users from the `orbeon-admin` group (i.e. to `testuser2` only).

A more complete example Bash script is available on [GitHub](https://github.com/orbeon/orbeon-forms/tree/master/docker/azure). It includes more commands, which will check if the resources already exist, among other things, but it follows roughly the same steps as described here.

Some values in the commands used in this guide will be hardcoded for simplicity. In a real-world scenario, you would likely want to parameterize them, for example by using environment variables.

## Requirements

The main requirement is an account with an Azure subscription.

The following utilities will be used during the installation process:

- `az` (Azure CLI)
- `psql` (PostgreSQL client)
- `kubectl` (Kubernetes deployment)
- `jq` (JSON manipulation)
- `base64` (Kubernetes passwords encoding)
- core Linux utilities such as `cat`, `curl`, `echo`, etc.

All steps described below can also be done manually via the Azure UI.

## Login and general configuration

The very first step is to login to Azure and set the Microsoft Graph API scope:

```bash
az login --scope https://graph.microsoft.com/.default
```

The following providers need to be registered:

```bash
az provider register --namespace Microsoft.Compute
az provider register --namespace Microsoft.ContainerRegistry
az provider register --namespace Microsoft.ContainerService
az provider register --namespace Microsoft.DBforPostgreSQL
az provider register --namespace Microsoft.Storage
```

##  Self-signed certificate

Generate a self-signed certificate for the application:

```bash
keytool \
  -genkey \
  -alias server \
  -keyalg RSA \
  -validity 3650 \
  -keysize 2048 \
  -keystore application.keystore \
  -storepass password \
  -keypass password \
  -dname "CN=localhost, OU=Unknown, O=Unknown, L=Unknown, ST=Unknown, C=Unknown"
```

This command uses the keystore filename (`application.keystore`) and passwords (`password`) used by default in WildFly's configuration (`standalone.xml` file). Using a stronger password is recommended.

## Entra ID

Retrieve the Entra ID domain:

```bash
ENTRA_ID_DOMAIN=$(az rest \
                  --method get \
                  --url 'https://graph.microsoft.com/v1.0/domains' \
                  --query 'value[0].id' \
                  -o tsv)
```

It should look like `contoso.onmicrosoft.com`.

### Users

Create two test users `testuser1` and `testuser2` (replace `contoso.onmicrosoft.com` with your domain):

```bash
az ad user create \
  --user-principal-name 'testuser1@contoso.onmicrosoft.com' \
  --password 'CHANGEME0!' \
  --display-name 'Test User 1'
```

The [user principal name (UPN)](https://learn.microsoft.com/en-us/entra/identity/hybrid/connect/plan-connect-userprincipalname) follows the format `prefix@domain`.

Optionally, you can associate an email address with the user. For this, we use the Microsoft Graph API:

```bash
az rest \
  --method patch \
  --url "https://graph.microsoft.com/v1.0/users/$user_id" \
  --body "{\"mail\":\"$email\"}"
```

To retrieve the user ID:

```bash
az ad user show --id 'testuser1@contoso.onmicrosoft.com' --query id -o tsv
```

### Groups

Create two groups `orbeon-user` and `orbeon-admin`:

```bash
az ad group create --display-name 'orbeon-user' --mail-nickname 'orbeon-user'
az ad group create --display-name 'orbeon-admin' --mail-nickname 'orbeon-admin'
```

Add users to groups:

```bash
az ad group member add --group 'orbeon-user' --member-id "$user1_id"
az ad group member add --group 'orbeon-user' --member-id "$user2_id"
az ad group member add --group 'orbeon-admin' --member-id "$user2_id"
```

### Application

Create an application called `Orbeon Forms`. This is needed to expose our users and groups to Orbeon Forms via the OIDC protocol.

```bash
az ad app create --display-name 'Orbeon Forms' --sign-in-audience 'AzureADMyOrg'
```

Retrieve the application ID:

```bash
ENTRA_ID_APP_ID=$(az ad app list --query "[?displayName=='Orbeon Forms'].appId" -o tsv)
```

You can also retrieve it from the output of the previous command.

Add an API identifier URI to the application:

```bash
az ad app update --id "$ENTRA_ID_APP_ID" --identifier-uris "api://$ENTRA_ID_APP_ID"
```

Retrieve the application object ID:

```bash
ENTRA_ID_APP_OBJECT_ID=$(az ad app list --query "[?displayName=='Orbeon Forms'].id" -o tsv)
```

Note that this is not the same as the application ID (`id` vs `appId`).

To have access to the groups/roles in both the OIDC ID and access tokens, we need to add a scope to the application. If we don't do this, WildFly's OIDC implementation will be unable to retrieve the groups/roles.

Add a scope called `groups.access` to the application:

```bash
az rest \
  --method PATCH \
  --uri "https://graph.microsoft.com/v1.0/applications/$ENTRA_ID_APP_OBJECT_ID" \
  --headers 'Content-Type=application/json' \
  --body "$(jq -n \
    --arg scope_id "$(uuidgen)" \
    '{
      api: {
        oauth2PermissionScopes: [{
          adminConsentDescription: "Allow the application to access groups on behalf of the signed-in user.",
          adminConsentDisplayName: "Access groups",
          id: $scope_id,
          isEnabled: true,
          type: "User",
          userConsentDescription: "Allow the application to access groups on your behalf.",
          userConsentDisplayName: "Access groups",
          value: "groups.access"
        }]
      }
    }')"
```

The scope ID is generated using `uuidgen`. Note that the `jq` command above is used to inject the scope ID into the JSON body. This can be done manually or in other ways as well.

For an existing scope, you can retrieve the scope ID from its name using the following command:

```bash
ENTRA_ID_SCOPE_ID=$(az ad app show \
                    --id "$ENTRA_ID_APP_ID" \
                    --query "api.oauth2PermissionScopes[?value=='groups.access'].id" \
                    -o tsv)
```

Pre-authorize the application, so that the users won't have to explicitly consent to the permissions:

```bash
az ad app show --id "$ENTRA_ID_APP_ID" | \
  jq --arg app_id "$ENTRA_ID_APP_ID" \
     --arg scope_id "$ENTRA_ID_SCOPE_ID" \
     '.api.preAuthorizedApplications = [{
       "appId": $app_id,
       "delegatedPermissionIds": [$scope_id]
     }]' | \
  az rest --method PATCH --uri "https://graph.microsoft.com/v1.0/applications/$ENTRA_ID_APP_OBJECT_ID" --body @-
```

Add a client secret:

```bash
ENTRA_ID_CREDENTIAL_SECRET=$(az ad app credential reset \
                             --id "$ENTRA_ID_APP_ID" \
                             --display-name 'Orbeon Forms Credential' \
                             --years 2 | jq -r '.password')
```

Beware: the command above will update/overwrite any existing secret with the same name.

Only security group membership claims will be included as group IDs:
    
```bash
az ad app show --id "$ENTRA_ID_APP_ID" | \
  jq '.groupMembershipClaims = "SecurityGroup"' | \
  az rest --method PATCH --uri "https://graph.microsoft.com/v1.0/applications/$ENTRA_ID_APP_OBJECT_ID" --body @-
```

Add optional OIDC claims (groups included as roles, email):

```bash
az ad app show --id "$ENTRA_ID_APP_ID" | \
  jq '.optionalClaims = {
    "accessToken": [{
      "additionalProperties": ["emit_as_roles"],
      "essential": false,
      "name": "groups",
      "source": null
    }],
    "idToken": [{
      "additionalProperties": ["emit_as_roles"],
      "essential": false,
      "name": "groups",
      "source": null
    },
    {
      "additionalProperties": [],
      "essential": false,
      "name": "email",
      "source": null
    }]
  }' | \
  az rest --method PATCH --uri "https://graph.microsoft.com/v1.0/applications/$ENTRA_ID_APP_OBJECT_ID" --body @-
```

Note that we have updated the application manifest multiple times, using the `az rest --method PATCH` command, but this was mainly done for demonstration purposes. In a real-world scenario, you would likely update the application manifest only once, with all the changes.

Grant Microsoft Graph permissions:

```bash
# Azure API constants
API_MICROSOFT_GRAPH='00000003-0000-0000-c000-000000000000'
API_PERMISSION_OPENID='37f7f235-527c-4136-accd-4a02d197296e'
API_PERMISSION_EMAIL='64a6cdd6-aab1-4aaf-94b8-3cc8405e90d0'

az ad app permission add \
  --id "$ENTRA_ID_APP_ID" \
  --api "$API_MICROSOFT_GRAPH" \
  --api-permissions "$API_PERMISSION_OPENID=Scope"
  
az ad app permission add \
  --id "$ENTRA_ID_APP_ID" \
  --api "$API_MICROSOFT_GRAPH" \
  --api-permissions "$API_PERMISSION_EMAIL=Scope"
```

Grant admin consent for permissions above:

```bash
az ad app permission admin-consent --id "$ENTRA_ID_APP_ID"
```

## Configuration files

Get the tenant ID:

```bash
ENTRA_ID_TENANT_ID=$(az account show --query tenantId -o tsv)
```

Build the OIDC provider URL:

```bash
ENTRA_ID_PROVIDER_URL="https://login.microsoftonline.com/$ENTRA_ID_TENANT_ID/v2.0"
```

Build the scope API URL:

```bash
ENTRA_ID_API_SCOPE_URL="api://$ENTRA_ID_APP_ID/$ENTRA_ID_SCOPE_VALUE"
```

Generate the OIDC configuration file:

```bash
cat << EOF > oidc.json
{
  "client-id": "$ENTRA_ID_APP_ID",
  "provider-url": "$ENTRA_ID_PROVIDER_URL",
  "credentials": {
    "secret": "$ENTRA_ID_CREDENTIAL_SECRET"
  },
  "principal-attribute": "oid",
  "scope": "profile $ENTRA_ID_API_SCOPE_URL"
}
EOF
```

The above configuration will return users as IDs. If you want to return users as emails, you can use `email` instead of `oid` as the value for `principal-attribute`.

The `oidc.json` file will look like this:

```json
{
  "client-id": "4a3e3344-7f1b-4aea-b5d4-a18705757270",
  "provider-url": "https://login.microsoftonline.com/9eacdffb-6700-4a98-a9a7-507d898fdfa8/v2.0",
  "credentials": {
    "secret": "BP28Q~XQ68YAyh_N2vr1vw8EPeaGKAwRUTXGJb3N"
  },
  "principal-attribute": "oid",
  "scope": "profile api://4a3e3344-7f1b-4aea-b5d4-a18705757270/groups.access"
}
```

In OIDC, we will refer to the groups by their IDs (not their display names):

```bash
ENTRA_ID_USER_GROUP_ID=$(az ad group show --group 'orbeon-user' --query id -o tsv)
ENTRA_ID_ADMIN_GROUP_ID=$(az ad group show --group 'orbeon-admin' --query id -o tsv)
```

Generate the Form Builder permissions file:
    
```bash
cat << EOF > form-builder-permissions.xml
<roles>
  <role name="$ENTRA_ID_ADMIN_GROUP_ID" app="*" form="*"/>
</roles>
EOF
```

The `form-builder-permissions.xml` file will look like this:

```xml
<roles>
  <role name="79390699-2df8-4110-b0b3-2b97c3db1821" app="*" form="*"/>
</roles>
```

Extract the `web.xml` file from the [Orbeon Forms WAR file](https://www.orbeon.com/download) or download [`web.template.xml` from GitHub](https://github.com/orbeon/orbeon-forms/blob/master/docker/azure/web.template.xml), and make sure the following lines are present:

```xml
<web-app>
    <!-- PostgreSQL configuration -->
    <resource-ref>
        <description>DataSource</description>
        <res-ref-name>jdbc/postgresql</res-ref-name>
        <res-type>javax.sql.DataSource</res-type>
        <res-auth>Container</res-auth>
    </resource-ref>
    <!-- Restrict Orbeon Forms to the orbeon-user group/role -->
    <security-constraint>
        <web-resource-collection>
            <web-resource-name>Form Runner</web-resource-name>
            <url-pattern>/*</url-pattern>
        </web-resource-collection>
        <auth-constraint>
            <role-name>ENTRA_ID_USER_GROUP_ID</role-name>
        </auth-constraint>
    </security-constraint>
    <!-- Restrict Form Builder and Forms Admin pages to the orbeon-admin group/role -->
    <security-constraint>
        <web-resource-collection>
            <web-resource-name>Form Builder</web-resource-name>
            <url-pattern>/fr/orbeon/builder/*</url-pattern>
            <url-pattern>/fr/admin</url-pattern>
        </web-resource-collection>
        <auth-constraint>
            <role-name>ENTRA_ID_ADMIN_GROUP_ID</role-name>
        </auth-constraint>
    </security-constraint>
    <!-- Use OIDC for authentication -->
    <login-config>
        <auth-method>OIDC</auth-method>
    </login-config>
    <security-role>
        <role-name>ENTRA_ID_USER_GROUP_ID</role-name>
        <role-name>ENTRA_ID_ADMIN_GROUP_ID</role-name>
    </security-role>
</web-app>
```

Replace `ENTRA_ID_USER_GROUP_ID` and `ENTRA_ID_ADMIN_GROUP_ID` with the actual group IDs.

Download [`standalone.postgresql.azure.xml` from GitHub](https://github.com/orbeon/orbeon-forms/blob/master/docker/azure/standalone.postgresql.azure.xml) and make sure the following lines are present:

```xml
<server>
    <profile>
        <subsystem xmlns="urn:jboss:domain:datasources:7.2">
            <datasources>
                <!-- PostgreSQL configuration -->
                <datasource jndi-name="java:/jdbc/postgresql" pool-name="postgresql" enabled="true" use-java-context="true">
                    <connection-url>jdbc:postgresql://DATABASE_SERVER.postgres.database.azure.com:5432/orbeon?useUnicode=true&amp;characterEncoding=UTF8&amp;socketTimeout=30&amp;tcpKeepAlive=true</connection-url>
                    <driver>postgresql-42.7.3.jar</driver>
                    <security user-name="orbeon@DATABASE_SERVER" password="orbeon"/>
                </datasource>
            </datasources>
        </subsystem>
    </profile>
</server>
```

Replace `DATABASE_SERVER` with your database server name, which must be unique across Azure.

We need `jboss-web-xml` to be configured for PostgreSQL:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<jboss-web>
    <resource-ref>
        <res-ref-name>jdbc/postgresql</res-ref-name>
        <jndi-name>java:/jdbc/postgresql</jndi-name>
    </resource-ref>
</jboss-web>
```

We will also configure the Orbeon Forms properties in `properties-local.xml` to use PostgreSQL instead of SQLite:

```xml
<properties xmlns:xs="http://www.w3.org/2001/XMLSchema">
    <!-- Change the following values for production use -->
    <property as="xs:string" name="oxf.crypto.password"              value="J$WYh!Ltgg4Q^jnR"/>
    <property as="xs:string" name="oxf.fr.field-encryption.password" value=""/>
    <property as="xs:string" name="oxf.fr.access-token.password"     value=""/>

    <property as="xs:string"  name="oxf.fr.persistence.provider.*.*.*" value="postgresql"/>
    <property as="xs:boolean" name="oxf.fr.persistence.sqlite.active"  value="false"/>
</properties>
```

## Resource group

Create a resource group:

```bash
az group create \
  --name 'orbeon-forms-resource-group' \
  --location 'westus'
```

It will be used by all the resources we create (Azure Storage, PostgreSQL, etc.).

## Storage

Create a storage account:

```bash
az storage account create \
  --name "$STORAGE_ACCOUNT" \
  --resource-group 'orbeon-forms-resource-group' \
  --location 'westus' \
  --sku Standard_LRS
```

The storage account name must be unique across Azure.

Create a storage share:

```bash
az storage share create \
  --name 'orbeon-forms-share' \
  --account-name "$STORAGE_ACCOUNT"
```

To upload a file to the storage share, use the following command:

```bash
az storage file upload \
  --account-name "$STORAGE_ACCOUNT" \
  --share-name 'orbeon-forms-share' \
  --source "$source" \
  --path "$destination"
```

We will need to upload the following configuration files:

- `application.keystore`
- `form-builder-permissions.xml`
- `jboss-web.xml`
- `license.xml` (get a free trial license [here](https://www.orbeon.com/download) if needed)
- `oidc.json`
- `properties-local.xml`
- `standalone.xml`
- `web.xml`

Retrieve the storage account access key:

```bash
STORAGE_ACCOUNT_SECRET_KEY=$(az storage account keys list \
                             --account-name "$STORAGE_ACCOUNT" \
                             --resource-group 'orbeon-forms-resource-group' \
                             --query '[0].value' \
                             --output tsv)
```

Alternatively, all configuration files could be included in a custom Docker image, but this is less flexible when configuration needs to be changed.

## Database

Create a PostgreSQL database server:

```bash
az postgres flexible-server create \
  --name "$DATABASE_SERVER" \
  --resource-group 'orbeon-forms-resource-group' \
  --location 'westus' \
  --admin-user "$DATABASE_ADMIN_USERNAME" \
  --admin-password "$DATABASE_ADMIN_PASSWORD" \
  --sku-name standard_d2ds_v4 \
  --version 16 \
  --public-access None
```

The database server name must be unique across Azure.

Retrieve your client's public IP address:

```bash
DATABASE_PUBLIC_IP=$(curl -s 'https://api.ipify.org')
```

This will allow you to configure a firewall rule to allow access to the database server only from your client's IP address.

```bash
az postgres flexible-server firewall-rule create \
  --rule-name 'local-ip-allowed' \
  --name "$DATABASE_SERVER" \
  --resource-group 'orbeon-forms-resource-group' \
  --start-ip-address "$DATABASE_PUBLIC_IP" \
  --end-ip-address "$DATABASE_PUBLIC_IP"
```

Alternatively, you can call `az postgres flexible-server create` without `--public-access None`. This will automatically create the firewall rule above.

Create the `orbeon` database:

```bash
az postgres flexible-server db create \
  --database-name `orbeon` \
  --server-name "$DATABASE_SERVER" \
  --resource-group 'orbeon-forms-resource-group';
```

Make the database administrator password available to the `psql` command:

```bash
export PGPASSWORD="$DATABASE_ADMIN_PASSWORD"
```

Create the `orbeon` database user:

```bash
psql \
  --host "$DATABASE_SERVER.postgres.database.azure.com" \
  --username "$DATABASE_ADMIN_USERNAME" \
  --dbname `orbeon` \
  --command "CREATE USER \"orbeon@$DATABASE_SERVER\" WITH PASSWORD '$password';"
```

Note that Azure PostgreSQL users need to follow the format `username@servername`.

Grant privileges to the `orbeon` database user:

```bash
psql \
  --host "$DATABASE_SERVER.postgres.database.azure.com" \
  --username "$DATABASE_ADMIN_USERNAME" \
  --dbname `orbeon` \
  --command "GRANT ALL PRIVILEGES ON DATABASE orbeon TO \"orbeon@$DATABASE_SERVER\";" \
  --command "GRANT ALL PRIVILEGES ON SCHEMA public TO \"orbeon@$DATABASE_SERVER\";" \
  --command "GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO \"orbeon@$DATABASE_SERVER\";" \
  --command "GRANT ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA public TO \"orbeon@$DATABASE_SERVER\";" \
  --command "ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT ALL PRIVILEGES ON TABLES TO \"orbeon@$DATABASE_SERVER\";" \
  --command "ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT ALL PRIVILEGES ON SEQUENCES TO \"orbeon@$DATABASE_SERVER\";";
```

Create the Orbeon Forms database schema:

```bash
psql \
  --host "$DATABASE_SERVER.postgres.database.azure.com" \
  --username "$DATABASE_ADMIN_USERNAME" \
  --dbname 'orbeon' \
  --file ./postgresql-2024_1.sql;
```

The SQL files needed to create the Orbeon Forms database schema can be downloaded from [PostgreSQL database setup](../form-runner/persistence/relational-db.md#postgresql-database-setup).

You can then delete the `local-ip-allowed` firewall rule:

```bash
az postgres flexible-server firewall-rule delete \
  --rule-name 'local-ip-allowed' \
  --name "$DATABASE_SERVER" \
  --resource-group 'orbeon-forms-resource-group' \
  --yes;
```

Alternatively, you can use other databases, such as Azure SQL Database or Azure Database for MySQL.

## Container registry

If you need to use a custom Docker image, create an Azure Container Registry:

```bash
az acr create --name "$CONTAINER_REGISTRY" --resource-group 'orbeon-forms-resource-group' --sku Basic
```

The container registry name must be unique across Azure.

Then login to the Azure Container Registry:

```bash
az acr login --name "$CONTAINER_REGISTRY"
```

Retrieve the Azure Container Registry ID:

```bash
CONTAINER_REGISTRY_ID=$(az acr show --name "$CONTAINER_REGISTRY" --resource-group 'orbeon-forms-resource-group' --query id -o tsv)
```

Create a Dockerfile to customize the Orbeon Forms Docker image:

```bash
cat > Dockerfile << EOF
FROM orbeon/orbeon-forms:2024.1-pe-wildfly
# TODO: customize your image here
EOF
```

Build the Docker image:

```bash
docker build --platform 'linux/amd64' -t 'orbeon-forms-custom:2024.1-pe-wildfly' .
```

Tag the Docker image with the full Azure Container Registry URL

```bash
docker tag 'orbeon-forms-custom:2024.1-pe-wildfly' "$CONTAINER_REGISTRY.azurecr.io/orbeon-forms-custom:2024.1-pe-wildfly"
```

Push the Docker image to the Azure Container Registry:

```bash
docker push "$CONTAINER_REGISTRY.azurecr.io/orbeon-forms-custom:2024.1-pe-wildfly"
```

## Kubernetes

Create an Azure Kubernetes Service (AKS) cluster:

```bash
az aks create \
  --name "$K8S_CLUSTER_NAME" \
  --resource-group 'orbeon-forms-resource-group' \
  --node-count 1 \
  --network-plugin azure \
  --generate-ssh-keys
```

The AKS cluster name must be unique across Azure.

Retrieve AKS credentials, save them locally to `~/.kube/config`, and set the AKS cluster as the current context:

```bash
az aks get-credentials \
  --name "$K8S_CLUSTER_NAME" \
  --resource-group 'orbeon-forms-resource-group' \
  --overwrite-existing
```

If you use a custom Docker image, you need to grant permission to the cluster to pull images from the Azure Container Registry.

```bash
# Retrieve the cluster's client ID
K8S_CLIENT_ID=$(az aks show \
                --name "$K8S_CLUSTER_NAME" \
                --resource-group 'orbeon-forms-resource-group' \
                --query 'identityProfile.kubeletidentity.clientId' \
                -o tsv)

az role assignment create --assignee "$K8S_CLIENT_ID" --role AcrPull --scope "$CONTAINER_REGISTRY_ID"
```

Generate the storage account name/key secret file

```bash
cat > storage-secret.yaml << EOF
apiVersion: v1
kind: Secret
metadata:
  name: storage-secret
type: Opaque
data:
  azurestorageaccountname: $(echo -n "$STORAGE_ACCOUNT" | base64)
  azurestorageaccountkey: $(echo -n "$STORAGE_ACCOUNT_SECRET_KEY" | base64)
EOF
```

The Azure Storage account name and key need to be encoded in Base64.

Import the storage account name/key secret

```bash
kubectl apply -f storage-secret.yaml.yaml
```

Generate the persistence volume configuration file:

```bash
cat > orbeon-forms-pv.yaml << EOF
apiVersion: v1
kind: PersistentVolume
metadata:
  name: orbeon-forms-pv
spec:
  capacity:
    storage: 5Gi
  accessModes:
    - ReadWriteMany
  storageClassName: azure-file
  azureFile:
    secretName: storage-secret
    shareName: orbeon-forms-share
    readOnly: false
EOF
```

Import the persistence volume configuration:

```bash
kubectl apply -f orbeon-forms-pv.yaml.yaml
```

Generate the persistence volume claim configuration file:

```bash
cat > orbeon-forms-pvc.yaml << EOF
apiVersion: v1
kind: PersistentVolumeClaim
metadata:
  name: orbeon-forms-pvc
spec:
  accessModes:
    - ReadWriteMany
  storageClassName: azure-file
  resources:
    requests:
     storage: 5Gi
EOF
```

Import the persistence volume claim configuration:

```bash
kubectl apply -f orbeon-forms-pvc.yaml
```

Depending on whether you use the default Orbeon Forms image or a custom one, you will need to set the image name as follows:

```bash
# Unmodified Orbeon Forms image
K8S_IMAGE='orbeon/orbeon-forms:2024.1-pe-wildfly'

# Custom Orbeon Forms image
K8S_IMAGE="$CONTAINER_REGISTRY.azurecr.io/orbeon-forms-custom:2024.1-pe-wildfly"
```

Generate the deployment configuration file:

```bash
cat > orbeon-forms-deployment.yaml << EOF
apiVersion: apps/v1
kind: Deployment
metadata:
  name: orbeon-forms-deployment
  labels:
  app: orbeon-forms
spec:
  replicas: 1
  selector:
    matchLabels:
     app: orbeon-forms
  template:
    metadata:
      labels:
        app: orbeon-forms
    spec:
      containers:
      - name: orbeon-forms
        image: $K8S_IMAGE
        ports:
        - containerPort: 8443
        volumeMounts:
          - name: azure-volume
            mountPath: /opt/jboss/wildfly/standalone/deployments/orbeon.war/WEB-INF/resources/config/license.xml
            subPath: license.xml
          - name: azure-volume
            mountPath: /opt/jboss/wildfly/standalone/deployments/orbeon.war/WEB-INF/resources/config/form-builder-permissions.xml
            subPath: form-builder-permissions.xml
          - name: azure-volume
            mountPath: /opt/jboss/wildfly/standalone/deployments/orbeon.war/WEB-INF/resources/config/properties-local.xml
            subPath: properties-local.xml
          - name: azure-volume
            mountPath: /opt/jboss/wildfly/standalone/deployments/orbeon.war/WEB-INF/jboss-web.xml
            subPath: jboss-web.xml
          - name: azure-volume
            mountPath: /opt/jboss/wildfly/standalone/deployments/orbeon.war/WEB-INF/oidc.json
            subPath: oidc.json
          - name: azure-volume
            mountPath: /opt/jboss/wildfly/standalone/deployments/orbeon.war/WEB-INF/web.xml
            subPath: web.xml
          - name: azure-volume
            mountPath: /opt/jboss/wildfly/standalone/configuration/application.keystore
            subPath: application.keystore
          - name: azure-volume
            mountPath: /docker-entrypoint-wildfly.d/standalone.xml
            subPath: standalone.xml
      volumes:
        - name: azure-volume
          persistentVolumeClaim:
          claimName: orbeon-forms-pvc
---
apiVersion: v1
kind: Service
metadata:
  name: orbeon-forms-service
spec:
  type: LoadBalancer
  selector:
    app: $K8S_APP
  ports:
    - protocol: TCP
      port: 443
      targetPort: 8443
EOF
```

Note that the `standalone.xml` file is mounted in the container as `docker-entrypoint-wildfly.d/standalone.xml`. This is because WildFly needs to move/rename that file, so it needs to be copied to instead of mounted directly in the `/opt/jboss/wildfly/standalone/configuration` configuration directory. See [this issue on GitHub](https://github.com/jboss-dockerfiles/wildfly/issues/70) for more information. The WildFly version of Orbeon Forms will copy any `standalone.xml` found in `docker-entrypoint-wildfly.d` to the WildFly configuration directory.

Import the deployment configuration:

```bash
kubectl apply -f orbeon-forms-deployment.yaml
```

You can display information about you Kubernetes contexts, cluster, pods, nodes, and service with the following commands:

```bash
# Contexts information
kubectl config get-contexts

# Cluster information
kubectl cluster-info

# Pods information
kubectl describe pods

# Nodes information
kubectl get nodes -o wide

# Service information
kubectl get service 'orbeon-forms-service'
```

Retrieve the cluster's external/public IP:

```bash
K8S_EXTERNAL_IP=$(kubectl get service 'orbeon-forms-service' --output jsonpath='{.status.loadBalancer.ingress[0].ip}')
```

Orbeon Forms will now be available from the following URL:

```bash
K8S_APP_URL="https://$K8S_EXTERNAL_IP/orbeon"
```

Update the Entra ID redirect URIs with the actual Orbeon Forms URL:

```bash
az ad app update --id "$ENTRA_ID_APP_ID" --web-redirect-uris "$K8S_APP_URL/*"
```

Retrieve the Kubernetes pod name:

```bash
K8S_POD=$(kubectl get pod -o name | head -1)
```

Display and follow the Orbeon Forms logs:

```bash
kubectl logs $K8S_POD -f
```

Alternatively, you can use Azure Container Instances (ACI) for a simpler deployment, but with limited support for file mounts, port mappings, etc.

## Limitations

Here is a list of limitations and possible improvements:

- Groups/roles are returned by Entra ID as IDs, not names.
- The example Bash script has been tested on macOS only, but should work on Linux and Windows, using the Windows Subsystem for Linux (WSL).
- No actual load balancing is configured in the Kubernetes cluster.
- The TLS/SSL configuration should be done at the Application Gateway Ingress Controller level and not at the WildFly level.
- Azure Resource Manager (ARM) templates could be used to automate the deployment.
