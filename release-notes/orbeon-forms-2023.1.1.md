# Orbeon Forms 2023.1.1

__Tuesday, March 19, 2024__

Today we released Orbeon Forms 2023.1.1 PE! This maintenance release contains bug-fixes and minor new features and is recommended for all users of:

- [Orbeon Forms 2023.1 PE](orbeon-forms-2023.1.md)

This release addresses the following issues since [Orbeon Forms 2023.1 PE](orbeon-forms-2023.1.md):

- Security
    - Use 16 byte (128 bits) instead of 8 byte (64 bits) salt for FIPS compliance ([\#6146](https://github.com/orbeon/orbeon-forms/issues/6146))
    - Make XML entity expansion configurable ([\#6135](https://github.com/orbeon/orbeon-forms/issues/6135))
- Form Builder
    - Copy section settings when inserting section template ([\#3495](https://github.com/orbeon/orbeon-forms/issues/3495))
    - Form Builder UI for enabling the attachment of the XML to the email sent ([\#6120](https://github.com/orbeon/orbeon-forms/issues/6120))
    - FB: "Add Language" dialog doesn't get positioned ([\#5569](https://github.com/orbeon/orbeon-forms/issues/5569))
    - Choices Editor reads/updates the incorrect repeated control ([\#6182](https://github.com/orbeon/orbeon-forms/issues/6182))
    - Null context when adding `save` to Form Builder's `publish` ([\#6148](https://github.com/orbeon/orbeon-forms/issues/6148))
    - References to `$fr-mode` in formulas now considered invalid ([\#6220](https://github.com/orbeon/orbeon-forms/issues/6220))
- Form Runner
    - Dynamic dropdown control 
        - Clear not accessible in dynamic dropdown with search ([\#6159](https://github.com/orbeon/orbeon-forms/issues/6159))
        - Custom value of open selection gets reset when resource changes ([\#6177](https://github.com/orbeon/orbeon-forms/issues/6177))
        - Dynamic dropdown with search using variable is `resource` is empty ([\#6140](https://github.com/orbeon/orbeon-forms/issues/6140))
        - Dynamic dropdown ability to auto-selecting only choice returned by the service ([\#5999](https://github.com/orbeon/orbeon-forms/issues/5999))
        - Dynamic dropdown with search always shows empty after loading data ([\#6161](https://github.com/orbeon/orbeon-forms/issues/6161))
        - Dynamic dropdown, with service performing search, with auto-select, when unique choice, clearing value re-selects it ([\#6158](https://github.com/orbeon/orbeon-forms/issues/6158))
        - Dynamic Dropdown: show error if loading data failed ([\#6172](https://github.com/orbeon/orbeon-forms/issues/6172))
        - First open value not saved if service does search in dynamic dropdown ([\#6178](https://github.com/orbeon/orbeon-forms/issues/6178))
    - Export and Purge
        - Export: forms with "All Forms" are not exported ([\#6211](https://github.com/orbeon/orbeon-forms/issues/6211))
        - Excel export: mismatch of fields in repeated sections or grids ([\#6128](https://github.com/orbeon/orbeon-forms/issues/6128))
        - Purge: should only support `lt` but not `gte` date constraints ([\#6210](https://github.com/orbeon/orbeon-forms/issues/6210))
        - Purge: possible issue with attachments when using date ranges ([\#6207](https://github.com/orbeon/orbeon-forms/issues/6207))
        - FormRunnerPersistence.getProviders returns "mysql" even when non-MySQL provider is configured ([\#6228](https://github.com/orbeon/orbeon-forms/issues/6228))
    - Offline
        - Offline: attaching file causes `GET` on `SubmissionProvider` ([\#6153](https://github.com/orbeon/orbeon-forms/issues/6153))
        - Offline: "Upload complete" message is not clear ([\#6171](https://github.com/orbeon/orbeon-forms/issues/6171))
        - Offline: exception when attaching image ([\#6166](https://github.com/orbeon/orbeon-forms/issues/6166))
        - Offline: multiple file attachments show the same filename ([\#6167](https://github.com/orbeon/orbeon-forms/issues/6167))
        - Offline: Image Attachment/Attachment to show/download content after attaching ([\#6165](https://github.com/orbeon/orbeon-forms/issues/6165))
        - Offline: option to have CSS produce shorter or relative paths ([\#6170](https://github.com/orbeon/orbeon-forms/issues/6170))
        - Offline: `fr|databound-select1[appearance ~= search]` doesn't work ([\#6201](https://github.com/orbeon/orbeon-forms/issues/6201))
    - Other
        - XML Schema validation: extra space in `filename` attribute name ([\#6134](https://github.com/orbeon/orbeon-forms/issues/6134))
        - Processes: support whitespace between action and parameters list ([\#6138](https://github.com/orbeon/orbeon-forms/issues/6138))
        - Loading indicator may never hide when `delay-before-display-loading > 0` ([\#6144](https://github.com/orbeon/orbeon-forms/issues/6144))
        - Email parameters lost in 2022 migration, not read from 2023 form definition ([\#6149](https://github.com/orbeon/orbeon-forms/issues/6149))
        - Migration code to fix forms incorrectly migrated `AllControlValuesParam(all)` ([\#6127](https://github.com/orbeon/orbeon-forms/issues/6127))
        - "_.isUndefined is not a function" when testing with Subject7 ([\#6126](https://github.com/orbeon/orbeon-forms/issues/6126))
        - TOC doesn't scroll form to repeated subsection ([\#6151](https://github.com/orbeon/orbeon-forms/issues/6151))
        - Warning about incomplete database config ([\#6124](https://github.com/orbeon/orbeon-forms/issues/6124))
        - Add service provider to help with connection context passing ([\#6157](https://github.com/orbeon/orbeon-forms/issues/6157))
        - Form with asynchronous submission error crashes ([\#6176](https://github.com/orbeon/orbeon-forms/issues/6176))
        - Potentially inconsistent format of date with native date picker ([\#6193](https://github.com/orbeon/orbeon-forms/issues/6193))
        - `fr:attachment`: use `download` attribute ([\#6198](https://github.com/orbeon/orbeon-forms/issues/6198))
        - `name` and `controlName` for email parameters lost during conversion ([\#6202](https://github.com/orbeon/orbeon-forms/issues/6202))
        - Improved PDF font fallback ([\#6196](https://github.com/orbeon/orbeon-forms/issues/6196))
        - JavaScript Embedding API: no error when trying to embed a non-existing form ([\#6195](https://github.com/orbeon/orbeon-forms/issues/6195))
        - Image annotation tooltip "stroke colorr" with two "r" ([\#6208](https://github.com/orbeon/orbeon-forms/issues/6208))
        - `send(headers = "X-Foo: bar")` causes XPath error ([\#6212](https://github.com/orbeon/orbeon-forms/issues/6212))
        - Support serving files with the `xml` extension ([\#6214](https://github.com/orbeon/orbeon-forms/issues/6214))
        - NPE if no cookies are sent ([\#6197](https://github.com/orbeon/orbeon-forms/issues/6197))
        - Portlet: error when attempting to read autosaved draft ([\#6139](https://github.com/orbeon/orbeon-forms/issues/6139))
        - Embedding JAR doesn't include `JavaxServletFilter`/`JakartaServletFilter` ([\#6192](https://github.com/orbeon/orbeon-forms/issues/6192))
        - Replace SimpleCaptcha by Kaptcha ([\#6215](https://github.com/orbeon/orbeon-forms/issues/6215))
        - Portlet: error when attempting to read autosaved draft ([\#6139](https://github.com/orbeon/orbeon-forms/issues/6139))
- Platform
    - Generate proper SQLite demo database during build step ([\#6104](https://github.com/orbeon/orbeon-forms/issues/6104))
    - NPE with `JavaxServletRegistration.addMapping()` ([\#6123](https://github.com/orbeon/orbeon-forms/issues/6123))
    - Fix async tests that sometimes fail in build ([\#6118](https://github.com/orbeon/orbeon-forms/issues/6118))
    - Async tests fail randomly ([\#6125](https://github.com/orbeon/orbeon-forms/issues/6125))
    - Sharing issue with global indented loggers ([\#179](https://github.com/orbeon/orbeon-forms/issues/179))
    - Data sources are available from `java:comp/env` in WildFly ([\#6094](https://github.com/orbeon/orbeon-forms/issues/6094))
    - PurgeTest fails ([\#6223](https://github.com/orbeon/orbeon-forms/issues/6223))
    - Tomcat shows `cats.effect` errors upon shutdown ([\#6089](https://github.com/orbeon/orbeon-forms/issues/6089))
- XForms
    - `<xf:action type="javascript">` must omit elements without DOM representation ([\#6216](https://github.com/orbeon/orbeon-forms/issues/6216))
    - `AjaxEventQueue` can get blocked when a response arrives for a form that has been removed ([\#6200](https://github.com/orbeon/orbeon-forms/issues/6200))
    - `Document.setValue()` must not return a Scala `Future` ([\#6129](https://github.com/orbeon/orbeon-forms/issues/6129))

You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page.

Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon), [Bluesky](https://bsky.app/profile/orbeon.bsky.social), or the [forum](https://groups.google.com/g/orbeon).

We hope you enjoy this release!

## Compatibility notes

### XML entity expansion

Orbeon Forms has a new global setting to control (internal) XML entity expansion. Previously, XML entity expansion, including character entities, was enabled but subject to a limit. Since this version, you can configure XML entity expansion. By default, for security reasons, and since entities are rarely used, this is set to 0. To restore the previous behavior, set this property to a positive number:

```xml
<property
    as="xs:integer"
    name="oxf.xml-parsing.entity-expansion-limit"
    value="0"/>
```

See also [XML entity expansion](/configuration/properties/general.md#xml-entity-expansion).