# Index of Features

<!-- toc -->

## Purpose

The purpose of this page is to index features, their documentation and [blog posts](http://blog.orbeon.com/) in a central location so that you can get an idea of what Orbeon Forms offers at a glance.

## See also

- [Orbeon web site](http://www.orbeon.com/)
- [Orbeon blog](http://blog.orbeon.com/)

## Status

As of 2016-02-11 this page is still being updated.

## List of Orbeon Forms features

### Form Builder and Form Runner

- Inserting and reordering grid rows: [blog post](http://blog.orbeon.com/2013/11/inserting-and-reordering-grid-rows.html)
- Repeated grids and sections
    - Repeated grids: [doc](form-builder/repeated-grids.md), [older blog post](http://blog.orbeon.com/2012/04/support-for-repeats-lands-in-form.html)
    - Repeated sections: [blog post](http://blog.orbeon.com/2014/01/repeated-sections.html)
    - Options for new repeat iterations: [blog post](http://blog.orbeon.com/2015/10/repeated-grids-and-sections-just-got.html)
    - Mimimal appearances of repeated grids and sections: [blog post](http://blog.orbeon.com/2015/12/leaner-repeated-sections-and-grids.html)
- Section Templates: [doc](form-builder/section-templates.md)
- Singleton forms: [doc](form-runner/advanced/singleton-form.md)
- Versioning of form definitions: [blog post about concept](http://blog.orbeon.com/2014/02/form-versioning.html), [blog post about publish options](http://blog.orbeon.com/2015/01/choosing-best-versioning-option-when.html)
- Form field validation: [doc](form-builder/validation.md), [blog post](http://blog.orbeon.com/2013/07/enhanced-validation-in-form-builder-and.html)
    - required fields (also via formula, see [blog post](http://blog.orbeon.com/2014/09/control-required-values-with-formulas.html))
    - white space trimming: [doc](form-builder/validation.md#trimming-leading-and-trailing-spaces), [blog post](http://blog.orbeon.com/2016/02/required-fields-more-subtle-than-you.html)
    - data types such as string, number, date, etc.
    - multiple constraints with formulas
    - common constraints such as minimum length and maximum length: [doc](form-builder/validation.md#commonconstraints), [blog post](http://blog.orbeon.com/2015/07/how-common-constraints-work.html)
    - errors, warnings, and informational validations
    - custom alert messages per validation
- Access control
    - Owner / group permissions: [doc](form-runner/access-control/owner-group.md), [blog post](http://blog.orbeon.com/2013/09/ownergroup-based-permissions-aka-see.html)
- Persistence (databases)
    - persistence API: [doc](form-runner/api/persistence/README.md)
    - persistence implementations: [doc](form-runner/persistence/db-support.md)

### Form Builder

- Summary page: [doc](form-builder/summary-page.md)
- Toolbox:
    - features: [doc](form-builder/toolbox.md)
    - configurability: [doc](form-builder/metadata.md)
    - XBL components: [doc](xforms/xbl/README.md)
- Control Settings: [doc](form-builder/control-settings.md)
    - control name
    - Summary page options
    - custom CSS classes
    - easy switching of control appearances: [blog post](http://blog.orbeon.com/2015/06/how-new-form-builder-appearance.html)
    - validation: [doc](form-builder/images/validation.png)
    - formulas: [doc](form-builder/formulas.md)
    - help text (plain text and rich text)
        - appearance of help messages: [blog post](http://blog.orbeon.com/2014/01/improving-how-we-show-help-messages.html)
- Itemset Editor: [doc](form-builder/itemset-editor.md)
    - Hints for checkboxes and radio buttons: [blog post](http://blog.orbeon.com/2014/02/hints-for-checkboxes-and-radio-buttons.html)
- Section Settings: [doc](form-builder/section-settings.md)
- Publishing a form definition: [doc](form-builder/publishing.md)
- Explanation text: [blog post](http://blog.orbeon.com/2015/04/adding-explanatory-text-to-your-forms.html)
- Extension API: [doc](form-builder/extension-api.md)
- Access control for editing forms: [doc](form-runner/access-control/README.md)
- Internationalization (i18n) / localization (l10n): [doc](form-builder/localization.md)
- Services and actions
    - HTTP Services: [doc](form-builder/http-services.md)
    - Database Services: [doc](form-builder/database-services.md
    - Actions: [doc](form-builder/actions.md)
- XML Schema Support: [doc](form-builder/xml-schema-support.md)
- Source code editor: [doc](form-runner/component/source-code-editor.md)
- Extension API: [doc](form-builder/extension-api.md)

### Form Runner

- Standard look & feel
- Custom XBL components: [doc](xforms/xbl/README.md)
- Summary Page: [blog post](http://blog.orbeon.com/2014/06/the-form-builder-summary-page-and-form.html)
    - search
- Detail Page
    - Responsive design for mobile devices such as smartphones: [doc](form-runner/feature/responsive-design.md), [blog](http://blog.orbeon.com/2015/08/responsive-design.html)
    - Review mode (printable)
    - Wizard view: [doc](form-runner/feature/wizard-view.md), [introduction blog post](http://blog.orbeon.com/2012/12/form-runner-wizard-view.html)
        - validated mode: [blog post](http://blog.orbeon.com/2015/03/new-wizard-validated-mode.html)
- Home Page: [doc](form-runner/feature/home-page.md), [blog post](http://blog.orbeon.com/2014/06/the-form-builder-summary-page-and-form.html)
- Buttons and Processes: [doc](form-runner/advanced/buttons-and-processes/README.md), [blog post](http://blog.orbeon.com/2013/04/more-powerful-buttons.html)
- Autosave: [doc](form-runner/persistence/autosave.md), [blog post](http://blog.orbeon.com/2013/10/autosave.html)
- PDF
    - Production: [doc](form-builder/pdf-production.md)
        - Automatic
        - Template-based: [doc](form-builder/pdf-templates.md)
    - Automatic highlighting of links [blog post](http://blog.orbeon.com/2015/04/automatic-web-links-in-pdf-files.html)
    - customizable file name: [doc](configuration/properties/form-runner.md#custom-pdf-filename)
    - TIFF production: [doc](form-runner/feature/tiff-production.md)
- Validation
    - as-you-type validation
    - centralized error summary showing currently relevant errors: [component doc](form-runner/component/images/xbl-error-summary-errors.png), [blog post on warnings/infos enhancements](http://blog.orbeon.com/2013/07/enhanced-validation-in-form-builder-and.html)
- Sending emails
- Captcha: [properties doc](configuration/properties/form-runner.md#captcha), [component doc](form-runner/component/captcha.md), [blog post](http://blog.orbeon.com/2011/12/stop-spammer-by-adding-captcha-to-your.html)
- Appearance of repeated sections: [properties doc](http://doc.orbeon.com/configuration/properties/form-runner.html#appearance-of-repeated-sections), [component doc](http://doc.orbeon.com/form-runner/component/grid.html#repeated-mode)
- Appearance of repeated grids: [properties doc](http://doc.orbeon.com/configuration/properties/form-runner.html#appearance-of-grids-sections), [component doc](http://doc.orbeon.com/form-runner/component/section.html#repeated-mode)

### Form controls

- Text controls
    - Input field
    - Textarea
    - Text output
    - Password field
    - Formatted text (rich text)
    - Explanation text
- Typed controls
    - Email field
    - US phone number field
    - Number field
    - Currency field
    - US state
- Date and time controls
    - Date, time, date and time
    - Dropdown date, fields date
- Selection controls
    - Dropdown menu
    - Radio buttons
    - Checkboxes
    - Scrollable list
    - Boolean input
    - Scrollable checkboxes
    - Dynamic data dropdown
    - Autocomplete
- Attachments
    - Static image
    - Image attachment
    - File attachment
    - Image annotation: [blog post](http://blog.orbeon.com/2013/08/new-image-annotation-control.html)
- Buttons
    - Button
    - Link button
- Other
    - Character counter: [doc](form-runner/component/character-counter.md)
    - Custom components: [doc](xforms/xbl/README.md)

### Databases

- Database support: [doc](form-runner/persistence/db-support.md)
- SQL Server support in Orbeon Forms: [blog post](http://blog.orbeon.com/2014/05/sql-server-support-in-orbeon-forms.html)
- PostgreSQL support in Orbeon Forms: [blog post](http://blog.orbeon.com/2014/12/postgresql-support-in-orbeon-forms.html)

### Form handling

- Session heartbeat: [doc](contributors/state-handling.md)
- Browser back/forward button support: [doc](contributors/state-handling.md)

### Embedding

- Server side Embedding: [doc](form-runner/link-embed/java-api.md), [blog post](http://blog.orbeon.com/2014/09/embedding-support-in-orbeon-forms-47.html)
- Liferay proxy portlet: [doc](form-runner/link-embed/liferay-proxy-portlet.md)
- Liferay full portlet: [doc](form-runner/link-embed/liferay-full-portlet.md)

### Performance

- Limiter filter to limit the number of concurrent form requests: [doc](configuration/advanced/limiter-filter.md)
- Internal service requests: [blog post](http://blog.orbeon.com/2015/01/saying-goodbye-to-internal-http.html)

### Misc

- Namespaced jQuery to avoid conflicts with other jQuery versions
- Run modes: [doc](configuration/advanced/run-modes.md
), [blog](http://blog.orbeon.com/2012/05/run-modes.html)

