# Revision history API

## Service endpoint

HTTP `GET` to the following path:

```
/fr/service/persistence/history/$app/$forms/$document-id
```

## Purpose

The revision history API is used to retrieve the revision history of a form data document.

## API

### Request

#### Path elements

The endpoint path requires values for the following path elements:

| Path element  | Description | Mandatory |
|---------------|-------------|-----------|
| `app`         | app name    | Yes       |
| `form`        | form name   | Yes       |
| `document-id` | document id | Yes       |

These specify the specific form data document for which to retrieve the revision history.

Note that this API does not retrieve draft form data, only final form data.

#### Parameters

The following URL parameters are supported:

| Parameter     | Description                                                 | Required | Multiple | Format           |
|---------------|-------------------------------------------------------------|----------|----------|------------------|
| `page-number` | the page number, starting at 1                              | No       | No       | positive integer |
| `page-size`   | the number of items per page, defaulting to 10, maximum 100 | No       | No       | positive integer |

A client can call the API using paging, to obtain smaller chunks of the revision history. A first request can, for example, use `page-number=1` and `page-size=10`. If the response indicates that there are more items, the client can then call the API again with `page-number=2` and `page-size=10`, and so on.

#### Examples

Retrieve the first page of revision history for the orbeon/bookshelf form data document with id `adcfea40124ef8e68fa81764df389d5a80b61762`:

```
/fr/service/persistence/history/orbeon/bookshelf/adcfea40124ef8e68fa81764df389d5a80b61762?
    page-number=1&
    page-size=10
```

### Response

The response is an XML document. Example:

```xml
<documents
    application-name="orbeon"
    form-name="bookshelf"
    document-id="adcfea40124ef8e68fa81764df389d5a80b61762"
    total="3"
    page-size="10"
    page-number="1"
    form-version="1"
    created-time="2024-01-18T01:37:29.206Z"
    created-username="">
    <document 
        modified-time="2024-02-22T22:36:34.018Z" 
        modified-username=""
        owner-username="alice@acme.org"
        owner-group=""
        deleted="false"/>
    <document 
        modified-time="2024-02-22T22:36:25.898Z" 
        modified-username="john@example.org"
        owner-username="alice@acme.org"
        owner-group=""
        deleted="false"/>
    <document 
        modified-time="2024-01-22T21:43:53.397Z"
        modified-username="alice@acme.org"
        owner-username="alice@acme.org"
        owner-group=""
        deleted="false"/>
</documents>
```

The following elements and attributes are returned:

- `application-name`: the application name, as requested in the URL
- `form-name`: the form name, as requested in the URL
- `document-id`: the document id, as requested in the URL
- `total`: the total number of entries (current and historical) found in the database
- `page-size`: the number of items per page, as requested in the URL
- `page-number`: the page number, starting at 1, as requested in the URL
- `form-version`: the form version associated with the form data
- `created-time`: the time the form data was initially created
- `created-username`: the username of the user who created the form data; empty if the data was modified by an anonymous user
- `document`: element describing one of the form data documents
    - `modified-time`: the time the form data was last modified
    - `modified-username`: the username of the user who last modified the form data; empty if the data was modified by an anonymous user
    - `owner-username`: the username of the user who owns the form data
    - `owner-group`: the group of the user who owns the form data; empty if there is no user group information
    - `deleted`: whether the form data was marked as deleted

## See also

- [Revision history](/form-runner/feature/revision-history.md)
- [Zip export API](/form-runner/api/persistence/export-zip.md)
