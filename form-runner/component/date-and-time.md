# Date and Time

## What it does

The Date and Time component (`fr:datetime`) allows users to enter both a date and a time in a single form control.

On the desktop and mobile:
* In edit mode (readwrite), the component displays two separate fields side-by-side: a [Date](date.md) field with a date picker, and a [Time](time.md) field.
* In readonly modes (such as view mode and PDF export), the component displays the combined formatted date and time as read-only text.

The value is stored in the form data in ISO 8601 format (`YYYY-MM-DDTHH:mm:ss`).

## Basic usage

From Form Builder, insert the **Date and Time** control from the **Date and Time** section of the toolbox.

In XForms, you use the component as follows:

```xml
<fr:datetime ref="departure-time">
    <xf:label>Departure Date and Time</xf:label>
</fr:datetime>
```

## Datatype

`fr:datetime` must be bound to a node of type `xs:dateTime`.

## Configuration

### Field width

The `field-width` parameter controls the width of the fields:

* `natural`: fields take a width defined by each component
* `full`: fields expand to take the available width of the grid cell

This parameter can be configured in Form Builder via the Control Settings dialog, or via property:

```xml
<property
    as="xs:string"
    name="oxf.xforms.xbl.fr.datetime.field-width"
    value="{if (fr:created-with-or-newer('2018.2')) then 'full' else 'natural'}"/>
```

### Formats in edit mode

In edit mode, the Date and Time control delegates date and time input to the embedded Date and Time sub-controls. Each sub-control follows its own configuration:

* The date field uses the date format configured for the [Date](date.md#output-format) control (via `oxf.xforms.xbl.fr.date.output-format` or `oxf.xforms.format.input.date`).
* The time field uses the time format configured for the [Time](time.md#output-format) control (via `oxf.xforms.xbl.fr.time.output-format` or `oxf.xforms.format.input.time`).

### Formats in readonly modes

[SINCE Orbeon Forms 2026.1 and 2025.1.3]

In readonly modes (such as Form Runner view mode and PDF export), the Date and Time control formats the value as follows:

1. **`output-format` parameter:** If the `output-format` parameter is specified on `fr:datetime`, that format is used. You can configure this globally or per app/form via property:

    ```xml
    <property
        as="xs:string"
        name="oxf.xforms.xbl.fr.datetime.output-format"
        value="[D01]/[M01]/[Y] [H01]:[m]:[s]"/>
    ```

    Or at the control level:

    ```xml
    <fr:datetime ref="departure-time" output-format="[D01]/[M01]/[Y] [H01]:[m]:[s]">
        <xf:label>Departure Date and Time</xf:label>
    </fr:datetime>
    ```

2. **Combined Date and Time formats:** If `output-format` is not specified on `fr:datetime`, the control dynamically combines the configured date format and time format, separated by a space:

    * Date format: taken from `oxf.xforms.xbl.fr.date.output-format` (falling back to `oxf.xforms.format.input.date`).
    * Time format: taken from `oxf.xforms.xbl.fr.time.output-format` (falling back to `oxf.xforms.format.input.time`).

This ensures that the format displayed in view mode and PDF matches the formats shown in edit mode.

For supported picture string tokens for dates and times, see [Date output format](date.md#output-format) and [Time output format](time.md#output-format).

## Compatibility notes

### Format in readonly modes

[SINCE Orbeon Forms 2026.1 and 2025.1.3]

In earlier versions of Orbeon Forms, the Date and Time control in readonly modes did not use the date and time components' output formats. Instead, it formatted its value using the global `oxf.xforms.format.output.dateTime` property, which defaults to a long format (such as `Wednesday January 7, 2004 04:38:35 UTC`). As a result, the value in view mode often appeared in a completely different format than the one seen in edit mode.

Starting with Orbeon Forms 2026.1 and 2025.1.3:

* The Date and Time control in readonly modes no longer uses the `oxf.xforms.format.output.dateTime` property.
* It now uses the combined date and time formats or the `output-format` parameter as described above.

> [!WARNING]
> **Backward Incompatibility**:
> If you previously customized `oxf.xforms.format.output.dateTime` to format the Date and Time control in view mode or PDF, this property will no longer affect `fr:datetime`. To configure the readonly format:
> * Configure the date and time formats on the Date and Time controls using `oxf.xforms.xbl.fr.date.output-format` and `oxf.xforms.xbl.fr.time.output-format` (or `oxf.xforms.format.input.date` and `oxf.xforms.format.input.time`).
> * Or configure `output-format` on `fr:datetime` directly or via `oxf.xforms.xbl.fr.datetime.output-format`.

## See also

* [Date](date.md)
* [Time](time.md)
* [Timestamp](timestamp.md)
* [Dropdown Date](dropdown-date.md)
* [Date and Time Format (Data Format)](../data-format/date-and-time.md)
