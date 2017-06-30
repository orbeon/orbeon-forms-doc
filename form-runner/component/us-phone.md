# US Phone Component

<!-- toc -->

## What it does

This component represents an US phone number. It:

- *validates* the stored format to make sure it contains exactly 10 digits.
- *removes* all `()*-[].#/'': ` characters from the input string before storing the value.
- *formats* the value as `(123) 456-7890` when displaying the value, but only if the stored value is only made of digits.
