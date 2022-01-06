# Form Settings

## Introduction

Some settings apply to the entire form definition. You access these settings with the "Form Settings" wrench icon on the top right of Form Builder. The General Settings also show initially when creating a new form definition.

## General Settings

General settings allow you to set the following form definition metadata:

- Application name
- Form name
- Form title in the current language
- Form description in the current language

![General Settings](images/form-settings-general.png)

[SINCE Orbeon Forms 2020.1]

The description can optionally use rich text.

![Form description with rich text](images/form-settings-general-html.png)

## Form Options

[SINCE Orbeon Forms 2016.2]

Form options include options which apply to the entire form definition.

![Form Options](images/form-settings-form-options.png)

- Singleton Form
    - Enable or disable singleton form behavior. 
    - See [Singleton Form](/form-runner/advanced/singleton-form.md)
- Maximum Attachment Size per File
    - [SINCE Orbeon Forms 2017.1]
    - Use property: use the [`oxf.fr.detail.attachment.max-size` property](/configuration/properties/form-runner-attachments.md#maximum-attachment-size)
- Maximum Aggregate Attachment Size
    - [SINCE Orbeon Forms 2017.1]
    - Use property: use the [`oxf.fr.detail.attachment.max-size-aggregate` property](/configuration/properties/form-runner-attachments.md#maximum-aggregate-attachment-size)
    - Other: the maximum size allowed in bytes
- Allowed File Types
    - [SINCE Orbeon Forms 2017.1]
    - Use property: use the [`oxf.fr.detail.attachment.mediatypes` property](/configuration/properties/form-runner-attachments.md#allowed-file-types)
    - Other: the maximum size allowed in bytes
- Appearance of Control Labels
    - Use property: use the [`oxf.xforms.label.appearance` property](/xforms/controls/input.md#per-form-properties)
    - Inline: labels show inline above the control
    - Other: a space-separated list of mediatypes or wildcard mediatypes
- Use Placeholder for Text Fields and Text Areas
    - Labels show inline above the control for most fields.
    - For text, date, and time input fields, labels show as an HTML *placeholder* within the field when the field is empty.
    - For text areas, labels show as an HTML *placeholder* within the field when the field is empty. [SINCE Orbeon Forms 2017.1]
- Appearance of Control Hints
    - Use property: use the `oxf.xforms.hint.appearance` property
    - Inline: hints show inline below the control
    - Tooltips: hints show as tooltips upon mouseover
- Use Placeholder for Text Fields and Text Areas
    - Hints show inline or as tooltips upon mouseover.
    - For text, date, and time input fields, hints show as an HTML *placeholder* within the field when the field is empty.
    - For text areas, hints show as an HTML *placeholder* within the field when the field is empty. [SINCE Orbeon Forms 2017.1]
- Simple Data Migration
    - [SINCE Orbeon Forms 2018.2]
    - For details, see [Simple data migration](/form-runner/feature/versioning.md#simple-data-migration).
- Automatic Calculations Dependencies
    - [SINCE Orbeon Forms 2018.1]
    - For details, see [Automatic calculations dependencies](/form-runner/feature/automatic-calculations-dependencies.md). 

Here is how hints appear depending on the type of control they are associated with:

<img alt="" src="../form-runner/images/placeholder-and-inline-hints.png" width="932">

## Control Settings

[SINCE Orbeon Forms 2018.2]

This tab shows settings for specific controls. Those settings apply to all control on the form except when they are overridden by individual settings in the "Control Settings" dialog.

For example, you can set a "Digits After Decimal" setting global to the form, and then override it on specific number controls as needed. 

![Number Control Settings](images/form-settings-controls-number.png) 

![Date Control Settings](images/form-settings-controls-date.png)

See also the [blog post](https://blog.orbeon.com/2019/03/form-level-and-control-level-settings.html).

## View Options

[SINCE Orbeon Forms 2018.1] These options are now in a separate tab.

![View Options](images/form-settings-view-options.png)

### Browser page layout

- [SINCE Orbeon Forms 2019.2]
- Use property: use the [`oxf.fr.detail.html-page-layout` property](/configuration/properties/form-runner-detail-page.md#html-page-layout)
- Fixed width: the form sections and grids take a fixed and predefined width of approximately 940px for large displays (the layout becomes responsive for smaller displays sizes).
- Fluid width: the form sections and grids take the entire web browser's viewport size.

See also the [blog post](https://blog.orbeon.com/2019/11/new-layout-choices-for-pdf-and-browser.html).

### Wizard options

For details, see [Wizard View](/form-runner/feature/wizard-view.md).
 
- Wizard View:
    - Use property: use the [`oxf.fr.detail.view.appearance` property](/form-runner/feature/wizard-view.md#using-a-property)
    - Always or Never: enable or disable the wizard view for this form definition, no matter how the property is configured.
- Wizard Navigation Validation Mode
    - Form Builder setting [SINCE Orbeon Forms 2018.1]
    - Use property: use the [`oxf.xforms.xbl.fr.wizard.validate` property](/form-runner/feature/wizard-view.md#lax-validated-mode)
    - Free, Lax or Strict: use the given validation mode for this form definition, no matter how the property is configured.
- Wizard Subsections Navigation
    - Form Builder setting [SINCE Orbeon Forms 2018.1]
    - Use property: use the [`oxf.xforms.xbl.fr.wizard.subsections-nav` property](/form-runner/feature/wizard-view.md#subsections-navigation)
    - Always or Never: enable or disable subsection navigation for this form definition, no matter how the property is configured.
- Wizard Subsections Table of Contents
    - Form Builder setting [SINCE Orbeon Forms 2018.1]
    - Use property: use the [`oxf.xforms.xbl.fr.wizard.subsections-toc` property](/form-runner/feature/wizard-view.md#visibility-in-the-table-of-contents)
    - "Show subsections for the active section only", "Show subsections for all sections", "Don't show subsections": use the given setting for this form definition, no matter how the property is configured.

## PDF Options

[SINCE Orbeon Forms 2019.2]

![PDF Options](images/form-settings-pdf-options.png)

- PDF page orientation
    - Use property: use the [`oxf.fr.detail.rendered-page-orientation` property](/configuration/properties/form-runner-pdf.md#automatic-pdf-page-size-and-orientation)
    - Portrait: use the portrait (vertical) orientation.
    - Landscape: use the landscape (horizontal) orientation.
- PDF page size
    - Use property: use the [`oxf.fr.detail.rendered-page-size` property](/configuration/properties/form-runner-pdf.md#automatic-pdf-page-size-and-orientation)
    - Letter: US letter size.
    - A4: standard A4 size.
    - Legal: US legal size.
    
See also the [blog post](https://blog.orbeon.com/2019/11/new-layout-choices-for-pdf-and-browser.html).

## Formulas

[SINCE Orbeon Forms 2020.1]

![Readonly formula](images/form-settings-formulas.png)

- **Read-Only:** Boolean expression specifying whether the entire form is read-only (not editable).
    - If this field is left blank, then the form is editable.
    - Otherwise, it is editable only if the result of the Boolean expression is `false()`.

[SINCE Orbeon Forms 2021.1]

- **Calculations in readonly modes (Review, PDF)**:
   - Use property: use the [`oxf.fr.detail.readonly.disable-calculate` property](/configuration/properties/form-runner-detail-page.md#Calculations in readonlymodes)
   - Enable: Calculated Value formulas run in readonly modes.
   - Disable: Calculated Value formulas do not run in readonly modes.

Sometimes calculations should not run in readonly modes, such as the View page. This can now be configured at the form level.

## About this Form

![About this Form](images/form-settings-about.png)

### Versions

[SINCE Orbeon Forms 2018.2]

This area shows relevant Orbeon Forms versions:

- "Created with Version": version with which this form definition was created.
    - *NOTE: This is blank for forms created prior to Orbeon Forms 2018.1.*
- "Updated with Versions": versions with which this form definition was updated This is updated:
    - when saving the form definition
    - when upgrading the form definition from the Form Runner Home page.
- "Current Version": the current Orbeon Forms version.

### Form Statistics

Form statistics show counts of various form elements.

## See also

- [Wizard View](/form-runner/feature/wizard-view.md)
- [Simple data migration](/form-runner/feature/versioning.md#simple-data-migration)
- [Automatic calculations dependencies](/form-runner/feature/automatic-calculations-dependencies.md)
- Blog posts
    - [New layout choices for PDF and browser views](https://blog.orbeon.com/2019/11/new-layout-choices-for-pdf-and-browser.html)
    - [Improved constraints on attachments uploads](https://blog.orbeon.com/2017/04/improved-constraints-on-attachments.html)
    - [Form-level and control-level settings](https://blog.orbeon.com/2019/03/form-level-and-control-level-settings.html)