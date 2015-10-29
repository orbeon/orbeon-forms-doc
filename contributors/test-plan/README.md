# Test Plan

For each release of Orbeon Forms, we follow this test plan, which tests functionality in addition to the ~800 automatic unit tests which run with every build of Orbeon Forms. In the future, we want to [automate most of this](https://github.com/orbeon/orbeon-forms/issues/227).

## Misc

### Distribution [4.10 DONE]

- README.md is up to date
  - links not broken (use Marked to save HTML, then check w/ Integrity)
  - latest release year
  - version number is correct
  - links to release notes (include link to new version even if blog post not up yet)
- file layout is correct in zip and wars
- check WAR files have reasonable sizes (sizes as of 4.8)
  - orbeon-auth.war (< 10 KB)
  - orbeon-embedding.war (1-2 MB)
  - proxy-portlet.war (1-2 MB)
  - orbeon.war (65 MB)
- check CE zip doesn't have
  - orbeon-embedding.war
  - proxy-portlet.war
- dropping the WAR file (with license included or in ~/.orbeon/license.xml) into Tomcat and Liferay works - out of the box
- make sure the PE license is not included

### Landing Page [4.10 DONE]

- version number is correct in logs when starting
- home page
  - layout of FR examples
  - layout of XForms examples
- XForms examples
  - load, look reasonable, and work

### PE Features Availability [4.10 DONE]

check that all PE features are available in PE, but not in CE:

- features which are checked
    - distribution: `orbeon-embedding.war` and `proxy-portlet.war` are not present
    - FB: no "Add Language" button
    - FB: check with CE that a PE dialog shows for
        - Services
        - Actions
        - Attach PDF
        - Attach Schema
        - Permissions
    - FR: PDF Template button doesn't show for DMV-14 and W-9
    - FR: TIFF button doesn't show even if configured [SINCE 4.11]
    - FR: Import page returns 404
    - FR: No remote server support in Form Runner home page
        - in `form-builder-permissions.xml` add `<role name="orbeon-user" app="*" form="*"/>`
        - in `properties-local.xml`

            ```xml
            <property
                as="xs:string"
                name="oxf.fr.authentication.container.roles"
                value="orbeon-user"/>

            <property as="xs:string"  name="oxf.fr.home.remote-servers">
                [
                    { "label": "Public Demo Server", "url": "http://demo.orbeon.com/orbeon" },
                    { "label": "Local Liferay", "url": "http://localhost:9090/orbeon" }
                ]
            </property>
             ```
        - in `web.xml` uncomment authentication section
        - access [http://localhost:8080/orbeon/fr/](http://localhost:8080/orbeon/fr/)
        - login with user with the `orbeon-user` role
        - check doesn't ask user for remote servers and only loads local form definitions
- features which are not checked yet but should be
    - Proxy portlet
    - Embedding
    - Oracle/DB2/SQL Server
    - Noscript mode
        - [#1043](https://github.com/orbeon/orbeon-forms/issues/1043)
        - [#1407](https://github.com/orbeon/orbeon-forms/issues/1407)
    - XML Schema generation
    - Captcha ([#1927](https://github.com/orbeon/orbeon-forms/issues/1927))
        - in `properties-local.xml` add
            - `<property as="xs:string" name="oxf.fr.detail.captcha.*.*" value="reCAPTCHA"/>`
            - the properties for the private/public key
        - access [http://localhost:8080/orbeon/fr/orbeon/bookshelf/new](http://localhost:8080/orbeon/fr/orbeon/bookshelf/new)
        - check the captcha isn't shown
- Check other features listed on the [web site](http://www.orbeon.com/download)

## Persistence

###  Basic Persistence [4.10 DONE]

Do at least for eXist and DB2, as automated tests already test most of this, and the code running for DB2 is almost identical to the code running for other relational databases. But if possible do for the other relational databases as well.

## Setup

Setup: in `properties-local.xml`, add:

```xml
<property
    as="xs:string"
    name="oxf.fr.persistence.provider.exist.*.*"
    value="exist"/>
<property
    as="xs:string"
    name="oxf.fr.persistence.provider.db2.*.*"
    value="db2"/>
<property
    as="xs:string"
    processor-name="oxf:page-flow"
    name="service-public-methods"
    value="GET HEAD"/>
```

## Create forms

Create same form in all apps: `exist/a`, `db2/a`

- add static image
- use Duplicate button in FB Summary
- then change app name

## Pages

- FB: create form, publish
- FR: check it shows on http://localhost:8080/orbeon/fr/
- FR: create new form, review, back to edit ([#1643](https://github.com/orbeon/orbeon-forms/issues/1643))
- FR: enter data, save
- FR: check it shows in the summary page

## Attachments

- FB: attach static image to form
- FB: add file attachment field
- FB: save and publish
    - DB2: be aware of [#1409](https://github.com/orbeon/orbeon-forms/issues/1409)
- FR: deployed form loads image
- FR: attach file, save, edit

## Search

- FB: check summary/search field, save and deploy
- FR: create new form data, see in summary
- FR: search free-text and structured
- FR: delete data in summary page works

## Duplicate

- FR: Summary: Duplicate button works
    - data for latest form
    - older data

## Home page

With all persistence layers active

- go to /fr/
- check that form definitions from all persistence layers show
- 
### Versioning

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
        
    - [Data Capture Permissions](data-capture-permissions.md) [4.10 DONE]
    - [Autosave and Permissions Test](autosave-and-permissions.md) [4.10 DONE]
    - [Other Database Tests](other-database-tests.md) [4.10 DONE]

## Form Builder

    - [Basic Features](basic-features.md) [4.10 DONE]
    - [Schema Support](schema-support.md) [4.10 DONE]
    - [Services and Actions](services-and-actions.md) [4.10 DONE]

## Form Builder / Form Runner

    - [Section Templates](section-templates.md) [4.10 DONE]
    - [PDF Automatic](pdf-automatic.md) [4.10 DONE]
    - [PDF Template](pdf-template.md) [4.10 DONE]
    - [Form Builder Permissions](form-builder-permissions.md) [4.10 DONE]

## Form Runner

    - [Sample forms](sample-forms.md) [4.10 DONE]
    - [New, Edit, Review Pages](new-edit-review-pages.md) [4.10 DONE]
    - [Responsive](responsive.md) [4.10 DONE]
    - [Home Page](home-page.md) [4.10 DONE]
    - [Summary Page](summary-page.md) [4.10 DONE]
    - [Excel Import](excel-import.md) [4.10 DONE]
    - [Liferay Support](liferay-support.md) [4.10 DONE]
    - [Embedding](embedding.md) [4.10 DONE]
    - [XForms Retry](xforms-retry.md) [4.10 DONE]
    - [Error Dialog](error-dialog.md) [4.10 DONE]
    - [Other Browsers](other-browsers.md) [4.10 DONE]
    - [Other](other.md) [4.10 DONE]
