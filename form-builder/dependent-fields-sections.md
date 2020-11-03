# Dependent fields and sections

## Rationale

- Both fields and sections can:
    - Be shown or hidden depending on the value of other fields, by setting their **visibility** property.  
    - Become read-only, as opposed to read-write, depending on the value of other fields, by setting their **read-only** property.
- In addition, fields can also:
    - Have a dynamic value set when the form is first loaded by users, by setting their **initial value** property.
    - Have a value that is updated "live" as users set or change the value of other fields in the form, by setting their **calculated value** property.

By using a combination of the aforementioned visibility, read-only, initial value, and calculated value properties for sections and fields, you can create forms that are very dynamic, which can behave like a spreadsheet, or like a small application.

The visibility, read-only, initial value, and calculated value are set using [formulas](formulas.md) in the [Control Settings](control-settings.md) or [Section Settings](section-settings.md) dialogs.

## Examples

### Hide a field based on the user's answer to an earlier questions

Say you want to show a textarea when users check a checkbox to indicate they want to provide more details:

![Textarea shown if checkbox is checked](images/dependent-fields-sections-checkbox.png)

1. Add a Single Checkbox control, open its Control Settings, name it `provide-additional-details`.
2. Add a Plain Text Area control, open its Control Settings, open the Formulas tab, set the Visibility to Formula, an enter `$provide-additional-details/string() = 'true'`.