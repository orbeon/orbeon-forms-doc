# Orbeon Forms 2024.1.4

**Monday, February 2, 2026**

Today we released Orbeon Forms 2024.1.4! This maintenance release mainly contains bug-fixes and is recommended for all users of:

* [Orbeon Forms 2024.1.3 PE](orbeon-forms-2024.1.3.md)
* [Orbeon Forms 2024.1.2 PE](orbeon-forms-2024.1.2.md)
* [Orbeon Forms 2024.1.1 PE](orbeon-forms-2024.1.1.md)
* [Orbeon Forms 2024.1 PE](orbeon-forms-2024.1.md)

## Issues addressed

In this release, we have addressed many issues, including:

* Security
    * Don't point to the landing page in the DOM when the home link is disabled ([\#7315](https://github.com/orbeon/orbeon-forms/issues/7315))
    * New page still shows for form with no `create` permission ([\#7373](https://github.com/orbeon/orbeon-forms/issues/7373))
    * Library upgrades
* Form Builder
    * FB: undo doesn't work in text field/area in dialog ([\#7341](https://github.com/orbeon/orbeon-forms/issues/7341))
    * Pasting section with formula and `fr:*` function fails ([\#7330](https://github.com/orbeon/orbeon-forms/issues/7330))
    * FB Summary: applications include the label "Application Name" ([\#7344](https://github.com/orbeon/orbeon-forms/issues/7344))
    * Don't disable custom classes that don't use AVTs in Form Builder at design time ([\#7347](https://github.com/orbeon/orbeon-forms/issues/7347))
    * Controls in section template disappear after it is marked as repeated and the form is saved ([\#7352](https://github.com/orbeon/orbeon-forms/issues/7352))
    * Checkbox incorrectly shows as checked after navigating Gird Settings with Previous or Next ([\#7429](https://github.com/orbeon/orbeon-forms/issues/7429))
* Form Runner
    * Avoid duplicate names for file types in automatic hints ([\#7327](https://github.com/orbeon/orbeon-forms/issues/7327)) 
    * Incorrect MIME type for ODT leads to name not being shown in automatic hints ([\#7331](https://github.com/orbeon/orbeon-forms/issues/7331))
    * Switching form language to Korean shows button in Arabic ([\#7339](https://github.com/orbeon/orbeon-forms/issues/7339))
    * `save()` → `save(draft = "true")` → `save()` duplicates files in `orbeon_form_data_attach` ([\#7343](https://github.com/orbeon/orbeon-forms/issues/7343))
    * Files attached to Multiple File Attachments marked as Encrypt data at rest can't be read ([\#7346](https://github.com/orbeon/orbeon-forms/issues/7346))
    * Provide `analyze-string()` in JVM environment ([\#7354](https://github.com/orbeon/orbeon-forms/issues/7354))
    * S3 endpoint override not applied ([\#7357](https://github.com/orbeon/orbeon-forms/issues/7357))
    * `Accept` header no longer forwarded in `replace="all"` submissions ([\#7358](https://github.com/orbeon/orbeon-forms/issues/7358))
    * Email sending using Gmail (sometimes) fails ([\#7162](https://github.com/orbeon/orbeon-forms/issues/7162))
    * HTML label with `<div>` shows in blue on new line in the error summary ([\#7410](https://github.com/orbeon/orbeon-forms/issues/7410))
    * Setting `oxf.fr.detail.button.lease.show-in-view-mode.visible.*.*` to false makes the button readonly instead of hiding it ([\#7411](https://github.com/orbeon/orbeon-forms/issues/7411))
    * User can submit form after losing the lease if the Review Form Validation Messages dialog is shown ([\#7421](https://github.com/orbeon/orbeon-forms/issues/7421))
    * With flat view enabled, can't publish form with control named `_` ([\#7433](https://github.com/orbeon/orbeon-forms/issues/7433))
    * Make error messages easier to read by removing the full file size and adding a significant digit to the compact version ([\#7437](https://github.com/orbeon/orbeon-forms/issues/7437))
    * Help for output without border and an empty value doesn't point to anything ([\#7440](https://github.com/orbeon/orbeon-forms/issues/7440))
    * Remove ⌃Tab / ⌃⇧Tab as keyboard shortcuts to switch tabs ([\#7446](https://github.com/orbeon/orbeon-forms/issues/7446))
    * Action targeting repeated iteration on same line doesn't work ([\#7464](https://github.com/orbeon/orbeon-forms/issues/7464))
    * Improve logging for invalid property value ([\#7456](https://github.com/orbeon/orbeon-forms/issues/7456))
* XBL Components
    * Dynamic dropdown with search stuck on showing "Searching…" ([\#7368](https://github.com/orbeon/orbeon-forms/issues/7368))
    * On mobile, the number field is cleared on tap ([\#7376](https://github.com/orbeon/orbeon-forms/issues/7376))
    * fr:number – Incorrect behavior of round-when-formatting when editing an existing value ([\#7386](https://github.com/orbeon/orbeon-forms/issues/7386))
    * Solution to dynamic dropdown with search in section template in view mode that does not require republishing ([\#7397](https://github.com/orbeon/orbeon-forms/issues/7397))
* Accessibility
    * Invalid `aria-readonly="true"` on calculated value without border ([\#7448](https://github.com/orbeon/orbeon-forms/issues/7448))
    * Document does not have a main landmark ([\#7449](https://github.com/orbeon/orbeon-forms/issues/7449))
* Offline
    * `replaceState()` can fail in some mobile embedded environments ([\#7428](https://github.com/orbeon/orbeon-forms/issues/7428)) 
* Embedding
    * Browser getting 500 error when retrieving a font through cross-site ([\#7349](https://github.com/orbeon/orbeon-forms/issues/7349))
    * Datepicker incorrectly positioned when form is embedded ([\#7375](https://github.com/orbeon/orbeon-forms/issues/7375))
    * 404 loading `model.min.js` with Formatted Text Area in embedded form ([\#7378](https://github.com/orbeon/orbeon-forms/issues/7378))
    * Formatted Text Area on ulterior page of wizard in cross-origin embedded form fails to loads `model.min.js` from incorrect host ([\#7400](https://github.com/orbeon/orbeon-forms/issues/7400))
* Distribution
    * `orbeon-embedding.jar` not to contain SLF4J classes ([\#7337](https://github.com/orbeon/orbeon-forms/issues/7337)) 
* Performance
    * `controlNameFromIdOpt()` is a hotspot ([\#7432](https://github.com/orbeon/orbeon-forms/issues/7432))

You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page, or use our Docker images.

Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon), [Bluesky](https://bsky.app/profile/orbeon.bsky.social), or the [forum](https://groups.google.com/g/orbeon).

We hope you enjoy this release!
