# Video component



## Availability

[SINCE Orbeon Forms 2023.1]

## What it does

The `fr:video` component shows a video. Like [fr:image](image.md), `fr:video` doesn't have an associated upload control and cannot be changed by the end-user of a form.

In addition, when in the Form Runner context, `fr:video` retrieves the resource while passing the `Orbeon-Form-Definition-Version` HTTP header when available.

## Basic usage

```xml
<fr:video bind="my-video-bind">
    <xf:label ref="$form-resources/my-video/label"/>
</fr:video>
```

The URL of the video is stored in instance data via a single-node binding, here using `bind`.

## See also

- [Image component](image.md)
- [Image attachment component](image-attachment.md)
- [Attachment component](attachment.md)