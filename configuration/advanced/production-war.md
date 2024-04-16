# Creating a production WAR

## Rationale

The standard Orbeon Forms WAR comes with demo apps and forms. For production, you can safely remove some of that from the WAR file.

## What can be removed

For most deployments, the following can be removed:

- `xforms-jsp`: demo JSP files
- `WEB-INF/resources/apps`: demo apps
- `WEB-INF/resources/forms/orbeon/controls`: some demo forms resources
    - NOTE: These have been removed in recent versions of Orbeon Forms.
- `WEB-INF/resources/forms/orbeon/dmv-14`: some demo forms resources
    - NOTE: These have been removed in recent versions of Orbeon Forms.
- `orbeon-cli.jar` and `commons-cli-1_0.jar`: for command-line XPL
    - NOTE: These have been removed in recent versions of Orbeon Forms.

## Removing Form Builder

Form Builder is packaged as a separate JAR file:

`WEB-INF/lib/orbeon-form-builder.jar`

If you don't need Form Builder in an installation, you can simply remove that JAR file.

## Removing the built-in SQLite database

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

Out-of-the-box, Orbeon Forms includes an SQLite embedded database with multiple demo forms. This setup is designed for a quick start, but for development or production use, you should configure Orbeon Forms to utilize a separate relational database. For more information, see [Relational Database](/form-runner/persistence/relational-db.md).

To disable the `sqlite` embedded database and demo forms, add the following property:

```xml
<property
    as="xs:boolean"
    name="oxf.fr.persistence.sqlite.active"
    value="false"/>
```

You can leave the SQLite library and database in place if you are not using them, but you can also opt to remove them. If you do so, in addition to the above configuration property, you remove the following:

- `WEB-INF/lib/sqlite-jdbc-*.jar`: the SQLite JDBC driver and implementation
- `WEB-INF/orbeon-demo.sqlite`: the SQLite database with demo forms


## Removing the built-in eXist database

### Deprecation

[SINCE Orbeon Forms 2019.1]

Using the eXist database with Orbeon Forms is deprecated. We recommend using one of the supported [relational databases](/form-runner/persistence/relational-db.md) for production.

### Removal

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

The eXist database is no longer included in the standard Orbeon Forms WAR file. If you are using that version or newer, you don't need to explicitly remove eXist.

### Steps

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

## See also

- [Database setup](/installation/README.md#database-setup)
- [Using Form Runner with a relational database](/form-runner/persistence/relational-db.md)
