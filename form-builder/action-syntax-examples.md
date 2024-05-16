# Action syntax examples
  
## Overview


]This document provides examples of the syntax used to define actions in the form builder.

## Example 1: Xxx

The Nobel Prize organization exposes a [REST API](https://www.nobelprize.org/about/developer-zone-2/). We would like to create a form that queries that API to return the 2023 Nobel Prize winners, and show these details in a table.

To do this, we start by creating a simple form with a nested repetition:

- a repeated section for the Nobel Prizes
- a nested repeated grid for the laureates

Here is how the form looks like in Form Builder:

![Nobel Prize form](images/action-syntax-nobel-form.png)

We then also create an HTTP Service endpoint:

![Nobel Prize service](images/action-syntax-nobel-service.png)

It points to the following API endpoint:

```
https://api.nobelprize.org/2.1/nobelPrizes?nobelPrizeYear=2023
```

Finally, we write, using the Form Builder's action syntax, an action that:

- runs upon form load
- calls the Nobel Prize service
- clears the `prizes` repeat
- iterates over the Nobel Prizes and laureates
- adds iterations to the `prizes` and `laureates` repeats
- sets values in the controls

There is a nested iteration due to the nested repeats. here is what the complete listener and action look like:

```xml
<fr:listener version="2018.2" events="form-load-after-controls" actions="my-action"/>

<fr:action name="my-action" version="2018.2">
    <fr:service-call service="get-nobel-prizes"/>
    <fr:repeat-clear repeat="prizes"/>
    <fr:data-iterate ref="/*/nobelPrizes/_">
        <fr:repeat-add-iteration repeat="prizes" at="end"/>
        <fr:control-setvalue value="awardYear" control="year" at="end"/>
        <fr:control-setvalue value="category/en" control="category" at="end"/>
        <fr:repeat-clear repeat="laureates"/>
        <fr:data-iterate ref="laureates/_">
            <fr:repeat-add-iteration repeat="laureates"/>
            <fr:control-setvalue value="knownName" control="known-name" at="end"/>
            <fr:control-setvalue value="fullName" control="surname" at="end"/>
            <fr:control-setvalue value="motivation/en" control="motivation" at="end"/>
        </fr:data-iterate>
    </fr:data-iterate>
</fr:action>
```

When you test or run the deployed form, you see the Nobel Prize winners for 2023:

![Nobel Prize winners](images/action-syntax-nobel-result.png)

## See also

- [Action syntax](actions-syntax.md)
- [Editing the source code of the form definition](edit-source.md)
- [Synchronizing repeated content](synchronize-repeated-content.md)
- [Actions](actions.md)
- [HTTP services](http-services.md)
- [Database services](database-services.md)
