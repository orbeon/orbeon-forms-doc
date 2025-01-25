# Examples of formulas

## Testing on a Yes No Answer field

Say you have a *Yes/No Answer* field (before 2016.1 called *Boolean Input*), named `yes-no`, and if the answer is *Yes*, you would like to show another field. In the *Visibility* expression for this other field, you should write:

```xpath
$yes-no/string() = 'true'
```

- Why not just write `$yes-no`? If you do, the expression will always evaluate to true, because Orbeon Forms evaluates `boolean($yes-no)`: since `$yes-no` points to a node, the `boolean` function [always returns true](https://www.w3.org/TR/xpath-functions-3/#func-boolean).
- Why not write `$yes-no = true()`? In this case, since `$yes-no` compared to boolean value, it is [atomized](https://www.w3.org/TR/xpath-30/#id-atomization). The node pointed to by `$yes-no` is of type boolean, so the atomization process takes the node typed value. When the node contains the string `true` or `false`, this expression works as expected. However, if it contains an empty string, which up to Orbeon Forms 2017.2 is the default value for that field, then the expression fails. For the purpose of determining whether a field is valid, read-only, or visible, when the expression fails the result will be considered to be `false`, so it will work if the expression is used on its own. However, if the expression is used in an `or` clause (`$yes-no = true() or something`), if the `$yes-no = true()` part is evaluated first, the expression as a whole will fail and return false, which is incorrect in case the other part of the `or` clause is true. Also, in the case of a calculated value, the result will be the empty string, instead of `false`.

## Sum of values in a repeat

### With Orbeon Forms 4.5 and newer

Scenario: compute the sum of values in multiple repeat repetitions.
Say you have:

- a repeated grid
- a decimal field called `price` on each row
- an integer field called `quantity` on each row
- a decimal text output field called `row-total` on each row
- a decimal text output field called `total` below the grid

You want to compute the row totals and athe general total.

Calculated value expression for `row-total`:

```xpath
$price * $quantity
```

Calculated value expression for `total`:

```xpath
sum($row-total[string() castable as xs:decimal], 0.0)
```

Explanation:

- when accessing control values with variables, the "closest" variables are found
- this means that the `row-total` control calculation applies to the closest `price` and `quantity` controls, that is, those on the same row, and each row gets its own total
- the `total` calculation is outside the repeat, and when it refers to `$row-total`, all `row-total` values are returned
- `sum()` is a standard XPath function to compute the sum of a sequence of items
- the predicate `[string() castable as xs:decimal]` excludes values that are blank or not a decimal number
- see this [blog post](https://blog.orbeon.com/2013/08/formulas-for-summing-values-done-right.html) for the use of `string()` within the predicate
- `sum()` supports a second argument which is the value to return in case no value satisfies the predicate (this makes sure that we return a decimal value, as we are using a literal decimal 0.0)

See also:

- [Resolution of repeated controls](formulas.md#resolution-of-repeated-controls)
- [Formulas for summing values, done right](https://blog.orbeon.com/2013/08/formulas-for-summing-values-done-right.html).
- [Unexpected result with variable inside an `<xf:bind>` iteration #152](https://github.com/orbeon/orbeon-forms/issues/152)

### From Orbeon Forms 4.0 to 4.4.x

Scenario: compute the sum of values in multiple repeat repetitions.

Say you have:

* a repeat called `my-repeat`
* with a decimal field called `number` on each row

Calculated value expression:

```xpath
sum($my-repeat/number[string() castable as xs:decimal])
```

Explanation:

* `$my-repeat` points to the repeat data's enclosing XML elements
* the nested `/number` path points to the `number` field within each iteration
* `[string() castable as xs:decimal]` excludes values that are blank or not a decimal number
* `sum()` is a standard XPath function to compute the sum of a sequence of items

## Constrain a number between two values

Scenario: Make the current integer number field valid only if its value is between two values, say 12 and 17 included.

Expression:

```xpath
. >= 12 and . <= 17
```

Explanation:

- `.` refers to the current value of the control
- `>=` or `ge` means "greater than or equals to"
- `<=` or `le` means "less than or equals to"
- `and` is the logical "and" operator

If you want to refer to a specific control by name, you can use:

```xpath
$my-control >= 12 and $my-control <= 17
```

## Constrain the length of a string between two values

Scenario: Make the current field valid only if its length is between two values, say 2 and 140.

Expression:

```xpath
string-length(.) >= 2 and string-length(.) <= 140
```

Explanation:

- `.` refers to the current value of the control
- The standard `string-length()` function returns the length of its argument
- `>=` or `ge` means "greater than or equals to"
- `<=` or `le` means "less than or equals to"
- `and` is the logical "and" operator

If you want to refer to a specific control by name, you can use:

```xpath
string-length($message) >= 2 and string-length($message) <= 140
```

[SINCE Orbeon Forms 4.10]

The same can be expressed, for the current control, as:

```xpath
xxf:min-length(2) and xxf:max-length(140)
```

## Validating with a regular expression

Scenario: check that a given value matches a regular expression, for example "only ASCII letters and digits, the dash, and underscore character".

```xpath
matches(., '^[A-Za-z0-9\-_]+$')
```

Explanation:

- the standard `matches()` function applies the regular expression passed as second argument to the first argument, and returns true if it does match

## Validating with a regular expression for an optional value

Scenario: check that a given number value is either blank or has exactly 5 digits.

```xpath
matches(string(.), '\d{5}') or xxf:is-blank(string(.))
```

Explanation:

- the standard `matches()` function applies the regular expression passed as second argument to the first argument, and returns true if it does match
- the built-in `xxf:is-blank()` function returns `true()` if the value passed is blank
- since the data is a number, we use the `string(.)` function to convert the value to a string before passing it to functions

## Make a control read-only based on the value of another control

Scenario: Make a control read-only if the value of the `first-name` control is blank:

Expression:

```xpath
xxf:is-blank($first-name)
```

Explanation:

- `$first-name` returns the value of the control with name "first-name"
- the built-in `xxf:is-blank()` function returns `true()` if the value passed is blank

## Setting a dynamic initial value

Scenario: As a form author, you can set a _static initial value_ for a control simply by setting that value at design time. For example:

* Enter a value in an input field
* Select an item in a dropdown list

But not all initial values can be static. For example, you might want a date selection control to contain the current date until the user changes it. In this case, you can use an "Initial Value" expression.

Initial Value expression:

```xpath
current-date()
```

Explanation:

* `current-date()` is a standard XPath function returning the current date.

## Simple calculated values

Scenario: compute the sum of two numbers entered by the user in two fields, "quantity1" and "quantity2".

Calculated Value expression:

```xpath
if ($quantity1 castable as xs:integer and $quantity2 castable as xs:integer)
then $quantity1 + $quantity2
else ''
```

Explanation:

* `if (...) then ... else ...` evaluates a condition and then returns one of two alternatives
* the condition `quantity1 castable as xs:integer` checks that the value from the field "quantity1" is an integer
* `quantity1 + $quantity2` simply adds the two values
* the value `''` represents an empty string
This can be specified for example on a Text Output control.

*NOTE: If the value of a control is calculated, by default it is also marked as read-only. If you want a calculated control to be still editable by the user, set its Read-Only property explicitly to `false()`.*

## Access a control in a particular repeat iteration

Given [this form](https://gist.github.com/orbeon/e7272c1b2499c3a5fb5f) and a control called `name` within a repeat:

- `$name[2]`: return the value of the control in the second iteration
- `string-join($name, ', ')`: join all values with commas
- `count($name)`: return the number of values

*NOTE: This works when the expression is outside repeat repetitions. For expressions within the same repeat, `$name` returns the closest control.*

See also [Model bind variables](/xforms/model-bind-variables.md) and this [StackOverflow question](http://stackoverflow.com/questions/27820641/access-to-iterated-controls-in-repeated-sections-in-orbeon/27830585?noredirect=1#comment44118606_27830585).

## Check the role(s) of the current user

See [Form Fields](/form-runner/access-control/form-fields.md).

## Check the Form Runner mode

### Modes

The Form Runner detail page can have the following modes: 
 
- `new`
- `edit`
- `view`
- `pdf`
- `email`
- `test` (when you are testing a form within Form Builder)

### With Orbeon Forms 2016.2 and newer

You can use the [`fr:mode()` XPath function](/xforms/xpath/extension-form-runner.md#frmode) exposed by Form Runner to all XPath expressions.

```xpath
fr:mode() = 'edit'
```

### With Orbeon Forms 2016.1.x and earlier

A special XPath variable named `$fr-mode` is exposed by Form Runner to all XPath expressions. 

You can test the mode as follows, for example in a Visibility expression:

```xpath
$fr-mode = 'edit'
```

## Access HTTP request parameters and HTTP headers

It can be useful to access HTTP headers to set default values upon form initialization, for example when single sign-on systems use HTTP headers as a way of communicating information to an application.

XPath expressions have access to a special function, `xxf:request-header()`, which allows retrieving a header by name. Example of setting the default value of a field using an initial value:

```xpath
xxf:get-request-header('full-name')
```

*NOTE: With Orbeon Forms 3.8 and 3.9, headers cannot be reliably accessed after the form is initialized, so this function should be used for setting initial values on controls only. See the next scenario for a workaround.*

## Check the type of an attachment

Scenario: field `my-attachment` must be a PDF file.

Constraint expression:

```xpath
ends-with(lower-case($my-attachment/@filename), '.pdf')
```

Explanation:

* Form Runner stores information about a file into XML attributes:
    * `@filename` accesses the file name as sent by the user's browser
    * `@mediatype` accesses the file type as sent by the user's browser
    * `@size` accesses the file size
* `$my-attachment/@filename` returns the file name associated with attachment "my-attachment"
* The `lower-case()` function converts that name to a lower case value
* The `ends-with()` function checks whether its first argument ends with the second argument

Similarly, you can test the file type:

```xpath
$my-attachment/@mediatype = 'application/pdf'
```

*NOTE: Because the file name and file type are sent by the client's browser, they cannot be trusted. This should only be considered a first level of data validation, and further validation based on the content must be performed at a later time if needed. See also issue [#1838](https://github.com/orbeon/orbeon-forms/issues/1838).*

## Set the value of a field from a URL parameter

Scenario: We want to set the value of a field from a URL parameter, but only if that parameter exists. If it doesn't, we want to leave the value of the field as it is.

```xpath
(xxf:get-request-parameter('my-parameter'), .)[1]
```

Explanation:

- If the parameter exists
    - `xxf:get-request-parameter()` returns a single string
    - so you get a sequence containing that string following by the current value of the field
    - we take the first value of the sequence, so the value of the parameter is used
- if the parameter doesn't exist
    - `xxf:get-request-parameter()` returns an empty XPath sequence
    - so you get a sequence containing only the current value of the field
    - we take the first value of that sequence, so we get the current value of the control

## Testing the values of a checkbox 

Say:

- a Checkboxes control is needed named `my-checkboxes`
- the Checkboxes control contains several choices, including two choices with values `foo` and `bar`
- you'd like to show another control if `my-checkboxes` has either `foo` or `bar` selected

```xpath
xxf:split($my-checkboxes) = ('foo', 'bar')
```

Explanation:

- `$my-checkboxes` refers to the XML element containing the checkbox values as a space-separated list of tokens, such as:
    - ""
    - "foo"
    - "bar"
    - "foo bar"
    - "baz foo bar"
    - etc. if you have more values
- the `xxf:split()` function splits that value on spaces and returns an XPath sequence of string tokens
- the `=` comparison will return `true()` if at least one value matches another value on the other side

So in the end that formula returns `true()` if the field `my-checkboxes` has the `foo` or `bar` value selected.

## Constraint the number of selected checkboxes

Scenario: For a given set of checkboxes, make sure the number of selected checkboxes is at most 3.

```xpath
count(xxf:split(.)) le 3
```

Explanation:

- `xxf:split(.)` tokenizes the space-separated values selected by the checkbox
- the number of tokens obtained with `count()` corresponds to the number of selected checkboxes
- then this makes sure the number of tokens is lower than or equal to 3

## Validating a Legal Entity Identifier (LEI)

You can use the following formula, in Control Settings, Validation and Alerts, Formula, to validate the relevant [LEI](https://en.wikipedia.org/wiki/Legal_Entity_Identifier) field. Should you need to assess the validity of a LEI field that is somewhere else in the form, just replace `.` on the first line with the appropriate expression. An examples of valid LEI are:

- `F50EOCWSQFAUVO9Q8Z97` (examples in ISO/DIS 17442)
- `549300Y7W34DK0WBPY51` (Google)
- `98450054NFCE7A67C172` (ManagedLEI)

```xpath
let $lei := .
return
  string-length($lei) = 20 and
  xs:decimal(
    string-join(
      for $code in string-to-codepoints($lei)
      return
        if ($code >= string-to-codepoints('A') and string-to-codepoints('Z') >= $code) then
          (: Letters A-Z :)
          xs:string($code - string-to-codepoints('A') + 10)
        else if ($code >= string-to-codepoints('0') and string-to-codepoints('9') >= $code) then
          (: Digits 0-9 :)
          xs:string($code - string-to-codepoints('0'))
          (: Unrecognized characters :)
        else
          '',
      ''
    )
  ) mod 97 = 1
```

## Validating an International Securities Identification Number (ISIN)

You can use the following formula, in Control Settings, Validation and Alerts, Formula, to validate the relevant [ISIN](https://en.wikipedia.org/wiki/International_Securities_Identification_Number) field. Should you need to assess the validity of a ISIN field that is somewhere else in the form, just replace `.` on the first line with the appropriate expression. Examples of valid ISIN are:

- `US0378331005` (Apple, Inc.)
- `AU0000XVGZA3` (Treasury Corporation of Victoria)
- `GB0002634946` (BAE Systems)

```xpath
let $isin := .
return
  (
    let
      $without-checksum := substring($isin, 1, 11),
      $letter-as-numbers-string :=
        string-join(
          for $code in string-to-codepoints($without-checksum)
          return
            if ($code >= string-to-codepoints('A') and string-to-codepoints('Z') >= $code) then
              (: Letters A-Z :)
              xs:string($code - string-to-codepoints('A') + 10)
            else if ($code >= string-to-codepoints('0') and string-to-codepoints('9') >= $code) then
              (: Digits 0-9 :)
              xs:string($code - string-to-codepoints('0'))
              (: Unrecognized characters :)
            else
              '',
          ''
        ),
      $letter-as-numbers-digits :=
        for $code in string-to-codepoints($letter-as-numbers-string)
        return xs:decimal(codepoints-to-string($code)),
      $reversed-digits     := reverse($letter-as-numbers-digits),
      $group-1-digits      := $reversed-digits[position() mod 2 = 1],
      $group-2-digits      := $reversed-digits[position() mod 2 = 0],
      $group-1-x2-decimals := for $d in $group-1-digits return $d * 2,
      $group-1-x2-string   := string-join(for $d in $group-1-x2-decimals return string($d), ''),
      $group-1-x2-digits   := for $code in string-to-codepoints($group-1-x2-string)
                              return xs:decimal(codepoints-to-string($code)),
      $sum-digits          := sum(($group-1-x2-digits, $group-2-digits)),
      $checksum-computed   := string((10 - ($sum-digits mod 10)) mod 10),
      $checksum-provided   := substring($isin, 12, 1),
      $proper-checksum     := $checksum-computed = $checksum-provided,
      $proper-length       := string-length($isin) = 12
    return
      $proper-checksum and $proper-length
  )
```

## Number of weekdays between 2 dates

- *Input* – In the following XPath expression, the start and end dates are inline, so you'll most likely want to modify it to refer to, say, dates entered by users in a form fields.
- *Weekends* – The expression takes Saturday and Sunday to be part of the weekend.
- *First day of the week* – The expression assumes Monday is the first day of the week. For instance check that on your system `format-date(xs:date('2018-12-10'), '[F1]')` returns 1. If not, you'll need to change the expressions for `$startW` and `$endW`.
- *Credits* – This is a translation to XPath of [Java code written by Roland](https://stackoverflow.com/a/44942039/5295).

```xpath
for
    $start          in xs:date('2018-12-15'),
    $end            in xs:date('2018-12-26'),
    $startW         in xs:integer(format-date($start, '[F1]')),
    $endW           in xs:integer(format-date($end, '[F1]')),
    $days           in days-from-duration($end - $start),
    $daysNoWeekends in $days - (floor($days div 7) * 2)
return
    if ($days mod 7 != 0 and ($startW = 7 or $endW = 7)) then $daysNoWeekends - 1
    else if ($endW < $startW) then $daysNoWeekends - 2
    else $daysNoWeekends
```

## Converting a date/time to a named timezone

The following formula formats the current date/time to an ISO date/time in a specific named timezone: 

```xpath
format-dateTime(
  current-dateTime(),
  '[Y0001]-[M01]-[D01]T[H01]:[m01]:[s01]',
  'en',
  (),
  'Europe/Paris'
) 
```

*NOTE: See [issue #4981](https://github.com/orbeon/orbeon-forms/issues/4981) for availability of a fix that impacts this example.*

## See also

- [Formulas](formulas.md)
- [Form Builder Validation](validation.md)
- [Formulas inspector](formulas-inspector.md)
- [When the internal data format matters](/form-runner/data-format/form-data.md#when-the-internal-data-format-matters)
- Blog posts
    - [Better formulas with XPath type annotations](https://blog.orbeon.com/2013/01/better-formulas-with-xpath-type.html)
    - [Formulas for summing values, done right](https://blog.orbeon.com/2013/08/formulas-for-summing-values-done-right.html)
    - [Control required values with formulas in Orbeon Forms 4.7](https://blog.orbeon.com/2014/09/control-required-values-with-formulas.html)
    - [Improvements to variables in formulas](https://blog.orbeon.com/2022/04/improvements-to-variables-in-formulas.html)
- [XForms Validation](/xforms/validation.md)
