# XBL Library



## Introduction

The XForms engine finds XBL bindings in the following ways:

1. Inline bindings, that is bindings under the form's `xh:head`, are always processed.
2. Bindings by name are searched in the Orbeon Forms resources.
3. Other bindings (such as bindings by attribute) are searched based on the `oxf.xforms.xbl.library` property.

## Inline bindings

You can place inline `xbl:xbl` elements within the `xh:head` element, at the same level as `xf:model` elements. For example:

```xml
<xh:html
    xmlns:xh="http://www.w3.org/1999/xhtml"
    xmlns:xf="http://www.w3.org/2002/xforms"
    xmlns:xbl="http://www.w3.org/ns/xbl">

    <xh:head>
        <xf:model id="fr-form-model">
            ...
        </xf:model>
        <xbl:xbl ...>
            <xbl:binding ...>
                ...
            </xbl:binding>
            <xbl:binding ...>
                ...
            </xbl:binding>
        </xbl:xbl>
        <xbl:xbl ...>
            <xbl:binding ...>
                ...
            </xbl:binding>
        </xbl:xbl>
    </xh:head>
    <xh:body>
        ...
    </xh:body>
</xh:html>
```

## By name bindings

Orbeon Forms allows for automatic inclusion of XBL bindings when matching by name only. This avoids including the XBL for those components in all the forms that use them. In addition, the bindings can be shared among forms, which saves memory and makes loading them faster.

Say element `<acme:button>` is found by the XForms engine, in your own `http://www.acme.com/xbl` namespace:

- The corresponding XBL file is located following the standard [directory layout rules](bindings.md#directory-layout).
- The resource, if found, is automatically included in the page for XBL processing.

Such bindings are checked for freshness every time a form is loaded. If a binding is out of date, it is reloaded and the form is recompiled.

## Other bindings

Bindings which are not inline and which are not by name only need to be explicitly listed so that the XForms engine is able to process them. You do this with the `oxf.xforms.xbl.library` property.

With Orbeon Forms 4.9, this property is empty as no built-in XBL component binds by attribute in that version:

```xml
<property as="xs:string" name="oxf.xforms.xbl.library">
</property>
```

XBL components with bindings by attribute can be added using the same format used by `oxf.xforms.resources.baseline`. Component references are separated by whitespace. With Orbeon Forms 4.10, the property is as follows:

```xml
<property as="xs:string"  name="oxf.xforms.xbl.library">
    fr:dropdown-select1
    fr:dropdown-date
    fr:fields-date
    fr:box-select
    fr:character-counter
</property>
```

With Orbeon Forms 2016.1, the property is as follows:

```xml
<property as="xs:string"  name="oxf.xforms.xbl.library">
    fr:tinymce
    fr:dropdown-select1
    fr:dropdown-date
    fr:fields-date
    fr:box-select
    fr:character-counter
    fr:bootstrap-select1
    fr:boolean-input
    fr:yesno-input
    fr:open-select1
</property>
```

The `fr` prefix must be in scope, and there must be a mapping (via `oxf.xforms.xbl.mapping`) for the URI associated with that prefix (this is the case by default for `fr` and `http://orbeon.org/oxf/xml/form-runner`).