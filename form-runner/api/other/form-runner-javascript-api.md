# Form Runner JavaScript API

## Finding a Form Runner control by name

[SINCE Orbeon Forms 2017.2]

The `findControlsByName()` function returns the HTML element(s) corresponding to the given Form Runner control name.

*NOTE: The control must be visible or it will not be found. In particular, if the control is in a hidden wizard page, the control will not be found.*

A Form Runner control name is the name entered by the form author in Form Builder. Examples:

- `first-name`
- `street-address`

```javascript
ORBEON.fr.API.findControlsByName(
    controlName : string, 
    formElem?   : HTMLElement
): HTMLElement[]
```

| Name            | Required | Type          | Description |
|-----------------|----------|---------------| ----------- |
| **controlName** | Yes      | `String`      | The name of the Form Runner control. |
| **formElem**    | No       | `HTMLElement` | The form object that corresponds to the XForms control you want to deal with. This argument is only needed when you have multiple "XForms forms" on the same HTML page, which only happens if you are running your form in embedded mode and you have multiple forms on the same page.<br><br>When the parameter is not present or null, the first form on the HTML page with the class `xforms-form` is used. |

If no control is found, an empty array is returned.

If there are multiple controls with the same name, the array will contain multiple elements. This can happen in the following cases:
 
- when controls are repeated, for example in a repeated grid or section
- when controls appear in the main form and section template and/or in different section templates

On the other hand, if the control exists but is not shown at a given time, for example if it is in a hidden wizard page, the array will be empty.

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
    ORBEON.jQuery(ORBEON.fr.API.findControlsByName('my-dropdown')).find('.xforms-select1')[0],
    1
)
```

Where the value  you pass corresponds to the position of the item starting at `0`.

## Telling whether the form data is safe

[SINCE Orbeon Forms 2019.2]

```javascript
ORBEON.fr.API.isFormDataSafe(
    formElem?   : HTMLElement
): boolean
```

| Name | Required | Type | Description |
| ---- | -------- | ---- | ----------- |
| **formElem**    |  No  | `HTMLElement` | The form object that corresponds to the XForms control you want to deal with. This argument is only needed when you have multiple "XForms forms" on the same HTML page, which only happens if you are running your form in embedded mode and you have multiple forms on the same page.<br><br>When the parameter is not present or null, the first form on the HTML page with the class `xforms-form` is used. |

Orbeon Forms supports the notion that form data can be "safe" or not: specifically, it is safe if it's been saved to a database.

This function allows you to tell whether the data is safe or not.

See also [the `set-data-status` action](/form-runner/advanced/buttons-and-processes/actions-form-runner.md#set-data-status).

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
| **controlName**   | Yes      | `String`       | The name of the Form Runner control. |
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
    readonly elementId        : string;   // id of the element in the DOM (can be missing from the DOM!)
    readonly errorPosition    : number;   // positive integer
    readonly repetitions      : number[]; // 1-based repeat indexes if within repeated grids/sections
    readonly controlName      : string;   // Form Runner control name
    readonly label            : string;   // label in the current language
    readonly validationMessage: string;   // validation message in the current language
    readonly validationLevel  : string;   // "error", "warning", or "info"
    readonly sectionNames     : string[]; // ancestor section names
}
```

The listener can be removed with `removeNavigateToErrorListener()` by passing the same function object as parameter.

```javascript
ORBEON.fr.API.errorSummary.removeNavigateToErrorListener(fn)
```

## See also

- [Adding your own JavaScript](/configuration/properties/form-runner.md#adding-your-own-javascript)
- [XForms client-side JavaScript API](/xforms/client-side-javascript-api.md)
