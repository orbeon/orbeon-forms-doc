# Form Definition Format

<!-- toc -->

## Overview

A Form Runner form is an XHTML document with a few twists. It contains:

- XForms markup following certain patterns
- some extension markup for the view

## Form definition

### Head

[IN PROGRESS]

### Body

The standard body of the form looks like this:

```xml
<xhtml:body>
    <fr:view width="..." appearance="...">
        <fr:body>
            <fr:section>
                ...
            </fr:section>
            <fr:section>
                ...
            </fr:section>
        </fr:body>
        <fr:buttons>...</fr:buttons>
    </fr:view>
</xhtml:body>
```

The following attributes and elements are optional:

- the `width` attribute on `<fr:view>`
- the `appearance` attribute on `<fr:view>` (reserved for future use)
- the `<fr:buttons>` element

When the `<fr:buttons>` element is present, Form Runner ignores the buttons configured by default for the detail page, and instead uses the content of the `<fr:buttons>` element.

## Form data

See [[Data Format|Form Runner ~ Data Format]]

## See also

- [[Data Format|Form Runner ~ Data Format]]
