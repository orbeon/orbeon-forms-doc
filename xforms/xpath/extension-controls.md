# Controls functions

## xxf:binding()

```xpath
xxf:binding(
    $control-id as xs:string
) as item()*
```

The `xxf:binding()` function returns a control's binding, that is the item or items to which the control is bound. Use this function carefully, as depending on when this function is called during XForms processing, it may refer to stale items. It is usually safe to use `xxf:binding()` in response to UI events.

```xml
<!-- Store the value of the element to which the first-name control is bound -->
<xf:setvalue ref="my-value" value="xxf:binding('first-name')"/>
```

_NOTE: This function can return not only nodes, but also atomic items._

[SINCE Orbeon Forms 2018.2]

This function also returns the binding if the control has a binding but is non-relevant. In previous versions, if the control was non-relevant, this function always returned the empty sequence.

## xxf:binding-context()

```xpath
xxf:binding-context(
    $control-id as xs:string
) as item()?
```

The `xxf:binding-context()` function returns the context of a control's binding, that is the single in-scope context item before the control's binding is applied if present. Use this function carefully, as depending on when this function is called during XForms processing, it may refer to stale items. It is usually safe to use `xxf:binding-context()` in response to UI events.

```xml
<!-- Store the value of the element to which the first-name control is bound -->
<xf:var
    name="context"
    value="xxf:binding-context('my-custom-component')"/>
```

_NOTE: Before Orbeon Forms 2016.1, this function could return more than one item when called for a component within a repeat. This was incorrect and has been fixed. See [#2642](https://github.com/orbeon/orbeon-forms/issues/2642)._

_NOTE: This function is rarely used, and when used is typically used from within XBL components._

_NOTE: This function can return not only nodes, but also atomic items._

## xxf:case()

```xpath
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

```xpath
xxf:cases(
    $switch-id as xs:string
) as xs:string*
```

The `xxf:cases()` function returns a sequence of ids of `<xf:case>` elements within the given `<xf:switch>`. It is recommended to use this function from XForms actions only.

## xxf:client-id()

[SINCE Orbeon Forms 4.3]

```xpath
xxf:client-id($static-or-absolute-id as xs:string) as xs:string?
```

Resolve the XForms object with the id specified, and return the id as used on the client.

Return the empty sequence if the resolution fails.

```xml
<xh:a href="#{xxf:client-id('my-element')}"/>

<xh:div id="my-element" xxf:control="true">...</xh:div>
```

## xxf:component-param-value()

[SINCE Orbeon Forms 2017.1]

```xpath
xxf:component-param-value(
    $name as xs:string
) as xs:anyAtomicType?
```

The `xxf:component-param-value()` function returns the string value of the given parameter of the current XBL component's bound node.

A parameter is specified by:

- an attribute on the XBL bound node (which can be an attribute value template (AVT))
- or, if not found, a property of the form `oxf.xforms.xbl.$PREFIX.$NAME.$PARAMETER`

When not returning an empty sequence, the function returns:

- an `xs:string` if the value comes from an attribute
- `xs:string`, `xs:integer`, `xs:boolean` or `xs:anyURI` depending on the actual type of the property when the value comes from a property
 
This function is intended for XBL component implementors.

If the user of the component writes:

```xml
<fr:number suffix="Bytes" .../>
```

The implementor of the component can use the parameter with:
 
```xml
<xf:var 
    name="suffix" 
    value="xxf:component-param-value('suffix')"/>
```

See also [`fr:component-param-value()`](/xforms/xpath/extension-form-runner.md#fr-component-param-value).

## xxf:context()

```xpath
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

## xxf:focusable()

[SINCE Orbeon Forms 4.6]

```xpath
xxf:focusable($static-or-absolute-id) as xs:boolean
```

For the given control id, return whether the control is currently focusable.

- To be focusable, a control must be relevant and not readonly.
- For container controls, the function returns `true()` only if it contains at least one focusable control.
- For `xf:switch`, the search only takes place in the current `xf:case`.
- Some controls are never focusable, like controls with appearance `xxf:internal` or `xf:var`.  

## xxf:index()

```xpath
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

## xxf:is-control-readonly()

[SINCE Orbeon Forms 2017.1]

```xpath
xxf:is-control-readonly(
    $control-id as xs:string
) as xs:boolean
```

The `xxf:is-control-readonly()` function returns `true()` if and only if the controlled specified by `$control-id` exists, is relevant, and is readonly.

## xxf:is-control-relevant()

[SINCE Orbeon Forms 2016.3]

```xpath
xxf:is-control-relevant(
    $control-id as xs:string
) as xs:boolean
```

The `xxf:is-control-relevant()` function returns `true()` if and only if the controlled specified by `$control-id` exists and is relevant.

## xxf:is-control-required()

[SINCE Orbeon Forms 2017.1]

```xpath
xxf:is-control-required(
    $control-id as xs:string
) as xs:boolean
```

The `xxf:is-control-required()` function returns `true()` if and only if the controlled specified by `$control-id` exists, is relevant, and is required.

## xxf:is-control-valid()

[SINCE Orbeon Forms 2017.1]

```xpath
xxf:is-control-valid(
    $control-id as xs:string
) as xs:boolean
```

The `xxf:is-control-valid()` function returns `true()` if and only if the controlled specified by `$control-id` exists, is relevant, and is valid.

## xxf:itemset()

```xpath
xxf:itemset(
    $control-id     as xs:string,
    $format         as xs:string,
    $selected-items as xs:boolean?
) as item()?
```

The `xxf:itemset()` function returns the current value of a given control's itemset.

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
* item help and hint if present [SINCE Orbeon Forms 4.5]
* item attributes (JSON only)
    * `class` attribute if present
    * `style` attribute if present
    * `xxforms-open` attribute if `xxf:open` is present
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
        <item>
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

Example of JSON result with attributes:

```json
[
  {
    "label": "Encyclopedia",
    "value": "en1",
    "attributes": {
      "class": "my-class-1",
      "xxforms-open": "true"
    },
    "children": [
      {
        "label": "Science",
        "value": "sc1",
        "attributes": {
          "class": "my-class-2",
          "xxforms-open": "true"
        }
      },
      {
        "label": "Culture",
        "value": "cu1",
        "attributes": {
          "class": "my-class-3",
          "xxforms-open": "true"
        },
        "children": [
          {
            "label": "Art",
            "value": "ar1",
            "attributes": {
              "class": "my-class-4",
              "xxforms-open": "true"
            }
          },
          {
            "label": "Craft",
            "value": "cr1",
            "attributes": {
              "class": "my-class-5",
              "xxforms-open": "true"
            }
          }
        ]
      }
    ]
  },
  {
    "label": "Encyclopedia",
    "value": "en2",
    "attributes": {
      "class": "my-class-6",
      "xxforms-open": "true"
    },
    "children": [
      {
        "label": "Science",
        "value": "sc2",
        "attributes": {
          "class": "my-class-7",
          "xxforms-open": "true"
        }
      },
      {
        "label": "Culture",
        "value": "cu2",
        "attributes": {
          "class": "my-class-8",
          "xxforms-open": "true"
        },
        "children": [
          {
            "label": "Art",
            "value": "ar2",
            "attributes": {
              "class": "my-class-9",
              "xxforms-open": "true"
            }
          },
          {
            "label": "Craft",
            "value": "cr2",
            "attributes": {
              "class": "my-class-10",
              "xxforms-open": "true"
            }
          }
        ]
      }
    ]
  }
]
```

## xxf:label, xxf:help, xxf:hint, xxf:alert

```xpath
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

## xxf:pending-uploads()

```xpath
xxf:pending-uploads() as xs:integer
```

The `xxf:pending-uploads()` function returns the number of known pending uploads in the page.

If there is no pending upload, the function returns 0.

A pending upload is an upload started but not completed yet.

_NOTE: The XForms engine is informed of uploads start and completion in an asynchronous way. This function only indicates the best knowledge the server has of the status of uploads at any given time._

See also: [Upload control][?]

## xxf:repeat-current()

_NOTE: You can often use [`xxf:context()`](#xxfcontext) or the XForms 1.1 `context()` function instead._

```xpath
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
## xxf:repeat-items()

[SINCE Orbeon Forms 4.5]

_NOTE: This function is also available in previous versions of Orbeon Forms as xxf:repeat-nodeset()._

```xpath
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

## xxf:repeat-position()

```xpath
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

## xxf:value()

```xpath
xxf:value(
    $control-id as xs:string
) as xs:string?
```

The `xxf:value()` function returns the value for one or more controls. If a control is non-relevant or cannot hold a value (like `xf:group` or `xf:repeat`), the function returns an empty sequence for that control.

_NOTE: You must be careful when using this function as a control's value might be out of date. Keep in mind that control values are updated during refresh._

[SINCE Orbeon Forms 2019.1]

```xpath
xxf:value(
    $control-id     as xs:string,
    $follow-indexes as xs:boolean
) as xs:string*
```

The two-argument function adds the `$follow-indexes` argument.

- `$control-id`
    - the id of the control or controls to find
    - the id may refer to zero, one, or multiple controls in the case of controls within `xf:repeat`
- `$follow-indexes`
    - if missing, takes the value `true()`.
    - if `false()`
        - This finds the "closest" matching control without checking repeat indexes. When descending into repeat iterations, all repeat iterations are chosen. 
        - Zero, one, or more values can be returned.
    - if `true()`
        - This finds the "closest" matching control by following repeat indexes when possible. When descending into repeat iterations, the iteration matching the enclosing repeat's current index is chosen.
        - At most one value is returned.

## xxf:formatted-value()

[SINCE Orbeon Forms 2019.1]

```xpath
xxf:formatted-value(
    $control-id     as xs:string,
    $follow-indexes as xs:boolean
) as xs:string?
```

The `xxf:formatted-value()` function returns the formatted value for one or more controls. If a control is non-relevant or cannot hold a value (like `xf:group` or `xf:repeat`), the function returns an empty sequence for that control.

- `$control-id`
    - the id of the control or controls to find
    - the id may refer to zero, one, or multiple controls in the case of controls within `xf:repeat`
- `$follow-indexes`
    - if missing, takes the value `true()`.
    - if `false()`
        - This finds the "closest" matching control without checking repeat indexes. When descending into repeat iterations, all repeat iterations are chosen. 
        - Zero, one, or more values can be returned.
    - if `true()`
        - This finds the "closest" matching control by following repeat indexes when possible. When descending into repeat iterations, the iteration matching the enclosing repeat's current index is chosen.
        - At most one value is returned.

_NOTE: You must be careful when using this function as a control's value might be out of date. Keep in mind that control values are updated during refresh._

See also [XLB modes](/xforms/xbl/modes.md#formatted-value).

## xxf:visited

```xpath
xxf:visited(
    $control-id as xs:string
) as xs:boolean?
```

Whether the given control has been visited, either by losing focus or via the `<xxf:setvisited>` action.

If the control is not found or not relevant, the function returns the empty sequence.
