# Orbeon Forms 2022.1.8

__Friday, September 6, 2024__

Today we released Orbeon Forms 2022.1.8 PE! This maintenance release contains bug-fixes and is recommended for all users of:

- [Orbeon Forms 2022.1.7 PE](orbeon-forms-2022.1.7.md)
- [Orbeon Forms 2022.1.6 PE](orbeon-forms-2022.1.6.md)
- [Orbeon Forms 2022.1.5 PE](orbeon-forms-2022.1.5.md)
- [Orbeon Forms 2022.1.4 PE](orbeon-forms-2022.1.4.md)
- [Orbeon Forms 2022.1.3 PE](orbeon-forms-2022.1.3.md)
- [Orbeon Forms 2022.1.2 PE](orbeon-forms-2022.1.2.md)
- [Orbeon Forms 2022.1.1 PE](orbeon-forms-2022.1.1.md)
- [Orbeon Forms 2022.1 PE](orbeon-forms-2022.1.md)

This release addresses the following issues since [Orbeon Forms 2022.1.7 PE](orbeon-forms-2022.1.7.md):

- Security
    - CVE-2018-18531 in Kaptcha ([\#6382](https://github.com/orbeon/orbeon-forms/issues/6382)) 
- Form Builder
    - Type with custom error message must not store xs:string ([\#6252](https://github.com/orbeon/orbeon-forms/issues/6252))
    - "Dropdown with Other" truncates entries in Number field settings ([\#6308](https://github.com/orbeon/orbeon-forms/issues/6308))
    - Parameters editor should never allow entering two parameters with the same name ([\#6368](https://github.com/orbeon/orbeon-forms/issues/6368))
    - Horizontal scrollbar in toolbox when version dropdown displayed ([\#6358](https://github.com/orbeon/orbeon-forms/issues/6358))
    - Form Builder displays gray zone below form in some cases ([\#6376](https://github.com/orbeon/orbeon-forms/issues/6376))
    - Control label editor sometimes appears below the control ([\#6384](https://github.com/orbeon/orbeon-forms/issues/6384))
    - "form not found" when closing Control Settings ([\#6277](https://github.com/orbeon/orbeon-forms/issues/6277))
- Form Runner
    - Rapid changes on Angular component can lead to multiple forms being embedded in the page ([\#6251](https://github.com/orbeon/orbeon-forms/issues/6251))
    - JavaScript Embedding API: no error when trying to embed a non-existing form ([\#6195](https://github.com/orbeon/orbeon-forms/issues/6195))
    - Don't create session when loading font files and for `OPTIONS` ([\#6019](https://github.com/orbeon/orbeon-forms/issues/6019))
    - Don't produce `role="textbox"` on input for date and time ([\#6270](https://github.com/orbeon/orbeon-forms/issues/6270))
    - Radio button, checkboxes incorrectly checked ([\#6272](https://github.com/orbeon/orbeon-forms/issues/6272))
    - Schema to be aware of the dynamic dropdown `itemset-empty` attribute ([\#6022](https://github.com/orbeon/orbeon-forms/issues/6022))
    - Scala.js TypeError: Cannot convert undefined or null to object ([\#6287](https://github.com/orbeon/orbeon-forms/issues/6287))
    - reCAPTCHA to support reset ([\#6291](https://github.com/orbeon/orbeon-forms/issues/6291))
    - "Single Checkbox with Label on Top" crashes when dynamically shown ([\#6339](https://github.com/orbeon/orbeon-forms/issues/6339))
    - TOC section stays highlighted ([\#6336](https://github.com/orbeon/orbeon-forms/issues/6336))
    - XPath errors using `fr:date` in plain XForms ([\#6347](https://github.com/orbeon/orbeon-forms/issues/6347))
    - Selected radio button and checkboxes are not showing in high contrast ([\#6350](https://github.com/orbeon/orbeon-forms/issues/6350))
    - Unwanted black rectangle between fields and alerts in the high contrast on Windows ([\#6349](https://github.com/orbeon/orbeon-forms/issues/6349))
    - Failed to execute 'add' on 'DOMTokenList': The token provided must not be empty ([\#6383](https://github.com/orbeon/orbeon-forms/issues/6383))
    - Excel file can't be opened in Excel when the language is set to German ([\#6391](https://github.com/orbeon/orbeon-forms/issues/6391))
    - Warnings presented as errors in Error Summary ([\#6410](https://github.com/orbeon/orbeon-forms/issues/6410))
    - Control cleared upon `xforms-disabled` doesn't recalculate ([\#6370](https://github.com/orbeon/orbeon-forms/issues/6370))
    - Possible `rebuild` followed by `refresh` but without `recalculate` ([\#6442](https://github.com/orbeon/orbeon-forms/issues/6442))
    - Chained service calls possibly causing connection pool starvation ([\#6466](https://github.com/orbeon/orbeon-forms/issues/6466))
    - Can't parse JSON from context with `xxf:json-to-xml()` ([\#6470](https://github.com/orbeon/orbeon-forms/issues/6470))
    - `xxf:json-to-xml()` fails instead of returning an empty sequence when the JSON is invalid ([\#6472](https://github.com/orbeon/orbeon-forms/issues/6472))

- You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page.

Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon), [Mastodon](https://mastodon.social/@orbeon), or the [forum](https://www.orbeon.com/community).

We hope you enjoy this release!
