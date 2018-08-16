# Automatic calculations dependencies

## Rationale

Calculated values or initial values can depend on the value of other form controls. In turn, those form controls can be calculated from yet other form controls. Such controls may appear further down in the form, or, in fact, in any place.

It is therefore important to compute calculated values in an order based on the *dependencies* of that value, not just in the order in which the controls appear in the form. This is very similar to what happens in a spreadsheet.

## Availability

[SINCE Orbeon Forms 4.10]

Form Runner supports dependencies of calculated values when calculated values refer to other controls via variables, such as `$`. This is done by adding the `xxf:analysis.calculate="true"` attribute on the first model of the form. This must be done via the "Edit Source" feature as there is no user interface for this.  

[SINCE Orbeon Forms 2018.1]

Orbeon Forms add an option to enable and disable automatic calculations dependencies in the "Form Settings" dialog.

In addition, for new form definitions, the "Automatic Calculations Dependencies" option is enabled by default starting with Orbeon Forms 2018.1.

_NOTE: Existing form definitions which do not have the `xxf:analysis.calculate="true"` attribute set in the form definition source are not automatically upgraded to enable automatic calculations dependencies. You must enable dependencies explicitly in the "Form Settings" dialog._

![Form Options](../../form-builder/images/form-settings-options.png)

## Examples

Consider the following grid, where the totals must appear, on one hand, on each line item row, but where the global totals are present *before* the grid.

![Grid with calcuations](../images/calculations-dependencies-grid.png)

Row totals are expressed as:

```xpath
$units * $unit-price
```

And the global totals as:

```xpath
sum($units)
```

and:

```xpath
sum($row-total)
```

Automatic calculations dependencies ensure that, even if the totals show in the form before the grid, their values update correctly as the user modifies the "Units" and "Unit Price" fields.

## See also 

- [Formulas](../../form-builder/formulas.md)
- [Control settings](../../form-builder/control-settings.md)
