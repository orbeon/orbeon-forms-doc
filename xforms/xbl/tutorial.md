# Tutorial

## Encapsulation

The component system favors a strong _encapsulation_ so that components can be:

* developed in isolation, without knowing the details of the application using them
* used without knowing the details of how they are implemented
* in short: reused as much as possible!

The goal is first eased of use and transparency for the form author. This means that sometimes the component author must do a little bit more work!

## The default: strong encapsulation

By default, within an `<xbl:binding>` element, encapsulation is strong: this means that XForms controls, models, and event handlers cannot:

* see ids of the XForms document using the component
* have access to the XPath context outside the component

In other words, things behave as if you were working in a new, completely separate XForms document!

If you place models within `<xbl:implementation>` or `<xbl:template>`, the same rule that applies in a top-level XForms document apply:

* The default XPath context starts with the root element of the first instance of the first model.
* However, if your component doesn't have a model, then the XPath context is set to an empty document node.
    * _NOTE: In the future, this might change to being an empty sequence. For implementation-dependent reasons, for now an empty document node had to be used._

## Creating a single-node binding

Orbeon Forms provides direct support for bindings with the `binding` mode:

```xml
<xbl:binding id="fr-foo" element="fr|foo" xxbl:mode="binding">
```

This automatically means that the component supports all the XForms binding attributes:

* `model`
* `context`
* `ref`
* `bind`

When a component has a binding, UI events are dispatched depending on the bound item:

* `xforms-enabled` / `xforms-disabled`
* `xforms-readonly` / `xforms-readwrite`
* `xforms-optional` / `xforms-required`
* `xforms-valid` / `xforms-invalid`

You can access the actual bound node via the `xxf:binding()` function:

```xml
xxf:binding('fr-foo')
```

The id passed must be the id of the `xbl:binding` element.

The `xxf:binding-context()` function returns the XPath evaluation context of the binding

```xml
xxf:binding-context('fr-foo')
```

## Adding support for a value

Some native XForms controls, like `xf:input`, `xf:textarea`, etc. support holding a value. This means in particular that the control is able to dispatch `xforms-value-changed` events. Some controls on the other hand, like `xf:group`, `xf:repeat`, can't hold a value and don't dispatch `xforms-value-changed` events.

The default for XBL components is that they don't hold a value. Use the `value` mode to add value behavior:

```xml
<xbl:binding
  id="fr-phone-binding"
  element="fr|us-phone"
  xxbl:mode="lhha binding value">
```

The `xxf:value()` function allows access to the control's value from the inside, by using the binding id:

```xml
<xbl:binding id="fr-gaga" element="fr|gaga" xxbl:mode="binding value">
    <xbl:template>
        <xf:output id="gaga-output" value="xxf:value('fr-gaga')"/>
    </xbl:template>
</xbl:binding>
```

## Adding LHHA elements

Orbeon Forms provides direct support for label, help, hint and alert (LHHA):

```xml
<xbl:binding id="fr-foo" element="fr|foo" xxbl:mode="lhha">
```

This automatically adds support for LHHA to the component:

```xml
<fr:foo>
    <xf:label>My label</xf:label>
    ...
</fr:foo>
```

By default, markup is output for the LHHA elements. You can disable this with the `custom-lhha` mode:

```xml
<xbl:binding id="fr-foo" element="fr|foo" xxbl:mode="lhha custom-lhha">
```

With this mode, no markup is output, and the component author can access the LHHA values with XPath functions:

```xml
<xf:output value="xxf:label('fr-foo')"/>
```

[SINCE Orbeon Forms 4.5] When using the `lhha` mode, it is possible to link the label handled by the XBL engine to an internal control, so that that control and the label are linked. This is done with the `xxbl:label-for` attribute:

```xml
<xbl:binding
  id="fr-dropdown-select1"
  element="fr|dropdown-select1"
  xxbl:container="span"
  xxbl:mode="lhha binding value"
  xxbl:label-for="select1">

<xbl:template>
    <xf:select1
        appearance="minimal"
        ref="xxf:binding('fr-dropdown-select1')"
        id="select1">
    ...
```

## A basic component

You can find the component discussed in this section in the Orbeon Forms distribution:

* XBL: xbl/orbeon/tutorial-input/tutorial-input.xbl
* Example: apps/xforms-sandbox/samples/xbl-tutorial-input.xhtml

Orbeon Forms modes make this very simple:

```xml
<xbl:xbl xmlns:xh="http://www.w3.org/1999/xhtml"
         xmlns:xf="http://www.w3.org/2002/xforms"
         xmlns:xs="http://www.w3.org/2001/XMLSchema"
         xmlns:ev="http://www.w3.org/2001/xml-events"
         xmlns:xxf="http://orbeon.org/oxf/xml/xforms"
         xmlns:fr="http://orbeon.org/oxf/xml/form-runner"
         xmlns:xbl="http://www.w3.org/ns/xbl"
         xmlns:xxbl="http://orbeon.org/oxf/xml/xbl">

    <xbl:binding
        element="fr|tutorial-input"
        id="fr-tutorial-input"
        xxbl:mode="lhha binding value">
        <xbl:template>
            <!-- Input points to the external single-node binding -->
            <xf:input ref="xxf:binding('fr-tutorial-input')"/>
        </xbl:template>
    </xbl:binding>
</xbl:xbl>
```

## Using local state

You can find the component discussed in this section in the Orbeon Forms distribution:

* XBL: `xbl/orbeon/tutorial-davinci/tutorial-davinci.xbl`
* Example: `apps/xforms-sandbox/samples/xbl-tutorial-davinci.xhtml`

Now assume you would like to write a component which stores the string of characters entered by the user but back to front, i.e. reversed. If the user types "Amelia", the string stored in the instance will be "ailemA".

How would you go about this? The binding between an XForms control and instance data is direct: the value entered by the user is stored into the instance as soon as the user moves out of the field, and you can't just write a transformation in between. So we need some intermediate state to store the value entered by the user.

To do so, we create a local instance. You can put it under the `<xbl:implementation>` or `<xbl:template>` elements in your XBL file:

```xml
<xf:model>
    <xf:instance><value/></xf:instance>
</xf:model>
```

Here we store a single value, so we just use a single root element in the instance:  `<value>`.

The local input field just points to the local instance instead of pointing to the external single-node binding:

```xml
<xf:input ref="instance()">
```

So here, the input field points to the `<value>` element.

_NOTE: You could also write `ref="."`, which would work because, like at the top-level of an XForms document, the default XPath context is the root element of the first instance in the first model._ Using `instance()` is a bit more explicit.

What is needed now is, when the local value changes, to copy it to the external single-node binding. You do so with an event handler:

```xml
<xf:input ref="instance()">
    <xf:setvalue
        ev:event="xforms-value-changed"
        ref="xxf:binding('fr-tutorial-davinci')"
        value="context()"/>
</xf:input>
```

What's missing now is to reverse the value:

```xml
<xf:input ref="instance()">
    <xf:setvalue
        event="xforms-value-changed"
        ref="xxf:binding('fr-tutorial-davinci')"
        value="
            codepoints-to-string(
                reverse(
                    string-to-codepoints(
                       instance()
                    )
                )
            )
        "/>
</xf:input>
```

Finally, the opposite operation is needed: when the component first comes to life, and when the external value changes, the internal value must update:

```xml
<xbl:handler
    event="xforms-enabled xforms-value-changed"
    ref="instance()"
    value="
        codepoints-to-string(
            reverse(
                string-to-codepoints(
                    xxf:binding('fr-tutorial-davinci')
                )
            )
        )
 "/>
```

So here you go: you have a fully working non-trivial component:

```xml
<xbl:xbl xmlns:xh="http://www.w3.org/1999/xhtml"
         xmlns:xf="http://www.w3.org/2002/xforms"
         xmlns:xs="http://www.w3.org/2001/XMLSchema"
         xmlns:ev="http://www.w3.org/2001/xml-events"
         xmlns:xxf="http://orbeon.org/oxf/xml/xforms"
         xmlns:fr="http://orbeon.org/oxf/xml/form-runner"
         xmlns:xbl="http://www.w3.org/ns/xbl"
         xmlns:xxbl="http://orbeon.org/oxf/xml/xbl">

    <xbl:binding
        element="fr|tutorial-davinci"
        id="fr-tutorial-davinci"
        xxbl:mode="lhha binding value">
        <xbl:handlers>
            <!-- When the control comes to life or its value changes, copy
                 the new value to the local model after reversing it -->
            <xbl:handler
                event="xforms-enabled xforms-value-changed"
                ref="instance()"
                value="
                    codepoints-to-string(
                        reverse(
                            string-to-codepoints(
                                xxf:binding('fr-tutorial-davinci')
                            )
                        )
                    )
                "/>
        </xbl:handlers>
        <xbl:template>
            <!-- Local model and instance -->
            <xf:model>
                <xf:instance><value/></xf:instance>
            </xf:model>
            <!-- Input points to the internal value -->
            <xf:input ref="instance()">
                <!-- When the local value changes, copy it to the external
                     single-node binding after reversing it -->
                <xf:setvalue
                    event="xforms-value-changed"
                    ref="xxf:binding('fr-tutorial-davinci')"
                    value="
                        codepoints-to-string(
                            reverse(
                                string-to-codepoints(
                                    instance()
                                )
                            )
                        )
                    "/>
            </xf:input>
        </xbl:template>
    </xbl:binding>
</xbl:xbl>
```
