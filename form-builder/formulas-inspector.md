# Formulas inspector

## Introduction

With Orbeon Forms, [formulas](formulas.md) are very important: they are used for calculating values, making parts of the form visible or readonly, and more. However, they can be difficult to debug, and so far Form Builder didn't have a way to show all formulas in a central location.

This new, still experimental feature allows you to inspect formulas. You access it under the "Test" menu.

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

You select the category of formulas from the list. Then the results table shows, in order, all the formulas of that category associated with form controls, as well as the controls they depend on or influence (see below).

The following example show dependencies between "Calculated Value" formulas and controls. A color scheme indicates the dependency relationships between controls via formulas.

![Example showing "Calculated Value" dependencies](/form-builder/images/inspect-formulas-example.png)

## Results table

### Basics

Each row shows a form control. The columns are as follows:

1. Position of the result in the list of results
2. Control name as defined in Form Builder
3. Formula
4. Control dependencies (see below)
5. Control datatype as defined in Form Builder
6. Control value if present
7. Path dependencies (see below)

There are several benefits to seeing all the formulas in this overview:

- You can see how many formulas you have in your form.
- You can quickly check them for correctness.
- You can see if the values they use have the correct datatype.

And so on! 

### Control dependencies

A formula can [refer to control values](formulas.md#referring-to-control-values-from-formulas) using the variable notation `$foo` where `foo` is the control name. A formula that refers to other controls with this notation lists these dependencies in the *Control dependencies* column.

In addition, when selecting a given row in the results table, the row highlights, and up to four other different colors are used to highlight variable dependencies:

- __Direct dependency:__ controls that the current formula directly depends on 
- __Transitive dependency:__ controls that the current formula indirectly depends on 
- __Direct influence:__ controls that are directly influenced by the current formula
- __Transitive influence:__ controls that are indirectly influenced by the current formula 

When a given row is selected, checking the "Show related rows only" checkbox allows focusing the view on only the controls that depend on or are influenced by the selected row.

Note that when you rename a control in Form Builder, all dependent formulas are automatically updated. See [Renaming of controls and formulas](/form-runner/feature/automatic-calculations-dependencies.md#renaming-of-controls-and-formulas).

### Path dependencies

In addition to *control dependencies*, a formula produces so-called *path dependencies*. These dependencies represent computational dependencies on the data. These are used to improve the performance of the evaluation of the formula.

- __Green circle:__ indicates that path dependencies were successfully established for the formula. The dependent paths show below.
- __Red circle:__ indicates that path dependencies were not established for the formula. 

If too many formulas, especially in a large form, cannot establish path dependencies, performance of the form can be decreased. The "Path dependencies" column can help indicate whether this is a problem.  

## See also

- [Examples of formulas](formulas-examples.md)
- [Formulas](formulas.md)