# Section and grid settings

## Availability

- Sections and repeated grids: [SINCE Orbeon Forms 3.8 or earlier].
- Non-repeated grids: [SINCE Orbeon Forms 2019.1].

## Basic settings and formulas

### Overview

![](images/section-settings.png)

### Section or grid name

Each section or grid has a _name_ or identifier, which determines how data is represented in XML. The name name specifies an identifier for the section or grid which is unique in the entire form. If no name is explicitly specified, Form Builder assigns a default name, such as "section-1" or "grid-1".

A section or grid name can be changed, provided it doesn't collide with another control name (an error will show otherwise).

[SINCE Orbeon Forms 2019.1]

See [Renaming of controls and formulas](/form-builder/formulas.md#renaming-of-controls-and-formulas).

### Page breaks

When producing PDF automatically, the "Page break before" checkbox, when enabled, ensures that the given section starts at the top of a new page. 

### Custom CSS classes

The "Custom CSS classes" field allows adding CSS classes which will be placed on the control in the resulting HTML. This can be used for custom styling.

See [Control Settings](control-settings.md#custom-css-classes) for more about custom CSS classes. 

### Formulas

- **Visibility:** Specifies whether the section or grid is visible. This can be either "Yes" (default), "No", or an XPath formula, in which case the section or grid is visible only if the formula evaluates to `true()`.
- **Read-Only:** Specifies whether the section or grid is read-only (not editable). This can be either "Yes", "No" (default), or an XPath formula, in which case the section or grid is editable only if the formula evaluates to `false()`.

For a section or grid to be visible or editable, all enclosing sections/grids must be visible or editable as well.

XPath expressions are described in more details in [Formulas](formulas.md).

## Repeat settings

See [Repeat settings](repeat-settings.md).

## Settings specific to sections

See [Section settings](section-settings.md).

## See also

- [Repeat settings](repeat-settings.md)
- [Section settings](section-settings.md)
- [Grid settings](grid-settings.md)
- [Repeated grids](repeated-grids.md)
- [Formulas](formulas.md)
- [Grid component](/form-runner/component/grid.md)
- [Section component](/form-runner/component/section.md)
- [Wizard view](/form-runner/feature/wizard-view.md)
- Blog post: [Dynamic loading of closed sections](https://blog.orbeon.com/2020/04/dynamic-loading-of-closed-sections.html)
