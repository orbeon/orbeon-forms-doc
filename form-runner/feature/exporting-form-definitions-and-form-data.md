# Form definitions and form data Zip export

## Availability

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

## Context

Orbeon Forms includes a few ways to export form definitions and form data. This page documents a very specific capability: exporting form definitions and form data in a batch, that is, multiple form definitions and their data are exported at once. The result is stored in a [zip file](https://en.wikipedia.org/wiki/ZIP_(file_format)) which downloads to your computer.

For a different type of export, see also [Excel and XML export](excel-xml-export.md).

## How to use the feature

The feature is available on the Form Runner [Forms Admin page](forms-admin-page.md). There, an "Export" button is available at the top of the page.

[//]: # (TODO)
[//]: # (![Export button]&#40;../images/export-button.png&#41;)

## Export modes

Exporting can be done in two different modes. You can either export the forms that were explicitly selected:

![Export selected forms](../images/export-selected-forms.png)

Or you can export forms by specifying a list of application, form, and version filters:

![Export following forms](../images/export-following-forms.png)

The application filter can either be set to "All applications" or to a specific application. When a specific application is selected, the form filter can either be set to "All forms" or to a specific form. The version filter can always be set to "All versions" or "Latest version" and when a specific form is selected, it can also be set to a specific version.

The "Form definition / data" dropdown allows you to choose whether you want to export form definitions, form data, or both.

The "Current data / revision history" dropdown allows you to choose whether you want to export the current data, the revision history, or both.

Finally, it's possible to specify a date range using the "From" and "Until" date pickers. This allows you to export only the data that was modified after and/or before the specified dates. Both the "From" and "Until" dates are inclusive.

## Zip file structure

The exported zip file contains all selected form definitions and data, as well as associated attachments.

The form data are stored in `data.xml` files, located in paths following the structure below:

`[app]/[form]/[version]/data/[document ID]/[last modified date/time]/data.xml`

Example:

`orbeon/acme-order-form/2/data/06ae53279bd3b74d20ee58e6a2a3240ab03206fa/2023-11-30T13:29:03.919Z/data.xml`

Form definitions are stored in `form.xhtml` files, located in similarly structured paths:

`[app]/[form]/[version]/form/latest/form.xhtml`

Example:

`orbeon/acme-order-form/2/form/latest/form.xhtml`

Attachments are stored alongside the form data and definitions. Examples:

`test-app/test-form/2/data/8679184c2b13c55fe74e84fa5c76b3aa643ca7f2/latest/b7dfdf7fa55954548c4bf1c3f1964f9cb276d20f.bin`

`orbeon/pta-remittance/1/form/latest/4ccffb5e691d8b2ec4c29f9bea88feb2e625580c.bin`

Form data, form definitions, and attachments can have metadata, in which case `.metadata.xml` files will be included as well. Example:

`orbeon/bookshelf/1/data/5bba537614484bd9e33c97bfc7649889/2023-08-21T09:16:09.914Z/data.xml.metadata.xml`

Metadata include information such as:

- the user/group who created the data
- the user who last modified the data
- the workflow stage

## Permissions

The export feature exports forms and data regardless of the permissions of the user who performs the export, as long as that user has access to the Forms Admin page. Therefore, it is very important to control access to the Forms Admin page.

## Limitations

At the moment, once the "Export" button has been clicked, the dialog will be closed and the export will be performed in the background. This means that you won't be able to see the progress of the export, and you won't be able to cancel it. This also means you shouldn't leave the current page until the zip file has been downloaded. This will be improved in a future version.

## See also

- [Purging historical data](purging-historical-data.md)
- [Revision history](revision-history.md)
- [Auditing](/form-runner/persistence/auditing.md)
- [Excel and XML export](excel-xml-export.md)
- [Summary page Excel Export](summary-page-export.md)
- [Excel and XML import](excel-xml-import.md)
- [Forms Admin page](forms-admin-page.md)
- [Zip Export API](/form-runner/api/persistence/export-zip.md)
- [Revision History API](/form-runner/api/persistence/revision-history.md)
- [Blog post: Exporting form definitions and data](https://www.orbeon.com/2024/04/form-data-export)