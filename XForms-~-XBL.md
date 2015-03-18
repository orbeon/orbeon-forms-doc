## See also

- [Guide to Using and Writing XBL Components](http://wiki.orbeon.com/forms/doc/developer-guide/xbl-components-guide)
- [[XBL Bindings| XForms ~ XBL ~ Bindings]]

## Introduction

### Why are components needed?  

The Orbeon Forms XForms engine proposes out of the box a set of controls, including input fields, radio buttons, etc. Those are typically implemented natively within the XForms engine.

Beyond the basic set of controls, there is an obvious need for creating new reusable controls. It would be difficult to modify the XForms engine itself. Orbeon Forms therefore proposes a complete framework inspired by the [XBL 2][1] specification to address this need.

*NOTE: The XBL 2 specification is no longer under development at W3C, but as of 2015 [Web Components](http://webcomponents.org/) embody most of the ideas of XBL 2, including custom elements, the shadow DOM, and strong encapsulation.*

### Use cases   

You can use components to implement:  

* Controls for datatypes which have a native implementation, but where a custom appearance is required
    * E.g. custom control for entering an xs:date with dropdown menus rather than a date picker
* Controls for datatypes which do not have a native implementation
    * E.g. control to capture the xs:duration type  
* Controls which do not have a standard XML type
    * E.g. phone number control
* Higher-level components  
    * Instance inspector component
    * Grid table component
    * Google Maps component
This is not an exhaustive list. Your imagination is the limit!

See the [documentation on already implemented components][2] to get a feel for what is possible.  

### Terminology

* **Component:** a piece of software which provides reusable behavior and presentation.  
* **Component instance:** a particular use of a component within an XForms document. A component might have multiple instances in a given page.
    * _NOTE: This should not be confused with XForms instances._
* **Component implementation:** the code which constitutes the inner workings of a particular component.  
* **Component author:** the person who writes a component.
* **Component user:** the person who uses a component.
    * In general, _writing_ a component will be harder than _using_ one.
    * Obviously the user can be the same as the author!

[1]: http://www.w3.org/TR/xbl/