# Number Component

<!-- toc -->

## Availability

Orbeon Forms 4.0.

## What it does

The number component is designed to enter integer or decimal numbers. See [Currency](../../form-runner/component/currency.md) for a similar component designed to enter currency amounts.

![](images/xbl-number.png)

## Basic usage

You use the number component like a regular input field, for example:

```xml
<fr:number ref="my-number">
  <xf:label>Quantity</xf:label>
</fr:number>
```

## Parameters

`fr:number` supports parameters, which you can set via properties or directly on fr:number:

* `prefix`: optional prefix shown before the number
* `suffix`: optional suffix shown after the number
* `digits-after-decimal`: digits to show after the decimal point (by default 0, which means the number is an integer)
* `decimal-separator`: single character to use as decimal separator
* `grouping-separator`: single character to use as thousands separator separator (can be blank)

These are the default values of the properties:

```xml
<property
    as="xs:string"
    name="oxf.xforms.xbl.fr.number.prefix"
    value=""/>
<property
    as="xs:string"
    name="oxf.xforms.xbl.fr.number.suffix"
    value=""/>
<property
    as="xs:string"
    name="oxf.xforms.xbl.fr.number.digits-after-decimal"
    value="0"/>
<property
    as="xs:string"
    name="oxf.xforms.xbl.fr.number.decimal-separator"
    value="."/>
<property
    as="xs:string"
    name="oxf.xforms.xbl.fr.number.grouping-separator"
    value=","/>
```

Here is an example with a suffix:

![](images/xbl-number.png)

```xml
<fr:number ref="my-number" suffix="m/s">
    <xf:label>Number</xf:label>
    <xf:hint>Number field with validation</xf:hint>
</fr:number>
```
