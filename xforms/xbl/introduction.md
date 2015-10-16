# Introduction

<!-- toc -->

## Why are components needed?

The Orbeon Forms XForms engine proposes out of the box a set of controls, including input fields, radio buttons, etc. Those are typically implemented natively within the XForms engine.

Beyond the basic set of controls, there is an obvious need for creating new reusable controls. It would be difficult to modify the XForms engine itself each time a new control is needed. Orbeon Forms therefore supports a complete framework inspired by the [XBL 2][1] specification to address this need.

*NOTE: The XBL 2 specification is no longer under development at W3C, but as of 2015 [Web Components](http://webcomponents.org/) embody most of the ideas of XBL 2, including custom elements, the shadow DOM, and strong encapsulation.*

## Use cases

You can use components to implement:

* Controls for datatypes which have a native implementation, but where a custom appearance is required
    * Example: A custom control for entering a date with dropdown menus rather than a date picker
* Controls for datatypes which do not have a native implementation
    * Example: A control to capture the `xs:duration` type
* Controls which do not have a standard XML type
    * Example: A phone number control
* Controls which wrap existing controls
    * Example: A [[character counter component|Form Runner ~ XBL Components ~ Character Counter]]
* Higher-level components
    * Examples:
        * A [[form grid component|Form Runner ~ XBL Components ~ Grid]]
        * A [[wizard component|Form Runner ~ XBL Components ~ Wizard]]
* Components which implement a completely new control based on a JavaScript library
    * Example: A [[map component|Form Runner ~ XBL Components ~ Map]]

This is not an exhaustive list. Your imagination is the limit!

You might want to also check the [[components|Form Runner ~ XBL Components]] provided out of the box by Orbeon Forms.

## Terminology

* **Component or custom control:** a piece of software which provides reusable behavior and presentation.
* **Component instance:** a particular use of a component within an XForms document. A component might have multiple instances in a given page.
    * _NOTE: This should not be confused with XForms instances._
* **Component implementation:** the code which constitutes the inner workings of a particular component.
* **Component author:** the person who writes a component.
* **Component user:** the person who uses a component.
    * In general, _writing_ a component will be harder than _using_ one.
    * Obviously the user can be the same as the author!

[1]: http://www.w3.org/TR/xbl/
