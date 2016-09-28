# JSON functions

<!-- toc -->

## xxf:json-to-xml()

[SINCE Orbeon Forms 2016.3]

```xpath
xxf:json-to-xml() as document-node()?
xxf:json-to-xml($json as xs:string?) as document-node()?
```

The `xxf:json-to-xml()` function takes a JSON string and returns an XML document using the [XForms 2.0 conversion scheme](../submission-json.md).

If the parameter is the empty sequence, the result is the empty sequence.

If the parameter is omitted, the context item converted to a string via the `string()` function is used. 

## xxf:xml-to-json()

[SINCE Orbeon Forms 2016.3]

```xpath
xxf:xml-to-json($xml as document-node()?) as xs:string?
```

The `xxf:json-to-xml()` function takes an XML document following the [XForms 2.0 conversion scheme](../submission-json.md) and converts it to a JSON string.

If the parameter is the empty sequence, the result is the empty sequence.
