# Automatic PDF

## Introduction

The PDF is produced based on the appearance of the form in your web browser. This is similar to printing a read-only version of your form. As a form author, you do not need to take any action to enable this mode.

## Font configuration and embedding

These properties allow specifying fonts to embed in PDF files. The `oxf.fr.pdf.font.path` property ends with an identifier for the font (here `vera`). It specifies the path to the font file. Optionally, the `oxf.fr.pdf.font.family` property ending with the same identifier (here `vera`) allows overriding the font family.

*NOTE: The path to the font file must be an absolute path on the filesystem. It cannot be just a relative path pointing to the Orbeon Forms resources, as is the case with for example CSS files.*

```xml
<property
    as="xs:string"
    name="oxf.fr.pdf.font.path.vera"
    value="/absolute/path/to/font-file.ttf"/>

<property
    as="xs:string"
    name="oxf.fr.pdf.font.family.vera"
    value="Arial"/>
```

To change the main font, you must map to the Helvetica Neue font. For example;

```xml
<property
    as="xs:string"
    name="oxf.fr.pdf.font.path.my-font"
    value="/path/to/font.ttf"/>

<property
    as="xs:string"
    name="oxf.fr.pdf.font.family.my-font"
    value="Helvetica Neue"/>
```

By default, the PDF engine picks standard fonts, which do not include all Unicode characters. This means for example that special characters, or Chinese characters, will not show by default in the resulting PDF file. To change this, you need to obtain a Unicode font and configure its usage. Here is an example of configuration properties which achieve this, assuming you have a Unicode font file called `arialuni.ttf`:

```xml
<property
     as="xs:string"  
     name="oxf.fr.pdf.font.resource.arial-unicode"                                  
     value="/absolute/path/to/arialuni.ttf"/>
<property 
    as="xs:string"  
    name="oxf.fr.pdf.font.family.arial-unicode"                            
    value="Helvetica Neue"/>
```

[SINCE Orbeon Forms 2019.2]

In addition to an absolute path to the font file, you can specify a path relative to the Orbeon Forms resources. Instead of `oxf.fr.pdf.font.path.*`, use `oxf.fr.pdf.font.resource.*`:

```xml
<property
     as="xs:string"  
     name="oxf.fr.pdf.font.resource.arial-unicode"                                  
     value="/forms/resources/arialuni.ttf"/>
<property 
    as="xs:string"  
    name="oxf.fr.pdf.font.family.arial-unicode"                            
    value="Helvetica Neue"/>
```

In this example, the path `/forms/resources/arialuni.ttf` is relative to the Orbeon Forms [resources](/xml-platform/resources/resource-managers.md), for example under `WEB-INF/resources`.

## See also

- [PDF Production](pdf-production.md).
- [PDF Templates](pdf-templates.md).
- [TIFF Production](/form-runner/feature/tiff-production.md).
