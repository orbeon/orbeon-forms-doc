# XBL Bindings



## Introduction

An XBL file contains one or more *bindings*, expressed with the `xbl:binding` element:

```xml

<xbl:xbl
    xmlns:xf="http://www.w3.org/2002/xforms"
    xmlns:acme="http://www.acme.com/xbl"
    xmlns:xbl="http://www.w3.org/ns/xbl">

    <xbl:binding
        id="acme-multi-tool"
        element="acme|multi-tool">

        ...binding definition here...

    </xbl:binding>
</xbl>
```

The binding:

- describes the component's markup and behavior
- determines which XML elements, in the source form, it associates with, in this case elements with qualified name `acme:multi-tool`.

## Directory layout

### Your own components

Each binding must have has a *direct* binding by element name (see [below](#binding-by-element-name)). In the example above, it is `acme|multi-tool`.

*NOTE: This is CSS syntax to express what in XML you would usually refer to as `acme:multi-tool`. It's just that CSS chose as namespace separator `|` instead of XML's standard `:`.*

The binding by element name can be split into two parts:

- `acme`
    - This is the namespace *prefix*.
    - The `acme` namespace prefix maps to the namespace URI `http://www.acme.com/xbl` via the XML namespace mapping `xmlns:acme="http://www.acme.com/xbl"`.
- `multi-tool`: this is the XML element's local name

*NOTE: It is up to you to choose a prefix and namespace URI for your project or company. You can then reuse those for a number of component bindings.*

So this binding gives us the pair:

- namespace URI: `http://www.acme.com/xbl`
- local name: `multi-tool`

The `http://www.acme.com/xbl` namespace URI in turn is the key to a *standardized* namespace prefix defined in properties:

```xml
<property as="xs:string" name="oxf.xforms.xbl.mapping.acme">
    http://www.acme.com/xbl
</property>
```

This is just another way of defining a mapping between a namespace prefix and a namespace URI, except here we do it in a centralized place so that all components using namespace `http://www.acme.com/xbl` yield the same prefix, `acme`.

Now let's see where files are located when an element `<acme:multi-tool>` is encountered:

1. Orbeon Forms looks for a property with a name that starts with `oxf.xforms.xbl.mapping` and with a value equal to the namespace `http://www.acme.com/xbl`. In this case it finds the property `oxf.xforms.xbl.mapping.acme`.
2. The XForms engine extracts the part of the property name after `oxf.xforms.xbl.mapping`, in this case `acme`.
3. This is used to resolve the resource `oxf:/xbl/acme/multi-tool/multi-tool.xbl`.
    - The first part of the path is always `xbl`.
    - This is followed by the prefix name, here `acme`.
    - This is followed by a directory with the same name as the local name of your component, containing an XBL file also with the same name, here `multi-tool/multi-tool.xbl`.


You place other files, such as CSS and JavaScript files related to the `acme|multi-tool` component, in the same directory. For example:

- `oxf:/xbl/acme/multi-tool/multi-tool.css`
- `oxf:/xbl/acme/multi-tool/multi-tool.js`

and so on.

### Built-in Form Runner components

All elements in the `http://orbeon.org/oxf/xml/form-runner` namespace (typically using the prefix `fr`, short for "Form Runner") are also handled this way, thanks to a mapping defined as follows:

```xml
<property as="xs:string" name="oxf.xforms.xbl.mapping.orbeon">
    http://orbeon.org/oxf/xml/form-runner
</property>
```

For example:

- `<fr:number>` is loaded from `oxf:/xbl/orbeon/number/number.xbl`
- `<fr:section>` is loaded from `oxf:/xbl/orbeon/section/section.xbl`

*NOTE: You shouldn't use the `fr` prefix or the `http://orbeon.org/oxf/xml/form-runner` namespace URI for your own components.*

See also: [By name bindings](library.md#by-name-bindings).

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
|<code>E[foo&#124;="en"]</code>	|an `E` element whose `foo` attribute value is a hyphen-separated list of values beginning with `en`|

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

- [How the new Form Builder Appearance Selector Works](https://blog.orbeon.com/2015/06/how-new-form-builder-appearance.html)
