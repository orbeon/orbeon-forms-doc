> [[Home]] ▸ [[XForms]]

See also [[Form Builder Validation|Form Builder ~ Validation]].

## Introduction

Orbeon Forms validates XForms instances following XForms 1.1 and adds some extensions to facilitate validation.

## Validation constructs

There are two main methods for validating data in XForms:

- with an imported XML Schema
- with `xf:bind`

Using a bind allows you to create precise error messages to the user based on the type of data entry error.

## Validation order

Orbeon Forms performs validation of a node in the following order:

- data type validation
    - XML Schema validation (lax/strict/none on model instances)
    - `xf:bind/@type`
    - `xf:type`
- required validation
    - required-but-empty
    - `xf:bind/@required`
    - `xf:required/@value`
- constraints
    - `xf:bind/@constraint`
    - `xf:constraint/@value`
    - are checked *only* if the control's data type is valid

## Extensions

### Multiple constraints and alerts

[SINCE: Orbeon Forms 4.3]

XForms allows a single `constraint` attribute on the `xf:bind` element. Orbeon Forms extends this to support any number of nested `xf:constraint` elements, each specifying a single validation:

```xml
<xf:bind ref="." id="input-bind">
    <xf:constraint 
        id="length-constraint"    
        level="error"   
        value="string-length() gt 1"/>
    <xf:constraint 
        id="uppercase-constraint" 
        level="warning" 
        value="for $first in substring(., 1, 1) return upper-case($first) = $first"/>
</xf:bind>
```

Deprecated example:

```xml
<xf:bind ref="." id="input-bind">
    <xf:constraint 
        id="length-constraint"    
        level="error"   
        value="string-length() gt 1"/>
    <xf:constraint 
        id="uppercase-constraint" 
        level="warning" 
        value="for $first in substring(., 1, 1) return upper-case($first) = $first"/>
</xf:bind>
```

Each constraint applies to the enclosing `xf:bind`.

Attributes:

- `level` attribute: optional, specifies an alert level (defaults to `error`)
- `constraint`: XPath expression specifying the constraint

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

*NOTE: As of Orbeon Forms 4.3, it is only possible to associate a warning or info validation level to a constraint specified with `xf:constraint/@value`. It is not possible to associate these levels to the required or data type validations: these always use the error level.*

### Nested validation elements

[SINCE: Orbeon Forms 4.9]

Instead of `type` and `required` attributes, you can use `xf:type` and `xf:required` elements nested within `xf:bind`.

This allows assigning a specific identifier to a validation with the `id` attribute, so that `xf:alert` can refer to those with the `validation` attribute:

```xml
<xf:bind id="control-1-bind" name="control-1" ref="control-1">
    <xf:required value="true()" id="validation-3-validation"/>
    <xf:type id="validation-4-validation">xs:decimal</xf:type>
    <xf:constraint value=". ge 10" id="validation-5-validation"/>
    <xf:constraint value=". ge 20" id="validation-6-validation" level="warning"/>
</xf:bind>
<fr:number bind="control-1-bind">
    <xf:alert ref="$form-resources/control-1/alert[1]" validation="validation-3-validation"/>
    <xf:alert ref="$form-resources/control-1/alert[2]" validation="validation-4-validation"/>
    <xf:alert ref="$form-resources/control-1/alert[3]" validation="validation-5-validation"/>
    <xf:alert ref="$form-resources/control-1/alert[4]" validation="validation-6-validation"/>
    <xf:alert ref="$fr-resources/detail/labels/alert"/>
</fr:number>
```

This allows having distinct alerts for indicating:

- that the value is required
- that the value must be of the given datatype
- that the value must satisfy constraints expressed in XPath

*LIMITATION: As of Orbeon Forms 4.9, only the first `type` and the first `required` attribute or element associated with an `xf:bind` element are taken into account. On the other hand, all `constraint` elements are handled.*

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
    - If there is a failed required validation, the default alert is active.
    - Otherwise, if there is a failed data type validation, the default alert is active.
    - Otherwise, if there are alerts that match specific failed constraints, those alerts and no others are active.
    - Otherwise, if there are alerts that match the specific level, those alerts and no others are active.
    - Otherwise, if present, the default alert is active.
    - Otherwise, no alert is active.

*NOTE: This is the behavior as of Orbeon Forms 4.6.2 and newer, see [#1830](https://github.com/orbeon/orbeon-forms/issues/1830). Prior to that, a failed required validation was at the same level as other failed validations. The 4.6.2 behavior makes it so that the required validation has priority over other error validations.*

Example:

```xml
<xf:bind ref="." id="input-bind">
    <xf:constraint 
        id="length-constraint"    
        level="error"   
        value="string-length() gt 1"/>
    <xf:constraint 
        id="uppercase-constraint" 
        level="warning" 
        value="for $first in substring(., 1, 1) return upper-case($first) = $first"/>
</xf:bind>

<xf:input id="my-input" ref=".">
    <xf:alert id="alert-1">All levels</xf:alert>
    <xf:alert id="alert-2" level="error">Error level</xf:alert>
    <xf:alert id="alert-3" level="warning">Warning level</xf:alert>
    <xf:alert id="alert-4" level="error warning">Error and warning level</xf:alert>
    <xf:alert id="alert-5" validation="length-constraint">Length constraint</xf:alert>
    <xf:alert id="alert-6" validation="uppercase-constraint">Uppercase constraint</xf:alert>
    <xf:alert id="alert-7" validation="length-constraint uppercase-constraint">Both constraints</xf:alert>
</xf:input>
```

### xxforms-valid and xxforms-invalid events

Orbeon Forms supports extensions events dispatched to an instance when it becomes valid or invalid:

- `xxforms-valid`
- `xxforms-invalid`

#### Orbeon Forms 4 behavior

[SINCE: 2012-10-25 / ORBEON FORMS 4.0]

These events are dispatched just after `xforms-revalidate` completes on a given model to all instances that change their validation state (from valid to invalid or from invalid to valid):

- If the instance is newly valid, `xxforms-valid` is dispatched
- If the instance is newly invalid, `xxforms-invalid` is dispatched

Before the initial validation of a model, instances are assumed to be in the valid state.

These events can be used, for example, to toggle the appearance of icons indicating that a form is valid or invalid:

```xml
<xf:instance id="my-instance">
    ...
</xf:instance>
<xf:action ev:event="xxforms-invalid" ev:observer="my-instance">
    <xf:toggle case="invalid-form-case"/>
</xf:action>
<xf:action ev:event="xxforms-valid" ev:observer="my-instance">
    <xf:toggle case="valid-form-case"/>
</xf:action>
```

#### Orbeon Forms 3.9 behavior

These events are dispatched just before `xforms-revalidate` completes, to all instances of the model being revalidated. For a given instance, either `xxforms-valid` or `xxforms-invalid` is dispatched for a given revalidation.

### xxforms-constraints-changed event

[SINCE: Orbeon Forms 4.3]

This event is dispatched to a control whenever the list of failed constraints associated with the control's bound node change.

Context information:

- `level`: the control's current validation level ("error", "warning", "info", or the empty sequence)
- `constraints`: ids of all current failed constraints
- `errors`: ids of current failed error constraints
- `warnings`: ids of current failed warning constraints (empty if current level is "error")
- `infos`: ids of current failed info constraints (empty if current level is "warning" or "error")
- `added-errors`: ids of current errors added since the last refresh
- `removed-errors`: ids of current errors removed since the last refresh
- `added-warnings`: ids of current warnings added since the last refresh
- `removed-warnings`: ids of current warnings removed since the last refresh
- `added-infos`: ids of current infos added since the last refresh
- `removed-infos`: ids of current infos removed since the last refresh

### xxf:valid() and xxf:invalid() XPath functions

`xxf:valid()` returns the validity of a instance data node or of a subtree of instance data.

`xxf:invalid-binds()` allows you to determine which bind caused node invalidity.

### xxf:xml type

This types checks that the value is well-formed XML:

```xml
<xf:bind ref="my-xml" type="xxf:xml"/>
```

Note that this checks the string value of the node, which means that the node must contain *escaped* XML.

### xxf:xpath2 type

This types checks that the value is well-formed XPath 2.0. Any variable used by the expression is assumed to be in scope:

```xml
<xf:bind ref="my-xpath" type="xxf:xpath2"/>
```

*NOTE: In both these cases, Orbeon Forms checks for the required MIP: if it evaluates to `false()` and the value is the empty string, then the instance data node is considered valid. This is contrary to XForms 1.1.*

### Controlling the XML Schema validation mode

When an XML Schema is provided, Orbeon Forms supports controlling whether a particular instance is validated in the following modes:

- "lax" mode
- "strict" mode
- not validated at all ("skip" mode)

Orbeon Forms implements a "lax" validation mode by default, where only elements that have definitions in the imported schemas are validated. Other elements are not considered for validation. This is in line with XML Schema and XSLT 2.0 lax validation modes, and with the default validation mode as specified in XForms 1.1

In addition, the author can specify the validation mode directly on each instance with the extension `xxf:validation` attribute, which takes values:

- `lax` (the default)
- `strict` (the root element has to have a definition in the schema and must be valid)
- `skip` (no validation at all for that instance)

```xml
<xf:model schema="my-schema.xsd">
    <xf:instance id="my-form" xxf:validation="strict">
        <my-form> ... </my-form>
    </xf:instance>
    <xf:instance id="items" xxf:validation="skip">
        <items> ... </items>
    </xf:instance>
</xf:model>
```

Nodes validated through an XML Schema receive data type annotations, which means that if an element or attribute is validated against `xs:date` in a schema, an XForms control bound to that node will display a date picker.

## XML Schema validation

[IN PROGRESS]

## Validation and submission

[IN PROGRESS]

## Validation and controls

[IN PROGRESS]