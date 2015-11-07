# Image Component

<!-- toc -->

## Availability

[SINCE Orbeon Forms 4.11]

## What it does

The `fr:image` component shows an image. Unlike [fr:image-attachment](FIXME Form Runner ~ XBL Components ~ Image Attachment), `fr:image` doesn't have an associated upload control and cannot be changed by the end-user of a form.

In addition, when in the Form Runner context, `fr:image` retrieves the resource while passing the `Orbeon-Form-Definition-Version` HTTP header when available.

*NOTE: Prior to Orbeon Forms 4.11, a plain `xf:output` was produced by Form Builder to achieve similar, but less explicit, behavior.*

## Basic usage

```xml
<fr:image bind="my-image-bind">
    <xf:label ref="$form-resources/my-image/label"/>
</fr:image>
```

The URL of the image is stored in instance data via a single-node binding, here using `bind`.
