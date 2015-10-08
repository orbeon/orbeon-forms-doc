## Availability

Since Orbeon Forms 4.11.

## Introduction

Form Builder exposes the following developer extension API:

- API to add a custom Form Settings tab
- API to add a custom Control Settings tab

Each extension is implemented with an XBL component. The component interacts with the enclosing dialog via events.

## Setting up an XBL component

First, make sure you have chosen a prefix-to-namespace mapping for your components, as explained in
[[Automatic inclusion of XBL bindings|XForms-~-Configuration-Properties#automatic-inclusion-of-xbl-bindings]].

Below, we assume the following example mapping:

```xml
<property
  as="xs:string"
  name="oxf.xforms.xbl.mapping.acme"
  value="http://www.acme.com/xbl"/>
```

In practice, you would probably choose a prefix different from `acme`, and a namespace different from
`http://www.acme.com/xbl`.

For more on XBL components, see [[XBL documentation|XForms ~ XBL]].

## Custom Form Settings tab

### Setup

In order to add a custom Form Settings tab, the following property must be set to a non-blank value:

```xml
<property 
  as="xs:string
  name="oxf.fb.extension.form-settings"
   value="acme:form-settings"/>
```

Here, the value `acme:form-settings` refers to the XBL component implementing the custom tab:

- `acme` is the prefix you have mapped with the `oxf.xforms.xbl.mapping.acme` property above
- `form-settings` is the name you give your XBL component (it doesn't have to be `form-settings`)

### The XBL component

You then create the file implementing the component under:

```
WEB-INF/resources/xbl/acme/form-settings/form-settings.xbl
```

Here is a template for the new XBL component:

```xml
<xbl:xbl xmlns:xh="http://www.w3.org/1999/xhtml"
         xmlns:xf="http://www.w3.org/2002/xforms"
         xmlns:xs="http://www.w3.org/2001/XMLSchema"
         xmlns:xxf="http://orbeon.org/oxf/xml/xforms"
         xmlns:fr="http://orbeon.org/oxf/xml/form-runner"
         xmlns:xbl="http://www.w3.org/ns/xbl"
         xmlns:xxbl="http://orbeon.org/oxf/xml/xbl"

         xmlns:acme="http://www.acme.com/xbl">

    <xbl:binding element="acme|form-settings" id="acme-form-settings">
        <xbl:handlers>
            <xbl:handler event="fb-initialize" phase="target">

                <!-- Example: access the form instance root -->
                <xf:var name="root" value="event('form-instance')"/>

                <!-- Further initialization -->

            </xbl:handler>
            <xbl:handler event="fb-apply" phase="target">

                <!-- Example: access the form instance root -->
                <xf:var name="root" value="event('form-instance')"/>

                <!-- Further code to save settings  -->

            </xbl:handler>
        </xbl:handlers>
        <!-- Local models -->
        <xbl:implementation>
            <xf:model>

                <!-- Local instance -->
                <xf:instance>
                    <some-local-instance/>
                </xf:instance>

                <!-- Further model content -->

            </xf:model>
        </xbl:implementation>
        <!-- View template -->
        <xbl:template>
            <xh:div>
                This will appear as the tab's content.
            </xh:div>
        </xbl:template>
    </xbl:binding>

</xbl:xbl>
```

### Responding to events

#### Events dispatched

Form Builder dispatches events to the component, following a predefined lifecycle:

- `fb-initialize` is dispatched to initialize the tab when the dialog shows.
- `fb-apply` is dispatched to save the settings, if any, to the form definition.

Handlers for these events can access the form definition and read from / write to it. Component authors have to be
very careful not damaging the form definition in the process.

#### Event parameters

`fb-initialize` and `fb-apply` both take the following parameters:

Parameter Name|Type|Value
---|---|---
`form` | `element(xh:html)` | root element of the form definition
`form-instance` | `element(form)` | root element of the form definition's form instance
`form-metadata` | `element(metadata)` | root element of the form definition's form metadata

## Custom Control Settings tab

### Setup

In order to add a custom Control Settings tab, the following property must be set to a non-blank value:


```xml
<property
  as="xs:string"
  name="oxf.fb.extension.control-settings"
  value="acme:control-settings"/>
```

Here, the value `acme:control-settings` refers to the XBL component implementing the custom tab:

- `acme` is the prefix you have mapped with the `oxf.xforms.xbl.mapping.acme` property above
- `control-settings` is the name you give your XBL component (it doesn't have to be `control-settings`)

### The XBL component

You then create the file implementing the component under:

```
WEB-INF/resources/xbl/acme/control-settings/control-settings.xbl
```

For an example template, see above for `acme:form-settings`.

### Responding to events

#### Events dispatched

Form Builder dispatches the following events to the component:

- `fb-initialize` is dispatched to initialize the tab when the dialog shows.
- `fb-apply` is dispatched to save the settings, if any, to the form definition.

Handlers for these events can access the form definition and read from / write to it. Component authors have to be
very careful not damaging the form definition in the process.

#### Event parameters

`fb-initialize` and `fb-apply` both take the following parameters:

Parameter Name|Type|Value
---|---|---
`form` | `element(xh:html)` | root element of the form definition
`form-instance` | `element(form)` | root element of the form definition's form instance
`form-metadata` | `element(metadata)` | root element of the form definition's form metadata
`data-holders` | `element()*` | all data holders for the given control

In addition, `fb-initialize` takes the following parameters:

Parameter Name|Type|Value
---|---|---
`original-control-id` | `xs:string` | original control id, such as `first-name-control`
`original-control-name` | `xs:string` | original control name, such as `first-name`

In addition, `fb-apply` takes the following parameters:

Parameter Name|Type|Value
---|---|---
`control-name` | `xs:string` | new control name, such as `first-name`

Between `fb-initialize` and `fb-apply`, the control name (and id) might have been changed in the dialog by the user.
The `original-control-name` and `control-name` parameters reflect that change when needed.

## See also

- [[Automatic inclusion of XBL bindings|XForms-~-Configuration-Properties#automatic-inclusion-of-xbl-bindings]]
- [[XBL documentation|XForms ~ XBL]]
