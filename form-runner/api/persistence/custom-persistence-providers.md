# Custom persistence providers

## Introduction

This page describes how to implement a custom persistence implementation (also known as a persistence *provider*) for Form Runner and Form Builder.

A persistence provider is responsible for storing and retrieving form definitions and form data, typically in a database. It also implements APIs such as search or revision history. Here is the list of core APIs:

- [CRUD API](crud.md)
- [Form metadata API](forms-metadata.md)
- [Search API](search.md)
- [Revision history API](revision-history.md)

[//]: # (- xxx [Lease API]&#40;lease.md&#41;)

[//]: # (- xxx [Reindexing API]&#40;reindexing.md&#41;)

[//]: # (- xxx [Caching]&#40;caching.md&#41;)

## Built-in persistence providers

Orbeon Forms ships (or shipped) with the following built-in persistence providers:

- Relational
    - This is the default provider, or rather an implementation that supports a series of providers, one for each of the following databases:
        - `postgresql`: PostgreSQL
        - `mysql`: MySQL
        - `oracle`: Oracle
        - `sqlserver`: SQL Server
        - `db2`: DB2
        - `sqlite`: SQLite
- eXist XML database
    - Support for eXist is deprecated since Orbeon Forms 2019.1 and removed with Orbeon Forms 2023.1. Therefore, this provider will not be discussed further in this document.

## Custom persistence providers

Before Orbeon Forms 2023.1, it was already possible to implement your own persistence implementation. However, it was difficult to implement some features, including:

- versioning
- permission checks

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

Since this version, there is a documented API between Form Runner and the persistence provider that simplifies handling these features in a custom persistence provider. This API is documented below.

## Architecture

Form Runner internally includes a *persistence proxy*, which exposes API endpoints for Form Runner, Form Builder, and external callers, as documented in other pages.

TODO

The proxy acts as an intermediary between public APIs and persistence providers. Persistence providers are never called directly by any software except the persistence proxy. 

The persistence proxy is responsible for:

- determining the persistence provider to use
- passing configuration information to the persistence provider, such as a relational datasource name
- passing the request to the persistence provider
- handling the response from the persistence provider

SINCE Orbeon Forms 2023.1, the persistence proxy is also responsible for most of the logic for:

- versioning
- permission checks

A persistence provider implementation can be local to Form Runner, or it can be remote and accessed via HTTP service call. The default relational provider is local to Form Runner and the persistence proxy calls it by using internal calls. A custom persistence provider will typically be remote and accessed via service calls.

##  Persistence provider protocol

### CRUD

"CRUD" stands for "Create, Read, Update, Delete". A persistence provider should implement the CRUD API. This API is used by Form Runner and Form Builder to store, retrieve, and optionally delete form definitions and form data.

The following HTTP methods must be supported:

- `GET`: retrieve a resource
- `PUT`: create or update a resource
- `DELETE`: delete a resource
- `HEAD`: retrieve a resource's metadata
    - this returns the same HTTP headers and status code as a `GET` request, but no response body
    - the implementation can be optimized to avoid reading the resource's content, but otherwise is identical to a `GET` request
    - the persistence proxy *will* issue `HEAD` requests, so the implementation must support it

Endpoints:

- for a form definition: `/fr/service/persistence/crud/$app/$form/form/form.xhtml`
- for form definition attachments: TODO
- for form data: `/fr/service/persistence/crud/$app/$form/data/$document/data.xml`
- for form data attachments: TODO
- TODO: drafts

URL parameters:

- `force-delete`
    - for `DELETE` or `HEAD` only
    - for form data only
    - this is used by the Purge API only
    - TODO: internal callers only???
- `last-modified-time`
    - for form data only
    - this is used by the Zip Export API, the Purge API, and the Revision History API 
    - TODO 

HTTP request headers:

- `Orbeon-Username`
    - for example: `hsimpson`
    - can be blank or missing
    - `PUT`/`DELETE` requests
        - store last modification username (new or existing resource)
            - the relational persistence provider stores this into the `last_modified_by` column
        - store creation username (for a new resource only)
            - the relational persistence provider stores this into the `username` column 
    - `GET`/`HEAD` requests
        - unused 
- `Orbeon-Group`
    - for example: `orbeon-user`
    - can be blank or missing
    - `PUT`/`DELETE` requests
        - store the owner group of the resource (for a new resource only) 
            - the relational persistence provider stores this into the `group` column 
    - `GET`/`HEAD` requests
        - unused 
- `Orbeon-Form-Definition-Version`
    - for example: `42` 
    - required for form definition only (ignored for form data)
    - must be a positive integer
    - if the persistence provider doesn't support versioning, it can ignore this value
        - in such a case, the proxy will pass `1` as the version number
- `Orbeon-Created-Existing`
    - [SINCE Orbeon Forms 2023.1.4]
    - millisecond-resolution ISO format date/time with the data's created data, if we are updating an existing resource
    - for example: `2024-07-17T21:52:11.611Z`
    - for `PUT`/`DELETE` requests
    - if present, the provider should use this to set the resource's original creation date/time
    - this value is obtained by the persistence proxy by calling `HEAD` or `GET` on the provider before calling `PUT` or `DELETE`
- `Orbeon-Username-Existing`
    - [SINCE Orbeon Forms 2023.1.4]
    - for example: `hsimpson`
    - can be blank or missing
    - original username of the user who created the data, if we are updating an existing resource
    - for `PUT`/`DELETE` requests
    - if present, the provider should use this to set the resource's original creation username
    - this value is obtained by the persistence proxy by calling `HEAD` or `GET` on the provider before calling `PUT` or `DELETE`
- `Orbeon-Group-Existing`
    - [SINCE Orbeon Forms 2023.1.4]
    - for example: `orbeon-user`
    - can be blank or missing
    - original group of the user who created the data, if we are updating an existing resource
    - for `PUT`/`DELETE` requests
    - if present, the provider should use this to set the resource's original creation group
    - this value is obtained by the persistence proxy by calling `HEAD` or `GET` on the provider before calling `PUT` or `DELETE`
- `Orbeon-Create-Flat-View`
    - if `true`, the provider should create a flat view, if it supports it
        - for `PUT`
        - for a form definition
        - not for an attachment
        - the form name is not `library`
- provider features as configured via properties
    - `Orbeon-Datasource`
        - JDBC datasource identifier
        - this is used for the built-in relational providers
        - default values: `mysql`, `postgresql`, `oracle`, `sqlserver`, `db2`, `sqlite`
        - typically a custom value is set by the administrator in `properties-local.xml`
        - a custom provider can use this to determine which datasource to use if needed, but it can also ignore it if datasources are not relevant to the particular implementation
    - `Orbeon-Versioning`
        - `true` or `false`
        - whether the provider supports versioning
    - `Orbeon-Lease`
        - `true` or `false`
        - whether the provider supports the lease feature
    - `Orbeon-Reindex`
        - `true` or `false`
        - whether the provider supports reindexing
    - `Orbeon-Reencrypt`
        - `true` or `false`
        - whether the provider supports re-encryption
    - `Orbeon-Sort`
        - `true` or `false`
        - whether the provider supports sorting
    - `Orbeon-Distinct`
        - `true` or `false`
        - whether the provider supports distinct values
- `If-Modified-Since`: "Wed, 17 Jul 2024 17:34:16 GMT"
    - TODO
- `Orbeon-Credentials`: `{"username":"hsimpson","groups":[],"roles":[],"organizations":[]}`
    - TODO

HTTP request body:

- for `GET`, `HEAD`, and `DELETE` requests, the body is empty
- for `PUT`, the body contains the resource to store
    - for form data, this is the XML data
    - for form definitions, this is the XHTML form definition
    - for form data attachments, this is the binary attachment
    - for form definition attachments, this is the binary attachment

HTTP response body:

- for `HEAD`, `DELETE` and `PUT` requests, the body is empty
- for `GET` requests, the body contains the resource to return
    - for form data, this is the XML data
    - for form definitions, this is the XHTML form definition
    - for form data attachments, this is the binary attachment
    - for form definition attachments, this is the binary attachment
- TODO: ranges

HTTP response codes:

- `200`: success
- `500`: internal server error
- `400`: bad request
- `404`: not found
    - for `GET` or `HEAD` requests, if the resource doesn't exist
- `410`: gone
    - for `GET` or `HEAD` requests, if we know that the resource used to exist but has been deleted
    - but for `HEAD` with `force-delete=true`, this is not returned, instead, the deleted resource metadata is returned

HTTP response headers for `GET` and `HEAD` requests:

- `Orbeon-Form-Definition-Version`
    - the version of the form definition
    - for form definitions
        - specific version of the form definition
        - for example: `42`
        - if the provider doesn't support versioning, it can return `1`
    - for form data
        - specific version of the form definition associated with the data
        - for example: `42`
        - if the provider doesn't support versioning, it can return `1`
- `Orbeon-Username`
    - username of the user who created the data
    - for example: `hsimpson`
- `Orbeon-Group`
    - group of the user who created the data
    - for example: `orbeon-user`
- `Orbeon-Last-Modified-By-Username`
    - username of the user who last modified the data
    - for example: `hsimpson`
- `Created`
    - RFC 1123 format date/time with the data's created data
    - for example: `Wed, 17 Jul 2024 21:52:11 GMT`
- `Last-Modified`
    - RFC 1123 format date/time with the data's last modification date 
    - for example: `Wed, 17 Jul 2024 21:52:11 GMT`
- `Orbeon-Created`
    - millisecond-resolution ISO format date/time with the data's created data
    - for example: `2024-07-17T21:52:11.611Z`
    - this is used by
        - the zip export API
        - to pass `Orbeon-Created-Existing` in `PUT` and `DELETE` requests
- `Orbeon-Last-Modified`
    - millisecond-resolution ISO format date/time with the data's last modification date
    - for example: `2024-07-17T21:52:11.611Z`
    - this is used by
        - the zip export API
        - the purge API
- `Content-Type`
    - `application/xml` for form data and form definitions, optional otherwise 
- `Content-Range`
    - TODO
    - `Content-Length`
        - needed in this case

HTTP response headers for `PUT` and `DELETE` requests:

- `Orbeon-Form-Definition-Version`
    - the version of the form definition
    - must be the same value as the value of the incoming `Orbeon-Form-Definition-Version` header
    - for example: `42`
- `Last-Modified`
    - RFC 1123 format date/time with the data's last modification date 
    - for example: `Wed, 17 Jul 2024 21:52:11 GMT`
    - the provider determines an instant/timestamp for the last modification
        - this must be stored in the database
        - the same value must be returned in the response
    - this is omitted if
        - the request is for deleting a draft
        - or if it is for force-deleting a resource
- `Orbeon-Last-Modified`
    - millisecond-resolution ISO format date/time with the data's last modification date
    - for example: `2024-07-17T21:52:11.611Z`
    - the provider determines an instant/timestamp for the last modification
        - this must be stored in the database
        - the same value must be returned in the response
    - this is omitted if
        - the request is for deleting a draft
        - or if it is for force-deleting a resource

### Form metadata API

TODO

## Custom persistence provider setup

First, configure `properties-local.xml`, for example:

```xml
<property
    as="xs:string"
    name="oxf.fr.persistence.provider.acme.*.*"
    value="my-acme-provider"/>

<property
    as="xs:anyURI"
    name="oxf.fr.persistence.my-acme-provider.uri"
    value="http://example.com/my-persistence"/>
```

This tells Orbeon Forms to dispatch all persistence API calls for applications with name `acme` to the specified endpoint base URL. The provider is thereby named `my-acme-provider`.

The second `*` wildcard can restrict the configuration to a single form. The third `*` wildcard can specify whether the configuration is for form definitions or form data, using the tokens `form` and `data`. For example the following would configure the `my-acme-provider` provider for form data only:

```xml
<property
    as="xs:string"
    name="oxf.fr.persistence.provider.acme.*.data"
    value="my-acme-provider"/>
```

_NOTE: You cannot name a provider `provider`._

In addition, you must configure the following properties to describe what your provider supports:

```xml
<!-- Whether provider support versioning -->
<property as="xs:boolean" name="oxf.fr.persistence.acme.versioning" value="false"/>

<!-- Whether provider support the lease feature -->
<property as="xs:boolean" name="oxf.fr.persistence.acme.lease"      value="false"/>

<!-- Whether provider support reindexing -->
<property as="xs:boolean" name="oxf.fr.persistence.acme.reindex"    value="false"/>

<!-- Whether provider support re-encryption -->
<property as="xs:boolean" name="oxf.fr.persistence.acme.reencrypt"  value="false"/>

<!-- Whether provider support distinct values -->
<property as="xs:boolean" name="oxf.fr.persistence.acme.distinct"   value="false"/>

<!-- Whether provider support sorting -->
<property as="xs:boolean" name="oxf.fr.persistence.acme.sort"       value="false"/>
```

[//]: # (### xxx)

[//]: # ()
[//]: # (- persistence headers)

[//]: # ()
[//]: # (- `Orbeon-Form-Definition-Version`: xxx passed in AND out)

[//]: # ()
[//]: # (Request for a `GET` or `HEAD`:)

[//]: # ()
[//]: # (| Header                           | Description                                                               |)

[//]: # (|----------------------------------|---------------------------------------------------------------------------|)

[//]: # (| `Orbeon-Form-Definition-Version` |                                                                           |)

[//]: # ()
[//]: # (Response for a `GET` or `HEAD`:)

[//]: # ()
[//]: # (| Header                           | Description                                                               |)

[//]: # (|----------------------------------|---------------------------------------------------------------------------|)

[//]: # (| `Orbeon-Form-Definition-Version` |                                                                           |)

[//]: # (| `Orbeon-Username`                |                                                                           |)

[//]: # (| `Orbeon-Group`                   |                                                                           |)

[//]: # (| `Orbeon-Workflow-Stage`          |                                                                           |)

[//]: # (| `Created`                        |                                                                           |)

[//]: # (| `Last-Modified`                  |                                                                           |)

[//]: # (| `Content-Type`                   | `application/xml` for form data and form definitions, optional otherwise  |)

[//]: # ()
[//]: # ()
[//]: # (Request for a `PUT` or `DELETE`:)

[//]: # ()
[//]: # (TODO)

[//]: # ()
[//]: # (Response for a `PUT` or `DELETE`:)

[//]: # ()
[//]: # (TODO)

## See also

- [CRUD API](crud.md)
- [Form metadata API](forms-metadata.md)
- [Search API](search.md)
- [Revision history API](revision-history.md)
- [Lease API](lease.md)
- [Reindexing API](reindexing.md)
- [Caching](caching.md)