# UI refresh events



## Introduction

### What are refresh events?

We call refresh events the following UI events defined in XForms, as they can occur during UI refresh:

* `xforms-value-changed`
* `xforms-enabled` / `xforms-disabled`
* `xforms-readwrite` / `xforms-readonly`
* `xforms-optional` / `xforms-required`
* `xforms-valid` / `xforms-invalid`

There are other UI events that are dispatched outside of refresh, including `DOMActivate`, `xforms-select`, etc. These are not considered here.

### Purpose of refresh events

Refresh events allow:

- reliably tracking a control's MIP status during its lifecycle
- reliably tracking changes to the control's value as seen by the control/user

<!--
Use cases include:

- performing JavaScript creation/destruction of resources associated with custom controls when a group or other containing control becomes relevant/non-relevant
- tracking control validity for displaying a summary of form errors
-->

<!--

### Relevance

The concept of "relevance" for a control relates to whether the control is "active" in the UI. A non-relevant control is typically not shown at all. We use the following notions:  

- A relevant control concretely exists, in that it has a run-time object representing the UI control and its state, associated with its template in the markup.  
- A non-relevant control does not exist. Only a template for it exists in the markup.
- A control goes from non-extant to extant by a process of creation.
- A control goes from extant to non-extant by a process of destruction.

In XForms 1.1, the notions above are already supported by the language related to the creation and destruction of repeat iterations, although they are absent from other parts of the spec.

XForms 1.1 only discusses creation/destruction (relevance changes) after the initial UI has been initialized.

Creation and destruction can happen in the following situations:  

- creation  
    - initial UI creation
    - creation of a new repeat iteration
    - subsequent change from non-relevant to relevant due to node bindings, MIPs, or enclosing control relevance during refresh
- destruction
    - destruction of a repeat iteration
    - subsequent change from relevant to non-relevant due to node bindings, MIPs, or enclosing control relevance during refresh

There are two ways of addressing the cases not covered by XForms 1.1:  

1. Use xforms-enabled/xforms-disabled
2. Use new events

For simplicity and consistency reasons, we choose option #1 and dispatch xforms-enabled/xforms-disabled in all the cases above.

Rules:  

- Every control receives `xforms-enabled` after being created (becoming relevant).
- Every control receives `xforms-disabled` before being destroyed (becoming non-relevant).
- While a control is relevant, it can get refresh events updates for the other refresh events (more on this below).  

## Repeat handling

In XForms, repeat processing works as follows:

- For each repeat iteration, a "repeat object" (implicit group) is created, containing the "run-time objects" representing the UI controls for that iteration.
- "the user interface form controls generated for the repeat object are initialized in the same manner as the user interface initialization that is performed during default processsing of `xforms-model-construct-done`"  

This means that user interface controls can be updated in these circumstances:  

1. during UI initialization  
2. during refresh
3. just after the update of a repeat’s set of items (through `xf:insert` / `xf:delete`)

An important question is whether refresh events must be dispatched after repeat node-set update, or only during refresh.

For xforms-disabled, there is an issue: if the controls are removed from the UI just after an xf:delete, then it is not possible to dispatch xforms-disabled events during a subsequent refresh because at that time, the controls will have disappeared already, and it is not possible to dispatch an event to a non-existing (non-relevant) control. So the only solution seems to be to dispatch xforms-disabled just before the controls are removed from the UI.

What about control creation? Could dispatching of events be deferred until a subsequent refresh? That could be possible except for the following problem: a control becomes relevant in a new iteration, then the iteration is removed before the subsequent refresh. In that case, the control would get xforms-disabled without ever getting xforms-enabled. So it seems that here again, xforms-enabled must be dispatched just after the creation of the new repeat iteration.

Rules:  

- After the creation of new repeat iterations (as a result of `xf:insert`), refresh events are dispatched for the newly created controls.
- Just before destruction of repeat iterations (as a result of `xf:delete`), refresh events are dispatched for the controls to be destroyed.

-->

## Lifecycle of a control

### Markup and concrete controls

We make a distinction between:

* XForms markup designed to represent a control, such as `<xf:input>`.
* concrete controls in the sense of runtime objects able to keep state.

XForms markup for controls may or may not yield the creation of concrete controls, depending on repeats and relevance conditions.

A concrete control has a simple lifecycle:

- It is created.
- While in existence, it might keep and modify state information.
- It may be destroyed.

After being destroyed:

- A concrete control is considered non-existent.
- Therefore it does not keep or modify internal state.

<!--
_NOTE: This solution equates existence, relevance, and visibility. As of 2010-10, the XForms working group is discussing whether some of these concepts should be separated._
-->

A control is concrete if it meets the conditions for relevance:

- It has no binding attribute and is at the top-level or within a relevant container control
- It has a binding attribute
    - AND it is at the top-level OR within a relevant container control
    - AND the binding attribute resolves to a non-empty node-set
    - AND
        - for single-node bindings, the first node of the binding has the relevant property
        - for items-set bindings, at least one node of the binding has the relevant property

Concrete controls are created at the following times:

- during processing of the default action for [`xforms-model-construct-done`][2] event, if they meet the conditions for relevance
- when a new repeat iteration is inserted into a repeat container, if they meet the conditions for relevance
    - either during `xf:insert` processing
    - or during refresh
- during refresh, when the condition for relevance goes from non-relevant to relevant

Concrete controls are destroyed at the following times:

- when a repeat iteration is removed from a repeat container
    - either during `xf:delete` processing [UNTIL Orbeon Forms 2017.2 included, see [#3503](https://github.com/orbeon/orbeon-forms/issues/3503)]
    - or during refresh
- during refresh, when the condition for relevance goes from relevant to non-relevant

_NOTE: If the binding of a control changes from one node to another during refresh, the control is not destroyed._

<!--
_[TODO: document instance replacement]_

_[TODO: document predicates changed]_

_[TODO: document unbound controls (e.g. trigger, group w/o ref)]_
-->

### Controls state

Controls maintain their own state, including value and MIP state.

### Control creation

Just after a control becomes relevant, whether during:  

- initial UI creation
- creation of a new repeat iteration
- subsequent change from non-relevant to relevant due to node bindings, MIPs, or enclosing control relevance during refresh  

The following occurs:  

- The control's state, including value and MIPs, is acquired through its binding and other attributes.
- `xforms-enabled` is dispatched
- `xxforms-visible` is dispatched if the control is not in a hidden switch case
    - SINCE Orbeon Forms 2018.1
    - See also [`xxforms-visible`](xforms/events-extensions-events.md#xxforms-visible). 
- non-default MIP events are dispatched (in order to reduce the number of events dispatched)  
    - `xforms-invalid`
    - `xforms-required`
    - `xforms-readonly`
    
_NOTE: a good rationale can be made for not dispatching non-default MIP events:_
- _If they are considered change events, then they should behave like `xforms-value-changed`._
- _Context information upon `xforms-enabled` can be used to determine the initial state of the MIPs._

_NOTE: `xforms-value-changed` is NOT dispatched (because there is no "value change")._

<!--
- _NOTE: We should add context information to all these events to provide access to_
    - _value_
    - _MIPs_
    - _Q: what happens just before the control becomes non-relevant?_  
-->

### Control destruction

Just before a control becomes non-relevant, whether during:  

- destruction of a repeat iteration
- subsequent change from relevant to non-relevant due to node bindings, MIPs, or enclosing control relevance during refresh

The following occurs:  

- `xxforms-hidden` is dispatched if the control is not in a hidden switch case
    - SINCE Orbeon Forms 2018.1
    - See also [`xxforms-hidden`](xforms/events-extensions-events.md#xxforms-hidden).
- `xforms-disabled` is dispatched
- no other events are dispatched  

### Changes during the lifetime of the control

Each relevant control stores its current value and MIP information. Therefore refresh events are related directly to controls, and no longer to instance data nodes. This is an important difference with XForms 1.1.

While the control is relevant, upon refresh:  

- Its current MIP and value are stored temporarily as the control's old state.  
- The new control state, including value and MIPs, is acquired through its binding and other attributes.  
- Old and new state are compared.  
- Events related to value and MIP _changes_ are dispatched.

NOTES:

- `xf:output/@value` also dispatches `xforms-value-changed`.
- `xf:var` also dispatch `xforms-value-changed`.

_NOTE: XForms 1.1 says that all MIP events must be dispatched upon value change. This is not not necessary because those events are properly tracked independently._

<!--
## Open questions

### Handling of relevance events for non-single-node binding controls

Relevance is a property which can apply to any XForms control, not only controls with a single-node binding. But is there any concrete case where this would apply currently:  

* xf:repeat
    * thought: handling at level of individual iterations is probably better and would also allow detecting the insertion of new iterations (xforms-enabled)
    * could complete this with new event xxforms-iteration-moved  
* xxf:dialog
    * thought: wait until standard xf:dialog is better formalized by XForms WG, in the meanwhile we do not need relevance  
* xf:case (not really a control!)
    * already gets xforms-selected  
* component
    * thought: if we keep thinking of it as a non-XForms-specific construct, then it should not get xforms-enabled/disabled  

### Alerts attached to data nodes by constraints

[TODO]
-->

<!--
## RFE: refresh done event?

It could be useful to add an xxforms-refresh-done event. This event could be used:

* to mark the end of a particular refresh
* possibly, contain context information such as whether
    * control values changed
    * repeat iterations added/removed (?)
    * relevance changed
    * etc.
-->

## Compatibility note: UI refresh events in XForms 1.1

Refresh events in XForms 1.1 are based on:

- marking instance data nodes as their value change or binds apply to them
- checking those markings during refresh
- dispatching events based on that check

This mechanism is not satisfying because we think that it goes against the expectations of the form author and it does not produce events reliably ([see email sent to public-forms mailing-list on June 12, 2007][1]). Therefore Orbeon Forms does not strictly follow XForms 1.1 on this topic.


[1]: http://lists.w3.org/Archives/Public/public-forms/2007Jun/0030.html
[2]: http://www.w3.org/TR/xforms11/#evt-modelConstructDone

## See also

- [Standard support](events-standard.md)
- [Keyboard events](events-extensions-keyboard.md)
- [Extension events](events-extensions-events.md)
- [Extension context information](events-extensions-context.md)
- [Other event extensions](events-extensions-other.md)
