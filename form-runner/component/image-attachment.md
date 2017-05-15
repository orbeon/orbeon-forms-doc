# Image Attachment Component

<!-- toc -->

## What it does

This component represents an attachment to a form which has to contain an image. The image is displayed by the component
when attached.  

## Accepted mediatypes

By default the component accepts `image/*` mediatypes. 

[SINCE Orbeon Forms 2017.1]

If an [`upload-mediatypes` custom constraint](../../xforms/xpath/extension-validation.md#xxfupload-mediatypes) is
present and not blank, it is used applied to the enclosed upload control. If missing, the default is `image/*`.
