> [[Home]] ▸ Form Runner ▸ [[APIs|Form-Runner ~ APIs]] ▸ [[Persistence| Form Runner ~ APIs ~ Persistence]]

## Related pages

- [[Overview|Form Runner ~ APIs ~ Persistence ~ Overview]]
- [[Search|Form Runner ~ APIs ~ Persistence ~ Search]]
- [[Form Metadata|Form Runner ~ APIs ~ Persistence ~ Forms Metadata]]
- [[Versioning|Form Runner ~ APIs ~ Persistence ~ Versioning]]
- [[Implementing a Persistence Service|Form Runner ~ APIs ~ Persistence ~ Implementing a Persistence Service]]

## Basics

When using GET, PUT and DELETE to deal with resources, the body of HTTP requests just contains the resource to handle.

* GET: request body is empty, response body contains resource, and the `Orbeon-Operations` response header lists the operations that the user can perform on the data (see [Supporting permissions in your persistence API implementation](http://blog.orbeon.com/2013/10/supporting-permissions-in-your.html)). If your implementation of the persistence API supports form versioning and you're returning a deployed form definition, you need to tell the call which version of the form definition you're returning through the `Orbeon-Form-Definition-Version` header.
* PUT: request body contains resource, request response is empty. If your implementation of the persistence API supports form versioning, then when storing a new document, you need to tell the persistence what version of the form definition was used to create this data, so the same version can then be used to subsequently edit the data. This is done by specifying the version in the `Orbeon-Form-Definition-Version` header.
* DELETE: both request and response bodies are empty

When the resource is an XML file (e.g. form.xhtml, data.xml), the persistence layer must return an appropriate XML content-type: `application/xml`, or `application/xhtml+xml`.

## URL parameters

[SINCE: Orbeon Forms 4.2]

When data is stored using PUT, the following URL parameter is set:

* `valid`: whether the data sent satisfies validation rules

Example:

```xml
http://example.org/orbeon/fr/service/persistence/crud/
    orbeon/
    bookshelf/
    data/891ce63e59c17348f6fda273afe28c2b/data.xml?
    valid=true
```

## Publishing from Form Builder

When you create, edit, or read form definition with Form Builder, form definitions are stored as Form Builder _data_. That is, they are stored under:

`/crud/orbeon/builder/data/[FORM_DATA_ID]`

On the other hand, when Form Builder _publishes_ a form definition, it stores it where Form Runner will find it, that is under:

`/crud/[APPLICATION_NAME]/[FORM_NAME]/form`

For example, this is what happens when saving and publishing a form definition acme/demo with a single attachment:

Save:

* PUT /crud/orbeon/builder/data/7b55c9d6f9b058376293e61d9f0d4442e379f717/a29fd47011b2957ef44a62d92995adfdbae03fa9.bin
* PUT /crud/orbeon/builder/data/7b55c9d6f9b058376293e61d9f0d4442e379f717/data.xml

Publish:

* PUT /crud/acme/demo/form/a29fd47011b2957ef44a62d92995adfdbae03fa9.bin
* PUT /crud/acme/demo/form/form.xhtml?document=7b55c9d6f9b058376293e61d9f0d4442e379f717

[SINCE Orbeon Forms 4.6]

When Form Builder publishes a form definition, if versioning is supported by the target persistence layer, it passes a `Orbeon-Form-Definition-Version` header with values:

* `next`: to indicate that the form definition must be published under the next available version
* or a specific version number: to indicate that the form definition must replace the given version

## Deleting all data for an existing form

To remove all instances of form data, issue a DELETE to:

`/crud/[APPLICATION_NAME]/[FORM_NAME]/data/`