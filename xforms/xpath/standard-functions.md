# Standard functions

<!-- toc -->

## XForms 1.1 functions

### Boolean functions

* `boolean-from-string()`
* `is-card-number()`

### Number functions

* `avg()`, `min()`, `max()`
    * availble from XPath 2.0
* `count-non-empty()`
* `index()`
* `power()`
* `random()`

### String functions

* `if()`
    * available as `xf:if()`
    * _NOTE: Prefer the native XPath 2.0 `if (...) then ... else ...` construct._
* `property()`
    * This function supports extension property names in the `http://orbeon.org/oxf/xml/xforms` namespace (usually mapped to the `xxf` prefix). Any such property name will return the value of an XForms engine property. Example:

        ```xml
        <xf:output value="property('xxf:noscript')"/>
        ```

    * NOTE: The standard XForms function returns an XPath 1.0 `string`. The Orbeon Forms implementation returns the following:
        * empty sequence (if the property is not found)
        * `xs:string`, `xs:integer`, `xs:boolean` or `xs:anyURI` depending on the type of the property
* `digest()`
* `hmac()`

### Date and time functions

_NOTE: Prefer the XPath 2.0 date and time functions when possible._

* `local-date()`
* `local-dateTime()`
* `now()`
* `days-from-date()`
* `days-to-date()`
* `seconds-from-dateTime()`
    * available as `xf:seconds-from-dateTime()`
* `seconds-to-dateTime()`
* `seconds()`
* `months()`

### Node-set functions

* `instance()`
* `current()`
* `context()`

### Object functions

* `choose()`
    * _NOTE: Prefer the native XPath 2.0 `if (...) then ... else ...` construct._
* `event()`

### Functions not yet implemented

The following XForms 1.1 functions are NOT supported as of February 2010:

* `id()`
* `adjust-dateTime-to-timezone()` (prefer the [XPath 2.0 Timezone Adjustment Functions on Dates and Time Values][2] functions)


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
