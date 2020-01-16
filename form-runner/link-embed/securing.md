# Securing Form Runner access when embedding



## Introduction

When the [Form Runner Java Embedding API](java-api.md) or the [Form Runner Liferay Proxy Portlet](liferay-proxy-portlet.md) is used, the system has two parts:

- the *embedding client*, which runs within your application (using embedding API) or within a portal
- the Form Runner server, which runs Form Runner and/or Form Builder

In this case not only does your application or the portal need to be secured, but the separate Form Runner server also needs to be properly secured. If that is not the case, then a user or attacker might, inadvertently or intentionally, manage to access the Form Runner server directly without going through your application or portal, possibly gaining access to forms or operations that must be disallowed. 

The main idea is that the Form Runner server must only respond to requests coming from your application or the proxy portlet, but not from direct HTTP requests.

This page describes a few solution which are not mutually exclusive:

- IP filter
- HTTPS and BASIC authentication
- Client certificate

## IP filter

A simple security step consists in setting up an IP filter on the Form Runner side. You can do this for example  with the third-party [UrlRewriteFilter](http://tuckey.org/urlrewrite/) servlet filter.

This is the Swiss Army knife of servlet filters. In particular, it allows you to filter requests based on on a number of factors, including the IP address of the originating host. In this case, that IP address would be that of the server on which your application or portal runs. That IP address would typically be local to your network.

If both your application or portal and Form Runner run on the same server, you can even restrict access to requests coming from `localhost`.

*WARNING: Using an IP filter does not protect access to users who have any kind of access to the host machine. For example, a user with rights to `ssh` into that machine will likely be able to connect to Form Runner via HTTP. So using an IP filter is only a solution in cases where the servers and network are trusted.*

## HTTPS and BASIC authentication

### Using HTTPS

#### Introduction

The connection between the embedding API and Form Runner uses HTTP or HTTPS. As in all cases with HTTP/HTTPS, it is better to use HTTPS so that the connection cannot be snooped on and so that the client knows it is connecting to the desired endpoint.

#### Client setup

To enable HTTPS, just use a URL starting with `https://` in the `form-runner-url` parameter in `web.xml`.

#### Server setup

The server or container on which Form Runner runs must have a proper SSL certificate installed and listen on the standard HTTPS port (443), unless a port is explicitly set by the client.

### Using BASIC authentication

#### Introduction

Form Runner must know that the request comes from the embedding application and not somebody else. For this, one way is to use [BASIC HTTP authentication](https://en.wikipedia.org/wiki/Basic_access_authentication), a standard HTTP-based way of passing a username and password.

There are two ways to set username and password using the embedding API:

- statically, within `web.xml`
- dynamically, by passing the `Authorization` when calling the API

#### Client setup with static username and password

This can be done in the `form-runner-url` parameter in `web.xml` by adding a username and password to the URL:

```xml
<init-param>
    <param-name>form-runner-url</param-name>
    <param-value>https://username:password@localhost:8080/orbeon</param-value>
</init-param>
```

The drawback of this solution is that the username and password are in clear in the `web.xml` file, which means that you have to properly secure access to that file.

#### Client setup with dynamic username and password

Another way is to pass the `Authorization` header directly from the embedding code, for example, assuming Java 8 which includes `java.util.Base64`:

```jsp
<%@ page
    pageEncoding="utf-8"
    contentType="text/html; charset=UTF-8"
    import="org.orbeon.oxf.fr.embedding.servlet.API" %>
<!DOCTYPE HTML>
<html>
<body>
    <%
        String username      = "jdoe";
        String password      = "secret";
        String combined      = username + ':' + password;
        String authorization = java.util.Base64.getEncoder().encodeToString(combined.getBytes);
        
        java.util.Map<String, String> headers = new java.util.HashMap<String, String>();
        headers.put("Authorization", "Basic " + authorization);
        
        API.embedFormJava(
            request,            // HttpServletRequest: incoming HttpServletRequest
            out,                // Writer: where the embedded form is written
            "my-application",   // String: Form Runner app name
            "my-form",          // String: Form Runner form name
            "new",              // String: Form Runner action name
            null,               // String: Form Runner document id (optional)
            null,               // String: query string (optional)
            headers             // Map<String, String>: custom HTTP headers (optional)
        );
    %>
</body>
</html>
```

#### Server setup

On the Form Runner side, BASIC authentication must be set up. `web.xml` must use the `BASIC` `auth-method`:

```xml
<login-config>
    <auth-method>BASIC</auth-method>
</login-config>
```

In addition, a user and password must be configured in the container. With Tomcat, the easiest way is to use [`tomcat-users.xml`](https://tomcat.apache.org/tomcat-8.5-doc/realm-howto.html#UserDatabaseRealm).

*NOTE: `web.xml` only supports one `auth-method`. This means that if you configure Form Runner with the `BASIC` method to authenticate your application, and you attempt to access Form Runner directly with a web browser, you will also have to use the `BASIC` authentication. You cannot, at the same time, use the `FORM` authentication.*

<!--

## Client certificate

TODO

## Secret token passing

TODO

-->

## See also

- [Form Runner Java Embedding API](java-api.md)
- [Form Runner Liferay Proxy Portlet](liferay-proxy-portlet.md) 
