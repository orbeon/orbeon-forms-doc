# Compatibility

## Support for let expressions

\[SINCE Orbeon Forms 2016.2]

As an extension to XPath 2.0, Orbeon Forms supports "let expressions" in XPath. XPath 2.0 doesn't support such expressions, while XQuery 1.0 and XPath 3.0 and 3.1 do. Example:

```
let $a := 'Lorem ipsum dolor',
    $b := 'sit amet',
    $c := string-join(($a, $b), ' ')
return
    concat($c, ', consectetur adipiscing elit.')
```

## XForms 1.1 if() function

The use of the XForms 1.1 `if()` function clashes with XPath 2.0's built-in `if (...) then ... else ...` construct.

The bottom line is that you cannot directly use the XForms `if()` function in Orbeon Forms. The following, for example, will not work in Orbeon Forms:

```
if (normalize-space(/first-name) = '', '', concat('Hello, ', /first-name, '!'))
```

The good news is that you have ways around this issue:

Use the XPath 2.0 `if (...) then ... else ...` construct instead:

```
if (normalize-space(/first-name) = '') then '' else concat('Hello, ', /first-name, '!')
```

Use the Orbeon Forms `xf:if()` extension, which behaves like the XForms `if()` function (not recommended):

```
xf:if (normalize-space(/first-name) = '', '', concat('Hello, ', /first-name, '!'))
```

## XForms seconds-from-dateTime() function

The XForms 1.1 `seconds-from-dateTime()` function clashes with the XPath 2.0 function of the same name:

* they take a parameter of different types
  * the XForms 1.1 function takes an `xs:string`
  * the XPath 2 function takes an `xs:dateTime`
* they do not have the same semantic
  * the XForms 1.1 function returns "the number of seconds difference between the specified dateTime (normalized to UTC) and 1970-01-01T00:00:00Z"
  * the XPath 2 function returns "an `xs:decimal` value greater than or equal to zero and less than 60, representing the seconds and fractional seconds in the localized value of `$arg`"
* The XForms version of the function is available as `xf:seconds-from-dateTime()`.
* The XPath 2.0 version of the function is available without a namespace as `seconds-from-dateTime()`.
