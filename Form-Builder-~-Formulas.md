## XPath expressions

XPath is a standard _expression language_ for XML. It does not allow you to _modify_ XML data, but it allows you to _query_ XML data and compute values.

In general you don't need to know about XPath in Form Builder, with the exception of some properties in the Control Settings and Section Settings dialogs. XPath expressions are considered an advanced feature of Form Builder, which might require some programming knowledge.

*NOTE: Incorrect XPath expressions may cause the form to behave improperly, so caution must be applied.*

## Control Settings dialog

In the Control Details dialog, XPath expressions are used to specify the following aspects of a control:

* **Constraint:** Boolean expression specifying whether the control is valid.
    * If this field is left blank, then the validity of the control depends on the data type and the "Required" option.
    * Otherwise, the control is valid if in addition to all the other constraint being met, the result of the Boolean expression is `true()`.
* **Visibility:** Boolean expression specifying whether the control is visible.
    * If this field is left blank, then the control is always visible, unless the section is not visible.
    * Otherwise, it is visible only if the result of the Boolean expression is `true()`.
* **Read-Only:** Boolean expression specifying whether the control is read-only, that is not editable.
    * If this field is left blank, then the control is editable unless the section is read-only.
    * Otherwise, the field is editable only if the result of the Boolean expression is `false()`.
* **Initial value:** string expression returning the initial value of the control when the form first shows.  

    * Default: the value set into the field at design time (usually a blank value).  
* **Calculated Value:** string expression specifying a calculated value of the control which updates while the form user interacts with the form.  

    * Default: the default value of the control, or the value entered by the form user.  

## Section Settings dialog

In the Section Details dialog, XPath expressions are used to specify the following aspects of a section:

* **Visibility:** Boolean expression specifying whether the section is visible or not.
    * If this field is left blank, then the section is always visible.
    * Otherwise, it is visible only if the result of the Boolean expression is true().
* **Read-Only:** Boolean expression specifying whether the section is shown as read-only or not.
    * If this field is left blank, then the section content is always editable.
    * Otherwise, the section content is editable only if the result of the Boolean expression is false().

## Grid Settings dialog

TODO

## Referring to control values

XPath expressions may refer to the value of other controls in the page, using variables named after the name of the controls to use.

Scenario: make a control read-only if the value of the "first-name" control is blank:

Expression:  

```ruby
normalize-space($first-name) = ''
```

Explanation:  

* `$first-name` returns the value of the control with name "first-name"
* `normalize-space()` is a standard XPath function which removes all leading and trailing white space and combine internal white space. Using this function ensures that, even if the value contains spaces, it resolves to an empty string.
* `= ''` compares the result of the function to the empty string

## Setting a dynamic initial value

Scenario: as a form author, you can set a _static initial value_ for a control simply by setting that value at design time. For example:  

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

## Scenario: checking the role(s) of the current user

See [[Form Fields|Form Runner ~ Access Control ~ Form Fields]].

## Scenario: checking the Form Runner mode

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

## Scenario: accessing HTTP request parameters and HTTP headers

It can be useful to access HTTP headers to set default values upon form initialization, for example when single sign-on systems use HTTP headers as a way of communicating information to an application.

XPath expressions have access to a special function, `xxf:request-header()`, which allows retrieving a header by name. Example of setting the default value of a field using an initial value:

```ruby
xxf:get-request-header('full-name')
```

*NOTE: With Orbeon Forms 3.8 and 3.9, headers cannot be reliably accessed after the form is initialized, so this function should be used for setting initial values on controls only. See the next scenario for a workaround.*

## Scenario: checking the type of an attachment

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

## Scenario: validating the length of a value

Scenario: the maximum length of the "last-name" control is 30 characters.

Constraint expression:

```ruby
string-length($last-name) <= 30
```
  
Explanation:  

* The standard `string-length()` function returns the length of its argument
* The `<=` or `lt` comparator means "lower than or equal to"

## See also

- [Better formulas with XPath type annotations](http://blog.orbeon.com/2013/01/better-formulas-with-xpath-type.html)
- [Formulas for summing values, done right](http://blog.orbeon.com/2013/08/formulas-for-summing-values-done-right.html)
- [Control required values with formulas in Orbeon Forms 4.7](http://blog.orbeon.com/2014/09/control-required-values-with-formulas.html)