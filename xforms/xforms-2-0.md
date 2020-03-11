# XForms 2.0 Support

## Orbeon Forms 4.3

Here at the feature from [XForms 2.0](https://www.w3.org/community/xformsusers/wiki/XForms_2.0) and its [XPath expression module](https://www.w3.org/community/xformsusers/wiki/XPath_Expressions_Module) that are available as of Orbeon Forms 4.3:

- `xf:var`
- `xf:repeat` over sequences of atomic values and nodes
- deprecation of `nodeset` in favor of `ref`
- multiple MIPs of the same property affecting the same node
- AVTs (Attribute Value Templates)
- `accept` attribute on `xf:upload`
- `xf:property` child of `xf:dispatch` element
- `iterate` attribute on actions
- `xf:valid()` function

## Orbeon Forms 4.5

XForms 2.0 features added with Orbeon Forms 4.5:

- `xf:bind()` function

## Orbeon Forms 4.8

XForms 2.0 features added with Orbeon Forms 4.8:

- `caseref` attribute on `xf:switch`
    - This allows storing the value of the currently-selected case to instance data.
- `case()` function
    - This function was already available as `xxf:case()` in previous versions.

## Orbeon Forms 2016.1

XForms 2.0 features added with Orbeon Forms 2016.1:

- `xf:submission` and `xf:instance` [JSON support](submission-standard.md#json-support).
    - This allows receiving `application/json` content. The JSON received is converted to an XML representation friendly to XPath expressions. This allows receiving data from JSON services and using it in your forms, including via Form Builder services.
    - This also allows sending `application/json` content, based on an XML representation.
- `xf:param` and `xf:body` on `xf:action`
- `type attribute on `xf:action` for types:
    - `text/javascript` / `application/javascript` / `javascript`
    - `text/xpath` / `application/xpath` / `xpath`

## Orbeon Forms 2017.1

XForms 2.0 features added with Orbeon Forms 2017.1:

- `nonrelevant` attribute on `<xf:submission>`
    - This deprecates the `relevant` attribute.
    - The values are `keep`, `remove`, and `empty`.

## Orbeon Forms 2019.1

XForms 2.0 features added with Orbeon Forms 2019.1:

- `target` on `<xf:load>`
    - This deprecates the `xxf:target` attribute.
- URI functions:
    - `xf:uri-scheme($uri as xs:string) as xs:string?`
    - `xf:uri-scheme-specific-part($uri as xs:string, $raw as xs:boolean) as xs:string?`
    - `xf:uri-authority($uri as xs:string, $raw as xs:boolean) as xs:string?`
    - `xf:uri-user-info($uri as xs:string, $raw as xs:boolean) as xs:string?`
    - `xf:uri-host($uri as xs:string) as xs:string?`
    - `xf:uri-port($uri as xs:string) as xs:integer?`
    - `xf:uri-path($uri as xs:string, $raw as xs:boolean) as xs:string?`
    - `xf:uri-query($uri as xs:string, $raw as xs:boolean) as xs:string?`
    - `xf:uri-fragment($uri as xs:string, $raw as xs:boolean) as xs:string?`
    - `xf:uri-param-names($uri as xs:string) as xs:string*`
    - `xf:uri-param-values($uri as xs:string, $name as xs:string) as xs:string*`

## Orbeon Forms 2020.1

### Features

XForms 2.0 features added with Orbeon Forms 2020.1:

- `xf:copy`
    - Orbeon Forms implements XForms 2.0 enhancements to `xf:copy` including support for `xf:item`, attributes, XPath 2 support, and clarifications.
    - *NOTE: While `xf:copy` is an XForms 1.0 feature, Orbeon Forms didn't support it until version 2020.1.*
    
### Example of `xf:copy` attributes support 

Let's say we an XML representation which proposes a discriminated union based on a `type` attribute. We have, in the data, either:

```xml
<label type="PredefinedButtonLabel"/>
```

or:

```xml
<label type="CustomButtonLabel">
    <name type="object">
        <values type="object">
            <en>Custom Button</en>
        </values>
    </name>
</label>
```

With `xf:copy`, the selection is written as follows:

```xml
<xf:select1 ref="label" appearance="full">
    <xf:item>
        <xf:label>Predefined</xf:label>
        <xf:copy ref="xf:attribute('type', 'PredefinedButtonLabel')"/>
    </xf:item>
    <xf:item>
        <xf:label>Custom</xf:label>
        <xf:copy
            ref="
                xf:attribute('type', 'CustomButtonLabel'),
                if (@type = 'CustomButtonLabel') then
                    *
                else
                    instance('my-custom-button-label-template')"/>
    </xf:item>
</xf:select1>
<xf:input ref="label/name/values/en"/>
```

The `if ... then ... else` pattern in the second `xf:copy` is there so that, in case the user has already selected a `CustomButtonLabel` and edited the value of the button label (here "Custom Button" initially), there will still be an exact match and the item shows as selected.

## Remaining features

For what remains to be implemented, see the [issues tagged "XForms 2.0"](https://github.com/orbeon/orbeon-forms/issues?q=is%3Aopen+sort%3Aupdated-desc+label%3A%22Area%3A+XForms+2.0%22).
