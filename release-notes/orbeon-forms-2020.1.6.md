# Orbeon Forms 2020.1.6 PE

__Friday, December 17, 2021__

Today we released Orbeon Forms 2020.1.6 PE. This maintenance release contains security updates and bug-fixes and is recommended for all users of:

- [Orbeon Forms 2020.1.5 PE](orbeon-forms-2020.1.5.md)
- [Orbeon Forms 2020.1.4 PE](orbeon-forms-2020.1.4.md)
- [Orbeon Forms 2020.1.3 PE](orbeon-forms-2020.1.3.md)
- [Orbeon Forms 2020.1.2 PE](orbeon-forms-2020.1.2.md)
- [Orbeon Forms 2020.1.1 PE](orbeon-forms-2020.1.1.md)
- [Orbeon Forms 2020.1 PE](orbeon-forms-2020.1.md)

This release addresses the following issues since [Orbeon Forms 2020.1.5 PE](orbeon-forms-2020.1.5.md):

- Upgrade to log4j 2.x ([\#4373](https://github.com/orbeon/orbeon-forms/issues/4373))
- "SecurityError: Attempt to use `history.replaceState()` more than 100 times per 30 seconds" ([\#5052](https://github.com/orbeon/orbeon-forms/issues/5052))
- Resolve symbolic links when checking for temporary file ([\#4761](https://github.com/orbeon/orbeon-forms/issues/4761))
- Improve reading of labels for date-time control ([\#4831](https://github.com/orbeon/orbeon-forms/issues/4831))
- Control Settings allow going to section template control via "Previous" ([\#5073](https://github.com/orbeon/orbeon-forms/issues/5073))
- Keyboard shortcuts stop working after dialog Prev/Next ([\#5077](https://github.com/orbeon/orbeon-forms/issues/5077))
- Avoid invalid `role="navigation"` on `<ul>` ([\#5079](https://github.com/orbeon/orbeon-forms/issues/5079))
- Forward all request attributes in internal requests ([\#5081](https://github.com/orbeon/orbeon-forms/issues/5081))
- Process not found when using publish API ([\#5082](https://github.com/orbeon/orbeon-forms/issues/5082))
- In 12-hour, midnight should show as 12:00 am, not 0:00 am ([\#5087](https://github.com/orbeon/orbeon-forms/issues/5087))
- Don't log passwords when debug logging all properties ([\#5090](https://github.com/orbeon/orbeon-forms/issues/5090))
- HTTP Service Editor misses request params column labels ([\#5093](https://github.com/orbeon/orbeon-forms/issues/5093))
- Make `xforms.resources` cache non-disk-persistent ([\#5092](https://github.com/orbeon/orbeon-forms/issues/5092))
- Form Runner function returning whether we are editing a draft ([\#5060](https://github.com/orbeon/orbeon-forms/issues/5060))
- `fr:mode()` to return `edit` when editing autosaved data ([\#5066](https://github.com/orbeon/orbeon-forms/issues/5066))
- New filter to log the body of incoming requests ([\#5098](https://github.com/orbeon/orbeon-forms/issues/5098))
- Third-party library upgrades

For more on the Log4j vulnerability issue, see our [blog post](https://blog.orbeon.com/2021/12/vulnerability-in-log4j-library.html).

UPDATES:

- 2020.1.6.1 addresses a regression introduced with the fix for #5066
- 2020.1.6.2 upgrades Log4j to version 2.17.1 to address the latest vulnerabilities

You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page.  
Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon), [Mastodon](https://mastodon.social/@orbeon), or the [forum](https://www.orbeon.com/community).

We hope you enjoy this release!
