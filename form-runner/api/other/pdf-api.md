# PDF API

## Availability

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

Previously, PDF files could be produced from the Form Runner user interface, or by sending or emailing a PDF, but not from the Form Runner API. This is now possible with the PDF API.

## Path and parameters

You access the PDF API as follows:

```
/fr/service/$app/$form/export/$document?form-version=$form-version&export-format=$export-format
```

Where:

- `$app` is the form definition's application name
- `$form` is the form definition's form name
- `$document` is the form data's document id
- `$form-version` is the form definition version number (latest published version by default)
- `$export-format` is either `pdf` or `tiff`

## Other parameters

You can also use the following URL parameters:

- `fr-pdf-show-hints`
    - defaults to the `oxf.fr.detail.static-readonly-hint` property, which itself defaults to `false`
    - when `true`, shows hints in the PDF
- `fr-pdf-show-alerts`
    - defaults to the `oxf.fr.detail.static-readonly-alert` property, which itself defaults to `false`
    - when `true`, shows alerts in the PDF
- `fr-use-pdf-template`
    - defaults to `true` if there is at least one PDF template attached to the form, `false` otherwise
    - when `false`, disables the use of PDF templates and use the automatic PDF generation instead
- `fr-pdf-template-name`
    - contributes to selecting a specific PDF template
- `fr-pdf-template-lang`
    - contributes to selecting a specific PDF template
