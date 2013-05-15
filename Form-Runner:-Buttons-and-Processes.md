## See also

- this blog post for an introduction to the feature: [More powerful buttons](http://blog.orbeon.com/2013/04/more-powerful-buttons.html)
- the predefined configuration properties in [properties-form-runner.xml](https://github.com/orbeon/orbeon-forms/blob/master/src/resources-packaged/config/properties-form-runner.xml)

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

## Defining a process

You define a process with a property starting with `oxf.fr.detail.process`. For example:

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

Here the following buttons get associated with processes defined in other properties:

- `summary`
- `save-final`
- `wizard-prev`
- `wizard-next`
- `review`

The `refresh`, `clear` and `pdf` buttons are currently not implemented as processes and handled directly by Form Runner.

## Reusable actions

### validate

Validate form data.

- parameters
    - `property`: specifies a boolean property which, if `false`, skips validation (used for backward compatibility)
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
    - switch to `edit` mode
    - dispatch `fr-data-save-done` to `fr-form-model`
- parameters
    - none

### email

Send an email with optionally XML form data, attachments, and PDF.

- parameters: none
- properties used: `oxf.fr.email.*`

### send

Send data to an HTTP URL.

- parameters
    - `property`: specifies a property prefix
- properties used
    - property prefix + `.method`: `GET`, `POST` (default), `PUT`
    - property prefix + `.prune`: whether to prune non-relevant nodes (`true` by default)
    - property prefix + `.replace`: `all` to load the resulting response in the browser, or `none` (default)
    - property prefix + `.content`: `xml` to send the XML data (default), `pdf-url` to send the PDF URL

### navigate

Navigate to an external page via client-side GET.

- parameters
    - `uri`: specifies the URL to navigate to
    - `property`: specifies a property containing the URL to navigate to
    - by default, try to guess based on the parameter

### success-message and error-message

- `success-message`: show a success message
    - parameters
        - `message`: resource key pointing to the message
- `error-message`: show an error message
    - parameters
        - `message`: resource key pointing to the message

### Running a sub-process

You can run a sub-process directly by name.

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

Alternatively, you can use the `process` action:

```
<property
  as="xs:string"
  name="oxf.fr.detail.process.home.*.*"
  value='process("orbeon-home")'/>
```

### Other actions

- `review`, `edit`, `summary`: navigate to these Form Runner pages
- `visit-all`: mark all controls as visited
- `unvisit-all`: mark all controls as not visited
- `expand-all`: expand all sections
- `collapse-all`: collapse all sections
- `result-dialog`: show the result dialog
- `captcha`: trigger the captcha
- `wizard-pref`: navigate the wizard to the previous page
- `wizard-next`: navigate the wizard to the next page
- `success`: complete the process right away successfully
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
