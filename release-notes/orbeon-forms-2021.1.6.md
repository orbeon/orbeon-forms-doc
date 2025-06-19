# Orbeon Forms 2021.1.6

__Tuesday, November 7, 2022__

Today we released Orbeon Forms 2021.1.6 PE! This maintenance release introduces bug-fixes and is recommended for all users of:

- [Orbeon Forms 2021.1.5 PE](orbeon-forms-2021.1.5.md)
- [Orbeon Forms 2021.1.4 PE](orbeon-forms-2021.1.4.md)
- [Orbeon Forms 2021.1.3 PE](orbeon-forms-2021.1.3.md)
- [Orbeon Forms 2021.1.2 PE](orbeon-forms-2021.1.2.md)
- [Orbeon Forms 2021.1.1 PE](orbeon-forms-2021.1.1.md)
- [Orbeon Forms 2021.1 PE](orbeon-forms-2021.1.md)

This release addresses the following issues since [Orbeon Forms 2021.1.5 PE](orbeon-forms-2021.1.5.md):

- Form Builder
    - Property to disable owner/group permissions in the Permissions dialog ([\#5488](https://github.com/orbeon/orbeon-forms/issues/5488)) 
- Form Runner
    - Incorrect display of repeated numbered readonly grids with CSS grids ([\#5480](https://github.com/orbeon/orbeon-forms/issues/5480))
    - Incorrect display of some CSS grids in view mode ([\#5481](https://github.com/orbeon/orbeon-forms/issues/5481))
    - Missing space between static readonly control and top of grid ([\#5476](https://github.com/orbeon/orbeon-forms/issues/5476))
    - Repetition number not shown in repeated grid with appearance "minimal" ([\#5475](https://github.com/orbeon/orbeon-forms/issues/5475))
    - PDF: Checkboxes should be different from radio buttons ([\#5482](https://github.com/orbeon/orbeon-forms/issues/5482))
    - Enabling the reCAPTCHA leads to a `Cannot read properties of undefined (reading 'internalShortDelay')` ([\#5496](https://github.com/orbeon/orbeon-forms/issues/5496))
    - `fr:datetime` shows labels for Date and Time ([\#5504](https://github.com/orbeon/orbeon-forms/issues/5504))
    - Misaligned label with required answer with HTML label ([\#5505](https://github.com/orbeon/orbeon-forms/issues/5505))
    - Entering invalid time doesn't cause control invalidity ([\#5474](https://github.com/orbeon/orbeon-forms/issues/5474))
    - Search dropdowns on Summary page are too wide ([\#5507](https://github.com/orbeon/orbeon-forms/issues/5507))
    - Inline `<xh:style>` works online but not offline ([\#5509](https://github.com/orbeon/orbeon-forms/issues/5509))
    - If a row doesn't contain long content, try keeping all cells on the same page in the PDF ([\#5511](https://github.com/orbeon/orbeon-forms/issues/5511))
    - Form Builder summary field search for app, form… doing exact instead of substring match ([\#5499](https://github.com/orbeon/orbeon-forms/issues/5499))
    - Form crashes when including a template that exist both in app and global ([\#5513](https://github.com/orbeon/orbeon-forms/issues/5513))
    - Readonly Formatted Text Area in initially collapsed section can be edited ([\#5515](https://github.com/orbeon/orbeon-forms/issues/5515))
    - Excel export produces invalid Excel file with C1_* name ([\#5514](https://github.com/orbeon/orbeon-forms/issues/5514))
- Other
    - No output in log file ([\#5498](https://github.com/orbeon/orbeon-forms/issues/5498)) 

You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page.

Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon), [Bluesky](https://bsky.app/profile/orbeon.bsky.social), or the [forum](https://groups.google.com/g/orbeon).

We hope you enjoy this release!
