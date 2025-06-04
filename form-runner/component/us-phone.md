# US Phone Number component

## What it does

This component represents a US phone number. It:

- *validates* the stored format to make sure it contains exactly 10 digits.
- *removes* all `()*-[].#/'': ` characters from the input string before storing the value.
- *formats* the value as `(123) 456-7890` when displaying the value, but only if the stored value is only made of digits.

## See also

- [US Employer Identification Number (EIN)](us-ein.md)
- [US Social Security Number (SSN)](us-ssn.md)
- [US State](us-state.md)