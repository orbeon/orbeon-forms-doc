# Form Runner offline embedding API

## Availability

[SINCE Orbeon Forms 2021.1]

As of Orbeon Forms 2023.1 this feature is considered in "beta" status.

## Introduction

See the [Offline test](/form-builder/offline-test.md) feature for a rationale and for the quickest way to preview this feature.

## Uses

As of Orbeon Forms 2023.1:

- The Form Runner offline embedding API is mainly relevant for users who want to embed Orbeon Forms in another application, typically a web view within a mobile app.
- Orbeon Forms does not yet provide an offline mode for end users of the regular Form Runner web application. For more about this, see [this issue](https://github.com/orbeon/orbeon-forms/issues/5184).

## APIs

### Server-side form compilation API

An Orbeon form first needs to be *compiled* into a serialized representation. This is done on the server-side, and the result is a zip file containing the compiled form definition, as well as any resources referenced by the form.

In order to obtain a compiled form definition, you call the following service endpoint with an HTTP `GET` request:

```
/fr/service/$app/$form/compile?format=zip
```

The resulting binary data can later be passed to the client-side `renderFormFromBase64()` API.

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

The first argument is a `SubmissionProvider` instance, which is described below. This tells Form Runner how to read and write form data, as well as how to call services.

The second, optional parameter, is the size of the compiled form cache, in number of compiled forms. This is used to limit the number of compiled forms kept in memory.

The effect of calling `configure()` is global. It applies to all subsequent calls to `renderForm()`.

You can render the form in a given HTML element with id `orbeon-wrapper` as follows:

```javascript
window.ORBEON.fr.FormRunnerOffline.renderForm(
    document.querySelector("#orbeon-wrapper"),
    compiledFormDefinition,
    {
         "appName"    : "my-app",
         "formName"   : "my-form",
         "formVersion": 1,
         "mode"       : "new"
    }
);
```

This returns a `Promise` which resolves when the configuration is complete.

The `compiledFormDefinition` parameter is the binary data obtained from the server-side compilation API, as a `Uint8Array`.

The third argument is a JavaScript object with the following properties:

| Property      | Type       | Description              |
|---------------|------------|--------------------------|
| `appName`     | `string`   |                          |
| `formName`    | `string`   |                          |
| `formVersion` | `number`   | positive integer         |
| `mode`        | `string`   | `new`\|`edit`\|`view`    |
| `documentId`  | `string?`  | for `edit`\|`view` modes |
| `queryString` | `string?`  | optional                 |
| `headers`     | `Headers?` | optional                 |
| `formData`    | `string?`  | for `POST`ed form data   |

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
         "appName"    : "my-app",
         "formName"   : "my-form",
         "formVersion": 1,
         "mode"       : "new"
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
  method : string;
  url    : URL;
  headers: Headers;
  body?  : Uint8Array | ReadableStream[Uint8Array] | null
}

interface SubmissionResponse {
  statusCode: number;
  headers   : Headers;
  body?     : Uint8Array | Byte[] | ReadableStream[Uint8Array] | null;
}

// Interface to be implemented by the embedder to support offline submissions
interface SubmissionProvider {
  submit(req: SubmissionRequest): SubmissionResponse;
  submitAsync(req: SubmissionRequest): Promise<SubmissionResponse>;
}
```

#### `SubmissionProvider`

This is the main entry point. You implement your own `SubmissionProvider` class and pass it to the `ORBEON.fr.FormRunnerOffline.configure()` function.

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
    - optional for methods that don't have a body: `GET`, `HEAD`, `DELETE`
    - required for methods that have a body: `POST`, `PUT`
    - `Uint8Array` or `ReadableStream[Uint8Array]`
    - `Uint8Array` is used for synchronous calls
    - `ReadableStream[Uint8Array]` is used for asynchronous calls
        - see the [ReadableStream](https://developer.mozilla.org/en-US/docs/Web/API/ReadableStream) documentation

Using `ReadableStream` is the most difficult part of this API. This standard JavaScript API exposes a way to get data in a streamable way, in chunks, asynchronously. This means that you don't need to have all your data in memory at once, and you can start processing data as soon as it is available. You can also move data across network or application boundaries asynchronously, for example between a Web View and a native app. 

Example:

```javascript
TODO
```

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
    - for methods that return a body: `POST`, `GET`
    - `Uint8Array` or `ReadableStream[Uint8Array]`
    - `Uint8Array` is used for synchronous calls or asynchronous calls that return a body synchronously
    - `ReadableStream[Uint8Array]` is used for asynchronous calls that return a body asynchronously
        - see the [ReadableStream](https://developer.mozilla.org/en-US/docs/Web/API/ReadableStream) documentation

Like for `SubmissionRequest`, using `ReadableStream` is the most difficult part of this API.

Example:

```javascript
TODO
```

## See also

- [Offline test](/form-builder/offline-test.md)
- [Form Runner JavaScript Embedding API](javascript-api.md)
