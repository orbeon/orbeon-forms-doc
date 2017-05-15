# Attachment Component

<!-- toc -->

## What it does

This component represents an attachment to a form.

## Accepted mediatypes

By default the component accepts any mediatype. 

[SINCE Orbeon Forms 2017.1]

If an [`upload-mediatypes` custom constraint](../../xforms/xpath/extension-validation.md#xxfupload-mediatypes) is
present and not blank, it is applied to the enclosed upload control.

Otherwise, if an `accept` attribute or associated `oxf.xforms.xbl.fr.attachment.accept` property is defined and not
blank, it is used. Example of system-wide property:

```xml
<property
    as="xs:string"  
    name="oxf.xforms.xbl.fr.attachment.accept"
    value="application/pdf"/>
```

Example of attribute on the control:
    
```xml
<fr:attachment
    bind="my-file"
    accept="application/pdf">
```

Otherwise, any mediatype is allowed.

## Events

[SINCE Orbeon Forms 2017.1]

This component dispatches the following events following the enclosed upload control:

- `xxforms-upload-start`
- `xxforms-upload-cancel`
- `xxforms-upload-done`
- `xxforms-upload-error`
