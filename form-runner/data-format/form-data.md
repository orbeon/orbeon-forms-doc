# Form Data Format

## Introduction

Form Runner loads and saves data in XML format. This page describes the format used.

## Basics

As you create a form definition with Form Builder, an XML representation for the data to capture is automatically created. It is organized as follows:

* A root element:
    `<form>`
* Within that element, for each section, a sub-element named after the section name:
    `<details>`
* Within a section element, a sub-element for each control in the section, named after the control name:
    `<author>`
* Within each control element, the value of the control is stored:
    `<author>J. K. Rowling</author>`

Example:

```xml
<form>
    <details>
        <title>Harry Potter and the Sorcerer's Stone</title>
        <author>J. K. Rowling</author>
        <language>en</language>
        <link>https://en.wikipedia.org/wiki/Harry_Potter_and_the_Philosopher%27s_Stone</link>
        <rating/>
        <publication-year>1997</publication-year>
        <review/>
        <image filename="" mediatype="" size=""/>
    </details>
    <notes>
        <note/>
    </notes>
</form>
```

## Nested sections

Each nested section is represented by a nested element within its enclosing section.

The element has the name of the section.

In this example, `my-nested-section` is nested within `my-section-1`:

```xml
<form>
    <my-section-1>
        <my-field-1/>
        <my-nested-section>
            <my-field-2/>
        </my-nested-section>
    </my-section-1>
    <my-section-2>
        <my-field03/>
    </my-section-2>
</form>
```

## Repeated sections

Each repeated section has an containing element with the name of the section, like for regular sections.

In addition, each iteration of the section has a nested element suffixed by `-iteration`. By default, that element has
the name of the section as prefix:

```xml
<form>
    <my-repeated-section>
        <my-repeated-section-iteration>
            <my-field-1/>
        </my-repeated-section-iteration>
        <my-repeated-section-iteration>
            <my-field-1/>
        </my-repeated-section-iteration>
    </my-repeated-section>
</form>
```

However, the form author can provide a custom name for the nested iteration, here `my-iteration-name`:

```xml
<form>
    <my-repeated-section>
        <my-iteration-name>
            <my-field-1/>
        </my-iteration-name>
        <my-iteration-name>
            <my-field-1/>
        </my-iteration-name>
    </my-repeated-section>
</form>
```

## Repeated grids

See also [Grid data format](../component/grid.md#data-format).

_NOTE: Non-repeated grids do not create containing elements as of Orbeon Forms 2018.1._

### Starting Orbeon Forms 4.8

Newly-published form definitions place a containing element within its enclosing section, exactly like for nested sections.

```xml
<note>
    <note-iteration>
        <note-text/>
    </note-iteration>
    <note-iteration>
        <note-text/>
    </note-iteration>
</note>
```

However, the form author can provide a custom name for the nested iteration, here `my-iteration-name`:

```xml
<note>
    <my-iteration-name>
        <note-text/>
    </my-iteration-name>
    <my-iteration-name>
        <note-text/>
    </my-iteration-name>
</note>
```

### Until Orbeon Forms 4.7

Each iteration is represented by an element with the name of the repeated section. There is no containing element:

```xml
<note>
    <note-text/>
</note>
<note>
    <note-text/>
</note>
```

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

- [Form Definition Format](../../form-runner/data-format/form-definition.md)
