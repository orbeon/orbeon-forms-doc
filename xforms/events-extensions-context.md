# Extension Event Context Information

<!-- toc -->

## Introduction

XForms specifies some context information for events. Orbeon Forms adds further context information to events.

## All events

Orbeon Forms enhances the XML Events `event()` function to take a qualified name as parameter:

```ruby
event($attribute-name as xs:QName) item()*
```

This allows namespacing attribute names, therefore better allowing for extension attributes.

On all events, the following extension attributes are supported:

| Context | Type | Description |
| --- | --- | --- |
| `xxf:type` | `xs:string` |  event type (also known as event name), for example `DOMActivate` |
| `xxf:targetid` | `xs:string` |  static id of the event target (`event('xxf:target')` is supported for backward compatibility) |
| `xxf:absolute-targetid` | `xs:string` |  absolute id of the event target |
| `xxf:observerid` | `xs:string` | static id of the event observer |
| `xxf:absolute-observerid` | `xs:string` | absolute id of the event observer |
| `xxf:bubbles` | `xs:boolean` | whether the event is allowed to bubble or not |
| `xxf:cancelable` | `xs:boolean` | whether the event is cancelable or not |
| `xxf:phase` | `xs:string` | current event phase: capture, target, or bubbling |
| `xxf:repeat-indexes` | `xs:string*` | event target's current repeat indexes, if any, starting from the ancestor repeat |
| `xxf:repeat-ancestors` | `xs:string*` | event target's ancestor repeat ids, if any |
| `xxf:target-prefixes` | `xs:string*` | event target's id prefixes, if any, starting from the ancestor components. This will be empty unless the target is within an XBL component |

## UI events

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

| Context | Type | Description |
| --- | --- | --- |
| `xxf:control-position` | `xs:integer` | event target's position in the user interface. This is the control's static position, i.e. this does not reflect  possible repeat iterations |
| `xxf:binding` | `node()?` | event target's single-node binding if any |
| `xxf:label` | `xs:string?` | event target's label value if any |
| `xxf:hint` | `xs:string?` | event target's hint value if any |
| `xxf:help` | `xs:string?` | event target's help value if any |
| `xxf:alert` | `xs:string?` | event target's alert value if any |

On `xforms-value-changed`, the following extension attributes are supported:

- `event('xxf:value')`
  *[SINCE Orbeon Forms 4.4]*
  The current value (that is, the value after the change) of the control.

On `xforms-select`, the following extension attributes are supported:

- `event('xxf:item-value')`
  When this event is dispatched to in response to a selection control item being selected, returns the value of the selected item.

## Other events

On `xforms-submit-serialize`, the following extension attributes are supported:

| Context | Type | Description |
| --- | --- | --- |
| `xxf:binding` | `node()?` |submission's single-node binding if any |
| `xxf:serialization` | `xs:string` |submission's requested serialization, e.g. `application/xml`, `application/x-www-form-urlencoded`, etc |
