# Buttons and processes

<!-- toc -->

## Availability

This feature is available with Orbeon Forms 4.2 and newer.

## Introduction

This documentation describes how to configure the behavior of the buttons that appear at the bottom of the Form Runner detail page, whether in `new`, `edit` or `view` mode. Here is an example of such buttons:

![Example of Form Runner buttons](../w9-form-buttons.png)

## What is a process?

A "process" consists in a list of actions, or steps, which are executed by Form Runner, typically when the user presses a button. For example, a process might say: "when the user presses this button, validate the form data, then save it to the database".

With Orbeon Forms, processes are configurable. They are defined separately from buttons, but can be linked to buttons. Orbeon Forms comes with predefined processes for some common tasks, such as saving form data, but you can write your own processes.

## Associating a process with a button

A process is automatically associated with a button by name when using the following properties:

- `oxf.fr.detail.buttons`
- `oxf.fr.detail.buttons.inner`
- `oxf.fr.detail.buttons.view`

For example:

```xml
<property
  as="xs:string"
  name="oxf.fr.detail.buttons.orbeon.controls"
  value="refresh summary clear pdf save-final wizard-prev wizard-next review"/>
```

Here the following buttons get associated with processes of the same name defined in separate properties:

- `refresh`
- `summary`
- `save-final`
- `wizard-prev`
- `wizard-next`
- `review`

*NOTE: As of Orbeon Forms 4.2, the `clear` and `pdf` buttons are not implemented as processes but handled directly by Form Runner.*

## Customizing existing processes

So how do you customize processes? Say you want to specify a couple of buttons on your "acme/hr" form. Like above, you define a property:

```xml
<property
  as="xs:string"
  name="oxf.fr.detail.buttons.acme.hr"
  value="save-draft send"/>
```

This places a `save-draft` and `send` buttons on the page. Their default labels are "Save" and "Send". Each button is automatically associated with processes of the same names, `save-draft` and `send`. These particular buttons and process names are standard, but we can customize them specifically for our form. Again, this is done with a property:

```xml
<property as="xs:string" name="oxf.fr.detail.process.send.acme.hr">
    require-valid
    then email
    then send("http://example.org/")
    then navigate("/success")
    recover navigate("/failure")
</property>
```

Button labels can be overridden as well:

```xml
<property
  as="xs:string"
  name="oxf.fr.resource.*.*.en.buttons.send"
  value="Fancy Send"/>
```

With this, we have overridden the functionality of the "Send" button specifically for the "acme/hr" form.

Another approach would be to create an entirely custom button instead of reusing the name `send`. Just pick a button/process name, say `foo`, and then:

- add `foo` to the list of buttons for the form
- define a button label for `foo`
- define a process for `foo`

*NOTE: With Orbeon Forms 4.5.x and earlier, the property must be `oxf.fr.resource.*.*.en.detail.buttons.send`. With Orbeon Forms 4.6 and newer, the `detail` token can and should be omitted.*

## What next?

You can learn more about:

- the [process syntax](syntax.md)
- the actions you can use, including:
    - [core actions](actions-core.md)
    - [Form Runner actions](actions-form-runner.md)
    - [XForms actions](actions-xforms.md)
- [predefined buttons, processes and dialogs](predefined.md)

## See also

- This blog post for an introduction to the feature: [More powerful buttons](http://blog.orbeon.com/2013/04/more-powerful-buttons.html)
- The predefined configuration properties in [`properties-form-runner.xml`](https://github.com/orbeon/orbeon-forms/blob/master/src/resources-packaged/config/properties-form-runner.xml)
