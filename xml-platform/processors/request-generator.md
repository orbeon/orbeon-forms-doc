# Request Generator

<!-- toc -->

## Introduction

Generators are a special category of processors that have no XML data inputs, only outputs. They are generally used at the top of an XML pipeline to generate XML data from a Java object or other non-XML source.

The Request generator streams XML from the current HTTP request. It can serialize request information including headers, parameters, query strings, user and server information.

_NOTE: The Request generator can be used as the first component in a web application pipeline, but it is recommended to use the Page Flow Controller and XForms whenever possible. There are cases where additional data from the request may be required, however, and where the Request generator must be used._

## Configuration

The Request generator takes a mandatory configuration to select which request information to return. This configuration consists of a series of `include` and `exclude` elements containing XPath expressions selecting a number of element from the request tree. Those expressions can be as complex as any regular XPath 1.0 expression that returns a single node or a node-set. However, it is recommended to keep those expressions as simple as possible. One known limitation is that it is not possible to test on the `value` element of uploaded files, as well as the content of the request body.

Sample Configuration:

```xml
<config>
  <include>/request/path-info</include>
  <include>/request/headers</include>
  <include>/request/parameters/parameter[starts-with(name, 'document-id')]</include>
  <exclude>/request/parameters/parameter[name = 'document-id-dummy']</exclude>
</config>
```

The full tree is:

```xml
<request>
  <container-type>servlet</container-type>
  <container-namespace/>
  <content-length>-1</content-length>
  <content-type/>
  <parameters>
    <parameter>
      <name>id</name>
      <value>12</value>
    </parameter>
    <parameter>       <name>print</name>
      <value>false</value>
    </parameter>
  </parameters>
  <body/>
  <protocol>HTTP/1.1</protocol>
  <remote-addr>127.0.0.1</remote-addr>
  <remote-host>localhost</remote-host>
  <scheme>http</scheme>
  <server-name>localhost</server-name>
  <server-port>8080</server-port>
  <is-secure>false</is-secure>
  <auth-type>BASIC</auth-type>
  <remote-user>jdoe</remote-user>
  <context-path>/ops</context-path>
  <headers>
    <header>
      <name>host</name>
      <value>localhost:8080</value>
    </header>
    <header>
      <name>user-agent</name>
      <value>Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.1) Gecko/20020826</value>
    </header>
    <header>
      <name>accept-language</name>
       <value>en-us, en;q=0.50</value>
    </header>
    <header>
      <name>accept-encoding</name>
      <value>gzip, deflate, compress;q=0.9</value>
    </header>
    <header>
      <name>accept-charset</name>
      <value>ISO-8859-1, utf-8;q=0.66, *;q=0.66</value>
    </header>
    <header>
      <name>keep-alive</name>
      <value>300</value>
    </header>
    <header>
      <name>connection</name>
      <value>keep-alive</value>
    </header>
    <header>
      <name>referer</name>
      <value>http://localhost:8080/ops/</value>
    </header>
    <header>
      <name>cookie</name>
      <value>JSESSIONID=DA6E64FC1E6DFF0499B5D6F46A32186A</value>
    </header>
  </headers>
  <attributes>
    <attribute>
      <name>oxf.xforms.renderer.deployment</name>
      <value>separate</value>
    </attribute>
    <attribute>
      <name>javax.servlet.forward.context_path</name>
      <value>/myapp</value>
    </attribute>
  </attributes>
  <method>GET</method>
  <path-info>/doc/home-welcome</path-info>
  <request-path>/doc/home-welcome</request-path>
  <path-translated>C:\orbeon\projects\OPS\build\ops-war\doc\home-welcome</path-translated>
  <query-string>id=12&print=false</query-string>
  <requested-session-id>DA6E64FC1E6DFF0499B5D6F46A32186A</requested-session-id>
  <request-uri>/ops/doc/home-welcome</request-uri>
  <request-url>http://localhost:8888/ops/doc/home-welcome</request-url>
  <servlet-path/>
</request>
```

_NOTE: Orbeon Forms adds a computed request information item: the `request-path`. This is defined as a concatenation of the `servlet-path` and the `path-info`. This is useful because both these are frequently mixed up and often change depending on the application server or its configuration._

_WARNING: This generator excludes all information items by default. To obtain the whole tree (as shown in the example above), you must explicitly include `/request`:_

```xml
<p:processor name="oxf:request">
  <p:input name="config">
    <config>
      <include>/request</include>
    </config>
  </p:input>
  <p:output name="data" id="request"/>
</p:processor>
```

## Request attributes

When the request includes `/request/attributes`, the Request generator attempts to retrieve request attributes. Since those can be any Java objects, the generator only includes string values.

## Request body

When the request includes `/request/body`, the Request generator retrieves the body of the request sent to the application server. The content of the body is made available as the following data types:

* If the attribute `stream-type` on the `config` element is set to `xs:anyURI`, an URI is returned as the value of the `/request/body` element.
* If the attribute `stream-type` on the `config` element is set to `xs:base64Binary`, the content of the request encoded as Base64 is returned as the value of the `/request/body` element.
* Otherwise, the content of the `/request/body` is set as either `xs:anyURI` if the request body is large (as set by the `max-upload-memory-size` property, by default larger than 10 KB), or `xs:base64Binary` if the request body is small.
* The URL stored as the value of the request body is only valid for the duration of the current request, unless the `stream-scope` attribute is set to `session`, in which case it is valid for the duration of the session. [SINCE: 2010-12-09]

Examples of configuration:

```xml
<config stream-type="xs:anyURI">
  <include>/request/body</include>
</config>
```

```xml
<config stream-type="xs:base64Binary">
  <include>/request/body</include>
</config>
```

```xml
<config stream-type="xs:anyURI" stream-scope="session">
  <include>/request/body</include>
</config>
```

The resulting data type is always set on the body element, for example:

```xml
<request>
  <body xsi:type="xs:anyURI">file:/C:/Tomcat/temp/upload_00000005.tmp</body>
</request>
```

_WARNING: Reading the request body is incompatible with reading HTML forms posted with the `multipart/form-data` encoding, typically used when uploading files. In such a case, you should read either only the request body, or only the request parameters._

## Request headers

Request header names are normalized to lowercase. According to the HTTP specification headers are case-insensitive. Normalization to lowercase makes it easier to compare header names without constantly calling the XPath `lower-case()` or similar.

## Uploaded files

Uploaded files are stored into `parameter` elements, like any other form parameter. The rules for the data type used are the same as for the request body (see above), the data type depending on the `stream-type` attribute and the size of the uploaded files:

```xml
<config stream-type="xs:anyURI">
  <include>/request/parameters</include>
</config>
```

The URL stored as the value an upload body is only valid for the duration of the current request, unless the `stream-scope` attribute is set to `session`, in which case it is valid for the duration of the session.

The `parameter` element for an uploaded file contains the following elements in addition to the `name` and `value` elements use for other parameters:

* `filename`: stores the file name sent by the user agent
* `content-type`: store the media type sent by the user agent
* `content-length`: stores the actual size in bytes of the uploaded data

A resulting uploaded file may look as follows:

```xml
<request>
  <parameters>
    <parameter>
      <name>upload-form-element-name</name>
      <filename>photo.jpg</filename>
      <content-type>image/jpeg</content-type>
      <content-length>2345</content-length>
      <value xsi:type="xs:anyURI">file:/C:/Tomcat/temp/upload_00000005.tmp</value>
    </parameter>
  </parameters>
</request>
```

_NOTE: The URL stored as the value of the upload or request body is only accessible from the server side, and will not be accessible from a client such as a web browser. It is not guaranteed to be a `file:` URL, only that it can be read with Orbeon Forms's [URL generator](url-generator.md)._
