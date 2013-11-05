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
- `xforms-mediatype-foo`: the control has `mediatype="foo/*"`
- `xforms-mediatype-foo-bar`: the control has `mediatype="foo/bar"`
- `xforms-static`: the control is in static readonly mode

### Classes on other elements

- `xforms-label`: a label element
- `xforms-help`: a help element
- `xforms-hint`: a hint element
- `xforms-alert`: an alert element
- `xforms-active`: an active alert
