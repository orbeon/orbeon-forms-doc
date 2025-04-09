# Orbeon Forms 2022.1.9

__Wednesday, April 9, 2025__

Today we released Orbeon Forms 2022.1.9 PE! This maintenance release contains bug-fixes and is recommended for all users of:

- [Orbeon Forms 2022.1.8 PE](orbeon-forms-2022.1.8.md)
- [Orbeon Forms 2022.1.7 PE](orbeon-forms-2022.1.7.md)
- [Orbeon Forms 2022.1.6 PE](orbeon-forms-2022.1.6.md)
- [Orbeon Forms 2022.1.5 PE](orbeon-forms-2022.1.5.md)
- [Orbeon Forms 2022.1.4 PE](orbeon-forms-2022.1.4.md)
- [Orbeon Forms 2022.1.3 PE](orbeon-forms-2022.1.3.md)
- [Orbeon Forms 2022.1.2 PE](orbeon-forms-2022.1.2.md)
- [Orbeon Forms 2022.1.1 PE](orbeon-forms-2022.1.1.md)
- [Orbeon Forms 2022.1 PE](orbeon-forms-2022.1.md)

This release addresses the following issues since [Orbeon Forms 2022.1.8 PE](orbeon-forms-2022.1.8.md):

- Can't parse JSON from context with `xxf:json-to-xml()` #6470
- `xxf:json-to-xml()` fails instead of returning an empty sequence when the JSON is invalid #6472
- `xforms-selected` not set for radio buttons and checkboxes when itemset changes #6486
- HTTP Service with Serialization set to HTML Form fails with "Content type may not be null" #6487
- Buttons with `loading-indicator` set to `inline` are slightly taller #6507
- PDF file from Summary page produces error #6515
- Escape value of parameter used in HTML label, hint, or help message #6501
- Hint as tooltip not showing for datetime control #6563
- Form Runner summary page reset the language to English #6582
- Plain textarea can be resized to overlap other controls #6612
- Second phase of `xf:submission replace="all"` unnecessarily send all the field values to the server #6682
- Submission, `send()` causing a new tab being opened #6792
- Add `xf:bind/@name` if missing #6860
- Reindex is triggered for attachments #6913
- Max aggregate file size not enforced when uploading multiple files #6606

You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page.

Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon), [Mastodon](https://mastodon.social/@orbeon), or the [forum](https://www.orbeon.com/community).

We hope you enjoy this release!
