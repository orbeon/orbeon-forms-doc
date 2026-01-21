# Import upload callback API

## Availability

[\[SINCE Orbeon Forms 2025.1.1\]](/release-notes/orbeon-forms-2025.1.1.md)

This API is a simple callback API to let a third-party service when a file has been uploaded in the [Import](/form-runner/feature/excel-xml-import.md) page.

## Enabling the callback API

You enable the callback API by setting the following property in your `properties-local.xml` file:

```xml
<property
    as="xs:string"
    name="oxf.fr.import.attachment-callback.uri.*.*"
    value="http://localhost:8084/post"/>
```

Replace `http://localhost:8084/post` with the endpoint where the callback should be sent.

Where the `*.*` suffix means that this applies to all applications and all forms. You can also set this property for a specific application and/or form by replacing the `*` with the application name and/or form name.

## Request format

When a file is uploaded in the Import page, a `POST` request is sent to the configured endpoint. The request contains the following URL parameters:

- `filename`: the name of the uploaded file as reported by the browser at upload time
- `app`: the current form's app name
- `form`: the current form's form name
- `form-version`: the form definition version in use
- `document`: the current document id, if it was passed to the Import page (as the `document-id` URL parameter)

Headers:

- `Content-Type`: the media type of the uploaded file

Example URL generated:

```
http://localhost:8084/post
    ?filename=Orbeon Demo_ Feedback Form (en).xlsx
    &app=orbeon
    &form=feedback
    &form-version=1
    &document=9eff349bfd95aab8d4d5e048bd25a815
```

## Expected response

If the callback endpoint responds with a `2xx` status code, the upload is considered successful.

If the callback endpoint responds with a non-`2xx` status code, or if the connection fails, the upload is considered failed, and an error message is shown in the log files. However, the import process is allowed to continue.

## See also

- [Excel and XML import](/form-runner/feature/excel-xml-import.md)
- [Form Runner APIs](../)