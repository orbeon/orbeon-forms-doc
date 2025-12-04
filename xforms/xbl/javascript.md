# JavaScript companion classes

## Rationale

Some Orbeon Forms components do not require any custom JavaScript code, for example components which simply combine other controls (such as a date components made of separate input fields or dropdown menus). In such cases, you implement all the logic with XForms.

On the other hand, some components encapsulate functionality mainly implemented in JavaScript. Orbeon Forms provides an easy way to interface with the JavaScript side: each JavaScript-based component must define a JavaScript class used to handle the component's lifecycle as well as hold custom data and functions. We call this class is called the component's *companion class*. One instance of this class is created by Orbeon Forms for each instance of relevant (visible) control. We call these instances *companion instances*.

## Directory layout

You place your JavaScript files alongside your XBL file. See [Directory layout](bindings.md#directory-layout) for details.

To include a companion JavaScript file, use the `<xbl:script>` element directly within the `<xbl:xbl>` element:

```xml
<xbl:xbl
    xmlns:xf="http://www.w3.org/2002/xforms"
    xmlns:acme="http://www.acme.com/xbl"
    xmlns:xbl="http://www.w3.org/ns/xbl">

    <xbl:script src="/xbl/acme/multi-tool/multi-tool.js"/>

    <xbl:binding
        id="acme-multi-tool"
        element="acme|multi-tool">

        ...binding definition here...

    </xbl:binding>
</xbl>
```

## Creating and declaring a companion class

### With Orbeon Forms 2022.1.1 and newer 

[SINCE Orbeon Forms 2022.1.1]

The first parameter to `declareCompanion()` must match the component's binding name, for example:

- if your component's binding is `acme|multi-tool`
    - pass `acme|multi-tool`
    - you place the JavaScript file under `/xbl/acme/multi-tool/multi-tool.js`
- if your component's binding is `foo|bar`
    - pass `foo|bar`
    - you place the JavaScript file under `/xbl/foo/bar/bar.js`

The second parameter to `declareCompanion()` can either be a JavaScript object that acts as the *prototype* for the companion class, or (and this is new with Orbeon Forms 2022.1.1) it can also be a [JavaScript *class*](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Classes) instead of a prototype. Note that this must not be an *instance* of the class (so don't use `new`), but the class itself. For example:

```javascript
(function() {

  // Optional shortcut to jQuery
  var $ = ORBEON.jQuery;

  // Register your companion class by its binding name
  ORBEON.xforms.XBL.declareCompanion("acme|multi-tool", class MultiTool {

    // Your custom data can go here
    myField: null;
    containerElem; // initialized in constructor
      
    constructor(containerElem) {
      // Remember the container element so that other methods can use it
      this.containerElem = containerElem;
    }

    init() {
      // Perform your JavaScript initialization here
    }

    destroy() {
      // Perform your JavaScript clean-up here
    }

    xformsUpdateReadonly(readonly) {
      // Orbeon Forms calls this when the control's readonly status changes
    }

    xformsUpdateValue(newValue) {
      // Orbeon Forms calls this when the control's value changes
    }

    xformsGetValue() {
      // Orbeon Forms calls this to obtain the control's value
    }

    xformsFocus() {
      // Orbeon Forms calls this when the control is handed focus
    }

    // Your custom functions can go here
    myFunction() {
      // ...
    }
  });
})();
```

Advantages of passing a JavaScript class include:

- Orbeon Forms directly passes the container element in the constructor, which provides clarity over the "magic" `this.container` field.
- You can use inheritance easily to share code between components.
- Classes have become mainstream in JavaScript since Web browsers support them natively.

### With Orbeon Forms 2016.1 and newer

Orbeon Forms 2016.1 and newer provide a simple way to declare a companion class by passing a JavaScript *prototype*. Here is the overall structure:

```javascript
(function() {

  // Optional shortcut to jQuery
  var $ = ORBEON.jQuery;

  // Register your companion class by its binding name
  ORBEON.xforms.XBL.declareCompanion("acme|multi-tool", {

    // Your custom data can go here
    myField: null,

    init: function () {
      // Perform your JavaScript initialization here
    },
    destroy: function () {
      // Perform your JavaScript clean-up here
    },
    xformsUpdateReadonly: function (readonly) {
      // Orbeon Forms calls this when the control's readonly status changes
    },
    xformsUpdateValue: function (newValue) {
      // Orbeon Forms calls this when the control's value changes
    },
    xformsGetValue: function () {
      // Orbeon Forms calls this to obtain the control's value
    },
    xformsFocus: function () {
      // Orbeon Forms calls this when the control is handed focus
    },
    // Your custom functions can go here
    myFunction: function () {
        // ...
    },
  });
})();

```

The first parameter to `declareCompanion()` must match the component's binding name, for example:

- if your component's binding is `acme|multi-tool`
    - pass `acme|multi-tool`
    - you place the JavaScript file under `/xbl/acme/multi-tool/multi-tool.js`
- if your component's binding is `foo|bar`
    - pass `foo|bar`
    - you place the JavaScript file under `/xbl/foo/bar/bar.js`

The second parameter to `declareCompanion()` is a JavaScript object that acts as the *prototype* for the companion class. This is documented further below.

### With Orbeon Forms 4.10 and earlier

[DEPRECATED SINCE Orbeon Forms 2022.1]
[REMOVED IN Orbeon Forms 2025.1]

In the JavaScript file corresponding to your component, you declared a companion class using `declareClass()`. This approach, along with the `instance()` static method, was removed in Orbeon Forms 2025.1 and no longer works:

```javascript
(function() {

    // Optional shortcut to jQuery
    var $ = ORBEON.jQuery;

    YAHOO.namespace("xbl.acme");
    YAHOO.xbl.acme.MultiTool = function() {};
    ORBEON.xforms.XBL.declareClass(YAHOO.xbl.acme.MultiTool, "xbl-acme-multi-tool");
    YAHOO.xbl.acme.MultiTool.prototype = {

        // Your custom data goes here
        myField: null,

        init: function() {
            // Perform your JavaScript initialization here
        },
        destroy: function() {
            // Perform your JavaScript clean-up here
        },
        xformsFocus: function() {
            // Orbeon Forms calls this when the control is handed focus
        },
        // Your custom functions go here
        myFunction: function() {
            ...
        },

        ...
    };
})();
```

* `YAHOO.namespace("xbl.acme")` defined a namespace for your class. All the XBL components that ship with Orbeon Forms were in the `xbl.fr` namespace. If you were defining a component for your company or project named Acme, you could use the namespace `xbl.acme`.
* ` ORBEON.xforms.XBL.declareClass()` defined your class as an XBL class:
    * It took 2 parameters: your class, and the CSS class found on the outermost HTML element that contains the markup for your components. This element is generated by Orbeon Forms, and the class name is derived from the by-name binding of your `<xbl:binding>`. For example, if the binding is `acme|multi-tool`, the class name is `xbl-acme-multi-tool`.

### The companion class

Whether you use the `declareCompanion()` method or the `declareClass()` method, and whether you pass a JavaScript prototype object or a JavaScript class, Orbeon Forms internally creates a JavaScript class which derives from either the class passed or a class created from the prototype. That class:

- adds a `container` property
    - This points to the outermost container HTML element associated with the component.
    - In your JavaScript code, you can refer to `this.container` to retrieve this element.
    - [SINCE Orbeon Forms 2022.1.1] We recommend you use a JavaScript class's constructor instead, which is directly passed that container element. 
- adds or overrides (if present) the `init()` and `destroy()` methods
    - This provides finer internal control over these lifecycle methods.
    - The overridden methods call your own `init()` and `destroy()` methods if present.
    - In general, you don't have to worry about this. However, you shouldn't call `init()` and `destroy()` yourself in any case. 
- adds a static `instance()` factory method to the class
    - [SINCE Orbeon Forms 2022.1.1] This was present for backward compatibility only. Use `instanceForControl()` instead.
    - [SINCE Orbeon Forms 2025.1] This backward compatibility was removed.

For example, if you know you have an input field with the class `acme-my-input` inside your component, you get the HTML element corresponding to that input with the following jQuery:

```javascript
this.container.querySelector(".acme-my-input")
```

### Summary of companion class methods

| Method                 | Description              | Mode                   | Since  | Status |
|------------------------|--------------------------|------------------------|--------|--------|
| `init`                 | initialize               | `javascript-lifecycle` | 2016.1 | fresh  |
| `destroy`              | clean-up                 | `javascript-lifecycle` | 2016.1 | fresh  |
| `xformsUpdateReadonly` | change readonly status   | `javascript-lifecycle` | 2016.1 | fresh  |
| `xformsUpdateValue`    | update value             | `external-value`       | 2016.1 | fresh  |
| `xformsGetValue`       | get value                | `external-value`       | 2016.1 | fresh  |
| `xformsFocus`          | hand focus               | `focus`                | 2016.1 | fresh  |
| `setFocus`             | hand focus               | `focus`                | 4.0    | legacy |
| `enabled`              | enable after full update |                        | 4.0    | legacy |

The `init()` method is not new in Orbeon Forms 2016.1, but when using the `javascript-lifecycle` mode it is called automatically. Prior to Orbeon Forms 2016.1, or when not using the `javascript-lifecycle`  mode, it is called either via XForms event handlers, or as a side-effect of calls to `setFocus()` or `enabled()`.

## Calling methods upon XForms events

### With Orbeon Forms 2016.1 and newer

You can call a JavaScript method defined in your JavaScript class when an XForms event occurs. For example, to call the `myFunction()` method on `xxforms-visible`, write:

```xml
<xxf:action type="javascript" event="xxforms-visible">
    ORBEON.xforms.XBL.instanceForControl(this).myFunction();
</xxf:action>
```

`instanceForControl()` gets or creates the instance of the JavaScript class associated with the current component. It creates class instances as necessary, keeping track of existing instances and maintaining a 1-to-1 mapping between instances of the XBL component in the form and instances of your JavaScript class.

__WARNING: You should use this only to call your own methods. Do not use this to call the `init()`, `destroy()`, or other lifecycle methods documented in this page.__

Note that a component can be created on the server, and receive the `xforms-enabled` event, but its HTML/JavaScript representation might not be visible and in fact there might not be any markup yet available for that control. This is the case, for example, for hidden switch cases, which is the construct used for hidden wizard pages. Therefore, use [`xxforms-visible`](/xforms/events-extensions-events.md#xxforms-visible) and [`xxforms-hidden`](/xforms/events-extensions-events.md#xxforms-hidden) instead of `xforms-enabled`/`xforms-disabled` in conjunction with `instanceForControl()`.

### With Orbeon Forms 4.10 and earlier

[DEPRECATED SINCE Orbeon Forms 2022.1]
[REMOVED IN Orbeon Forms 2025.1]

With Orbeon Forms 4.10 and earlier, you obtained the class using the JavaScript namespaces you declared alongside the class, and directly called the `instance()` factory function:

```xml
<xxf:action type="javascript" event="xforms-enabled">
    YAHOO.xbl.acme.MultiTool.instance(this).myFunction();
</xxf:action>
```

## Support for the external-value mode

### Introduction

[SINCE Orbeon Forms 2016.1]

When the [`external-value` mode](modes.md#the-externalvalue-mode) is enabled, the following two methods must be provided:

- `xformsUpdateValue()`
- `xformsGetValue()`

For an example, see the implementation of the `fr:code-mirror` component: [`code-mirror.xbl`](https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/xbl/orbeon/code-mirror/code-mirror.xbl) and [`code-mirror.js`](https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/assets/xbl/orbeon/code-mirror/code-mirror.js).

### The xformsUpdateValue method

The XForms engine calls this method:

- if the `javascript-lifecycle` and the `external-value` modes are enabled, just after the control is initialized,
- when the internal value of the control changes,
- and in response to calls to `ORBEON.xforms.Document.setValue()`.

#### Method parameters

`xformsUpdateValue()` receives a string and must update the associated JavaScript control, making the value accessible to the user.

#### Method return value

`xformsUpdateValue()` must return:

- If it sets value is synchronously: `undefined` (or not return anything).
- If it sets value is asynchronously: a jQuery deferred object whose `done()` method must be called once the value is known to have been fully applied. For instance:
	```javascript
	var editor   = this.editor;
	var deferred = $.Deferred();
	setTimeout(function() {
	    editor.setValue(newValue);
	    deferred.resolve();
	}, 0);
	return deferred.promise();
	```
    This allows the XForms engine to know when it is safe to call `xformsGetValue()` after a new value has been set.

[SINCE Orbeon Forms 2020.1]

In addition to a jQuery deferred object (with a `done()` method), you can also return a JavaScript `Promise` object (with a `then()` method). The latter is the recommended way since JavaScript promises are implemented natively by all major browsers (except IE 11, but Orbeon Forms includes a polyfill for IE 11). 

### The xformsGetValue method

The XForms engine calls this method when:

- it needs the control's value,
- and in response to calls to `ORBEON.xforms.Document.getValue()`.

`xformsGetValue()` returns a string obtained from the associated JavaScript control.

### External value serialization/deserialization

[SINCE Orbeon Forms 2019.1]

By default, the external value exchanged with the client is identical to the storage value of the component.

By using the `xxbl:serialize-external-value` and `xxbl:deserialize-external-value` attributes on `<xbl:binding>`, you can create XPath expressions that transform the external value back and forth.

This is useful if the value must contain more than the storage value of the component. For example the `fr:number` component uses this to communicate a display value, an edit value and a decimal separator to the client.

```xml
<xbl:binding
    id="fr-number"
    element="
        fr|number,
        xf|input:xxf-type('xs:decimal'),
        xf|input:xxf-type('xs:integer')"
        
    xxbl:mode="... value external-value javascript-lifecycle ..."
    xxbl:serialize-external-value="... expression serializing the value to the client... "
    xxbl:deserialize-external-value="... expression deserializing the external value from the client... "
>
... rest of the binding...
```

For `xxbl:serialize-external-value`:

- XPath context item: XPath string of the control's storage value
- Expression result: XPath string to send to the client's companion class's `xformsUpdateValue()` method

For `xxbl:deserialize-external-value`:
 
- XPath context item: XPath string provided by the client's companion class's `xformsGetValue()` method
- Expression result: XPath string to use as the control's storage value  

## Support for the javascript-lifecycle mode

### Introduction

[SINCE Orbeon Forms 2016.1]

When the [`javascript-lifecycle` mode](modes.md#the-javascriptlifecycle-mode) is enabled, the following methods should be provided:

- `init()`
- `destroy()`
- `xformsUpdateReadonly()`

*NOTE: The XForms engine does not call these methods if they are not present.*

On the JavaScript side, the lifecycle of a companion instance does not exactly follow that of the XForms controls when repeats are involved.

For an example, see [the implementation of the `fr:code-mirror` component](https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/xbl/orbeon/code-mirror/code-mirror.xbl).

### The init method

The `init()` method is called when the control becomes relevant, including:

- when the page first loads and the control is initially relevant
- when the control becomes relevant at a later time
- when a new repeat iteration is added
- when `xxf:full-update` or `xxf:dynamic` replace an entire block of HTML on the client

### The destroy method

The `destroy()` method is called when the control becomes non-relevant, including:

- when the control becomes non-relevant after the page has loaded

Since Orbeon Forms 2016.1, it is *not* called:

- when a repeat iteration is removed
- when `xxf:full-update` or `xxf:dynamic` replace an entire block of HTML on the client

The assumption is that, when HTML elements are removed from the browser DOM, the associated JavaScript resources are garbage-collected. This means that you have to be careful about clean-up of event handlers in particular in such cases.

### The xformsUpdateReadonly method

The `xformsUpdateReadonly()` method is called when the control's readonly status changes.

It takes a boolean parameter set to `true` if the control becomes readonly and to `false` if the control becomes readwrite.

It is *not* called just after the control is initialized.

## Read-only parameters

[UNTIL Orbeon Forms 2021.1]

So your JavaScript can access the current value of parameters and be notified when their value changes, include the `oxf:/oxf/xslt/utils/xbl.xsl` XSL file, and call `xxbl:parameter()` function for each parameter, as in:

```xml
<xbl:xbl>
    <xbl:script src="/xbl/orbeon/currency/currency.js"/>
    <xbl:binding id="fr-currency" element="fr|currency">
        <xbl:template xxbl:transform="oxf:unsafe-xslt">
            <xsl:transform version="2.0">
                <xsl:import href="oxf:/oxf/xslt/utils/xbl.xsl"/>
                <xsl:template match="/*">
                    ...
                    <xsl:copy-of select="xxbl:parameter(., 'prefix')"/>
                    <xsl:copy-of select="xxbl:parameter(., 'digits-after-decimal')"/>
                    ...
                </xsl:template>
            </xsl:transform>
        </xbl:template>
    </xbl:binding>
</xbl:xbl>
```

The arguments of `xxbl:parameter()` are:

1. The element corresponding to your component, e.g. the `<fr:currency>` element written by the user of your component. If your template matches on `/*`, this will be the current node.
2. The name of the parameter.

Then in JavaScript, you can access the current value of the property with:

```javascript
var prefixElement =
    this.container.querySelector(".xbl-fr-currency-prefix");

var prefix =
    ORBEON.xforms.Document.getValue(prefixElement.id);
```

Whenever the value of a parameter changes, a method of your JavaScript class is called. The name of this method is ` parameterFooChanged` if "foo" is the name of your property. Parameters names are in lowercase and use dash as a word separator, while the method names use camel case. E.g. if your parameter name is `digits-after-decimal`, you will defined a method `parameterDigitsAfterDecimalChanged`.

## Sending events from JavaScript

You can dispatch custom events to bindings from JavaScript using the `ORBEON.xforms.Document.dispatchEvent()` function. If  you are calling it with custom events, make sure you are allowing the custom event names on the binding first:

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
