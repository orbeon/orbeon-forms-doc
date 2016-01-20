# Extension events

<!-- toc -->

## Introduction

This page documents extension events, that is events which are not part of the XForms specifications. Such events, by convention, start with the prefix `xxforms-`.

## xxforms-nodeset-changed

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

## xxforms-index-changed

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

## xxforms-iteration-moved

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

## xxforms-value-changed

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

## xxforms-valid

Dispatched in response to: instance being valid after validation

Target: instance  
Bubbles: Yes  
Cancelable: Yes  
Context Info: none

The `xxforms-valid` event is dispatched to an instance after validation if it is valid.  

## xxforms-invalid

Dispatched in response to: instance being invalid after validation

Target: instance  
Bubbles: Yes  
Cancelable: Yes  
Context Info: none

The `xxforms-invalid` event is dispatched to an instance after validation if it is invalid.  

## xxforms-dialog-open

Dispatched in response to: `` action

Target: dialog  
Bubbles: Yes  
Cancelable: Yes  
Context Info: none

The `xxforms-dialog-open` event is dispatched to an dialog in response to running the  action targeting that dialog.  

## xxforms-dialog-close

Dispatched in response to: `` action

Target: dialog  
Bubbles: Yes  
Cancelable: Yes  
Context Info: none

The `xxforms-dialog-close` event is dispatched to an dialog in response to:

* running the `<xxf:hide>` action targeting that dialog
* the user closing the dialog with the dialog close box, if present
