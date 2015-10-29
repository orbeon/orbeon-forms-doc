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
        
### Data Capture Permissions [4.10 DONE]

## Setup

Repeat what follows with eXist, Oracle, MySQL, PostgreSQL, SQL Server, DB2 with the following settings:

```xml
<property
    as="xs:string"
    name="oxf.fr.persistence.provider.exist.*.*"
    value="exist"/>
<property
    as="xs:string"
    name="oxf.fr.persistence.provider.oracle.*.*"
    value="oracle"/>
<property
    as="xs:string"
    name="oxf.fr.persistence.provider.mysql.*.*"
    value="mysql"/>
<property
    as="xs:string"
    name="oxf.fr.persistence.provider.postgresql.*.*"
    value="postgresql"/>
<property
    as="xs:string"
    name="oxf.fr.persistence.provider.sqlserver.*.*"
    value="sqlserver"/>
<property
    as="xs:string"
    name="oxf.fr.persistence.provider.db2.*.*"
    value="db2"/>
<property
    as="xs:string"
    name="oxf.fr.authentication.method"
    value="container"/><!-- change to header for header-based auth -->
<property
    as="xs:string"
    name="oxf.fr.authentication.container.roles"
    value="orbeon-user orbeon-sales orbeon-admin clerk admin"/>
```

- repeat with eXist, Oracle, MySQL, PostgreSQL, SQL Server, DB2
- restore `form-builder-permissions.xml` to default
- for container auth
    - in `web.xml`
        - uncomment security section towards the end
        - change first `<url-pattern>` from `/fr/*` to `/auth` (it doesn't matter that page doesn't exist, it's just a path to force authentication)
    - in `tomcat-users.xml`, setup users:
        - `<user username="clerk" password="clerk" roles="orbeon-user,clerk"/>`
        - `<user username="admin" password="admin" roles="orbeon-user,admin"/>`
- for headers-based  auth
    - `<property as="xs:string"  name="oxf.fr.authentication.method" value="header"/>`
    - set rewriting rules with Charles (⌘⇧W)
        - for user clerk ([gist][16])
        - for user admin ([gist][17])
    - to switch between users in below steps
        - enable rewrite for clerk or admin headers, or disable rewrite
        - remove JSESSIONID when switching users
            
## Tests
            
- in Form Builder
    - create new form `exist/permissions`, `oracle/permissions`, `mysql/permissions`, `postgresql/permissions`, `sqlserver/permissions`, `db2/permissions` (create 1 form then use Duplicate button)
    - enable permissions for form and configure like on [doc page][18]
    - save and publish
- make sure permissions are followed
    - anonymous user
        - home page: link goes to new page (not summary)
        - summary page: unauthorized (fixed regression with [#1201][19])
        - detail page: only `new` accepted, `edit`, `view`, `pdf` are unauthorized
        - enter and save data on `new`
        - check URL doesn't change to `edit`
    - logged in user
        - check permissions as clerk/clerk
            - remove `JSESSIONID` (i.e. with Dev Tools)
            - switch user
            - home page: link goes to the summary page
            - summary page
                - sees data previously entered by anonymous user, cannot delete
                - click on existing data created by anonymous user shows read-only view
                - replace `view` with `edit` in URL shows 404
                - PDF works
                - click on new button opens new page
            - new/edit
                - save data works
                - user is owner so can edit his own data
                - cannot delete from Summary because no `delete` permission
        - check permissions as admin/admin
            - remove `JSESSIONID` (i.e. with Dev Tools)
            - switch user
            - on click goes to summary page
            - on summary page
                - click on new opens new page
                - sees data previously entered by anonymous user and clerk
                - delete button enabled and works
                - on open data, can edit data

[16]: https://gist.github.com/ebruchez/10079296
[17]: https://gist.github.com/ebruchez/10079254
[18]: https://github.com/orbeon/orbeon-forms/wiki/Form-Runner-~-Access-Control-~-Deployed-Forms#example
[19]: https://github.com/orbeon/orbeon-forms/issues/1201

### Autosave and Permissions Test [4.10 DONE]

Repeat what follows with Oracle, MySQL, PostgreSQL, SQL Server, DB2 with the following settings:

*NOTE: As of Orbeon Forms 4.10, autosave is not supported with eXist.*

```xml
<property
    as="xs:string"
    name="oxf.fr.persistence.provider.oracle.*.*"
    value="oracle"/>
<property
    as="xs:string"
    name="oxf.fr.persistence.provider.mysql.*.*"
    value="mysql"/>
<property
    as="xs:string"
    name="oxf.fr.persistence.provider.postgresql.*.*"
    value="postgresql"/>
<property
    as="xs:string"
    name="oxf.fr.persistence.provider.sqlserver.*.*"
    value="sqlserver"/>
<property
    as="xs:string"
    name="oxf.fr.persistence.provider.db2.*.*"
    value="db2"/>
<property 
    as="xs:string"  
    name="oxf.fr.authentication.container.roles" 
    value="a b"/>
<property 
    as="xs:string"  
    name="oxf.http.proxy.host"                   
    value="localhost"/>
<property 
    as="xs:integer" 
    name="oxf.http.proxy.port"                   
    value="8888"/>
```

Setup permissions e.g. in `tomcat-users.xml`:

```xml
<user username="a1" password="a1" roles="a"/>
<user username="a2" password="a2" roles="a"/>
<user username="b1" password="b1" roles="b"/>
```

Authorize on:

    http://localhost:8080/47pe/auth

### Autosave with permissions

1. In FB, create form `$provider/autosave`.
    - Create a field *first name*, marked as shown on summary page.
    - Enable permissions as shown below, save, deploy.  
        ![Permissions dialog](images/test-permissions.png)
    - duplicate for all providers and publish
2. Logged in as user `b1` in group `b`:
    - `$provider/autosave/new`, type *Ned*, save, change to *Ned2*, tab out, after 6s go to the summary page, check it shows *Ned2* as draft
3. Logged in as user `a1` in group `a`:
    - Can see data of other users, but in readonly mode (since everyone can read)
        - Load `$provider/autosave/summary`
        - Check *Ned* shows, but has the readonly "label"
        - Check *Ned2* shows, but has the readonly "label"
        - Check that clicking on *Ned* and *Ned2* brings up the data in readonly mode
        - Edit the URL to have `edit` instead of `view`, check a 403 is returned
    - Drafts for saved
        - Load `$provider/autosave/new`
            - Check we don't get a prompt to edit the draft created by b1 (since we only have read access to it).
            - Type *Homer*, hit save, edit into *Homer2*, after 6s go to summary page, check it shows *Homer* and *Homer2* as draft
        - `$provider/autosave/summary`, click on *Homer2*, check the draft comes up
        - `$provider/autosave/summary`, click on *Homer*, check prompt comes up, try both options and see that *Homer*/*Homer2* comes up
        - editing one of the form data (*Homer* or *Homer2*), hit save, back on the summary check the draft was removed
    - Drafts for new
        - `$provider/autosave/new`, type *Bart*, after 6s go to summary page, check it shows *Bart* as draft
        - `$provider/autosave/new`, check prompt, and try both options
        - `$provider/autosave/new`, on prompt start from scratch, type *Lisa*, after 6s go to summary, check it shows *Bart* and *Lisa* as draft
        - `$provider/autosave/new`, check prompt, try both options, in particular the one showing the drafts for new
    - Summary
        - Edit *Homer*, change to *Homer4*, after 6s go back to summary page.
        - Delete *Homer*, check *Homer4* is deleted as well
        - Check *Lisa*, then view, check in view mode without prompt
        - Delete *Bart*, check *Lisa* not deleted
4. With anonymous user:
    - `$provider/autosave/summary` only shows saved data, not drafts
    - change form definition to remove the read permission form anyone
    - `$provider/autosave/summary` returns 403 (since anonymous users don't have the read permission)
    - `$provider/autosave/new`, type *Homer*, tab out, after 6s check that no autosave was done (e.g. with Charles that no PUT was made to the persistence layer)
5. Permissions of drafts in summary page
    - Log in as user `a1` in group `a`.
    - `$provider/autosave/summary`, delete everything (to clean things up).
    - As user `a1` in group `a`, go to `$provider/autosave/new`, type *Homer*, hit save, edit into *Homer2*, after 6s go to `$provider/autosave/summary`, check it shows *Homer* and *Homer2* as draft.
    - As user `a2` in group `a`, go to `$provider/autosave/summary`, check it shows *Homer* and *Homer2* as draft.
    - As user `b1` in group `b`, go to `$provider/autosave/summary`, check it shows neither *Homer* nor *Homer2*.

### Autosave without permissions

This tests for [#1858](https://github.com/orbeon/orbeon-forms/issues/1858)

1. User is authenticated
1. Create form without permissions
1. Go to /new, enter text in field, tab out, wait for autosave
1. Go to /new again
1. Dialog must propose loading draft
1. Save
1. Make change to text in field, tab out, wait for autosave
1. Go back to /edit
1. Dialog must propose loading draft

### Other Database Tests [4.10 DONE]

## DB2 DDL

Do the following just with DB2; there is no need to test this with Oracle, MySQL, and SQL Server as this is done by the unit tests. Before each test, run the `drop table` statements below.

1. Run the [4.3 DDL] and [4.3 to 4.4 DDL].
2. Run the [4.4 DDL] and [4.4 to 4.6 DDL].
3. Run the [4.6 DDL].

```sql
drop table orbeon_form_definition ;
drop table orbeon_form_definition_attach ;
drop table orbeon_form_data ;
drop table orbeon_form_data_attach ;
```

## Oracle and DB2 Flat View

- Make sure Oracle and DB2 datasources are  setup in `server.xml`.
- Enable the flat view option, adding:

    ```xml
    <property 
        as="xs:boolean"
        name="oxf.fr.persistence.oracle.create-flat-view" 
        value="true"/>

    <property 
        as="xs:boolean"
        name="oxf.fr.persistence.db2.create-flat-view" 
        value="true"/>

    <property 
        as="xs:string"
        name="oxf.fr.persistence.provider.oracle.*.*"
        value="oracle"/>

    <property 
        as="xs:string"
        name="oxf.fr.persistence.provider.db2.*.*"
        value="db2"/>
    ```
- Remove existing view if any: `drop view orbeon_f_db2_a ;`
- Create a new form from [this source](https://gist.github.com/avernet/ff343c6a5e6c3be077d2), which has the sections and controls named as in the table in the [[flat view documentation|Form-Runner-~-Persistence-~-Flat-View]]
  - rename app name to `oracle` or `db2` depending
  - publish, check that a view with the appropriate column names is created.

    ```sql
    SELECT * FROM orbeon_f_db2_a;
    ```

    ```sql
    SELECT * FROM orbeon_f_oracle_a;
    ```

  [4.3 DDL]: https://github.com/orbeon/orbeon-forms/blob/master/src/resources/apps/fr/persistence/relational/ddl/db2-4_3.sql
  [4.3 to 4.4 DDL]: https://github.com/orbeon/orbeon-forms/blob/master/src/resources/apps/fr/persistence/relational/ddl/db2-4_3-to-4_4.sql
  [4.4 DDL]: https://github.com/orbeon/orbeon-forms/blob/master/src/resources/apps/fr/persistence/relational/ddl/db2-4_4.sql
  [4.4 to 4.6 DDL]: https://github.com/orbeon/orbeon-forms/blob/master/src/resources/apps/fr/persistence/relational/ddl/db2-4_4-to-4_6.sql
  [4.6 DDL]: https://github.com/orbeon/orbeon-forms/blob/master/src/resources/apps/fr/persistence/relational/ddl/db2-4_6.sql

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
