# Label, hint, help

## External label, hint, help

Usually, an XForms label, hint, or help is nested within the control:

```markup
<xf:input ref="name">
    <xf:label>Name:</xf:label>
</xf:input>
```

Orbeon Forms supports external `<xf:label>`, `<xf:hint>` or `<xf:help>` elements using the `for` attribute:

```markup
<xf:label for="my-input">Name:</xf:label>

<xf:input ref="name" id="my-input"/>
```

This allows more flexibility to place label, hint, or help in a different location within the form.

## Enhanced repeat support

\[SINCE Orbeon Forms 2017.2]

It is possible to place an external `<xf:label>`, `<xf:hint>` or `<xf:help>` outside of an `<xf:repeat>`:

```markup
<xf:label for="my-input" id="my-input-label">Name</xf:label>
<xf:hint  for="my-input" id="my-input-hint" >Name Hint</xf:hint>
<xf:help  for="my-input" id="my-input-help" >Name Help</xf:help>

<xf:repeat ref="row" id="my-repeat">
    <xf:input ref="input" id="my-input"/>
</xf:repeat>
```

In this case, the label, hint or help always appears on the page, but only once, and this even if the control within the repeat appears more than once. This is useful, for example, to place a label in a table heading.

In the resulting HTML, Orbeon Forms generates ARIA attributes to link repeated controls to the external label, hint or alert:

* label: `aria-labelledby`&#x20;
* hint: `aria-describedby`&#x20;
* help: `aria-details`&#x20;

## See also

* [Validation](https://github.com/orbeon/orbeon-forms-doc/tree/c432b92f4f85b0983a3ce0b85bb2bdd4e53d043e/xforms/controls/validation.md)
* [Grid component](../../form-runner/component/grid.md)
