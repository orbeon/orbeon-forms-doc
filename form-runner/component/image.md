# Image component

## Availability

[SINCE Orbeon Forms 2016.1]

## What it does

The `fr:image` component shows an image. Unlike [fr:image-attachment](image-attachment.md), `fr:image` doesn't have an associated upload control and cannot be changed by the end-user of a form.

## Basic usage

```xml
<fr:image bind="my-image-bind">
    <xf:label ref="$form-resources/my-image/label"/>
</fr:image>
```

The URL of the image is stored in instance data via a single-node binding, here using `bind`.

## Form Runner usage

When in the Form Runner context, `fr:image` retrieves the resource while passing the `Orbeon-Form-Definition-Version` HTTP header when available.

## Compatibility notes

Prior to Orbeon Forms 2016.1, a plain `xf:output` was produced by Form Builder to achieve similar, but less explicit, behavior.

## See also

- [Image attachment component](image-attachment.md)
- [Attachment component](attachment.md)
- [Video component](video.md)