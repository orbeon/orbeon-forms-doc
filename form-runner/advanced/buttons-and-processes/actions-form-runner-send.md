# Form Runner send action



##  Introduction

This action sends data to an HTTP URL.

## Configuration

### Using parameters

[SINCE Orbeon Forms 4.4 except `property`]

The following example uses three parameters in the `send` action for the form `my_app/my_form`:

```xml
<property as="xs:string" name="oxf.fr.detail.process.send.my_app.my_form" >
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
    - `true` to remove all occurrences of `fr:`-prefixed elements and attributes
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

### Using properties

The following example refers in the `send` action to the properties with the common
prefix `oxf.fr.detail.process.send.my_app.my_form`. It configures the URL, the method,
and the type of content using three additional sub-properties.

```xml
<property as="xs:string" name="oxf.fr.detail.process.send.my_app.my_form" >
    send("oxf.fr.detail.process.send.my_app.my_form")
</property>
<property
    as="xs:string"
    name="oxf.fr.detail.process.send.my_app.my_form.uri"
    value="http://example.org/accept-form"
    />
<property
    as="xs:string"
    name="oxf.fr.detail.process.send.my_app.my_form.method"
    value="PUT"
    />
<property
    as="xs:string"
    name="oxf.fr.detail.process.send.my_app.my_form.content"
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

#### Properties and XPath Value Templates

[SINCE Orbeon Forms 4.4]

The following properties are XPath Value Templates evaluating in the context of the root element of the form data instance:

- `uri`
- `method`
- `prune`
- `annotate`
- `content`
- `parameters`
- `replace` [SINCE Orbeon Forms 4.7]

**Example**

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

Example:

    http://example.org/service?
        document=7520171020e65a1585e72574ae1fbe138c415bee&
        process=139ceb515f918d6d17030b81255d8a3dfa0501cc&
        valid=true&
        app=acme&
        form=invoice&
        form-version=1&
        language=en

## Sending PDF and TIFF content 

### Controlling the format

[SINCE Orbeon Forms 2018.1]

When using PDF templates and `content = "pdf"` or `content = "tiff"`, you can control the PDF processing with the
following:

- `use-pdf-template`
    - default: `true`
    - If at least one PDF template is available, the default is to use one of the PDF templates. But if
      `use-pdf-template = "false"`, then use of any PDF template is disabled and the automatic PDF is produced.  
- `pdf-template-name`
    - If `pdf-template-name` specifies a name, such as with `pdf-template-name = "archive"`, the list of available PDF
      templates is reduced to those having an exactly matching name. If no matching name is found, an error is raised. 
- `pdf-template-lang` 
    - If `pdf-template-lang` specifies a language, such as with `pdf-template-lang = "fr"`, the list of available
      PDF templates as reduced by `pdf-template-name` is used to find a PDF template with a matching language.
      If no matching language is found, an error is raised.
    - If `pdf-template-lang` is empty or missing:
        - The PDF template with the current form language is used, if there is a match.
        - If there is no match, the first available PDF template is used.
        
See also [PDF templates](../../../form-builder/pdf-templates.md)

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

*NOTE: When the PDF must be sent to a remote service, it is better to send the PDF binary directly using `content = "pdf"` .*

### Sending a TIFF URL

[SINCE Orbeon Forms 2016.1]

When `content =  "tiff-url"` is specified, the XML document sent has the following format:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<url>/xforms-server/dynamic/567f14ee46c6b21640c1a5a7374d5ad8</url>
```

The TIFF can be retrieved by accessing that path with the proper session cookie.

A use case for this is to submit the URL to a local confirmation page. The page can then link to the URL provided, and the user can download the TIFF file.

*NOTE: When the TIFF must be sent to a remote service, it is better to send the TIFF binary directly using `tiff`.*

## Sending form metadata

[SINCE Orbeon Forms 4.7]

When `content = "metadata"` is specified, the XML document sent contains metadata per control. [This page](https://gist.github.com/orbeon/3684806b0a30a9a5ace9) shows examples based on the Orbeon Forms sample forms.

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

## See also

- [PDF templates](../../../form-builder/pdf-templates.md)
