# Content-Security-Policy header


## Availability

[SINCE Orbeon Forms 2018.1]

## Rationale

The [`Content-Security-Policy` HTTP header](https://developer.mozilla.org/en-US/docs/Web/HTTP/CSP) is a relatively recent HTTP header which "helps to detect and mitigate certain types of attacks, including Cross Site Scripting (XSS) and data injection attacks".

Some organizations set that header to strict values which disallow inline JavaScript and CSS within HTML pages.

Up until Orbeon Forms 2017.2, Orbeon Forms included some inline scripts and CSS in the HTML served to the browser. With Orbeon Forms 2018.1 and newer, Orbeon Forms no longer does this by default. This makes Orbeon Forms safer by default if you set the `Content-Security-Policy` header to disable such inline content.

## Configuration property

The following XForms property allows you to re-enable inline scripts and CSS. The default is `false`:

```xml
<property 
    as="xs:boolean" 
    name="oxf.xforms.inline-resources"                            
    value="true"/>
``` 

We recommend leaving the value to the default of `false`.

## Generating the Content-Security-Policy header

In many cases, the `Content-Security-Policy` header is generated more globally by a reverse proxy or server.

But Orbeon Forms is able to produce that header as well, whether just for testing or for deployment. To enable this, simply uncomment the following entry in the Orbeon Forms WAR file's `web.xml`:


```xml
<init-param>
    <param-name>content-security-policy</param-name>
    <param-value>default-src 'self'</param-value>
</init-param>
```

You can set the `param-value` to any legal value supported by web browsers. In this example, `default-src 'self'` "Refers to the origin from which the protected document is being served, including the same URL scheme and port number. You must include the single quotes." ([Mozilla](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Security-Policy/default-src))



*NOTE: The `content-security-policy` name must remain in lowercase. It is a configuration parameter name, not the actual header name.*

## See also 

- [Security](security.md)
