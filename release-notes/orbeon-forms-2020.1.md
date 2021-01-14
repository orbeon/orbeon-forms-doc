# Orbeon Forms 2020.1

__Wednesday, December 30, 2020__

Today we released Orbeon Forms 2020.1! This release introduces new features and bug-fixes.

## Notable features and enhancement

### Improvements to attachment controls

This version of Orbeon Forms introduces a new *Multiple File Attachments* control, which includes reordering attached files, a localized "Attached Files" buttons, and more. The existing *File Attachment* control also got updated and is now called *Single File Attachment*.

![The new *Multiple File Attachments* control](/form-runner/component/images/xbl-attachment-multiple.png)

See the [blog post](https://blog.orbeon.com/2020/05/the-new-multiple-file-attachments.html) for details.

In addition, you can now control whether downloading an attached file is allowed or not.

![Allow download option](/form-runner/component/images/xbl-attachment-control-settings.png)

See [the documentation](/form-runner/component/attachment.md) for details. 
 
### Copy and paste between forms in Form Builder

Before Orbeon Forms 2020.1, the cut/copy/paste operations were restricted to the currently running instance of Form Builder. This version of Orbeon Forms allows these operations between forms belonging to the same *user session*.

See the [blog post](https://blog.orbeon.com/2020/06/copying-and-pasting-across-forms.html) for details.
  
### JavaScript embedding

This version of Orbeon Forms introduces a JavaScript embedding API. As the name implies, this new API allows you to load an Orbeon form directly using JavaScript calls. This way of embedding forms is provided in addition to the existing Java API.

See the [blog post](https://blog.orbeon.com/2020/07/introducing-javascript-embedding-api.html) and [documentation](/form-runner/link-embed/javascript-api.md) for details.
  
### Basic Form Builder keyboard shortcuts

Form Builder now supports basic keyboard shortcuts, for the following:

- [Cut, copy, and paste](/form-builder/cut-copy-paste.md#keyboard-shortcuts)
- [Save](/form-builder/form-editor.md#keyboard-shortcuts)
- [Undo and redo](/form-builder/undo-redo.md#keyboard-shortcuts)

In the future, we plan to add more keyboard shortcuts.

### Workflow

This release takes baby steps towards supporting *workflow* features.

The concept of *stage* is introduced at the database level and can be controlled with the following:

- The [`set-workflow-stage` action](/form-runner/advanced/buttons-and-processes/actions-form-runner.md#set-workflow-stage) allows you to set a specific workflow stage.
- The [`fr:workflow-stage-value()`](/xforms/xpath/extension-form-runner.md#fr-workflow-stage-value) function allows you to read the current workflow stage.

In addition, you can now set an entire form as readonly based on a formula. This can be used in conjunction with the `fr:workflow-stage-value()` function, for example, to make the form readonly in certain conditions.

See the [documentation](/form-builder/form-settings.md#formula) for details.

### Automatic renaming of actions

When renaming an HTTP or database service, all actions using those services are automatically updated. If you attempt to delete a service, you are informed in case any actions are using that service.

This is in addition to the [existing feature](/form-builder/formulas.md#renaming-of-controls-and-formulas) whereby when a control or section or grid is renamed, dependent formulas which use the notation `$foo` (where `foo` is the control name) are automatically updated.

### Rich text form description

The form description now supports rich text.

![Form description with rich text](/form-builder/images/form-settings-general-html.png)

See the [documentation](/form-builder/form-settings.md) for details.

## Other features and enhancements

Including the features and enhancements above, we [closed over 180 issues](https://github.com/orbeon/orbeon-forms/issues?q=is%3Aissue+is%3Aclosed+project%3Aorbeon%2Forbeon-forms%2F20) since [Orbeon Forms 2019.2](orbeon-forms-2019.2.md), including the following.

### Form Builder and Form Runner

- The [Actions Editor](/form-builder/actions.md) supports setting a hint for control choices. ([\#4566](https://github.com/orbeon/orbeon-forms/issues/4566))
- Emails can include a link back to the form. ([\#4461](https://github.com/orbeon/orbeon-forms/issues/4461)) ([doc](/form-builder/advanced/template-syntax.md#links))
- Emails support `Reply-To`. ([\#4480](https://github.com/orbeon/orbeon-forms/issues/4480)) ([doc](/form-builder/control-settings.md#email-options))
- Some math functions are now exposed in formulas. ([\#4508](https://github.com/orbeon/orbeon-forms/issues/4508)) ([doc](/xforms/xpath/standard-functions.md#xpath-3-0-functions))
- We improved the choices for the appearance of Calculated Values. ([\#4531](https://github.com/orbeon/orbeon-forms/issues/4531))
- We added a checkbox to mark the service response as binary. ([\#4280](https://github.com/orbeon/orbeon-forms/issues/4280)) ([doc](/form-builder/advanced/services-and-actions/http-services.md#advanced-parameters))
- We automatically migrate older `fr:autocomplete` uses. ([\#4242](https://github.com/orbeon/orbeon-forms/issues/4242))

### Form Runner

- We've made some improvements in the SQL Orbeon Forms sends to your relational database, improving performance in high throughput environments, in particular when using MySQL.
- Rotating image attachments in PDF files to match the orientation indication present in images taken on mobile phones. ([\#4442](https://github.com/orbeon/orbeon-forms/issues/4442))
- Allow users to view form data if they can't edit it because of a lease. ([blog](https://blog.orbeon.com/2020/07/allowing-users-to-view-form-data-if.html))
- Control when and where the captcha is shown. ([\#4651](https://github.com/orbeon/orbeon-forms/issues/4651)) ([doc](/configuration/properties/form-runner-detail-page.md#captcha))
- Allow the Summary page to show data across all versions. ([\#4699](https://github.com/orbeon/orbeon-forms/issues/4699))
- Structured search on Summary page supports multiple selection controls. ([\#4479](https://github.com/orbeon/orbeon-forms/issues/4479))
- Action to clear a control, including attachment controls ([\#4427](https://github.com/orbeon/orbeon-forms/issues/4427)) ([doc](/form-builder/advanced/services-and-actions/actions-syntax.md#clearing-the-value-of-a-control))

### Accessibility

- Voice Over in Safari no longer reads the required "star". ([\#4628](https://github.com/orbeon/orbeon-forms/issues/4628))
- The Wizard table of contents (TOC) is first in the markup for accessibility. ([\#4211](https://github.com/orbeon/orbeon-forms/issues/4211))
- Readonly input fields are now focusable. ([\#3816](https://github.com/orbeon/orbeon-forms/issues/3816))
- Labels for dropdowns "with search" are tied to the dropdown. ([\#4713](https://github.com/orbeon/orbeon-forms/issues/4713))
- Link Buttons (buttons that show as a link), behave more like other buttons: on Safari they are reachable with the tab key, and users are able to activate them using the space key, in addition to enter, where supported by the browser. ([Tweet](https://twitter.com/orbeon/status/1288634524641894400))

### Other

- We implemented the XForms `xf:copy` element. ([\#4438](https://github.com/orbeon/orbeon-forms/issues/4438))
- We updated the default favicon to show nicely on high dpi monitors. ([Tweet](https://twitter.com/orbeon/status/1274053039062540288))
- We added the XPath 3 `environment-variable()` function. ([\#4718](https://github.com/orbeon/orbeon-forms/issues/4718))
- We upgraded to TinyMCE 5. ([\#4382](https://github.com/orbeon/orbeon-forms/issues/4382)) ([blog](https://blog.orbeon.com/2020/01/upgrading-to-tinymce-5.html))
- We refactored and moved a lot of the client-side code to Scala.js as part of our ongoing plan to [keep our code current](https://blog.orbeon.com/2019/12/keeping-our-code-current.html).

## Internationalization

See also:  

- [Supported languages](/form-runner/feature/supported-languages.md) for the list of supported languages.
- [Localizing Orbeon Forms](/contributors/localizing-orbeon-forms.md) for information about how to localize Form Builder and Form runner in additional languages. Localization depends on volunteers, so please let us know if you want to help!

## Browser support

- **Form Builder (creating forms)**
    - Chrome 87 (latest stable version)
    - Firefox 84 (latest stable version) and the current [Firefox ESR](https://www.mozilla.org/en-US/firefox/enterprise/)
    - Microsoft Edge 87 (latest stable version)
    - Safari 14 (latest stable version)
- **Form Runner (accessing form)**
    - All browsers supported by Form Builder (see above)
    - IE11, Edge 18
    - Safari Mobile on iOS 13
    - Chrome for Android (stable channel)

## Compatibility notes

### With header-based authentication, provide header on every request

Starting with Orbeon Forms 2020.1, if using header-based authentication, you should provide the headers with every request made to Orbeon Forms for which you want the user to be authenticated. You can get the old behavior, by setting the `oxf.fr.authentication.header.sticky` property to `true`. For more on this, see [when to set the headers](/form-runner/access-control/users.md#when-to-set-the-headers).

### Navigation from the `view` to the `edit` page

When users load the `view` page for a form and click on the "Edit" button, to edit the data they are currently viewing, starting with Orbeon Forms 2020.1, the browser loads the `edit` page as if users were to paste the URL of the edit page in the location bar of their browser, while up to Orbeon Forms 2019.2, the data loaded by the `view` page was sent to the `edit` page. In general, this won't make a difference, but it could, if:

- Between the time a user A loads the `view` page and the time user A clicks on the "Edit" button, user B edits, changes, and saves the data. Since Orbeon Forms 2020.1, user A will be editing the new data, while up to Orbeon Forms 2019.2, user A would  have been editing outdated data.
- After a user loads the `view` page, the data changes somehow. Then the user clicks on "Edit". At this point, since Orbeon Forms 2020.1 they will see the data without the change done while viewing, since the data is reloaded from the database, while up to Orbeon Forms 2019.2 the data would include the change done on the `view` page. Changing the data on the view page is rare, and unless the data is immediately saved, this was considered to be a bad practice, and isn't supported anymore.

### xforms-deselect event

This change is due to the implementation of support for the `xf:copy` element.

The default action of the `xforms-deselect` event on a single-selection control now clears the selection control's value. This is consistent with the behavior of multiple selection controls.

XForms code which depends, upon `xforms-select`, on the presence of the older value of the selection in the control's bound node, needs to be updated. This should be a rare occurrence.
