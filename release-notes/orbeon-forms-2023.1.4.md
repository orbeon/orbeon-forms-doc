# Orbeon Forms 2023.1.4

__Thursday, August 22, 2024__

Today we released Orbeon Forms 2023.1.4 PE! This maintenance release contains bug-fixes and new features and is recommended for all users of:

- [Orbeon Forms 2023.1.3 PE](orbeon-forms-2023.1.3.md)
- [Orbeon Forms 2023.1.2 PE](orbeon-forms-2023.1.2.md)
- [Orbeon Forms 2023.1.2 PE](orbeon-forms-2023.1.2.md)
- [Orbeon Forms 2023.1.1 PE](orbeon-forms-2023.1.1.md)
- [Orbeon Forms 2023.1 PE](orbeon-forms-2023.1.md)

## New features

This maintenance release introduces support for [Friendly Captcha](https://friendlycaptcha.com/), an alternative to Google's reCAPTCHA and, in their own words, "GDPR-Compliant Bot Protection". For more information, see the [documentation](/form-runner/component/captcha.md#friendly-captcha). Thanks to the kind users who have sponsored this enhancement!

The following small features have also been added:

- `edit-to-new` process action ([doc](https://doc.orbeon.com/form-runner/advanced/buttons-and-processes/actions-form-runner#edit-to-new))
- `captcha-reset` process action ([doc](https://doc.orbeon.com/form-runner/component/captcha#resetting-the-captcha))

## Issues addressed

This release addresses the following issues since [Orbeon Forms 2023.1.3 PE](orbeon-forms-2023.1.3.md):

- Security
    - CVE-2018-18531 in Kaptcha ([\#6382](https://github.com/orbeon/orbeon-forms/issues/6382))
    - Support Friendly Captcha ([\#6439](https://github.com/orbeon/orbeon-forms/issues/6439))
    - Control cleared upon `xforms-disabled` doesn't recalculate ([\#6370](https://github.com/orbeon/orbeon-forms/issues/6370))
    - MIPs fail to evaluate after `send() then save()` ([\#6436](https://github.com/orbeon/orbeon-forms/issues/6436))
- Form Builder
    - Form Builder displays gray zone below form in some cases ([\#6376](https://github.com/orbeon/orbeon-forms/issues/6376))
    - Control label editor sometimes appears below the control ([\#6384](https://github.com/orbeon/orbeon-forms/issues/6384))
    - Control for Simplified appearance in Section/Grid Settings is missing ([\#6301](https://github.com/orbeon/orbeon-forms/issues/6301))
    - Email templates: prevent saving if there are duplicate parameter names ([\#6415](https://github.com/orbeon/orbeon-forms/issues/6415))
    - Dynamic Dropdown not to call service at design-time ([\#6422](https://github.com/orbeon/orbeon-forms/issues/6422))
- Form Runner
    - Narrow layout: bottom buttons hide content ([\#6417](https://github.com/orbeon/orbeon-forms/issues/6417))
    - Reflow/responsive repeated grids on devices with narrow screens ([\#4491](https://github.com/orbeon/orbeon-forms/issues/4491))
    - Failed to execute 'add' on 'DOMTokenList': The token provided must not be empty ([\#6383](https://github.com/orbeon/orbeon-forms/issues/6383))
    - PDF button on summary page doesn't do anything ([\#6386](https://github.com/orbeon/orbeon-forms/issues/6386))
    - Excel file can't be opened in Excel when the language is set to German ([\#6391](https://github.com/orbeon/orbeon-forms/issues/6391))
    - `ORBEON.fr.API.destroyForm()` throwing `java.lang.IllegalArgumentException` ([\#6397](https://github.com/orbeon/orbeon-forms/issues/6397))
    - Mask for Error Panel and other dialogs stays after `destroyForm()` ([\#6396](https://github.com/orbeon/orbeon-forms/issues/6396))
    - Finnish resources for attachment controls ([\#6401](https://github.com/orbeon/orbeon-forms/issues/6401))
    - Automatic PDF to produce put produced CSS first, so it can be overridden ([\#6402](https://github.com/orbeon/orbeon-forms/issues/6402))
    - NPE if no cookies are sent ([\#6406](https://github.com/orbeon/orbeon-forms/issues/6406))
    - On Windows, option in dropdown showing "Please select" is gray ([\#6407](https://github.com/orbeon/orbeon-forms/issues/6407))
    - Warnings presented as errors in Error Summary ([\#6410](https://github.com/orbeon/orbeon-forms/issues/6410))
    - `open-rendered-format` parameters to support XVTs ([\#6419](https://github.com/orbeon/orbeon-forms/issues/6419))
    - Attachment control not visible on iOS ([\#6420](https://github.com/orbeon/orbeon-forms/issues/6420))
    - Upload fails with exception ([\#6421](https://github.com/orbeon/orbeon-forms/issues/6421))
    - Lease expiration doesn't cause lease state change ([\#6424](https://github.com/orbeon/orbeon-forms/issues/6424))
    - Improve autosave buttons appearance ([\#6430](https://github.com/orbeon/orbeon-forms/issues/6430))
    - Don't show form description when a document message is present ([\#6431](https://github.com/orbeon/orbeon-forms/issues/6431))
    - Process action to create a new document id ([\#6435](https://github.com/orbeon/orbeon-forms/issues/6435))
    - Support `save then email` for users without the `read` permission ([\#5768](https://github.com/orbeon/orbeon-forms/issues/5768))
    - Possible `rebuild` followed by `refresh` but without `recalculate` ([\#6442](https://github.com/orbeon/orbeon-forms/issues/6442))
    - Model RRRR must force previous actions ([\#1660](https://github.com/orbeon/orbeon-forms/issues/1660))
    - Add `captcha-reset` process action ([\#6441](https://github.com/orbeon/orbeon-forms/issues/6441))
    - Offline: if captcha property is enabled, crash on `isNewOrEditMode()` ([\#6451](https://github.com/orbeon/orbeon-forms/issues/6451))
    - JavaScript error when Dynamic dropdown with search becomes non-relevant ([\#6446](https://github.com/orbeon/orbeon-forms/issues/6446))
- Other
    - Update slf4j-api to 2.0.16 ([\#6437](https://github.com/orbeon/orbeon-forms/issues/6437))
    - Refactor persistence layer permissions ([\#5741](https://github.com/orbeon/orbeon-forms/issues/5741))
    - `ProcessorService`: log HTTP method and URL parameters ([\#6414](https://github.com/orbeon/orbeon-forms/issues/6414))
    - Landing page images must not be stored under `WEB-INF` ([\#6449](https://github.com/orbeon/orbeon-forms/issues/6449))

You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page.

Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon), [Mastodon](https://mastodon.social/@orbeon), or the [forum](https://www.orbeon.com/community).

We hope you enjoy this release!
