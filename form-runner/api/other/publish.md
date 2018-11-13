# Publish form definition

## Availability

Since Orbeon Forms 2017.2.

This is an Orbeon Forms PE feature.

## Internal uses

The API is also used internally by Form Builder and by the Form Runner Home page.

## Purpose

The purpose of the `publish` API is to publish a form definition.

The form definition is:

- either provided as XHTML+XForms in the Orbeon Forms form definition format
- or stored in the database under the `orbeon/builder` app/form name

It is important to note that you cannot simply take a form definition and `PUT` it via the [persistence REST API](../persistence/README.md).
The reason for this is that publishing requires adding some information to the form definition, such as:

- data migration information for repeated grids
- section templates must be included

Only Form Builder and Form Runner are able to provide this information consistently. So in order to publish a form definition,
this API must be used. 

## Interface

If the form definition is provided in the request:

- URL: `/fr/service/publish`
- Optional URL parameter:
    - Name: `document-id`
    - Value: Form Builder document id for retrieval of attachments
- Method: `POST`
- Request body: XHTML+XForms

If the form definition is NOT provided in the request:

- URL: `/fr/service/orbeon/builder/publish/$document-id`
    - `$document-id`: Form Builder document id for retrieval of attachments
- Method: `POST`
- Request body: empty

The following URL parameters apply in both cases:

- `form-definition-version`:
    - missing: indicates the latest published version, or `1` if there is no published version 
    - `next`: to indicate that the form definition must be published under the next available version
    - or a specific version number: to indicate that the form definition must replace the given version
- `available`:
    - when `false`: make or keep the form definition unavailable
    - when `true` or missing: make or keep the form definition available
- `version-comment`:
    - when present, a [versioning comment](../../../form-builder/publishing.md#versioning-comments) to store with the form definition

Response body:

- `Content-Type: application/xml`
- app/form name used

Example response body:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<response>
    <app>acme</app>
    <form>sales</form>
    <published-attachments>2</published-attachments>
    <published-version>1</published-version>
</response>
```

_NOTE: When the form definition is POSTed as XML, form definition attachments such as PDF, XML Schema, or image attachments
will not be published reliably if a `document-id` is not passed._

## Example using curl

The following examples use the [curl](https://curl.haxx.se/) command-line utility. They are indented on multiple lines for clarity but in practice each command must be written on a single line.

### Publish a form definition stored in Form Builder

The following publishes the form definition saved with Form Builder under the `ef8b20715f447ef1ed6f2479161dc663b23f7cdc` document id:

```
curl
  -v
  -k
  -X POST
  http://localhost:9090/orbeon/fr/service/orbeon/builder/publish/ef8b20715f447ef1ed6f2479161dc663b23f7cdc
``` 

### Publish a form definition provided via HTTP POST

The following publishes the form definition provided in the file `form.xhtml`:

```
curl
  -v
  -k
  -d @form.xhtml
  -H "Content-Type: application/xml"
  -X POST http://localhost:9090/orbeon/fr/service/publish
```

## Permissions

- The caller must either call the service internally or have [authorized the service](/xml-platform/controller/authorization-of-pages-and-services.md).
- Appropriate container or permission headers must also be set to allow accessing the form definition and data.  

## See also

- [Form Builder publishing](../../../form-builder/publishing.md)
