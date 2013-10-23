## General Orbeon Forms FAQ

### What is Orbeon Forms?

Orbeon Forms is an open source, standard-based web forms solution, which includes:

- *Form Builder:* a browser-based WYSIWYG form authoring tool (also known as a form designer)
- *Form Runner:* a runtime environment which facilitates the deployment and integration of large and complex forms
- a core *forms processing engine* which implements the [XForms specification](http://www.w3.org/TR/xforms/) and an XBL-inspired component model

See also:

- the Orbeon web site at [orbeon.com](http://www.orbeon.com/)
- the latest [README file](https://github.com/orbeon/orbeon-forms/blob/master/README.md)

### What differentiates Orbeon Forms from other web solutions?

A few things come to mind:

1. Orbeon Forms is not intended to be a general-purpose web app platform, but instead focuses on forms, typically large forms, and handling many of them. It's not uncommon for users to have hundreds of forms to create and deploy, or to have forms with hundreds of fields (sometimes more than 1000 fields).

2. Although from time to time customization is required, and some complex rules do require the use of XPath, Orbeon Forms strives not to require coding in Java, JavaScript or other programming languages
 
3. Finally, Orbeon Forms is biased towards XML documents as a representation of both form definitions and form data.

### Is there any cost associated with using Orbeon Forms?

[Community Edition (CE) builds](http://www.orbeon.com/download) are available free of charge whether your use it to build open source or commercial applications.

[Professional Edition (PE) builds](http://www.orbeon.com/download) are available through [PE Subscription plans](http://www.orbeon.com/pricing).

The complete [source code](github.com/orbeon/orbeon-forms/) to Orbeon Forms CE and PE is available free of charge and under *real* open source terms. Note that out of the box, the build file creates a CE build.

With the open source code, you are free as you please to:

- extend the platform
- build applications on top of the platform

Note however that if you make changes to the existing Orbeon Forms code, you are bound by the terms of the LGPL license, which requires you to redistribute changes to the open source community when you distribute your application.

Orbeon offers commercial support for Orbeon Forms via [PE Subscription plans]() and [Dev Support plans](http://www.orbeon.com/services).

### Is Orbeon Forms an XForms Engine?

Orbeon Forms *includes* an XForms engine (also known as an XForms processor), which we refer to as the Orbeon Forms XForms engine.

But Orbeon Forms also includes:

- the Form Builder authoring tool
- the Form Runner runtime
- an XML pipeline engine running the XPL pipeline language
- an application controller (the Page Flow Controller or PFC)

You are free to use Orbeon Forms for its XForms functionality only, but you can also leverage more of Orbeon Forms to build your forms-based application.
