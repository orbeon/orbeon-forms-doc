# Orbeon Forms 2023.1.7

__Thursday, January 29, 2026__

Today we released Orbeon Forms 2023.1.8 PE! This maintenance release contains bug-fixes and new features and is recommended for all users of:

- [Orbeon Forms 2023.1.7 PE](orbeon-forms-2023.1.7.md)
- [Orbeon Forms 2023.1.6 PE](orbeon-forms-2023.1.6.md)
- [Orbeon Forms 2023.1.5 PE](orbeon-forms-2023.1.5.md)
- [Orbeon Forms 2023.1.4 PE](orbeon-forms-2023.1.4.md)
- [Orbeon Forms 2023.1.3 PE](orbeon-forms-2023.1.3.md)
- [Orbeon Forms 2023.1.2 PE](orbeon-forms-2023.1.2.md)
- [Orbeon Forms 2023.1.2 PE](orbeon-forms-2023.1.2.md)
- [Orbeon Forms 2023.1.1 PE](orbeon-forms-2023.1.1.md)
- [Orbeon Forms 2023.1 PE](orbeon-forms-2023.1.md)

## Issues addressed

This release addresses the following issues since [Orbeon Forms 2023.1.7 PE](orbeon-forms-2023.1.7.md):

- Form Builder
    - FB: crash when pasting a section containing a section template ([\#6925](https://github.com/orbeon/orbeon-forms/issues/6925))
- Form Runner
    - Submission, `send()` causing a new tab being opened ([\#6792](https://github.com/orbeon/orbeon-forms/issues/6792))
    - Attachments: setting `allow-download` to `false` doesn't work ([\#6795](https://github.com/orbeon/orbeon-forms/issues/6795))
    - Attachment controls to show a more telling number of digits after the decimal for file sizes ([\#5789](https://github.com/orbeon/orbeon-forms/issues/5789))
    - PDF header/footer lines cannot be removed with CSS ([\#6851](https://github.com/orbeon/orbeon-forms/issues/6851))
    - Add `xf:bind/@name` if missing ([\#6860](https://github.com/orbeon/orbeon-forms/issues/6860))
    - Section with "Page break before" doesn't stick after reopening in Form Builder ([\#6865](https://github.com/orbeon/orbeon-forms/issues/6865))
    - PDF: Long explanatory text causes page break ([\#6903](https://github.com/orbeon/orbeon-forms/issues/6903))
    - Reindex is triggered for attachments ([\#6913](https://github.com/orbeon/orbeon-forms/issues/6913))
    - Control variables in email template parameter formulas not interpreted ([\#6899](https://github.com/orbeon/orbeon-forms/issues/6899))
    - Support `fr:is-readonly-mode()` outside of Form Runner, to support `fr:section` in plain XForms ([\#6983](https://github.com/orbeon/orbeon-forms/issues/6983))
    - Can't use `doc()` in Calculated Value ([\#6836](https://github.com/orbeon/orbeon-forms/issues/6836))
    - Draft filesystem attachments are not supported ([\#7051](https://github.com/orbeon/orbeon-forms/issues/7051))
    - Undeclared variable in XPath expression: $autosave-now ([\#7257](https://github.com/orbeon/orbeon-forms/issues/7257))
    - Java embedding API doesn't work with Tomcat 10+ / WildFly 27+ (jakarta vs javax) ([\#7004](https://github.com/orbeon/orbeon-forms/issues/7004))

You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page.

Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon), [Bluesky](https://bsky.app/profile/orbeon.bsky.social), or the [forum](https://groups.google.com/g/orbeon).

We hope you enjoy this release!
