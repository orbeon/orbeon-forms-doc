# Scripting Actions

<!-- toc -->

## Calling client-side JavaScript

### Basic usage

Call client-side JavaScript as a result of XForms events:

```xml
<xf:action ev:event="xforms-value-changed">
    <xf:setvalue ref=".">test</xf:setvalue>
    <xxf:script>var v = 2; myValueChanged(v);</xxf:script>
</xf:action>
```

Or:

```xml
<xf:action ev:event="xforms-value-changed">
    <xf:setvalue ref=".">test</xf:setvalue>
    <xf:action type="javascript">var v = 2; myValueChanged(v);</xf:action>
</xf:action>
```

The embedded JavaScript has access to the following JavaScript variables:

* `this`: element observing the event causing `<xxf:script>` or `<xf:action>` to run
* `event`:
    * `event.target`: returns the element which is the target of the event causing `<xxf:script>` or `<xf:action>` to run

_NOTE: JavaScript actions are always executed last in a sequence of XForms actions, even if they appear before other XForms actions._

### Passing parameters

[SINCE Orbeon Forms 4.11]

You can specify parameters to pass to the JavaScript, for example:
 
```xml
<xxf:script>
    <xxf:param name="binding" value="xxf:binding('fr-code-mirror')"/>
    <xxf:body>
        var instance = YAHOO.xbl.fr.CodeMirror.instance(this);
        instance.enabled();
        instance.xformsValue(binding);
    </xxf:body>
</xxf:script>
```

In order to do this:

- nest as many `<xxf:param>` elements as there are parameters to pass
    - the `name` attribute specifies the name of the JavaScript parameter to pass
    - the `value` attribute contains an XPath expression evaluating the value of the parameter
- embed the JavaScript code within an `<xxf:body>` element instead of directly under `<xxf:script>` or `<xf:action>`

Parameters are scoped at the beginning of the embedded JavaScript and you just access them by name.
 
For an example within Orbeon Forms, see [code-mirror.xbl](https://github.com/orbeon/orbeon-forms/blob/83c1bde2386bc5c69af72a132db61378d2077fc9/src/resources-packaged/xbl/orbeon/code-mirror/code-mirror.xbl).

## Calling server-side XPath

Simple server-side scripts can be written in XPath. This is particularly useful when using extension functions that have side effects.

You specify an XPath script either with:

```xml
<xf:action type="xpath">
```

or:

```xml
<xxf:script type="xpath">
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

Two action names are available to run scripts:

* `<xxf:script>`:
    * this is a custom Orbeon Forms action
    * this never runs nested XForms actions
    * without a `type` attribute, the default is to run client-side JavaScript 
    * with a `type` attribute, this is able to run nested client-side JavaScript or server-side XPath scripts
* `<xf:action type="...">`:
    * this is a standard XForms action with an extension `type` attribute
    * without a `type` attribute, the behavior is standard XForms and runs nested XForms actions
    * with a `type` attribute, this is able to run nested client-side JavaScript or server-side XPath scripts

Orbeon favors the use of `<xf:action type="...">` when possible.
