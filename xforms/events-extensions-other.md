# Other event extensions

## Creating keyboard shortcuts with the keypress event

### Support events

- UNTIL Orbeon Forms 2018.2
    - `keypress`
- SINCE Orbeon Forms 2019.1
    - `keypress`, `keydown` and `keyup`

### Basic usage

You can, by listening to the `keypress`, `keydown` and `keyup` events, run actions as users type a certain key combination. Your listener can be registered on:

- __The whole document__, in which case it will run whenever users press the key combination you specified. You can register a listener on the whole document either by declaring you listener directly under the `xh:body` as in:

    ```xml
    <xh:body>
        <xf:action 
            event="keydown" 
            xxf:modifiers="ctrl" 
            xxf:text="y">
            ...
        </xf:action>
        ...
    </xh:body>
    ```
    
    Or you can declare it anywhere in your form with an observer set to `#document`, as in:
    
    ```xml
    <xf:action 
        event="keydown" 
        observer="#document" 
        xxf:modifiers="ctrl"
        xxf:text="y">
        ...
    </xf:action>
    ```
- __Part of the document__, in which case you set your actions to listen on a XForms control such as a `xf:group` or an `xf:input`. Note that in this case, your listener will be called only if a form control (either the one you have specified, or form control inside the one you have specified for container form controls) has the focus when users press the key combination.
- __A dialog__, in which case your listener will be called only when users press the key combination while the dialog is open. In this case, the only requirement for the listener to be called is for the dialog to be open; the focus does not necessarily need to be on a form control inside the dialog.

You specify what key stroke(s) you want to listen to with the following two attributes:

- `xxf:text`:
    - mandatory for `keypress`, `keydown` and `keyup` handlers
    - the key you want to listen to
    - SINCE Orbeon Forms 2019.1
        - special keys are supported: `backspace`, `tab`, `enter`, `return`, `capslock`, `esc`, `escape`, `space`, `pageup`, `pagedown`, `end`, `home`, `left`, `up`, `right`, `down`, `ins`, `del`, `plus`
- `xxf:modifier`
    - optional
    - space-separated list of key modifiers that need to be pressed in addition to the key
    - UNTIL Orbeon Forms 2018.2
        - values can be `Control`, `Shift`, and `Alt`
    - SINCE Orbeon Forms 2019.1
        - values are no longer case-sensitive, but lowercase is recommended
        - `shift`, `ctrl` (`control` is supported for backward compatibility), `alt`/`option`, `meta`/`command`
        
*NOTE: When using modifiers, the event name should be `keydown`. For backward compatibility, `keypress` is still supported but translated to `keydown`.* 
        
## Filtering on the event phase

XML Events 1 only supports filtering event handlers on a subset of the DOM Level 2 Events phases. Orbeon Forms extends that behavior and supports registering handlers that match on one of the 3 main event phases specified by DOM Level 2 Events: `capture`, `target`, and `bubbling`.

Orbeon Forms supports the following values for the `phase` attribute:  

* `capture`: only activate the handler during the capture phase (this is compatible with all the specifications)
* `default` or unspecified: only activate the handler during the target or bubbling phase (this is compatible with XML Events 1 but not included in the current XBL 2 proposal)
* `target`: only activate the handler during the target phase (this is not present in XML Events 1)  
* `bubbling`: only activate the handler during the bubbling phase (this is not present in XML Events 1)

_NOTE: In the future, the XBL 2 default action phase could be integrated if considered desirable. It is hoped that the `target` and `bubbling` values will be supported in XML Events 2._

_NOTE: In most cases, the `phase` attribute can be omitted, in which case the `target` or `bubbling` phase is matched. This allows placing a handler directly within a target element, or any of its ancestors, which are the most common use cases in XForms._  

## Adding context information to events 

Orbeon Forms supports passing event context attributes with the XForms 2.0 `<xf:property>` child element. The actions supported are actions which directly cause an event to be dispatched:

* `<xf:dispatch>`
* `<xf:send>`
* `<xxf:show>`
* `<xxf:hide>`

Here is how you pass context attributes when executing an action:

```xml
<xf:dispatch name="rename-control" target="my-model">
    <xf:property name="control"      value="my/control"/>
    <xf:property name="control-name" value="'beverage-selection'"/>
</xf:dispatch>
```

*NOTE: Prior to standard XForms support, the `<xxf:context>` child element was introduced for the same purpose and can still be used for backward compatibility. The `select` attribute can also be used in place of `value`.*

`<xf:property>` supports the following two attributes:

||||
|---|---|---|
| `name` |  Mandatory | Name of the context attribute. |
| `value` |  Mandatory |  XPath 2.0 expression determining the value of the context attribute.  |

Note that the context attribute name cannot be a qualified name (QName), because this would not be compatible with [DOM 2 Events][1]. However, a QName can be used as custom event name.

In order to avoid confusion with standard XForms names, we recommend you use prefixed names if you use custom context information with standard event names (when supported). However, with custom event names, prefixing is not necessary.

Context attributes passed this way can be retrieved using the `event()` function:

```xml
<xf:action event="rename-control">
    <xf:setvalue ref="event('control')/@name" value="event('control-name')"/>
</xf:action>
```
 
_NOTE: At the moment, with, `<xf:dispatch>`, only custom events support passing context attributes this way. Built-in events, such as `xforms-value-changed`, or `DOMActivate`, ignore nested `<xf:property>` elements._

## Allowing duplicate event in the event queue
 
[SINCE Orbeon Forms 2017.1]

Prior to Orbeon Forms 2017.1, the `<xf:dispatch>` action used with a `delay` value would always add all events to the event queue. This was not per the XForms specification, which says that events with the same name and effective target as ones already in the event queue are skipped.
 
Orbeon Forms 2017.1 rectifies that to follow XForms and skips duplicates. This change of default is usually not a problem.

To allow duplicates, the `xxf:allow-duplicates="true"` attribute is supported:

```xml
<xf:dispatch
    name="my-event"
    targetid="my-targetid"
    delay="10"
    xxf:allow-duplicates="true"/>
```

`xxf:allow-duplicates` is an AVT.

## Enhanced support for xforms-select and xforms-deselect

[_TODO: describe support for these events on xf:upload]_  

## Targeting effective controls within repeat iterations

The following actions all support attributes resolving to a particular control:

* `<xf:dispatch>` (`target` attribute)
* `<xf:setfocus>` (`control` attribute)
* `<xf:toggle>` (`case` attribute)
* `<xxf:show>` (`neighbor` attribute)

When that control is within a repeat iteration, the actual control targetted is chosen based on the current set of repeat indexes. However, in some cases, it is useful to be able to target the control within a particular iteration. This is achieved with the `xxf:repeat-indexes` extension attribute on these actions. This attribute takes a space-separated list of repeat indexes, starting with the outermost repeat. Example:

```xml
<!-- Repeat hierarchy -->
<xf:repeat ref="todo-list">
    <xf:repeat ref="todo-item">
        <xf:switch>
             <xf:case id="edit-case">...</xf:case>
             <xf:case id="view-case">...</xf:case>
        </xf:switch>
    </xf:repeat>
</xf:repeat>

<xf:trigger>
    <xf:label>Toggle Me!</xf:label>
    <!-- Toggle the case within the 5th todo item of the 3rd todo list -->
    <xf:toggle event="DOMActivate" case="edit-case" xxf:repeat-indexes="3 5"/>
</xf:trigger>
```

## Multiple event names, observers and targets on event handlers

The `event`, `observer` and `target` attributes, defined by the [XML Events specification][2], only support one event name, observer, or target respectively. Orbeon Forms supports as an extension a list of space-separated values. The behavior is as follows:

* For `event`: the handler is called if any of the specified events matches.

    ```xml
    <xf:action event="DOMFocusIn DOMFocusOut">
        <!-- Reacting to either the "DOMFocusIn" and "DOMFocusOut" events -->
        ...
    </xf:action>
    ```

* For `observer`: the event handler is attached to all the observers specified.

    ```xml
    <xf:action event="DOMActivate" observer="my-input my-trigger">
        <!-- Observing both the "my-input" and "my-trigger" controls -->
        ...
    </xf:action>
    ```

* For `target`: the handler is called if any of the specified targets matches.

    ```xml
    <xf:action event="xforms-submit-done" target="create-submission update-submission">
        <!-- Checking that either the "create-submission" and "update-submission" controls is a target -->
        ...
    </xf:action>
    ```

The extensions above have been [requested][8] for inclusion in XML Events 2.

## Catching all events  

The special `#all` event name on `event` can be used to catch all events:

```xml
<xf:group>
    <!-- Stop propagation of all events -->
    <xf:action event="#all" propagate="stop"/>
    ...
</xf:group>
```

## Specifying the current observer as target restriction

The special `#observer` target name on `target` can be used to specify that the listener must be activated only if the event target is the listener's observer:

```xml
<xf:group>
    <!-- Restrict activation to events dispatched to the group -->
    <xf:action event="my-event" target="#observer"/>
    ...
</xf:group>
```

In this example, this is identical to:`  

```xml
<xf:group>
    <!-- Restrict activation to events dispatched to the group -->
    <xf:action event="my-event" phase="target"/>
    ...
</xf:group>
```

## Observing the preceding sibling element

The `observer` attribute can be set to the value `#preceding-sibling`:

```xml
<xf:repeat ref="...">
    ...
</xf:repeat

<xf:action
  event="xforms-enabled xforms-disabled xxforms-index-changed xxforms-ref-changed"
  observer="#preceding-sibling"
  target="#observer">
    
</xf:action>
```

In this example, we observe events targeted to the repeat element just before the event handler.

This is useful in situations where it is not possible to explicitly set an id on a control, for example when doing complex things with XBL.

## Phantom handlers

Event handler support the `xxf:phantom="true"` attribute to specify that the event handler is listening to events flowing across XBL scopes.

XForms events flow along XBL boundaries and are fully encapsulated. This attribute allows special consumers of events to have a global view of events flowing in the XForms page. Example:

```xml
<xf:action
  observer="my-observer"
  event="xforms-enabled"
  xxf:phantom="true">
```

In this example, the handler will catch events that happen not only in the current XBL scope, but also those in nested XBL scopes.

This is an advanced feature and should be used wisely. It is used in Orbeon Forms by the error summary (`fr:error-summary`) and by Form Builder.

[1]: https://www.w3.org/TR/DOM-Level-2-Events/events.html
[2]: https://www.w3.org/TR/2010/NOTE-xml-events2-20101216/
[3]: https://www.w3.org/TR/DOM-Level-3-Events/
[4]: https://www.w3.org/TR/xbl/
[5]: https://www.w3.org/TR/xbl/#the-handler
[6]: https://www.w3.org/TR/2003/NOTE-DOM-Level-3-Events-20031107/keyset.html
[8]: https://lists.w3.org/Archives/Public/www-html-editor/2008JanMar/0012.html
