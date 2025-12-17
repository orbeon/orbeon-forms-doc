# Custom modes

## Availability

[\[SINCE Orbeon Forms 2025.1\]](../../release-notes/orbeon-forms-2025.1.md)

_NOTE: This is an advanced feature which may require programming skills._

## Introduction

The Form Runner _Detail page_ is the page where users create, edit, and view form data. Each such function is referred to as a _mode_. For more, see [Form Runner detail page modes](detail-page-modes.md).

Orbeon Forms 2025.1 introduces the ability to define _custom modes_ for the Form Runner Detail page. This allows you to create different views of the same form data. You do so by writing a custom component, while Orbeon Forms handles data management, state keeping, and permissions for you.

Custom modes are supported by functions to:

* navigate _away_ from Orbeon Forms to external pages, or pages embedded in iframes
* navigate _back_ to Orbeon Forms, while keeping the form data and other state information

However, custom modes can be used separately from these functions, and conversely these functions can be used without custom modes.

Here is an example of workflow involving standard modes, a custom mode, an external page or service, and navigation back to Orbeon Forms:

![Custom modes workflow](../../.gitbook/assets/custom-modes-workflow.webp)

An important use case for this is calling external services such as:

* payment providers
* signature providers
* and more.

## Defining custom modes

A custom mode has to be defined before it can be used. You do so with the `oxf.fr.detail.custom-modes.*.*` property. The property value is a JSON array of objects, each object defining a custom mode.

As usual, with wildcards representing the application name and form name, you can define custom modes for all forms, for all forms in a specific application, or for a specific form.

```xml
<property as="xs:string" name="oxf.fr.detail.custom-modes.*.*" xmlns:acme="http://www.acme.com/xbl">
    [
      {
        "name": "acme:demo-sign",
        "mode-type": "readonly",
        "persistence": false
      }
    ]
</property>
```

* The `name` attribute is the name of the custom mode.
  * This must be an XML qualified name with a prefix and a colon `:`. Unqualified names are reserved for builtin modes.
  * The prefix, here `acme`, must be defined in the `xmlns` attribute of the property or the properties file, here `xmlns:acme="http://www.acme.com/xbl"`.
  * A matching XBL component of the same name will be used as view component, here `acme:demo-sign`.
* The `mode-type` attribute defines the type of the custom mode. It can be one of the following values:
  * `readonly`: the form is displayed in read-only mode, and users cannot change or save any data.
  * `edition`: the form is displayed in edit mode, and users can change data and save data, given appropriate permissions.
* The `persistence` attribute defines whether the mode is allowed to read/write data from/to the persistence layer.
  * If `true`, the mode is allowed to access the persistence layer, given appropriate permissions.
  * If `false`, the mode is not allowed to access the persistence layer.
    * In this case, form data typically comes from navigation from another mode, or from state restoration, and is passed to another mode or saved through `fr:save-state()`.

Custom mode names appear in URLS, for example:

```
/fr/orbeon/feedback/fr:demo-sign/9eff349bfd95aab8d4d5e048bd25a815
```

## Saving state

Form state includes:

* the current form data
* the current application name, form name, and form version
* the current mode
* the current workflow stage
* current form permissions
* and more

When navigating away from Orbeon Forms, you typically want to save that state, so that when the user comes back to Orbeon Forms, they can continue where they left off.

A new function is provided to save the current state of the form data, and other information, and return a token which can be used later to restore that state:

```xpath
fr:save-state() as xs:string

fr:save-state(
    $continuation-mode as xs:string?
) as xs:string

fr:save-state(
    $continuation-mode as xs:string?,
    $continuation-workflow-state as xs:string?
) as xs:string
```

The parameters are optional:

* `$continuation-mode`:
  * If provided, when the user comes back to Orbeon Forms, they will be put in that mode.
  * If not provided, they will be put in the same mode as when `fr:save-state()` was called.
* `$continuation-workflow-state`:
  * If provided, when the user comes back to Orbeon Forms, they will be put in that workflow state.
  * If not provided, they will be put in the same workflow state as when `fr:save-state()` was called.

This function can be called from a button process, for example:

```xml
<property as="xs:string"  name="oxf.fr.detail.process.demo-external-sign.*.*">
    navigate("https://example.org/signature?token={encode-for-uri(fr:save-state('view'))}")
</property>
```

A side effect of calling this function is that the form state, as described above, is serialized, encrypted, and temporarily saved into a server-side store.

In addition, a token is created, which allows later restoration of the state (see below).

The token includes an expiration time, which is 15 minutes by default. The idea is that the user will leave Orbeon Forms temporarily to perform a task, but will come back to Orbeon Forms relatively soon in most cases. This can be important as storing the state requires some resources on the server side.

You can change the token expiration time in minutes by setting the following property:

```xml
<property 
    as="xs:integer" 
    name="oxf.fr.state-token.validity.*.*"                               
    value="15"/>
```

## Restoring state

Orbeon Forms provides a new callback endpoint:

```
/fr/callback?token=...
```

This must be reached with an HTTP `GET`. When called, the callback:

* checks that a valid, non-expired token is provided
* obtain the state from the store
* navigates to the Detail page, restoring the state that was stored
  * is a continuation mode was specified with `fr:save-state()`, Form Runner switches to that mode, otherwise uses the same mode as when `fr:save-state()` was called
  * if a continuation workflow stage was specified with `fr:save-state()`, Form Runner switches to that workflow state, otherwise uses the same workflow stage as when `fr:save-state()` was called

## Implementing a view component

The mode's qualified name determines the name of the XBL view component. Like for any custom component, an explicit mapping from prefix to namespace must be provided, for example:

```xml
<property 
    as="xs:string" 
    name="oxf.xforms.xbl.mapping.acme" 
    value="http://www.acme.com/xbl" />
```

See also [XBL Bindings](../../xforms/xbl/bindings.md) for more about defining custom components.

You place your component files in the `xbl` directory, for example:

```
WEB-INF/resources/xbl/acme/demo-sign/demo-sign.xbl
```

Here is a simple example of a view component implemented by a `demo-sign.xbl`:

```xml
<xbl:xbl
    xmlns:xh="http://www.w3.org/1999/xhtml"
    xmlns:xf="http://www.w3.org/2002/xforms"
    xmlns:xs="http://www.w3.org/2001/XMLSchema"
    xmlns:xxf="http://orbeon.org/oxf/xml/xforms"
    xmlns:fr="http://orbeon.org/oxf/xml/form-runner"
    xmlns:xbl="http://www.w3.org/ns/xbl"
    xmlns:xxbl="http://orbeon.org/oxf/xml/xbl"
    xmlns:acme="http://www.acme.com/xbl">
    <xbl:binding
        id="acme-demo-sign"
        element="acme|demo-sign"
        xxbl:container="div"
        xxbl:mode=""
    >
        <xbl:template>
            <xh:div>
                This is the signature page.
            </xh:div>
        </xbl:template>
    </xbl:binding>
</xbl:xbl>
```

This component simply displays a message. In a real-world scenario, you would do more, for example:

* show the values of some form fields
* embed an `iframe` or other content with JavaScript
* call external services
* and more.

## The `change-mode()` action

The `change-mode()` action allows you to navigate to any mode, including custom modes. Examples:

* `change-mode("edit")`: This action navigates to the `edit` mode from any mode, and is identical to the `edit` action.
* `change-mode("view")`: This action navigates to the `view` mode from any mode, and is identical to the `review` action.
* `change-mode("new")`: This action navigates to the `new` mode from any mode.
* `change-mode("acme:demo-sign")`: This action navigates to the custom `acme:demo-sign` mode from any mode.

_NOTE: It is not possible to use this action to produce a `pdf` or `tiff` view of the form data. Use the `open-rendered-format()` action instead._

## Configuring buttons

### Navigating to a custom mode

Say you are on the `new` or `edit` page, and want to add a button to navigate to your custom signature page. You can do so by defining a custom `demo-sign` button using the `oxf.fr.detail.buttons.*.*` property. For example:

```xml
<property as="xs:string"  name="oxf.fr.detail.buttons.*.*">
    demo-sign
</property>
```

You must also assign a label to that button:

```xml
<property as="xs:string" name="oxf.fr.resource.*.*.en.buttons.demo-sign">
    &lt;i class="fa fa-fw fa-file-signature"&gt;&lt;/i&gt; Demo Sign
</property>
```

Finally, assign a simple process to that button which changes the mode to your custom mode:

```xml
<property as="xs:string"  name="oxf.fr.detail.process.demo-sign.*.*">
    change-mode(mode = "acme:demo-sign")
</property>
```

As usual, the wildcards in the property names can be replaced by specific application and form names.

The result will be a button on the `new` or `edit` page which, when clicked, will switch to your custom signature page, while keeping data and state.

![Button to navigate to a custom mode page](../../.gitbook/assets/custom-modes-demo-sign-button.webp)

### Buttons on custom mode pages

You configure buttons on custom mode pages using the `oxf.fr.detail.buttons.*.*.*` property. For example, to add a button which calls `fr:save-state()` and navigates to an external signature provider, you could use:

```xml
<property as="xs:string"  name="oxf.fr.detail.buttons.acme:demo-sign.*.*">
    new demo-external-sign
</property>
```

The `new` button can be configured to navigate back to the `new` mode. The `demo-external-sign` button can be configured to call `fr:save-state()` and navigate to an external signature provider.

```xml
<property as="xs:string"  name="oxf.fr.detail.process.new.*.*">
    change-mode(mode = "new")
</property>

<property as="xs:string" name="oxf.fr.resource.*.*.en.buttons.demo-external-sign">
    &lt;i class="fa fa-fw fa-file-signature"&gt;&lt;/i&gt; Demo External Sign
</property>

<property as="xs:string"  name="oxf.fr.detail.process.demo-external-sign.*.*">
    navigate("https://example.org/signature?token={encode-for-uri(fr:save-state('view'))}")
</property>
```

A very important part is the call to `fr:save-state('view')`, which saves the current state, and specifies that when the user comes back to Orbeon Forms, they should be put in the `view` mode.

Note the `encode-for-uri()` call, which ensures that the token is properly encoded for use in a URL.

The external service provider must be configured to call back to Orbeon Forms at the `/fr/callback?token=...` endpoint. When the user comes back to Orbeon Forms, they will be put in the `view` mode, and see the form data in read-only mode.

## Security considerations

Form state is sensitive information, and Orbeon Forms makes sure to handle it securely:

* the form state is encrypted on the server using the encryption password
* the server store is configured to expire items after a certain time
* the token does not expose sensitive information
* the token can only be used once; a second use returns an error
* form state is removed from the server store once it has been used

## Example

As an example of use of the custom mode logic, the built-in [Confirmation page](confirmation-page.md) feature uses a custom mode to display the confirmation message after form submission. You can look at the source code of that feature for inspiration, see [`confirmation.xbl`](https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/xbl/orbeon/confirmation/confirmation.xbl).

## See also

* [Confirmation page](confirmation-page.md)
* [Detail page configuration properties](../../configuration/properties/form-runner-detail-page.md)
* [Simple processes](../advanced/buttons-and-processes/)
