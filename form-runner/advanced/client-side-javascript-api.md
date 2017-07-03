# Client-side JavaScript API

<!-- toc -->

## Finding a Form Runner control by name

[SINCE Orbeon Forms 2017.2]

The `findControlsByName()` function returns the HTML element(s) corresponding to the given Form Runner control name.

A Form Runner control name is the name entered by the form author in Form Builder. Examples:

- `first-name`
- `street-address`

```javascript
ORBEON.fr.API.findControlsByName(name: string, formElem?: HTMLElement): HTMLElement[]
```

| Name | Required | Type | Description |
| ---- | -------- | ---- | ----------- |
| **name**     |  Yes |  `String`| The name of the Form Runner control.
| **formElem** |  No | `HTMLElement` | The form object that corresponds to the XForms control you want to deal with. This argument is only needed when you have multiple "XForms forms" on the same HTML page, which only happens if you are running your form in embedded mode and you have multiple forms on the same page.<br><br>When the parameter is not present or null, the first form on the HTML page with the class `xforms-form` is used.

If no control is found, an empty array is returned.

If there are multiple controls with the same name, the array will contain multiple elements. This can happen in the following cases:
 
- when controls are repeated, for example in a repeated grid or section
- when controls appear in the maine form and section template and/or in different section templates
