# Orbeon Forms 2024.1.3

__Tuesday, October 21, 2025__

Today we released Orbeon Forms 2024.1.3! This maintenance release contains bug-fixes and a few new features and is recommended for all users of:

- [Orbeon Forms 2024.1.2 PE](orbeon-forms-2024.1.2.md)
- [Orbeon Forms 2024.1.1 PE](orbeon-forms-2024.1.1.md)
- [Orbeon Forms 2024.1 PE](orbeon-forms-2024.1.md)

## New features

### Form Builder view mode

Form Builder now supports a view mode, which allows users to view form definitions in a read-only manner. In this mode:

- the user cannot make any changes to the form
- dialogs can be open, but they are not editable
- the user cannot save or publish the form 

You access this mode using the "Review" button from the Form Builder Summary page.

![Form Builder view mode](/form-builder/images/form-builder-view-mode.webp)

### Option to store attachments in S3

You can configure Form Runner to store attachments in S3 in addition to the database or the filesystem. This is useful for larger attachments.

For more, see the [documentation](/configuration/properties/persistence.md#storing-attachments-in-the-filesystem-or-on-s3).

### Ability to change the encryption key

It is now possible to change the encryption key for encryption at rest. This will cause reencryption of all the data.

For more, see the [documentation](/form-builder/field-level-encryption.md#changing-the-encryption-password).

### Tab duplication detection

When duplicating a browser tab, Orbeon Forms now warns the user to close that tab.

For more, see the [blog post](https://www.orbeon.com/2025/09/tab-duplication-detection).

### Improved landing page configuration

You can now specify custom cards on the landing page for:

- published form definitions in a particular app
- form data for a particular form

This including specifying titles, descriptions, and icons for those cards.

In addition, if the configuration is blank, the landing page is entirely disabled.

For more, see the [documentation](/form-runner/feature/landing-page.md#configuration-properties).

[//]: # (### Paging of large sections)

[//]: # ()
[//]: # (You can now enable paging for large repeated sections. )
[//]: # (- Repeated grids/sections: support paging &#40;[#4137]&#40;https://github.com/orbeon/orbeon-forms/issues/4137&#41;&#41;)
[//]: # (- JavaScript API for repeated section paging &#40;[#7183]&#40;https://github.com/orbeon/orbeon-forms/issues/7183&#41;&#41;)
[//]: # (- Improve `wizard.focus&#40;&#41;` to handle pager &#40;[#7202]&#40;https://github.com/orbeon/orbeon-forms/issues/7202&#41;&#41;)

### Other new features

[//]: # (- For [#7058]&#40;https://github.com/orbeon/orbeon-forms/issues/7058&#41;: Form Builder UI)

- New keyboard shortcut allow you to move a grid line up or down in Form Builder: <kbd>⌃⇧↑</kbd> and <kbd>⌃⇧↓</kbd>.
- You can now automatically open search options in the Forms/Admin pages ([doc](/form-runner/feature/published-forms-page.md#search-options-opened-on-load)).
- The WebP image format is now supported in automatic PDF production.

## New demo form

We have added new demo forms:
 
- Health History ([builder](https://demo.orbeon.com/demo/fr/orbeon/builder/edit/12553579e52f1008617b8d7a000e659db2b15133), [runner](https://demo.orbeon.com/demo/fr/orbeon/health-history/edit/13e1a4f2a555d31326d2b3bb041b11f4d8f95539))
- Medical Record Amendment ([builder](https://demo.orbeon.com/demo/fr/orbeon/builder/edit/7f9b4de690effbf69a5d3c5207b9c880eaa23524), [runner](https://demo.orbeon.com/demo/fr/orbeon/medical-record-amendment/edit/8c7a175dd8ada3e5292b4e993b230fcca1eee12b))

## Issues addressed

In this release, we have addressed many issues, including:

- Form Builder
    - Crash when importing section template with AVT for custom CSS classes ([#7291](https://github.com/orbeon/orbeon-forms/issues/7291))
    - In Form Builder, when the main area is scrolled down, if clicking on the dropdown with search, the combobox shows too high ([#7254](https://github.com/orbeon/orbeon-forms/issues/7254))
    - "General Settings" has too much space on top ([#7226](https://github.com/orbeon/orbeon-forms/issues/7226))
    - Form Builder: cmd-enter doesn't commit rich text ([#7225](https://github.com/orbeon/orbeon-forms/issues/7225))
    - Control Settings: "Restrict to role" checkbox should make parent option read-only ([#7159](https://github.com/orbeon/orbeon-forms/issues/7159))
    - Template inadvertently selected when creating new form ([#7140](https://github.com/orbeon/orbeon-forms/issues/7140))
    - Dialog title bar gets partially cut with short viewport ([#7134](https://github.com/orbeon/orbeon-forms/issues/7134))
    - XPath error after merging US Address section template with prefix or suffix ([#6937](https://github.com/orbeon/orbeon-forms/issues/6937))
    - Form Builder summary: force exact app/form-match and hide filter when app dropdown is used ([#7249](https://github.com/orbeon/orbeon-forms/issues/7249))
    - In Control Settings dialog, ensure long labels don't overlap the language selector ([#7247](https://github.com/orbeon/orbeon-forms/issues/7247))
    - In Control Settings, a suggestion for a control name can show over other controls if the label is long ([#7245](https://github.com/orbeon/orbeon-forms/issues/7245))
    - Control Names dialog content can overflow ([#7181](https://github.com/orbeon/orbeon-forms/issues/7181))
    - Formulas console no longer shows errors ([#7176](https://github.com/orbeon/orbeon-forms/issues/7176))
    - Form Builder makes a request to /not-found when loaded ([#7130](https://github.com/orbeon/orbeon-forms/issues/7130))
    - Keyboard shortcuts to move to the previous/next control in the control settings dialog ([#7129](https://github.com/orbeon/orbeon-forms/issues/7129))
    - Form Builder label editor should have a name or id ([#7121](https://github.com/orbeon/orbeon-forms/issues/7121))
    - Drag and drop doesn't work in Form Builder when embedded via JavaScript Embedding API ([#7118](https://github.com/orbeon/orbeon-forms/issues/7118))
    - Form titles containing commas are truncated in Form Builder Forms landing page card ([#7117](https://github.com/orbeon/orbeon-forms/issues/7117))
    - Form Builder unresponsive after Esc in Open selection Formula ([#7116](https://github.com/orbeon/orbeon-forms/issues/7116))
    - Controls in repeated section disappear after section unmarked as repeated ([#7112](https://github.com/orbeon/orbeon-forms/issues/7112))
    - After reordering controls in a repeated grid, the order of the binds and template elements remains unchanged ([#7312](https://github.com/orbeon/orbeon-forms/issues/7312))
- Form Runner
    - Help popover for calculated value appears too far from text when border is disabled ([#7128](https://github.com/orbeon/orbeon-forms/issues/7128))
    - Help popover shows over calendar icon for natural width date ([#7127](https://github.com/orbeon/orbeon-forms/issues/7127))
    - Help popover shows below the top navigation bar ([#7126](https://github.com/orbeon/orbeon-forms/issues/7126))
    - Import: dynamic default values must not overwrite `POST`ed data in `edit` mode ([#7236](https://github.com/orbeon/orbeon-forms/issues/7236))
    - Choices filter doesn't apply if items come from an `itemsetid` ([#7285](https://github.com/orbeon/orbeon-forms/issues/7285))
    - Don't show Form Builder Forms in Quick Links tile and Form Builder Forms tile to user who can't access Form Builder ([#7248](https://github.com/orbeon/orbeon-forms/issues/7248))
    - Forms Admin not to show remote operations when we don't have any remote set up ([#7304](https://github.com/orbeon/orbeon-forms/issues/7304))
    - Complete resources in Italian and German for Form Runner and Form Builder ([#7292](https://github.com/orbeon/orbeon-forms/issues/7292))
    - `HEAD` requests on data when `POST`ing data to `/edit/1234` ([#7271](https://github.com/orbeon/orbeon-forms/issues/7271))
    - Improve file type label derived from MIME type ([#7268](https://github.com/orbeon/orbeon-forms/issues/7268))
    - Undeclared variable in XPath expression: $autosave-now ([#7257](https://github.com/orbeon/orbeon-forms/issues/7257))
    - In the 4.0.0 format, annotate with `fr:relevant="false"` children of non-relevant grid elements ([#7223](https://github.com/orbeon/orbeon-forms/issues/7223))
    - Form Data Attachments API adds spurious section elements, nested `<attachment>` ([#7215](https://github.com/orbeon/orbeon-forms/issues/7215))
    - Background API incorrectly returning HTML ([#7158](https://github.com/orbeon/orbeon-forms/issues/7158))
    - First validation always picked if uses `fr:param` ([#7152](https://github.com/orbeon/orbeon-forms/issues/7152))
    - User without create but with update permission because owner can't attach new files to existing data ([#7145](https://github.com/orbeon/orbeon-forms/issues/7145))
    - "Can't set request entity" exception when using attachments provider with custom implementation of the persistence API ([#7141](https://github.com/orbeon/orbeon-forms/issues/7141))
    - Empty cells in summary page are only partially clickable ([#7097](https://github.com/orbeon/orbeon-forms/issues/7097))
    - Exporting or purging a form with thousands of form data entries from the UI doesn't work ([#7088](https://github.com/orbeon/orbeon-forms/issues/7088))
    - Draft attachments are not deleted when draft is saved ([#7049](https://github.com/orbeon/orbeon-forms/issues/7049))
    - Loading indicator not showing anymore after file upload ([#7244](https://github.com/orbeon/orbeon-forms/issues/7244))
    - Don't use 503 responses to tell the client to retry ([#7241](https://github.com/orbeon/orbeon-forms/issues/7241))
    - External `POST` to `edit` page throws error ([#7240](https://github.com/orbeon/orbeon-forms/issues/7240))
    - Landing: better alignment of columns ([#7239](https://github.com/orbeon/orbeon-forms/issues/7239))
    - Label on right of grid can overflow grid cell ([#7238](https://github.com/orbeon/orbeon-forms/issues/7238))
    - Doing a field search with containing uppercases will never return any data on Oracle and DB2 ([#7231](https://github.com/orbeon/orbeon-forms/issues/7231))
    - Don't swallow exceptions during JS app init ([#7230](https://github.com/orbeon/orbeon-forms/issues/7230))
    - Pager: add page selection dropdown/field ([#7200](https://github.com/orbeon/orbeon-forms/issues/7200))
    - `fr:is-design-time()` fails outside of Form Runner ([#7191](https://github.com/orbeon/orbeon-forms/issues/7191))
    - Newline in error summary message list if message is long ([#7187](https://github.com/orbeon/orbeon-forms/issues/7187))
    - `ConnectionContext` not passed with Excel import when passing `document-id` ([#7175](https://github.com/orbeon/orbeon-forms/issues/7175))
    - Form Runner: `page-public-methods` prevent use of authorizer ([#7171](https://github.com/orbeon/orbeon-forms/issues/7171))
    - Section Next/Prev buttons are always disabled ([#7169](https://github.com/orbeon/orbeon-forms/issues/7169))
    - Robust Singleton form constraint checking for databases other than Oracle ([#7164](https://github.com/orbeon/orbeon-forms/issues/7164))
    - Help propagate `fr:static-readonly`/`fr:pdf-template` inside section ([#7114](https://github.com/orbeon/orbeon-forms/issues/7114))
    - `wizard.focus()` fails if passed repeat indexes ([#7216](https://github.com/orbeon/orbeon-forms/issues/7216))
- XBL Components
    - Minor behavior improvements to `fr:friendly-captcha` ([#7286](https://github.com/orbeon/orbeon-forms/issues/7286))
    - File attachment control no longer allows selection after language change ([#7203](https://github.com/orbeon/orbeon-forms/issues/7203))
    - Hint doesn't work in Dynamic Dropdown with Search ([#7133](https://github.com/orbeon/orbeon-forms/issues/7133))
    - When the date format uses leading zeros, allow dates to be entered without separators ([#7122](https://github.com/orbeon/orbeon-forms/issues/7122))
- PDF Support
    - Incorrect layout of grid with leading empty cells ([#7207](https://github.com/orbeon/orbeon-forms/issues/7207)) 
    - "Dropdown with other" doesn't look good in the PDF ([#6911](https://github.com/orbeon/orbeon-forms/issues/6911))
    - Incorrect Unicode diacritics output in PDF file ([#7214](https://github.com/orbeon/orbeon-forms/issues/7214))
    - Don't set `max-height` on PDF logo ([#7255](https://github.com/orbeon/orbeon-forms/issues/7255))
- Embedding and portlet support
    - Portlet preferences cause `java.lang.VerifyError` ([#7272](https://github.com/orbeon/orbeon-forms/issues/7272))
    - Proxy Portlet: update web.xml and remove log4j jars ([#7270](https://github.com/orbeon/orbeon-forms/issues/7270))
    - Proxy Portlet: filter out non-configured paths ([#7227](https://github.com/orbeon/orbeon-forms/issues/7227))
    - Java embedding: output `Writer` must not be closed ([#7194](https://github.com/orbeon/orbeon-forms/issues/7194))
- XForms
    - `iframe` in custom dialog initially loads non-existing URL ([#7267](https://github.com/orbeon/orbeon-forms/issues/7267))
    - `xxf:lang()` crashes ([#7209](https://github.com/orbeon/orbeon-forms/issues/7209))
- Offline
    - Import and export buttons must not show in the JS environment ([#7204](https://github.com/orbeon/orbeon-forms/issues/7204))
    - Offline: image attachment show content in new form session ([#7149](https://github.com/orbeon/orbeon-forms/issues/7149))
    - Test Offline: if form languages does not include the default language, labels of buttons are missing ([#7136](https://github.com/orbeon/orbeon-forms/issues/7136))
    - Offline: separate property to configure autosave ([#7182](https://github.com/orbeon/orbeon-forms/issues/7182))
- Distribution
    - Orbeon Forms application restarts regularly in WildFly Docker container ([#7217](https://github.com/orbeon/orbeon-forms/issues/7217))
    - Third-party library upgrades
- Other
    - Running `FormRunnerApiClientTest`, getting "not a valid selector" SyntaxError ([#7250](https://github.com/orbeon/orbeon-forms/issues/7250))
    - Native function to determine if there are any open dialogs ([#7179](https://github.com/orbeon/orbeon-forms/issues/7179))

You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page, or use our Docker images.

Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon), [Bluesky](https://bsky.app/profile/orbeon.bsky.social), or the [forum](https://groups.google.com/g/orbeon).

We hope you enjoy this release!
