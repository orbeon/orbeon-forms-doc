# International Bank Account Number (IBAN)

## Availability

[SINCE Orbeon Forms 2025.1.1]

## What it does

This component represents an [International Bank Account Number (IBAN)](https://en.wikipedia.org/wiki/International_Bank_Account_Number).

It allows the user to enter an International Bank Account Number (IBAN) such as `BE68539007547034`.

* The user input is validated:
  * to ensure that it matches the IBAN format: two letters, two digits, followed by alphanumeric characters
  * to ensure that the checksum is correct, using the [mod-97 algorithm](https://en.wikipedia.org/wiki/International_Bank_Account_Number#Validating_the_IBAN)

## See also

* [International Securities Identification Number (ISIN)](isin.md)
* [Legal Entity Identifier (LEI)](lei.md)
* [US Phone Number](us-phone.md)
* [US Employer Identification Number (EIN)](us-ein.md)
* [US Social Security Number (SSN)](us-ssn.md)
* [US State](us-state.md)
