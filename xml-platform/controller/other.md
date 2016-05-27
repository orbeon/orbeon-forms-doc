# Other configuration elements

<!-- toc -->

## Overview

A page flow file is comprised of three sections:

* The `<files>` elements list files that must be served directly to the client, such as images or CSS files.
* The `<page>` elements declare pages and for each one specify identifier, path, model, view, and XML submission.
* The `<epilogue>``, <not-found-handler>`, `<unauthorized-handler>` and `<error-handler>` elements define additional behavior that apply to all the pages.

## The files element

Some files are not dynamically generated and are served to the client as-is. This is typically the case for images such as GIF, JPEG, CSS files, etc..

You tell the PFC what files to serve directly with one or more `<files>` elements in the page flow. The example below shows the configuration used by the Orbeon Forms examples:

```xml
<controller>
    <files path="*.gif"/>
    <files path="*.css"/>
    <files path="*.pdf"/>
    <files path="*.js"/>
    <files path="*.png"/>
    <files path="*.jpg"/>
    <files path="*.wsdl"/>
    <files path="*.html" mediatype="text/html"/>
    <files path="*.java" mediatype="text/plain"/>
    <files path="*.txt"  mediatype="text/plain"/>
    <files path="*.xq"   mediatype="text/plain"/>
    ...
</controller>
```

With `<files path="*.gif">`, if a request reaches the PFC with the path `images/logo.gif`, the file `oxf:/images/logo.gif` is sent in response to that request.

The `<files>` element supports the [`path` and `matcher`][11] attributes like the `<page>` element. It also supports a `mediatype` attribute telling the PFC what media type must be sent to the client with the files. The PFC uses defaults for well-known extension, as defined by the [Resource Server processor][22]. In doubt, you can specify the mediatype attribute.

## The epilogue element

![][23]

You often want a common look and feel across pages. Instead of duplicating the code implementing this look and feel in every page view, you can define it in a central location called the _page flow epilogue_. The `<epilogue>` element specifies the [XPL pipeline][9] which implements the page flow epilogue.

This is an example of `<epilogue>` element, pointing to the default epilogue XPL pipeline:

```xml
<epilogue url="oxf:/config/epilogue.xpl">  
```

The page flow epilogue is discussed in more details in the [Page Flow Epilogue][8] documentation.

## The not-found-handler element

The `<not-found-handler>` element is used to specify a page (the "not found" page) to call when no `<page>` element in the page flow is matched by the current request. There can be only one `<not-found-handler>` per page flow.

The "not found" page is also run by the PFC when a page throws an exception denoting that a resource has not been found:

* `HttpStatusCodeException` with code 404
* `ResourceNotFoundException`

The "not found" page does _not_ run for resources served through the `<files>` element. In that case, the PFC returns instead a "not found" code to the user agent (code 404 in the case of HTTP).

This is an example of `<not-found-handler>` element and the associated `<page>` element:

```xml
<page id="my-not-found" path="/not-found" view="/config/not-found.xhtml">  
...
<not-found-handler page="my-not-found">  
```

By default, `oxf:/config/not-found.xhtml` displays a simple XHTML page telling the user that the page requested cannot be found.

_NOTE: The "not found" page does not run for resources served through the `<files>` element. In that case, the PFC returns instead a "not found" code to the user agent (code 404 in the case of HTTP)._

## The unauthorized-handler element

The `<unauthorized-handler>` element is used to specify a page (the "unauthorized" page) to call when an unauthorized condition has taken place. Specifically, this happens when a page throws an exception:

* `HttpStatusCodeException` with code 401 or 403

Example:

This is an example of `<unauthorized-handler>` element and the associated `<page>` element:

```xml
<page id="my-unauthorized" path="/unauthorized" view="unauthorized.xhtml">  
...
<unauthorized-handler page="my-unauthorized">
```

## Error handling and the error-handler element

Several things can go wrong during the execution of a page flow by the PFC, in particular:

1. An action, page model, page view or epilogue may generate an error at runtime.
2. The page flow may be ill-formed.

Errors occurring while running a given page (#1 above) can be handled via the `<error-handler>` element. This works in a way very similar to the `<not-found-handler>` element:

```xml
<page id="error" path="/error" model="/config/error.xpl"/>
...
<error-handler page="error"/>
```

Other errors (#2 above) are not directly handled by the PFC. Instead, they are handled with the error [XPL pipeline][9] specified in the web application's `web.xml` file. By default, the error processor is the Pipeline processor, which runs the `oxf:/config/error.xpl` XPL pipeline. You can configure `error.xpl` for your own needs. By default, it formats and displays the Java exception which caused ther error.

See [Packaging and Deployment][3] for more information about configuring error processors.

[3]: http://www.orbeon.com/orbeon/doc/integration-packaging
[8]: http://www.orbeon.com/orbeon/doc/reference-epilogue
[9]: http://www.orbeon.com/orbeon/doc/reference-xpl-pipelines
[11]: #matchers
[22]: http://www.orbeon.com/orbeon/doc/processors-other#resource-server
[23]: ../../images/legacy/reference-controller-epilogue.png
