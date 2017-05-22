# Form Runner configuration properties

<!-- toc -->

## Children pages

- [Attachments](form-runner-attachments.md)
- [Detail page](form-runner-detail-page.md)
- [Persistence](persistence.md)

## Default values

For the latest default values of Form Runner properties, see [properties-form-runner.xml](https://github.com/orbeon/orbeon-forms/blob/master/src/main/resources/config/properties-form-runner.xml).

## Form Runner properties documented elsewhere

* [Custom Model Logic](../../form-runner/advanced/custom-model-logic.md)
    * `oxf.fr.detail.model.custom`
* [Wizard View](../../form-runner/component/wizard.md)
    * `oxf.fr.detail.view.appearance`
    * `oxf.fr.detail.buttons.inner`
    * `oxf.xforms.xbl.fr.wizard.validate`
    * `oxf.xforms.xbl.fr.wizard.separate-toc`
    * `oxf.xforms.xbl.fr.wizard.subsections-nav`
    * `oxf.xforms.xbl.fr.wizard.subsections-toc`
* [Autosave](../../form-runner/persistence/autosave.md)
    * `oxf.fr.detail.autosave-delay`
    * `oxf.fr.persistence.*.autosave`
* [Configuration Properties ~ Persistence](persistence.md)
    * `oxf.fr.persistence.provider`
    * `oxf.fr.persistence.[provider].uri`
    * `oxf.fr.persistence.[provider].active`
    * `oxf.fr.persistence.[provider].autosave`
    * `oxf.fr.persistence.[provider].permissions`
    * `oxf.fr.persistence.[provider].versioning`
    * `oxf.fr.persistence.[provider].data-format-version`
* [Form Runner Access Control](../../form-runner/access-control/README.md)
    * `oxf.fr.support-owner-group`
    * `oxf.fr.authentication.method`
    * `oxf.fr.authentication.container.roles`
    * `oxf.fr.authentication.container.roles.split`
    * `oxf.fr.authentication.header.username`
    * `oxf.fr.authentication.header.group`
    * `oxf.fr.authentication.header.roles`
    * `oxf.fr.authentication.header.roles.split`
    * `oxf.fr.authentication.header.roles.property-name`
* [Form Runner Home Page](../../form-runner/feature/home-page.md)
    * `oxf.fr.home.page-size`
    * `oxf.fr.home.remote-servers`
* [TIFF Production](../../form-runner/feature/tiff-production.md)
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

For more details, see [Language selection at runtime](../../form-runner/feature/localization.html#language-selection-at-runtime)

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

 For more details, see [Language selection at runtime](../../form-runner/feature/localization.html#language-selection-at-runtime)

## Summary Page

### Summary Page size

```xml
<property
    as="xs:integer"
    name="oxf.fr.summary.page-size.*.*"
    value="10"/>
```

Number of rows shown in the Summary Page.

### Created and Last Modified columns

By default, the Summary Page shows a Created and Modified columns:

![](/form-runner/images/summary-created-last-modified.png)

You can remove either one of those columns by setting the value appropriate property to `false`:

```xml
<property
    as="xs:boolean"
    name="oxf.fr.summary.show-created.*.*"
    value="true"/>

<property
    as="xs:boolean"
    name="oxf.fr.summary.show-last-modified.*.*"
    value="true"/>
```

### Buttons on the Summary Page

```xml
<property
    as="xs:string"
    name="oxf.fr.summary.buttons.*.*"
    value="home review pdf delete duplicate new"/>
```

The property configures which buttons are included on the Summary Page, and in what order they are shown. Possible buttons are:

* `home`
    * Label: "Home"
    * Action: Navigate to the Form Runner Home Page.
* review
    * Label: "Review"
    * Action: Navigate to the Detail Page in "view" mode to review the selected form data.
* `pdf`
    * Label: "PDF"
    * Action: Create a PDF file for the selected form data.
* `tiff` [SINCE Orbeon Forms 2016.1]
    * Label: "TIFF"
    * Action: Create a TIFF image file for the selected form data.
* `delete`
    * Label: "Delete"
    * Action: Delete the selected form data.
* `import`
    * Label: "Import"
    * Action: Import data via the [Excel import page](../../form-runner/advanced/excel.md).
* `duplicate` [SINCE Orbeon Forms 4.5]
    * Label: "Duplicate"
    * Action: Duplicate the selected form data (or form definition on the Form Builder Summary Page), including attachments.
    * Usage: Select one or more checkboxes and press the "Duplicate" button. When the operation completes the Summary Page refreshes with the new duplicated form data.
* `new`
    * Label: "New"
    * Action: Navigate to the Detail Page in "new" mode to create new form data.

## Detail page

xxx

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

```xml
<property
    as="xs:anyURI"
    name="oxf.fr.default-logo.uri.*.*"
    value="/apps/fr/style/orbeon-logo-trimmed-transparent-42.png"/>
```

With this property, you can set the default logo URI. This logo appears on the summary and Detail Pages for a given form. You can omit (or comment out) this property or set its value to `""` (empty string) if you don't want a default logo at all.

If you use two `*` wildcards, as in the example above, the property also sets the logo on the [Form Runner Home page](../../form-runner/feature/home-page.md).

## Adding your own CSS

### Adding your own CSS files

1. Place your CSS file(s) under one of the following recommended locations:
    * `WEB-INF/resources/forms/assets`: CSS for all forms
    * `WEB-INF/resources/forms/APP/assets`: CSS for app name APP
    * `WEB-INF/resources/forms/APP/FORM/assets`: CSS for app name APP and form name FORM
2. Define the `oxf.fr.css.custom.uri.*.*` property to point to the file(s) you added.

```xml
<property
    as="xs:string"
    name="oxf.fr.css.custom.uri.*.*"
    value="/forms/acme/assets/acme.css"/>
```

You can add more than one file, and just separate the paths by whitespace in the property.

[SINCE Orbeon Forms 2017.1]

In addition to `oxf.fr.css.custom.uri`, you can also use the following properties, which apply only to the Summary and Detail pages respectively:

```xml
<property
    as="xs:string"
    name="oxf.fr.summary.css.custom.uri.*.*"
    value="/forms/acme/assets/acme-summary.css"/>
```

```xml
<property
    as="xs:string"
    name="oxf.fr.detail.css.custom.uri.*.*"
    value="/forms/acme/assets/acme-detail.css"/>
```

### Authoring CSS

1. **Disable the minimal and combined resources**. When working on your CSS, you might want to temporarily set the following properties in your `properties-local.xml`, which  will disable the combined and minimized resources, so the files and line numbers you see in your browser correspond to what you have on disk.

    ```xml
    <property
        as="xs:boolean"
        name="oxf.xforms.minimal-resources"
        value="false"/>

    <property
        as="xs:boolean"
        name="oxf.xforms.combine-resources"
        value="false"/>
    ```
2. **Know which class names to use in your CSS selectors**. We strongly recommend you use the [Chrome Dev Tools][15] or [Firebug][16] to check which classes are generated by Orbeon Forms. Look specifically for classes that start with `fr-`. Once you have your CSS working with Chrome and/or Firefox, to test it on IE, you'll need to enable minimal resources, as IE is [unable to loads more than 31 CSS files][17].

 **Use case** |  **Sample CSS** |  **Description**
-----|-----|-----
 Change the width of a column |  `.fr-grid-invoice .fr-grid-col-1 { width: 40px }` | 1. In Form Builder, you can name grids (for now [only repeated grids can be named][18]). When doing so, the table element corresponding to your grid gets a `fr-grid-my-name` class, where `my-name` is the name you choose for the grid. In the example, the name was `invoice`.<br>2. Each column gets a class `fr-grid-col-1`, `fr-grid-col-2` and so on, starting with the number 1.

## Adding your own JavaScript

[SINCE Orbeon Forms 4.4]

1. Place your JavaScript file(s) under one of the following recommended locations:
    * `WEB-INF/resources/forms/assets`: scripts for all forms
    * `WEB-INF/resources/forms/APP/assets`: scripts for app name APP
    * `WEB-INF/resources/forms/APP/FORM/assets`: scripts for app name APP and form name FORM
2. Define the `oxf.fr.js.custom.uri.*.*` property to point to the file(s) you added.

```xml
<property
    as="xs:string"
    name="oxf.fr.js.custom.uri.*.*"
    value="/forms/acme/assets/acme.js forms/acme/sales/assets/acme-sales.js"/>
```

You can add more than one file, and just separate the paths by whitespace in the property.

[SINCE Orbeon Forms 2017.1]

In addition to `oxf.fr.js.custom.uri`, you can also use the following properties, which apply only to the Summary and Detail pages respectively:

```xml
<property
    as="xs:string"
    name="oxf.fr.summary.js.custom.uri.*.*"
    value="/forms/acme/assets/acme-summary.js"/>
```

```xml
<property
    as="xs:string"
    name="oxf.fr.detail.js.custom.uri.*.*"
    value="/forms/acme/assets/acme-detail.js"/>
```

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

## Email settings

These properties control email sending in Form Runner:

```xml
<property
    as="xs:string"
    name="oxf.fr.email.smtp.host.*.*"
    value="my.outgoing.smtp.server.org"/>

<property
    as="xs:string"
    name="oxf.fr.email.smtp.port.*.*"
    value="587"/>

<property
    as="xs:string"
    name="oxf.fr.email.smtp.encryption.*.*"
    value="tls"/>

<property
    as="xs:string"
    name="oxf.fr.email.smtp.username.*.*"
    value="jdoe"/>

<property
    as="xs:string"
    name="oxf.fr.email.smtp.credentials.*.*"
    value="secret"/>

<property
    as="xs:string"
    name="oxf.fr.email.from.*.*"
    value="john@example.org"/>

<!-- The list of emails is space- or comma- separated -->
<property
    as="xs:string"
    name="oxf.fr.email.to.*.*"
    value="mary@example.org,nancy@example.org"/>
    
<!-- The list of emails is space- or comma- separated -->
<!-- [SINCE Orbeon Forms 2017.1] -->
<property
    as="xs:string"
    name="oxf.fr.email.cc.*.*"
    value="mary@example.org,nancy@example.org"/>

<!-- The list of emails is space- or comma- separated -->
<!-- [SINCE Orbeon Forms 2017.1] -->
<property
    as="xs:string"
    name="oxf.fr.email.bcc.*.*"
    value="mary@example.org,nancy@example.org"/>

<property
    as="xs:boolean"
    name="oxf.fr.email.attach-pdf.*.*"
    value="true"/>

<property
    as="xs:boolean"
    name="oxf.fr.email.attach-tiff.*.*"
    value="true"/>

<property
    as="xs:boolean"
    name="oxf.fr.email.attach-xml.*.*"
    value="true"/>

<property
    as="xs:string"
    name="oxf.fr.email.attach-files.*.*"
    value="all"/>
```

The following properties control the connection to the SMTP server.

- `host`: required SMTP host name
- `port`: optional SMTP port override. If not specified, the defaults are:
    * plain SMTP: 25
    * TLS: 587
    * SSL: 465
- `encryption`:
    * blank: none (plain SMTP)
    * `tls`: use TLS
    * `ssl`: use SSL
- `username`: SMTP username (required if TLS or SSL is used, optional otherwise)
- `credentials`: SMTP password

Email addresses properties:

- `from`: sender email address(es) appearing in the email sent
- `to`: recipient email address(es) of the email sent

Attachment properties:

- `attach-pdf`: whether the PDF representation is attached to the email
- `attach-tiff`: whether the TIFF representation is attached to the email
- `attach-xml`:  whether the XML data is attached to the email
- `attach-files`:
    - SINCE Orbeon Forms 2016.1
    - whether file and image form attachments are attached to the email
    - `all`: all form attachments are included (this is the default)
    - `none`: no form attachments is included
    - `selected`: only form attachments selected in the Form Builder with "Include as Email Attachment" are included

## Sections and grids

### Appearance of repeated sections

[SINCE Orbeon Forms 2016.1]

The following property allows you to set the appearance of repeated sections to `full` (the default) or `minimal` for all forms or for a subset of forms:

```xml
<property
    as="xs:string"
    name="oxf.xforms.xbl.fr.section.repeat.appearance.*.*"
    value="minimal"/>
```

See also the [`appearance`](../../form-runner/component/section.html#repeated-mode) attribute of the [section component](../../form-runner/component/section.html).

### Appearance of repeated grids

[SINCE Orbeon Forms 2016.1]

The following property allows you to set the appearance of repeated grids to `full` (the default) or `minimal` for all forms or for a subset of forms:

```xml
<property
    as="xs:string"
    name="oxf.xforms.xbl.fr.grid.repeat.appearance.*.*"
    value="minimal"/>
```

See also the [`appearance`](../../form-runner/component/grid.html#repeated-mode) attribute of the [grid component](../../form-runner/component/grid.html).

### Insert position of repeated sections

[SINCE Orbeon Forms 2016.2]

The following property allows you to select where new iterations are added when using the "Add Another" or "+" button. Allowed values are `index` (default for the `full` appearance) and `bottom` (default for the `minimal` appearance):

```xml
<property
    as="xs:string"
    name="oxf.xforms.xbl.fr.section.repeat.insert.*.*"
    value="index"/>
```

See also the [`insert`](../../form-runner/component/section.html#repeated-mode) attribute of the [section component](../../form-runner/component/section.html).

### Insert position of repeated grids

[SINCE Orbeon Forms 2016.2]

The following property allows you to select where new iterations are added when using the "Add Another" or "+" button. Allowed values are `index` (default for the `full` appearance) and `bottom` (default for the `minimal` appearance):

```xml
<property
    as="xs:string"
    name="oxf.xforms.xbl.fr.grid.repeat.insert.*.*"
    value="index"/>
```

See also the [`insert`](../../form-runner/component/grid.html#repeated-mode) attribute of the [grid component](../../form-runner/component/grid.html).

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
    as="xs:boolean
    name="oxf.xforms.xbl.fr.section.animate.*.*"
    value="false"/>
```

A value of `false` can be more efficient with slower browsers or large forms.

### Deprecated properties

Before Orbeon Forms 2016.1, you could use the following properties, deprecated since Orbeon Forms 2016.1. Section collapsing:

```xml
<property
    as="xs:boolean"
    name="oxf.fr.detail.ajax.section.collapse.*.*"
    value="false"/>
```

Section collapsing in noscript mode:


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
[14]: https://github.com/orbeon/orbeon-forms/wiki/Form-Runner-~-Buttons-and-Processes#send
[15]: https://developer.chrome.com/devtools
[16]: http://getfirebug.com/
[17]: http://wiki.orbeon.com/forms/doc/contributor-guide/browser#TOC-IE-limit-of-31-CSS-files
[18]: https://github.com/orbeon/orbeon-forms/issues/635
[19]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/i18n/resources.xml
