

## Overview

This components behaves very much like an `<xf:select1>`, except that the data populating the control doesn't come from an instance, but from a service. You specify the URI of the service and the data-bound select1 component takes care of calling it when necessary, retrieving the data, and using that data to populate a drop-down, list, or radio buttons.

You can also use this component to create chained select1 controls, where the value selected in the first one drives the list of values available in the second one, and so on. The following example illustrates this situation.

## Example

You need 3 drop-downs: _state_, _city_, and _zip code_. The _state_ drop-down lists the 50 states, the _city_ drop-down lists all the cities in the selected state, and the _zip code_ drop-down lists all the zip codes for the selected city. This is the situation you see in the following screenshot:

![](images/xbl-databound-select1.png)


To populate the _state_ drop-down, you use a pre-existing service that [returns the list of states][2].This service is at the URI `/xforms-sandbox/service/zip-states`. You specify that URI in the `resource` attribute. Other than that, the `<fr:databound-select1>` looks very much like an `<xf:select1>`:

```xml
<fr:databound-select1
    ref="state"
    appearance="minimal"
    resource="/xforms-sandbox/service/zip-states">
    <xf:label>State</xf:label>
    <xf:itemset ref="/states/state">
        <xf:label ref="@name"/>
        <xf:value ref="@abbreviation"/>
    </xf:itemset>
</fr:databound-select1>
```

So far so good: assuming you have such a service that returns the data you need, this component saves you from having to declare a submission to call the service, to declare an instance to store the data, and to call the service when appropriate. But things become more interesting when you chain another `<fr:databound-select1>`. Let's assume you have a service that [returns the list cities for a state][3], taking that state as a parameter. As for the first drop-down, you specify the URI of that service in the `resource` attribute, and use an AVT to pass the value of the parameter. The component then takes care of calling the service whenever the parameter changes, to update the data as necessary:

```xml
<fr:databound-select1
    ref="city"
    appearance="minimal"
    resource="/xforms-sandbox/service/zip-cities?state-abbreviation={state}">
    <xf:label>City</xf:label>
    <xf:itemset ref="/cities/city">
        <xf:label ref="@name"/>
        <xf:value ref="@name"/>
    </xf:itemset>
</fr:databound-select1>
```

Using this technique, you can chain as many drop-downs (or lists, or radio buttons) as necessary, letting the component take care of calling the service when necessary. You can see the [source of an example][4] using 3 `<fr:databound-select1>` and producing the result shown in screenshot you've seen earlier.

## Hiding empty dropdowns

[SINCE Orbeon Forms 4.6] The data-bound select1 component adds an attribute `itemset-empty` on the element it is bound to, and populates it with either the value `true` if the itemset you provide is empty, or the value `false` otherwise.

You can rely on the value in the `itemset-empty` attribute to hide the control when users can't select a value, if in your form it doesn't make sense to show the control when there is just no value available to select. When doing so, make sure not to make the control non-relevant in that case (e.g. don't do this an `<xf:bind relevant="…">`), as when non-relevant the control is completely disabled, and it wouldn't be able to re-evaluate the AVT you provided through the `resource` attribute. Instead, you should use CSS for this, for instance:

1. Add an `fr:bind` or update the relevant `fr:bind` for this control, adding a `fr:itemset-empty` [[custom MIP|XForms ~ Binds#custom-mips]], as in: `<xf:bind ref="…" fr:itemset-empty="@itemset-empty = 'true'">`. If you're doing this in Form Builder, edit the source of the form, find the `xf:bind` that corresponds to your control, and add the attribute fr:itemset-empty="@itemset-empty = 'true'".
2. Add CSS to hide the control, using the class added on the control based on the value of the custom MIP, as in: `.orbeon .fr-itemset-empty-true { display: none }`. If you're doing this in Form Builder, you can place this CSS in your [[custom CSS file|Form Runner ~ Configuration properties#adding-your-own-css]], or if you're just using this in one form, in the source of the form, by editing the source of the form, and below the `xh:title`, adding:

    ```xml
    <xh:style type="text/css">
        .orbeon .fr-itemset-empty-true { display: none }
    </xh:style>
    ```

You can of course use the `itemset-empty` attribute for other things. For instance, you can make the control required only if the itemset isn't empty, by adding, on the `xf:bind,` the attribute `required="@itemset-empty = 'false'"`.

[2]: https://github.com/orbeon/orbeon-forms/blob/master/src/resources/apps/xforms-sandbox/services/zip-states.xpl
[3]: https://github.com/orbeon/orbeon-forms/blob/master/src/resources/apps/xforms-sandbox/services/zip-cities.xpl
[4]: https://github.com/orbeon/orbeon-forms/blob/master/src/resources-packaged/xbl/orbeon/databound-select1/databound-select1-unittest.xhtml
