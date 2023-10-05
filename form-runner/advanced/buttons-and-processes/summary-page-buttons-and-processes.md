# Summary page buttons and processes

## Configuring Summary page buttons

### Basic configuration

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
* `review`
    * Label: "Review"
    * Action: Navigate to the Detail page in "view" mode to review the selected form data.
* `pdf`
    * Label: "PDF"
    * Action: Create a PDF file for the selected form data.
* `tiff` [SINCE Orbeon Forms 2016.1]
    * Label: "TIFF"
    * Action: Create a TIFF image file for the selected form data.
* `excel-export` [SINCE Orbeon Forms 2023.1]
    * Label: "Excel Export"
    * Action: Export the selected form data in Excel format.
* `xml-export` [SINCE Orbeon Forms 2023.1]
    * Label: "XML Export"
    * Action: Export the selected form data in XML format.
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

## Custom process configuration

[SINCE Orbeon Forms 2023.1]

Originally, Form Runner processes associated with buttons were only [available for the Detail Page](/form-runner/advanced/buttons-and-processes/README.md). Since Orbeon Forms 2023.1, they are also available on the Form Runner [Summary page](/configuration/properties/form-runner-summary-page.md). This allows you to add custom buttons to the Summary page, and to configure what happens when users press those buttons.

You configure a Summary page process with properties starting with the `oxf.fr.summary.process` prefix. For example, to create a process for the `new` button:

```xml
<property as="xs:string"  name="oxf.fr.summary.process.new.*.*">
    confirm(message = "Are you sure?")
    then suspend
    then navigate(uri = "/fr/{fr:app-name()}/{fr:form-name()}/new")
</property>
```

Unlike Detail page processes, Summary page processes currently don't have access to the form data associated with them. Therefore, they are best used for navigation or similar simple tasks.  

As with the Detail page processes, you can control button visibility and whether a button is disabled using properties starting with the `oxf.fr.summary.button` prefix. The value of these properties is an XPath expression. The following two properties apply to a hypothetical `acme` button:

```xml
<property as="xs:string"  name="oxf.fr.summary.button.acme.visible.*.*">
    ...
</property>
<property as="xs:string"  name="oxf.fr.summary.button.acme.enabled.*.*">
    ...
</property>
```

## See also 

- [Buttons and processes](/form-runner/advanced/buttons-and-processes/README.md)
- [Home Page](home-page.md)
- [Summary page configuration properties](/configuration/properties/form-runner-summary-page.md)
- [Summary page](/form-runner/feature/summary-page.md)
- [Form Builder Summary Page](/form-builder/summary-page.md)
