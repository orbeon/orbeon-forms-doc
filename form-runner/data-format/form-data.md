# Form data format

## Introduction

Form data is represented by Form Runner using XML. There are two different usages of the data format:

- *Internally*: when a user is actively working with a form and Form Runner performs operations such as calculations, validations, actions, and so on. This is referred to as the *internal data format*.
- *Externally*: when Form Runner loads and saves data to and from a database or external services. This is referred to as the *external data format*.

This page describes the formats used.

## When the internal data format matters

In most cases, the internal data format does not matter to form authors.

However, XPath formulas or custom XForms code have full access to the form data represented using the internal data format. It is possible to write such formulas or XForms in ways that make them depend on the specific data format. For example, you might assume that a non-repeated grid has, or does not have, an enclosing element. If and when the internal data format changes due to an Orbeon Forms upgrade, the given formula might break.

When XPath formulas or custom XForms code are required, we recommend making sure that those do not depend on the specifics of the internal data format.

On way to avoid issues is to avoid referring to data in the form using relative XPath expressions, such as:
 
```xpath
../my-other-control
```

Instead, use the variable notation with `$`:

```xpath
$my-other-control
```

__THE BOTTOM LINE IS THIS: The internal format is different from the external format, it is subject to change between versions, and it is not something you should rely on. If you do, things will break in the future.__

## Formats in use

|Format version|Orbeon Forms version introduced|Description|Internal data format|Default external data format|
|---|---|---|---|---|
|4.0.0|3.x|Base format|Yes|Yes|
|4.8.0|4.8|Update repeated grids format|Yes|No|
|2019.1.0|2019.1.0|Update non-repeated grids format|Yes|No| 

## Formats configuration

There is no option to change the version of the internal data format. This is a fixed format for a given version of Orbeon Forms.

- When using the `send` action, the `data-format-version` parameter can be used to specify the format. See [Send action](/form-runner/advanced/buttons-and-processes/actions-form-runner-send.md).
- When POSTing data to a form page, the `data-format-version` parameter can be used to specify the format. See [Initial data posted to the New Form page](/configuration/properties/form-runner-detail-page.md#initial-data-posted-to-the-new-form-page).
- The `oxf.fr.persistence.[provider].data-format-version` property specifies the data format version used in the database. See [`data-format-version` property](/configuration/properties/persistence.md#data-format-version-property).

## 4.0.0 format

### Introduction
 
The 4.0.0 format is the default *external data format*.

### Basics

The data is organized as follows:

- A root element:
    `<form>`
- Within that element, for each section, a sub-element named after the section name:
    `<details>`
- Within a section element, a sub-element for each control in the section, named after the control name:
    `<author>`
- Within each control element, the value of the control is stored:
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

### Nested sections

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

### Repeated sections

Each repeated section has an enclosing element with the name of the section, like for regular sections.

In addition, each iteration of the section has a nested element suffixed by `-iteration`. By default, that element has the name of the section as prefix:

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

### Non-repeated grids

Non-repeated grids do not have a representation in XML. 

### Repeated grids

See also [Grid data format](/component/grid.md#data-format).

Each iteration is represented by an element with the name of the repeated section. There is no enclosing element:

```xml
<note>
    <note-text/>
</note>
<note>
    <note-text/>
</note>
```

###Attachments

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

## 4.8.0 format

### Introduction

With Orbeon Forms 4.8, the 4.8.0 data format is introduced. It changes the way repeated grids are represented to be in line with the format for repeated sections.

- The default external data format remains 4.0.0 unless explicitly changed via configuration.
- The 4.8.0 format is the new *internal data format*. There is no option to change the version of the internal data format.

### Repeated grids

An enclosing element with the name of the repeated grid is added, exactly like for sections. Nested iteration elements, named with a `-iteration` suffix, enclose each iteration content: 

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

With Form Builder, the form author can provide a custom name for the nested iteration, here `my-iteration-name`:

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

## 2019.1.0 format

### Introduction

With Orbeon Forms 2019.1, the 2019.1 data format is introduced. It changes the way non-repeated grids are represented to be in line with the format for non-repeated sections, repeated grids and repeated sections.

- The default external data format remains 4.0.0 unless explicitly changed via configuration.
- The 2019.1.0 format is the new *internal data format*. There is no option to change the version of the internal data format.

### Non-repeated grids

An enclosing element with the name of the non-repeated grid is added, exactly like for sections. Elements for nested controls directly under that enclosing element.

In the following example, the `<details-grid>` and `<review-grid>` elements are added, compared to the 4.8.0 and 4.0.0 data formats:

```xml
<form>
    <details>
        <details-grid>
            <title>Harry Potter and the Sorcerer's Stone</title>
            <author>J. K. Rowling</author>
            <image filename="" mediatype="" size=""/>
            <language>en</language>
            <link>https://en.wikipedia.org/wiki/Harry_Potter_and_the_Philosopher%27s_Stone</link>
            <rating/>
            <publication-year>1997</publication-year>
        </details-grid>
        <review-grid>
            <review/>
        </review-grid>
    </details>
    <notes>
        <note>
            <note-iteration>
                <note-text/>
            </note-iteration>
        </note>
    </notes>
</form>
```

## Encryption

If in your form you have fields marked to be encrypted at rest, an attribute is added on the element corresponding to those fields, as follows:

- Since Orbeon Forms 2019.1
    - `fr:attachment-encrypted="true"` for attachment fields
    - `fr:value-encrypted="true"` for other fields
- Update to Orbeon Forms 2018.2
    - `encrypted="true"`
    
Explicitly marking fields that have been encrypted in the data allows form authors to change a form definition adding or removing fields to be encrypted without having to create a new version of that form definition, should form authors want to do so.

## See also

- [Grid data format](/component/grid.md#data-format)
- [Form Definition Format](/form-runner/data-format/form-definition.md)
- [Field-level encryption](/form-builder/field-level-encryption.md)
- [Send action](/form-runner/advanced/buttons-and-processes/actions-form-runner-send.md) (`data-format-version` parameter)
- [Initial data posted to the New Form page](/configuration/properties/form-runner-detail-page.md#initial-data-posted-to-the-new-form-page) (`data-format-version` parameter)
- [The `data-format-version` property](/configuration/properties/persistence.md#data-format-version-property)
