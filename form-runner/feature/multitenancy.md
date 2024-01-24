# Multitenancy

## Introduction

Multitenancy consists in using a single Orbeon Forms installation to handle multiple "tenants" such as organizations or entities within a single organization, while isolating those organizations or entities from each other. For example, a form relevant to company Acme will not be available to a user with access to forms from company Ajax only.

## Orbeon Forms support

Orbeon Forms has some support for multitenancy thanks to the concept of *application name* (see [Terminology](/form-runner/overview/multitenancy.md)). For multitenancy, you use the application name to refer to an organization or entity, and you enable access control to isolate those entities.

## Configuration

### Roles

Make sure that there are roles identifying users from one or the other organization. For example, users from company Acme might have the `acme-user` role, while users from company Ajax might have the `ajax-user` role.

See also [Providing information about the user](/form-runner/access-control/users.md).

### Isolating forms with Form Runner

Now that users have a role identifying them, it is possible to isolate users at the Form Runner level. There are two separate configuration you can do to enforce that.

First, you can set individual form permissions (see also [Setting permissions](/form-runner/access-control/deployed-forms#setting-permissions.md)). For a given form:

1. In Form Builder, enable permission for that form definition.
2. Indicate that all operations require the role `acme-user`, for example.
3. Publish the form definition.

This approach is flexible but it has one drawback: there is not a central location to enforce the role for all forms with a given app name, so you have to be careful to set permissions for all the forms appropriately (see also [RFE #1860](https://github.com/orbeon/orbeon-forms/issues/1860)).
 
Second, for the reason above, we also recommend enabling protection at the `web.xml` level:

```xml
<security-constraint>
    <web-resource-collection>
        <web-resource-name>Acme forms</web-resource-name>
        <url-pattern>/fr/acme/*</url-pattern>
    </web-resource-collection>
    <auth-constraint>
        <role-name>acme-user</role-name>
    </auth-constraint>
</security-constraint>
<security-constraint>
    <web-resource-collection>
        <web-resource-name>Ajax forms</web-resource-name>
        <url-pattern>/fr/ajax/*</url-pattern>
    </web-resource-collection>
    <auth-constraint>
        <role-name>ajax-user</role-name>
    </auth-constraint>
</security-constraint>
```

The example configuration above enforces that the `acme-user` and `ajax-user` roles must be present to access the given Form Runner URLs. See also [Linking](/form-runner/link-embed/linking.md#paths).

### Isolating forms with Form Builder

You might also want to isolate forms at the Form Builder level. This means that a use from company Acme can only see and publish form definitions for company Acme.

You do this with settings in `form-builder-permissions.xml`, for example:

```xml
<roles>
  <role name="acme-user" app="acme" form="*"/>
  <role name="ajax-user" app="ajax" form="*"/>
</roles>
```

The configuration above allows only users with the given roles to create, view, edit, and publish form definitions associated with their own organization.

*NOTE: If your forms are authored independently from a given organization, you don't have to implement this part: simply give permission to Form Builder to the appropriate user or role.* 

See also [Form Builder permissions](/form-runner/access-control/editing-forms.md).

### Database configuration

Optionally, you can setup different database providers for each application name. For example:

```xml
<property 
    as="xs:string" 
    name="oxf.fr.persistence.provider.oracle.acme.*" 
    value="oracle"/>

<property 
    as="xs:string" 
    name="oxf.fr.persistence.provider.oracle.ajax.*" 
    value="mysql"/>
```

The above configuration allows you to point to two completely different databases for published form definitions and their form data.

Unpublished Form Builder form definitions must be stored in the same database provider at this point. The following configuration allows you to explicitly specify which:


```xml
<property 
    as="xs:string" 
    name="oxf.fr.persistence.provider.oracle.orbeon.builder" 
    value="oracle"/>
```

In general, however, you don't want to have entirely different database providers, but you would like two different database schema in the same database or two databases of the same type.

This requires setting more properties. Here is an example with MySQL, which allows you to use two separate MySQL datasources:

```xml
<property as="xs:anyURI"  name="oxf.fr.persistence.mysql_acme.uri"          value="/fr/service/mysql"/>
<property as="xs:string"  name="oxf.fr.persistence.mysql_acme.datasource"   value="mysql_acme"/>
<property as="xs:boolean" name="oxf.fr.persistence.mysql_acme.autosave"     value="true"/>
<property as="xs:boolean" name="oxf.fr.persistence.mysql_acme.permissions"  value="true"/>
<property as="xs:boolean" name="oxf.fr.persistence.mysql_acme.versioning"   value="true"/>
<property as="xs:boolean" name="oxf.fr.persistence.mysql_acme.lease"        value="true"/>
<property as="xs:boolean" name="oxf.fr.persistence.mysql_acme.reindex"      value="true"/>
<property as="xs:boolean" name="oxf.fr.persistence.mysql_acme.reencrypt"    value="true"/>
<property as="xs:boolean" name="oxf.fr.persistence.mysql_acme.sort"         value="true"/>
<property as="xs:boolean" name="oxf.fr.persistence.mysql_acme.active"       value="true"/>

<property as="xs:anyURI"  name="oxf.fr.persistence.mysql_ajax.uri"          value="/fr/service/mysql"/>
<property as="xs:string"  name="oxf.fr.persistence.mysql_ajax.datasource"   value="mysql_ajax"/>
<property as="xs:boolean" name="oxf.fr.persistence.mysql_ajax.autosave"     value="true"/>
<property as="xs:boolean" name="oxf.fr.persistence.mysql_ajax.permissions"  value="true"/>
<property as="xs:boolean" name="oxf.fr.persistence.mysql_ajax.versioning"   value="true"/>
<property as="xs:boolean" name="oxf.fr.persistence.mysql_ajax.lease"        value="true"/>
<property as="xs:boolean" name="oxf.fr.persistence.mysql_ajax.reindex"      value="true"/>
<property as="xs:boolean" name="oxf.fr.persistence.mysql_ajax.reencrypt"    value="true"/>
<property as="xs:boolean" name="oxf.fr.persistence.mysql_ajax.sort"         value="true"/>
<property as="xs:boolean" name="oxf.fr.persistence.mysql_ajax.active"       value="true"/>
```

Finally, you do not have to use two separate databases or datasources: you can also keep all the data in the same database schema, which simplifies the configuration.

See also [Using Form Runner with a relational database](/form-runner/persistence/relational-db.md).

## Conclusion

By setting roles and the appropriate Form Runner, Form Builder and database configurations, you can completely isolate organizations.

## See also 

- [Access control and permissions](/form-runner/access-control/README.md)
- [Linking](/form-runner/link-embed/linking.md)
- [Using Form Runner with a relational database](/form-runner/persistence/relational-db.md)
