> [[Home]] ▸ [[XForms]] ▸ [[XBL|XForms ~ XBL]]

## Related pages

- [[FAQ|XForms ~ XBL ~ FAQ]]
- [[Learning from Existing Components|XForms ~ XBL ~ Learning from Existing Components]]
- [[Tutorial|XForms ~ XBL ~ Tutorial]]
- [[Bindings| XForms ~ XBL ~ Bindings]]
- [[XForms Models|XForms ~ XBL ~ XForms Models]]
- [[Including Content|XForms ~ XBL ~ Including Content]]
- [[Event Handling|XForms ~ XBL ~ Event Handling]]
- [[Conventions|XForms ~ XBL ~ Conventions]]

## Why are components needed?  

The Orbeon Forms XForms engine proposes out of the box a set of controls, including input fields, radio buttons, etc. Those are typically implemented natively within the XForms engine.

Beyond the basic set of controls, there is an obvious need for creating new reusable controls. It would be difficult to modify the XForms engine itself each time a new control is needed. Orbeon Forms therefore supports a complete framework inspired by the [XBL 2][1] specification to address this need.

*NOTE: The XBL 2 specification is no longer under development at W3C, but as of 2015 [Web Components](http://webcomponents.org/) embody most of the ideas of XBL 2, including custom elements, the shadow DOM, and strong encapsulation.*

## Use cases

You can use components to implement:  

* Controls for datatypes which have a native implementation, but where a custom appearance is required
    * A custom control for entering a date with dropdown menus rather than a date picker
* Controls for datatypes which do not have a native implementation
    * A control to capture the `xs:duration` type  
* Controls which do not have a standard XML type
    * A phone number control
* Higher-level components
    * A form section component
    * A form grid component
    * A Google Maps component

This is not an exhaustive list. Your imagination is the limit!

Components can also be categorized by the way they operate:

* A component might simply add a little bit of functionality over an existing XForms control
    * A simple currency field
* A component might *group* together multiple XForms controls
    * A component to enter a date by using multiple text fields
* A component might implement a completely new control based on a JavaScript library
    * Example: a Google Map component
* A component might take, and possibly transform, nested markup placed by the user
    * Example: a data table component with sorting and paging

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