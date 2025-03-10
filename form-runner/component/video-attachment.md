# Video attachment component

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

## What it does

This component represents an attachment to a form which has to contain a video. The video is displayed by the component
when attached.

![Video attachment component](images/xbl-video-attachment.png)

## Accepted mediatypes

By default the component accepts `video/*` mediatypes. 

If an [`upload-mediatypes` custom constraint](/contributors/extension-validation.md#xxfupload-mediatypes) is
present and not blank, it is used applied to the enclosed upload control. If missing, the default is `video/*`.

## Events

This component dispatches the following events following the enclosed upload control:

- `xxforms-upload-start`
- `xxforms-upload-cancel`
- `xxforms-upload-done`
- `xxforms-upload-error`

## See also

- [Image component](image.md)
- [Video component](video.md)
- [Attachment component](attachment.md)