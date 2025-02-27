# Form Runner email action

[Orbeon Forms PE only]

##  Introduction

The `email` action sends an email with optionally XML form data, attachments, and PDF. It is automatically associated with the "Email" button, but can be called by an process.  

## Parameters

- [SINCE Orbeon Forms 2022.1] `template`: Optional name of the email template to use.
- [\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md) `data-format-version`: The data format version for the XML data, if included as email attachment.
  - `4.0.0`: the default (which matches the backward compatibility format of the data, as stored in the database)
  - `4.8.0`
  - `2019.1.0`
- [\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md) `match`: The behaviour to use when multiple templates are found. Can be `first` (default) or `all`. If `all`, then all matching templates are used (i.e. one email per template is sent). If `first`, then only the first matching template is used.
- [SINCE Orbeon Forms 2024.1.1] `s3-store`: If `true`, email attachments are stored in an S3 bucket in addition to being sent via email. If `false` or absent, attachments are only sent via email.
    - `s3-config`: Specifies the name of the S3 configuration to use. The S3 configuration properties are described below.
    - `s3-path`: Defines the XPath expression used to determine the storage path for attachments. The path is relative to the root of the S3 bucket. It is evaluated against the XML data of the form. If the XPath expression evaluates to an empty string, attachments are stored at the root of the S3 bucket. This expression has the same constraints as [the PDF, TIFF and XML attachments filenames](https://doc.orbeon.com/configuration/properties/form-runner/form-runner-detail-page/form-runner-email#attachment-properties).
    - Fallback behavior:
      - If `s3-config` or `s3-path` are not provided as parameters, the properties `oxf.fr.email.s3-config.*.*` or `oxf.fr.email.s3-path.*.*` are used instead.
      - If a property is not found for the S3 configuration name, `default` is used as the configuration name.
      - A default value for the `oxf.fr.email.s3-path.*.*` property is provided, which uses the app name, form name, etc. to generate a path. It is however recommended to provide a custom path that fits your needs.

## Email template selection

When an email is sent, the following algorithm is used to determine what template is used:
- If no email template is defined, then a default title and body defined in the Form Runner resources is used.
- If at least one email template is defined:
  - All the templates that are for a specific language which doesn't correspond to the current language are filtered out.
  - If the `template` parameter is present, all the templates whose name doesn't match the value of the `template` parameter are filtered out.
  - [\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md) If the "Enable this template only if the following formula evaluates to true" XPath expression is present and evaluates to `false`, the template is filtered out.
  - [\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md) If more than one template is left and the `match` parameter is set to `first` or absent, then the first template is used, following the order in which they are defined in the form. If the `match` parameter is set to `all`, then all remaining templates are used.
  - If no template is left, then no email is sent.

## Configuration properties

### Email properties

See [Email configuration properties](/configuration/properties/form-runner-email.md).

### S3 properties

An S3 configuration consists of a set of properties that follow the naming convention:

```
oxf.fr.s3.[configuration-name].[s3-property]
```

- `[configuration-name]`: The name of the S3 configuration.
- `[s3-property]`: One of the following properties:
  - `endpoint`: The S3 endpoint (default: `s3.amazonaws.com`)
  - `region`: The AWS region (e.g. `eu-south-1` or `us-east-1`)
  - `bucket`: The name of the S3 bucket.
  - `accesskey`: The access key for authentication.
  - `secretaccesskey`: The secret access key for authentication.

No default S3 configuration is provided. You must define at least one S3 configuration to use the `s3-store` parameter. For instance, your S3 configuration properties could look as follows.

```xml
<property as="xs:string"  name="oxf.fr.s3.default.endpoint"        value="s3.amazonaws.com"/>
<property as="xs:string"  name="oxf.fr.s3.default.region"          value="us-east-1"/>
<property as="xs:string"  name="oxf.fr.s3.default.bucket"          value="orbeon"/>
<property as="xs:string"  name="oxf.fr.s3.default.accesskey"       value="YYDLE3Z65JK7SZLB5RXB"/>
<property as="xs:string"  name="oxf.fr.s3.default.secretaccesskey" value="1csA5grUiF/TcAD7lOkWd0KBrYLDhQtK5sWl163U"/>
```

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
