# Standard Submissions Support

<!-- toc -->

## Introduction

Orbeon Forms supports most standard XForms features, including:

- the specified processing processing model
- events: `xforms-submit`, `xforms-submit-serialize`, `xforms-submit-done`, and `xforms-submit-error`
- all HTTP and HTTPS methods
- serializations: `application/x-www-form-urlencoded`, `application/xml`, `application/json` (SINCE Orbeon Forms 4.11), `multipart/form-data`, as well as [extensions](submission-extensions.md).
- replacements: `all`, `instance`, and `text`
- SOAP support

One exception is the lack of support for `multipart/related`. 

For more information, please visit the [XForms 1.1 specification][1].

## JSON support

[SINCE Orbeon Forms 4.11]

### Receiving JSON

#### Introduction

When a submission returns an `application/json` mediatype, the submission parses the received JSON and converts it to an XML representation friendly to XPath expressions.
  
*NOTE: This is also supported with `xf:instance` when using the `src` or `resource` attributes, when the content is received with an `application/json` mediatype.*

#### Conversion examples

The following JSON object:

```json
{ "given": "Mark", "family": "Smith" }
```

converts to:

```xml
<json type="object">
    <given>Mark</given>
    <family>Smith</family>
</json>
```

and the values can be accessed in XPath with the paths `instance()/given` and `instance()/family`.

Numbers have a `type="number"` attribute:

```json
{ "name": "Mark", "age": 21 }
```

converts to:

```xml
<json type="object">
    <name>Mark</name>
    <age type="number">21</age>
</json>
```

Booleans similarly have a `type="boolean"` attribute:"

```json
{ "selected": true }
```

converts to:

```xml
<json type="object">
    <selected type="boolean">true</selected>
</json>
```

Arrays use the `_` element name and the `type="array"` attribute:

```json
{ "cities": ["Amsterdam", "Paris", "London"] }
```

converts to:

```xml
<json type="object">
    <cities type="array">
        <_>Amsterdam</_>
        <_>Paris</_>
        <_>London</_>
    </cities>
</json>
```

and the string "Paris" can be accessed with `instance()/cities/_[2]`.


JSON `null` adds a `type="null"` attribute:"

```json
{ "p": null }
```

converts to:

```xml
<json type="object">
    <p type="null"/>
</json>
```

Here is a link to [more examples](https://github.com/orbeon/orbeon-forms/blob/master/src/test/scala/org/orbeon/oxf/json/ConverterTest.scala) as part of the test suite.

### Sending JSON

When setting `serialization="application/json"` on `xf:submission`, the source XML is converted to JSON. The source XML must be compatible with the XForms XML representation of JSON shown above to be meaningful.

## Disabling Validation and relevance checks

Orbeon Forms supports the XForms 1.1 `validate` and `relevant` attributes on `<xf:submission>`. These boolean attributes disable processing of validation and relevance respectively for a given submission:

```xml
<xf:submission id="my-submission"
    method="post"
    validate="false"
    relevant="false"
    resource="http://example.org/rest/draft/"
    replace="none"/>
```

## Controlling serialization

Orbeon Forms supports the XForms 1.1 `serialization` on `<xf:submission>`. This is particularly useful to specify the value `none` with a `get` method:

```xml
<xf:submission id="my-submission"
    method="get"
    serialization="none"
    resource="http://example.org/document.xml" 
    replace="instance"
    instance="my-instance"/>
```

## Asynchronous submissions

See [Asynchronous Submissions](submission-asynchronous.md).

[1]: http://www.w3.org/TR/xforms11/#submit-submission-element
