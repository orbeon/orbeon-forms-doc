# XML functions

## xf:attribute() / xxf:attribute()

This function is available in both the `xf` and `xxf` namespaces, and we recommend you use it with the `xf` namespace.

```xpath
xf:attribute(
    $qname as xs:anyAtomicType
) as attribute()

xf:attribute(
    $qname as xs:anyAtomicType,
    $value as xs:anyAtomicType?
) as attribute()
```

The `xf:attribute()` function returns a new XML attribute with the qualified name provided as first argument. If the qualified name is not of type `xs:QName`, its string value is used. If it has a prefix, it is resolved using in-scope namespaces. The second argument is an optional value for the attribute. It defaults to the empty string.

```xml
<!-- Add an attribute called "id" with a value of "first-name" to element "section" -->
<xf:insert
    context="section"
    origin="xf:attribute('id', 'first-name')"/>
```

The first argument can be of type `xs:QName`:

```xml
<!-- Add an attribute called "id" with a value of "foo:bar" to element "section"
     and resolve the namespaces on element $element -->
<xf:insert
    context="section"
    origin="xf:attribute(resolve-QName('foo:bar', $element), 'first-name')"/>
```

## xxf:call-xpl()

```xpath
xxf:call-xpl(
    $xplURL        as xs:string,
    $inputNames    as xs:string*,
    $inputElements as element()*,
    $outputNames   as xs:string+
) as document-node()*
```

This function lets you call an XPL pipeline.

- `$xplURL` is the URL of the pipeline. It must be an absolute URL.
- `$inputNames` is a sequence of strings, each one representing the name of an input of the pipeline that you want to connect.
- `$inputElements` is a sequence of elements to be used as input for the pipeline. The `$inputNames` and `$inputElements` sequences must have the same length. For each element in `$inputElements`, a document is created and connected to an input of the pipeline. Elements are matched to input name by position, for instance the element at position 3 of `$inputElements` is connected to the input with the name specified at position 3 in `$inputNames`.
- `$outputNames` is a sequence of output names to read.

The function returns a sequence of document nodes corresponding the output of the pipeline. The returned sequence will have the same length as `$outputNames` and will correspond to the pipeline output with the name specified on `$outputNames` based on position.

The example below shows a call to the `xxf:call-xpl` function, calling a pipeline with two inputs and one output:

```xpath
xxf:call-xpl(
    'oxf:/examples/sandbox/xpath/run-xpath.xpl',
    (
        'input',
        'xpath'
    ),
    (
        instance('instance')/input,
        instance('instance')/xpath
    ),
    'html'
)
```


## xxf:classes()

```xpath
xxf:classes() as xs:string*
xxf:classes($el as node()) as xs:string*
```

Returns for the context element or the given element if provided, all the classes on the `class` attribute.

## xxf:create-document()

```xpath
xxf:create-document() as document-node()
```

The `xxf:create-document()` creates a new empty XML document. You can then insert new data into that document with the `xf:insert` action.

```xml
<xf:var name="new" value="xxf:create-document()"/>
<xf:insert context="$new" origin="instance('my-data')"/>
```


## xf:element() / xxf:element()

This function is available in both the `xf` and `xxf` namespaces, and we recommend you use it with the `xf` namespace.

```xpath
xf:element(
    $element-name as xs:anyAtomicType
) as element()

xf:element(
    $element-name as xs:anyAtomicType,
    $content      as item()*
) as element()
```

The `xf:element()` function returns a new XML element with the qualified name provided. If the qualified name is not of type `xs:QName`, its string value is used. If it has a prefix, it is resolved using in-scope namespaces.

```xml
<!-- Insert an element called "value" as a child of element "section" -->
<xf:insert
    context="section"
    origin="xf:element('value')">
```

The second, optional argument can take a sequence of items specifying attributes and content for the new element:

```xml
<!-- Insert an element called "value" as a child of element "section",
     with an attribute and text content -->
<xf:insert
    context="section"
    origin="xf:element('value', (xf:attribute('id', 'my-value'), 'John'))"/>
```

The first argument can be of type `xs:QName`:

```xml
<!-- Insert an element called "foo:bar" as a child of element "section"
     and resolve the namespaces on element $element -->
<xf:insert
    context="section"
    origin="xf:element(resolve-QName('foo:bar', $element))"/>
```

## xxf:evaluate()

```xpath
xxf:evaluate(
    $xpath as xs:string
) as item()*
```

The `xxf:evaluate()` function allows you to evaluate XPath expressions dynamically. For example:

```xml
<xf:input ref="xxf:evaluate(concat('instance(''my-instance'')/document', my-xpath))">
    <xf:label>...</xf:label>
</xf:input>
```

## xxf:extract-document()

```xpath
xxf:extract-document(
  $element as element()
) as document-node()

xxf:extract-document(
  $element               as element(),
  $excludeResultPrefixes as xs:boolean
) as document-node()

xxf:extract-document(
  $element               as element(),
  $excludeResultPrefixes as xs:boolean,
  $readonly as xs:boolean
) as document-node()
```

The `xxf:extract-document()` function extracts a new XML document from a document fragment under an enclosing element. For example with the following instance:

```xml
<xf:instance id="my-instance">
    <library>
        <book>
            <title>Jacques le fataliste et son maître</title>
            <author>Denis Diderot</author>
        </book>
    </library>
</xf:instance>
```

The expression:

```xpath
xxf:extract-document(instance('my-instance')/book, '', false())
```

returns a new XML document rooted at the `<book>` element:

```xml
<book>
    <title>Jacques le fataliste et son maître</title>
    <author>Denis Diderot</author>
</book>
```

* `$excludeResultPrefixes`: optional parameter; contains a list of space-separated namespace prefixes to exclude. Defaults to the empty string.
* `$readonly`: optional parameter; when set to `true()`, return a readonly instance. Defaults to `false()`.

## xxf:form-urlencode()

```xpath
xxf:form-urlencode($document as node()) as xs:string
```

Performs `application/x-www-form-urlencoded` encoding on an XML document as per the [XForms specification](https://www.w3.org/community/xformsusers/wiki/XForms_2.0#Serialization_as_application.2Fx-www-form-urlencoded).

## xxf:has-class()

```xpath
xxf:has-class($class-name as xs:string) as xs:boolean
xxf:has-class($class-name as xs:string, $el as node()) as xs:boolean
```

Returns whether the context element, or the given element, has a `class` attribute containing the specified class name.

## xxf:mutable-document()

```xpath
xxf:mutable-document(
    $node as node()
) as document-node()
```

The `xxf:mutable-document()` function takes a document as input and returns a mutable document, i.e. a document on which you can for example use `xf:setvalue`.

```xml
<xf:action ev:event="xforms-submit-serialize">
  <!-- Get initial document to submit -->
  <xf:var
    name="request-document"
    value="xxf:mutable-document(saxon:parse(/my/request))"/>
  <!-- Set value -->
  <xf:setvalue
    ref="$request-document/my/first-name">Joe</xf:setvalue>
  <!-- Serialize request document -->
  <xf:setvalue
    ref="event('submission-body')"
    value="saxon:serialize($request-document, instance('my-output-instance'))"/>
</xf:action>
```

Note that by compatibility with the XSLT `document()` and XPath 2.0 `doc()` functions, and unlike the `instance()` function, `xxf:mutable-document()` returns a document node, not a document element.

## xxf:serialize()

```xpath
xxf:serialize(
    $item as node(),
    $format as xs:string?
) as xs:string
```

The `xxf:serialize()` function allows you to serialize an XML node to XML, HTML, XHTML or text. For example:

```xml
<xf:bind ref="my-html" calculate="xxf:serialize(instance('my-instance'), 'html')"="">
```

## xxf:sort()

```xpath
xxf:sort(
    $sequence   as item()*,
    $sort-key   as item(),
    $datatype   as xs:string?,
    $order      as xs:string?,
    $case-order as xs:string?,
    $lang       as xs:string?,
    $collation  as xs:string?,
    $stable     as xs:string?
) as item()*
```

The second argument differs from the `exf:sort()` function in that it doesn't take a plain string but a literal expression, for example:

```xml
<xf:itemset
  ref="
    xxf:sort(
        instance('samples-instance')/file,
        @name,
        'text',
        'ascending'
    )">
  ...
</xf:itemset>
```

The last 3 arguments are available since Orbeon Forms 2021.1.9, 2022.1.4, and 2023.1. They follow the XSLT 2.0 semantics for the corresponding attributes of `<xsl:sort>` (see [recommendation](https://www.w3.org/TR/xslt20/#xsl-sort)). Those additional arguments are of particular significance when you need the sorting to follow particular, often language-dependent, rules. For instance, `xxf:sort(//first-name, ., 'text', 'ascending', 'upper-first', 'pl')` sorts the `<first-name>` in the current document according to the rules of the Polish language.
