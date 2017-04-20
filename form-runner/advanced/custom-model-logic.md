# Custom model logic

<!-- toc -->

## Availability

[SINCE Orbeon Forms 4.4]

*NOTE: This is an advanced feature which requires XForms knowledge.*

## Introduction

It can be useful to be able to define custom model logic, either for a particular form or shared among a number of forms. For this:

1. Place your XML file with your custom model logic under one of the following recommended locations:
    - `WEB-INF/resources/forms/resources`: custom model logic for all forms
    - `WEB-INF/resources/forms/APP/resources`: custom model logic for app name APP
    - `WEB-INF/resources/forms/APP/FORM/resources`: custom model logic for app name APP and form name FORM
2. Define the `oxf.fr.detail.model.custom` property to point to the file(s) you added.

    ```xml
    <property
      as="xs:anyURI"
      name="oxf.fr.detail.model.custom.*.*"
      value="oxf:/forms/resources/model.xml"/>
    ```

    The value of the property is a URL which points to a file containing the custom logic. The format of the file is that of an XForms model. The custom content is included in your form's main model, which is identified by `fr-form-model`. This means that you have access to your form data in instance `fr-form-instance`, for example.

## Separate file vs. logic directly within the form definition

The `oxf.fr.detail.model.custom` property makes your custom model logic separate from the rest of your form definition.

This has the following benefits:

- you can easily share that logic between more than one form definition
- you can rest assured that changed to your form definition with Form Builder will not impact your custom model logic

We can note the following drawbacks:

- you need to place a file on the server as it is not yet possible to upload custom model logic via Form Builder directly
- custom logic needs to be migrated manually from environment to environment

## Example

Here is a very simple example:

```xml
<xf:model xmlns:xf="http://www.w3.org/2002/xforms">
    <xf:message event="foobar">
        Your email is: <xf:output value="instance()//email"/>.
    </xf:message>
</xf:model>
```

Here we define custom XForms that reacts to an event called `foobar` and displays a message to the user.

We can create a [custom button](../../form-runner/advanced/buttons-and-processes/README.md) that dispatches that event with the following properties:

```xml
<!-- Place custom button on all `acme` forms -->
<property
  as="xs:string"
  name="oxf.fr.detail.buttons.acme.*"
  value="whizz"/>

<!-- Label for the button -->
<property
  as="xs:string"
  name="oxf.fr.resource.acme.*.en.detail.buttons.whizz"
  value="Whizz"/>

<!-- React to button activation by dispatching the event -->
<property
  as="xs:string"
  name="oxf.fr.detail.process.whizz.acme.*"
  value='xf:dispatch("foobar")'/>

<!-- Specify the custom model content -->
<property
  as="xs:anyURI"
  name="oxf.fr.detail.model.custom.acme.*"
  value='oxf:/forms/acme/whizz.xml'/>
```

The result:

![Form Runner message](../images/your-email-is.png)
