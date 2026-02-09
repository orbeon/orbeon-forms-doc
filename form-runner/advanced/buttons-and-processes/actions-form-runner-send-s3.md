# Form Runner send-s3 action

## Availability

[SINCE Orbeon Forms 2026.1]

## Introduction

The `send-s3` action stores form data as XML in an S3 bucket. This is useful when you want to store a copy of the form data in S3, alongside the database, for example for archival purposes.

This action uses the same S3 configuration as the [`email` action's S3 storage feature](/form-runner/feature/s3.md#bucket-configuration).

## Parameters

- `content`: The type of content to send. Currently, only `xml` is supported (and is the default).
- `s3-config`: The name of the S3 configuration to use. Defaults to `default`. See [S3 bucket configuration](/form-runner/feature/s3.md#bucket-configuration).
- `s3-path`: An XPath expression that determines the S3 key (path) under which the data is stored. The expression is evaluated against the XML data of the form.
- `data-format-version`: The data format version. Defaults to `4.0.0`. Possible values include `edge`, `2019.1.0`, `4.8.0`, and `4.0.0`.

### Using properties

If `s3-config` or `s3-path` are not provided as parameters, the following properties are used instead:

- `oxf.fr.send-s3.s3-config`
- `oxf.fr.send-s3.xml.s3-path`

A default value for `oxf.fr.send-s3.xml.s3-path` is provided:

```xml
<property as="xs:string" name="oxf.fr.send-s3.xml.s3-path.*.*">
    concat(
        fr:app-name(), '/',
        fr:form-name(), '/',
        fr:form-version(), '/',
        fr:process-dateTime(), '-',
        fr:document-id(), '/',
        'data.xml'
    )
</property>
```

This generates S3 keys such as:

```
my-app/my-form/1/2025-03-12T03:43:43.334-07:00-175279c65b1cc95d1b027f3c92c3beebc85c05aa/data.xml
```

## Example

Here is an example of a process that saves data to the database and then stores a copy of the XML data in S3:

```xml
<property as="xs:string"  name="oxf.fr.detail.process.save-final.my-app.my-form">
    require-uploads
    then validate-all
    then save
    then send-s3(
        s3-config = "form-submissions-bucket",
        s3-path   =
          "concat(
              fr:app-name(),         '/',
              fr:form-name(),        '/',
              fr:form-version(),     '/',
              fr:process-dateTime(), '-',
              fr:document-id(),      '/',
              'data.xml'
          )"
    )
    then success-message("save-success")
    recover error-message("database-error")
</property>
```

If the default S3 path works for you, the call can be simplified to:

```xml
send-s3(s3-config = "form-submissions-bucket")
```

## See also

- [S3 storage](/form-runner/feature/s3.md)
- [Form Runner actions](actions-form-runner.md)
- [Form Runner send action](actions-form-runner-send.md)
- [Form Runner email action](actions-form-runner-email.md)
