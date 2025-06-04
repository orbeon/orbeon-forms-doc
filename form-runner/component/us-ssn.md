# US Social Security Number (SSN) component

## Availability

[\[SINCE Orbeon Forms 2024.1.2\]](/release-notes/orbeon-forms-2024.1.2.md)

## What it does

This component represents a US Social Security Number (SSN).

![US Social Security Number (SSN) field with obscured value](/form-runner/component/images/xbl-us-ssn-edit-obscured.webp)

It allows the user to enter a Social Security Number in the format `078-05-1120`, with or without the dashes.

- The user input is sanitized to remove any non-digit characters.
- The user input is validated:
    - to ensure that it contains exactly 9 digits, otherwise the field value is considered invalid
    - and to reject known invalid patterns, such as numbers starting with `000`, `666`, or `9`, or numbers that are all the same digit (e.g., `111-11-1111`)
- The value is formatted as `078-05-1120` when displaying the value.
- The value is stored in the database as a string of 9 digits including two separator dashes, for example `078-05-1120`.

_NOTE: Storing the value with dashes is a design choice which seems to match the most common usage._

## Options

Because a Social Security Number is a sensitive piece of information, by default the value of the field is not shown entirely:

- In editable modes, the value is obscured, like a password field.
- In readonly modes, such as the View and PDF modes, only the last four digits are shown, like `•••-••-1120`.

For editable modes, you can configure the field to behave in the following ways:

- Always visible, like `078-05-1120`.
- Always obscured, like `•••••••••`.
- Obscured by default, but visible when the user clicks on a "Reveal" checkbox next to the field. This allows the user to see the full SSN when needed, such as to verify that the number entered is correct, while still keeping it hidden by default, so that someone glancing at a screen will not see the full number.

![US Social Security Number (SSN) field with revealed value](/form-runner/component/images/xbl-us-ssn-edit-revealed.webp)

For readonly modes, such as the View and PDF modes, you can configure the field to behave in the following ways:

- Visible, like `078-05-1120`.
- Obscured, like `•••-••-••••`.
- Only show the last four digits, like `•••-••-1120`.

![US Social Security Number (SSN) field with partially-revealed value](/form-runner/component/images/xbl-us-ssn-view-partial.webp)

These options are controlled in Form Builder or using configuration properties:

- at the form control level, using the "Control Settings" dialog
- at the form level, using the "Form Settings" dialog
- in configuration properties

![US Social Security Number (SSN) field settings](/form-runner/component/images/xbl-us-ssn-settings.webp)

The default properties are as follows:

```xml
<property as="xs:string"  name="oxf.xforms.xbl.fr.us-ssn.edit-format"                        value="reveal"/> <!-- allowed values: `obscured`, `visible`, `reveal` -->
<property as="xs:string"  name="oxf.xforms.xbl.fr.us-ssn.view-format"                        value="partial"/><!-- allowed values: `obscured`, `visible`, `partial` -->
<property as="xs:string"  name="oxf.xforms.xbl.fr.us-ssn.pdf-format"                         value="partial"/><!-- allowed values: `obscured`, `visible`, `partial` -->
<property as="xs:string"  name="oxf.xforms.xbl.fr.us-ssn.pdf-template-format"                value="partial"/><!-- allowed values: `obscured`, `visible`, `partial` -->
```

## Data security and GDPR

Because the SSN is a sensitive piece of information, we recommend that you handle this data in the following ways:

- First, consider not requesting this information from users unless absolutely necessary.
- Second, if you request it, consider making only transient use of it, such as calling a verification service, without storing it in your database.
- Finally, if you do store it, consider encrypting it in your database, and ensure that you comply with data protection regulations such as GDPR. Orbeon Forms [supports encryption at rest](/form-builder/field-level-encryption.md), which satisfies this requirement.

## See also

- Blog post: [Field-level encryption](https://blog.orbeon.com/2019/04/field-level-encryption.html)
- [Field-level encryption](/form-builder/field-level-encryption.md)
- [Basic Settings](/form-builder/control-settings.md)
- [US Employer Identification Number (EIN)](us-ein.md)