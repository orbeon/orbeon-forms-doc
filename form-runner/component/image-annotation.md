# Image Annotation Component

<!-- toc -->

## Rationale

The image annotation component allows users to select an image and annotate it. Both the original image users select and the image with the annotation are saved as part of the data.

![](images/xbl-image-annotation.png)

## Browser support

This component uses [wPaint][2], which requires support for the [HTML5 canvas][3] in the web browser. Most browsers have support for the canvas, but note that Internet Explorer [IE9 or later is required][4] (this feature will not work with IE7 and IE8).

## In Form Builder

To add this component to your form, in the toolbar, in the _Attachments_ section, choose _Image annotation_.

## In XForms

When using this component in your own XForms (rather than through Form Builder), you must bind it to an element that is either empty, or contains two elements `<image>` and `<annotation>`. If the element is empty, the two elements `<image>` and `<annotation>` are automatically added by the component.

[1]: http://wiki.orbeon.com/forms/_/rsrc/1375574398193/doc/developer-guide/xbl-components/image-annotation/Screen%20Shot%202013-08-03%20at%204.59.16%20PM.png
[2]: https://github.com/websanova/wPaint
[3]: http://en.wikipedia.org/wiki/Canvas_element
[4]: http://en.wikipedia.org/wiki/Canvas_element#Support
