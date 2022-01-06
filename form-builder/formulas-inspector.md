# Formulas inspector

## Introduction

With Orbeon Forms, [formulas](formulas.md) are very important: they are used for calculating values, making parts of the form visible or readonly, and more. However, they can be difficult to debug, and so far Form Builder didn't have a way to show all formulas in a central location.

A new, still experimental feature allows you to inspect formulas. You access it under the "Test" menu.

![The "Inspect Formulas" button](/form-builder/images/inspect-formulas-button.png)

## Availability

[SINCE Orbeon Forms 2021.1]

This feature considered experimental with Orbeon Forms 2021.1, but only because it is still fairly basic! However, it is still useful, and we hope to improve it in newer versions of Orbeon Forms.

## Usage

The formulas inspector allows you to see, in a table, the following formulas used in the form for:

- Initial Value
- Calculated Value
- Visibility
- Required
- Read-Only

You select the category of formulas from the list. Then the results table shows, in order, all the formulas of that category associated with form controls, as well as the controls they depend on (see below).

The following example show dependencies between "Calculated Value" formulas and controls. A color scheme indicates the dependency relationships between controls via formulas.

![Example showing "Calculated Value" dependencies](/form-builder/images/inspect-formulas-example.png)

## Results table

### Basics

Each row shows a formula. The columns are as follows:

1. Position of the result in the list of results
2. Name of the control in Form Builder
3. Formula
4. Variable dependencies (see below)
5. Datatype associated with the form control
6. Control value if present
7. Path dependencies (see below)

### Variable dependencies

A formula can [refer to control values](formulas.md#referring-to-control-values-from-formulas) using the notation `$foo` where `foo` is the control name. A formula that refers to other controls via this notation lists these dependencies in the *Control dependencies* column.

In addition, when selecting a given row in the results table, the row highlights, and up to four other different colors are used to highlight variable dependencies:

- __Direct dependency:__ controls that the current formula directly depends on 
- __Transitive dependency:__ controls that the current formula indirectly depends on 
- __Direct influence:__ controls that are directly influenced by the current formula
- __Transitive influence:__ controls that are indirectly influenced by the current formula 

When a given row is selected, checking the "Show related rows only" checkbox allows focusing the view on only the controls that depend on or are influenced by the selected row.

### Path dependencies

xxx

## See also

- [Formulas](formulas.md)