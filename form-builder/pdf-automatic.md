# Automatic PDF



## Introduction

The PDF is produced based on the appearance of the form in your web browser. This is similar to printing a read-only version of your form. As a form author, you do not need to take any action to enable this mode.

## Configuration of fonts

By default, the PDF engine picks standard fonts, which do not include all Unicode characters. This means for example that Chinese characters will not show by default. To change this, you need to obtain a Unicode font and configure its usage. Here is an example of configuration properties which achieve this:

```xml
<property
     as="xs:string"  
     name="oxf.fr.pdf.font.path.arial-unicode"                                  
     value="/path/to/font/ARIALUNI.TTF"/>
<property 
    as="xs:string"  
    name="oxf.fr.pdf.font.family.arial-unicode"                            
    value="Helvetica Neue"/>
```

Here:
 
- The font file is called `ARIALUNI.TTF` and the path `/path/to/font/ARIALUNI.TTF` points to it.
- The family is set to "Helvetica Neue" so that it matches the font set via CSS.
- The `arial-unicode` token is arbitrary and is used to link the two properties.

## See also

- [PDF Production](pdf-production.md).
- [PDF Templates](pdf-templates.md).
- [TIFF Production](../form-runner/feature/tiff-production.md).
