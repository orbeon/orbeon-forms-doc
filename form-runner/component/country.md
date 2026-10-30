# Country

## Availability

[\[SINCE Orbeon Forms 2026.1\]](../../release-notes/orbeon-forms-2026.1.md)

## Overview

The `fr:country` component allows users to select a country or territory from the 249 official [ISO 3166-1 alpha-2](https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2) countries and territories.

Key features:

* **Standard ISO codes**: Stores the 2-letter ISO 3166-1 alpha-2 code (such as `US`, `CH`, `GB`, `FR`, `JP`) in the form data.
* **Localized names and collation**: Displays country names in the active Form Runner language, sorted according to that language's dictionary collation rules (for example, `Égypte` sorts under `E` in French and `Ägypten` sorts under `A` in German). Works both online and in offline (Scala.js) mode.
* **Enhanced search**: Uses a searchable dropdown by default on desktop and mobile, supporting:
  * Diacritic-insensitive matching (e.g. typing `cote` matches `Côte d’Ivoire`, `osterreich` matches `Österreich`).
  * Matching by 2-letter and 3-letter ISO codes (e.g. `CH`/`CHE`, `DE`/`DEU`, `GB`/`GBR`) and common aliases or endonyms (e.g. `UK`, `Great Britain`, `USA`, `UAE`, `Holland`, `Suisse`, `Schweiz`, `Deutschland`).
  * Relevance ranking that prioritizes exact code/alias matches and prefix matches ahead of substring matches.
* **Browser autofill**: Includes `autocomplete="country"` by default (configurable in Form Builder's Control Settings dialog) and unencrypted ISO option values so browsers can automatically fill the country from saved address profiles.
* **Mobile-friendly**: Prevents iOS Safari auto-zoom on search input focus, disables autocorrect/spellcheck in the search box, provides touch-friendly option targets, and scrolls the control into view above the virtual keyboard when opened on touch devices.

## XForms usage

You use `fr:country` like a regular XForms control:

```xml
<fr:country ref="residence-country">
    <xf:label>Country of residence</xf:label>
</fr:country>
```

## Parameters and Form Builder settings

You can configure `fr:country` in Form Builder's **Control Settings** dialog, via attributes directly on `<fr:country>`, or via configuration properties:

| Parameter             | Type         | Default  | Description                                                                                                                                                                                                                                         |
|-----------------------|--------------|----------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `preferred-countries` | `xs:string`  | `""`     | Space- or comma-separated list of 2-letter ISO codes to pin at the top of the list in the specified order (e.g. `US CA GB`). Preferred countries appear once at the top above a divider line and are not duplicated in the alphabetical list below. |
| `include-countries`   | `xs:string`  | `""`     | Space- or comma-separated list of 2-letter ISO codes. When non-empty, only the specified countries are available in the selector.                                                                                                                   |
| `exclude-countries`   | `xs:string`  | `""`     | Space- or comma-separated list of 2-letter ISO codes to exclude from the selector.                                                                                                                                                                  |
| `show-code`           | `xs:boolean` | `false`  | When `true`, appends the 2-letter ISO code in parentheses to each country label (e.g. `Switzerland (CH)`).                                                                                                                                          |
| `show-flag`           | `xs:boolean` | `false`  | When `true`, prepends the country's Unicode flag emoji to each country label (e.g. `🇨🇭 Switzerland`).                                                                                                                                               |
| `appearance`          | `xs:string`  | `search` | Control appearance: `search` (searchable dropdown, default) or `dropdown` (standard `<select>` dropdown).                                                                                                                                           |

### Examples

Pinning preferred countries at the top and showing flags and codes:

```xml
<fr:country
    ref="country"
    preferred-countries="CH FR DE IT"
    show-flag="true"
    show-code="true">
    <xf:label>Country</xf:label>
</fr:country>
```

Restricting the list to specific countries:

```xml
<fr:country
    ref="shipping-country"
    include-countries="US CA MX"
    exclude-countries="MX">
    <xf:label>Shipping country</xf:label>
</fr:country>
```

## Configuration properties

The default global properties are:

```xml
<property as="xs:string"  name="oxf.xforms.xbl.fr.country.appearance"          value="search"/>
<property as="xs:string"  name="oxf.xforms.xbl.fr.country.preferred-countries" value=""/>
<property as="xs:string"  name="oxf.xforms.xbl.fr.country.include-countries"   value=""/>
<property as="xs:string"  name="oxf.xforms.xbl.fr.country.exclude-countries"   value=""/>
<property as="xs:boolean" name="oxf.xforms.xbl.fr.country.show-code"           value="false"/>
<property as="xs:boolean" name="oxf.xforms.xbl.fr.country.show-flag"           value="false"/>
```

You can override any of these properties globally or per application/form in `properties-local.xml`, for example:

```xml
<property
    as="xs:string"
    name="oxf.xforms.xbl.fr.country.preferred-countries.*.*"
    value="US CA GB"/>
<property
    as="xs:boolean"
    name="oxf.xforms.xbl.fr.country.show-flag.*.*"
    value="true"/>
```

## See also

* [Static and Dynamic Dropdown](static-dynamic-dropdown.md)
* [US State](us-state.md)
* [US Phone Number](us-phone.md)
* [Control Settings](../../form-builder/control-settings.md)
