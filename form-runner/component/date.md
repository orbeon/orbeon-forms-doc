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

## Configuration

`fr:date` supports parameters, which you can set via properties, Form Builder settings, or directly on `fr:date`.

### Week start day

[SINCE Orbeon Forms 2022.1]

The `week-start-day` parameter allows you to specify the first day of the week:

- values: `sunday` or `monday`
- default: when not specified, the start day depends on the language (for example Sunday for English, Monday for French)

This is unspecified by default:

```xml
<property 
    as="xs:string" 
    name="oxf.xforms.xbl.fr.date.week-start-day.*.*" 
    value=""/>
```

### Output format

[SINCE Orbeon Forms 2023.1.4]

The Date component adds an `output-format` parameter. This parameter can be used to override the global property at the control level, the form level, or via `properties-local.xml` with the following new property:

```xml
<property
    as="xs:string"
    name="oxf.xforms.xbl.fr.date.output-format"
    value=""/>
```

As usual, the property can use an app name and form name (with possible wildcards) to specify a default value for all controls in a given app/form:

```xml
<property
    as="xs:string"
    name="oxf.xforms.xbl.fr.date.output-format.acme.*"
    value="[H01]:[m]"/>
```

The value is in the same format as the global `oxf.xforms.format.input.date` property.

By default, the `output-format` parameter is not set, and the global `oxf.xforms.format.input.date` property is used for backward compatibility.

### Native date picker

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

### Today's highlight

[UNTIL Orbeon Forms 2024.1], today's date was not highlighted in the datepicker. [\[SINCE Orbeon Forms 2024.1\]](/release-notes/orbeon-forms-2024.1.md), today's date is highlighted by default. You can disable this behavior by setting the following property.

```xml
<property 
    as="xs:boolean"  
    name="oxf.xforms.xbl.fr.date.today-highlight.*.*"             
    value="false"/>
```

## See also

- [Dropdown Date](dropdown-date.md)
- [Dates to Exclude constraint in Form Builder](/form-builder/validation.md#dates-to-exclude-constraint)