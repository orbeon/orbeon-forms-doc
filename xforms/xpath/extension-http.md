# HTTP request functions



## xxf:get-portlet-mode()

[SINCE Orbeon Forms 4.2]

Return the portlet mode.

```xpath
xxf:get-portlet-mode() as xs:string
```

If running within a portlet context, return the portlet mode (e.g. `view`, `edit`), otherwise return the empty sequence.

*NOTE: This function only works with the full portlet. The proxy portlet is not supported.*

## xxf:get-remote-user()

```xpath
xxf:get-remote-user() as xs:string?
```

Returns the username for the current user of the application, if known by the container, for instance because users log in with BASIC of FORM-based authentication.

## xxf:get-request-attribute()

```xpath
xxf:get-request-attribute(
    $name         as xs:string,
    $content-type as xs:string?
) as item()?
```

The `xxf:get-request-attribute()` function returns the value of the given request attribute. The attribute may have been previously placed in the request through Java code, or using `xxf:set-request-attribute()`, for example.

The types of attribute objects supported are the same types supported by the Scope generator, plus types stored with `xxf:set-request-attribute()`.

If present, the second parameter can specify the `text/plain` content type. In that case, if a String object is retrieved, it is returned as an `xs:string` instead of being parsed as XML.

```xml
<!-- Get the "document" attribute and use it to replace instance "my-instance" -->
<xf:insert
  ref="instance('my-instance')"
  origin="xxf:get-request-attribute('document')"/>
```

## xxf:get-request-header()

```xpath
xxf:get-request-header(
    $header-name as xs:string
) as xs:string*

xxf:get-request-header(
    $header-name as xs:string,
    $encoding    as xs:string
) as xs:string*
```

The `xxf:get-request-header()` function returns the value(s) of the given request HTTP header.

- `$header-name`
    - required header name
    - The case is ignored.
- `$encoding`
    - [SINCE Orbeon Forms 2018.2]
    - optional encoding name
    - values
        - `ISO-8859-1` (the default): each byte in the header value represents an ISO-8859-1 character
        - `UTF-8`: each byte in the header value represents a UTF-8 byte
    - *NOTE: The only supported standard for header names is ISO-8859-1. But in practice, headers can be encoded with other character encodings. The callee must then know which character encoding is passed.*

```xml
<!-- Remember the User-Agent header -->
<xf:setvalue
  ref="user-agent"
  value="xxf:get-request-header('User-Agent')"/>
```

This function can be used even after page initialization, and can be used everywhere other XPath functions are supported.

## xxf:get-request-method()

[SINCE Orbeon Forms 4.2]

Return the current HTTP method.

```xpath
xxf:get-request-method() as xs:string
```

Return the HTTP method of the current request, such as `GET`, `POST`, etc.

## xxf:get-request-parameter()

```xpath
xxf:get-request-parameter(
    $parameter-name as xs:string
) as xs:string*
```

The `xxf:get-request-parameter()` function returns the value(s) of the given request parameter.

```xml
<!-- Remember the "columns" parameter -->
<xf:setvalue
  ref="columns"
  value="xxf:get-request-parameter('columns')"/>
```

This function can be used even after page initialization, and can be used everywhere other XPath functions are supported.

_NOTE: By default, most if not all servlet containers do not use the UTF-8 encoding but use ISO-8859-1 instead to decode__ URL parameters__. You can configure your servlet container to support UTF-8 instead. See the following resources:_

* [http://stackoverflow.com/questions/138948/how-to-get-utf-8-working-in-java-webapps](http://stackoverflow.com/questions/138948/how-to-get-utf-8-working-in-java-webapps)
* [http://www.mail-archive.com/users@tomcat.apache.org/msg48593.html](http://www.mail-archive.com/users@tomcat.apache.org/msg48593.html)
* [http://tomcat.apache.org/tomcat-6.0-doc/config/http.html](http://tomcat.apache.org/tomcat-6.0-doc/config/http.html)

## xxf:get-request-context-path()

[SINCE Orbeon Forms 2018.2.1]

```xpath
xxf:get-request-context-path() as xs:string
```

The `xxf:get-request-context-path()` function returns the context path of the incoming HTTP request.

This function can be used even after page initialization, and can be used everywhere other XPath functions are supported.

## xxf:get-request-path()

```xpath
xxf:get-request-path() as xs:string
```

The `xxf:get-request-path()` function returns the path of the incoming HTTP request (without the Java servlet context if any).

```xml
<xf:setvalue
  ref="request-path"
  value="xxf:get-request-path()"/>
```

This function can be used even after page initialization, and can be used everywhere other XPath functions are supported.

## xxf:get-session-attribute()

```xpath
xxf:get-session-attribute(
    $name         as xs:string,
    $content-type as xs:string?
) as item()?
```

The `xxf:get-session-attribute()` function returns the value of the given session attribute.

The types of attribute objects supported are the same types supported by the Scope generator, plus types stored with `xxf:set-session-attribute()`.

If present, the second parameter can specify the `text/plain` content type. In that case, if a String object is retrieved, it is returned as an `xs:string` instead of being parsed as XML.

```xml
<!-- Get the "document" attribute and use it to replace instance "my-instance" -->
<xf:insert
  ref="instance('my-instance')"
  origin="xxf:get-session-attribute('document')"/>
```

## xxf:get-window-state()

[SINCE Orbeon Forms 4.2]

Return the portlet window state.

```xpath
xxf:get-window-state() as xs:string
```

If running within a portlet context, return the window state (e.g. `normal`, `minimized`, `maximized`), otherwise return the empty sequence.

*NOTE: This function only works with the full portlet. The proxy portlet is not supported.*

## xxf:is-user-in-role()

```xpath
xxf:is-user-in-role(
    $role as xs:string
) as xs:boolean
```

Returns true if and only if the container recognizes that the current user of the application has the specified role. Roles will be typically known by the container when users are logged in using either BASIC or FORM-based authentication.


## xxf:set-request-attribute()

```xpath
xxf:set-request-attribute(
    $name  as xs:string,
    $value as item()
)
```

The `xxf:set-request-attribute()` function stores the given value as a request attribute.

```xml
<!-- Set the "document" attribute into the request -->
<xf:insert
  context="."
  origin="xxf:set-request-attribute('document', instance('my-instance'))"/>
```

## xxf:set-session-attribute()

```xpath
xxf:set-session-attribute(
    $name  as xs:string,
    $value as item()
)
```

The `xxf:set-session-attribute()` function stores the given value as a session attribute.

```xml
<!-- Set the "document" attribute into the session -->
<xf:insert
  context="."
  origin="xxf:set-session-attribute('document', instance('my-instance'))"/>
```

## xxf:user-ancestor-organizations()

[SINCE Orbeon Forms 2016.3]

```xpath
xxf:user-ancestor-organizations(
    $leaf-org as xs:string
) as xs:string*
```

Return the current user's organizations which are the ancestors of the leaf organization passed if any, from leaf to root. The leaf organization itself is not returned.

Say you have:

```
Orbeon, Inc.
└── Orbeon California
    └── Orbeon San Mateo

```

Then:

```xpath
xxf:user-ancestor-organizations('Orbeon San Mateo')
```

returns:

```
'Orbeon California', 'Orbeon, Inc.'
```

This:
 
```xpath
for $org in 'Orbeon San Mateo'
    return ($org, xxf:user-ancestor-organizations($org))
```

returns:

```
'Orbeon San Mateo', Orbeon California', 'Orbeon, Inc.'
```

This function only works with the header-driven method. See [Form Runner Access Control Setup](../../form-runner/access-control/README.md).

## xxf:user-group()

[SINCE Orbeon Forms 4.9]

```xpath
xxf:user-group() as xs:string?
```

Return the current user's group if available. This function works with container- and header-driven methods. See [Form Runner Access Control Setup](../../form-runner/access-control/README.md).

## xxf:user-organizations()

[SINCE Orbeon Forms 2016.3]

```xpath
xxf:user-organizations() as xs:string*
```

Return the current user's leaf organizations if any.

Say you have these organizations:

```
Orbeon, Inc.
└── Orbeon California
    └── Orbeon San Mateo

California Department of Education
└── Local School District
```

`xxf:user-organizations()` returns:

```
'Orbeon San Mateo', 'Local School District'
```

This function only works with the header-driven method. See [Form Runner Access Control Setup](../../form-runner/access-control/README.md).

## xxf:user-roles()

[SINCE Orbeon Forms 4.9]

```xpath
xxf:user-roles() as xs:string*
```

Return the current user's roles if available. This function works with container- and header-driven methods. See [Form Runner Access Control Setup](../../form-runner/access-control/README.md).

## xxf:username()

[SINCE Orbeon Forms 4.9]

```xpath
xxf:username() as xs:string?
```

Return the current user's username if available. This function works with container- and header-driven methods. See [Form Runner Access Control Setup](../../form-runner/access-control/README.md).
