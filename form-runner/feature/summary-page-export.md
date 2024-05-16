# Summary page Excel Export

## Availability

[\[SINCE Orbeon Forms 2023.1.3\]](/release-notes/orbeon-forms-2023.1.3.md)

This is an Orbeon Forms PE feature.

## Context

Orbeon Forms includes a few ways to export form definitions and form data. This page documents a very specific capability: exporting a subset of *form data* as seen in the Summary Page in Excel format.

[//]: # (, appropriate for [importing]&#40;/form-runner/feature/excel-xml-import.md&#41; the data into Orbeon Forms at a later time)

For different types of export, see also:

- [Form definitions and form data batch export](exporting-form-definitions-and-form-data.md)
- [Excel and XML Export](excel-xml-export.md)

## How to use the feature

### Locations

You can export form data from the Form Runner [Summary page](summary-page.md).

### Enabling the feature

In order to enable the feature, you need to add the `excel-export-with-search` token:

```xml
<property as="xs:string"  name="oxf.fr.summary.buttons.*.*">
    home review pdf delete duplicate excel-export-with-search new
</property>
```

![Excel export button in the Summary page](../images/summary-excel-export.png)

### Exported data

Activating the button exports the data currently displayed in the Summary Page, including:

- all the pages accessible using paging
- search results if a search is active
- sorting order if specified

The export only includes data that the user can already see in the Summary page.

The Excel document includes:

- a heading row including the column names
- one row per form data row
- form metadata is included as shown in the Summary page, if configured
    - Created: creation date/time
    - Modified: last modification date/time
    - Created By: username of the user who created the data
    - Modified By: username of the user who last modified the data
    - Workflow stage: name of the workflow stage associated with the data

![Example of Excel export from the Summary page](../images/summary-excel-export-sheet.png)

## See also

- Blog post: [Excel export and import](https://blog.orbeon.com/2021/09/excel-export-and-import.html)
- [Excel and XML Import](excel-xml-import.md)
- [Form definitions and form data batch export](exporting-form-definitions-and-form-data.md)
- [Excel and XML Export](excel-xml-export.md)
- [Service calls](/form-runner/link-embed/linking.md)