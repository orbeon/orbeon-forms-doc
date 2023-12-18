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

The following parameters allow controlling hints, alerts, and required controls in the PDF:

- `fr-pdf-show-hints`
    - defaults to the `oxf.fr.detail.static-readonly-hint` property, which itself defaults to `false`
    - when `true`, shows hints in the PDF
- `fr-pdf-show-alerts`
    - defaults to the `oxf.fr.detail.static-readonly-alert` property, which itself defaults to `false`
    - when `true`, shows alerts in the PDF
- `fr-pdf-show-required`
    - defaults to the `oxf.fr.detail.static-readonly-required` property, which itself defaults to `false`
    - when `true`, style required controls in the PDF

The following parameters allow controlling the use of PDF templates:

- `fr-use-pdf-template`
    - This defaults to `true` if there is at least one PDF template attached to the form, `false` otherwise.
    - If at least one PDF template is available, the default is to use one of the PDF templates. But if
      `use-pdf-template = "false"`, then use of any PDF template is disabled and the automatic PDF is produced.  
- `fr-pdf-template-name`
    - This contributes to selecting a specific PDF template.
    - If `pdf-template-name` specifies a name, such as with `pdf-template-name = "archive"`, the list of available PDF
      templates is reduced to those having an exactly matching name. If no matching name is found, an error is raised. 
- `fr-pdf-template-lang` 
    - This contributes to selecting a specific PDF template.
    - If `pdf-template-lang` specifies a language, such as with `pdf-template-lang = "fr"`, the list of available
      PDF templates as reduced by `pdf-template-name` is used to find a PDF template with a matching language.
      If no matching language is found, an error is raised.
    - If `pdf-template-lang` is empty or missing:
        - The PDF template with the current form language is used, if there is a match.
        - If there is no match, the first available PDF template is used.
