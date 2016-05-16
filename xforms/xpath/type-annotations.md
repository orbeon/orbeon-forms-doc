# Type annotations

<!-- toc -->

## Basics

Orbeon Forms supports exposing MIP type annotations to XPath 2.0.

This means that if you have a type associated with a node, e.g.:

```xml
<xf:bind ref="age" type="xs:integer">
```

then the type is not only used for validation, but also determines the typed value of the element node age.

This means that earlier, you had to write the following:

```xml
<xf:bind
  ref="age"
  type="xs:integer"
  constraint="xs:integer(.) le 150"/>
```

With exposing type annotations, you can simply write:

```xml
<xf:bind
  ref="age"
  type="xs:integer"
  constraint=". le 150"/>
```

or:

```xml
<xf:bind
  ref="date"
  type="xs:date"
  constraint=". le (current-date() + xdt:dayTimeDuration('P2D'))"/>
```

## Enabling type annotations

_NOTE: Type annotations are not automatically enabled for backward compatibility reasons. However, they are enabled by default for new forms created with Form Builder._

The following property controls whether instance types annotations are exposed to XPath 2.0 expressions:

```xml
<property
    as="xs:boolean"
    name="oxf.xforms.expose-xpath-types"
    value="true"/>
```

* If set to `false` (the default), instance types are not made available to XPath expressions.
* If set to `true`, they are made available.

You can as usual enable this on a per-page basis:

```xml
<xf:model xxf:expose-xpath-types="true">
```

[SINCE Orbeon Forms 4.2]

You can also enable this on a per-instance basis:

```xml
<xf:instance xxf:expose-xpath-types="true">
```

## Where does this work?

Static type annotations (with `xf:bind/@type` and `xsi:type`) can be used by all XPath expressions, including  `xf:bind/@calculate`.

NOTE: Here is the order in which the XForms engine processes type annotations  in the model:

- during a model _rebuild_, all `xf:bind` point to their associated instance nodes if any
- static type annotations with `xf:bind/@type` and `xsi:type` are available just after a rebuild
- this means that type annotations can be used during subsequent model _recalculate _and _revalidate_
- however, annotations done via a schema are _not_ reliably available during _recalculate_
