> [[Home]] ▸ [[Form Builder|Form Builder]]

## Availability

Since Orbeon Forms 4.11.

## Introduction

Form Builder exposes the following extension API:

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

For more on XBL components, see [XBL - Guide to Using and Writing XBL Components](http://wiki.orbeon.com/forms/doc/developer-guide/xbl-components-guide).

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

Form Builder dispatches the following events to the component:

- `fb-initialize`: initialize the tab when the dialog shows
    - `form` parameter: points to the root element of the form definition
    - `form-instance` parameter: points to the root element of the form definition's form instance
    - `form-metadata` parameter: points to the root element of the form definition's form metadata
- `fb-apply`: save the settings, if any, to the form definition:
    - `form` parameter: points to the root element of the form definition
    - `form-instance` parameter: points to the root element of the form definition's form instance
    - `form-metadata` parameter: points to the root element of the form definition's form metadata

Handlers for these events can access the form definition and read from/ write to it. Component authors have to be
very careful not damaging the form definition in the process.

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

For an example template, see above for `acme:form-settings`.

### Responding to events

Form Builder dispatches the following events to the component:

- `fb-initialize`
    - `form` parameter: points to the root element of the form definition
    - `form-instance` parameter: points to the root element of the form definition's form instance
    - `form-metadata` parameter: points to the root element of the form definition's form metadata
    - `original-control-id` parameter: the original control id, such as `first-name-control`
    - `original-control-name` parameter: the original control name, such as `first-name`
    - `data-holders` parameter: sequence of all data holders for the given control
- `fb-apply`
    - `form` parameter: points to the root element of the form definition
    - `form-instance` parameter: points to the root element of the form definition's form instance
    - `form-metadata` parameter: points to the root element of the form definition's form metadata
    - `control-name` parameter : the new control name, such as `first-name`
    - `data-holders` parameter : sequence of all data holders for the given control

Between `fb-initialize` and `fb-apply`, the control name (and id) might have been changed in the dialog by the user.
So `original-control-name` and `control-name` reflect that: they might be identical or different.