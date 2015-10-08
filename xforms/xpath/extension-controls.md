# Controls functions

## xxf:binding()

```ruby
xxf:binding(
    $control-id as xs:string
) as item()*
```

The `xxf:binding()` function returns a control's binding, that is the node or nodes to which the control is bound. Use this function carefully, as depending on when this function is called during XForms processing, it may refer to stale nodes. Likely the safest use of `xxf:binding()` is in response to UI events.

```xml
<!-- Store the value of the element to which the first-name control is bound -->
<xf:setvalue ref="my-value" value="xxf:binding('first-name')"/>
```

_NOTE: This function can return not only nodes, but also atomic items._

## xxf:client-id()

[SINCE Orbeon Forms 4.3]

```ruby
xxf:client-id($static-or-absolute-id as xs:string) as xs:string?
```

Resolve the XForms object with the id specified, and return the id as used on the client.

Return the empty sequence if the resolution fails.

```xml
<xh:a href="#{xxf:client-id('my-element')}"/>

<xh:div id="my-element" xxf:control="true">...</xh:div>
```

## xxf:context()

```ruby
xxf:context(
    $element-id as xs:string
) as node()
```

The `xxf:context()` function allows you to obtain the single-node binding for an enclosing `xf:group`, `xf:repeat`, or `xf:switch`. It takes one mandatory string parameter containing the id of an enclosing grouping XForms control. For xf:repeat, the context returned is the context of the current iteration.

```xml
<xf:group ref="employee" id="employee-group">
    <!-- The context is being set to another instance that controls the visibility of the group. -->
    <xf:group ref="instance('control-instance')/input">
        <!-- Using xxf:context() allows reclaiming the context of the enclosing group. -->
        <xf:input ref="xxf:context('employee-group')/name">
            <xf:label>Employee Name</xf:label>
        </xf:input>
    </xf:group>
</xf:group>
```

See also the XForms 1.1 `context()` function, which returns the current evaluation context:

```xml
<xf:group ref="employee">
    <xf:setvalue ref="instance('foo')/name" value="context()/name"/>
</xf:group>
```

## xxf:value()

```ruby
xxf:value(
    $control-id as xs:string
) as xs:string?
```

The `xxf:value`() function returns a control's value, it is has any. If the control is non-relevant or cannot hold a value (like `xf:group` or `xf:repeat`), the function returns the empty sequence.

_NOTE: You must be careful when using this function as a control's value might be out of date. Keep in mind that control values are updated during refresh._

## xxf:index()

```ruby
xxf:index(
    $repeat-id as xs:string?
) as xs:integer
```

The `xxf:index()` function behaves like the standard XForms `index()` function, except that its argument is optional. When the argument is omitted, the function returns the index of the closest enclosing `<xf:repeat>` element. This function must always be used within `<xf:repeat>` otherwise an error is raised.

```xml
<xf:repeat ref="employee" id="employee-repeat">
    <div>
    <xf:trigger>
        <xf:label>Add One</xf:label>
        <xf:insert
            ev:event="DOMActivate"
            ref="../employee"
            at="xxf:index()"/>
    </xf:trigger>
    </div>
</xf:repeat>
```

## xxf:case()

```ruby
xxf:case(
    $switch-id as xs:string
) as xs:string?
```

The `xxf:case()` function returns the id of the currently selected `<xf:case>` within the given `<xf:switch>`. It is recommended to use this function from XForms actions only.

```xml
<xf:switch id="my-switch">
    <xf:case id="my-case-1">...</xf:case>
    <xf:case id="my-case-2">...</xf:case>
</xf:switch>
...
<xf:trigger>
    <xf:label>Add One</xf:label>
    <xf:setvalue if="xxf:case('my-switch')" ref="foobar" value="12"/>
</xf:trigger>
```

## xxf:cases()

```ruby
xxf:cases(
    $switch-id as xs:string
) as xs:string*
```

The `xxf:cases()` function returns a sequence of ids of `<xf:case>` elements within the given `<xf:switch>`. It is recommended to use this function from XForms actions only.

## xxf:repeat-items()

[SINCE: Orbeon Forms 4.5]

_NOTE: This function is also available in previous versions of Orbeon Forms as xxf:repeat-nodeset()._

```ruby
xxf:repeat-nodeset(
    $repeat-id as xs:string?
) as node()*
```

The `xxf:repeat-nodeset()` function returns the node-set of an enclosing `xf:repeat`. It takes a string parameter containing the id of an enclosing repeat XForms control. When the argument is omitted, the function returns the index of the closest enclosing `<xf:repeat>` element. This function must always be used within `<xf:repeat>` otherwise an error is raised.

```xml
<xf:repeat id="employee-repeat" ref="employee">
    <xh:div>
        <xf:output value="count(xxf:repeat-nodeset('employee-repeat'))"/>
    </xh:div>
</xf:repeat>
```

## xxf:repeat-current()

_NOTE: You can often use [`xxf:context()`][2] or the XForms 1.1 `context()` function instead._

```ruby
xxf:repeat-current(
    $repeat-id as xs:string?
) as node()
```

The `xxf:repeat-current()` function allows you to obtain a reference to an enclosing `xf:repeat`'s current iteration node. It takes one optional string parameter. If present, the id of the enclosing `xf:repeat` is searched. If absent, the function looks for the closest enclosing `xf:repeat`.

```xml
<xf:repeat ref="employee" id="employee-repeat">
    <tr>
        <td>
            <!-- The context is being set to another instance that controls the
                 visibility of the group. -->
            <xf:group ref="instance('control-instance')/input">
                <!-- Using xxf:repeat-current() allows reclaiming the context of
                     the repeat iteration. -->
                <xf:input ref="xxf:repeat-current('employee-repeat')/name">
                    <xf:label>Employee Name</xf:label>
                </xf:input>
            </xf:group>
        </td>
    </tr>
</xf:repeat>
```

The `xxf:repeat-current()` function must be called from within an `xf:repeat` element.

## xxf:repeat-position()

```ruby
xxf:repeat-position(
    $repeat-id as xs:string?
) as xs:integer
```

The `xxf:repeat-position()` function returns an enclosing `xf:repeat`'s current iteration position. It takes one optional string parameter. If present, the id of the enclosing `xf:repeat` is searched. If absent, the function looks for the closest enclosing `xf:repeat`.

```xml
<xf:repeat ref="employee" id="employee-repeat">
    <div>
        <xf:output value="xxf:repeat-position()"/>
    </div>
</xf:repeat>
```

The `xxf:repeat-current()` function must be called from within an `xf:repeat` element.

## xxf:pending-uploads()

```ruby
xxf:pending-uploads() as xs:integer
```

The xxf:pending-uploads() function returns the number of known pending uploads in the page.

If there is no pending upload, the function returns 0.

A pending upload is an upload started but not completed yet.

_NOTE: The XForms engine is informed of uploads start and completion in an asynchronous way. This function only indicates the best knowledge the server has of the status of uploads at any given time._

See also: [Upload control][?]

## xxf:itemset()

```ruby
xxf:itemset(
    $control-id     as xs:string,
    $format         as xs:string,
    $selected-items as xs:boolean?
) as item()?
```

The xxf:itemset() function returns the current value of a given control's itemset.

* The first parameter is the id of a selection control (`<xf:select>` or `<xf:select1>`).
* The second parameter is the format to return:
    * `xml`: an XML document, useful when the result is handled directly in XForms; specifically, the document is returned
    * `json`: a JSON tree, useful when the result is handled from JavaScript
* The third, optional parameter determines whether information about selected items is returned

The resulting tree represents the itemset hierarchy as seen by the control, with the following information:

* relevant items
* hierarchy of the itemset
* item label
* item value
* item help and hint if present [SINCE: Orbeon Forms 4.5]
* which are the currently selected items, if `$selected-items` is `true()`

_NOTE: Because itemsets re-evaluate during refresh, it is recommended that this function be used only within action handlers responding to refresh events to ensure consistency._

With the following flat instance and itemset:

```xml
<xf:instance id="itemset">
    <fruits>
        <fruit id="1">
            <description>Apple</description>
            <color>Green</color>
        </fruit>
        <fruit id="2">
            <description>Banana</description>
            <color>Yellow</color>
        </fruit>
        <fruit id="3">
            <description>Orange</description>
            <color>Orange</color>
        </fruit>
        <fruit id="4">
            <description>Kiwi</description>
            <color>Green</color>
        </fruit>
    </fruits>
</xf:instance>
...
<xf:select1 id="my-select1">
    <xf:itemset ref="instance('itemset')/fruit">
        <xf:label ref="description"/>
        <xf:value ref="@id"/>
    </xf:itemset>
</xf:select1>
```

Example of XML result with `xxf:itemset('my-select1', 'xml', true())` (formatted for readability)

```xml
<itemset>
    <choices>
        <item>
            <label>Apple</label>
            <value>1</value>
        </item>
        <items>
            <label>Banana</label>
            <value>2</value>
        </item>
        <item selected="true">
            <label>Orange</label>
            <value>3</value>
        </item>
        <item>
            <label>Kiwi</label>
            <value>4</value>
        </item>
    </choices>
</itemset>
```

In this example, you can access the label of the selected item with `xxf:itemset('my-select1', 'xml', true())/itemset/choices/item[@selected = 'true']/value`. The following is an example of JSON result with `xxf:itemset('my-select1', 'json', true())` (formatted for readability):

```json
[
   [
      "Apple","1"
   ],
   [
      "Banana","2"
   ],
   [
      "Orange","3",true
   ],
   [
      "Kiwi","4"
   ]
]
```

With the following hierarchical instance and itemset:

```xml
<xf:instance id="itemset">
    <items>
        <item id="1"/>
        <item id="2">
            <item id="2.1"/>
            <item id="2.2">
                <item id="2.2.1"/>
                <item id="2.2.2">
                    <item id="2.2.2.1"/>
                    <item id="2.2.2.2"/>
                </item>
            </item>
        </item>
    </items>
</xf:instance>
...
<xf:select1 id="my-select1">
    <xf:itemset ref="instance('itemset')//item">
        <xf:label ref="@id"/>
        <xf:value ref="@id"/>
    </xf:itemset>
</xf:select1>
```

Example of XML result with `xxf:itemset('my-select1', 'xml'`, true())` (formatted for readability):

```xml
<itemset>
    <choices>
        <item>
            <label>1</label>
            <value>1</value>
        </item>
        <item>
            <label>2</label>
            <value>2</value>
            <choices>
                <item selected="true">
                    <label>2.1</label>
                    <value>2.1</value>
                </item>
                <item>
                    <label>2.2</label>
                    <value>2.2</value>
                    <choices>
                        <item>
                            <label>2.2.1</label>
                            <value>2.2.1</value>
                        </item>
                        <item>
                            <label>2.2.2</label>
                            <value>2.2.2</value>
                            <choices>
                                <item>
                                    <label>2.2.2.1</label>
                                    <value>2.2.2.1</value>
                                </item>
                                <item>
                                    <label>2.2.2.2</label>
                                    <value>2.2.2.2</value>
                                </item>
                            </choices>
                        </item>
                    </choices>
                </item>
            </choices>
        </item>
    </choices>
</itemset>
```

Example of JSON result with `xxf:itemset('my-select1', 'json', true())` (formatted for readability):

```json
[
   [
      "1","1"
   ],
   [
      "2","2",
      [
         "2.1","2.1",true
      ],
      [
         "2.2","2.2",
         [
            "2.2.1","2.2.1"
         ],
         [
            "2.2.2","2.2.2",
            [
               "2.2.2.1","2.2.2.1"
            ],
            [
               "2.2.2.2","2.2.2.2"
            ]
         ]
      ]
   ]
]
```

## xxf:label, xxf:help, xxf:hint, xxf:alert

```ruby
xxf:label($control-id as xs:string) as xs:string?
xxf:help ($control-id as xs:string) as xs:string?
xxf:hint ($control-id as xs:string) as xs:string?
xxf:alert($control-id as xs:string) as xs:string?
```

These functions return a control's current label, help, hint, or alert, given a control id.

```xml
<xf:input id="my-input" ref="foo">
    <xf:label>Label</xf:label>
</xf:input>

<xf:trigger>
   <xf:label>Get label</xf:label>
    <xf:message ev:event="DOMActivate" value="xxf:label('my-input')"/>
</xf:trigger>
```

If the control is not  relevant, or does not have an associated label, help, hint, or alert, the empty sequence is returned.

## xxf:visited

```ruby
xxf:visited(
    $control-id as xs:string
) as xs:boolean?
```

Whether the given control has been visited, either by losing focus or via the `<xxf:setvisited>` action.

If the control is not found or not relevant, the function returns the empty sequence.
