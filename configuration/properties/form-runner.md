# Form Runner configuration properties

## Children pages

- [Detail page](form-runner-detail-page.md)
    - [Attachments](form-runner-attachments.md)
    - [Email](form-runner-email.md)
    - [PDF](form-runner-pdf.md)
- [Persistence](persistence.md)
- [Summary page](form-runner-summary-page.md)

## Default values

For the latest default values of Form Runner properties, see [properties-form-runner.xml](https://github.com/orbeon/orbeon-forms/blob/master/src/main/resources/config/properties-form-runner.xml).

## Form Runner properties documented elsewhere

* [Custom dialogs and model Logic](/form-runner/advanced/custom.md)
    * `oxf.fr.detail.model.custom`
    * `oxf.fr.detail.dialogs.custom`
* [Wizard View](/form-runner/component/wizard.md)
    * `oxf.fr.detail.view.appearance`
    * `oxf.fr.detail.buttons.inner`
    * `oxf.xforms.xbl.fr.wizard.validate`
    * `oxf.xforms.xbl.fr.wizard.separate-toc`
    * `oxf.xforms.xbl.fr.wizard.section-status`
    * `oxf.xforms.xbl.fr.wizard.subsections-nav`
    * `oxf.xforms.xbl.fr.wizard.subsections-toc`
* [Autosave](/form-runner/persistence/autosave.md)
    * `oxf.fr.detail.autosave-delay`
    * `oxf.fr.persistence.*.autosave`
* [Simple data migration](/form-runner/feature/simple-data-migration.md)
    * `oxf.fr.detail.data-migration`
* [Configuration Properties ~ Persistence](persistence.md)
    * `oxf.fr.persistence.provider`
    * `oxf.fr.persistence.[provider].uri`
    * `oxf.fr.persistence.[provider].active`
    * `oxf.fr.persistence.[provider].autosave`
    * `oxf.fr.persistence.[provider].permissions`
    * `oxf.fr.persistence.[provider].versioning`
    * `oxf.fr.persistence.[provider].data-format-version`
* [Form Runner Access Control](/form-runner/access-control/README.md)
    * `oxf.fr.support-owner-group`
    * `oxf.fr.authentication.method`
    * `oxf.fr.authentication.container.roles`
    * `oxf.fr.authentication.container.roles.split`
    * `oxf.fr.authentication.header.username`
    * `oxf.fr.authentication.header.group`
    * `oxf.fr.authentication.header.roles`
    * `oxf.fr.authentication.header.roles.split`
    * `oxf.fr.authentication.header.roles.property-name`
    * `oxf.fr.authentication.header.sticky`
* [Form Runner Published Forms Page](/form-runner/feature/published-forms-page.md)
    * `oxf.fr.home.page-size`
    * `oxf.fr.home.table.link-to`
    * `oxf.fr.home.remote-servers`
* [TIFF Production](/form-runner/feature/tiff-production.md)
    * `oxf.fr.detail.tiff.compression.type`
    * `oxf.fr.detail.tiff.compression.quality`
    * `oxf.fr.detail.tiff.scale`
    * `oxf.fr.detail.tiff.filename`

## Language

### Default language

The following property determines Form Runner's default language:

```xml
<property
    as="xs:string"
    name="oxf.fr.default-language.*.*"
    value="en">
```

When wildcards are specified, this property can control the default language for a given app or form.

The property without wildcards can also be used to control the default language of pages which don't involve a specific form, such as the Form Runner Home Page

```xml
<property
    as="xs:string"
    name="oxf.fr.default-language"
    value="en">
```

For more details, see [Language selection at runtime](/form-runner/feature/localization.md#language-selection-at-runtime)

### Available languages

For a given form, you can filter which languages are available in the language selector with a space-separated list of language codes:

```xml
<property
  as="xs:string"
  name="oxf.fr.available-languages.*.*"
  value="en fr"/>
```

The language selector by default shows all languages available in the form definition. When this property is specified, only the intersection of the languages is shown in the selector. For example:

* Example 1
    * form languages: `en fr jp`
    * property: `en fr`
    * resulting languages: `en fr`
* Example 2
    * form languages: `en fr jp`
    * property: `en jp kr`
    * resulting languages: `en jp`

If the property is blank or contains the wildcard `*`, all the form languages are available.

[SINCE Orbeon Forms 4.3]

For pages which don't involve a specific form, such as the Form Runner Home Page, the following property controls the available languages:

```xml
<property
  as="xs:string"
  name="oxf.fr.available-languages"
  value="en fr"/>
```

For more details, see [Language selection at runtime](/form-runner/feature/localization.md#language-selection-at-runtime)

## Timezone

[SINCE Orbeon Forms 2021.1]

The Summary and Home pages display timestamps showing the created and last modified dates of forms and data.

You can configure an explicit timezone to display these timestamps:

```xml
<property
      as="xs:string"
      name="oxf.fr.default-timezone"
      value="America/Los_Angeles"/>
```

```xml
<property
      as="xs:string"
      name="oxf.fr.default-timezone"
      value="Asia/Kolkata"/>
```

If the property is blank (the default), the Java environment's default timezone is used.

[UNTIL Orbeon Forms 2020.1]

The Java environment's default timezone is used.

## Summary Page

See [Summary page configuration properties](form-runner-summary-page.md).

## Detail page

See [Detail page configuration properties](form-runner-detail-page.md).

## Show Orbeon Forms version

[UNTIL Orbeon Forms 4.6, use `oxf.show-version` starting Orbeon Forms 4.6.1]

```xml
<property
    as="xs:boolean"
    name="oxf.fr.version.*.*"
    value="true"/>
```

Whether to show the Orbeon Forms version at the bottom.

## Default logo

With this property, you can set the default logo URI. This logo appears on the Summary, Detail and Home pages for a given form. You can omit (or comment out) this property or set its value to the empty string if you don't want a default logo at all.

This is the default value of the property:

```xml
<property as="xs:anyURI"  name="oxf.fr.default-logo.uri.*.*">
    /apps/fr/style/orbeon-navbar-logo.png
</property>
```

If you use two `*` wildcards, as in the example above, the property also sets the logo on the [Published Forms page](/form-runner/feature/published-forms-page.md) and the [Forms Admin page](/form-runner/feature/forms-admin-page.md)

1. Place your logo file(s) under one of the following recommended locations:
    - `WEB-INF/resources/forms/assets`: logo for all forms
    - `WEB-INF/resources/forms/APP/assets`: logo for app name APP
    - `WEB-INF/resources/forms/APP/FORM/assets`: logo for app name APP and form name FORM
2. Define the `oxf.fr.default-logo.uri.*.*` property to point to the file(s) you added. The path points to location under the `WEB-INF/resources` directory.

For example, to change the default logo for all forms and pages to an image called `my-logo.png`, place the image at the proper location and use the following property:

```xml
<property as="xs:anyURI"  name="oxf.fr.default-logo.uri.*.*">
    /forms/assets/my-logo.png
</property>
```

NOTE: Since Orbeon Forms 4.0, this property doesn't have an impact on the Form Builder logo. To override the Form Builder logo, you can use custom CSS (see the [`oxf.fr.css.custom.uri`](/form-runner/styling/css#adding-your-own-css-files) configuration).

<!--
```xml
<property as="xs:anyURI"  name="oxf.fr.default-logo.uri.orbeon.builder">
    /apps/fr/style/orbeon-navbar-logo.png
</property>
```
-->

## Adding your own CSS

See the [CSS page](/form-runner/styling/css.md).

## Adding your own JavaScript

[SINCE Orbeon Forms 4.4]

1. Place your JavaScript file(s) under one of the following recommended locations:
    * `WEB-INF/resources/forms/assets`: scripts for all forms
    * `WEB-INF/resources/forms/APP/assets`: scripts for app name APP
    * `WEB-INF/resources/forms/APP/FORM/assets`: scripts for app name APP and form name FORM
2. Define the [`oxf.fr.js.custom.uri`](/configuration/properties/form-runner.md#adding-your-own-adding-your-own-javascript-files) property to point to the file(s) you added. The path points to location under the `WEB-INF/resources` directory.

```xml
<property as="xs:string" name="oxf.fr.js.custom.uri.*.*">
    /forms/acme/assets/acme.js
    /forms/acme/sales/assets/acme-sales.js
</property>
```

You can add more than one file, and just separate the paths by whitespace in the property.

[SINCE Orbeon Forms 2017.1]

In addition to [`oxf.fr.js.custom.uri`](/configuration/properties/form-runner.md#adding-your-own-adding-your-own-javascript-files), you can also use the following properties, which apply only to the Summary and Detail pages respectively:

- [`oxf.fr.summary.js.custom.uri`](form-runner-summary-page.md#adding-your-own-javascript-files)
- [`oxf.fr.detail.js.custom.uri`](form-runner-detail-page.md#adding-your-own-javascript-files)

## Overriding resources

In some cases, it might make sense to change some of the resources provided out of the box by Form Runner. For instance, the Detail Page can have a submit button, which in English has a label "Submit". For your application, another label might make more sense, for instance "Send". To override Form Runner resources, you define properties with a name that has the following structure:

1. The name start with `oxf.fr.resource`.
2. Followed by the name of the application and form name for which you want to redefine the resource. You can use `*` for either if you want the redefinition to apply to all the applications or all the forms. For instance: `*.*`, or `my-app.my-form`.
3. The 2-letter code for the language for which you want to override the resource. For instance: `en`.
4. A dot-separated path corresponding to the path of the resource you want to override as defined by Form Runner [`resources.xml`][19].
5. Resources are aggressively caches, so you need to restart your application server (or redeploy the web app) after changing a property that overrides resources.

For instance, to change the label of the submit button to be "Send" in English for all applications and forms, write:

```xml
<property
    as="xs:string"
    name="oxf.fr.resource.*.*.en.detail.buttons.send"
    value='&lt;i class="icon-arrow-right"/&gt; Send'/>
```

This also works for resources that don't exist yet. For your own resources, start with a prefix specific to your company or project. For example:

```xml
<property
  as="xs:string"
  name="oxf.fr.resource.*.*.en.acme.my-resource-1"
  value="Resource 1 in English"/>

<property
  as="xs:string"
  name="oxf.fr.resource.*.*.fr.acme.my-resource-1"
  value="Resource 1 en français"/>
```

*NOTE: You can add new resources as shown above, but you cannot create new languages.*

## Email settings

See [Email](form-runner-email.md).

## Sections and grids

### Appearance of repeated sections

[SINCE Orbeon Forms 2016.1]

The following property allows you to set the appearance of repeated sections to `full` (the default) or `minimal` for all forms or for a subset of forms:

```xml
<property
    as="xs:string"
    name="oxf.xforms.xbl.fr.section.appearance.*.*"
    value="minimal"/>
```

See also the [`appearance`](/form-runner/component/section.md#repeated-mode) attribute of the [section component](/form-runner/component/section.md).

### Appearance of repeated grids

[SINCE Orbeon Forms 2016.1]

The following property allows you to set the appearance of repeated grids to `full` (the default) or `minimal` for all forms or for a subset of forms:

```xml
<property
    as="xs:string"
    name="oxf.xforms.xbl.fr.grid.appearance.*.*"
    value="minimal"/>
```

See also the [`appearance`](/form-runner/component/grid.md#repeated-mode) attribute of the [grid component](/form-runner/component/grid.md).

### Insert position of repeated sections

[SINCE Orbeon Forms 2016.2]

The following property allows you to select where new repetitions are added when using the "Add Another" or "+" button. Allowed values are `index` (default for the `full` appearance) and `bottom` (default for the `minimal` appearance):

```xml
<property
    as="xs:string"
    name="oxf.xforms.xbl.fr.section.insert.*.*"
    value="index"/>
```

See also the [`insert`](/form-runner/component/section.md#repeated-mode) attribute of the [section component](/form-runner/component/section.md).

### Insert position of repeated grids

[SINCE Orbeon Forms 2016.2]

The following property allows you to select where new repetitions are added when using the "Add Another" or "+" button. Allowed values are `index` (default for the `full` appearance) and `bottom` (default for the `minimal` appearance):

```xml
<property
    as="xs:string"
    name="oxf.xforms.xbl.fr.grid.insert.*.*"
    value="index"/>
```

See also the [`insert`](/form-runner/component/grid.md#repeated-mode) attribute of the [grid component](/form-runner/component/grid.md).

### Section collapsing

[SINCE Orbeon Forms 2016.1]

The following property allows you to set whether a section content can be collapsed by clicking on its title for all forms or for a subset of forms:

```xml
<property
    as="xs:boolean"
    name="oxf.xforms.xbl.fr.section.collapsible.*.*"
    value="false"/>
```

By default, sections are allowed to collapse.

The following property controls the same behavior in noscript mode:

[UNTIL Orbeon Forms 2018.1]

```xml
<property
    as="xs:boolean"
    name="oxf.xforms.xbl.fr.section.noscript.collapsible.*.*"
    value="false"/>
```

A value of `false` may make sections more accessible and less confusing to screen reader users.

The following property controls the whether collapsing/opening of sections uses an animation. The default is `true`:

```xml
<property
    as="xs:boolean"
    name="oxf.xforms.xbl.fr.section.animate.*.*"
    value="false"/>
```

A value of `false` can be more efficient with slower browsers or large forms.

### Grid markup

At runtime, grids in your forms are rendered using HTML tables, with elements such as `table`, `tr`, and `td`, this instead of using the more modern [CSS grid layout](https://developer.mozilla.org/en-US/docs/Web/CSS/CSS_Grid_Layout) (with `div` elements). This is because older browsers, in particular IE11, only provides limited support for CSS grids.

[SINCE Orbeon Forms 2020.1.7 and 2021.1.1] As support for IE11 will be dropped in future versions of Orbeon Forms, we anticipate that Orbeon Forms will by default use the CSS grids layout instead of HTML tables. You can already make this choice today by adding the following property:

```xml
<property
    as="xs:string"
    name="oxf.xforms.xbl.fr.grid.markup.*.*"
    value="css-grid"/>
```

### Deprecated properties

Before Orbeon Forms 2016.1, you could use the following properties, deprecated since Orbeon Forms 2016.1. Section collapsing:

```xml
<property
    as="xs:boolean"
    name="oxf.fr.detail.ajax.section.collapse.*.*"
    value="false"/>
```

Section collapsing in noscript mode:

[UNTIL Orbeon Forms 2018.1]


```xml
<property
    as="xs:boolean"
    name="oxf.fr.detail.noscript.section.collapse.*.*"
    value="false"/>
```

Section collapsing animation:

```xml
<property
    as="xs:boolean"
    name="oxf.fr.detail.ajax.section.animate.*.*"
    value="true"/>
```

## Noscript properties

[DEPRECATED SINCE Orbeon Forms 2016.3]

[UNTIL Orbeon Forms 2018.1]

### Show noscript link

```xml
<property
    as="xs:boolean"
    name="oxf.fr.noscript-link.*.*"
    value="true"/>
```

Whether to show the link to the noscript/full version.

### Noscript: use table layout

```xml
<property
    as="xs:boolean"
    name="oxf.fr.detail.noscript.table.*.*"
    value="true"/>
```

Whether forms in noscript mode are allowed to use a layout based on tables. If `false`, no tables are used. WYSIWYG is lost, but the form may be more accessible. The default is `true`.


[6]: https://sites.google.com/a/orbeon.com/forms/doc/developer-guide/configuration-properties/configuration-properties-base
[11]: https://www.google.com/recaptcha/admin/create
[14]: ../../form-runner/advanced/buttons-and-processes/form-runner-actions/send-action.md
[15]: https://developer.chrome.com/devtools
[16]: http://getfirebug.com/
[17]: http://wiki.orbeon.com/forms/doc/contributor-guide/browser#TOC-IE-limit-of-31-CSS-files
[18]: https://github.com/orbeon/orbeon-forms/issues/635
[19]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/i18n/resources.xml
