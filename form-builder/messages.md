# Messages

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

![Messages dialog](images/messages.png)

## Purpose

Some of the [Form Runner actions](/form-runner/advanced/buttons-and-processes/actions-form-runner.md) display messages to users. For instance, when a form is saved, a message is displayed to confirm that the form was saved successfully. The Messages dialog allows you to customize those messages directly in Form Builder, instead of using properties such as `oxf.fr.resource.*.*.en.detail.messages.save-success`.

## Usage

You can open the Messages dialog by clicking on the "Messages" button, under the "Advanced" tab, in the toolbox.

<img src="images/advanced-menu.png" width="245">

Each message must have a language, a name, and a value.

The message name can be one of the pre-defined names present in [Form Runner's `resources.xml`](https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/i18n/resources.xml) (e.g. `save-success` or `upload-error`) or a custom name, which can be entered by selecting "Other".

When a message is defined in the Messages dialog, it overrides any message defined in a property.

## Supported actions

The following [Form Runner actions](/form-runner/advanced/buttons-and-processes/actions-form-runner.md) take a message as parameter:

- [`confirm`](/form-runner/advanced/buttons-and-processes/actions-form-runner.md#confirm)
- [`success-message`](/form-runner/advanced/buttons-and-processes/actions-form-runner.md#success-message-and-error-message)
- [`error-message`](/form-runner/advanced/buttons-and-processes/actions-form-runner.md#success-message-and-error-message)

For example, you can reference the pre-defined message `save-success` by calling the following action:

```
success-message(resource = "save-success")
```

Or simply:

```
success-message("save-success")
```

## Custom messages

When overriding existing processes or introducing custom processes in the properties, you can also reference custom messages that are not used internally by Form Runner. You can do so by selecting "Other" in the name dropdown and entering a custom name.

For example, you might want to define a custom `email-and-save` process to email and save your form, and display a custom `sent-and-saved` message (defined via the Messages dialog) upon success. This allows you to use a more specific message than the pre-defined `save-success` message.

```xml
<property as="xs:string" name="oxf.fr.detail.buttons.acme.foo">
    summary clear pdf review email-and-save
</property>

<property as="xs:string"  name="oxf.fr.detail.process.email-and-save.acme.foo">
    email(template = "email-template-name")
    then save
    then success-message("sent-and-saved")
</property>

<property
    as="xs:string"
    name="oxf.fr.resource.*.*.en.buttons.email-and-save"
    value="Email &amp; save"/>
```

## See also

- Blog post: [Customizing Form Runner messages directly from Form Builder](https://blog.orbeon.com/2023/10/customizing-form-runner-messages.html)