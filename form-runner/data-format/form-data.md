# Form Data Format

<!-- toc -->

## Introduction

Form Runner loads and saves data in XML format. This page describes the format used.

## Basics

As you create a form definition with Form Builder, an XML representation for the data to capture is automatically created. It is organized as follows:

* A root element:
    `<form>`
* Within that element, for each section, a sub-element named after the section name:
    `<address>`
* Within a section element, a sub-element for each control in the section, named after the control name:
    `<first-name>`
* Within each control element, the value of the control is stored:
    `<first-name>Alice</first-name>`

Example:

```xml
<form>
    <details>
        <title/>
        <author/>
        <language/>
        <link/>
        <rating/>
        <publication-year/>
        <review/>
        <image filename="" mediatype="" size=""/>
    </details>
    <notes>
        <note/>
    </notes>
</form>
```

## Nested sections

TODO

## Repeated grids and sections

TODO: Specify how nested grids and nested sections are represented.

_NOTE: Non-repeated grids do not create containing elements._

## Attachments

For attachments, the control element is slightly different:

- the text content is a URL pointing to the location of the attachment in the persistence layer
- attributes are used for storing
    - the file name
    - the file media type
    - the file size

Example:

```xml
<my-attachment filename="book.png" mediatype="image/png" size="13245">
/fr/service/exist/crud/orbeon/builder/data/5277.../book.png
</my-attachment>
```

## See also

- [Form Definition Format](FIXME Form Runner ~ Form Definition Format)
