# Standard functions

<!-- toc -->

## XPath 2.0 functions and constructors

### Standard documentation

These functions are documented in [XQuery 1.0 and XPath 2.0 Functions and Operators (Second Edition)](https://www.w3.org/TR/xpath-functions/).

### Functions

Orbeon Forms supports the XPath functions from [XQuery 1.0 and XPath 2.0 Functions and Operators (Second Edition)](https://www.w3.org/TR/xpath-functions/).

Example of use:

```xpath
string-join(('cat', 'dog', 'alligator'), ', ')
```

Namespaces:

- These functions are available in the default function namespace.
- These function are also available in the XPath functions namespace (`http://www.w3.org/2005/xpath-functions`), usually associated with the `fn` prefix.

This means that it is usually not necessary to declare a namespace for these functions, nor is it necessary to use a prefix in calls to these functions.

We recommend *not prefixing* calls to the standard XPath functions.

### Constructors

Orbeon Forms supports the XPath constructors from [XQuery 1.0 and XPath 2.0 Functions and Operators (Second Edition)](https://www.w3.org/TR/xpath-functions/).

Example of use:

```xpath
xs:dateTime('2016-01-11T12:00:00Z')
```

These constructors are in the `http://www.w3.org/2001/XMLSchema` namespace, usually associated with the `xs` prefix. This means that using a prefix is required.

## XForms functions

### Standard documentation
These functions are documented in XForms 2.0's [XPath Expressions Module](https://www.w3.org/community/xformsusers/wiki/XPath_Expressions_Module).

### Namespaces

- These functions are available in the default function namespace. 
- These function are also available in the XForms namespace (`http://www.w3.org/2002/xforms`), usually associated with the `xf` prefix.

<!--
- These function are also available in the XForms functions namespace (`http://www.w3.org/2002/xforms-functions`), which doesn't have a standard prefix. [SINCE Orbeon Forms 2017.1]
-->
 
This means that it is usually not necessary to declare a namespace for these functions in the form, nor is it necessary to use a prefix in calls to the core XPath functions:
 
```xpath
valid(element)
```

For extra clarity, you can prefix calls to XForms functions. For example:

```xpath
xf:valid(element)
```

### XForms 2.0 functions

#### xf:bind()

[SINCE Orbeon Forms 4.5]

```xpath
xf:bind($id as xs:string) as node()*
```

This function returns the sequence of nodes associated with the bind specified by the `id` parameter.

#### xf:valid()

[SINCE Orbeon Forms 4.3]

```xpath
xf:valid() as xs:boolean
xf:valid($items as item()*) as xs:boolean
xf:valid($items as item()*, $relevant as xs:boolean) as xs:boolean
xf:valid($items as item()*, $relevant as xs:boolean, $recurse as xs:boolean) as xs:boolean
```

The `valid()` function returns the validity of XPath items, including instance data nodes.

### Boolean functions

- `xf:boolean-from-string()`
- `xf:is-card-number()`

### Number functions

- `xf:count-non-empty()`
- `xf:index()`
- `xf:power()`
- `xf:random()`

### String functions

- `xf:property()`
    * This function supports extension property names in the `http://orbeon.org/oxf/xml/xforms` namespace (usually mapped to the `xxf` prefix). Any such property name will return the value of an XForms engine property. Example:

        ```xml
        <xf:output value="property('xxf:noscript')"/>
        ```

    * NOTE: The standard XForms function returns an XPath 1.0 `string`. The Orbeon Forms implementation returns the following:
        * empty sequence (if the property is not found)
        * `xs:string`, `xs:integer`, `xs:boolean` or `xs:anyURI` depending on the type of the property
- `xf:digest()`
- `xf:hmac()`

### Date and time functions

_NOTE: Prefer the XPath 2.0 date and time functions when possible._

- `xf:local-date()`
- `xf:local-dateTime()`
- `xf:now()`
- `xf:days-from-date()`
- `xf:days-to-date()`
- `xf:seconds-from-dateTime()`
    - available as `xf:seconds-from-dateTime()` only
- `xf:seconds-to-dateTime()`
- `xf:seconds()`
- `xf:months()`

### Node-set functions

- `xf:instance()`
- `xf:current()`
- `xf:context()`

### Object functions

- `xf:event()`

### Unsupported and obsolete XForms 1.1 functions

The following functions from XForms 1.1 are obsolete:

- `xf:adjust-dateTime-to-timezone()`
    - use the [XPath 2.0 Timezone Adjustment Functions on Dates and Time Values](https://www.w3.org/TR/xpath-functions/#timezone.functions), which offer similar functionality
- `xf:avg()`
    - use the standard XPath 2.0 function instead (`avg()`)
    - the XForms 1.1 version of this function is not implemented in Orbeon Forms
- `xf:choose()`
    - use the native XPath 2.0 `if (...) then ... else ...` construct instead
- `xf:id()`
    - use the native [XPath 2.0 `id()` function](https://www.w3.org/TR/xpath-functions/#func-id) instead
- `xf:if()`
    - available as `xf:if()` only
    - use the native XPath 2.0 `if (...) then ... else ...` construct instead
- `xf:min()`, `xf:max()`
    - use the standard XPath 2.0 functions instead (`min()` and `max()`)
    - the XForms 1.1 versions of these functions are not implemented in Orbeon Forms

## XSLT 2.0 functions

### Namespaces

These functions are available in the default function namespace.
 
Example:
 
```xpath
format-number(xs:integer(year), '0000')
```

### Functions

The following functions from XSLT 2.0 are  available:

- `format-date()` ([external documentation](http://www.w3.org/TR/2005/WD-xslt20-20050915/#function-format-date))
- `format-dateTime()` ([external documentation](http://www.w3.org/TR/2005/WD-xslt20-20050915/#function-format-dateTime))
- `format-time()` ([external documentation](http://www.w3.org/TR/2005/WD-xslt20-20050915/#function-format-time))
- `format-number()` ([external documentation](http://www.w3.org/TR/2005/WD-xslt20-20050915/#function-format-number))

## eXforms functions

### Namespaces

These functions are available in the `http://www.exforms.org/exf/1-0` namespace, usually associated with the `exf` prefix.

### Functions

eXForms was a suggested set of extensions to XForms 1.0, grouped into different modules.

Orbeon Forms supports the `exf:mip` module, which includes the following functions:

- `exf:relevant()`
- `exf:readonly()`
- `exf:required()`

_NOTE: XPath 2.0-compatible versions of these functions will be available as part of XForms 2.0._

Orbeon Forms also supports the following from the *sorting module*:

```xpath
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
