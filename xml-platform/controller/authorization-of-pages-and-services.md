# Authorization of Pages and Services

<!-- toc -->

## Availability

This feature is available since Orbeon Forms 4.0. Orbeon Forms 3.9 and earlier did *not* protect pages and services as described below.

## Rationale

Orbeon Forms runs within a the servlet container. This has huge benefits, including portability. But in general, such a container is fairly inflexible. For example, you cannot match pages based on regular expressions, and you cannot specify two different forms of authentication in the same web application based on whether you're targeting a page or service (such as form-based authentication, versus basic authentication). In addition, it is not easily possible for a web application to have full access to the container's authentication and authorization mechanisms, certainly not in a pain-free and portable way.

On the other hand, Orbeon Forms would like to allow some level of configuration of this directly in a page flow. The solution chosen is a solution of _delegation_.

## Basic operation

When a request for a page or service reaches the controller, the following takes place by default:

1. The controller checks whether the request is an _internal request_, that is, a request from a local Orbeon Forms application. If that's the case, the request is allowed (this is based on a mechanism of private tokens).
2. If the request is not internal, the controller then checks whether the request has a _public method_. If so, the request is allowed.
    * For pages, the public methods are GET and HEAD by default.
    * For services, there are no public methods by default.
3. If the method is not public, the controller then delegates to an _authorization_ service.

## Configuration

### Public methods

Global properties for the controller allow changing the defaults:

```xml
<property
  as="xs:string"
  processor-name="oxf:page-flow"
  name="page-public-methods"
  value="GET HEAD POST"/>

<property
  as="xs:string"
  processor-name="oxf:page-flow"
  name="service-public-methods"
  value="GET HEAD"/>
```

On a per-controller basis, the `<controller>` element supports two attributes, `page-public-methods` and `service-public-methods`. These override the global properties:

```xml
<controller xmlns="http://www.orbeon.com/oxf/controller"
  page-public-methods="GET HEAD POST"
  service-public-methods="GET HEAD">
```

Finally, the `<page>` and `<service>` elements support the `public-methods` attribute, which allows setting the list of public methods for a given page or service:

```xml
<page    public-methods="GET HEAD POST" view="view.xpl"/>
<service public-methods="GET HEAD"      view="view.xpl"/>
```

In addition to any HTTP method name, the property or attribute supports the `#all` token to indicate that all methods are allowed.

### Backward compatibility

With previous versions of Orbeon Forms, requests were unrestricted. Although we don't recommend it, if you want to, you can enable the old behavior with the following properties:

```xml
<property
  as="xs:string"
  processor-name="oxf:page-flow"
  name="page-public-methods"
  value="GET HEAD POST PUT DELETE"/>

<property
  as="xs:string"
  processor-name="oxf:page-flow"
  name="service-public-methods"
  value="GET HEAD POST PUT DELETE"/>
````

### Authorization service

You can configure the authorization service via a property:

```xml
<property
  as="xs:anyURI"
  processor-name="oxf:page-flow"
  name="authorizer"
  value="/orbeon-auth"/>
```

The value of this property is either an absolute URL or an absolute path. If it is an absolute path, it is resolved against the host receiving the request. For example, if your servlet container is deployed on `http://localhost:8080/`, the path above resolves to `http://localhost:8080/orbeon-auth`.

This means that the authorization service can reside within the same container as Orbeon Forms, or in a completely different location.

## How the authorization service works

It's pretty simple: Orbeon Forms _forwards_ the incoming request to that service. This includes: HTTP method, headers, and requested path. Note, that even in the case of a POST or PUT, the request body is _not_ forwarded. The requested path is appended to the path of the authorization service. For example, if the request was for the following service:

    /fr/service/exist/crud/acme/gaga/form/form.xhtml

with the settings above, the following URL would be called:

    http://localhost:8080/orbeon-auth/fr/service/exist/crud/acme/gaga/form/form.xhtml

This allows the authorization service to discriminate between different types of pages and services.

## A simple authorization service

Orbeon Forms ships with a very simple WAR file: `orbeon-auth.war`. This war file contains a dummy servlet and a web.xml with stub to configure BASIC authentication. You typically deploy this WAR file within the same servlet container as the main Orbeon Forms WAR file. This means that you can set the property above to `/orbeon-auth`. Here is the default content of the `web.xml`:

```xml
<web-app version="2.4"
         xsi:schemaLocation="http://java.sun.com/xml/ns/j2ee http://java.sun.com/xml/ns/j2ee/web-app_2_4.xsd"
         xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
         xmlns="http://java.sun.com/xml/ns/j2ee">
    <display-name>Optional authorizer for Orbeon Forms</display-name>
    <description>
        See "Authorization of pages and services" in the Orbeon Forms doc:
        http://wiki.orbeon.com/forms/doc/developer-guide/page-flow-controller/authorization
    </description>
    <servlet>
        <servlet-name>orbeon-authorizer-servlet</servlet-name>
        <servlet-class>org.orbeon.oxf.controller.AuthorizerServlet</servlet-class>
    </servlet>
    <!-- The authorizer servlet handles any request -->
    <servlet-mapping>
        <servlet-name>orbeon-authorizer-servlet</servlet-name>
        <url-pattern>/*</url-pattern>
    </servlet-mapping>
    <!-- Example: require that all external requests to Form Runner services are
         authenticated with BASIC authentication and have the orbeon-service role.
         Block any other request. -->
    <security-constraint>
        <web-resource-collection>
            <web-resource-name>Form Runner services</web-resource-name>
            <url-pattern>/fr/service/*</url-pattern>
        </web-resource-collection>
        <auth-constraint>
            <role-name>orbeon-service</role-name>
        </auth-constraint>
    </security-constraint>
    <security-constraint>
        <web-resource-collection>
            <web-resource-name>Everything else</web-resource-name>
            <url-pattern>/*</url-pattern>
        </web-resource-collection>
        <!-- Make sure there is an empty auth-constraint to require authentication.
             But since there are no constraints specified, authentication will always
             fail. -->
        <auth-constraint/>
    </security-constraint>
    <login-config>
        <auth-method>BASIC</auth-method>
    </login-config>
    <security-role>
        <role-name>orbeon-service</role-name>
    </security-role>
</web-app>
```

By default, this configuration enables BASIC auth and only authorizes requests with a role called `orbeon-service`. This role is arbitrary. You can configure your servlet container to handle any role. If you are using Tomcat, you can for example configure users and roles in the [tomcat-users.xml file][1].

With this setup, a request for an Orbeon Forms page or service is forwarded to this authorization service. If the request comes with appropriate credentials for BASIC authentication which translates into a user with the given role, the servlet returns a successful response, and the request is authorized. Otherwise, the request is denied, and the request to the page or service is rejected.

Note that this example is just about the simplest way that you can implement the authorization service. But you most likely will want to do some more advanced configuration. Also, note that you are not limited to BASIC authentication. You could for example fully delegate authorization to your own servlet.

## Implementing your own service

You don't have to use the WAR provided to implement your service. You can implement your own servlet, or in fact implement your own service which can reside on any server and be written with any technology you like.

Just make sure that your service responds with a successful HTTP return code when the request is authorized, and a non-successful HTTP return code when the request is not authorized (such as 401 or 403).

It is very important to validate _services_ independently from _logged in users_. This is because in general, human users of the application must not be able to access services directly!

[1]: http://tomcat.apache.org/tomcat-7.0-doc/realm-howto.html#UserDatabaseRealm
