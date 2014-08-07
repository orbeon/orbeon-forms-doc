## Status

[SINCE: Orbeon Forms 4.7]

## Rationale

The intent is to allow Java (and other Java Virtual Machine (JVM)-based languages) applications to easily embed forms produced with Form Builder within other pages.

## Configuration

### Basic configuration

You deploy Form Runner in a separate web app, which can be located in the same servlet container as your web app or in a separate or even remote servlet container.

Your own web app does the following:

- include `orbeon-embedding.jar` under `WEB-INF/lib`
- setup a filter in `WEB-INF/lib`
- call the embedding API when producing a page

This is a typical filter configuration:

```xml
<!-- Declare and configure the embedding filter -->
<filter>
    <filter-name>orbeon-form-runner-filter</filter-name>
    <filter-class>org.orbeon.oxf.fr.embedding.servlet.ServletFilter</filter-class>
    <init-param>
        <param-name>form-runner-url</param-name>
        <param-value>http://localhost:8080/orbeon</param-value>
    </init-param>
    <init-param>
        <param-name>orbeon-prefix</param-name>
        <param-value>/orbeon</param-value>
    </init-param>
</filter>
<!-- Any JSP resource is processed by the filter -->
<filter-mapping>
    <filter-name>orbeon-form-runner-filter</filter-name>
    <url-pattern>*.jsp</url-pattern>
    <dispatcher>REQUEST</dispatcher>
    <dispatcher>FORWARD</dispatcher>
</filter-mapping>
<!-- This ensures that Orbeon resources are proxied appropriately -->
<filter-mapping>
    <filter-name>orbeon-form-runner-filter</filter-name>
    <url-pattern>/orbeon/*</url-pattern>
    <dispatcher>REQUEST</dispatcher>
    <dispatcher>FORWARD</dispatcher>
</filter-mapping>
```

And here is an example of embedding a form from a JSP page:

```jsp
<%@ page
    pageEncoding="utf-8"
    contentType="text/html; charset=UTF-8"
    import="org.orbeon.oxf.fr.embedding.servlet.API" %>
<!DOCTYPE HTML>
<html>
<body>
    <%
        API.embedFormJava(
            request,      // HttpServletRequest: incoming HttpServletRequest
            out,          // Writer: where the embedded form is written
            "orbeon",     // String: Form Runner form name
            "bookshelf",  // String: Form Runner app name
            "new",        // String: Form Runner action name
            null,         // String: Form Runner document id (optional)
            null,         // String: query string (optional)
            null          // Map<String, String>: custom HTTP headers (optional)
        );
    %>
</body>
</html>
```

### Logging configuration

The embedding JAR uses SLF4J for logging. If your application already uses SLF4J and already has slf4j-api.jar, you can remove the one provided by Orbeon under `WEB-INF/lib`. Otherwise, you must keep slf4j-api.jar in your application's `WEB-INF/lib` folder.

*OPTIONAL: In addition, if you want to actually configure logging for the embedding library, you must add a logging adapter for SLF4j and the appropriate configuration file, for example for log4j. See the sample configuration file under `WEB-INF/classes/log4j.properties.template`.*

### HTTP client configuration

The embedding implementation communicates with the rest of Orbeon Forms via HTTP or HTTPS. In general, you probably won't have to change this configuration. By default, it is as follows:

```xml
<init-param>
    <param-name>oxf.http.stale-checking-enabled</param-name>
    <param-value>true</param-value>
</init-param>
<init-param>
    <param-name>oxf.http.so-timeout</param-name>
    <param-value>0</param-value>
</init-param>
<init-param>
    <param-name>oxf.http.chunk-requests</param-name>
    <param-value>false</param-value>
</init-param>
<init-param>
    <param-name>oxf.http.proxy.host</param-name>
    <param-value/>
</init-param>
<init-param>
    <param-name>oxf.http.proxy.port</param-name>
    <param-value/>
</init-param>
<init-param>
    <param-name>oxf.http.proxy.exclude</param-name>
    <param-value/>
</init-param>
<init-param>
    <param-name>oxf.http.ssl.hostname-verifier</param-name>
    <param-value>strict</param-value>
</init-param>
<init-param>
    <param-name>oxf.http.ssl.keystore.uri</param-name>
    <param-value/>
</init-param>
<init-param>
    <param-name>oxf.http.ssl.keystore.password</param-name>
    <param-value/>
</init-param>
<init-param>
    <param-name>oxf.http.proxy.use-ssl</param-name>
    <param-value>false</param-value>
</init-param>
<init-param>
    <param-name>oxf.http.proxy.username</param-name>
    <param-value/>
</init-param>
<init-param>
    <param-name>oxf.http.proxy.password</param-name>
    <param-value/>
</init-param>
<init-param>
    <param-name>oxf.http.proxy.ntlm.host</param-name>
    <param-value/>
</init-param>
<init-param>
    <param-name>oxf.http.proxy.ntlm.domain</param-name>
    <param-value/>
</init-param>
```

Details about the meaning of these parameters are [available here](http://wiki.orbeon.com/forms/doc/developer-guide/configuration-properties/configuration-properties-base#TOC-HTTP-Client).

### Form Runner configuration

TODO

You can enable HTTP gzip compression between the embedding implementation and Form Runner. For example, with Tomcat:

```xml
<Connector
    port="8080"
    protocol="HTTP/1.1"
    connectionTimeout="20000"
    compression="force"
    compressableMimeType="
        text/html,
        text/xml,
        text/plain,
        text/css,
        text/javascript,
        text/json,
        application/x-javascript,
        application/javascript,
        application/json"
    redirectPort="8443"/>
```
## Use cases

TODO

## How it works

The embedding implementation:

- makes an HTTP or HTTPs request to Form Runner to retrieve the HTML to embed when you call the API
- appropriately rewrites URLs in the HTML returned by Form Runner
- keeps track of session and other cookies
- proxies requests for resources, Ajax calls and file uploads to Form Runner

## Limitations

- navigation between pages, such as the Form Runner Edit and Review pages, is not supported
- embedding Form Builder is currently not supported
- embedding multiple forms is known to work in some cases, but has known issues so we don't recommend doing this (see [#1854](https://github.com/orbeon/orbeon-forms/issues/1854))