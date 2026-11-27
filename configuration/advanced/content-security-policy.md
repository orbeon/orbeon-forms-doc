# Content-Security-Policy header

## Availability

\[SINCE Orbeon Forms 2018.1]

## What is the Content-Security-Policy header?

The [`Content-Security-Policy` HTTP header](https://developer.mozilla.org/en-US/docs/Web/HTTP/CSP), also known as CSP header, is a relatively recent HTTP header which "helps to detect and mitigate certain types of attacks, including [Cross Site Scripting (XSS)](https://en.wikipedia.org/wiki/Cross-site_scripting) and data injection attacks".

## Disabling inline scripts and CSS

### Introduction

Some organizations set the `Content-Security-Policy` header to strict values which disallow inline JavaScript and CSS within HTML pages, for example with `default-src 'self'`.

Up until Orbeon Forms 2017.2, Orbeon Forms included some inline scripts and CSS in the HTML served to the browser. Disabling inline scripts and CSS with `Content-Security-Policy` with those Orbeon Forms versions will prevent Orbeon Forms from working correctly.

With Orbeon Forms 2018.1 and newer, Orbeon Forms no longer produces inline scripts and CSS by default, which allows for these strict values of the `Content-Security-Policy` header. While Orbeon Forms already [takes measures](security.md) against XSS and data injection, disabling the use of inline scripts can make Orbeon Forms even safer by default.

### Inline stylesheets of the head

[\[SINCE Orbeon Forms 2026.1\]](/release-notes/orbeon-forms-2026.1.md)

Orbeon Forms serves the `<style>` elements of the `<head>` of the page from a URL: the `<xh:style>` elements of a form definition, the inline stylesheets of XBL components, and the CSS produced for the [`oxf.fr.style.variable` properties](/form-runner/styling/css-variables.md). With earlier versions, a policy which disallows inline styles, such as `default-src 'self'`, blocked them.

A `<style>` element stays inline when:

- it has an attribute other than `type="text/css"` and `media="all"`;
- it contains `url(` or `@import`;
- [combined resources](/configuration/advanced/javascript-css-assets.md#combined-asset-resources) are disabled.

Orbeon Forms doesn't change `style` attributes, for example in rich text content, or the styles which JavaScript libraries add to the page in the browser.

### Configuration property

The following XForms property allows you to re-enable inline scripts and CSS. The default is `false`:

```markup
<property 
    as="xs:boolean" 
    name="oxf.xforms.inline-resources"                            
    value="true"/>
```

We recommend leaving the value to the default of `false`.

With `true`, the [inline stylesheets of the head](#inline-stylesheets-of-the-head) stay inline too.

## Generating the Content-Security-Policy header

In many cases, the `Content-Security-Policy` header is generated more globally by a reverse proxy or server.

But Orbeon Forms is able to produce that header as well, whether just for testing or for deployment. To enable this, simply uncomment the following entry in the Orbeon Forms WAR file's `web.xml`:

```xml
<context-param>
    <param-name>oxf.orbeon-form-runner-auth-servlet-filter.content-security-policy</param-name>
    <param-value>default-src 'self'; img-src 'self' data:</param-value>
</context-param>
```

With Orbeon Forms 2022.1 and earlier, the entry is an `<init-param>` of the `orbeon-form-runner-auth-servlet-filter` filter instead:

```xml
<init-param>
    <param-name>content-security-policy</param-name>
    <param-value>default-src 'self'; img-src 'self' data:</param-value>
</init-param>
```

You can set the `param-value` to any legal value supported by web browsers. In this example, `default-src 'self'` "Refers to the origin from which the protected document is being served, including the same URL scheme and port number. You must include the single quotes." ([Mozilla](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Security-Policy/default-src))

_NOTE: The `content-security-policy` part of the name must remain in lowercase. It is a configuration parameter name, not the actual header name._

## See also

* [Security](security.md)
* Blog post: [Improving security with the Content-Security-Policy header](https://blog.orbeon.com/2018/08/improving-security-with-content.html)
