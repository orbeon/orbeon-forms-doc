# US Employer Identification Number (EIN) component

## Availability

[\[SINCE Orbeon Forms 2024.1.2\]](/release-notes/orbeon-forms-2024.1.2.md)

## What it does

This component represents a US Employer Identification Number (EIN).

![US Employer Identification Number (EIN)](/form-runner/component/images/xbl-us-ein-edit.webp)

It allows the user to enter an Employer Identification Number in the format `12-3456789`, with or without the dash.

- The user input is sanitized to remove any non-digit characters.
- The user input is validated:
    - to ensure that it contains exactly 9 digits, otherwise the field value is considered invalid
- The value is formatted as `12-3456789` when displaying the value.
- The value is stored in the database as a string of 9 digits including two separator dashes, for example `12-3456789`.

_NOTE: Storing the value with dashes is a design choice which seems to match the most common usage._

Unlike the [US Social Security Number (SSN)](us-ssn.md), the EIN does not have any additional options for obscuring or revealing the value, as it is considered less sensitive than a Social Security Number.

## See also

- [US Phone Number](us-phone.md)
- [US Social Security Number (SSN)](us-ssn.md)
- [US State](us-state.md)
