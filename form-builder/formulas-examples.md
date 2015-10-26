# Examples of formulas

<!-- toc -->

## Constrain a number between two values

Scenario: Make the current integer number field valid only if its value is between two values, say 12 and 17 included.

Expression:

```ruby
. >= 12 and . <= 17
```

Explanation:

- `.` refers to the current value of the control
- `>=` or `ge` means "greater than or equals to"
- `<=` or `le` means "less than or equals to"
- `and` is the logical "and" operator

If you want to refer to a specific control by name, you can use:

```ruby
$my-control >= 12 and $my-control <= 17
```

## Constrain the length of a string between two values

Scenario: Make the current field valid only if its length is between two values, say 2 and 140.

Expression:

```ruby
string-length(.) >= 2 and string-length(.) <= 140
```

Explanation:

- `.` refers to the current value of the control
- The standard `string-length()` function returns the length of its argument
- `>=` or `ge` means "greater than or equals to"
- `<=` or `le` means "less than or equals to"
- `and` is the logical "and" operator

If you want to refer to a specific control by name, you can use:

```ruby
string-length($message) >= 2 and string-length($message) <= 140
```

[SINCE Orbeon Forms 4.10]

The same can be expressed, for the current control, as:

```ruby
xxf:min-length(2) and xxf:max-length(140)
```

## Validating with a regular expression

Scneario: check that a given value matches a regular expression, for example "only ASCII letters and digits, the dash, and underscore character".

```ruby
matches(., '^[A-Za-z0-9\-_]+$')
```

Explanation:

- the standard `matches()` function applies the regular expression passed as second argument to the first argument, and returns true if it does match

## Make a control read-only based on the value of another control

Scenario: Make a control read-only if the value of the `first-name` control is blank:

Expression:

```ruby
normalize-space($first-name) = ''
```

Explanation:

* `$first-name` returns the value of the control with name "first-name"
* `normalize-space()` is a standard XPath function which removes all leading and trailing white space and combine internal white space. Using this function ensures that, even if the value contains spaces, it resolves to an empty string.
* `= ''` compares the result of the function to the empty string

## Setting a dynamic initial value

Scenario: As a form author, you can set a _static initial value_ for a control simply by setting that value at design time. For example:

* Enter a value in an input field
* Select an item in a dropdown list

But not all initial values can be static. For example, you might want a date selection control to contain the current date until the user changes it. In this case, you can use an "Initial Value" expression.

Initial Value expression:

```ruby
current-date()
```

Explanation:

* `current-date()` is a standard XPath function returning the current date.

## Simple calculated values

Scenario: compute the sum of two numbers entered by the user in two fields, "quantity1" and "quantity2".

Calculated Value expression:

```ruby
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

## Sum of values in a repeat

[SINCE Orbeon Forms 4.x]

Scenario: compute the sum of values in multiple repeat iterations.

Say you have:

* a repeated grid
* a decimal field called `price` on each row
* an integer field called `quantity` on each row
* a decimal text output field called `row-total` on each row
* a decimal text output field called `total` below the grid

You want to compute the row totals and athe general total called.

Calculated value expression for thw 

[SINCE Orbeon Forms 4.0]

Scenario: compute the sum of values in multiple repeat iterations.

Say you have:

* a repeat called `my-repeat`
* with a decimal field called `number` on each row

Calculated value expression:

```ruby
sum($my-repeat/number[. castable as xs:decimal])
```

Explanation:

* `$my-repeat` points to the repeat data's enclosing XML elements
* the nested `/number` path points to the `number` field within each iteration
* [`. castable as xs:decimal]` excludes values that are blank or not decimal number
* `sum()` is a standard XPath function to compute the sum of a sequence of items

## Access a control in a particular repeat iteration

Given [this form](https://gist.github.com/orbeon/e7272c1b2499c3a5fb5f) and a control called `name` within a repeat:

- `$name[2]`: return the value of the control in the second iteration
- `string-join($name, ', ')`: join all values with commas
- `count($name)`: return the number of values

*NOTE: This works when the expression is outside repeat iterations. For expressions within the same repeat, `$name` returns the closest control.*

See also [[Model bind variables|XForms ~ Model bind variables]] and this [StackOverflow question](http://stackoverflow.com/questions/27820641/access-to-iterated-controls-in-repeated-sections-in-orbeon/27830585?noredirect=1#comment44118606_27830585).

## Check the role(s) of the current user

See [[Form Fields|Form Runner ~ Access Control ~ Form Fields]].

## Check the Form Runner mode

A special XPath variable named `$fr-mode` is exposed by Form Runner to all XPath expressions. Its value can be one of:

- `new`
- `edit`
- `view`
- `pdf`
- `email`

You can test the mode as follows, for example in a Visibility expression:

```ruby
$fr-mode = 'edit'
```

## Access HTTP request parameters and HTTP headers

It can be useful to access HTTP headers to set default values upon form initialization, for example when single sign-on systems use HTTP headers as a way of communicating information to an application.

XPath expressions have access to a special function, `xxf:request-header()`, which allows retrieving a header by name. Example of setting the default value of a field using an initial value:

```ruby
xxf:get-request-header('full-name')
```

*NOTE: With Orbeon Forms 3.8 and 3.9, headers cannot be reliably accessed after the form is initialized, so this function should be used for setting initial values on controls only. See the next scenario for a workaround.*

## Check the type of an attachment

Scenario: field `my-attachment` must be a PDF file.

Constraint expression:

```ruby
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

```ruby
$my-attachment/@mediatype = 'application/pdf'
```

*NOTE: Because the file name and file type are sent by the client's browser, they cannot be trusted. This should only be considered a first level of data validation, and further validation based on the content must be performed at a later time if needed. See also issue [#1838](https://github.com/orbeon/orbeon-forms/issues/1838).*

## Set the value of a field from a URL parameter

Scenario: We want to set the value of a field from a URL parameter, but only if that parameter exists. If it doesn't, we want to leave the value of the field as it is.

```ruby
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

## Constraint the number of selected checkboxes

Scenario: For a given set of checkboxes, make sure the number of selected checkboxes is at most 3.

```ruby
count(xxf:split(.)) le 3
```

Explanation:

- `xxf:split(.)` tokenizes the space-separated values selected by the checkbox
- the number of tokens obtained with `count()` corresponds to the number of selected checkboxes
- then this makes sure the number of tokens is lower than or equal to 3

## See also

- [Formulas](formulas.md)
- [Form Builder Validation](validation.md)
- [Better formulas with XPath type annotations](http://blog.orbeon.com/2013/01/better-formulas-with-xpath-type.html)
- [Formulas for summing values, done right](http://blog.orbeon.com/2013/08/formulas-for-summing-values-done-right.html)
- [Control required values with formulas in Orbeon Forms 4.7](http://blog.orbeon.com/2014/09/control-required-values-with-formulas.html)
- [[XForms Validation|XForms ~ Validation]]
