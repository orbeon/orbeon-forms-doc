# Orbeon Forms 2024.1.6

**Tuesday, October 27, 2026**

Today we released Orbeon Forms 2024.1.6! This maintenance release mainly contains bug-fixes and is recommended for all users of:

* [Orbeon Forms 2024.1.5 PE](orbeon-forms-2024.1.5.md)
* [Orbeon Forms 2024.1.4 PE](orbeon-forms-2024.1.4.md)
* [Orbeon Forms 2024.1.3 PE](orbeon-forms-2024.1.3.md)
* [Orbeon Forms 2024.1.2 PE](orbeon-forms-2024.1.2.md)
* [Orbeon Forms 2024.1.1 PE](orbeon-forms-2024.1.1.md)
* [Orbeon Forms 2024.1 PE](orbeon-forms-2024.1.md)

## Performance enhancements

* **Excel/XML import and export:** Improved performance of Excel export (specifically cell merge region handling and property templates) and added ID indexing for faster form definition processing during import/export ([\#7840](https://github.com/orbeon/orbeon-forms/issues/7840)).

## Issues addressed

In this release, we have addressed many issues, including:

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
    * Check user is authorized on publish ([\#7990](https://github.com/orbeon/orbeon-forms/issues/7990))
    * Only the host app can establish the Form Runner session in embedding ([\#7948](https://github.com/orbeon/orbeon-forms/issues/7948))
    * Cookie incorrectly forwarded to the persistence layer ([\#7066](https://github.com/orbeon/orbeon-forms/issues/7066))
    * Form Builder `/new` to 403 when user can't create forms ([\#7997](https://github.com/orbeon/orbeon-forms/issues/7997))
    * Form Runner service calls: enforce stateless request handling and prevent `Set-Cookie` ([\#7929](https://github.com/orbeon/orbeon-forms/issues/7929))
    * Third-party library updates
* Form Builder
    * Show section template names upon hover ([\#7777](https://github.com/orbeon/orbeon-forms/issues/7777))
    * Form Builder scrolls to wrong position when clicking on an Explanatory Text ([\#7599](https://github.com/orbeon/orbeon-forms/issues/7599))
    * Control Settings: roles are not read in the correct order from the form definition ([\#7600](https://github.com/orbeon/orbeon-forms/issues/7600))
    * Action Editor to upgrade syntax for setting parameter names using `/*/my-param` syntax ([\#7631](https://github.com/orbeon/orbeon-forms/issues/7631))
    * Minor UX bug when clicking left-column in repeated-grid ([\#7662](https://github.com/orbeon/orbeon-forms/issues/7662))
    * FB form template selector: cursor keys no longer scroll templates ([\#7734](https://github.com/orbeon/orbeon-forms/issues/7734))
    * Keyboard shortcuts work erratically ([\#7954](https://github.com/orbeon/orbeon-forms/issues/7954))
    * Form migration tests on lexical QNames ([\#7624](https://github.com/orbeon/orbeon-forms/issues/7624))
* Form Runner
    * If a draft exists, after selecting to either use it or start from scratch, the "Leave Site?" alert shows even when no changes were made ([\#7606](https://github.com/orbeon/orbeon-forms/issues/7606))
    * Uploading file, in deeply nested attachment field, or with a long name, is rejected ([\#7623](https://github.com/orbeon/orbeon-forms/issues/7623))
    * CSS class with AVT and if/then/else fails ([\#7625](https://github.com/orbeon/orbeon-forms/issues/7625))
    * MySQL error: User-level lock name 'orbeon/fr/app-form/…' should not exceed 64 characters ([\#7677](https://github.com/orbeon/orbeon-forms/issues/7677))
    * Published Forms sorted by title shows title for forms not available in the current language first ([\#7695](https://github.com/orbeon/orbeon-forms/issues/7695))
    * Error in data migration possibly due to `LazyList` ([\#7700](https://github.com/orbeon/orbeon-forms/issues/7700))
    * Property to disable warning the user when data is unsafe is not honored anymore ([\#7736](https://github.com/orbeon/orbeon-forms/issues/7736))
    * Upload/attachments: consider using file extension as source of truth ([\#7698](https://github.com/orbeon/orbeon-forms/issues/7698))
    * Excel export: column order should be the same as on Summary page ([\#7583](https://github.com/orbeon/orbeon-forms/issues/7583))
    * "Deleting all records" gets into an infinite loop ([\#7961](https://github.com/orbeon/orbeon-forms/issues/7961))
    * Fix message format syntax in French and Catalan validation dialog message ([\#6184](https://github.com/orbeon/orbeon-forms/issues/6184))
    * Section bottom borders are rendered incorrectly in PDF ([\#7393](https://github.com/orbeon/orbeon-forms/issues/7393))
    * PDF image rotation error with alpha channel ([\#7673](https://github.com/orbeon/orbeon-forms/issues/7673))
    * Icon broken in emergency-medical-consent demo form description ([\#7710](https://github.com/orbeon/orbeon-forms/issues/7710))
    * Process with suspend and an expression with double quote inside single quote leads to XPath syntax error ([\#7712](https://github.com/orbeon/orbeon-forms/issues/7712))
    * Bookshelf demo: "Invalid schema type: publication-year" ([\#7615](https://github.com/orbeon/orbeon-forms/issues/7615))
* XBL Components
    * Help for dropdown with search isn't positioned correctly ([\#7406](https://github.com/orbeon/orbeon-forms/issues/7406))
    * Date control using browser datepicker resets after typing the first digit of the year ([\#7674](https://github.com/orbeon/orbeon-forms/issues/7674))
    * Date and Time incorrectly shown as invalid during internal navigation if previously visited ([\#7699](https://github.com/orbeon/orbeon-forms/issues/7699))
    * Tooltip not always hidden when moving pointer over tooltip ([\#7622](https://github.com/orbeon/orbeon-forms/issues/7622))
* XForms Engine
    * `<xf:select>`/`<xf:copy>` doesn't support attributes correctly ([\#7586](https://github.com/orbeon/orbeon-forms/issues/7586))
    * `xf:dispatch` with delay behaving differently from what is depicted in documentation ([\#7648](https://github.com/orbeon/orbeon-forms/issues/7648))
    * Mirrored inner `fr-form-instance` not propagating from `setvalue` ([\#7644](https://github.com/orbeon/orbeon-forms/issues/7644))
    * Couldn't cast `XFormsVariableControl` to `XFormsEventHandler` ([\#7880](https://github.com/orbeon/orbeon-forms/issues/7880))
    * Don't resend `<xxf:submission>` on subsequent Ajax requests ([\#7447](https://github.com/orbeon/orbeon-forms/issues/7447))
* Offline
    * `fr|databound-select1-search` XPath expression fails in JS environment ([\#7849](https://github.com/orbeon/orbeon-forms/issues/7849))
* Embedding
    * `orbeon-embedding.jar` not to contain SLF4J classes ([\#7337](https://github.com/orbeon/orbeon-forms/issues/7337))
* Distribution
    * RestApiTest using S3 occasionally fails on CI ([\#7585](https://github.com/orbeon/orbeon-forms/issues/7585))
    * Docker images: update base Tomcat and WildFly versions, pin PostgreSQL version, and add vulnerability scanning ([\#7702](https://github.com/orbeon/orbeon-forms/issues/7702))

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

### File upload media type detection based on file extension

When uploading files, Orbeon Forms now determines the MIME type primarily based on the file extension rather than relying on the client-provided `Content-Type` header sent by the browser ([\#7698](https://github.com/orbeon/orbeon-forms/issues/7698)).

You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page, or use our Docker images.

Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon), [Bluesky](https://bsky.app/profile/orbeon.bsky.social), or the [forum](https://groups.google.com/g/orbeon).

We hope you enjoy this release!
