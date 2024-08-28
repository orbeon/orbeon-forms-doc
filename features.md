# Index of Features

## Purpose

The purpose of this page is to index features, their documentation and [blog posts](https://blog.orbeon.com/) in a central location so that you can get an idea of what Orbeon Forms offers at a glance.

## See also

- [Orbeon website](https://www.orbeon.com/)
- [Orbeon blog](https://blog.orbeon.com/)

## Status

As of 2016-04-19 this page is still being updated.

## List of Orbeon Forms features

### Form Builder and Form Runner

- Security features
    - Protecting against attacks: [doc](configuration/advanced/security.md)
    - Content-Security-Policy header: [doc](configuration/advanced/content-security-policy.md), [blog post](https://blog.orbeon.com/2018/08/improving-security-with-content.html)
    - Field-level encryption: [doc](form-builder/field-level-encryption.md)
- Inserting and reordering grid rows: [blog post](https://blog.orbeon.com/2013/11/inserting-and-reordering-grid-rows.html)
- Repeated grids and sections
    - Repeat settings: [doc](form-builder/container-settings.md)
    - Repeated grids: [doc](form-builder/repeated-grids.md), [older blog post](https://blog.orbeon.com/2012/04/support-for-repeats-lands-in-form.html)
    - Repeated sections: [blog post](https://blog.orbeon.com/2014/01/repeated-sections.html)
    - Options for new repeat repetitions: [blog post](https://blog.orbeon.com/2015/10/repeated-grids-and-sections-just-got.html)
    - Minimal appearances of repeated grids and sections: [blog post](https://blog.orbeon.com/2015/12/leaner-repeated-sections-and-grids.html)
    - Frozen repetitions
- Section Templates:
    - main feature: [doc](form-builder/section-templates.md)
    - merging: [doc](form-builder/section-templates.md#merging-section-templates)
- Singleton forms: [doc](form-runner/advanced/singleton-form.md)
- Versioning of form definitions: [doc](/form-runner/feature/versioning.md), [blog post about concept](https://blog.orbeon.com/2014/02/form-versioning.html), [blog post about publish options](https://blog.orbeon.com/2015/01/choosing-best-versioning-option-when.html)
    - overwriting of existing version: [doc](form-builder/publishing.md#versioning)
    - associating a comment with a given form version: [doc](form-builder/publishing.md#versioning), [blog post](https://blog.orbeon.com/2016/09/versioning-comments.html)
    - simple data migration: [doc](/form-runner/feature/simple-data-migration.md), [blog post](https://blog.orbeon.com/2018/09/simple-data-migration.html)
- Viewing data revision history: [doc](/form-runner/feature/revision-history.md) 
- Form field validation: [doc](form-builder/validation.md), [blog post](https://blog.orbeon.com/2013/07/enhanced-validation-in-form-builder-and.html)
    - required fields (also via formula, see [blog post](https://blog.orbeon.com/2014/09/control-required-values-with-formulas.html))
    - whitespace trimming: [doc](form-builder/validation.md#trimming-leading-and-trailing-spaces), [blog post](https://blog.orbeon.com/2016/02/required-fields-more-subtle-than-you.html)
    - data types such as string, number, date, etc.
    - multiple constraints with formulas
    - common constraints: [doc](form-builder/validation.md#commonconstraints), [blog post](https://blog.orbeon.com/2015/07/how-common-constraints-work.html)
        - minimum/maximum length
        - positive, positive or zero, negative or zero, or negative value
        - maximum fractional digits
        - attachment sizes and file types
    - errors, warnings, and informational validations
    - custom alert messages per validation
- Access control
    - Owner / group permissions: [doc](form-runner/access-control/owner-group.md), [blog post](https://blog.orbeon.com/2013/09/ownergroup-based-permissions-aka-see.html)
    - Organizations: [doc](form-runner/access-control/organization.md)
- Persistence (databases)
    - persistence API: [doc](form-runner/api/persistence/README.md)
    - persistence implementations: [doc](form-runner/persistence/db-support.md)
    - relational database support: [doc](form-runner/persistence/relational-db.md), [blog post about new indexes](https://blog.orbeon.com/2016/06/new-indexes-boost-performance-with.html)
- Datasets: [doc](form-runner/feature/datasets.md), [blog post](https://blog.orbeon.com/2017/01/datasets.html)
- Multitenancy: [doc](/form-runner/feature/multitenancy.md)
- User menu: [doc](/form-runner/access-control/login-logout)

### Form Builder

- 12-column layout: [doc](form-builder/form-area.md#the-12-column-layout), [blog post](https://blog.orbeon.com/2018/05/the-12-column-layout.html)
    - Drag & drop of cell boundaries: [blog post](https://blog.orbeon.com/2018/10/resizing-cells-with-drag-drop-in-form.html)
- Summary page: [doc](form-builder/summary-page.md)
- Toolbox:
    - features: [doc](form-builder/toolbox.md)
    - configurability: [doc](form-builder/metadata.md)
    - XBL components: [doc](xforms/xbl/README.md)
    - undo and redo: [doc](form-builder/undo-redo.md), [blog post](https://blog.orbeon.com/2017/12/new-orbeon-forms-20172-feature-undo-and.html)
    - cut, copy and paste: [doc](form-builder/cut-copy-paste.md)
- Form Settings: [doc](form-builder/form-settings.md)
    - General Settings
    - HTML form description
    - Form Options: [doc](form-builder/form-settings.md#form-options)
        - Singleton forms: [doc](form-runner/advanced/singleton-form.md)
        - Wizard view: [doc](form-runner/feature/wizard-view.md)
    - Control Settings
    - View Options
    - PDF Options
    - About this Form
- Control Settings: [doc](form-builder/control-settings.md)
    - control name
    - Summary page options
    - custom CSS classes
    - easy switching of control appearances: [blog post](https://blog.orbeon.com/2015/06/how-new-form-builder-appearance.html)
    - validation: [doc](form-builder/validation.md)
    - formulas: [doc](form-builder/formulas.md)
    - help text (plain text and rich text)
        - appearance of help messages: [blog post](https://blog.orbeon.com/2014/01/improving-how-we-show-help-messages.html)
- Choices editor: [doc](form-builder/itemset-editor.md)
    - Hints for checkboxes and radio buttons: [blog post](https://blog.orbeon.com/2014/02/hints-for-checkboxes-and-radio-buttons.html)
- Section Settings: [doc](form-builder/section-settings.md)
    - whether section is collapsible
    - whether section is initially collapsed
- Publishing a form definition: [doc](form-builder/publishing.md)
- Explanation text: [blog post](https://blog.orbeon.com/2015/04/adding-explanatory-text-to-your-forms.html)
- Extension API: [doc](form-builder/extension-api.md)
- Access control for editing forms: [doc](form-runner/access-control/README.md)
- Internationalization (i18n) / localization (l10n): [doc](form-builder/localization.md)
- Services and actions
    - HTTP Services: [doc](form-builder/http-services.md)
        - JSON support (for reading only): [doc](xforms/submission-standard.md#json-support)
        - setting URL parameters
    - Database Services: [doc](form-builder/database-services.md)
    - Actions: [doc](form-builder/actions.md)
- XML Schema Support: [doc](form-builder/xml-schema-support.md)
- Source code editor: [doc](form-runner/component/source-code-editor.md)whether section is collapsible
- Extension API: [doc](form-builder/extension-api.md)
- Basic keyboard shortcuts
    - Cut/Copy/Paste: [doc](form-builder/cut-copy-paste.md#keyboard-shortcuts)
    - Undo/Redo: [doc](form-builder/undo-redo.md#keyboard-shortcuts)
    - Save button: [doc](form-builder/form-editor.md#keyboard-shortcuts)

### Form Runner

- Standard look & feel
- Automatic calculations dependencies: [doc](form-runner/feature/automatic-calculations-dependencies.md), [blog post](https://blog.orbeon.com/2018/10/automatic-calculation-dependencies.html)
- Custom XBL components: [doc](xforms/xbl/README.md)
- Summary Page: [blog post](https://blog.orbeon.com/2014/06/the-form-builder-summary-page-and-form.html)
    - search
- Detail Page
    - Responsive design for mobile devices such as smartphones: [doc](form-runner/feature/responsive-design.md), [blog](https://blog.orbeon.com/2015/08/responsive-design.html)
        - show numeric keypad/numbers and punctuation" keyboard pane when possible: [doc](form-runner/component/number.md#mobile-support), [blog post](https://blog.orbeon.com/2016/01/better-numeric-input-on-mobile.html)
    - Review mode (printable)
    - Wizard view: [doc](form-runner/feature/wizard-view.md), [introduction blog post](https://blog.orbeon.com/2012/12/form-runner-wizard-view.html)
        - validated mode: [blog post](https://blog.orbeon.com/2015/03/new-wizard-validated-mode.html), [doc](feature/wizard-view.md#validated-mode)
        - highlighting of sections in error/accessible sections
        - buttons appearing only on the wizard's last page
        - status information for each section
        - subsection navigation
        - optional separate table of contents
- File scan API: [doc](form-runner/api/other/file-scan-api.md) 
- Forms Admin Page: [doc](form-runner/feature/forms-admin-page.md), [blog post](https://blog.orbeon.com/2014/06/the-form-builder-summary-page-and-form.html)
- Published Forms Page: [doc](form-runner/feature/published-forms-page.md), [blog post](https://blog.orbeon.com/2014/06/the-form-builder-summary-page-and-form.html)
- Landing page: [doc](form-runner/feature/landing-page.md)
- Excel and XML Import: [doc](/form-runner/feature/excel-xml-import.md)
- Excel and XML Export: [doc](/form-runner/feature/excel-xml-export.md)
- Buttons and Processes: [doc](form-runner/advanced/buttons-and-processes/README.md), [blog post](https://blog.orbeon.com/2013/04/more-powerful-buttons.html)
- Autosave: [doc](form-runner/persistence/autosave.md), [blog post](https://blog.orbeon.com/2013/10/autosave.html)
- Export
    - [Export of form definitions and form data](form-runner/feature/exporting-form-definitions-and-form-data.md)
    - Excel export
    - XML export
- [Purging historical data](form-runner/feature/purging-historical-data.md) 
- PDF
    - Production: [doc](form-runner/feature/pdf-production.md)
        - Automatic
        - Template-based: [doc](form-runner/feature/pdf-templates.md)
            - multiple PDF templates
    - Automatic highlighting of links [blog post](https://blog.orbeon.com/2015/04/automatic-web-links-in-pdf-files.html)
    - Customizable file name: [doc](configuration/properties/form-runner/form-runner-detail-page.md#custom-pdf-filename)
    - TIFF production: [doc](form-runner/feature/tiff-production.md)
    - Ability to send to services: [blog post](https://blog.orbeon.com/2016/08/submitting-pdf-file-to-external-service.html)
- Validation
    - as-you-type validation
    - explicit validation: [doc](configuration/properties/form-runner/form-runner-detail-page.md#validation-mode)
    - centralized error summary showing currently relevant errors: [component doc](form-runner/component/error-summary.md), [blog post on warnings/infos enhancements](https://blog.orbeon.com/2013/07/enhanced-validation-in-form-builder-and.html)
- Sending emails
    - controlling which attachments are included: [Form Builder doc](form-builder/control-settings.md#basic-options), [Form Runner doc](configuration/properties/form-runner/form-runner-detail-page/form-runner-email#attachment-properties)
    - controlling To, Cc, Bcc, From: [doc](form-builder/control-settings.md), [blog post](https://blog.orbeon.com/2017/05/more-flexible-email-senders-and.html)
    - Email templates: [doc](https://doc.orbeon.com/form-builder/advanced/email-settings), [blog post](https://blog.orbeon.com/2018/11/email-templates.html)
- Captcha: [properties doc](configuration/properties/form-runner/form-runner-detail-page.md#captcha), [component doc](form-runner/component/captcha.md), [blog post](https://blog.orbeon.com/2011/12/stop-spammer-by-adding-captcha-to-your.html)
- Appearance of repeated sections: [properties doc](configuration/properties/form-runner.md#appearance-of-repeated-sections), [component doc](form-runner/component/grid.md#repeated-mode)
- Appearance of repeated grids: [properties doc](configuration/properties/form-runner.md#appearance-of-grids-sections), [component doc](form-runner/component/section.md#repeated-mode)
- Function library: [doc](xforms/xpath/extension-form-runner.md), [blog post](https://blog.orbeon.com/2016/08/a-stable-function-library-for-form.html)
- Repeated content synchronization: [doc](/form-builder/synchronize-repeated-content.md)
- Grids
    - HTML tables-free layout: [blog post](https://blog.orbeon.com/2016/06/saying-farewell-to-html-tables.html))
- Services and APIs
    - [Duplicate form data API](form-runner/api/other/duplicate-form-data.md)
    - [Generate XML Schema API](form-runner/api/other/duplicate-form-data.md)
    - [List form data attachments API](form-runner/api/persistence/list-form-data-attachments.md)
    - [Publish form definition API](form-runner/api/other/publish.md)
    - [Run form in the background API](form-runner/api/other/run-form-background.md)

### Form controls

- Text controls
    - Text field
    - Plain text area
    - Formatted text area
    - Password field
- Utility controls
    - Explanatory text
    - Calculated value
    - Hidden field: [doc](/form-runner/component/hidden.md), [blog post](https://blog.orbeon.com/2019/02/hidden-fields.html)
- Typed controls
    - Number
        - control rounding when formatting/capturing data
        - left/right alignment
    - Email address
    - Currency
    - US phone number
    - US state
- Date and time controls
    - Date, time, date and time
        - date picker
        - option to exclude dates from the date picker: [doc](/form-builder/validation.md#dates-to-exclude-constraint) 
        - date control: [doc](/form-runner/component/date.md)
        - time control: [doc](/form-runner/component/time.md)
    - Dropdown date
    - Fields date
- Selection controls [doc](form-builder/toolbox.md#selection-controls)
    - Dropdown menu
    - Radio buttons
    - Radio buttons with "Other": [doc](form-runner/component/open-select1.md)
    - Checkboxes
    - Scrollable checkboxes
    - Yes/No answer: [doc](form-runner/component/yesno-input.md)
    - Single checkbox: [doc](form-runner/component/checkbox-input.md)
    - Dynamic data dropdown
    - Autocomplete
- Attachments
    - File attachment
    - Image attachment
    - Static image
    - Static video
    - Handwritten signature: [doc](form-runner/component/handwritten-signature.md)
    - Image annotation: [blog post](https://blog.orbeon.com/2013/08/new-image-annotation-control.html)
    - Control maximum size and file types: [blog post](https://blog.orbeon.com/2017/04/improved-constraints-on-attachments.html)
- Buttons
    - Button
    - Link button
- Other
    - Character counter: [doc](form-runner/component/character-counter.md)
    - Custom components: [doc](xforms/xbl/README.md)
        - support JavaScript Companion Classes: [doc](xforms/xbl/javascript.md)

### Databases

- Database support: [doc](form-runner/persistence/db-support.md)
- SQL Server support in Orbeon Forms: [blog post](https://blog.orbeon.com/2014/05/sql-server-support-in-orbeon-forms.html)
- PostgreSQL support in Orbeon Forms: [blog post](https://blog.orbeon.com/2014/12/postgresql-support-in-orbeon-forms.html)

### Form handling

- Session heartbeat: [doc](contributors/state-handling.md)
- Browser back/forward button support: [doc](contributors/state-handling.md)

### Embedding

- Server side Embedding: [doc](form-runner/link-embed/java-api.md), [blog post](https://blog.orbeon.com/2014/09/embedding-support-in-orbeon-forms-47.html), [Form Builder embedding blog post](https://blog.orbeon.com/2017/02/form-builder-embedding.html)
- Liferay proxy portlet: [doc](form-runner/link-embed/liferay-proxy-portlet.md)
- Liferay full portlet: [doc](form-runner/link-embed/liferay-full-portlet.md)

### Performance

- Limiter filter to limit the number of concurrent form requests: [doc](configuration/advanced/limiter-filter.md)
- Internal service requests: [blog post](https://blog.orbeon.com/2015/01/saying-goodbye-to-internal-http.html)

### Misc

- Loading indicator and spinners: [blog post](https://blog.orbeon.com/2016/04/how-do-you-tell-users-something-is.html)
- Namespaced jQuery to avoid conflicts with other jQuery versions
- Run modes: [doc](configuration/advanced/run-modes.md), [blog](https://blog.orbeon.com/2012/05/run-modes.html)

