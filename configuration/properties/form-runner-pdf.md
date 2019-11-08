# PDF configuration properties

## Custom PDF filename

[SINCE Orbeon Forms 4.9]

The following property dynamically controls the name of the PDF file produced on the Detail Page. By default, if the property value is blank, the PDF filename is a random id assigned to the current form session.

```xml
<property
    as="xs:string"
    name="oxf.fr.detail.pdf.filename.*.*"
    value=""/>
```

The value of the property, if not empty, is an XPath expression which runs in the context of the root element of the XML document containing form data. The trimmed string value of the result of the expression is used to determine the filename.

Example:

```xml
<property
    as="xs:string"
    name="oxf.fr.detail.pdf.filename.*.*"
    value="//customer-id"/>
```

If the form contains a `customer-id` field, the PDF filename will be the value of that field followed by `.pdf`. If the field is blank, the default, random id filename is used, as if the property had not been specified. 

## Hyperlinks in automatic mode

[SINCE Orbeon Forms 4.6]

The following property controls whether hyperlinks are enabled in the generated PDF. By default, they are enabled:

```xml
<property
    as="xs:boolean"
    name="oxf.fr.detail.pdf.hyperlinks.*.*"
    value="true"/>
```

When set to `true`:

* HTTP and HTTPS URLs in input field and text areas are automatically hyperlinked.
* Hyperlinks in rich text controls are preserved.
* Hyperlinks in the rest of the form, if any, are preserved.

When set to `false`:

* HTTP and HTTPS URLs in input field and text areas are not hyperlinked, but placeholders are added.
* Hyperlinks in rich text controls are removed and placeholders are left.
* Hyperlinks in the rest of the form, if any, are removed and placeholders are left.
* Placeholders consist of an HTML `<a>` without an `href` attribute. This helps with CSS styling.

For example, the default style for hyperlinks only highlights and underlines `<a>` elements with an `href` attribute:

```css
a[href] {
    text-decoration: underline;
    &:link, &:visited {
        color: @linkColor !important;
    }
}
```

## Barcode

[Orbeon Forms PE] The following property specifies whether a barcode must be included on PDF files produced from a PDF template. Adding a barcode to a PDF produced without a PDF template isn't supported at this point (see [RFE #2190](https://github.com/orbeon/orbeon-forms/issues/2190)).

```xml
<property
    as="xs:boolean"
    name="oxf.fr.detail.pdf.barcode.*.*"
    value="false"/>
```

## Font embedding in automatic mode

These properties allow specifying fonts to embed in PDF files. The `oxf.fr.pdf.font.path` property ends with an identifier for the font (here `vera`). It specifies the path to the font file. Optionally, the oxf.fr.pdf.font.family property ending with the same identifier (here `vera`) allows overriding the font family.

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

## Font embedding in template mode

In template mode, fonts can be specified to provide glyphs which are not present in the PDF's original font(s). Several fonts can be specified, separated by spaces:

```xml
<property
    as="xs:string"
    name="oxf.fr.pdf.template.font.paths"
    value="/path/to/font1.ttf /path/to/font2.ttf"/>
```

## Disabling the PDF button when form is invalid

[BEFORE Orbeon Forms 4.2]

With version 4.0 and earlier, the PDF button is always disabled if invalid data is present in the form.

[SINCE Orbeon Forms 4.2]

The PDF button is always enabled, allowing users to generate a PDF for the current form, even if some data in the form is invalid. If instead, you wish to disable the PDF button when the form is invalid, set the following property to `true` (it is set to `false` by default):

```xml
<property
    as="xs:boolean"
    name="oxf.fr.detail.pdf.disable-if-invalid.*.*"
    value="false"/>
```

## Automatic PDF page size and orientation

[SINCE Orbeon Forms 2019.2]

Available page orientations:

- `portrait` (default)
- `landscape`

```xml
<property 
    as="xs:string" 
    name="oxf.fr.detail.rendered-page-orientation.*.*" 
    value="portrait"/>
```

Available page sizes:

- `letter` (default)
- `a4`
- `legal`

```xml
<property 
    as="xs:string" 
    name="oxf.fr.detail.rendered-page-size.*.*" 
    value="letter"/>
```

These can also be configured for a particular form in Form Builder's Form Settings dialog.

## See also
 
- Blog post: [New layout choices for PDF and browser views](https://blog.orbeon.com/2019/11/new-layout-choices-for-pdf-and-browser.html)
