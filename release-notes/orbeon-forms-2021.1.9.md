# Orbeon Forms 2021.1.9

__Tue, June 6, 2023__

Today we released Orbeon Forms 2021.1.9 PE! This maintenance release introduces bug-fixes and is recommended for all users of:

- [Orbeon Forms 2021.1.8 PE](orbeon-forms-2021.1.8.md)
- [Orbeon Forms 2021.1.7 PE](orbeon-forms-2021.1.7.md)
- [Orbeon Forms 2021.1.6 PE](orbeon-forms-2021.1.6.md)
- [Orbeon Forms 2021.1.5 PE](orbeon-forms-2021.1.5.md)
- [Orbeon Forms 2021.1.4 PE](orbeon-forms-2021.1.4.md)
- [Orbeon Forms 2021.1.3 PE](orbeon-forms-2021.1.3.md)
- [Orbeon Forms 2021.1.2 PE](orbeon-forms-2021.1.2.md)
- [Orbeon Forms 2021.1.1 PE](orbeon-forms-2021.1.1.md)
- [Orbeon Forms 2021.1 PE](orbeon-forms-2021.1.md)

This release addresses the following issues since [Orbeon Forms 2021.1.8 PE](orbeon-forms-2021.1.8.md):

- Support for the JCache API (JSR-107) ([\#5399](https://github.com/orbeon/orbeon-forms/issues/5399))
- Assertion failed in `setInlineInstance`, `XFormsModel.scala` ([\#5772](https://github.com/orbeon/orbeon-forms/issues/5772))
- SQL Processor storing `DATE` in RFC 1123 instead of ISO format ([\#5775](https://github.com/orbeon/orbeon-forms/issues/5775))
- Click on the help icon has no effect if field is readonly ([\#5788](https://github.com/orbeon/orbeon-forms/issues/5788))
- Prevent caching of response to `/xforms-server/baseline.js` ([\#5786](https://github.com/orbeon/orbeon-forms/issues/5786))
- `xxf:sort()` to support `lang`, `collation`, and `stable` parameters ([\#5794](https://github.com/orbeon/orbeon-forms/issues/5794))
- When data is passed with the `multipart/form-data` encoding, `data-migration-behavior` on the URL is ignored ([\#5802](https://github.com/orbeon/orbeon-forms/issues/5802))
- Element bound to a dynamic dropdown with search to NOT always have a `@label` ([\#5816](https://github.com/orbeon/orbeon-forms/issues/5816))
- Server sends date with timezone to client for date field ([\#5822](https://github.com/orbeon/orbeon-forms/issues/5822))
- `fr|databound-select1-search` to show missing label in view modes when possible ([\#5813](https://github.com/orbeon/orbeon-forms/issues/5813))
- Occasional blank page after click on "Review" ([\#5719](https://github.com/orbeon/orbeon-forms/issues/5719))
- Section label editor closes on heartbeat ([\#5841](https://github.com/orbeon/orbeon-forms/issues/5841))
- Form Builder should not add `bind` to custom dialog in form definition ([\#5733](https://github.com/orbeon/orbeon-forms/issues/5733))
- Limit app/form name to 255 characters ([\#5848](https://github.com/orbeon/orbeon-forms/issues/5848))
- Don't let buttons overflow grids ([\#5851](https://github.com/orbeon/orbeon-forms/issues/5851))

You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page.

Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon), [Mastodon](https://mastodon.social/@orbeon), or the [forum](https://www.orbeon.com/community).

We hope you enjoy this release!
