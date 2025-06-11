# Custom persistence providers

## Introduction

This page describes how to implement a custom persistence implementation (also known as a persistence *provider*) for Form Runner and Form Builder.

A persistence provider is responsible for storing and retrieving form definitions and form data, typically in a database. It also implements APIs such as search or revision history. Here is the list of core APIs:

- [CRUD API](crud.md)
- [Form Metadata API](forms-metadata.md)
- [Search API](search.md)
- [Revision History API](revision-history.md)
- [Lease API](lease.md)

[//]: # (- xxx [Reindexing API]&#40;reindexing.md&#41;)

[//]: # (- xxx [Caching]&#40;caching.md&#41;)

## Architecture

### Overview

Form Runner internally includes a *persistence proxy*, which exposes API endpoints for Form Runner, Form Builder, and external callers.

![Persistence architecture](../../images/architecture-persistence.png)

The proxy acts as an intermediary between public APIs and persistence providers. Persistence providers are never called directly by any software except the persistence proxy. 

The persistence proxy is responsible for:

- determining the persistence provider to use
- passing configuration information to the persistence provider, such as a relational datasource name
- passing the request to the persistence provider
- handling the response from the persistence provider

SINCE Orbeon Forms 2023.1, the persistence proxy is also responsible for most of the logic for:

- versioning
- permission checks

This makes it easier to implement a persistence provider, as the more complex logic is handled by the persistence proxy.

A persistence provider implementation can be local to Form Runner, or it can be remote and accessed via HTTP service call. The built-in relational provider is local to Form Runner and the persistence proxy calls it by using internal calls. A custom persistence provider will typically be remote and accessed via service calls.

### Built-in persistence providers

Orbeon Forms ships (or shipped) with the following built-in persistence providers:

- Relational databases
    - This is the default provider, or rather an implementation that supports a series of providers, one for each of the following databases:
        - `postgresql`: PostgreSQL
        - `mysql`: MySQL
        - `oracle`: Oracle
        - `sqlserver`: SQL Server
        - `db2`: DB2
        - `sqlite`: SQLite
- eXist XML database
    - Support for eXist is deprecated since Orbeon Forms 2019.1 and removed with Orbeon Forms 2023.1. Therefore, this provider is not discussed further in this document.

### Custom persistence providers

Before Orbeon Forms 2023.1, it was already possible to implement your own persistence implementation. However, it was difficult to implement some features, including:

- versioning
- permission checks

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

Since this version, there is a documented API between Form Runner and the persistence provider that simplifies handling these features in a custom persistence provider. This API is documented below.

##  Persistence provider protocol

### CRUD

#### Overview

The [CRUD API](crud.md) is used to store, retrieve, and optionally delete form definitions and form data.

"CRUD" stands for "Create, Read, Update, Delete". A persistence provider should implement the CRUD API. 

#### HTTP methods

The following HTTP methods are used and must be supported:

- `GET`: retrieve a resource
- `PUT`: create or update a resource
- `DELETE`: delete a resource
- `HEAD`: retrieve a resource's metadata
    - this returns the same HTTP headers and status code as a `GET` request, but no response body
    - the implementation can be optimized to avoid reading the resource's content, but otherwise is identical to a `GET` request
    - the persistence proxy *will* issue `HEAD` requests, in the following cases, so the implementation must support it if the behavior is to be correct
        - for `PUT` and `DELETE` of form definition and form data
        - in order to obtain the following original information from an existing record, if present:
            - creation date/time
            - creation username (form data only)
            - creation group (form data only)
- `LOCK`
    - see the [Lease API](lease.md)
- `UNLOCK`
    - see the [Lease API](lease.md)

#### Endpoints

- published form definition
  - form definition XHTML: `/crud/$app/$form/form/form.xhtml`
  - form definition attachment: `/crud/$app/$form/form/$filename`
- form data
  - form data XML: `/crud/$app/$form/$data-or-draft/$document/data.xml`
  - form data attachments: `/crud/$app/$form/$data-or-draft/$document/$filename`
  - `$data-or-draft`
      - for data (or "final" data): `data`
      - for draft: `draft`
          - see below for details 

#### Drafts

Orbeon Forms supports the concept of [autosave drafts](/form-runner/persistence/autosave.md). With the built-in relational provider, a draft is marked specially in database tables with a `draft` column. A custom persistence provider that supports drafts must make that distinction in its storage mechanism.

_NOTE: This only applies to form data and form data attachments, not to published form definitions._

For CRUD operations, whether a draft is being operated on is indicated by the `$data-or-draft` part of the URL (see above). Following REST principles, a resource stored (`PUT`) as `data` must be retrieved (`GET`/`HEAD`) or deleted (`DELETE`) as data and be separate from a resource stored as `draft`, and vice versa. 

There is one case where handling of drafts is different. Consider the following assumptions:

- For a given document id, only a single draft makes sense.
- Historical data is not required for drafts.
- Since there is only one "revision" of a draft, all associated attachments can also be removed when the XML data is removed.
- Once final (non-draft) data is saved, the draft is no longer needed.

For these reasons, a `PUT` or `DELETE` of `data` or `draft` XML (but not attachments) must start by force-deleting (that is deleting without leaving a trace of the data in the database) any draft data XML and attachments associated with the given document id, in the database or store handled by the provider. This logic is implemented by the built-in relational provider.

_NOTE: As of Orbeon Forms 2023.1.4, the provider is expected to implement this logic. This may change in later versions of Orbeon Forms, where the persistence proxy may handle this logic and issue the force-delete with a separate call on the provider. This would be less efficient for the provider, but easier to implement._

#### Revisions

Orbeon Forms supports the concept of [revision history](/form-runner/feature/revision-history.md).

This concept applies to:

- form data XML
- only non-draft form data

This doesn't apply to:

- form definitions
- attachments
- draft form data 

_NOTE: Nothing prevents applying revision history to form data attachments. However, as of Orbeon Forms 2023.1.4, this is not a requirement of persistence providers, as attachments are `PUT` only once by Form Runner._

This means that, when doing a `PUT` or `DELETE` of form data, the provider must keep older revisions of that data. The built-in relational provider uses a last modification date (`last_modified_time`) to identify revisions. It also uses a separate table (`orbeon_i_current`) which identifies the current data to improve performance.

The [Revision History API](revision-history.md) is used to find out about revisions.

In addition, a `GET`/`HEAD` request should support the `last-modified-time` URL parameter to identify a specific revision of the data (see below).

#### URL parameters

- `force-delete`
    - for `DELETE` or `HEAD` only
    - for form data only
    - this is used by the Purge API only
    - TODO: internal callers only???
- `last-modified-time`
    - for form data only
    - this is used by the [Zip Export API](export-zip.md), the Purge API, and the [Revision History API](revision-history.md)
    - this optional parameter is used with `GET`/`HEAD`/`DELETE` to retrieve or delete a specific revision of the data
    - the value is a millisecond-resolution ISO format date/time
        - for example: `last-modified-time=2024-07-17T21:52:11.611Z`

#### HTTP request headers

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
        - whether the provider supports the Lease feature
        - see also the [Lease API](lease.md) 
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

#### HTTP request body

- for `GET`, `HEAD`, and `DELETE` requests, the body is empty
- for `PUT`, the body contains the resource to store
    - for form data, this is the XML data
    - for form definitions, this is the XHTML form definition
    - for form data attachments, this is the binary attachment
    - for form definition attachments, this is the binary attachment

#### HTTP response body

- for `HEAD`, `DELETE` and `PUT` requests, the body is empty
- for `GET` requests, the body contains the resource to return
    - for form data, this is the XML data
    - for form definitions, this is the XHTML form definition
    - for form data attachments, this is the binary attachment
    - for form definition attachments, this is the binary attachment
- TODO: ranges

#### HTTP response status codes

- `200`: success
- `500`: internal server error
- `400`: bad request
- `404`: not found
    - for `GET` or `HEAD` requests, if the resource doesn't exist
- `410`: gone
    - for `GET` or `HEAD` requests, if we know that the resource used to exist but has been deleted
    - but for `HEAD` with `force-delete=true`, this is not returned, instead, the deleted resource metadata is returned

#### HTTP response headers

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
        - the [Zip Export API](export-zip.md)
        - to pass `Orbeon-Created-Existing` in `PUT` and `DELETE` requests
- `Orbeon-Last-Modified`
    - millisecond-resolution ISO format date/time with the data's last modification date
    - for example: `2024-07-17T21:52:11.611Z`
    - this is used by
        - the [Zip Export API](export-zip.md)
        - the Purge API
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

### Form Metadata API

#### Overview

The [Form Metadata API](forms-metadata.md) is used to retrieve the list of *published* forms as well as associated metadata, including published versions, form titles, and permissions. There are some internal uses of this API, therefore a persistence provider should implement it. 

The persistence proxy calls this API to retrieve metadata about a specific form, including permissions, either for the latest version or for all versions. This matches the following endpoints with parameters:

- `/form/$app/$form?all-versions=true&all-forms=true`
- `/form/$app/$form?all-versions=false&all-forms=true`

In addition to this internal persistence proxy use, as mentioned in [Form Metadata API](forms-metadata.md), other internal uses also call this API to list all published forms.

#### HTTP methods

- `GET` only
- `HEAD` support is not required

#### Endpoints

The following endpoints are used:

- `/form`: list all published forms
- `/form/$app`: same, but restrict by the given app name
- `/form/$app/$form`: same, but restrict by the given app and form name

#### URL parameters

- `all-versions`:
    - `true`
        - the response must include all published form definition versions
    - omitted or set to `false`
        - the response must include only the published form definition with the highest version number 
- `modified-since`
    - ISO date/time
    - this can be missing
    - if present, only form definitions which have been modified since the given date/time must be returned

_NOTE: The `all-forms` parameter is handled by the persistence proxy._

#### HTTP request headers

- `Orbeon-Datasource`
    - JDBC datasource identifier
    - this is used for the built-in relational providers
    - default values: `mysql`, `postgresql`, `oracle`, `sqlserver`, `db2`, `sqlite`
    - typically a custom value is set by the administrator in `properties-local.xml`
    - a custom provider can use this to determine which datasource to use if needed, but it can also ignore it if datasources are not relevant to the particular implementation

#### HTTP request body

The body is empty.

#### HTTP response body

The response body is an XML document with the list of published forms and associated metadata.

The document returned by this API looks like this:

```xml
<forms>
    <form>
        <application-name>orbeon</application-name>
        <form-name>bookshelf</form-name>
        <last-modified-time>2014-06-04T11:21:33.043-07:00</last-modified-time>
        <form-version>1</form-version>
        <title xml:lang="en">Orbeon Forms Bookshelf</title>
        <title xml:lang="fr">Orbeon Forms Bookshelf</title>
    </form>
    <form>
        <application-name>orbeon</application-name>
        <form-name>w9</form-name>
        <last-modified-time>2014-06-04T11:21:34.051-07:00</last-modified-time>
        <form-version>3</form-version>
        <title xml:lang="en">Request for Taxpayer Identification Number and Certification</title>
    </form>
    <form>
        <application-name>acme</application-name>
        <form-name>order</form-name>
        <last-modified-time>2014-08-21T16:52:24.429-07:00</last-modified-time>
        <form-version>2</form-version>
        <title xml:lang="en">ACME Order Form</title>
        <title xml:lang="fr">Formulaire de commande ACME</title>
        <permissions>
            <permission operations="delete">
                <group-member/>
            </permission>
            <permission operations="delete">
                <owner/>
            </permission>
            <permission operations="create read update"/>
        </permissions>
    </form>
</forms>
```

The following elements and attributes are straightforward to include:

- `<application-name>`
- `<form-name>`
- `<last-modified-time>`
- `<form-version>`

Other elements and their content come from the form definition's metadata:

- `<title>`
- `<permissions>`
- `<available>`

The built-in relational provider handles this as follows:

- when a form definition is published (`PUT` of `form.xhtml`), the content of the XHTML document is parsed and the following element is identified
    - `/xh:html/xh:head/xf:model[@id = 'fr-form-model']/xf:instance[@id = 'fr-form-metadata']/metadata`
- the children elements with the following names are extracted from under that `<metadata>` element:
    - `<title>`
    - `<permissions>`
    - `<available>`
- the result is stored in the database alongside the form definition's XHTML content, for faster later retrieval by Form Metadata API calls

Custom persistence providers may choose a different approach, such as extracting the metadata from form definitions on demand, but the result must be the same. If you want to support permissions for deployed forms, it is particularly important to include the form definition's `<permissions>` element in the response.

_NOTE: The `operations` attribute on the `<form>` elements is *not* added by the persistence provider: the persistence proxy takes care of adding that attribute._

#### HTTP response status codes

- `200`: success
- `500`: internal server error
- `400`: bad request

#### HTTP response headers

- `Content-Type`
    - `application/xml` 

### Search API

#### Overview

The Search API is used to search for form data documents. It is used internally by:

- the [Form Runner Summary page](/form-runner/feature/summary-page.md)
- the [Form Builder Summary page](/form-builder/summary-page.md)
- a form's Edit or New page, when looking for data associated with:
    - [Singleton form](/form-runner/advanced/singleton-form.md)
    - [Autosave drafts](/form-runner/persistence/autosave.md)
- the [Zip Export API](export-zip.md)
- the Purge API

#### HTTP methods

- `POST` only

#### Endpoints

The following endpoint is used:

- `/search/$app/$form`

#### URL parameters

TODO

#### HTTP request headers

- `Content-Type`
    - `application/xml`
- `Orbeon-Datasource`
    - JDBC datasource identifier
    - this is used for the built-in relational providers
    - default values: `mysql`, `postgresql`, `oracle`, `sqlserver`, `db2`, `sqlite`
    - typically a custom value is set by the administrator in `properties-local.xml`
    - a custom provider can use this to determine which datasource to use if needed, but it can also ignore it if datasources are not relevant to the particular implementation
- `Orbeon-Form-Definition-Version`
    - `all`: 
    - positive integer
    - missing: TODO

#### HTTP request body

See [Search API](search.md).

#### HTTP response body

See [Search API](search.md).

#### HTTP response status codes

- `200`: success
- `500`: internal server error
- `400`: bad request

#### HTTP response headers

- `Content-Type`
    - `application/xml`

### Revision History API

#### Overview

The Revision History API is used to retrieve the revision history of a form data document. It is used internally by:

- the [Revision history feature](/form-runner/feature/revision-history.md)
- the [Zip Export API](export-zip.md)
- the Purge API

#### HTTP methods

- `GET` only
- `HEAD` support is not required

#### Endpoints

The following endpoint is used:

- `/history/$app/$forms/$document`

#### URL parameters

See [Revision History API](revision-history.md#parameters).

#### HTTP request headers

- `Orbeon-Datasource`
    - JDBC datasource identifier
    - this is used for the built-in relational providers
    - default values: `mysql`, `postgresql`, `oracle`, `sqlserver`, `db2`, `sqlite`
    - typically a custom value is set by the administrator in `properties-local.xml`
    - a custom provider can use this to determine which datasource to use if needed, but it can also ignore it if datasources are not relevant to the particular implementation

#### HTTP request body

The body is empty.

#### HTTP response body

See [Revision History API](revision-history.md#response).

#### HTTP response status codes

- `200`: success
- `500`: internal server error
- `400`: bad request

#### HTTP response headers

- `Content-Type`
    - `application/xml` 

## Scenarios

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

_NOTE: You cannot name a provider `provider` or `persistence`._

In addition, you must configure the following properties to describe what your provider supports:

```xml
<!-- Whether the provider supports versioning -->
<property as="xs:boolean" name="oxf.fr.persistence.my-acme-provider.versioning" value="false"/>

<!-- Whether the provider supports the Lease feature -->
<property as="xs:boolean" name="oxf.fr.persistence.my-acme-provider.lease"      value="false"/>

<!-- Whether the provider supports reindexing -->
<property as="xs:boolean" name="oxf.fr.persistence.my-acme-provider.reindex"    value="false"/>

<!-- Whether the provider supports re-encryption -->
<property as="xs:boolean" name="oxf.fr.persistence.my-acme-provider.reencrypt"  value="false"/>

<!-- Whether the provider supports distinct values -->
<property as="xs:boolean" name="oxf.fr.persistence.my-acme-provider.distinct"   value="false"/>

<!-- Whether the provider supports sorting -->
<property as="xs:boolean" name="oxf.fr.persistence.my-acme-provider.sort"       value="false"/>
```

## See also

- [CRUD API](crud.md)
- [Form Metadata API](forms-metadata.md)
- [Reindexing](reindexing.md)
- [Search API](search.md)
- [Revision History API](revision-history.md)
- [Lease API](lease.md)
- [Reindexing API](reindexing.md)
- [Caching](caching.md)
- [Relational](/form-runner/persistence/relational-db.md)