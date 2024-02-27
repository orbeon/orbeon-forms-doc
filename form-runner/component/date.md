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

On iOS, the native iOS date picker is used by default, unless users enabled Request Desktop Website in Safari, in which case they will see the same behavior as they would on the desktop. 

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md) As browser support for the native date picker on desktop has improved over the year, and since it is better supported by screen readers, you may want to use the native date picker not only on mobile but also on desktop. You can achieve this by setting the property mentioned below. When doing so, the date format is determined by the browser based on the user's locale. Consequently, the properties `oxf.xforms.format.input.date` and `oxf.xforms.format.output.date` have no effect on the format of the native date picker.

```xml
<property 
    as="xs:string"  
    name="oxf.xforms.xbl.fr.date.native-picker.*.*"             
    value="always"/>
```

<figure>
    <img src="/form-runner/images/native-date-picker-chrome.png" width="270">
    <figcaption>Native date picker on Chrome desktop</figcaption>
</figure>

## See also

- [Dropdown Date](dropdown-date.md)
- [Dates to Exclude constraint in Form Builder](/form-builder/validation.md#dates-to-exclude-constraint)