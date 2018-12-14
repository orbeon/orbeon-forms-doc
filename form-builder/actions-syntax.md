# Action syntax

## Availability

This feature is available since Orbeon Forms 2018.2.

## Rationale

Orbeon Forms supports services and actions. With Orbeon Forms 2018.2, a first step towards more powerful actions is introduced. There is no interface for it yet, but instead an XML configuration which you can paste into the form definition via the ["Edit Source"](edit-source.md) dialog.

## Enhancements

In addition to the features available through the ["Actions" dialog](actions.md), the following enhancements are available:

- Call an action in response to multiple events.
- Call an arbitrary number of services.
- Run actions without calling services.
- Iterate over service responses.
- Clear repeated grid or repeated sections iterations.
- Add repeated grid or repeated sections iterations.

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
```

## Syntax

### Listeners

TODO

### Actions

TODO

## See also

- [Actions](actions.md)
- [HTTP services](http-services.md)
- [Database services](database-services.md)
