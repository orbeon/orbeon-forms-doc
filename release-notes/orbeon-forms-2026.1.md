# Orbeon Forms 2026.1

__Wednesday, December 31, 2025__  

Today we released Orbeon Forms 2025.1! This release introduces new features, many improvements, and bug-fixes!

![Orbeon Forms 2025.1](/images/orbeon-forms-2025.1.webp)

## Major new features

TODO

## Other new features and enhancements

TODO

## Compatibility and upgrade notes

### Cookie forwarding and session cookie name

In previous versions, Orbeon Forms assumed by convention that the first cookie listed in the `oxf.http.forward-cookies` property was the session cookie name. If that cookie was not found in the incoming request or did not match the session, Orbeon Forms synthesized a cookie using the session ID. This could inadvertently overwrite a custom cookie's value with the session ID.

Starting with this release:

* The session cookie name is no longer determined by the position of cookies in `oxf.http.forward-cookies`.
* A new property, `oxf.http.forward-cookies.session-cookie-name`, explicitly defines the session cookie name (defaulting to `JSESSIONID`).
* Non-session cookies listed in `oxf.http.forward-cookies` are always forwarded with their incoming values and will never have a session ID synthesized for them.
* Incoming cookies are parsed directly from the incoming `Cookie` header, ensuring custom cookies (such as JWT tokens) and container routing suffixes are preserved.

**Backward incompatibility:** If your application server uses a custom session cookie name other than `JSESSIONID` and you previously relied on placing it first in `oxf.http.forward-cookies`, you must now explicitly configure `oxf.http.forward-cookies.session-cookie-name`:

```xml
<property
    as="xs:string"
    name="oxf.http.forward-cookies.session-cookie-name"
    value="MYSESSIONID"/>
```

For more details, see [Cookies forwarding](../configuration/properties/properties-general-http-client.md#cookies-forwarding).