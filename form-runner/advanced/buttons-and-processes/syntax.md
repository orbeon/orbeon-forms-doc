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

The wildcards, as usual with Form Funner, can specify a form's application and form names. Here, the process is available to all forms in all apps because of the `*.*` wildcard.

The inline value, staring in this example with `require-uploads`, actuallydescribes the process. It follows a DSL (domain-specific language) described in more details below. The process DSL supports:

- actions with or without parameters
- combinators to handle success and failure
- grouping with parentheses
- conditions ("if")
- sub-processes
- a set of standard actions, including:
    - [core actions](actions-core.md)
    - [Form Runner actions](actions-form-runner.md)
    - [XForms actions](actions-xforms.md)

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
