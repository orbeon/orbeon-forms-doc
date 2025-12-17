# Expression analysis

## Availability

This is an Orbeon Forms PE feature.

## Rationale

Whenever a user makes a change to a form, for example by entering a new value in a form field, the XForms engine must process that change, including:

* storing the user data into an XML document (XForms instance)
* re-evaluate validity constraints and other properties on the data
* run recalculations
* update the controls that bind to the data so that they reflect the latest data

Often, this results in a very large number of XPath evaluations, which can be costly.

The XPath analysis feature of Orbeon Forms PE allows the XForms engine, when loading a page, to go through XPath expressions to analyze them and understand dependencies between XML data, constraints, and controls.

This enables the XForms engine to process user interactions faster.

## Enabling XPath analysis

_NOTE: At the XForms level, this feature is disabled by default, but it is enabled for forms created by Form Builder by default._

You enable XPath analysis with the following property:

```markup
<property 
    as="xs:boolean" 
    name="oxf.xforms.xpath-analysis" 
    value="true"/>
```

You can also turn on this property specifically for a given form by adding `xxf:xpath-analysis="true"`, on the first model:

```markup
<xf:model xxf:xpath-analysis="true">
```

## How XPath analysis works

Consider this instance in a `people-model` model:

```markup
<xf:instance id="people">
  <people>
    <person>
      <name>Mary</name>
      <age>20</age>
    </person
    <person>
      <name>Bob</name>
      <age>27</age>
    </person
  </people>
</xf:instance>
```

And consider a group at the top-level:

```markup
<xf:group id="my-group" ref="person[age ge 21]">
```

The binding with `ref` is relative to the root element of the `instance('people')` instance. The expression person `[age ge 21]` is relative to that root element. It is analyzed as if the form author had written directly:

```markup
instance('people')/`person[age ge 21]
```

Now by analyzing this expression, the XForms engine can know that the expression:

* depends on the _value_ of an element called `<age>` child of an element `<person>` child of `instance('people')`
* returns an element `<person>` child of `instance('people')`
* only depends on the people instance in the `people-model` model

The XForms engine then uses this information to determine when the ref binding on the group needs to be re-evaluated.

So what can cause the binding for my-group to require an update?

1.  A **structural change** to the instance, such as a new `<person>` element replacing the current `<person>` element.

    Currently, any structural change in a model invalidates all bindings and values touching that model, and it is as if XPath analysis had been turned off. (This can be improved in the future.)
2.  A **change to the value** of any element matching `instance('people')/person/age`.

    If the _value_ of such an  element changes between two refreshes, then the binding needs to be reevaluated.
3.  Otherwise, no evaluation is needed!

    For example, if the _value_ of `instance('people')/person/name` changes, the binding need not change.

So the XForms engine can simply follow this algorithm:

* Was there a structural change since the last refresh? If so, re-evaluate the binding.
* Was there a change to the value of any `<age>` element matching `instance('people')/person/age`? If so, re-evaluate the binding.
* Otherwise, don't re-evaluate the binding.

The same idea applies to:

* control bindings
* control values
* itemsets
* values of label, help, hint, and help elements
* `<xf:bind>` bindings
* values of model item properties

## XPath expressions supported

Not all XPath expressions are currently analyzed fully. If an XPath expression is not analyzable, it will simply be re-evaluated whenever needed.

These expression are not analyzed:

Expression containing the following functions:

* `index()` / `xxf:index()`&#x20;
* `xxf:case()`
* all functions depending on external data, like the request, session, `or xxf:call-xpl()`
* Java functions

For debugging purposes, you can log the result of the XPath analysis to the servlet container's standard output by enabling this property:

```markup
<property 
    as="xs:boolean" 
    name="oxf.xforms.debug.log-xpath-analysis" 
    value="true">
```

This lists all the expressions considered, mark whether they were analyzed or not, and list all aspects of the analysis.
