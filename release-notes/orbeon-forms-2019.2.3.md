# Orbeon Forms 2019.2.3 PE

__Monday, February 15, 2021__

Today we released Orbeon Forms 2019.2.3 PE. This maintenance release contains bug-fixes and is recommended for all users of:

- [Orbeon Forms 2019.2.2 PE](orbeon-forms-2019.2.2.md)
- [Orbeon Forms 2019.2.1 PE](orbeon-forms-2019.2.1.md)
- [Orbeon Forms 2019.2 PE](orbeon-forms-2019.2.md)

This release addresses the following issues since [Orbeon Forms 2019.2.2 PE](orbeon-forms-2019.2.2.md):

- Chrome 81 beta: insert row icons don't show anymore in grids ([\#4470](https://github.com/orbeon/orbeon-forms/issues/4470))
- Missing `fr:relevant="false"` ([\#4519](https://github.com/orbeon/orbeon-forms/issues/4519))
- Coming back to wizard page with `visited` event messes up MIPs ([\#4506](https://github.com/orbeon/orbeon-forms/issues/4506))
- `visible` and other Form Runner action listeners can run multiple times ([\#4505](https://github.com/orbeon/orbeon-forms/issues/4505))
- Modal button spinner disappears upon click with `replace="all"` ([\#4511](https://github.com/orbeon/orbeon-forms/issues/4511))
- Extra labels show in the "Validations and Alerts" settings ([\#4523](https://github.com/orbeon/orbeon-forms/issues/4523))
- Selected value of Dynamic dropdown with search, but without the service doing the search, not shown in `view` and `pdf` modes ([\#4525](https://github.com/orbeon/orbeon-forms/issues/4525))
- `javascript-lifecycle`'s `destroy()` is not called as expected ([\#4541](https://github.com/orbeon/orbeon-forms/issues/4541))
- Error dialog for attachment control requires multiple clicks to close ([\#4543](https://github.com/orbeon/orbeon-forms/issues/4543))
- Control name sometimes doesn't update with "next" ([\#4503](https://github.com/orbeon/orbeon-forms/issues/4503))
- Action editor fails for controls ending with `-control` ([\#4518](https://github.com/orbeon/orbeon-forms/issues/4518))
- Mediatype or size error in attachment only shows generic error message ([\#4574](https://github.com/orbeon/orbeon-forms/issues/4574))
- Allow larger headers in upload payload ([\#4579](https://github.com/orbeon/orbeon-forms/issues/4579))
- Submission headers should not be filtered ([\#4606](https://github.com/orbeon/orbeon-forms/issues/4606))
- Incorrect formatting or numbers with lots of digits ([\#4687](https://github.com/orbeon/orbeon-forms/issues/4687))
- Wizard always sets the focus on the first control, doesn't honor `oxf.fr.detail.initial-focus.*.*` ([\#4711](https://github.com/orbeon/orbeon-forms/issues/4711))
- Required static dropdown with search shows as invalid on click ([\#4401](https://github.com/orbeon/orbeon-forms/issues/4401))
- Consider issuing DELETE to `orbeon_i_control_text` only after checking we know we have something to delete ([\#4487](https://github.com/orbeon/orbeon-forms/issues/4487))
- Control is reset upon selection in Actions Editor / Service Response Action ([\#4568](https://github.com/orbeon/orbeon-forms/issues/4568))
- Uploading a file of zero bytes causes an error ([\#4466](https://github.com/orbeon/orbeon-forms/issues/4466))
- Java 11: new isBlank method conflicts with our extension isBlank ([\#4490](https://github.com/orbeon/orbeon-forms/issues/4490))
- On file upload, URL in data is not change from URL to temp file to URL to persistence API ([\#4684](https://github.com/orbeon/orbeon-forms/issues/4684))
- Backward compatible mode for the summary to show data across all versions ([\#4699](https://github.com/orbeon/orbeon-forms/issues/4699))
- List of checkboxes inelegantly split between 2 pages in PDF ([\#4715](https://github.com/orbeon/orbeon-forms/issues/4715))
- Confirmation dialog stays open on second run of the process ([\#4741](https://github.com/orbeon/orbeon-forms/issues/4741))
- Incorrect tooltip positioning for single checkbox ([\#4672](https://github.com/orbeon/orbeon-forms/issues/4672))
- `File` with incorrect path passed to `FileScan.complete()` ([\#4745](https://github.com/orbeon/orbeon-forms/issues/4745))


You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page.

Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon), [Bluesky](https://bsky.app/profile/orbeon.bsky.social), or the [forum](https://groups.google.com/g/orbeon).

We hope you enjoy this release!
