# Attribute Value Templates (AVTs)

<!-- toc -->

## Introduction

Certain attributes in XForms are literal values defined by the form author at the time the form is written, as opposed to being evaluated at runtime. Examples include the `resource` attribute on `<xf:submission>` or `<xf:load>`.

To improve this, Orbeon Forms supports a notation called _Attribute Value Templates_ (AVTs) which allows including XPath expressions within attributes. You include XPath expressions in attributes by enclosing them within curly brackets (`{` and `}`).

_NOTE: AVTs were first introduced in [XSLT][1]._

## Example

Consider this example:

```xml
<xf:load
    resource="/forms/detail/{
        instance('documents-instance')/documents/document[index('documents-repeat')]/id
    }"/>
```

When `<xf:load>` is executed, the `resource` attribute is evaluated. The results is the concatenation of `/forms/detail/` and of the result of the expression within brackets:

```xml
instance('documents-instance')/documents/document[index('documents-repeat')]/id
```

If the `id` element pointed to contains the string `C728595E0E43A8BF50D8DED9F196A582`, the `resource` attribute takes the value:

```xml
/forms/detail/C728595E0E43A8BF50D8DED9F196A582
```

Note the following:

* If you need curly brackets as literal values instead of enclosing an XPath expression, escape them using double brackets (`{{` and `}}`).
* You can use as many XPath expressions as you want within a single attributes, each of them enclosed by curly brackets.

## AVTs on XForms elements

### Model

#### <xf:submission>

* `method`
* `action` and `resource`
* `serialization`
* `mediatype`
* `version`
* `encoding`
* `separator`
* `indent`
* `omit-xml-declaration`
* `standalone`
* `validate`
* `relevant`
* `mode`
* `xxf:target`
* `xxf:username`
* `xxf:password`
* `xxf:readonly`
* `xxf:shared`
* `xxf:xinclude`
* `xxf:calculate`

### Actions

#### <xf:dispatch>

* `name`
* `target`
* `bubbles`
* `cancelable`
* `delay`
* `xxf:show-progress`
* `xxf:progress-message`

#### <xxf:hide>

* `dialog`

#### <xf:insert>

* `position`

#### <xf:load>

* resource
* replace
* xxf:target
* xxf:show-progress
* f:url-type

#### <xf:setfocus>

* `control`
* `xxf:deferred-updates`

#### <xf:setindex>

* `xxf:deferred-updates`

#### <xxf:show>

* `dialog`
* `neighbor`
* `constrain`

#### <xf:toggle>

* `case`
* `xxf:deferred-updates`

### Controls

#### All controls

* `style`: equivalent to the HTML `style` attribute
* `class`: equivalent to the HTML `class` attribute

#### <xf:input> and <xf:secret>

* `xxf:size`: equivalent to the HTML `size` attribute
* `xxf:maxlength`: equivalent to the HTML `maxlength` attribute
* `xxf:autocomplete`: equivalent to the HTML `autocomplete` attribute

#### `<xf:textarea>`

* `xxf:cols`: equivalent to the HTML `cols` attribute (prefer using CSS for this)
* `xxf:rows`: equivalent to the HTML `rows` attribute (prefer using CSS for this)
* `xxf:maxlength`: equivalent to the HTML 5 `maxlength` attribute

## AVTs on XHTML Elements

AVTs are also supported on XHTML elements. 

For example:

```xml
<xh:table class="zebra-table">
    <xh:tbody>
        <xf:repeat ref="*">
            <xh:tr
                class="zebra-row-{if (position() mod 2 = 0) then 'even' else 'odd'}">
                <xh:td>
                    <xf:output value="."/>
                </xh:td>
            </xh:tr>
        </xf:repeat>
    </xh:tbody>
</xh:table>
```

In the above example, the value of the `class` attribute on `<xh:tr>` is determined dynamically through XPath and XForms. Even table rows get the class `zebra-row-even` and odd table rows get the class `zebra-row-odd`.

The values of XHTML attributes built using AVTs update as you interact with the XForms page. In the example above, inserting or deleting table rows after the page is loaded will still correctly update the `class` attribute.

It is also possible to use AVTs outside `<xh:body>`, for example:

```xml
<xh:html lang="{instance('language-instance')}" xml:lang="{instance('language-instance')}">...</xh:html>
```

AVTs are also usable on HTML elements within `<xf:label>`, `<xf:hint>`, `<xf:help>`, `<xf:alert>`:

```xml
<xf:input ref="foobar">
    <xf:label>
        <xh:span 
            class="{
                if (. = 'green') then 'green' else 'red'
            }-label">Inverted label</xh:span>
    </xf:label>
</xf:input>
``` 

_NOTE: It is not possible to use AVTs within the id attribute of XHTML elements._  

[1]: http://www.w3.org/TR/xslt20/
