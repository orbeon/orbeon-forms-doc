# Examples

<!-- toc -->

## Redirection with the PFC

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

## Implementing XML services with the PFC

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
