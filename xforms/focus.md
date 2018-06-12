# Keyboard Focus



## What is the focus?

In user interfaces, the _focus_ usually refers to _keyboard focus._ A control is said to have focus when it currently receives keyboard input, whether for text entry or for manipulating the control itself (like using cursor keys to open a dropdown). Browsers usually indicate focus with a highlight:

![](images/xforms-focus-textarea.png)

XForms supports focus with a few constructs:

* the `<xf:setfocus>` action and the the `xforms-focus` event
* the `DOMFocusIn` and `DOMFocusOut` events

This page describes the behaviors associated with focus in the Orbeon Forms XForms engine.

## Focusable controls

In Orbeon Forms, the following controls accept focus from the user:

* `xf:input`
* `xf:secret`
* `xf:select`/ `xf:select1`
* `xf:trigger` / `xf:submit`
* `xf:textarea`
* `xf:upload`
* XBL controls with the `focus` mode

Focus can only happen on those controls under the following conditions:

* The control is relevant.
* The control is not read-only.
* The control is not hidden, namely.
    * It is not in a hidden case.
    * It is not in a closed dialog.

The following controls never accept focus:

* `xf:output`
* grouping controls
    * `xf:group`
    * `xf:switch` / `xf:case`
    * `xf:repeat`
    * `xxf:dialog`
    * XBL controls without the `focus` mode

_NOTE: While grouping controls never accept focus directly, XForms actions can be used to set focus to nested controls (see below)._

## User actions changing the focus

As a user, you can impact the focus in the following ways:

* by clicking inside a control (for example on an input field)
    * this removes focus from the previously focused control if any
    * then sets the focus to the clicked control
* by clicking outside of any controls
    * this removes focus from all controls
* by tabbing between fields, when enabled by the browser
    * this removes focus from the previously focused control
    * then sets the focus to the newly entered control
* by clicking outside of a control but within a repeat iteration
    * if focus was on a control within another repeat iteration, focus is moved to the new iteration's corresponding control
    * otherwise, focus is removed and the iteration becomes the new iteration

## XForms actions changing the focus

### xf:setfocus

#### Basic usage

The XForms [`<xf:setfocus>`][2] action allows the form author to change the focus to a particular control. It is similar to the HTML `focus()` function, with handling of grouping controls as a nice bonus feature.

Here is a basic use of the action:

```xml
<xf:input ref="name" id="name-input"/>

<xf:trigger>
    <xf:label>Focus</xf:label>
    <xf:setfocus event="DOMActivate" control="name-input"/>
</xf:trigger>
```

#### Grouping controls

You can also use the action on grouping controls, in which case it will set the focus to the first nested control able to accept focus:

```xml
<xf:group id="my-group">
    <xf:input ref="name" id="name-input"/>
    <xf:input ref="age" id="age-input"/>
</xf:group>

<xf:trigger>
    <xf:label>Focus</xf:label>
    <xf:setfocus event="DOMActivate" control="my-group"/>
</xf:trigger>
```

When the grouping control is a switch or a repeat, the focus naturally follows the selected case or the current repeat iteration, respectively. In this example, the focus will be set to the input control in the repeat iteration that matches the current repeat index:

```xml
<xf:repeat id="my-repeat" ref="person">
    <xf:input ref="name" id="name-input"/>
    <xf:input ref="age" id="age-input"/>
</xf:repeat>

<xf:trigger>
    <xf:label>Focus</xf:label>
    <xf:setfocus event="DOMActivate" control="my-repeat"/>
</xf:trigger>
```

#### Focusing only on xf:input

[DEPRECATED with Orbeon Forms 2016.3. Use `includes` and `excludes` instead.]

The `input-only` extension attribute, when set to `true`, changes the behavior of the action by allowing focus only on the `<xf:input>` control. This means that buttons (`<xf:trigger>`) in particular are excluded.

This is convenient in particular to focus on the first control supporting input under a grouping control:

```xml
<xf:setfocus 
    control="my-group" 
    input-only="true"/>
```

This way, the user can use the keyboard right away, yet the programmer does not have to be specific as to which control must receive focus.

The attribute is an AVT.

#### Includes and excludes

[SINCE Orbeon Forms 2016.3]

The `includes` and `excludes` attribute allow filtering which controls are allowed to receive focus when using `<xf:setfocus>`.

- `includes`
    - list of control QNames to include
    - include all controls if missing or blank
- `excludes`
    - list of control QNames to exclude
    - exclude no controls if missing or blank

These attributes deprecate `input-only="true"` which is equivalent to `includes="xf:input"`.

This allows focus on all controls except `<xf:trigger>`:

```xml
<xf:setfocus 
    control="my-group" 
    excludes="xf:trigger"/>
```

This allows focus on `<xf:input>` and `<xf:textarea>` only:

```xml
<xf:setfocus 
    control="my-group" 
    includes="xf:input xf:textarea"/>
```


### xf:setindex

XForms enforces that, within a repeat, only a control part of the repeat iteration that matches the current repeat index can have focus.

This means that:

1. if the user clicks on a control within a repeat iteration, the focus changes to that control and the current iteration is adjusted
2. if the current repeat iteration changes, and focus was on a control within another iteration, the focus as adjusted

When using the `<xf:setindex>` action, the following logic takes place if the index changes:

* if the control with focus is within the old iteration, focus is removed from that control
* if possible (based on relevance, read-only, and visibility), focus is set to the corresponding control in the new iteration

### xf:toggle and xxf:hide

Similarly to `<xf:setindex>`, XForms enforces that a control within a hidden case or dialog cannot have focus. If a focused control is within a case or dialog and the case is hidden via `<xf:toggle>` or the dialog closed via `<xxf:hide>`, focus is removed from that control.

### xxf:show

When a dialog is opened with `<xxf:show>`, and focus was on a control outside the dialog, focus is first removed.

Then, focus is set to the dialog control itself, which means that the first focusable control within the dialog gets focus. This behavior can be overridden, as described further below.

### xf:insert and xf:delete

The `<xf:insert>` and `<xf:delete>` actions can immediately:

* add or remove iterations
* change the current repeat index, depending on iterations added, removed, or moved

The following logic takes place:

* if the iteration has moved and the focused control is still focusable, keep the focus on that control
* if the iteration has moved and the focused control is no longer focusable, remove the focus
* if the iteration has been removed
    * remove focus from the control
    * try to find the corresponding control matching the current index, as for `<xf:setindex>`
        * if the control is found and focusable, focus on it

## Focus change during refresh

Lots of things can change during refresh:

* controls might become non-relevant
* repeat iterations might be added, removed, or move
* with the XForms 2 `@caseref` attribute, cases can become hidden

Just after refresh, the focus is adjusted accordingly:

* for repeats, the same logic that applies to the `<xf:insert>` and `<xf:delete>` actions takes place
* otherwise, if focus was on a control that has become non-relevant, read-only, or hidden, focus is removed

## Overriding the default focus behavior

### Overriding focus behavior in dialogs

When a dialog opens, you can explicitly focus on a given control upon receiving the `xxforms-dialog-open` event. In this example, the form author sets focus on `age-input` instead of `name-input` (which would receive focus by default as it is the first control):

```xml
<xxf:dialog id="my-dialog">
    
    <xf:setfocus event="xxforms-dialog-open" control="age-input"/>
    
    <xf:input ref="name" id="name-input"/>
    <xf:input ref="age" id="age-input"/>

</xxf:dialog>
```

### Overriding focus behavior in XBL components

You can override an XBL component's default focus behavior with an event handler. For example, to run JavaScript upon focus:

```xml
<xbl:handler event="xforms-focus" phase="target" defaultAction="cancel">
    <xxf:script id="xf-sf">YAHOO.xbl.fr.Currency.instance(this).setfocus();</xxf:script>
</xbl:handler>
```

Or, to focus to a specific nested control:

```xml
<xbl:handler event="xforms-focus" phase="target" defaultAction="cancel">
    <!-- Override default behavior and focus on search input -->
    <xf:setfocus control="search"/>
</xbl:handler>
```

In both cases, `defaultAction="cancel"` ensures that after the overridden behavior has completed, the default algorithm above does not take place.

## The DOMFocusIn and DOMFocusOut events

These two events can be used to detect whether a control has lost or gained focus. For example:

```xml
<xf:input ref="name" id="name-input">
    <xf:action event="DOMFocusIn">
        ...
    </xf:action>
</xf:input>
```

When the XForms engine determines that a control must receive focus, it starts a sequence of events, as follows:

* dispatch `DOMFocusOut` to the control that previously had focus, if any
* dispatch `DOMFocusOut` to the ancestor grouping controls of the control that previously had focus, if any
* dispatch `DOMFocusIn` to the ancestor grouping controls of the control that is gaining focus, if any
* dispatch `DOMFocusIn` to the control that is gaining focus, if any

These two events bubble, so you have to be careful with event listeners. Often, it is a good idea to use the `target` attribute on event handlers to make sure that focus changes are detected on the expected control:

```xml
<xf:group id="my-group">
    <!-- Runs when focus enters the group, but not when focus changes from
         name-input to age-input or back -->
    <xf:action event="DOMFocusIn" target="my-group">
        ...
    </xf:action>
    
    <xf:input ref="name" id="name-input"/>
    <xf:input ref="age" id="age-input"/>
    
</xf:group>
```

## The xforms-focus event

The `xforms-focus` event should usually be considered an internal event used by the `xf:setfocus` action. However, it is useful to know about this event in two situations:

* to prevent focus from taking place on a control or grouping control
* to override the focus behavior, as in the XBL component example described above

## Algorithms for the xf:setfocus action and the xforms-focus event  

* refresh models and view if needed
* resolve the target `control` attribute following [Resolving ID References in XForms][3]  
* dispatch the [`xforms-focus`][4] event to that control
* the control then searches for the actual control to focus, if any, following the algorithm below

Orbeon Forms implements the following XForms 1.1-compatible behavior when receiving [`xforms-focus`][4]:  

* For any control:
    * if the control is non-relevant, read-only, or not visible, processing of the event terminates and the focus is not changed
* If the target control is a core form control (such as `<xf:input>`):
    * focus is set to that control
* If the target control is one of the following container form control: `<xf:group>`, `<xf:case>`, `<xxf:dialog>`, or an XBL control:
    * children controls are recursively searched and the first descendant control able to receive focus receives focus  
    * if there is no such control, processing of the event terminates
* If the target control is the `<xf:switch>` control:  
    * the currently selected child `<xf:case>` control is recursively searched and the first descendant control able to receive focus receives it
    * If there is no such control, processing of the event terminates
* If the target control is the `<xf:repeat>` control:  
    * the repeat iteration corresponding to the current repeat index is recursively searched and the first descendant control able to receive focus receives it.
    * if there is no such control, or if there is no iteration, processing of the event terminates

_NOTE: the `xforms-focus` event does not bubble._

[2]: http://www.w3.org/TR/xforms11/#action-setfocus
[3]: http://www.w3.org/TR/xforms11/#idref-resolve
[4]: http://www.w3.org/TR/xforms11/#evt-focus
