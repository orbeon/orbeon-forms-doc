# Events - Extensions

<!-- toc -->

## Orbeon Forms extensions   

### Creating keyboard shortcuts

In some cases, you want your form to react to users pressing certain key combination by running XForms actions. Typically, this allows you to define keyboard shortcuts for operations that would otherwise require using the mouse or require many keystrokes.

You declare a listener on a certain key combination with:

```xml
<xf:group id="my-group">
    <xf:action ev:event="keypress" xxf:modifiers="control" xxf:text="y">
        <!-- Runs when an element inside my-group has the focus, and ctrl-y is pressed -->
    </xf:action>    
</xf:group>
```

* The attributes `modifiers` and `text` are [borrowed from XBL][5] where they are defined on the `xbl:handler` element. In time they could become part of XML Events. The supported values of modifier are `shift`, `alt`, and `control` (from the [DOM Level 3 events][6] specification but in lowercase, to follow XBL 2).
* When you declare a listener on `keypress`, you must specify the key with `xxf:text` and can optionally indicate a modifier with `xxf:modifiers`.

A `keypress` listener can be active:  

* For the whole document, if it has `ev:observer="#document"` on the listener.  
* For a given dialog, if your listener is declared directly under the `` or has an `ev:observer` pointing to that dialog.
* For only a section of the document. For instance, in the above example the listener is active only if the key is pressed while a form element inside that group has the focus. In most cases, you'll rather want the listener to be active for the whole document or for a specific dialog.

### Filtering on the event phase

XML Events 1 only supports filtering event handlers on a subset of the DOM Level 2 Events phases. Orbeon Forms extends that behavior and supports registering handlers that match on one of the 3 main event phases specified by DOM Level 2 Events: `capture`, `target`, and `bubbling`.

Orbeon Forms supports the following values for the `ev:phase` attribute:  

* `capture`: only activate the handler during the capture phase (this is compatible with all the specifications)
* `default` or unspecified: only activate the handler during the target or bubbling phase (this is compatible with XML Events 1 but not included in the current XBL 2 proposal)
* `target`: only activate the handler during the target phase (this is not present in XML Events 1)  
* `bubbling`: only activate the handler during the bubbling phase (this is not present in XML Events 1)

_NOTE: In the future, the XBL 2 default action phase could be integrated if considered desirable. It is hoped that the `target` and `bubbling` values will be supported in XML Events 2._

_NOTE: In most cases, the `ev:phase` attribute can be omitted, in which case the `target` or `bubbling` phase is matched. This allows placing a handler directly within a target element, or any of its ancestors, which are the most common use cases in XForms._  

### Enhanced event() function support

See [Extension event context information][7]

### Enhanced xf:dispatch support

Orbeon Forms supports passing event context attributes with the `` child element. The actions supported are actions which directly cause an event to be dispatched:

* `<xf:dispatch>`
* `<xf:send>`
* `<xxf:show>`
* `<xxf:hide>`

Here is how you pass context attributes when executing an action:

```xml
<xf:dispatch name="rename-control" target="my-model">
    <xxf:context name="control" select="my/control"/>
    <xxf:context name="control-name" select="'beverage-selection'"/>
</xf:dispatch>
```

`<xxf:context>` supports the following two attributes:

|||
|---|---|
| `name` |  Mandatory | Name of the context attribute. |
| `select` |  Mandatory |  XPath 2.0 expression determining the value of the context attribute.  |

Note that the context attribute name cannot be a qualified name (QName), because this would not be compatible with [DOM 2 Events][1]. However, a QName can be used as custom event name.

In order to avoid confusion with standard XForms names, we recommend you use prefixed names if you use custom context information with standard event names (when supported). However, with custom event names, prefixing is not necessary.

Context attributes passed this way can be retrieved using the `event()` function:

```xml
<xf:action ev:event="rename-control">
    <xf:setvalue ref="event('control')/@name" value="event('control-name')"/>
</xf:action>
```
 
_NOTE: At the moment, with, `<xf:dispatch>`, only custom events support passing context attributes this way. Built-in events, such as `xforms-value-changed`, or `DOMActivate`, ignore nested `<xxf:context>` elements._

### Enhanced support for xforms-select and xforms-deselect

[_TODO: describe support for these events on xf:upload]_  

### Targeting effective controls within repeat iterations

The following actions all support attributes resolving to a particular control:

* `<xf:dispatch>` (`target` attribute)
* `<xf:setfocus>` (`control` attribute)
* `<xf:toggle>` (`case` attribute)
* `<xxf:show>` (`neighbor` attribute)

When that control is within a repeat iteration, the actual control targetted is chosen based on the current set of repeat indexes. However, in some cases, it is useful to be able to target the control within a particular iteration. This is achieved with the `xxf:repeat-indexes` extension attribute on these actions. This attribute takes a space-separated list of repeat indexes, starting with the outermost repeat. Example:

```xml
<!-- Repeat hierarchy -->
<xf:repeat nodeset="todo-list">
    <xf:repeat nodeset="todo-item">
        <xf:switch>
             <xf:case id="edit-case">...</xf:case>
             <xf:case id="view-case">...</xf:case>
        </xf:switch>
    </xf:repeat>
</xf:repeat>

<xf:trigger>
    <xf:label>Toggle Me!</xf:label>
    <!-- Toggle the case within the 5th todo item of the 3rd todo list -->
    <xf:toggle ev:event="DOMActivate" case="edit-case" xxf:repeat-indexes="3 5"/>
</xf:trigger>
```

### Multiple event names, observers and targets on event handlers

The `ev:event`, `ev:observer` and `ev:target` attributes, defined by the [XML Events specification][2], only support one event name, observer, or target respectively. Orbeon Forms supports as an extension a list of space-separated values. The behavior is as follows:

* For `ev:event`: the handler is called if any of the specified events matches.

    ```xml
    <xf:action ev:event="DOMFocusIn DOMFocusOut">
        <!-- Reacting to either the "DOMFocusIn" and "DOMFocusOut" events -->
        ...
    </xf:action>
    ```

* For `ev:observer`: the event handler is attached to all the observers specified.

    ```xml
    <xf:action ev:event="DOMActivate" ev:observer="my-input my-trigger">
        <!-- Observing both the "my-input" and "my-trigger" controls -->
        ...
    </xf:action>
    ```

* For `ev:target`: the handler is called if any of the specified targets matches.

    ```xml
    <xf:action ev:event="xforms-submit-done" ev:target="create-submission update-submission">
        <!-- Checking that either the "create-submission" and "update-submission" controls is a target -->
        ...
    </xf:action>
    ```

The extensions above have been [requested][8] for inclusion in XML Events 2.

### Catching all events  

The special `#all` event name on `ev:event` can be used to catch all events:

```xml
<xf:group>
    <!-- Stop propagation of all events -->
    <xf:action ev:event="#all" ev:propagate="stop"/>
    ...
</xf:group>
```

### Specifying the current observer as target restriction

The special `#observer` target name on `ev:target` can be used to specify that the listener must be activated only if the event target is the listener's observer:

```xml
<xf:group>
    <!-- Restrict activation to events dispatched to the group -->
    <xf:action ev:event="my-event" ev:target="#observer"/>
    ...
</xf:group>
```

In this example, this is identical to:`  

```xml
<xf:group>
    <!-- Restrict activation to events dispatched to the group -->
    <xf:action ev:event="my-event" ev:phase="target"/>
    ...
</xf:group>
```

### Observing the preceding sibling element

The `ev:observer` attribute can be set to the value `#preceding-sibling`:

```xml
<xf:repeat ref="...">
    ...
</xf:repeat

<xf:action
  ev:event="xforms-enabled xforms-disabled xxforms-index-changed xxforms-nodeset-changed"
  ev:observer="#preceding-sibling"
  ev:target="#observer">
    
</xf:action>
```

In this example, we observe events targeted to the repeat element just before the event handler.

This is useful in situations where it is not possible to explicitly set an id on a control, for example when doing complex things with XBL.

### Phantom handlers

Event handler support the `xxf:phantom="true"` attribute to specify that the event handler is listening to events flowing across XBL scopes.

XForms events flow along XBL boundaries and are fully encapsulated. This attribute allows special consumers of events to have a global view of events flowing in the XForms page. Example:

```xml
<xf:action
  ev:observer="my-observer"
  ev:event="xforms-enabled"
  xxf:phantom="true">
```

In this example, the handler will catch events that happen not only in the current XBL scope, but also those in nested XBL scopes.

This is an advanced feature and should be used wisely. It is used in Orbeon Forms by the error summary (`fr:error-summary`) and by Form Builder.

## Orbeon Forms extension events

### xxforms-nodeset-changed

Dispatched in response to: node-set changed on `<xf:repeat>`  
Target: `<xf:repeat>` element  
Bubbles: Yes  
Cancelable: Yes  
Context Info:  

* `event('xxf:from-positions') as xs:integer*`
    * previous positions of all the iterations that moved
* `event('xxf:to-positions') as xs:integer*`
    * new positions of all the iterations that moved, in an order matching `event('xxf:from-positions')`
* `event('xxf:new-positions') as xs:integer*`
    * positions of all newly created iterations

The `xxforms-nodeset-changed` event allows you to detect changes to a repeat node-set:

* Nodes added to the nodeset, unless the nodeset was empty  
* Nodes reordered

This event is not dispatched when the repeat control becomes relevant or non-relevant. (A repeat control is considered non-relevant if its node-set is empty.)

Example:

```xml
<xf:group>
    <xxf:script ev:target="my-repeat" ev:event="xxforms-nodeset-changed xforms-enabled xforms-disabled">
        alert("Nodeset changed!");
    </xxf:script>
    <xf:repeat nodeset="record" id="my-repeat">
        ...
    </xf:repeat>
</xf:group>
```

In this example:

* The `ev:target` attribute ensures that this particular handler only catches events for `my-repeat`, in case there are some nested repeats or some other repeats within the group.
* The `ev:event` attribute lists not only `xxforms-nodeset-changed` event, but also the `xforms-enabled` and `xforms-disabled` event so the event runs when the nodeset goes from empty to non-empty or from non-empty to empty.

We recommend that you put the handler for `xxforms-nodeset-changed` outside the `` element, as shown in the example above. This ensures that, in case the repeat node-set becomes empty, actions associated with your event handler will still execute within a non-empty XPath context.

If nodes related to a repeat are inserted with `xf:insert` or `xf:delete` (including instance replacement upon submission), you could detect changes to the repeat node-set with XForms 1.1 using `xforms-insert` and `xforms-delete` events on instances. However these events are harder to use in this scenario, and will not catch situations where the repeat nodeset changes without insertions / deletions.  

Currently, we interpret handlers placed directly within `<``xf:repeat``>` as being attached to a particular repeat **iteration**, not to the repeat element itself. This means you can write things like:

```xml
<xf:repeat nodeset="value" id="my-repeat">
    <xf:action ev:event="my-event">
        <xxf:variable name="position" select="position()"/>
        <xf:setvalue ref="." value="$position"/>
    </xf:action>
    ...
</xf:repeat>
```

The context size is the size of the repeat nodeset, and the context position that of the current iteration. Things work as if within the repeat you had an implicit group, which is in fact now how XForms 1.1 specifies repeat.

Now the question is: what happens if you dispatch an event to `<``xf:repeat``>` itself?  

We propose the current solution:

* if an event targets `<``xf:repeat``>`, then instead we dispatch it to the current repeat iteration, setting the appropriate XPath context for handlers associated with the iteration  
* in the case where there is no repeat iteration (empty repeat nodeset), the XPath context becomes empty

### xxforms-index-changed

Dispatched in response to: index changed on `<xf:repeat>`  
Target: `<xf:repeat>` element  
Bubbles: Yes  
Cancelable: Yes  
Context Info:  

* `event('xxf:old-index') as xs:integer`: previous index
* `event('xxf:new-index') as xs:integer`: new index

The `xxforms-index-changed` event allows you to detect changes to a repeat index.

```xml
<xf:repeat nodeset="value" id="my-repeat">
    <xf:action ev:event="my-event">
        <xxf:variable name="position" select="position()"/>
        <xf:setvalue ref="." value="$position"/>
    </xf:action>
    ...
</xf:repeat>
```

The `xxforms-index-changed` event is not dispatched during control creation, only when the index changes. In order to obtain the index during creation, you can attach a listener for `xforms-enabled` to the `<``xf:repeat``>` element and use the `index()` or `xxf:index()` function to obtain that repeat's initial index:

```xml
<xf:group>
    <xf:repeat nodeset="item" id="my-repeat">
        <!-- Test handler with ev:target="#observer" -->
        <xf:action ev:event="xforms-enabled" ev:target="#observer">
            ... use index('my-repeat') or xxf:index() here ...
        </xf:action>
    </xf:repeat>
    <!-- Test handler with ev:target -->
    <xf:action ev:event="xforms-enabled" ev:target="my-repeat">
        ... use index('my-repeat') here ...
    </xf:action>
</xf:group>
```

### xxforms-iteration-moved

Dispatched in response to: iteration containing the control has changed since the last refresh or the time the iteration was first created

Target: control element  
Bubbles: No  
Cancelable: Yes  
Context Info: none

The `xxforms-iteration-moved` event is dispatched during refresh, just after `xforms-value-changed` (if dispatched).

This event is not dispatched when the repeat control becomes relevant or non-relevant.

_NOTE: A repeat control is considered non-relevant if its node-set is empty._

The iteration in which a control is present can change when repeat node-sets change as a consequence of inserted nodes, for example.

This event is useful for example to run `` actions to update client-side data in response to moved iterations. Here is an example from the `` implementation:

```xml
<xf:input ref="$result">
    <xxf:script id="xf-ch" ev:event="xforms-value-changed xxforms-iteration-moved">
        YAHOO.xbl.fr.Currency.instance(this).update();
     </xxf:script>
    <xxf:script id="xf-ro" ev:event="xforms-readonly">YAHOO.xbl.fr.Currency.instance(this).readonly();</xxf:script>
    <xxf:script id="xf-rw" ev:event="xforms-readwrite">YAHOO.xbl.fr.Currency.instance(this).readwrite();</xxf:script>
</xf:input>
```

_NOTE: This event doesn't bubble, so event listeners must directly observe the controls receiving the event._

### xxforms-value-changed

Dispatched in response to: value changed in an instance

Target: instance  
Bubbles: Yes  
Cancelable: Yes

Context Info:

* `event('node') as node()`: element or attribute node whose value has changed
* `event('old-value') as xs:string`: previous value
* `event('new-value') as `xs:string: new value

The `xxforms-value-changed` event is dispatched to an instance when an element or attribute value is changed in that instance, namely through the following mechanisms:

* `calculate` or `xxf:default` MIP
* `<xf:setvalue>` action
* value of a bound control changed by the user
* submission result with `replace="text"`

Example:

```xml
<html xmlns:xforms="http://www.w3.org/2002/xforms"
      xmlns:ev="http://www.w3.org/2001/xml-events"
      xmlns="http://www.w3.org/1999/xhtml">

    <head>
        <xf:model>
            <xf:instance id="table">
                <table xmlns=""/>
            </xf:instance>
        </xf:model>
    </head>
    <body>
       <xf:message ev:event="xxforms-value-changed" ev:observer="table">Changed!</xf:message>
        <xf:input ref="instance()">
            <xf:label>Change me:</xf:label>
        </xf:input>
    </body>
</html>
```

### xxforms-valid

Dispatched in response to: instance being valid after validation

Target: instance  
Bubbles: Yes  
Cancelable: Yes  
Context Info: none

The `xxforms-valid` event is dispatched to an instance after validation if it is valid.  

### xxforms-invalid

Dispatched in response to: instance being invalid after validation

Target: instance  
Bubbles: Yes  
Cancelable: Yes  
Context Info: none

The `xxforms-invalid` event is dispatched to an instance after validation if it is invalid.  

### xxforms-dialog-open

Dispatched in response to: `` action

Target: dialog  
Bubbles: Yes  
Cancelable: Yes  
Context Info: none

The `xxforms-dialog-open` event is dispatched to an dialog in response to running the  action targeting that dialog.  

### xxforms-dialog-close

Dispatched in response to: `` action

Target: dialog  
Bubbles: Yes  
Cancelable: Yes  
Context Info: none

The `xxforms-dialog-close` event is dispatched to an dialog in response to:

* running the `<xxf:hide>` action targeting that dialog
* the user closing the dialog with the dialog close box, if present

[1]: https://www.w3.org/TR/DOM-Level-2-Events/events.html
[2]: https://www.w3.org/TR/2010/NOTE-xml-events2-20101216/
[3]: https://www.w3.org/TR/DOM-Level-3-Events/
[4]: https://www.w3.org/TR/xbl/
[5]: https://www.w3.org/TR/xbl/#the-handler
[6]: https://www.w3.org/TR/2003/NOTE-DOM-Level-3-Events-20031107/keyset.html
[7]: https://github.com/orbeon/orbeon-forms/wiki/XForms-~-Events
[8]: https://lists.w3.org/Archives/Public/www-html-editor/2008JanMar/0012.html


