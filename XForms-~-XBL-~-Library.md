> [[Home]] ▸ [[XBL|XForms ~ XBL]]

## Introduction

The XForms engine finds XBL bindings in the following ways:

1. Inline bindings, that is bindings under the form's `xh:head`, are always processed.
2. Bindings by name are searched in the Orbeon Forms resources.
3. Other bindings (such as bindings by attribute) are searched based on the `oxf.xforms.xbl.library` property.

## Inline bindings

TODO

## Bindings by name

Orbeon Forms allows for automatic inclusion of XBL bindings when matching by name only. This avoids including the XBL for those components in all the forms that use them. In addition, the bindings can be shared among forms, which saves memory and makes loading them faster.

Automatic inclusion works by defining a mapping between:

- the XML namespace in which your XBL components are
- and a name which indirectly points to a directory containing the XBL file

The mapping is done via properties starting with `oxf.xforms.xbl.mapping`, for example:

```xml
<property as="xs:string" name="oxf.xforms.xbl.mapping.acme">
    http://www.acme.com/xbl
</property>
```

With the property above:

1. Say element `<acme:button>` is found by the XForms engine, in your own `http://www.acme.com/xbl` namespace.
2. Orbeon Forms looks for a property with a name that starts with `oxf.xforms.xbl.mapping` and with a value equal to the namespace `http://www.acme.com/xbl`. In this case it finds the property `oxf.xforms.xbl.mapping.acme`.
3. The XForms engine extracts the part of the property name after `oxf.xforms.xbl.mapping`. In this case `acme`.
4. This is used to resolve a resource called `oxf:/xbl/acme/button/button.xbl`.
    * The first part of the path is always `xbl`.
    * This is followed by the directory name found in step 3, here `acme`.
    * This is followed by a directory with the same name as the local name of your component, containing an XBL file also with the same name, here `button/button.xbl`.  
    * The resource, if found, is automatically included in the page for XBL processing.

By default, all the elements in the `http://orbeon.org/oxf/xml/form-runner` namespace (typically using the prefix `fr`) are handled this way, as a mapping is defined by default as follows:

```xml
<property as="xs:string"  name="oxf.xforms.xbl.mapping.orbeon">
    http://orbeon.org/oxf/xml/form-runner
</property>
```

For example:

- `<fr:number>` is loaded from `/xbl/orbeon/number/number.xbl`
- `<fr:section>` is loaded from `/xbl/orbeon/section/section.xbl`

etc.

Such bindings are checked for freshness every time a form is loaded. If a binding is out of date, it is reloaded and the form is recompiled.

## Other bindings

TODO