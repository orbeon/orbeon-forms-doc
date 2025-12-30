# Image annotation

## Rationale

The image annotation component allows users to select an image and annotate it. Both the original image users select and the image with the annotation are saved as part of the data.

![](../../.gitbook/assets/xbl-image-annotation.png)

## Configuration

[\[SINCE Orbeon Forms 2023.1\]](../../release-notes/orbeon-forms-2023.1.md) You can change the start stroke color with the following property:

```xml
<property 
    as="xs:string"  
    name="oxf.xforms.xbl.fr.wpaint.start-stroke-color.*.*"      
    value="#FF8C00"/>
```

## In Form Builder

To add this component to your form, in the toolbar, in the _Attachments_ section, choose _Image annotation_.

## In XForms

When using this component in your own XForms (rather than through Form Builder), you must bind it to an element that is either empty, or contains two elements `<image>` and `<annotation>`. If the element is empty, the two elements `<image>` and `<annotation>` are automatically added by the component.
