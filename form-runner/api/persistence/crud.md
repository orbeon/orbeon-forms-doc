# CRUD API

## Service endpoint

| Operation | HTTP Method  | URL                                                                          |
| --------- | ------------ | ---------------------------------------------------------------------------- |
| Create    | `PUT`        | `/fr/service/persistence/crud/$app/$form/$data-or-draft/$document/$filename` |
| Read      | `GET`/`HEAD` | `/fr/service/persistence/crud/$app/$form/$data-or-draft/$document/$filename` |
| Update    | `PUT`        | `/fr/service/persistence/crud/$app/$form/$data-or-draft/$document/$filename` |
| Delete    | `DELETE`     | `/fr/service/persistence/crud/$app/$form/$data-or-draft/$document/$filename` |

Where:

* `$app` is the form definition's application name
* `$form` is the form definition's form name
* `$data-or-draft`
  * for data: `data`
  * for draft: `draft`
* `$document` is the form data's document id
* `$filename` is the data or attachment's filename
  * data: always `data.xml`
  * attachment: a random id followed by the `.bin` extension, for example `2a3bd8bb935c54280948a5874c47e2994359d01b.bin`

## Basics

### GET and HEAD

* The request body is empty and response body contains resource.
* If what is requested can't be found, the CRUD API returns at 404.
* \[SINCE Orbeon Forms 4.5] The `Orbeon-Operations` response header lists the operations that the user can perform on the data (see [Supporting permissions in your persistence API implementation](https://blog.orbeon.com/2013/10/supporting-permissions-in-your.html)).
* If the implementation of the persistence API supports form versioning:
  * \[SINCE Orbeon Forms 4.5] If the request is for a form definition, the `Orbeon-Form-Definition-Version` request header tells which version of the form definition is requested.
  * \[SINCE Orbeon Forms 2023.1] If the request is for a form data, the `Orbeon-Form-Definition-Version` response header indicates which version of the form definition was used to create the relevant form data.
* \[SINCE Orbeon Forms 2023.1] The built-in implementation of the persistence API supports the HEAD method. Form Runner uses HEAD instead of GET as an optimization when it doesn't need the response body. Implementations of the CRUD API must handle HEAD requests similarly to GET requests, but return an empty body.

### PUT

* The request body contains the resource to store.
* The `Content-Type` must contain the content-type of the resource to store. For XML in particular, pass `application/xml`.
* The request response is empty.
* If your implementation of the persistence API supports form versioning, then when storing a new document, you need to tell the persistence what version of the form definition was used to create this data, so the same version can then be used to subsequently edit the data. This is done by specifying the version in the `Orbeon-Form-Definition-Version` header.

### DELETE

* Both request and response bodies are empty.

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

## Migrating a form definition for integration purposes

\[SINCE Orbeon Forms 2021.1]

Some Orbeon Forms integrators retrieve form definitions using the CRUD API to analyze them and, for example, to perform operations on the data with the knowledge of the form structure acquired from those form definitions.

However, the format of Orbeon Forms form definitions has evolved over time, in particular to follow changes to the [data format](../../data-format/form-data.md). This is because a form definition follows the so-called _internal data format_. This means that retrieving a form definition with a newer version of Orbeon Forms can yield a form definition that is different from the format obtained in earlier versions. In particular, extra XML elements and `<xf:bind>` elements can be present.

To alleviate this issue, a URL parameter, `form-definition-format-version`, is provided. It applies to the `GET` method only, in the following cases:

*   To retrieve a _published form definition_ (also include the `Orbeon-Form-Definition-Version` header to specify the form definition version):

    ```
    /fr/service/persistence/crud/$app/$form/form/form.xhtml
    ```
*   To retrieve an _unpublished form definition_ from the Form Builder data:

    ```
    /fr/service/persistence/crud/orbeon/builder/(data|draft)/$doc/data.xml
    ```

By default, without this parameter, a form definition is returned unchanged from the database. But if `form-definition-format-version` is used, the form definition is partially migrated to match the specified destination data format:

* `form-definition-format-version=4.0.0`: target the 4.0.0 data format
* `form-definition-format-version=4.8.0`: target the 4.8.0 data format
* `form-definition-format-version=2019.1.0`: target the 2019.1.0 data format

The internal data format version associated with form definition retrieved is determined as follows:

* If the form definition includes `updated-with-version` or `created-with-version` metadata, that information is used to infer the data format associated with the given Orbeon Forms version.
* Else the form definition was last updated with a version older than Orbeon Forms 2018.2, and the data format version associated with the form definition is assumed to be 4.8.0.

**WARNING: This means that this feature does not currently work to migrate a form definition published with a version older than Orbeon Forms 4.8.0, if the definition has not been republished or upgraded with Orbeon Forms 4.8.0 or newer. Or rather, the migration might fail in unexpected ways.**

More specifically, the form definition read from the database is transformed as follows to adjust to match the specified data format:

* inline instance data, under `fr-form-instance`
* repeat templates (`-template` instances)
* binds and controls hierarchy

Other aspects of the form definition, including names of form controls and various attributes, are left unchanged.

For example, if the form definition has been published with Orbeon Forms 2020.1, instance data might look like this:

```xml
<xf:instance id="fr-form-instance">
    <form fr:data-format-version="2019.1.0">
        <text-controls>
            <grid-1>
                <input>Alice</input>
                <textarea>Lorem ipsum...</textarea>
            </grid-1>
        </text-controls>
        ...
    </form>
</xf:instance>
```

Data migrated to the 4.8.0 format will look like the following, since elements representing non-repeated grids are missing:

```xml
<xf:instance id="fr-form-instance">
    <form fr:data-format-version="4.8.0">
        <text-controls>
            <input>Alice</input>
            <textarea>Lorem ipsum...</textarea>
        </text-controls>
        ...
    </form>
</xf:instance>
```

**WARNING: It is important that the form definition obtained this way be used only to infer form structure, and not to be stored again in the Form Runner database.**

_NOTE: The use of this feature should be rare._

## Examples using curl

The following examples use the [curl](https://curl.haxx.se/) command-line utility. They are indented on multiple lines for clarity but in practice each command must be written on a single line.

The service must be open for these examples to work.\
See [Authorization of pages and services](../../../xml-platform/controller/authorization-of-pages-and-services.md).

The general format of a path to access form data is:

```
/fr/service/persistence/crud/$app/$form/(data|draft)/$doc/data.xml
```

So with the following assumptions:

* app name (`$app`): `acme`
* form name (`$form`): `order`
* document id (`$doc`): `fc4c32532e8d35a2d0b84e2cf076bb070e9c1e8e`

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

* app name (`$app`): `orbeon`
* form name (`$form`): `builder`
* document id (`$doc`): `7b55c9d6f9b058376293e61d9f0d4442e379f717`

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

\[SINCE Orbeon Forms 2017.1]

* `nonrelevant`
  * when using `GET` for data only
  * values
    * `remove`: remove all XML data elements with attribute `fr:relevant="false"`
    * `keep`: \[SINCE Orbeon Forms 2018.1] do not remove XML data elements
    * missing: do not remove XML data elements

_NOTE: This property only operates properly on data stored with Orbeon Forms 2017.1 and newer, as only Orbeon Forms 2017.1 and newer stores data with the `fr:relevant="false"` annotation._

### Passed by Form Runner

\[SINCE Orbeon Forms 4.2]

The following URL parameters are set by Form Runner:

* `valid`
  * passed when data is stored using `PUT` only
  * values
    * `true`: data sent satisfies validation rules
    * `false`: data sent does not satisfy validation rules

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

When you create, edit, or read form definition with Form Builder, those are considered _unpublished_ form definitions and are stored as Form Builder _data_. Those are . They are stored under the path:

`/crud/orbeon/builder/data/$doc`

On the other hand, when Form Builder _publishes_ a form definition, Form Builder first makes modifications to the form definition (such as adding section templates) and stores it where Form Runner will find it, under the path:

```
/crud/$app/$form/form
```

For example, this is what happens when saving and publishing a form definition `acme/order` with a single attachment (for example for a PDF template or static image):

Save:

* `PUT /crud/orbeon/builder/data/7b55c9d6f9b058376293e61d9f0d4442e379f717/a29fd47011b2957ef44a62d92995adfdbae03fa9.bin`
* `PUT /crud/orbeon/builder/data/7b55c9d6f9b058376293e61d9f0d4442e379f717/data.xml`

Publish:

* `PUT /crud/acme/order/form/a29fd47011b2957ef44a62d92995adfdbae03fa9.bin`
* `PUT /crud/acme/order/form/form.xhtml?document=7b55c9d6f9b058376293e61d9f0d4442e379f717`

\[SINCE Orbeon Forms 4.6]

When Form Builder publishes a form definition, if versioning is supported by the target persistence layer, it passes an `Orbeon-Form-Definition-Version` header with values:

* missing: indicates the latest published version, or `1` if there is no published version
* `next`: to indicate that the form definition must be published under the next available version
* or a specific version number: to indicate that the form definition must replace the given version
  * _NOTE: The version number must be a positive integer._

\[SINCE Orbeon Forms 2017.2.]

Orbeon Forms implements, exposes, and internally uses the [Publish form definition API](../other/publish.md) to publish form definitions. Orbeon recommends using that API to publish form definitions.

## Attachments

Form data supports _attachments_. These are usually binary data attached by the user via an Attachment or Image attachment control. Attachments are stored separately from the XML data.

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

* the file name
* the mediatype
* the file size

### Encrypted attachments

\[SINCE Orbeon Forms 2019.1] Implementations of the persistence API don't need to do anything special to handle encrypted attachments, and they can just save the data they received and serve the data they saved. Conversely, consumers of the persistence API will always receive decrypted data when issuing a `GET` request and aren't expected to do the encryption themselves when issuing a `PUT` request. This ensures that the persistence API is as simple as possible to both implementers and users of the persistence API.

For this to work, a component we call the _persistence proxy_ sits between consumers and implementations of the API. It is the persistence proxy that takes care of encrypting and decrypting attachments as necessary. However, for the persistence proxy to be able to do this, as a consumer of the API:

* When storing an attachment (`PUT`), you must "tell" the persistence proxy what field this attachment corresponds to, so the persistence proxy can figure out whether it needs to be encrypted based on the form definition. This is done by adding an `Orbeon-Path-To-Holder` header to your `PUT` request. The value of the header is the path to the element in the XML data that corresponds to the attachment currently being stored. The path should skip the root element name (`/form`), but include "iteration" elements for repeated grids and repeated sections. For instance:
  * If you have a section named `personal-information`, with an attachment field `photo`, then the value of the header should be `personal-information/photo`.
  * If you have a section named `children`, with a repeated grid named `child`, with an attachment field `photo`, then the value of the header should be `children/child/child-iteration/photo`.
* When reading an attachment (`GET`), you must "tell" the persistence proxy whether the attachment was encrypted. You can know this by checking whether the element in the XML data that corresponds to the attachment has an attribute `fr:attachment-encrypted = 'true'`. If so, you need to add the header `Orbeon-Decrypt: true` to your `GET` request. (The `fr:attachment-encrypted = 'true'` attribute in the XML data is automatically set or removed, as appropriate, by the persistence proxy when the data is saved.)

## Deleting all data for an existing form

**WARNING: This feature is not supported when using relational databases.**

To remove all instances of form data, issue a `DELETE` to:

```
/crud/$app/$form/data/
```

## See also

* [Reindexing](reindexing.md)
* [Search](search.md)
* [List form data attachments](list-form-data-attachments.md)
* [Form metadata](forms-metadata.md)
* [Caching](caching.md)
* [Versioning](versioning.md)
* [Custom persistence providers](custom-persistence-providers.md)
