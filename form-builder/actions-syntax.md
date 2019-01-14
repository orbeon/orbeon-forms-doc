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

    <<fr:listener
        version="2018.2"
        .../>
        
    <fr:action name="my-action" version="2018.2">
        ...
    </fr:action
    
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

- `version`
    - mandatory action format version
    - must always be `2018.2`
- `events`
    - mandatory
    - space-separated list of event names
    - When more than one event name is present, the listener reacts if *any* of the listed events is present.
- `controls`
    - only mandatory for events which relate to a particular control, like `enabled` or `value-changed`
    - space-separated list of control names
    - When more than one control name is present, the listener reacts if an event is dispatched to *any* of the listed controls.
- `actions`
    - optional, but nothing will happen if there is not at least one action referenced
    - space-separated list of action names
    - When more than one action name is present, *all* the specified actions are called when the listener reacts to an event. 

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

TODO

An action looks like this:

```xml
<fr:action version="2018.2">
```

- `version`
    - mandatory action format version
    - must always be `2018.2`
    
    

```xml
<fr:service-call>
```

```xml
<fr:data-iterate>
```

```xml
<fr:repeat-clear>
```

```xml
<fr:repeat-add-iteration>
```

```xml
<fr:repeat-remove-iteration>
```

```xml
<fr:control-setvalue>
```

```xml
<fr:control-setitems>
```

```xml
<fr:control-setitems>
```

```xml
<fr:dataset-write>
```

## See also

- [Actions](actions.md)
- [HTTP services](http-services.md)
- [Database services](database-services.md)
