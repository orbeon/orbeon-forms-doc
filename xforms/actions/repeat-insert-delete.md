# Repeat, Insert, and Delete XForms Actions

<!-- toc -->

## Basics

A very common requirement of user interfaces consists in repeating visual elements, such as rows in a table or entries in a list. Those repeated sections usually (but not always) have an homogeneous aspect: they all have the same or a very similar structure. For example, multiple table rows will differ only in the particular content they display in their cells. An example of this is an invoice made of lines with each a description, unit price, and quantity.

XForms provides a very powerful mechanism to implement such repeated structures: the `<xf:repeat>` action. You use `<xf:repeat>` around XHTML elements or XForms controls. For example, to repeat a table row, you write:

```xml
<xf:repeat>
    <xh:tr>
        ...
    </xh:tr>
</xf:repeat>
```

This is not enough to be functional code: you need to indicate to the `<xf:repeat>` element how many repetitions must be performed. This is done not by supplying a simple count value, but by binding the the element to a node-set with the `ref` attribute. Consider the following XForms instance:

```xml
<xf:instance id="employees-instance">
    <employees>
        <employee>
            <first-name>Alice</first-name>
        </employee>
        <employee>
            <first-name>Bob</first-name>
        </employee>
        <employee>
            <first-name>Marie</first-name>
        </employee>
    </employees>
</xf:instance>
```

Assuming you want to produce one table row per employee, add the following `ref` attribute:

```xml
<xf:repeat ref="instance('employees-instance')/employee">
    <xh:tr>...</xh:tr>
</xf:repeat>
```


This produces automatically three `xh:tr` rows. Note that we explicitly use the XForms `instance()` function, but you may not have to do so if that instance is already in scope. Then you display in each row the content of the `first-name` element for each employee:

```xml
<xf:repeat ref="instance('employees-instance')/employee">
    <xh:tr>
        <xh:td>
            <xf:output ref="first-name"/>
        </xh:td>
    </xh:tr>
</xf:repeat>
```

This works because for each iteration, the _context node_ for the `ref` attribute changes: during the first iteration, the context node is the first `employee` element of the XForms instance; during the second iteration, the second `employee` element, and so on.

## Repeat index

Each `<xf:repeat>` element has an associated index, representing the notion of a currently selected repeat iteration. This information can be used in CSS for example to highlight the currently selected iteration. The current index for a given repeat can be obtained with the XPath `index()` function.

Each nested repeat keeps its own separate index value.

The `index()` function should not be confused wit the `position()` function.

_NOTE: XForms 1.1 does not explicitly limit in what type of XPath expressions `index()` can be used. However, in Orbeon Forms, it is strongly advised at the moment to only use `index()` within actions, and to avoid using it in control bindings and binds, as doing so may yield unpredictable results._

## Deleting iterations with the delete action

`<xf:repeat>` may be used purely for display purposes, but it can also be used for interactively editing repeated data. This includes allowing the user to delete and insert iterations. Two XForms actions are used for this purpose: `<xf:delete>` and `<xf:insert>`.

`<xf:delete>` is provided with a `ref` attribute pointing to the collection of nodes to delete. It also has an optional `at` attribute, which contains an XPath expression returning the index of the element to delete. See how `<xf:delete>` is used in these scenarios:

```xml
<!-- This deletes the last element of the collection -->
<xf:delete ref="employees" at="last()"/>

<!-- This deletes the first element of the collection -->
<xf:delete ref="employees" at="1"/>

<!-- This deletes the currently selected element of the collection (assuming the repeat id 'employee-repeat') -->
<xf:delete ref="employees" at="index('employee-repeat')"/>

<!-- This deletes all elements of the collection -->
<xf:delete ref="employees"/>
```

_NOTE: Prior to XForms 1.1, the `at` attribute was mandatory. Since XForms 1.1, it is optional, and if omitted the `ref` attribute specifies all the nodes to remove._

## Inserting iterations with the insert action

_See also the [Insert a new item into a repeat][1] how-to guide._

`<xf:insert>` has a `ref` attribute pointing to the collection into which the insertion must take place. If no `origin` attribute is specified, `<xf:insert>` then considers the _last_ element of that collection (and all its content if any) as a _template_ for the new element to insert: it duplicates it and inserts it into the collection at a position you specify. In this case, the last element of a collection acts as a _template_ for insertions:

```xml
<!-- Insert a copy of the template before the last element of the collection -->
<xf:insert ref="employees" at="last()" position="before"/>

<!-- Insert a copy of the template after the last element of the collection -->
<xf:insert ref="employees" at="last()" position="after"/>

<!-- Insert a copy of the template before the first element of the collection -->
<xf:insert ref="employees" at="1" position="before"/>

<!-- Insert a copy of the template after the first element of the collection -->
<xf:insert ref="employees" at="1" position="after"/>
<xf:insert ref="employees" at="last()" position="after"/>

<!-- Insert a copy of the template before the currently selected element of the collection -->
<xf:insert ref="employees" at="index('employee-repeat')" position="before"/>

<!-- Insert a copy of the template after the currently selected element of the collection -->
<xf:insert ref="employees" at="index('employee-repeat')" position="after"/>
```

The `at` attribute contains an XPath expression returning the index of the element before or after which the insertion must be performed. The `position` element contains either `after` or `before`, and specifies whether the insertion is performed before or after the element specified by the `at` attribute.


As an extension to XForms 1.1, Orbeon Forms supports inserting an element into a document node:

```xml
<xf:insert context="instance()/root()" origin="instance('other')">
```

This is particularly useful in conjunction with the `xxf:create-document()` function.

## Using a trigger to execute actions

Insertions and deletions are typically performed when the user of the application presses a button, with the effect of adding a new repeated element before or after the currently selected element, or of deleting the currently selected element. You use an `xf:trigger` control and the XPath `index()` function for that purpose:

```xml
<xf:trigger>
    <xf:label>Add</xf:label>
    <xf:action ev:event="DOMActivate">
        <xf:insert ref="employees" at="index('employee-repeat')" position="after"/>
    </xf:action>
</xf:trigger>
```

or:

```xml
<xf:trigger>
    <xf:label>Delete</xf:label>
    <xf:action ev:event="DOMActivate">
        <xf:delete ref="employees" at="index('employee-repeat')"/>
    </xf:action>
</xf:trigger>
```

Note that we use `xf:action` as a container for `<xf:insert>` and `<xf:delete>`. Since there is only one action to execute, `xf:action` is not necessary, but it may increase the legibility of the code. It is also possible to write:

```xml
<xf:trigger>
    <xf:label>Add</xf:label>
    <xf:insert ev:event="DOMActivate" ref="employees" at="index('employee-repeat')" position="after"/>
</xf:trigger>
```

or:

```xml
<xf:trigger>
    <xf:label>Delete</xf:label>
    <xf:delete ev:event="DOMActivate" ref="employees" at="index('employee-repeat')"/>
</xf:trigger>
```

Notice in that case how `ev:event="DOMActivate"` has been moved from the enclosing `xf:action` to the `<xf:insert>` and `<xf:delete>` elements.

## Nested repeats

It is often desirable to nest repeat sections. Consider the following XForms instance representing a company containing departments, each containing a number of employees:

```xml
<xf:instance id="departments">
    <departments>
        <department>
            <name>Research and Development</name>
            <employees>
                <employee>
                    <first-name>John</first-name>
                </employee>
                <employee>
                    <first-name>Mary</first-name>
                </employee>
            </employees>
        </department>
        <department>
            <name>Support</name>
            <employees>
                <employee>
                    <first-name>Anne</first-name>
                </employee>
                <employee>
                    <first-name>Mark</first-name>
                </employee>
                <employee>
                    <first-name>Sophie</first-name>
                </employee>
            </employees>
        </department>
    </departments>
</xf:instance>
```

This document clearly contains two nested sections subject to repetition:

* **Departments:** a node-set containing all the `department` elements can be referred to with the following XPath expression: `instance('departments')/department`.
* **Employees:** a node-set containing all the `employee` elements can be referred to with the following XPath expression: `instance('departments')/department/employees/employee`. However, if the _context node_ of the XPath expression points to a particular `department` element, then the following _relative_ XPath expression refers to all the `employee` elements under that `department` element: `employees/employee`.

Following the example above, here is how departments and employees can be represented in nested tables with xf:

```xml
<xh:table>
    <xf:repeat ref="instance('departments')/department">
        <xh:tr>
            <xh:td>
                <xf:output ref="name"/>
            </xh:td>
            <xh:td>
                <xh:table>
                    <xf:repeat ref="employees/employee">
                        <xh:tr>
                            <xh:td>
                                <xf:output ref="first-name"/>
                            </xh:td>
                        </xh:tr>
                    </xf:repeat>
                </xh:table>
            </xh:td>
        </xh:tr>
    </xf:repeat>
</xh:table>
```

In the code above, the second `<xf:repeat>`'s `ref` expression is interpreted relatively to the `department` element of the parent `<xf:repeat>` for each iteration of the parent's repetition. During the first iteration of the parent, the "Research and Development" department is in scope, and `employees/employee` refers to the two employees of that department, John and Mary. During the second iteration of the parent, the "Support" department is in scope, and `employees/employee` refers to the three employees of that department, Anne, Mark and Sophie.

## Iterating over plain values

XForms 1.1 only specifies iterating over instance data nodes (elements or attributes). Orbeon Forms supports iterating over values, for example:

```xml
<xf:repeat ref="1 to 10">
    <xf:output value="position()"/>
    <xf:output value="."/>
</xf:repeat>
```

In this case, the context item within the repeat is a number, not a node.

_NOTE: This ability is also part of the XForms 2 specification._

## Evaluation of initial values upon insert

Orbeon Forms supports the `xxf:default` extension attribute on `xf:bind` to specify [dynamic initial values](../../xforms/binds.md#dynamic-initial-values).

By default (no pun intended), `xxf:default` does not apply to the newly inserted nodes. But by setting the `xxf:defaults` attribute (note the plural "defaults") on `xf:insert` to `true`, this behavior can be changed, and any `xxf:default` pointing to a newly-inserted node is re-evaluated during the next recalculation.

Consider the following example where the bind points to any `<value>` element child of the root element, and sets a dynamic initial value:

```xml
<xf:instance>
  <data>
      <value/>
  </data>
</xf:instance>

<xf:bind
    ref="instance()/value"
    xxf:default="count(//*)"/>
```

The data looks like this after initialization:

```xml
<data>
    <value>2</value>
</data>
```

Running the following insert adds a new `<value>` element after the first one:

```xml
<xf:insert
    ref="instance()/value"
    position="after"
    origin="xf:element('value')"
    xxf:defaults="true"/>
```

Because the action specifies `xxf:defaults="true"`, the first `value` element is unchanged, but the new `<value>` element gets its dynamic initial value set:

```xml
<data>
    <value>2</value>
    <value>3</value>
</data>
```

## See also

- [Dynamic initial values](../../xforms/binds.md#dynamic-initial-values)
- [Grid component](../../form-runner/components/grid.md)

[1]: http://wiki.orbeon.com/forms/how-to/logic/repeat-insert-position
