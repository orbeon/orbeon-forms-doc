# Page Flow Controller

<!-- toc -->

## Introduction to the Page Flow Controller

![][1]

The Orbeon Forms Page Flow Controller (PFC) is the heart of your Orbeon Forms web application. It dispatches incoming user requests to individual pages built out of models and views, following the model / view / controller (MVC) architecture.

The PFC is configured with a file called a _page flow_. A page flow not only describes the pages offered by your application, it also declaratively describes an entire application's navigation logic, allowing the development of pages and XML services completely independently from each other.

The PFC encourages designing applications with a total separation between:

* **Site Logic** or **Page Flow**: when, and how to navigate from one page to another.
* **Page Logic** (the MVC page model): how data entered by the user is processed (for example validated, then fed to a backend), and how data is retrieved from a backend.
* **Page Layout** (the MVC page view): how information is displayed and presented to the user.
* **Site Presentation**: the layout and look and feel common to all pages in the web application or the web site, e.g.: site navigation menus, headers and footers, table backgrounds, or number formatting.

_NOTE: By default, the PFC is configured in `web.xml` as the main processor for the Orbeon Forms servlet and portlet. However, you are not required to use the PFC with Orbeon Forms: you can define your own main processor for servlets and portlets, as documented in [Packaging and Deployment][2]. For most web applications, the PFC should be used._

## Compatibility note

Starting Orbeon Forms 4.0, the following elements and attributes are introduced:

* `controller` element: replaces the `config` element`
* `path` attribute: replaces the `path-info` attribute
* `mediatype` attribute: replaces the `mime-type` attribute

If you are using builds prior to that date, including Orbeon Forms 3.9 and 3.9.1, you must use the old attributes. Starting Orbeon Forms 4.0, you should use the new attributes.

## Basics

### Page flow configuration

A page flow is usually defined in a file called `page-flow.xml` stored at the root of your Orbeon Forms resources. This XML file has a root element called `<controller>`, which has to be within the `http://www.orbeon.com/oxf/controller` namespace. All the XML elements in a page flow have to be in that namespace unless stated otherwise. You start a page flow document as follows:

```xml
<controller xmlns="http://www.orbeon.com/oxf/controller">
    ...
</controller>
```

You can configure the location of the page flow configuration file in the web application's `web.xml` file. See [Packaging and Deployment][3] for more information. In most cases, it is not necessary to change the default name and location.

### Pages

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

### Services

In addition to pages, web applications also often consist of _services_. A service is not unlike a page, but it is usually consumed by software rather than a human being.

The `<service>` element denotes a service:

* It clearly indicates that we are dealing with a service.
* It sets different defaults for authentication and authorization.

By default, pages can be accessed by the outside world. Of course, it is possible to protect them by an authentication and authorization mechanism. But, in general, the intent of a page is to be accessible from a web browser, at least for certain users.

On the other hand, by default, a service can only be accessed by an Orbeon Forms application and not from the outside world. This means that services are more secure. This makes sense because in many cases services exposed by Orbeon Forms are intended for consumption by Orbeon Forms applications themselves.

This behavior can be modified, see [Authorization of pages and services][5] for more information.

_NOTE: In previous versions of Orbeon Forms, services were implemented using the `<page>` element._

### Static pages and simple pages with XSLT

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

### Page model and page view

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

## XML submission

### Rationale

A page built out of a model and a view can retrieve information from a data source and format it. However, this is not enough to make a page which can use parameters submitted by a client to configure what data is being presented, or how it is presented.

The Orbeon Forms PFC uses the concept of _XML submission_ to provide page configurability. To the model and view of a given page, an XML submission is simply an XML document whose content is available as an [XPL pipeline][9] or an XSLT input called `instance`.

There are different ways to produce an XML submission:

* **Internal XForms submission.** The built-in Orbeon Forms XForms engine uses an HTTP POST XForms submission to submit an XForms instance.
* **External submission.** An external application or a client-side XForms engine uses HTTP POST to submit an XML document directly to a page.
* **PFC page navigation.** The PFC, based on a user configuration, produces an XML document to submit internally to a given page.
* **Default submission.** Each page can refer to a _default submission document_ containing an XML document automatically submitted to the page if no other submission is done.

### Internal XForms submission

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

### External XML submission

An external XML submission must refer to the URL of the page accepting the submission. It is up to the developer to provide this URL to the external application, for example `http://www.orbeon.org/myapp/xmlrpc` if you have a page declaring the path `/xmlrpc`:

```xml
<page path="/xmlrpc" model="xmlrpc.xpl">  
```

### Default submission

In case there is no external or internal XML submission, it is possible to specify a static default XML submission document. This is particularly useful to extract information from a page URL, as documented below. You specify a default submission with the `default-submission` attribute as follows:

```xml
<page
  path="/report/detail"
  default-submission="report-detail-default-submission.xml"/>
```

### Accessing XML submission data

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

### Extracting data from the URL

XML submission using HTTP POST convenient in many cases, however there are other ways page developers would like to configure the way a page behaves:

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

## Navigating between pages

### Page flow

![][13]

The site logic or page flow describes the conditions that trigger the navigation from one page to the other. It also describes how arguments are passed from one page to the other. In a simple web application simulating an ATM, as illustrated by the [ATM example][14] the navigation logic could look like the one described in the diagram on the right. In this diagram, the square boxes represent pages and diamond-shaped boxes represent actions performed by the end-user.

With the PFC, page flow is expressed declaratively and externally to the pages. Consequently, pages can be designed independently from each other. The benefits of a clear separation between site logic and page logic and layout include:

* **Simplicity:** the site logic is declared in one place and in a declarative way. You don't need to write custom logic to perform redirects between pages or pass arguments from page to page.
* **Maintainability:** having different developers implementing independent page is much easier. Since the relationship between pages is clearly stated in the page flow, it also becomes much easier to modify a page in an existing application without affecting other pages.

### Actions and results

#### An example

Consider a `view-account` page in the ATM web application. The page displays the current balance and lets the user enter an amount of money to withdraw from the account. The page looks like this:

![][15]

This page is composed of different parts illustrated in the figure below:

* **The page model** retrieves the current balance.
* **The page view** displays the balance, and presents a form for the user to enter the amount to withdraw.
* **An action** executed when the user enters an amount in the text field. This action checks if the amount entered is inferior or equal to the account balance. If it is, the balance is decreased by the amount entered and the transaction is considered valid. Otherwise, the transaction is considered illegal. Depending on the validity of the transaction, a different page is displayed. If the transaction is valid, the `anything-else` page is displayed; otherwise the `low-balance` page is displayed.
![][16]

This behavior is described in the Page Flow with:

```xml
<page
  id="view-account"
  path="/view-account"
  model="view-account-get-balance-model.xpl"
  view="view-account-get-balance-view.xsl">
    <action when="/amount != ''" action="view-account-action.xpl">
        <result id="success" when="/success = 'true'" page="anything-else"/>
        <result id="failure" when="/success = 'false'" page="low-balance"/>
    </action>
</page>
```

#### The `<page>` element

On the `<page>` element, as documented above:

* The `path` attribute tells the PFC what relative URL corresponds to this page. The URL is relative to the application context path.
* The `model` attribute points to the page model [XPL pipeline][9].
* The `view` attribute points to the page view XSLT template.

#### The `<action>` element

The `<page>` element contains an `<action>` element. It is named _action_ because it is typically executed as a consequence of an action performed by the end-user, for example by clicking on a button or a link which causes a form to be submitted. There may be more than one `<action>` element within a `<page>` element element. On an `<action>` element:

* The `when` attribute contains an XPath 2.0 expression executed against the XML submission. The first `<action>` element with a `when` attribute evaluating to `true()` is executed. The `when` attribute is optional: a missing `when` attribute is equivalent to `when="true()"`. Only the last `<action>` element is allowed to have a missing `when` attribute. This allows for defining a default action which executes if no other action can execute.
* When the action is executed, if the optional `action` attribute is present, the [XPL pipeline][9] it points to is executed.

#### The `<result>` element

The `<action>` element can contain zero or more `<result>` elements.

* If an `action` attribute is specified on the `<action>` element, the `<result>` element can have a `when` attribute. The `when` attribute contains an XPath 2.0 expression executed against the `data` output of the action [XPL pipeline][9]. The first `<result>` evaluating to `true()` is executed. The `when` attribute is optional: a missing `when` attribute is equivalent to `when="true()"`. Only the last `<result>` element is allowed to have a missing when attribute. This allows for defining a default action result which executes if no other action result can execute.
* A `<result>` element optionally has a `page` attribute. The `page` attribute contains a page id pointing to a page declared in the same page flow. When the result is executed and the `page` attribute is present, the destination page is executed, and the user is forwarded to the corresponding page.

    NOTE: In this case, a page model or page view specified on the enclosing `<page>` element does not execute! Instead, control is transferred to the page with the identifier specified. This also means that the [page flow epilogue][17] does not execute.

* A `<result>` element can optionally contain an XML submission. The submission can be created using XSLT, XQuery, or the deprecated XUpdate. You specify which language to use with the `transform` attribute on the `<result>` element. The inline content of the `<result>` contains then a transformation in the language specified. Using XSLT is recommended.

    The transformation has automatically access to:

    * An `instance` input, containing the current XML submission. From XSLT, XQuery and XUpdate, this input is available with the `doc('input:instance')` function. If there is no current XML submission, a "null" document is available instead:
    * An `action` input, containing the result of the action [XPL pipeline][9] if present. From XSLT, XQuery and XUpdate, this input is available with the `doc('input:action')` function. If there is no action result, a "null" document is available instead:
    * The default input contains the current current XML submission as available from the `instance` input.

    The result of the transformation is automatically submitted to the destination page. If there is no destination page, it replaces the current XML submission document made availabe to the page model and page view.

An action [XPL pipeline][9] supports an optional `instance` input containing the current XML submission, and produces an optional `data` output with an action result document which may be used by a `<result>` element's `when` attribute, as well as by an XML submission-producing transformation. This is an example of action XPL pipeline:

```xml
<p:config
  xmlns:p="http://www.orbeon.com/oxf/pipeline"
  xmlns:oxf="http://www.orbeon.com/oxf/processors">

    <p:param name="instance" type="input"/>
    <p:param name="data" type="output"/>

    <!-- Call the data access layer -->
    <p:processor name="oxf:pipeline">
        <p:input name="config" href="../data-access/delegate/read-document.xpl"/>
        <p:input name="document-id" href="#instance#xpointer(/*/document-id)"/>
        <p:output name="document-info" ref="data"/>
    </p:processor>

</p:config>
```

Notice the `instance` input, the `data` output, as well as a call to a data access layer which uses information from the XML submission and directly returns an action result document.

The following is an example of using XSLT within `<result>` element in order to produce an XML submission passed to a destination page:

```xml
<action
  when="/form/action = 'show-detail'"
  action="../bizdoc/summary/find-document-action.xpl">
    <result page="detail" transform="oxf:xslt">
        <form xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xsl:version="2.0">
            <document-id><xsl:value-of select="doc('input:action')/document-info/document-id"/></document-id>
            <document><xsl:copy-of select="doc('input:action')/document-info/document/*"/></document>
        </form>
    </result>
</action>
```

Notice the `transform` attribute set to `oxf:xslt`, and the use of the `doc('input:action')` to refer to the output of the action XPL pipeline specified by the `action` attribute on the `<action>` element. The current XML submission can also be accessed with`doc('input:instance')`.

Also see the [Orbeon Forms Tutorial][18].

### Controlling internal XML submissions

You can control what method is used to perform an internal XML submission described within a `<result>` element. Consider this page flow:

```xml
<page id="a" path="/a" model="..." view="...">
    <action when="...">
        <result page="b"/>
    </action>
</page>
```

```xml
<page id="b" path="/b" model="..." view="...">  
```

Going from page "a" to page "b" can be done with either a "forward" or a "redirect" method:

|   |   |
|---|---|
| Redirect |  ![][19] |
| Forward  |  ![][20] |

The benefit of the "redirect" method is that after being redirected to page _b_, the end-user will see a URL starting with `/b` in the browser's address bar. He will also be able to bookmark that page and to come back to it later. However, a drawback is that the request for page _b_ is sent by the browser with a `GET` method. Since browsers impose limits on the maximum amount of information that can be sent in a `GET` (URL length), this method might not work if the amount of information that needs to be passed to page _b_ from page _a_ is too large. This typically happens when working with fairly large XML submissions. In those cases, you must use the "forward" method, which does not limit the amount of information passed from page to page. The "forward" method also reduces the number of roundtrips with the server.

_NOTE: A third instance passing option, `redirect-exit-portal`, behaves like the `redirect` method but sends a redirection which exits the portal, if any. This is mainly useful for the Orbeon Forms examples portal._

You can configure the method:

1. At the application level, in `properties.xml` with:
    
    ```xml
    <property as="xs:string" processor-name="oxf:page-flow" name="instance-passing" value="forward|redirect"/>
    ```

2. At the page flow level with the `instance-passing` attribute on the page flow root element:

    ```xml
    <controller instance-passing="forward|redirect">...</controller>  
    ```

3. In the page flow at the "result" level, with the `instance-passing` attribute on the `<result>` element:

    ```xml
    <page id="a" path="/a" model="..." view="...">
        <action when="...">
            <result page="b" instance-passing="forward|redirect"/>
        </action>
    </page>
    ```

A configuration at the application level (`properties.xml`) can be overridden by a configuration at the page flow level (`instance-passing` on the root element), which can in its turn be overridden by a configuration at the result level (`instance-passing` on the `<result>` element).

## Paths and matchers

### Patterns

The value of the `path` attribute can be either a glob pattern or a full regular expression (Java regular expressions, which are very similar to Perl 5 regular expressions).

| Value |  Description |
|---|---|
| Glob |  This is the default but can also be set explicitly with the  `matcher` attribute set to glob (or oxf:glob-matcher for backward compatibility) on the `<page>` element. This supports the wildcards "`*`" and "?". as well as character classes, as [documented here][21].|
|Regular expression|This is enabled with the  `matcher` attribute set to regexp (or oxf:perl5-matcher for backward compatibility) on the `<page>` element. This enables full Java/Perl 5 regular expressions.|

Simple examples of glob:

* `/about/company.html `matches exactly this URL
* `about/*` matches any URL that starts with `about/`
* `*.gif` matches any URL that ends with `.gif`
* `a?c` matches `aac`, `abc`, `etc`.
  
A default value for the `matcher` attribute can also be placed on the element:

```xml
<controller xmlns="http://www.orbeon.com/oxf/controller" matcher="regexp">
    ...
</controller>
```

_COMPATIBILITY NOTE: Prior to Orbeon Forms 4.0, expression matchers were fully configurable via XPL processors. Starting Orbeon Forms 4.0, only Java/Perl 5 regular expressions and glob expressions are supported._

### Matching files and pages with regular expressions

Groups of files can be matched using a single `<files>` element with the regexp matcher:

```xml
<files
  path="/doc/[^.]*\.html"
  matcher="regexp"/>
```

A matcher can also be specified on a `<page>` element:

```xml
<page
  path="/forms/([_A-Za-z\-0-9]+)/page/([0-9]{1,3})"
  matcher="regexp"/>
```

When using a matcher that allows for groups, the part of the path matched by those groups can be extracted as documented above with the `<setvalue>` element. This is only supported with the regexp matcher.

### Parametrizing the `model` and `view` attributes

The result of matches can be referred to directly in the `model` and `view` attributes using the notation `${_group-number_}`:

```xml
<page
  path="/forms/([_A-Za-z\-0-9]+)/page/([0-9]{1,3})"
  matcher="regexp"
  model="oxf:/forms/${1}/model.xpl"
  view="oxf:/forms/${1}/view-${2}.xhtml"/>
```

In this case, if the path contains: `/forms/my-form/page/12`:

* The model file read will be `oxf:/forms/my-form/model.xpl`
* The view file `oxf:/forms/my-form/view-12.xhtml`

Parametrizing `model` and `view` attributes this way often allows greatly reducing the size of page flows that contain many similar pages.

### Navigating to pages that use matchers

When a `result` element directs flow to a page that uses matchers and `<setvalue>` elements, the PFC attemps to rebuild the destination path accordingly. Consider the following example:

```xml
<page id="source" path="/">
    <action>
        <result page="destination" transform="oxf:xslt">
            <form xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xsl:version="2.0">
                <username>orbeon</username>
                <blog-id>12345</blog-id>
            </form>
        </result>
    </action>
</page>
```

```xml
<page
  id="destination"
  path="/user/([^/]+)/blog/([^/]+)"
  matcher="regexp"
  view="blogs/${1}/blog-${2}.xhtml">
    <setvalue ref="/form/username" matcher-group="1"/>
    <setvalue ref="/form/blog-id" matcher-group="2"/>
</page>
```

In this example, accessing the `source` page directly causes navigation to the `destination` page. Using the following information:

* The internal XML submission
* The destination page's matcher groups
* The `<setvalue>` elements

The PFC reconstructs the path to `/user/orbeon/blog/12345`. This path is used to request the `destination` page. In this case, the `view` attribute evaluates to `blogs/orbeon/blog-12345.xhtml`.

_NOTE: Navigation to pages that use matchers but that do not provide an internal XML submission or `<setvalue>` elements will cause the requested path to have its literal value, in the example above `/user/([^/]+)/blog/([^/]+)`. It is not advised to perform navigation this way._

## Other configuration elements

### Overview

A page flow file is comprised of three sections:

* The `<files>` elements list files that must be served directly to the client, such as images or CSS files.
* The `<page>` elements declare pages and for each one specify identifier, path, model, view, and XML submission.
* The `<epilogue>``, <not-found-handler>`, `<unauthorized-handler>` and `<error-handler>` elements define additional behavior that apply to all the pages.

### The files element

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

### The epilogue element

![][23]

You often want a common look and feel across pages. Instead of duplicating the code implementing this look and feel in every page view, you can define it in a central location called the _page flow epilogue_. The `<epilogue>` element specifies the [XPL pipeline][9] which implements the page flow epilogue.

This is an example of `<epilogue>` element, pointing to the default epilogue XPL pipeline:

```xml
<epilogue url="oxf:/config/epilogue.xpl">  
```

The page flow epilogue is discussed in more details in the [Page Flow Epilogue][8] documentation.

### The not-found-handler element

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

### The unauthorized-handler element

The `<unauthorized-handler>` element is used to specify a page (the "unauthorized" page) to call when an unauthorized condition has taken place. Specifically, this happens when a page throws an exception:

* `HttpStatusCodeException` with code 401 or 403

Example:

This is an example of `<unauthorized-handler>` element and the associated `<page>` element:

```xml
<page id="my-unauthorized" path="/unauthorized" view="unauthorized.xhtml">  
...
<unauthorized-handler page="my-unauthorized">
```

### Error handling and the error-handler element

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

## Typical combinations of page model and page view

The sections below show how page model and page view are often combined.

### View only

Simple pages with no back-end code can be implemented with a single [XPL pipeline][9], XSLT template or static page. A view XPL pipeline must have a `data` output. The XML generated by the view then goes to the epilogue. ![][24]

### Model only

If a page is not sent back to the user agent, there is no need for a view. This is typically the case when a redirect needs to be issued, a binary file is produced, or when a page simply implements an XML service. ![][25]

### View only with xml submission

This is a variant of the _view only_ scenario, where an XML submission is used. In this case, the view receives the XML submission as the `instance` input. ![][26]

### Model only with xml submission

This is a variant of the _model only_ scenario, where an XML submission is used. ![][27]

### View and model

This is the classic case. An XPL pipeline implements the page model and an XSLT template implements the page view where data produced by the model is consumed by the view. ![][28]

### View and model with xml submission, case 1

This is the equivalent of the previous model where an XML submission is used. In this case an `instance` input is made available to the model and the view. ![][29]

### View and model with xml submission, case 2

This is a variant of the previous case where the model declares an `instance` output. This allows the model to modify the submitted XML instance. This is typically useful when the view displays some values from the XML submission document but these values are not exactly the same as those entered by the user. For example, a page with a text field where the user types an airport code. If the user enters a known city such as San Francisco, the application may automatically add the corresponding airport code (SFO in this case). ![][30]

## Examples

### Redirection with the PFC

The following example illustrates how to perform a simple redirection with the PFC. Assume you want some path, `/a`, to be redirect to another path, `/b`. You can do this as follows:

```xml
<page path="/a">
    <action>
        <result page="page-b" instance-passing="redirect"/>
    </action>
</page
```

```xml
<page id="page-b" path="/b">
    ...
</page>
```

Note that you do not have to use `redirect`, but that doing so will cause the user agent to display the path to page `/b` in its URL bar. The `instance-passing` attribute is also unnecessary if `redirect` is already the default instance passing mode.

### Implementing XML services with the PFC

The PFC allows you to very easily receive an XML document submitted, for example with an HTTP POST, and to generate an XML response. This can be useful to implement XML services such as XML-RPC, SOAP, or any XML-over-HTTP service. The following PFC configuration defines a simple XML service:

```xml
<page path="/xmlrpc" model="xml-rpc.xpl">  
```

Notice that there is no `view` attribute: all the processing for this page is done in the page model.

The following content for `xml-rpc.xpl` implements an XML service returning as a response the POST-ed XML document:

```xml
<p:config 
  xmlns:p="http://www.orbeon.com/oxf/pipeline"
  xmlns:oxf="http://www.orbeon.com/oxf/processors">

    <!-- The XML submission is available on the "instance" input -->
    <p:param name="instance" type="input" schema-href="request.rng"/>

    <!-- Processing of the XML submission (here we just return the request) -->
    <p:processor name="oxf:identity">
        <p:input name="data" href="#instance"/>
        <p:output name="data" id="response"/>
    </p:processor>

    <!--  TODO: update this, it's obsolete, must use xml-converter -->
    <!-- Generate a response -->
    <p:processor name="oxf:xml-serializer">
        <p:input name="data" href="#response" schema-href="response.rng"/>
        <p:input name="config">
            <config/>
        </p:input>
    </p:processor>
</p:config>
```

Notice the optional `schema-href` attributes which allow validating the request and the response against schemas.

[1]: ../images/legacy/reference-controller-oxf-app.png
[2]: http://www.orbeon.com/orbeon/doc/integration-packaging#main-processor
[3]: http://www.orbeon.com/orbeon/doc/integration-packaging
[4]: #page-navigation
[5]: authorization-of-pages-and-services.md
[6]: resources/resource-managers
[7]: ../images/legacy/reference-controller-mvc.png
[8]: http://www.orbeon.com/orbeon/doc/reference-epilogue
[9]: http://www.orbeon.com/orbeon/doc/reference-xpl-pipelines
[10]: #action-element
[11]: #matchers
[12]: http://www.orbeon.com/orbeon/doc/processors-generators-request
[13]: ../images/legacy/reference-controller-navigation.png
[14]: http://www.orbeon.com/orbeon/atm/
[15]: ../images/legacy/reference-controller-atm-screen.png
[16]: ../images/legacy/reference-controller-atm-logic.png
[17]: #epilogue-element
[18]: ../xforms/tutorial/README.md
[19]: ../images/legacy/home-changes-forward.png
[20]: ../images/legacy/home-changes-redirect.png
[21]: http://jakarta.apache.org/oro/api/org/apache/oro/text/GlobCompiler.html
[22]: http://www.orbeon.com/orbeon/doc/processors-other#resource-server
[23]: ../images/legacy/reference-controller-epilogue.png
[24]: ../images/legacy/reference-controller-view.png
[25]: ../images/legacy/reference-controller-model.png
[26]: ../images/legacy/reference-controller-view-xforms.png
[27]: ../images/legacy/reference-controller-model-xforms.png
[28]: ../images/legacy/reference-controller-view-model.png
[29]: ../images/legacy/reference-controller-view-model-xforms-1.png
[30]: ../images/legacy/reference-controller-view-model-xforms-2.png
 