# Orbeon Forms 2022.1.7

__Friday, April 5, 2024__

Today we released Orbeon Forms 2022.1.7 PE! This maintenance release contains bug-fixes and minor new features and is recommended for all users of:

- [Orbeon Forms 2022.1.6 PE](orbeon-forms-2022.1.6.md)
- [Orbeon Forms 2022.1.5 PE](orbeon-forms-2022.1.5.md)
- [Orbeon Forms 2022.1.4 PE](orbeon-forms-2022.1.4.md)
- [Orbeon Forms 2022.1.3 PE](orbeon-forms-2022.1.3.md)
- [Orbeon Forms 2022.1.2 PE](orbeon-forms-2022.1.2.md)
- [Orbeon Forms 2022.1.1 PE](orbeon-forms-2022.1.1.md)
- [Orbeon Forms 2022.1 PE](orbeon-forms-2022.1.md)

This release also addresses the following issues since [Orbeon Forms 2022.1.6 PE](orbeon-forms-2022.1.6.md):

- Security
    - Upgrade to TinyMCE 6 ([\#6249](https://github.com/orbeon/orbeon-forms/issues/6249))
- Form Builder
    - "Test Offline" uses inline script ([\#6242](https://github.com/orbeon/orbeon-forms/issues/6242))
    - References to `$fr-mode` in formulas now considered invalid ([\#6220](https://github.com/orbeon/orbeon-forms/issues/6220))
    - Choices Editor reads/updates the incorrect repeated control ([\#6182](https://github.com/orbeon/orbeon-forms/issues/6182))
    - In Email Settings dialog, Email templates tab, control dropdowns of different widths ([\#6205](https://github.com/orbeon/orbeon-forms/issues/6205))
    - FB: "Add Language" dialog doesn't get positioned ([\#5569](https://github.com/orbeon/orbeon-forms/issues/5569))
- Form Runner
    - `role` and ARIA attributes for radio buttons and checkboxes in static readonly ([\#6235](https://github.com/orbeon/orbeon-forms/issues/6235))
    - Dropdown part in Dropdown with "Other" overflowing when labels are very long ([\#6233](https://github.com/orbeon/orbeon-forms/issues/6233))
    - Excel export: mismatch of fields in repeated sections or grids ([\#6128](https://github.com/orbeon/orbeon-forms/issues/6128))
    - `AjaxEventQueue` can get blocked when a response arrives for a form that has been removed ([\#6200](https://github.com/orbeon/orbeon-forms/issues/6200))
    - Support serving files with the `xml` extension ([\#6214](https://github.com/orbeon/orbeon-forms/issues/6214))
    - Image annotation tooltip "stroke colorr" with two "r" ([\#6208](https://github.com/orbeon/orbeon-forms/issues/6208))
    - Improved PDF font fallback ([\#6196](https://github.com/orbeon/orbeon-forms/issues/6196))
    - `name` and `controlName` for email parameters lost during conversion ([\#6202](https://github.com/orbeon/orbeon-forms/issues/6202))
    - "Malformed class name" error in the `orbeon.log` ([\#6186](https://github.com/orbeon/orbeon-forms/issues/6186))
    - "_.isUndefined is not a function" when testing with Subject7 ([\#6126](https://github.com/orbeon/orbeon-forms/issues/6126))
    - Multiple elements with `id="xforms-select-full-template"` and `id="xforms-select1-full-template"` ([\#6224](https://github.com/orbeon/orbeon-forms/issues/6224))

You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page.

Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon), [Mastodon](https://mastodon.social/@orbeon), or the [forum](https://www.orbeon.com/community).

We hope you enjoy this release!
