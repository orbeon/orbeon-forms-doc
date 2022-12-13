# Orbeon Forms 2022.1 (Draft)

__Friday, December 30, 2021__

Today we released Orbeon Forms 2022.1! This release introduces new features and bug-fixes.

## New features

### Major new features

- Better landing page ([\#1410](https://github.com/orbeon/orbeon-forms/issues/1410))
- Form Builder: Drag and Drop of controls to grid cells ([\#5252](https://github.com/orbeon/orbeon-forms/issues/5252))
- Per-app and global permissions configuration ([\#1860](https://github.com/orbeon/orbeon-forms/issues/1860))
- Image attachment: ability to resize images upon upload ([\#3061](https://github.com/orbeon/orbeon-forms/issues/3061))
- File Scan API v2 ([\#4967](https://github.com/orbeon/orbeon-forms/issues/4967))
- Ability to use variables for min, max, etc. ([\#309](https://github.com/orbeon/orbeon-forms/issues/309))
- Admin page: add Selected badge and align ([\#5426](https://github.com/orbeon/orbeon-forms/issues/5426))
- Summary: Button to select all drafts ([\#3582](https://github.com/orbeon/orbeon-forms/issues/3582))
- Improved simple data migration ([\#5415](https://github.com/orbeon/orbeon-forms/issues/5415))
- FR: send() to support multipart for attachments ([\#788](https://github.com/orbeon/orbeon-forms/issues/788))
- New list permission to protect Summary pages ([\#5397](https://github.com/orbeon/orbeon-forms/issues/5397))
- Support multiple email templates per form ([\#3912](https://github.com/orbeon/orbeon-forms/issues/3912))
- Replace flying saucer with open html to pdf ([\#5342](https://github.com/orbeon/orbeon-forms/issues/5342))
- Separate the Home page and the Admin page ([\#2753](https://github.com/orbeon/orbeon-forms/issues/2753))

### Other new features

- Use of CSS grids by default
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

- `xf:input` bound to an `xs:date`, `xs:time`, or `xs:dateTime` node don't work and show a plain input field instead.
- `fr:date`, which worked outside of Form Runner with Orbeon Forms 2021.1, currently doesn't work either.

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

Form Builder already used [CSS grids](https://developer.mozilla.org/en-US/docs/Web/CSS/CSS_Grid_Layout) for rendering the form being edited. Starting with Orbeon Forms 2022.1, by default, Orbeon Forms also uses CSS grids for all grids at runtime as well. Previously, the default was to use HTML tables at runtime. This is made possible with the drop of Internet Explorer support. 

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