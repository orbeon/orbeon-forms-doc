# Properties

<!-- toc -->

## Overview

Orbeon Forms is configured via _configuration properties_. They are usually setup in a file called `properties-local.xml` and stored in the Orbeon Forms WAR file as:

```
WEB-INF/resources/config/properties-local.xml
```

Orbeon Forms will do a certain number of things out of the box without you having to setup anything in your `properties-local.xml`. But if you want to change the default behavior (and it is likely you will want to in order to setup access control, database access, configure buttons, etc.), you will need to make changes to that file. This page describes the basics of that process.

## Documentation for specific properties

Each Orbeon Forms subsystem defines its own properties. They are documented in the following pages:

- [General properties](general.md)
- [XForms properties](xforms.md)
- [Form Runner properties](form-runner.md)
    - [Persistence properties](persistence.md)
- [Form Builder properties](form-builder.md)

## What's in a property

 A property is made of:

- a _type_, such as `xs:boolean`
- a _name_, such as `oxf.resources.versioned`
- a _value_, such as `true`
- optionally, and rarely, a _processor name_, such as  `oxf:page-flow`, which refers to an XPL processor name

This is typically put together like this in `properties-local.xml`:

```xml
<property
  as="xs:boolean"
  name="oxf.resources.versioned"
  value="true"/>
```

Some properties support wildcards, for example:

```
oxf.fr.persistence.app.uri.*.*.*
```

You can also place longer property values inline [SINCE Orbeon Forms 4.6]: 

```xml
<property as="xs:string" name="oxf.fr.detail.process.save-final.*.*">
    require-uploads
    then validate-all
    then save
    then success-message("save-success")
    recover error-message("database-error")
</property>
```

Changes to most properties are taken into account as soon as you save your property file (e.g. `properties-local.xml`), however changes to some properties are only taken into account when the server is first started.

## Categories of properties

Properties fall in two categories:

- *standard*, which means they are defined by Orbeon Forms
- *custom*, which means they are defined by form authors, administrators or integrators

All standard properties have standard values defined in built-in property files, described in the pages linked above, and can be overridden when needed.

In general, here is how you deal with properties:

- you look up the documentation for a given property
- if needed, you set or override the property in `properties-local.xml`

## Setting and overriding properties

You can change properties by editing `properties-local.xml`. That file goes in the directory `WEB-INF/resources/config`, inside the Orbeon Forms web app.

If that file doesn't exist yet in your installation of Orbeon Forms, you can create it by renaming or copying the file `properties-local.xml.template` into `properties-local.xml`. At this point, your `properties-local.xml` will only contain an opening `<properties>` tag and closing `</properties>` tag, and you'll want to edit it to add properties between those two tags, as in:

```xml
<properties xmlns:xs="http://www.w3.org/2001/XMLSchema"
            xmlns:oxf="http://www.orbeon.com/oxf/processors">
    <property as="xs:string"  
              name="oxf.fr.persistence.provider.*.*.*"
              value="oracle"/>
</properties>
```

## Wildcards in properties

Property names may be defined using wildcards. A property name is assumed to be built as a series of path elements separated by  `.`  characters. A path element may contain a  `*`  character instead of an actual path element value.

```xml
<property
  as="xs:anyURI"
  name="oxf.fr.persistence.app.uri.*.*.*"
  value="/fr/service/exist"/>

<property
  as="xs:anyURI"
  name="oxf.fr.persistence.app.uri.*.*.data"
  value="/fr/service/oracle"/>

<property
  as="xs:anyURI"
  name="oxf.fr.persistence.app.uri.orbeon.builder.form"
  value="/fr/service/resource"/>
```

In this example:

* If the property name `oxf.fr.persistence.app.uri.orbeon.builder.form`  is requested, the value  `/fr/service/resource`  is returned (exact match).
* If the property name `oxf.fr.persistence.app.uri.orbeon.foobar.data`  is requested, the value  `/fr/service/oracle`  is returned, because the path elements  `orbeon`,  `foobar`  match wildcards, and  `data`  matches the last path element.
* If the property name `oxf.fr.persistence.app.uri.orbeon.foobar.form`  is requested, the value  `/fr/service/exist`  is returned, because the path elements  `orbeon`,  `foobar`  and  `form`  all match wildcards.

This allows creating hierarchical properties with generic defaults and more specific values.

In general these are used with Form Runner or Form Builder and in these cases:

* The first wildcard matches a forms "application name"
* The second wildcard matches a forms "form name"

There is a precedence order with wildcards:

1. an exact match is checked first and always wins if found
2. a wildcard match is done then, starting from the left

So:

* `foo.bar`  wins over  `*.*`,  `*.bar`, and  `foo.*`
* `foo.*`  wins over  `*.bar`

## Built-in property files

The default values for those properties are provided in the following files, which are stored in `orbeon-resources-private.jar`:

* `config/properties-dev.xml`
    * root of  `dev`  mode properties
* `config/properties-prod.xml`
    * root of  `prod`  mode properties
* `config/properties-base.xml`
    * base Orbeon Forms properties
* `config/properties-xforms.xml`
* `config/properties-form-runner.xml`
* `config/properties-form-builder.xml`
In general, you shouldn't modify these files.

## Properties types

Properties have a documented type, which must be one of the following:

* `xs:string`
* `xs:boolean`
* `xs:integer`
* `xs:anyURI`
* `xs:QName`
* `xs:date`
* `xs:dateTime`
* `xs:NMTOKENS`

## Defining your own properties

In addition to the standard properties, you can define your own properties. You can then access them from:

* XPath expressions in XForms with [`xxf:property()`](../../xforms/xpath/extension-core.md#xxfproperty).
* XPath expressions in XPL with `p:property()`, where the prefix `p` is mapped to the namespace `http://www.orbeon.com/oxf/pipeline`.
* XPath expressions in XSLT with `pipeline:property()`, where the prefix pipeline is mapped to namespace `java:org.orbeon.oxf.processor.pipeline.PipelineFunctionLibrary`.

In all cases, for security reasons, those functions won't return the value of properties that contain the string "password" in the name of the property.

## Different properties for dev vs. production

In general, you can define all your custom properties in `properties-local.xml`. However, if the value of a property needs to differ depending on the environment, e.g. the value is different for `dev` and `prod`, then you can define those properties twice, in `properties-local-dev.xml` *and* `properties-local-prod.xml`, and have different values defined for the property depending on the file. In that case, you would still keep your custom properties that don't differ depending on the environment in `properties-local.xml`.

- Properties you define in `properties-local-dev.xml` apply in `dev` run mode only, and in that case override properties in `properties-local.xml`.
- Properties you define in `properties-local-prod.xml` apply in `prod` run mode only, and in that case override properties in `properties-local.xml`.

## For contributors: properties subsystem initialization

The properties sub-system is initialized after the [Resource Manager][2] (the properties being read like any other Orbeon Forms resources). By default, the following top-level URL is loaded:

* web app in `prod` mode:  `oxf:/config/properties-prod.xml`
* web app in  `dev`  mode:  `oxf:/config/properties-dev.xml`
* web app (prior to 2012-05-03):  `oxf:/config/properties.xml`
* command-line:  `oxf:/properties.xml`

Property files support inclusions via XInclude. This is the mechanism used by Orbeon Forms to load  all the secondary property files.

[1]: http://wiki.orbeon.com/forms/doc/developer-guide/xforms-xpath-functions#TOC-xxf:property-
[2]: http://wiki.orbeon.com/forms/doc/developer-guide/resource-managers
