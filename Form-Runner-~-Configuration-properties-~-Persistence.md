## Levels of configuration

Storage for the persistence hierarchy can be configured at multiple levels:

* Globally
* For each _application_
    * The application is referred to with an _app name._
* For each _form definition_ within an application
    * The form definition is referred to with a _form name._
* For each _resource type_ (form definition vs. form data)
    * The resource type is referred to with an identifier:
        * _form_: form definition as XHTML,  with optional associated resources (images, PDF template, and other attachments)
        * _data_: form data as XML, with optional associated attachments

This allow you for example to store certain form definition on disk, while storing the associated data, as filled-out by users, in one or more databases.

## Persistence layer configuration

Orbeon Forms 4.0 introduces a level of indirection in the persistence layer configuration: you map the parameters *app*, *form*, and *form definition or data* to a _provider_, and then map that provider to its REST persistence API. This is configured via properties starting with `oxf.fr.persistence.provider`.

By default, eXist is configured for all apps and forms, including form definitions and form data. This is done by associating the `exist` provider:

```xml
<property
    as="xs:string"
    name="oxf.fr.persistence.provider.*.*.*"
    value="exist"/>
```

In each such provider mapping, there are 3 configurable pieces of information. They represent, in this order:

1. A string to specify the Form Runner application name, like `orbeon` or `acme`.
2. A Form Runner form name, like `registration` or `address`.
3. Whether the configuration regards form data (`data`), or the form configuration files (`form`).

Each of those can be a wildcard (`*`). Wildcards allow you to setup Orbeon Forms to use different persistence providers for different app, form, and resource  type combinations.

The properties are interpreted hierarchically and you may specify the configuration more or less specifically:

Property | Explanation
---------|------------
`oxf.fr.persistence.provider.*.*.*`                 | form definitions and form data for all applications
`oxf.fr.persistence.provider.*.*.data`              | form data for all applications
`oxf.fr.persistence.provider.*.*.form`              | form definitions for all applications
`oxf.fr.persistence.provider.orbeon.*.form`         | form definitions for all forms in application "orbeon"
`oxf.fr.persistence.provider.orbeon.*.data`         | form data for all forms in  application "orbeon"
`oxf.fr.persistence.provider.orbeon.contact.*`      | form definitions and data for "orbeon/contact"
`oxf.fr.persistence.provider.orbeon.contact.form`   | form definitions for "orbeon/contact"
`oxf.fr.persistence.provider.orbeon.contact.data`   | form data for "orbeon/contact"

For example some built-in demo forms as well as Form Builder load their form definitions directly from the Orbeon
Forms web application. This is done my using more specific properties (without wildcards):

```xml
<property
    as="xs:string"
    name="oxf.fr.persistence.provider.orbeon.builder.form"
    value="resource"/>
<property
    as="xs:string"
    name="oxf.fr.persistence.provider.orbeon.dmv-14.form"
    value="resource"/>
```

One thing you might notice is that, as far as configuring persistence, you treat Form Builder itself just like another form, with the app name is `orbeon` and the form name is `builder`.

If you want to change the default provider to Oracle, and since a provider named `oracle` is predefined, the following configuration will do just that:

```xml
<property
    as="xs:string"
    name="oxf.fr.persistence.provider.*.*.*"
    value="oracle"/>
```

Because wildcards are used, this property does not override the configuration for the built-in demo forms as well as Form Builder! This is desirable, because the Form Builder implementation itself is not usually something you want to store somewhere else.

In the case of Oracle, the wildcards also allow you to setup Orbeon Forms to use different schemas for different app, form, and resource type combination. For instance you could store everything related to an `hr` app in one Oracle database schema and everything related to another `finance` app in another Oracle database schema. For more on this, see how to setup the persistence layer for [[multiple schemas|Installation ~ Relational Database Setup#with-multiple-schemas]].

Each provider supports standard properties:

```xml
<!-- URI of the provider -->
<property
    as="xs:anyURI"
    name="oxf.fr.persistence.[provider].uri"
    value="[URI to reach the provider]"/>

<!-- Whether the provider is active (used only by the Home page as of 4.4) [SINCE Orbeon Forms 4.4]  -->
<property
    as="xs:boolean"
    name="oxf.fr.persistence.[provider].active"
    value="[true|false]"/>

<!-- Whether autosave is supported [SINCE Orbeon Forms 4.4]  -->
<property
    as="xs:boolean"
    name="oxf.fr.persistence.[provider].autosave"
    value="[true|false]"/>

<!-- Whether user/group permissions are supported [SINCE Orbeon Forms 4.4]  -->
<property
    as="xs:boolean"
    name="oxf.fr.persistence.[provider].permissions"
    value="[true|false]"/>

<!-- Whether versioning is supported [SINCE Orbeon Forms 4.5]  -->
<property
    as="xs:boolean"
    name="oxf.fr.persistence.[provider].versioning"
    value="[true|false]"/>
```

An *active* provider means a provider which is visible to Form Runner. If a provider is marked as not active, by setting the property to `false`, the Form Runner Home page, for example, will not show form definitions associated with this provider.

Each provider may have specific configuration properties. For the latest settings, see
[`properties-form-runner.xml`](https://github.com/orbeon/orbeon-forms/blob/master/src/resources-packaged/config/properties-form-runner.xml#L17).

_NOTE: You can't name a persistence provider `provider`._

## See also

- [Persistence API](https://sites.google.com/a/orbeon.com/forms/doc/developer-guide/form-
runner/persistence-api)