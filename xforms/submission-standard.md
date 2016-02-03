# Advanced Submissions - Standard Support

<!-- toc -->

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

For more information, please visit the [XForms 1.1 specification][1].

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

[1]: http://www.w3.org/TR/xforms11/#submit-submission-element
