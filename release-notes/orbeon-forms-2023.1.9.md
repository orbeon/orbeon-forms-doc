# Orbeon Forms 2023.1.9

**Tuesday, October 27, 2026**

Today we released Orbeon Forms 2023.1.9 PE! This maintenance release mainly contains bug-fixes and security updates and is recommended for all users of:

* [Orbeon Forms 2023.1.8 PE](orbeon-forms-2023.1.8.md)
* [Orbeon Forms 2023.1.7 PE](orbeon-forms-2023.1.7.md)
* [Orbeon Forms 2023.1.6 PE](orbeon-forms-2023.1.6.md)
* [Orbeon Forms 2023.1.5 PE](orbeon-forms-2023.1.5.md)
* [Orbeon Forms 2023.1.4 PE](orbeon-forms-2023.1.4.md)
* [Orbeon Forms 2023.1.3 PE](orbeon-forms-2023.1.3.md)
* [Orbeon Forms 2023.1.2 PE](orbeon-forms-2023.1.2.md)
* [Orbeon Forms 2023.1.1 PE](orbeon-forms-2023.1.1.md)
* [Orbeon Forms 2023.1 PE](orbeon-forms-2023.1.md)

## Issues addressed

In this release, we have addressed the following issues:

* Security
    * Disallow `xxforms-upload-store` as an untrusted client event ([\#7975](https://github.com/orbeon/orbeon-forms/issues/7975))
    * Email: stricter validation of attachment URLs ([\#7970](https://github.com/orbeon/orbeon-forms/issues/7970))
    * Email: normalize line breaks in header values ([\#7969](https://github.com/orbeon/orbeon-forms/issues/7969))
    * Port fix for CVE-2022-23437 in Apache Xerces ([\#7972](https://github.com/orbeon/orbeon-forms/issues/7972))
    * Enhanced protocol filtering on `xf:output` ([\#7952](https://github.com/orbeon/orbeon-forms/issues/7952))
    * Restrict serving some assets ([\#7923](https://github.com/orbeon/orbeon-forms/issues/7923))
    * Improve sanitation of HTML attributes ([\#7930](https://github.com/orbeon/orbeon-forms/issues/7930))
    * Filter sensitive properties sent to the client / JS environment ([\#7931](https://github.com/orbeon/orbeon-forms/issues/7931))
    * Require absolute URL to the authorizer ([\#7957](https://github.com/orbeon/orbeon-forms/issues/7957))
    * Match service base URL at start of URL only, send token to internal requests only ([\#7944](https://github.com/orbeon/orbeon-forms/issues/7944))
    * Only the host app can establish the Form Runner session in embedding ([\#7948](https://github.com/orbeon/orbeon-forms/issues/7948))
    * Cookie incorrectly forwarded to the persistence layer ([\#7066](https://github.com/orbeon/orbeon-forms/issues/7066))
    * Form Runner service calls: enforce stateless request handling and prevent `Set-Cookie` ([\#7929](https://github.com/orbeon/orbeon-forms/issues/7929))
    * Third-party library updates
* Form Builder
    * Form migration tests on lexical QNames ([\#7624](https://github.com/orbeon/orbeon-forms/issues/7624))
* Form Runner
    * `<fr:repeat-add-iteration>` doesn't handle initial values ([\#7491](https://github.com/orbeon/orbeon-forms/issues/7491))
* Distribution
    * Docker images: update base WildFly version and Docker files ([\#7702](https://github.com/orbeon/orbeon-forms/issues/7702))

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

### Authorizer URL must be absolute

In previous versions, the `oxf.page-flow.authorizer` property accepted an absolute path (such as `/orbeon-auth`), which was resolved relative to the current host.

Starting with this release, the authorizer property strictly requires an absolute URL with a scheme (e.g. `http://localhost:8080/orbeon-auth`). If the property contains a relative or scheme-less path, Orbeon Forms will log an error and deny access.

You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page.

Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon), [Bluesky](https://bsky.app/profile/orbeon.bsky.social), or the [forum](https://groups.google.com/g/orbeon).

We hope you enjoy this release!
