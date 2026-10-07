# Property profiles

## Availability

[SINCE Orbeon Forms 2026.1]

## Overview

Property profiles provide a mechanism to group, share, and reuse configuration properties across multiple forms without having to duplicate property definitions in `properties-local.xml`.

Often, families or categories of forms (such as forms belonging to the same business unit or following the same workflow) share identical configuration settings, such as:

- Detail page buttons (e.g. Save, Review, PDF, Submit)
- Custom submit and action processes
- PDF layout, headers, footers, and templates
- Email notifications and recipients
- Time window / availability constraints

Prior to property profiles, administrators had to either:

1. Duplicate the exact same property definitions for every distinct form name (`oxf.fr.detail.buttons.my-app.form-1`, `oxf.fr.detail.buttons.my-app.form-2`, etc.).
2. Or use generic application-level or global wildcards (`oxf.fr.detail.buttons.my-app.*` or `oxf.fr.detail.buttons.*.*`), which apply unconditionally to *all* forms in that app or system.
3. Or explicitly group forms by application name, which is inflexible, visible in form URLs, and might not match the grouping of forms configurations.

Property profiles solve this by allowing properties in `properties-local.xml` to be tagged with a profile name (using the `profiles` attribute). Form authors can then assign a profile to any form in Form Builder. When a form is copied or created under a new name, assigning the profile instantly applies all associated properties.

## Defining properties with profiles

You assign one or more profile names to a property in `properties-local.xml` using the `profiles` attribute on `<property>`:

```xml
<!-- Default buttons for all forms assigned the `review-and-submit` profile -->
<property
    as="xs:string"
    name="oxf.fr.detail.buttons.*.*"
    profiles="review-and-submit"
    value="save review pdf"/>
```

### Multiple profiles

A single property definition can apply to multiple profiles by providing a space-separated list of profile names:

```xml
<property
    as="xs:string"
    name="oxf.fr.detail.buttons.*.*"
    profiles="standard-review expedited-review"
    value="save summary submit"/>
```

This property is picked up by forms using either the `standard-review` profile or the `expedited-review` profile.

### Combining profiles with wildcards

Profiles work seamlessly in combination with application and form name wildcards:

```xml
<!-- Global default for all forms with profile `internal` -->
<property
    as="xs:string"
    name="oxf.fr.detail.buttons.*.*"
    profiles="internal"
    value="save submit"/>

<!-- Override for forms in the `hr` app with profile `internal` -->
<property
    as="xs:string"
    name="oxf.fr.detail.buttons.hr.*"
    profiles="internal"
    value="save review send-to-hr"/>

<!-- Specific form in `hr` app with profile `internal` -->
<property
    as="xs:string"
    name="oxf.fr.detail.buttons.hr.onboarding"
    profiles="internal"
    value="save review custom-onboarding-process"/>
```

## Assigning a profile to a form

### In Form Builder

Form authors can assign a profile to a form in Form Builder:

1. Click the **Form Settings** (wrench icon) in the top toolbar.
2. In the **General Settings** tab, locate the **Profile** field.
3. Select an existing profile from the dropdown list, or enter a new profile name.
   - The dropdown automatically lists all profile names discovered in your configured properties.
   - Selecting `(None)` removes the profile assignment.
4. Click **Apply** and save the form.

### In XForms

With plain XForms, the profile is set on the top-level `<xf:model>` element using the `xxf:property-profile` attribute:

```xml
<xf:model xxf:property-profile="review-and-submit">
    ...
</xf:model>
```

When saving a form in Form Builder, this attribute is automatically added or updated on the `<xf:model>` element.

## Property resolution rules

When Orbeon Forms resolves a configuration property for a given form:

1. **If the form has a profile assigned** (e.g. `profile1`):
   1. It searches for a property matching the exact name with `profiles="profile1"`.
   2. It searches for a property matching application-level or global wildcards with `profiles="profile1"`.
   3. If no matching profile-specific property is found, it falls back to matching default properties (properties defined without a `profiles` attribute or with `profiles=""`), checking exact names first, then wildcards.

2. **If the form has no profile assigned**:
   - Only default properties (without a `profiles` attribute) are matched.
   - Properties defined with specific `profiles` are ignored.

### Precedence summary

When multiple property definitions match a given property query, the resolution order is:

| Priority | Name Match              | Profile Match        | Example Property Definition                                                     |
|:---------|:------------------------|:---------------------|:--------------------------------------------------------------------------------|
| 1        | Exact name (`app.form`) | Matching profile     | `<property name="oxf.fr.detail.buttons.hr.onboarding" profiles="internal" ...>` |
| 2        | Exact name (`app.form`) | Default (no profile) | `<property name="oxf.fr.detail.buttons.hr.onboarding" ...>`                     |
| 3        | App wildcard (`app.*`)  | Matching profile     | `<property name="oxf.fr.detail.buttons.hr.*" profiles="internal" ...>`          |
| 4        | App wildcard (`app.*`)  | Default (no profile) | `<property name="oxf.fr.detail.buttons.hr.*" ...>`                              |
| 5        | Global wildcard (`*.*`) | Matching profile     | `<property name="oxf.fr.detail.buttons.*.*" profiles="internal" ...>`           |
| 6        | Global wildcard (`*.*`) | Default (no profile) | `<property name="oxf.fr.detail.buttons.*.*" ...>`                               |

## Limitations

As of Orbeon Forms 2026.1, property profiles are not supported for the following:

- Landing, Published Forms, and Admin pages: these do not pertain to a specific form, so they cannot be assigned a profile.
- Summary page (`oxf.fr.default-timezone`, `oxf.fr.summary.show-$column.*.*`, `oxf.fr.summary.show-version-selector.*.*`, `oxf.fr.summary.page-size.*.*`)
- Configuration of the persistence layer (`oxf.fr.persistence.**` properties)
- Patching of resources (`oxf.fr.resource.**` properties)
- Form permissions (`oxf.fr.permissions.$app.$form` properties)
- TIFF settings (`oxf.fr.detail.tiff.*` properties)
- Low-level XForms properties (`oxf.xforms.*` properties like `oxf.xforms.encrypt-item-values`, etc.)

## XPath and API access

### `xxf:property()`

In XForms XPath expressions, calls to [`xxf:property('propertyName')`](../../xforms/xpath/extension-core.md#xxfproperty) automatically take into account the current form's property profile if one is configured on `<xf:model>`.

### Form Runner functions

Form Runner property lookups and functions (such as `fr:component-param-value()`) automatically take into account the current form's profile when retrieving property values.

### Java Property API

Custom property providers implementing `org.orbeon.properties.api.PropertyProvider` can supply profile information by implementing `PropertyDefinition.getProfiles()`, returning a collection of profile name strings.

## See also

* [Properties overview](README.md)
* [Form Runner properties](form-runner.md)
* [Form Settings in Form Builder](../../form-builder/form-settings.md)
* [Configuration properties with wildcards](README.md#wildcards-in-properties)
