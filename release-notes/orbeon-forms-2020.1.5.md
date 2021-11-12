# Orbeon Forms 2020.1.4 PE

__Thursday, November 11, 2021__

Today we released Orbeon Forms 2020.1.5 PE. This update contains bug-fixes and is recommended for all [Orbeon Forms 2020.1.4 PE](orbeon-forms-2020.1.4.md), [Orbeon Forms 2020.1.3 PE](orbeon-forms-2020.1.3.md), [Orbeon Forms 2020.1.2 PE](orbeon-forms-2020.1.2.md), [Orbeon Forms 2020.1.1 PE](orbeon-forms-2020.1.1.md) and [Orbeon Forms 2020.1 PE](orbeon-forms-2020.1.md) users.

This release addresses the following issues since [Orbeon Forms 2020.1.4 PE](orbeon-forms-2020.1.4.md):

- "SecurityError: Attempt to use `history.replaceState()` more than 100 times per 30 seconds" ([\#5052](https://github.com/orbeon/orbeon-forms/issues/5052))
- Form Builder to keep `xxf:upload.max-size-aggregate` and other configurations from template ([\#5044](https://github.com/orbeon/orbeon-forms/issues/5044))
- Draft selection page always shows drafts for the latest form version ([\#5043](https://github.com/orbeon/orbeon-forms/issues/5043))
- Hint for single checkbox (`<fr:checkbox-input>`) isn't saved in the form definition ([\#5042](https://github.com/orbeon/orbeon-forms/issues/5042))
- Radios not marked as visited when a value is selected ([\#5040](https://github.com/orbeon/orbeon-forms/issues/5040))
- On Firefox, second click on link to `#` clears the `window.history.state` ([\#5039](https://github.com/orbeon/orbeon-forms/issues/5039))
- When an anchor in an `<xf:output mediatype="text/html">` has the focus, on blur, the focus is set back ([\#5038](https://github.com/orbeon/orbeon-forms/issues/5038))
- Autosave prompt showing when going from view to edit if arrived to the view page directly from the summary page ([\#5033](https://github.com/orbeon/orbeon-forms/issues/5033))
- Can't move cell boundaries with a 24 column grid ([\#5031](https://github.com/orbeon/orbeon-forms/issues/5031))
- Avoid cutting text areas using little vertical space across PDF pages ([\#5027](https://github.com/orbeon/orbeon-forms/issues/5027))
- Attachment metadata doesn't show in PDF template ([\#5023](https://github.com/orbeon/orbeon-forms/issues/5023))
- Don't use minimized/non-combined resources in dev mode ([\#5015](https://github.com/orbeon/orbeon-forms/issues/5015))
- Minimal repeated grid and section: allow tabbing through "-" icons ([\#5010](https://github.com/orbeon/orbeon-forms/issues/5010))
- Minimal repeated section: missing "Remove" icon title on hover ([\#5009](https://github.com/orbeon/orbeon-forms/issues/5009))
- Don't run actions when user is being asked whether to open a draft ([\#5006](https://github.com/orbeon/orbeon-forms/issues/5006))
- Auto-save to take organization-based permissions into account ([\#5004](https://github.com/orbeon/orbeon-forms/issues/5004))
- Date picker to support Danish ([\#5002](https://github.com/orbeon/orbeon-forms/issues/5002))
- reCAPTCHA not to log requests sent to Google ([\#4991](https://github.com/orbeon/orbeon-forms/issues/4991))
- Summary page structure search for dropdown should use exact not substring search ([\#4989](https://github.com/orbeon/orbeon-forms/issues/4989))
- Exact search on Oracle produces an ORA-00932 ([\#4988](https://github.com/orbeon/orbeon-forms/issues/4988))
- Long section titles should wrap ([\#4982](https://github.com/orbeon/orbeon-forms/issues/4982))
- `format-dateTime` not acting on supplied Olson time zone ([\#4981](https://github.com/orbeon/orbeon-forms/issues/4981))
- Explanatory text: clicking on the link icon closes the editor ([\#4807](https://github.com/orbeon/orbeon-forms/issues/4807))

You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page.  
Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon) or the [forum](https://www.orbeon.com/community).

We hope you enjoy this release!
