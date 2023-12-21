# Time window

[SINCE Orbeon Forms 2023.1]
[Orbeon Forms PE only]

<figure>
    <img src="time-window.png" width="1022">
    <figcaption>Time Window tab of the Form Settings dialog</figcaption>
</figure>

For some forms, you might only want to accept new entries after a given start date or before a given end date. You can define such constraints by opening your form in Form Builder, opening the Form Settings dialog, and switching to the Time Window tab, which is also shown in the above screenshot. This allows you to define an optional start date and end date. If users try to fill out a new form before or after the specified dates, they will be shown a default message, which you can also override in the same tab.

## Properties

If you prefer to define a time window for your forms using properties, you can do so using the property names shown below. The properties values, if provided, must be in the `xs:dateTime` format, as in `2024-01-01T00:00:00` for the beginning of January 1, 2024. By default, your form will be using the properties, which you can override through the UI discussed above.

```xml
<property as="xs:string"  name="oxf.fr.detail.available-from.dateTime.*.*" value=""/>
<property as="xs:string"  name="oxf.fr.detail.available-to.dateTime.*.*"   value=""/>
```

## Applies to new forms

The time window restriction you set applies only to new forms. For example, if a user has access to specific form data within the time window, they will retain access even after the time window has closed. To prevent users from modifying the data after the time window ends, use the read-only feature under the Formulas tab. Implement a formula similar to `current-date() >= xs:date('2024-01-01')`.