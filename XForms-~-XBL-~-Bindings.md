## Introduction

An XBL file contains one or more *bindings*, expressed with the `xbl:binding` element:

```xml
<xbl:binding
  id="fr-number"
  element="fr|number, xf|input:xxf-type('xs:decimal')"
  xxbl:mode="lhha binding value focus">
```

The binding:

- describes the component's markup and behavior
- determines which XML elements, in the source form, it associates with

## Binding selectors

The `element` attribute specifies, via one or more CSS selectors, which XML elements, in the source form, it associates with. The following CSS selectors are supported:

- by element name
- by element name and XML datatype (Form Builder only)
- by attribute only [SINCE Orbeon Forms 4.9]
- by element name and attribute [SINCE Orbeon Forms 4.9]

The simplest way is to bind by element name:

```xml
element="fr|number"
```

This means that elements called `fr:number` in the form will use this binding.

Form Builder, at design time only (as of Orbeon Forms 4.9), also supports bindings by name and XML datatype:

```xml
xf|input:xxf-type('xs:decimal')
```

This means that an input field bound to an `xs:decimal` or `xf:decimal` type will be bound, at design time, by the `fr:number` binding as well.

*NOTE: In the future, we would like such bindings to work at runtime as well, see [#1248](https://github.com/orbeon/orbeon-forms/issues/1248).*

[SINCE Orbeon Forms 4.9]

It is possible to associate bindings by:

- attribute only
- or by name and attribute

```xml
element="fr|charcounter, [appearance ~= charcounter]"
```

The above matches elements such as `<fr:charcounter>` (by name) but also `<foo:bar appearance="cool charcounter stuff">`.

or:

```xml
element="foo|bar[appearance ~= charcounter]"
```

The above matches elements having the `charcounter` appearance, but which also have the name `foo:bar`.

The following standard CSS operations on attributes are supported: `=`, `~=`, `|=`, `^=`, `$=`, `*=`.