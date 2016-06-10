# Basics

<!-- toc -->

## Page flow configuration

A page flow is usually defined in a file called `page-flow.xml` stored at the root of your Orbeon Forms resources. This XML file has a root element called `<controller>`, which has to be within the `http://www.orbeon.com/oxf/controller` namespace. All the XML elements in a page flow have to be in that namespace unless stated otherwise. You start a page flow document as follows:

```xml
<controller xmlns="http://www.orbeon.com/oxf/controller">
    ...
</controller>
```

You can configure the location of the page flow configuration file in the web application's `web.xml` file. See [Packaging and Deployment][3] for more information. In most cases, it is not necessary to change the default name and location.

## Pages

Most web applications consist of a set of _pages_, identified to clients (web browsers) by a certain URL, for example:

```xml
http://www.orbeon.org/myapp/report/detail?first=12&amp;count=10#middle
```

In most cases the URL can be split as follows:

* `http://www.orbeon.org/` identifies the web or application server hosting the application.
* `/myapp` may optionally identify the particular web application running on that server. Whils this part of the URL path is not mandatory, its use is encouraged on Java application servers, where it is called the _context path_.
* `/report/detail` identifies the particular page within the web application. Such a path may be "flat", or hierarchical, separated with "/" (slashes).
* `?first=12&amp;count=10` specifies an optional _query string_ which identifies zero or more _parameters_ passed to that page. Each parameter has a value. This example has two parameters: the first one is called `first` with value `12`, and the second one is called `count` with value `10`.
* `#middle` is an optional fragment identifier identifying a section of the resulting page. Usually, this part of the URL is not handled by the web application, instead the web browser uses it to scroll to a section of the resulting page identified by this identifier (here `middle`).

For a particular web application, what usually matters in order to identify a particular page is the path within the URL under the context path, here `/report/detail`. Therefore, in an Orbeon Forms page flow, each page is identified with a unique path information. You declare a minimal page like this:

```xml
<page path="/report/detail">  
```

Other pages may be declared as follows:

```xml
<page path="/report/summary">  
```

```xml
<page path="/home">  
```

A `<page>` element can have an optional `id` attribute useful for [navigating between pages][4].

See [Authorization of pages and services][5] for information about configuring page authorization.

## Services

In addition to pages, web applications also often consist of _services_. A service is not unlike a page, but it is usually consumed by software rather than a human being.

The `<service>` element denotes a service:

* It clearly indicates that we are dealing with a service.
* It sets different defaults for authentication and authorization.

By default, pages can be accessed by the outside world. Of course, it is possible to protect them by an authentication and authorization mechanism. But, in general, the intent of a page is to be accessible from a web browser, at least for certain users.

On the other hand, by default, a service can only be accessed by an Orbeon Forms application and not from the outside world. This means that services are more secure. This makes sense because in many cases services exposed by Orbeon Forms are intended for consumption by Orbeon Forms applications themselves.

This behavior can be modified, see [Authorization of pages and services][5] for more information.

_NOTE: In previous versions of Orbeon Forms, services were implemented using the `<page>` element._

## Static pages and simple pages with XSLT

Creating a static page with Orbeon Forms is quite easy: just add a `view` attribute on a `<page>` element which points to an XHTML file:

```xml
<page
  path="/report/detail"
  view="oxf:/report/detail/report-detail-view.xhtml"/>
```

Here, using the `oxf:` protocol means that the file is searched through the Orbeon Forms [resource manager][6] sandbox. It is also possible to use relative paths, for example:

```xml
<page
  path="/report/detail"
  view="report/detail/report-detail-view.xhtml"/>
```

The path is relative to the location of the page flow configuration file where the `<page>` element is contained. Here is an example of the content of `report-detail-view.xhtml`:

```xml
<xh:html xmlns:xh="http://www.w3.org/1999/xhtml">
    <xh:head>
        <xh:title>Hello World Classic</xh:title>
    </xh:head>
    <xh:body>
        <xh:p>Hello World!</xh:p>
    </xh:body>
</xh:html>
```

It is recommended to to use XHTML and to put all the elements in the XHTML namespace, `http://www.w3.org/1999/xhtml`. This can be done by using default namespace declaration on the root element (`xmlns="http://www.w3.org/1999/xhtml`) or by mapping the namespace to a prefix such as `xhtml` and to use that prefix throughout the document, as shown above. The file must contain well-formed XML: just using a legacy HTML file won't work without some adjustments, usually minor.

Instead of using a static XHTML page, you can also use an XSLT template to generate a dynamic page. This allows using XSLT constructs mixed with XHTML constructs, for example:

```xml
<html
  xmlns="http://www.w3.org/1999/xhtml" 
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xsl:version="2.0">
    <head>
        <title>Current Time</title>
    </head>
    <body>
        <p>The time is now <xsl:value-of select="current-dateTime()"/>!</p>
    </body>
</html>
```

When XSLT templates are used, it is recommended to use the `.xsl` extension:

```xml
<page
  path="/report/detail"
  view="report/detail/report-detail-view.xsl"/>
```

## Page model and page view

![][7]

In the MVC architecture, the page logic is implemented by a _page model_, and the page layout by a _page view_. The MVC architecture promotes the separation of model, view and controller:

* **The page model** is responsible for calling or implementing the business logic. It is in charge of gathering the information to be displayed by the page view.
* **The page view** is in charge of presenting to the user the information gathered by the page model. The page view usually produces XHTML and XForms, but it can also produce other results such as XSL-FO, RSS, etc. Handling of the output of the view is done in the [page flow epilogue][8], which by default knows how to handle XHTML, XForms, XSL-FO, and custom XML document.
* **The controller** is responsible for dispatching a request from a client such as a web browser to the appropriate page model and view and connecting the model with the view. In Orbeon Forms, the controller is the PFC itself, which is configured with a page flow.

For instance, a news page can use a page model to retrieve the list of headlines and then pass this information as an XML document to a page view. The view produces an XHTML page by creating a table with the content of the headlines, adding a logo at the top of the page, a copyright notice at the bottom, etc.

Each PFC `<page>` element therefore supports attributes defining what page model and page view must be used:

* The `model` attribute is a URL refering to an [XPL pipeline][9] (optionally an XSLT stylesheet or a static XML file) implementing the model.
* The `view` attribute is a URL refering to an XSLT stylesheet (optionally an [XPL pipeline][9] or a static XML file) implementing the view.

The model passes data to the view as an XML document, as follows:

* **XPL model.** The model document must be generated by the [XPL pipeline][9] on an output named `data`.
* **XSLT model.** The model document is the default output of the XSLT transformation.
* **Static XML model.** The model document is the static XML document specified.
* **XPL view.** The model document is available on an input named `data`.
* **XSLT view.** The model document is available as the default input of the XSLT transformation.
* **Static XML view.** In this case, no model document is available to the view.

A model [XPL pipeline][9] and an XSLT view can be declared as follows for the `/report/detail` page:

```xml
<page
  path="/report/detail"
  model="report/detail/report-detail-model.xpl"
  view="report/detail/report-detail-view.xsl"/>
```

Here, the location of the model and view definitions mirrors the path of the page, and file names repeat the directory path, so that files can be searched easier. It is up to the developer to choose a naming convention, but it is recommended to follow a consistent naming structure. Other possibilities include:

```xml
<page
  path="/report/detail"
  model="report-detail-model.xpl"
  view="report-detail-view.xsl"/>
```

or:

```xml
<page
  path="/report/detail"
  model="models/report-detail-model.xpl"
  view="views/report-detail-view.xsl"/>
```

A typical XSLT view can extract model data passed to it automatically by the PFC on its default input, for example, if the model generates a document containing the following:

```xml
<employee>
    <name>John Smith</name>
</employee>
```

Then an XSLT view can display the content of the `<name>` element as follows:

```xml
<html
  xmlns="http://www.w3.org/1999/xhtml"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xsl:version="2.0">
    <head>
        <title>Hello World MVC</title>
    </head>
    <body>
        <p>Hello <xsl:value-of select="/employee/name"/>!</p>
    </body>
</html>
```

[3]: http://wiki.orbeon.com/forms/doc/developer-guide/integration-packaging
[4]: #page-navigation
[5]: authorization-of-pages-and-services.md
[6]: ../resources/resource-managers.md
[7]: ../../images/legacy/reference-controller-mvc.png
[8]: http://wiki.orbeon.com/forms/doc/developer-guide/reference-epilogue
[9]: http://wiki.orbeon.com/forms/doc/developer-guide/reference-xpl-pipelines
