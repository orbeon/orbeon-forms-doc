# Formulas

## Formulas and XPath

Formulas in Orbeon Forms are expressed using *XPath*, a standard _expression language_ for XML. It does not allow you to _modify_ XML data, but it allows you to _query_ XML data and compute values.

For those familiar with Microsoft Excel or other spreadsheet software, you can think of formulas with Orbeon Forms as what follows the "=" sign in a spreadsheet. It is similar to that, but with a slightly different syntax and set of rules. 

In general you don't need to know about XPath in Form Builder, with the exception of some properties in the Control Settings and Section Settings dialogs. XPath expressions are considered an advanced feature of Form Builder, which might require some programming knowledge.

*NOTE: Incorrect XPath expressions may cause the form to behave improperly, so caution must be applied.*

## Referring to control values from formulas

To refer to a control value from a formula, use the notation `$foo` where `foo` is the control name. For example:

```xpath
$price * $quantity
```

See also [Examples of formulas](formulas-examples.md).

## Renaming of controls and formulas

[SINCE Orbeon Forms 2019.1]

When a control or section or grid is renamed, dependent formulas which use the notation `$foo` where `foo` is the control name, are automatically updated.

## Where do formulas appear?

### Control Settings dialog

In the Control Settings dialog, XPath expressions are used to specify the following aspects of a control.

#### Validations and alerts

![Validations](images/control-settings-validations.png)

- **Constraint:** Boolean expression specifying whether the control is valid.
    - If this field is left blank, then the validity of the control depends on the data type and the "Required" option.
    - Otherwise, the control is valid if in addition to all the other constraint being met, the result of the Boolean expression is `true()`.

#### Formulas

![Formulas](images/control-settings-formulas.png)

- **Visibility:** 
    - Boolean expression specifying whether the control is visible.
    - If this field is left blank, then the control is always visible, unless the section is not visible.
    - Otherwise, it is visible only if the result of the boolean expression is `true()`.
- **Read-Only:** 
    - Boolean expression specifying whether the control is read-only (not editable).
    - If this field is left blank, then the control is editable unless the section is read-only.
    - Otherwise, the field is editable only if the result of the Boolean expression is `false()`.
- **Initial value:** 
    - String expression returning the initial value of the control when the form first shows. This is only applied in `new` mode.
    - Default: the value set into the field at design time (usually a blank value).
- **Calculated Value:** 
    - String expression specifying a calculated value of the control which updates while the form user interacts with the form.
    - Default: the default value of the control, or the value entered by the form user.

### Section/Grid Settings dialog

![Basic Settings and Formulas](images/section-settings.png)

In the Section/Grid Settings dialog, XPath expressions are used to specify the following aspects of a section or grid:

* **Visibility:** Boolean expression specifying whether the section is visible or not.
    * If this field is left blank, then the section is always visible.
    * Otherwise, it is visible only if the result of the Boolean expression is true().
* **Read-Only:** Boolean expression specifying whether the section is shown as read-only or not.
    * If this field is left blank, then the section content is always editable.
    * Otherwise, the section content is editable only if the result of the Boolean expression is false().

### Actions

TODO

## Referring to control values

XPath expressions may refer to the value of other controls in the page, using variables named after the name of the controls to use.

## Examples

See [examples of formulas](formulas-examples.md).

## See also

- [Examples of formulas](formulas-examples.md)
- [Form Builder Validation](validation.md)
- [Better formulas with XPath type annotations](https://blog.orbeon.com/2013/01/better-formulas-with-xpath-type.html)
- [Formulas for summing values, done right](https://blog.orbeon.com/2013/08/formulas-for-summing-values-done-right.html)
- [Control required values with formulas in Orbeon Forms 4.7](https://blog.orbeon.com/2014/09/control-required-values-with-formulas.html)
- [XForms Validation](../xforms/validation.md)
