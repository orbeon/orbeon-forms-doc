# Date Component

## What it does

The date component is designed to enter a date, without time. On the desktop, the component shows as a field with a linked date picker.

## Basic usage

You use the date component like a regular input field, for example:

```xml
<fr:date ref="birth-date">
  <xf:label>Birth Date</xf:label>
</fr:date>
```

## Datatype

`fr:date` must be bound to `xs:date`.

## Parameters

[SINCE Orbeon Forms 2022.1]

`fr:date` supports parameters, which you can set via properties, Form Builder settings, or directly on `fr:date`:

- `week-start-day`
    - first day of the week
    - values: `sunday` or `monday`
    - default: when not specified, the start day depends on the language (for example Sunday for English, Monday for French)

These are the default values of the properties:

```xml
<property 
    as="xs:string" 
    name="oxf.xforms.xbl.fr.date.week-start-day.*.*" 
    value=""/>
```

## Native date picker

On iOS, the native iOS date picker will be used unless you've selected Request Desktop Website in Safari. [SINCE Orbeon Forms 2023.1] As browser support for the native date picker on the desktop has improved over the year, and the native date picker is better supported by screen readers, you may want to always use the native date picker. You can do this by setting the following property: 

```xml
<property 
    as="xs:string"  
    name="oxf.xforms.xbl.fr.date.native-picker.*.*"             
    value="always"/>
```

## See also

- [Dropdown Date](dropdown-date.md)