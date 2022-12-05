# Automatic PDF

## Introduction

The PDF is produced based on the appearance of the form in your web browser. This is similar to printing a read-only version of your form. As a form author, you do not need to take any action to enable this mode.

## Font configuration and embedding

### Overview

These properties allow specifying fonts to embed in PDF files. You do this with 2 properties, which are tied together by a name you choose, and which doesn't have any significance outside the `properties-local.xml`. For instance, say you want to use the [Roboto](https://fonts.google.com/specimen/Roboto) font. You first map the family name, which you use in your CSS (here `Roboto`, with an uppercase `R`) to the name you chose (here `roboto`, with a lowercase `r`):

```xml
<property
    as="xs:string"
    name="oxf.fr.pdf.font.family.roboto"
    value="Roboto"/>
```

Then you indicate where the TTF file for the name you chose is located on disk, using either one of the following two properties. Use the first [SINCE Orbeon Forms 2019.2] if you want to place your font inside the Orbeon Forms `WIB-INF/resources` directory, or use the second if you want to place it somewhere else, referring to the file using an absolute path.

```xml
<property
     as="xs:string"  
     name="oxf.fr.pdf.font.resource.roboto"                                  
     value="/forms/resources/Roboto-Medium.ttf"/>
<property
    as="xs:string"
    name="oxf.fr.pdf.font.path.roboto"
    value="/absolute/path/to/Roboto-Medium.ttf"/>
```

### Changing the default font

To change the main font, you must map your TTF file to the `Helvetica Neue` family. You often need to do this if you are using characters that are not included in the default font used by the PDF engine, for instance to have Chinese characters show in the PDF. To change this, you need to obtain a Unicode font, and configure it as follows, here assuming the file is named `arialuni.ttf`:

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

### New default font

[SINCE Orbeon Forms 2022.1]

Orbeon Forms now includes the [Inter font](https://rsms.me/inter/) by default. This provides more characters out of the box (see [Language support](https://rsms.me/inter/#languages)).

## See also

- [PDF Production](pdf-production.md)
- [PDF Templates](pdf-templates.md)
- [Testing PDF production](pdf-test.md)
- [TIFF Production](/form-runner/feature/tiff-production.md)
