# Orbeon Forms 2022.1.6

__Tuesday, January 30, 2024__

Today we released Orbeon Forms 2022.1.6 PE! This maintenance release contains bug-fixes and minor new features and is recommended for all users of:

- [Orbeon Forms 2022.1.4 PE](orbeon-forms-2022.1.5.md),
- [Orbeon Forms 2022.1.4 PE](orbeon-forms-2022.1.4.md)
- [Orbeon Forms 2022.1.3 PE](orbeon-forms-2022.1.3.md)
- [Orbeon Forms 2022.1.2 PE](orbeon-forms-2022.1.2.md)
- [Orbeon Forms 2022.1.1 PE](orbeon-forms-2022.1.1.md)
- [Orbeon Forms 2022.1 PE](orbeon-forms-2022.1.md)

This release also addresses the following issues since [Orbeon Forms 2022.1.5 PE](orbeon-forms-2022.1.5.md):

- Security
    - Use 16 byte (128 bits) instead of 8 byte (64 bits) salt for FIPS compliance #6146
- Form Builder
    - Choices Editor: missing space around checkboxes #5985
    - Drag-and-dropping and removing a row in a `fb:dnd-repeat` causes an exception #6000
- Form Runner
    - Email
        - Migration code to fix forms incorrectly migrated `AllControlValuesParam(all)` #6127
        - Email parameters lost in 2022 migration, not read from 2023 form definition #6149
    - PDF
        - "Test PDF" fails for DMV-14 form #6035
        - "Test PDF" fails with error #6034
    - Other
        - Date picker causes `StringIndexOutOfBoundsException` #5936
        - Option for landing, forms, and admin pages to link to summary page without form version #6017
        - Image Attachment settings are no longer persisted in form definition #5811
        - Import: way to validate item values #6008
        - "_.isUndefined is not a function" when testing with Subject7 #6126
        - NPE in `getLast()` #6016
        - When the itemset changes, always send the current value, even if it is unchanged #5986
        - JavaScript error "/ by zero" with attachment field #6029
        - Localize error message on search for summary and landing pages #6030
        - Setting `oxf.xforms.xbl.fr.number.grouping-separator` to a space has no effect #6027
        - Support `xxf:itemset()` on `fr:open-select1` #6047
        - Loading indicator may never hide when `delay-before-display-loading > 0` #6144
        - XML Schema validation: extra space in filename attribute name #6134
        - Assertion failed in `XFormsControlLifecycleHandler.scala` #6065
    - Embedding
        - Extraneous `/` in requests to XForms Server with embedding #6066
        - Second embedding not having any effect when using JavaScript embedding API #6079
        - Portlet: error when attempting to read autosaved draft #6139
        - Offline: `format-dateTime()` doesn't work #5976
        - Offline: "Upload complete" message is not clear #6171
        - Clarify Liferay handling of history API #4127

You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page.

Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon), [Mastodon](https://mastodon.social/@orbeon), or the [forum](https://www.orbeon.com/community).

We hope you enjoy this release!
