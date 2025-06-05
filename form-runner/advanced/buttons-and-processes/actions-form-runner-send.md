# Form Runner send action

##  Introduction

The `send` action is one of the most important actions that Orbeon Forms can perform with form data: it sends data to an HTTP (or HTTPS) service. The following sections detail the possible configuration parameters of this action. 

## Configuration

### Using parameters

[SINCE Orbeon Forms 4.4 except `property`]

The following example uses three parameters in the `send` action for the form `my-app/my-form`:

```xml
<property as="xs:string" name="oxf.fr.detail.process.send.my-app.my-form" >
    send(
        uri        = "http://example.org/accept-form",
        method     = "PUT",
        content    = "metadata"
    )
</property>
```

The following parameters can be used:

- <a name="send_parameter_property"></a>`property`: specifies an optional property prefix
- <a name="send_parameter_uri"></a>`uri`: URL to which to send the data
- <a name="send_parameter_method"></a>`method`: `GET`, `POST`(default), or `PUT`
- <a name="send_parameter_nonrelevant"></a>`nonrelevant`
    - [SINCE Orbeon Forms 2017.1]
    - values
        - `keep`: all values are serialized, 
        - `remove`: non-relevant values are not serialised 
        - `empty`: non-relevant nodes are serialized as empty values
    - default: `remove`
- <a name="send_parameter_prune"></a>`prune`
    - [DEPRECATED SINCE Orbeon Forms 2017.1]
    - use `nonrelevant` instead
    - whether to prune non-relevant nodes (`true` by default)
- <a name="send_parameter_annotate"></a>`annotate`: space-separated list of levels to annotate elements (the empty string by default)
- <a name="send_parameter_replace"></a>`replace`: `all` to load the resulting response in the browser, or `none` (default)

    [SINCE Orbeon Forms 4.5]

    If `replace` is set to `all` and the service issues a redirection via an HTTP status code, the redirection is propagated to the client. This also works with portlets.

    *SECURITY NOTE: If `replace` is set to `all`, the content of resources or redirection URLs accessible by the Orbeon Forms server are forwarded to the web browser. Care must be taken to forward only resources that users of the application are allowed to see.*

- <a name="send_parameter_content"></a>`content`:
    - `xml` to send the XML data (default)
    - `metadata` to send form metadata, see [details](#sending-form-metadata) [SINCE Orbeon Forms 4.7]
    - `pdf` to send the PDF binary, see [details](#sending-a-pdf-binary) [SINCE Orbeon Forms 2016.2]
    - `tiff` to send the TIFF binary, see [details](#sending-a-tiff-binary) [SINCE Orbeon Forms 2016.2]
    - `pdf-url` to send the PDF URL, see [details](#sending-a-pdf-url)
    - `tiff-url` to send the TIFF URL, see [details](#sending-a-tiff-url) [SINCE Orbeon Forms 2016.1]

- <a name="send_parameter_data-format-version"></a>`data-format-version` [SINCE Orbeon Forms 4.8]:
    - `edge`: send the data in the latest internal format
    - `2019.1.0`: send the data in the Orbeon Forms 2019.1-compatible format [SINCE Orbeon Forms 2019.1]
    - `4.8.0`: send the data in the Orbeon Forms 4.8-compatible format
    - `4.0.0`: send the data in the Orbeon Forms 4.0-compatible format (the default)
- <a name="send_parameter_parameters"></a>`parameters`: name of parameters sent to the service end point, in addition to the
    form content
    - space-separated list of standard parameters to automatically add to the URL (see below)
    - default: `app form form-version document valid language process data-format-version`
        - `form-version` added to defaults in Orbeon Forms 4.7
        - `process` added to defaults in Orbeon Forms 4.7
- <a name="send_parameter_serialization"></a>`serialization`:

    [SINCE Orbeon Forms 4.7]

    - determine the serialization of the XML data
    - values
        - `application/xml`: XML serialization
        - `none`: no serialization
    - default
        - `application/xml` when `method` is set to `post` or `put`
        - `none` otherwise

- <a name="send_parameter_prune_metadata"></a>`prune-metadata`:

    [SINCE Orbeon Forms 2016.1]

    - this is applied when `content` is set to `xml` only
    - `true` to remove all occurrences of `fr:`-prefixed elements
    - `false` to leave such occurrences
    - default
        - `false` when `data-format-version` is set to `edge`
        - `true` otherwise
        
- <a name="send_parameter_content_type"></a>`content-type`:

    [SINCE Orbeon Forms 2016.2]

    - specify the `Content-Type` header to set when `method` is set to `post` or `put`
    - it is usually not necessary to specify `content-type` explicitly
    - default
        - `application/xml` when `content` is set to `xml`, `metadata`, `pdf-url` or `tiff-url`
        - `application/pdf` when `content` is set to `pdf`
        - `image/tiff` when `content` is set to `tiff`

- <a name="send_parameter_show_progress"></a>`show-progress`:

    [SINCE Orbeon Forms 2017.1]

    - if `replace` is set to `all`, whether to continue showing the loading the indicator while the browser navigates away from the current page
    - typically, you'll only want to set this parameter to `false` if you know that URL the browser navigates to won't replace the current page, say because the page will be opened in another window, or be downloaded by the browser
    - default: `true`
    
- <a name="send_parameter_target"></a>`target`:

    [SINCE Orbeon Forms 2017.1]

    - if `replace` is set to `all`, specifies the name of the window where to display the result from the `send()`, with same semantic as the [HTML `target` attribute on `<a>`](https://developer.mozilla.org/en-US/docs/Web/HTML/Element/a#attr-target)
    - typically, if setting a `target`, you'll also want to add a `replace = "false"` attribute, so the loading indicator on the current page doesn't stay after the content in the target page has been loaded
    - default: none
    
- <a name="send_parameter_headers"></a>`headers`:

    [SINCE Orbeon Forms 2019.2]

    - specifies one or more custom HTTP headers to add to the HTTP request
    - a single header has the usual HTTP format:
        ```
        headers = "Foo: value of foo"
        ```
    - multiple headers can be separated with a `\n` newline escape sequence, as in:
        ```
        headers = "Foo: value of foo\nBar: value of bar"
        ```
    - a value template may be used, with the caveat about character encodings below:
        ```
        headers = "Foo: {//my-control}"
        ```
    - *NOTE: HTTP headers typically do not support all Unicode characters as header values.* 

### Using properties

The following example refers in the `send` action to the properties with the common
prefix `oxf.fr.detail.process.send.my-app.my-form`. It configures the URL, the method,
and the type of content using three additional sub-properties.

```xml
<property as="xs:string" name="oxf.fr.detail.process.send.my-app.my-form" >
    send("oxf.fr.detail.process.send.my-app.my-form")
</property>
<property
    as="xs:string"
    name="oxf.fr.detail.process.send.my-app.my-form.uri"
    value="http://example.org/accept-form"
    />
<property
    as="xs:string"
    name="oxf.fr.detail.process.send.my-app.my-form.method"
    value="PUT"
    />
<property
    as="xs:string"
    name="oxf.fr.detail.process.send.my-app.my-form.content"
    value="metadata"
    />
```

The following properties can be used to configure a `send` action with properties:

- property prefix + `.uri`: see [`uri` parameter](#send_parameter_property)
- property prefix + `.method`: see [`method` parameter](#send_parameter_method)
- property prefix + `.nonrelevant`: see [`prune` parameter](#send_parameter_nonrelevant)
- property prefix + `.prune`: see [`prune` parameter](#send_parameter_prune)
- property prefix + `.annotate`: see [`annotate` parameter](#send_parameter_annotate)
- property prefix + `.replace`: see [`replace` parameter](#send_parameter_replace)
- property prefix + `.content`: see [`content` parameter](#send_parameter_content)
- property prefix + `.parameters`: see [`parameters` parameter](#send_parameter_parameters)
- property prefix + `.serialization`: see [`serialization` parameter](#send_parameter_serialization)
- property prefix + `.prune-metadata`: see [`prune-metadata` parameter](#send_parameter_prune_metadata)
- property prefix + `.content-type`: see [`content-type` parameter](#send_parameter_content_type)
- property prefix + `.show-progress`: see [`show-progress` parameter](#send_parameter_show_progress)
- property prefix + `.target`: see [`target` parameter](#send_parameter_target)
- property prefix + `.headers`: see [`headers` parameter](#send_parameter_headers)

#### Properties and XPath Value Templates

[SINCE Orbeon Forms 4.4]

The following properties are XPath Value Templates evaluating in the context of the root element of the form data instance:

- `uri`
- `method`
- `headers`
- `prune`
- `annotate`
- `content`
- `parameters`
- `replace` [SINCE Orbeon Forms 4.7]

**Example**

```xml
<property as="xs:string" name="oxf.fr.detail.send.success.uri.my-app.my-form">
    /fr/service/custom/orbeon/echo?action=submit&amp;foo={
        encode-for-uri(xxf:instance("fr-form-instance")//foo)
    }&amp;bar={
        encode-for-uri(xxf:instance("fr-form-instance")//bar)
    }
</property>
```

Note the use of the `encode-for-uri()` function which escapes the value to place after the `=` sign.

### Precedence of parameters over properties

Parameters have a higher precedence. In this example, the `uri` parameter is used, even if a `oxf.fr.detail.send.success.uri` property is present:

```xpath
send(property = "oxf.fr.detail.send.success", uri = "http://acme.org/orbeon")
```

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
        - *NOTE: Starting with Orbeon Forms 2018.1, this always returns `false`.*
    - `process`: unique process id for the currently running process [SINCE Orbeon Forms 4.7]
    - `workflow-stage`: the current workflow stage associated with the data, as returned by the [`fr:workflow-stage-value()`](/xforms/xpath/extension-form-runner.md#fr-workflow-stage-value) function [SINCE Orbeon Forms 2021.1]

Example:

    http://example.org/service?
        document=7520171020e65a1585e72574ae1fbe138c415bee&
        process=139ceb515f918d6d17030b81255d8a3dfa0501cc&
        valid=true&
        app=acme&
        form=invoice&
        form-version=1&
        language=en
        
[SINCE Orbeon Forms 2018.2]
        
If a parameter name is already present on the URL, the parameter initially on the URL takes precedence. 

## Sending PDF and TIFF content 

### Controlling the format

[SINCE Orbeon Forms 2018.1]

When using PDF templates and `content = "pdf"` or `content = "tiff"`, you can control the PDF processing with the
following parameters. For more on these parameters, see the 
[`open-rendered-format` action](actions-form-runner.md#open-rendered-format). 

- `show-hints`
- `show-alerts`
- `show-required`
- `use-pdf-template`
- `pdf-template-name`
- `pdf-template-lang` 

See also [PDF templates](/form-runner/feature/pdf-templates.md)

### Sending a PDF binary

[SINCE Orbeon Forms 2016.2]

When `content = "pdf"` is specified, the PDF binary is sent with a `Content-Type` set to `application/pdf`.

### Sending a TIFF binary

[SINCE Orbeon Forms 2016.2]

When `content = "tiff"` is specified, the TIFF binary is sent with a `Content-Type` set to `image/tiff`.

### Sending a PDF URL

When `content = "pdf-url"` is specified, the XML document sent has the following format:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<url>/xforms-server/dynamic/567f14ee46c6b21640c1a5a7374d5ad8</url>
```

The PDF can be retrieved by accessing that path with the proper session cookie.

A use case for this is to submit the URL to a local confirmation page. The page can then link to the URL provided, and the user can download the PDF.

*NOTE: When the PDF must be sent to a remote service, send the PDF binary directly with `content = "pdf"` .*

### Sending a TIFF URL

[SINCE Orbeon Forms 2016.1]

When `content =  "tiff-url"` is specified, the XML document sent has the following format:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<url>/xforms-server/dynamic/567f14ee46c6b21640c1a5a7374d5ad8</url>
```

The TIFF can be retrieved by accessing that path with the proper session cookie.

A use case for this is to submit the URL to a local confirmation page. The page can then link to the URL provided, and the user can download the TIFF file.

*NOTE: When the TIFF must be sent to a remote service, send the TIFF binary directly with `content = "tiff"` .*

## Sending form metadata

[SINCE Orbeon Forms 4.7]

When `content = "metadata"` is specified, the XML document sent contains metadata per control. [This page](https://gist.github.com/orbeon/3684806b0a30a9a5ace9) shows examples based on the Orbeon Forms sample forms.

*NOTE: The `<value>` element is present only since Orbeon Forms 4.7.1.*

The metadata is linked to the data with the `for` attribute:

- The value of the `for` attribute can contain multiple ids separated by a space. This associates the given piece of metadata with multiple values in the form data. This typically happens where there are repeated fields in the form, so that there is no duplication of identical metadata.
- Ids in the `for` attribute match the ids you get on the data when asking `send()` to annotate the data with ids using the `edge` format, that is with a `send(annotate = "id", data-format-version = "edge")`.

Here is an example of `send` process which sends XML data to a service, followed by sending metadata:

```xml
<property as="xs:string"  name="oxf.fr.detail.process.send.my-app.my-form">
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

## Sending attachments and multiple items

[SINCE Orbeon Forms 2022.1]

You can send multiple items in a single `send` request. In this case, the items are sent using a multipart request (with a `Content-Type: multipart/related` header). This allows the recipient of the request to access all the items without performing multiple requests.

In order to generate a multipart request, you pass the `content` parameter one or more of the following tokens:

- `xml`: form data in XML format
- `metadata`: [form metadata](#sending-form-metadata) in XML format
- `attachments`: all attachments associated with the form data (but not those associated with the form definition) 
- `pdf`: [PDF binary](#sending-a-pdf-binar)
- `tiff`: [TIFF binary](#sending-a-tiff-binary)
- `excel-with-named-ranges`: [Excel export](https://blog.orbeon.com/2021/09/excel-export-and-import.html) 
- `xml-form-structure-and-data`: XML form structure and data export (not yet documented)

For example, to send form data with its attachments:

```xml
<property as="xs:string" name="oxf.fr.detail.process.send.my-app.my-form" >
    send(
        uri     = "http://example.org/accept-form",
        method  = "POST",
        content = "xml attachments"
    )
</property>
```

To send form data, attachments, and the PDF file:

```xml
<property as="xs:string" name="oxf.fr.detail.process.send.my-app.my-form" >
    send(
        uri     = "http://example.org/accept-form",
        method  = "POST",
        content = "xml attachments pdf"
    )
</property>
```

The recipient will need the ability to decode a multipart request. This is usually done with a utility library. For example [Apache Commons FileUpload](https://commons.apache.org/proper/commons-fileupload/).  

When sending XML data and attachments in the same request, paths to attachments in the XML data are replaced with `cid:` URIs. Each attachment part is given a `Content-ID` header with the corresponding id.

The `Content-Type` header sent by Orbeon Forms looks like this (the boundary parameter will change for each request):

```
Content-Type: multipart/related; boundary=CHZ6Pogx-A1VVuDgU22pcJASumg8S0CrOZhooqlw
``` 

The content body looks like this:

```
--CHZ6Pogx-A1VVuDgU22pcJASumg8S0CrOZhooqlw
Content-Disposition: form-data
Content-Type: application/xml; charset=UTF-8
Content-Transfer-Encoding: binary

<?xml version="1.0" encoding="UTF-8"?>
<form xmlns:fr="http://orbeon.org/oxf/xml/form-runner" fr:data-format-version="4.0.0">
    <my-main-section>
        <first-name>Bob</first-name>
        <pet-picture filename="cat.jpg" mediatype="image/jpeg" size="56803">cid:94b0e57e87fa8f42cb494fdc2808f58c5b31be41</pet-picture>
    </my-main-section>
</form>
--CHZ6Pogx-A1VVuDgU22pcJASumg8S0CrOZhooqlw
Content-ID: <94b0e57e87fa8f42cb494fdc2808f58c5b31be41>
Content-Disposition: attachment; filename*=UTF-8''cat.jpg
Content-Type: image/jpeg
Content-Transfer-Encoding: binary

...binary image content here...
--CHZ6Pogx-A1VVuDgU22pcJASumg8S0CrOZhooqlw
Content-ID: <7a361b305064d3a95511da35a8c53edbabe3af8b>
Content-Disposition: attachment; filename*=UTF-8''My%20demo%20multipart%20form%20-%20ce85cc4b7be9975d.pdf
Content-Type: application/pdf
Content-Transfer-Encoding: binary

...binary PDF content here...

--CHZ6Pogx-A1VVuDgU22pcJASumg8S0CrOZhooqlw--
```

## Annotating XML data

`annotate` can contain the following tokens:

- `error`, `warning`, `info`: XML elements are annotated with information associated with the given level or levels.
- `id`: XML elements are annotated with a unique id. [SINCE Orbeon Forms 4.7]

If the property is missing or empty, no annotation takes place. For example:

```xml
<property
  as="xs:string"
  name="oxf.fr.detail.send.success.annotate.my-app.my-form"
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

## Debugging the `send` action

The `send` action operates on the server, so you won't see the data submitted in your browser's dev tools, in particular.

There are a few ways to debug those requests:

1. Setup the service you are calling to log requests, if it can do that.
1. Use the built-in `echo` service, and modify your `send` process to look like this:
    ```xml
    <property as="xs:string" name="oxf.fr.detail.process.send.my-app.my-form">
        send(
            uri = "/fr/service/custom/orbeon/echo",
            method="POST",
            content="xml",
            replace="all"
        )
    </property>
    ```
    This will send the payload to your browser window.
1. Enable XForms logging and the [`submission-details`](/configuration/advanced/xforms-logging.md) token. The payloads will be logged to orbeon.log. This will log all Orbeon Forms submissions, however.
1. Enable logging of http wire in `log4j.xml` with:
    ```xml
    <category name="org.apache.http.wire">
        <priority value="debug"/>
    </category>
    ```
    The information will be logged to orbeon.log. This will log all HTTP requests, however, and can be very verbose.
1. Use an HTTP proxy like [Charles](https://www.charlesproxy.com/).

The easiest ways are probably options 1 and 2 above.

## See also

- [Form Runner actions](actions-form-runner.md)
- [Form Runner save action](actions-form-runner-save.md)
- [PDF templates](/form-runner/feature/pdf-templates.md)
