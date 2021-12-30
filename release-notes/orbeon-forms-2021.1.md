# Orbeon Forms 2021.1

__TODO, December 31, 2020__

[RELEASE NOTES WORK IN PROGRESS]

Today we released Orbeon Forms 2021.1! This release introduces new features and bug-fixes.

## Notable features and enhancement

### Test PDF

Orbeon Forms 2021.1 allows you to test PDF production without publishing your form.

![The "Test PDF" button](/form-builder/images/test-pdf-button.png)

This feature provides a number of options that help test some of the form's logic as well:

![The “Test PDF Production” dialog](/form-builder/images/test-pdf-use-data.png)

For more details:

- [blog post](https://blog.orbeon.com/2021/11/testing-pdf-production-in-form-builder.html)
- [documentation](https://doc.orbeon.com/form-builder/advanced/pdf-production/pdf-test)

### Quick control search

In large forms, it is often difficult to navigate to a specific form control. Orbeon Forms 2021.1 introduces a quick way to find a control using one of the following keyboard shortcuts:

- ⌘J (macOS)
- ⌃J (other operating systems)

The short cuts open a small dialog. Clicking or pressing the Enter key and then typing a search term allows you to search from the list of available controls. Upon selection, Form Builder highlights and scrolls to the control selected.

If the "Open Settings Dialog" option is selected, the "Control Settings" dialog for the control also opens right away.

![The quick search dialog](/form-builder/images/quick-search.png)

### Excel export/import

Orbeon Forms 2021.1 includes a new feature to import and export Excel files.

Previous Orbeon Forms versions already supported an Excel import feature. Here are the main differences between the two types of Excel imports:

|Existing import feature|New import feature|
|---                                      |---                        |
|multiple form documents at a time (batch)|one form document at a time|
|row and column-based                     |named range based          |
|no repeat support                        |repeat support             |
|fixed layout                             |customizable layout        |

The new document-based import feature does not replace the previous one but complements it. Each feature might be improved in the future.

The export feature produces Excel files that look like this:

![Example of Excel export](/form-builder/images/excel-export-example.png)

For more details:

- [blog] post(https://blog.orbeon.com/2021/09/excel-export-and-import.html)

### XML format for import/export

Similarly to the import and export of Excel files, Orbeon Forms 2021.1 allows you to export form data and description to an XML file, and to reimport the data back.

Orbeon Forms internally represents form data as XML. However, this import/export feature doesn't directly export that data. It exports:

- metadata about the form (title, etc.)
- information about the structure of the form, including sections, grids, their labels, and more
- the data proper, but only data that is not always hidden

TODO: doc, blog

### Experimental offline mode

Since its inception, Orbeon Forms has had a hybrid architecture for forms:

- the user interface runs in the browser, implemented in JavaScript (and more recently Scala.js)
- the form's logic and validations runs on the server

This architecture has benefits, such as protecting the confidentiality of internal data that never leaves the server.

This is still the case with the Orbeon Forms 2021.1, however we made lots of internal changes to support running Form Runner in a pure JavaScript environment. This will, in the future, allow running forms entirely offline, as well as embedded within mobile apps.

With Orbeon Forms 2021.1, we are releasing a preview of this feature. From Form Builder, simply use the new "Test Offline" button to see whether your form operates and renders properly in this new mode.

![The "Test Offline" button](/form-builder/images/test-offline-button.png)

When you do this:

- The form definition edited in Form Builder is compiled to a serialized representation.
- The JavaScript-based form runtime is loaded in the Form Builder test window, loads the compiled form, and renders it. 

From the user's perspective, this works almost exactly like the "Test" button which has always been present in Form Builder.

[screenshot]

As of Orbeon Forms 2021.1, there are limitations:
 
- Services do not run offline and will return errors.
- There is no Summary page, Home page, or navigation between pages.
- Some controls are not fully supported, including the Formatted Text Area as well as attachment controls.
- Some formulas might fail.
- There is no XML Schema support for datatype validation.
- The APIs to compile and embed forms are not yet documented.
- TODO

### Inspect formulas

With Orbeon Forms, formulas are very important: they are used for calculating values, making parts of the form visible or readonly, and more. However, they can be difficult to debug, and so far Form Builder didn't have a way to show all formulas in a central location.

A new, still experimental feature allows you to inspect formulas. You access it under the "Test" menu.

![The "Inspect Formulas" button](/form-builder/images/inspect-formulas-button.png)

This allows you to see, in a table, the following formulas used in the form for:

- Initial values
- Calculated values
- Visibility
- Required
- Read-Only

![Example showing Calculated Value dependencies](/form-builder/images/inspect-formulas-example.png)

In addition, the dependencies between these formulas is shown with colors so that, for complex forms, you can know which values impact a formula, and the other way around.

Inspect Formulas: first phase #4825

### Actions Editor

The Actions Editor features two new enhancements:

- You can now set service values from a formula in addition to a control value.
- You can control whether an action runs based on formula.

![Action request formula](/form-builder/images/actions-request-formula.png)

![Action condition formula](/form-builder/images/actions-condition-formula.png)

These two features were already supported by the [Action Syntax](/form-builder/actions-syntax.md) but were not available in the Form Builder UI.

For more details:

- [blog post](https://blog.orbeon.com/2021/10/enhancements-actions-editor-2021-1.html)

### Grids

Orbeon Forms 2021.1 improves grids in two ways.

First, the Grid Settings dialog now shows the number of grid rows in the grid.

![Number of grid rows](/form-builder/images/grid-settings-number-of-grid-rows.png)

Second, for repeated grids only, an option allows you to automatically show a row number at the beginning of each grid repetition.

![Number of grid rows](/form-builder/images/grid-settings-show-repetition-number.png)

For more details:

- [blog post](https://blog.orbeon.com/2021/09/enhancements-to-repeated-grids.html)

## Other

Form Builder: option to index fields separately from Summary page #5018
Add double type again as a user choice #4847
Dropdowns to support hint on items #5054

PDF: Option to show all dropdown control values in automatic PDF #5026
Option not to run calculation in readonly modes #3672
Background API: ability to disable calculations #4977


XForms 2.0:

- xforms-dialog-shown/xforms-dialog-hidden #5078
- Implement xf:parse() #3699

A11y:

- Announce new error messages in the error summary #4795
- Avoid invalid role="navigation" on <ul> #5079
- Improve reading of labels for date-time control #4831

New functions:

- XPath function returning whether a form is embedded #4976
- Add XPath function to tell whether the form is running in the background #4958
- Function to access the value of a control within a section template #2471
- Form Runner function returning whether we are editing a draft #5060

API:

- Form metadata API: ability to filter by modified-since date #4987

Flat view to support form versions #5037

Home page: ability to delete (unpublish) published form definition #3597

Summary page: ability to show the created-by/last-modified-by users #4986
Summary/Home to show timestamps in a configurable timezone #1101\

send action to support a workflow-stage parameter #5070

Allow section templates to communicate when included in same form #5005

Section template: show name of library/section #1700

New filter to log the body of incoming requests #5098

## Internationalization

TODO

See also:  

- [Supported languages](/form-runner/feature/supported-languages.md) for the list of supported languages.
- [Localizing Orbeon Forms](/contributors/localizing-orbeon-forms.md) for information about how to localize Form Builder and Form runner in additional languages. Localization depends on volunteers, so please let us know if you want to help!

## Browser support

TODO

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

### Log4j2

Due to December 2021 Log4j vulnerabilities, Orbeon Forms now uses the latest Log4j 2 libraries. Until Orbeon Forms 2020.1.5, Orbeon Forms used older Log4j 1 libraries. While Orbeon Forms was not vulnerable to these specific attacks, we decided to migrate Orbeon Forms to Log4j 2 in order to respond faster to future vulnerabilities should they arise.

For details, see the following blog posts;

- [Vulnerability in the log4j library](https://blog.orbeon.com/2021/12/vulnerability-in-log4j-library.html)
- [Orbeon Forms PE Log4j maintenance releases](https://blog.orbeon.com/2021/12/orbeon-forms-pe-log4j-maintenance.html)
- [More Orbeon Forms PE Log4j maintenance releases](https://blog.orbeon.com/2021/12/more-orbeon-forms-pe-log4j-maintenance.html)

Log4j 2 uses different configuration files than Log4j 1. However, we provide backward compatibility support for the older Log4j 1 configuration file. This means that in most cases, you do not have to update your configuration file immediately.

Orbeon Forms no longer ships with a `log4j.xml` configuration file, but it ships with a `log4j2.xml` configuration file.

- If you have pre-existing `log4j.xml` configuration file, for example because you are upgrading to Orbeon Forms 2021.1 from an older version, you can still use that configuration file, which will take precedence over the new `log4j2.xml` file. However:
    - You must make sure that you do not have duplicate log file names in the configuration, or Log4j 2 will complain about that and ignore the configuration.
    - We recommend that you consider moving to a `log4j2.xml` configuration file.
- If you do not yet have an existing `log4j.xml` file:
    - We recommend that you update the  `log4j2.xml` configuration file that ships with Orbeon Forms.
    
If you are creating or updating a `log4j2.xml` file, you cannot simply copy the contents of an existing `log4j.xml` to `log4j2.xml` as the two formats are incompatible! Instead, start with the `log4j2.xml` provided, and visit the [Log4j 2 configuration](https://logging.apache.org/log4j/2.x/manual/configuration.html) online to understand and make changes.

### Combining custom and built-in relational persistence

If you are using the following unlikely combination:

- The built-in implementation of persistence API for relational databases to store forms.
- Your own implementation of the persistence API to store data.

Then, starting with Orbeon Forms 2021.1, your implementation of the persistence API needs to support the `HEAD` method, in addition to `GET`.

### "Save Draft" button

The `save-draft` button is now called `save-progress`. The button label is also renamed to say "Save Progress" instead of "Save Draft" by default. The reason for this renaming is that it reflects the intention better, and reduces confusion with the word "draft" also used for autosave drafts.

The `save-draft` button remains for backward compatibility. By default, it calls the `process("save-progress")` process.

We recommend that you review whether you have customized the `save-draft` process and/or button resources in your `properties-local.xml` and udpate them to the new name as needed,