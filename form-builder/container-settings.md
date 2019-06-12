# Section and grid settings

## Availability

For non-repeated grids: [SINCE Orbeon Forms 2019.1].

## Basic settings and formulas

### Overview

![](images/section-settings.png)

### Section or grid name

Each section or grid has a _name_ or identifier, which determines how data is represented in XML. The name name specifies an identifier for the section or grid which is unique in the entire form. If no name is explicitly specified, Form Builder assigns a default name, such as "section-1" or "grid-1".

### Page breaks

When producing PDF automatically, the "Page break before" checkbox, when enabled, ensures that the given section starts at the top of a new page. 

### Custom CSS classes

The "Custom CSS classes" field allows adding CSS classes which will be placed on the control in the resulting HTML. This can be used for custom styling.

### Formulas

* **Visibility:** Boolean expression specifying whether the section or grid is visible.
    * If this field is left blank, then the section or grid is always visible, unless an enclosing section is not visible.
    * Otherwise, it is visible only if the result of the Boolean expression is `true()`.
* **Read-Only:** Boolean expression specifying whether the section or grid is read-only (not editable).
    * If this field is left blank, then the section or grid is editable unless an enclosing section is read-only.
    * Otherwise, it is editable only if the result of the Boolean expression is `false()`.

XPath expressions are described in more details in [Formulas](formulas.md).

## Repeat settings

See [Repeat settings](repeat-settings.md).

## Settings specific to sections

See [Section settings](section-settings.md).

## See also

- [Repeat settings](repeat-settings.md)
- [Section settings](section-settings.md)
- [Repeated grids](repeated-grids.md)
- [Formulas](formulas.md)
- [Grid component](/form-runner/component/grid.md)
- [Section component](/form-runner/component/section.md)
- [Wizard view](/form-runner/feature/wizard-view.md)
