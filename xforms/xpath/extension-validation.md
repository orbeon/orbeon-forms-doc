# Validation functions

<!-- toc -->

## xxf:max-length()

[SINCE Orbeon Forms 4.10]

```ruby
xxf:max-length($max as xs:integer?) as xs:boolean
```

Return `true()` if the context item converted to a string via the `string()` function contains at most the number of characters
specified by `$max.` Also return `true()` if `$max` is the empty sequence.

Following [XPath 2.0](http://www.w3.org/TR/xpath-functions/#string-types):

> what is counted is the number of XML characters in the string (or equivalently, the number of Unicode code points). Some implementations may represent a code point above xFFFF using two 16-bit values known as a surrogate. A surrogate counts as one character, not two.

## xxf:min-length()

[SINCE Orbeon Forms 4.10]

```ruby
xxf:min-length($min as xs:integer?) as xs:boolean
```

Return `true()` if the context item converted to a string via the `string()` function contains at least the number of characters
specified by `$min.` Also return `true()` if `$min` is the empty sequence.

Following [XPath 2.0](http://www.w3.org/TR/xpath-functions/#string-types):

> what is counted is the number of XML characters in the string (or equivalently, the number of Unicode code points). Some implementations may represent a code point above xFFFF using two 16-bit values known as a surrogate. A surrogate counts as one character, not two.
