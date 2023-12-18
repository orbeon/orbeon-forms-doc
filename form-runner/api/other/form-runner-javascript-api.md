# Form Runner JavaScript API

## Getting a reference to a form

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

You can get a reference to an object representing a Form Runner form with the `getForm()` function:

```javascript
ORBEON.fr.API.getForm(formIdOrElem: string | HTMLElement): FormRunnerForm
```

The `formIdOrElem` parameter is described [below](#identifying-a-form-by-id-or-element).

## Identifying a form by id or element

The `formIdOrElem` parameter used in APIs can be:

- missing or `undefined`: this searches for the first Orbeon Forms form on the page
- a `string`: this is the namespaced id of the form
- an HTML element: this is the HTML form element, or a descendant or an HTML form element

If the form is not found, `null` is returned. If found, an object is returned, which contains methods described below.

## The `FormRunnerForm` object

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

### Finding a Form Runner control by name

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

The `findControlsByName()` function returns the HTML element(s) corresponding to the given Form Runner control name.

*NOTE: The control must be visible or it will not be found. In particular, if the control is in a hidden wizard page, the control will not be found.*

A Form Runner control name is the name entered by the form author in Form Builder. Examples:

- `first-name`
- `street-address`

```javascript
function findControlsByName(
    controlName : string
): HTMLElement[]
```

| Name            | Required | Type     | Description                          |
|-----------------|----------|----------|--------------------------------------|
| **controlName** | Yes      | `string` | The name of the Form Runner control. |

If no control is found, an empty array is returned.

If there are multiple controls with the same name, the array will contain multiple elements. This can happen in the following cases:
 
- when controls are repeated, for example in a repeated grid or section
- when controls appear in the main form and section template and/or in different section templates

On the other hand, if the control exists but is not shown at a given time, for example if it is in a hidden wizard page, the array will be empty.

Example:

```javascript
ORBEON.fr.API.getFOrm().findControlsByName('street-address')
```

### Setting a control's value

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

The `setControlValue()` function allows you to set the value of a Form Runner control.

```javascript
function setControlValue(
    controlName: string,
    controlValue: string | number | string[] | number[]
): void
```

| Name            | Required | Type                                             | Description                         |
|-----------------|----------|--------------------------------------------------|-------------------------------------|
| **controlName** | Yes      | `string`                                         | The name of the Form Runner control |
| **controlValue**| Yes      | `string` or `number` or `string[]` or `number[]` | The value of values to set          |

Here is how to set the value of a text field called `my-field` to the value "Hello!":

```javascript
ORBEON.fr.API.getForm().setControlValue('my-field', 'Hello!')
```

For single selection controls, you pass the index of the value to select, either as a string or as a number:

```javascript
ORBEON.fr.API.getForm().setControlValue('my-single-selection', 2)
```

or:

```javascript
ORBEON.fr.API.getForm().setControlValue('my-single-selection', '2')
```

For multiple selection controls, you pass a space-separated string of indexes:

[//]: # (or an array of indexes of the values to select:)

[//]: # ()
[//]: # (```javascript)

[//]: # (ORBEON.fr.API.getForm&#40;&#41;.setControlValue&#40;'my-multiple-selection', '2 7 8'&#41;)

[//]: # (```)

[//]: # ()
[//]: # (or:)

[//]: # ()
[//]: # (```javascript)

[//]: # (ORBEON.fr.API.getForm&#40;&#41;.setControlValue&#40;'my-multiple-selection', [2, 7, 8]&#41;)

[//]: # (```)

[//]: # ()
[//]: # (or:)

[//]: # ()
[//]: # (```javascript)

[//]: # (ORBEON.fr.API.getForm&#40;&#41;.setControlValue&#40;'my-multiple-selection', ['2', '7', '8']&#41;)

[//]: # (```)

The following controls are supported:

- Text field
- Plain text area
- Dropdown
- Checkboxes
- Radio buttons
- Number
- Currency
- Date
- Time

### Activating a form control

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

The `activateControl()` function allows you to activate a Form Runner control. This is useful to activating buttons that are present within the form (as opposed to process buttons, which are typically placed at the bottom of the form). However, you can also activate other controls, in particular Text Fields.

```javascript
function activateControl(controlName: string): void
```

| Name            | Required | Type     | Description                         |
|-----------------|----------|----------|-------------------------------------|
| **controlName** | Yes      | `string` | The name of the Form Runner control |

The following controls are supported:

- Text field (as if the user presses the Return or Enter key)
- Button

### Telling whether the form data is safe

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

```javascript
function isFormDataSafe(): boolean
```

Orbeon Forms supports the notion that form data can be "safe" or not: specifically, it is safe if it's been saved to a database.

This function allows you to tell whether the data is safe or not.

Example:

```javascript
ORBEON.fr.API.getForm().isFormDataSafe()
```

See also [the `set-data-status` action](/form-runner/advanced/buttons-and-processes/actions-form-runner.md#set-data-status).

### Activating a process button

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

```javascript
function activateProcessButton(buttonName: string): void
```

Process buttons are typically buttons that are not directly part of the form, but placed at the bottom of the form. They include functions such as "Save", "Send", "Next", etc.

You can activate a process button with this function by specifying the button name. Example:

```javascript
ORBEON.fr.API.getForm().activateProcessButton("wizard-next")
```

### Adding and removing process callback functions

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

Form Runner simple processes now support a [`callback()` action](/form-runner/advanced/buttons-and-processes/actions-form-runner.md#callback) server action that allows client-side callback functions to be called.

You add a callback function on the client using:

```javascript
function addCallback(name: string, fn: () => void): void
```

| Name     | Required | Type         | Description                                        |
|----------|----------|--------------|----------------------------------------------------|
| **name** | Yes      | `string`     | Name passed to the server-side `callback()` action |
| **fn**   | Yes      | `() => void` | Callback function to be called on the client       |

You remove a callback function on the client using:

```javascript
function removeCallback(name: String, fn: () => void): void
```

The `name` and `fn` parameters must be the same as those passed to `addCallback()`. 

## Finding a Form Runner control by name

[SINCE Orbeon Forms 2017.2]

The `findControlsByName()` function returns the HTML element(s) corresponding to the given Form Runner control name.

```javascript
ORBEON.fr.API.findControlsByName(
    controlName  : string, 
    formIdOrElem?: HTMLElement
): HTMLElement[]
```

| Name             | Required | Type                                  | Description                                                                     |
|------------------|----------|---------------------------------------|---------------------------------------------------------------------------------|
| **controlName**  | Yes      | `string`                              | The name of the Form Runner control.                                            |
| **formIdOrElem** | No       | `formIdOrElem: string \| HTMLElement` | See [Identifying a form by id or element](#identifying-a-form-by-id-or-element) |

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

Prefer using first the `getForm()` function, and then, on the object returned, the `findControlsByName()` function.

## Setting a control's value

You can set a control's value using the [XForms client-side JavaScript API](/xforms/client-side-javascript-api.md).

For example, here is how to set the value of a text field called `my-field` to the value "Hello!":

```javascript
ORBEON.xforms.Document.setValue(
    ORBEON.fr.API.findControlsByName('my-field')[0],
    'Hello!'
)
```

NOTE: For dropdowns created with Form Builder, the API does not provide direct support as of Orbeon Forms 2019.1. You can do it with the following JavaScript:

```javascript
ORBEON.xforms.Document.setValue(
    ORBEON.fr.API.findControlsByName('my-dropdown')[0].querySelector(".xforms-select1"),
    1
)
```

Where the value  you pass corresponds to the position of the item starting at `0`.

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

Prefer using first the `getForm()` function, and then, on the object returned, the `setControlValue()` function.

## Telling whether the form data is safe

[SINCE Orbeon Forms 2019.2]

```javascript
ORBEON.fr.API.isFormDataSafe(
    formIdOrElem: string | HTMLElement
): boolean
```

| Name             | Required | Type                                  | Description                                                                     |
|------------------|----------|---------------------------------------|---------------------------------------------------------------------------------|
| **formIdOrElem** | No       | `formIdOrElem: string \| HTMLElement` | See [Identifying a form by id or element](#identifying-a-form-by-id-or-element) |

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

Prefer using first the `getForm()` function, and then, on the object returned, the `isFormDataSafe()` function.

## Focusing on a control

[SINCE Orbeon Forms 2017.2]

The `wizard.focus()` function sets keyboard focus on a Form Runner control by name, including toggling wizard pages first if needed.

```javascript
ORBEON.fr.API.wizard.focus(
    controlName   : String,
    repeatIndexes : Int[]?
)
```

| Name              | Required | Type           | Description                          |
|-------------------|----------|----------------|--------------------------------------|
| **controlName**   | Yes      | `string`       | The name of the Form Runner control. |
| **repeatIndexes** | No       | array of `Int` | Repeat indexes.                      |

This function doesn't have any effect if the control is readonly or non-relevant.

Example:

```javascript
ORBEON.fr.API.wizard.focus('street-address')
```

The optional `repeatIndexes` parameter allows reaching controls within repeats. For example, with one level of
repeat:

```javascript
ORBEON.fr.API.wizard.focus('comment', [ 2 ])
```

accesses the second iteration of the `comment` field.

Similarly, for nested repeats, you add as many elements in the array as there are nested repeats:

```javascript
ORBEON.fr.API.wizard.focus('comment', [ 3, 2 ])
```

When `repeatIndexes` is not specified, if the field is repeated, a single field is selected following the current
repeat indexes.  

*NOTE: This only supports the wizard's `free` validation mode. `lax` and `strict` are not yet supported.*

## Listening for Error Summary navigation

[SINCE Orbeon Forms 2022.1]

For analytics purposes, it can be useful to capture when the user is interacting with the Error Summary.

You can do so with the `errorSummary.addNavigateToErrorListener()` function: 

```typescript
ORBEON.fr.API.errorSummary.addNavigateToErrorListener(
    listener: (e: ErrorSummaryNavigateToErrorEvent) => void
)
```

Example:

```javascript
ORBEON.fr.API.errorSummary.addNavigateToErrorListener(
    function(e) { console.log(e); }
)
```

The parameter to the function is an event object defined as follows:

```typescript
type ErrorSummaryNavigateToErrorEvent = {
    readonly validationPosition: number;   // positive integer
    readonly controlName       : string;   // Form Runner control name
    readonly repetitions       : number[]; // 1-based repeat indexes if within repeated grids/sections
    readonly controlLabel      : string;   // control label in the current language
    readonly validationMessage : string;   // validation message in the current language
    readonly validationLevel   : string;   // "error", "warning", or "info"
    readonly sectionNames      : string[]; // ancestor section names
    readonly elementId         : string;   // id of the element in the DOM (can be missing from the DOM!)
}
```

The listener can be removed with `removeNavigateToErrorListener()` by passing the same function object as parameter.

```javascript
ORBEON.fr.API.errorSummary.removeNavigateToErrorListener(fn)
```

## See also

- [Adding your own JavaScript](/configuration/properties/form-runner.md#adding-your-own-javascript)
- [XForms client-side JavaScript API](/xforms/client-side-javascript-api.md)
