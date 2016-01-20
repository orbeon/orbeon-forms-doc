# Events - Standard Support

<!-- toc -->

## Events in XForms

The event model of XForms is based on the [Document Object Model (DOM) Level 2 Events][1] specification. This is the same specification that defines how your web browser handles events in HTML documents. This is good news because it means that his knowledge is reusable between XForms and HTML/JavaScript development!

What's new with XForms is that it allows users to declaratively register event handlers following the [XML Events][2] specification. If you write HTML and JavaScript code running directly in your browser, you would typically register event handlers using JavaScript APIs. In XForms, which does not mandate JavaScript, XML Events provide a declarative alternative to using JavaScript. This usually makes it clearer how listeners are attached to XForms objects.  

## Status of the specifications  

The XML Events specification dates from 2003 and was based on DOM Level 2 Events, which itself dates back to 2000.

Since then, refinements have taken place in the DOM Level 3 Events specification ([currently a working draft][3]). The [XBL 2 specification][4] also proposes a syntax for declarative event handlers.

There are differences between these specifications, in particular with regard to how events phases are defined hand how handlers can specify event phases.  

* DOM Level 3 Events nicely clarifies the different event phases (capture, target, and bubbling).
* XML Events 1 `phase="default"` attribute means a listener is activated on the `target` or `bubbling` phase.
* XML Events 1 does not support activating an event strictly on the `target` or bubbling phase.
* XBL 2 adds a _default action_ phase separate from the `target` or `bubbling` phases.
* XBL 2 proposes `capture`, `target`, `bubble`, `default-action`, and unspecified values for the phase attribute. If unspecified, this means the `target` or `bubbling` phase.
* XML Events 2 is being developed but is still a working draft  

## Orbeon Forms support

### Registering event handlers  

[TODO: basic placement and ev:event support]  

### Using the ev:observer and ev:target attributes

The `ev:observer` attribute  allows you to register event handlers by specifying an element identifier, instead of embedding the event handler within that element. This is particularly useful to register event handlers on `` elements, which do not allow you to directly embed XML event handlers.

```xml
<xf:model id="main-model">
    <!-- Child instance -->
   <xf:instance id="child-instance" src="my-instance.xml"/>
    <!-- Register the event handler on the child instance -->
    <xf:dispatch ev:observer="child-instance" ev:event="xforms-insert" targetid="main-model" name="update-after-insert"/>
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
            <xf:action ev:observer="my-group" ev:target="my-input" ev:event="DOMFocusIn">
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
            <xf:action ev:observer="my-model" ev:event="xforms-ready">
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

Since late January 2010 builds, you can place event handlers at the top-level under the `<xh:body>` element:

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
    <xf:setvalue ev:event="my-event" ev:observer="my-input" ref="my-value">43</xf:setvalue>
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
    <xbl:handler event="xforms-focus" phase="target" defaultAction="cancel">
        <xxf:script id="xf-sf">YAHOO.xbl.fr.Currency.instance(this).setfocus();</xxf:script>
    </xbl:handler>
</xbl:handlers>
```

### The ev:handler attribute

[TODO: not supported by Orbeon Forms as of 2010-01]

### The  element

[TODO: not supported by Orbeon Forms as of 2010-01]

### The keypress event

You can, by listening to the `keypress` event, run actions as users type a certain key combination. Your listener can be registered on:

* **The whole document**, in which case it will run whenever users press the key combination you specified. You can register a listener on the whole document either by declaring you listener directly under the `xh:body` as in:

```xml
<xh:body>
    <xf:action ev:event="keypress" xxf:modifiers="Control" xxf:text="y">
        ...
    </xf:action>
    ...
</xh:body>
```

Or you can declare it anywhere in your form with an observer set to `#document`, as in:

```xml
<xf:action ev:event="keypress" ev:observer="#document" 
        xxf:modifiers="Control" xxf:text="y">
    ...
</xf:action>
```

* **Part of the document**, in which case you set your actions to listen on a XForms control such as a `xf:group` or an `xf:input`. Note that in this case, your listener will be called only if a form control (either the one you have specified, or form control inside the one you have specified for container form controls) has the focus when users press the key combination.
* **A dialog**, in which case your listener will be called only when users press the key combination while the dialog is open. In this case, the only requirement for the listener to be called is for the dialog to be open; the focus does not necessarily need to be on a form control inside the dialog.

You specify what key stroke you want to listen to with the following two attributes:

* `xxf:text` specifies the key you want to listen to. This attribute is mandatory: if you have a `ev:event="keypress"` on an action, then you need to specify an `xxf:text`.
* `xxf:modifier` specifies what key modifier needs to be pressed in addition to the key. This is a space separated list of values, where the values can be `Control`, `Shift`, and `Alt`. This attribute is optional: leave it out to listener to a key press with no modifier.
