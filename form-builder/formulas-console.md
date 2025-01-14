# Formulas console

## Availability

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

## Introduction

The Formulas console allows you to view formulas errors that occur when testing a form.

![Formulas console](images/formulas-console.png)

Some errors are detected by Orbeon Forms statically, when compiling the form; other errors are detected dynamically, when the form runs and there is user interaction. Both types of errors are shown in the Formulas console.

Each entry shows:

- some context for the error
- the control name, if applicable
- the formula
- the error message
- the count of the number of times the error occurred

The console automatically opens when the first error occurs. You can close and open the console with the icon in the top right of the console area.

## See also

- [Formulas inspector](formulas-inspector.md)
- [Examples of formulas](formulas-examples.md)
- [Formulas](formulas.md)
- [Testing a form in web mode](web-test.md)