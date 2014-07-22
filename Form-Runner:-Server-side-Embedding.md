## Status

[SINCE: Orbeon Forms 4.7]

This documentation is preliminary as of 2014-07-22.

## Rationale

The intent is to allow Java (and other Java Virtual Machine (JVM)-based languages) applications to easily embed forms produced with Form Builder within other pages.

## Configuration

You deploy Form Runner in a separate web app, which can be located in the same servlet container as your web app or in a remote servlet container. The embedding code communicates with Form Runner via HTTP or HTTPS.

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
            getServletConfig().getServletContext(),
            request,
            out,
            "orbeon",
            "bookshelf",
            "new",
            null,
            null
        );
    %>
</body>
</html>
```

## Limitations

- navigation between pages, such as the Form Runner Edit and Review pages, is not supported
- embedding Form Builder is currently not supported