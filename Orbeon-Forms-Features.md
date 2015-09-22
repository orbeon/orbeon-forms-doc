> [[Home]] ▸ Orbeon Forms Features

## Purpose

The purpose of this page is to index features, their documentation and [blog posts](http://blog.orbeon.com/) in a central location so that you can get an idea of what Orbeon Forms offers at a glance.

## See also

- [[Documentation home|https://github.com/orbeon/orbeon-forms/wiki]]
- [Orbeon web site](http://www.orbeon.com/)
- [Orbeon blog](http://blog.orbeon.com/)

## Status

As of 2015-09-18 this page is still being updated.

## List of Orbeon Forms features

### Form Builder and Form Runner

- Inserting and reordering grid rows: [blog post](http://blog.orbeon.com/2013/11/inserting-and-reordering-grid-rows.html)
- Repeated grids: [[doc|Form Builder ~ Repeated Grids]], [older blog post](http://blog.orbeon.com/2012/04/support-for-repeats-lands-in-form.html)
- Repeated sections: [blog post](http://blog.orbeon.com/2014/01/repeated-sections.html)
- Section Templates: [[doc|Form Builder ~ Section Templates]]
- Singleton forms: [[doc|Form Runner ~ Singleton Form]]
- Versioning of form definitions: [blog post about concept](http://blog.orbeon.com/2014/02/form-versioning.html), [blog post about publish options](http://blog.orbeon.com/2015/01/choosing-best-versioning-option-when.html)
- Form field validation: [[doc|Form Builder ~ Validation]], [blog post](http://blog.orbeon.com/2013/07/enhanced-validation-in-form-builder-and.html)
    - required fields (also via formula, see [blog post](http://blog.orbeon.com/2014/09/control-required-values-with-formulas.html))
    - data types such as string, number, date, etc.
    - multiple constraints with formulas
    - common constraints such as minimum length and maximum length: [[doc|Form Builder ~ Validation#common-constraints]], [blog post](http://blog.orbeon.com/2015/07/how-common-constraints-work.html)
    - errors, warnings, and informational validations
    - custom alert messages per validation
- Access control
    - Owner / group permissions: [[doc|Form Runner ~ Access Control ~ Owner Group]], [blog post](http://blog.orbeon.com/2013/09/ownergroup-based-permissions-aka-see.html)
- Persistence (databases)
    - persistence API: [[doc|Form Runner ~ APIs ~ Persistence]]
    - persistence implementations: [[doc|Orbeon Forms Features ~ Database Support]]

### Form Builder

- Summary page: [[doc|Form Builder ~ Summary Page]]
- Toolbox:
    - features: [[doc|https://github.com/orbeon/orbeon-forms/wiki/Form-Builder-~-Toolbox]]
    - configurability: [[doc|Form Builder ~ Toolbox ~ Metadata]]
    - XBL components: [[doc|XForms ~ XBL]]
- Control Settings: [[doc|Form Builder ~ Control Settings]]
    - control name
    - Summary page options
    - custom CSS classes
    - easy switching of control appearances: [blog post](http://blog.orbeon.com/2015/06/how-new-form-builder-appearance.html)
    - validation: [[doc|Form Builder ~ Validation]]
    - formulas: [[doc|Form Builder ~ Formulas]]
    - help text (plain text and rich text)
        - appearance of help messages: [blog post](http://blog.orbeon.com/2014/01/improving-how-we-show-help-messages.html)
- Itemset Editor: [[doc|Form Builder ~ Itemset Editor]]
    - Hints for checkboxes and radio buttons: [blog post](http://blog.orbeon.com/2014/02/hints-for-checkboxes-and-radio-buttons.html)
- Section Settings: [[doc|Form Builder ~ Section Settings]]
- Publishing a form definition: [[doc|Form Builder ~ Publishing]]
- Explanation text: [blog post](http://blog.orbeon.com/2015/04/adding-explanatory-text-to-your-forms.html)
- Extension API: [[doc|Form Builder ~ Extension API]]
- Access control for editing forms: [[doc|Form Runner ~ Access Control ~ Editing Forms]]
- Internationalization (i18n) / localization (l10n): [[doc|Form Builder ~ Creating Localized Forms]]
- Services and actions
    - HTTP Services: [[doc|Form Builder ~ HTTP Services]]
    - Database Services: [[doc|Form Builder ~ Database Services]]
    - Actions: [[doc|Form Builder ~ Actions]]
- XML Schema Support: [[doc|Form Builder ~ XML Schema Support]]
- Source code editor: [[doc|Form Builder ~ Editing the Source Code of the Form]]
- Extension API: [[doc|Form Builder ~ Extension API]]

### Form Runner

- Standard look & feel
- Custom XBL components: [[doc|XForms ~ XBL]]
- Summary Page: [blog post](http://blog.orbeon.com/2014/06/the-form-builder-summary-page-and-form.html)
    - search
- Detail Page
    - Responsive design for mobile devices such as smartphones: [[doc|Form Runner ~ Responsive Design]], [blog](http://blog.orbeon.com/2015/08/responsive-design.html)
    - Review mode (printable)
    - Wizard view: [[doc|Form Runner ~ Wizard View]], [introduction blog post](http://blog.orbeon.com/2012/12/form-runner-wizard-view.html)
        - validated mode: [blog post](http://blog.orbeon.com/2015/03/new-wizard-validated-mode.html)
- Home Page: [[doc|Form Runner ~ Home Page]], [blog post](http://blog.orbeon.com/2014/06/the-form-builder-summary-page-and-form.html)
- Buttons and Processes: [[doc|Form-Runner-~-Buttons-and-Processes]], [blog post](http://blog.orbeon.com/2013/04/more-powerful-buttons.html)
- Autosave: [[doc|Form-Runner ~ Autosave]], [blog post](http://blog.orbeon.com/2013/10/autosave.html)
- PDF
    - Production: [[doc|Form Builder ~ PDF Production]]
        - Automatic
        - Template-based: [[doc|Form Builder ~ PDF Production ~ PDF Templates]]
    - Automatic highlighting of links [blog post](http://blog.orbeon.com/2015/04/automatic-web-links-in-pdf-files.html)
    - customizable file name: [[doc|Form Runner ~ Configuration properties#custom-pdf-filename]]
    - TIFF production: [[doc|Form Runner ~ TIFF Production]]
- Validation
    - as-you-type validation
    - centralized error summary showing currently relevant errors
- Sending emails
- Captcha: [[doc|Form Runner ~ Configuration properties#captcha]], [[XBL doc|Form Runner ~ XBL Components ~ Captcha]], [blog post](http://blog.orbeon.com/2011/12/stop-spammer-by-adding-captcha-to-your.html)

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
    - Character counter: [[doc|Form Runner ~ XBL Components ~ Character Counter]]
    - Custom components: [[doc|XForms ~ XBL]]

### Databases

- Database support: [[doc|Orbeon-Forms-Features-~-Database-Support]]
- SQL Server support in Orbeon Forms: [blog post](http://blog.orbeon.com/2014/05/sql-server-support-in-orbeon-forms.html)
- PostgreSQL support in Orbeon Forms: [blog post](http://blog.orbeon.com/2014/12/postgresql-support-in-orbeon-forms.html)

### Form handling

- Session heartbeat: [[doc|Contributors ~ Internals ~ State Handling]]
- Browser back/forward button support: [[doc|Contributors ~ Internals ~ State Handling]]

### Embedding

- Server side Embedding: [[doc|Form Runner ~ APIs ~ Server-side Embedding]], [blog post](http://blog.orbeon.com/2014/09/embedding-support-in-orbeon-forms-47.html)
- Liferay proxy portlet: [[doc|Form Runner ~ Portal ~ Liferay Proxy Portlet Guide]]
- Liferay full portlet: [[doc|Form-Runner-~-Portal-~-Full-Portlet-Guide]]

### Performance

- Limiter filter to limit the number of concurrent form requests: [[doc|Installation ~ Limiter Filter]]
- Internal service requests: [blog post](http://blog.orbeon.com/2015/01/saying-goodbye-to-internal-http.html)

### Misc

- Namespaced jQuery to avoid conflicts with other jQuery versions
- Run modes: [[doc|Installation ~ Run Modes]], [blog](http://blog.orbeon.com/2012/05/run-modes.html)