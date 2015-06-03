> [[Home]] ▸ Form Runner ▸ [[XBL Components|Form Runner ~ XBL Components]]

## Overview

[Autocomplete][1] is a feature provided by many applications, and this component makes it easy to add an autocomplete field to your form. The autocomplete controls is not unlike an `<xf:select1>` selection controls:

* You use a [single-node binding attribute][2] to bind the control to the node holding the current value for the control.
* You use a combination of `<xf:itemset>` and `<xf:item>` to define the values suggested to the user. Just as with `<xf:select1>`, what is visible to users are item's labels and what is stored in the node to which your autocomplete is bound is the selected item's value.
* Just like the `<xf:select1>`, the autocomplete only implements a closed selection: users will only be able to select a value which exists in the itemset. The value of the node bound to the autocomplete won't be set to the value typed by users in the field, unless they select an item from the suggestion list or type text which is identical to one of the item's label.

![Auto Complete field used for a list of countrie][3]

## Modes

The autocomplete can work in one of three modes, which in order of increasing complexity, are referred to as: _static_, _resource_, and _dynamic_.

### Static

* When to use the _static_ mode?
    * When you can reasonably determine the full list of suggested values without having to do any processing when users type in the field, such as submissions to a service you provide. You will typically use this mode when the list of suggested values isn't extremly large. Examples include a list of states, of countries, or of departments in your company.
    * When the default filtering mechanism works for you (more on this below).
* Does the itemset really need to be static?

    No, you don't need hard code the list of values in your form, or even to know it when you generate the page. You can fetch the list from a service using an `<xf:submission>`, and this list can change depending on other values entered by users in other fields of the page. But at a given point in time, that list must be complete; you can't change it depending what users are currently typing in the field as they type.

* How does the filtering work?

    In this mode, the autocomplete field determines which values are shown in the suggestion based on what users typed in the field. It will only show items which label starts with the text entered by users. The comparison is not case sensitive. For instance, typing "ar" in a field that expects a list of countries with show "Argentina", amongst other countries, but not "Saudi Arabia".

* What does the syntax look like?

    ```xml
    <fr:autocomplete ref="country-name" dynamic-itemset="false">
        <xf:label>Enter a country name: </xf:label>
        <xf:itemset ref="instance('all-countries')/country">
            <xf:label ref="name"/>
            <xf:value ref="name"/>
        </xf:itemset>
    </fr:autocomplete>
    ```

### Resource

* When to use the _resource_ mode?

    When you have a service responding to an HTTP GET, that, given the current value users typed in the search field, returns an XML document with the list of suggestions.

* What does the syntax look like?

    ```xml
    <fr:autocomplete
            ref="country-name"
            labelref="country-name/@label"
            resource="/fr/service/custom/orbeon/controls/countries?country-name={$fr-search-value}"
            max-results-displayed="4">
        <xf:label>Country code: </xf:label>
        <xf:itemset ref="/countries/country">
            <xf:label ref="name"/>
            <xf:value ref="us-code"/>
        </xf:itemset>
        <xf:alert>Value is mandatory</xf:alert>
    </fr:autocomplete>
    ```

    * `ref`: binds the autocomplete to a node that will that the autocomplete will populate with the selected item's value.
    * `labelref`: binds the autocomplete to a node that will that the autocomplete will populate with the selected item's label. This attribute is optional, but if you don't specify it, when loading a form where an autocomplete already has a value, then autocomplete will show empty, which most likely will be seen as incorrect behavior by users. This is because in _resource_ mode, as well as in _dynamic_ mode (see below), the autocomplete doesn't know about all the items, and thus can't automatically infer the label it needs to show users based on a value stored in the instance. `labelref` solves this problem by also storing the label in the instance.
    * `resource`: points to an HTTP service responding with an XML document. It is interpreted as an AVT, and you can use the `$fr-search-value` to refer to the value users typed so far in the search field, as done in the above example.
    * `<xf:itemset>`: points to the items in the XML returned by the service, and for each item to its label and value.
    * Whenever the result of the `resource` AVT evaluates to a different URI, a request is made by the autocomplete to that new URI to retrieve the a new itemset. Typically, your `resource` AVT will use `$fr-search-value`, and thus whenever users change the value in the search field, your service will be called. But this mechanism is general, and should your AVT, say, refer to a node in an instance that can change, then the service will we called again every time a change occurs.


### Dynamic

* When to use the _dynamic_ mode?

    When you want the list of suggestions to change dynamically and that the _resource_ mode doesn't offer the flexibility you need. (Say, the suggestions are not retrieved from a service, or a service that takes a POST rather than a GET.)

* How to update the itemset?

    In most cases, you will want to update the itemset as users type, based on the value they entered. For this, listen to the event `fr-search-changed` on the `<fr:autocomplete>`, and run an action that updates the itemset. You are in control of where the data comes from, what subset of the data is to be displayed in the suggestion list based on what the user types, and when the suggestion is to be made.

    For instance, the following code reacting to the `fr-search-changed` event is written is a such a way that:

    * The suggestion list shows up only after users enter at least 2 characters.
    * The suggestion list is updating by running a submission which calls a service, providing the text typed by users so far.

    ```xml
    <fr:autocomplete
            ref="instance('selected-countries')/dynamic"
            labelref="instance('selected-countries')/dynamic/@label"
            id="dynamic-autocomplete"
            dynamic-itemset="true">

        <!-- React to user searching -->
        <xf:action ev:event="fr-search-changed">
            <xf:var
                name="search-value"
                value="event('fr-search-value')"/>
            <xf:var
                name="make-suggestion"
                value="string-length($search-value) >= 2"/>
            <xf:action if="$make-suggestion">
                <!-- Update itemset -->
                <xf:setvalue
                    ref="instance('search-dynamic')/country-name"
                    value="$search-value"/>
                <xf:send submission="update-countries"/>
            </xf:action>
            <xf:action if="not($make-suggestion)">
                <!-- Delete itemset -->
                <xf:delete
                    ref="instance('searched-countries')/country"/>
            </xf:action>
        </xf:action>

        <xf:label>Country code: </xf:label>
        <xf:itemset ref="instance('searched-countries')/country">
            <xf:label ref="name"/>
            <xf:value ref="us-code"/>
        </xf:itemset>
    </fr:autocomplete>
    ```

*For more on the labelref attribute, see the above section the _resource_ mode.*


## Events

* `fr-search-changed` – What did the user type in the field?

    When using the autocomplete in dynamic itemset mode, you can listen on the `fr-search-changed` event to be notified when the value typed in the field changes. This event is dispatched as users type in the field, just like the `xforms-value-changed` event would for an incremental `<xf:input>`. The preceding example uses this event to update the itemset as users type in the field.

* `xforms-value-changed` – When did the user make a selection?

    Just as with other XForms controls, you can listen on `xforms-value-changed` to be notified when the value of the node bound to the autocomplete changes. That event is also dispatched when users make a selection from the suggestion list, or when they happen to have typed a value that exactly matches one of the items in the itemset. [AS OF 2011-06-02] This event is dispatched when the autocomplete looses the focus. At that time, the node bound to the autocomplete is also updated with the value corresponding to the label typed or selected by users. If the label in the field doesn't correspond to label of any item in the itemset, both the value of the node bound to the autocomplete as well as the content of the search field are set to empty string.

## Setting the content of the text field

### Setting by label

You can set the content of the text field by sending the `fr-set-label` event to the autocomplete component, with `label` as context information. For instance:

```xml
<xf:dispatch
        event="DOMActivate"
        targetid="my-autocomplete"
        name="fr-set-label">
    <xf:property name="label" select="'Canada'"/>
</xf:dispatch>
```


Just as if users had typed that label, if you are in dynamic itemset mode, this will trigger the `fr-search``-changed` event, upon which your code can change the itemset. If you do change the itemset, and there is a single _value_ corresponding to the _label_ you specified, then the node to which the autocomplete is bound is set to that _value_.

### Setting by value

When using a static itemset, you can set the value of the node you bind to the autocomplete control. This value can either be set initially (i.e. be present in the instance when the page is loaded), or can be set dynamically. When this happens, the autocomplete control will populate the search field with the label corresponding to that value, if there is one.

Don't set the value of the node bound to an autocomplete control if you are using the dynamic or resource mode. This is because when the itemset is dynamic, you load the itemset based on the search term entered by users, responding to the `fr-search-changed` event. If the value is set, the autocomplete can't dispatch a `fr-search-changed` event, since it doesn't know the label yet, and your code won't load the appropriate itemset that contains the label for the new value. In dynamic itemset mode, always use the `fr-set-label` event to indirectly set the value of the control. If you want the autocomplete to be populated based on a value in the instance when the form loads, on `xforms-model-construct-done` you need to find what the label corresponding to this value is, and dispatch an `fr-set-label` event with that label. [AS OF 2011-06-02] As an exception to this rule, setting the value of bound node to empty string is supported in all cases: it is understood as a "reset" and will also set the search field to empty string.

## Maximum number of displayed results

By default, the autocomplete displays a maximum of 10 items. You can have the autocomplete display less of more results by:

* Adding attribute `max-results-displayed`. You would typically use this attribute to provide a static value. For instance:

    ```xml
    <fr:autocomplete max-results-displayed="20">
    ```

* Adding a nested element <fr:max-results-displayed>, which supports [single node binding attributes][2] (`ref`, `model`, and `bind`) as well as the `value` attribute. For instance:

    ```xml
    <fr:autocomplete>
        <fr:max-results-displayed value="/configuration/max-results-displayed">
    ```

* By defining the property:

    ```xml
    <property
        as="xs:integer"
        name="oxf.xforms.xbl.fr.autocomplete.max-results-displayed"
        value="10">
    ```

When several configurations are used for an autocomplete component, the `<fr:max-results-displayed>` element has priority over the `max-results-displayed` attribute, which has priority over the global property.

## Displaying all the items

By default, the autocomplete control shows a button next to the autocomplete. Clicking on the button shows the suggestions users would get if they had typed the value currently in the field. For a country list, if you have _Sw_ in the field and you click on the button, you would get _Swaziland_, _Sweden_, and _Switzerland_. This won't be a common scenario as users will automatically get those suggestions when typing the _Sw_. A more common case is users not knowing what to type in the field. In that case they can click on the button when the field is empty, and the suggestion will contain the first _n_ possible values.

![][6]


This is the out of the box behavior in static mode. It is left to you to return the first _n_ items when in dynamic mode, when you get the `fr-search-changed` event with an empty `label`, and in resource mode when your service is called with an empty search value.

You can disable this button by adding a `show-suggestions-button="false"` attribute on the control. The default value of this attribute is `true`.

## Styling

* By default, the width of the input field is `140px`. You can change the width of all the autocomplete fields with:

    ```css
    .xbl-fr-autocomplete .xforms-input input { width: 30em }
    ```

    To change a specific autocomplete, add your own class on the autocomplete, say `<fr:autocomplete class="my-autocomplete"> `and the following CSS:

    ```css
    .my-autocomplete .xforms-input input { width: 30em }
    ```

* You can constraint the height of the section that displays results with CSS. For instance, the following will set a max height constraint of 100 pixels:

    ```css
    .fr-autocomplete-container .yui-ac-content {
        overflow-x: hidden;
        overflow-y: auto;
        max-height: 100px;
        *height:expression(this.scrollHeight&gt;100?"100px":"auto");
    }
    ```

Then, a vertical scrollbar will show when necessary:

![][7]


[1]: http://en.wikipedia.org/wiki/Autocomplete
[2]: http://www.w3.org/TR/xforms11/#structure-attrs-single-node
[3]: http://wiki.orbeon.com/forms/_/rsrc/1268331937355/doc/developer-guide/xbl-components/XBL%20-%20Existing%20XBL%20Components%20%28forms%29.png
[4]: http://wiki.orbeon.com/forms/_/rsrc/1276659909582/doc/developer-guide/xbl-components/autocomplete/autocomplete-full-itemset-button.png
[5]: http://wiki.orbeon.com/forms/_/rsrc/1276659957537/doc/developer-guide/xbl-components/autocomplete/autcomplete-full-itemset-dropdown.png
[6]: http://wiki.orbeon.com/forms/_/rsrc/1307056059556/doc/developer-guide/xbl-components/autocomplete/button-full-suggestions.png
[7]: http://wiki.orbeon.com/forms/_/rsrc/1268331937357/doc/developer-guide/xbl-components/css-scroll.png
