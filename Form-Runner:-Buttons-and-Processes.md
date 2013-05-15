## See also

See this blog post for an introduction to the feature: [More powerful buttons](http://blog.orbeon.com/2013/04/more-powerful-buttons.html).

## Availability

This feature is available with Orbeon Forms 4.2 and newer.

Up to version 4.1, Orbeon Forms had a few predefined buttons to specify what happens with form data:

- The "Save" button to save data to the database.
- The "Submit" button to save data and show a dialog after saving (with options to clear data, keep data, navigate to another page, or close the window).
- The "Send" (AKA "workflow-send") button to save data and then allow:
    - sending an email
    - sending form data to a service
    - redirecting the user to a success or error page

For more information, see [Configuration Properties - Form Runner](http://wiki.orbeon.com/forms/doc/developer-guide/configuration-properties/configuration-properties-form-runner).

## Reusable actions

The following set of reusable actions is available:

- `validate`: validate form data
- `pending-uploads`: checking whether there are pending uploads
- `save`: save data via the persistence layer
- `success-message`: show a success message
- `error-message`: show an error message
- `email`: send an email
- `send`: send the data to an HTTP service
- `navigate`: navigate to an external page
- `review`, `edit`, `summary`: navigate to these Form Runner pages
- `visit-all`: mark all controls as visited
- `unvisit-all`: mark all controls as not visited
- `expand-all`: expand all sections
- `collapse-all`: collapse all sections
- `result-dialog`: show the result dialog
- `captcha`: trigger the captcha
- `wizard-pref`: navigate the wizard to the previous page
- `wizard-next`: navigate the wizard to the next page
- `success`: complete the process
- `process`: run a sub-process

## Combining actions

Running a single action might be useful, but it is much more useful in many cases to combine actions. This means that you must be able to:

- specify which actions run and in which order
- decide what to do when they succeed or fail
- decide how to associate them with buttons

For this purpose, the following combinators are defined:

- `then`: used to specify that the following sequence of actions must run if the current action succeeds.
- `recover`: used to specify that the following individual action must run if the current action fails. Once the following action has run, processing continues successfully.

For example, the behavior of the (now deprecated) "Save" button is specified this way:

    require-uploads
    then maybe-require-valid
    then save
    then success-message("save-success")
    recover error-message("database-error")

Notice that there are:

- action names, like `save` and `success-message`
- sub-processes, like `require-uploads` and `maybe-require-valid`, which runs a number of steps and stop processing if uploads are pending or the data is not valid)
- two different combinators, `then` and `recover`

So in the example above what you want to say is the following:

- start by checking that there are no pending uploads (if that's not the case, the process is interrupted)
- then in case of success validate the data (if that's not the case, the process is interrupted)
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

## Predefined buttons

And we introduce new buttons with predefined behavior:

- `save-final`: validate and save to the db
- `save-draft`: save to the db without validating
- `send`: validate and send to a service

In fact all buttons can do the same tasks if they are configured appropriately! But by default the buttons above are preconfigured to do different tasks, for convenience.

## Configuring processes

So how do you configure processes? Say you want to specify a couple of buttons on your "acme/hr" form. Like before, you define a property:

```xml
    <property
      as="xs:string"
      name="oxf.fr.detail.buttons.acme.hr"
      value="save-draft send"/>
```

This places "Save" and "Send" buttons on the page. Each button is automatically associated with processes of the same names (`save-draft` and `send`). These particular buttons and process names are standard, but we can override them specifically for our form. Again, this is done with a property:

```xml
    <property
      as="xs:string"
      name="oxf.fr.detail.process.send.acme.hr"
      value='require-valid
             then pdf
             then email
             then send("http://example.org/")
             then navigate("/success")
             recover navigate("/failure")'/>
```

Button labels can be overridden as well, as was the case before:

```xml
    <property
      as="xs:string"
      name="oxf.fr.resource.*.*.en.detail.buttons.send"
      value="Fancy Send"/>
```

All the configuration above for a button called `send` could have been done with an entirely custom button named `foo`.

## Compatibility notes

The following buttons are deprecated:

- `save`: use `save-draft` or `save-final`
- `submit`: use the `send` button with the desired sequence of actions
- `workflow-send`: use the `send` button with the desired sequence of actions
- `workflow-review`: use `review` instead
- `workflow-edit`: use `edit` instead

The following property is the projected:

    oxf.fr.detail.send.pdf

Instead, use:

```xml
    <property
      as="xs:string"
      name="oxf.fr.detail.send.success.content.*.*"
      value="pdf-url"/>
```

Identical results can be achieved by using the appropriate reusable actions.
