# Model functions

<!-- toc -->

## xxf:bind()

```ruby
xxf:bind(
    $bind-id as xs:string
) as node()*
```

The `xxf:bind()` function returns the node-set of a given bind:

```xml
<!-- The following... -->
<xf:input bind="my-bind">
...
</xf:input>
<!-- ...is equivalent to this: -->
<xf:input ref="xxf:bind('my-bind')">
...
</xf:input>
```

`xxf:bind()` is particularly useful when referring to a bind is subject to a condition:

```xml
<xf:hint
    ref="
        for $bind in xxf:bind('my-hint') return
            if (normalize-space($bind) = '') then
                instance('default-hint')
            else
                $bind
"/>
```

## xxf:evaluate-bind-property()

```ruby
xxf:evaluate-bind-property(
    $bind-id        as xs:string,
    $property-qname as xs:string
) as xs:anyAtomicType?
```

The `xxf:evaluate-bind-property()` function evaluates a property of a given bind. Depending on the property, it returns:

* `xs:string`
    * `calculate`
    * `xxf:default`
    * custom MIPs
* `xs:boolean`
    * `relevant`
    * `readonly`
    * `required`
    * `constraint`
* `xs:QName`

If the property is not present on the bind, an empty sequence is returned.

```xml
<xf:bind
    id="my-bind"
    ref="foo"
    xxf:default="count(preceding-sibling::foo) + 42"
    relevant="count(preceding-sibling::foo) mod 2 = 0"
    type="xs:integer"/>
...
<xf:output value="xxf:evaluate-bind-property('my-bind', 'xxf:default')"/>
<xf:output value="xxf:evaluate-bind-property('my-bind', 'relevant')"/>
<xf:output value="xxf:evaluate-bind-property('my-bind', 'type')"/>
```

_NOTE: The property is instantly evaluated, which means that it might be different from the value evaluated during the previous model recalculation or revalidation._

## xxf:type()

```ruby
xxf:type(
    $node as node()?
) as xs:QName?
```

The `xxf:type()` function returns the type of the instance data node passed as parameter. If an empty sequence is passed, the function returns an empty sequence. Otherwise, the type of the instance data node is searched. If no type information is available, the function returns an empty sequence. Otherwise, a QName associated with the type is returned.

```xml
<xf:output
    value="
        for $t in xxf:type(date) return
            concat(
                '{',
                namespace-uri-from-QName($t),
                '}',
                local-name-from-QName($t)
            )">
    <xf:label>Type:</xf:label>
</xf:output>
```

## xxf:valid()

```ruby
xxf:valid() as xs:boolean
xxf:valid($item as xs:item*) as xs:boolean
xxf:valid($item as xs:item*, $recurse as xs:boolean) as xs:boolean
xxf:valid($item as xs:item*, $recurse as xs:boolean, $relevant) as xs:boolean
```

The `xxf:valid()` function returns the validity of an instance data node or of a subtree of instance data specified by the first argument.

If the first argument is specified, its first item is obtained, if any.

If the first argument is not specified, the context item is used, if any

If the second argument is specified and `true()`, the function recurses into attributes and descendant nodes.

If no item is available, or if the item is an atomic value, the function returns `true()`.

If the optional third argument is specified and set to true(), non-relevant nodes are ignored, as in the case of `xf:submission`.

Because of the way the XForms processing model is defined, the evaluation of `calculate`, `required`, `readonly` and `relevant` takes place during the processing of the `xforms-recalculate` event, which generally takes place before the processing of vaidation with the `xforms-revalidate` event. This means that by default using `xxf:valid()` to control, for example, whether a button is read-only or relevant will not work.

## xxf:custom-mip

```ruby
xxf:custom-mip(
    $item as item()*,
    $mip-name as xs:string
) as xs:string
```

Return the value of the custom MIP of the first item specified, if any.

The name of the property must match the qualified name used on the `xf:bind` that sets the property.

## xxf:invalid-binds()

```ruby
xxf:invalid-binds(
    $node as node()?
) as xs:string*
```

The `xxf:invalid-binds()` function returns a sequence of strings. If the node was made invalid because of an `<xf:bind>` element, then the id of that bind element is present in the list.

This function is useful to help determine the reason why a node is invalid:

```xml
<xf:bind ref="age" constraint=". ge 21" id="age-limit"/>
...
<xf:action if="xxf:invalid-binds(event('xxf:binding')) = 'age-limit'">
...
</xf:action>
```

You can also use this function to show bind-specific errors:

```xml
<xf:alert value="if (xxf:invalid-binds(.)="age-limit" )="" then="" ...="" else="" ..."="">
```

Note however that the function actually only returns the first invalid bind at the moment, not all of them. So this works for scenarios where error messages go from general to specific.
