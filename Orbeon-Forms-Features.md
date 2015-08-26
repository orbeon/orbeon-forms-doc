> [[Home]] ▸ Orbeon Forms Features

## Purpose

The purpose of this page is to index features by name in a central place.

## Status

As of 2015-07-07 this page is *very* incomplete.

## List of Orbeon Forms features

### Form Builder and Form Runner

- Inserting and reordering grid rows: [blog post](http://blog.orbeon.com/2013/11/inserting-and-reordering-grid-rows.html)
- Repeated grids: [[doc|Form Builder ~ Repeated Grids]], [older blog post](http://blog.orbeon.com/2012/04/support-for-repeats-lands-in-form.html)
- Repeated sections: [blog post](http://blog.orbeon.com/2014/01/repeated-sections.html)
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
    - persistence API: [doc](http://wiki.orbeon.com/forms/doc/developer-guide/form-runner/persistence-api)
    - [[persistence implementations|Orbeon Forms Features ~ Database Support]]

### Form Builder

- Control Settings: [[doc|Form Builder ~ Control Settings]]
    - control name
    - Summary page options
    - custom CSS classes
    - easy switching of control appearances: [blog post](http://blog.orbeon.com/2015/06/how-new-form-builder-appearance.html)
    - validation
    - formulas
    - help text (plain text and rich text)
- Explanation text: [blog post](http://blog.orbeon.com/2015/04/adding-explanatory-text-to-your-forms.html)
- Access control for editing forms: [[doc|Form Runner ~ Access Control ~ Editing Forms]]
- Internationalization (i18n)

### Form Runner

- Standard look & feel
- Summary Page: [blog post](http://blog.orbeon.com/2014/06/the-form-builder-summary-page-and-form.html)
    - search
- Detail Page
    - Responsive design for mobile devices such as smartphones: [[doc|Form Runner ~ Responsive Design]]
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

### Form controls

- text controls
    - input field
    - textarea
    - text output
    - password field
    - formatted text (rich text)
    - explanation text
- typed controls
    - email field
    - US phone number field
    - number field
    - currency field
    - US state
- date and time controls
    - date, time, date and time
    - dropdown date, fields date
- selection controls
    - dropdown menu
    - radio buttons
    - checkboxes
    - scrollable list
    - boolean input
    - scrollable checkboxes
    - dynamic data dropdown
    - autocomplete
- attachments
    - static image
    - image attachment
    - file attachment
    - image annotation
- buttons
    - button
    - link button
- other
    - character counter: [[doc|Form Runner ~ XBL Components ~ Character Counter]]

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

- namespaced jQuery to avoid conflicts with other jQuery versions