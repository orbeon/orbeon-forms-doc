> [[Home]] ▸ [[XBL|XForms ~ XBL]]

## Event propagation

XBL promotes a strong encapsulation of data and behavior. In particular events which target elements within the component typically are invisible to the component user:

- Events flow along XBL scopes:
    - An event flowing *inside* the component is not visible from outside the component.
    - An event flowing *outside* the component, including within content copied with `xbl:content` is not visible from inside the component.
- For special use cases like `fr:error-summary`, _[phantom handlers][3]_ are introduced.
- `DOMFocusIn` and `DOMFocusOut` follow the behavior outlined in [XForms - Focus][4].

## Component user: attaching event handlers to the bound node

By default, you get the same behavior you have with built-in controls, like `xf:input`.

Event handlers can directly observe elements with XBL bindings:

```xml
<foo:bar>
    <xf:action ev:event="xforms-value-changed">
        ...
    </xf:action>
</foo:bar>
```

The action handler above observes events going through element `<foo:bar>`, assuming `<foo:bar>` has an XBL binding.

Similarly, the following observes events targeted directly to `<foo:bar>`:

```xml
<foo:bar>
    <xf:action ev:event="xforms-enabled" ev:target="#observer">
        ...
    </xf:action>
</foo:bar>
```

Note that to achieve this the XBL engine does some special work here:

- it recognizes those nested handlers
- it attaches them to the bound node
- it hides them from the component's `xbl:content` processing

_NOTE: However these handlers are still visible from XSLT processing when using `xxbl:transform`_

You can disable this automatic processing of nested event handlers, although you should only need this for very special components:

```xml
<xbl:binding id="fr-foo" element="fr|foo" xxbl:mode="nohandlers">
```

It is also possible to attach handlers by id, like with any XForms control:

```xml
<foo:bar id="my-foobar">
    ...
</foo:bar>
...
<xf:action ev:event="xforms-value-changed" ev:observer="my-foobar">
    ...
</xf:action>
```

_NOTE: Only standard XForms controls and elements which have an XML binding can be used as event observers. Other elements, such as an HTML `<div>`, cannot be event observers._

## Component user: nested content under the bound node

Some components, such as a tabview, in effect behave like XForms grouping controls (like `xf:group`, `xf:switch/case`, `xf:repeat`). With such components, a lot of content is typically nested under the bound node:

```xml
<fr:tab>
    <xh:div>
        <xf:group>
            <xf:action ev:event="DOMActivate">
                ...
            </xf:action>
        </xf:group>
    </xh:div>
</fr:tab>
```

It is up to the component author to handle nested content properly. When using `xbl:content`, the XBL engine does the work for you:

* the nested content is automatically visible from the "outside"  of the component
    * ids and variables are visible across the bound node (here `fr:tab`)
* events flow nicely as the form author would expect when using a regular XForms grouping control

## Component author: hooking-up creation and destruction event handlers

You can use the `xforms-model-construct-done` event on local models. This event is dispatched when the component is being initialized. This event can be dispatched multiple times to a given component during a form's lifecycle, as the component is initialized each time it becomes relevant.

As is the case for top-level models, when `xforms-model-construct-done` is dispatched, UI controls are not present yet. So you cannot reach controls from action handlers responding to that event.

[UNTIL Orbeon Forms 4.9 included]

`xforms-ready` is not dispatched to local models. Here is how you can register handlers to perform initializations when the component is created and destroyed:

```xml
<xbl:template>
    <xf:group id="component-group">
        <xf:action ev:event="xforms-enabled" ev:target="component-group">
            <!-- Perform construction here -->
            <xxf:script>...</xxf:script>
        </xf:action>
        <xf:action ev:event="xforms-disabled" ev:target="component-group">
            <!-- Perform destruction here -->
            <xxf:script>...</xxf:script>
        </xf:action>
        ... Rest of component ...
    </xf:group>
</xbl:template>
```

Note the `ev:target` attributes, which ensure that only events actually targeting this particular group are handled. If you omit that attribute, you might observe more than one event for creation or destruction, which is in general not desired.

[SINCE Orbeon Forms 4.10]

`xforms-ready` is dispatched to XForms models nested within the component. Specifically, `xforms-ready` is dispatched when the component receives the `xforms-enabled` event during a refresh. This event can be dispatched multiple times to a given component during a form's lifecycle, as the component is initialized each time it becomes relevant.

As is the case for top-level models, when `xforms-ready` is dispatched, UI controls are present.

## Component author: dispatching events from within the component

This allows a component to send information to the outside world.

A component can dispatch events to its bound element by using `xf:dispatch` and using the id of the `xbl:binding` element as target.

Example:

```xml
<xbl:binding id="foobar-component" element="fr|foobar">
    <xbl:template>
        <!-- Local controls -->
        <xf:trigger id="internal-trigger-1">
            <xf:label>Dispatch outside</xf:label>
            <xf:dispatch
                    ev:event="DOMActivate"
                    name="my-event"
                    targetid="foobar-component">
                <xf:property name="my:answer" select="42"/>
            </xf:dispatch>
        </xf:trigger>
    </xbl:template>
</xbl:binding>
```

The component user can listen to this event as expected, for example:

```xml
<fr:foobar id="my-foobar">
    <xf:message ev:event="my-event">
        <xf:output value="concat('Got it: ', event('my:answer'))"/>
    </xf:message>
</fr:foobar>
```

The use of the `my:` prefix in the event context information is not mandatory, but if a prefixed is used a namespace mapping must be in scope. It is good practice to use a prefix so as to prevent name conflicts with standard XForms event context information.

## Component author: listening for events dispatched to the component

This allows a component to receive information from the outside world.

You can register event handler attached to the bound node inside your component with the `xbl:handlers/xbl:handler` elements:

```xml
<xbl:binding id="fr-bar" element="fr|bar">
    <xbl:handlers>
        <!-- Handlers are attached to the bound node -->
        <xbl:handler event="my-event" phase="target">
            <xf:setvalue model="model" ref="value1" value="event('fr:one')"/>
            <xf:setvalue model="model" ref="value2" value="event('fr:two')"/>
        </xbl:handler>
    </xbl:handlers>
```

The `xbl:handler` element looks very much like an `xf:action` element. In particular, it supports the following attributes:

* `event`: specifies which event(s) to listen to.
    *NOTE: Like for `ev:event`, Orbeon Forms supports as an extension a list of space-separated event names.*
* `phase`: whether to call the handler upon the `capture`, `target`, or `bubble `phase.

The `xbl:handler` element can contain one or more XForms actions.

## Component user: dispatching events to the component

The following example responds to a button being activated and dispatches an event with custom context information to an `fr:bar` component:

```xml
<fr:bar id="my-bar"/>

<xf:trigger>
    <xf:label>Insert</xf:label>
    <xf:dispatch ev:event="DOMActivate" name="my-event" targetid="my-bar">
        <xf:property name="fr:one" select="'Red'"/>
        <xf:property name="fr:two" select="'Blue'"/>
    </xf:dispatch>
</xf:trigger>
```

When the event `my-event` reaches the component, it activates the event handler registered with `xbl:handler`. That handler has access to the custom context information using the `event()` function.

Event handlers are attached to the bound node but they execute within the context of the component, which means that they have access to XForms elements declared within the component. This includes:

- `xf:model` elements declared within `xbl:implementation`
- `xf:model` elements declared within `xbl:template`
- controls declared within `xbl:template`

[3]: http://wiki.orbeon.com/forms/doc/developer-guide/xforms-events
[4]: http://wiki.orbeon.com/forms/doc/developer-guide/xforms-focus