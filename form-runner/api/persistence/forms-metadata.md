# Form Metadata API

## Service endpoint

HTTP `GET` (v1) or `POST` (v2) to the following path:

```
/fr/service/persistence/form
```

## Purpose

This API is used to obtain the list of published forms as well as associated metadata, including published versions, form titles, and permissions.

In particular, this API is used internally by:

- the [Published Forms page](/form-runner/feature/published-forms-page.md)
- the [Forms Admin page](/form-runner/feature/forms-admin-page.md)
- the [Zip Export API](/form-runner/api/persistence/export-zip.md)
- the [persistence proxy](custom-persistence-providers.md), to obtain a given form's permissions and versions

## API (v2)

[\[SINCE Orbeon Forms 2024.1\]](/release-notes/orbeon-forms-2024.1.md)

The Form Metadata API v2 tries to behave as much as possible like the first version of the API. The main difference is that it uses `POST` instead of `GET`. This allows for more complex requests to be passed to the API (filtering, sorting, paging).

### Request

#### Basics

All requests to the API must include an XML content body, with a `Content-Type: application/xml` header.

You get the list of all the published forms with a `POST` on:

``` 
/fr/service/persistence/form
```

with the following payload, which is the simplest request that can be sent:

```xml
<search/>
```

The following optional attributes can be added to the `search` element:

| Attribute                  | Default value | Description                                                                                 |
|----------------------------|---------------|---------------------------------------------------------------------------------------------|
| `all-forms`                | `false`       | [Same semantics as in v1](#returning-all-form-definitions)                                  |
| `ignore-admin-permissions` | `false`       | [Same semantics as in v1](#ignoring-admin-permissions)                                      |
| `xml:lang`                 | None          | Default language for language-specific filtering/sorting directives (e.g. `en`, `fr`, etc.) |

#### Filtering

To filter the results, you can add a `filter` element to your request:

```xml
<element metadata="application-name" match="exact">acme</element>
```

| Attribute    | Mandatory | Description                                                                                                 |
|--------------|-----------|-------------------------------------------------------------------------------------------------------------|
| `metadata`   | Yes       | The metadata to filter by                                                                                   |
| `match`      | Yes       | The match type                                                                                              |
| `xml:lang`   | No        | Language for language-specific metadata, overrides any request-wide value specified on the `search` element |
| `url`        | No        | URL of the remote server or local server if no URL specified                                                |
| `combinator` | No        | Combinator to use in case of multiple local and/or remote values                                            |

One or multiple filters can be specified. If no filter is present, all results are returned.

The following metadata are supported:

| Metadata           | Value type      | Language-specific | Description                                                                                                           |
|--------------------|-----------------|-------------------|-----------------------------------------------------------------------------------------------------------------------|
| `application-name` | String          | No                | Application name                                                                                                      |
| `form-name`        | String          | No                | Form name                                                                                                             |
| `form-version`     | Number          | No                | Form version                                                                                                          |
| `created`          | Date/time       | No                | Creation date/time                                                                                                    |
| `last-modified`    | Date/time       | No                | Last modification date/time                                                                                           |
| `last-modified-by` | String          | No                | Username of the user who last modified the form definition                                                            |
| `title`            | String          | Yes               | Form title                                                                                                            |
| `available`        | Boolean         | No                | Whether the form definition is available                                                                              |
| `operations`       | List of strings | No                | Space-separated list of operations allowed on the form (`admin`, `create`, `read`, `update`, `delete` and/or `list`)  |

The following match types are supported and follow the same semantics as in the [Search API](search.md#match-types):

| Match type  | Value type        | Description                                                    |
|-------------|-------------------|----------------------------------------------------------------|
| `exact`     | String, boolean   | Exact match                                                    |
| `substring` | String            | Substring match                                                |
| `gte`       | Date/time, number | Greater than or equal                                          |
| `gt`        | Date/time, number | Greater than                                                   |
| `lte`       | Date/time, number | Lower than or equal                                            |
| `lt`        | Date/time, number | Lower than                                                     |
| `token`     | List of strings   | All listed values must be found in the metadata (in any order) |

For an `exact` match on `form-version`, the special value `latest` can be used to retrieve the latest version of each form.

The following combinators are supported:

| Combinator | Value type               | Description                                                           |
|------------|--------------------------|-----------------------------------------------------------------------|
| `min`      | Date/time                | Least recent date/time                                                |
| `max`      | Date/time                | Most recent date/time                                                 |
| `or`       | Boolean, list of strings | Booleans: `OR` operator<br/>Lists of strings: union of values         |
| `and`      | Boolean, list of strings | Booleans: `AND` operator<br/>Lists of strings: intersection of values |

Date/time values are expected to be given in ISO format.

#### Sorting

To sort the results, you can add a `sort` element to your request:

```xml
<sort metadata="form-name" direction="asc"/>
```

| Attribute    | Mandatory | Description                                                                                                 |
|--------------|-----------|-------------------------------------------------------------------------------------------------------------|
| `metadata`   | Yes       | The metadata to sort by (see above)                                                                         |
| `direction`  | Yes       | The sort direction, either `asc` (ascending) or `desc` (descending)                                         |
| `xml:lang`   | No        | Language for language-specific metadata, overrides any request-wide value specified on the `search` element |
| `url`        | No        | Same semantics as on `filter` element (see above)                                                           |
| `combinator` | No        | Same semantics as on `filter` element (see above)                                                           |

If no `sort` element is specified, the results are sorted by last modification date/time, in descending order, using the most recent last modification date/time between the local server and any remote server (i.e. using the `max` combinator).

Limitations:

 - At the moment, only a single `sort` element is supported.

#### Paging

To page the results, you can use the following XML element:

```xml
<pagination page-number="1" page-size="10"/>
```

`page-number` is the 1-based page number and `page-size` is the maximum number of results to return per page.

Paging the results is optional. By default, all results are returned.

#### Remote servers

The Form Metadata API v2 supports querying one or multiple remote servers. To do so, you can add `remote-server` elements to your request:

```xml
<remote-server url="http://staging.example.org:8080/orbeon" username="orbeon-admin" password="changeme"/>
```

When `remote-server` elements are present, the API queries the remote servers in addition to the local providers. The results are then aggregated.

For more information about remote server configuration, see [Remote server operations](/form-runner/feature/forms-admin-page.md#remote-server-operations).

#### Examples

Retrieving the latest versions of any form with the application name `acme`, sorted by form name, and paginated:

```xml
<search>
    <filter metadata="application-name" match="exact">acme</filter>
    <filter metadata="form-version" match="exact">latest</filter>
    <sort metadata="form-name" direction="asc"/>
    <pagination page-number="1" page-size="10"/>
</search>
```

Retrieving any form that is available, either locally or remotely, sorted by the French title of the form on the local server:

```xml
<search>
    <remote-server url="http://staging.example.org:8080/orbeon" username="orbeon-admin" password="changeme"/>
    <filter metadata="available" match="exact" combinator="or">true</filter>
    <sort metadata="title" xml:lang="fr" direction="asc"/>
</search>
```

Retrieving forms that have the `admin` operation on the remote server:

```xml
<search>
    <remote-server url="http://staging.example.org:8080/orbeon" username="orbeon-admin" password="changeme"/>
    <filter metadata="operations" match="token" url="http://staging.example.org:8080/orbeon">admin</filter>
</search>
```

### Response

#### Response format

Response documents in v2 follow the same format as in v1:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<forms search-total="6">
    <form operations="*">
        <application-name>orbeon</application-name>
        <form-name>dmv-14</form-name>
        <form-version>1</form-version>
        <last-modified-time>2023-12-18T15:11:35.131Z</last-modified-time>
        <created>2023-12-18T15:11:35.131Z</created>
        <title xml:lang="en">Notice of Change of Address</title>
        <available>true</available>
    </form>
    <form operations="*">
        <application-name>orbeon</application-name>
        <form-name>controls</form-name>
        <form-version>2</form-version>
        <last-modified-time>2023-12-18T15:11:34.702Z</last-modified-time>
        <created>2023-12-18T15:11:34.702Z</created>
        <title xml:lang="en">All Form Controls</title>
        <title xml:lang="fr">Tous les contrôles de formulaire</title>
        <available>true</available>
    </form>
    <form operations="create list">
        <application-name>acme</application-name>
        <form-name>form-with-permissions</form-name>
        <form-version>6</form-version>
        <last-modified-time>2024-01-17T13:25:00.291Z</last-modified-time>
        <last-modified-by>admin</last-modified-by>
        <created>2024-01-17T13:25:00.291Z</created>
        <title xml:lang="en">Form with permissions</title>
        <available>true</available>
        <permissions>
            <permission operations="create"/>
            <permission operations="read -list">
                <owner/>
            </permission>
        </permissions>
    </form>
    <!-- ... more <form> elements ... -->
</forms>
```

The main difference with v1 is that metadata for remote servers can be included in the response as well, if credentials for remote servers are included in the request. In that case, metadata specific to remote servers will be included in `remote-server` elements. Each remote server is identified by its URL.

Example:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<forms search-total="113">
    <form operations="*">
        <application-name>orbeon</application-name>
        <form-name>controls</form-name>
        <form-version>2</form-version>
        <last-modified-time>2023-12-18T15:11:34.702Z</last-modified-time>
        <created>2023-12-18T15:11:34.702Z</created>
        <title xml:lang="en">All Form Controls</title>
        <title xml:lang="fr">Tous les contrôles de formulaire</title>
        <available>true</available>
        <remote-server operations="*" url="http://staging.example.org:8080/orbeon">
            <last-modified-time>2024-09-05T09:41:10.663Z</last-modified-time>
            <created>2024-09-05T09:41:10.663Z</created>
            <title xml:lang="en">All Form Controls</title>
            <title xml:lang="fr">Tous les contrôles de formulaire</title>
            <available>true</available>
        </remote-server>
    </form>
    <form>
        <application-name>orbeon</application-name>
        <form-name>placeholders</form-name>
        <form-version>1</form-version>
        <remote-server operations="*" url="http://staging.example.org:8080/orbeon">
            <last-modified-time>2024-09-05T09:41:10.423Z</last-modified-time>
            <created>2024-09-05T09:41:10.423Z</created>
            <title xml:lang="en">Appearance of Labels Inline or as Placeholders</title>
            <available>true</available>
        </remote-server>
    </form>
    <!-- ... -->
</forms>
```

The following elements are never included in the remote server metadata, as they are used to identify (and hence group) the form definitions: `application-name`, `form-name`, and `form-version`.

## API (v1)

### Request

#### Basics

You get the list of all the published forms with a `GET` on:

``` 
/fr/service/persistence/form
```

This, in turn, calls the corresponding API for each persistence API implementation defined in the properties as [*active*](/configuration/properties/persistence.md#property_active), since different forms can be published on different persistence implementations. For example, this might call MySQL implementation doing a `GET` on:
 
    /fr/service/mysql/form
    
Then it might call the eXist implementation with another `GET` on:
 
    /fr/service/exist/form
    
Finally, it aggregates the results returned by each implementation.

#### Restricting by app/form name

[SINCE Orbeon Forms 4.3]

Optionally, an app name or both an app name and form name can be specified on the URL. In that case, the API only returns information about published forms in that specific app, or that specific app and form is returned.

* When an app specified, the URL looks like:  
  `/fr/service/persistence/form/$app`
* When both an app and form name are specified, the URL looks like:  
  `/fr/service/persistence/form/$app/$form`
  
#### Returning all form definition versions
  
[SINCE Orbeon Forms 2016.2]

Optionally, you can pass the URL parameter `all-versions`:

- when set to `true`
    - All form definition versions are returned.
- when omitted or set to `false`
    - Only the form definition with the highest published version number is returned.

#### Returning all form definitions

[SINCE Orbeon Forms 2019.1, 2018.2.1, 2018.1.4, 2017.2.3]

Optionally, you can pass the URL parameter `all-forms`:

- when set to `true`
    - all forms are returned without checking for permissions or other criteria
- when omitted or set to `false` (which was the behavior before `all-forms` was supported)
    - Only forms to which the user has "access" are returned.
    - If the user has access as per `form-builder-permissions.xml`, then all form definitions are returned.
        - [SINCE Orbeon Forms 2022.1], this logic is ignored if `ignore-admin-permissions` is set to `true`.
    - Otherwise:
        - `library` form definitions are excluded 
        - forms which have permissions defined and for which the user has no permissions at all are excluded
        - unavailable form definitions are excluded

#### Filtering by date

[SINCE Orbeon Forms 2021.1]

Optionally, you can pass the URL parameter `modified-since`:

- this must be an ISO date/time
- when passed, only form definitions which have been modified since the given date/time are returned

For example:

```
/fr/service/persistence/form?modified-since=2021-09-09T04:56:42.257Z
```

#### Ignoring admin permissions

[SINCE Orbeon Forms 2022.1]

Optionally, you can pass the URL parameter `ignore-admin-permissions`:

- when set to `true`
    - Even if the user has access to the form as per `form-builder-permissions.xml`, the forms are filtered as [documented above](#returning-all-form-definitions).
- when omitted or set to `false` (the default)
    - The behavior is the same as before this parameter was supported.

If the `all-forms` parameter is set to `true`, then this parameter is ignored, and all forms are returned without checking for permissions or other criteria as [documented](#returning-all-form-definitions).

### Response

#### Response format 

Each `<form>` element contains:

- `<application-name>`
- `<form-name>`
- All the elements inside the *form metadata* instance of the corresponding form definition
    - This can be retrieved with the following XPath expression:  
      `/xh:html/xh:head/xf:model/xf:instance[@id = 'fr-form-metadata']/metadata/*`
    - [SINCE Orbeon Forms 2016.1]
        - The `<description>` and `<migration>` elements are excluded.
- `<last-modified-time>`
    - [SINCE Orbeon Forms 4.4]
    - [UNTIL Orbeon Forms 4.10.x]
        - last modification date/time for the app/form combination
    - [SINCE Orbeon Forms 2016.1]
        - last modification date/time for the app/form/version combination
- `<form-version>`
    - [SINCE Orbeon Forms 2016.1]
    - contains the version number of the given `<form>`
    - *NOTE: This is only returned when using a relational database, as the implementation of the persistence API for eXist [doesn't support versioning yet](https://github.com/orbeon/orbeon-forms/issues/1524).*
- `<available>`
    - set to `false` when the form definition is marked as not available
- `<permissions>`
    - contains the form definition permissions as contained in the form definition's metadata
    - this is required for permissions checking to work 

#### Latest published version

The document returned by this API looks like this:

```xml
<forms>
    <form operations="*">
        <application-name>orbeon</application-name>
        <form-name>bookshelf</form-name>
        <title xml:lang="en">Orbeon Forms Bookshelf</title>
        <title xml:lang="fr">Orbeon Forms Bookshelf</title>
        <last-modified-time>2014-06-04T11:21:33.043-07:00</last-modified-time>
        <form-version>1</form-version>
    </form>
    <form operations="*">
        <application-name>orbeon</application-name>
        <form-name>w9</form-name>
        <title xml:lang="en">Request for Taxpayer Identification Number and Certification</title>
        <last-modified-time>2014-06-04T11:21:34.051-07:00</last-modified-time>
        <form-version>3</form-version>
    </form>
    <form operations="create read update">
        <application-name>acme</application-name>
        <form-name>order</form-name>
        <title xml:lang="en">ACME Order Form</title>
        <title xml:lang="fr">Formulaire de commande ACME</title>
        <last-modified-time>2014-08-21T16:52:24.429-07:00</last-modified-time>
        <form-version>2</form-version>
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

#### All versions

The document returned by this API looks like this, here for the `acme/order` form:

```xml
<forms>
    <form operations="admin *">
        <application-name>acme</application-name>
        <form-name>order</form-name>
        <last-modified-time>2018-11-02T21:52:46.173Z</last-modified-time>
        <form-version>1</form-version>
        <title xml:lang="en">Contact</title>
        <title xml:lang="fr">Contact</title>
    </form>
    <form operations="admin *">
        <application-name>acme</application-name>
        <form-name>order</form-name>
        <last-modified-time>2018-11-02T21:54:22.481Z</last-modified-time>
        <form-version>2</form-version>
        <title xml:lang="en">Contact</title>
        <title xml:lang="fr">Contact</title>
    </form>
    <form operations="admin *">
        <application-name>acme</application-name>
        <form-name>order</form-name>
        <last-modified-time>2018-12-05T23:31:33.320Z</last-modified-time>
        <form-version>3</form-version>
        <title xml:lang="en">Contact</title>
        <title xml:lang="fr">Contact</title>
    </form>
    <form operations="admin *">
        <application-name>acme</application-name>
        <form-name>order</form-name>
        <last-modified-time>2018-12-13T00:12:11.650Z</last-modified-time>
        <form-version>4</form-version>
        <title xml:lang="en">Contact</title>
        <title xml:lang="fr">Contact</title>
    </form>
    <form operations="admin *">
        <application-name>acme</application-name>
        <form-name>order</form-name>
        <last-modified-time>2018-12-20T19:30:10.435Z</last-modified-time>
        <form-version>5</form-version>
        <title xml:lang="en">Contact</title>
        <title xml:lang="fr">Contact</title>
        <available>false</available>
    </form>
</forms>
```

Note that the versions do not have to be in order and some versions can be missing.

Here note that version 5 is marked as not available with:

```xml
<available>false</available>
```

## See also

- [CRUD](crud.md)
- [Reindexing](reindexing.md)
- [Search](search.md)
- [Form metadata](forms-metadata.md)
- [Caching](caching.md)
- [Versioning](versioning.md)
- [Custom persistence providers](custom-persistence-providers.md)
