# Form Runner actions



## Children pages

- [send action](actions-form-runner-send.md)

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

## rollback

[SINCE Orbeon Forms 2017.2]

Rollback some of the changes that have taken place during the current process.

- parameters
    - `changes`: must be "in-memory-form-data"
    
At the beginning of a top-level process, the current state of:

- in-memory form data
- data status (see the `set-data-status` action)

is temporarily saved.

Upon running the `rollback` action, that stat is restored.
 
This means, for example, that if the instance data was changed due to actions such as:

- `xf:setvalue`
- `save` (which updates paths to attachments)

then the state of the data is restored to what it was prior to running the current top-level process.

Example:

```
xf:setvalue(ref = "my-section/my-name", value = "'Sam'")
then rollback(changes = "in-memory-form-data")
```

Limitations:

- This does not work across `suspend` / `resume` boundaries.
- There is no rollback at the database level.

## save

The documentation for the `save` action is on a [separate page](actions-form-runner-save.md).

## email

Send an email with optionally XML form data, attachments, and PDF.

- parameters: none
- properties used: [`oxf.fr.email.*`](../../../configuration/properties/form-runner.md#email-settings)

## send

The documentation for the `send` action is on a [separate page](actions-form-runner-send.md).

## set-data-status

[SINCE Orbeon Forms 4.7]

Set the status of the in-memory form data.

- parameters
    - `status`: specifies the status of the data
        - `safe`: mark the data as in initial state or saved (default)
        - `unsafe`: mark the data as modified by the user and not saved

This action can be useful in conjunction with `send`. Upon successfully sending the data, if the data is not in addition saved to the local database, this action can be used to indicate to the user that the data is safe.

See also [the `oxf.fr.detail.warn-when-data-unsafe` property](/configuration/properties/form-runner-detail-page.md#warning-the-user-when-data-is-unsafe).

## navigate

Navigate to an external page via client-side `GET`.

- parameters
    - `uri`: an XPath value template which specifies the URL to navigate to
    - `property`: specifies a property containing the URL to navigate to
    - by default, try to guess based on the parameter
    - `target`
        - SINCE Orbeon Forms 2019.1
        - where to display the location
        - `_self|_blank` or name of the browsing context

You can also use the `navigate` action to execute JavaScript:

```
navigate(uri = "javascript:myFunction()")
```

[SINCE Orbeon Forms 4.6]

The URL value, whether directly or via a property, can be an XPath Value Template, which runs in the context of the root element of the main form instance:

```
navigate(uri = "http://example.org/{xxf:get-request-parameter('bar')}/{.//code}")
```

## relinquish-lease

[SINCE Orbeon Forms 2017.2.1]

The `relinquish-lease` action will, if the current user has a lease on the data being  currently edited, relinquish that lease. The action has no effect if the [lease isn't enabled](../../feature/lease.md#enabling-the-lease-feature), or if the current user doesn't own the lease.

In most cases you'll want to use this action in conjunction with [`navigate`](#navigate) or [`send(replace ="all")`](actions-form-runner-send.md) to release a potential lease the user might have before you take that user to another page. This way other users will be able to access the current data without having to wait for the lease to expire.

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

You can also use a path to a resource, including a custom resource. For example" 

```xml
save
then confirm(resource = "acme.my-resource-1")
then suspend
then send("oxf.fr.detail.send.success")'/>
```

If you have defined custom resources as follows:

```xml
<property
  as="xs:string"
  name="oxf.fr.resource.*.*.en.acme.my-resource-1"
  value="Resource 1 in English"/>

<property
  as="xs:string"
  name="oxf.fr.resource.*.*.fr.acme.my-resource-1"
  value="Resource 1 en français"/>
```

## open-rendered-format

[SINCE Orbeon Forms 2017.1] This action takes a `format` parameter, whose value must be either `pdf` or `tiff`, as in `open-rendered-format(format = "pdf")`. Depending on the value of the parameter, it generates a view of the current form in the specified format, and sends the generated PDF or TIFF to the browser. This action will attempt to have the browser show the generated PDF or TIFF, and do so in a new browser tab or window. However, not all browsers support this completely:

- Showing the PDF or TIFF inline:
    - PDF – All browsers will show the PDF inline directly, except Edge which will first ask users if they want to save or open it.
    - TIFF – Browsers other than Safari don't know how to show a TIFF file inline, and thus will just download the file.
- Opening the PDF or TIFF in a [new tab or browser window](../../../xforms/submission-extensions.md#target-window-or-frame):
    - With Chrome, IE, and Edge the PDF or TIFF will show in a new window.
    - With Safari and Firefox the PDF or TIFF will show in the current window.

## Other actions

- `captcha`: Trigger the captcha.
- `collapse-all`: Collapse all sections (when not using the wizard view).
- `expand-all`: Expand all sections (when not using the wizard view).
- `expand-invalid`: [SINCE Orbeon Forms 2018.1] This action expands all the sections that contain an error. Out-of-the-box, this action is used by the `require-valid` process, in turn called when validating data, say before save, so users can see all the sections that contain an error.
- `new-to-edit`:
    - [SINCE Orbeon Forms 2017.1]
    - If possible, switch the detail page's URL from new mode to edit mode. Before this action, this was done automatically as part of the `save` action.
- `result-dialog`: Show the result dialog.
- `review`, `edit`: Navigate to these Form Runner pages.
- `show-relevant-errors`:
    - [SINCE Orbeon Forms 2018.1]
    - This is the new name for the `visit-all` action.
    - This shows all "relevant errors". When using the [wizard view](../../feature/wizard-view.md) in `lax` and strict` mode, this shows errors to show for all available wizard pages. Errors for controls on non-available pages are not shown.
- `summary`: Navigate to this Form Runner page (a predefined process since 4.7).
- `unvisit-all`:
    - Mark all controls as not visited.
- `visit-all`:
    - [DEPRECATED SINCE Orbeon Forms 2018.1]
    - See `show-relevant-errors` instead.
- `wizard-prev`: Navigate the wizard to the previous page.
- `wizard-next`: Navigate the wizard to the next page.
