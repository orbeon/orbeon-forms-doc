# Securing Form Runner access when embedding

<!-- toc -->

## Introduction

When Form Runner embedding is used, the system has two parts:

- the embedding client, which runs within your application and uses the embedding API
- the Form Runner server, which runs Form Runner and/or Form Builder

In this case not only your application needs to be properly secured, but the Form Runner server also needs to be secured.

## IP filter

TODO

## HTTPS and BASIC Authentication 

First, the connection between the embedding API and Form Runner uses HTTP or HTTPS. As in all cases with HTTP/HTTPS, it is better to use HTTPS so that the connection cannot be snooped on and so that the client knows it is connecting to the desired endpoint. To enable HTTPS, just use a URL starting with `https://` in the `form-runner-url` parameter in `web.xml`. The server / container on which Form Runner runs must have a proper SSL certificate installed.

Second, Form Runner must know that the request comes from the embedding application and not somebody else. For this, one way is to use BASIC HTTP authentication. This too can be done in the `form-runner-url` parameter in `web.xml` by adding a username and password to the URL:

```xml
<init-param>
    <param-name>form-runner-url</param-name>
    <param-value>https://username:password@localhost:8080/orbeon</param-value>
</init-param>
```

The drawback of this solution is that the username and password are in clear in the `web.xml` file, which means that you have to properly secure access to that file.

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
            "my_application",   // String: Form Runner app name
            "my_form",          // String: Form Runner form name
            "new",              // String: Form Runner action name
            null,               // String: Form Runner document id (optional)
            null,               // String: query string (optional)
            headers             // Map<String, String>: custom HTTP headers (optional)
        );
    %>
</body>
</html>
```

## Client certificate

TODO