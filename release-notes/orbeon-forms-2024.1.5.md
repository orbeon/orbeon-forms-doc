# Orbeon Forms 2024.1.5

**xxx, March xxx, 2026**

Today we released Orbeon Forms 2024.1.5! This maintenance release mainly contains bug-fixes and is recommended for all users of:

* [Orbeon Forms 2024.1.4 PE](orbeon-forms-2024.1.4.md)
* [Orbeon Forms 2024.1.3 PE](orbeon-forms-2024.1.3.md)
* [Orbeon Forms 2024.1.2 PE](orbeon-forms-2024.1.2.md)
* [Orbeon Forms 2024.1.1 PE](orbeon-forms-2024.1.1.md)
* [Orbeon Forms 2024.1 PE](orbeon-forms-2024.1.md)

## Issues addressed

In this release, we have addressed many issues, including:

* Security
    * Connection: obfuscate `Authorization` header with debug logging ([\#7521](https://github.com/orbeon/orbeon-forms/issues/7521))
    * Library updates
* Form Builder
    * "true is not a member of Enum (all, none, selected)" if unselecting Use default in Email Settings ([\#7500](https://github.com/orbeon/orbeon-forms/issues/7500))
    * Menu for repeated grid positioned incorrectly in Form Builder when .fb-main is scrolled down ([\#7502](https://github.com/orbeon/orbeon-forms/issues/7502))
    * Clicking on the right of "Delete template" in Email Settings deletes the template ([\#6058](https://github.com/orbeon/orbeon-forms/issues/6058))
    * `fr|databound-select1-search`: don't evaluate resource at design-time ([\#7555](https://github.com/orbeon/orbeon-forms/issues/7555))
    * HTTP Service Editor grid menu is cropped ([\#7463](https://github.com/orbeon/orbeon-forms/issues/7463))
    * Container Settings: completion menu truncated by dialog ([\#7317](https://github.com/orbeon/orbeon-forms/issues/7317))
    * "Cannot read properties of null" in Form Builder's Form Settings ([\#7587](https://github.com/orbeon/orbeon-forms/issues/7587))
    * When in-place editing a control label drag-and-dropping another control cancels the label edition ([\#6867](https://github.com/orbeon/orbeon-forms/issues/6867))
* Form Runner
    * Help popover too wide when shown over the control ([\#7462](https://github.com/orbeon/orbeon-forms/issues/7462))
    * Support forcing path style in the S3 bucket config ([\#7466](https://github.com/orbeon/orbeon-forms/issues/7466))
    * When a control's label has bullet points, the bullet list in the error summary shows in blue instead of red ([\#7479](https://github.com/orbeon/orbeon-forms/issues/7479))
    * Readonly repeated section with paging shows a single iteration in view mode ([\#7481](https://github.com/orbeon/orbeon-forms/issues/7481))
    * Solution to attachment in section template in view mode that does not require republishing ([\#7482](https://github.com/orbeon/orbeon-forms/issues/7482))
    * Add CSS class on cell with checkbox and text field for custom iteration name ([\#7484](https://github.com/orbeon/orbeon-forms/issues/7484))
    * `<fr:repeat-add-iteration>` doesn't handle initial values ([\#7491](https://github.com/orbeon/orbeon-forms/issues/7491))
    * Support non-standard region in S3 bucket configuration ([\#7501](https://github.com/orbeon/orbeon-forms/issues/7501))
    * "Undeclared variable" error with itemset filter ([\#7504](https://github.com/orbeon/orbeon-forms/issues/7504))
    * Navbar workflow stage margins are incorrect ([\#7505](https://github.com/orbeon/orbeon-forms/issues/7505))
    * Repeated section template sometimes crashes when removing a repetition ([\#7506](https://github.com/orbeon/orbeon-forms/issues/7506))
    * Clicking to the right of a button inside a cell triggers the button ([\#7515](https://github.com/orbeon/orbeon-forms/issues/7515))
    * Datepicker does not change to the selected language ([\#7520](https://github.com/orbeon/orbeon-forms/issues/7520))
    * Purge API not deleting files ([\#7517](https://github.com/orbeon/orbeon-forms/issues/7517))
    * Section templates: value dependencies ([\#7427](https://github.com/orbeon/orbeon-forms/issues/7427))
    * `fr:workflow-stage-value()` formula not evaluating in section template ([\#7492](https://github.com/orbeon/orbeon-forms/issues/7492))
    * Process `save` action to ensure document is synchronized ([\#7533](https://github.com/orbeon/orbeon-forms/issues/7533))
    * Continue button missing in Review Form Validation Messages dialog if shown from the view page ([\#7534](https://github.com/orbeon/orbeon-forms/issues/7534))
    * Grid to annotate with a class cells on empty row ([\#7544](https://github.com/orbeon/orbeon-forms/issues/7544))
    * Service with variable in URL AVT fails ([\#7550](https://github.com/orbeon/orbeon-forms/issues/7550))
    * Service with variable in URL AVT does not resolve variable against action source ([\#7551](https://github.com/orbeon/orbeon-forms/issues/7551))
    * Schema generation for form with string validation with custom alert fails with "Invalid QName {}" error ([\#7552](https://github.com/orbeon/orbeon-forms/issues/7552))
    * Handle singleton with draft ([\#7560](https://github.com/orbeon/orbeon-forms/issues/7560))
    * For the form metadata API, the persistence proxy incorrectly expects implementation of the persistence of API to always return all versions ([\#7565](https://github.com/orbeon/orbeon-forms/issues/7565))
    * Summary Excel export: headers and values discrepancy ([\#7566](https://github.com/orbeon/orbeon-forms/issues/7566))
    * Connection not closed after `xforms-submit-error` ([\#7571](https://github.com/orbeon/orbeon-forms/issues/7571))
    * Wizard status: mark section as completed when visited if no required fields ([\#7494](https://github.com/orbeon/orbeon-forms/issues/7494))
    * Email template: selection control value in template shows item value, not label ([\#7573](https://github.com/orbeon/orbeon-forms/issues/7573))
    * When a control's value changes, do not mark it as visited if the control is focused ([\#7574](https://github.com/orbeon/orbeon-forms/issues/7574))
    * Possible deadlock with concurrent search and deletes ([\#7503](https://github.com/orbeon/orbeon-forms/issues/7503))
    * Normalize app/form name case from form definition ([\#7172](https://github.com/orbeon/orbeon-forms/issues/7172))
    * Export: support POST so that URL limit is not exceeded ([\#7459](https://github.com/orbeon/orbeon-forms/issues/7459))
    * More user-friendly file names in persistence ([\#6565](https://github.com/orbeon/orbeon-forms/issues/6565))
    * In wizard TOC, hints for section labels, shown on hover, incorrectly contain HTML tags ([\#7595](https://github.com/orbeon/orbeon-forms/issues/7595))
    * Wizard pager for top-level repeated section incorrectly disables "Next" icon ([\#7229](https://github.com/orbeon/orbeon-forms/issues/7229))
* XBL Components
    * Date and Time field not to show error after date is selected ([\#7461](https://github.com/orbeon/orbeon-forms/issues/7461))
    * Clearer distinction in the date control between interacting with the datepicker and the field ([\#7518](https://github.com/orbeon/orbeon-forms/issues/7518))
    * Dropdown to always show empty as the first value, instead of "Please select:" ([\#6389](https://github.com/orbeon/orbeon-forms/issues/6389))
* Accessibility
    * Dropdown with search creates two elements with `role="combobox"`, one is missing `aria-controls` ([\#7442](https://github.com/orbeon/orbeon-forms/issues/7442))
    * Screen reader to announce section name in addition to field label ([\#7451](https://github.com/orbeon/orbeon-forms/issues/7451))
    * Attachment control "Cancel" link should be localized in Form Runner ([\#7562](https://github.com/orbeon/orbeon-forms/issues/7562))
* Offline
    * JS env: zero length file size constraint not checked ([\#7522](https://github.com/orbeon/orbeon-forms/issues/7522))
    * JS env: default attachment error action runs ([\#7563](https://github.com/orbeon/orbeon-forms/issues/7563))
    * Support customized attachment filenames in offline mode ([\#7082](https://github.com/orbeon/orbeon-forms/issues/7082))
* Embedding
    * JavaScript error when destroying a form using a lease and embedded with the JavaScript API ([\#7535](https://github.com/orbeon/orbeon-forms/issues/7535))
* Distribution
    * `SecureUtilsTest` can occasionally fail ([\#7507](https://github.com/orbeon/orbeon-forms/issues/7507))

You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page, or use our Docker images.

Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon), [Bluesky](https://bsky.app/profile/orbeon.bsky.social), or the [forum](https://groups.google.com/g/orbeon).

We hope you enjoy this release!
