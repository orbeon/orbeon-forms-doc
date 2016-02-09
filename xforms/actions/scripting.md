# Scripting Actions

<!-- toc -->

## Calling client-side JavaScript

### Basic usage

Call client-side JavaScript as a result of XForms events:

Starting Orbeon Forms 4.11:

```xml
<xf:action ev:event="xforms-value-changed" type="javascript">
    var v = 2;
    myValueChanged(v);
</xf:action>
```

Prior to Orbeon Forms 4.11:

```xml
<xxf:script ev:event="xforms-value-changed">
    var v = 2;
    myValueChanged(v);
</xxf:script>
```

or:

```xml
<xxf:script ev:event="xforms-value-changed" type="javascript">
    var v = 2;
    myValueChanged(v);
</xxf:script>
```

The embedded JavaScript has access to the following JavaScript variables:

* `this`: element observing the event causing `<xf:action>` to run
* `event`:
    * `event.target`: returns the element which is the target of the event causing `<xf:action>` to run

_NOTE: Because regular XForms actions run on the server and JavaScript actions run on the client, JavaScript actions are always executed last in a sequence of XForms actions, even if they appear before other XForms actions._

### Passing parameters

[SINCE Orbeon Forms 4.11]

You can specify parameters to pass to the JavaScript, for example:

```xml
<xf:action type="javascript">
    <xf:param
        name="newValue"
        value="xxf:binding('fr-code-mirror')"/>
    <xf:body>
        var instance = YAHOO.xbl.fr.CodeMirror.instance(this);
        instance.enabled();
        instance.xformsValue(newValue);
    </xf:body>
</xf:action>
```

Syntax:

- nest as many `<xf:param>` elements as there are parameters to pass
    - the `name` attribute specifies the name of the JavaScript parameter to pass
    - the `value` attribute contains an XPath expression evaluating the value of the parameter
- embed the JavaScript code within an `<xf:body>` element instead of directly under `<xf:action type="javascript">`

Parameters are scoped at the beginning of the embedded JavaScript and you just access them by name, like `newValue` in the above example.

For an example within Orbeon Forms, see [code-mirror.xbl](https://github.com/orbeon/orbeon-forms/blob/83c1bde2386bc5c69af72a132db61378d2077fc9/src/resources-packaged/xbl/orbeon/code-mirror/code-mirror.xbl).

### XBL handlers

[SINCE Orbeon Forms 4.11]

The `<xbl:handler>` element works like an `<xf:action>` element and also supports the `type` attribute and nested `<xf:param>` and `<xf:body>` elements:

```xml
<xbl:handler event="xforms-enabled xxforms-iteration-moved" type="javascript">
    <xf:param
        name="newValue"
        value="xxf:binding('fr-code-mirror')"/>
    <xf:body>
        var instance = YAHOO.xbl.fr.CodeMirror.instance(this);
        instance.enabled();
        instance.xformsValue(newValue);
    </xf:body>
</xbl:handler>
```

## Calling server-side XPath

Simple server-side scripts can be written in XPath. This is particularly useful when using extension functions that have side effects.

You specify an XPath script either with:

```xml
<xf:action type="xpath">
   ...
</xf:action>
```

Or (deprecated as of Orbeon Forms 4.11):

```xml
<xxf:script type="xpath">
   ...
</xxf:script>
```

Example:

```xml
<xf:action type="xpath">
    xxf:set-session-attribute('foo', $total),
    xxf:set-session-attribute('bar', instance()/bar)
</xf:action>
```

When putting multiple XPath statements, separate them with a comma. This creates an XPath sequence, which is evaluated in order.

XPath scripts have access to the current XPath context, including the focus and in-scope variables.

## `<xxf:script>` or `<xf:action>`?

Orbeon Forms first introduced JavaScript with the `<xxf:script>` action. This action is deprecated as of Orbeon Forms 4.11 and it is recommended to use `<xf:action type="javascript">` and `<xf:action type="xpath">` instead.

Here are the differences between the two:

* `<xf:action type="...">`:
    * this is a standard XForms action
    * without a `type` attribute, the behavior is standard XForms and runs nested XForms actions
    * with a `type` attribute, this is able to run nested client-side JavaScript or server-side XPath scripts
    * Only available for JavaScript actions starting Orbeon Forms 4.11.
* `<xxf:script>`:
    * this is a custom Orbeon Forms action
    * this never runs nested XForms actions
    * without a `type` attribute, the default is to run client-side JavaScript
    * with a `type` attribute, this can run nested client-side JavaScript or server-side XPath scripts

