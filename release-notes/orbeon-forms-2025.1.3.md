# Orbeon Forms 2025.1.3

**Tuesday, October xxx, 2026**

Today we released Orbeon Forms 2025.1.3! This maintenance release mainly contains bug-fixes and is recommended for all users of:

* [Orbeon Forms 2025.1.2 PE](orbeon-forms-2025.1.2.md)
* [Orbeon Forms 2025.1.1 PE](orbeon-forms-2025.1.1.md)
* [Orbeon Forms 2025.1 PE](orbeon-forms-2025.1.md)

## New features
 

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

### Date and Time format in readonly modes

In previous versions, the Date and Time control (`fr:datetime`) used the global `oxf.xforms.format.output.dateTime` property to format its value in readonly modes (such as the Form Runner view mode and in PDF output). Because edit mode formats the date and time parts according to the Date and Time control configurations, the format shown in view mode often differed noticeably from edit mode.

Starting with this release:

* The Date and Time control in readonly modes no longer uses the `oxf.xforms.format.output.dateTime` property.
* Instead, it formats the value in readonly modes by combining the configured date format and time format (separated by a space):
  * Date format: from the `oxf.xforms.xbl.fr.date.output-format` property (or `oxf.xforms.format.input.date`)
  * Time format: from the `oxf.xforms.xbl.fr.time.output-format` property (or `oxf.xforms.format.input.time`)
* An `output-format` parameter can also be specified directly on `fr:datetime` or via `oxf.xforms.xbl.fr.datetime.output-format`.

**Backward incompatibility:** If you previously customized `oxf.xforms.format.output.dateTime` to adjust the appearance of the Date and Time control in readonly modes, this property will no longer affect the Date and Time control. You should instead configure the date and time formats using the Date and Time control properties (`oxf.xforms.xbl.fr.date.output-format` and `oxf.xforms.xbl.fr.time.output-format`), or configure `oxf.xforms.xbl.fr.datetime.output-format`.

For more details, see [Date and time component](../form-runner/component/date-and-time.md).

You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page, or use our Docker images.

Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon), [Bluesky](https://bsky.app/profile/orbeon.bsky.social), or the [forum](https://groups.google.com/g/orbeon).

We hope you enjoy this release!
