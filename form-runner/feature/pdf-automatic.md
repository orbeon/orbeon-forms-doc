# Automatic PDF

## Introduction

The PDF is produced based on the appearance of the form in your web browser. This is similar to printing a read-only version of your form. As a form author, you do not need to take any action to enable this mode. In addition, a header and footer is provided by default.

[//]: # (TODO: Add a screenshot.)

## Header and footer configuration

### Introduction

[SINCE Orbeon Forms 2023.1]

By default, Form Runner produces automatic PDF files with the following header and footer:

- Header: Form Runner logo, form title
- Footer: Form title, page number

[//]: # (TODO: Add a screenshot.)

Starting Orbeon Forms 2023.1, header and footers are entirely configurable and you can control exactly what is shown in all six header or footer positions:

- Header left
- Header center
- Header right
- Footer left
- Footer center
- Footer right

Additionally, you can configure different headers and footers for the first page and subsequent pages, as well as for odd and even pages (also known as "left" and "right" pages).

For each position, you can specify:

- that it should remain blank
- that it should use the default
- that it should use custom content

Custom content is based on the Form Runner system of *templates* with parameters. For more information, see [Template syntax](/form-builder/template-syntax.md).

### Example of header and footer configurations

The default global configuration is expressed in JSON Format and is as follows:

```json
{
  "pages": {
    "all": {
      "header": {
        "left": {
          "values": {
            "_": "{$fr-logo}"
          }
        },
        "center": {
          "values": {
            "_": "{$fr-form-title}"
          }
        }
      },
      "footer": {
        "left": {
          "values": {
            "_": "{$fr-form-title}"
          }
        },
        "center": {
          "values": {
            "_": "{$fr-page-number} / {$fr-page-count}"
          }
        }
      }
    }
  },
  "parameters": {
    "fr-logo": {
      "type": "image"
    },
    "fr-form-title": {
      "type": "form-title"
    },
    "fr-page-number": {
      "type": "page-number",
      "format": "decimal"
    },
    "fr-page-count": {
      "type": "page-count",
      "format": "decimal"
    }
  }
}
```

The following is an example of custom configuration that updates the default configuration to add a submission date in the footer's right position:

```xml
<property as="xs:string" name="oxf.fr.detail.pdf.header-footer.*.*">
    {
      "pages": {
        "all": {
          "footer": {
            "right": {
              "values": {
                "_": "Submitted on: {$current-dateTime}",
                "fr": "Envoyé le: {$current-dateTime}"
              }
            }
          }
        }
      },
      "parameters": {
        "current-dateTime": {
          "type": "formula",
          "value": "format-dateTime(current-dateTime(), '[D]/[M]/[Y] [h]:[m]:[s] [P,*-2]', xxf:lang(), (), ())"
        }
      }
    }
</property>
```

### Header and footer configuration JSON format

The top-level is a JSON object with the following properties:

- `"pages"`: contains zero or more page configurations
- `"parameters"`: contains zero or more parameters

The `"pages"` property is a JSON object with the following properties:

- `"all"`: configuration for all pages
- `"first"`: optional configuration for the first page only
- `"odd"`: optional configuration for odd pages
- `"even"`: optional configuration for even pages

Each page configuration is a JSON object with the following properties:

- `"header"`: configuration for the header
- `"footer"`: configuration for the footer

Each header or footer configuration is a JSON object with the following properties:

- `"left"`: configuration for the left position
- `"center"`: configuration for the center position
- `"right"`: configuration for the right position

Each position configuration is:

- `"none"`: the position is blank
- `"inherit"`: the position is the same as the default
    - this is the same behavior if the position is not specified, but it is more explicit 
- a JSON object with the following properties:
    - `"values"`: a JSON object with the following properties:
        - each property name represents a language code
            - the language code can be `"_"` to represent any language
        - each property value is a template string
    - `"visible"`: a formula that determines whether the position is visible

The `"parameters"` property is a JSON object with the following properties:

- each property name represents a unique parameter name
- each property value is a JSON object with the following properties:
    - `"type"`: the type of parameter, one of:
        - `"formula"`: the parameter value is a formula (XPath expression) (with the required `"value"` parameter)
        - `"control-value"`: the parameter value is the value of a control (with the required `"control-name"` parameter)
        - `"form-title"`: show the form title (no additional parameters)
        - `"image"`: show an image (the form logo, no additional parameters)
        - `"page-count"`: show the page count (with the optional `"format"` parameter)
        - `"page-number"`: show the page number (with the optional `"format"` parameter)
        - links (no additional parameters)
            - `"link-to-edit-page"`
            - `"link-to-view-page"`
            - `"link-to-new-page"`
            - `"link-to-summary-page"`
            - `"link-to-home-page"`
            - `"link-to-forms-page"`
            - `"link-to-admin-page"`
            - `"link-to-pdf"` 
    - `"value"`: the required formula (XPath expression) to evaluate, if the type is `"formula"`
    - `"control-name"`: the required name of the control to use, if the type is `"control-value"`
    - `"format"`: the optional format to use, if the type is `"page-count"` or `"page-number"`
        - defaults to `"decimal"` 

The `"format"` property is a JSON string with the following format, as used in CSS:

- `"decimal"`
- `"decimal-leading-zero"`
- `"lower-roman"`
- `"upper-roman"`
- `"lower-greek"`
- `"lower-alpha"`
- `"upper-alpha"`

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

[SINCE Orbeon Forms 2023.1]

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

## See also

- [PDF Production](pdf-production.md)
- [PDF Templates](pdf-templates.md)
- [PDF configuration properties](/configuration/properties/form-runner-pdf.md)
- [Testing PDF production](/form-builder/pdf-test.md)
- [TIFF Production](/form-runner/feature/tiff-production.md)
- [Sending PDF and TIFF content: Controlling the format](/form-runner/advanced/buttons-and-processes/actions-form-runner-send.md)
- Blog post: [New layout choices for PDF and browser views](https://blog.orbeon.com/2019/11/new-layout-choices-for-pdf-and-browser.html)