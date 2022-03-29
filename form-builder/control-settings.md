# Control settings

## Introduction

The Control Settings dialog allows controlling all the aspects the a control besides its label and hint. The dialog
has several tabs, detailed below.

### Basic Settings

![Basic Settings tab](images/control-settings.png)

#### Basic options

The control *name* specifies a identifier for the control, unique in the entire form (except [Section Templates](section-templates.md)). The
identifier is used for the following:

- to refer to the control value from formulas, using the variable notation `$foo` where `foo` is the control name
- to determine an XML element name when the form data is represented as XML

If a control name is not explicitly specified, Form Builder assigns a default name, such as `control-42`.

A control name can be changed, provided it doesn't collide with another control name (an error will show otherwise). [SINCE Orbeon Forms 2019.1] When renaming a control, all dependent [formulas](formulas.md) that use the `$foo` notation, as well as all dependent [templates](template-syntax.md) and [actions](actions.md), including [when using the new syntax](actions-syntax.md).

##### Main options

The following options are available:

- __Show on Summary page:__
    - When selected, the control value is visible as a Summary page column.
- __Show in Search:__
    - When selected, the control value is searchable in the Summary page.
- __Index control:__
    - SINCE Orbeon Forms 2021.1
    - Index the value of this control even if it's not shown or searchable from the Summary page. This makes the control accessible from the [search API](../form-runner/api/persistence/search.md).
- __Encrypt data at rest:__
    - SINCE Orbeon Forms 2018.1
    - This is an Orbeon Forms PE feature.
    - See [Field-level encryption](/form-builder/field-level-encryption.md) for details.

##### Email options
    
The following email options are available:

*NOTE: SINCE Orbeon Forms 2018.2, these options are in a separate list.*
    
- __Email recipient:__
    - When selected, the control is used to determine an email recipient ("To:") when the form data is sent by email.
    - If more than one non-blank email addresses is found, they are all included as email recipients. In addition, the `oxf.fr.email.to` property is used.
    - A single control value can contain more than one email address, separated by commas (`,`) or spaces.
- __Email carbon copy recipient:__
    - SINCE Orbeon Forms 2017.1
    - When selected, the control is used to determine a carbon copy email recipient ("Cc:") when the form data is sent by email.
    - If more than one non-blank email addresses is found, they are all included as email recipients. In addition, the `oxf.fr.email.cc` property is used.
    - A single control value can contain more than one email address, separated by commas (`,`) or spaces.
- __Email blind carbon copy recipient:__
    - SINCE Orbeon Forms 2017.1
    - When selected, the control is used to determine a blind carbon copy email recipient ("Bcc:") when the form data is sent by email.
    - If more than one non-blank email addresses is found, they are all included as email recipients. In addition, the `oxf.fr.email.bcc` property is used.
    - A single control value can contain more than one email address, separated by commas (`,`) or spaces.
- __Email sender:__
    - SINCE Orbeon Forms 2017.1
    - When selected, the control is used to determine an email sender ("From:") when the form data is sent by email.
    - Only *one* "From:" email address is used, specifically the first non-blank address selected in the form. If no such address is found the `oxf.fr.email.from` property is used.
- __Email reply-to:__
    - SINCE Orbeon Forms 2020.1
    - When selected, the control is used to determine a reply-to address ("Reply-To:") when the form data is sent by email.
    - Only *one* "Reply-To:" email address is used, specifically the first non-blank address selected in the form. If no such address is found the `oxf.fr.email.reply-to` property is used.
- __Exclude from email body:__
    - SINCE Orbeon Forms 2018.1
    - When using "All Control Values" in an [email body template](email-settings.md), controls selected with this checkbox will be
      *excluded* and omitted from the email body.
- __Include as email attachment__:
    - SINCE Orbeon Forms 2016.1
    - this option only shows for file and image attachments
    - when the property `oxf.fr.email.attach-files` is set to `selected`, only file and image attachments with this option checked are attached to the email
- __Show in email subject:__
    - DEPRECATED SINCE Orbeon Forms 2018.1: Use a [template for the subject](email-settings.md) instead.
    - When selected, the control value is used as part of the subject of the email when the form data is sent by email.
    - If more than one non-blank values are found, they are all included in the email subject, comma-separated.

#### Name of the PDF field

[SINCE Orbeon Forms 2018.2]

This informational field shows the name of the field to use when using a PDF template.

See also [PDF templates](/form-builder/pdf-templates.md).

#### Custom CSS classes

The "Custom CSS Classes" field allows adding CSS classes which will be placed on the control in the resulting HTML. This can be used for custom styling.

CSS classes don't directly control the style. Instead, they are space-separated tokens, which can then be referred to in [your own CSS files](/form-runner/styling/css.md).

You could have for example:

```
my-name-field my-highlight
```

In your custom CSS file, you could refer to these classes with a selector, for example:

```css
.orbeon .my-name-field.my-highlight input {
  border-color: blue;
}
```

*NOTE: The above uses example `my-` prefixes as it is a good idea to prefix CSS classes to avoid clashes with other CSS classes on the page.*

#### Control appearance

[SINCE Orbeon Forms 4.10]

Some controls support more than one appearance. For example, a single selection control can appear as a dropdown menu,
or as radio buttons. When available, the "Control Appearance" selector allows selecting and changing the appearance of
the control.

See also [How the new Form Builder Appearance Selector Works](https://blog.orbeon.com/2015/06/how-new-form-builder-appearance.html).

#### Custom control settings

Some controls have custom settings. For example:

![Custom Control Settings](images/control-settings-custom-properties.png)

The following controls have custom settings:

- [Number](/form-runner/component/number.md#form-builder-customer-settings)
- [Currency](/form-runner/component/currency.md#form-builder-customer-settings)
- [US Phone Number](/form-runner/component/us-phone.md#form-builder-customer-settings)
- Date
- Time
- Date and Time
- [Dropdown Date](/form-runner/component/dropdown-date.md#form-builder-customer-settings)
- Dropdown
- Dropdown with Search
- [File Attachment](/form-runner/component/attachment.md#form-builder-customer-settings)
- [Image Attachment](/form-runner/component/image-attachment.md#form-builder-customer-settings) [SINCE Orbeon Forms 2022.1]

See also [Control metadata for the Control Settings dialog](metadata.md#control-metadata-for-the-control-settings-dialog)

#### Automatic PDF options

[SINCE Orbeon Forms 2021.1]

When producing a PDF file, single-selection controls like the "Static Dropdown" normally just output the selected value. However, in some cases, it is useful for the user to see all the available options.

You can achieve this with the "Automatic PDF" options in the "Control Settings" dialog for single-selection controls. This option is hierarchical: you can set it at the field level, form level, or globally. They are:

- __Use default__: use the form settings, and if they also have "Use default" use the property (see below)
- __Show the selected value only__: the label for the selected value shows
- __Show all values as radio buttons__: all possible values show as radio buttons and the selected value, if any, shows selected 

![Automatic PDF options](/form-builder/images/control-settings-pdf-options.png)

The supported properties are:

- `oxf.xforms.xbl.fr.dropdown-select1.pdf-appearance.*.*`
- `oxf.xforms.xbl.fr.'dropdown-select1-search'.pdf-appearance.*.*`

The possible values for each property are:

- empty: show the selected value only (default)
- `full`: show all values as radio buttons

Example to change the default the "Static Dropdown" control to showing all values in the automatic PDF:

```xml
<property 
    as="xs:string" 
    name="oxf.xforms.xbl.fr.dropdown-select1.pdf-appearance.*.*"
    value="full"/>
```

The global default is "Show the selected value only".

### Validations and alerts

![Validations and alerts tab](images/control-settings-validations.png)

See [Form Builder Validation](validation.md) for details.

### Formulas

![Formulas tab](images/control-settings-formulas.png)

See [Formulas](formulas.md) for details.

[SINCE Orbeon Forms 2018.2]

The "Yes" and "No" options have their own radio button. Select "Formula" to enter a dynamic "Visibility" or "Read-Only" formula.

### Explanatory Text

[SINCE Orbeon Forms 2019.1]

For the Explanatory Text control only, you can modify the text in this location, including making the text dynamic using templates as is the case for the Label, Hint and Help Message.

![Explanatory Text tab](images/control-settings-explanatory-text.png)

### Label and Hint

[SINCE Orbeon Forms 2017.2]

In addition to setting a control's label and hint in place in the form area, you can also set and update them in this tab. You can switch between plain text and HTML text as well. The "Previous" and "Next" buttons allow quick navigation between controls.

![Label tab](images/control-settings-label-hint.png)

### Help Message

![Help tab](images/control-settings-help.png)

This allows specifying some help text, which can be plain text or rich text when the "Use HTML" checkbox is selected.

The help message is available at runtime through a help icon positioned next to the control. By default, the icon opens a pop-up containing the help text. In *noscript* mode (removed since Orbeon Forms 2018.1), the icon links to a help section at the bottom of the form.

The help text is localizable.

See also [Improving how we show help messages](https://blog.orbeon.com/2014/01/improving-how-we-show-help-messages.html).

### Dynamic labels, hints and help messages

[SINCE Orbeon Forms 2018.1]

Controls support *dynamic* labels, hints, and help messages. This means that, instead of being specified once and for all at form design time, labels, hints and help messages can incorporate dynamic parts such as control values and other custom expressions.

For more, see [Template syntax](template-syntax.md).

## See also

- [Control metadata for the Control Settings dialog](metadata.md#control-metadata-for-the-control-settings-dialog)
- [Form Builder Validation](validation.md)
- [Formulas](formulas.md)
- [Template syntax](template-syntax.md)
- Blog posts
    - [Enhanced validation in Form Builder and Form Runner](https://blog.orbeon.com/2013/07/enhanced-validation-in-form-builder-and.html)
    - [Improving how we show help messages](https://blog.orbeon.com/2014/01/improving-how-we-show-help-messages.html)
    - [How the new Form Builder Appearance Selector Works](https://blog.orbeon.com/2015/06/how-new-form-builder-appearance.html)
    - [Improved constraints on attachments uploads](https://blog.orbeon.com/2017/04/improved-constraints-on-attachments.html)
    - [More flexible email senders and recipients](https://blog.orbeon.com/2017/05/more-flexible-email-senders-and.html)
