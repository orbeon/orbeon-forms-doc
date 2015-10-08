> [[Home]] ▸ [[XForms]] ▸ [[XForms ~ XPath Function Library]]

<!-- toc -->

## Introduction

The following functions are documented on this page:

- XForms 2.0 functions
    - `xf:valid()`
    - `xf:bind()`
- XSLT 2.0 functions
    - `format-date()`
    - `format-dateTime()`
    - `format-time()`
    - `format-number()`
- eXforms functions
    - `exf:relevant()`
    - `exf:readonly()`
    - `exf:required()`
    - `exf:sort()`

## XForms 2.0 functions

### xf:valid()

[SINCE Orbeon Forms 4.3]

```ruby
xf:valid() as xs:boolean
xf:valid($items as item()*) as xs:boolean
xf:valid($items as item()*, $relevant as xs:boolean) as xs:boolean
xf:valid($items as item()*, $relevant as xs:boolean, $recurse as xs:boolean) as xs:boolean
```

The `valid()` function returns the validity of XPath items, including instance data nodes.

### xf:bind()

[SINCE Orbeon Forms 4.5]

```ruby
xf:bind($id as xs:string) as node()*
```

This function returns the sequence of nodes associated with the bind specified by the `id` parameter.


## XSLT 2.0 functions

The following functions from XSLT 2.0 are  available:

The following functions from XSLT 2.0 are  available:

- `format-date()` ([external documentation](http://www.w3.org/TR/2005/WD-xslt20-20050915/#function-format-date))
- `format-dateTime()` ([external documentation](http://www.w3.org/TR/2005/WD-xslt20-20050915/#function-format-dateTime))
- `format-time()` ([external documentation](http://www.w3.org/TR/2005/WD-xslt20-20050915/#function-format-time))
- `format-number()` ([external documentation](http://www.w3.org/TR/2005/WD-xslt20-20050915/#function-format-number))

## eXforms functions

eXForms was a suggested set of extensions to XForms 1.0, grouped into different modules. Orbeon Forms supports the `exf:mip` module, which includes the following functions:

- `exf:relevant()`
- `exf:readonly()`
- `exf:required()`

_NOTE: These functions will be available as part of XForms 2.0 support._

Orbeon Forms also supports the following from the *sorting module*:

```ruby
exf:sort(
    $sequence   as item()*,
    $sort-key   as xs:string,
    $datatype   as xs:string?,
    $order      as xs:string?,
    $case-order as xs:string?
) as item()*
```

Note that the second argument is interpreted as a string, unlike with `xxf:sort()`:

```xml
<xf:itemset ref="exf:sort(instance('samples-instance')/file, '@name', 'text', 'ascending')">
    ...
</xf:itemset>
```

eXForms functions live in the `http://www.exforms.org/exf/1-0` namespace, usually bound to the prefix `exf` or `exforms`.
