# Input control

## Basic Usage

```xml
<xf:input ref="text"/>
```

## Standard appearance

By default, the text area control is rendered as a regular single-line input field:

![](../images/xforms-input-simple.png)

_Examples of input fields_

### Attributes

With the standard appearance, the following attributes are available and forwarded to the corresponding HTML element:

- `xxf:maxlength`
- `xxf:pattern`
    - SINCE Orbeon Forms 2016.1
- `xxf:autocomplete`
- `xxf:size`
    - not recommended, use CSS instead
- `xxf:title`
    - SINCE Orbeon Forms 2016.1
    - can be useful for accessibility

Example:

```xml
<xf:input ref="text" xxf:autocomplete="off"/>
```

### Placeholder for label and hint

#### Per-control properties

The label or hint associated with `<xf:input>` may have the `minimal`  appearance:

```xml
<xf:input ref=".">
    <xf:label appearance="minimal">Your name</xf:label>
    <xf:hint>Hint</xf:hint>
</xf:input>

<xf:input ref=".">
    <xf:label>Your name</xf:label>
    <xf:hint appearance="minimal">Hint</xf:hint>
</xf:input>
```

This causes either the label or the hint to appear on the background of the field when it is empty. If both the label and hint have a `minimal` appearance, the label wins.

This is only supported for text, date, and time input fields.

Orbeon Forms leverages the HTML 5 `placeholder` attribute for browsers that support it (Firefox 3.5+, Chrome, Safari, Opera), and simulates the HTML 5 `placeholder` functionality  in JavaScript for browsers that don't support it (IE8 and IE9). In that case, you can customize how the placeholder is displayed by overriding the CSS class `xforms-placeholder`.

_NOTE: The `xxf:placeholder` appearance is deprecated. It has the same effect as the `minimal` appearance. The latter is standardized in XForms 2.0._

#### Per-form properties

[SINCE Orbeon Forms 2016.2]

The XForms `oxf.xforms.label.appearance` or `oxf.xforms.hint.appearance` (or the corresponding `xxf:label.appearance` and `xxf:hint.appearance` attributes on the first `<xf:model>` element) allow setting a default for the labels and hint appearances for the entire form.

The default value is `full`:

```xml
<property
    as="xs:string"  
    name="oxf.xforms.label.appearance"                            
    value="full"/>
    
<property
    as="xs:string"  
    name="oxf.xforms.hint.appearance"                             
    value="full"/>
```

Supported values for `oxf.xforms.label.appearance`:

- `full`: labels show inline above the control (the default)
- `full minimal`: labels show inline above the control, but for text, date, and time input fields only, labels show as an HTML *placeholder* within the field when the field is empty

Supported values for `oxf.xforms.hint.appearance`:

- `full`: hints show inline below the control (the default)
- `full minimal`: hints show inline below the control, but for text, date, and time input fields only, hints show as an HTML *placeholder* within the field when the field is empty
- `tooltip`: hints show as tooltips upon mouseover
- `tooltip minimal`: hints show as tooltips upon mouseover, but for text, date, and time input fields only, hints show as an HTML *placeholder* within the field when the field is empty

When the global property includes `minimal`, it is possible to override the appearance on the control with `appearance="full"`:

```xml
<xf:input ref=".">
    <xf:label appearance="full">Your name</xf:label>
    <xf:hint>Hint</xf:hint>
</xf:input>

<xf:input ref=".">
    <xf:label>Your name</xf:label>
    <xf:hint appearance="full">Hint</xf:hint>
</xf:input>
```

## Appearance based on type and appearance

The way the XForms input control is rendered on the page depends on the type of the node it is bound to, and possibly the control's appearance:

| Type          | Appearance          | Description                                                                       |
|---------------|---------------------|-----------------------------------------------------------------------------------|
| `xs:string`   |                     | standard input field                                                              |
| `xs:string`   | `character-counter` | input field with [character counter](/form-runner/component/character-counter.md) |
| `xs:boolean`  |                     | single checkbox                                                                   |
| `xs:date`     |                     | input field with date picker and date parsing and formatting                      |
| `xs:date`     | `dropdowns`         | maps to `fr:dropdown-date`                                                        |
| `xs:date`     | `fields`            | maps to `fr:fields-date`                                                          |
| `xs:date`     | `minimal`           | icon with date picker without an input field                                      |
| `xs:time`     |                     | input field with time parsing and formatting                                      |
| `xs:dateTime` |                     | combined date and time fields                                                     |

![Boolean input](../images/xforms-input-boolean.png)

*NOTE: The Boolean input is deprecated. Use the `fr:checkbox-input` component instead.*

![Date picker](../images/xforms-datepicker-simple.png)

![Date and time input variations](../images/xforms-input-dates-times.png)

## date, time, and dateTime types

### Date picker configuration

When using the date picker, you can choose whether you want 2 months to be displayed instead of one, and whether users should be able to quickly navigate to a specific year and month by setting the [`oxf.xforms.datepicker.navigator`][3] property.

*NOTE: Make sure not to use the date picker inside an `<xh:p>`.*

### Smart date and time format

The date and time controls allow you to type a date and a time in a number of formats, as listed below. When the field loses the focus, the value you entered is parsed and, if recognized, stored and then formatted according to a configurable format.

For times, see [Time component](/form-runner/component/time.md).

[//]: # (For dates, see [Date component]&#40;/form-runner/component/date.md&#41;.)

Date formats:

| Example                                                    | Note                                  |
|------------------------------------------------------------|---------------------------------------|
| today                                                      |                                       |
| tomorrow                                                   |                                       |
| yesterday                                                  |                                       |
| 4th                                                        | The 4th of the current year and month |
| 4th Jan                                                    |                                       |
| 4th Jan 2003                                               |                                       |
| Jan 4th                                                    |                                       |
| Jan 4th 2003                                               |                                       |
| 10/20/2000 ("US format") or 20/10/2000 ("European format") |                                       |
| 10/20 ("US format") or 20/10 ("European format")           |                                       |
| 10202000 ("US format") or 20102000 ("European format")     | [SINCE Orbeon Forms 2017.2]           |
| 2000-10-20                                                 | ISO or "Asian" format                 |

In the table above, the "US format" applies the `oxf.xforms.format.input.date`  property starts with `[M`, and the "European format" when that property starts with `[D`.

### Two digits years

If you type in a date field a year with only two digits (say 5/20/10), the control will assume that you intended to type a year in the twentieth or twenty-first century, rather than a year in the first century. It will convert the two-digit year you typed into a four digits year by taking the corresponding year in either the twentieth or twenty first century based on which one is closest to the current year. So for instance, if the current year is 2020:

* 10 is changed to 2010
* 80 is changed to 1980
* 60 is changed to 2060


### Date picker internationalization

By default, the months and days of the week are in English in the date picker (as shown in the screenshot above). You can  change this by setting the value of the lang attribute on the  element of the page. The value of the attribute two-letter [ISO 639-1 language code](https://en.wikipedia.org/wiki/List_of_ISO_639-1_codes). For instance with  months and weekdays will be shown in French, for instance:

![Localized date picker](../images/xforms-datepicker-french.png)

For more on which languages are supported localized out-of-the-box, see [supported languages](/form-runner/feature/localization.md). Note that the changing the language also changes which day of the week is shown first in the calendar: in English, Sunday is shown first; with French and Spanish, Monday is shown first.

### Date picker in scrollable area

If you are using the date picker in an area of your page which is scrollable (e.g. `<div style="overflow: scroll">`), if users scroll in that area while the date picker is open, you want the date picker to be anchored to the field it is related to and to scroll with the content of the scrollable area (versus being attached to the page, and only scrolling if the page is scrolled). For this to work, assuming you have the class `scrollable-area` on your scrollable area, you need to add the following CSS:

```css
.scrollable-area { position: relative }
```

### On iOS

On iOS (iPhone, iPad, iPod touch), inputs bound to nodes of type `xs:date`, `xs:time`, or `xs:dateTime` are rendered using the iOS 5 browser native date or time widgets, which iOS users are accustomed to, and which provides a better usability, especially on the smaller screen iPhone and iPod touch.

![iOS date picker](../images/xforms-ios-date.png)

![iOS time picker](../images/xforms-ios-time.png)

### Limitations

* If the time has a milliseconds part:
    * The time will be shown without the milliseconds part.
    * If users modify the time, the milliseconds part will be lost.
* If the time has a timezone specification:
    * The date will be shown as if the time zone indication was not there.
    * If users modify the time the time zone will be lost.

## Text input sanitization

[SINCE Orbeon Forms 4.0.1]

Input sanitization allows you to apply a filter on the data entered by the user, before the data is stored into instance data. One use of sanitization is to replace undesired characters. For example, the following will replace sign and double rounded quotes with straight quotes, several long dashes with regular dashes and ellipsis character with three dots:

```xml
<property as="xs:string" name="oxf.xforms.sanitize">
    {
        "&#x2018;": "&#039;",
        "&#x2019;": "&#039;",
        "&#x201c;": "\"",
        "&#x201d;": "\"",
        "&#x2013;": "&#045;",
        "&#x2014;": "&#045;",
        "&#x2219;": "&#045;",
        "&#x2022;": "&#045;",
        "&#x00BF;": "&#063;",
        "&#x2026;": "..."
    }
</property>
```

The configuration is a JSON map of string to string. The algorithm is as follows:

- If the configuration is blank string, sanitization is turned off.
- If not blank, then the JSON configuration is parsed and sanitization is turned on.
- Each mapping contains a search string on the left, and a replacement string on the right.
- For each mapping, all instances of the search string in the input data are replaced with the replacement string.
- Each replacement is applied in the order in which it appears in the JSON map.

Although this would be probably useless, and is definitely not recommended, the following would replace all instances of "Hello!" with "Hi!":

```xml
<property as="xs:string" name="oxf.xforms.sanitize">
    {
        "Hello!": "Hi!"
    }
</property>
```

sanitization applies only to:

- input fields bound to string data
- text areas

You can also use the `xxf:sanitize` attribute on the XForms model to set a filter local to a given page.

## See also

- Blog post: [Use HTML5 placeholders, in XForms](https://blog.orbeon.com/2012/01/use-html5-placeholders-in-xforms.html)

[3]: /configuration/properties/xforms.md#navigator
