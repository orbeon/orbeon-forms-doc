# Action syntax

## Availability

This feature is available since Orbeon Forms 2018.2.

## Rationale

Orbeon Forms supports services and actions. With Orbeon Forms 2018.2, a first step towards more powerful actions is introduced. There is no interface for it yet, but instead an XML configuration which you can paste into the form definition via the ["Edit Source"](edit-source.md) dialog.

## Enhancements

In addition to the features available through the ["Actions" dialog](actions.md), the following enhancements are available:

- Call an action in response to multiple events.
- Support more event types.
- Call an arbitrary number of services.
- Run actions without calling services.
- Iterate over service responses.
- Clear repeated grid or repeated section iterations.
- Add repeated grid or repeated section iterations.

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
    - the `my-repeated-grid` repeated grid is cleared
    - iterating over the XML response, for each iteration
        - adds an iteration at the end of the `my-repeated-grid` repeated grid
        - sets the value of controls on the new iteration
    - finally, all the `result-dropdown` dropdown control items are updated

```xml
    <!-- other Form Builder code here -->
    
    <fr:listener
        version="2018.2"
        events="activate"
        controls="my-button"
        actions="my-action"/>
    
    <fr:action name="my-action" version="2018.2">
    
        <fr:service-call service="my-service">
            <fr:value control="foo" ref="/*/request/foo"/>
            <fr:value control="bar" ref="/*/request/bar"/>
        </fr:service-call>
    
        <fr:repeat-clear repeat="my-repeated-grid"/>
    
        <fr:data-iterate ref="/*/response/row">
            <fr:repeat-add-iteration repeat="my-repeated-grid" at="end"/>
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

## Syntax

### Listeners

#### Basic syntax

A listener looks like this:

```xml
<fr:listener
    version="2018.2"
    events="..."
    controls="..."
    actions="..."
>
```

Attribute|Mandatory|Value|Comment
---------|---------|---------|---------
`version`|Yes|format version|always `2018.2`
`events`|Yes|space-separated list of event names|When more than one event name is present, the listener reacts if *any* of the listed events is present.
`controls`|Yes for events which relate to a particular control, like `enabled` or `value-changed`|space-separated list of control names|When more than one control name is present, the listener reacts if an event is dispatched to *any* of the listed controls.
`actions`|No, but nothing will happen if there is not at least one action referenced|space-separated list of action names|When more than one action name is present, *all* the specified actions are called when the listener reacts to an event.
    
*NOTE: It is not recommended to mix and match, in a single listener, events for which a control name is required and events for which a control name is not required. Instead, use multiple listeners.* 

#### Events supported

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

### Actions

#### Basic syntax

An action looks like this:

```xml
<fr:action version="2018.2">
```

- `version`
    - mandatory action format version
    - must always be `2018.2`

#### Calling a service
   

```xml
<fr:service-call>
```

#### Iterating over data

```xml
<fr:data-iterate>
```

#### Removing all iterations of a repeat

```xml
<fr:repeat-clear>
```

#### Adding iterations to a repeat

```xml
<fr:repeat-add-iteration>
```

#### Removing iterations from a repeat

```xml
<fr:repeat-remove-iteration>
```

#### Setting the value of a control

```xml
<fr:control-setvalue/>
``` 

#### Setting the choices of a selection control

```xml
<fr:control-setitems>
```

#### Writing to a dataset

```xml
<fr:dataset-write>
```

#### Calling a process

[SINCE Orbeon Forms 2019.1]

```xml
<fr:process-call
    scope="oxf.fr.detail.process"
    name="send"/>
```

Attribute|Mandatory|Value|Comment
---------|---------|---------|---------
`scope`|Yes|property scope|
`name` |Yes|process name  |

#### Navigating to a page or URL

[SINCE Orbeon Forms 2019.1]

```xml
<fr:navigate
    location="https://www.bbc.com/news"/>
```

Attribute|Mandatory|Value|Comment
---------|---------|---------|---------
`location`|Yes|path or URL|
`target`  |No |`_self|_blank` or name of the browsing context|where to display the location

## See also

- [Actions](actions.md)
- [HTTP services](http-services.md)
- [Database services](database-services.md)
