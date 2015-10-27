# XForms actions

<!-- toc -->

## Introduction

The actions in this page are called "XForms actions" because they closely match actions found in the XForms specification.

## xf:setvalue

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

## xf:dispatch

[SINCE Orbeon Forms 4.3]

Dispatch an XForms event by name to an XForms target.

- parameters
    - `name` (default parameter): specifies the name of the event to dispatch
    - `targetid`: specifies the event's target id,  `fr-form-model` by default. [SINCE Orbeon Forms 4.4]

## xf:send

[SINCE Orbeon Forms 4.3]

Send an XForms submission.

- parameters
    - `submission`: specifies the id of the submission to send

## xf:show

[SINCE Orbeon Forms 4.3]

Open an XForms dialog by id.

- parameters
    - `dialog`: specifies the id of the dialog to open

## xf:hide

[SINCE Orbeon Forms 4.10]

Close an XForms dialog by id.

- parameters
    - `dialog`: specifies the id of the dialog to close
