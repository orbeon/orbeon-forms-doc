# Upload Control

<!-- toc -->

## Appearance

Example of the upload control before selection:

![](../images/xforms-upload-empty.png)

Example of the upload control sending a file in the background, with progress bar and "Cancel" button:

![](../images/xforms-upload-progress.png)

## Basic usage

XForms allows you to upload files with the XForms upload control:

```xml
<xf:upload ref="files/file[1]">
  <xf:filename ref="@filename"/>
  <xf:mediatype ref="@mediatype"/>
  <xxf:size ref="@size"/>
</xf:upload>
```

The related section of the XForms model can look like this:

```xml
<xf:instance id="my-instance">
  <files>
    <file filename="" mediatype="" size=""/>
  </files>
</xf:instance>
<xf:bind nodeset="file" type="xs:anyURI"/>
```

The `file` element is the element storing the result of the file upload. The result can be stored in two ways:

* __As a URL:__ By specifying the type `xs:anyURI`.
* __As Base64-encoded text:__ By specifying the type `xs:base64Binary`. Base64 is a mechanism to encode any binary data using a 65-character subset of US-ASCII. Using this mechanism allows embedding binary data into XML documents, at the typical cost of taking 50% more space than the original binary data. For more information, please refer to the [RFC][3].

The optional `xf:filename`, `xf:mediatype`, and `xxf:size` (the latter is an Orbeon Forms extension) allow storing metadata about an uploaded file:

* `xf:filename`: stores the file name sent by the user agent
* `xf:mediatype`: store the media type sent by the user agent
* `xxf:size`: stores the actual size in bytes of the uploaded data

_SECURITY NOTE: The file name and the media type are provided by the user agent (typically a web browser). Not only are they not guaranteed to be correct, but they must not be trusted._

## Upload as a URL

The result of a file upload when using `xs:anyURI` contains metadata parameters and typically looks like this:

```xml
file:/home/tomcat/apache-tomcat-6.0.29/temp/xforms_upload_2351863081926002422.tmp?
  filename=orbeon-logo-trimmed-42.png&
  mediatype=image%2Fpng&
  size=8437&
  signature=1726db1bea2b3be2f635f60e4f99dc72864548c5
```

_NOTE: The metadata includes a signature placed by the `<xf:upload>` control. This signature allows `<xf:output>` to verify that signature and disallow display or download of `file:` URLs not constructed by `<xf:upload>`. This enhances the security of uploads._

The URL stored as the value of the upload is temporary and only valid until either:

* the session expires,
* the Java VM quits,
* or a new file upload replaces the existing URI in the XForms instance.

The URL is only accessible from the server side, and will not be accessible from a client such as a web browser. It is not guaranteed to be a `file:` URL, only that it can be read with Orbeon Forms's [URL generator][4] or `<xf:output>`.

The contents of the file can be retrieved using the [URL Generator][4]. The result will be an XML document containing a single root element containing the uploaded file in Base64-encoded text.

_NOTE: Using the `xs:anyURI` type allows Orbeon Forms to make sure the uploaded file does not have to reside entirely in memory. This is the preferred method for uploading large files._

## Upload as inline Base64-encoded binary

The result of a file upload looks as follows when using `xs:base64Binary`:

```xml
<file filename="photo.jpg" mediatype="image/jpeg" size="2345">
/9j/4AAQSkZJRgABAQEASABIAAD/2wBDAAQDAwQDAwQEBAQFBQQFBwsHBwYGBw4KCggLEA4RERAO
  EA8SFBoWEhMYEw8QFh8XGBsbHR0dERYgIh8cIhocHRz/2wBDAQUFBQcGBw0HBw0cEhASHBwcHBwc
  ...
</file>
```

In this case, the uploaded file is encoded an directly embedded into the XML instance. This is a good method to handle small files only, because the entire file is converted and stored in memory.

## Validating a file upload

You can validate a file based on its name, mediatype, and size. Assume file metadata is stored into:

```xml
<file filename="" mediatype="" size="">
```

The following bind checks that if an uploaded file is present, the file size is no greater than 500,000 bytes and the mediatype is a JPEG image:

```xml
<xf:bind
    ref="file"
    constraint="
        . = '' or (
            @size le 500000 and
            @mediatype = ('image/jpeg', 'image/pjpeg')
        )"
/>
```

Note that Internet Explorer can send the `image/pjpeg` mediatype.

_SECURITY NOTE: The file name and the media type are provided by the user agent (typically a web browser). Not only are they not guaranteed to be correct, but they must not be trusted._

## The accept attributes

[SINCE Orbeon Forms 4.3]

The `accept` attribute is simply passed to the web browser. As of 2013, most web browsers do support filtering files based on that attribute,  as per the [HTML specification][5]. For example:

```xml
<xf:upload ref="file" accept="image/*">
```

The `accept` attribute is an XForms 2.0 feature. For backward compatibility, the `mediatype` attribute is also supported.

_SECURITY NOTE: This is not a guarantee that the file sent will have that mediatype, because some browsers do not support that feature, and even when it does, the browser must not be trusted._

## Controlling upload between the client and the server

### Rationale

With regular controls such as input, textarea, etc., upon a change of value by the user a small Ajax request is sent by the browser to the server to synchronize the data into the XForms data model.

With uploaded files, this is often not possible, because files can be very large and so take a significant amount of time to be sent to the server, up to minutes or even hours depending on file size and connection speed.

So there is a distinction to make between:

* _selection_: the user _selecting a file_ with the upload control's file selector
* _synchronization_: the file data being _fully sent to the server_ and available for processing by XForms

Orbeon Forms is able in most cases to synchronize files in the background: the user can keep working with the page while files are being uploaded.

### When synchronization takes place

Any files are selected but not synchronized upon a submission with `replace="all"`, those files are synchronized then.

In other cases, the process works as follows:

* as soon as the user selects a file with an upload control, the file is automatically scheduled for synchronization with the server
* background synchronization starts as soon as possible:
    * immediately if no other upload is pending
    * when other pending uploads have completed
* pending uploads can be canceled by the user with a Cancel button
* while background synchronization is taking place, the user can keep interacting and updating the page
* no boilerplate code or otherwise is needed to start synchronization!

By default, `<xf:submission>` checks for uploads and interrupts the submission if:

* the effective serialization is not `none`
* there is a pending upload
    * for an upload control
        * which is relevant
        * and bound to the instance being submitted

If a submission detects that there is a pending upload, the submission terminates with:

* dispatching the `xforms-submit-error` event
* with an `event('error-type')` set to `xxforms-pending-uploads`

You can this way detect pending uploads on submission with code like:

```xml
<xf:action
    ev:event="xforms-submit-error"
    if="event('error-type') = 'xxforms-pending-uploads'">
    ...
</xf:action>
```

This can be overridden with the `xxf:uploads` AVT attribute:

```xml
<xf:submission xxf:uploads="false" ...>
```

In addition, the `xxf:pending-uploads()` function returns the number of pending uploads. Example:

```xml
<xf:bind
    ref="save-button"
    readonly="xxf:pending-uploads() gt 0">
```

### Events

See [Upload control events](../events-extensions-events.md#upload-control-events).

_NOTE: `xforms-select` is no longer dispatched when a file is selected._

[3]: https://www.ietf.org/rfc/rfc2045.txt
[4]: http://wiki.orbeon.com/forms/doc/developer-guide/processors-url-generator
[5]: https://html.spec.whatwg.org/multipage/forms.html#file-upload-state-(type=file)
