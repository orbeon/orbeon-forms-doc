## Extensions

### Multiple constraints and alerts

[SINCE: Orbeon Forms 4.3]

XForms allows placing a single `constraint` attribute on the `xf:bind` element. Orbeon Forms extends this to support any number of nested `xf:constraint` elements, each specifying a single constraint:

```xml
<xf:bind ref="." id="input-bind">
    <xf:constraint id="length-constraint"    level="error"   value="string-length() gt 1"/>
    <xf:constraint id="uppercase-constraint" level="warning" value="for $first in substring(., 1, 1) return upper-case($first) = $first"/>
</xf:bind>
```
Each constraint applies to the enclosing `xf:bind`.

- `id` attribute: optional, useful to attach alerts
- `level` attribute: optional, specifies an alert level (defaults to `error`)
- `value`: XPath expression specifying the constraint

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

xxx

### Multiple alerts

[SINCE: Orbeon Forms 4.3]

xxx
