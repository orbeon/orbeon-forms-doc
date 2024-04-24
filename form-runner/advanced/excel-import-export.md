# Excel named ranges import and export

## Availability

[\[SINCE Orbeon Forms 2021.1\]](/release-notes/orbeon-forms-2021.1.md)

## Introduction

This feature is aimed at organizations whose users are familiar with Excel spreadsheets. Users can export a form to a spreadsheet and fill it out on their computers. They can pass the spreadsheet around for other users to complete it. When they are done, they can import the resulting spreadsheet back to Orbeon Forms, where the data is captured, validated, shown to the user for review, and then saved to the Orbeon Forms database.

## Export

### Basics 

The Excel export feature allows you to export a form definition to an Excel file. The Excel document contains all the fields of the form, attempting to mirror the layout of the original form. This includes section titles and field labels.

You can export an Excel file from the following locations:

| Page                     | Data Included |
|--------------------------|---------------|
| Form Builder             | No            |
| Form Runner Summary page | Yes           |
| Form Runner Detail page  | Yes           |

The export feature produces Excel files that look like this:

![Example of Excel export](/form-builder/images/excel-export-export.png)

### Named ranges

Orbeon Forms includes, in the generated Excel file, named ranges which match the control names assigned in Form Builder. This allows reimporting data in those cells during a subsequent import.

Excel names do not support all the character supported in Form Runner names. In addition, Excel has some very specific and poorly documented rules for name ranges for names that look like cell references. Such names are modified by Orbeon Forms when needed with an `_` prefix. The following shows some modification that are performed by Orbeon Forms:

| Original Name | Modified Name |
|---------------|---------------|
| `C`           | `_C`          |
| `c`           | `_c`          |
| `R`           | `_R`          |
| `r`           | `_r`          |
| `B1`          | `_B1`         |
| `C1_FOO`      | `_C1_FOO`     |
| `C123456`     | `_C123456`    |
| `AA1`         | `_AA1`        |
| `AAA1`        | `_AAA1`       |

Excel allows you to see the name of a given cell or range:

![Named ranges in Excel](/form-builder/images/excel-export-ranges.png)

### Repeated content

In the case of repeated grids, groups of cells share the same name.

![Repeated grid in Excel](/form-builder/images/excel-export-repeat-export.png)

### Exporting from the Form Builder Detail page

With Form Builder, the Excel Export button is available by default. You can control its availability with the following property, by adding the `excel-export` button token. We show here the default value of this property for Orbeon Forms 2023.1:

```xml
<property as="xs:string"  name="oxf.fr.detail.buttons.orbeon.builder">
    home
    summary
    new
    form-definition-xhtml-export|excel-export|xml-export
    test|test-pdf|test-offline|test-formulas
    publish
    save
</property>
```

Note the `|` which denote combined buttons.

### Exporting from the Form Runner Summary and Detail pages

With Form Runner, you can enable the export button with the following properties, by including the same `excel-export` button token. Summary page:

```xml
<property as="xs:string"  name="oxf.fr.summary.buttons.*.*">
    home review pdf delete duplicate excel-export new
</property>
```

Detail page:

```xml
<property as="xs:string"  name="oxf.fr.detail.buttons.*.*">
    summary clear pdf excel-export wizard-toc wizard-prev wizard-next save-final review
</property>
```

In addition, you must set the following property to enable export with data on the Summary and Detail pages:

```xml
<property
    as="xs:boolean"
    name="oxf.fr.export.page.enable.*.*"
    value="true"/>
```

The reason for this extra setting is that, even if the user has read access to the form, exporting a form in Excel or XML format can reveal some hidden data.

### Exporting through a service call

You can also export [through a service call](/form-runner/link-embed/linking.md#paths).

## Import

### Basics

Previous Orbeon Forms versions already supported an Excel import feature. Here are the main differences between the two types of Excel imports:

| Existing import feature                   | New import feature          |
|-------------------------------------------|-----------------------------|
| multiple form documents at a time (batch) | one form document at a time |
| row and column-based                      | named range based           |
| no repeat support                         | repeat support              |
| fixed layout                              | customizable layout         |

The new document-based import feature does not replace the previous one but complements it.

You don’t have to export a form to Excel to import data: as long as you have a spreadsheet that includes the appropriate named ranges (see above), you can use it for importing. This means that you can reuse existing spreadsheets with a few additions, and keep a spreadsheet layout that users are familiar with.

### Importing from the Import page

The same [Import page](excel.md) that was used for the earlier Excel import feature is used for the new name ranges-based Excel import.

However, you must configure the Import page to support the named ranges-based Excel import. You can do this by setting the following property, where the `excel-named-ranges` token indicates support for the format (the `xml-form-structure-and-data` allows support for the XML format):

```xml
<property
    as="xs:string"
    name="oxf.fr.import.allowed-formats.*.*" 
    value="excel-named-ranges xml-form-structure-and-data"/>
```

The import page wizard checks the validity of the format during import. When checked, you can open the data to review errors and decide to fix them or to perform a new import.

[//]: # (TODO: merging of data)

[//]: # (### Validation of static lists of choices)

[//]: # ()
[//]: # ([\[SINCE Orbeon Forms 2023.1\]]&#40;/release-notes/orbeon-forms-2023.1.md&#41;)

[//]: # ()
[//]: # (You can enable the following property to enable validation of static lists of choices during import:)

```xml
<property
    as="xs:boolean"
    name="oxf.fr.detail.validate-selection-controls-choices.*.*"
    value="true"/>
```

## Limitations

As available in Orbeon Forms 2021.1, the new Excel import and export feature has the following limitations:

- no 24-column mode
- no dynamic list of choices (for example dynamic dropdowns)
- no calculations or validations are exported
- no nested repeats
- repeated grids or sections are always flattened
- no section templates support
- some form controls are not supported, including
    - Formatted Text
    - Handwritten Signature
    - Attachments

## See also

- Blog post: [Excel export and import](https://blog.orbeon.com/2021/09/excel-export-and-import.html)
- [Import page](/form-runner/advanced/excel.md)
- [Service calls](/form-runner/link-embed/linking.md)

[//]: # (https://3.basecamp.com/3600924/buckets/16915667/messages/4541166737)
[//]: # ()
[//]: # (<!-- Mais ne le montrer qu'en mode `edit` -->)
[//]: # (<property )
[//]: # (    as="xs:string"  )
[//]: # (    name="oxf.fr.detail.button.import.visible.acme.contact")
[//]: # (    value="fr:mode&#40;&#41; = 'edit'"/>)
[//]: # ()
[//]: # (<!-- Le bouton `import` navigue à la page d'import en passant le `document-id` -->)
[//]: # (<property as="xs:string" name="oxf.fr.detail.process.import.acme.contact">)
[//]: # (    require-uploads)
[//]: # (    then navigate&#40;'/fr/{fr:app-name&#40;&#41;}/{fr:form-name&#40;&#41;}/import?document-id={fr:document-id&#40;&#41;}'&#41;)
[//]: # (</property>)