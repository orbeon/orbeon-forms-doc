# Formulas

## Formulas and XPath

In Orbeon Forms, formulas are expressed using *XPath*, a standard _expression language_ for XML. It does not allow you to _modify_ XML data, but it allows you to _query_ XML data and compute values.

For those familiar with Microsoft Excel or other spreadsheet software, you can think of formulas with Orbeon Forms as what follows the "=" sign in a spreadsheet. It is similar to that, but with a slightly different syntax and set of rules. 

In general, you don't need to know about XPath in Form Builder, with the exception of some properties in the Control Settings and Section Settings dialogs. Formulas are considered an advanced feature of Form Builder, which might require some programming knowledge.

*NOTE: Incorrect XPath expressions may cause the form to behave improperly, so caution must be applied.*

## Examples of formulas

See [Examples of formulas](formulas-examples.md).

## Referring to control values from formulas

### Basic usage

To refer to a form control value from a formula, use the variable notation `$foo` where `foo` is the control name. For example:

```xpath
$price * $quantity
```

This assumes a form control named "price" and another form control named "quantity". 

### Variables in depth

In a formula, when referring to `$price`, the result is actually an XML node, as this is how Orbeon Forms stores data internally. Think of a node as a container for a value:

```xml
<price>4.99</price>
```

There are two ways to obtain a value from this node:

- using the `string()` function
  - this returns, of course, a value of type `string` 
  - example: `$price/string()` or `string($price)`
- using the `data()` function
  - this returns a value of the type associated with the associated form control
  - example: `$price/data(.)` or `data($price)`

Now in many cases, XPath gets the value for you without calling `string()` or `data()`. For example when you perform a multiplication:

```xpath
$price * $quantity
```

Here, XPath "sees" that the multiplication cannot apply to nodes, and automatically fetches ("atomizes") the value from the nodes. In this case, if the associated form control has type "Decimal", then the values will be retrieved from the nodes and converted to decimal numbers as if you had used the `data()` function explicitly:

```xpath
data($price) * data($quantity)
```

Note that the multiplication will fail if the form controls do not contain valid decimal numbers.

The `data()` function can be a little tricky because of this. Now say you'd like instead to *concatenate* the text of those nodes containing decimal values even if the don't contain valid decimal values. Then you have to explicitly use the `string()` function:

```xpath
concat('Price: ', string($price), 'Quantity: ', string($quantity))
```

### Where you can use variables

You can use the variable notation in the following formulas in the Control Settings dialog:

- Required
- Validation
- Calculated Value
- Initial Value
- Visibility
- Read-Only

[SINCE Orbeon Forms 2022.1]

You can also use the variable notation in the following formulas:

- Repeated grids and sections:
    - Minimum Number of Repetitions
    - Maximum Number of Repetitions
    - Freeze Repetitions
- Number and Currency fields
    - Prefix
    - Suffix
- Dynamic Dropdown:
    - Resource URL
    - Choices formula
    - Label formula
    - Value formula
    - Hint formula
- Actions
    - [Simple Actions](actions.md)
    - [Action Syntax](actions-syntax.md)
- [Template parameters](template-syntax.md)
    - Control name
    - Formula

See also the blog post [Improvements to variables in formulas](https://blog.orbeon.com/2022/04/improvements-to-variables-in-formulas.html).

### Resolution of repeated controls

When referring to controls that are repeated, for example within repeated grids or repeated sections, a variable can return a sequence of multiple values. Each value corresponds to a repeated control.

However, not all controls in the form are necessarily selected. Instead, the "closest" controls are selected, as follows:

- The closest enclosing repeat iteration between the location of the formula and the control identified by the variable is identified. If there is none, then this is the top-level of the form.
- Then all the controls within that iteration, or within the entire form when there is no such iteration, are selected.

For example, consider

- a repeated grid
- a decimal field called `price` on each row
- an integer field called `quantity` on each row
- a decimal text output field called `row-total` on each row
- a decimal text output field called `total` below the grid

Calculated value expression for `row-total`:

```xpath
$price * $quantity
```

The `row-total` calculated value applies to the closest `price` and `quantity` controls, that is, those on the same row, and each row gets its own row total.

Calculated value expression for `total`:

```xpath
sum($row-total[string() castable as xs:decimal], 0.0)
```

On the other hand, the `total` calculation is outside the repeat, and when it refers to `$row-total`, all `row-total` values are returned. Therefore, the sum applies to all the `row-total` values (assuming they can be cast as `xs:decimal`, in this example).

TODO: Is this the same as using `fr:control-string-value('$price', false())`, or is there a subtle difference?

## Renaming of controls and formulas

[SINCE Orbeon Forms 2019.1]

When a control or section or grid is renamed, dependent formulas which use the variable notation `$foo` (where `foo` is the control name) are automatically updated.

[SINCE Orbeon Forms 2022.1]

In addition, other references which use the variable notation `$foo` where `foo` is the control name, are automatically updated, including:

- Repeated grids and sections:
    - Minimum Number of Repetitions
    - Maximum Number of Repetitions
    - Freeze Repetitions 
- Number and Currency fields
    - Prefix
    - Suffix
- Dynamic Dropdown:
    - Resource URL
    - Choices formula
    - Label formula
    - Value formula
    - Hint formula
- Actions
    - [Simple Actions](actions.md)
    - [Action Syntax](actions-syntax.md)
- [Template parameters](template-syntax.md)
    - Control name
    - Formula

## Where do formulas appear?

### Control Settings dialog

In the Control Settings dialog, formulas are used to specify the following aspects of a control.

#### Validations and alerts

![Validations](images/control-settings-validations.png)

- **Constraint:** Boolean expression specifying whether the control is valid.
    - If this field is left blank, then the validity of the control depends on the data type and the "Required" option.
    - Otherwise, the control is valid if in addition to all the other constraint being met, the result of the Boolean expression is `true()`.

#### Formulas

![Formulas](images/control-settings-formulas.png)

- **Visibility:**
    - Specifies whether the control is visible. This can be either "Yes" (default), "No", or an XPath formula.
    - If a formula is specified, the control is visible only if the formula evaluates to `true()`.
- **Read-Only:**
    - Specifies whether the control is read-only (not editable). This can be either "Yes", "No" (default), or an XPath formula.
    - If a formula is specified, the control is editable only if the formula evaluates to `false()`.
- **Initial value:** 
    - String expression returning the initial value of the control when the form first shows. This is only applied in `new` mode.
    - Default: the value set into the field at design time (usually a blank value).
- **Calculated Value:** 
    - String expression specifying a calculated value of the control which updates while the form user interacts with the form.
    - Default: the default value of the control, or the value entered by the form user.

### Section/Grid Settings dialog

![Basic Settings and Formulas](images/section-settings.png)

In the Section/Grid Settings dialog, formulas are used to specify the following aspects of a section or grid:

- **Visibility:** Specifies whether the section or grid is visible. This can be either "Yes" (default), "No", or an XPath formula, in which case the section or grid is visible only if the formula evaluates to `true()`.
- **Read-Only:** Specifies whether the section or grid is read-only (not editable). This can be either "Yes", "No" (default), or an XPath formula, in which case the section or grid is editable only if the formula evaluates to `false()`.

[SINCE Orbeon Forms 2020.1]

In the Form Settings dialog, an XPath expression can be used to specify whether the entire form is read-only (not editable).

### Actions

In the Section/Grid Settings dialog, formulas are used to specify the following aspects of a section or grid:

TODO

## Examples

See [examples of formulas](formulas-examples.md).

## See also

- [Examples of formulas](formulas-examples.md)
- [Form Builder Validation](validation.md)
- [Formulas inspector](formulas-inspector.md)
- [When the internal data format matters](/form-runner/data-format/form-data.md#when-the-internal-data-format-matters)
- Blog posts
    - [Better formulas with XPath type annotations](https://blog.orbeon.com/2013/01/better-formulas-with-xpath-type.html)
    - [Formulas for summing values, done right](https://blog.orbeon.com/2013/08/formulas-for-summing-values-done-right.html)
    - [Control required values with formulas in Orbeon Forms 4.7](https://blog.orbeon.com/2014/09/control-required-values-with-formulas.html)
    - [Improvements to variables in formulas](https://blog.orbeon.com/2022/04/improvements-to-variables-in-formulas.html)
- [XForms Validation](/xforms/validation.md)
