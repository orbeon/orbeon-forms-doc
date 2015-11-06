# Production war

<!-- toc -->


## Rationale

The standard Orbeon Forms WAR comes with demo apps and forms. For production, you can safely remove some of that from the WAR file.

## What can be removed

For most deployments, the following can be removed:

- `xforms-jsp`: demo JSP files
- `WEB-INF/resources/apps`: demo apps
- `WEB-INF/resources/forms/orbeon/controls`: some demo forms resources
- `WEB-INF/resources/forms/orbeon/dmv-14`: some demo forms resources
- `orbeon-cli.jar` and `commons-cli-1_0.jar`: for command-line XPL

## Removing Form Builder

Form Builder is packaged as a separate JAR file:

`WEB-INF/lib/orbeon-form-builder.jar`

If you don't need Form Builder in an installation, you can simply remove that JAR file.

## Removing the built-in eXist database

The version of eXist which ships with Orbeon Forms is intended for demo purposes only. If you plan to use eXist, we recommend setting up an external eXist database.

To remove the embedded eXist:

- remove
  - `WEB-INF/lib/exist-*.jar`: the embedded eXist implementation and its dependencies
  - `WEB-INF/exist-data`: data for the embedded eXist XML database
- in `WEB-INF/web.xml`:
  - remove all `<servlet>`, `<servlet-mapping>`, `<filter`, and `<filter-mapping>` which refer to eXist

If you want to point to an external eXist database, set the following property:

```xml
<property
    as="xs:anyURI"
    name="oxf.fr.persistence.exist.uri"
    value="$urlToExistRestService"/>
```

And replace `$urlToExistRestService` with the actual URL of the eXist REST service.

If you don't need an eXist database at all, in `properties-local.xml`, add the following to fully disable the eXist persistence implementation.:

```xml
<property
    as="xs:boolean"
    name="oxf.fr.persistence.exist.active"
    value="false"/>
```

Then set a property to select the persistence implementation you are using, for example, for SQL Server:

```xml
<property
    as="xs:string"
    name="oxf.fr.persistence.provider.*.*.*"
    value="sqlserver"/>
```

