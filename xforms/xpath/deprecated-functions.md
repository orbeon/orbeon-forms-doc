# Core functions

<!-- toc -->

## xxf:event()

_NOTE: This function is deprecated since Orbeon Forms 4 and is just an alias for the `event()` function._

```ruby
xxf:event(
    $attribute-name as xs:string
) as item()*
```

`xxf:event()` works like the XForms 1.1 `event()` function, except that when using XBL components, `xxf:event()` returns event information from the original event instead of the retargeted event.

## xf:if() / xxf:if()

_NOTE: Prefer the XPath 2 `if(...) then ... else ...` construct._

```ruby
xf:if()
xxf:if()
```

This function implements the semantic of the XForms 1.0 `if()` function.