# XForms Events

<!-- toc -->

## Standard event context information

*TODO*

## Extension event context information

### All events

Orbeon Forms enhances the XML Events `event()` function to take a qualified name as parameter:

```ruby
event($attribute-name as xs:QName) item()*
```

This allows namespacing attribute names, therefore better allowing for extension attributes.

On all events, the following extension attributes are supported:

- `event('xxf:type') as xs:string`
  Return the event type (also known as event name), for example `DOMActivate`.
- `event('xxf:targetid') as xs:string`
  Return the static id of the event target (`event('xxf:target')` is supported for backward compatibility).
- `event('xxf:absolute-targetid') as xs:string`
  *[SINCE 2012-07-10 / Orbeon Forms 4.0]*
  Return the absolute id of the event target.
- `event('xxf:observerid') as xs:string`
  *[SINCE 2012-05-18 / Orbeon Forms 4.0]*
  Return the static id of the event observer.
- `event('xxf:absolute-observerid') as xs:string`
  *[SINCE 2012-07-10 / Orbeon Forms 4.0]*
  Return the absolute id of the event observer.
- `event('xxf:bubbles') as xs:boolean`
  Return whether the event is allowed to bubble or not.
- `event('xxf:cancelable') as xs:boolean`
  Return whether the event is cancelable or not.
- `event('xxf:phase') as xs:string`
  The current event phase: capture, target, or bubbling.
- `event('xxf:repeat-indexes') as xs:string*`
  Return the event target's current repeat indexes, if any, starting from the ancestor repeat.
- `event('xxf:repeat-ancestors') as xs:string*`
  Return the event target's ancestor repeat ids, if any.
- `event('xxf:target-prefixes') as xs:string*`
  Return the event target's id prefixes, if any, starting from the ancestor components. This will be empty unless the target is within an XBL component.

### UI events

These are:

- `DOMActivate`
- `DOMFocusIn`
- `DOMFocusOut`
- `xforms-select`
- `xforms-deselect`
- `xforms-enabled`
- `xforms-disabled`
- `xforms-help`
- `xforms-hint`
- `xforms-valid`
- `xforms-invalid`
- `xforms-required`
- `xforms-optional`
- `xforms-readonly`
- `xforms-readwrite`
- `xforms-value-changed`

The following extension attributes are supported:

- `event('xxf:control-position') as xs:integer`
  Return the event target's position in the user interface. This is the control's static position, i.e. this does not reflect possible repeat iterations.
- `event('xxf:binding') as node()?`
  Return the event target's single-node binding if any.
- `event('xxf:label') as xs:string?`
  Return the event target's label value if any.
- `event('xxf:hint') as xs:string?`
  Return the event target's hint value if any.
- `event('xxf:help') as xs:string?`
  Return the event target's help value if any.
- `event('xxf:alert') as xs:string?`
  Return the event target's alert value if any.

On `xforms-value-changed`, the following extension attributes are supported:

- `event('xxf:value')`
  *[SINCE Orbeon Forms 4.4]*
  The current value (that is, the value after the change) of the control.

On `xforms-select`, the following extension attributes are supported:

- `event('xxf:item-value')`
  When this event is dispatched to in response to a selection control item being selected, returns the value of the selected item.

### Other events

On `xforms-submit-serialize`, the following extension attributes are supported:

- `event('xxf:binding') as node()?`
  Return the submission's single-node binding if any.
- `event('xxf:serialization') as xs:string`
  Return the submission's requested serialization, e.g. `application/xml`, `application/x-www-form-urlencoded`, etc..
