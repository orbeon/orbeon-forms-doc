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
ORBEON.fr.API.getForm().findControlsByName('street-address')
```

### Setting a control's value

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

The `setControlValue()` function allows you to set the value of a Form Runner control.

```typescript
function setControlValue(
    controlName : string,
    controlValue: string | int,
    index       : int[]
): Promise<void> | undefined
```

[//]: # ( | number | string[] | number[])

| Name             | Required | Type                   | Description                                                                    | Since                       |
|------------------|----------|------------------------|--------------------------------------------------------------------------------|-----------------------------|
| **controlName**  | Yes      | `string`               | The name of the Form Runner control                                            | 2023.1                      |
| **controlValue** | Yes      | `string` \| `int`      | The value to set                                                               | 2023.1 (2024.1.2 for `int`) |
| **index**        | No       | `int[]` \| `undefined` | If specified, and the control is repeated, the 0-based position of the control | 2023.1.1                    |

Here is how to set the value of a text field called `my-field` to the value "Hello!":

```javascript
ORBEON.fr.API.getForm().setControlValue('my-field', 'Hello!')
```

For single selection controls, pass the index of the item to select:

```javascript
ORBEON.fr.API.getForm().setControlValue('my-single-selection', '2')
```

For multiple selection controls, pass a space-separated string of item indexes:

```javascript
ORBEON.fr.API.getForm().setControlValue('checkboxes', '0 1 3')
```

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

[\[SINCE Orbeon Forms 2023.1.1\]](/release-notes/orbeon-forms-2023.1.1.md)

The `setControlValue()` function returns either:

- a JavaScript `Promise` which resolves when the value is set on the server
- or `undefined` if
    - the control is not found
    - the control is an XBL component which doesn't support the JavaScript lifecycle

If a control is repeated, use the optional `index` parameter to specify which repetition to set. The index is 0-based.

```javascript
ORBEON.fr.API.getForm().setControlValue('my-repeated-control', 'Anvil', 3)
```

[\[SINCE Orbeon Forms 2024.1.2\]](/release-notes/orbeon-forms-2024.1.2.md)

For single-selection controls, you can also pass an integer value rather than a string:

```javascript
ORBEON.fr.API.getForm().setControlValue('my-single-selection', 2)
```

### Getting a control's value

[\[SINCE Orbeon Forms 2024.1.2\]](/release-notes/orbeon-forms-2024.1.2.md)

The `getControlValue()` function allows you to get the value of a Form Runner control.

```typescript
function getControlValue(
    controlName : string,
    index       : int[]
): string | undefined
```

[//]: # ( | number | string[] | number[])

| Name             | Required | Type                   | Description                                                                    | Since    |
|------------------|----------|------------------------|--------------------------------------------------------------------------------|----------|
| **controlName**  | Yes      | `string`               | The name of the Form Runner control                                            | 2024.1.2 |
| **index**        | No       | `int[]` \| `undefined` | If specified, and the control is repeated, the 0-based position of the control | 2024.1.2 |

Here is how to get the value of a text field called `my-field`:

```javascript
ORBEON.fr.API.getForm().getControlValue('my-field')
```

The `getControlValue()` function returns either:

- a `string` with the value of the control
- or `undefined` if
    - the control is not found
    - the control is an XBL component which doesn't support the JavaScript lifecycle

### Activating a form control

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

The `activateControl()` function allows you to activate a Form Runner control. This is useful to activating buttons that are present within the form (as opposed to process buttons, which are typically placed at the bottom of the form). However, you can also activate other controls, in particular Text Fields.

```typescript
function activateControl(
    controlName: string
): Promise<void>
```

| Name            | Required | Type     | Description                         |
|-----------------|----------|----------|-------------------------------------|
| **controlName** | Yes      | `string` | The name of the Form Runner control |

The following controls are supported:

- Text field (as if the user presses the Return or Enter key)
- Button

[\[SINCE Orbeon Forms 2023.1.1\]](/release-notes/orbeon-forms-2023.1.1.md)

The `activateControl()` function returns either:

- a JavaScript `Promise` which resolves when the activation has run on the server
- or `undefined` if
    - the control is not found
    - the control is not a trigger or input control

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

You can set a control's value using the [XForms JavaScript API](/xforms/client-side-javascript-api.md).

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

## Wizard API

### Focusing on a control

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

[\[SINCE Orbeon Forms 2023.1.2\]](/release-notes/orbeon-forms-2023.1.2.md)

This now also works with the `strict` and `lax` validation modes. In these modes, the wizard will check whether the switch to the requested wizard page is allowed.

- If so, it will switch and focus on the control.
- If not, it will ignore the `focus()` request. 

## Error Summary API

### Listening for Error Summary navigation

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

## Pager API

[SINCE Orbeon Forms 2025.1]

The Pager API allows you to interact with pagination controls in repeated sections.

### Getting a pager for a specific repeated section

The `getPager()` function returns a pager object for a specific repeated section by name. This method is called on the `FormRunnerForm` object.

```typescript
function getPager(repeatedSectionName: string): Pager | undefined
```

| Name                     | Required | Type     | Description                          |
|--------------------------|----------|----------|--------------------------------------|
| **repeatedSectionName**  | Yes      | `string` | Name of the repeated section control |

It returns a `Pager` object if found, or `undefined` if:
- The repeated section is not found
- The repeated section is disabled
- The repeated section doesn't have pagination enabled

Example:

```javascript
const pager = ORBEON.fr.API.getForm().getPager('my-repeated-section');
if (pager != null) {
    console.log('Current page:', pager.pageNumber);
}
```

### Getting all pagers in the form

The `getPagers()` function returns all pager objects in the current form. This method is called on the `FormRunnerForm` object.

```typescript
function getPagers(): Pager[]
```

It returns an array of `Pager` objects for all repeated sections that have pagination enabled and are not disabled.

Example:

```javascript
const pagers = ORBEON.fr.API.getForm().getPagers();
pagers.forEach(pager => {
    console.log(`Section: ${pager.repeatedSectionName}, page: ${pager.pageNumber}`);
});
```

### The `Pager` object

The `Pager` object provides information about and control over pagination in a repeated section.

#### Properties

All properties except the section name return `number`:

| Property                 | Type     | Description                                           |
|--------------------------|----------|-------------------------------------------------------|
| **repeatedSectionName**  | `string` | Name of the repeated section                          |
| **itemFrom**             | `number` | Index of the first item on the current page (1-based) |
| **itemTo**               | `number` | Index of the last item on the current page (1-based)  |
| **itemCount**            | `number` | Total number of items across all pages                |
| **pageSize**             | `number` | Number of items displayed per page                    |
| **pageNumber**           | `number` | Current page number (1-based)                         |
| **pageCount**            | `number` | Total number of pages                                 |

Example:

```javascript
const pager = ORBEON.fr.API.getForm().getPager('employees');
if (pager != null) {
    console.log(`Showing items ${pager.itemFrom}-${pager.itemTo} of ${pager.itemCount}`);
    console.log(`Page ${pager.pageNumber} of ${pager.pageCount}`);
}
```

#### Setting the current page

The `setCurrentPage()` function navigates to a specific page.

```typescript
function setCurrentPage(page: number): void
```

| Name     | Required | Type     | Description                          |
|----------|----------|----------|--------------------------------------|
| **page** | Yes      | `number` | Page number to navigate to (1-based) |

Example:

```javascript
ORBEON.fr.API.getForm().getPager('my-repeated-section').setCurrentPage(3);
```

#### Listening for page changes

You can listen for page change events using the `addPageChangeListener()` function.

```typescript
function addPageChangeListener(
    listener: (event: PageChangeEvent) => void
): void
```

The `PageChangeEvent` object contains information about the page change:

```typescript
type PageChangeEvent = {
    readonly repeatedSectionName: string; // Repeated section name
    readonly previousPage       : number; // Previous page number (1-based)
    readonly currentPage        : number; // Current page number (1-based)
    readonly pageCount          : number; // Total number of pages
    readonly itemCount          : number; // Total number of items across all pages
}
```

Example:

```javascript
const pager = ORBEON.fr.API.getForm().getPager('my-repeated-section');

function onPageChange(event) {
    console.log(`Section ${event.repeatedSectionName} changed from page ${event.previousPage} to page ${event.currentPage}`);
}

pager.addPageChangeListener(onPageChange);
```

#### Removing page change listeners

You can remove a previously added listener using the `removePageChangeListener()` function.

```typescript
function removePageChangeListener(
    listener: (event: PageChangeEvent) => void
): void
```

The `listener` parameter must be the same function object that was passed to `addPageChangeListener()`.

Example:

```javascript
const pager = ORBEON.fr.API.getForm().getPager('my-repeated-section');

// Remove the previously added listener
pager.removePageChangeListener(onPageChange);
```

## See also

- [Adding your own JavaScript](/configuration/properties/form-runner.md#adding-your-own-javascript)
- [XForms JavaScript API](/xforms/client-side-javascript-api.md)
