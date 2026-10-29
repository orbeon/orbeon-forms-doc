# Orbeon Forms 2026.1

__Wednesday, December 31, 2025__  

Today we released Orbeon Forms 2025.1! This release introduces new features, many improvements, and bug-fixes!

![Orbeon Forms 2025.1](/images/orbeon-forms-2025.1.webp)

## Major new features

TODO

## Other new features and enhancements

- Navigation bar: Color scheme selector ([#7996](https://github.com/orbeon/orbeon-forms/issues/7996))
- Wizard: option to hide the table of contents ([#7960](https://github.com/orbeon/orbeon-forms/issues/7960))
- XPath: new `xxf:csv-to-xml()` and `xxf:xml-to-csv()` functions ([#6826](https://github.com/orbeon/orbeon-forms/issues/6826))

## Compatibility and upgrade notes

### Wizard separate table of contents property deprecation

The `oxf.xforms.xbl.fr.wizard.separate-toc` property and `<wizard-separate-toc>` form metadata element are deprecated in favor of `oxf.xforms.xbl.fr.wizard.toc.position` and `<wizard-toc-position>` ([#7960](https://github.com/orbeon/orbeon-forms/issues/7960)). Existing configurations using `separate-toc` continue to be supported.

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
