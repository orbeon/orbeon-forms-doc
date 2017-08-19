# Client-side JavaScript API

<!-- toc -->

## Finding a Form Runner control by name

[SINCE Orbeon Forms 2017.2]

The `findControlsByName()` function returns the HTML element(s) corresponding to the given Form Runner control name.

A Form Runner control name is the name entered by the form author in Form Builder. Examples:

- `first-name`
- `street-address`

```javascript
ORBEON.fr.API.findControlsByName(
    controlName : string, 
    formElem?   : HTMLElement
): HTMLElement[]
```

| Name | Required | Type | Description |
| ---- | -------- | ---- | ----------- |
| **controlName** |  Yes |  `String`     | The name of the Form Runner control. |
| **formElem**    |  No  | `HTMLElement` | The form object that corresponds to the XForms control you want to deal with. This argument is only needed when you have multiple "XForms forms" on the same HTML page, which only happens if you are running your form in embedded mode and you have multiple forms on the same page.<br><br>When the parameter is not present or null, the first form on the HTML page with the class `xforms-form` is used. |

If no control is found, an empty array is returned.

If there are multiple controls with the same name, the array will contain multiple elements. This can happen in the following cases:
 
- when controls are repeated, for example in a repeated grid or section
- when controls appear in the main form and section template and/or in different section templates

## Focusing on a control

[SINCE Orbeon Forms 2017.2]

The `wizard.focus()` function sets keyboard focus on a Form Runner control by name, including toggling wizard pages
first if needed.

```javascript
ORBEON.fr.API.wizard.focus(
    controlName   : String,
    repeatIndexes : Int[]?
)
```

| Name | Required | Type | Description |
| ---- | -------- | ---- | ----------- |
| **controlName**   |  Yes |  `String` | The name of the Form Runner control. |
| **repeatIndexes** |  No  |  array of `Int` | Repeat indexes. |

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


## See also

- [XForms client-side JavaScript API](../../xforms/client-side-javascript-api.md)
