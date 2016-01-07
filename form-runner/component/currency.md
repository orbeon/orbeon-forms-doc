# Currency Component

<!-- toc -->

## What it does

The currency component is an input field specialized to capture amounts in a particular currency.

![](images/xbl-currency6.png)

When the input field doesn't have the focus, it shows a formatted currency, such as `1,234.00`. When the control has the focus, it shows the plain number, such as `1234` to facilitate input.

While working with currency amounts, the component is careful not to do any manipulation that relies on how numbers are represented on a particular platform, to avoid any possibility of rounding or precision error. [SINCE: 2011-03-07]

## Basic usage

You use the number component like a regular input field, for example:

```xml
<fr:currency ref="my-amount">
  <xf:label>Amount</xf:label>
</fr:currency>
```

## Parameters

`fr:currency` supports parameters, which you can set via properties or directly on `fr:currency`:

* `prefix`: optional prefix shown before the number ( by default the dollar sign)
* `suffix`: optional suffix shown after the number
* `digits-after-decimal`: digits to show after the decimal point (by default 2)
* `decimal-separator`: single character to use as decimal separator
* `grouping-separator`: single character to use as thousands separator separator (can be blank)
* `round-when-formatting`: when formatting the number for display, whether to round the value to `digits-after-decimal` if there are more digits after the decimal point or not. The default is `false`.
    * SINCE Orbeon Forms 4.11
    * Rounding uses the [half to even](https://en.wikipedia.org/wiki/Rounding#Round_half_to_even) method.
* `round-when-storing`: when storing the number entered by the user, whether to round the value to `digits-after-decimal` if there are more digits after the decimal point or not. The default is `false`.
    * SINCE Orbeon Forms 4.11
    * Rounding uses the [half to even](https://en.wikipedia.org/wiki/Rounding#Round_half_to_even) method.

These are the default values of the properties:

```xml
<property
    as="xs:string"
    name="oxf.xforms.xbl.fr.currency.prefix"
    value="$"/>
<property
    as="xs:string"
    name="oxf.xforms.xbl.fr.currency.suffix"
    value=""/>
<property
    as="xs:string"
    name="oxf.xforms.xbl.fr.currency.digits-after-decimal"
    value="2"/>
<property
    as="xs:string"
    name="oxf.xforms.xbl.fr.currency.decimal-separator"
    value="."/>
<property
    as="xs:string"
    name="oxf.xforms.xbl.fr.currency.grouping-separator"
    value=","/>
<property 
    as="xs:boolean" 
    name="oxf.xforms.xbl.fr.currency.round-when-formatting"         
    value="false"/>
<property 
    as="xs:boolean" 
    name="oxf.xforms.xbl.fr.currency.round-when-storing"            
    value="false"/>
```

### Currency prefix

By default the dollar sign ("$") is used as a currency prefix. You can override this either:

* Globally for all your forms in your ‘properties-local.xml’ by setting the following property:
```xml
<property
    as="xs:string"
    name="oxf.xforms.xbl.fr.currency.prefix"
    value="£"/>
```
* For a particular instance of the component:
    * With the `prefix` attribute, for static values.
    * With the `<fr:prefix>` element, for dynamic values.

### Digits after the decimal sign

By default, the component shows the value to 2 digits after the decimal sign. You can override this either:

* Globally for all your forms in your `properties-local.xml` by setting the following property:
```xml
<property
    as="xs:string"
    name="oxf.xforms.xbl.fr.currency.digits-after-decimal"
    value="3"/>
```
* For a particular instance of the component:
    * With the `digits-after-decimal` attribute, for static values.
    * With the `<fr:digits-after-decimal>` element, for dynamic values.

If you set `digits-after-decimal` to 0, then the decimal separator isn't shown. The component truncates any additional digits beyond the specified number; it doesn't perform rounding.

## Examples

### Control without focus

```xml
<fr:currency ref="value"/>
```

![](images/xbl-currency1.png)

### Setting a static prefix with the prefix attribute

```xml
<fr:currency ref="value" prefix="£"/>
```

![](images/xbl-currency2.png)

### Setting a dynamic prefix with the fr:prefix element

```xml
<fr:currency ref="value">
    <fr:prefix ref="/config/prefix"/>
</fr:currency>
```

### Showing 3 digits after decimal sign

```xml
<fr:currency
    ref="value"
    digits-after-decimal="3/>
```

![](images/xbl-currency2.png)

### Showing 0 digits after the decimal sign

```xml
<fr:currency
    ref="value"
    digits-after-decimal="0/>
```

![](images/xbl-currency3.png)

### Read-only input field, because bound to node set as read-only with a MIP

```xml
<xforms:bind ref="readonly-node" readonly="true()"/>

<fr:currency ref="readonly-node"/>
```

![](images/xbl-currency4.png)
