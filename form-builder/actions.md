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

![Actions Editor](/form-builder/images/actions.png)

## Basic action configuration

This is the meaning of the fields of the dialog:

- **Action Name.** This is the name of the action, as seen by Form Builder. Must start with a letter, and may not contain spaces.
- **React to.** The event which starts the action. Can be one of the following:
    - **Value Change.** A control's value has changed.
    - **Value Change or Form Load.** A control's value has changed OR the form just finished loading.
    - **Activation.** A button has been clicked, or the "Enter" key has been pressed in a text line.
    - **Form Load.** The form just finished loading.
- **Condition.** [SINCE: 2011-12-01]
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

- **Set Response Control Values.**
    - Function: uses a value returned by the service to set a control's value.
        - **Destination Control.** Specifies the control whose value must be set.
        - **Destination XPath Expression.** The expression must point to an element or attribute node of the response body returned by the service.
    - You can add as many such rows as you want using the "+" button, and remove existing entries with the trashcan icon.
- **Set response Selection Control Items.**
    - Function: uses values returned by the service to set a selection control's set of items (itemset) with them.
        - **Destination Selection Control.** Specifies the selection control whose items must be set. Only selection controls appear in this list.
        - **Items.**
            - The XPath expression must point to a set of element or attribute nodes of the response body returned by the service.
            - For each node returned, an item is created.
        - **Label.** The XPath expression must return the text of the label for an item. It is relative to the item expression.
        - **Value.** The XPath expression must return the text of the value for an item. It is relative to the item expression.
    - All the previous items of the selection control are replaced with the items specified here.
    - [SINCE Orbeon Forms 4.3] The selected value(s) are updated per the new itemset:
        - For single-selection controls: if the item value currently stored in the instance data is not part of the returned set of items, the value is cleared.
        - For multiple-selection controls: any of the space-separated values currently stored in the instance data that are not part of the returned set of item values are filtered out.
    - You can add as many such rows as you want using the "+" button, and remove existing entries with the trashcan icon.

## Internationalization

[SINCE Orbeon Forms 4.7] Your service should return localized labels for all the languages supported by your form. For instance, if your form is available in English and French, a service you use to populate a dropdown with a list of countries might return:

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
