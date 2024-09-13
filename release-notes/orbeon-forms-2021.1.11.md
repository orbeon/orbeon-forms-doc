# Orbeon Forms 2021.1.11

__Friday, September 13, 2024__

Today we released Orbeon Forms 2021.1.11 PE! This maintenance release introduces bug-fixes and is recommended for all users of:

- [Orbeon Forms 2021.1.10 PE](orbeon-forms-2021.1.10.md)
- [Orbeon Forms 2021.1.9 PE](orbeon-forms-2021.1.9.md)
- [Orbeon Forms 2021.1.8 PE](orbeon-forms-2021.1.8.md)
- [Orbeon Forms 2021.1.7 PE](orbeon-forms-2021.1.7.md)
- [Orbeon Forms 2021.1.6 PE](orbeon-forms-2021.1.6.md)
- [Orbeon Forms 2021.1.5 PE](orbeon-forms-2021.1.5.md)
- [Orbeon Forms 2021.1.4 PE](orbeon-forms-2021.1.4.md)
- [Orbeon Forms 2021.1.3 PE](orbeon-forms-2021.1.3.md)
- [Orbeon Forms 2021.1.2 PE](orbeon-forms-2021.1.2.md)
- [Orbeon Forms 2021.1.1 PE](orbeon-forms-2021.1.1.md)
- [Orbeon Forms 2021.1 PE](orbeon-forms-2021.1.md)

This release addresses the following issues since [Orbeon Forms 2021.1.10 PE](orbeon-forms-2021.1.10.md):

- Setting `oxf.xforms.xbl.fr.number.grouping-separator` to a space has no effect ([\#6027](https://github.com/orbeon/orbeon-forms/issues/6027))
- JavaScript error "/ by zero" with attachment field ([\#6029](https://github.com/orbeon/orbeon-forms/issues/6029))
- Date picker causes `StringIndexOutOfBoundsException` ([\#5936](https://github.com/orbeon/orbeon-forms/issues/5936))
- Assertion failed in `XFormsControlLifecycleHandler.scala` ([\#6065](https://github.com/orbeon/orbeon-forms/issues/6065))
- XML Schema validation: extra space in `filename` attribute name ([\#6134](https://github.com/orbeon/orbeon-forms/issues/6134))
- Control cleared upon `xforms-disabled` doesn't recalculate ([\#6370](https://github.com/orbeon/orbeon-forms/issues/6370))
- Possible `rebuild` followed by `refresh` but without `recalculate` ([\#6442](https://github.com/orbeon/orbeon-forms/issues/6442))
- Can't parse JSON from context with `xxf:json-to-xml()` ([\#6470](https://github.com/orbeon/orbeon-forms/issues/6470))
- `xxf:json-to-xml()` fails instead of returning an empty sequence when the JSON is invalid ([\#6472](https://github.com/orbeon/orbeon-forms/issues/6472))

Please send feedback via [Twitter](https://twitter.com/orbeon), [Mastodon](https://mastodon.social/@orbeon), or the [forum](https://www.orbeon.com/community).

We hope you enjoy this release!
