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

TODO