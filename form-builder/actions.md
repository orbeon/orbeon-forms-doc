# Actions

<!-- toc -->

## Introduction

The Form Builder action editor is an [Orbeon Forms PE](http://www.orbeon.com/pricing) feature and allows you to implement simple actions in your form. The basic philosophy goes as follows:

1. React to an event occurring on the form, such as the form being loaded or a user action.
2. Call an HTTP or database service:
    - passing in parameters from the form,
    - receiving parameters back from the service.
3. Use the returned parameters to update the form

Actions are tightly coupled with services. In the future, support might be added for actions which do not require services.

![Actions Editor](images/actions.png)

## Basic action configuration

This is the meaning of the fields of the dialog:

- **Action Name.** This is the name of the action, as seen by Form Builder. Must start with a letter, and may not contain spaces.
- **React to.** The event which starts the action. Can be one of the following:
    - **Value Change.** A control's value has changed.
    - **Value Change or Form Load.** A control's value has changed OR the form just finished loading.
    - **Activation.** A button has been clicked, or the "Enter" key has been pressed in a text line.
    - **Form Load.** The form just finished loading.
- **Condition.**
    - **Run always.** Run the action independently from the form mode.</span></font>
    - **Run on creation only.** Run the action only in creation mode, that is when the user creates new data, as opposed to editing, viewing, emailing, or generating a PDF.
- **Control.** Except for the Form Load event which does not depend on a particular control, this specifies which control the action reacts to.
- **Service to Call.** The service to call as a response to the action.

Like for services, once your action is defined, the Save buttons saves it to the form. You can come back to it and modify it later by clicking on the Edit icon next to the action name. You can also delete the action using the trashcan icon.

## Passing parameters to the service

- **Set Service Request Values.**
    - Used with HTTP services only
    - Function: uses the value of a control and stores that value into the body of the XML service request.
        - **Source Control.** Specifies the control whose value must be used.
        - **Destination XPath Expression.** The expression must point to an element or attribute node of the request body defined in the HTTP service under "XML Request Body"
    - You can add as many such rows as you want using the "+" button, and remove existing entries with the trashcan icon.
- **Set Database Service Parameters.**
    - Used with database services only.
    - Function: uses the value of a control and sets that value as the Nth query parameter of the database service.
        - **Source Control.** Specifies the control whose value must be used.
        - **Parameter Number.** To set the first query parameter, use the value "1" (without the quotes), the second, "2", etc.
    - You can add as many such rows as you want using the "+" button, and remove existing entries with the trashcan icon.

## Handling the service response

### Setting the value of a control

As a result of running an action, you can set a form control's value from data returned by a service using "Set Response Control Values".

- __Destination Control.__
    - Specifies the control whose value must be set.
    - A single "closest" control will be selected.
- __Source XPath Expression.__
    - The expression is evaluated in the context of root element of the XML data returned by the service.
    - The expression can point to an element or attribute node of the response body, but can also be a more complex expression. Its result is converted to a string.

### Setting the items of a selection control

#### Basics

As a result of running an action, you can set a selection control's set of items (AKA "itemset") using "Set response Selection Control Items".

Selection controls include dropdown menus, checkboxes, and more.

- __Destination Selection Control.__
    - Specifies the selection control whose items must be set. Only selection controls appear in this list.
    - Depending on the relative position of the source of the action and the target selection controls, one or more "closest" controls can be selected (see the detailed explanation of the behavior below).
- __Items.__
    - The XPath expression must point to a set of element or attribute nodes of the response body returned by the service.
    - For each node returned, an item is created.
- __Label.__ The XPath expression must return the text of the label for an item. It is relative to the current item node.
- __Value.__ The XPath expression must return the text of the value for an item. It is relative to the current item node.

#### Adjustment of control values

[SINCE Orbeon Forms 4.3]

Each selection control's selected value(s) are updated to be in range following the new itemset:

- For single-selection controls: if the item value currently stored in the instance data is not part of the returned set of items, the value is cleared.
- For multiple-selection controls: any of the space-separated values currently stored in the instance data that are not part of the returned set of item values are removed.

#### Behavior starting with Orbeon Forms 4.11

In the presence of repeated grids or sections, the destination selection control can resolve to zero, one or more concrete controls.

The way this works is that the "closest" controls are searched. This means:

- If the destination selection control is not within a repeated grid or section, then the single destination control is updated.
- If the source of the action is *within the same repeated iteration* as the destination selection control, then that single destination control is updated. Occurrences of the selection control on other repeat iterations are not updated.
- If the source of the action is at a higher level compared to the destination selection control, then all iterations of the selection control are updated, and subsequent new iterations added will also use the new itemset.

#### Behavior up to Orbeon Forms 4.10 included

All the previously available items of the selection control(s) identified are replaced with the items specified. In other words, the itemset for the given control is global. This is the case even in the presence of repeated grids or sections.

#### Internationalization

[SINCE Orbeon Forms 4.7]

Your service should return localized labels for all the languages supported by your form. For instance, if your form is available in English and French, a service you use to populate a dropdown with a list of countries might return:

```xml
<response>
    <row>
        <value>us</value>
        <lang>en</lang>
        <label>United States</label>
    </row>
    <row>
        <value>us</value>
        <lang>fr</lang>
        <label>États-Unis</label>
    </row>
    <row>
        <value>ch</value>
        <lang>en</lang>
        <label>Switzerland</label>
    </row>
    <row>
        <value>ch</value>
        <lang>fr</lang>
        <label>Swiss</label>
    </row>
</response>
```

After the service is called, the *items*, *label*, and *value* XPath expressions you wrote when defining the action are executed once per language supported by the form, and for each execution the `$fr-lang` variable is set to current language. So in the case of our hypothetical service returning a list of countries, you will define the *items* as `/response/row[@lang = $fr-lang]`, the *value* simply as `value`, and *label* as `label`.

While in theory this allows you to have the *values* depend on the language, to avoid unexpected behavior when users switch languages or different users look at the same data using a different language, you should make sure that values are the same for all languages, and only the *labels* differ between languages.

## Namespace handling

At this point, you can't declare custom namespace mappings in the action editor. So say you have a response looking like this:

```xml
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
    <soap:Body>
        <my:items xmlns:my="http://example.org/my">
            <my:item>
                <my:label>Cat</my:label>
                <my:value>cat</my:value>
            </my:item>
            <my:item>
                <my:label>Dog</my:label>
                <my:value>dog</my:value>
            </my:item>
            <my:item>
                <my:label>Bird</my:label>
                <my:value>bird</my:value>
            </my:item>
        </my:items>
    </soap:Body>
</soap:Envelope>
```

or, using the default namespace mechanism:

```xml
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
    <soap:Body>
        <items xmlns="http://example.org/my">
            <item>
                <label>Cat</label>
                <value>cat</value>
            </item>
            <item>
                <label>Dog</label>
                <value>dog</value>
            </item>
            <item>
                <label>Bird</label>
                <value>bird</value>
            </item>
        </items>
    </soap:Body>
</soap:Envelope>
```

In both these cases, `<my:item>` (or `<item>`) and nested elements are in the `http://example.org/my` namespace. This means that your XPath expression must match elements in a namespace and that, in theory, you need a custom namespace mapping in form builder. Since this is not supported yet, you can work around the issue by using XPath expressions with wildcards:

- Items: `/soap:Envelope/soap:Body/*:items/*:item`
- Label: `*:label`
- Value: `*:value`

You can even use shorter variations if element names are use consistently, for example:

- Items: `//*:item`
- Label: `*:label`
- Value: `*:value`

These expressions with wildcards ignore namespace information completely, so you have to be careful if your XML document contains elements with the same name but in different namespaces.
