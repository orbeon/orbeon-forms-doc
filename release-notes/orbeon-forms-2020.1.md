# Orbeon Forms 2020.1

## Browser support

- **Form Builder (creating forms)**
    - Chrome xxx (latest stable version)
    - Firefox xx (latest stable version) and the current [Firefox ESR](https://www.mozilla.org/en-US/firefox/enterprise/)
    - Microsoft Edge 83 (latest stable version)
    - Safari xxx (latest stable version)
- **Form Runner (accessing form)**
    - All browsers supported by Form Builder (see above)
    - IE11, Edge 18
    - Safari Mobile on iOS 13
    - Chrome for Android (stable channel)

## Compatibility notes

### With header-based authentication, provide header on every request

Starting with Orbeon Forms 2020.1, if using header-based authentication, you should provide the headers with every request made to Orbeon Forms for which you want the user to be authenticated. You can get the old behavior, by setting the `oxf.fr.authentication.header.sticky` property to `true`. For more on this, see [when to set the headers](form-runner/access-control/users.md#when-to-set-the-headers).

### Navigation from the `view` to the `edit` page

When users load the `view` page for a form and click on the "Edit" button, to edit the data they are currently viewing, starting with Orbeon Forms 2020.1, the browser loads the `edit` page as if users were to paste the URL of the edit page in the location bar of their browser, while up to Orbeon Forms 2019.2, the data loaded by the `view` page was sent to the `edit` page. In general, this won't make a difference, but it could, if:

- Between the time a user A loads the `view` page and the time user A clicks on the "Edit" button, user B edits, changes, and saves the data. Since Orbeon Forms 2020.1, user A will be editing the new data, while up to Orbeon Forms 2019.2, user A would  have been editing outdated data.
- After a user loads the `view` page, the data changes somehow. Then the user clicks on "Edit". At this point, since Orbeon Forms 2020.1 they will see the data without the change done while viewing, since the data is reloaded from the database, while up to Orbeon Forms 2019.2 the data would include the change done on the `view` page. Changing the data on the view page is rare, and unless the data is immediately saved, this was considered to be a bad practice, and isn't supported anymore.

### xforms-deselect event

This change is due to the implementation of support for the `xf:copy` element.

The default action of the `xforms-deselect` event on a single-selection control now clears the selection control's value. This is consistent with the behavior of multiple selection controls.

XForms code which depends, upon `xforms-select`, on the presence of the older value of the selection in the control's bound node, needs to be updated. This should be a rare occurrence.

