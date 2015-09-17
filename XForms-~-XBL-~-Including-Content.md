> [[Home]] ▸ [[XForms]] ▸ [[XBL|XForms ~ XBL]]

The `xbl:content` element allows copying elements which are descendant elements from the bound node. The selectors work as if applied to an XML document rooted at the bound node.

Say you have this markup:

```xml
<fr:inline-input ref="name">
    <xf:label>Name</xf:label>
</fr:inline-input>
```

The implementation of the `fr:inline-input` component can copy the nested `xf:label` element under a group as follows:

```xml
<xf:group>
    <xbl:content includes="xf|label"/>
```

The CSS selection expression above is exactly equivalent to writing in XPath:

```xml
descendant-or-self::xf:label
```

Note the difference of notation in XML/XPath and in CSS to refer to qualified element names:

* XML/XPath uses a "colon" character: `foo:bar`
* CSS uses a "pipe" character: `foo|bar`

Both XPath and CSS are expression languages allowing selecting nodes from XML documents, but they have a quite different syntax!

Now here is a more complex scenario:

```xml
<fr:link-select1 ref="gender">
    <xf:label>Gender</xf:label>
    <xf:itemset nodeset="instance('genders')/gender">
        <xf:label ref="label"/>
        <xf:value ref="value"/>
    </xf:itemset>
</fr:link-select1>
```

You would think that the implementation of the `fr:link-select1` component could simply copy the nested `xf:label` element as follows:

```xml
<xf:group>
    <xbl:content includes="xf|label"/>
```

But this doesn't work properly because the CSS selector `xf|label` actually returns _all descendant label elements_, including the `xf:label` element under `xf:itemset`.

The recommend way to express this is as follows:

```xml
<xf:group>
    <xbl:content includes=":root > xf|label"/>
```

The `:root` pseudo-class refers to the bound element (here `fr:link-select1`). The `&gt;` combinator "describes a childhood relationship between two elements", like the XPath `/` axis. So the result is equivalent to the XPath:

```xml
/*/xf:label
```
