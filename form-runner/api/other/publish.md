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

## Example using curl

The following example uses the [curl](https://curl.haxx.se/) command-line utility:

```
curl -v -k -X POST http://localhost:9090/orbeon/fr/service/orbeon/builder/publish/ef8b20715f447ef1ed6f2479161dc663b23f7cdc
``` 

Provided access to the service is open, this will publish the form definition edited at:

```
http://localhost:9090/orbeon/fr/orbeon/builder/edit/ef8b20715f447ef1ed6f2479161dc663b23f7cdc
``` 

## Permissions

The caller must either call the service internally or have proper credentials to access the data (username, group, roles).
