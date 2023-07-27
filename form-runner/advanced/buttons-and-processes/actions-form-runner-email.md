# Form Runner email action

##  Introduction

The `email` action sends an email with optionally XML form data, attachments, and PDF. It is automatically associated with the "Email" button, but can be called by an process.  

## Parameters

- [SINCE Orbeon Forms 2022.1] `template`: Every template you define in Form Builder has a name, and can be either for a specific language or for any language. When an email is sent, the following algorithm is used to determine what template is used:
    - All the templates that are for a specific language which doesn't correspond to the current language are filtered out.
    - If the `template` template parameter is present, all the templates whose name doesn't match the value of the `template` parameter are filtered out.
    - If exactly one template is left, then that template is used. If more than one template is left, then the first template is used, following the order in which they are defined in the form. If no template is left, then a default title and body defined in the Form Runner resources is used.
- [SINCE Orbeon Forms 2023.1] `data-format-version`: The data format version for the XML data, if included as email attachment.
    - `4.0.0`: the default (which matches the backward compatibility format of the data, as stored in the database)
    - `4.8.0`
    - `2019.1.0`

## Configuration properties

See [Email configuration properties](/configuration/properties/form-runner-email.md).

## See also

- [Form Runner actions](actions-form-runner.md)
- [Form Runner save action](actions-form-runner-save.md)
- [PDF templates](/form-runner/feature/pdf-templates.md)
