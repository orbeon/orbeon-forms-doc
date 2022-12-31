# XForms - Variables

## Rationale

Orbeon Forms supports declaring variables which look and behave very much like XSLT variables. Variables are extremely useful, for example to avoid repeating long XPath expressions, or to give an XPath expression unambiguous access to data computed in enclosing `<xf:group>` or `<xf:repeat>` elements.

## The variable element

You define variables with the extension element:

```xml
<xf:var>
```

For backward compatibility, the following elements are still supported:

* `<xxf:variable>`
* `<exf:variable>` (in the eXforms namespace)

The element supports the following attributes: 

|           |           |                                                                           |
|-----------|-----------|---------------------------------------------------------------------------|
| `name`    | mandatory | name of the variable                                                      |
| `as`      | optional  | type of the variable (ignored but can be used for documentation purposes) |
| `model`   | optional  | contributes to the variable's single-item binding                         |
| `context` | optional  | contributes to the variable's single-item binding                         |
| `ref`     | optional  | contributes to the variable's single-item binding                         |
| `bind`    | optional  | contributes to the variable's single-item binding                         |
| `value`   | optional  | XPath 2.0 expression determining the value of the variable                |

The expression is evaluated in the element's single-item binding if present, otherwise in the context of its parent.

If the `value` attribute is omitted, the text content of the `<xf:var>` element is used as the value.

## Variables within xf:model

Directly under the `<xf:model>` element, the following rules apply:

- Variables are supported directly under the `<xf:model>` element.
- Model variables are evaluated in the order in which they appear in the model.
- All model variables are visible to other model elements such as `<xf:bind>` or `<xf:submission>`.
- Model variables are also visible from XPath expression outside of models whenever that model is in scope.

```xml
<xh:head>
    <xf:model id="my-model">
        <xf:instance id="my-instance">...</xf:instance>
        <xf:var name="mine" value="."/>
    </xf:model>
    <xf:model id="her-model">
        <xf:instance id="her-instance">...</xf:instance>
        <xf:var name="hers" value="instance('her-instance')"/>
    </xf:model>
</xh:head>
<xh:body>
    <!-- $mine is visible from here, but not $hers -->
    <xf:output value="$mine">
        <xf:label>My stuff:</xf:label>
    </xf:output>
    <xf:group model="her-model">
        <!-- $hers is visible from here, but not $mine -->
        <xf:output value="$hers">
            <xf:label>Her stuff:</xf:label>
        </xf:output>
    </xf:group>
</xh:body>
```
## Variables declared by xf:bind elements

See [XForms: Model bind variables](model-bind-variables.md).

## Variables outside of xf:model

Outside of models, the following rules apply:

- Variables are supported anywhere XForms controls are allowed.
- A given variable is visible to any XPath expression on a following sibling element or on a following sibling element's descendant element.

```xml
<xh:body>
    <!-- Variable pointing to the default instance's root element -->
    <xf:var name="instance" value="."/>
    <!-- variable pointing to all the item children -->
    <!-- It uses the variable declared above -->
    <xf:var name="items" value="$instance/item"/>

    <!-- The code below uses the variables in scope -->
    <xf:repeat ref="$items" id="items-repeat">
        <xf:var name="current-item" value="."/>
        <xf:var name="current-position" value="position()"/>
        <xf:output id="my-count" ref="$current-item/value">
            <xf:label value="concat($current-item/label, ':')"/>
            <xf:setvalue event="my-event" ref="$current-item/value" value="count($items) + $current-position"/>
        </xf:output>
    </xf:repeat>
</xh:body>
```

## Variables within xf:action

Variables are allowed within `<xf:action>` elements, whether those elements are used within models or controls.

```xml
<xf:model>
    <xf:instance>
        <instance>
            <foo>42</foo>
            <bar>43</bar>
        </instance>
    </xf:instance>
    <!-- These variables are defined within the model -->
    <xf:var name="foo" value="foo"/>
    <xf:var name="bar" value="bar"/>
</xf:model>
...
<xf:group>
    <!-- This variable is defined within the group control and has access to the in-scope model variables -->
    <xf:var name="sum" value="$foo + $bar"/>
    <xf:group>
        <xf:action event="my-event">
            <!-- This action has access to all variables in scope -->
            <xf:setvalue ref="sum" value="$sum"/>
            <!-- This variable is defined within the action and has access to all the variables in scope -->
            <xf:var name="difference" value="$foo - $bar"/>
            <xf:setvalue ref="difference" value="$difference"/>
        </xf:action>
    </xf:group>
</xf:group>
```

## The xxf:value element

You can nest an `<xxf:value>` element within `<xf:var>`.

_NOTE: The `<xxf:value>` element replaces the deprecated  `<xxf:sequence>` element but works exactly the same way. Until Orbeon Forms 4.6 included, you must use the element `<xxf:sequence>`. From Orbeon Forms 4.7 onward, use `<xxf:value>`._

The element supports the following attributes: 

||||
|---|---|---|
|`model`    |optional|contributes to the element's single-item binding|
|`context`  |optional|contributes to the element's single-item binding|
|`ref`      |optional|contributes to the element's single-item binding|
|`bind`     |optional|contributes to the element's single-item binding|
|`value`    |optional|XPath 2.0 expression determining the value of the variable|

The expression is evaluated in the element's single-item binding if present, otherwise in the context of its parent `<xf:var>` element. If the attribute is omitted, the text content of the `<xxf:value>` element is used as the value.

This allows you to decouple the variable definition (name) from its evaluation:

```xml
<xf:var name="sum">
    <xxf:value value="$foo + $bar"/>
</xf:var>
```

This is particularly useful for XBL component implementors, where this can be used in combination with the `xxbl:scope` attribute:

```xml
<xf:var name="result" as="node()?">
    <xxf:value value="." xxbl:scope="outer"/>
</xf:var>
```

Single-item attributes may be used on the `<xxf:value>` element.

## Scoping of variables in the view

The following guidelines apply when using variables in the view:

- Only variables in the current XBL scope are visible
- All preceding view variables are visible even across changes of models (with model="...")
- Finally, the current model's variables are visible
- Variable hiding applies between view variables and model variables

Example:

```xml
<xh:html>
    <xh:head>
        <xf:model id="model1">
            <xf:instance>
                <instance>
                    <foo>42</foo>
                </instance>
            </xf:instance>
            <xf:var name="foo" value="foo"/>
        </xf:model>
        <xf:model id="model2">
            <xf:instance>
                <instance>
                    <foo>44</foo>
                </instance>
            </xf:instance>
            <xf:var name="foo" value="foo"/>
        </xf:model>
    </xh:head>
    <xh:body>
        <!-- Top-level -->
        <!-- model1 is the default model: $foo from model1 is visible -->
        <xf:var name="gaga" value="41"/>
        <!-- $gaga is visible because preceding in the view -->
        <xf:group model="model2">
            <!-- model2 is the current model: $foo from model2 is visible -->
            <xf:var name="toto" value="43"/>
            <!-- $gaga and $toto are visible because they are preceding in the view -->
            <xf:group model="model1">
                <!-- model1 is the current model: $foo from model1 is visible, and $gaga is visible  -->
                <!-- $gaga and $toto are visible because they are preceding in the view -->
            </xf:group>
        </xf:group>
    </xh:body>
</xh:html>
```

## Current limitations

The `<xf:var>` element has the following limitations:

- The `as` attribute is ignored but can be used for documentation purposes.
- Cannot be used within XForms leaf controls such as `<xf:input>`, including within nested items or itemsets (except within `<xf:action>` elements, where they are allowed).
- Cannot be used within nested model elements, including `<xf:bind>` or `<xf:submission>` (except within `<xf:action>` elements, where they are allowed).
