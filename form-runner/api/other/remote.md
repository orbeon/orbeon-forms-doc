# Remote server APIs

## Availability

[SINCE Orbeon Forms 2025.1]

This is an Orbeon Forms PE feature.

## Purpose

The remote server APIs allow you to push form definitions to or pull form definitions from a remote server configured in the `oxf.fr.home.remote-servers` property. These APIs simplify the process of synchronizing form definitions between different Orbeon Forms instances. They are also used internally by the [Forms Admin page](/form-runner/feature/forms-admin-page.md) when pushing to or pulling from a remote server.

## Configuration

Before using these APIs, configure the remote servers using the `oxf.fr.home.remote-servers` property. If you already have a configuration in place (for instance, for the Forms Admin page), add `"api-access": true` to any servers you wish to access through these APIs. For more details, see the [Remote servers configuration](/configuration/properties/form-runner.md#remote-servers) documentation.

## Push to remote API

### Purpose

The push to remote API reads a form definition from the local server and publishes it to a configured remote server, including all attachments.

### Interface

- URL: `/fr/service/push-to-remote/$app/$form`
    - `$app`: the form definition's application name
    - `$form`: the form definition's form name
- Method: `POST`
- Request body: empty
- URL parameters:
    - `remote-server`: required, the name of the remote server as configured in `oxf.fr.home.remote-servers`
- Request headers:
    - `Orbeon-Form-Definition-Version`: required, the version number of the form definition to push
    - `Authorization`: optional, credentials for the local server (standard HTTP Basic authentication)
    - `Orbeon-Remote-Authorization`: optional, credentials for the remote server (HTTP Basic authentication format)

### Example with curl

The following pushes version 1 of the `acme/order` form definition to the remote server named `prod`, using `remote:remote` as credentials on the remote:

```
curl -v \
    --request POST \
    --url "http://localhost:8080/orbeon/fr/service/push-to-remote/acme/order" \
    --url-query "remote-server=prod" \
    --header "Orbeon-Form-Definition-Version: 1" \
    --header "Orbeon-Remote-Authorization: Basic cmVtb3RlOnJlbW90ZQ=="
```

## Pull from remote API

### Purpose

The pull from remote API reads a form definition from a configured remote server and publishes it to the local server, including all attachments.

### Interface

- URL: `/fr/service/pull-from-remote/$app/$form`
    - `$app`: the form definition's application name
    - `$form`: the form definition's form name
- Method: `POST`
- Request body: empty
- URL parameters:
    - `remote-server`: required, the name of the remote server as configured in `oxf.fr.home.remote-servers`
- Request headers:
    - `Orbeon-Form-Definition-Version`: required, the version number of the form definition to pull
    - `Authorization`: optional, credentials for the local server (standard HTTP Basic authentication)
    - `Orbeon-Remote-Authorization`: optional, credentials for the remote server (HTTP Basic authentication format)

### Example with curl

The following pulls version 1 of the `acme/order` form definition from the remote server named `prod`, using `remote:remote` as credentials on the remote:

```
curl -v \
    --request POST \
    --url "http://localhost:8080/orbeon/fr/service/pull-from-remote/acme/order" \
    --url-query "remote-server=prod" \
    --header "Orbeon-Form-Definition-Version: 1" \
    --header "Orbeon-Remote-Authorization: Basic cmVtb3RlOnJlbW90ZQ=="
```

## Response

- Success: HTTP status code `200` (OK)
- Errors:
    - `400` (Bad Request): missing required parameters or invalid configuration
    - `403` (Forbidden): the remote server does not allow API access
    - `404` (Not Found): the form definition was not found on the remote server
    - Other status codes may be returned based on the underlying operations

## See also

- [Forms Admin page](/form-runner/feature/forms-admin-page.md)
- [Publish form definition API](publish.md)
- [CRUD API](../persistence/crud.md)
