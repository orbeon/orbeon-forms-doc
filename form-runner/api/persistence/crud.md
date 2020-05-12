# CRUD API

## Service endpoint

| Operation | HTTP Method | URL |
| --------- | ----------- | --- |
| Create    | `PUT`       | <code>/fr/service/persistence/crud/$app/$form/(data&#124;draft)/$doc/data.xml</code> |
| Read      | `GET`       | <code>/fr/service/persistence/crud/$app/$form/(data&#124;draft)/$doc/data.xml</code> |
| Update    | `PUT`       | <code>/fr/service/persistence/crud/$app/$form/(data&#124;draft)/$doc/data.xml</code> |
| Delete    | `DELETE`    | <code>/fr/service/persistence/crud/$app/$form/(data&#124;draft)/$doc/data.xml</code> |

## Basics

### GET

- The request body is empty and response body contains resource.
- [SINCE Orbeon Forms 4.5] The `Orbeon-Operations` response header lists the operations that the user can perform on the data (see [Supporting permissions in your persistence API implementation](https://blog.orbeon.com/2013/10/supporting-permissions-in-your.html)).
- [SINCE Orbeon Forms 4.5] If the implementation of the persistence API supports form versioning, and the request is for a form definition, the `Orbeon-Form-Definition-Version` request header tells which version of the form definition is requested.
    
### PUT

- The request body contains the resource to store.
- The `Content-Type` must contain the content-type of the resource to store. For XML in particular, pass `application/xml`.
- The request response is empty.
- If your implementation of the persistence API supports form versioning, then when storing a new document, you need to tell the persistence what version of the form definition was used to create this data, so the same version can then be used to subsequently edit the data. This is done by specifying the version in the `Orbeon-Form-Definition-Version` header.

### DELETE

- Both request and response bodies are empty.

### Content-Types

When the resource is an XML file (e.g. `form.xhtml`, `data.xml`), the persistence layer must return an appropriate XML content-type: `application/xml`, or `application/xhtml+xml`.

### Form Builder version number

For callers, when `PUT`ting data under the path `/orbeon/builder/data/`, which means storing unpublished form definitions and form definition attachments, always set `Orbeon-Form-Definition-Version` to the value `1`, or omit the versioning header (which defaults to 1 for new data, and to the latest version of the resource for existing data). This is because Form Builder stores its unpublished form definitions like a regular published form would, but in effect there is only one "version" of Form Builder.

### Constraints

For a particular application name and form name, the document id must be unique. The document id cannot be reused across form definition versions. This allows the document id to be used to infer the form definition version number when retrieving data for a given application name and form name.

### Updating a resource

When using `PUT` to update a resource (whether form data or attachment), the built-in relational persistence layer checks that the version number provided matches the existing version number in the database. Custom persistence layers should do the same.

For example, if you `PUT` an attachment with path (indented for legibility):

```
/fr/service/persistence/crud
    /acme
    /order
    /data
    /fc4c32532e8d35a2d0b84e2cf076bb070e9c1e8e
    /8bf211aef805f1354129ee47cc0964d256ba7cae.bin
```

and pass the header:

```
Orbeon-Form-Definition-Version: 3
``` 

and there is already, in the database, such an attachment that was created with version 3, the request is successful and the resource is updated.

On the other hand if, for the same resource, you pass the header:

```
Orbeon-Form-Definition-Version: 4
```

the persistence layer returns a `400` "Bad Request" HTTP status code.
  

## Examples using curl

The following examples use the [curl](https://curl.haxx.se/) command-line utility. They are indented on multiple lines for clarity but in practice each command must be written on a single line.

The service must be open for these examples to work.
See [Authorization of pages and services](../../../xml-platform/controller/authorization-of-pages-and-services.md).

The general format of the a path to access form data is:

```
/fr/service/persistence/crud/$app/$form/(data|draft)/$doc/data.xml
```

So with the following assumptions:

- app name (`$app`): `acme`
- form name (`$form`): `order`
- document id (`$doc`): `fc4c32532e8d35a2d0b84e2cf076bb070e9c1e8e`

then you can `PUT` form data as follows:

```
curl
  -v
  -k
  -d @mydata.xml
  -H "Content-Type: application/xml"
  -H "Orbeon-Form-Definition-Version: 1"
  -X PUT
  http://localhost:8080/orbeon/fr/service/persistence/crud/acme/order/data/fc4c32532e8d35a2d0b84e2cf076bb070e9c1e8e/data.xml
```

The following retrieves the data (the `GET` method is used implicitly by curl):

```
curl 
  -v 
  -k 
  -H "Orbeon-Form-Definition-Version: 1" 
  http://localhost:8080/orbeon/fr/service/persistence/crud/acme/order/data/fc4c32532e8d35a2d0b84e2cf076bb070e9c1e8e/data.xml
```

The same API is used by Form Builder to store in-progress (unpublished) form definitions under the `orbeon/builder` app/form names. So with the following assumptions:

- app name (`$app`): `orbeon`
- form name (`$form`): `builder`
- document id (`$doc`): `7b55c9d6f9b058376293e61d9f0d4442e379f717`

then you can `PUT` a form definition so that it's available to Form Builder:

```
curl
  -v
  -k
  -d @mydata.xml
  -H "Content-Type: application/xml"
  -H "Orbeon-Form-Definition-Version: 1"
  -X PUT
  http://localhost:8080/orbeon/fr/service/persistence/crud/orbeon/builder/data/7b55c9d6f9b058376293e61d9f0d4442e379f717/data.xml
```

The following retrieves the form definition (the `GET` method is used implicitly):

```
curl 
  -v 
  -k 
  -H "Orbeon-Form-Definition-Version: 1" 
  http://localhost:8080/orbeon/fr/service/persistence/crud/orbeon/builder/data/7b55c9d6f9b058376293e61d9f0d4442e379f717/data.xml
```

## URL parameters

### Supported by the persistence implementation

[SINCE Orbeon Forms 2017.1]

- `nonrelevant`
    - when using `GET` for data only
    - values
        - `remove`: remove all XML data elements with attribute `fr:relevant="false"`
        - `keep`: [SINCE Orbeon Forms 2018.1] do not remove XML data elements
        - missing: do not remove XML data elements
        
_NOTE: This property only operates properly on data stored with Orbeon Forms 2017.1 and newer, as only Orbeon Forms 2017.1 and newer stores data with the `fr:relevant="false"` annotation._

### Passed by Form Runner

[SINCE Orbeon Forms 4.2]

The following URL parameters are set by Form Runner:

- `valid`
    - passed when data is stored using `PUT` only
    - values
        - `true`: data sent satisfies validation rules
        - `false`: data sent does not satisfy validation rules
    
This allows a persistence implementation to store this information if desired.

Example:

```xml
http://example.org/orbeon/fr/service/persistence/crud/
    orbeon/
    bookshelf/
    data/891ce63e59c17348f6fda273afe28c2b/data.xml?
    valid=true
```

## Publishing from Form Builder

When you create, edit, or read form definition with Form Builder, those are considered *unpublished* form definitions and are stored as Form Builder *data*. Those are . They are stored under the path:

`/crud/orbeon/builder/data/$doc`

On the other hand, when Form Builder *publishes* a form definition, Form Builder first makes modifications to the form definition (such as adding section templates) and stores it where Form Runner will find it, under the path:

```
/crud/$app/$form/form
```

For example, this is what happens when saving and publishing a form definition `acme/order` with a single attachment (for example for a PDF template or static image):

Save:

- `PUT /crud/orbeon/builder/data/7b55c9d6f9b058376293e61d9f0d4442e379f717/a29fd47011b2957ef44a62d92995adfdbae03fa9.bin`
- `PUT /crud/orbeon/builder/data/7b55c9d6f9b058376293e61d9f0d4442e379f717/data.xml`

Publish:

- `PUT /crud/acme/order/form/a29fd47011b2957ef44a62d92995adfdbae03fa9.bin`
- `PUT /crud/acme/order/form/form.xhtml?document=7b55c9d6f9b058376293e61d9f0d4442e379f717`

[SINCE Orbeon Forms 4.6]

When Form Builder publishes a form definition, if versioning is supported by the target persistence layer, it passes an `Orbeon-Form-Definition-Version` header with values:

- missing: indicates the latest published version, or `1` if there is no published version
- `next`: to indicate that the form definition must be published under the next available version
- or a specific version number: to indicate that the form definition must replace the given version
    - *NOTE: The version number must be a positive integer.*
    
[SINCE Orbeon Forms 2017.2.]

Orbeon Forms implements, exposes, and internally uses the [Publish form definition API](/form-runner/api/other/publish.md) to publish form definitions. Orbeon recommends using that API to publish form definitions.

## Attachments

Form data supports *attachments*. These are usually binary data attached by the user via an Attachment or Image attachment control. Attachments are stored separately from the XML data.

### `PUT` and `GET`

In order to save an attachment, you have to use a separate `PUT` request for each attachment. Conversely, to read an attachment that has been saved, use a `GET` to the attachment path. The format of the attachment path is as follows, on a single line (here split in multiple lines for readability):

```
/fr/service/persistence/crud
    /$app/$form
    /(data|draft)
    /$doc
    /$attachment.bin
```

where `$attachment` is a random id for the attachment. The `.bin` extension is expected.

### Attachment metadata

The XML data must point to the attachment using that same path. For example:

```xml
<form>
    <my-section>
        <my-image
            filename="MyImage.jpg"
            mediatype="image/jpeg" 
            size="22143">/fr/service/persistence/crud/orbeon/bookshelf/data/16ab5903c7f0deb74fdc51dbdf705375/26abcf492f64db9808f2b13847e0cf8b.bin</my-image>    
    </my-section>
</form>
```

Note that in the case of attachments, the XML data contains attributes indicating:

- the file name
- the mediatype
- the file size

### Encrypted attachments

[SINCE Orbeon Forms 2019.1] Implementations of the persistence API don't need to do anything special to handle encrypted attachments, and they can just save the data they received and serve the data they saved. Conversely, consumers of the persistence API will always receive decrypted data when issuing a `GET` request and aren't expected to do the encryption themselves when issuing a `PUT` request. This ensures that the persistence API is as simple as possible to both implementers and users of the persistence API.

For this to work, a component we call the *persistence proxy* sits between consumers and implementations of the API. It is the persistence proxy that takes care of encrypting and decrypting attachments as necessary. However, for the persistence proxy to be able to do this, as a consumer of the API:

- When storing an attachment (`PUT`), you must "tell" the persistence proxy what field this attachment corresponds to, so the persistence proxy can figure out whether it needs to be encrypted based on the form definition. This is done by adding an `Orbeon-Path-To-Holder` header to your `PUT` request. The value of the header is the path to the element in the XML data that corresponds to the attachment currently being stored. The path should skip the root element name (`/form`), but include "iteration" elements for repeated grids and repeated sections. For instance:
    - If you have a section named `personal-information`, with an attachment field `photo`, then the value of the header should be `personal-information/photo`.
    - If you have a section named `children`, with a repeated grid named `child`, with an attachment field `photo`, then the value of the header should be `children/child/child-iteration/photo`.
- When reading an attachment (`GET`), you must "tell" the persistence proxy whether the attachment was encrypted. You can know this by checking whether the element in the XML data that corresponds to the attachment has an attribute `fr:attachment-encrypted = 'true'`. If so, you need to add the header `Orbeon-Decrypt: true` to your `GET` request. (The `fr:attachment-encrypted = 'true'` attribute in the XML data is automatically set or removed, as appropriate, by the persistence proxy when the data is saved.)

## Deleting all data for an existing form

To remove all instances of form data, issue a `DELETE` to:

```
/crud/[APPLICATION_NAME]/[FORM_NAME]/data/
```

## See also

- [Search](search.md)
- [List form data attachments](list-form-data-attachments.md)
- [Form metadata](forms-metadata.md)
- [Caching](caching.md)
- [Versioning](versioning.md)
- [Implementing a persistence service](implementing-a-persistence-service.md)
