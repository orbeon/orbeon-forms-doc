# Deprecated functions

<!-- toc -->

## xxf:event()

```ruby
xxf:event(
    $attribute-name as xs:string
) as item()*
```

`xxf:event()` works like the XForms 1.1 `event()` function, except that when using XBL components, `xxf:event()` returns event information from the original event instead of the retargeted event.

This function is deprecated since Orbeon Forms 4 and is just an alias for the standard `event()` function.

## xf:if() / xxf:if()

```ruby
xf:if()
xxf:if()
```

This function implements the semantic of the XForms 1.0 `if()` function.

Prefer the XPath 2 `if(...) then ... else ...` construct.