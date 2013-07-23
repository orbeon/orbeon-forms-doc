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

Constraints combine with a logical "AND" for a given level.

If there is a single error constraint, the following binds are equivalent:

```xml
<xf:bind ref="." id="input-bind" constraint="string-length() gt 1"/>

<xf:bind ref="." id="input-bind">
    <xf:constraint level="error" value="string-length() gt 1"/>
</xf:bind>
```

### Validation levels

[SINCE: Orbeon Forms 4.3]

In XForms, a control value can only be *valid* or *invalid*. Orbeon Forms adds the following additional validation levels:

- warning
- info

The default validation level is *error*.

As of Orbeon Forms 4.3, it is possible to set a constraint level for XPath constraints.

### Multiple alerts

[SINCE: Orbeon Forms 4.3]

TODO