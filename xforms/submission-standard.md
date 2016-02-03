# Advanced Submissions - Standard Support

<!-- toc -->

## Introduction

Orbeon Forms supports most standard XForms features, including:

- serializations: `application/x-www-form-urlencoded`, `application/xml`, `application/json` (SINCE Orbeon Forms 4.11), `multipart/form-data`, as well as [extensions](submission-extensions.md).
- replacements: `all`, `instance`, and `text`
- SOAP support

One exception is the lack of support for `multipart/related`. 

For more information, please visit the [XForms 1.1 specification][1].

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
For more information, please visit the [XForms 1.1 specification][1].

## Asynchronous submissions

See [Asynchronous Submissions](submission-asynchronous.md).

[1]: http://www.w3.org/TR/xforms11/#submit-submission-element
