# Orbeon Forms 2021.1.4

__Monday, August 1, 2022__

Today we released Orbeon Forms 2021.1.4 PE! This release introduces security fixes, accessibility enhancements, and bug-fixes and is recommended for all users of:

- [Orbeon Forms 2021.1.3 PE](orbeon-forms-2021.1.3.md)
- [Orbeon Forms 2021.1.2 PE](orbeon-forms-2021.1.2.md)
- [Orbeon Forms 2021.1.1 PE](orbeon-forms-2021.1.1.md)
- [Orbeon Forms 2021.1 PE](orbeon-forms-2021.1.md)

This release addresses the following issues since [Orbeon Forms 2021.1.3 PE](orbeon-forms-2021.1.3.md):

- Security
    - Update CodeMirror to latest version ([\#5359](https://github.com/orbeon/orbeon-forms/issues/5359))
    - Upgrade to Log4j 2.18.0 ([\#5370](https://github.com/orbeon/orbeon-forms/issues/5370))
    - Remove dependency on old `jcifs` library ([\#5293](https://github.com/orbeon/orbeon-forms/issues/5293))
- Accessibility
    - Improve accessibility of Form Runner dialogs ([\#5330](https://github.com/orbeon/orbeon-forms/issues/5330))
    - Do not announce field as invalid before it is shown as such ([\#4797](https://github.com/orbeon/orbeon-forms/issues/4797))
    - Focus not set on first field if form starts with a repeated grid ([\#5336](https://github.com/orbeon/orbeon-forms/issues/5336))
    - Label for time field is not read ([\#5329](https://github.com/orbeon/orbeon-forms/issues/5329))
    - Screen reader not reading options in dropdowns with search ([\#4854](https://github.com/orbeon/orbeon-forms/issues/4854))
    - Fields to point to help and hint with `aria-describedby` ([\#4832](https://github.com/orbeon/orbeon-forms/issues/4832))
- Embedding
    - `fr:is-embedded()` to return true when embedding with JavaScript API ([\#5325](https://github.com/orbeon/orbeon-forms/issues/5325)) 
    - `fr:is-embedded()` to take an optional parameter ([\#5390](https://github.com/orbeon/orbeon-forms/issues/5390))
    - Use `fr:is-embedded()` to hide navigations buttons in embedded modes ([\#5323](https://github.com/orbeon/orbeon-forms/issues/5323))
    - PDF button isn't serving a PDF when the form embedded with the JavaScript API ([\#5355](https://github.com/orbeon/orbeon-forms/issues/5355))
- Form Builder
    - `<fb:template>` containing element in a custom namespace causes error opening Form Settings ([\#5315](https://github.com/orbeon/orbeon-forms/issues/5315))
    - Change time field to date/dateTime field fails ([\#5388](https://github.com/orbeon/orbeon-forms/issues/5388))
- Other 
    - Email addresses to support including a personal name ([\#5313](https://github.com/orbeon/orbeon-forms/issues/5313))
    - `Authorization` header not passed to persistence API when doing a PUT for file attachments ([\#5310](https://github.com/orbeon/orbeon-forms/issues/5310))
    - `Authorization` header not passed to persistence API when doing a GET for file attachments ([\#5314](https://github.com/orbeon/orbeon-forms/issues/5314))
    - On save, 1 always passed as version when reading attachments to draft ([\#5316](https://github.com/orbeon/orbeon-forms/issues/5316))
    - During Push to Remote, `Authorization` header not sent in PUT, resulting in a 403 ([\#5317](https://github.com/orbeon/orbeon-forms/issues/5317))
    - Error with undeclared `$class-value` when `class` present on non-repeated `tr` or `td` of `fr:grid` ([\#5319](https://github.com/orbeon/orbeon-forms/issues/5319))
    - Incorrect wizard status classes ([\#5320](https://github.com/orbeon/orbeon-forms/issues/5320))
    - Value of custom attributes is reset when removing an attached file is removed ([\#5322](https://github.com/orbeon/orbeon-forms/issues/5322))
    - Action running on form load fails to update data if autosaved or manually-saved document is loaded ([\#5358](https://github.com/orbeon/orbeon-forms/issues/5358))
    - Offline: data with some characters is not properly serialized ([\#5360](https://github.com/orbeon/orbeon-forms/issues/5360))
    - Action running "after data is ready" doesn't have access to the latest calculated values on `/edit` ([\#5378](https://github.com/orbeon/orbeon-forms/issues/5378))
    - Date picker shown for readonly date on iOS ([\#5376](https://github.com/orbeon/orbeon-forms/issues/5376))

You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page.  
Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon) or the [forum](https://www.orbeon.com/community).

We hope you enjoy this release!
