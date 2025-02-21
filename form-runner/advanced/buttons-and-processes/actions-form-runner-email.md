# Form Runner email action

[Orbeon Forms PE only]

##  Introduction

The `email` action sends an email with optionally XML form data, attachments, and PDF. It is automatically associated with the "Email" button, but can be called by an process.  

## Parameters

- [SINCE Orbeon Forms 2022.1] `template`: Every template you define in Form Builder has a name, and can be either for a specific language or for any language. When an email is sent, the following algorithm is used to determine what template is used:
    - All the templates that are for a specific language which doesn't correspond to the current language are filtered out.
    - If the `template` template parameter is present, all the templates whose name doesn't match the value of the `template` parameter are filtered out.
    - If exactly one template is left, then that template is used.
    - If no template is left, then a default title and body defined in the Form Runner resources is used.
    - [\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md) If more than one template is left and the `match` parameter is set to `first` or absent, then the first template is used, following the order in which they are defined in the form. If the `match` parameter is set to `all`, then all remaining templates are used.
- [\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md) `data-format-version`: The data format version for the XML data, if included as email attachment.
    - `4.0.0`: the default (which matches the backward compatibility format of the data, as stored in the database)
    - `4.8.0`
    - `2019.1.0`
- [\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md) `match`: The behaviour to use when multiple templates are found. Can be `first` (default) or `all`. If `all`, then all matching templates are used (i.e. one email per template is sent). 

## Configuration properties

See [Email configuration properties](/configuration/properties/form-runner-email.md).

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
