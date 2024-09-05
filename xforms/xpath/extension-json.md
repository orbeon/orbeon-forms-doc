# JSON functions

## xxf:json-to-xml()

[SINCE Orbeon Forms 2016.3]

The `xxf:json-to-xml()` function takes a JSON string and returns an XML document using the [XForms 2.0 conversion scheme](../submission-json.md).

```xpath
xxf:json-to-xml() as document-node()?
xxf:json-to-xml($json as xs:string?) as document-node()?
```

- If the parameter is omitted, the context item is converted to a string using the `string()` function.
- The function returns an empty sequence in the following cases:
    - The parameter is an empty sequence.
    - [SINCE Orbeon Forms 2024.1, 2023.1.5, 2022.1.8, 2021.1.11] The parameter, or the context item if the parameter is omitted, cannot be parsed as JSON. In earlier versions, this case would raise an error instead of returning an empty sequence.

## xxf:xml-to-json()

[SINCE Orbeon Forms 2016.3]

```xpath
xxf:xml-to-json($xml as document-node()?) as xs:string?
```

The `xxf:json-to-xml()` function takes an XML document following the [XForms 2.0 conversion scheme](../submission-json.md) and converts it to a JSON string.

If the parameter is the empty sequence, the result is the empty sequence.
