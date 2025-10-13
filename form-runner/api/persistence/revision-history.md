# Revision History API

## Availability

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

## Service endpoint

HTTP `GET` to the following path:

```
/fr/service/persistence/history/$app/$form/$document
```

## Purpose

The Revision History API is used to retrieve the revision history of a form data document.

## API

### Request

#### Path elements

The endpoint path requires values for the following path elements:

| Path element | Description | Mandatory |
|--------------|-------------|-----------|
| `app`        | App name    | Yes       |
| `form`       | Form name   | Yes       |
| `document`   | Document id | Yes       |

These specify the specific form data document for which to retrieve the revision history.

Note that this API does not retrieve draft form data, only final form data.

#### Parameters

The following URL parameters are supported:

| Parameter         | Description                                                 | Required | Multiple | Format           | Default       |
|-------------------|-------------------------------------------------------------|----------|----------|------------------|---------------|
| `page-number`     | The page number, starting at 1                              | No       | No       | Positive integer | 1             |
| `page-size`       | The number of items per page, defaulting to 10, maximum 100 | No       | No       | Positive integer | 10            |
| `include-diffs`   | Include diffs between consecutive revisions                 | No       | No       | Boolean          | False         |
| `lang`            | Language for labels in diffs                                | No       | No       | String           | See below     |
| `truncation-size` | Maximum size for diff values before truncation              | No       | No       | Positive integer | No truncation |

A client can call the API using paging, to obtain smaller chunks of the revision history. A first request can, for example, use `page-number=1` and `page-size=10`. If the response indicates that there are more items, the client can then call the API again with `page-number=2` and `page-size=10`, and so on.

[SINCE Orbeon Forms 2025.1] When `include-diffs` is set to `true`, the response will include a `<diffs>` element within each `<document>` element (except for the original/oldest revision), showing the differences between that revision and the previous one. The `lang` parameter specifies which language to use for control labels in the diffs. If not specified, the API will use the language specified by the [`oxf.fr.default-language` property](/configuration/properties/form-runner.md#default-language) or the fist language available in the form definition. The `truncation-size` parameter limits the length of diff values to prevent overly large responses.

#### Examples

Retrieve the first page of revision history for the orbeon/bookshelf form data document with id `adcfea40124ef8e68fa81764df389d5a80b61762`:

```
/fr/service/persistence/history/orbeon/bookshelf/adcfea40124ef8e68fa81764df389d5a80b61762?
    page-number=1&
    page-size=10
```

Retrieve the first page with diffs between consecutive revisions:

```
/fr/service/persistence/history/orbeon/public-records/e7fb0c80221b191bf18e95e08d55ef9242ddcf73?
    page-number=1&
    page-size=10&
    include-diffs=true&
    lang=en
```

### Response

The response is an XML document. Example:

```xml
<documents
    application-name="orbeon"
    form-name="bookshelf"
    document-id="adcfea40124ef8e68fa81764df389d5a80b61762"
    total="3"
    min-last-modified-time="2024-01-22T21:43:53.397Z"
    max-last-modified-time="2024-02-22T22:36:34.018Z"
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

Example response with `include-diffs=true`:

```xml
<documents
    application-name="orbeon"
    form-name="public-records"
    document-id="e7fb0c80221b191bf18e95e08d55ef9242ddcf73"
    total="3"
    min-last-modified-time="2025-08-25T07:48:14.732Z"
    max-last-modified-time="2025-10-13T08:45:39.678Z"
    page-size="10"
    page-number="1"
    form-version="1"
    created-time="2025-08-25T07:48:14.732Z"
    created-username="">
    <document
        modified-time="2025-10-13T08:45:39.678Z"
        modified-username="admin"
        owner-username="alice@acme.org"
        owner-group=""
        deleted="false">
        <diffs older-modified-time="2025-10-13T08:45:18.641Z">
            <diff type="value-changed" name="company">
                <label>Company</label>
                <from/>
                <to>Acme Corporation</to>
            </diff>
        </diffs>
    </document>
    <document
        modified-time="2025-10-13T08:45:18.641Z"
        modified-username="john@example.org"
        owner-username="alice@acme.org"
        owner-group=""
        deleted="false">
        <diffs older-modified-time="2025-08-25T07:48:14.732Z">
            <diff type="value-changed" name="preferred-method-of-contact">
                <label>Preferred method of contact in the event of questions</label>
                <from>Email</from>
                <to>Fax</to>
            </diff>
            <diff type="iteration-added" name="grid-3" count="1"/>
            <diff type="value-changed" name="control-3">
                <label/>
                <from/>
                <to>Bridge maintenance reports</to>
            </diff>
        </diffs>
    </document>
    <document
        modified-time="2025-08-25T07:48:14.732Z"
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
- `min-last-modified-time`:
    - [\[SINCE Orbeon Forms 2023.1.1\]](/release-notes/orbeon-forms-2023.1.1.md)
    - the earliest last modified time found in the database
- `max-last-modified-time`:
    - [\[SINCE Orbeon Forms 2023.1.1\]](/release-notes/orbeon-forms-2023.1.1.md)
    - the latest last modified time found in the database
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
    - [SINCE Orbeon Forms 2025.1] `diffs`: (only present when `include-diffs=true`) element containing the differences between this revision and the previous one
        - `older-modified-time`: the modification time of the older revision
        - `diff`: element describing a specific difference (may appear multiple times)
            - `type` (attribute): the type of difference, one of:
                - `value-changed`: a control value was changed
                - `iteration-added`: one or more iterations were added to a repeated grid or section
                - `iteration-removed`: one or more iterations were removed from a repeated grid or section
                - `element-added`: an element was added (in a non-repeated container)
                - `element-removed`: an element was removed (from a non-repeated container)
                - `other`: other (unspecified) types of changes
            - `name` (attribute): the name of the control, grid, or section
            - `count` (attribute): the number of iterations added or removed (for `iteration-added` and `iteration-removed` types)
            - `label` (sub-element): the label for the control, grid, or section in the specified language (if available)
                - `mediatype`: attribute indicating if the label contains HTML (`text/html`)
            - `from` (sub-element): the old value (for `value-changed` type)
            - `to` (sub-element): the new value (for `value-changed` type)

## Revision Diff API

[SINCE Orbeon Forms 2025.1]

### Service endpoint

HTTP `GET` to the following path:

```
/fr/service/persistence/history/$app/$form/$document/diff
```

### Purpose

The Revision Diff API is used to retrieve the differences between two specific revisions of a form data document.

### API

#### Request

##### Path elements

The endpoint path requires values for the following path elements:

| Path element | Description | Mandatory |
|--------------|-------------|-----------|
| `app`        | App name    | Yes       |
| `form`       | Form name   | Yes       |
| `document`   | Document id | Yes       |

##### Parameters

The following URL parameters are supported:

| Parameter             | Description                                    | Required | Multiple | Format           | Default   |
|-----------------------|------------------------------------------------|----------|----------|------------------|-----------|
| `form-version`        | The form version                               | Yes      | No       | Positive integer | -         |
| `older-modified-time` | The modification time of the older revision    | Yes      | No       | ISO 8601 instant | -         |
| `newer-modified-time` | The modification time of the newer revision    | Yes      | No       | ISO 8601 instant | -         |
| `lang`                | Language for labels in diffs                   | No       | No       | String           | See above |
| `truncation-size`     | Maximum size for diff values before truncation | No       | No       | Positive integer | See above |

##### Examples

Retrieve the differences between two specific revisions:

```
/fr/service/persistence/history/orbeon/bookshelf/adcfea40124ef8e68fa81764df389d5a80b61762/diff?
    form-version=1&
    older-modified-time=2024-01-22T21:43:53.397Z&
    newer-modified-time=2024-02-22T22:36:25.898Z&
    lang=en
```

#### Response

The response is an XML document containing a `<diffs>` element with the same structure as described in the Revision History API response when `include-diffs=true`. Example:

```xml
<diffs
    older-modified-time="2024-01-22T21:43:53.397Z"
    newer-modified-time="2024-02-22T22:36:25.898Z">
    <diff type="value-changed" name="title">
        <label>Title</label>
        <from>The Great Gastby</from>
        <to>The Great Gatsby</to>
    </diff>
    <diff type="iteration-added" name="note" count="1"/>
</diffs>
```

The response elements are:

- `diffs`: root element containing all differences
    - `older-modified-time`: the modification time of the older revision
    - `newer-modified-time`: the modification time of the newer revision
    - `diff`: element describing a specific difference (structure as documented in the Revision History API response)

### Limitations

- Section templates are not supported yet.
- Form Builder form definitions are not supported yet.

## See also

- Blog post: [Data Revision History](https://www.orbeon.com/2024/08/revision-history)
- [Revision history](/form-runner/feature/revision-history.md)
- [Zip Export API](/form-runner/api/persistence/export-zip.md)
