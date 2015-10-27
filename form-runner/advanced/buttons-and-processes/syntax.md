# Process syntax

<!-- toc -->

## Defining a process

You define a process with a property (typically in [[`properties-local.xml`|Installation-~-Configuration-Properties]]) starting with `oxf.fr.detail.process`. For example:

```xml
<property as="xs:string" name="oxf.fr.detail.process.save-final.*.*">
    require-uploads
    then require-valid
    then save
    then success-message("save-success")
    recover error-message("database-error")
</property>
```

The name of the process immediately follows the property prefix, here `save-final`.

The wildcards, as usual with Form Funner, can specify a form's application and form names.

The inline value, staring in this example with `require-uploads`, describes the process following a DSL (domain-specific language) described in more details below. The process DSL supports:

- actions with parameters
- combinators to handle success and failure
- grouping with parentheses
- conditions ("if")
- sub-processes
- a set of standard actions

## Simple actions

### Actions without parameters

Some actions, such as the `email` action, don't have or don't require any parameters. You just write the name of the action:

```xml
<property as="xs:string" name="oxf.fr.detail.process.email-my-form.*.*">
    email
</property>
```

### Actions with parameters

[SINCE Orbeon Forms 4.4]

In Orbeon Forms 4.2 and 4.3, actions support only an anonymous default parameter. With 4.4, actions support named parameters in addition to an anonymous default parameter:

```ruby
send(
    uri      = "http://acme.org/orbeon",
    annotate = "error warning",
    replace  = "all"
)
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
- `save-draft`: save the form data even if it isn't valid
- `validate`: run `validate-all`
- `review`: navigate to the review page if the data is valid
- `edit`: navigate to the edit page from the review page
- `send`: validate then send data to a service
- `pdf`: generate a PDF version of the current form
- `tiff` [SINCE Orbeon Forms 4.11]
    - generate a TIFF version of the current form (see [[TIFF Production|Form Runner ~ TIFF Production]])
- `email`: validate then email data
- `collapse-all`: run the action of the same name
- `expand-all`: run the action of the same name
- `refresh`: visit all controls and update the page (noscript mode only)
- `wizard-prev`: run the action of the same name
- `wizard-next`: run the action of the same name
- `close`: navigate to the URL specified by `oxf.fr.detail.close.uri` or, if not specified, to the summary page
    *NOTE: The button in fact navigates to a page, but doesn't just close the current window/tab, as there is no cross-browser way to do this.*

In fact all buttons except the `pdf` and `tiff` buttons can do the same tasks if they are configured appropriately! But
by default the buttons above are preconfigured to do different tasks, for convenience.

## Standard dialogs

[SINCE Orbeon Forms 4.3]

### Validation dialog

The following dialog can be opened with the `xf:show` action:

- `fr-validation-dialog`: the validation dialog which asks the user to review validation messages

Example:

    xf:show("fr-validation-dialog") then suspend

![The validation dialog](review-messages.png)

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

For more information, see [[Configuration Properties - Form Runner|Form-Runner-~-Configuration-properties]].

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

## See also

- This blog post for an introduction to the feature: [More powerful buttons](http://blog.orbeon.com/2013/04/more-powerful-buttons.html)
- The predefined configuration properties in [`properties-form-runner.xml`](https://github.com/orbeon/orbeon-forms/blob/master/src/resources-packaged/config/properties-form-runner.xml)
