# Actions Extensions

## Extension actions

### The <xxf:setvisited> action

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

By default, as per XForms 1.1, these actions first invoke [deferred update behavior][4].

Setting this attribute to `false` disables invoking deferred update behavior. This can lead to performance improvements when a large number of such actions run in a sequence.

This attribute is an [AVT][5] which allows for dynamic evaluation of the attribute.

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

[2]: https://www.w3.org/TR/xforms11/#action
[4]: https://www.w3.org/TR/xforms/#action-deferred-update-behavior
[5]: http://wiki.orbeon.com/forms/doc/developer-guide/xforms-attribute-value-templates
