# Custom dialogs and model logic 

## Introduction

*NOTE: This is an advanced feature which requires XForms knowledge.*

It can be useful to be able to define custom dialogs and/or custom model logic, either for a particular form or shared among a number of forms. For this, place your XML files with your custom dialogs and/or model logic under one of the following recommended locations:

- `WEB-INF/resources/forms/resources`: if applies to all forms
- `WEB-INF/resources/forms/APP/resources`: if applies to all form in a specific app
- `WEB-INF/resources/forms/APP/FORM/resources`: if applies to a specific with a given app and form name

Then:

- [SINCE Orbeon Forms 4.4] To point to a file containing your own model logic, define the `oxf.fr.detail.model.custom.*.*` property as shown in the example below. The value of the property is a URL which points to a file containing the custom logic. The format of the file is that of an XForms model. The custom content is included in your form's main model, which is identified by `fr-form-model`. This means that you have access to your form data in instance `fr-form-instance`, for example.
- [SINCE Orbeon Forms 2017.2] To point to files containing a dialog, define the `oxf.fr.detail.dialogs.custom.*.*` property, as shown in the example below. Each file listed in the value of the property is expected to have a `<xxf:dialog>` as the root element.

```xml
<property
    as="xs:anyURI"
    name="oxf.fr.detail.model.custom.*.*"
    value="oxf:/forms/resources/my-model.xml"/>
<property
    as="xs:anyURI"
    name="oxf.fr.detail.dialogs.custom.*.*"
    value="oxf:/forms/resources/my-dialog.xml"/>
```

## Separate file vs. code included within the form definition

The properties mentioned earlier make your dialogs and custom model logic separate from the rest of your form definition. This has the following benefits:

- You can easily share that logic between form definitions.
- You can rest assured that changed to your form definition with Form Builder will not impact your custom model logic.

We can note the following drawbacks:

- You need to place a file on the server as it is not yet possible to upload custom model logic via Form Builder directly.
- Custom logic needs to be migrated manually from environment to environment.

## Example of custom model logic

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
