# Email settings

## Availability

This feature is available since Orbeon Forms 2018.1.

This is an Orbeon Forms PE feature.

## Introduction

The "Email Settings" button under the "Advanced" tab in the toolbox opens the "Email Settings" dialog.

<img src="images/advanced-menu.png" width="245">

The Email Settings allow you to specify, for the given form:

- an email subject
- an email body

In addition, subject and body can optionally be parametrized to fill placeholders with control values or
the result of formulas (specified as XPath expressions).

If the form definition has more than one language, subject and body are specified by language.

## Email subject

You can specify an email subject by deselecting the "Template from Properties" checkbox. When that checkbox is selected, the subject template [comes from a property](../configuration/properties/form-runner/detail-page/email.md#email-subject-and-body). This is the default for backward compatibility.

```xml
<property 
    as="xs:string"
    name="oxf.fr.resource.*.*.en.email.subject"
    value="Here is your confirmation: "/>
```

![Email Subject](images/email-settings-subject.png)

## Email body


You can specify an email body by deselecting the "Template from Properties" checkbox. When that checkbox is selected, the body template [comes from a property](../configuration/properties/form-runner/detail-page/email.md#email-subject-and-body). This is the default for backward compatibility.

```xml
<property 
    as="xs:string"
    name="oxf.fr.resource.*.*.en.email.body"
    value="Hi, here is an email from Orbeon Forms!"/>
```

The email body can be set in plain text or in rich text (HTML) with the relevant checkbox.

![Email Body](images/email-settings-body.png)

## Template syntax

You set a template placeholder in the subject by using the exact syntax:

```
{$foo}
``` 

where `foo` is a name which must match one of the template parameters configured below. You can add new parameters
with the "+" icon and remove them with the "-" icon.

The following options are available for each template parameter:

- Parameter name: this must be unique among parameters.
- Parameter value:
    - __Control Value:__ get the value of a control.
    - __XPath Expression:__ calculated expression.
    - __All Control Values:__ the value of all form controls.
        - This is only available for the email body.
        - This is experimental as of Orbeon Forms 2018.1. 
    
*NOTE: An XPath expression runs in the context of the form data's root element. However, the [Form Runner function library](../xforms/xpath/extension-form-runner.md) is not yet available to the expressions as of Orbeon Forms 2018.1.*

## Localization

When the form definition has more than one language, a language selector appears and allows switching between
languages for setting a subject or body in the given language.

Note that template parameters are not localized and are used no matter what language is selected. However, a template
in a given language may omit references to any or all template parameters.

## See also

- [Email subject and body](../configuration/properties/form-runner/detail-page/email.md#email-subject-and-body)
