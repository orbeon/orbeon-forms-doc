## Introduction

An important part of designing a form is to prevent incorrect data from being captured. For example:

- an applicant's first and last names is required
- an applicant's age must be a positive number and has to be greater than a minimum
- an id number has to follow a specific syntax

If such conditions are not met, the user must see an *error* and cannot submit the form until they are corrected.

In addition, some values might be correct, but the user should be encouraged to pay special attention to them. In such cases, the user should see a *warning* or *informational message* before submitting the form.

Form Builder supports this kind of validations via the "Validations and Alerts" tab of the "Control Settings" dialog. You open the dialog with the "Control Settings" icon to the right of each control.

*NOTE: Prior to Orbeon Forms 4.3, the dialog was called "Validation Properties". Orbeon Forms 4.3 combines dialogs and adds functionality.*

![Control validation settings](images/fb-validation.png)

## Validation types

The value associated with a control can be validated with 3 different validation types:

1. *Required*. This indicates whether the value can be empty or not.
2. *Data Type*. For example `string`, `decimal`, `date`, or `time`.
3. *Constraint*. A custom formula, expressed in XPath, which determines whether the value is valid or not for a certain validation level.

## Required validation

This simple validation has only two possibilities:

- *Yes*: a value is required and cannot be empty. (*NOTE: Blank spaces count as "not empty".*)
- *No*: a value is not required and can be empty.

When the value is required, an asterisk appears next to the control to signify to the user that the value is required.

At runtime, if the value is required but not empty, the value is marked as invalid.

## Data type validation

The list of data types includes:

- common types such as a plain `string`, `decimal`, `date`, `time`, `boolean`, or `email`
- XML Schema types (only if an XML Schema with simple types was attache to the form)

At runtime, if the value is required and does not match the specified datatype, the value is marked as invalid.

*NOTE: When selecting certain controls from the toolbox, such as "Email", "Date", "Time", and "Date and Time", the appropriate data type is already selected by Form Builder. Changing the type to a different type might change the appearance of the control to match the type selected.*

If an XML Schema containing simple types has been attached to the form (Orbeon Forms PE only), the simples types are listed in the Schema Type menu.

Either a built-in data type or an XML Schema data type can be selected. If you select an XML Schema data type, the built-in data type is automatically reset. Similarly, if you select a built-in data type, the XML Schema data type is reset.

At runtime, if the value is required and does not match the specified datatype, the value is marked as invalid. For example, if the value must be an `integer`, the value "John" is invalid.

*NOTE: If the control is of type `string`, doesn't have a constraint and is not required, then any value entered is valid. This is the default for input fields and text areas.*

When an XML Schema data type is selected:

- If *Required* is set to *Yes*, the control is still made required, and an asterisk appears.
- If *Required* is set to *No*, the value must still match the definition of the XML Schema type to be valid. If the XML Schema type requires a non-empty value, setting *Required* to *No* does not make the value optional.

## Constraint validation

[SINCE Orbeon Forms 4.3]

- There can be more than one constraint applied to a given control.
- You add constraints with the `+` icon and remove them with the `-` icon.
- Each constraint can have a *level* associated with it and a custom alert message.

Each constraint is a boolean XPath expression running with the XML element containing the value as context item.

For example the following expression, which would make sense for a birthday date field, checks that the user is 18 year old or older:

    . <= (current-date() - xs:yearMonthDuration("P18Y"))

The constraint *fails* if the expression doesn't return `true()`. This also means that it fails if there is an error while running the constraint.

## Control validity

A control value (entered by the user, constant, or calculated) is either *valid* or *invalid*. It is invalid if any of the following conditions is met:

- It is required but remains empty.
- It does not match the selected data type.
- There is a failed *error* constraint.

## Validation levels

If a control is valid, it can have a *warning* level. This is the case if there is at least one failed *warning* constraint.

If a control doesn't have a warning level, it can have an *info* level. This is the case if there is at least one failed *info* constraint.

A warning or info level does not make the control value invalid and it is still possible to submit form data.

*NOTE: As of Orbeon Forms 4.3, it is not possible to associate a validation level to the required or data type validations: they always use the error level.*

## Localization of messages

All alert messages can be localized.

When opening the dialog, the current language of the form determines the language used. To switch languages:

- close the dialog
- select another form language
- reopen the dialog

## Alert messages

When the user enters data, if the value is invalid or if the control has a warning or info level, the control is highlighted and one or more alert messages are shown. The message is selected as follows, generally following the philosophy of "more specific messages win over less specific messages":

- If data type validation has failed:
    - The default alert message for the control is used if available, or a global default Form Runner message is used otherwise.
    - In this case, constraints are not checked which means that no constraint-specific message is shown.
- If required or data type validation has failed:
    - The default alert message for the control is used if available, or a global default Form Runner message is used otherwise.
    - However, if there is one or more failed error constraints with specific messages, those messages are used.
- If at least one error constraint has failed:
    - If no specific alert message is specified for the validation, the default alert message for the control is used if available, or a global default Form Runner message is used otherwise.
    - If a specific alert message is specified, then it is used.
    - More than one message can show is several error constraints have failed.
- Only if the control is valid, if at least one warning constraint has failed:
    - The specific alert message is used.
    - More than one message can show is several warning constraints have failed.
- Only if the control is valid and doesn't have any failed warning constraints, if at least one info constraint has failed:
    - The specific alert message is used.
    - More than one message can show is several info constraints have failed.

*NOTE: As of Orbeon Forms 4.3, it is not possible to associate specific alert message to the required or data type validations: they always use the default or global alert message.*

Alert messages appear:

- under the control value
- in the Error Summary section of the form
- as badge counts in the navigation bar
- as general count in the browser's title bar

![Alert Messages](images/fr-validation.png)

## Validation errors and review messages dialog

By default, when saving or sending form data, the following happens:

- If any control value is invalid, a dialog shows and the operation is stopped.
- If all controls are invalid and there are no warning or info messages, the operation continues.
- If all controls are invalid and there is at least one warning or info message, the "Review Messages" dialog shows.

![Review Messages](images/fr-review-messages.png)

The user has the following choices:

- Stop the operation, close the dialog and review the warnings and/or informational messages.
- Continue the operation, ignoring the warnings and/or informational messages.

These processes are entirely configurable. See [Buttons and Processes](/orbeon/orbeon-forms/wiki/Form-Runner:-Buttons-and-Processes) for more information.

Optionally, it is possible to annotate the XML data submitted with warning messages. See [Buttons and Processes](/orbeon/orbeon-forms/wiki/Form-Runner:-Buttons-and-Processes) for more information.