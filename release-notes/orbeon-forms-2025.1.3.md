# Orbeon Forms 2025.1.3

__DRAFT RELEASE NOTES__

**TODO, October TODO, 2026**

Today we released Orbeon Forms 2025.1.3! This maintenance release contains bug-fixes, performance improvements, and new features and is recommended for all users of:

* [Orbeon Forms 2025.1.2 PE](orbeon-forms-2025.1.2.md)
* [Orbeon Forms 2025.1.1 PE](orbeon-forms-2025.1.1.md)
* [Orbeon Forms 2025.1 PE](orbeon-forms-2025.1.md)

## New features

### Choices Editor: copy and paste to/from Excel

In Form Builder, the "Edit Choices" dialog now features "Copy to Clipboard" and "Paste from Clipboard" buttons. This allows you to easily copy the list of item choices (including labels, values, and hints) to and from Microsoft Excel or other spreadsheet tools using TSV format ([\#7784](https://github.com/orbeon/orbeon-forms/issues/7784)).

In addition, you can now press <kbd>E</kbd> (or <kbd>⌘E</kbd> / <kbd>Ctrl+E</kbd>) to quickly open the Edit Choices dialog for a selected selection control ([\#7946](https://github.com/orbeon/orbeon-forms/issues/7946)).

### Form Discovery API

A new persistence API endpoint allows external applications to retrieve the distinct list of published application names, form names, and form versions in both XML and JSON formats ([\#6573](https://github.com/orbeon/orbeon-forms/issues/6573)).

### Option to hide the Wizard Table of Contents

You can now configure wizard forms to completely hide the Table of Contents (TOC) sidebar / topbar, presenting a streamlined step-by-step experience with only previous and next navigation buttons ([\#7960](https://github.com/orbeon/orbeon-forms/issues/7960)). This can be configured in Form Builder via **Form Settings → Wizard**, or via the `oxf.fr.detail.wizard.toc.hide` property.

### Revision history: show workflow stage changes

When viewing the revision history of form data, transitions between workflow stages are now tracked and highlighted alongside field value changes in the diff view ([\#7911](https://github.com/orbeon/orbeon-forms/issues/7911)).

### New `log()` process action

A new `log()` action is now available in Form Runner processes. This enables you to output custom diagnostic or audit messages to the application log with custom log levels (`error`, `warn`, `info`, `debug`) directly from action pipelines ([\#7781](https://github.com/orbeon/orbeon-forms/issues/7781)).

### Overriding XBL component resources

Form Runner now provides a unified mechanism using `fr:resource()` to override text and messages defined within XBL components (such as buttons, hints, and alerts in attachment and date components) via configuration properties or form resources ([\#3235](https://github.com/orbeon/orbeon-forms/issues/3235)).

### Model Context Protocol (MCP) server enhancements

The Form Builder MCP server for AI agents has been expanded with several new capabilities:

* **Attach PDF templates:** New `form_attach_pdf_template` tool allows AI agents to attach PDF fillable templates to form definitions ([\#7897](https://github.com/orbeon/orbeon-forms/issues/7897)).
* **Control settings:** New `control_get_settings` and `control_set_settings` tools allow reading and updating specific control settings ([\#7900](https://github.com/orbeon/orbeon-forms/issues/7900)).
* **Container settings:** New `container_get_settings` and `container_set_settings` tools allow reading and updating section and grid settings ([\#7909](https://github.com/orbeon/orbeon-forms/issues/7909)).
* **Declarative custom parameters:** XBL components can now declaratively expose custom parameters to Form Builder and MCP tools ([\#7984](https://github.com/orbeon/orbeon-forms/issues/7984)).

### Landing page enhancements

* Added support for creating custom cards that link directly to `new` form pages ([\#7823](https://github.com/orbeon/orbeon-forms/issues/7823)).
* Added configurable sorting for cards displaying form data on the Landing page ([\#7978](https://github.com/orbeon/orbeon-forms/issues/7978)).

### Offline image transformations

Offline forms now support image transformations (such as resizing and compression) on client-side image attachments upon upload ([\#7859](https://github.com/orbeon/orbeon-forms/issues/7859)).

## Performance enhancements

* **PDF rendering performance:** Implemented CSS selector bucket indexing in the PDF generator and upgraded Apache PDFBox to improve rendering speed on complex forms ([\#7681](https://github.com/orbeon/orbeon-forms/issues/7681)).
* **Excel/XML import and export:** Improved performance of Excel export (specifically cell merge region handling and property templates) and added ID indexing for faster form definition processing during import/export ([\#7840](https://github.com/orbeon/orbeon-forms/issues/7840)).
* **Form cache updates and invalidation:** Refactored form definition cache updates and invalidations for better reliability and performance ([\#6914](https://github.com/orbeon/orbeon-forms/issues/6914), [\#7842](https://github.com/orbeon/orbeon-forms/issues/7842)).

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

### Authorizer URL must be absolute

In previous versions, the `oxf.page-flow.authorizer` property accepted an absolute path (such as `/orbeon-auth`), which was resolved relative to the current host.

Starting with this release, the authorizer property strictly requires an absolute URL with a scheme (e.g. `http://localhost:8080/orbeon-auth`). If the property contains a relative or scheme-less path, Orbeon Forms will log an error and deny access ([\#7957](https://github.com/orbeon/orbeon-forms/issues/7957)).

## Issues addressed

In this release, we have addressed over 60 issues, including:

* Security
    * Disallow `xxforms-upload-store` as an untrusted client event ([\#7975](https://github.com/orbeon/orbeon-forms/issues/7975))
    * Email: stricter validation of attachment URLs ([\#7970](https://github.com/orbeon/orbeon-forms/issues/7970))
    * Email: normalize line breaks in header values ([\#7969](https://github.com/orbeon/orbeon-forms/issues/7969))
    * Email: escape non-HTML values in HTML templates ([\#7971](https://github.com/orbeon/orbeon-forms/issues/7971))
    * Port fix for CVE-2022-23437 in Apache Xerces ([\#7972](https://github.com/orbeon/orbeon-forms/issues/7972))
    * Enhanced protocol filtering on `xf:output` ([\#7952](https://github.com/orbeon/orbeon-forms/issues/7952))
    * Restrict serving some assets ([\#7923](https://github.com/orbeon/orbeon-forms/issues/7923))
    * Improve sanitation of HTML attributes ([\#7930](https://github.com/orbeon/orbeon-forms/issues/7930))
    * Filter sensitive properties sent to the client / JS environment ([\#7931](https://github.com/orbeon/orbeon-forms/issues/7931))
    * Require absolute URL to the authorizer ([\#7957](https://github.com/orbeon/orbeon-forms/issues/7957))
    * Match service base URL at start of URL only, send token to internal requests only ([\#7944](https://github.com/orbeon/orbeon-forms/issues/7944))
    * Check user is authorized on form publish ([\#7990](https://github.com/orbeon/orbeon-forms/issues/7990))
    * Form Builder `/new` to return 403 on load if user is not authorized ([\#7997](https://github.com/orbeon/orbeon-forms/issues/7997))
    * Only the host app can establish the Form Runner session in embedding ([\#7948](https://github.com/orbeon/orbeon-forms/issues/7948))
    * Limit Ajax request body size ([\#7974](https://github.com/orbeon/orbeon-forms/issues/7974))
    * Cookie incorrectly forwarded to the persistence layer ([\#7066](https://github.com/orbeon/orbeon-forms/issues/7066))
    * Library updates (including Apache Xerces, Apache PDFBox, Bouncy Castle, AWS SDK, Netty, SLF4J, Log4j, and Jackson)
* Form Builder
    * Choices Editor: ability to copy/paste list of items to/from Excel ([\#7784](https://github.com/orbeon/orbeon-forms/issues/7784))
    * Add keyboard shortcut for the "Edit Choices" dialog ([\#7946](https://github.com/orbeon/orbeon-forms/issues/7946))
    * Edit Choices: default value not saved ([\#7958](https://github.com/orbeon/orbeon-forms/issues/7958))
    * Show section template names upon hover ([\#7777](https://github.com/orbeon/orbeon-forms/issues/7777))
    * Disable options which only apply to grids or sections ([\#7322](https://github.com/orbeon/orbeon-forms/issues/7322))
    * Always add spacer after grids in Form Builder ([\#7852](https://github.com/orbeon/orbeon-forms/issues/7852))
    * Only show terminal fields in the dropdown ([\#7888](https://github.com/orbeon/orbeon-forms/issues/7888))
    * Enforce required form title and description in Form Builder ([\#7916](https://github.com/orbeon/orbeon-forms/issues/7916))
    * Add dropdowns with search for application and form name ([\#7917](https://github.com/orbeon/orbeon-forms/issues/7917))
    * Form Settings -> Form Options: misaligned radio buttons ([\#7895](https://github.com/orbeon/orbeon-forms/issues/7895))
    * Form Builder toolbox drag and drop regression ([\#7768](https://github.com/orbeon/orbeon-forms/issues/7768))
    * When closing dialog, close help popovers inside the dialog ([\#7829](https://github.com/orbeon/orbeon-forms/issues/7829))
    * Keyboard shortcuts work erratically ([\#7954](https://github.com/orbeon/orbeon-forms/issues/7954))
    * Section template: section label is no longer copied ([\#7828](https://github.com/orbeon/orbeon-forms/issues/7828))
    * Action in section template to pass value to parameter ([\#7760](https://github.com/orbeon/orbeon-forms/issues/7760))
    * Don't lose subsections when manipulating containers ([\#7762](https://github.com/orbeon/orbeon-forms/issues/7762))
    * Fix XPath syntax error in Control Settings ([\#7780](https://github.com/orbeon/orbeon-forms/issues/7780))
    * Rejected file message to show in Form Builder's test mode ([\#7868](https://github.com/orbeon/orbeon-forms/issues/7868))
* Form Runner
    * Form Discovery API: API to return distinct application, form, and version values ([\#6573](https://github.com/orbeon/orbeon-forms/issues/6573))
    * Revision history: show workflow stage changes ([\#7911](https://github.com/orbeon/orbeon-forms/issues/7911))
    * `log()` process action ([\#7781](https://github.com/orbeon/orbeon-forms/issues/7781))
    * Wizard: option to hide the TOC ([\#7960](https://github.com/orbeon/orbeon-forms/issues/7960))
    * Wizard: separate TOC layout is incorrect ([\#7959](https://github.com/orbeon/orbeon-forms/issues/7959))
    * `fr-wizard-page` didn't update anymore ([\#7613](https://github.com/orbeon/orbeon-forms/issues/7613))
    * Landing page: support linking to `new` pages and card sorting ([\#7823](https://github.com/orbeon/orbeon-forms/issues/7823), [\#7978](https://github.com/orbeon/orbeon-forms/issues/7978))
    * Landing page: Application and Form Name incorrectly pushed down ([\#7937](https://github.com/orbeon/orbeon-forms/issues/7937))
    * Landing page: add thumbnail to the default Form Builder card ([\#7956](https://github.com/orbeon/orbeon-forms/issues/7956))
    * Forms page: partial match checkbox / hint for exact match checkbox ([\#7928](https://github.com/orbeon/orbeon-forms/issues/7928), [\#7758](https://github.com/orbeon/orbeon-forms/issues/7758))
    * Forms page: width of the Created and Modified search dropdowns ([\#7936](https://github.com/orbeon/orbeon-forms/issues/7936))
    * Forms Admin page: Reindex progress bar and status message not shown ([\#7795](https://github.com/orbeon/orbeon-forms/issues/7795))
    * Summary page: cleanup `summary-metadata-columns` on open ([\#7926](https://github.com/orbeon/orbeon-forms/issues/7926))
    * Custom modes: propagate request parameters ([\#7803](https://github.com/orbeon/orbeon-forms/issues/7803))
    * "Deleting all records" gets into an infinite loop ([\#7961](https://github.com/orbeon/orbeon-forms/issues/7961))
    * Initial form data from service now uses simple data migration ([\#7769](https://github.com/orbeon/orbeon-forms/issues/7769))
    * For non-relevant controls, get value from data in emails ([\#7773](https://github.com/orbeon/orbeon-forms/issues/7773))
    * `email()` can cause unneeded PDF generation ([\#7872](https://github.com/orbeon/orbeon-forms/issues/7872))
    * Email: TIFF attachments no longer sent since XPL/XSL-to-Scala refactoring ([\#7891](https://github.com/orbeon/orbeon-forms/issues/7891))
    * New property to configure SMTP HELO/EHLO name ([\#7890](https://github.com/orbeon/orbeon-forms/issues/7890))
    * "Calculations in Read-Only Modes (Review, PDF)" set to Disable is ignored on Review pages and in PDF files ([\#7921](https://github.com/orbeon/orbeon-forms/issues/7921))
    * Rich text does not show in PDF ([\#7945](https://github.com/orbeon/orbeon-forms/issues/7945))
    * PDF/A output is not conformant ([\#7998](https://github.com/orbeon/orbeon-forms/issues/7998))
    * Improve PDF image rotation handling ([\#7856](https://github.com/orbeon/orbeon-forms/issues/7856))
    * `send(content = "pdf-url")` to produce dynamic resource URL ([\#7976](https://github.com/orbeon/orbeon-forms/issues/7976))
    * `send()`: URL credentials are no longer used ([\#7794](https://github.com/orbeon/orbeon-forms/issues/7794))
    * Improve layout of buttons on narrow viewports ([\#7896](https://github.com/orbeon/orbeon-forms/issues/7896))
    * Hidden fields error summary: errors without underline, number aligned ([\#7818](https://github.com/orbeon/orbeon-forms/issues/7818))
    * Fix message format syntax in French and Catalan validation dialog message ([\#6184](https://github.com/orbeon/orbeon-forms/issues/6184))
    * Property to define custom favicon: `oxf.fr.favicon.uri` ([\#5567](https://github.com/orbeon/orbeon-forms/issues/5567))
    * Form Runner service calls: enforce stateless request handling and prevent `Set-Cookie` ([\#7929](https://github.com/orbeon/orbeon-forms/issues/7929))
* XBL Components & Form Controls
    * Provide way to override resources defined in XBL ([\#3235](https://github.com/orbeon/orbeon-forms/issues/3235))
    * `fr:datetime` control shows different date format in edit and view modes ([\#7850](https://github.com/orbeon/orbeon-forms/issues/7850))
    * Dates to Exclude can be one-day off ([\#7833](https://github.com/orbeon/orbeon-forms/issues/7833))
    * Attachment: download button shows when "Allow download" is unchecked ([\#7791](https://github.com/orbeon/orbeon-forms/issues/7791))
    * Attachment upload: support showing multiple inline alert messages on rejection ([\#7792](https://github.com/orbeon/orbeon-forms/issues/7792))
    * Attachment upload: drop area still grey after pointer leaves ([\#7824](https://github.com/orbeon/orbeon-forms/issues/7824))
    * Help tooltip position for textarea when page is scrolled down ([\#7810](https://github.com/orbeon/orbeon-forms/issues/7810))
    * Label Formatted Text Area `div` with `role="textbox"` for accessibility ([\#7812](https://github.com/orbeon/orbeon-forms/issues/7812))
    * `fr|checkbox-input` doesn't support hint ([\#7962](https://github.com/orbeon/orbeon-forms/issues/7962))
    * LEI and IBAN validation: reject 00, 01, 99 as check-digit pairs ([\#7775](https://github.com/orbeon/orbeon-forms/issues/7775))
    * Header of repeated grid with no iterations to show ([\#7759](https://github.com/orbeon/orbeon-forms/issues/7759))
    * Changing `oxf.xforms.xbl.library` value can break repeated grids ([\#7793](https://github.com/orbeon/orbeon-forms/issues/7793))
* Model Context Protocol (MCP) Server
    * Expose MCP endpoint as public method ([\#7898](https://github.com/orbeon/orbeon-forms/issues/7898))
    * New `form_attach_pdf_template` tool ([\#7897](https://github.com/orbeon/orbeon-forms/issues/7897))
    * `control_get_settings` and `control_set_settings` tools ([\#7900](https://github.com/orbeon/orbeon-forms/issues/7900))
    * `container_get_settings` and `container_set_settings` tools ([\#7909](https://github.com/orbeon/orbeon-forms/issues/7909))
    * XBL: declarative custom parameters support for control settings ([\#7984](https://github.com/orbeon/orbeon-forms/issues/7984))
    * MCP: session error with `form_new` ([\#7953](https://github.com/orbeon/orbeon-forms/issues/7953))
    * MCP default token expiration and web.xml path configuration ([\#7001](https://github.com/orbeon/orbeon-forms/issues/7001), [\#7701](https://github.com/orbeon/orbeon-forms/issues/7701))
* XForms Engine
    * Couldn't cast `XFormsVariableControl` to `XFormsEventHandler` ([\#7880](https://github.com/orbeon/orbeon-forms/issues/7880))
    * Don't resend `<xxf:submission>` on subsequent Ajax requests ([\#7447](https://github.com/orbeon/orbeon-forms/issues/7447))
    * XPath error seen when reporting XPath errors ([\#7853](https://github.com/orbeon/orbeon-forms/issues/7853))
* Offline
    * Offline: support upload image transformations ([\#7859](https://github.com/orbeon/orbeon-forms/issues/7859))
    * `fr|databound-select1-search` XPath expression fails in JS environment ([\#7849](https://github.com/orbeon/orbeon-forms/issues/7849))
    * Two XPath expressions crash offline ([\#7934](https://github.com/orbeon/orbeon-forms/issues/7934))

You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page, or use our Docker images.

Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon), [Bluesky](https://bsky.app/profile/orbeon.bsky.social), or the [forum](https://groups.google.com/g/orbeon).

We hope you enjoy this release!
