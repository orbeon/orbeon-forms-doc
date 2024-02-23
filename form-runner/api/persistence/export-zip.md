# Zip export API

## Service endpoint

HTTP `GET` to the following path:

```
/fr/service/persistence/export
```

## Purpose

The Zip export API is used to export a form definition and its data in a single ZIP file. This is internally used by the [Forms Admin page](/form-runner/feature/forms-admin-page.md) to export a form definition and its data.

## API

### Request

#### Basics

The following URL parameters are supported:

| Parameter                     | Description                                            | Required | Multiple | Format                                            |
|-------------------------------|--------------------------------------------------------|----------|----------|---------------------------------------------------|
| `match`                       | specify a given app/form/version triplet to export     | Yes      | Yes      | see below                                         |
| `content`                     | whether to export form definitions, form data, or both | Yes      | No       | `definition` and/or `form-data` tokens, see below |
| `data-revision-history`       | whether to export data revision history                | No       | No       | `exclude`, `include`, `only` token, see below     |
| `data-last-modified-time-gte` | minimum last modified date of the data, included       | No       | No       | ISO date                                          |
| `data-last-modified-time-lt`  | maximum last modified date of the data, excluded       | No       | No       | ISO date                                          |

The `match` parameter is a triplet of app, form, and version, separated by slashes. The app and form are required, and the version is optional. If the version is not specified, the latest version is used.

Here are the parts of the triplet:

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

The `content` parameter is a comma-separated list of tokens, which can include `form-definition` and/or `form-data`:

- `form-definition`: export the form definition
- `form-data`: export the form data
- `form-definition,form-data`: export both the form definition and the form data

The `data-revision-history` parameter is a token which can be one of:

- `exclude`: don't include data revision history but only the data itself
- `include`: include both data revision history and the data itself
- `only`: only include data revision history

`data-revision-history` only applies when exporting form data.

The `data-last-modified-time-gte` and `data-last-modified-time-lt` parameters are used to filter the data to export based on the last modified time of the data. Both parameters are optional, and if both are omitted, all data is exported. They only apply when exporting form data.

#### Examples

Export all forms, including all versions, and form data, including historical data:

```
/fr/service/persistence/export?match=//all&content=form-definition,form-data&data-revision-history=include
```

Export the latest version of all forms, and form data, including historical data:

```
/fr/service/persistence/export?match=//latest&content=form-definition,form-data&data-revision-history=include
```

Export all forms, including all versions, and form data, excluding historical data:

```
/fr/service/persistence/export?match=//all&content=form-definition,form-data&data-revision-history=exclude
```

Export all forms within the `acme` app, including all versions, and form data, including historical data:

```
/fr/service/persistence/export?match=acme//all&content=form-definition,form-data&data-revision-history=include
```

Export the data only, including historical data, for the acme/sales version 3 form:

```
/fr/service/persistence/export?match=acme/sales/3&content=form-data&data-revision-history=include
```

### Response

The response contains a ZIP file binary. The structure is documented [here](/form-runner/feature/exporting-form-definitions-and-form-data.md#zip-file-structure).

## See also

- [Exporting form definitions and form data](/form-runner/feature/exporting-form-definitions-and-form-data.md)
