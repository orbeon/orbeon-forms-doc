> [[Home]] ▸ [[XForms]] ▸ [[XBL|XForms ~ XBL]]

## Related pages

- [[Introduction|XForms ~ XBL ~ Introduction]]
- [[FAQ|XForms ~ XBL ~ FAQ]]
- [[Learning from Existing Components|XForms ~ XBL ~ Learning from Existing Components]]
- [[Tutorial|XForms ~ XBL ~ Tutorial]]
- [[Bindings| XForms ~ XBL ~ Bindings]]
- [[Including Content|XForms ~ XBL ~ Including Content]]
- [[Event Handling|XForms ~ XBL ~ Event Handling]]
- [[Conventions|XForms ~ XBL ~ Conventions]]

## Placement of local models

A component can have its own set of XForms models, called _local models_. For each instance of the component, a new copy of the models is made, so that component instances behave completely independently from each other.

With XForms 1.1, the standard convention is to place your models under the `<xh:head>` element. An XBL component does not have an `<xh:head>` element, so Orbeon Forms supports placing models in two places:

* Under the `<xbl:implementation>` element. In this case, models are identical for all instances of a particular component.
* Under the `<xbl:template>` element. In this case, models can be different depending on the component instance, since the XBL template can parametrize its elements and attributes.

_NOTE: In XBL, the purpose of `<xbl:implementation>` is to place new methods and properties. Orbeon Forms uses XForms as the implementation or "scripting" language of XBL components, so it does not have methods and properties. But models, through events, can implement behavior, so allowing model placement under `<xbl:implementation>` seems to fit the intent of XBL._

## Local model-related events

XForms 1.1 specifies the following event sequence upon value changes, insertions, etc.:

* `rebuild` (if document structure changed): update binds structure/dependencies
* `recalculate` (if value changed): perform MIPs and calculated values updates
* `revalidate` (if value changed): perform instance revalidation
* `refresh`: updates the UI and dispatch UI events

XBL components with local models are no different and they receive these events when needed.

## Construction and destruction of local models

The following rules apply:

* Models go through initialization when a sub-tree of controls is created.
    * Situations:
        * control becomes relevant
        * a repeat iteration is bound to a new node
    * Events dispatched upon creation:
        * `xforms-model-construct`
        * `xforms-model-construct-done`
    * events dispatched upon receiving `xforms-enabled`
        * [SINCE Orbeon Forms 4.10] `xforms-ready`
* Models go through destruction when a sub-tree of controls is deleted.
    * Situations:
        * control becomes non-relevant
        * a repeat iteration is no longer bound to a node
    * Events dispatched:
        * `xforms-model-destruct`
* Models do not get construction/destruction events when iterations change
