# Orbeon Forms 2022.1.4

__Wednesday, May 30, 2023__

Today we released Orbeon Forms 2022.1.4 PE! This maintenance release contains bug-fixes and minor new features and is recommended for all users of:

- [Orbeon Forms 2022.1.3 PE](orbeon-forms-2022.1.3.md)
- [Orbeon Forms 2022.1.2 PE](orbeon-forms-2022.1.2.md)
- [Orbeon Forms 2022.1.1 PE](orbeon-forms-2022.1.1.md)
- [Orbeon Forms 2022.1 PE](orbeon-forms-2022.1.md)

This release also addresses the following issues since [Orbeon Forms 2022.1.3 PE](orbeon-forms-2022.1.3.md):

- Form Builder
    - Warn user when referring to a non-existent variable in a formula ([\#4028](https://github.com/orbeon/orbeon-forms/issues/4028)) 
    - Default form template contains `xforms:` prefix for grid bind ([\#5812](https://github.com/orbeon/orbeon-forms/issues/5812))
    - Improve Edit Source performance with large form definitions ([\#5830](https://github.com/orbeon/orbeon-forms/issues/5830))
    - Assertion failed in `setInlineInstance`, `XFormsModel.scala` ([\#5772](https://github.com/orbeon/orbeon-forms/issues/5772))
    - Test PDF dialog doesn't show backdrop over Test window ([\#5779](https://github.com/orbeon/orbeon-forms/issues/5779))
    - Email dialog value for "Attach files users uploaded to the form" is ignored ([\#5777](https://github.com/orbeon/orbeon-forms/issues/5777))
    - "Errors" label in "Control Settings" is not localized ([\#5791](https://github.com/orbeon/orbeon-forms/issues/5791))
- Form Runner
    - Long input field truncated in PDF file ([\#5716](https://github.com/orbeon/orbeon-forms/issues/5716))
    - Occasional blank page after click on "Review" ([\#5719](https://github.com/orbeon/orbeon-forms/issues/5719))
    - Uploading a file of zero bytes causes an error (again) ([\#5827](https://github.com/orbeon/orbeon-forms/issues/5827))
    - French message for upload scan error shows "dun" instead of "d'un" and the placeholder may not be replaced ([\#5823](https://github.com/orbeon/orbeon-forms/issues/5823))
    - Readonly grid shows incorrectly ([\#5743](https://github.com/orbeon/orbeon-forms/issues/5743))
    - Server sends date with timezone to client for date field ([\#5822](https://github.com/orbeon/orbeon-forms/issues/5822))
    - Dynamic Dropdown with Search doesn't show initial value in `/view` and PDF ([\#5806](https://github.com/orbeon/orbeon-forms/issues/5806))
    - Element bound to a dynamic dropdown with search to NOT always have a `@label` ([\#5816](https://github.com/orbeon/orbeon-forms/issues/5816))
    - `fr|databound-select1-search` to show missing label in view modes when possible ([\#5813](https://github.com/orbeon/orbeon-forms/issues/5813))
    - `xf:input` with date `xf:type` child element is not migrated to `fr:date` ([\#5810](https://github.com/orbeon/orbeon-forms/issues/5810))
    - "could not read asset to aggregate" error ([\#5809](https://github.com/orbeon/orbeon-forms/issues/5809))
    - When data is passed with the `multipart/form-data` encoding, `data-migration-behavior` on the URL is ignored ([\#5802](https://github.com/orbeon/orbeon-forms/issues/5802))
    - Checkbox with HTML label doesn't display correctly ([\#5803](https://github.com/orbeon/orbeon-forms/issues/5803))
    - Line height is incorrect in explanatory text in PDF ([\#5800](https://github.com/orbeon/orbeon-forms/issues/5800))
    - Forms administration page not to start `ping-event` unless there is a background process running ([\#5771](https://github.com/orbeon/orbeon-forms/issues/5771))
    - `xxf:sort()` to support `lang`, `collation`, and `stable` parameters ([\#5794](https://github.com/orbeon/orbeon-forms/issues/5794))
    - Prevent caching of response to `/xforms-server/baseline.js` ([\#5786](https://github.com/orbeon/orbeon-forms/issues/5786))
    - Dropdown with search doesn't get `aria-describedby` pointing to help and hint ([\#5785](https://github.com/orbeon/orbeon-forms/issues/5785))
    - Error caused by `addScrollPadding()` ([\#5774](https://github.com/orbeon/orbeon-forms/issues/5774))
    - SQL Processor storing `DATE` in RFC 1123 instead of ISO format ([\#5775](https://github.com/orbeon/orbeon-forms/issues/5775))
    - Log details when simple data migration finds a problem ([\#5761](https://github.com/orbeon/orbeon-forms/issues/5761))
    - 2021.1 form definition with `<fr:param type="LinkToPdfParam">` fails to import in 2022.1 ([\#5838](https://github.com/orbeon/orbeon-forms/issues/5838))

You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page.

Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon), [Mastodon](https://mastodon.social/@orbeon), or the [forum](https://www.orbeon.com/community).

We hope you enjoy this release!
