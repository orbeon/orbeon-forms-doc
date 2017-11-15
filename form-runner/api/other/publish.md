# Publish form definition

<!-- toc -->

## Availability

Since Orbeon Forms 2017.2.

This is an Orbeon Forms PE feature.

## Purpose

The purpose of the `publish` API is to publish a form definition.

The form definition is:

- either provided as XHTML+XForms in the Orbeon Forms form definition format
- or stored in the database under the `orbeon/builder` app/form name

It is important to note that you cannot simply take a form definition and PUT it via the [persistence REST API](../persistence/README.md).
The reason for this is that publishing requires adding some information to the form definition, such as:

- data migration information for repeated grids
- section templates must be included

Only Form Builder and Form Runner are able to provide this information consistently. So in order to publish a form definition,
this API must be used. 

## Interface

If the form definition is provided in the request:

- URL: `/fr/service/publish`
- Method: `POST`
- Request body: XHTML+XForms

If the form definition is NOT provided in the request:

- URL: `/fr/service/orbeon/builder/publish/$document`
- Method: `POST`
- Request body: empty

Response body:

- `Content-Type: application/xml`
- app/form name used

Example response body:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<response>
    <app>acme</app>
    <form>sales</form>
</response>
```

_NOTE: When the form definition is POSTed as XML, form definition attachments such as PDF, XML Schema, or image attachments
may not be published reliably._

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
