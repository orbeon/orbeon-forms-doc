# Image attachment component



## What it does

This component represents an attachment to a form which has to contain an image. The image is displayed by the component
when attached.  

## Accepted mediatypes

By default the component accepts `image/*` mediatypes. 

[SINCE Orbeon Forms 2017.1]

If an [`upload-mediatypes` custom constraint](../../xforms/xpath/extension-validation.md#xxfupload-mediatypes) is
present and not blank, it is used applied to the enclosed upload control. If missing, the default is `image/*`.

## Events

[SINCE Orbeon Forms 2017.1]

This component dispatches the following events following the enclosed upload control:

- `xxforms-upload-start`
- `xxforms-upload-cancel`
- `xxforms-upload-done`
- `xxforms-upload-error`

## See also

- [Image component](image.md)
- [Attachment component](attachment.md)