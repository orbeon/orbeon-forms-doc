# Actions Extensions



## Extension actions

### The `<xxf:setvisited>` action

The `<xxf:setvisited>` action allows updating the "visited" flag of a control. It has the following attributes:

* `control`: id of the initial control to update
* `visited`: if `true`, mark the control(s) as visited, otherwise as not visited
* `recurse`: if `true`, update the given control as well as descendant controls, otherwise update just the specified control

All attributes are AVTs.

Example:

```xml
<xxf:setvisited
    control="{$control-id}"
    visited="true"
    recurse="true"/>
```

See also the `xxf:visited()` function.

## Extension attributes

### xxf:deferred-updates

The `xxf:deferred-updates` attribute is supported on the following actions:

* `<xf:setfocus>`
* `<xf:setindex>`
* `<xf:toggle>`

By default, as per XForms 1.1, these actions first invoke [deferred update behavior](https://www.w3.org/TR/xforms/#action-deferred-update-behavior).

Setting this attribute to `false` disables invoking deferred update behavior. This can lead to performance improvements when a large number of such actions run in a sequence.

This attribute is an [AVT](/xforms/attribute-value-templates.md) which allows for dynamic evaluation of the attribute.

Examples:

```xml
<xf:setindex
    repeat="my-repeat"
    index="2"
    xxf:deferred-updates="false"/>

<xf:setindex
    repeat="my-repeat"
    index="2"
    xxf:deferred-updates="{count(item) > 0}"/>
```

### Option to not toggle ancestors `<xf:toggle>` controls

[SINCE Orbeon Forms 2021.1]

With Orbeon Forms, the `<xf:toggle>` action also causes ancestor `<xf:switch>` controls, if present, to toggle so that every ancestor `<xf:case>` is visible.

Orbeon Forms adds an extension attribute, `xxf:toggle-ancestors="false"`, which allows toggling only the given `<xf:switch>`, even if it is contained within ancestor `<xf:switch>`/`<xf:case>` controls that are not visible, without toggling those.

For example, with:

```xml
<xf:switch id="my-outer-switch">
    <xf:case id="my-outer-case-1"/>
    <xf:case id="my-outer-case-2">
        <xf:switch id="my-inner-switch">
            <xf:case id="my-inner-case-1"/>
            <xf:case id="my-inner-case-2"/>
        </xf:switch>
    </xf:case>
</xf:switch>
```

Assuming, initially:

- `my-outer-case-1` is selected
- `my-outer-case-2` is deselected
- `my-inner-case-1` is selected
- `my-inner-case-2` is deselected

The following action:

```xml
<xf:toggle 
    case="my-inner-case-2"
    xxf:toggle-ancestors="false"/>
```

Produces the following result:

- `my-outer-case-1` remains selected
- `my-outer-case-2` remains deselected
- `my-inner-case-1` is newly deselected
- `my-inner-case-2` is newly selected

While the default behavior, with action:

```xml
<xf:toggle 
    case="my-inner-case-2"/>
```

Produces the following result:

- `my-outer-case-1` is newly deselected
- `my-outer-case-2` is newly selected 
- `my-inner-case-1` is newly deselected
- `my-inner-case-2` is newly selected 

See also [XForms 1.1-compatible of switch/case](/configuration/properties/xforms.md#xforms-1.1-compatible-of-switch-case).

## See also

- [Keyboard events](/xforms/events-extensions-keyboard.md)
- [Extension events](/xforms/events-extensions-events.md)
- [Extension context information](/xforms/events-extensions-context.md)
- [Other event extensions](/xforms/events-extensions-other.md)
