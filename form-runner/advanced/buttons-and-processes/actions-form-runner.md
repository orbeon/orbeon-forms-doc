# Form Runner actions

<!-- toc -->

## Introduction

These actions are specific to Form Runner. They allow you to validate, save and send data, in particular.

## validate

Validate form data.

- parameters
    - `level`: validation level to check: one of `error`, `warning`, or `info`
    - `property`: specifies a boolean property which, if `false`, skips validation (used for backward compatibility) [Orbeon Forms 4.2 only, removed in Orbeon Forms 4.3]
- result
    - success if data is valid
    - failure if data is invalid
    
[SINCE Orbeon Forms 2016.3]

When the validation mode is set to `explicit`, first update the validity of all controls with `explicit` validation and perform a refresh.

## wizard-update-validity

[SINCE Orbeon Forms 2016.3]

When the validation mode is set to `explicit`, first update the validity of all wizard pages up to the current wizard page included.

## pending-uploads

Check whether there are pending uploads.

- parameters
    - none
- result
    - success if there are no pending uploads
    - failure if there are pending uploads

## save

Save data and attachments via the persistence layer.

- steps
    - dispatch `fr-data-save-prepare` to `fr-form-model`
    - save attachments
    - save XML
    - switch to `edit` mode (be aware of [#1653](https://github.com/orbeon/orbeon-forms/issues/1653))
    - dispatch `fr-data-save-done` to `fr-form-model`
- parameters
    - `draft`: "true" if must be saved as a draft [SINCE Orbeon Forms 4.4]
    - `query`: additional query parameters to pass the persistence layer (is an XPath value template) [SINCE Orbeon Forms 4.6.1]

Example of use of the `query` parameter:

```xml
<property as="xs:string"  name="oxf.fr.detail.process.save-final.*.*">
    require-uploads
    then validate-all
    then save(query = "foo=bar&amp;title={//title}")
    then success-message("save-success")
    recover error-message("database-error")
</property>
```

The full URL, for attachments as well as for the XML data, is composed of:

- the URL pointing to the persistence layer path, including the file name
- the following URL parameter
    - `valid`: whether the data sent satisfies validation rules

*NOTE: The `save` action doesn't check data validity before running.*

Example:

    http://example.org/orbeon/fr/service/persistence/crud/
        orbeon/
        bookshelf/
        data/891ce63e59c17348f6fda273afe28c2b/data.xml?
        valid=true

## email

Send an email with optionally XML form data, attachments, and PDF.

- parameters: none
- properties used: [`oxf.fr.email.*`](../../../configuration/properties/form-runner.md#email-settings)

## send

This documentation is on a [separate page](actions-form-runner-send.md).

## set-data-status

[SINCE Orbeon Forms 4.7]

Set the status of the form data in memory.

- parameters
    - `status`: specifies the status of the data
        - `safe`: mark the data as in initial state or saved (default)
        - `unsafe`: mark the data as modified by the user and not saved

This action can be useful in conjunction with `send`. Upon successfully sending the data, if the data is not in addition saved to the local database, this action can be used to indicate to the user that the data is safe.

## navigate

Navigate to an external page via client-side GET.

- parameters
    - `uri`: an XPath value template which specifies the URL to navigate to
    - `property`: specifies a property containing the URL to navigate to
    - by default, try to guess based on the parameter

You can also use the `navigate` action to execute JavaScript:

```
navigate(uri = "javascript:myFunction()")
```

[SINCE Orbeon Forms 4.6]

The URL value, whether directly or via a property, can be an XPath Value Template, which runs in the context of the root element of the main form instance:

```
navigate(uri = "http://example.org/{xxf:get-request-parameter('bar')}/{.//code}")
```

## success-message and error-message

- `success-message`: show a success message
    - parameters
        - `message`: message to show (is an XPath value template)
        - `resource`: resource key pointing to the message
- `error-message`: show an error message
    - parameters
        - `message`: message to show (is an XPath value template)
        - `resource`: resource key pointing to the message

[SINCE Orbeon Forms 4.7] The value of the `message` parameter and the message to which points the resource key in the `resource` parameter are interpreted as an XPath Value Template.

## confirm

[SINCE Orbeon Forms 4.5]

Show a confirmation dialog. If the user selects "No", the current process is aborted. If the user selects "Yes", the current process is resumed.

![Confirmation dialog](../../images/confirm.png)

- parameters
    - `message`: message to show (is an XPath value template)
    - `resource`: resource key pointing to the message

Example of use:

```
save
then confirm
then suspend
then send("oxf.fr.detail.send.success")'/>
```

*NOTE: The `confirm` action is not synchronous, so the process *must* be suspended right after or the process will continue before the dialog is shown to the user.*

You can use a specific confirmation message with the `message` parameter:

```xml
save
then confirm(message = "Please confirm that you would like to submit your data.")
then suspend
then send("oxf.fr.detail.send.success")'/>
```

You can also override the default confirmation message:

```xml
<property
  as="xs:string"
  name="oxf.fr.resource.*.*.en.detail.messages.confirmation-dialog-message"
  value="Are you sure you want to proceed?"/>
```

## Other actions

- `review`, `edit`: navigate to these Form Runner pages
- `summary`: navigate to this Form Runner page (a predefined process since 4.7)
- `visit-all`:
    - mark all controls as visited
    - [SINCE Orbeon Forms 2017.1] When using the [wizard view](../../feature/wizard-view.md) in `lax` and strict` mode, this marks all controls up to and including the latest visited page as visited. Controls on further, non-visited pages are not marked as visited.
- `unvisit-all`: mark all controls as not visited
- `expand-all`: expand all sections
- `collapse-all`: collapse all sections
- `result-dialog`: show the result dialog
- `captcha`: trigger the captcha
- `wizard-prev`: navigate the wizard to the previous page
- `wizard-next`: navigate the wizard to the next page
