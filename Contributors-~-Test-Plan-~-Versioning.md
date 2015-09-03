> [[Home]] ▸ Contributors ▸ [[Test Plan|Contributors ~ Test Plan]]

## Setup

### Database

Try with db2 at least.

### Properties

```xml
<property
    as="xs:boolean"
    name="oxf.fr.email.attach-pdf.db2.versioning"
    value="true"/>

<property
    as="xs:boolean"
    name="oxf.fr.email.attach-tiff.db2.versioning"
    value="true"/>

<property as="xs:string" name="oxf.fr.detail.buttons.db2.versioning">
    pdf tiff email save send
</property>

<property as="xs:string" name="oxf.fr.detail.process.send.db2.versioning">
    send(
        uri     = '/fr/service/custom/orbeon/echo', 
        replace = 'all', 
        content = 'pdf-url'
    )
</property>

<property
    as="xs:string"
    name="oxf.fr.email.to.db2.versioning"
    value=""/>
```

Also have other email properties setup.

## Steps

- create form db2/versioning
    -fields
        - 1 email field with "Email Recipient", say e.g. `erik at bruchez dot org`
        - 1 email field without "Email Recipient"
        - 1 static Image with image statically attached
        - 1 Image Attachment with image statically attached
        - 1 PDF template with image (e.g. `personal-information$last-name` from DVM-14)
    - publish as version 1
    - go to new page
        - image and image attachment show
        - check PDF template works
        - check TIFF works
        - check save works
        - check email works and sent to correct address
            - has image attachment but not static image
            - has PDF with image attachment
            - has TIFF
            - has XML
        - check send produces PDF path
            - load path in browser shows PDF with image attachment
    - review and back to edit works
    - save
- edit the form definition
    - remove "Email Recipient" from 1st email field and clear it
    - add "Email Recipient" to 2nd email field and add e.g. `ebruchez at orbeon dot com`
    - change static Image
    - change static Image Attachment
    - publish as version 2
    - go to new page
        - check PDF template works
        - check TIFF works
        - check save works
        - check email works and sent to correct address
            - has image attachment but not static image
            - has PDF with image attachment
            - has TIFF
            - has XML
        - check send produces PDF path
            - load path in browser shows PDF with image attachment
    - review and back to edit works
    - save
    - go to new page with `?form-version=1`
        - check all the steps work like before v2 was created
        - relevant issues:
            [#2363](https://github.com/orbeon/orbeon-forms/issues/2363),
            [#1911](https://github.com/orbeon/orbeon-forms/issues/1911),
            [#2371](https://github.com/orbeon/orbeon-forms/issues/2371),
            [#2372](https://github.com/orbeon/orbeon-forms/issues/2372),
            [#2367](https://github.com/orbeon/orbeon-forms/issues/2367),
            [#2330](https://github.com/orbeon/orbeon-forms/issues/2330)
- XML Schema production
    - `/fr/service/(oracle|mysql|sqlserver|db2)/a/schema`
        - schema with B is produced
    - `/fr/service/(oracle|mysql|sqlserver|db2)/a/schema?form-version=1`
        - *NOTE: Adjust version numbers depending on which versions were published.*
        - schema with A is produced
- go to the summary page, click on first row (created last)
    - check field B/value b and attachment show
    - check PDF
- go to the summary page, click on second row (created first)
    - check field A/value a and attachment show
    - check PDF
- Form Builder Publish dialog options (new in 4.6)
    - with persistence layer which supports versioning (mysql)
        - if mysql/a form has never been published
            - no options and no messages are shown
            - latest version shows "-"
            - publish message says version 1 was created
        - if mysql/a form has been published
            - latest version shows correct number
            - option to create new version or overwrite (check version numbers)
            - switch option shows different message
            - publish message says which version was created/updated
    - with persistence layer which doesn't support versioning (exist)
        - latest version line doesn't show
        - if no exist/a form has been published
            - no options and no messages are shown
        - if exist/a form has been published
            - no options are shown
            - message about overwrite
        - publish message says version 1 was updated