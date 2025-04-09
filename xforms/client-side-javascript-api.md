# XForms JavaScript API

## Rationale

While XForms gets you a long way towards creating a dynamic user-friendly user interface, there are some dynamic behaviors of the user interface that cannot be implemented easily or at all with XForms, or you might already have some JavaScript code that you would like to reuse. A JavaScript client-side API is provided to handle those cases, or other use cases involving JavaScript.

## Calling JavaScript from XForms

### Scripting actions

See [Scripting Actions](actions/scripting.md).

### The javascript: protocol

[DEPRECATED SINCE Orbeon Forms 2016.1]

In addition to `<xxf:script>` / `<xf:action>`, you can also use the `javascript:` protocol from the `<xf:load>` action to run scripts:

```xml
<xf:load
    resource="
        javascript:ORBEON.Builder.controlAdded.fire('{$effective-id}')
"/>
```

Using AVTs on the `resource` attribute allows you to pass parameters from XForms to JavaScript, although you have to be careful to properly escape values.

Starting Orbeon Forms 2016.1, the recommended way to pass parameters is to use the nested `<xxf:param>` attribute of [scripting actions](actions/scripting.md).

## Calling XForms from JavaScript

### Public API
  
The following objects are publicly exposed:
 
- `ORBEON.xforms.Document`
- `ORBEON.xforms.Events.orbeonLoadedEvent`
- `ORBEON.xforms.Events.errorEvent`
    - See [JavaScript Event Handler](/configuration/advanced/client-error-handling.md#javascript-event-handler).

Associated functions are described in more details below.

In addition, `ORBEON.jQuery` exposes the version of jQuery used by Orbeon Forms.

### Getting and setting controls value

```javascript
ORBEON.xforms.Document.getValue(controlIdOrElement, form)
ORBEON.xforms.Document.setValue(controlIdOrElement, newValue, form)
```

| Name | Required | Type | Description |
| ---- | -------- | ---- | ----------- |
| **controlIdOrElement** |  Yes |  `String` or `HTMLElement` | Either the id of the control *without namespace* (in the case of portal or embedding), or the control element.
| **newValue** |  Yes | Any value convertible with `toString()` | Value to set on the control (for `setValue()` only).
| **form** |  No | `HTMLElement` | [SINCE Orbeon Forms 2016.1] The form object that corresponds to the XForms control you want to deal with. This argument is only needed when you have multiple "XForms forms" on the same HTML page, which only happens if you are running your form in embedded mode and you have multiple forms on the same page.<br><br>When the parameter is not present or null, the first form on the HTML page with the class `xforms-form` is used.


- To get a control value, use: `var value = ORBEON.xforms.Document.getValue(myControl)`.
- To set a control value, use: `ORBEON.xforms.Document.setValue(myControl, "42")`. Setting the value with JavaScript is equivalent to changing the value of the control in the browser. This will trigger the recalculation of the instances, and the dispatch of the `xforms-value-changed` event. More formally, the Value Change sequence of events occurs.

For both `getValue()` and `setValue()`:

* The first parameter (`myControl` in the above example) can either be:
    * The id of the control (a string).
    * The element in the DOM that has this id.
* Within repeats, currently you have to pass an "effective id", which is the id as found within the HTML element.

As an example, consider you have the model below. It declares an instance with two elements `<foo>` and `<bar>`, where `<bar>` is a copy of `<foo>`, implemented with a calculate MIP.

```xml
<xf:model>
    <xf:instance id="instance">
        <instance>
            <foo>42</foo>
            <bar/>
        </instance>
    </xf:instance>
    <xf:bind nodeset="/instance/bar" calculate="/instance/foo"/>
</xf:model>
```

 The input control below is bound to `<foo>`, and the output control is bound to `<bar>`. When activated, the trigger executes JavaScript with the `<xxf:script>` action. It increments the value of the input control bound to `<foo>`. When this happens the value displayed by the output control bound to `<bar>` is  incremented as well, as `<bar>` is a copy of `<foo>`.

```xml
<xh:p>
    <xf:input ref="foo" id="foo">
        <xf:label class="fixed-width">Value of foo:</xf:label>
    </xf:input>
</xh:p>
<xh:p>
    <xf:output ref="bar">
        <xf:label class="fixed-width">Value of bar:</xf:label>
    </xf:output>
</xh:p>
<xh:p>
    <xf:trigger>
        <xf:label>Increment foo with JavaScript</xf:label>
        <xxf:script ev:event="DOMActivate">
            var fooValue = ORBEON.xforms.Document.getValue("foo");
            ORBEON.xforms.Document.setValue("foo", Number(fooValue) + 1);
        </xxf:script>
    </xf:trigger>
</xh:p>
```

[\[SINCE Orbeon Forms 2024.1\]](/release-notes/orbeon-forms-2024.1.md)

The `setValue()` function returns a JavaScript `Promise` which resolves when the value is set on the server.

#### For forms created in Form Builder

Keep in mind that, in Form Builder, if you have a field named `first-name`, the XForms id for that field will be `first-name-control`. Also, because your field is inside other containers, for instance a section, the id in the HTML for that field will have some prefix. Consequently, when using `getValue()` and `setValue()` described above, you'll want to write code along those lines:

```javascript
var firstNameControl = ORBEON.jQuery('*[id $= "first-name-control"]')[0];
var firstName = ORBEON.xforms.Document.getValue(firstNameControl);
```

The first line above uses the [`$=` CSS selector](https://www.w3.org/TR/selectors4/#attribute-substrings) to get the element in the DOM whose id ends with `first-name-control`.

#### For compound XBL controls

Some XBL controls, like `<fr:dropdown>` AKA `<xf:select1 appearance="dropdown">` are compound controls: they are built out of other controls and do not directly export a value to JavaScript.

For those, you have to go search for the nested core XForms control which holds the value, in this case the `.xforms-select1` control:

```javascript
ORBEON.xforms.Document.getValue(this.querySelector(".xforms-select1"))
```

### Setting focus on a control

[SINCE Orbeon Forms 2017.2]

```javascript
ORBEON.xforms.Document.focus(controlIdOrElement, form)
```

This function sets keyboard focus on the given control and lets the server know about the focus change if needed.

| Name | Required | Type | Description |
| ---- | -------- | ---- | ----------- |
| **controlIdOrElement** |  Yes |  `String` or `HTMLElement` | Either the id of the control *without namespace* (in the case of portal or embedding), or the control element.
| **form** |  No | `HTMLElement` | The form object that corresponds to the XForms control you want to deal with. This argument is only needed when you have multiple "XForms forms" on the same HTML page, which only happens if you are running your form in embedded mode and you have multiple forms on the same page.<br><br>When the parameter is not present or null, the first form on the HTML page with the class `xforms-form` is used.

Example:

```javascript
ORBEON.xforms.Document.focus(
    'fr-view-component≡text-controls-section≡xf-788≡input-control'
)
```

Actual focus only takes place if the control is focusable, not readonly, and not hidden within a hidden `switch`/`case` or `dialog`. 

### Dispatching events

#### Basic usage

```javascript
ORBEON.xforms.Document.dispatchEvent(
    {
        targetId:  'my-target',
        eventName: 'my-event'
    }
);
```

You can dispatch your own events from JavaScript by calling the function `ORBEON.xforms.Document.dispatchEvent()`. The function takes a single parameter which is an object with properties as defined in the table below. Calling the function with several parameters in order listed in the table below is supported as a deprecated alternative for backward compatibility.

In most cases, you only need to call `dispatchEvent()` with a target id and event name, as in:

```javascript
ORBEON.xforms.Document.dispatchEvent(
    {
        targetId:  'main-model',
        eventName: 'do-something'
    }
);
```

An event handler for the custom event can be in an XForms model or control, and can execute any valid XForms action. Here an action is explicitly declared to handle the `do-something` event on the XForms model:

```xml
<xf:model id="main-model" xxf:external-events="do-something">
    <xf:action ev:event="do-something">
        <xf:setvalue ref="first-name" value="instance('default-values')/first-name"/>
        <xf:toggle case="first-name-case"/>
    </xf:action>
</xf:model>
```

#### Parameters

| Name | Required | Description |
| ---- | -------- | ----------- |
| **targetId** |  Yes |  Id of the target element. The element must be an element in the XForms namespace: you cannot dispatch events to HTML elements. In addition, the id must identify either a relevant and non-readonly XForms control, or a model object that supports event handlers such as `<xf:model>`, `<xf:instance>`, or `<xf:submission>`. |
| **eventName** |  Yes |  Name of the event.
| **properties** |  No |  Allows you to attach custom properties to the event.
| **form** |  No |  The form object that corresponds to the XForms form you want to dispatch the event to. This argument is only needed when you have multiple "XForms forms" on the same HTML page, which only happens if you are running your form in embedded mode and you have multiple forms on the same page.<br><br>When the parameter is not present or null, the first form on the HTML page with the class `xforms-form` is used. |
| **incremental** |  No |  When `false` the event is sent to the XForms server right away. When `true` the event is sent after a small delay, giving the opportunity for other events that would occur during that time span to be aggregated with the current event.
| **ignoreErrors** |  No |  When set to `true`, errors happening while the event is dispatched to the server are ignored. This is in particular useful when you are using a JavaScript timer (e.g. `window.setInterval()`) that runs a JavaScript function on a regular interval to dispatch an event to the server, maybe to have part of the UI updated. In some cases, you might not want to alert the user when a there is a maybe temporary communication error while the event is being dispatched to the server. In those cases, you call `dispatchEvent()` with `ignoreErrors` set to `true`. |


The following parameters were previously documented but had no effect. They are ignored and you should not use them:

- `bubbles`
- `cancelable`

#### Security considerations

For security reasons, by default Orbeon Forms prohibits client-side JavaScript from dispatching any external events except `DOMActivate`, `DOMFocusIn`, `DOMFocusOut`, and `keypress`. Furthermore, these events can only be dispatched to relevant and non-readonly XForms controls. In order to enable dispatching of custom events, you must first add the `xxf:external-events` attribute on the first `<xf:model>` element, for example:

```xml
<xf:model xxf:external-events="acme-super-event acme-famous-event">
    ...
</xf:model>
```

This attribute contains a space-separated list of event name. In this example, you explicitly enable your JavaScript code to fire the two events `acme-super-event` and `acme-famous-event` to any relevant and non-readonly XForms controls, or to any model object supporting event handlers. Note that you can only enable custom events, but you cannot enable standard XForms or DOM events in addition to `DOMActivate`, `DOMFocusIn` and `DOMFocusOut`.

Since the event handlers for custom events can be called by JavaScript code that runs on the client, you need to be aware that these handlers can potentially be activated by anybody able to load the form in his browser.

XBL components can also declare that they support external events:

```xml
<xbl:binding xxf:external-events="acme-super-event acme-famous-event">
    <xbl:handlers>
        <xbl:handler event="acme-super-event" phase="target">
            ...
        </xbl:handler>
    </xbl:handlers
    ...
</xbl:binding>
```

In that case the setting is only valid for the given XBL binding, not for the entire form. This in particular allows JavaScript companion code to communicate with XBL components.

Any XForms control can declare external events with the `xxf:external-events` attribute:

```xml
<xf:group xxf:external-events="fr-select fr-deselect">
    ...
</xf:group>
```

#### Custom properties

The `properties` parameter is a JSON object:

For instance:

```javascript
ORBEON.xforms.Document.dispatchEvent({
    targetId:   "my-target",
    eventName:  "my-event",
    properties: { p1: 'v1', p2: 'v2' }
});
```

When properties are provided, the code handling the event can access the property values with the `event()` function. With the event dispatched above, in the handler for `my-event` on `my-target`, calling `event('p1')` will return the string `'v1'`.

Passing properties is only supported when calling `dispatchEvent()` with a single object parameter; it isn't supported when calling it with multiple parameters.

### The orbeonLoaded event

The `ORBEON.xforms.Events.orbeonLoadedEvent` fires when the form is fully loaded and initialized.

To add your event listener on, say `orbeonLoadedEvent`, write:

```javascript
(function () {
    addEventListener("DOMContentLoaded", function () {
        ORBEON.xforms.Events.orbeonLoadedEvent.subscribe(function () {
            alert("Hello!");
        });
    });
}).call(this);
```

The above does a few things:

- Add an event listener upon `DOMContentLoaded` (similar to jQuery's "ready"). When this event fires, the global `ORBEON` object exists.
- Add your event handler for "orbeonLoaded".

*NOTE: In some cases, if the Orbeon Forms initialization happened *before* adding the event handler, it was possible that the event could not fire. This is fixed with issue [#3891](https://github.com/orbeon/orbeon-forms/issues/3891).* 

### The error event

The `ORBEON.xforms.Events.errorEvent` fires when the JavaScript code catches an error.

See also [JavaScript event handler](/configuration/advanced/client-error-handling.md#javascript-event-handler).

The arguments of the listener for `errorEvent` are as follows:

1. The first argument is the name of the event (the string `errorEvent`), which most likely you don't need to know about if that listener is explicitly registered to this event.
2. The second argument contains information about the error. Assuming you defined your second argument to be named `eventData`, inside your listener you can access:
    - `eventData.title` – A string describing the issue.
    - `eventData.details` – A string containing HTML with more information about the error, including:
        - If it happened in JavaScript: information of where the error happened (such as the file name and the line number).
        - If if happened on the server: detailed information about where the error happened (such as the invalid XPath expression and the file where that expression is found).

### Removing event listeners

To remove your event listener on, say `orbeonLoadedEvent`, you'll need to implement your listener in a named function, say `myListener`:

```javascript
ORBEON.xforms.Events.orbeonLoadedEvent.unsubscribe(myListener);
```

## See also

- [Adding your own JavaScript](/configuration/properties/form-runner.md#adding-your-own-javascript)
- [Form Runner JavaScript API](/form-runner/api/other/form-runner-javascript-api.md)
