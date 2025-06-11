# Persistence API

## Children pages

- [CRUD](crud.md)
- [Reindexing](reindexing.md)
- [Search](search.md)
- [List form data attachments](list-form-data-attachments.md)
- [Form metadata](forms-metadata.md)
- [Caching](caching.md)
- [Versioning](versioning.md)
- [Lease API](lease.md)
- [Reindexing API](reindexing.md)
- [Revision History API](revision-history.md)
- [Zip Export API](export-zip.md)
- [Custom persistence providers](custom-persistence-providers.md)

## Introduction

Orbeon Forms *form definitions* and *form data* are stored via an abstraction called the **persistence API**. This allows storing form definitions and form data for all or specific forms in different places. For example, you could store your form definitions into Oracle, and your form data into your own, custom database.

## Built-in support

Out of the box, Orbeon Forms provides an implementation of the persistence API for a number of databases, and other implementations are provided by third parties. See [Database Support](../../../form-runner/persistence/db-support.md) for details.

## Configuration

For a description of the terminology and configuration properties, see [Form Runner Configuration Properties](../../../configuration/properties/form-runner.md).

## REST-based

The Form Runner/Form Builder persistence API is based on REST. This means that Form Builder and Form Runner use HTTP to communicate with a persistence layer implementation.

Following the REST philosophy, HTTP methods are used to determine what CRUD operation to perform:

* `GET`: read a resource
* `PUT`: create or update a resource
* `DELETE`: delete a resource

For example, to deal with form data:

/fr/service/persistence

| Operation | HTTP Method | URL                                                                                       |
|-----------|-------------|-------------------------------------------------------------------------------------------|
| Create    | `PUT`       | <code>/fr/service/persistence/crud/$app/$form/(data&#124;draft)/$document/data.xml</code> |
| Read      | `GET`       | <code>/fr/service/persistence/crud/$app/$form/(data&#124;draft)/$document/data.xml</code> |
| Update    | `PUT`       | <code>/fr/service/persistence/crud/$app/$form/(data&#124;draft)/$document/data.xml</code> |
| Delete    | `DELETE`    | <code>/fr/service/persistence/crud/$app/$form/(data&#124;draft)/$document/data.xml</code> |
| Search    | `POST`      | `/fr/service/persistence/search/$app/$form`                                               |
| Metadata  | `GET`       | `/fr/service/persistence/form`                                                            |

where:

- `$app` is the form definition's application name
- `$form` is the form definition's form name
- `$document` is the form data's document id

## Optional features

[SINCE Orbeon Forms 4.4]

- Drafts – If your persistence provider does not support drafts, you can specifically turn off the autosave feature for that persistence layer with the `oxf.fr.persistence.*.autosave` property. See [Autosave](../../persistence/autosave.md) and [persistence layer configuration](../../../configuration/properties/persistence.md) for details.
- User/group permissions – Similarly, if your persistence provider does not support user/group permissions, you can specifically turn off the permissions feature for that persistence layer with the `oxf.fr.persistence.*.permissions` property. See [persistence layer configuration](../../../configuration/properties/persistence.md) for details.

## Virtual hierarchy of data

Form Runner/Form Builder access data under a virtual hierarchy of URLs, not unlike directories or folders in a filesystem. However, this hierarchy can be physically located in different places:

* An XML database, like eXist.
* A disk-based filesystem.
* Your own system, which you can implement on top of a database or other type of storage.

Following XML database technology, we use the terms **collections** and **resources** instead of directories and files.

The hierarchy looks like this:


- `acme/` → app name
    - `address/` → form name
        - `form/` → form definition
            - `form.xhtml` → the form definition proper
            - `e9ed0270896ac9012612c570698c6955d8c0de67.bin` → PDF template attached to form definition
            - `d7419b1467b0c0c4df3ed415aaa788325817d478.bin` → image attached to form definition
            - `873ee4175a820fe3bac2fabb808b1ce9927d7f3a.bin` → other file attached to form definition
        - `data/` → form data
            - `74cb880847c638577e7afa15911efc99ceaf2dcb` → one instance of form data
                - `data.xml` → form data proper
                - `c738877f52110302e407e9bba87e0ef92531b357.bin` → image attached to form data
                - `a502ea5404ba8afb9e364a3e73dbd477eb6e40b8.bin` → other file attached to form data
        - `4abca3daf30523fd17c3699a6044f2e53b21f933` → another instance of form data
            - `data.xml`
        - `draft/` → draft form data
            - `9438f77ff8326c0c206b02f090f818841affd98a` → one instance of draft form data
                - `data.xml`
                - …
    - `contract/`
        - `form/`
            - …
        - `data/`
            - …
- `foobar/`
    - …

The hierarchy is organized as follows:

* At the top-level there is one collection per application
* Within an application collection, there is one collection per form definition
* Within a form definition  there is one collection called `form` for the form definitions produced by Form Builder, and one collection called `data` for form data produced by Form Runner
* Each `form` collection contains:
    * `form.xhtml`: the main form definition, which is an XHTML+XForms resource
    * optional attachments, such as images, PDF files, and other file attachments uploaded by the form author when editing the form definition
* Each `data` collection contains one collection for each form data id, identified by an automatically-generated UUID
* The `draft` collection is analogous to the "data" collection, but used by [autosave](../../../form-runner/persistence/autosave.md) to store form data before users explicitly save it
    * implementations of the persistence API are expected to remove the draft (with the corresponding attachments) when the corresponding data is saved
* Each form data collection contains:
    * `data.xml`: the main form data document
    * optional attachments, such as images uploaded by the user when editing the form data

## Headers

All persistence URLs are called with headers matching configuration properties for the given persistence layer implementation. For example:

* eXist
    * property:

        ```xml
        <property
            as="xs:anyURI"
            name="oxf.fr.persistence.exist.exist-uri"
            value="/exist/rest/db/orbeon/fr">
        ```

    * header:

        ```
        Orbeon-Exist-Uri: /exist/rest/db/orbeon/fr
        ```

* Oracle
    * properties:

        ```xml
        <property
            as="xs:string"
            name="oxf.fr.persistence.oracle.datasource"
            value="oracle">
        <property
            as="xs:boolean
            name=" oxf.fr.persistence.oracle.create-flat-view"
            value="false">
        ```

    * headers:

        ```
        Orbeon-Datasource: oracle
        Orbeon-Create-Flat-View: false
        ```

* MySQL
    * property:

        ```xml
        <property
            as="xs:string"
            name="oxf.fr.persistence.mysql.datasource"
            value="mysql">
        ```

    * header:

        ```
        Orbeon-Datasource: mysql
        ```

* DB2
    * property:

        ```xml
        <property
            as="xs:string"
            name="oxf.fr.persistence.db2.datasource"
            value="db2">
        ```

    * header:

        ```
        Orbeon-Datasource: db2
        ```
