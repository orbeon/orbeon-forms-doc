# Zip Export API

## Service endpoint

HTTP `GET` to the following path:

```
/fr/service/persistence/export
```

## Purpose

The Zip Export API is used to export a form definition and its data in a single ZIP file. This is internally used by the [Forms Admin page](/form-runner/feature/forms-admin-page.md) to export a form definition and its data.

## API

### Request

#### Parameters

The following URL parameters are supported:

| Parameter                     | Description                                            | Required | Multiple | Format                                                  |
|-------------------------------|--------------------------------------------------------|----------|----------|---------------------------------------------------------|
| `match`                       | specify a given app/form/version triplet to export     | Yes      | Yes      | see below                                               |
| `content`                     | whether to export form definitions, form data, or both | Yes      | No       | `definition` and/or `form-data` tokens, see below       |
| `data-revision-history`       | whether to export data revision history                | No       | No       | `exclude` (default), `include`, `only` token, see below |
| `data-last-modified-time-gte` | minimum last modified date of the data, included       | No       | No       | ISO date                                                |
| `data-last-modified-time-lt`  | maximum last modified date of the data, excluded       | No       | No       | ISO date                                                |

#### Matching app names, form names, and form versions

The `match` parameter is a triplet of app, form, and version, separated by slashes. Here are the parts of the triplet:

1. app name
    - specific app name
    - empty: all forms
2. form name
    - specific form name
    - empty: all forms (only allowed if the app name is also empty)
3. form version
    - specific version as a positive integer
    - `latest`: the latest version
    - `all`: all versions

#### Form definitions and form data

The `content` parameter is a comma-separated list of tokens, which can include `form-definition` and/or `form-data`:

- `form-definition`: export the form definition
- `form-data`: export the form data
- `form-definition,form-data`: export both the form definition and the form data

#### Historical data

The `data-revision-history` parameter is a token which can be one of:

- `exclude`: don't include data revision history but only the data itself (default)
- `include`: include both data revision history and the data itself
- `only`: only include data revision history

`data-revision-history` only applies when exporting form data.

#### Filtering by date range

The `data-last-modified-time-gte` and `data-last-modified-time-lt` parameters are used to filter the data to export based on the last modified time of the data. Both parameters are optional, and if both are omitted, all data is exported. They only apply when exporting form data.

The dates represent instants in time, are used for comparison with last modified dates in the database.

The dates in ISO format can specify a time zone offset, for example `+01:00` for Central European Time. Otherwise, make sure the dates are in UTC, with a `Z` suffix:

- `2024-02-01T00:00:00+01:00`
- `2024-02-01T00:00:00Z`

If you want, for example, the form data saved on a specific day, you can use the `data-last-modified-time-gte` and `data-last-modified-time-lt` parameters to specify a date range. However, consider that the first date/time is inclusive, and the second date/time is exclusive. So the parameters would look like (URL-encoded):

- `data-last-modified-time-gte=2024-02-01T00%3A00%3A00Z`: on or after February 1, 2024 at midnight
- `data-last-modified-time-lt=2024-02-03T00%3A00%3A00Z`: but before February 3, 2024 at midnight

#### Examples

Export all forms, including all versions, and form data, including historical data:

```
/fr/service/persistence/export?
    match=//all&
    content=form-definition,form-data&
    data-revision-history=include
```

Export the latest version of all forms, and form data, including historical data:

```
/fr/service/persistence/export?
    match=//latest&
    content=form-definition,form-data&
    data-revision-history=include
```

Export all forms, including all versions, and form data, excluding historical data:

```
/fr/service/persistence/export?
    match=//all&
    content=form-definition,form-data&
    data-revision-history=exclude
```

Export all forms within the `acme` app, including all versions, and form data, including historical data:

```
/fr/service/persistence/export?
    match=acme//all&
    content=form-definition,form-data&
    data-revision-history=include
```

Export the data only, including historical data, for the acme/sales version 3 form:

```
/fr/service/persistence/export?
    match=acme/sales/3&
    content=form-data&
    data-revision-history=include
```

Export the form definitions and form data for the latest versions of the orbeon/travel and orbeon/bookshelf forms, including historical data, between the dates of February 1, 2024, included and February 11, 2024, excluded.

```
/fr/service/persistence/export?
   match=orbeon/travel/latest&
   match=orbeon/bookshelf/latest&
   content=form-definition,form-data&
   data-revision-history=include&
   data-last-modified-time-gte=2024-02-01T00%3A00%3A00Z&
   data-last-modified-time-lt=2024-02-11T00%3A00%3A00Z
```

### Response

The response contains a ZIP file binary. The structure is documented [here](/form-runner/feature/exporting-form-definitions-and-form-data.md#zip-file-structure).

## See also

- [Form definitions and form data Zip export](/form-runner/feature/exporting-form-definitions-and-form-data.md)
- [Revision History API](/form-runner/api/persistence/revision-history.md)
- [Blog post: Exporting form definitions and data](https://www.orbeon.com/2024/04/form-data-export)