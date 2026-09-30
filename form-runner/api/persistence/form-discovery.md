# Form Discovery API

## Availability

[\[SINCE Orbeon Forms 2025.1.3\]](/release-notes/orbeon-forms-2025.1.3.md)

## Service endpoints

HTTP `GET` to the following paths:

- `/fr/service/persistence/distinct-apps`: returns distinct application names
- `/fr/service/persistence/distinct-forms/$app`: returns distinct form names for a given application
- `/fr/service/persistence/distinct-versions/$app/$form`: returns distinct version numbers for a given application and form

## Purpose

The Form Discovery API allows callers to retrieve distinct, sorted lists of:

- Published application names
- Published form names for a given application
- Published form version numbers for a given application and form

This is particularly useful when building cascading selection menus or dynamic dropdowns (e.g., selecting an application first, then selecting from the forms available for that application, and finally selecting a version).

Unlike the [Form Metadata API](forms-metadata.md), which returns full metadata documents (including localized titles, description, timestamps, and permissions), this API returns only the unique values, reducing payload size and parsing overhead.

For example, internally, this API is used by the [Forms Admin page](/form-runner/feature/forms-admin-page.md) for the Export and Purge dialogs to populate application, form, and version dropdowns.

It can also be used by external applications, CI/CD scripts, or custom administration tools that need to discover available forms without the overhead of retrieving full form metadata.

## HTTP method

Only the `GET` method is supported. Any other HTTP method (`POST`, `PUT`, `DELETE`, etc.) returns `405 Method Not Allowed`.

## Parameters

The following optional URL query parameters are supported:

| Parameter                  | Type                     | Default                                                                              | Description                                                                                                                                                                                                                    |
|----------------------------|--------------------------|--------------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `all-versions`             | boolean (`true`/`false`) | `false` (for `distinct-apps` and `distinct-forms`), `true` (for `distinct-versions`) | When `false`, only the latest version of each form definition is considered. When `true`, all published versions of each form definition are considered.                                                                       |
| `all-forms`                | boolean (`true`/`false`) | `false`                                                                              | When `false`, only available forms (`available = true`) that the user has permission to access are returned. When `true`, all form definitions are returned, including unavailable forms and library forms (`orbeon/library`). |
| `ignore-admin-permissions` | boolean (`true`/`false`) | `false`                                                                              | When `false`, users with administrative permissions see all available forms. When `true`, administrative permissions are ignored and standard form permissions apply.                                                          |

[//]: # (| `modified-since`           | ISO 8601 date/time       | None                                                                                 | When specified, only form definitions modified on or after this timestamp are considered.                                                                                                                                      |)

## Content negotiation and response formats

The API supports content negotiation via the HTTP `Accept` header.

### XML response

XML is returned by default when no `Accept` header is provided, or when the `Accept` header explicitly requests XML (such as `application/xml` or `text/xml`).

The response is an XML document with a root element `<_>` and a sequence of `<_>` child elements.

- For `distinct-apps`, each child `<_>` element contains an application name (sorted alphabetically).
- For `distinct-forms`, each child `<_>` element contains a form name (sorted alphabetically).
- For `distinct-versions`, each child `<_>` element contains a version number (sorted numerically in ascending order).
- If no matching items exist, an empty `<_/>` element is returned.

#### Examples

Distinct applications:

```xml
<_>
    <_>acme</_>
    <_>orbeon</_>
</_>
```

Distinct forms for application `acme`:

```xml
<_>
    <_>expense-report</_>
    <_>order</_>
</_>
```

Distinct versions for application `acme` and form `order`:

```xml
<_>
    <_>1</_>
    <_>2</_>
    <_>3</_>
</_>
```

Empty result (e.g. for a non-existent application or form):

```xml
<_/>
```

### JSON response

JSON is returned when the `Accept` header specifies `application/json` (or `*/*`).

The response is a JSON array formatted with indentation:

- For `distinct-apps` and `distinct-forms`, a JSON array of strings (sorted alphabetically).
- For `distinct-versions`, a JSON array of numbers (sorted numerically in ascending order).
- If no matching items exist, an empty JSON array `[]` is returned.

#### Examples

Distinct applications:

```json
[
  "acme",
  "orbeon"
]
```

Distinct forms for application `acme`:

```json
[
  "expense-report",
  "order"
]
```

Distinct versions for application `acme` and form `order`:

```json
[
  1,
  2,
  3
]
```

Empty result (e.g. for a non-existent application or form):

```json
[]
```

## Authentication and permissions

Access to the Form Discovery API follows the standard Form Runner service authentication rules. See [Authentication of server-side service APIs](/form-runner/api/authentication.md).

User credentials can be provided using standard Orbeon Forms headers (`Orbeon-Username`, `Orbeon-Group`, `Orbeon-Roles`, or `Orbeon-Credentials`).

Forms for which the user does not have permission are automatically excluded from the results, unless the user has administrative privileges or `all-forms=true` is passed.

## Persistence providers

The Form Discovery API is implemented in the Persistence Proxy layer. It queries published form definitions via the [Form Metadata API](forms-metadata.md) internally and extracts the distinct values in memory.

As a result:

- Custom persistence providers do **not** need to implement separate endpoints for distinct applications, forms, or versions. As long as a provider supports form metadata, this API works automatically.
- When remote servers are configured in Orbeon Forms, form definitions from remote servers are also discovered and merged into the results.

## Examples

### Getting distinct applications

```bash
# Request XML (default)
curl \
  --user api:password \
  http://localhost:8080/orbeon/fr/service/persistence/distinct-apps

# Request JSON
curl \
  --user api:password \
  --header "Accept: application/json" \
  http://localhost:8080/orbeon/fr/service/persistence/distinct-apps
```

### Getting distinct forms for an application

```bash
curl \
  --user api:password \
  --header "Accept: application/json" \
  http://localhost:8080/orbeon/fr/service/persistence/distinct-forms/acme
```

### Getting distinct versions for a form

```bash
curl \
  --user api:password \
  --header "Accept: application/json" \
  http://localhost:8080/orbeon/fr/service/persistence/distinct-versions/acme/order
```

### Including all form versions when listing applications

```bash
curl \
  --user api:password \
  --header "Accept: application/json" \
  "http://localhost:8080/orbeon/fr/service/persistence/distinct-apps?all-versions=true"
```

### Passing user credentials via headers

```bash
curl \
  --header "Accept: application/json" \
  --header "Orbeon-Username: jdoe" \
  --header "Orbeon-Roles: employee" \
  http://localhost:8080/orbeon/fr/service/persistence/distinct-apps
```

## See also

- [Form Metadata API](forms-metadata.md)
- [Zip Export API](export-zip.md)
- [Authentication of server-side service APIs](/form-runner/api/authentication.md)
- [Forms Admin page](/form-runner/feature/forms-admin-page.md)
- [Persistence API](README.md)
