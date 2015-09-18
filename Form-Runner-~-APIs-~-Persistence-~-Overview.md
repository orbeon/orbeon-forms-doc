> [[Home]] ▸ Form Runner ▸ [[APIs|Form-Runner ~ APIs]] ▸ [[Persistence| Form Runner ~ APIs ~ Persistence]]

## Related pages

- [[CRUD|Form Runner ~ APIs ~ Persistence ~ CRUD]]
- [[Search|Form Runner ~ APIs ~ Persistence ~ Search]]
- [[Form Metadata|Form Runner ~ APIs ~ Persistence ~ Forms Metadata]]
- [[Versioning|Form Runner ~ APIs ~ Persistence ~ Versioning]]
- [[Implementing a Persistence Service|Form Runner ~ APIs ~ Persistence ~ Implementing a Persistence Service]]

## REST-based

The Form Runner/Form Builder persistence API is based on REST. This means that Form Builder and Form Runner use HTTP to communicate with a persistence layer implementation.

Following the REST philosophy, HTTP methods are used to determine what CRUD operation to perform:

* `GET`: read a resource
* `PUT`: create or update a resource
* `DELETE`: delete a resource

For example, to deal with form data:

| Operation | HTTP Method | URL |
| --------- | ----------- | --- |
| Create    | PUT         | `/crud/$app/$form/(data|draft)/$doc/data.xml` |
| Read      | GET         | `/crud/$app/$form/(data|draft)/$doc/data.xml` |
| Update    | PUT         | `/crud/$app/$form/(data|draft)/$doc/data.xml` |
| Delete    | DELETE      | `/crud/$app/$form/(data|draft)/$doc/data.xml` |
| Search    | POST        | `/search/$app/$form` |
| Metadata  | GET         | `/form` |

where:

- `$app` is the form definition's application name
- `$form` is the form definition's form name
- `$doc` is the form definition's document id

## Optional features

[SINCE: Orbeon Forms 4.4]

If your persistence provider does not support drafts, you can specifically turn off the autosave feature for that persistence layer with the `oxf.fr.persistence.*.autosave` property. See [[Autosave|Form Runner ~ Autosave]] and [[persistence layer configuration|Form Runner ~ Configuration properties ~ Persistence]] for details.

Similarly, if your persistence provider does not support user/group permissions, you can specifically turn off the permissions feature for that persistence layer with the `oxf.fr.persistence.*.permissions` property. See [[persistence layer configuration|Form Runner ~ Configuration properties ~ Persistence]] for details.

## Virtual hierarchy of data

Form Runner/Form Builder access data under a virtual hierarchy or URLs, not unlike directories or folders in a filesystem. However this hierarchy can be physically located in different places:

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
* Within a form definition  there is one collection called "form" for the form definitions produced by Form Builder, and one collection called "data" for form data produced by Form Runner
* Each "form" collection contains:
    * form.xhtml: the main form definition, which is an XHTML+XForms resource
    * optional attachments, such as images, PDF files, and other file attachments uploaded by the form author when editing the form definition
* Each "data" collection contains one collection for each form data id, identified by an automatically-generated UUID
* The "draft" collection is analogous to the "data" collection, but used by the [[Autosave|Form Runner ~ Autosave]] to store form data before users explicitly save it
    * implementations of the persistence API are expected to remove the draft (with the corresponding attachments) when the corresponding data is saved
* Each form data collection contains:
    * data.xml: the main form data document
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