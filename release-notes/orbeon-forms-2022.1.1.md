# Orbeon Forms 2022.1.1

__Tuesday, February 21, 2023__

Today we released Orbeon Forms 2022.1.1 PE! This maintenance release contains new features and bug-fixes and is recommended for all users of:

- [Orbeon Forms 2022.1 PE](orbeon-forms-2022.1.md)

We don't usually include new features in maintenance releases, but we made an exception for this release, as we wanted to include the following:

- Time form control improvements ([blog post](https://blog.orbeon.com/2023/01/time-form-control-improvements.html)) ([\#5534](https://github.com/orbeon/orbeon-forms/issues/5534))
- Ability to right-align Number/Currency fields ([doc](/form-runner/component/number.md), [blog post](https://blog.orbeon.com/2023/08/right-aligning-number-and-currency.html)) ([\#3245](https://github.com/orbeon/orbeon-forms/issues/3245))

This release also addresses the following issues since [Orbeon Forms 2022.1 PE](orbeon-forms-2022.1.md):

- Email templates fixes
    - `email` action fails if content of attachment field is to be sent and the field was left empty ([\#5675](https://github.com/orbeon/orbeon-forms/issues/5675))
    - List of controls in Email Settings dialog isn't updated ([\#5672](https://github.com/orbeon/orbeon-forms/issues/5672))
    - A published pre-2022.1 form, without a subject or body template, isn't upgraded when sending an email ([\#5649](https://github.com/orbeon/orbeon-forms/issues/5649))
    - Error migrating email templates in customer form ([\#5597](https://github.com/orbeon/orbeon-forms/issues/5597))
    - Email attachments are missing when `oxf.fr.email.attach-files.*.*` is set to `selected` ([\#5603](https://github.com/orbeon/orbeon-forms/issues/5603))
    - New email dialog: email body cleared when changing attached files in dialog ([\#5602](https://github.com/orbeon/orbeon-forms/issues/5602))
- Performance improvements
    - `unload` handler disables bfcache ([\#5643](https://github.com/orbeon/orbeon-forms/issues/5643))
- Offline mode fixes
    - Companion classes don't work in offline mode ([\#5635](https://github.com/orbeon/orbeon-forms/issues/5635))
    - Offline: can't return JSON from `SubmissionProvider` ([\#5636](https://github.com/orbeon/orbeon-forms/issues/5636))
- UI fixes
    - Radio/checkbox alignment relative to Single Checkbox ([\#5640](https://github.com/orbeon/orbeon-forms/issues/5640))
    - Make settings dialog more compact ([\#5607](https://github.com/orbeon/orbeon-forms/issues/5607))
    - Improve checkboxes/radio alignments in dialogs ([\#5606](https://github.com/orbeon/orbeon-forms/issues/5606))
    - Move "Automatic Calculations Dependencies" to "Formulas" tab ([\#5605](https://github.com/orbeon/orbeon-forms/issues/5605))
    - "Test PDF" button must not show in "Test Offline" ([\#5104](https://github.com/orbeon/orbeon-forms/issues/5104))
    - Not enough padding at top of offline test ([\#5601](https://github.com/orbeon/orbeon-forms/issues/5601))
    - Readonly fields background remains white ([\#5600](https://github.com/orbeon/orbeon-forms/issues/5600))
    - Orbeon logo is low-resolution ([\#5594](https://github.com/orbeon/orbeon-forms/issues/5594))
    - Incorrect simplified repeated grid border between iterations ([\#5685](https://github.com/orbeon/orbeon-forms/issues/5685))
    - Error Summary: long error message doesn' wrap ([\#5681](https://github.com/orbeon/orbeon-forms/issues/5681))
- Other fixes
    - JavaScript error when login page detected ([\#5638](https://github.com/orbeon/orbeon-forms/issues/5638))
    - Actions in section template only using fields with a `.` are not running ([\#5648](https://github.com/orbeon/orbeon-forms/issues/5648))
    - Error Summary doesn't update error with nested repeats ([\#5682](https://github.com/orbeon/orbeon-forms/issues/5682))
    - Localize static dropdown in Chinese ([\#5680](https://github.com/orbeon/orbeon-forms/issues/5680))
    - Improve handling of time without seconds ([\#5630](https://github.com/orbeon/orbeon-forms/issues/5630)) 
    - Result dialog: support AVT in resource ([\#5598](https://github.com/orbeon/orbeon-forms/issues/5598)) 
    - `fr:component-param-value()`: add `componentId` parameter ([\#5639](https://github.com/orbeon/orbeon-forms/issues/5639))
    - FB: "New" from Form Builder causes error ([\#5572](https://github.com/orbeon/orbeon-forms/issues/5572))
    - Issue retrieving CSS files for PDF production ([\#5671](https://github.com/orbeon/orbeon-forms/issues/5671))
    - Map component showing error about missing callback in the console ([\#5677](https://github.com/orbeon/orbeon-forms/issues/5677))
    - Readonly mode with resizeable textarea: crash when clicking ([\#5623](https://github.com/orbeon/orbeon-forms/issues/5623))
    - Click on readonly number field toggles edit/view value ([\#5587](https://github.com/orbeon/orbeon-forms/issues/5587))
    - Can't click on "Use Placeholder for Text Fields and Text Areas" checkboxes ([\#5595](https://github.com/orbeon/orbeon-forms/issues/5595))
    - License download form doesn't download license anymore ([\#5610](https://github.com/orbeon/orbeon-forms/issues/5610))
    - License download form crashes upon initialization ([\#5609](https://github.com/orbeon/orbeon-forms/issues/5609))
    - Can't DnD request actions in dialog ([\#5599](https://github.com/orbeon/orbeon-forms/issues/5599))
    - Crash when parsing some version numbers ([\#5632](https://github.com/orbeon/orbeon-forms/issues/5632))
    - JavaScript in `view` when the first control in the form is a dropdown ([\#5651](https://github.com/orbeon/orbeon-forms/issues/5651))

You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page.

Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon), [Bluesky](https://bsky.app/profile/orbeon.bsky.social), or the [forum](https://groups.google.com/g/orbeon).

We hope you enjoy this release!

## Compatibility notes

### Time formats

With this release, for Time controls, the `oxf.xforms.format.output.time` property is no longer used in readonly modes. Instead, the `output-format` parameter of the time field is used, which defaults to the value of the global `oxf.xforms.format.input.time` property.

See [Time component](/form-runner/component/time.md) for more information.