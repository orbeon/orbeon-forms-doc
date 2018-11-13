# XML submission



## Rationale

A page built out of a model and a view can retrieve information from a data source and format it. However, this is not enough to make a page which can use parameters submitted by a client to configure what data is being presented, or how it is presented.

The Orbeon Forms PFC uses the concept of _XML submission_ to provide page configurability. To the model and view of a given page, an XML submission is simply an XML document whose content is available as an [XPL pipeline][9] or an XSLT input called `instance`.

There are different ways to produce an XML submission:

* **Internal XForms submission.** The built-in Orbeon Forms XForms engine uses an HTTP `POST` XForms submission to submit an XForms instance.
* **External submission.** An external application or a client-side XForms engine uses HTTP `POST` to submit an XML document directly to a page.
* **PFC page navigation.** The PFC, based on a user configuration, produces an XML document to submit internally to a given page.
* **Default submission.** Each page can refer to a _default submission document_ containing an XML document automatically submitted to the page if no other submission is done.

## Internal XForms submission

The most common case of XML submission in Orbeon Forms is submission from the built-in XForms engine. Assume you have a page defined as follows:

```xml
<page
  path="/report/detail"
  model="report/detail/report-detail-model.xpl"
  view="report/detail/report-detail-view.xsl"/>
```

If you wish to submit an XForms instance to this page from within `report-detail-view.xsl`, create an XForms submission as follows:

```xml
<xf:submission id="main" method="post" action="/report/detail">  
```

This ensures that when this XForms submission is activated, an XML document containing the submitted XForms instance will be made available to the page model and view.

_NOTE: The `action` attribute on the `xf:submission` element should not be confused with the `<action>` element of the page flow. The former specifies a URL to which the XForms submission must be performed, as per the XForms 1.0 recommendation; the latter specifies a PFC [action][10] executed when a specified boolean XPath expression operating on an XML submission evaluates to true. The XForms submission's `action` attribute instead matches a PFC `<page>` element's `path` attribute._

You can also directly submit to another page by specifying a different action, for example:

```xml
<xf:submission id="main" method="post" action="/report/summary">  
```

In general it is recommended to leave the control of the flow between pages to PFC _actions_, as documented below.

## External XML submission

An external XML submission must refer to the URL of the page accepting the submission. It is up to the developer to provide this URL to the external application, for example `http://www.orbeon.org/myapp/xmlrpc` if you have a page declaring the path `/xmlrpc`:

```xml
<page path="/xmlrpc" model="xmlrpc.xpl">  
```

## External JSON submission

[SINCE Orbeon Forms 2016.2]

It is possible to submit JSON to a page or service with the `application/json` mediatype.

The JSON received is automatically converted to XML using the [XForms 2.0 conversion scheme](../../xforms/submission-json.md).

[SINCE Orbeon Forms 2017.1]

In addition to the `application/json` mediatype, mediatypes of the form `a/b+json` are recognized.

## Default submission

In case there is no external or internal XML submission, it is possible to specify a static default XML submission document. This is particularly useful to extract information from a page URL, as documented below. You specify a default submission with the `default-submission` attribute as follows:

```xml
<page
  path="/report/detail"
  default-submission="report-detail-default-submission.xml"/>
```

## Accessing XML submission data

The mechanisms described above explain how a page receives an XML submission, but not how to actually access the submitted XML document. You do this in one of the following ways:

* **XPL model.** The model accesses the XML submission document from its `instance` input.
* **XSLT model.** The model accesses the XML submission document using the `doc('input:instance')` function.
* **Static XML model.** The model cannot access the XML submission document.
* **XPL view.** The view accesses the XML submission document from its `instance` input.
* **XSLT view.** The view accesses the XML submission document using the `doc('input:instance')` function.
* **Static XML view.** The view cannot access the XML submission document.

If no submission has taken place, the XML submission document is an Orbeon Forms "null" document, as follows:

```xml
<null xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:nil="true">  
```

## Extracting data from the URL

XML submission using HTTP `POST` convenient in many cases, however there are other ways page developers would like to configure the way a page behaves:

* **Using URL parameters.** URL parameters are specified in a query string after a question mark in the URL, explained above.
* **Using URL path elements.** URL paths can be hierarchical, and the elements of the paths can have a user-defined meaning.

A PFC page can easily extract data from the URL using the `<setvalue>` element nested within the `<page>` element. To do so, an XML submission must take place on the page. This can be achieved by using the default submission if no other submission is taking place. The default submission document must contain placeholders for for the values to extract from the URL. Given an URL query string of `first=12&amp;count=10` with two parameters, `first` and `count`, a default sumission document can look as follows:

```xml
<submission>
    <first/>
    <count/>
</submission>
```

The following page extracts the two URL parameters:

```xml
<page path="/report/detail" default-submission="report-detail-default-submission.xml">
    <setvalue ref="/submission/first" parameter="first"/>
    <setvalue ref="/submission/count" parameter="count"/>
</page>
```

The `<setvalue>` element uses the `ref` attribute, which contains an XPath 2.0 expression identifying exactly one element or attribute in the XML document. The text value of the element or attribute is then set with the value of the URL parameter specified. If there is no parameter with a matching name, the element or attribute is not modified. This allows using default values, for example:

```xml
<submission>
    <first/>
    <count>5</count>
</submission>
```

In such a case, if no `count` parameter is specified on the URL, the default value will be available.

With a query string of `first=12&amp;count=10`, the resulting XML document will be:

```xml
<submission>
    <first>12</first>
    <count>10</count>
</submission>
```

With a query string of `first=12`, the resulting XML document will be:

```xml
<submission>
    <first>12</first>
    <count>5</count>
</submission>
```

_NOTE: The default submission document does not have to use element or attribute names identical to the URL parameter names. Doing so however may make the code clearer._

If there are multiple URL parameters with the same name, they will be stored in the element or attribute separated by spaces.

It is also possible to extract data from the URL path. To do so, use a `matcher` attribute on the page as [documented below][11]. You can then extract regular expression groups using the `<setvalue>` element with the `matcher-group` attribute:

```xml
<page
  path="/blog/([^/]+)/([^/]+)"
  matcher="regexp"
  default-submission="recent-posts/recent-posts-default-submission.xml"
  model="recent-posts/recent-posts-model.xpl"
  view="recent-posts/recent-posts-view.xpl">
    <setvalue ref="/form/username" matcher-group="1"/>
    <setvalue ref="/form/blog-id" matcher-group="2"/>
</page>
```

The `matcher-group` attribute contains a positive integer identifying the number of the regular expression group to extract. With a path of `/blog/jdoe/456`, the first group contains the value `jdoe`, and the second group the value `456`.

The `<setvalue>` element also supports a `value` attribute, which can be used to set a value using an XPath expression:

```xml
<setvalue ref="/form/date-time" value="current-dateTime()">  
```

Finally, `<setvalue>` supports setting a literal value:

```xml
<setvalue ref="/form/mode">print</setvalue>  
```

_NOTE: If a page actually uses an XML submission, which means either having `<action>` elements, or reading the instance in the page model or page view, it must not expect to be able to read the HTTP request body separately using the [Request generator][12]._

[9]: http://wiki.orbeon.com/forms/doc/developer-guide/xml-pipeline-language-xpl
[10]: #action-element
[11]: #matchers
[12]: ../processors/request-generator.md
