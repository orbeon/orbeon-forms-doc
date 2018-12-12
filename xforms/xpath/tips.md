## XPath tips

## Boolean comparisons

Consider:

```xml
<xf:bind ref="is-soap" type="xs:boolean"/>
...
<xf:action if="is-soap = 'true'">
```

This will cause an XPath dynamic error, because 'true' is a string and XPath will _atomize_ (get an atomic value) the content of the `is-soap` node, which results in a boolean. Because there is no automatic conversion between the two, and error is raised and the action won't run. Instead, write:

```xml
<xf:action if="is-soap = true()">
```

Or:

```xml
<xf:action if="data(is-soap)">
```

Or:


```xml
<xf:action if="string(is-soap) = 'true'">
```

This latter expression works whether type annotations are enabled or not.

## XForms requested type vs. XPath typed value

When type annotations are enabled and an XPath expression requires access to a typed value, if the value is not of the specified type, the expression  stops its evaluation. This behavior is different from 3.9, where an untyped value would be returned instead.
