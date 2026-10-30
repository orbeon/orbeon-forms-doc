# Phone Number

## Availability

[\[SINCE Orbeon Forms 2026.1\]](../../release-notes/orbeon-forms-2026.1.md)

## Overview

The `fr:phone` component allows users to enter and validate national and international phone numbers. It combines a searchable country calling-code selector with a national phone number input field.

Key features:

* **Standard E.164 storage**: Valid phone numbers are normalized and stored in the canonical [ITU-T E.164](https://en.wikipedia.org/wiki/E.164) format (such as `+16502530000`, `+41446681800`, or `+442079460000`). If the user enters an invalid phone number, the raw text is preserved in the bound node so that validation (`type="fr:phone"`) marks the control as invalid without losing the user's input.
* **International validation and formatting**: Powered by Google's [`libphonenumber`](https://github.com/google/libphonenumber) both online (JVM) and in offline (Scala.js) mode:
  * Validates phone numbers against the selected country's numbering rules.
  * Formats valid phone numbers in the country's national format while editing (for example, `(650) 253-0000` for `US`, `044 668 18 00` for `CH`, `020 7946 0000` for `GB`).
  * Formats phone numbers in international format in read-only, view, and PDF modes (for example, `+1 650-253-0000`, `+41 44 668 18 00`).
* **Country calling-code selector**: Displays a compact badge showing the selected country's flag, optional ISO code, and calling code (e.g. `🇨🇭 +41`), and opens a searchable dropdown listing countries in the active Form Runner language along with their calling codes. Supports searching by:
  * Country name (diacritic-insensitive, e.g. `cote` matches `Côte d’Ivoire`).
  * 2-letter and 3-letter ISO codes (e.g. `CH`/`CHE`, `GB`/`GBR`) and common aliases or endonyms (e.g. `UK`, `USA`, `Suisse`, `Schweiz`, `Deutschland`).
  * Country calling code (e.g. `+41` or `41`).
* **Automatic country detection**: When a user types or pastes a phone number in international format starting with `+` or `00` (such as `+33 1 42 68 55 00` or `0041446681800`), the component automatically updates the country selector to the corresponding country and formats the national number accordingly.
* **Dynamic national example placeholder**: Displays a country-specific example national phone number as the input placeholder (e.g. `(201) 555-0123` for `US`, `021 234 56 78` for `CH`) when no explicit placeholder is configured on the control.
* **Browser and mobile support**: Uses `type="tel"` to trigger the telephone numeric keypad on mobile devices and sets `autocomplete="tel-national"` so browsers can autofill the phone number.

## XForms usage

You use `fr:phone` like a regular XForms control, typically bound to a node with `type="fr:phone"` for validation:

```xml
<xf:bind ref="phone-number" type="fr:phone"/>
...
<fr:phone ref="phone-number">
    <xf:label>Phone number</xf:label>
</fr:phone>
```

## Parameters and Form Builder settings

You can configure `fr:phone` in Form Builder's **Control Settings** dialog, via attributes directly on `<fr:phone>`, or via configuration properties:

| Parameter             | Type         | Default | Description                                                                                                                                                                                                                                         |
|-----------------------|--------------|---------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `default-country`     | `xs:string`  | `""`    | Default 2-letter ISO 3166-1 alpha-2 country code selected when the field is empty (e.g. `US`, `CH`, `GB`). When empty, falls back to the first country in `preferred-countries` if specified, or to the country matching the current Form Runner language. |
| `preferred-countries` | `xs:string`  | `""`    | Space- or comma-separated list of 2-letter ISO codes to pin at the top of the list in the specified order (e.g. `US CA GB`). Preferred countries appear once at the top above a divider line and are not duplicated in the alphabetical list below. |
| `include-countries`   | `xs:string`  | `""`    | Space- or comma-separated list of 2-letter ISO codes. When non-empty, only the specified countries are available in the selector.                                                                                                                   |
| `exclude-countries`   | `xs:string`  | `""`    | Space- or comma-separated list of 2-letter ISO codes to exclude from the selector.                                                                                                                                                                  |
| `show-code`           | `xs:boolean` | `false` | When `true`, includes the 2-letter ISO code in the country dropdown items (e.g. `🇨🇭 Switzerland (CH) +41`) and in the closed selector badge (e.g. `🇨🇭 CH +41`).                                                                                  |
| `show-flag`           | `xs:boolean` | `true`  | When `true`, prepends the country's Unicode flag emoji to each country item and to the closed selector badge (e.g. `🇨🇭 +41`).                                                                                                                      |

### Examples

Setting a default country and pinning preferred countries at the top:

```xml
<fr:phone
    ref="phone"
    default-country="CH"
    preferred-countries="CH FR DE IT">
    <xf:label>Phone number</xf:label>
</fr:phone>
```

Restricting the selector to specific countries and showing the 2-letter ISO code:

```xml
<fr:phone
    ref="contact-phone"
    default-country="US"
    include-countries="US CA MX"
    show-code="true">
    <xf:label>Contact phone</xf:label>
</fr:phone>
```

## Configuration properties

The default global properties are:

```xml
<property as="xs:string"  name="oxf.xforms.xbl.fr.phone.default-country"     value=""/>
<property as="xs:string"  name="oxf.xforms.xbl.fr.phone.preferred-countries" value=""/>
<property as="xs:string"  name="oxf.xforms.xbl.fr.phone.include-countries"   value=""/>
<property as="xs:string"  name="oxf.xforms.xbl.fr.phone.exclude-countries"   value=""/>
<property as="xs:boolean" name="oxf.xforms.xbl.fr.phone.show-code"           value="false"/>
<property as="xs:boolean" name="oxf.xforms.xbl.fr.phone.show-flag"           value="true"/>
```

You can override any of these properties globally or per application/form in `properties-local.xml`, for example:

```xml
<property
    as="xs:string"
    name="oxf.xforms.xbl.fr.phone.default-country.*.*"
    value="CH"/>
<property
    as="xs:string"
    name="oxf.xforms.xbl.fr.phone.preferred-countries.*.*"
    value="CH FR DE IT"/>
```

## See also

* [Country](country.md)
* [US Phone Number](us-phone.md)
* [Control Settings](../../form-builder/control-settings.md)
