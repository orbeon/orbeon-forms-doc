# URL Generator

<!-- toc -->

## Introduction

Generators are a special category of processors that have no XML data inputs, only outputs. They are generally used at the top of an XML pipeline to generate XML data from a Java object or other non-XML source.

The URL generator fetches a document from a URL and produces an XML output document. The protocols supported are `http:`, `https:`, and `file:` as well as the Orbeon Forms resource protocol (`oxf:`). See [Resource Managers][1] for more information about the `oxf:` protocol.

## Content type

The URL generator operates in several modes depending on the content type of the source document. The content type is determined according to the following priorities:

1. Use the content type in the `content-type` element of the configuration if `force-content-type` is set to `true`.
2. Use the content type set by the connection (for example, the content type sent with the document by an HTTP server), if any. Note that when using the `oxf:` or `file:` protocol, the connection content type is never available. When using the `http:` protocol, the connection content type may or may not be available depending on the configuration of the HTTP server.
3. Use the content type in the `content-type` element of the configuration, if specified.
4. Use `application/xml`.

In addition, it is possible to force the mode using the `<mode>` configuration element:

```xml
<p:processor name="oxf:url-generator">
  <p:input name="config">
    <config>
      <url>http://example.org/resource</url>
      <mode>binary</mode>
    </config>
  </p:input>
  <p:output name="data" id="binary-document"/>
</p:processor>
```

## XML mode

The XML mode is selected when:

* the content type is `text/xml`, `application/xml`, or ends with `+xml` according to the selection algorithm above
* the `xml` mode is forced using the `<mode>` configuration element

The generator fetches the specified URL and parses the XML document.

The following options are available:

* `validating`:
    * if set to `true`, a validating parser (using a DTD) is used, otherwise a non-validating parser is used
    * default: `false`
* `handle-xinclude`:
    * if set to `true`, handle XInclude inclusions during parsing
    * default: `true`
* `external-entities`:
    * if set to `true`, external entities are processed
    * default: `false`
* `handle-lexical`:
    * if set to `true`, propagate XML comments present in the input
    * default: `true`

Example:

```xml
<p:processor name="oxf:url-generator">
  <p:input name="config">
    <config>
      <url>oxf:/urlgen/note.xml</url>
      <content-type>application/xml</content-type>
      <validating>true</validating>
      <handle-xinclude>false</handle-xinclude>
      <external-entities>false</external-entities>
      <handle-lexical>false</handle-lexical>
    </config>
  </p:input>
  <p:output name="data" id="xml"/>
</p:processor>
```

If the URL is an HTTP or HTTPS URL and the server returns a non-success status code, an exception is raised.

_NOTE: The URL must point to a well-formed XML document. If it doesn't, an exception is raised._

_NOTE: Be careful when setting _external-entities_ to true, as non-trusted documents with external entities could be used by malicious users to inject content into your XML document._

## HTML mode

The HTML mode is selected when:

- the content type is `text/html` according to the selection algorithm above
- the `html` mode is forced using the `<mode>` configuration element

In this mode, the URL generator uses HTML Tidy to transform HTML into XML. This feature is useful to later extract information from HTML using XPath.

Examples:

```xml
<p:processor name="oxf:url-generator">
  <p:input name="config">
    <config>
      <url>http://www.cnn.com</url>
      <content-type>text/html</content-type>
      <tidy-options>
        <show-warnings>false</show-warnings>
        <quiet>true</quiet>
      </tidy-options>
    </config>
  </p:input>
  <p:output name="data" id="html"/>
</p:processor>
```

```xml
<p:processor name="oxf:url-generator">
  <p:input name="config">
    <config>
      <url>oxf:/html/example.html</url>
      <content-type>text/html</content-type>
      <force-content-type>true</force-content-type>
      <tidy-options>
        <show-warnings>false</show-warnings>
        <quiet>true</quiet>
      </tidy-options>
    </config>
  </p:input>
  <p:output name="data" id="html"/>
</p:processor>
```

The `<tidy-options>` part of the configuration in the two examples above is optional. However, by default `quiet` is set to false, which causes HTML Tidy to output messages to the console when it finds invalid HTML. To prevent this, add a `<tidy-options>` section to the configuration with `quiet` set to true.

Even if HTML Tidy has some tolerance for malformed HTML, you should use well-formed HTML whenever possible.

If the URL is an HTTP or HTTPS URL and the server returns a non-success status code, an exception is raised.

## Text mode

The text mode is selected when:

* the content type according to the selection algorithm above starts with `text/` and is different from `text/html` or `text/xml`, for example `text/plain`
* the `text` mode is forced using the `<mode>` configuration element

In this mode, the URL generator reads the input as a text file and produces an XML document containing the text read.

Example:

```xml
<p:processor name="oxf:url-generator">
  <p:input name="config">
    <config>
      <url>oxf:/list.txt</url>
      <content-type>text/plain</content-type>
    </config>
  </p:input>
  <p:output name="data" id="text"/>
</p:processor>
```

Assume the input document contains the following text:

```
This is line one of the input document!  
This is line two of the input document!  
This is line three of the input document!
```

The resulting document consists of a `document` root element containing the text according to the [text document format][2]. The following attributes are present:

* `xsi:type`, set to `xs:string`
* `content-type`, if known
* `status-code`, if the resource was retrieved through HTTP or HTTPS

```xml
<document xsi:type="xs:string" content-type="text/plain">
  This is line one of the input document! This is line two of the input document! This is line three of the input document!
</document>
```

_NOTE: The URL generator performs streaming. It generates a stream of short character SAX events. It is therefore possible to generate an "infinitely" long document with a constant amount of memory, assuming the generator is connected to other processors that do not require storing the entire stream of data in memory, for example the [__SQL processor_][3]_ (with an appropriate configuration to stream BLOBs), or the [_HTTP serializer_][4]._

## JSON mode

[SINCE Orbeon Forms 2016.2]

The JSON mode is selected when:

- the content type is `application/json` according to the selection algorithm above
- the `json` mode is forced using the `<mode>` configuration element

In this mode, the URL generator uses the [XForms 2.0 conversion scheme](../../xforms/submission-json.md) to convert the incoming JSON content to XML.



## Binary mode

The binary mode is selected when:

- the content type is neither one of the XML content types nor one of the `text/*` content types
- the `binary` mode is forced using the `<mode>` configuration element

In this mode, the URL generator uses a Base64 encoding to transform binary content into XML according to the [binary document format][5]. For example:

```xml
<p:processor name="oxf:url-generator">
  <p:input name="config">
    <config>
      <url>oxf:/my-image.jpg</url>
      <content-type>image/jpeg</content-type>
    </config>
  </p:input>
  <p:output name="data" id="image-data"/>
</p:processor>
```

The resulting document consists of a `document` root node containing character data encoded with Base64. The following attributes are present:

* `xsi:type`, set to `xs:base64Binary`
* `content-type`, if known
* `status-code`, if the resource was retrieved through HTTP or HTTPS

```xml
<document xsi:type="xs:base64Binary" content-type="image/jpeg">
  /9j/4AAQSkZJRgABAQEBygHKAAD/2wBDAAQDAwQDAwQEBAQFBQQFBwsHBwYGBw4KCggLEA4R ... KKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooA//2Q==
</document>
```

_NOTE: The URL generator performs streaming. It generates a stream of short character SAX events. It is therefore possible to generate an "infinitely" long document with a constant amount of memory, assuming the generator is connected to other processors that do not require storing the entire stream of data in memory, for example the [__SQL processor_][3]_ (with an appropriate configuration to stream BLOBs), or the [__HTTP serializer_][4]_. _

## Character encoding

For text and XML, the character encoding is determined as follows:

1. Use the encoding in the `encoding` element of the configuration if `force-encoding` is set to `true`.
2. Use the encoding set by the connection (for example, the encoding sent with the document by an HTTP server), if any, unless `ignore-connection-encoding` is set to `true` (for XML documents, precedence is given to the connection encoding as per RFC 3023). Note that when using the `oxf:` or `file:` protocol, the connection encoding is never available. When using the `http:` protocol, the connection encoding may or may not be available depending on the configuration of the HTTP server. The encoding is specified along with the content type in the `content-type` header, for example:
    ```
    content-type: text/html; charset=iso-8859-1
    ```
3. Use the encoding in the `encoding` element of the configuration, if specified.
4. For XML, the character encoding is determined automatically by the XML parser.
5. For text, including HTML: use the default of iso-8859

When reading XML documents, the preferred method of determining the character encoding is to let either the connection or the XML parser auto detect the encoding. In some instances, it may be necessary to override the encoding. For this purpose, the `force-encoding` and `encoding` elements can be used to override this default behavior, for example:

```xml
<p:processor name="oxf:url-generator">
  <p:input name="config">
    <config>
      <url>oxf:/urlgen/note.xml</url>
      <content-type>application/xml</content-type>
      <encoding>iso-8859-1</encoding>
      <force-encoding>true</force-encoding>
    </config>
  </p:input>
  <p:output name="data" id="xml"/>
</p:processor>
```

This use should be reserved for cases where it is known that a document specifies an incorrect encoding and it is not possible to modify the document.

HTML example:

```xml
<p:processor name="oxf:url-generator">
  <p:input name="config">
    <config>
      <url>http://www.cnn.com</url>
      <content-type>text/html</content-type>
      <encoding>iso-8859-1</encoding>
    </config>
  </p:input>
  <p:output name="data" id="html"/>
</p:processor>
```

Note that only the following encodings are supported for HTML documents:

* `iso-8859-1`
* `utf-8`

Also note that use of the HTML `<meta>` tag to specify the encoding from within an HTML document is not supported.

## HTTP headers

When retrieving a document from an HTTP server, you can optionally specify the headers sent to the server by adding one or more `header` elements, as illustrated in the example below:

```xml
<p:processor name="oxf:url-generator">
  <p:input name="config">
    <config>
      <url>http://www.cnn.com</url>
      <content-type>text/html</content-type>
      <header>
        <name>User-Agent</name>
        <value>Mozilla/5.0</value>
      </header>
      <header>
        <name>Accept-Language</name>
        <value>en-us,fr-fr</value>
      </header>
    </config>
  </p:input>
  <p:output name="data" id="html"/>
</p:processor>
```

In addition, you can provide a list of space-separated header names with the `forward-headers` element. Any header listed is automatically forwarded if it exists in the incoming request:

```xml
<forward-headers>Authorization SM_USER</forward-headers>
```

Headers specified with the `header` element have precedence over `forward-headers`.

## Cache control

### Local cache

It is possible to configure whether the URL generator caches documents locally in the Orbeon Forms cache. By default, it does. To disable caching, use the `cache-control/use-local-cache` element, for example:

```xml
<p:processor name="oxf:url-generator">
  <p:input name="config">
    <config>
      <url>http://www.cnn.com</url>
      <content-type>text/html</content-type>
      <cache-control><use-local-cache>false</use-local-cache></cache-control>
    </config>
  </p:input>
  <p:output name="data" id="html"/>
</p:processor>
```

Using the local cache causes the URL generator to check if the document is in the Orbeon Forms cache first. If it is, its validity is checked with the protocol handler (looking at the last modified date for files, the `last-modified` header for http, etc.). If the cached document is valid, it is used. Otherwise, it is fetched and put in the cache.

When the local cache is disabled, the document is never revalidated and always fetched.

### Conditional GET

Usually, the URL generator does forced GET requests. You can enable conditional GETs with the `cache-control/conditional-get` element.

When `conditional-get` is set to true, and if the URL generator finds a corresponding resource in its local cache, it sends a conditional HTTP GET using the `If-Modified-Since` header. If the server responds with a code 304, the URL generator uses the resource it holds in cache, following usual HTTP semantics.

Example of configuration:

```xml
<p:processor name="oxf:url-generator">
  <p:input name="config">
    <config>
      <url>http://www.cnn.com</url>
      <content-type>text/html</content-type>
      <cache-control><conditional-get>true</conditional-get></cache-control>
    </config>
  </p:input>
  <p:output name="data" id="html"/>
</p:processor>
```

Relation to other settings:

* When `handle-xinclude` is set to `true`, `conditional-get` is automatically overridden to false.
* When `conditional-get` is set to true, `use-local-cache` is automatically overridden to true as well.

## Authentication

The simplest way to handle authentication is to embed user names and passwords in the URL:

```xml
<p:processor name="oxf:url-generator">
  <p:input name="config">
    <config>
      <url>http://aUsername:aPassword@example.com</url>
    </config>
  </p:input>
  <p:output name="data" id="html"/>
</p:processor>
```

In that case the default authentication parameters are applied: preemptive authentication is used and forces the HTTP basic scheme.

If you don't want to embed user names and passwords in URLs or need more control over authentication schemes, you can use an `authentication` element:

```xml
<p:processor name="oxf:url-generator">
  <p:input name="config">
    <config>
      <url>http://example.com</url>
      <authentication>
        <username>aUsername</username>
        <password>aPassword</password>
        <preemptive>true|false</password>
        <domain>an NTLM domain name</domain>
      </authentication>
    </config>
  </p:input>
  <p:output name="data" id="html"/>
</p:processor>
```

* The `username` and `password` are self explanatory and contain the username and password.
* When `preemptive` is set to `false`, the preemptive mode is switched off and the URL generator will use a basic or digest scheme as requested by the server.
* When the `domain` element is present the NTLM authentication scheme is used with this domain name.  

## Relative URLs

URLs passed to the URL generator can be relative. For example, consider the following pipeline fragment declared in a file called `oxf:/my-pipelines/backend/import.xpl`:

```xml
<p:processor name="oxf:url-generator">
  <p:input name="config">
    <config>
      <url>../../documents/claim.xml</url>
    </config>
  </p:input>
  <p:output name="data" id="file"/>
</p:processor>
```

In this case, the URL resolves to: `oxf:/documents/claim.xml`.

[1]: ../resources/resource-managers.md
[2]: reference-formats#text-documents
[3]: http://wiki.orbeon.com/forms/doc/developer-guide/processors-sql#binary-data
[4]: processors-serializers-http
[5]: reference-formats#binary-documents

