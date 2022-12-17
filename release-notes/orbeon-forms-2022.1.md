# Orbeon Forms 2022.1 (Draft)

__Friday, December 30, 2021__

Today we released Orbeon Forms 2022.1! This release introduces new features and bug-fixes.

## New features

### Major new features

#### Improved landing page and navigation

Form Runner now features a new landing page, organized in cards which provides quick access to:

- Quick links, including Form Builder and the Administration page
- Your published forms
- Your in-progress Form Builder forms
- Demonstration forms

[TODO: screenshot]

Some cards directly list content, including the list of published forms and the list of in-progress Form Builder forms.

[TODO: screenshot]

You can configure whether you want to have a particular card on the landing page. For example, you can hide the demonstration forms for production deployment.

All Form Runner and Form Builder navigation bars now provide a direct link to the landing page. This can be disabled if not needed.

[TODO: screenshot]

Finally, Form Runner buttons now are sticky, so that they are always visible when scrolling. This is particularly useful on the Summary and Forms pages, which can display long lists of forms or form data.

Previously, Form Runner featured a static landing page, as well as a Home page, which doubled as an Admin page when the user was an administrator. The Home page is now replaced by a Forms page, which shows accessible published forms but no administration functions, and a separate Administration page, which is only accessible to administrators. 

[TODO: screenshot]

[//]: # (- Better landing page &#40;[\#1410]&#40;https://github.com/orbeon/orbeon-forms/issues/1410&#41;&#41;)

#### Drag and drop of form controls to grid cells

Form Builder has long supported drag and drop of form controls *between* grid cells. This release introduces drag and drop of form controls from the toolbox to grid cells, which is more intuitive to newcomers and also convenient for experienced users. You can still click on toolbox controls to add them to a grid cell, but you can now also drag and drop them.

[TODO: screenshot]

[//]: # (- Form Builder: Drag and Drop of controls to grid cells &#40;[\#5252]&#40;https://github.com/orbeon/orbeon-forms/issues/5252&#41;&#41;)

#### Per-app and global permissions configuration

Previously, Form Runner permissions were configured per form. This release introduces the ability to configure permissions per app, as well as globally. This is particularly useful when you have a large number of forms, and want to configure permissions for all of them at once.

Per-app and global permissions are currently set using configuration properties. In the future, we plan to add a UI to configure them. 

[//]: # (- Per-app and global permissions configuration &#40;[\#1860]&#40;https://github.com/orbeon/orbeon-forms/issues/1860&#41;&#41;)

#### Resizable attached images

The Image Attachment form control now supports resizing images. You can configure the maximum width and height of images, and the control will automatically resize images to fit within these dimensions.

You can also choose the resulting image format, which can be PNG or JPEG, and the quality of the JPEG image. 

[TODO: screenshot]

[//]: # (- Image attachment: ability to resize images upon upload &#40;[\#3061]&#40;https://github.com/orbeon/orbeon-forms/issues/3061&#41;&#41;)

#### Improved selection on the Summary, Forms, and Admin pages

The Summary, Forms, and Admin pages now support quick selection of multiple form data or published forms. These pages already allowed you to perform operations on one or more rows, such as deleting data, publishing forms, etc. However, you had to check each checkbox individually. We have now implemented two new features that improve on this:

- A menu to quickly select items on the Summary page (the Admin page already had such a menu).
- The ability to “shift-click” checkboxes.

The menu allows you to select all items, or to select only items that match a particular status. For example, you can select only published forms on the Admin page, or only form data that is not an in-progress draft in a form's Summary page. The number of selected 

If you are familiar with for example Gmail, you might know that you can select a checkbox, and then shift-click another one, and all the checkboxes in the interval will be selected. Similarly, you can deselect a series of checkboxes. The exact logic is a little subtle, but we implemented something very similar in Form Runner.

[TODO: screenshot]

This doesn’t only apply to the Form Runner Summary, Forms, and Admin pages: it applies to any checkboxes in a repeated grid. And it also applies to the Checkboxes form control, so you can quickly select and deselect a range of checkboxes.

[TODO: link to blog post]

#### Improved simple data migration

Orbeon Forms 2018.2 introduced Simple Data Migration (see also the original blog post). This feature allows the form author to make changes to a published form, including adding and removing form fields, grids, and sections, without creating a new form version.

Simple Data Migration, introduced with Orbeon Forms 2018.2, is very useful, allowing the form author to make changes to a published form, including adding and removing form fields, grids, and sections, without creating a new form version. This release introduces a number of improvements to Simple Data Migration and with Orbeon Forms 2022.1, you can move form controls within the form as long as they remain within the same nesting of repeated content, republish the form over the existing version, and things will just work. For example:

- Move controls at the top-level of a form, even across grids and sections.
- Move controls within a given level of repeated grids or repeated sections, even across nested grids.
- Simply moving a form control this way allows you to reorganize your form while keeping access to existing data.

[TODO: screenshot]

[Blog post: Improved simple data migration](https://blog.orbeon.com/2022/09/improved-simple-data-migration.html)

#### Improved appearance of dialogs

TODO

#### TODO

- File Scan API v2 ([\#4967](https://github.com/orbeon/orbeon-forms/issues/4967))
- Ability to use variables for min, max, etc. ([\#309](https://github.com/orbeon/orbeon-forms/issues/309))
- Admin page: add Selected badge and align ([\#5426](https://github.com/orbeon/orbeon-forms/issues/5426))
- Summary: Button to select all drafts ([\#3582](https://github.com/orbeon/orbeon-forms/issues/3582))
- FR: send() to support multipart for attachments ([\#788](https://github.com/orbeon/orbeon-forms/issues/788))
- New list permission to protect Summary pages ([\#5397](https://github.com/orbeon/orbeon-forms/issues/5397))
- Support multiple email templates per form ([\#3912](https://github.com/orbeon/orbeon-forms/issues/3912))
- Replace flying saucer with open html to pdf ([\#5342](https://github.com/orbeon/orbeon-forms/issues/5342))
- Separate the Home page and the Admin page ([\#2753](https://github.com/orbeon/orbeon-forms/issues/2753))

### Other new features

- Use of CSS grids by default
- Improved dialog appearance (#xxxx)
- JavaScript embedding API to support setting headers ([\#5142](https://github.com/orbeon/orbeon-forms/issues/5142))
- Actions Editor to support passing URL parameters by name ([\#5094](https://github.com/orbeon/orbeon-forms/issues/5094))
- Lots of a11y improvements
- JavaScript Embedding improvements
- File scan API: ability to return specific error message, mediatype, filename ([\#4960](https://github.com/orbeon/orbeon-forms/issues/4960))
- File scan API: allow replacement of the file binary ([\#4962](https://github.com/orbeon/orbeon-forms/issues/4962))
- Allow admins to configure the behavior of the Version dropdown of the Publish dialog ([\#5281](https://github.com/orbeon/orbeon-forms/issues/5281))
- Ability to change "Add Another Repetition" label ([\#3911](https://github.com/orbeon/orbeon-forms/issues/3911))
- Search API: option to return all indexed fields ([\#4968](https://github.com/orbeon/orbeon-forms/issues/4968))
- Email addresses to support including a personal name ([\#5313](https://github.com/orbeon/orbeon-forms/issues/5313))
- Make Summary page fluid by default ([\#5228](https://github.com/orbeon/orbeon-forms/issues/5228))
- Date picker: option to select week start day ([\#5334](https://github.com/orbeon/orbeon-forms/issues/5334))
- XPath function to obtain link back to Form Runner ([\#5338](https://github.com/orbeon/orbeon-forms/issues/5338))
- Add support for shift-click for checkboxes ([\#1729](https://github.com/orbeon/orbeon-forms/issues/1729))
- Summary search: "Dropdown with Other" only shows static options ([\#5408](https://github.com/orbeon/orbeon-forms/issues/5408))
- Summary: align table headers depending on content ([\#1693](https://github.com/orbeon/orbeon-forms/issues/1693))
- Actions Editor to support new "disappears" for controls ([\#5468](https://github.com/orbeon/orbeon-forms/issues/5468))
- PDF: Checkboxes should be different from radio buttons ([\#5482](https://github.com/orbeon/orbeon-forms/issues/5482))
- Wizard: Form Builder UI for separate TOC ([\#5321](https://github.com/orbeon/orbeon-forms/issues/5321))
- Wizard: support section status even when TOC is not separate ([\#3379](https://github.com/orbeon/orbeon-forms/issues/3379))
- "Dropdown with Other" component ([\#5172](https://github.com/orbeon/orbeon-forms/issues/5172))
- Allow fr:drop-trigger to be used by Form Runner ([\#5546](https://github.com/orbeon/orbeon-forms/issues/5546))
- Update function to access the value of a control within a section template ([\#5246](https://github.com/orbeon/orbeon-forms/issues/5246))
- Ability to show control names in overlay on the form ([\#5545](https://github.com/orbeon/orbeon-forms/issues/5545))
- Embed a richer font into PDF files by default ([\#5536](https://github.com/orbeon/orbeon-forms/issues/5536)) ([doc](https://doc.orbeon.com/form-builder/advanced/pdf-production/pdf-automatic#new-default-font))

## Compatibility notes

### Internet Explorer support

This release no longer supports Internet Explorer. In particular, Internet Explorer 11 is no longer supported.

### Legacy date/time fields outside of Form Runner

With this release, we have removed support for the date picker based on the long-deprecated YUI library. This date picker was used in the following cases:

1. With Form Builder/Form Runner, when forms had not been migrated (that is, either republished or migrated using the Admin page).
2. When using plain XForms outside of Form Builder/Form Runner.

With this release, Form Runner now automatically migrates forms at runtime to use the new date picker, even if they have not been explicitly migrated. However, if you are using plain XForms outside of Form Runner, the new date picker currently does not work.

Specifically, with plain XForms:

- `xf:input` fields bound to an `xs:date`, `xs:time`, or `xs:dateTime` node don't work and show plain input fields instead.
- `fr:date`, which worked outside of Form Runner with Orbeon Forms 2021.1, currently doesn't work.

We plan to add support for the new date picker in plain XForms in a future point release. If you are using plain XForms and not Form Builder/Form Runner, we don't recommend upgrading until backward compatibility is added.

### Number fields outside of Form Runner

The `fr:number` field doesn't work outside of Form Runner, see [([\#5533](https://github.com/orbeon/orbeon-forms/issues/5533))](https://github.com/orbeon/orbeon-forms/issues/5533).

We plan to add support for the new date picker in plain XForms in a future point release. If you are using plain XForms and not Form Builder/Form Runner, we don't recommend upgrading until backward compatibility is added. 

### Form Runner landing page

TODO:

- `/home/` vs. `/fr/`
- `/fr/` now is a landing page
- `/fr/forms` now points to published forms
- `/fr/admin` now points to published forms and admin tasks

### Extra elements in initial data POSTed to form

If the data posted contains extra elements, those were ignored prior to Orbeon Forms 2022.1. With Orbeon Forms 2022.1 and newer, they cause an error.

See [Initial data posted to the New Form page](/configuration/properties/form-runner-detail-page.md#initial-data-posted-to-the-new-form-page).

### Use of CSS grids

Form Builder already used [CSS grids](https://developer.mozilla.org/en-US/docs/Web/CSS/CSS_Grid_Layout) for rendering the form being edited. Starting with Orbeon Forms 2022.1, by default, Orbeon Forms also uses CSS grids for all grids at runtime as well. Previously, the default was to use HTML tables at runtime. This is made possible with the removal of Internet Explorer support. 

There can be impact on custom CSS, since the markup now contains `<div>` elements instead of `<table>`, `<tr>`, `<td>`, etc., and the default CSS use `display: grid` and related CSS properties.

Although we do not recommend it, it is possible to change the default back to HTML tables with the following property:

```xml
<property
    as="xs:string"
    name="oxf.xforms.xbl.fr.grid.markup.*.*"
    value="html-table"/>
```

### User information when calling Orbeon Forms service APIs

If, with earlier versions of Orbeon Forms, you were calling service APIs and passing headers, they had to match the headers configured with the header-driven method. Starting Orbeon Forms 2022.1, the headers can only be the `Orbeon-*` headers.

See [Providing user information](/form-runner/api/authentication.md#providing-user-information) for more.

### `xf:load` handling of `xxf:show-progress`

Before Orbeon Forms 2022.1, when loading a `javascript:` URI, `xf:load` was ignoring the value of the `xxf:show-progress` attribute, and always behaving as if the attribute was set to `false`. Instead, starting with Orbeon Forms 2022.1, if you don't specify `xxf:show-progress`, it defaults to `false` for `javascript:` URIs, and to `true` otherwise.

This allows you to keep the progress indicator when using `xf:load` to run JavaScript that loads a page. Conversely, in the unlikely case you had some code doing a `<xf:load resource="javascript: …" xxf:show-progress="true"/>` but didn't want the progress indicator to be kept, then you will need to either remove the `xxf:show-progress="true"` or change the value of the attributes to `false`.