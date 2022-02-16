# Action syntax

## Availability

This feature is available since Orbeon Forms 2018.2.

## Rationale

Orbeon Forms supports services and actions. With Orbeon Forms 2018.2, a first step towards more powerful actions is introduced. There is no interface for it yet, but instead an XML configuration which you can paste into the form definition via the ["Edit Source"](edit-source.md) dialog.

## Enhancements

In addition to the features available through the ["Actions" dialog](actions.md), the following enhancements are available:

- Call an action in response to *multiple events*.
- Support *more event types*.
- Call an *arbitrary number* of services.
- Run actions *without* calling services.
- Clear repeated grid or repeated section repetitions.
- Add repeated grid or repeated section repetitions.
- *Repeatedly run* parts of an action. [SINCE Orbeon Forms 2019.1]
- *Conditionally run* parts of an action. [SINCE Orbeon Forms 2019.1]

## Updating the form definition

You place listeners and actions within the source code, preferably before the end of the main `<xf:model>` content. For example: 

```xml
    <!-- other Form Builder code here -->

    <fr:listener
        version="2018.2"
        .../>
        
    <fr:action name="my-action" version="2018.2">
        ...
    </fr:action>
    
    <!-- Put `<fr:listener>` and `<fr:action>` just above this. -->
</xf:model>
```

## Example

The following example:

- listens to an activation event on the button `my-button`
- in response, calls the `my-action` action, which
    - sets values from controls into the `my-service` service request
    - calls the `my-service` service
- when the service call completes
    - the `my-table` repeated grid is cleared
    - iterating over the XML response, for each iteration
        - adds a repetition at the end of the `my-table` repeated grid
        - sets the value of controls on the new repetition
    - finally, all the `result-dropdown` dropdown control items are updated

```xml
    <!-- other Form Builder code here -->
    
    <fr:listener
        version="2018.2"
        events="activated"
        controls="my-button"
        actions="my-action"/>
    
    <fr:action name="my-action" version="2018.2">
    
        <fr:service-call service="my-service">
            <fr:value control="foo" ref="/*/foo"/>
            <fr:value control="bar" ref="/*/bar"/>
        </fr:service-call>
    
        <fr:repeat-clear repeat="my-table"/>
    
        <fr:data-iterate ref="/*/row">
            <fr:repeat-add-iteration repeat="my-table" at="end"/>
            <fr:control-setvalue value="@field" control="result-field" at="end"/>
            <fr:control-setvalue value="@dropdown" control="result-dropdown" at="end"/>
        </fr:data-iterate>
    
        <fr:control-setitems
            items="/*/response/item"
            label="@label"
            value="@value"
            control="result-dropdown"/>
    
    </fr:action>

    <!-- Put `<fr:listener>` and `<fr:action>` just above this. -->
</xf:model>
```

## Listeners

### Basic syntax

A listener looks like this:

```xml
<fr:listener
    version="2018.2"
    modes="..."
    events="..."
    controls="..."
    actions="..."
>
```

Attribute|Mandatory|Value|Comment
---------|---------|---------|---------
`version`|Yes|format version|always `2018.2`
`modes`|No|space-separated list of modes|The listener is enabled for each mode listed only. If absent, the listener is enabled for all modes.
`events`|Yes|space-separated list of event names|When more than one event name is present, the listener reacts if *any* of the listed events is present.
`controls`|Yes for events which relate to a particular control, like `enabled` or `value-changed`|space-separated list of control names|When more than one control name is present, the listener reacts if an event is dispatched to *any* of the listed controls.
`actions`|No, but nothing will happen if there is not at least one action referenced|space-separated list of action names|When more than one action name is present, *all* the specified actions are called when the listener reacts to an event.
    
*NOTE: It is not recommended to mix and match, in a single listener, events for which a control name is required and events for which a control name is not required. Instead, use multiple listeners.* 

### Modes supported

- `new`
- `edit`
- `view`
- `pdf`

### Events supported

Controls:

- `enabled`: the control has become enabled
- `disabled`: the control has become disabled
- `visible`: the control has become visible (for example in a wizard page)
- `hidden`: the control has become hidden (for example in a wizard page)
- `value-changed`: the value of an enabled control has changed
- `activated`: the control has been activated (clicked, or enter in text field) 
- `item-selected`: an item of an enabled control has been selected
- `item-deselected`: an item of an enabled control has been deselected

Form load:

- `form-load-before-data`: run before the data's initial values are calculated
- `form-load-after-data`: run when the data is ready
- `form-load-after-controls`: run after the controls are ready

See also [Running processes upon page load](/configuration/properties/form-runner-detail-page.md#running-processes-upon-page-load) for the detail of the form load events.

## Actions

### Basic syntax

An action looks like this:

```xml
<fr:action
    version="2018.2" 
    name="my-action" 
>
```
    
Attribute|Mandatory|Value|Comment
---------|---------|---------|---------
`version`|Yes|format version|always `2018.2`
`name`   |Yes|action name|must be unique in the form definition    

## Control structures

### Iterating over data

[SINCE Orbeon Forms 2019.1]

#### Basic usage

```xml
<fr:data-iterate ref="...expression...">
    ...
</fr:data-iterate>
```

`<fr:data-iterate>` allows you to iterate over data. The contained actions are executed once for each value returned by the expression.

Containing actions can include one or more calls to services.  

Attribute|Mandatory|Value|Comment
---------|---------|---------|---------
`ref`|Yes|sequence XPath expression|runs in the current XPath evaluation context

In the following example, each repetition adds a row to the grid, calls a service, passing the attachment id, and sets the attachment value on the last row.

```xml
<fr:action name="populate-attachments" version="2018.2">

    <fr:service-call service="get-attachments-list"/>
    <fr:repeat-clear repeat="my-grid"/>

    <fr:data-iterate ref="/*/row">
        <fr:repeat-add-iteration repeat="my-grid" at="end"/>
        <fr:service-call service="get-attachment">
            <fr:url-param name="text" value="attachment-id"/>
        </fr:service-call>
        <fr:control-setattachment control="my-attachment" at="end"/>
    </fr:data-iterate>

</fr:action>
```

#### Nesting of iterations

[SINCE Orbeon Forms 2019.2]

Calls to `<fr:data-iterate>` can be nested. This allows, for example, filling nested repeated sections and/or grids with the result of a service call that returns hierarchical data.

Example:

```xml
<fr:action name="my-action" version="2018.2">
    <fr:service-call service="get-nobel-prizes"/>
    <fr:repeat-clear repeat="prizes"/>
    <fr:data-iterate ref="/*/prizes/_">
        <fr:repeat-add-iteration repeat="prizes"/>
        <fr:control-setvalue value="year"     control="year"     at="end"/>
        <fr:control-setvalue value="category" control="category" at="end"/>
        <fr:repeat-clear repeat="laureates"/>
        <fr:data-iterate ref="laureates/_">
            <fr:repeat-add-iteration repeat="laureates"/>
            <fr:control-setvalue value="firstname"  control="firstname"  at="end"/>
            <fr:control-setvalue value="surname"    control="surname"    at="end"/>
            <fr:control-setvalue value="motivation" control="motivation" at="end"/>
        </fr:data-iterate>
    </fr:data-iterate>
</fr:action>
``` 

### Conditions

[SINCE Orbeon Forms 2019.1]

```xml
<fr:if condition="...boolean expression...">
    ...
</fr:if>
```

`<fr:if>` allows you to conditionally run a block of actions.

Attribute|Mandatory|Value|Comment
---------|---------|---------|---------
`condition`|Yes|boolean XPath expression|runs in the current XPath evaluation context

In the following example, with the repetition performed by `<fr:data-iterate>`, the call to the service that retrieves an attachment depends on whether there is a non-blank attachment id provided. 

```xml
<fr:action name="populate-attachments" version="2018.2">

    <fr:service-call service="get-attachments-list"/>
    <fr:repeat-clear repeat="my-grid"/>

    <fr:data-iterate ref="/*/row">
        <fr:repeat-add-iteration repeat="my-grid" at="end"/>
        <fr:if condition="xxf:non-blank(attachment-id)">
            <fr:service-call service="get-attachment">
                <fr:url-param name="text" value="attachment-id"/>
            </fr:service-call>
            <fr:control-setattachment control="my-attachment" at="end"/>
        </fr:if>
    </fr:data-iterate>

</fr:action>
```

## Individual actions

### Calling a service

```xml
<fr:service-call service="...service name...">
    <fr:value value="..." ref="..."/>
</fr:service-call>
```

`<fr:service-call>` calls a service by name.

Attribute|Mandatory|Value|Comment
---------|---------|---------|---------
`service`|Yes|name of the service to call|must be an existing service

#### Passing a value

```xml
<fr:value value="..." ref="..."/>
```

or:

```xml
<fr:value control="..." ref="..."/>
```

When calling an [HTTP service](/form-builder/http-services.md), you can set XML request body values using nested `<fr:value>` elements.

Attribute|Mandatory|Value|Comment
---------|---------|---------|---------
`control`|No|control name|either this or `value` must be specified
`value`|No|value expression|either this or `control` must be specified
`ref`|Yes|destination expression|points to an element or attribute in the request XML

#### Passing a URL parameter

```xml
<fr:url-param name="..." value="..."/>
```

When calling an [HTTP service](/form-builder/http-services.md), you can pass URL parameters using nested `<fr:url-param>` elements.

Attribute|Mandatory|Value|Comment
---------|---------|---------|---------
`control`|No|control name|either this or `value` must be specified
`value`|No|value expression|either this or `control` must be specified
`name`|Yes|parameter name|URL parameter name

#### Passing a SQL parameter

```xml
<fr:sql-param index="..." value="..."/>
```

When calling a [database service](/form-builder/database-services.md), you can pass parameters using nested `<fr:sql-param>` elements.

Attribute|Mandatory|Value|Comment
---------|---------|---------|---------
`control`|No|control name|either this or `value` must be specified
`value`|No|value expression|either this or `control` must be specified
`index`|Yes|positive integer|SQL query parameter index

### Removing all repetitions of a repeat

```xml
<fr:repeat-clear
    repeat="..."
    at="..."/>
```

Attribute|Mandatory|Value|Comment
---------|---------|---------|---------
`repeat`|Yes|repeated grid or repeated section name| |
`at`|No|space-delimited position tokens: `start`, `end`, or a positive integer|missing leading tokens default to `end`|

This action starts by identifying a single repeated grid or section with the `repeat` attribute. See `<fr:repeat-add-iteration>` for details. The only difference is that with this action, `at` is only used to identify the ancestor repeated sections if any.

With `my-repeated-grid` nested within `my-repeated-section`:

- Remove all repetitions of `my-repeated-section`:
    ```xml
    <fr:repeat-clear repeat="my-repeated-section"/>
    ```
- Remove all repetitions of the last `my-repeated-grid`:
    ```xml
    <fr:repeat-clear repeat="my-repeated-grid" at="end"/>
    ```
- Remove all repetitions of the first `my-repeated-grid`:
    ```xml
    <fr:repeat-clear repeat="my-repeated-grid" at="start"/>
    ```
- Remove all repetitions of the second `my-repeated-grid`:
    ```xml
    <fr:repeat-clear repeat="my-repeated-grid" at="2"/>
    ```

### Adding repetitions to a repeat

```xml
<fr:repeat-add-iteration
    repeat="..."
    at="..."/>
```

Attribute|Mandatory|Value|Comment
---------|---------|---------|---------
`repeat`|Yes|repeated grid or repeated section name| |
`at`|No|space-delimited position tokens: `start`, `end`, or a positive integer|missing leading tokens default to `end`|

This action starts by identifying a single repeated grid or section with the `repeat` attribute. If the repeated grid or section is at the top-level, there is only one possible match. If the repeated grid or section is *nested* within one or more repeated sections, then a single repetition of the ancestor repeated sections is determined using the optional `at` attribute.

With `my-repeated-grid` nested within `my-repeated-section`:

- Insert a new repetition at the end of the last `my-repeated-grid`:
    ```xml
    <fr:repeat-add-iteration repeat="my-repeated-grid" at="end end"/>
    ```
    or:
    ```xml
    <fr:repeat-add-iteration repeat="my-repeated-grid" at="end"/>
    ```
    or:
    ```xml
    <fr:repeat-add-iteration repeat="my-repeated-grid"/>
    ```
- Insert a new repetition at the end of the first `my-repeated-grid`:
    ```xml
    <fr:repeat-add-iteration repeat="my-repeated-grid" at="start end"/>
    ```
- Insert a new repetition at the start of the first `my-repeated-grid`:
    ```xml
    <fr:repeat-add-iteration repeat="my-repeated-grid" at="start start"/>
    ```
- Insert a new repetition after repetition 2 of the third `my-repeated-grid`:
    ```xml
    <fr:repeat-add-iteration repeat="my-repeated-grid" at="3 2"/>
    ```
- Insert a new repetition at the end of `my-repeated-section`:
    ```xml
    <fr:repeat-add-iteration repeat="my-repeated-section" at="end"/>
    ```
    or:
    ```xml
    <fr:repeat-add-iteration repeat="my-repeated-section"/>
    ```

### Removing repetitions from a repeat

```xml
<fr:repeat-remove-iteration
    repeat="..."
    at="..."/>
```

Attribute|Mandatory|Value|Comment
---------|---------|---------|---------
`repeat`|Yes|repeated grid or repeated section name| |
`at`|No|space-delimited position tokens: `start`, `end`, or a positive integer|missing leading tokens default to `end`|

This action starts by identifying a single repeated grid or section with the `repeat` attribute. See `<fr:repeat-add-iteration>` for details.

With `my-repeated-grid` nested within `my-repeated-section`:

- Remove the last repetition of the last `my-repeated-grid`:
    ```xml
    <fr:repeat-remove-iteration repeat="my-repeated-grid" at="end end"/>
    ```
    or:
    ```xml
    <fr:repeat-remove-iteration repeat="my-repeated-grid" at="end"/>
    ```
    or:
    ```xml
    <fr:repeat-remove-iteration repeat="my-repeated-grid"/>
    ```
- Remove the last repetition of the first `my-repeated-grid`:
    ```xml
    <fr:repeat-remove-iteration repeat="my-repeated-grid" at="start end"/>
    ```
- Remove repetition 2 of the third `my-repeated-grid`:
    ```xml
    <fr:repeat-remove-iteration repeat="my-repeated-grid" at="3 2"/>
    ``` 

### Setting the value of a control

```xml
<fr:control-setvalue/>
```

`<fr:control-setvalue/>` sets the value of a form control.

Attribute|Mandatory|Value|Comment
---------|---------|---------|---------
`control`|Yes|control name| |
`value`|Yes|value expression|value to set| |
`at`|No|space-delimited position tokens: `start`, `end`, or a positive integer|missing leading tokens default to `end`|
 
```xml
<fr:control-setvalue
    control="my-control"
    value="current-date()"/>
```

### Clearing the value of a control

[SINCE Orbeon Forms 2020.1]

```xml
<fr:control-clear/>
``` 

`<fr:control-clear/>` clears the value of a control. For attachment controls, this clears the file but also the file metadata (filename, mediatype, and size).

Attribute|Mandatory|Value|Comment
---------|---------|---------|---------
`control`|Yes|control name| |
`at`|No|space-delimited position tokens: `start`, `end`, or a positive integer|missing leading tokens default to `end`|

Example:

```xml
<fr:control-clear control="my-control"/>
```

### Setting the choices of a selection control

```xml
<fr:control-setitems/>
```

`<fr:control-setitems/>` sets the choices (or "itemset") of a selection control such as a dropdown or radio buttons.

Attribute|Mandatory|Value|Comment
---------|---------|---------|---------
`control`|Yes|control name| |
`items`|Yes|XPath expression returning one XPath item for each choice| |
`label`|Yes|relative XPath expression returning the label for the current choice| |
`hint`|No|relative XPath expression returning the hint for the current choice|\[SINCE Orbeon Forms 2020.1\]|
`value`|Yes|relative XPath expression returning the value for the current choice| |
`at`|No|space-delimited position tokens: `start`, `end`, or a positive integer|missing leading tokens default to `end`|

_NOTE: Hints are only supported for checkboxes and radio buttons as of Orbeon Forms 2020.1._

### Writing to a dataset

```xml
<fr:dataset-write
    name="..."/>
```

Attribute|Mandatory|Value|Comment
---------|---------|---------|---------
`name`|Yes|dataset name| |

This action takes the latest service result and saves it to the dataset specified by name. Example:

```xml
<fr:dataset-write
    name="my-dataset"/>
```

### Calling a process

[SINCE Orbeon Forms 2019.1]

```xml
<fr:process-call
    scope="oxf.fr.detail.process"
    name="send"/>
```

Attribute|Mandatory|Value|Comment
---------|---------|---------|---------
`scope`|Yes|property scope| |
`name` |Yes|process name  | |

### Navigating to a page or URL

[SINCE Orbeon Forms 2019.1]

```xml
<fr:navigate
    location="https://www.bbc.com/news"/>
```

Attribute|Mandatory|Value|Comment
---------|---------|---------|---------
`location`|Yes|path or URL| |
`target`  |No |`_self` or `_blank` or name of the browsing context|where to display the location

### Setting the value of an attachment control

[SINCE Orbeon Forms 2019.1]

```xml
<fr:control-setattachment
    control="..."/>
``` 

Attribute|Mandatory|Value|Comment
---------|---------|---------|---------
`control`|Yes|control name| |

When the response of a service is binary, this action allows setting the value of an attachment control to the content of the service response:

```xml
<fr:control-setattachment
    control="my-attachment"/>
``` 

This supports the following controls:

- `<fr:attachment>`
- `<fr:image-attachment>`

The mediatype and received by the service and the actual size of the attachment are automatically set. However, the filename is not set automatically.

See also the following actions:
 
- `<fr:control-setfilename>`
- `<fr:control-setmediatype>`

### Setting the filename of an attachment control

[SINCE Orbeon Forms 2019.1]

```xml
<fr:control-setfilename
    control="..."
    value="..."/>
```

Attribute|Mandatory|Value|Comment
---------|---------|---------|---------
`control`|Yes|control name| |
`value`|Yes|value expression|value of the filename to set

This action allows setting the filename of an attachment control. Example:

```xml
<fr:control-setfilename
    control="my-attachment"
    value="'My Image.png'"/>
```

This supports the following controls:

- `<fr:attachment>`
- `<fr:image-attachment>`

### Setting the mediatype of an attachment control

[SINCE Orbeon Forms 2019.1]

```xml
<fr:control-setmediatype
    control="..."
    value="..."/>
```

Attribute|Mandatory|Value|Comment
---------|---------|---------|---------
`control`|Yes|control name| |
`value`|Yes|value expression|value of the mediatype to set

This action allows setting the mediatype of an attachment control. Example:

```xml
<fr:control-setmediatype
    control="my-attachment"
    value="'image/png'"/>
```

This supports the following controls:

- `<fr:attachment>`
- `<fr:image-attachment>`

Note that the `<fr:control-setattachment>` action automatically sets a mediatype.

### Alert for debugging

[SINCE Orbeon Forms 2022.1, 2021.1.2, 2020.1.7] The `<fr:alert>` action is intended to be used for debugging, allowing you to get some visibility on the value of intermediary results, or more generally the value of any expression is at a given point. The value of the `message` attribute uses the [AVT syntax](/xforms/core/attribute-value-templates.md), so the example below would show a dialog with the message "The answer is 42".

```xml
<fr:alert message="The answer is {40+2}"/>
```

## Error handling

With Orbeon Forms 2018.2, errors when running services are silently ignored and the action continues.

With Orbeon Forms 2019.1, errors:

1. cause the entire action to stop.
2. run the `oxf.fr.detail.process.action-service-error` process

The default implementation of the service error process is as follows:

```xml
<property as="xs:string"  name="oxf.fr.detail.process.action-service-error.*.*">
    error-message("service-error")
</property>
```

You can provide your own service error process in properties-local.xml.     

## Evaluation context of XPath expressions

The context item used for XPath evaluations is set as follows:

- At the beginning of an action, it is the root element of the form data.
- Within an `<fr:data-iterate>`, and until a service response is available, it is the current iteration item.
- After a service call, whether at the top-level or within an `<fr:data-iterate>`, it is the root element of the preceding action response.

[SINCE Orbeon Forms 2019.1]

You can explicitly set the XPath evaluation context to the current iteration item within a `<fr:data-iterate>` with the `expression-context` attribute set to `current-iteration`.

In the following example, without the `expression-context="current-iteration"` attribute, the second `<fr:value>` would evaluate within the context of the first service call's response.

```xml
<fr:data-iterate ref="/*/row">
    <fr:service-call service="my-first-service">
        <fr:value
            value="foo"
            ref="/some/path"/>
    </fr:service-call>
    <fr:service-call service="my-second-service">
        <fr:value
            expression-context="current-iteration"
            value="bar"
            ref="/some/other/path"/>
    </fr:service-call>
</fr:data-iterate>
```

To be more explicit, the attribute can also be set on the first `<fr:value>`:

```xml
<fr:data-iterate ref="/*/row">
    <fr:service-call service="my-first-service">
        <fr:value
            expression-context="current-iteration"
            value="foo"
            ref="/some/path"/>
    </fr:service-call>
    <fr:service-call service="my-second-service">
        <fr:value
            expression-context="current-iteration"
            value="bar"
            ref="/some/other/path"/>
    </fr:service-call>
</fr:data-iterate>
```

## See also

- [Editing the source code of the form definition](/form-builder/edit-source.md)
- [Actions](actions.md)
- [HTTP services](http-services.md)
- [Database services](database-services.md)
