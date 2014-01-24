## Status

*This documentation is not complete yet.*

## CSS classes in use

The XForms engine place the following classes on HTML elements.

### Classes on control elements

- `xforms-control`: indicates a core control (excludes `group`, `switch`, and `repeat`)
- `xforms-input`, etc.: the kind of control
- `xforms-type-email`, etc.: the built-in datatype of the control, here `email`
- `xforms-type-custom-*`: the extension datatype of the control
- `xforms-visited`: the control has been visited, i.e. tabbed out once
- constraint and error classes
    - `xforms-invalid`; the control has an error level
    - `xforms-warning`; the control has a warning level
    - `xforms-info`; the control has an info level
- `xforms-disabled`: the control is non-relevant
- `xforms-readonly`: the control is readonly
- `xforms-required`: the control is required
    - `xforms-empty`: the control is required and empty
    - `xforms-filled`: the control is required and filled
- `xforms-incremental`: the control is in incremental mode
- appearance
    - `xforms-select1-appearance-full`: an `xf:select1` control with `appearance="full"`
- mediatype
    - `xforms-mediatype-foo`: the control has `mediatype="foo/*"`
    - `xforms-mediatype-foo-bar`: the control has `mediatype="foo/bar"`
- `xforms-static`: the control is in static readonly mode

### Classes on XBL components

- `xbl-component`: indicate an XBL component
- `xbl-fr-foo-bar`: indicate an `foo:bar` component

### Classes on other elements

- `xforms-label`: a label element
- `xforms-help`: a help element
- `xforms-hint`: a hint element
- alerts
    - `xforms-alert`: an alert element
    - `xforms-active`: an active alert
- items
    - `xforms-items`: wrapper around all checkboxes/radio buttons
    - `xforms-selected`: wrapper around a selected checkboxe/radio button
    - `xforms-deselected`: wrapper around a deselected checkboxe/radio button
- `xforms-field`: in static readonly mode only, outputs which look like fields [SINCE Orbeon Forms 4.5]
    - `xf:input`, `xf:secret`, `xf:select`/`xf:select1`

### Classes on group, switch, case, and repeat

TODO