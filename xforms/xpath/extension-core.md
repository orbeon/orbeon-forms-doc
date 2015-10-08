# Core functions

## xxf:evaluate-avt()

```ruby
xxf:evaluate-avt(
    $avt-expression as xs:string
) as item()*
```

This function is similar to `saxon:evaluate()` or `xxf:evaluate()`, but instead of evaluating an XPath expression, it evaluates a string representing an attribute value template.

```xml
<xf:output
    value="xxf:evalute-avt('/xforms-sandbox/service/zip-zips?state-abbreviation={state}&amp;city={city}')"/>
```

## xxf:instance()

```ruby
xxf:instance(
    $instance-id as xs:string
) as element()?
```

The `xxf:instance()` function works like the standard `instance()` function except that it searches for instances:

* independently from the current in-scope model specified by the `model` attribute
* across ancestor XBL scopes

The search works as follows:

* first, in a given XBL scope, the function searches all models within that scope
* second, it recursively searches ancestor XBL scopes

For example, if there are no XBL components, and 2 top-level models:

```xml
<xf:model id="main-model">
  <xf:instance id="main-instance">
    ...
  </xf:instance>
</xf:model>
<xf:model id="resources-model">
  <xf:instance id="resources-instance">
    ...
  </xf:instance>
</xf:model>
...
<xf:group model="main-model">
  <xf:output value="xxf:instance('resources-instance')/title"/>
</xf:group>
```

[SINCE Orbeon Forms 4.5] The `xxf:instance()` function can also take an absolute id as parameter.

## xxf:document-id()

```ruby
xxf:document-id() as xs:string`
```

Each time an XForms document is created, typically when a user requests a page, it is assigned a unique id.

This function returns the current XForms document (or page)'s unique id.

## xf:if() / xxf:if()

```ruby
xf:if()
xxf:if()
```

This function implements the semantic of the XForms 1.0 `if()` function. See Note About XPath 2.0 Expressions for more details.

## xxf:event()

_NOTE: This function is deprecated since Orbeon Forms 4 and is just an alias for the `event()` function._

```ruby
xxf:event(
    $attribute-name as xs:string
) as item()*
```

`xxf:event()` works like the XForms 1.1 `event()` function, except that when using XBL components, `xxf:event()` returns event information from the original event instead of the retargeted event.
