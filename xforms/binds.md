# XForms Binds

<!-- toc -->

## Introduction

The XForms `xf:bind` element is used to point to data in the data model. It allows to associate with data:

- an `id`
- a `name` (see [Model Bind Variables](../xforms/model-bind-variables.md))
- properties, called "Model Item Properties" or MIPs

This serves the following functions:

- in the model
  - determine which part of the data is valid, readonly, and relevant
  - apply calculations to the data (formulas)
  - export variables used in calculations (see [Model Bind Variables](../xforms/model-bind-variables.md))
- in the model and in the view
  - as an indirection to the data model, with the `bind` attribute or the `xxf:bind()` function

## Extensions

### Nested MIP elements

[SINCE Orbeon Forms 4.3 for `xf:constraint`, and 4.9 for other elements]

Instead of the `type`, `readonly`, `required`, `relevant`, `calculate`, `constraint`, and `xxf:default` attributes, you can use nested elements:

- `xf:type`
- `xf:readonly`
- `xf:required`
- `xf:relevant`
- `xf:calculate`
- `xf:constraint`
- `xxf:default` (see below)

All elements except `xf:type` (whose value is not an XPath expression but a type literal) have a `value` attribute. The following binds are equivalent:

```xml
<xf:bind
    ref="control-1"
    type="xf:decimal"
    xxf:default="42"
    constraint=". ge 10"
    required="true()"/>
```

and:

```xml
<xf:bind ref="control-1">
    <xf:type>xs:decimal</xf:type>
    <xxf:default value="42"/>
    <xf:constraint value=". ge 10"/>
    <xf:required value="true()"/>
    <xf:relevant value="$qty gt 0"/>
</xf:bind>
```

This enables two features:

- the ability to assign a specific `id` attribute to a property
- the ability to specify multiple `readonly`, `required`, `relevant`, and `constraint` rules (which combined using either a boolean "or" or a boolean "and")

See [XForms Validation](../xforms/validation.md) for details about the validation-related elements (`xf:type`, `xf:required`, and `xf:constraint`).

### Multiple binds pointing to the same node

_NOTE: This is scheduled to be standardized in XForms 2._

Properties (MIPs) have a default value:

- required: `false`
- valid: `true` (depends on a series of conditions, including `required`)
- relevant: `true`
- readonly: `false`

The resulting value of a property on a given node when multiple binds touch that node is the result of a boolean combination:

- required: boolean "or"
- valid: boolean "and"
- relevant: boolean "and"
- readonly: boolean "or"

The values also combined this way when multiple nested elements are specified on a same bind.

Consider the following example:

```xml
<xf:model>
    <xf:instance>
        <instance>
            <some-node/>
        </instance>
    </xf:instance>
    <xf:bind ref="instance()//*" required="true()"/>
    <xf:bind ref="instance()/some-node" required="false()"/>
</xf:model>
```

`some-node`'s required property is here set to true, and the order of the two binds doesn't matter.

_NOTE: This is the behavior with Orbeon Forms 3.9 onwards. With Orbeon Forms 3.8, the combination was based on bind order._

### Extension XPath functions

- The `xxf:bind()` function returns the node-set of a given bind.
- The `xxf:evaluate-bind-property()` function evaluates a property of a given bind.
- The `xxf:type()` function returns the type of the instance data node passed as parameter.

For details, see [XPath Function Library](FIXME XForms ~ XPath Function Library).

### Custom MIPs

You can place user-defined Model Item Properties (MIPs) on the `<xf:bind>` element.

When `xxf:custom-mips` is missing (see below), any attribute not in a standard Orbeon namespace is interpreted as a custom MIP:

```xml
<xf:bind ref="*" foo:bar="if (starts-with(., 'g')) then 'is-g' else 'is-not-g'"/>
```

The value of the attribute must be a valid XPath expression. The expression result is converted to a string to set the MIP value.

Custom MIPs have the side effect of placing CSS classes on controls bound to affected nodes. Class names are computed as the concatenation of:

* MIP attribute prefix
* "-"
* MIP attribute local name
* "-"
* MIP value

With the example above, the following class names can be set: `foo-bar-is-g` or `foo-bar-is-not-g`.

[SINCE Orbeon Forms 4.0.1]

The model supports the `xxf:custom-mips` attribute, which lists the names of the custom MIPs in use. When this attribute is present, only the attributes with names listed are interpreted as MIPs. Other non-built-in attributes are ignored.

When this attribute is missing, the backward-compatible behavior is enabled and all non-built-in attributes are considered custom MIPs.

It is recommended to use the `xxf:custom-mips` attribute to specify which attributes are custom MIPs.

```xml
<xf:model xxf:custom-mips="foo:bar foo:baz">
    <xf:bind ref="name" foo:bar="normalize-space(.) = ''" foo:baz="42"/>
</xf:model>
```

### Dynamic initial values

#### The xxf:default MIP

In XForms, default or initial values can be set by pre-populating an instance document's elements and attributes with initial data, for example:

```xml
<xf:instance>
    <form>
        <username>jdoe</username>
        ...
    </form>
</xf:instance>
```

For dynamic values, for example coming from request parameters or session values, there is no declarative notation and you must use `xforms-submit-done, `xforms-model-construct-done or `xforms-submit-ready`, which is sometimes cumbersome:

```xml
<xf:setvalue
    event="xforms-model-construct-done"
    ref="username"
    value="xxf:get-request-header('MY_USER')"/>
```

For convenience, Orbeon Forms support an extension model item property: `xxf:default`. It works like the standard `calculate`, except that it is evaluated only once, just before the first evaluation of the `calculate` expressions if any.

```xml
<xf:bind ref="username" xxf:default="xxf:get-request-header('MY_USER')"/>
```

#### Forcing recalculation of initial values with the recalculate action

The `<xf:recalculate>` supports an extension attribute, `xxf:defaults`, which, when set to `true`, forces the re-evaluation of initial values before performing the recalculation.

```xml
<xf:recalculate xxf:defaults="true"/>
```

The `xxf:defaults` attribute is an AVT so can include XPath expressions between curly brackets:

```xml
<xf:recalculate xxf:defaults="{instance()/status = 'dirty'}"/>
```

#### Evaluation of initial values upon insert

See [Evaluation of initial values upon insert](xforms/actions/repeat-insert-delete.md).

### Deferred rebuild, recalculate and revalidate

XForms provides actions to force a given model's rebuild, recalculate, revalidate. As XForms specifies it, these actions have an immediate effect.

Orbeon Forms provides an extension on those actions to defer the behavior of those actions by just setting the appropriate flags defined in the XForms specification, but not actually running the actions immediately. The actions will run as needed the next time the flags are checked.

```xml
<xf:rebuild     xxf:deferred="true"/>
<xf:recalculate xxf:deferred="true"/>
<xf:revalidate  xxf:deferred="true"/>
```

### Static appearance for read-only controls

Sometimes, read-only controls don't appear very nicely in web browsers. For example, a combo box will appear grayed out. It maybe be hard to read, and there is not much point showing a combo box since the user can't interact with it. Furthermore, with some browsers, like IE 6 and earlier, it is not even possible to make disabled controls appear nicer with CSS. In order to make read-only versions of forms look nicer, Orbeon Forms supports a special extension attribute that allows you to produce a "static" appearance for read-only controls. You enable this on your first XForms model:

```xml
<xf:model xxf:readonly-appearance="static">
    ...
</xf:model>
```

The attribute takes one of two vales: `static` or `dynamic` (the default). When using the value `static`, read-only controls do not produce disabled HTML form controls. This has one major limitation: you can't switch a control back to being read-write once it is displayed as read-only.

You can also set the `xxf:readonly-appearance` attribute directly on individual XForms controls.

See Form Runner's _Preview_ mode for an example of this feature in action.

## Tips

### Making an entire instance read-only

You often want to present a form without allowing the user to enter data. An easy solution is to use the `readonly` MIP in the model. By making for example the root element of an instance read-only, all the controls bound to any node of that instance will appear read-only (because the read-only property is inherited in an instance):

```xml
<xf:instance>
    <form>
        ...
    </form>
</xf:instance>
<xf:bind ref="instance()" readonly="true()"/>
```

## See also

- [XForms Validation](../xforms/validation.md)
- [XForms Model bind variables](xforms/model-bind-variables.md)
- [Better formulas with XPath type annotations](http://blog.orbeon.com/2013/01/better-formulas-with-xpath-type.html)
- [Formulas for summing values, done right](http://blog.orbeon.com/2013/08/formulas-for-summing-values-done-right.html)
- [Control required values with formulas in Orbeon Forms 4.7](http://blog.orbeon.com/2014/09/control-required-values-with-formulas.html)
- [Evaluation of initial values upon insert](xforms/actions/repeat-insert-delete.md)
- [Grid component](form-runner/component/grid.md)
