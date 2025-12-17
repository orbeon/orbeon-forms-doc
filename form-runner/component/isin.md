# International Securities Identification Number (ISIN)

## Availability

[\[SINCE Orbeon Forms 2024.1.2\]](../../release-notes/orbeon-forms-2024.1.2.md)

## What it does

This component represents an [International Securities Identification Number (ISIN)](https://en.wikipedia.org/wiki/International_Securities_Identification_Number).

<figure><img src="../../.gitbook/assets/xbl-isin-edit.webp" alt="" width="338"><figcaption><p>International Securities Identification Number (ISIN)</p></figcaption></figure>

It allows the user to enter an International Securities Identification Number (ISIN) such as `US0378331005`.

* The user input is validated:
  * to ensure that it contains exactly 12 characters, starting with two letters, followed by 9 alphanumeric characters, and ending with a check digit
  * to make sure the check digit is correct, using the [Luhn algorithm](https://en.wikipedia.org/wiki/Luhn_algorithm)

## See also

* [Legal Entity Identifier (LEI)](lei.md)
* [US Phone Number](us-phone.md)
* [US Employer Identification Number (EIN)](us-ein.md)
* [US Social Security Number (SSN)](us-ssn.md)
* [US State](us-state.md)
