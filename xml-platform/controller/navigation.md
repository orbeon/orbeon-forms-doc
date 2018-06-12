# Navigating between pages



## Page flow

![][13]

The site logic or page flow describes the conditions that trigger the navigation from one page to the other. It also describes how arguments are passed from one page to the other. In a simple web application simulating an ATM, the navigation logic could look like the one described in the diagram on the right. In this diagram, the square boxes represent pages and diamond-shaped boxes represent actions performed by the end-user.

With the PFC, page flow is expressed declaratively and externally to the pages. Consequently, pages can be designed independently from each other. The benefits of a clear separation between site logic and page logic and layout include:

* **Simplicity:** the site logic is declared in one place and in a declarative way. You don't need to write custom logic to perform redirects between pages or pass arguments from page to page.
* **Maintainability:** having different developers implementing independent page is much easier. Since the relationship between pages is clearly stated in the page flow, it also becomes much easier to modify a page in an existing application without affecting other pages.

## Actions and results

### An example

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

### The `<page>` element

On the `<page>` element, as documented above:

* The `path` attribute tells the PFC what relative URL corresponds to this page. The URL is relative to the application context path.
* The `model` attribute points to the page model [XPL pipeline][9].
* The `view` attribute points to the page view XSLT template.

### The `<action>` element

The `<page>` element contains an `<action>` element. It is named _action_ because it is typically executed as a consequence of an action performed by the end-user, for example by clicking on a button or a link which causes a form to be submitted. There may be more than one `<action>` element within a `<page>` element element. On an `<action>` element:

* The `when` attribute contains an XPath 2.0 expression executed against the XML submission. The first `<action>` element with a `when` attribute evaluating to `true()` is executed. The `when` attribute is optional: a missing `when` attribute is equivalent to `when="true()"`. Only the last `<action>` element is allowed to have a missing `when` attribute. This allows for defining a default action which executes if no other action can execute.
* When the action is executed, if the optional `action` attribute is present, the [XPL pipeline][9] it points to is executed.

### The `<result>` element

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

## Controlling internal XML submissions

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

[9]: http://wiki.orbeon.com/forms/doc/developer-guide/xml-pipeline-language-xpl
[13]: ../../images/legacy/reference-controller-navigation.png
[15]: ../../images/legacy/reference-controller-atm-screen.png
[16]: ../../images/legacy/reference-controller-atm-logic.png
[17]: #epilogue-element
[18]: ../../xforms/tutorial/README.md
[19]: ../../images/legacy/home-changes-forward.png
[20]: ../../images/legacy/home-changes-redirect.png
