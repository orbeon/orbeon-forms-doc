# General configuration properties

## Default values

For the latest default values of general properties, see [`properties-base.xml`](https://github.com/orbeon/orbeon-forms/blob/master/src/main/resources/config/properties-base.xml).

## URL rewriting

### oxf.url-rewriting.service.base-uri

| | |
| --- | --- |
| Name | `oxf.url-rewriting.service.base-uri` |
| Purpose | specify the base URL for rewriting some internal service URLs |
| Type | `xs:anyURI` |
| Default Value | Empty. Rewriting is done against the incoming request. |

Usually Orbeon Forms uses the host, port, and context name as seen by the browser, such as:

```
http://www.mycompany.com/orbeon
```

to infer how to reach itself when calling some service URLs (see below for which URLs apply depending on the Orbeon
Forms version). But in some cases, Orbeon Forms cannot reach to itself this way and an explicit base URL must be
specified with this property.

Such cases include:

- accessing the server through different host names (like `https://foo/orbeon` and `https://bar/orbeon` reaching the same Orbeon Forms instance)
- accessing the embedded eXist database (for demo purposes) when the request goes through a reverse proxy

When you are in such configurations, please make sure to set `oxf.url-rewriting.service.base-uri` to point to the local
servlet container instance, for example:

```xml
<property
    as="xs:anyURI"  
    name="oxf.url-rewriting.service.base-uri"              
    value="http://localhost:8080/orbeon"/>
``` 

#### Orbeon Forms 4.7 and newer

Since Orbeon Forms 4.7, this property is only used for the following:

- access to the embedded eXist database
- access to custom services located in the Orbeon web app (there are none by default)

You *don't need* to set this property if:

- you do not use the embedded eXist or custom services
- or you use the embedded eXist database or a custom service and
  - you are running your servlet container on a local computer for testing or deployment
  - or your external server name and port are accessible from the servlet container

When things don't work out of the box, typically when the network setup contains a front-end web server and/or prevents
a network connection from the servlet container to itself, setting it to the following is usually enough:

```xml
<property
    as="xs:anyURI"
    name="oxf.url-rewriting.service.base-uri"
    value="http://localhost:8080/orbeon"/>
```

Make sure to adjust the port and prefix as needed.

#### Orbeon Forms 4.6.x and earlier

Up to and including Orbeon Forms 4.6.x, this property was used for all service calls, including calls to internal
services used by Form Runner and Form Builder, such as loading i18n resources and accessing the persistence
implementation.

With 4.6.x and earlier, you *don't need* to set this property if:

- you are running your servlet container on a local computer for testing or deployment
- or your external server name and port are accessible from the servlet container

When things don't work out of the box, typically when the network setup contains a front-end web server and/or prevents
a network connection from the servlet container to itself, setting it to the following is usually enough:

```xml
<property
    as="xs:anyURI"
    name="oxf.url-rewriting.service.base-uri"
    value="http://localhost:8080/orbeon"/>
```

Make sure to adjust the port and prefix as needed.

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
| Name | `oxf.cache.size` |
| Purpose | set the size of the Orbeon Forms object cache |
| Type | `xs:integer` | |
| Default Value | 1000 |

Orbeon Forms uses an efficient caching system. Orbeon Forms automatically determines what can be cached and when to expire objects. This size is reasonable for most applications. A bigger cache tends to make the application faster, but it uses more memory. To tune the cache size, see the suggestions in the [Performance and Tuning][1] section.

### oxf.xpath.environment-variable.enabled 

Access to environment variables with the [`environment-variable()` function](/xforms/xpath/standard-functions.md#environment-variable) is disabled by default. If you wish to make this XPath function available, set the following property to `true`.

```xml
<property 
    as="xs:boolean" 
    name="oxf.xpath.environment-variable.enabled"
    value="true"/>
```

### oxf.cache.xpath.size

| | |
| --- | --- |
| Name | `oxf.cache.xpath.size` |
| Purpose | set the size of the Orbeon Forms XPath cache |
| Type | `xs:integer` |
| Default Value | 2000 |

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

See [HTTP client configuration properties](properties-general-http-client.md).

## Epilogue and theme properties

### oxf.epilogue.theme

| | |
| --- | --- |
| Name | `oxf.epilogue.theme` |
| Purpose | specifies the theme stylesheet |
| Type | `xs:anyURI` |
| Default Value | `oxf:/config/theme-examples.xsl` |

This can be overwritten for a given app by placing a file `theme.xsl` inside the app directory.

###  oxf.epilogue.theme.embeddable

| | |
| --- | --- |
| Name | `oxf.epilogue.theme.embeddable` |
| Purpose | specifies the theme stylesheet to use when within a portlet or in embeddable mode |
| Type | `xs:anyURI` |
| Default Value | `oxf:/config/theme-portlet-examples.xsl` |

This can be overwritten for a given app by placing a file `theme-embeddable.xsl `inside the app directory.

### oxf.epilogue.theme.renderer

| | |
| --- | --- |
| Name | `oxf.epilogue.theme.renderer` |
| Purpose | specifies the theme stylesheet to use when using the XForms filter, whether in integrated or separate deployment mode |
| Type | `xs:anyURI` |
| Default Value | `oxf:/config/theme-plain.xsl` |

### oxf.epilogue.theme.error

| | |
| --- | --- |
| Name | `oxf.epilogue.theme.error` |
| Purpose | specifies the theme stylesheet to use on the error page |
| Type | `xs:anyURI` |
| Default Value | `oxf:/config/theme-error.xsl` |

### oxf.epilogue.use-theme

| | |
| --- | --- |
| Name | `oxf.epilogue.use-theme` |
| Purpose | whether a theme stylesheet must be applied |
| Type | `xs:boolean` |
| Default Value | `true` |

### oxf.epilogue.output-xhtml

| | |
| --- | --- |
| Name | `oxf.epilogue.output-xhtml` |
| Purpose | whether to output XHTML to the browser or not |
| Type | `xs:boolean` |
| Default Value | `false` |

### oxf.epilogue.renderer-rewrite

| | |
| --- | --- |
| Name | `oxf.epilogue.renderer-rewrite` |
| Purpose | whether the XForms renderer used in separate deployment must rewrite URLs |
| Type | `xs:boolean` |
| Default Value | `false` |

### oxf.epilogue.process-svg

| | |
| --- | --- |
| Name | `oxf.epilogue.process-svg` |
| Purpose | whether SVG content must be converted server-side to images |
| Type | `xs:boolean` |
| Default Value | `true` |


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
```

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

This property can easily be commented out for deployment, or placed in `properties-local-dev.xml` (see also [Run Modes](../../configuration/advanced/run-modes.md)).

### Test recipient

Configure a test recipient email address for all email processors. This global property when specified overrides all the other SMTP recipient configurations for all email processors.

```xml
<property
    as="xs:string"
    processor-name="oxf:email"
    name="test-to"
    value="joe@example.org"/>
```

This property can easily be commented out for deployment, or placed in `properties-local-dev.xml` (see also [Run Modes](../../configuration/advanced/run-modes.md)).

## Rarely used properties

### oxf.log4j-config

| | |
| --- | --- |
| Name | `oxf.log4j-config` |
| Purpose | configure the logging system |
| Type | `xs:anyURI` |
| Default Value | The logging system not initialized with a warning if this property is not present. |

Orbeon Forms uses the log4j logging framework. In Orbeon Forms, log4j is configured with an XML file. Here is the [default Orbeon Forms log4j configuration][8].

If this property is not set, the log4j initialization is skipped. This is useful if another subsystem of your application has already initialized log4j prior to the loading of Orbeon Forms.

_NOTE: You don't usually need to modify this property._

### oxf.pipeline.processors

| | |
| --- | --- |
| Name | `oxf.pipeline.processors` |
| Purpose | specify the URL of the XML file with processor definitions for the XPL pipeline engine |
| Type | `xs:anyURI` |
| Default Value | `oxf:/processors.xml` |

_NOTE: You don't usually need to modify this property._

### oxf.validation.processor

| | |
| --- | --- |
| Name | `oxf.validation.processor` |
| Purpose | control the automatic processor validation |
| Type | `xs:boolean` |
| Default Value | Enabled |

Many processors validate their configuration input with a schema. This validation is automatic and allows meaningful error reporting. To potentially improve the performance of the application, validation can be disabled in production environments.

_NOTE: It is  strongly discouraged to disable validation, as validation can highly contribute to the robustness of the application._

### oxf.validation.user

| | |
| --- | --- |
| Name | `oxf.validation.user` |
| Purpose | control user-defined validation |
| Type | `boolean` |
| Default Value | Enabled |

User-defined validation is activated in the [XML Pipeline Definition Language][9] with the attributes `schema-href` and `schema-uri`. To potentially improve the performance of the application, validation can be disabled in production environments.

_NOTE: It is  strongly discouraged to disable validation, as validation can highly contribute to the robustness of the application._

### sax.inspection

| | |
| --- | --- |
| Name | `sax.inspection` |
| Purpose | enable inspection SAX events |
| Type | `xs:boolean` |
| Default Value | `false` |

SAX is the underlying mechanism in Orbeon Forms by which processors receive and generate XML data. Given only the constraints of the SAX API, it is possible for a processor to generate an invalid sequence of SAX events. Another processor that receives that invalid sequence of events may or may not be able to deal with it without throwing an exception. Some processors try to process invalid SAX events, while others throw exceptions. This means that when a processor generating an invalid sequence of SAX events is used in a pipeline, the problem might go unnoticed, or it might cause some other processor downstream to throw an exception.

To deal more efficiently with those cases, the `sax.inspection` property can be set to `true`. When it is set to `true`, the pipeline engine checks the outputs of every processor at runtime and makes sure that valid SAX events are generated. When an error is detected, an exception is thrown right away, with information about the processor that generated the invalid SAX events.

There is a performance penalty for enabling SAX events inspection. So this property should not be enabled on a production system.

_NOTE: You don't usually need to enable this property._


[1]: http://wiki.orbeon.com/forms/doc/developer-guide/admin/performance-tuning
[3]: http://docs.oracle.com/javase/6/docs/technotes/guides/security/jsse/JSSERefGuide.html#X509TrustManager
[8]: https://github.com/orbeon/orbeon-forms/blob/master/orbeon-war/src/main/webapp/WEB-INF/resources/config/log4j.xml
[9]: http://wiki.orbeon.com/forms/doc/developer-guide/xml-pipeline-language-xpl
