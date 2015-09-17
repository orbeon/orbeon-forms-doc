> [[Home]] ▸ [[XForms]] ▸ [[XBL|XForms ~ XBL]]

All the generic extensions are in the namespace `http://orbeon.org/oxf/xml/xbl`, and the usual mapping of this namespace is `xmlns:xxbl="http://orbeon.org/oxf/xml/xbl"`.

## xxbl:container attribute

The `xxbl:container` attribute on `xbl:binding` allows specifying the name of the HTML element that the XBL binding uses to encapsulate content. By default, this is `xh:div`. Here is how to change it to `xh:span`:

```xml
<xbl:binding id="my-binding" element="fr|my-binding" xxbl:container="span">
```

## xxbl:attr attribute

The standard `xbl:attr` attribute does not support accessing attributes other than those positioned exactly on the bound element. This is a serious limitation. The Orbeon Forms implementation adds an extension attribute, `xxbl:attr`,  which takes an XPath expression:

```xml
xxbl:attr="xf:alert/(@context | @ref | @bind | @model)"
```

## xbl:template/xxbl:transform attribute

When more flexibility is needed than XBL can provide, the `xxbl:transform` attribute is your friend.

```xml
<xbl:template xxbl:transform="processorName">
    Transformation (inline)
</xbl:template>
```

Orbeon Forms runs the processor specified by this attribute connecting its `config` input to the content of the `xbl:template` and its `data` input to the bound element and replaces the content of the `xbl:template` by the `data` output of the processor. The most frequent expected use is to run XSLT transformations. For instance, to create a widget that alternates styles in table rows within an `xf:repeat`:

```xml
<xbl:binding id="foo-table-alternate" element="foo|table-alternate">
    <xbl:template xxbl:transform="oxf:xslt">
        <xsl:transform version="2.0">
            <xsl:template match="@*|node()">
                <xsl:copy>
                    <xsl:apply-templates select="@*[not(name() = ('style1', 'style2'))]|node()"/>
                </xsl:copy>
            </xsl:template>
            <xsl:template match="foo:table-alternate">
                <xh:table>
                    <xsl:apply-templates select="@*|node()"/>
                </xh:table>
            </xsl:template>
            <xsl:template match="xf:repeat/xh:tr" >
                <xf:var name="position" value="position()"/>
                <xf:group ref=".[$position mod 2 = 1]">
                    <xh:tr xbl:attr="style=style1">
                        <xsl:apply-templates select="@*|node()"/>
                    </xh:tr>
                </xf:group>
                <xf:group ref=".[not($position mod 2 = 1)]">
                    <xh:tr xbl:attr="style=style2">
                        <xsl:apply-templates select="@*|node()"/>
                    </xh:tr>
                </xf:group>
            </xsl:template>
        </xsl:transform>
    </xbl:template>
</xbl:binding>
```

This component can be invoked through:

```xml
<foo:table-alternate class="gridtable" style1="background: red" style2="background: white">
    <xh:tr>
        <xh:th>Label</xh:th>
        <xh:th>Value</xh:th>
    </xh:tr>
    <xf:repeat ref="item">
        <xh:tr>
            <xh:td>
                <xf:output value="@label"/>
            </xh:td>
            <xh:td>
                <xf:output value="@value"/>
            </xh:td>
        </xh:tr>
    </xf:repeat>
</foo:table-alternate>
```

The `xbl:template/@xxbl:transform="oxf:xslt"` attribute specifies that its child element (`xsl:transform`) is considered as an XSLT transformation, runs against the bound element (`foo:table-alternate`), and the result of this transformation is used as the actual content of the `xbl:template`.

The result of your transformation will often contain XBL attributes, and in this sample `xbl:attr` attributes are used to set the `style` of the rows with: `<xh:tr xbl:attr="style=style1">`.

The result of the transformation has to be a (well formed) single rooted XML fragment. This might seem obvious, but that means that you might have to encapsulate the sub elements of `xbl:template` in an XHTML `div` or  `span` compared to what you would have done if you were not applying a transformation.

The transformation has full access to the bound element and can transform any of its child nodes or attributes. To keep things as encapsulated as possible and not change the behavior of bound elements that are potentially embedded, it is a good practice to define tight transformation templates that affect only the nodes that are meant to be transformed. In the example given above, one might for instance argue that  `<xsl:template match="foo:table-alternate/xf:repeat/xh:tr">` would be safer than `<xsl:template match="xf:repeat/xh:tr">`.

You can use `<xsl:message terminate="yes">` to report to the user errors that occur during the XSLT transformation. For example:

```xml
<xsl:message terminate="yes">Terminating!</xsl:message>
```


This results in an error will be output in the log, and an error message will show in the browser.
