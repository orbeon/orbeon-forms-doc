# Orbeon Forms 2023.1.7

__Thursday, February 13, 2025__

Today we released Orbeon Forms 2023.1.7 PE! This maintenance release contains bug-fixes and new features and is recommended for all users of:

- [Orbeon Forms 2023.1.6 PE](orbeon-forms-2023.1.6.md)
- [Orbeon Forms 2023.1.5 PE](orbeon-forms-2023.1.5.md)
- [Orbeon Forms 2023.1.4 PE](orbeon-forms-2023.1.4.md)
- [Orbeon Forms 2023.1.3 PE](orbeon-forms-2023.1.3.md)
- [Orbeon Forms 2023.1.2 PE](orbeon-forms-2023.1.2.md)
- [Orbeon Forms 2023.1.2 PE](orbeon-forms-2023.1.2.md)
- [Orbeon Forms 2023.1.1 PE](orbeon-forms-2023.1.1.md)
- [Orbeon Forms 2023.1 PE](orbeon-forms-2023.1.md)

## Issues addressed

This release addresses the following issues since [Orbeon Forms 2023.1.6 PE](orbeon-forms-2023.1.6.md):

- Form Builder
    - Renamed control is not updated in Email Settings #6741
    - "Undeclared variable in XPath expression: $form-resources" reported by Test console #6400
    - Avoid warning about target `fb-custom-form-settings` that can't be found when load Form Builder #6753
    - Support variable references in Actions Editor / Service Request Actions / Formula #6766
- Form Runner
    - Second phase of `xf:submission replace="all"` unnecessarily send all the field values to the server #6682
    - Dropup menu shows empty keyboard shortcut box #6693
    - Empty Formatted Text Area in view mode shows lighter gray line #6692
    - Friendly Captcha: add configurable endpoints #6697
    - Error sending email #6698
    - Offline: `fr:tinymce` uses `xxf:call-xpl()` #6112
    - Incorrect automatic hint when multiple file format are accepted #6724
    - `save` then `email` fails for logged in users #6739
    - Avoid Ehcache warning "Statistics can no longer be enabled via configuration" #6754
    - PDF shows initial blank page in some cases #6771
    - Dynamic dropdown with search get `xforms-value-changed` when opened #6787

You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page.

Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon), [Mastodon](https://mastodon.social/@orbeon), or the [forum](https://www.orbeon.com/community).

We hope you enjoy this release!
 