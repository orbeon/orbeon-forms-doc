# Orbeon Forms 2023.1.5

__Wednesday, October 2, 2024__

Today we released Orbeon Forms 2023.1.5 PE! This maintenance release contains bug-fixes and new features and is recommended for all users of:

- [Orbeon Forms 2023.1.4 PE](orbeon-forms-2023.1.4.md)
- [Orbeon Forms 2023.1.3 PE](orbeon-forms-2023.1.3.md)
- [Orbeon Forms 2023.1.2 PE](orbeon-forms-2023.1.2.md)
- [Orbeon Forms 2023.1.2 PE](orbeon-forms-2023.1.2.md)
- [Orbeon Forms 2023.1.1 PE](orbeon-forms-2023.1.1.md)
- [Orbeon Forms 2023.1 PE](orbeon-forms-2023.1.md)

## Issues addressed

This release addresses the following issues since [Orbeon Forms 2023.1.4 PE](orbeon-forms-2023.1.4.md):

- Form Builder
    - Option not to show the "Require token" row ([\#6500](https://github.com/orbeon/orbeon-forms/issues/6500))
- Form Runner
    - Dynamic dropdown (`databound-select1.xbl`) to evaluate Choice formula (`xf:itemset ref=""`) in outer scope ([\#6443](https://github.com/orbeon/orbeon-forms/issues/6443))
    - PDF no longer shows headers/footers ([\#6460](https://github.com/orbeon/orbeon-forms/issues/6460))
    - Crash loading form with custom XBL component ([\#6461](https://github.com/orbeon/orbeon-forms/issues/6461))
    - Offline: error loading data with large images on Safari ([\#6462](https://github.com/orbeon/orbeon-forms/issues/6462))
    - Offline: zip resource resolver is not hooked up consistently ([\#6469](https://github.com/orbeon/orbeon-forms/issues/6469))
    - Offline version of `xxf:json-to-xml()` doesn't support being called with no parameter, using context ([\#6471](https://github.com/orbeon/orbeon-forms/issues/6471))
    - Offline, `xxf:has-class('gaga', ())` and `xxf:classes(())` incorrectly use the context node ([\#6514](https://github.com/orbeon/orbeon-forms/issues/6514))
    - Add `getNativeSession` on Session instances ([\#6465](https://github.com/orbeon/orbeon-forms/issues/6465))
    - Can't parse JSON from context with `xxf:json-to-xml()` ([\#6470](https://github.com/orbeon/orbeon-forms/issues/6470))
    - `xxf:json-to-xml()` fails instead of returning an empty sequence when the JSON is invalid ([\#6472](https://github.com/orbeon/orbeon-forms/issues/6472))
    - Better case-insensitive check of authentication headers ([\#6473](https://github.com/orbeon/orbeon-forms/issues/6473))
    - Improve lease buttons appearance ([\#6482](https://github.com/orbeon/orbeon-forms/issues/6482))
    - Support dollar syntax to refer to control values in AVT Action Syntax `fr:alert` ([\#6485](https://github.com/orbeon/orbeon-forms/issues/6485))
    - `xforms-selected` not set for radio buttons and checkboxes when itemset changes ([\#6486](https://github.com/orbeon/orbeon-forms/issues/6486))
    - HTTP Service with Serialization set to HTML Form fails with "Content type may not be null" ([\#6487](https://github.com/orbeon/orbeon-forms/issues/6487))
    - Portlet: error when attempting to read autosaved draft ([\#6139](https://github.com/orbeon/orbeon-forms/issues/6139))
    - Date control placeholder to be localized in Spanish, Czech, and Turkish ([\#6496](https://github.com/orbeon/orbeon-forms/issues/6496))
    - Possible NPE in `HttpServletRequest` wrapper ([\#6503](https://github.com/orbeon/orbeon-forms/issues/6503))
    - Grid with "Grid Tab Order: Columns" can have incorrect width ([\#6504](https://github.com/orbeon/orbeon-forms/issues/6504))
    - Grid with "Grid Tab Order: Columns" can have incorrect repeated layout ([\#6506](https://github.com/orbeon/orbeon-forms/issues/6506))
    - Buttons with `loading-indicator` set to `inline` are slightly taller ([\#6507](https://github.com/orbeon/orbeon-forms/issues/6507))
    - Setting `oxf.xforms.xbl.fr.wizard.separate-toc.*.*` to true has no effect ([\#6512](https://github.com/orbeon/orbeon-forms/issues/6512))
    - PDF file from Summary page produces error ([\#6515](https://github.com/orbeon/orbeon-forms/issues/6515))
    - Placeholder for dropdown with search doesn't update when language changes ([\#6516](https://github.com/orbeon/orbeon-forms/issues/6516))
    - Escape value of parameter used in HTML label, hint, or help message ([\#6501](https://github.com/orbeon/orbeon-forms/issues/6501))
    - Change of `oxf.fr.detail.captcha.component.*.*` to be taken into account without restarting the server ([\#6520](https://github.com/orbeon/orbeon-forms/issues/6520))
    - `$fr-mode = ('new', 'edit')` as "Run the action in the following case" is always false ([\#6522](https://github.com/orbeon/orbeon-forms/issues/6522))
    - Authorizer WAR file misses some classes ([\#6453](https://github.com/orbeon/orbeon-forms/issues/6453))
    - `auth-war`: use Proguard ([\#6459](https://github.com/orbeon/orbeon-forms/issues/6459))
    - Support `xxf:itemset()` with Dynamic dropdown with search (`databound-select1-search.xbl`) ([\#6444](https://github.com/orbeon/orbeon-forms/issues/6444))
    - Dropdowns "please select" to be localized in Czech and Turkish ([\#6502](https://github.com/orbeon/orbeon-forms/issues/6502))

You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page.

Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon), [Bluesky](https://bsky.app/profile/orbeon.bsky.social), or the [forum](https://groups.google.com/g/orbeon).

We hope you enjoy this release!
