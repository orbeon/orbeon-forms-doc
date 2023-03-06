# Orbeon Forms 2018.1.4 PE

__Friday, December 17, 2021__

Today we released Orbeon Forms 2018.1.4 PE. This maintenance release contains security updates and bug-fixes and is recommended for all users of:

- [Orbeon Forms 2018.1.3 PE](https://blog.orbeon.com/2018/11/orbeon-forms-201813-pe.html)
- [Orbeon Forms 2018.1.2 PE](https://blog.orbeon.com/2018/10/orbeon-forms-201812-pe.html)
- [Orbeon Forms 2018.1.1 PE and CE](https://blog.orbeon.com/2018/09/orbeon-forms-201811-pe-and-ce.html)
- [Orbeon Forms 2018.1 PE](https://blog.orbeon.com/2018/09/orbeon-forms-20181.html)

This release addresses the following issues since [Orbeon Forms 2018.2.3 PE](https://blog.orbeon.com/2019/05/orbeon-forms-201823-pe.html):

- Upgrade to log4j 2.x ([\#4373](https://github.com/orbeon/orbeon-forms/issues/4373))
- Inconsistent placehoders in repeated grid ([\#3836](https://github.com/orbeon/orbeon-forms/issues/3836))
- Error when setting `oxf.xforms.sanitize` ([\#3882](https://github.com/orbeon/orbeon-forms/issues/3882))
- Errors don't show on last page of wizard ([\#3889](https://github.com/orbeon/orbeon-forms/issues/3889))
- Make `orbeonLoadedEvent` to fire once only ([\#3891](https://github.com/orbeon/orbeon-forms/issues/3891))
- Form metadata API doesn't return library forms unless user is admin ([\#3919](https://github.com/orbeon/orbeon-forms/issues/3919))
- Incorrect test for form definition versioning ([\#3922](https://github.com/orbeon/orbeon-forms/issues/3922))
- "Initial data from service" attempts to migrate data even if response is empty ([\#3935](https://github.com/orbeon/orbeon-forms/issues/3935))
- Email template fails when Explanation control is present ([\#3941](https://github.com/orbeon/orbeon-forms/issues/3941))
- Import: validation status flag can be out of date ([\#3942](https://github.com/orbeon/orbeon-forms/issues/3942))
- Newly-relevant controls after repeat fail to update ([\#3976](https://github.com/orbeon/orbeon-forms/issues/3976))
- JavaScript error in Form Builder when opening section ([\#4011](https://github.com/orbeon/orbeon-forms/issues/4011))
- Failing to add iteration to top-level repeated section containing a section ([\#4032](https://github.com/orbeon/orbeon-forms/issues/4032))
- Inserting iteration before causes "illegal state when comparing controls" ([\#4035](https://github.com/orbeon/orbeon-forms/issues/4035))
- "New from service" doesn't set data if there is no migration metadata ([\#4048](https://github.com/orbeon/orbeon-forms/issues/4048))
- Help doesn't always show correctly in repeated grid ([\#4062](https://github.com/orbeon/orbeon-forms/issues/4062))
- Upgrade to Xerces 2.12 ([\#4139](https://github.com/orbeon/orbeon-forms/issues/4139))
- Explicit validation is not triggered when navigating to review page ([\#4287](https://github.com/orbeon/orbeon-forms/issues/4287))
- Database reindex not working with v1 of the form ([\#4502](https://github.com/orbeon/orbeon-forms/issues/4502))
- Email processor not to generate multiple `To` headers ([\#4823](https://github.com/orbeon/orbeon-forms/issues/4823))
- Remove YUI menu component for CVE-2010-4710 ([\#3972](https://github.com/orbeon/orbeon-forms/issues/3972))
- Email template with "All controls": attachment should show filename ([\#3947](https://github.com/orbeon/orbeon-forms/issues/3947))
- Occasionally getting empty page ([\#3893](https://github.com/orbeon/orbeon-forms/issues/3893))
- fr:section: setting `readonly="true"` still shows "Insert" action ([\#3846](https://github.com/orbeon/orbeon-forms/issues/3846))
- Third-party library upgrades

For more on the Log4j vulnerability issue, see our [blog post](https://blog.orbeon.com/2021/12/vulnerability-in-log4j-library.html).

UPDATES:

- 2018.1.4.1 upgrades Log4j to version 2.17.1 to address the latest vulnerabilities

You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page.

Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon), [Mastodon](https://mastodon.social/@orbeon), or the [forum](https://www.orbeon.com/community).

We hope you enjoy this release!
