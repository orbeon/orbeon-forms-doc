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

![Created and Last Modified](/form-runner/images/summary-created-last-modified.png)

You can remove either one of those columns by setting the appropriate property value to `false`:

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

## Show the workflow stage

[SINCE Orbeon Forms 2020.1]

You can add this column by setting the following property value to `true`:

```xml
<property 
    as="xs:boolean" 
    name="oxf.fr.summary.show-workflow-stage.*.*"                           
    value="true"/>
```

## Show created by and last modified by users

[SINCE Orbeon Forms 2021.1]

You can add either one of those columns by setting the appropriate property value to `true`:

```xml
<property 
    as="xs:boolean" 
    name="oxf.fr.summary.show-username.*.*"                           
    value="true"/>
<property 
    as="xs:boolean" 
    name="oxf.fr.summary.show-last-modified-by.*.*"                   
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
    
### New button: version of the form

[SINCE Orbeon Forms 2019.2]

By default, the "New" button takes users to the current version of the form, that is, the one selected in the dropdown at the top of the page. You can change this behavior by setting the property below to either `latest` to use the latest published version of the form, or an integer value to always used that specific version. The default value of the property is `current`, which corresponds to the default behavior.

```xml
<property 
    as="xs:string"
    name="oxf.fr.summary.new.form-version.*.*"
    value="latest"/> 
```

## Versioning

[SINCE Orbeon Forms 2020.1 and 2019.2.3] The following property allows you to configure whether the [summary page](form-runner/feature/summary-page.md) shows data:
 
- if set to `true`: for one version at a time, which is the default;
- if set to `false`: for all data across form definition versions, as it used to be the case up until Orbeon Forms 2018.1.

Even when set to `false`, if the `form-version` request parameter is passed to the summary page, it will will not ignore the request parameter and will only show data created with that version of the form definition.

```xml
<property 
    as="xs:boolean"
    name="oxf.fr.summary.show-version-selector.*.*"
    value="false"/>
```
