# Standard Event Support

<!-- toc -->

## Events in XForms

The event model of XForms is based on the [Document Object Model (DOM) Level 2 Events][1] specification. This is the same specification that defines how your web browser handles events in HTML documents. This is good news because it means that his knowledge is reusable between XForms and HTML/JavaScript development!

What's new with XForms is that it allows users to declaratively register event handlers following the [XML Events 2][2] specification. If you write HTML and JavaScript code running directly in your browser, you would typically register event handlers using JavaScript APIs. In XForms, which does not mandate JavaScript, XML Events provide a declarative alternative to using JavaScript. This usually makes it clearer how listeners are attached to XForms objects.  

## Orbeon Forms support

### Optional `ev:` prefix for event attributes

The usual XForms way of using XML events is by prefixing attributes with the `ev:` prefix. This is in fact not absolutely mandated by XForms, and leads to heavier attribute syntax, so Orbeon Forms allows using the attributes without a namespace. The examples below usually use the `ev:` prefix, but most of the Orbeon Forms code doesn't.

Example with prefix:

```xml
<xf:dispatch 
    ev:observer="child-instance" 
    ev:event="xforms-insert" 
    targetid="main-model" 
    name="update-after-insert"/>
```

Example without prefix:

```xml
<xf:dispatch 
    observer="child-instance" 
    event="xforms-insert" 
    targetid="main-model" 
    name="update-after-insert"/>
```

### Registering event handlers  

[TODO: basic placement and ev:event support]  

### Using the ev:observer and ev:target attributes

The `ev:observer` attribute  allows you to register event handlers by specifying an element identifier, instead of embedding the event handler within that element. This is particularly useful to register event handlers on `` elements, which do not allow you to directly embed XML event handlers.

```xml
<xf:model id="main-model">
    <!-- Child instance -->
    <xf:instance 
        id="child-instance"
        src="my-instance.xml"/>
    <!-- Register the event handler on the child instance -->
    <xf:dispatch 
        ev:observer="child-instance" 
        ev:event="xforms-insert" 
        targetid="main-model" 
        name="update-after-insert"/>
</xf:model>
```

Note that you still need to use the `ev:event` attribute to specify to what event the handler responds. The following example shows how you can define event handlers for XForms elements anywhere in an XForms document:

```xml
<xh:html>
    <xh:head>
        <xf:model id="my-model">
            <!-- A small instance -->
            <xf:instance id="my-instance">
                <instance>initial</instance>
            </xf:instance>
            <!-- Event handler located in the model but observing an element in the view -->
            <xf:action
                ev:observer="my-group"
                ev:target="my-input"
                ev:event="DOMFocusIn">
                <xf:setvalue ref=".">new</xf:setvalue>
            </xf:action>
        </xf:model>
    </xh:head>
    <xh:body>
        <xf:group id="my-group">
            <!-- A simple XForms input control -->
            <xf:input id="my-input" ref=".">
                <xf:label>My Data</xf:label>
            </xf:input>
            <!-- Event handler located in the view but observing an element in the model -->
            <xf:action
                ev:observer="my-model"
                ev:event="xforms-ready">
                <xf:dispatch name="DOMFocusIn" targetid="my-input"/>
            </xf:action>
        </xf:group>
    </xh:body>
</xh:html>
```

The above example also shows how you can constrain an event handler to respond to an event dispatched to a particular target element using the `ev:target` attribute. here, the event handler responds to `DOMFocusIn` events, but only those dispatched to the `my-input` control.   

### The ev:propagate attribute

[TODO: describe]

### The ev:defaultAction attribute

[TODO: describe]

### Top-level event handlers  

You can place event handlers at the top-level under the `<xh:body>` element:

```xml
<xh:body>
    <xxf:variable name="answer" select="42"/>
    <xf:setvalue ev:event="my-event" ref="value" value="$answer"/>
    ...
</xh:body>
```
  
Previously, you had to use a top-level `<xh:group>` for this to work:

```xml
<xh:body>
    <xf:group>
        <xxf:variable name="answer" select="42"/>
        <xf:setvalue ev:event="my-event" ref="value" value="$answer"/>
        ...
    </xf:group>
</xh:body>
```
  
You can also explicitly register top-level handlers using the `#document` observer id:

```xml
<xf:setvalue ev:observer="#document" ev:event="my-event" ref="value" value="$answer"/>
```
  
_NOTE: Events from top-level models do not bubble to handlers observing on  `#document`. Arguably, they should!_  

### Event handlers on XBL bound nodes  

When using an XBL component, you can register handlers in the same way you register handlers on built-in XForms controls.  

In this case, the handler is placed directly under the bound node (`<fr:foo>`):

```xml
<fr:foo id="my-foo">
    <xf:setvalue ev:event="my-event" ref="my-value">43</xf:setvalue>
</fr:foo>
```

Event handlers with the `ev:observer` attribute are also recognized as long as the handler is directly under the bound node:

```xml
<fr:foo id="my-foo">
    <xf:setvalue
        ev:event="my-event"
        ev:observer="my-input"
        ref="my-value">43</xf:setvalue>
</fr:foo>
<xf:input id="my-input" ref="my-value"/>
```

_NOTE: For event handlers nested further within the bound node, the behavior is up to the XBL component. Typically, components that are containing controls, such as ``, manage event handlers as you expect!_  

### Event handlers within XBL bindings  

Event handlers on XBL bindings are very similar to regular XML Events handlers, except:

* they use the `` containing element placed within the `` section of an XBL binding
* attributes do not use the XML Events namespace (typically with the `ev:` prefix)  
* the XBL 2 `default-action` attribute is not supported but instead the XML Events 1 `defaultAction` is supported (both support the value `cancel` and `perform` values)  

Example:

```xml
<xbl:handlers>
    <xbl:handler
        event="xforms-focus"
        phase="target"
        defaultAction="cancel">
        <xxf:script id="xf-sf">YAHOO.xbl.fr.Currency.instance(this).setfocus();</xxf:script>
    </xbl:handler>
</xbl:handlers>
```

### Delayed events

#### Support and limitations

Orbeon Forms partially supports the XForms 1.1 `delay` attribute on `<xf:dispatch>`. The limitations are:

* Events are not combined as specified in XForms 1.1.
* Custom event context information is not supported and simply ignored (see [#2579](https://github.com/orbeon/orbeon-forms/issues/2579)).

Until Orbeon Forms 4.10 included:

* A delay greater than zero always incurs a round-trip with the client. This may mean higher latency and resource usage than could be expected. You are advised to use delays in the order of seconds at least.
* A delay of `0` causes the event to be dispatched synchronously righ away, as if the `delay` attribute is not present.
* A non-integer value causes the action to fail with an error.

Since Orbeon Forms 4.11:

- Delayed events are checked for expiration before sending responses to the client.
- A delay of `0` can be used to dispatch an event asynchronously with a guarantee that there will be no roundtrip to the client. This conforms to XForms 1.1.
- A non-integer value causes the event to be dispatched synchronously right away. This conforms to XForms 1.1.

#### Extensions

The boolean attribute `xxf:show-progress` allows specifying whether the client must enable the loading indicator when sending back delay events from the client. The default is `true` and the indicator is used.

```xml
<xf:dispatch 
    name="my-event" 
    targetid="my-model" 
    delay="2000"
    xxf:show-progress="false"/>
```

The `xxf:progress-message` attribute allows specifying a custom progress message when `xxf:show-progress` is `true`. By default, the standard progres message is used.

```xml
<xf:dispatch 
    name="my-event" 
    targetid="my-model" 
    delay="2000"
    xxf:show-progress="true"
    xxf:progress-message="Autosave..."/>
```

For more information, see the [XForms specification](https://www.w3.org/TR/xforms11/#action-dispatch).

### The ev:handler attribute

*Not supported by Orbeon Forms.*

### The ev:listener element

*Not supported by Orbeon Forms.*

## Historical note: differences between specifications  

There are differences between some events specifications, in particular with regard to how events phases are defined hand how handlers can specify event phases.  

* DOM Level 3 Events nicely clarifies the different event phases (capture, target, and bubbling).
* XML Events 1 `phase="default"` attribute means a listener is activated on the `target` or `bubbling` phase.
* XML Events 1 does not support activating an event strictly on the `target` or bubbling phase.
* XBL 2 adds a _default action_ phase separate from the `target` or `bubbling` phases.
* XBL 2 proposes `capture`, `target`, `bubble`, `default-action`, and unspecified values for the phase attribute. If unspecified, this means the `target` or `bubbling` phase.
* XML Events 2 is a W3C Working Group Note and covers declarative event handling for XML

[1]: https://www.w3.org/TR/DOM-Level-2-Events/events.html
[2]: https://www.w3.org/TR/2010/NOTE-xml-events2-20101216/
[3]: https://www.w3.org/TR/DOM-Level-3-Events/
[4]: https://www.w3.org/TR/xbl/
