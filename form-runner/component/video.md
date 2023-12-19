# Video component

## Availability

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

## What it does

The `fr:video` component shows a video. Like [fr:image](image.md), `fr:video` doesn't have an associated upload control and cannot be changed by the end-user of a form.

![Video component](images/xbl-video.png)

## Basic usage

```xml
<fr:video bind="my-video-bind">
    <xf:label ref="$form-resources/my-video/label"/>
</fr:video>
```

The URL of the video is stored in instance data via a single-node binding, here using `bind`.

## Form Runner usage

When in the Form Runner context, `fr:video` retrieves the resource while passing the `Orbeon-Form-Definition-Version` HTTP header when available.

## Limitations

In PDF output, the Video component currently doesn't show any content. Ideally, it should show a still of the video.

## See also

- [Video attachment component](video-attachment.md)
- [Image component](image.md)
- [Image attachment component](image-attachment.md)
- [Attachment component](attachment.md)