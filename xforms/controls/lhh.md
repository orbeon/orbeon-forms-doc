# Label, hint, help

<!-- toc -->

## External label, hint, help

Usually, an XForms label, hint, or help is nested within the control:

```xml
<xf:input ref="name">
    <xf:label>Name:</xf:label>
</xf:input>
```

Orbeon Forms supports external `<xf:label>`, `<xf:hint>` or `<xf:help>` elements using the `for` attribute:

```xml

<xf:label for="my-input">Name:</xf:label>

<xf:input ref="name" id="my-input"/>
```

This allows more flexibility to place label, hint, or help in a different location within the form.

## Enhanced repeat support

[SINCE Orbeon Forms 2017.2]

It is possible to place an external `<xf:label>`, `<xf:hint>` or `<xf:help>` outside of an `<xf:repeat>`:

```xml
<xf:label for="my-input" id="my-input-label">Name</xf:label>
<xf:hint  for="my-input" id="my-input-hint" >Name Hint</xf:hint>
<xf:help  for="my-input" id="my-input-help" >Name Help</xf:help>

<xf:repeat ref="row" id="my-repeat">
    <xf:input ref="input" id="my-input"/>
</xf:repeat>
```

In this case, the label, hint or help always appears on the page, but only once, and this even if the control within the
repeat appears more than once. This is useful, for example, to place a label in a table heading.

In the resulting HTML, Orbeon Forms generates ARIA attributes to link repeated controls to the external label, hint or
alert:

- `aria-labelledby` for the label
- `aria-describedby` for the hint
- `aria-details` for the help


## See also 

- [Validation](validation.md)
- [Grid component](../../form-runner/component/grid.md)
