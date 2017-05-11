# Standard Submissions Support

<!-- toc -->

## Introduction

Orbeon Forms supports most standard XForms features, including:

- the specified processing processing model
- events: `xforms-submit`, `xforms-submit-serialize`, `xforms-submit-done`, and `xforms-submit-error`
- all HTTP and HTTPS methods
- serializations: `application/x-www-form-urlencoded`, `application/xml`, `application/json` (SINCE Orbeon Forms 2016.1), `multipart/form-data`, as well as [extensions](submission-extensions.md).
- replacements: `all`, `instance`, and `text`
- SOAP support

One exception is the lack of support for `multipart/related`.

For more information, please visit the [XForms 1.1 specification][1].

## JSON support

See [JSON support](submission-json.md).

## Disabling Validation and relevance handling

Orbeon Forms supports the XForms 1.1 `validate` and `relevant` attributes on `<xf:submission>`. These boolean attributes disable processing of validation and relevance pruning respectively for a given submission:

```xml
<xf:submission id="my-submission"
    method="post"
    validate="false"
    relevant="false"
    resource="http://example.org/rest/draft/"
    replace="none"/>
```

XForms 2.0 introduces `nonrelevant` (while keeping `relevant` for backward compatibility) for clarity:

```xml
<xf:submission id="my-submission"
    method="post"
    validate="false"
    nonrelevant="keep"
    resource="http://example.org/rest/draft/"
    replace="none"/>
```

`nonrelevant` takes values `keep`, `remove` and `empty`, which empties non-relevant elements and attributes:

```xml
<xf:submission id="my-submission"
    method="post"
    nonrelevant="empty"
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
