# Modes

<!-- toc -->

## Introduction

The XBL component binding defined with `<xbl:binding>` supports the `xxbl:mode` attribute, which contains an optional space-separated list of tokens. Each token enables a mode, as described below. Modes change the behavior of the component.

## The binding mode

The `binding` mode enables an optional XForms single-item binding. This means that the component can be used with XForms's `ref` or `bind` attribute, including the modifying `context` and `model` attributes.

For an example, see [Creating a single-node binding](http://doc.orbeon.com/xforms/xbl/tutorial.html#creating-a-single-node-binding).

## The value mode

The `value` mode makes the component hold a value through its binding. This means the component behaves like `<xf:input>` and other controls and dispatches `xforms-value-changed` events.

You use this mode in addition to `binding`.

For an example, see [Adding support for a value](http://doc.orbeon.com/xforms/xbl/tutorial.html#adding-support-for-a-value).

## The external-value mode

[SINCE Orbeon Forms 4.11]

You use the `external-value` mode in addition to the `binding` and `value` modes.

By default, `value` doesn't expose the control's value to the client. By adding the `external-value` mode, the control's value:

- can be set from the client using `ORBEON.xforms.Document.setValue`
- can be read from the client using `ORBEON.xforms.Document.getValue` if the JavaScript companion class of the component supports it
- is sent to the client in Ajax responses, and calls the JavaScript companion class of the component if it supports it

For an example, see [the implementation of the `fr:code-mirror` component](https://github.com/orbeon/orbeon-forms/blob/master/src/resources-packaged/xbl/orbeon/code-mirror/code-mirror.xbl).

You use this mode when the component implementation is mostly done in JavaScript and not with nested XForms controls.

## The lhha and custom-lhha modes

The `lhha` mode allows the component to support the `<xh:label>`, `<xh:hint>`, `<xh:help>` and `<xh:alert>` element, whether:

- directly nested under the component's bound element
- or using the `for` attribute

By default, markup is output for the LHHA elements. You can disable this with the additional `custom-lhha` mode.

For an example, see [Adding LHHA elements](http://doc.orbeon.com/xforms/xbl/tutorial.html#adding-lhha-elements).

[SINCE Orbeon Forms 4.5]

Sometimes, a component implementation uses HTML form controls, and you would like a `<label>` element pointing to it with a `for` attribute in the generated HTML markup.

You enable this with the `xxbl:label-for` attribute.

The value of the attribute must be the id of a nested XForms control or a nested HTML control element.

For examples, see [some of the Orbeon Forms XBL components](https://github.com/orbeon/orbeon-forms/tree/master/src/resources-packaged/xbl/orbeon).

## The focus mode

The `focus` mode allows the component to handle keyboard focus natively, so that the XForms engine is aware of focus.

You use this mode when the component implementation is mostly done in JavaScript and not with nested XForms controls.

## The nohandlers mode

The `nohandlers` mode disables automatic processing of nested event handlers. You should only need this for very special components.

For an example and more details, see [http://doc.orbeon.com/xforms/xbl/event-handling.html#component-user-attaching-event-handlers-to-the-bound-node).
