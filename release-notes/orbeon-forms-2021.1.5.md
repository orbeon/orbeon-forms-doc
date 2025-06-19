# Orbeon Forms 2021.1.5

__Tuesday, October 4, 2022__

Today we released Orbeon Forms 2021.1.5 PE! This maintenance release introduces security fixes, accessibility enhancements, and bug-fixes and is recommended for all users of:

- [Orbeon Forms 2021.1.4 PE](orbeon-forms-2021.1.4.md)
- [Orbeon Forms 2021.1.3 PE](orbeon-forms-2021.1.3.md)
- [Orbeon Forms 2021.1.2 PE](orbeon-forms-2021.1.2.md)
- [Orbeon Forms 2021.1.1 PE](orbeon-forms-2021.1.1.md)
- [Orbeon Forms 2021.1 PE](orbeon-forms-2021.1.md)

This release addresses the following issues since [Orbeon Forms 2021.1.4 PE](orbeon-forms-2021.1.4.md):

- Security
    - Update jquery to 3.6.1 ([\#5434](https://github.com/orbeon/orbeon-forms/issues/5434))
    - Update to TinyMCE 5.10.5
    - Other library updates
- Accessibility
    - Improve focus outline of readonly fields ([\#5442](https://github.com/orbeon/orbeon-forms/issues/5442))
- Embedding
    - Lease "Show in read-only mode" can't be used when embedding ([\#5403](https://github.com/orbeon/orbeon-forms/issues/5403))
    - JS embedding fails with `inline-resources=true` ([\#5449](https://github.com/orbeon/orbeon-forms/issues/5449))
    - Embedding: missing namespacing of some `id`s in XHR response ([\#5448](https://github.com/orbeon/orbeon-forms/issues/5448))
    - JS embedding: TinyMCE error in the second form ([\#5446](https://github.com/orbeon/orbeon-forms/issues/5446))
    - Concurrent use of `embedForm` can lead to malfunctioning form ([\#5438](https://github.com/orbeon/orbeon-forms/issues/5438))
    - With JavaScript embedding, loading indicator stuck when switching form ([\#5413](https://github.com/orbeon/orbeon-forms/issues/5413))
    - With the JavaScript embedding API, occasional error when switching form: `TypeError: Argument 1 ('child') to Node.removeChild must be an instance of Node` ([\#5412](https://github.com/orbeon/orbeon-forms/issues/5412))
- Form Builder
    - Search Controls only opens the top-level section ([\#5348](https://github.com/orbeon/orbeon-forms/issues/5348))
    - Split/merge icons stop working after inserting new section ([\#5361](https://github.com/orbeon/orbeon-forms/issues/5361))
    - Offline test: "Unknown time-zone ID: America/Los_Angeles" error ([\#5472](https://github.com/orbeon/orbeon-forms/issues/5472))
- Form Runner
    - `fr:control-clear` doesn't clear multiple attachments ([\#5471](https://github.com/orbeon/orbeon-forms/issues/5471))
    - Summary search: "Dropdown with Other" only shows static options ([\#5408](https://github.com/orbeon/orbeon-forms/issues/5408))
    - Summary search doesn't do exact match for open single-selection ([\#5410](https://github.com/orbeon/orbeon-forms/issues/5410))
    - Sending `pdf-url` doesn't have `<url>` root element as documented ([\#5460](https://github.com/orbeon/orbeon-forms/issues/5460))
    - Misaligned label with required single checkbox with HTML label ([\#5459](https://github.com/orbeon/orbeon-forms/issues/5459))
    - Issues when a boolean input readonly MIP changes ([\#5427](https://github.com/orbeon/orbeon-forms/issues/5427))
    - No default filenames for PDF and other downloads ([\#5456](https://github.com/orbeon/orbeon-forms/issues/5456))
    - Date picker shown for readonly date on iOS ([\#5376](https://github.com/orbeon/orbeon-forms/issues/5376))
    - Getting a message with placeholders when deleting a service ([\#5424](https://github.com/orbeon/orbeon-forms/issues/5424))
    - Help tooltip for with currency field overlaps suffix ([\#5421](https://github.com/orbeon/orbeon-forms/issues/5421))
    - Setting `xxf:format.input.date` on the model doesn't work ([\#5422](https://github.com/orbeon/orbeon-forms/issues/5422))
    - Incorrect `fr:data-format-version` in some cases ([\#5420](https://github.com/orbeon/orbeon-forms/issues/5420))
    - Placeholder hint also shows in repeated grid header ([\#5401](https://github.com/orbeon/orbeon-forms/issues/5401))
    - `encoded string too long` error with a form ([\#5404](https://github.com/orbeon/orbeon-forms/issues/5404))
    - Review possible regression with two-pass submission paths ([\#5398](https://github.com/orbeon/orbeon-forms/issues/5398))
    - Errors in Polish resources ([\#5394](https://github.com/orbeon/orbeon-forms/issues/5394))
- Other
    - "Prefix `xxf` has not been declared" with AVT and non-default namespace mapping ([\#5428](https://github.com/orbeon/orbeon-forms/issues/5428))
    - Broken test due to external service down ([\#5425](https://github.com/orbeon/orbeon-forms/issues/5425))
    - Error when switching between HTTP/HTTPS on the same app ([\#5395](https://github.com/orbeon/orbeon-forms/issues/5395))

You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page.

Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon), [Bluesky](https://bsky.app/profile/orbeon.bsky.social), or the [forum](https://groups.google.com/g/orbeon).

We hope you enjoy this release!
