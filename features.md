# Index of Features

<!-- toc -->

## Purpose

The purpose of this page is to index features, their documentation and [blog posts](http://blog.orbeon.com/) in a central location so that you can get an idea of what Orbeon Forms offers at a glance.

## See also

- [Orbeon web site](http://www.orbeon.com/)
- [Orbeon blog](http://blog.orbeon.com/)

## Status

As of 2015-10-22 this page is still being updated.

## List of Orbeon Forms features

### Form Builder and Form Runner

- Inserting and reordering grid rows: [blog post](http://blog.orbeon.com/2013/11/inserting-and-reordering-grid-rows.html)
- Repeated grids and sections
    - Repeated grids: [doc](form-builder/repeated-grids.html), [older blog post](http://blog.orbeon.com/2012/04/support-for-repeats-lands-in-form.html)
    - Repeated sections: [blog post](http://blog.orbeon.com/2014/01/repeated-sections.html)
    - Options for new repeat iterations: [blog post](http://blog.orbeon.com/2015/10/repeated-grids-and-sections-just-got.html)
- Section Templates: [doc](FIXME Form Builder ~ Section Templates)
- Singleton forms: [doc](FIXME Form Runner ~ Singleton Form)
- Versioning of form definitions: [blog post about concept](http://blog.orbeon.com/2014/02/form-versioning.html), [blog post about publish options](http://blog.orbeon.com/2015/01/choosing-best-versioning-option-when.html)
- Form field validation: [doc](FIXME Form Builder ~ Validation), [blog post](http://blog.orbeon.com/2013/07/enhanced-validation-in-form-builder-and.html)
    - required fields (also via formula, see [blog post](http://blog.orbeon.com/2014/09/control-required-values-with-formulas.html))
    - data types such as string, number, date, etc.
    - multiple constraints with formulas
    - common constraints such as minimum length and maximum length: [doc](FIXME Form Builder ~ Validation#common-constraints), [blog post](http://blog.orbeon.com/2015/07/how-common-constraints-work.html)
    - errors, warnings, and informational validations
    - custom alert messages per validation
- Access control
    - Owner / group permissions: [doc](FIXME Form Runner ~ Access Control ~ Owner Group), [blog post](http://blog.orbeon.com/2013/09/ownergroup-based-permissions-aka-see.html)
- Persistence (databases)
    - persistence API: [doc](FIXME Form Runner ~ APIs ~ Persistence)
    - persistence implementations: [doc](FIXME Orbeon Forms Features ~ Database Support)

### Form Builder

- Summary page: [doc](FIXME Form Builder ~ Summary Page)
- Toolbox:
    - features: [doc](form-builder/toolbox.md)
    - configurability: [doc](FIXME Form Builder ~ Toolbox ~ Metadata)
    - XBL components: [doc](FIXME XForms ~ XBL)
- Control Settings: [doc](FIXME Form Builder ~ Control Settings)
    - control name
    - Summary page options
    - custom CSS classes
    - easy switching of control appearances: [blog post](http://blog.orbeon.com/2015/06/how-new-form-builder-appearance.html)
    - validation: [doc](FIXME Form Builder ~ Validation)
    - formulas: [doc](FIXME Form Builder ~ Formulas)
    - help text (plain text and rich text)
        - appearance of help messages: [blog post](http://blog.orbeon.com/2014/01/improving-how-we-show-help-messages.html)
- Itemset Editor: [doc](FIXME Form Builder ~ Itemset Editor)
    - Hints for checkboxes and radio buttons: [blog post](http://blog.orbeon.com/2014/02/hints-for-checkboxes-and-radio-buttons.html)
- Section Settings: [doc](FIXME Form Builder ~ Section Settings)
- Publishing a form definition: [doc](FIXME Form Builder ~ Publishing)
- Explanation text: [blog post](http://blog.orbeon.com/2015/04/adding-explanatory-text-to-your-forms.html)
- Extension API: [doc](FIXME Form Builder ~ Extension API)
- Access control for editing forms: [doc](FIXME Form Runner ~ Access Control ~ Editing Forms)
- Internationalization (i18n) / localization (l10n): [doc](FIXME Form Builder ~ Creating Localized Forms)
- Services and actions
    - HTTP Services: [doc](FIXME Form Builder ~ HTTP Services)
    - Database Services: [doc](FIXME Form Builder ~ Database Services)
    - Actions: [doc](FIXME Form Builder ~ Actions)
- XML Schema Support: [doc](FIXME Form Builder ~ XML Schema Support)
- Source code editor: [doc](FIXME Form Builder ~ Editing the Source Code of the Form)
- Extension API: [doc](FIXME Form Builder ~ Extension API)

### Form Runner

- Standard look & feel
- Custom XBL components: [doc](FIXME XForms ~ XBL)
- Summary Page: [blog post](http://blog.orbeon.com/2014/06/the-form-builder-summary-page-and-form.html)
    - search
- Detail Page
    - Responsive design for mobile devices such as smartphones: [doc](FIXME Form Runner ~ Responsive Design), [blog](http://blog.orbeon.com/2015/08/responsive-design.html)
    - Review mode (printable)
    - Wizard view: [doc](FIXME Form Runner ~ Wizard View), [introduction blog post](http://blog.orbeon.com/2012/12/form-runner-wizard-view.html)
        - validated mode: [blog post](http://blog.orbeon.com/2015/03/new-wizard-validated-mode.html)
- Home Page: [doc](FIXME Form Runner ~ Home Page), [blog post](http://blog.orbeon.com/2014/06/the-form-builder-summary-page-and-form.html)
- Buttons and Processes: [doc](FIXME Form-Runner-~-Buttons-and-Processes), [blog post](http://blog.orbeon.com/2013/04/more-powerful-buttons.html)
- Autosave: [doc](FIXME Form-Runner ~ Autosave), [blog post](http://blog.orbeon.com/2013/10/autosave.html)
- PDF
    - Production: [doc](FIXME Form Builder ~ PDF Production)
        - Automatic
        - Template-based: [doc](FIXME Form Builder ~ PDF Production ~ PDF Templates)
    - Automatic highlighting of links [blog post](http://blog.orbeon.com/2015/04/automatic-web-links-in-pdf-files.html)
    - customizable file name: [doc](FIXME Form Runner ~ Configuration properties#custom-pdf-filename)
    - TIFF production: [doc](FIXME Form Runner ~ TIFF Production)
- Validation
    - as-you-type validation
    - centralized error summary showing currently relevant errors: [XBL component doc](FIXME Form Runner ~ XBL Components ~ Error Summary), [blog post on warnings/infos enhancements](http://blog.orbeon.com/2013/07/enhanced-validation-in-form-builder-and.html)
- Sending emails
- Captcha: [doc|Form Runner ~ Configuration properties#captcha]], [[XBL doc](FIXME Form Runner ~ XBL Components ~ Captcha), [blog post](http://blog.orbeon.com/2011/12/stop-spammer-by-adding-captcha-to-your.html)

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
    - Character counter: [doc](FIXME Form Runner ~ XBL Components ~ Character Counter)
    - Custom components: [doc](FIXME XForms ~ XBL)

### Databases

- Database support: [doc](FIXME Orbeon-Forms-Features-~-Database-Support)
- SQL Server support in Orbeon Forms: [blog post](http://blog.orbeon.com/2014/05/sql-server-support-in-orbeon-forms.html)
- PostgreSQL support in Orbeon Forms: [blog post](http://blog.orbeon.com/2014/12/postgresql-support-in-orbeon-forms.html)

### Form handling

- Session heartbeat: [doc](FIXME Contributors ~ Internals ~ State Handling)
- Browser back/forward button support: [doc](FIXME Contributors ~ Internals ~ State Handling)

### Embedding

- Server side Embedding: [doc](FIXME Form Runner ~ APIs ~ Server-side Embedding), [blog post](http://blog.orbeon.com/2014/09/embedding-support-in-orbeon-forms-47.html)
- Liferay proxy portlet: [doc](FIXME Form Runner ~ Portal ~ Liferay Proxy Portlet Guide)
- Liferay full portlet: [doc](FIXME Form-Runner-~-Portal-~-Full-Portlet-Guide)

### Performance

- Limiter filter to limit the number of concurrent form requests: [doc](FIXME Installation ~ Limiter Filter)
- Internal service requests: [blog post](http://blog.orbeon.com/2015/01/saying-goodbye-to-internal-http.html)

### Misc

- Namespaced jQuery to avoid conflicts with other jQuery versions
- Run modes: [doc](FIXME Installation ~ Run Modes), [blog](http://blog.orbeon.com/2012/05/run-modes.html)

