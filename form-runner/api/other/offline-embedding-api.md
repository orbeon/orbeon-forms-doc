# Offline embedding API

## Availability

[SINCE Orbeon Forms 2021.1]

As of Orbeon Forms 2022.1 this feature is considered in "beta" status.

## Introduction

Since its inception, Orbeon Forms has had a hybrid architecture for forms:

- the user interface runs in the browser, implemented in JavaScript (and more recently Scala.js)
- the form's logic and validations runs on the server

This architecture has benefits, such as protecting the confidentiality of internal data that never leaves the server.

This is still the case with the Orbeon Forms 2021.1, however we made lots of internal changes to support running Form Runner in a pure JavaScript environment. This will, in the future, allow running forms entirely offline, as well as embedded within mobile apps.

With Orbeon Forms 2021.1, we are releasing a preview of this feature. From Form Builder, simply use the new "Test Offline" button to see whether your form operates and renders properly in this new mode.

![The "Test Offline" button](/form-builder/images/test-offline-button.png)

When you do this:

- The form definition edited in Form Builder is compiled to a serialized representation.
- The JavaScript-based form runtime is loaded in the Form Builder test window, loads the compiled form, and renders it. 

From the user's perspective, this works almost exactly like the "Test" button which has always been present in Form Builder.

As of Orbeon Forms 2021.1, there are limitations, including the following:
 
- The APIs to compile and embed forms are not yet documented.
- Some controls are not fully supported, including the Formatted Text Area as well as attachment controls.
- Some formulas might not work.
- Performance needs some improvements.
- There is no XML Schema support for datatype validation (although this is rarely used).

## Uses

As of Orbeon Forms 2023.1:

- The offline embedding API is only relevant for users who want to embed Orbeon Forms in another application, typically a web view within a mobile app.
- Orbeon Forms does not yet provide an offline mode for end users of the regular Form Runner web application. For more about this, see [this issue](https://github.com/orbeon/orbeon-forms/issues/5184).

## APIs

### Server-side form compilation API

An Orbeon form first needs to be *compiled* into a serialized representation. This is done on the server-side, and the result is a zip file containing the compiled form definition, as well as any resources referenced by the form.

In order to obtain a compiled form definition, you call the following service endpoint:

```
/fr/service/$app/$form/compile?format=zip
```

The resulting binary data can later be passed to the client-side `renderFormFromBase64()` API.

### Client-side embedding API

TODO

ORBEON.fr.FormRunnerOffline()

renderFormFromBase64()

configure()

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
    - asynchronous call
    - [\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)
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

