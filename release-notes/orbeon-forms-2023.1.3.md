# Orbeon Forms 2023.1.3

__Thursday, June 13, 2024__

Today we released Orbeon Forms 2023.1.3 PE! This maintenance release contains bug-fixes and new features and is recommended for all users of:

- [Orbeon Forms 2023.1.2 PE](orbeon-forms-2023.1.2.md)
- [Orbeon Forms 2023.1.1 PE](orbeon-forms-2023.1.1.md)
- [Orbeon Forms 2023.1 PE](orbeon-forms-2023.1.md)

## New features

You can now export form data from the Summary page in Excel format, with a simple new button:

![Form data and Excel export button on the Summary page](/form-runner/images/summary-excel-export.png)

For more, see the [documentation](/form-runner/feature/summary-page-export.md).

In addition, we have added new keyboard shortcuts to Form Builder. For more see, the [blog post](https://www.orbeon.com/2024/07/keyboard-shortcuts).

## Issues addressed

This release addresses the following issues since [Orbeon Forms 2023.1.2 PE](orbeon-forms-2023.1.2.md):

- Accessibility
    - Single checkbox input is missing ARIA attributes ([\#6303](https://github.com/orbeon/orbeon-forms/issues/6303))
    - `<xf:select1>` and `<xf:select>` with `appearance="full"` to have `aria-required` and `aria-invalid` ([\#6302](https://github.com/orbeon/orbeon-forms/issues/6302))
    - Missing padding for Warning label ([\#6305](https://github.com/orbeon/orbeon-forms/issues/6305))
    - Show label/name of current control in Control Settings dialog title ([\#6292](https://github.com/orbeon/orbeon-forms/issues/6292))
    - Allow hiding the title of a section ([\#6288](https://github.com/orbeon/orbeon-forms/issues/6288))
    - XBL components support for `aria-invalid` and `aria-required` ([\#6279](https://github.com/orbeon/orbeon-forms/issues/6279))
    - Selected radio button and checkboxes are not showing in high contrast ([\#6350](https://github.com/orbeon/orbeon-forms/issues/6350))
    - Unwanted black rectangle between fields and alerts in the high contrast on Windows ([\#6349](https://github.com/orbeon/orbeon-forms/issues/6349))
- Form Builder
    - Radio buttons for Collapsible not showing in Section/Grid Settings dialog ([\#6345](https://github.com/orbeon/orbeon-forms/issues/6345)) 
    - "Dropdown with Other" truncates entries in Number field settings ([\#6308](https://github.com/orbeon/orbeon-forms/issues/6308))
    - Email template condition missing after reloading form ([\#6319](https://github.com/orbeon/orbeon-forms/issues/6319))
    - Date and time selection controls does not show in the Time Window tab of the Form Settings dialog ([\#6299](https://github.com/orbeon/orbeon-forms/issues/6299))
    - Explanatory Text TinyMCE toolbar is small and doesn't always work ([\#6315](https://github.com/orbeon/orbeon-forms/issues/6315))
    - Disallow browser textarea resizing in the Form Builder form area ([\#6312](https://github.com/orbeon/orbeon-forms/issues/6312))
    - Hidden field is too wide and icon is too small ([\#6309](https://github.com/orbeon/orbeon-forms/issues/6309))
    - Horizontal scrollbar in toolbox when version dropdown displayed ([\#6358](https://github.com/orbeon/orbeon-forms/issues/6358))
    - Parameters editor should never allow entering two parameters with the same name ([\#6368](https://github.com/orbeon/orbeon-forms/issues/6368))
- Form Runner
    - Excel table export from Summary page ([\#6323](https://github.com/orbeon/orbeon-forms/issues/6323))
    - Summary page Excel export: optional document id export ([\#6329](https://github.com/orbeon/orbeon-forms/issues/6329))
    - Baseline updates do not include XBL components ([\#6352](https://github.com/orbeon/orbeon-forms/issues/6352))
    - `fr:run-process-by-name()` isn't sync anymore ([\#6342](https://github.com/orbeon/orbeon-forms/issues/6342))
    - TOC section stays highlighted ([\#6336](https://github.com/orbeon/orbeon-forms/issues/6336))
    - "Single Checkbox with Label on Top" crashes when dynamically shown ([\#6339](https://github.com/orbeon/orbeon-forms/issues/6339))
    - `fr:dropdown-select1` to support minimal label ([\#6333](https://github.com/orbeon/orbeon-forms/issues/6333))
    - Enabling form level placeholders hides full labels for all XBL components ([\#6334](https://github.com/orbeon/orbeon-forms/issues/6334))
    - Email template expression doesn't resolve value correctly ([\#6335](https://github.com/orbeon/orbeon-forms/issues/6335))
    - Process action `send(content = "pdf")` failing ([\#6328](https://github.com/orbeon/orbeon-forms/issues/6328))
    - Improvements to Spanish resources for repeated grid, error dialog, attachment control ([\#6320](https://github.com/orbeon/orbeon-forms/issues/6320))
    - Inline spinner button loses spinner when label updates ([\#6069](https://github.com/orbeon/orbeon-forms/issues/6069))
    - Explanatory Text can overflow ([\#6316](https://github.com/orbeon/orbeon-forms/issues/6316))
    - Number and currency don't support minimal labels ([\#6304](https://github.com/orbeon/orbeon-forms/issues/6304))
    - Don't show upload metadata if readonly and missing ([\#6298](https://github.com/orbeon/orbeon-forms/issues/6298))
    - Purge: 403 status code while purging data locally ([\#6289](https://github.com/orbeon/orbeon-forms/issues/6289))
    - Scala.js TypeError: Cannot convert undefined or null to object ([\#6287](https://github.com/orbeon/orbeon-forms/issues/6287))
    - reCAPTCHA to support reset ([\#6291](https://github.com/orbeon/orbeon-forms/issues/6291))
    - Export from Forms Admin doesn't produce a zip on SQL Server ([\#6283](https://github.com/orbeon/orbeon-forms/issues/6283))
    - Exception caused by tinyMceObject.render() when loading orbeon/controls ([\#6262](https://github.com/orbeon/orbeon-forms/issues/6262))
    - For SQL Server, switch from `ntext` to `nvarchar(max)` ([\#6266](https://github.com/orbeon/orbeon-forms/issues/6266))
    - Excel form definition export crashes on Controls form ([\#6327](https://github.com/orbeon/orbeon-forms/issues/6327))
    - Overlap of some fields in PDF ([\#6367](https://github.com/orbeon/orbeon-forms/issues/6367))
- Other
    - Missing license statement in two files ([\#6354](https://github.com/orbeon/orbeon-forms/issues/6354))
    - Configuration banner shows for custom persistence provider ([\#6300](https://github.com/orbeon/orbeon-forms/issues/6300))
    - XForms servlet filter stopped working in 2023.1 ([\#6325](https://github.com/orbeon/orbeon-forms/issues/6325))

You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page.

Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon), [Bluesky](https://bsky.app/profile/orbeon.bsky.social), or the [forum](https://groups.google.com/g/orbeon).

We hope you enjoy this release!
