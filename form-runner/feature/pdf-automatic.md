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

Then you indicate where the TTF file for the name you chose is located on disk, using either one of the following two properties. Use the first [SINCE Orbeon Forms 2019.2] if you want to place your font inside the Orbeon Forms `WEB-INF/resources` directory, or use the second if you want to place it somewhere else, referring to the file using an absolute path.

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

By default, the PDF is output with most colors removed and a black-and-white scheme. With the `oxf.fr.detail.pdf.color-mode` property, you can change that default. This impacts the following form controls:

- Explanatory Text
- Formatted Text
- Calculated Value (if the value is in HTML format)

Allowed values:

- `black-and-white` (default): Most colors are removed and a black-and-white scheme is used.
- `keep-field-colors`: Field colors are preserved in the PDF output.
- [\[SINCE Orbeon Forms 2025.1\]](/release-notes/orbeon-forms-2025.1.md) `color`: Field colors are preserved, and CSS variables related to colors can be overridden in a custom `@media print` section and will be used by the PDF generator. For more information, see [Overriding CSS variables](/form-runner/styling/css.md#overriding-css-variables).

```xml
<property
    as="xs:string"
    name="oxf.fr.detail.pdf.color-mode.*.*"
    value="keep-field-colors"/>
```

## Zero width no-break space characters

[\[SINCE Orbeon Forms 2026.1\]](/release-notes/orbeon-forms-2026.1.md)

U+FEFF characters (zero width no-break space, byte order mark) are removed from the text of the PDF, as some PDF/A levels don't allow them. To prevent a line break, use a no-break space (U+00A0) instead.

## Accessibility and PDF/A support

[SINCE Orbeon Forms 2022.1.5 and 2023.1]

The following properties allow enabling accessibility and PDF/A support:

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

### PDF/A conformance

[SINCE Orbeon Forms 2025.1.3]

With one of the levels above, the automatic PDF is a PDF/A file. Orbeon Forms tests this with the [veraPDF](https://verapdf.org/) validator, for the eight levels. Before Orbeon Forms 2025.1.3, the output didn't pass validation.

With PDF/A enabled, the PDF differs from a regular PDF as follows:

- __All levels:__ the document includes an sRGB color profile.
- __PDF/A-1a and 1b__, which don't allow transparency: images with transparency are drawn on a white background.
- __PDF/A-2a and 3a__, which don't allow Unicode Private Use Area characters: the file type icon of attachments is hidden, the star of required fields uses a regular character, and the Star Rating control only shows its value as text, for example "4 / 5".
- __All levels__, [SINCE Orbeon Forms 2026.1]: images which PDF/A doesn't allow as they are get converted to 8-bit sRGB. This includes CMYK images, 16-bit images, and most PNG and TIFF images with their own color profile. Their colors can change slightly.

[SINCE Orbeon Forms 2026.1] Three changes apply to all automatic PDFs, with or without PDF/A, and each removes a cause of validation failure: U+FEFF characters are removed from the text (see [Zero width no-break space characters](#zero-width-no-break-space-characters)), the creation date uses the Gregorian calendar whatever the locale of the server, and the PDF no longer contains an empty interactive form.

### PDF/A limitations

`oxf.fr.pdf.pdf/a` only applies to automatic PDFs. With a [PDF template](pdf-templates.md), the property has no effect, and Orbeon Forms does nothing to make the PDF a PDF/A file.

An automatic PDF doesn't pass validation in the following cases:

- __Long documents, PDF/A-1a and 1b:__ a section or a text which spans a few dozen pages. For long documents, use PDF/A-2 or PDF/A-3.
- __Private Use Area characters, PDF/A-2a and 3a:__ a character between U+E000 and U+F8FF which one of the fonts of the PDF contains, for example an icon from an icon font in an HTML label. If no font contains the character, the file is valid, but the character doesn't show.
- __Very long strings:__ a string of more than 65,535 bytes with PDF/A-1, or 32,767 bytes with PDF/A-2 and PDF/A-3, for example a URL of that length in a field or in a link. We consider this an extreme and rare case.

A valid file can still miss content: as in a regular PDF, a character which none of the configured fonts contains doesn't show correctly (see [Font configuration and embedding](#font-configuration-and-embedding)). Also, Orbeon Forms only validates with veraPDF, and another validator can judge some points differently. If you archive PDF/A files, validate them with your own forms and data.

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
- Blog post: [PDF Production](https://www.orbeon.com/2025/02/pdf-production)