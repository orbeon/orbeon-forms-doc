# Automatic PDF header and footer configuration

## Introduction

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

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

## Example of header and footer configurations

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

## Header and footer configuration JSON format

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
    - `"css"`: a CSS string that is applied to the position
        - this is because custom CSS in a separate file doesn't impact positions in PDF headers and footers 

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
    - `"control-name"`: the name of the control to use, if the type is `"control-value"`
        - either one of `"control-name"` or `"control-css-class"` is required for the type `"control-value"` 
    - `"control-css-class"`: or the name of a custom CSS class for the control to use, if the type is `"control-value"`
        - if multiple controls has this CSS class, the first one is used
        - either one of `"control-name"` or `"control-css-class"` is required for the type `"control-value"`
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

## See also

- [Automatic PDF](pdf-automatic.md)
- [PDF templates](pdf-templates.md)
- [PDF production](pdf-production.md)