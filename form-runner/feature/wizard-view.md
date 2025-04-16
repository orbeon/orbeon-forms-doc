# Wizard view

## Introduction

By default, with Form Runner all the form sections appear in the same page, on top of each other. If your form is large that means that you have to scroll to fill out the entire form.

With the wizard view, top-level sections instead appear in a table of contents area to the left, and only a single top-level section is shown at any given time in a separate wizard "page":

![Form Runner Wizard](../images/wizard.webp)

You can navigate between pages by clicking on a title in the table of contents, or you can use the navigation arrows. You can also use "Prev" and "Next" buttons when configured.

Errors on your form appear at the bottom as usual, and the title of pages that contain errors are highlighted in red. If you click on an error you are taken directly to the page and control containing the error.

## Enabling the wizard view 

### Using a property

The wizard view is optional - you can use the regular view instead, and you can enable this view per form, per app, or globally with a property:

```xml
<property
  as="xs:string"
  name="oxf.fr.detail.view.appearance.*.*"
  value="wizard"/>
```

### From Form Builder

[SINCE Orbeon Forms 2016.2]

You can enable or disable the wizard view for a specific form definition directly from Form Builder's Form Settings dialog, under Form Options:

- __Use Default:__ use  the configuration specified with the `oxf.fr.detail.view.appearance` property
- __Always:__ enable the wizard view no matter what the `oxf.fr.detail.view.appearance` property specifies
- __Never:__ disable the wizard view no matter what the `oxf.fr.detail.view.appearance` property specifies

![](../../form-builder/images/form-settings-view-options.png)

## Modes

### Introduction

The wizard supports two mode:

- the *free* mode (which is the default mode)
- the *validated* mode [SINCE Orbeon Forms 4.9]

### Free mode

When using the free mode, you can freely:

- go back to the preceding page
- go forward to the next page
- change page from the table of contents
- leaving a page marks all fields on the given page as visited, ensuring that errors on that page, if any, show in the error summary


### Lax mode

[SINCE Orbeon Forms 4.9]

When using the validated mode:

- you can freely go back to the preceding page
- but you can only go forward to the next page if
  - there are no errors on all preceding pages as well as the current page
  - if you have already visited the next page
- the table of contents only allows you to navigate
  - to pages you have already visited
  - or to the next page if there are no errors on all preceding pages as well as the current page [SINCE Orbeon Forms 2016.1]
- you should generally use the "Prev" or "Next" buttons for navigation
- any attempt to navigate to the next page marks all the fields of the preceding pages as well as the current page as visited, ensuring that errors on those pages, if any, show in the error summary

This only applies to navigation between top-level sections. When subsection navigation is enabled, the validated mode applies only to top-level sections, while navigation within a given top-level section is always free.

[FROM Orbeon Forms Forms 4.9 to 2016.2]

The following property enables the lax validated mode:

```xml
<property
  as="xs:boolean"
  name="oxf.xforms.xbl.fr.wizard.validate.*.*"
  value="true"/>
```

[SINCE Orbeon Forms 2016.3]

The following property enables the lax validated mode:

```xml
<property
  as="xs:string"
  name="oxf.xforms.xbl.fr.wizard.validate.*.*"
  value="lax"/>
```

Setting the value to `true` is still supported for backward compatibility.

You can see in the following picture that the sections in the table of contents which have not yet been visited are grayed out:

![Wizard validated mode](../images/wizard-validated.png)

### Strict mode

[SINCE Orbeon Forms 2016.3]

The following property enables the strict validated mode:

```xml
<property
  as="xs:string"
  name="oxf.xforms.xbl.fr.wizard.validate.*.*"
  value="strict"/>
```

This mode behaves the same as the lax validated mode, except that it doesn't matter if a forward section has been visited or not: if there are errors in the current or preceding wizard pages, it is not possible to navigate with the "Next" button or via the table of contents.

This only applies to navigation between top-level sections. When subsection navigation is enabled, the validated mode applies only to top-level sections, while navigation within a given top-level section is always free.

## Buttons

### Inner buttons

The following property allows specifying which buttons are presented *inside* the wizard, as opposed to the bottom of the page. This example places the wizard's "Prev" and "Next" buttons just under the current wizard section:

```xml
<property
    as="xs:string"
    name="oxf.fr.detail.buttons.inner.*.*"
    value="wizard-prev wizard-next"/>
```

![](../../form-runner/images/wizard-buttons.png)

## Separate table of contents

[SINCE Orbeon Forms 2016.2]

When set to `true`, the following property enables showing the table of contents separately, as if on a different page:

```xml
<property
    as="xs:boolean"
    name="oxf.xforms.xbl.fr.wizard.separate-toc.*.*"
    value="true"/>
```

When `false` (the default):

- the table of contents appears to the left (wide desktop layout) or the top (narrow mobile layout)
- the section content appears to the right (wide desktop layout) or under the table of contents (narrow mobile layout)

When `true`:

- the table of contents takes the entire width when landing on the page
- when selecting a section, the view toggles to the selected section (top-level or subsection depending on whether subsection navigation is enabled)

<!-- TODO: screenshot -->

[SINCE Orbeon Forms 2022.1]

The Form Settings in Form Builder allow overriding the default set by configuration properties at the form level.

## Section status

### Status indication

[SINCE Orbeon Forms 2016.2]

When enabling the separate table of contents of the wizard (see above), the wizard automatically indicates, the status of each section:

- __Not Started:__ the user hasn't visited the section yet.
- __Incomplete:__ the user has visited the section but some required fields are not filled.
- __Errors:__ the user has visited the section and some fields have been filled but contain errors.
- __Complete:__ the user has visited the section and all the required fields for the section have been filled. 

![Wizard validated mode](../images/wizard-status.png)

[SINCE Orbeon Forms 2022.1]

By default, the status of each section is not shown in the regular (not separate) wizard table of contents. Setting the `section-status` property to `true` allows changing this default: 

```xml
<property
    as="xs:boolean"
    name="oxf.xforms.xbl.fr.wizard.section-status.*.*"
    value="true"/>
```

The Form Settings in Form Builder allow overriding the default set by configuration properties at the form level. 

<!-- TODO: screenshot -->

### CSS classes

CSS classes are available on table of content entries to reflect the status of each section:

- `disabled`
- `active`
- `started`
- `changed`
- `incomplete`
- `invalid`

[SINCE Orbeon Forms 2018.1]

The following classes indicate the first and last sections:

- `first-page`: the first visible page
- `last-top-level-page`: the first visible top-level page 
- `last-page`: the last visible page
    - this can be different from `last-top-level-page` when using subsection navigation

### Wizard status persistence

[SINCE Orbeon Forms 2017.2]

The wizard status is persisted when the data is saved to the database. This means that if the user saves incomplete 
data and comes back to it, information about visited or changed wizard pages is restored.

The wizard shows the last possible page upon loading data.

## Subsections

### Subsections navigation

[SINCE Orbeon Forms 2016.2]

```xml
<property
    as="xs:boolean"
    name="oxf.xforms.xbl.fr.wizard.subsections-nav.*.*"
    value="true"/>
```

When `false` (the default):

- the wizard allows you to navigate only between top-level sections

When `true`:

- the wizard navigates through the first level of subsections when present
- it shows one first level of subsections at a time, or one top-level section at a time for those top-level sections which don't have any subsections
- second-level and deeper levels of subsections appear on the same wizard page
- the "Next" and "Prev" buttons go to the next or previous first-level subsection if any, and then to the next or previous top-level section's first subsection if any
- the validated mode, if enabled, still applies only to top-level sections, which means that
    - it is possible to freely navigate through subsections within a given top-level section
    - however when attempting to navigate to the next top-level section, validation constraints apply

Grids directly nested within top-level sections which have any subsections are ignored in this mode. The recommendation is to avoid this situation when using subsections navigation and not place grids directly at the same level of first-level subsections. This restriction is lifted in Orbeon Forms 2016.3.

Top-level repeated sections are not supported. The recommendation is to avoid this situation when using subsections navigation and not use top-level repeated sections. This is improved in Orbeon Forms 2016.3 where top-level repeated sections disable subsection navigation for themselves.

<!-- TODO: screenshot -->

[SINCE Orbeon Forms 2016.3]

Grids directly nested within top-level sections which have any subsections are also part of the navigation. Each grid is navigated independently from the other grids.

The `subsections-nav` attribute on top-level `fr:section` elements can be used to explicitly disable subsection navigation. See [Wizard Component](../component/wizard.md#the-subsections-nav-attribute).

### Visibility in the table of contents

[SINCE Orbeon Forms 2016.2]

```xml
<property
    as="xs:string"
    name="oxf.xforms.xbl.fr.wizard.subsections-toc.*.*"
    value="active"/>
```

In all cases, the table of contents shows all visible top-level sections.

When `active` (the default and only behavior until 2016.1):

- the table of contents shows visible subsections only for the currently-visible top-level section

When `all`:

- the table of contents shows all visible subsections

When `none`:

- the table of contents doesn't show any subsection

<!-- TODO: screenshot -->

## Paging large repeated sections

By default, top-level repeated sections appear as a single entry in the table of contents. The section's repeated content appears within the section, each repetition on top of the other, as is the case when the wizard is not used.

[SINCE Orbeon Forms 2019.1]

It is now possible to make each iteration of a top-level repeated section that is the target of [synchronized repeated content](/form-builder/synchronize-repeated-content.md) appear as a separate entry in the table of contents. The benefit of this is that, if the repeated section content is large, then less content appears on screen at the same time. This is better for usability and for performance. 

To enable this, go to the relevant [section repeat settings](/form-builder/repeat-settings.md), and:

- Uncheck "Allow the user to add, remove, or move repetitions"
- Check "Show one repetition at a time in the wizard"

![Repeated Content Configuration](../../form-builder/images/container-settings-repeated-content-one-repetition.png)

In addition, each repetition can have an individual [repetition label](/form-builder/section-settings.md#dynamic-iteration-label).

![](../../form-builder/images/section-settings-repetition-label.png)

## Disabling full update

[SINCE Orbeon Forms 2022.1]

On the browser, by default, only the HTML for the page of the wizard currently being displayed is present in the DOM. When users switch from page A to page B, the HTML for page B is loaded and replaces in the DOM the HTML for page A. This makes the initial load of the form faster, as the HTML for just a single page is loaded, and the browser can then process the form more efficiently, having to deal with a much smaller DOM.

However, should you want to disable this behavior, and have the HTML for all the pages loaded at once, maybe because you have custom JavaScript for which it is convenient to access control values outside the current page, you can do so by setting the following property:

```xml
<property 
    as="xs:boolean"
    name="oxf.xforms.xbl.fr.wizard.full-update.*.*"                   
    value="false"/>
```

## Page name URL parameter

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

The URL of the form now contains a `fr-wizard-page` parameter, which indicates the page currently being displayed. For example:

```
/fr/orbeon/controls/new?fr-wizard-page=selection-controls
```

Form Runner also reads this URL parameter to attempt initializing the wizard to the requested page. This is useful when you want to bookmark a specific page of the wizard, or when you want to send a link to a specific page of the wizard to someone else.

Requesting a specific wizard page may fail in case the page is non-existent or not accessible to the current user. In particular, this is the case for non-available pages in validated modes. In that case, the wizard displays the first available page of the wizard as if the parameter had not been provided.

When using paged repeated sections, the URL parameter indicates the repetition number, and requesting that URL parameter value shows the given repetition if it exists and is available. If the page name doesn't correspond to a repeated section, or if the index requested is out of bounds, the wizard displays the first available page of the wizard as if the parameter had not been provided. The index is 1-based and encoded after an encoded `/` character (`%2F`), for example:

```
/fr/orbeon/travel/edit/28431b9fac9ba642a6bf8408a513538a97273483
    ?fr-wizard-page=detail-section%2F2
```

It is possible to disable this behavior by setting the following property to `false`:

```xml
<property 
    as="xs:boolean"
    name="oxf.xforms.xbl.fr.wizard.allow-url-param.*.*"                   
    value="false"/>
```

It is possible to read the current wizard page name using the [`fr:wizard-current-page-name()`](/xforms/xpath/extension-form-runner.md#fr-wizard-current-page-name) function. This can be used for example whe calling a process from a button to pass the current wizard page name as a parameter to the service URL.

## See also

- [Synchronizing repeated content](/form-builder/synchronize-repeated-content.md)
- [Repeat settings](/form-builder/repeat-settings.md)
- [Dynamic iteration label](/form-builder/section-settings.md#dynamic-iteration-label)
- [Wizard XBL component](/form-runner/component/wizard.md)
- [Buttons and Processes](/form-runner/advanced/buttons-and-processes/README.md)
- Blog posts
    - [Form Runner Wizard View](https://blog.orbeon.com/2012/12/form-runner-wizard-view.html)
    - [New wizard validated mode](https://blog.orbeon.com/2015/03/new-wizard-validated-mode.html)
    - [Synchronized master-detail views](https://blog.orbeon.com/2019/01/synchronized-master-detail-views.html)
    - [Dynamic loading of closed sections](https://blog.orbeon.com/2020/04/dynamic-loading-of-closed-sections.html)
