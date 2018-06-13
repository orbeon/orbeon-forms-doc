# Summary page configuration properties



## Adding your own CSS files

[SINCE Orbeon Forms 2017.1]

In addition to `oxf.fr.css.custom.uri`, you can also use the following property, which apply only to the Summary page:

```xml
<property
    as="xs:string"
    name="oxf.fr.summary.css.custom.uri.*.*"
    value="/forms/acme/assets/acme-summary.css"/>
```

See also [Adding your own CSS](form-runner.md#adding-your-own-css).

## Adding your own JavaScript files

[SINCE Orbeon Forms 2017.1]

In addition to `oxf.fr.js.custom.uri`, you can also use the following property, which apply only to the Summary page:

```xml
<property
    as="xs:string"
    name="oxf.fr.summary.js.custom.uri.*.*"
    value="/forms/acme/assets/acme-summary.js"/>
```

See also [Adding your own JavaScript](form-runner.md#adding-your-own-javascript).

## Summary page size

```xml
<property
    as="xs:integer"
    name="oxf.fr.summary.page-size.*.*"
    value="10"/>
```

Number of rows shown in the Summary page.

## Created and Last Modified columns

By default, the Summary page shows a Created and Modified columns:

![](/form-runner/images/summary-created-last-modified.png)

You can remove either one of those columns by setting the value appropriate property to `false`:

```xml
<property
    as="xs:boolean"
    name="oxf.fr.summary.show-created.*.*"
    value="true"/>

<property
    as="xs:boolean"
    name="oxf.fr.summary.show-last-modified.*.*"
    value="true"/>
```

## Buttons on the Summary page

```xml
<property
    as="xs:string"
    name="oxf.fr.summary.buttons.*.*"
    value="home review pdf delete duplicate new"/>
```

The property configures which buttons are included on the Summary page, and in what order they are shown. Possible buttons are:

* `home`
    * Label: "Home"
    * Action: Navigate to the Form Runner Home page.
* review
    * Label: "Review"
    * Action: Navigate to the Detail page in "view" mode to review the selected form data.
* `pdf`
    * Label: "PDF"
    * Action: Create a PDF file for the selected form data.
* `tiff` [SINCE Orbeon Forms 2016.1]
    * Label: "TIFF"
    * Action: Create a TIFF image file for the selected form data.
* `delete`
    * Label: "Delete"
    * Action: Delete the selected form data.
* `import`
    * Label: "Import"
    * Action: Import data via the [Excel import page](../../form-runner/advanced/excel.md).
* `duplicate` [SINCE Orbeon Forms 4.5]
    * Label: "Duplicate"
    * Action: Duplicate the selected form data (or form definition on the Form Builder Summary page), including attachments.
    * Usage: Select one or more checkboxes and press the "Duplicate" button. When the operation completes the Summary page refreshes with the new duplicated form data.
* `new`
    * Label: "New"
    * Action: Navigate to the Detail page in "new" mode to create new form data.