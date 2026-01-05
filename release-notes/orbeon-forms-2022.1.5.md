# Orbeon Forms 2022.1.5

__Wednesday, September 20, 2023__

Today we released Orbeon Forms 2022.1.5 PE! This maintenance release contains bug-fixes and minor new features and is recommended for all users of:

- [Orbeon Forms 2022.1.4 PE](orbeon-forms-2022.1.4.md)
- [Orbeon Forms 2022.1.3 PE](orbeon-forms-2022.1.3.md)
- [Orbeon Forms 2022.1.2 PE](orbeon-forms-2022.1.2.md)
- [Orbeon Forms 2022.1.1 PE](orbeon-forms-2022.1.1.md)
- [Orbeon Forms 2022.1 PE](orbeon-forms-2022.1.md)

This release also addresses the following issues since [Orbeon Forms 2022.1.4 PE](orbeon-forms-2022.1.4.md):

- Security
    - Remove old XPath and Transformations Sandbox ([\#5943](https://github.com/orbeon/orbeon-forms/issues/5943))
    - Upgrade to bcmail 1.75 ([\#5877](https://github.com/orbeon/orbeon-forms/issues/5877))
    - With restrictive content security policy, click on the help icon navigates away form the form ([\#5951](https://github.com/orbeon/orbeon-forms/issues/5951))
- Form Builder
    - "Test PDF" must produce meaningful filename ([\#5105](https://github.com/orbeon/orbeon-forms/issues/5105))
    - Form Builder: search by form name yields unexpected results ([\#5928](https://github.com/orbeon/orbeon-forms/issues/5928))
    - Occasional error when loading Form Builder Summary page ([\#5872](https://github.com/orbeon/orbeon-forms/issues/5872))
    - Limit app/form name to 255 characters ([\#5848](https://github.com/orbeon/orbeon-forms/issues/5848))
    - Form Builder should not add `bind` to custom dialog in form definition ([\#5733](https://github.com/orbeon/orbeon-forms/issues/5733))
    - Section label editor closes on heartbeat ([\#5841](https://github.com/orbeon/orbeon-forms/issues/5841))
    - "All Control Values" doesn't show in email template parameter dropdown ([\#5927](https://github.com/orbeon/orbeon-forms/issues/5927))
    - Form Builder not to add an empty `@xml:lang` on templates, and do support empty `@xml:lang` at runtime ([\#5908](https://github.com/orbeon/orbeon-forms/issues/5908))
    - References to `$form-resources` in formulas now considered invalid ([\#5909](https://github.com/orbeon/orbeon-forms/issues/5909))
    - JavaScript error in Form Builder when copying a Formatted Text Area ([\#5963](https://github.com/orbeon/orbeon-forms/issues/5963))
- Form Runner
    - Email
        - XML attached to email is always in `edge` data format ([\#5911](https://github.com/orbeon/orbeon-forms/issues/5911))
        - Possible issue with empty parameter name when sending email ([\#5900](https://github.com/orbeon/orbeon-forms/issues/5900))
        - `email` action support for new parameter `match = "all"` ([\#5938](https://github.com/orbeon/orbeon-forms/issues/5938))
        - Possible incorrect email template conversion #5923
    - PDF
        - `fr:attachment` in PDF must not contain link to download ([\#5917](https://github.com/orbeon/orbeon-forms/issues/5917))
        - Properties to configure automatic PDF accessibility and PDF/A settings ([\#5914](https://github.com/orbeon/orbeon-forms/issues/5914))
        - Special characters not showing in PDF ([\#5881](https://github.com/orbeon/orbeon-forms/issues/5881)) 
        - 500 when testing PDF template from Form Builder ([\#5894](https://github.com/orbeon/orbeon-forms/issues/5894))
        - PDF table of contents to include repeated sections titles ([\#5855](https://github.com/orbeon/orbeon-forms/issues/5855))
        - Image not showing in PDF produced from template ([\#5893](https://github.com/orbeon/orbeon-forms/issues/5893))
        - Disable PDF parameters for page paths ([\#5918](https://github.com/orbeon/orbeon-forms/issues/5918))
        - Automatic PDF generation can be extremely slow on large forms ([\#5888](https://github.com/orbeon/orbeon-forms/issues/5888))
    - Other
        - Session heartbeat request sent too frequently ([\#5889](https://github.com/orbeon/orbeon-forms/issues/5889))
        - Excel import: show errors immediately in review data page ([\#5722](https://github.com/orbeon/orbeon-forms/issues/5722))
        - Excel/XML export button on Summary page ([\#5264](https://github.com/orbeon/orbeon-forms/issues/5264))
        - Automatically visit changed visible fields ([\#5934](https://github.com/orbeon/orbeon-forms/issues/5934))
        - Support for the JCache API (JSR-107) ([\#5399](https://github.com/orbeon/orbeon-forms/issues/5399))
        - Support presence of multiple caching providers ([\#5937](https://github.com/orbeon/orbeon-forms/issues/5937))
        - Missing "link" between field or group and alert ([\#5932](https://github.com/orbeon/orbeon-forms/issues/5932))
        - Focus on first invalid field after closing Errors/Validation dialog ([\#5883](https://github.com/orbeon/orbeon-forms/issues/5883))
        - `fr:simple-captcha` must update its status when the user sets a value ([\#5920](https://github.com/orbeon/orbeon-forms/issues/5920))
        - Variable notation not working in action condition ([\#5919](https://github.com/orbeon/orbeon-forms/issues/5919))
        - Session heartbeat request sent too frequently ([\#5889](https://github.com/orbeon/orbeon-forms/issues/5889))
        - Search API can generate invalid SQL ([\#5862](https://github.com/orbeon/orbeon-forms/issues/5862))
        - Don't let buttons overflow grids ([\#5851](https://github.com/orbeon/orbeon-forms/issues/5851))
        - Minimal read-write repeated grid doesn't show properly ([\#5840](https://github.com/orbeon/orbeon-forms/issues/5840))
        - `content.css` pollutes top-level page ([\#5962](https://github.com/orbeon/orbeon-forms/issues/5962)) 
        - Support cross-site embedding with the Form Runner JavaScript embedding API ([\#5974](https://github.com/orbeon/orbeon-forms/issues/5974))
        - Workflow stage is lost when going to the view page ([\#5984](https://github.com/orbeon/orbeon-forms/issues/5984))
        - Improve handling of expired session on the client ([\#5678](https://github.com/orbeon/orbeon-forms/issues/5678))

You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page.

Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon), [Bluesky](https://bsky.app/profile/orbeon.bsky.social), or the [forum](https://groups.google.com/g/orbeon).

We hope you enjoy this release!
