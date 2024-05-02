# XForms actions

## Introduction

The actions in this page are called "XForms actions" because they closely match actions found in the XForms specification.

## xf:setvalue

[SINCE Orbeon Forms 4.7]

Set a value in the form data.

- parameters
    - `ref`: required XPath expression, relative to the root element of the form data, points to the nodes to set.
    - `value`: optional XPath expression, relative to the node to set, specifies the value to set.
    - `all`: [SINCE Orbeon Forms 2018.1] optional boolean
        - If `"false"` (the default) the First-item rule is applied: if the `ref` XPath expression returns a sequence of size greater than 1, the first item in the sequence is used.
        - If `"true"`, all the items in the sequence are used.

If `value` is omitted, the value selected is the empty string.

Examples:

```xpath
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

## xf:insert

[SINCE Orbeon Forms 2021.1]

Insert data in an XML document.

- parameters
    - `origin`: required XPath expression, relative to the root element of the form data, points to the nodes to insert.
    - `before`: optional XPath expression, relative to the root element of the form data, points to a node to insert *before*.
    - `after`: optional XPath expression, relative to the root element of the form data, points to a node to insert *after*.
    - `into`: optional XPath expression, relative to the root element of the form data, points to a node to insert *into*.

Only one of `into`, `before`, or `after` is used, as follows:

- If `before` is specified and contains at least one node, then insertion occurs *before* that node.
- Or else, if `after` is specified and contains at least one node, then insertion occurs *after* that node.
- Or else, if `into` is specified and contains at least one node, then insertion occurs *into* that node.

xxx

Examples:

```xpath
xf:insert(
    into  = "xxxx",
    origin = "xxxx"
)
```

## xf:delete

[SINCE Orbeon Forms 2021.1]

Delete data from an XML document.

- parameters
    - `ref`: required XPath expression, relative to the root element of the form data, points to the nodes to delete.

Examples:

```xpath
xf:delete(
    ref = "xxxx"
)
```