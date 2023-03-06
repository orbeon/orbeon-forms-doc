# Orbeon Forms 2019.2.4 PE

__Friday, December 17, 2021__

Today we released Orbeon Forms 2019.2.4 PE. This maintenance release contains security updates and bug-fixes and is recommended for all users of:

- [Orbeon Forms 2019.2.3 PE](orbeon-forms-2019.2.3.md)
- [Orbeon Forms 2019.2.2 PE](orbeon-forms-2019.2.2.md)
- [Orbeon Forms 2019.2.1 PE](orbeon-forms-2019.2.1.md)
- [Orbeon Forms 2019.2 PE](orbeon-forms-2019.2.md)

This release addresses the following issues since [Orbeon Forms 2019.2.3 PE](orbeon-forms-2019.2.3.md):

- Upgrade to log4j 2.x ([\#4373](https://github.com/orbeon/orbeon-forms/issues/4373))
- Attachment control shouldn't get the focus in review ([\#4860](https://github.com/orbeon/orbeon-forms/issues/4860))
- Optimize SQL query finding the form version for a give document ([\#4796](https://github.com/orbeon/orbeon-forms/issues/4796))
- XBL companion `destroy` method to be called when switching wizard page ([\#4372](https://github.com/orbeon/orbeon-forms/issues/4372))
- `data-format-version` parameter should be optional when database is not in 4.0.0 format ([\#4861](https://github.com/orbeon/orbeon-forms/issues/4861))
- Form Builder library version doesn't stick ([\#4875](https://github.com/orbeon/orbeon-forms/issues/4875))
- Error when publishing a form with attached files from Form Builder ([\#4765](https://github.com/orbeon/orbeon-forms/issues/4765))
- Form without migrations doesn't set `fr:relevant` attributes ([\#4911](https://github.com/orbeon/orbeon-forms/issues/4911))
- Upload field shown as empty at the end of an upload ([\#5035](https://github.com/orbeon/orbeon-forms/issues/5035))
- Third-party library upgrades

For more on the Log4j vulnerability issue, see our [blog post](https://blog.orbeon.com/2021/12/vulnerability-in-log4j-library.html).

UPDATES:

- 2019.2.4.1 upgrades Log4j to version 2.17.1 to address the latest vulnerabilities

You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page.

Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon), [Mastodon](https://mastodon.social/@orbeon), or the [forum](https://www.orbeon.com/community).

We hope you enjoy this release!
