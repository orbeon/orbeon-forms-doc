> [[Home]] ▸ [[Form Runner|Form Runner]]

## Availability

This is an Orbeon Forms PE feature available in Orbeon Forms 4.x.

## What it is

This feature allows you to import batches of data from a source Excel spreadsheet to a deployed Form Runner form.

## How it works

It's pretty simple!

1. You start the import from the Form Runner import page, accessible from the summary page

    ![][1]

2. You select the Excel 2007 file to upload and import

    ![][2]

3. Form Runner validates the Excel file and give you the option to add to existing data for the given form, or remove all existing data first

    ![][3]

4. Form Runner imports valid data from the Excel file

    ![][4]

5. The import is complete!

    ![][5]

_NOTE: Only the Excel 2007 .xlsx format (Office Open XML) is supported. The older, .xls format is not supported._

## Enabling the import button on the summary page

You enable the import button by adding the import token to the `oxf.fr.summary.buttons.*.*` property. Here for the Orbeon Contact form:

```xml
<property as="xs:string"
  name="oxf.fr.summary.buttons.orbeon.contact"
  value="new import edit print pdf delete"/>
```

## Mapping between form controls and Excel spreadsheet

A given Excel file contains data for a single Orbeon Forms form.

The spreadsheet must follow this format:

- only the first sheet is considered
- the first row is a special header row, where each cell contains an identifier that matches a control name in the given form
- each subsequent row contains data for a new instance of form data

In your form, you create controls with names that match the names in the first row (header row) of the Excel document.

Here is an example spreadsheet for the sample Orbeon Contact form:

![][6]

_NOTE: Only characters allowed in XML names are allowed as control names in Form Builder. In case your Excel header row requires names with non-XML characters (Form Builder will tell you the name is not allowed), simply replace them by "_" in Form Builder._

## Limitations

The import functionality does not support importing data into repeated grids.

[1]: http://wiki.orbeon.com/forms/_/rsrc/1305744746496/doc/developer-guide/form-runner/importing-data/01%20Summary%20shadow.png
[2]: http://wiki.orbeon.com/forms/_/rsrc/1305744758433/doc/developer-guide/form-runner/importing-data/02%20Upload%20shadow.png
[3]: http://wiki.orbeon.com/forms/_/rsrc/1305744776932/doc/developer-guide/form-runner/importing-data/03%20Validation%20shadow.png
[4]: http://wiki.orbeon.com/forms/_/rsrc/1305744793626/doc/developer-guide/form-runner/importing-data/04%20Import%20shadow.png
[5]: http://wiki.orbeon.com/forms/_/rsrc/1305744818223/doc/developer-guide/form-runner/importing-data/05%20Complete%20shadow.png
[6]: http://wiki.orbeon.com/forms/_/rsrc/1305745300125/doc/developer-guide/form-runner/importing-data/excel.png
