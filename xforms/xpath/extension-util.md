# Utility functions

<!-- toc -->

## xxf:is-blank()

[SINCE Orbeon Forms 2016.1]

```xpath
xxf:is-blank() as xs:boolean
xxf:is-blank($value as xs:string?) as xs:boolean
```

If no argument is passed, use the context item converted to a string.

If an empty sequence is passed, return `true()`.

Return `true()` if the argument or context item string is a blank string. Otherwise return `false()`.

With one argument, this function returns the same as `not(xxf:non-blank(arg))`.

A string is considered blank if all leading and trailing Unicode whitespace, non-breakable space, zero-width space, and ISO control characters are removed, and the result is the empty string.

## xxf:non-blank()

[SINCE Orbeon Forms 2016.1]

```xpath
xxf:non-blank() as xs:boolean
xxf:non-blank($value as xs:string?) as xs:boolean
```

If no argument is passed, use the context item converted to a string.

If an empty sequence is passed, return `false()`.

Return `true()` if the argument or context item string is not a blank string. Otherwise return `false()`.

With one argument, this function returns the same as `not(xxf:is-blank(arg))`.

A string is considered blank if all leading and trailing Unicode whitespace, non-breakable space, zero-width space, and ISO control characters are removed, and the result is the empty string.

## xxf:split()

[SINCE Orbeon Forms 4.3]

```xpath
xxf:split() as xs:string*
xxf:split($value as xs:string) as xs:string*
xxf:split($value as xs:string, $separators as xs:string) as xs:string*
```

Split a string with one or more separator characters.

If no argument is passed, split the context item on white space.

If `$separator` is specified, each character passed is allowed as separator.

```xpath
xxf:split(' foo  bar   baz ')
xxf:split('foo$bar_baz', '$_')
element/xxf:split()
element/@value/xxf:split()
```

## xxf:trim()

[SINCE Orbeon Forms 2016.1]

```xpath
xxf:trim() as xs:boolean
xxf:trim($value as xs:string?) as xs:string?
```

If no argument is passed, use the context item converted to a string.

If an empty sequence is passed, return the empty sequence.

The result is a string with all leading and trailing Unicode whitespace, non-breakable space, zero-width space, and ISO control characters are removed.
