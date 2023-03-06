# Orbeon Forms 2021.1.3

__Wednesday, April 27, 2022__

Today we released Orbeon Forms 2021.1.3 PE! This maintenance release introduces bug-fixes and accessibility enhancements and is recommended for all users of:

- [Orbeon Forms 2021.1.2 PE](orbeon-forms-2021.1.2.md)
- [Orbeon Forms 2021.1.1 PE](orbeon-forms-2021.1.1.md)
- [Orbeon Forms 2021.1 PE](orbeon-forms-2021.1.md)

This release addresses the following issues since [Orbeon Forms 2021.1.2 PE](orbeon-forms-2021.1.2.md):

- PDF: field borders are too close from each other ([\#5268](https://github.com/orbeon/orbeon-forms/issues/5268))
- `xxf:r()` function doesn't reevaluate if 3rd parameter is a `map` ([\#5305](https://github.com/orbeon/orbeon-forms/issues/5305))
- "Unsupported field: Year" in the SQL processor ([\#5307](https://github.com/orbeon/orbeon-forms/issues/5307))
- Never use CSS grid for the automatic PDF ([\#5301](https://github.com/orbeon/orbeon-forms/issues/5301))
- File uploaded to element with `type="xf:base64Binary"` encoded incorrectly ([\#5297](https://github.com/orbeon/orbeon-forms/issues/5297))
- "window.document.getElementById(…) is null" with embedded Form Builder ([\#5296](https://github.com/orbeon/orbeon-forms/issues/5296))
- Remove EOL Apache Commons libraries ([\#5292](https://github.com/orbeon/orbeon-forms/issues/5292))
- Explanatory Text doesn't show in PDF template ([\#5290](https://github.com/orbeon/orbeon-forms/issues/5290))
- WAR file is larger with 2021.1 ([\#5250](https://github.com/orbeon/orbeon-forms/issues/5250))
- Allow admins to configure the behavior of the Version dropdown of the Publish dialog ([\#5281](https://github.com/orbeon/orbeon-forms/issues/5281))
- `NoSuchElementException` when saving data ([\#5275](https://github.com/orbeon/orbeon-forms/issues/5275))
- Handwritten Signature: regression of ([\#2670](https://github.com/orbeon/orbeon-forms/issues/2670)) ([\#5273](https://github.com/orbeon/orbeon-forms/issues/5273))
- "Can't find variable: google" when a form using `fr:map` loaded is through the Form Runner embedding API ([\#5269](https://github.com/orbeon/orbeon-forms/issues/5269))
- Offline: Formatted Text Area doesn't work ([\#5262](https://github.com/orbeon/orbeon-forms/issues/5262))
- Error at runtime with cell expanded both horizontally and vertically ([\#5260](https://github.com/orbeon/orbeon-forms/issues/5260))
- Months in German on the summary page to be in uppercase ([\#5261](https://github.com/orbeon/orbeon-forms/issues/5261))
- Remove Link Button from the toolbox ([\#5265](https://github.com/orbeon/orbeon-forms/issues/5265))
- "Prefix xxf has not been declared" ([\#5279](https://github.com/orbeon/orbeon-forms/issues/5279))
- `oxf.fr.default-timezone`: `MatchError` with "Z" timezone ([\#5287](https://github.com/orbeon/orbeon-forms/issues/5287))
- Update function to access the value of a control within a section template ([\#5246](https://github.com/orbeon/orbeon-forms/issues/5246))
- Excel/XML export button on detail page ([\#5263](https://github.com/orbeon/orbeon-forms/issues/5263))

You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page.

Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon), [Mastodon](https://mastodon.social/@orbeon), or the [forum](https://www.orbeon.com/community).

We hope you enjoy this release!
