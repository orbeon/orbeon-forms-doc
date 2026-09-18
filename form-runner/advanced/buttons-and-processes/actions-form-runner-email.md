# Form Runner email action

[Orbeon Forms PE only]

##  Introduction

The `email` action sends an email with optionally XML form data, attachments, and PDF. It is automatically associated with the "Email" button, but can be called by a process.  

## Parameters

- [SINCE Orbeon Forms 2022.1] `template`: Optional name of the email template to use.
- PDF parameters (when a rendered PDF version of the form is attached to the email):
    - `use-pdf-template`: whether to use a PDF template, if the form has any. Defaults to `true`.
    - [DEPRECATED SINCE Orbeon Forms 2026.1] `pdf-template-name`: name of the PDF template to use. Use `pdf-template-names` instead.
    - [SINCE Orbeon Forms 2026.1] `pdf-template-names`: space-separated list of PDF template names. One PDF is attached for each name, in the order given. See [Multiple PDF attachments](#multiple-pdf-attachments) below.
    - `pdf-template-lang`: language of the PDF template to use.
- [\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md) `data-format-version`: The data format version for the XML data, if included as email attachment.
  - `4.0.0`: the default (which matches the backward compatibility format of the data, as stored in the database)
  - `4.8.0`
  - `2019.1.0`
- [\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md) `match`: The behaviour to use when multiple templates are found. Can be `first` (default) or `all`. If `all`, then all matching templates are used (i.e. one email per template is sent). If `first`, then only the first matching template is used.
- [SINCE Orbeon Forms 2024.1.1] `s3-store`: If `true`, email attachments are stored in an S3 bucket in addition to being sent via email. If `false` or absent, attachments are only sent via email. See [S3 storage](/form-runner/feature/s3.md) for more information about how to use this feature.
- `lang`: Optional language override for email template selection. If not provided, the current form language is used.

## Configuration properties

See [Email configuration properties](/configuration/properties/form-runner-email.md).

## Email template selection

When an email is sent, the following algorithm is used to determine what template is used:

- If no email template is defined, then a default title and body defined in the Form Runner resources is used.
- If at least one email template is defined:
  - All the templates that are for a specific language which doesn't correspond to the current language are filtered out.
  - If the `template` parameter is present, all the templates whose name doesn't match the value of the `template` parameter are filtered out.
  - [\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md) If the "Enable this template only if the following formula evaluates to true" XPath expression is present and evaluates to `false`, the template is filtered out.
  - [\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md) If more than one template is left and the `match` parameter is set to `first` or absent (the default), then the first template is used, following the order in which they are defined in the form. If the `match` parameter is set to `all`, then all remaining templates are used.
  - If no template is left, then no email is sent.

## Multiple PDF attachments

[SINCE Orbeon Forms 2026.1]

When a form has several PDF templates, the `pdf-template-names` parameter attaches several PDFs to the same email, one per template name. Names are separated by spaces, and the PDFs are attached in the order given:

```xml
<property as="xs:string"  name="oxf.fr.detail.process.send.acme.order">
    email(
        template           = "acme-order",
        pdf-template-names = "agreement confirmation"
    )
</property>
```

Notes:

- If both `pdf-template-name` and `pdf-template-names` are specified, the template from `pdf-template-name` comes first, followed by the templates from `pdf-template-names`. Duplicate names are ignored.
- The `use-pdf-template` and `pdf-template-lang` parameters apply to all the templates.
- The name of each PDF attachment is determined by the `oxf.fr.email.pdf.filename` property. With the default value of this property, all the PDFs attached to the same email have the same name. To give each PDF a distinct name, use the `fr:pdf-template-name()` function in this property, as described in [Attachment properties](/configuration/properties/form-runner-email.md#attachment-properties). When `s3-store` is enabled, attachments with the same name are stored under distinct keys: a number is added before the extension of each of them, for example `form-1.pdf`, `form-2.pdf`, `form-3.pdf`.

## Example

Here is an example of a `submit` process for the `acme`/`order` form, which saves data and then sends an email while specifying the email template to use as well as the XML data format version to use for the attachment:

```xml
<property as="xs:string"  name="oxf.fr.detail.process.submit.acme.order">
    require-uploads
    then validate-all
    then save
    then email(
        data-format-version = "4.0.0",
        template = "acme-order"
    )
    then success-message("email-success")
</property>
```

## See also

- [Form Runner email properties](/configuration/properties/form-runner-email.md)
- [Form Builder email settings](/form-builder/email-settings.md)
- [PDF templates](/form-runner/feature/pdf-templates.md)
- [S3 storage](/form-runner/feature/s3.md)