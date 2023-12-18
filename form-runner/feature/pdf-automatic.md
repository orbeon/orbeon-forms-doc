# Automatic PDF

## Introduction

The PDF is produced based on the appearance of the form in your web browser. This is similar to printing a read-only version of your form. As a form author, you do not need to take any action to enable this mode. In addition, a header and footer is provided by default.

[//]: # (TODO: Add a screenshot.)

## Header and footer configuration

See [Header and footer configuration](pdf-automatic-header-footer.md).

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

## PDF color mode

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

By default, the PDF is output with most colors removed and a black-and-white scheme. With the `oxf.fr.detail.pdf.color-mode`, you can change that default to allow field colors to be used. This impacts the following form controls:

- Explanatory Text
- Formatted Text
- Calculated Value (if the value is in HTML format)

```xml
<property
    as="xs:string"
    name="oxf.fr.detail.pdf.color-mode.*.*"
    value="keep-field-colors"/>
```

## Accessibility and PDF/A support

[SINCE Orbeon Forms 2022.1.5 and 2023.1]

Experimentally, the following properties allow enabling accessibility and PDF/A support:

```xml
<property as="xs:boolean" name="oxf.fr.pdf.accessibility" value="true"/>
<property as="xs:string"  name="oxf.fr.pdf.pdf/a"         value="3u"/>
```

- `oxf.fr.pdf.accessibility`
    - enable or disable accessibility support
    - values: `true` or `false`
    - default: `false`
- `oxf.fr.pdf.pdf/a`
    - enable or disable PDF/A support
    - values:
        - `none`: disabled
        - `1a`: PDF/A-1a – Level A (accessible) conformance
        - `1b`: PDF/A-1b – Level B (basic) conformance
        - `2a`: PDF/A-2a
        - `2b`: PDF/A-2b
        - `2u`: PDF/A-2b plus Unicode
        - `3a`: PDF/A-3a
        - `3b`: PDF/A-3b
        - `3u`: PDF/A-3b plus Unicode
    - default: `none`

Note that the values above are simply passed to the PDF renderer library used by Orbeon Forms. There is currently no guarantee of compliance with the PDF/A standard. 

## See also

- [Automatic PDF header and footer configuration](pdf-automatic-header-footer.md)
- [Automatic PDF styling and CSS](/form-runner/styling/automatic-pdf.md)
- [PDF Production](pdf-production.md)
- [PDF Templates](pdf-templates.md)
- [PDF configuration properties](/configuration/properties/form-runner-pdf.md)
- [Testing PDF production](/form-builder/pdf-test.md)
- [TIFF Production](/form-runner/feature/tiff-production.md)
- [Sending PDF and TIFF content: Controlling the format](/form-runner/advanced/buttons-and-processes/actions-form-runner-send.md)
- Blog post: [New layout choices for PDF and browser views](https://blog.orbeon.com/2019/11/new-layout-choices-for-pdf-and-browser.html)