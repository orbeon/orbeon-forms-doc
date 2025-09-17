# Form data format

## Introduction

Form data is represented by Form Runner using XML. There are two different usages of the data format:

- *Internally*: when a user is actively working with a form and Form Runner performs operations such as calculations, validations, actions, and so on. This is referred to as the *internal data format*.
- *Externally*: when Form Runner loads and saves data to and from a database or external services. This is referred to as the *external data format*.

This page describes the formats used.

## When the internal data format matters

__TL;DR: The internal format is different from the external format, it is subject to change between versions, and it is not something you should rely on. If you do, things will break in the future.__

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

## Data compatibility

The *publishing* of a form definition determines the data format that is supported by that form definition.

However, some limited changes to a form definition allow for the data to remain compatible. For more, see [Simple data migration](form-runner/feature/simple-data-migration.md) and the [associated blog post](https://blog.orbeon.com/2018/09/simple-data-migration.html).

## Formats in use

| Format version | Orbeon Forms version introduced | Description                      | Internal data format | Default external data format |
|----------------|---------------------------------|----------------------------------|----------------------|------------------------------|
| 4.0.0          | 3.x                             | Base format                      | Yes                  | Yes                          |
| 4.8.0          | 4.8                             | Update repeated grids format     | Yes                  | No                           |
| 2019.1.0       | 2019.1.0                        | Update non-repeated grids format | Yes                  | No                           | 

## Formats configuration

There is no option to change the version of the internal data format. This is a fixed format for a given version of Orbeon Forms.

- When using the `send` action, the `data-format-version` parameter can be used to specify the format. See [Send action](/form-runner/advanced/buttons-and-processes/actions-form-runner-send.md).
- When `POST`ing data to a form page, the `data-format-version` parameter can be used to specify the format. See [Initial data](/configuration/properties/form-runner-initial-data.md).
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

### Attachments

For attachments, the control element is slightly different:

- the text content is a URL pointing to the location of the attachment in the persistence layer
- attributes are used for storing
    - the file name
    - the file media type
    - the file size

Example:

```xml
<my-single-attachment filename="cat.jpg" mediatype="image/jpeg" size="2494208">
    /fr/service/persistence/crud/acme/registration/data/c15ed6664c41714f4c33e31126a98357a03b3aac/032e3d4ba1a9578eccfecff0f88d33bf9da9ba4e.bin
</my-single-attachment>
```

### Multiple attachments

[SINCE Orbeon Forms 2020.1]

Controls that support multiple attachments contain nested `<_/>` (anonymous) elements, as follows:

```xml
<my-multiple-attachment>
    <_ filename="screenshot.png" mediatype="image/png" size="202422">
        /fr/service/persistence/crud/acme/registration/data/c15ed6664c41714f4c33e31126a98357a03b3aac/ec1b860e3e0d0224ca2d153a016fd3a83e5c2bf4.bin
    </_>
    <_ filename="cat.jpg" mediatype="image/jpeg" size="2494208">
        /fr/service/persistence/crud/acme/registration/data/c15ed6664c41714f4c33e31126a98357a03b3aac/032e3d4ba1a9578eccfecff0f88d33bf9da9ba4e.bin
    </_>
    <_ filename="Presentation.pdf" mediatype="application/pdf" size="1617993">
        /fr/service/persistence/crud/acme/registration/data/c15ed6664c41714f4c33e31126a98357a03b3aac/dfafa33289158b34c9c63e7c16f6b425b76fae43.bin
    </_>
    <_ filename="User's Manual (complete).pdf" mediatype="application/pdf" size="731131">
        /fr/service/persistence/crud/acme/registration/data/c15ed6664c41714f4c33e31126a98357a03b3aac/d42d5f46b20f04876c7ef36fb7c6a8089d68363a.bin
    </_>
</my-multiple-attachment>
```

The meaning of the attributes and the content of the element is the same as in the case of a single file attachment.

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

## Wizard section status

When using the [Wizard](/form-runner/component/wizard.md), annotations are added to the XML elements, in the form data, that represent the form sections shown as Wizard pages. The purpose of this is so that the Wizard can restore the status of sections when reloading incomplete data from a draft in the database.

Specifically, the `fr:section-status` attribute is added to these XML elements and contains a list of unordered, space-separated tokens, as follows:

- missing attribute:
    - The section has not been visited by the user yet.
- `changed` token:
    - The section has been visited and at least one field value was changed by the user.
- `incomplete` token:
    - The section has been visited and has incomplete fields (required fields that are empty).
- `invalid` token:
    - The section has been visited and has at least one invalid field (separately from incomplete fields).
- `visible-incomplete` token:
    - The section has been visited and has incomplete fields shown to the user in the Error Summary.
    - This token is only present if `incomplete` is present as well.
- `visible-invalid` token:
    - The section has been visited and has visible error fields shown to the user in the Error Summary.
    - This token is only present if `invalid` is present as well.

Example:

```xml
<form xmlns:fr="http://orbeon.org/oxf/xml/form-runner">
    <!-- Section is visited, has incomplete fields, and invalid fields -->
    <my-section-1 fr:section-status="changed incomplete invalid visible-incomplete visible-invalid">
        ...
    </my-section-1>
    <!-- Section is visited without changes -->
    <my-section-2 fr:section-status="">
        ...
    </my-section-2>
    <!-- Section is not visited -->
    <my-section-3>
        ...
    </my-section-3>
</form>
```

_NOTE: When the Wizard is not in use, these annotations are not added as of Orbeon Forms 2021.1._

## See also

- [Grid data format](/component/grid.md#data-format)
- [Form Definition Format](/form-runner/data-format/form-definition.md)
- [Field-level encryption](/form-builder/field-level-encryption.md)
- [Send action](/form-runner/advanced/buttons-and-processes/actions-form-runner-send.md) (`data-format-version` parameter)
- [Initial data](/configuration/properties/form-runner-initial-data.md) (`data-format-version` parameter)
- [The `data-format-version` property](/configuration/properties/persistence.md#data-format-version-property)
