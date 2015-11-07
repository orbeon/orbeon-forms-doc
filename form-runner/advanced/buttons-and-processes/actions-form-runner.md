# Form Runner actions

<!-- toc -->

## Introduction

These actions are specific to Form Runner. They allow you to validate, save and send data, in particular.

## validate

Validate form data.

- parameters
    - `level`: validation level to check: one of `error`, `warning`, or `info`
    - `property`: specifies a boolean property which, if `false`, skips validation (used for backward compatibility) [Orbeon Forms 4.2 only, removed in Orbeon Forms 4.3]
- result
    - success if data is valid
    - failure if data is invalid

## pending-uploads

Check whether there are pending uploads.

- parameters
    - none
- result
    - success if there are no pending uploads
    - failure if there are pending uploads

## save

Save data and attachments via the persistence layer.

- steps
    - dispatch `fr-data-save-prepare` to `fr-form-model`
    - save attachments
    - save XML
    - switch to `edit` mode (be aware of [#1653](https://github.com/orbeon/orbeon-forms/issues/1653))
    - dispatch `fr-data-save-done` to `fr-form-model`
- parameters
    - `draft`: "true" if must be saved as a draft [SINCE Orbeon Forms 4.4]
    - `query`: additional query parameters to pass the persistence layer (is an XPath value template) [SINCE Orbeon Forms 4.6.1]

Example of use of the `query` parameter:

```xml
<property as="xs:string"  name="oxf.fr.detail.process.save-final.*.*">
    require-uploads
    then validate-all
    then save(query = "foo=bar&amp;title={//title}")
    then success-message("save-success")
    recover error-message("database-error")
</property>
```

The full URL, for attachments as well as for the XML data, is composed of:

- the URL pointing to the persistence layer path, including the file name
- the following URL parameter
    - `valid`: whether the data sent satisfies validation rules

*NOTE: The `save` action doesn't check data validity before running.*

Example:

    http://example.org/orbeon/fr/service/persistence/crud/
        orbeon/
        bookshelf/
        data/891ce63e59c17348f6fda273afe28c2b/data.xml?
        valid=true

## email

Send an email with optionally XML form data, attachments, and PDF.

- parameters: none
- properties used: [`oxf.fr.email.*`](FIXME Form-Runner-~-Configuration-properties#email-settings)

## send

## Configuration

Send data to an HTTP URL.

- parameters [SINCE Orbeon Forms 4.4 except `property`]
    - `property`: specifies an optional property prefix
    - `uri`: URL to which to send the data
    - `method`: `GET`, `POST` (default), `PUT`
    - `prune`: whether to prune non-relevant nodes (`true` by default)
    - `annotate`: space-separated list of levels to annotate elements (the empty string by default)
    - `replace`: `all` to load the resulting response in the browser, or `none` (default)
    - `content`:
        - `xml` to send the XML data (default)
        - `pdf-url` to send the PDF URL
        - `tiff-url` to send the TIFF URL [SINCE Orbeon Forms 4.11]
        - `metadata`: to send form metadata [SINCE Orbeon Forms 4.7]
    - `data-format-version` [SINCE Orbeon Forms 4.8]:
        - `edge`: send the data in the latest internal format
        - `4.0.0`: send the data in the Orbeon Forms 4.0-compatible format (the default)
    - `parameters`:
        - space-separated list of standard parameters to automatically add to the URL (see below)
        - default: `app form form-version document valid language process data-format-version`
            - `form-version` added to defaults in Orbeon Forms 4.7
            - `process` added to defaults in Orbeon Forms 4.7
- properties used
    - property prefix + `.uri`: see `uri` parameter
    - property prefix + `.method`: see `method` parameter
    - property prefix + `.prune`: see `prune` parameter
    - property prefix + `.annotate`: see `annotate` parameter
    - property prefix + `.replace`: see `replace` parameter
    - property prefix + `.content`: see `content` parameter
    - property prefix + `.parameters`: see `content` parameter

Parameters have a higher precedence. In this example, the `uri` parameter is used, even if a `oxf.fr.detail.send.success.uri` property is present:

```ruby
send(property = "oxf.fr.detail.send.success", uri = "http://acme.org/orbeon")
```

*SECURITY NOTE: If `replace` is set to `all`, the content of resources or redirection URLs accessible by the Orbeon Forms server are forwarded to the web browser. Care must be taken to forward only resources that users of the application are allowed to see.*

[SINCE Orbeon Forms 4.4]

The following properties are XPath Value Templates evaluating in the context of the root element of the form data instance:

- `uri`
- `method`
- `prune`
- `annotate`
- `content`
- `parameters`
- `replace` [SINCE Orbeon Forms 4.7]

Example:

```xml
<property as="xs:string" name="oxf.fr.detail.send.success.uri.*.*">
    /fr/service/custom/orbeon/echo?action=submit&amp;foo={
        encode-for-uri(xxf:instance("fr-form-instance")//foo)
    }&amp;bar={
        encode-for-uri(xxf:instance("fr-form-instance")//bar)
    }
</property>
```

Note the use of the `encode-for-uri()` function which escapes the value to place after the `=` sign.

[SINCE Orbeon Forms 4.5]

If `replace` is set to `all` and the service issues a redirection via an HTTP status code, the redirection is propagated to the client. This also works with portlets.

## URL format

The full URL is composed of:

- the URL specified by the `uri` property
- the following URL parameters (when present in `parameters`)
    - `app`: the current form's app name
    - `form`: the current form's form name
    - `form-version`: the form definition version in use [SINCE Orbeon Forms 4.5]
    - `document`: the current document id
    - `valid`: whether the data sent satisfies validation rules
    - `language`: the language of the form at the time it was submitted [SINCE Orbeon Forms 4.5]
    - `noscript`: whether the noscript mode was in use [SINCE Orbeon Forms 4.6]
    - `process`: unique process id for the currently running process [SINCE Orbeon Forms 4.7]

Example:

    http://example.org/service?
        document=7520171020e65a1585e72574ae1fbe138c415bee&
        process=139ceb515f918d6d17030b81255d8a3dfa0501cc&
        valid=true&
        app=acme&
        form=invoice&
        form-version=1&
        language=en

## Sending a PDF URL

When `pdf-url` is specified, the XML document sent has the following format:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<url>/xforms-server/dynamic/567f14ee46c6b21640c1a5a7374d5ad8</url>
```

The PDF can be retrieved by accessing that path with the proper session cookie.

A use case for this is to submit the URL to a local confirmation page. The page can then link to the URL provided, and the user can download the PDF.

*NOTE: We realize that if the URL is sent to a remote server, requiring the session cookie is not ideal. We hope to address this in a future release of Orbeon Forms.*

## Sending a TIFF URL

[SINCE Orbeon Forms 4.11]

When `tiff-url` is specified, the XML document sent has the following format:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<url>/xforms-server/dynamic/567f14ee46c6b21640c1a5a7374d5ad8</url>
```

The TIFF can be retrieved by accessing that path with the proper session cookie.

A use case for this is to submit the URL to a local confirmation page. The page can then link to the URL provided, and the user can download the TIFF file.

*NOTE: We realize that if the URL is sent to a remote server, requiring the session cookie is not ideal. We hope to address this in a future release of Orbeon Forms.*

## Sending form metadata

[SINCE Orbeon Forms 4.7]

When `metadata` is specified, the XML document sent contains metadata per control. [This page](https://gist.github.com/orbeon/3684806b0a30a9a5ace9) shows examples based on the Orbeon Forms sample forms.

*NOTE: The `<value>` element is present only since Orbeon Forms 4.7.1.*

The metadata is linked to the data with the `for` attribute, which can contain multiple id values separated by a space. This associates the given piece of metadata with multiple values in the form data. This typically happens where there are repeated fields in the form, so that there is no duplication of identical metadata.

Here is an example of `send` process which sends XML data to a service, followed by sending metadata:

```xml
<property as="xs:string"  name="oxf.fr.detail.process.send.orbeon.*">
    require-uploads
    then validate-all
    then send(
        uri      = "http://localhost:8080/orbeon/xforms-sandbox/service/echo-xml",
        replace  = "none",
        method   = "post",
        content  = "xml",
        annotate = "id"
    )
    then send(
        uri      = "http://localhost:8080/orbeon/xforms-sandbox/service/echo-xml",
        replace  = "none",
        method   = "post",
        content  = "metadata"
    )
</property>
```

## Annotating XML data

`annotate` can contain the following tokens:

- `error`, `warning`, `info`: XML elements are annotated with information associated with the given level or levels.
- `id`: XML elements are annotated with a unique id. [SINCE Orbeon Forms 4.7]

If the property is missing or empty, no annotation takes place. For example:

```xml
    <property
      as="xs:string"
      name="oxf.fr.detail.send.success.annotate.acme.hr"
      value="warning info"/>
```

```xml
    <form xmlns:xxf="http://orbeon.org/oxf/xml/xforms">
        <my-section>
            <number xxf:info="Nice, greater than 1000!">2001</number>
            <text xxf:warning="Should be shorter than 10 characters">This is a bit too long!</text>
        </my-section>
    </form>
```

## set-data-status

[SINCE Orbeon Forms 4.7]

Set the status of the form data in memory.

- parameters
    - `status`: specifies the status of the data
        - `safe`: mark the data as in initial state or saved (default)
        - `unsafe`: mark the data as modified by the user and not saved

This action can be useful in conjunction with `send`. Upon successfully sending the data, if the data is not in addition saved to the local database, this action can be used to indicate to the user that the data is safe.

## navigate

Navigate to an external page via client-side GET.

- parameters
    - `uri`: specifies the URL to navigate to
    - `property`: specifies a property containing the URL to navigate to
    - by default, try to guess based on the parameter

You can also use the `navigate` action to execute JavaScript:

```
navigate(uri = "javascript:myFunction()")
```

[SINCE Orbeon Forms 4.6]

The URL value, whether directly or via a property, can be an XPath Value Template, which runs in the context of the root element of the main form instance:

```
navigate(uri = "http://example.org/{xxf:get-request-parameter('bar')}/{.//code}")
```

## success-message and error-message

- `success-message`: show a success message
    - parameters
        - `message`: message to show
        - `resource`: resource key pointing to the message
- `error-message`: show an error message
    - parameters
        - `message`: message to show
        - `resource`: resource key pointing to the message

[SINCE Orbeon Forms 4.7] The value of the `message` parameter and the message to which points the resource key in the `resource` parameter are interpreted as an XPath Value Template.

## confirm

[SINCE Orbeon Forms 4.5]

Show a confirmation dialog. If the user selects "No", the current process is aborted. If the user selects "Yes", the current process is resumed.

![Confirmation dialog](../confirm.png)

- parameters
    - `message`: message to show
    - `resource`: resource key pointing to the message

Example of use:

```
save
then confirm
then suspend
then send("oxf.fr.detail.send.success")'/>
```

*NOTE: The `confirm` action is not synchronous, so the process *must* be suspended right after or the process will continue before the dialog is shown to the user.*

You can use a specific confirmation message with the `message` parameter:

```xml
save
then confirm(message = "Please confirm that you would like to submit your data.")
then suspend
then send("oxf.fr.detail.send.success")'/>
```

You can also override the default confirmation message:

```xml
<property
  as="xs:string"
  name="oxf.fr.resource.*.*.en.detail.messages.confirmation-dialog-message"
  value="Are you sure you want to proceed?"/>
```

## Other actions

- `review`, `edit`: navigate to these Form Runner pages
- `summary`: navigate to this Form Runner page (a predefined process since 4.7)
- `visit-all`: mark all controls as visited
- `unvisit-all`: mark all controls as not visited
- `expand-all`: expand all sections
- `collapse-all`: collapse all sections
- `result-dialog`: show the result dialog
- `captcha`: trigger the captcha
- `wizard-prev`: navigate the wizard to the previous page
- `wizard-next`: navigate the wizard to the next page
