See also [Form Builder: Validation](/orbeon/orbeon-forms/wiki/Form-Builder:-Validation).

## Extensions

### Multiple constraints and alerts

[SINCE: Orbeon Forms 4.3]

XForms allows a single `constraint` attribute on the `xf:bind` element. Orbeon Forms extends this to support any number of nested `xf:constraint` elements, each specifying a single constraint:

```xml
<xf:bind ref="." id="input-bind">
    <xf:constraint id="length-constraint"    level="error"   value="string-length() gt 1"/>
    <xf:constraint id="uppercase-constraint" level="warning" value="for $first in substring(., 1, 1) return upper-case($first) = $first"/>
</xf:bind>
```

Each constraint applies to the enclosing `xf:bind`.

Attributes:

- `level` attribute: optional, specifies an alert level (defaults to `error`)
- `value`: XPath expression specifying the constraint

The `id` attribute is optional and useful to attach alerts.

Constraints combine with a logical "AND" for a given level. For example, if the error level is used (the default), the value is valid only if all constraints evaluate to `true()`.

If there is a single error constraint, the following binds are equivalent:

```xml
<xf:bind ref="." id="input-bind" constraint="string-length() gt 1"/>

<xf:bind ref="." id="input-bind">
    <xf:constraint level="error" value="string-length() gt 1"/>
</xf:bind>
```

### Validation levels

[SINCE: Orbeon Forms 4.3]

Orbeon Forms supports the following validation levels:

- error (which corresponds to XForms's valid/invalid)
- warning
- info

The default validation level is *error*.

The warning and info levels allow checking validation conditions that are weaker than errors.

Levels are hierarchical. If a control is valid, it can have a *warning* level. This is the case if there is at least one failed warning constraint.

If a control doesn't have a warning level, it can have an *info* level. This is the case if there is at least one failed info constraint.

A warning or info level does not make the control value invalid and it is still possible to submit form data.

*NOTE: As of Orbeon Forms 4.3, it is only possible to associate a warning or info validation level to a constraint specified with `xf:constraint`. It is not possible to associate these levels to the required or data type validations: these always use the error level.*

### Multiple alerts

[SINCE: Orbeon Forms 4.3]

A control can have more than one `xf:alert` elements. By default, an `xf:alert` is considered the *default* alert for the control and is active for all validation levels and constraints:

    <xf:alert>

If a `level` attribute is specified, the alert is active for the given levels:

    <xf:alert level="warning info">

If a `validation` attribute is specified, the alert is active only for the given validations:

    <xf:alert validation="c1 c2">

In this example, `c1` and `c2` refer to `id` attributes on `xf:constraint` elements. Only `xf:constraint` elements associated with a bind pointing to the node to which the control is bound are considered.

Blank `level` and `validation` attributes are equivalent to no attributes.

If both `level` and `validation` attributes are specified, `level` is ignored:

    <xf:alert level="error" validation="c1 c2">

More than one alert can be active at the same time, following a hierarchy:

- If the control doesn't have a validation level, no alert is active.
- If there is a level:
    - If there are alerts that match specific constraints, those alerts and no others are active.
    - Otherwise, if there are alerts that match the specific level, those alerts and no others are active.
    - Otherwise, if present, the default alert is active.
    - Otherwise, no alert is active.