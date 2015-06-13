> [[Home]] ▸ [[Installation]]

## Configuration properties

For general documentation about configuration properties, see [[Configuration Properties |Installation ~ Configuration Properties]].

## Default values

For the latest default values of base properties, see [`properties-base.xml`](https://github.com/orbeon/orbeon-forms/blob/master/src/resources-packaged/config/properties-base.xml).

## Encryption properties

### oxf.crypto.password

This property is used to create a private key used for encryption. It is recommended to change the default value of the password, even though a random seed is used.

```xml
<property
  as="xs:string"
  name="oxf.crypto.password"
  value="CHANGE THIS PASSWORD"/>
```

Orbeon forms uses encryption in a few cases, including:

- to send confidential XForms events to the browser
- for the client state mode of the XForms engine (which is not the default)

_NOTE: If the backwards compatibility property `oxf.xforms.password` is defined, then it is used first._

### oxf.crypto.key-length

This property specifies the length of the AES encryption key. The default is 128 bits.

```xml
<property
  as="xs:integer"
  name="oxf.crypto.key-length"
  value="128"/>
```

Higher strength encryption is usually not enabled by default in the JVM. See [Java Cryptography Extension (JCE) Unlimited Strength Jurisdiction Policy Files](http://www.oracle.com/technetwork/java/javase/downloads/jce-6-download-429243.html). When higher strength encryption is available, this value can be changed to 256, for example.

### oxf.crypto.hash-algorithm

This property specifies the default hash algorithm. The default is SHA1. Higher strength encryption is usually not enabled by default in the JVM.

```xml
<property
  as="xs:string"
  name="oxf.crypto.hash-algorithm"
  value="SHA1"/
```

Higher strength encryption is usually not enabled by default in the JVM. See [Java Cryptography Extension (JCE) Unlimited Strength Jurisdiction Policy Files](http://www.oracle.com/technetwork/java/javase/downloads/jce-6-download-429243.html). When higher strength encryption is available, this value can be changed to 256, for example.

Orbeon forms uses hash algorithms in at least the following cases:

- to encode random identifiers, such as document ids in Form Runner
- for internal caching purposes
- XForms
    - keys for client-side scripts
    - keys for aggregated JavaScript and CSS resources
    - keys for dynamic xf:output resources
    - HMAC for server-side uploaded files

## Global properties

### oxf.cache.size

| | |
| --- | --- |
| Purpose |  Setup the size of the Orbeon Forms cache |
| Type |  `xs:integer` |   |
| Default Value |  500 |

Orbeon Forms uses an efficient caching system. Orbeon Forms automatically determines what can be cached and when to expire objects. The cache has a default size of 200, meaning that it can hold 200 objects. This size is reasonable for most applications. A bigger cache tends to make the application faster, but it uses more memory. To tune the cache size, see the suggestions in the [Performance and Tuning][1] section.

### oxf.cache.xpath.size

| | |
| --- | --- |
| Purpose |  Setup the size of the Orbeon Forms XPath cache |
| Type |  `xs:integer` |
| Default Value |  2000 |

This property configures the maximum number of compiled XPath expressions to keep in the XPath cache. To tune the cache size, see the suggestions in the [Performance and Tuning][1] section.

_NOTE: A profiler run shows that 2000 cache entries takes, for fairly typical XPath expressions, about 5 MB of memory._

### Showing the Orbeon Forms version number

[SINCE Orbeon Forms 4.6.1]

This property controls whether Orbeon Forms outputs its version number to the client web browser:

- at the bottom of pages, in particular with Form Runner
- in the `<xh:meta name="generator" content="…">` element
- in combined JavaScript and CSS resource files built by the XForms engine

```xml
<property
  as="xs:boolean"
  name="oxf.show-version"
  value="false"/>
```

Default:

- `prod` mode: `false`
- `dev` mode: `true`

### XSLT output location mode

During development, the following XSLT transformer configuration helps with line number errors. The following values are allowed:

- `none`: no XSLT output line number information provided. This is recommended for deployment.
- `dumb`: minimal XSLT output line number information provided.
- `smart`: maximal XSLT output line number information provided. This is recommended for development.

```xml
<property
    as="xs:string"
    processor-name="oxf:builtin-saxon"
    name="location-mode"
    value="none"/>

<property
    as="xs:string"
    processor-name="oxf:unsafe-builtin-saxon"
    name="location-mode"
    value="none"/>
```

Default:

- `prod` mode: `none`
- `dev` mode: `smart`

## HTTP Server

### Errors and exceptions

The following property specifies whether the server is allowed to send detailed error and exceptions messages to the browser:

```xml
<property
    as="xs:boolean"
    name="oxf.http.exceptions"
    value="false"/>
```

Default:

- `prod` mode: exceptions are not sent to the browser
- `dev` mode: exceptions are sent to the browser

## HTTP Client

### Proxy setup

To configure an HTTP proxy to be used for all the HTTP connections established by Orbeon Forms, add the following two properties:

```xml
<property
    as="xs:string"
    name="oxf.http.proxy.host"
    value="localhost"/>

<property
    as="xs:integer"
    name="oxf.http.proxy.port"
    value="8090"/>

<property
    as="xs:boolean"
    name="oxf.http.proxy.use-ssl"
    value="false"/>
    
<property
    as="xs:string"
    name="oxf.http.proxy.exclude"
    value=""/>
```

By default, the host and port properties are commented and Orbeon Forms doesn't use a proxy. Some of the use cases where you will want to define a proxy include:

- Your network setup requires you to go through a proxy.
- You would like see what goes through HTTP by using a tool that acts as an HTTP proxy, such as [Apache TCPMon][2].

To connect to the proxy over HTTPS, instead of HTTP which is the default, set the `oxf.http.proxy.use-ssl` property to true.

[SINCE Orbeon Forms 4.6]

You can exclude host names from the proxy using the `oxf.http.proxy.exclude` property, which contains a space-delimited list of hostnames.

### SSL hostname verifier

When using HTTPS, you can specify how the hostname of the server is checked against the hostname in its certificate. You do so with the following property:

```xml
<property 
    as="xs:string" 
    name="oxf.http.ssl.hostname-verifier" 
    value="strict"/>
```

The possible values are:

- `strict` — (the default) See [`StrictHostnameVerifier`](http://hc.apache.org/httpcomponents-client-ga/httpclient/apidocs/org/apache/http/conn/ssl/StrictHostnameVerifier.html).
- `browser-compatible` — See [`BrowserCompatHostnameVerifier`](http://hc.apache.org/httpcomponents-client-ga/httpclient/apidocs/org/apache/http/conn/ssl/BrowserCompatHostnameVerifier.html).
- `allow-all` — See [`AllowAllHostnameVerifier`](http://hc.apache.org/httpcomponents-client-ga/httpclient/apidocs/org/apache/http/conn/ssl/AllowAllHostnameVerifier.html).

Typically, you'll leave this property to its default value (`strict`). However, you might need to set it to `allow-all` to be able to connect to a server with a self-signed certificate if the `cn` in the certificate doesn't match the hostname you're using to connect to that server.

### SSL key store

When using HTTPS, you can specify which key store (or trust store) to use to verify the server you are connecting to. The following properties are relevant:

```xml
<property
    as="xs:anyURI"
    name="oxf.http.ssl.keystore.uri"
    value="oxf:/config/my.keystore"/>

<property
    as="xs:string"
    name="oxf.http.ssl.keystore.password"
    value="changeit"/>
```

- `oxf.http.ssl.keystore.uri` specifies the URL of the key store file
    - this can use the `file:` or `oxf:` protocols
    - if this property is blank, the default JSSE algorithm to find a trust store applies (see the [JSSE Reference Guide][3])
- `oxf.http.ssl.keystore.password` specifies the password needed to access the key store file

This can be useful for using self-signed certificates:

- setup your servlet container (usually via a connector configuration) to point to a key store containing your certificate
- setup the `oxf.http.ssl.keystore.*` properties to point to that same key store

This enables a secure connection between Orbeon Forms and the servlet container.

### State setup

When Orbeon Forms performs XForms submissions, or retrieves documents in XPL over HTTP, it has the ability to keep state across requests. State information includes:

- Cookies, including session cookies
- Credentials / authentication information, e.g. HTTP Basic authentication

By default, these settings are stored in the current user session:

```xml
<property as="xs:string"  name="oxf.http.state" value="session"/>
```

You can change the property to the following values:

- `none`: state is not stored at all between HTTP requests
- `request`: state is stored for the duration of an HTTP request from the user's browser
- `session`: state is stored for the duration of the user's session
- `application`: state is stored for the duration of the web application's life

This setting impacts the following HTTP requests:

- Loading of XForms instances with `xf:instance/@src`
- Requests issued by `xf:submission`
- Requests issued by HTTP URLs in XPL pipelines
- Requests issued by `oxf:url-generator` in XPL pipelines
- Requests issued by `oxf:xhtml-to-pdf` when requesting external resources such as CSS, JavaScript, and image files

In fact, any HTTP request from Orbeon Forms should be handled by the above mechanisms. If you find an exception, please let Orbeon know on [the forum][4]!

_NOTE: These settings only control how the HTTP **client*- in Orbeon Forms handles cookies and credentials._


### Headers forwarding

When Orbeon Forms performs XForms submissions, or retrieves documents in XPL over HTTP, it has the ability to forward incoming HTTP headers. For example, if you want to forward the `Orbeon-Client` and the `Authorization` header to your services:

```xml
<property
    as="xs:string"
    name="oxf.http.forward-headers"
    value="Orbeon-Client Authorization"/>
```

_WARNING: For security reasons, you should be careful with header forwarding, as this might cause non trusted services to receive client headers._

### Cookies forwarding

Similar to general headers forwarding, cookies can be forwarded. By default, the property is as follows:

```xml
<property
    as="xs:string"
    name="oxf.http.forward-cookies"
    value="JSESSIONID JSESSIONIDSSO"/>
```

This is typically used to forward session cookies to the same application server.

Be sure to place the main session cookie name first in the list, as special handling takes place with that cookie.

_NOTE: When a username for HTTP Basic authentication is specified, cookies are not forwarded._

_WARNING: For security reasons, you should be careful with cookies forwarding, as this might cause non trusted services to receive client cookies._

### Stale checking

This property is tied to the [HttpClient stale checking](http://hc.apache.org/httpclient-3.x/apidocs/org/apache/commons/httpclient/params/HttpConnectionParams.html#setStaleCheckingEnabled%28boolean%29):

> Defines whether stale connection check is to be used. Disabling stale connection check may result in slight performance improvement at the risk of getting an I/O error when executing a request over a connection that has been closed at the server side.

By default, Orbeon checks for stale HTTP connections. You can disabling stale connection checking by setting the following property to `false` (it is `true` by default):

```xml
<property
    as="xs:boolean"
    name="oxf.http.stale-checking-enabled"
    value="false"/>
```

### Socket timeout

This property is tied to the [HttpClient SO timeout](http://hc.apache.org/httpclient-3.x/apidocs/org/apache/commons/httpclient/params/HttpConnectionParams.html#setSoTimeout%28int%29):

> Sets the default socket timeout (SO_TIMEOUT) in milliseconds which is the timeout for waiting for data. A timeout value of zero is interpreted as an infinite timeout.

By default, Orbeon doesn't set a timeout with HttpClient. Setting a timeout can be potentially dangerous as it can lead to service calls that take longer to run than the timeout you specified to fail in a way that can be unpredictable, as it is possible for your services to sometimes return before the timeout and sometimes after. If, nevertheless, you need to set a timeout, you can do so by adding the following property, e.g. here setting a timeout at 1 minute:

```xml
<property
    as="xs:integer"
    name="oxf.http.so-timeout"
    value="60000"/>
```

_NOTE: These two headers are computed values and it is only possible to override them with constant values by using the properties above. In general we don't recommend overriding these headers by using the properties above._

## URL rewriting

### oxf.url-rewriting.service.base-uri

- **Name:** `oxf.url-rewriting.service.base-uri`
- **Purpose:** Specify the base URL for rewriting service URLs.
- **Type:** `xs:anyURI`
- **Subtype:** HTTP or HTTPS URL
- **Default:** Empty. Rewriting is done against the incoming request.

If this property is set to a non-blank value, URLs are rewritten against it.

Example:

```xml
<property
    as="xs:anyURI"
    name="oxf.url-rewriting.service.base-uri"
    value="http://localhost:8080/orbeon"/>
```

Usually Orbeon Forms will use the host, port, and context name as seen by the browser, e.g. `http://www.mycompany.com/orbeon`, to infer how to reach itself when calling service URLs. In some cases, this needs to be changed e.g.:

- You have a web server (e.g. Apache httpd) which forwards requests to the application server (e.g. Tomcat).
- The application server cannot connect to the web server because of a specific network configuration.

In those cases, the application server will try to connect to `http://www.mycompany.com/orbeon/...` which isn't reachable. To solve those, you can use the `oxf.url-rewriting.service.base-uri` property to provide a URL for the application server which can be reached by the application server, for instance: `http://localhost:8080/orbeon`.


[TODO: Explain where this property is used.]

## Epilogue and theme properties

### oxf.epilogue.theme

| | |
| --- | --- |
| Name |  `oxf.epilogue.theme` |
| Purpose |  Specifies the theme stylesheet. This can be overwritten for a given app by placing a file `theme.xsl` inside the app directory. |
| Type |  `xs:anyURI` |
| Default Value |  `oxf:/config/theme-examples.xsl` |

###  oxf.epilogue.theme.embeddable

| | |
| --- | --- |
| Name |  `oxf.epilogue.theme.embeddable` |
| Purpose |  Specifies the theme stylesheet to use when within a portlet or in embeddable mode. This can be overwritten for a given app by placing a file `theme-embeddable.xsl `inside the app directory. |
| Type |  `xs:anyURI` |
| Default Value |  `oxf:/config/theme-portlet-examples.xsl` |

### oxf.epilogue.theme.renderer

| | |
| --- | --- |
| Name |  `oxf.epilogue.theme.renderer` |
| Purpose |  Specifies the theme stylesheet to use when using the XForms filter, whether in integrated or separate deployment mode. By default, this is a plain theme. |
| Type |  `xs:anyURI` |
| Default Value |  `oxf:/config/theme-portlet-examples.xsl` |

### oxf.epilogue.theme.error

| | |
| --- | --- |
| Name |  `oxf.epilogue.theme.error` |
| Purpose |  Specifies the theme stylesheet to use on the error page |
| Type |  `xs:anyURI` |
| Default Value |  `oxf:/config/theme-error.xsl` |

### oxf.epilogue.use-theme

| | |
| --- | --- |
| Name |  `oxf.epilogue.use-theme` |
| Purpose |  Specifies whether a theme stylesheet must be applied |
| Type |  `xs:boolean` |
| Default Value |  `true` |

### oxf.epilogue.output-xhtml

| | |
| --- | --- |
| Name |  `oxf.epilogue.output-xhtml` |
| Purpose |  Whether to output XHTML to the browser or not |
| Type |  `xs:boolean` |
| Default Value |  `false` |

### oxf.epilogue.renderer-rewrite

| | |
| --- | --- |
| Name |  `oxf.epilogue.renderer-rewrite` |
| Purpose |  Whether the XForms renderer used in separate deployment must rewrite URLs |
| Type |  `xs:boolean` |
| Default Value |  `false` |

### oxf.epilogue.process-svg

| | |
| --- | --- |
| Name |  `oxf.epilogue.process-svg` |
| Purpose |  Whether SVG content must be converted server-side to images |
| Type |  `xs:boolean` |
| Default Value |  `true` |


## Email processor properties

### Global SMTP host

Configure the SMTP host for all email processors. This global property can be overridden by local processor configurations.

```xml
<property
    as="xs:string"
    processor-name="oxf:email"
    name="smtp-host"
    value="mail.example.org"/>
```

### Global SMTP port

Configure the SMTP port for all email processors. This global property can be overridden by local processor configurations.

```xml
<property
    as="xs:string"
    processor-name="oxf:email"
    name="smtp-port"
    value="25"/>
```

### Global SMTP username

Configure the SMTP username for all email processors. This global property can be overridden by local processor configurations.

```xml
<property
    as="xs:string"
    processor-name="oxf:email"
    name="username"
    value="john"/>
```

### Global SMTP password

Configure the SMTP password for all email processors. This global property can be overridden by local processor configurations.

```xml
<property
    as="xs:string"
    processor-name="oxf:email"
    name="password"
    value="secret"/>

### Global SMTP encryption

Configure the SMTP encryption for all email processors. This global property can be overridden by local processor configurations.

```xml
<property
    as="xs:string"
    processor-name="oxf:email"
    name="encryption"
    value="tls"/>
```

### Test SMTP host

Configure a test SMTP host for all email processors. This global property when specified overrides all the other SMTP host configurations for all email processors, whether in the processor configuration or using the `smtp-host` property.

```xml
<property
    as="xs:string"
    processor-name="oxf:email"
    name="test-to"
    value="joe@example.org"/>
```

This property can easily be commented out for deployment, or placed in `properties-local-dev.xml` (see also [Run Modes][7]).

### Test recipient

Configure a test recipient email address for all email processors. This global property when specified overrides all the other SMTP recipient configurations for all email processors.

```xml
<property
    as="xs:string"
    processor-name="oxf:email"
    name="test-to"
    value="joe@example.org"/>
```

This property can easily be commented out for deployment, or placed in `properties-local-dev.xml` (see also [Run Modes][7]).

## Rarely used properties

### oxf.log4j-config

| | |
| --- | --- |
| Purpose |  Configures the logging system |
| Type |  `xs:anyURI` |
| Default Value |  The logging system not initialized with a warning if this property is not present. |

Orbeon Forms uses the log4j logging framework. In Orbeon Forms, log4j is configured with an XML file. Here is the [default Orbeon Forms log4j configuration][8].

If this property is not set, the log4j initialization is skipped. This is useful if another subsystem of your application has already initialized log4j prior to the loading of Orbeon Forms.

_NOTE: You don't usually need to modify this property._

### oxf.pipeline.processors

- **Name:** `oxf.pipeline.processors`
- **Purpose:** Specify the URL of the XML file with processor definitions for the XPL pipeline engine.
- **Subtype: **HTTP or HTTPS URL
- **Default:** oxf:/processors.xml
- **Note:** You don't usually need to modify this property.

### oxf.validation.processor

| | |
| --- | --- |
| Purpose |  Controls the automatic processor validation |
| Type |  `xs:boolean` |
| Default Value |  Enabled |

Many processors validate their configuration input with a schema. This validation is automatic and allows meaningful error reporting. To potentially improve the performance of the application, validation can be disabled in production environments.

_NOTE: It is  strongly discouraged to disable validation, as validation can highly contribute to the robustness of the application._

### oxf.validation.user

| | |
| --- | --- |
| Purpose |  Controls user-defined validation |
| Type |  `boolean` |
| Default Value |  Enabled |

User-defined validation is activated in the [XML Pipeline Definition Language][9] with the attributes `schema-href` and `schema-uri`. To potentially improve the performance of the application, validation can be disabled in production environments.

_NOTE: It is  strongly discouraged to disable validation, as validation can highly contribute to the robustness of the application._

### sax.inspection

| | |
| --- | --- |
| Purpose |  Enable inspection SAX events |
| Type |  `xs:boolean` |
| Default Value |  false |

SAX is the underlying mechanism in Orbeon Forms by which processors receive and generate XML data. Given only the constraints of the SAX API, it is possible for a processor to generate an invalid sequence of SAX events. Another processor that receives that invalid sequence of events may or may not be able to deal with it without throwing an exception. Some processors try to process invalid SAX events, while others throw exceptions. This means that when a processor generating an invalid sequence of SAX events is used in a pipeline, the problem might go unnoticed, or it might cause some other processor downstream to throw an exception.

To deal more efficiently with those cases, the `sax.inspection` property can be set to `true`. When it is set to true, the pipeline engine checks the outputs of every processor at runtime and makes sure that valid SAX events are generated. When an error is detected, an exception is thrown right away, with information about the processor that generated the invalid SAX events.

There is a performance penalty for enabling SAX events inspection. So this property should not be enabled on a production system.

_NOTE: You don't usually need to enable this property._

[1]: http://wiki.orbeon.com/forms/doc/developer-guide/admin/performance-tuning
[2]: http://ws.apache.org/commons/tcpmon/
[3]: http://docs.oracle.com/javase/6/docs/technotes/guides/security/jsse/JSSERefGuide.html#X509TrustManager
[4]: http://www.orbeon.com/ops/doc/home-faq#mailing-list
[7]: http://wiki.orbeon.com/forms/doc/developer-guide/run-modes
[8]: https://github.com/orbeon/orbeon-forms/blob/master/src/resources/config/log4j.xml
[9]: http://wiki.orbeon.com/forms/doc/developer-guide/xml-pipeline-language-xpl
