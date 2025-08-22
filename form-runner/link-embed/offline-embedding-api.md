# Form Runner offline embedding API

## Availability

[SINCE Orbeon Forms 2021.1]

As of Orbeon Forms 2023.1 this feature is considered in "beta" status.

## Introduction

See the [Offline test](/form-builder/offline-test.md) feature for a rationale and for the quickest way to preview this
feature.

## Uses

As of Orbeon Forms 2023.1:

- The Form Runner offline embedding API is mainly relevant for users who want to embed Orbeon Forms in another
  application, typically a web view within a mobile app.
- Orbeon Forms does not yet provide an offline mode for end users of the regular Form Runner web application. For more
  about this, see [this issue](https://github.com/orbeon/orbeon-forms/issues/5184).

## APIs

### Server-side form compilation API

An Orbeon form first needs to be *compiled* into a serialized representation. This is done on the server-side, and the
result is a zip file containing the compiled form definition, as well as any resources referenced by the form.

In order to obtain a compiled form definition, you call the following service endpoint with an HTTP `GET` request:

```
/fr/service/$app/$form/compile?format=zip
```

The resulting binary data can later be passed to the client-side `renderForm()` API.

### Client-side embedding API

The API entry point is from the global JavaScript object:

```javascript
ORBEON.fr.FormRunnerOffline
```

You must provide a specific configuration before running `renderForm()` and other methods:

```javascript
ORBEON.fr.FormRunnerOffline.configure(
    submissionProvider,
    compiledFormCacheSize
);
```

The first argument is a `SubmissionProvider` instance, which is described below. This tells Form Runner how to read and
write form data, as well as how to call services.

The second, optional parameter, is the size of the compiled form cache, in number of compiled forms. This is used to
limit the number of compiled forms kept in memory.

The effect of calling `configure()` is global. It applies to all subsequent calls to `renderForm()`.

You can render the form in a given HTML element with id `orbeon-wrapper` as follows:

```javascript
window.ORBEON.fr.FormRunnerOffline.renderForm(
    document.querySelector("#orbeon-wrapper"),
    compiledFormDefinition,
    {
        "appName": "my-app",
        "formName": "my-form",
        "formVersion": 1,
        "mode": "new"
    }
);
```

This returns a `Promise` which resolves when the form initialization is complete.

The `compiledFormDefinition` parameter is the binary data obtained from the server-side compilation API, as
a `Uint8Array`.

The third argument is a JavaScript object with the following properties, many of which are optional.

| Property      | Type        | Description                          | Availability |
|---------------|-------------|--------------------------------------|--------------|
| `appName`     | `string`    |                                      |              |
| `formName`    | `string`    |                                      |              |
| `formVersion` | `number`    | positive integer                     |              |
| `mode`        | `string`    | `new`\|`edit`\|`view`                |              |
| `documentId`  | `string?`   | for `edit`\|`view` modes only        |              |
| `queryString` | `string?`   | optional                             |              |
| `headers`     | `Headers?`  | optional                             |              |
| `formData`    | `string?`   | for `POST`ed form data               |              |
| `userName`    | `string?`   | username for credentials functions   | 2024.1.2     |
| `groupName`   | `string?`   | group name for credentials functions | 2024.1.2     |
| `userRoles`   | `string[]?` | roles for credentials functions      | 2024.1.2     |

If `formData` is defined, it must be a string containing form data in XML format. This is the equivalent of performing an HTTP `POST` when online.

Note that regular reading/writing data is done through the `SubmissionProvider` interface, which is described below.

If you are only loading one form in the page, you can chain the calls as follows:

```javascript
ORBEON.fr.FormRunnerOffline.configure(
    submissionProvider,
    compiledFormCacheSize
).renderForm(
    document.querySelector("#orbeon-wrapper"),
    compiledFormDefinition,
    {
        "appName": "my-app",
        "formName": "my-form",
        "formVersion": 1,
        "mode": "new"
    }
);
```

### Client-side submission provider API

When running in offline mode, Form Runner needs to be able to:

- read form data
- write form data
- call services

This is done through the `SubmissionProvider` interface, which is implemented by the embedder:

```typescript
interface SubmissionRequest {
    method: string;
    url: URL;
    headers: Headers;
    body?: Uint8Array | ReadableStream<Uint8Array> | null
}

interface SubmissionResponse {
    statusCode: number;
    headers: Headers;
    body?: Uint8Array | Byte[] | ReadableStream<Uint8Array> | null;
}

// Interface to be implemented by the embedder to support offline submissions
interface SubmissionProvider {
    submit(req: SubmissionRequest): SubmissionResponse;

    submitAsync(req: SubmissionRequest): Promise<SubmissionResponse>;
}
```

#### `SubmissionProvider`

This is the main entry point. You implement your own `SubmissionProvider` class and pass it to
the `ORBEON.fr.FormRunnerOffline.configure()` function.

This class has two methods:

- `submit()`
    - synchronous call
    - only method supported until Orbeon Forms 2023.1
    - deprecated as of Orbeon Forms 2023.1
- `submitAsync()`
    - [\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)
    - asynchronous call
    - used for saving data
    - used for asynchronous service calls

#### `SubmissionRequest`

This is the request object passed to the `submit()` and `submitAsync()` methods.

It has the following properties:

- `method`
    - possible HTTP method
    - `GET`, `POST`, `PUT`, `DELETE`, `HEAD`
- `url`
    - JavaScript `URL` object
    - see the [URL](https://developer.mozilla.org/en-US/docs/Web/API/URL) documentation
    - this is the URL of the resource to be accessed
- `headers`
    - JavaScript `Headers` object
    - see the [Headers](https://developer.mozilla.org/en-US/docs/Web/API/Headers) documentation
    - HTTP headers associated with the request, including:
        - `Content-Type`: for methods that have a body: `POST`, `PUT`
        - `Orbeon-Workflow-Stage`: workflow stage information
        - `Orbeon-Form-Definition-Version`: form definition version information
- `body`
    - optional for methods that don't have a body: `GET`, `HEAD`, `DELETE`, in which case it can be left `undefined`
    - required for methods that have a body: `POST`, `PUT`
    - `Uint8Array` or `ReadableStream<Uint8Array>`
        - `Uint8Array` is used for synchronous calls
        - `ReadableStream<Uint8Array>` is used for asynchronous calls
            - see also the JavaScript [ReadableStream](https://developer.mozilla.org/en-US/docs/Web/API/ReadableStream)
              documentation

Using `ReadableStream` is the most difficult part of this API. This standard JavaScript API exposes a way to get data in
a streamable way, in chunks, asynchronously. This means that you don't need to have all your data in memory at once, and
you can start processing data as soon as it is available. You can also move data across network or application
boundaries asynchronously, for example between a Web View and a native app.

[This is an example](https://gist.github.com/ebruchez/b57887e624234d228c426ba0d893c189) of a demo SubmissionProvider
implementation which uses `ReadableStream`. The example is written in Scala, but the same exact logic applies to a
JavaScript or TypeScript implementation.

#### `SubmissionResponse`

This is the response object returned by the `submit()` and `submitAsync()` methods.

It has the following properties:

- `statusCode`
    - HTTP status code
    - `200`, `201`, `204`, `400`, `401`, `403`, `404`, `500`, etc.
- `headers`
    - JavaScript `Headers` object
    - see the [Headers](https://developer.mozilla.org/en-US/docs/Web/API/Headers) documentation
    - HTTP headers associated with the response, including:
        - `Content-Type`: for methods that return a body: `POST`, `GET`
        - `Orbeon-Workflow-Stage`: workflow stage information when reading form data
- `body`
    - for methods that return a body: `POST`, `GET`, left `undefined` for methods that don't return a body
    - `Uint8Array` or `ReadableStream<Uint8Array>`
        - `Uint8Array` is used for synchronous calls or asynchronous calls that return a body synchronously
        - `ReadableStream<Uint8Array>` is used for asynchronous calls that return a body asynchronously
            - see the [ReadableStream](https://developer.mozilla.org/en-US/docs/Web/API/ReadableStream) documentation

## Form Runner calls

### Introduction

Form Runner calls are made through the `SubmissionProvider` interface.

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

Form Runner persistence calls (saving and reading form data and attachments) use `submitAsync()` instead of `submit()`
in call cases.

### Saving form data

Form Runner starts by saving attachments, if needed (see below). Only if saving all attachments succeeds does Form
Runner save the form data itself.

Then Form Runner saves the form data itself.

- `submitAsync()` is called
- the `method` is `PUT`
- the `url` is the URL of the form data resource
    - for `save-final`: `/fr/service/persistence/crud/$app/$form/data/$document/data.xml`
    - for `save-progress`: `/fr/service/persistence/crud/$app/$form/draft/$document/data.xml`
        - this is only called if [autosave](/form-runner/persistence/autosave.md) is enabled
- the `headers` include:
    - `Content-Type`: `application/xml`
    - `Orbeon-Workflow-Stage`: workflow stage information
    - `Orbeon-Form-Definition-Version`: form definition version information

The XML body is passed as a `ReadableStream<Uint8Array>`. You can convert that to a `Promise<Uint8Array>` if needed.

### Reading form data

When Form Runner needs to read form data, it calls:

- `submitAsync()`
- the `method` is `GET`
- the `url` is the URL of the form data resource
    - for reading final data: `/fr/service/persistence/crud/$app/$form/data/$document/data.xml`
    - for reading autosaved data: `/fr/service/persistence/crud/$app/$form/draft/$document/data.xml`
- the `headers` must include:
    - `Content-Type`: `application/xml`
    - `Orbeon-Workflow-Stage`: workflow stage information, if applicable (in particular if this was specified when the
      data was saved)
    - `Orbeon-Form-Definition-Version`: form definition version

Here, the XML body can be returned as a `ReadableStream<Uint8Array>` or directly as a `Uint8Array`.

### Saving attachments

This works like saving data, except that:

- the `url` is the URL of the attachment resource
    - for `save-final`: `/fr/service/persistence/crud/$app/$form/data/$document/$attachment.bin`
    - for `save-progress`: `/fr/service/persistence/crud/$app/$form/draft/$document/$attachment.bin`
        - this is only called if [autosave](/form-runner/persistence/autosave.md) is enabled
- the `headers` include:
    - `Content-Type`: `application/octet-stream`
    - `Orbeon-Form-Definition-Version`: form definition version

Currently, Form Runner always uses a `.bin` extension for attachments, even if the original file had a different
extension. Similarly, Form Runner always uses `application/octet-stream` as the `Content-Type` for attachments, even if
the original file had a different `Content-Type`.

Form Runner will issue one call to `submitAsync()` per attachment.

Again, an attachment body is passed as a `ReadableStream<Uint8Array>`. The main difference, compared with form data, is
that attachments can be typically much larger: from a few megabytes to gigabytes, when images and videos are attached,
for example. This makes it all the more important to leverage `ReadableStream` to avoid having to load the entire
attachment in memory at once.

### Reading attachments

TODO

### Service calls

Form Runner will issue service calls through the `SubmissionProvider` API as well. This includes form author-defined service calls in Form Builder, as well as services calls specified in the Dynamic Dropdown, in particular.

For example a Dynamic Dropdown might call a service at the following URL with the `GET` method

```
/fr/service/custom/orbeon/controls/countries
```

Make sure you return a `Content-Type` header with a value of `application/xml` or `application/json`, depending on the format you return.

The service can then retrieve the list of countries from a database, and return the list as XML or JSON in a response body. Here again, you can return either a `ReadableStream<Uint8Array>` or a `Uint8Array`.

### Autosave

[\[SINCE Orbeon Forms 2024.1.3\]](/release-notes/orbeon-forms-2024.1.3.md)

Until this version, Form Runner would autosave data under the [normal conditions](../persistence/autosave.md#enabling-autosave).

However, the persistence provider considered to check the `oxf.fr.persistence.$name.autosave` property was the provider in use in the server-side JVM environment. This does not make sense in the JavaScript environment, where saving data goes through the [`SubmissionProvider` API](#submissionprovider).

Instead, since this version, a special persistence provider name called `javascript` is exposed. By default, for this provider, autosave is disabled. You can enable autosave for this provider with:

```xml
<property
    as="xs:boolean"
    name="oxf.fr.persistence.javascript.autosave"
    value="true"/>
```

## See also

- [Offline test](/form-builder/offline-test.md)
- [Form Runner JavaScript Embedding API](javascript-api.md)
