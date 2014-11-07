## See also

- this blog post for an introduction to the feature: [More powerful buttons](http://blog.orbeon.com/2013/04/more-powerful-buttons.html)
- the predefined configuration properties in [`properties-form-runner.xml`](https://github.com/orbeon/orbeon-forms/blob/master/src/resources-packaged/config/properties-form-runner.xml)

## Availability

This feature is available with Orbeon Forms 4.2 and newer.

## What this is about

This page describes how to configure the behavior of the buttons that appear at the bottom of the Form Runner detail page, whether in `new`, `edit` or `view` mode. Here is an example of buttons:

![Example of Form Runner buttons](images/w9-form-buttons.png)

## Defining a process

You define a process with a property (typically in [`properties-local.xml`](http://wiki.orbeon.com/forms/doc/developer-guide/configuration-properties)) starting with `oxf.fr.detail.process`. For example:

```xml
<property
  as="xs:string"
  name="oxf.fr.detail.process.save-final.*.*"
  value='require-uploads
         then require-valid
         then save
         then success-message("save-success")
         recover error-message("database-error")'/>
```

The name of the process immediately follows the property prefix, here `save-final`.

The wildcards, as usual with Form Funner, can specify a form's application and form names.

The value describes the process following a DSL (domain-specific language) described in more details below. The simple process DSL supports:

- actions with parameters
- combinators to handle success and failure
- grouping with parentheses
- conditions ("if")
- sub-processes
- a set of standard actions

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

NOTE: As of Orbeon Forms 4.2, the `clear` and `pdf` buttons are not implemented as processes but handled directly by Form Runner.

## Core actions

### success

Complete the process right away successfully.

### process

You can run a sub-process with the `process` action:

```xml
<property
  as="xs:string"
  name="oxf.fr.detail.process.home.*.*"
  value='process("orbeon-home")'/>
```

You can also run a sub-process directly by name.

Example:

```xml
<!-- Define a sub-process which navigates to the "/" URL -->
<property
  as="xs:string"
  name="oxf.fr.detail.process.orbeon-home.*.*"
  value='navigate("/")'/>

<!-- Use that sub-process from the "home" process -->
<property
  as="xs:string"
  name="oxf.fr.detail.process.home.*.*"
  value='orbeon-home'/>
```

### suspend

[SINCE Orbeon Forms 4.3]

Suspend the current process. The continuation of the process is associated with the current form session.

### resume

[SINCE Orbeon Forms 4.3]

Resume the process previously suspended with `suspend`. 

### abort

[SINCE Orbeon Forms 4.3]

Abort the process previously suspended with `suspend`. This clears the information associated with the process and it won't be possible to resume it with `resume`.

### nop

[SINCE Orbeon Forms 4.3]

Don't do anything and return a success value.

## Core Form Runner actions

### validate

Validate form data.

- parameters
    - `level`: validation level to check: one of `error`, `warning`, or `info`
    - `property`: specifies a boolean property which, if `false`, skips validation (used for backward compatibility) [Orbeon Forms 4.2 only, removed in Orbeon Forms 4.3]
- result
    - success if data is valid
    - failure if data is invalid

### pending-uploads

Check whether there are pending uploads.

- parameters
    - none
- result
    - success if there are no pending uploads
    - failure if there are pending uploads

### save

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

### email

Send an email with optionally XML form data, attachments, and PDF.

- parameters: none
- properties used: [`oxf.fr.email.*`](http://wiki.orbeon.com/forms/doc/developer-guide/configuration-properties/configuration-properties-form-runner#TOC-Email-settings)

### send

#### Configuration

Send data to an HTTP URL.

- parameters [SINCE Orbeon Forms 4.4 except `property`]
    - `property`: specifies an optional property prefix
    - `uri`: URL to which to send the data
    - `method`: `GET`, `POST` (default), `PUT`
    - `prune`: whether to prune non-relevant nodes (`true` by default)
    - `annotate`: space-separated list of levels to annotate elements (the empty string by default)
    - `replace`: `all` to load the resulting response in the browser, or `none` (default)
    - `content`:
        - `xml` to send the XML data (default)
        - `pdf-url` to send the PDF URL
        - `metadata`: to send form metadata [SINCE Orbeon Forms 4.7]
    - `parameters`:
        - space-separated list of standard parameters to automatically add to the URL (see below)
        - default: `app form form-version document valid language`
            - `form-version` added to defaults in Orbeon Forms 4.7
- properties used
    - property prefix + `.uri`: see `uri` parameter
    - property prefix + `.method`: see `method` parameter
    - property prefix + `.prune`: see `prune` parameter
    - property prefix + `.annotate`: see `annotate` parameter
    - property prefix + `.replace`: see `replace` parameter
    - property prefix + `.content`: see `content` parameter
    - property prefix + `.parameters`: see `content` parameter

Parameters have a higher precedence. In this example, the `uri` parameter is used, even if a `oxf.fr.detail.send.success.uri` property is present:

```ruby
send(property = "oxf.fr.detail.send.success", uri = "http://acme.org/orbeon")
```

*SECURITY NOTE: If `replace` is set to `all`, the content of resources or redirection URLs accessible by the Orbeon Forms server are forwarded to the web browser. Care must be taken to forward only resources that users of the application are allowed to see.*

[SINCE Orbeon Forms 4.4]

The following properties are XPath Value Templates evaluating in the context of the root element of the form data instance:

- `uri`
- `method`
- `prune`
- `annotate`
- `content`
- `parameters`
- `replace` [SINCE Orbeon Forms 4.7]

Example:

```xml
<property as="xs:string" name="oxf.fr.detail.send.success.uri.*.*">
    /fr/service/custom/orbeon/echo?action=submit&amp;foo={
        encode-for-uri(xxf:instance("fr-form-instance")//foo)
    }&amp;bar={
        encode-for-uri(xxf:instance("fr-form-instance")//bar)
    }
</property>
```

Note the use of the `encode-for-uri()` function which escapes the value to place after the `=` sign.

[SINCE Orbeon Forms 4.5]

If `replace` is set to `all` and the service issues a redirection via an HTTP status code, the redirection is propagated to the client. This also works with portlets.

#### URL format

The full URL is composed of:

- the URL specified by the `uri` property
- the following URL parameters (when present in `parameters`)
    - `app`: the current form's app name
    - `form`: the current form's form name
    - `form-version`: the form definition version in use [SINCE Orbeon Forms 4.5]
    - `document`: the current document id
    - `valid`: whether the data sent satisfies validation rules
    - `language`: the language of the form at the time it was submitted [SINCE Orbeon Forms 4.5]
    - `noscript`: whether the noscript mode was in use [SINCE Orbeon Forms 4.6]

Example:

    http://example.org/service?
      app=acme&
      form=invoice&
      document=f0cd6bf16ba1f783773bb7165f0d79deab37585f&
      valid=true&
      language=fr

#### Sending a PDF URL

When `pdf-url` is specified, the XML document sent has the following format:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<url>/xforms-server/dynamic/567f14ee46c6b21640c1a5a7374d5ad8</url>
```

The PDF can be retrieved by accessing that path with the proper session cookie.

*NOTE: This is not ideal, see [#1840](https://github.com/orbeon/orbeon-forms/issues/1840)*

#### Sending form metadata

[SINCE Orbeon Forms 4.7]

When `metadata` is specified, the XML document sent contains metadata per control. [This page](https://gist.github.com/orbeon/3684806b0a30a9a5ace9) shows examples based on the Orbeon Forms sample forms.

The metadata is linked to the data with the `for` attribute, which can contain multiple id values separated by a space. This associates the given piece of metadata with multiple values in the form data. This typically happens where there are repeated fields in the form, so that there is no duplication of identical metadata.

Here is an example of `send` process which sends XML data to a service, followed by sending metadata:

```xml
<property as="xs:string"  name="oxf.fr.detail.process.send.orbeon.*">
    require-uploads
    then validate-all
    then send(
        uri      = "http://localhost:8080/orbeon/xforms-sandbox/service/echo-xml",
        replace  = "none",
        method   = "post",
        content  = "xml",
        annotate = "id"
    )
    then send(
        uri      = "http://localhost:8080/orbeon/xforms-sandbox/service/echo-xml",
        replace  = "none",
        method   = "post",
        content  = "metadata"
    )
</property>
```

#### Annotating XML data

`annotate` can contain the following tokens:

- `error`, `warning`, `info`: XML elements are annotated with information associated with the given level or levels.
- `id`: XML elements are annotated with a unique id. [SINCE Orbeon Forms 4.7]

If the property is missing or empty, no annotation takes place. For example:

```xml
    <property
      as="xs:string"
      name="oxf.fr.detail.send.success.annotate.acme.hr"
      value="warning info"/>
```

```xml
    <form xmlns:xxf="http://orbeon.org/oxf/xml/xforms">
        <my-section>
            <number xxf:info="Nice, greater than 1000!">2001</number>
            <text xxf:warning="Should be shorter than 10 characters">This is a bit too long!</text>
        </my-section>
    </form>
```

### set-data-status

[SINCE Orbeon Forms 4.7]

Set the status of the form data in memory.

- parameters
    - `status`: specifies the status of the data
        - `safe`: mark the data as in initial state or saved (default)
        - `unsafe`: mark the data as modified by the user and not saved

This action can be useful in conjunction with `send`. Upon successfully sending the data, if the data is not in addition saved to the local database, this action can be used to indicate to the user that the data is safe.

### navigate

Navigate to an external page via client-side GET.

- parameters
    - `uri`: specifies the URL to navigate to
    - `property`: specifies a property containing the URL to navigate to
    - by default, try to guess based on the parameter

[SINCE Orbeon Forms 4.6]

The URL value, whether directly or via a property, can be an XPath Value Template, which runs in the context of the root element of the main form instance:

```
navigate(uri = "http://example.org/{xxf:get-request-parameter('bar')}/{.//code}")
```

### success-message and error-message

- `success-message`: show a success message
    - parameters
        - `message`: message to show
        - `resource`: resource key pointing to the message
- `error-message`: show an error message
    - parameters
        - `message`: message to show
        - `resource`: resource key pointing to the message

[SINCE Orbeon Forms 4.7] The value of the `message` parameter and the message to which points the resource key in the `resource` parameter are interpreted as an XPath Value Template.

### confirm

[SINCE Orbeon Forms 4.5]

Show a confirmation dialog. If the user selects "No", the current process is aborted. If the user selects "Yes", the current process is resumed.

![Confirmation dialog](images/fr-confirm.png)

- parameters
    - `message`: message to show
    - `resource`: resource key pointing to the message

Example of use:

```
save
then confirm
then suspend
then send("oxf.fr.detail.send.success")'/>
```

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

### xf:setvalue

[SINCE Orbeon Forms 4.7]

Set a value in the form data.

- parameters
    - `ref`: required XPath expression, relative to the root element of the form data, pointing to the node to set
    - `value`: optional XPath expression, relative to the node to set, specifying the value to set

If `value` is omitted, the value selected is the empty string.

Examples:

```ruby
xf:setvalue(ref = "//first-name")                            // clear the value of the `first-name` element, if found
xf:setvalue(ref = "//submit-date", value = "current-date()") // set the value of the `submit-date` element, if found, to the current date
```

### xf:dispatch

[SINCE Orbeon Forms 4.3]

Dispatch an XForms event by name to an XForms target.

- parameters
    - `name` (default parameter): specifies the name of the event to dispatch
    - `targetid`: specifies the event's target id,  `fr-form-model` by default. [SINCE Orbeon Forms 4.4]

### xf:send

[SINCE Orbeon Forms 4.3]

Send an XForms submission.

- parameters
    - `submission`: specifies the id of the submission to send

### xf:show

[SINCE Orbeon Forms 4.3]

Open an XForms dialog by id.

- parameters
    - `dialog`: specifies the id of the dialog to open

### Other actions

- `review`, `edit`: navigate to these Form Runner pages
- `summary`: navigate to this Form Runner page (a predefined process since 4.7)
- `visit-all`: mark all controls as visited
- `unvisit-all`: mark all controls as not visited
- `expand-all`: expand all sections
- `collapse-all`: collapse all sections
- `result-dialog`: show the result dialog
- `captcha`: trigger the captcha
- `wizard-prev`: navigate the wizard to the previous page
- `wizard-next`: navigate the wizard to the next page

## Predefined reusable processes

The following sub-processes are predefined and can be reused from other processes:

- `require-uploads`: check whether there are pending uploads and if so display an error message and interrupt the process
- `require-valid`: mark all controls as visited, check whether data is valid and if not display an error message and interrupt the process
- `review-messages`: if there are any `error`, `warning` or `info` messages, open a dialog so the user can decide whether to review them or continue the process [SINCE Orbeon Forms 4.3]
- `validate-all`: combine `require-valid` and `review-messages` [SINCE Orbeon Forms 4.3]
- `orbeon-home`: navigate to '/'
- `form-runner-home`: navigate to '/fr'
- `summary`: navigate to this Form Runner page (a predefined process since 4.7)

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

## Combining actions

Running a single action might be useful, but it is much more useful to combine actions. This means that you must be able to:

- specify which actions run and in which order
- decide what to do when they succeed or fail
- decide how to associate them with buttons

For this purpose, the following combinators are defined:

- `then`: used to specify that the following sequence of actions must run if the current action succeeds.
- `recover`: used to specify that the following individual action must run if the current action fails. Once the following action has run, processing continues successfully.

With actions and combinators, the syntax becomes:

- The process must start with an action or a sub-process.
- The process must also end with an action or a sub-process.
- Two actions or sub-processes must be separated by a combinator.
- Some actions have parameters (NOTE: As of Orbeon Forms 4.2, only a single, unnamed parameter is supported.).

For example, the behavior of the "Save" button, associated with the `save-final` process, is specified this way:

    require-uploads
    then validate-all
    then save
    then success-message("save-success")
    recover error-message("database-error")

Notice that there are:

- action names, like `save` and `success-message`
- sub-processes, like `require-uploads` and `validate-all`, which runs a number of steps and stop processing if uploads are pending or the data is not valid)
- the two combinators, `then` and `recover`

So in the example above what you want to say is the following:

- start by checking that there are no pending uploads
    - if there are, the process is interrupted
- then in case of success validate the data
    - if it's invalid, the process is interrupted
    - if there are warnings or info messages, a dialog is shown the user
- then in case of success save the data
- then in case of success show a success message
- if saving has failed, then show an error message

A process which just saves the data without checking validity and shows success and error messages looks like this:

    save
    then success-message("save-draft-success")
    recover error-message("database-error")

Validating and sending data to a service looks like this:

    require-valid
    then send("oxf.fr.detail.send.success")

Some actions can take parameters. In the example above we point to properties to configure the `send` action. This means that, within a single process, you can have any number of `send` actions which send data to various services. This also allows you to have separate buttons to send data to different services. These two scenarios were not possible before.

## Action parameters

[SINCE Orbeon Forms 4.4]

In Orbeon Forms 4.2 and 4.3, actions support only an anonymous default parameter. With 4.4, actions support named parameters in addition to an anonymous default parameter:

```ruby
send(uri = "http://acme.org/orbeon", annotate = "error warning", replace = "all")
```

## Grouping actions

[SINCE Orbeon Forms 4.4]

You can use parentheses to group actions. For example:

```ruby
visit-all
then captcha
then validate("error")
recover (
    visit-all
    then expand-all
    then error-message("form-validation-error")
    then success
)
```

Here `recover` processes the entire content of the parentheses. Without the parentheses, only `visit-all` would be processed by `recover`, and the subsequent `expand-all` would run whether the preceding actions are successful or not.

## Conditions

[SINCE Orbeon Forms 4.4]

You can use `if` to evaluate a condition during the expression of a process. The condition is expressed as an XPath expression and runs in the context of the root element of the main form instance:

```ruby
if ("//secret = 42")
then success-message(message = "yea")
else error-message(message = "nay")
```

The `else` branch is optional. This means that the following two lines are equivalent:

```ruby
if ("xpath") then action1 then action2
if ("xpath") then action1 else nop then action2
```

The `if` and `else` operators have a higher precedence than the `then` and `recover` combinators. This means that if you need more that one action to run in either one of the branches, parentheses must be added:

```ruby
if ("xpath") then (action1 then action2) else action3
```

This also means that the following two lines are equivalent:

```ruby
if ("xpath") then action1 else action2 then action3
(if ("xpath") then action1 else action2) then action3
```

## Predefined buttons

The following buttons are predefined and associated with the processes of the same name:

- `home`: navigate to `/`
- `summary`: navigate to the summary page
- `save-final`: save the form data if it is valid
- `save-draft`: save the form data even if it is valid
- `validate`: run `validate-all`
- `review`: navigate to the review page if the data is valid
- `edit`: navigate to the edit page from the review page
- `send`: validate then send data using the `oxf.fr.detail.send.success` property prefix
- `email`: validate then email data
- `collapse-all`: run the action of the same name
- `expand-all`: run the action of the same name
- `refresh`: visit all controls and update the page (noscript mode only)
- `wizard-prev`: run the action of the same name
- `wizard-next`: run the action of the same name
- `save-final`: validate and save to the db
- `save-draft`: save to the db without validating
- `send`: validate and send to a service
- `close`: navigate to the URL specified by `oxf.fr.detail.close.uri` or, if not specified, to the summary page  
    *NOTE: The button in fact navigates to a page, but doesn't just close the current window/tab, as there is no cross-browser way to do this.*

In fact all buttons can do the same tasks if they are configured appropriately! But by default the buttons above are preconfigured to do different tasks, for convenience.

## Standard dialogs

[SINCE Orbeon Forms 4.3]

### Validation dialog

The following dialog can be opened with the `xf:show` action:

- `fr-validation-dialog`: the validation dialog which asks the user to review validation messages

Example:

    xf:show("fr-validation-dialog") then suspend

![The validation dialog](images/fr-review-messages.png)

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

## Customizing processes

So how do you customize processes? Say you want to specify a couple of buttons on your "acme/hr" form. Like before, you define a property:

```xml
<property
  as="xs:string"
  name="oxf.fr.detail.buttons.acme.hr"
  value="save-draft send"/>
```

This places a `save-draft` and `send` buttons on the page. Their default labels are "Save" and "Send". Each button is
automatically associated with processes of the same names, `save-draft` and `send`. These particular buttons and
process names are standard, but we can customize them specifically for our form. Again, this is done with a property:

```xml
<property
  as="xs:string"
  name="oxf.fr.detail.process.send.acme.hr"
  value='require-valid
         then email
         then send("http://example.org/")
         then navigate("/success")
         recover navigate("/failure")'/>
```

Button labels can be overridden as well, as was the case before:

```xml
<property
  as="xs:string"
  name="oxf.fr.resource.*.*.en.buttons.send"
  value="Fancy Send"/>
```

*NOTE: With Orbeon Forms 4.5.x and earlier, the property must be `oxf.fr.resource.*.*.en.detail.buttons.send`. With Orbeon Forms 4.6 and newer, the `detail` token can and should be omitted.*

All the configuration above for a button called `send` could have been done with an entirely custom button named `foo`.

## Compatibility notes

### Starting with Orbeon Forms 4.2

#### Related Orbeon Forms 4.1 and earlier functionality

Up to version 4.1, Orbeon Forms had a few predefined buttons to specify what happens with form data:

- The "Save" button to save data to the database.
- The "Submit" button to save data and show a dialog after saving (with options to clear data, keep data, navigate to another page, or close the window).
- The "Send" (AKA "workflow-send") button to save data and then allow:
    - sending an email
    - sending form data to a service
    - redirecting the user to a success or error page

For more information, see [Configuration Properties - Form Runner](http://wiki.orbeon.com/forms/doc/developer-guide/configuration-properties/configuration-properties-form-runner).

#### Deprecated buttons

The following buttons are deprecated:

- `save`: use `save-draft` or `save-final`
- `submit`: use the `send` button with the desired sequence of actions
- `workflow-send`: use the `send` button with the desired sequence of actions
- `workflow-review`: use `review` instead
- `workflow-edit`: use `edit` instead

When the `workflow-send` is used, the behavior matches that of Orbeon Forms 4.1 and earlier. The following properties
are considered to build a process:

- `oxf.fr.detail.send.email`
- `oxf.fr.detail.send.success.uri`
- `oxf.fr.detail.send.error.uri`

#### Removed property

The following property is no longer supported:

```xml
<property
  as="xs:boolean"
  name="oxf.fr.detail.send.pdf.*.*"
  value="false"/>
```

Instead, use:

```xml
<property
  as="xs:string"
  name="oxf.fr.detail.send.success.content.*.*"
  value="pdf-url"/>
```

### Starting with Orbeon Forms 4.3

#### The validate action no longer supports a property

The `validate` action no longer supports a `property` parameter. In particular, this means that the following property is no longer supported:

```xml
<property
  as="xs:boolean"
  name="oxf.fr.detail.save.validate.*.*"
  value="true"/>
```

This also means that the `maybe-require-valid` process is no longer available.

Instead, use the `save-draft` process, or customize a process with the `save` action but no `require-valid`.