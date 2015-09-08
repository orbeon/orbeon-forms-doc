> [[Home]] ▸ [[XForms]] ▸ [[XBL|XForms ~ XBL]]

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

### Introduction

The `element` attribute specifies, via one or more CSS selectors, which XML elements, in the source form, it associates with. The following CSS selectors are supported:

- by element name
- by element name and XML datatype (Form Builder only)
- by attribute only [SINCE Orbeon Forms 4.9]
- by element name and attribute [SINCE Orbeon Forms 4.9]

### Binding by element name

The simplest way is to bind by element name:

```xml
element="fr|number"
```

This means that elements called `fr:number` in the form will use this binding.

### Binding by datatype

Form Builder, at design time only (as of Orbeon Forms 4.9), also supports bindings by name and XML datatype:

```css
xf|input:xxf-type('xs:decimal')
```

This must be used on conjunction with a "direct" binding like `fr|number`:

```css
fr|number, xf|input:xxf-type('xs:decimal')
```

In this case an input field (`xf:input`) bound to an `xs:decimal` or `xf:decimal` type will be *substituted*, at design time, with an `fr:number` binding.

*NOTE: In the future, we would like such bindings to work at runtime as well, see [#1248](https://github.com/orbeon/orbeon-forms/issues/1248).*

### Binding by attribute

[SINCE Orbeon Forms 4.9]

It is possible to associate bindings by:

- attribute only
- or by name and attribute

The following matches elements such as `<fr:character-counter>` (by name) but also `<foo:bar appearance="cool character-counter stuff">`,
because of the `~=` operator which means that the attribute is "a list of whitespace-separated values, one of which is exactly equal to"
`character-counter`:

```xml
element="fr|character-counter, [appearance ~= character-counter]"
```

The following matches elements having the `character-counter` appearance and which, at the same time, have the name `foo:bar`:

```xml
element="foo|bar[appearance ~= character-counter]"
```

The following standard CSS selector operations on attributes are supported: `=`, `~=`, `^=`, `$=`, `*=`, `|=` (from [Selectors Level 4 draft](http://dev.w3.org/csswg/selectors-4/)):

|Example        |Meaning|
|---------------|-------|
|`E[foo="bar"]`	|an `E` element whose `foo` attribute value is exactly equal to `bar`|
|`E[foo~="bar"]`|an `E` element whose `foo` attribute value is a list of whitespace-separated values, one of which is exactly equal to `bar`|
|`E[foo^="bar"]`|an `E` element whose `foo` attribute value begins exactly with the string `bar`|
|`E[foo$="bar"]`|an `E` element whose `foo` attribute value ends exactly with the string `bar`|
|`E[foo*="bar"]`|an `E` element whose `foo` attribute value contains the substring `bar`|
|`E[foo|="en"]`	|an `E` element whose `foo` attribute value is a hyphen-separated list of values beginning with `en`|

### Binding to elements in the XForms namespace

[SINCE Orbeon Forms 4.10]

An XBL binding can also be bound to elements in the XForms namespace. For example:
   
```css
fr|character-counter,
xf|input[appearance ~= character-counter],
xf|textarea[appearance ~= character-counter],
xf|secret[appearance ~= character-counter],
fr|tinymce[appearance ~= character-counter]
```

This selector associates, for example the following element to the `fr:character-counter` component:

```xml
<xf:input appearance="character-counter">
```

## See also

- [How the new Form Builder Appearance Selector Works](http://blog.orbeon.com/2015/06/how-new-form-builder-appearance.html)
