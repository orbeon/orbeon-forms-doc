# Predefined buttons, processes and dialogs

<!-- toc -->

## Predefined buttons

The following buttons are predefined and associated with the processes of the same name:

| Name | Description | Since |
| ---- | ----------- | ----- |
| `home` | navigate to `/` | 4.2 |
| `summary` | navigate to the summary page | 4.2 |
| `save-final` | save the form data if it is valid | 4.2 |
| `save-draft` | save the form data even if it isn't valid | 4.2 |
| `validate` | run `validate-all` | 4.2 |
| `review` | navigate to the review page if the data is valid | 4.2 |
| `edit` | navigate to the edit page from the review page | 4.2 |
| `send` | validate then send data to a service | 4.2 |
| `pdf` | generate a PDF version of the current form | 4.2 |
| `tiff` | generate a TIFF version of the current form (see [TIFF Production](TIFF Production)) | 4.2 |
| `email` | validate then email data | 4.2 |
| `collapse-all` | run the action of the same name | 4.2 |
| `expand-all` | run the action of the same name | 4.2 |
| `refresh` | visit all controls and update the page (noscript mode only) | 4.2 |
| `wizard-prev` | run the action of the same name | 4.2 |
| `wizard-next` | run the action of the same name | 4.2 |
| `close` | navigate to the URL specified by `oxf.fr.detail.close.uri` or, if not specified, to the summary page<br>*NOTE: The button in fact navigates to a page, but doesn't just close the current window/tab, as there is no cross-browser way to do this.* | 4.2 |

In fact all buttons except the `pdf` and `tiff` buttons can do the same tasks if they are configured appropriately! But
by default the buttons above are preconfigured to do different tasks, for convenience.

## Predefined reusable processes

The following sub-processes are predefined and can be reused from other processes:

| Name | Description | Since |
| ---- | ----------- | ----- |
| `require-uploads` | check whether there are pending uploads and if so display an error message and interrupt the process | 4.2 |
| `require-valid` | mark all controls as visited, check whether data is valid and if not display an error message and interrupt the process | 4.2 |
| `review-messages` | if there are any `error`, `warning` or `info` messages, open a dialog so the user can decide whether to review them or continue the process | 4.3 |
| `validate-all` | combine `require-valid` and `review-messages` | 4.3 |
| `orbeon-home` | navigate to '/' | 4.2 |
| `form-runner-home` | navigate to '/fr' | 4.2 |
| `summary` | navigate to this Form Runner page | 4.7 |

## Processes that apply to services

### oxf.fr.service.duplicate.transform

[SINCE Orbeon Forms 4.7]

This process is called by the `duplicate` service after the original data has been read and before it is written back. This allows performing simple value transformations on the data such as clearing or setting fields upon data duplication.

Examples:

```xml
<!-- Clear the value of the `first-name` element, if found -->
<property as="xs:string" name="oxf.fr.service.duplicate.transform.myapp.myform">
    xf:setvalue(ref = "//first-name")
</property>
<!-- Set the value of the `submit-date` element, if found, to the current date -->
<property as="xs:string" name="oxf.fr.service.duplicate.transform.myapp.myform">
    xf:setvalue(ref = "//submit-date", value = "current-date()")
</property>
```

## Standard dialogs

[SINCE Orbeon Forms 4.3]

### Validation dialog

The following dialog can be opened with the `xf:show` action:

- `fr-validation-dialog`: the validation dialog which asks the user to review validation messages

Example:

    xf:show("fr-validation-dialog") then suspend

![The validation dialog](../review-messages.png)

### The result dialog

The `result-dialog` action shows a configurable dialog. You can customize:

- The **message** shown in the dialog, which can either be a static message informing users that the data has been submitted (the default), or a message returned by the persistence layer. In the later case, it is assumed that the persistence layer responds to a CRUD PUT operation with the HTML to display in the dialog. None of the persistence implementations that ship with Orbeon Forms do that, so this property is only relevant if you implement your own persistence layer. Otherwise, you will want to leave this property to its default value:

    ```xml
    <property
      as="xs:boolean"
      name="oxf.fr.detail.submit.content-from-persistence.*.*"
      value="false"/>
    ```
- The **buttons** shows in the submit dialog, which can be:
    - `clear`: Sets all the fields to their default value and closes the dialog.
    - `keep`: Keeps the field values as they are and closes the dialog.
    - `go`: Go to a URL (see below for how the URL can be configured)
    - `close-window`: Closes the window. For this to work, JavaScript must be enabled, and the window in which the form is shown must have been opened by another page you created.

    ```xml
    <property
      as="xs:string"
      name="oxf.fr.detail.submit.buttons.*.*"
      value="go"/>
    ```
- The **go URI**, if you have enabled the go button. When the "go" button is pressed, users will be taken to the URI specified by the following property. The value of the property is an XPath expression evaluated in the context of the form instance. This allows you both to have a "dynamic" URI (which depends on the initial data or data entered by users) or a "static" URI in the form of a URI between single quote in the XPath expression.

    ```xml
    <property
      as="xs:string"
      name="oxf.fr.detail.submit.go.uri-xpath.*.*"
      value="/book/details/link"/>
    ```

