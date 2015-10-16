# FAQ

<!-- toc -->

## What can component do?

A lot! Among other things, they can:

* behave like regular XForms controls (such as `<xf:input>`), including handling bindings, labels, and events
* keep local state using local models and instances
* run their own submission to talk to the outside world, with `<xf:submission>`
* integrate with JavaScript code, for example to expose cool widgets to XForms authors
* copy and/or modify XForms and HTML markup provided by the component user
* use nested components

## What does a component implementation look like?

That's the cool part: a component implementation looks very much like regular XForms within HTML!

A component is really a way of packaging and reusing a piece of XForms and HTML markup. So when writing a component, you can leverage all your knowledge of XForms and HTML.

## How do you use a component?

This is even easier: each component is assigned an element name. For example, must built-in Orbeon Forms components are in the "fr:" namespace (for Form Runner), and so you write things like:

```xml
<fr:fields-date ref="my-date">
  <xf:label>Birth Date</xf:label>
</fr:fields-date>
```

Most components try to follow the XForms philosophy: for example, they use common attributes like `ref`, and usually allow nesting `<xf:label>`, `<xf:help>`, etc.

## Why not implement components in Java?

This could be attractive, but then:

* A component author would have to know Java, how to run a compiler, deploy the code, etc.
* Orbeon Forms would have to define APIs to provide access to the low-level workings of the components. This is a lot of work!
* We think it is much easier to write HTML and XForms!
* This would raise the question of which  template language to use to produce the resulting markup. On the other hand, XBL is its own template language, and you can even combine it with XSLT.

## Why use XBL and not simply XSLT?

Using XSLT to implement components is not always a good alternative:

* Components may be used (instantiated) multiple times, and identifiers (the "id" attribute often used in HTML and XForms) must be handled accordingly:
    * Each component instantiation must produce unique ids in the browser
    * Id resolution must depend on where ids are used
    * Ids in different repeat iterations must be unique as well
* XSLT does not enforce any encapsulations rules, including:
    * Visibility of objects from inside or outside a component
    * Containment of event flows within the component
* Local models and instances are better handled natively
    * Each instantiation requires new models and new instances
    * Within repeats, model/instance duplication occurs at runtime

Using XBL addresses all these issues.

Convinced? If not, read on! In the section about extensions, you'll find out that you can embed XSLT transformations within Orbeon Forms XBL components.

## What's special about the XBL 2 implementation in Orbeon Forms?

* Orbeon Forms components are inspired by [XBL 2](http://www.w3.org/TR/xbl/). XBL in this case is implemented server-side, not on the client!
* Support for components is implemented at the XForms engine level.
* Components can therefore be used within XForms pages, but not within non-XForms page (such as plain XHTML pages).
* Because the XBL specification does not detail how it can be used in conjunction with XForms, Orbeon Forms uses XBL in a particular way, but it is not necessarily the only possible way.
* Orbeon Forms implements a superset of a subset of XBL!
