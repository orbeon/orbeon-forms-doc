## See also

- this blog post for an introduction to the feature: [More powerful buttons](http://blog.orbeon.com/2013/04/more-powerful-buttons.html)
- the predefined configuration properties in [`properties-form-runner.xml`](https://github.com/orbeon/orbeon-forms/blob/master/src/resources-packaged/config/properties-form-runner.xml)

## Availability

This feature is available with Orbeon Forms 4.2 and newer.

## What this is about

This page describes how to configure the behavior of the buttons that appear at the bottom of the Form Runner detail page, whether in `new`, `edit` or `view` mode. Here is an example of buttons:

![Example of Form Runner buttons](images/w9-form-buttons.png)

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
    - switch to `edit` mode
    - dispatch `fr-data-save-done` to `fr-form-model`
- parameters
    - none

The full URL, for attachments as well as for the XML data, is composed of:

- the URL pointing to the persistence layer path, including the file name
- the following URL parameter
    - `valid`: whether the data sent satisfies validation rules

Example:

    http://example.org/orbeon/fr/service/persistence/crud/
        orbeon/
        bookshelf/
        data/891ce63e59c17348f6fda273afe28c2b/data.xml?
        valid=true

### email

Send an email with optionally XML form data, attachments, and PDF.

- parameters: none
- properties used: `oxf.fr.email.*`

### send

Send data to an HTTP URL.

- parameters
    - `property`: specifies a property prefix
- properties used
    - property prefix + `.uri`: URL to which to send the data
    - property prefix + `.method`: `GET`, `POST` (default), `PUT`
    - property prefix + `.prune`: whether to prune non-relevant nodes (`true` by default)
    - property prefix + `.replace`: `all` to load the resulting response in the browser, or `none` (default)
    - property prefix + `.content`: `xml` to send the XML data (default), `pdf-url` to send the PDF URL

The full URL is composed of:

- the URL specified by the `uri` property
- the following URL parameters
    - `app`: the current form's app name
    - `form`: the current form's form name
    - `document`: the current document id
    - `valid`: whether the data sent satisfies validation rules

Example:

    http://example.org/service?
      app=acme&
      form=invoice&
      document=f0cd6bf16ba1f783773bb7165f0d79deab37585f&
      valid=true

### navigate

Navigate to an external page via client-side GET.

- parameters
    - `uri`: specifies the URL to navigate to
    - `property`: specifies a property containing the URL to navigate to
    - by default, try to guess based on the parameter

### success-message and error-message

- `success-message`: show a success message
    - parameters
        - `message`: message to show
        - `resource`: resource key pointing to the message
- `error-message`: show an error message
    - parameters
        - `message`: message to show
        - `resource`: resource key pointing to the message

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

## Predefined reusable processes

The following sub-processes are predefined and can be reused from other processes:

- `require-uploads`: check whether there are pending uploads and if so display an error message and interrupt the process
- `require-valid`: mark all controls as visited, check whether data is valid and if not display an error message and interrupt the process
- `review-messages`: if there are any `error`, `warning` or `info` messages, open a dialog so the user can decide whether to review them or continue the process [SINCE Orbeon Forms 4.3]
- `validate-all`: combine `require-valid` and `review-messages` [SINCE Orbeon Forms 4.3]
- `orbeon-home`: navigate to '/'
- `form-runner-home`: navigate to '/fr'

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

For example, the behavior of the (now deprecated) "Save" button is specified this way:

    require-uploads
    then require-valid
    then save
    then success-message("save-success")
    recover error-message("database-error")

Notice that there are:

- action names, like `save` and `success-message`
- sub-processes, like `require-uploads`, which runs a number of steps and stop processing if uploads are pending or the data is not valid)
- the two combinators, `then` and `recover`

So in the example above what you want to say is the following:

- start by checking that there are no pending uploads (if there are, the process is interrupted)
- then in case of success validate the data (if it's invalid, the process is interrupted)
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

The following buttons are predefined and associated with the processes of the same name:

- `home`: navigate to `/`
- `summary`: navigate to the summary page
- `close`: navigate to the URL specified by `oxf.fr.detail.close.uri` or, if not specified, to the summary page
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

In fact all buttons can do the same tasks if they are configured appropriately! But by default the buttons above are preconfigured to do different tasks, for convenience.

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

### Related Orbeon Forms 4.1 and earlier functionality

Up to version 4.1, Orbeon Forms had a few predefined buttons to specify what happens with form data:

- The "Save" button to save data to the database.
- The "Submit" button to save data and show a dialog after saving (with options to clear data, keep data, navigate to another page, or close the window).
- The "Send" (AKA "workflow-send") button to save data and then allow:
    - sending an email
    - sending form data to a service
    - redirecting the user to a success or error page

For more information, see [Configuration Properties - Form Runner](http://wiki.orbeon.com/forms/doc/developer-guide/configuration-properties/configuration-properties-form-runner).

### Deprecated buttons

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

### Removed property

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