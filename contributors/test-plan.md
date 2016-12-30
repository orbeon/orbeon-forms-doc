# Test Plan

For each release of Orbeon Forms, we follow this test plan, which tests functionality in addition to the ~800 automatic unit tests which run with every build of Orbeon Forms. In the future, we want to [automate most of this](https://github.com/orbeon/orbeon-forms/issues/227).

<!-- toc -->

## Misc

### Distribution \[2016.3 DONE\]

- [x] `README.md` is up to date
  - [x] links not broken (use Marked to save HTML, then check w/ Integrity)
  - [x] latest release year
  - [x] version number is correct
  - [x] links to release notes (include link to new version even if blog post not up yet)
- [x] file layout is correct in zip and wars
- [x] check WAR files have reasonable sizes (sizes as of 2016.3)
  - `orbeon-auth.war` (3 KB)
  - `orbeon-embedding.war` (1.3 MB)
  - `proxy-portlet.war` (1.7 MB)
  - `orbeon.war` (85 MB as of 2016.3)
  - `orbeon-xforms-filter.jar` (474 KB)
- [x] check CE zip doesn't have
  - `orbeon-embedding.war`
  - `proxy-portlet.war`
- [x] dropping the WAR file (with license included or in `~/.orbeon/license.xml`) works out of the box
    - [x] Tomcat
- [x] make sure the PE license is not included

### Landing Page \[2016.3 DONE\]

- version number is correct in logs when starting
- landing page
  - layout of FR examples
  - layout of XForms examples
- XForms examples
  - load, look reasonable, and work

### PE Features Availability \[2016.3 TODO\]

check that all PE features are available in PE, but not in CE:

- [ ] features which are checked
    - [ ] distribution: `orbeon-embedding.war` and `proxy-portlet.war` are not present
    - [ ] FB: no "Add Language" button
    - [ ] FB: check with CE that a PE dialog shows for
        - Services
        - Actions
        - Attach PDF
        - Attach Schema
        - Permissions
    - [ ] FB: no Signature control in toolbox
    - [ ] FR: PDF Template button doesn't show for DMV-14 and W-9
    - [ ] FR: TIFF button doesn't show even if configured \[SINCE 2016.1\]
    - [ ] FR: Import page returns 404
    - [ ] FR: No remote server support in Form Runner home page
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
        - access `http://localhost:8080/2016.2-ce/fr/`
        - login with `orbeon-user` (or any user with the `orbeon-user` role)
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
        - access http://localhost:8080/2016.3-pe/fr/orbeon/bookshelf/new
        - check the captcha isn't shown
- Check other features listed on the [web site](http://www.orbeon.com/download)

## Persistence

###  Basic Persistence \[2016.3 DONE\]

Do the following for eXist and DB2. We do not test other relational databases here, as automated tests already test most of this, and the code running for DB2 is almost identical to the code running for other relational databases.

- Setup: in `properties-local.xml`, add:
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
- Create forms: same form in all apps: `exist/a`, `db2/a`
    - change the input field to be shown in summary and search
    - add a static image, attach and image
    - use Duplicate button in FB Summary
    - then change app name
- Pages
    - FB: create form, publish
    - FR: check it shows on http://localhost:8080/2016.3-pe/fr/
    - FR: create new form, review, back to edit ([#1643](https://github.com/orbeon/orbeon-forms/issues/1643))
    - FR: enter data, save
    - FR: check it shows in the summary page
- Attachments
    - FB: add file attachment field
    - FB: save and publish
        - DB2: be aware of [#1409](https://github.com/orbeon/orbeon-forms/issues/1409)
    - FR: deployed form loads image
    - FR: attach file, save, edit
- Search
    - FR: create new form data, see in summary
    - FR: search free-text and structured
    - FR: delete data in summary page works
- Duplicate
    - FR: Summary: Duplicate button works

### Versioning \[2016.3 DONE\]

Do the following on DB2.

- Setup
    - Properties
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
            as="xs:boolean"
            name="oxf.fr.email.attach-pdf.db2.versioning"
            value="true"/>
        <property
            as="xs:boolean"
            name="oxf.fr.email.attach-tiff.db2.versioning"
            value="true"/>
        <property as="xs:string" name="oxf.fr.detail.buttons.db2.versioning">
            pdf tiff email save review send
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
        <property as="xs:string"  processor-name="oxf:page-flow" name="service-public-methods"  value="GET HEAD POST PUT DELETE"/>
        <property as="xs:string"  processor-name="oxf:page-flow" name="page-public-methods"     value="GET HEAD POST PUT DELETE"/>
        ```

        Also add the email properties (starting with `oxf.fr.email`) from your own `properties-local.xml`.

- Steps
    - create form `db2/versioning`
        - fields
            - Name section `personal-information`
            - 1 email field with "Email Recipient", say `gaga@orbeon.com` (use the proper ID)
            - 1 input field "First name"
                - Name it `first-name`
            - 1 static Image with image statically attached
            - 1 Image Attachment with image statically attached
            - Attach PDF template
                - Use `src/resources/forms/orbeon/dmv-14/form/dmv14.pdf`
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
        - add "Email Recipient" to 2nd email field and add say `gaga@gmail.com` (use the proper ID)
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
        - `/fr/service/db2/versioning/schema`
            - Check this is the schema for first form published earlier
        - `/fr/service/db2/versioning/schema?form-version=1`
            - *NOTE: Adjust version numbers depending on which versions were published.*
            - Check this is the schema for the second form published earlier
    - go to the summary page, click on first row (created last)
        - check the data shows with the correct version of the form
        - check PDF
    - go to the summary page, click on second row (created first)
        - check field A/value a and attachment show
        - check PDF
    - Form Builder Publish dialog options (new in 4.6)
        - with persistence layer which supports versioning (db2)
            - if `db2/a` has never been published
                - no options and no messages are shown
                - latest version shows "-"
                - publish message says version 1 was created
            - if `db2/a` form has been published
                - latest version shows correct number
                - option to create new version or overwrite (check version numbers)
                - switch option shows different message
                - publish message says which version was created/updated
        - with persistence layer which doesn't support versioning (exist)
            - latest version line doesn't show
            - if no `exist/a` form has been published
                - no options and no messages are shown
            - if exist/a form has been published
                - no options are shown
                - message about overwrite

### Data Capture Permissions \[2016.3 DONE\]

####  Setup

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

- restore `form-builder-permissions.xml` to default
- for container auth:
    - in `web.xml`
        - uncomment security section towards the end
        - change first `<url-pattern>` from `/fr/*` to `/auth` (it doesn't matter that page doesn't exist, it's just a path to force authentication)
    - in `tomcat-users.xml`, setup users:
        ```xml
        <user username="clerk" password="clerk" roles="clerk,orbeon-user"/>
        <user username="admin" password="admin" roles="admin,orbeon-user"/>
        ```
- for headers-based auth:
    - `<property as="xs:string"  name="oxf.fr.authentication.method" value="header"/>`
    - set rewriting rules with Charles (⌘⇧E)
        - for user clerk ([gist](https://gist.github.com/ebruchez/10079296))
        - for user admin ([gist](https://gist.github.com/ebruchez/10079254))
    - to switch between users in below steps
        - enable rewrite for clerk or admin headers, or disable rewrite
        - remove JSESSIONID when switching users

#### Tests

- in Form Builder
    - create new form `exist/permissions`
        - save and publish
        - enable permissions for form and configure like on [doc page](../form-runner/access-control/deployed-forms.md#example)
    - use Duplicate button to create
        - `oracle/permissions`
        - `mysql/permissions`
        - `postgresql/permissions`
        - `sqlserver/permissions`
        - `db2/permissions`
- make sure permissions are followed
    - anonymous user
        - home page: link goes to new page (not summary)
        - summary page: unauthorized (fixed regression with [#1201](https://github.com/orbeon/orbeon-forms/issues/1201))
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
                - replace `view` with `edit`, getting an "Unauthorized" page
                - PDF works
                - click on new button opens new page
            - new/edit
                - save data works
                - user is owner so can edit his own data
                - cannot delete from Summary because no `delete` permission
        - check permissions as admin/admin
            - remove `JSESSIONID` (i.e. with Dev Tools)
            - switch to `admin`/`admin` user
            - on click goes to summary page
            - on summary page
                - click on new opens new page
                - sees data previously entered by anonymous user and clerk
                - delete button enabled and works
                - on open data, can edit data

### Autosave and Permissions Test \[2016.3 ERIK TODO\]

Repeat what follows with:

- \[2016.3 TODO\] Oracle
- \[2016.3 TODO\] MySQL
- \[2016.3 TODO\] PostgreSQL
- \[2016.3 TODO\] SQL Server
- \[2016.3 TODO\] DB2

Use the following in your `properties-local.xml`:

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

In `web.xml`:

- uncomment security section towards the end
- change first `<url-pattern>` from `/fr/*` to `/auth` (it doesn't matter that page doesn't exist, it's just a path to force authentication)

Setup permissions e.g. in `tomcat-users.xml`:

```xml
<user username="a1" password="a1" roles="a,orbeon-user"/>
<user username="a2" password="a2" roles="a,orbeon-user"/>
<user username="b1" password="b1" roles="b,orbeon-user"/>
```

Authorize on:

```
http://localhost:8080/2016.3-pe/fr/auth
```

- Autosave with permissions
    1. In FB, create form `mysql/autosave`.
        - Create a field *first name*, marked as shown on summary page.
        - Enable permissions as shown below and publish.
            ![Permissions dialog](images/test-permissions.png)
        - duplicate for all providers and publish
    2. Logged in as user `b1` in group `b`:
        - With header-based authentication, make sure to clear the `JSESSIONID` after switching user, as Orbeon Forms stores the user credentials in the sessions and doesn't recompute them if they change.
        - `$PROVIDER/autosave/new`, type *b1 Ned*, save, change to *b1 Ned draft*, tab out, after 6s go to the summary page, check it shows *b1 Ned draft* as draft
    3. Logged in as user `a1` in group `a`:
        - Can see data of other users, but in readonly mode (since everyone can read)
            - Load `$PROVIDER/autosave/summary`
            - Check *b1 Ned* and *b1 Ned draft* show, but has the readonly "label"
            - Check that clicking on *b1 Ned* and *b1 Ned draft* brings up the data in readonly mode
            - Edit the URL to have `edit` instead of `view`, check a 403 is returned
        - Drafts for saved
            - Load `$PROVIDER/autosave/new`
                - Check we don't get a prompt to edit the draft created by `b1` (since we only have read access to it).
                - Type *a1 Homer*, hit save, edit into *a1 Homer draft*, after 6s go to summary page, check it shows *a1 Homer* and *a1 Homer draft* as draft
            - `$PROVIDER/autosave/summary`, click on *a1 Homer draft*, check the draft comes up
            - `$PROVIDER/autosave/summary`, click on *a1 Homer*, check prompt comes up, try both options and see that *a1 Homer*/*a1 Homer draft* comes up
            - editing one of the form data, hit save, back on the summary check the draft was removed
        - Drafts for new
            - `$PROVIDER/autosave/new`, type *a1 Bart draft*, after 6s go to summary page, check it shows *a1 Bart draft* as draft
            - `$PROVIDER/autosave/new`, check prompt, and try both options
            - `$PROVIDER/autosave/new`, on prompt start from scratch, type *a1 Lisa draft*, after 6s go to summary, check it shows *a1 Bart draft* and *a1 Lisa draft* as draft
            - `$PROVIDER/autosave/new`, check prompt, try both options, in particular the one showing the drafts for new
        - Summary
            - Edit *a1 Homer*, change to *a1 Homer draft*, after 6s go back to summary page.
            - Delete *a1 Homer*, check *a1 Homer draft* is deleted as well
            - Check *a1 Lisa draft*, then review, check in view mode without prompt
            - Delete *a1 Bart draft*, check *a1 Lisa draft* not deleted
    4. With anonymous user:
        - `$PROVIDER/autosave/summary` only shows saved data, not drafts
        - change form definition to remove the read permission form anyone
        - `$PROVIDER/autosave/summary` returns 403 (since anonymous users don't have the read permission)
        - `$PROVIDER/autosave/new`, type *guest Maggie*, tab out, after 6s check that no autosave was done (e.g. with Charles that no PUT was made to the persistence layer)
    5. Permissions of drafts in summary page
        - Log in as user `a1` in group `a`.
        - `$PROVIDER/autosave/summary`, delete everything (to clean things up).
        - As user `a1` in group `a`, go to `$PROVIDER/autosave/new`, type *a1 Homer*, hit save, edit into *a1 Homer draft*, after 6s go to `$PROVIDER/autosave/summary`, check it shows *a1 Homer* and *a1 Homer draft* as draft.
        - As user `a2` in group `a`, go to `$PROVIDER/autosave/summary`, check it shows *a1 Homer* and *a1 Homer draft* as draft.
        - As user `b1` in group `b`, go to `$PROVIDER/autosave/summary`, check it shows neither *a1 Homer* nor *a1 Homer2 draft*.
    6. Autosave without permissions (tests for [#1858](https://github.com/orbeon/orbeon-forms/issues/1858))
        - Edit the form definition, uncheck *Enable permissions for this form*, publish
        - Log in as user `a1` of group `a`
        - Go to /new, enter `a1 Marge draft`, tab out, wait for autosave
        - Go to /new again, dialog must propose loading draft
        - Choose to open the draft, edit it into `a1 Marge`, save, change to `a1 Marge draft`, wait for autosave
        - Go to the summary page, click on `a1 Marge`, dialog must propose loading draft

### DB2 DDL \[2016.3 DONE\]

Do the following just with DB2; there is no need to test this with Oracle, MySQL, and SQL Server as this is done by the unit tests. Before each test, run the `drop table` statements below.

- In a tab, open the doc with the [table linking to the DB2 DDL](../form-runner/persistence/relational-db.md#db2-database-setup)
- Initial setup
    - Start DB2 instance on EC2
    - Connect to DB2, e.g. with SQLWorkbenchJ, using the new IP of the instance from the EC2 Console
- Test initial creations; for each version:
    - Run the `DROP TABLE` DDL below
    - Run the "from scratch DDL" for that version
    - Check there are errors in the console
- Test upgrade DDL
    - Run the `DROP TABLE` DDL below
    - Run the 4.3 "from scratch" DDL
    - In sequence run all the "upgrade" DDL, checking there are no errors

```sql
drop table orbeon_form_definition ;
drop table orbeon_form_definition_attach ;
drop table orbeon_form_data ;
drop table orbeon_form_data_attach ;
drop table orbeon_i_current ;
drop table orbeon_i_control_text ;
```

### Flat View \[2016.3 ALEX TODO\]

- Make sure PostgreSQL, Oracle, SQL Server, and DB2 datasources are  setup in `server.xml`.
- Enable the flat view option, adding:

    ```xml
    <property
        as="xs:boolean"
        name="oxf.fr.persistence.postgresql.create-flat-view"
        value="true"/>
    <property
        as="xs:boolean"
        name="oxf.fr.persistence.oracle.create-flat-view"
        value="true"/>
    <property
        as="xs:boolean"
        name="oxf.fr.persistence.sqlserver.create-flat-view"
        value="true"/>
    <property
        as="xs:boolean"
        name="oxf.fr.persistence.db2.create-flat-view"
        value="true"/>
    <property
        as="xs:string"
        name="oxf.fr.persistence.provider.postgresql.*.*"
        value="postgresql"/>
    <property
        as="xs:string"
        name="oxf.fr.persistence.provider.oracle.*.*"
        value="oracle"/>
    <property
        as="xs:string"
        name="oxf.fr.persistence.provider.sqlserver.*.*"
        value="sqlserver"/>
    <property
        as="xs:string"
        name="oxf.fr.persistence.provider.db2.*.*"
        value="db2"/>
    ```
- Create a new form from [this source](https://gist.github.com/avernet/ff343c6a5e6c3be077d2), which has the sections and controls named as in the table in the [flat view documentation](../form-runner/persistence/flat-view.md)
  - Rename app name to `oracle`, `sqlserver`, `postgresql`, or `db2` depending
  - Publish, check that a view with the appropriate column names is created with the `SELECT` statement below.
  - Go to `/new` of the form, enter values, save, run the SQL again, and check that the value entered show in the view.

    ```sql
    SELECT * FROM orbeon_f_oracle_a;
    SELECT * FROM orbeon_f_sqlserver_a;
    SELECT * FROM orbeon_f_postgresql_a;
    SELECT * FROM orbeon_f_db2_a;
    ```

## Form Builder

### Basic Features \[2016.3 ERIK DONE\]

- [x] create new form
- [x] insert sections, grids, repeated grids
- [x] rename sections and controls
    - check renamed in source
- [x] move sections
    - up/down
    - right/left (subsections) (be aware of [#2031](https://github.com/orbeon/orbeon-forms/issues/2031))
- [x] repeated grid
    - [x] set min/max as ints
    - [x] set min/max as XPath expressions, e.g. `1 + 2`
- [x] make section repeated
    - [x] insert/move/remove iterations
    - [x] set min/max as ints
    - [x] set min/max as XPath expressions, e.g. `1 + 2`
- [x] set control label, hint, items
    - [x] plain
    - [x] HTML
    - [x] check HTML label appears correct in summary page / search
    - [x] placeholder labels
    - [x] placeholder hints
- [x] set control help ([Lorum Ipsum](http://www.lipsum.com/feed/html))
    - [x] plain
    - [x] HTML
    - [x] check help icon appears when help is set, and disappears when help is blanked
- [x] set section help
    - [x] check help icon appears when help is set, and disappears when help is blanked
- [x] set control validation
    - set custom error constraint and alert
    - set custom warning constraint and alert
    - set required
    - check that if control is required but empty, generic message shows, not constraint message ([#1829](https://github.com/orbeon/orbeon-forms/issues/1829))
    - check that if control is required but empty and there is an unmet constraint, generic message shows ([#1830](https://github.com/orbeon/orbeon-forms/issues/1830))
- [x] cut/copy/paste
    - copy control with help, required, constraint, and warning
    - paste control
    - check in source that all elements have been renamed
      - including `$form-resources` references (see [#1820](https://github.com/orbeon/orbeon-forms/issues/1820))
      - including `@validation` and `xf:constraint/@id` (see [#1785](https://github.com/orbeon/orbeon-forms/issues/1785))
    - check that form runs and new control validates constraints properly
- [x] set control MIPs and properties
    - check required star appears with required set to `true()`
    - check Show in Summary/Search work when form deployed
- [x] set section MIPs
    - check show/hide based on control value e.g. `$fortytwo = '42'`
- [x] edit/modify source
    - change e.g. control label
- [x] image annotation control
  - create simple form and test works, saves, loads
- [x] i18n (PE)
    - [x] check en/fr/es/it/de (languages with full support)
    - [x] switch FB language and check language changes
    - [x] add language
    - [x] edit label and items and switch languages
    - [x] edit source and change top-level language, make sure language selector switches
    - [x] remove language
    - [x] [#1223](https://github.com/orbeon/orbeon-forms/issues/1223)
        - add lang not fully supported (e.g. Afrikaans) , remove all other languages, enter some labels
        - Test and Publish/new -> must show Afrikaans labels, not blank
- [x] Form Builder Summary page
    - check that search in Summary page updates title/description when FR language is changed
- [x] set form title/description
- [x] test form
- [x] save
- [x] publish form
    - check that attachments are published too (e.g. attach static img, dynamic img, and PDF file attachment)
- [x] warning dialog if attempt to close page when unsaved
- [x] serialization/deserialization [#1894](https://github.com/orbeon/orbeon-forms/issues/1894)
    - set properties
    ```xml
    <property
        as="xs:integer"
        name="oxf.xforms.cache.documents.size"
        value="1"/>
    <property
        as="xs:integer"
        name="oxf.xforms.cache.static-state.size"
        value="1"/>
    ```
    - restart Tomcat
    - in 1st tab, visit http://localhost:8080/2016.3-pe/fr/orbeon/builder/new
    - enter a/a to go to editor
    - in 2nd tab, visit http://localhost:8080/2016.3-pe/fr/orbeon/contact/new
    - back to 1st tab
    - insert control
    - check there is no JS error

### Singleton forms \[2016.3 TODO\]

- test that the features works as [documented](../form-runner/advanced/singleton-form.md)
    - no form data
    - one instance of form data
    - more than one instance of form data

### Schema Support \[2016.3 TODO\]

- attach Schema
    - attach the [Bookcast schema](https://github.com/orbeon/orbeon-forms/blob/master/src/resources/apps/xforms-bookcast/schema.xsd)
        - available types contains `rating`, `language`, `link`
    - change to the [XForms types schema](https://github.com/orbeon/orbeon-forms/blob/master/src/main/resources/org/orbeon/oxf/xforms/xforms-types.xsd)
        - available types contains `card-number`, `dayTimeDuration`, `yearMonthDuration`
    - delete schema, check removed from model, be aware of
        - Delete attached XML Schema causes error if type in use [#694](https://github.com/orbeon/orbeon-forms/issues/694))
        - When removing schema, type from previously selected schema are showing [#2733](https://github.com/orbeon/orbeon-forms/issues/2733)
    - re-add schema
    - assign types to controls
    - check that validation is working as per the types
    - check schema types are reloaded in Control Settings dialog
        - *NOTE: This is note the case with `xforms-types.xsd`, probably because the types are in the `xf:` namespace. Use the Bookcast `schema.xsd` instead.*

### Database service \[2016.3 TODO\]

- setup db
    - use MySQL on RDS (`jdbc:mysql://mysql.c4pgtxbv1cuq.us-east-1.rds.amazonaws.com:3306/orbeon?useUnicode=true&amp;characterEncoding=UTF8`)
    - set datasource in `server.xml`
    - create test table + data row if doesn't exist (can use IntelliJ Database tools)

    ```sql
    create table orbeon_address_book (
      id      integer not null primary key,
      first   varchar(255) not null,
      last    varchar(255) not null,
      phone   varchar(255) not null
    );
    insert into orbeon_address_book values(1, "John", "Smith", "5551231234");
    insert into orbeon_address_book values(2, "Mary", "Smith", "5551111111");
    ```
- setup form
  - 1 input field
  - 1 output field
  - 1 selection control
- create db service

    ```sql
    SELECT * FROM orbeon_address_book
    WHERE id = <sql:param type="xs:string" select=""/>
    ```
- create action
    - sets service value on request for param `1`
    - sets control values on response, e.g. `concat(/*/*/first, ' ', /*/*/last)`
    - set itemset values on response
        - `/*/*`
        - `concat(first, ' ', last)`
        - `id`

### HTTP service \[2016.3 ERIK DONE\]

- using echo service is ok
    - POST to `/fr/service/custom/orbeon/echo`
    - body:
        ```xml
        <items>
            <item label="Foo" value="foo"/>
            <item label="Bar" value="bar"/>
        </items>
        ```
- test
    - call service upon form load and set control value upon response
    - same with button activation
    - same but set service values on request from control
    - set itemset values on response, e.g. use

## Form Builder / Form Runner

### Section Templates \[2016.3 ERIK DONE\]

- examples here but create new to make sure builder works!
    - https://gist.github.com/ebruchez/6187690
    - https://gist.github.com/ebruchez/6187704
- create acme/library
    - 3 sections
    - S1
        - 2 fields, readonly or visibility dependency from one field on the other
    - S2
        - repeated grid
    - S3
        - nest repeated section with repeated grid inside
    - 2 languages
    - 1 HTTP service/action
        - load-languages/set-languages
        - load oxf:/apps/fr/i18n/languages.xml
        - upon form load
        - set itemset
            - @english-name
            - @code
    - test/save/publish
- insert components from library into acme/test-library
    - insert S1 and S2 twice, S3
    - add French language
    - check language changes in builder (be aware of [#690](https://github.com/orbeon/orbeon-forms/issues/690))
    - publish
        - check control visibility change
        - check language changes
        - check services load in both languages (same labels)
        - enter data, save, check that data loads back in all fields
        - test that repeated grid in section template shows ([#1370](https://github.com/orbeon/orbeon-forms/issues/1370)) in the builder and nicely
        - check review, PDF
- make sure Clear works
    - pass [#807](https://github.com/orbeon/orbeon-forms/issues/807)
    - fail https://github.com/orbeon/orbeon-forms/issues/3052
- makes invalid controls in section template prevent saving

### PDF Automatic \[2016.3 ERIK DONE\]

- [x] Controls and Bookshelf
    - input field and text areas have highlighted and clickable links
    - try TIFF output as well
- [x] Controls
    - [x] image annotation shows in PDF
    - *NOTE: Disabled in 2016.1 and 2016.2, re-enabled in 2016.3 but not re-added to Controls form.*
- [x] form title in header/footer
- [x] logo in title
- [x] page numbering/total at bottom center
- [x] PDF looks good overall
- [x] send PDF binary works
    ```xml
    <property
        as="xs:string"
        name="oxf.fr.detail.buttons.orbeon.bookshelf"
        value="pdf email send"/>

    <property as="xs:string" name="oxf.fr.detail.process.send.orbeon.bookshelf">
        send(uri = 'http://posttestserver.com/post.php?a=b', method = 'post', replace = 'all', content = 'pdf')
    </property>
    ```

### PDF Template \[2016.3 ERIK DONE\]

- [x] attach e.g. [831113e3ef799f2c9f57ee0b10f789a8951360ba.bin](https://github.com/orbeon/orbeon-forms/blob/master/data/orbeon/fr/orbeon/w9/form/831113e3ef799f2c9f57ee0b10f789a8951360ba.bin?raw=true) (W9 example)
- [x] add field "name" in section "applicant"
- [x] publish and test that name appears in PDF and TIFF
- [x] change
- [x] remove PDF
  - publish and test, must see notemplate PDF/TIFF
- [x] check that DMV-14 PDF works and is filled out
  - check Vote and Leased checkboxes
  - check that state appears ([#3053](https://github.com/orbeon/orbeon-forms/issues/3053))
- [x] W9 form
  - check that signature appears in the PDF and doesn't go over background PDF lines

### Form Builder Permissions \[2016.3 ERIK TODO\]

- *NOTES 2014-03-20*
    - *Would be really nice to have automated for this!*
- Do eXist AND relational as code is a bit different.
- setup
    - "Uncomment this for the Form Runner authentication" in `web.xml`
    - `tomcat-users.xml`

    ```xml
    <tomcat-users>
        <user
            username="orbeon-user"
            password="xforms"
            roles="orbeon-user"/>
        <user
            username="orbeon-sales"
            password="xforms"
            roles="orbeon-user,orbeon-sales"/>
        <user
            username="orbeon-admin"
            password="xforms"
            roles="orbeon-user,orbeon-admin"/>
        <user
            username="orbeon-service"
            password="xforms"
            roles="orbeon-user,orbeon-service"/>
    </tomcat-users>
    ```
    - `properties-local.xml`

    ```xml
    <property
        as="xs:string"
        name="oxf.fr.authentication.method"
        value="container"/><!-- change to header for header-based auth -->
    <property
        as="xs:string"
        name="oxf.fr.authentication.container.roles"
        value="orbeon-user orbeon-sales orbeon-admin"/>
    ```
    - `form-builder-permissions.xml`

    ```xml
    <roles>
        <role name="*"            app="guest" form="*"/>
        <role name="orbeon-sales" app="sales" form="*"/>
    </roles>
    ```
- [ ] browser 1
    - clear cookies
    - [ ] `http://localhost:8080/2016.3-pe/fr/orbeon/builder/new`
        - login as `orbeon-sales`
        - must see guest and sales as app names
    - [ ] create sales/my-sales-form
        - set permissions
            - Anyone → Create
            - orbeon-sales → Read and Update
        - save and publish
    - [ ] can access
        - http://localhost:8080/2016.3-pe/fr/sales/my-sales-form/summary
        - http://localhost:8080/2016.3-pe/fr/sales/my-sales-form/new
    - [ ] new
        - enter data and save
    - [ ] summary
        - check that saved in summary
        - check can edit and duplicate
        - check Delete button is disabled
        - check PDF works
    - [ ] `http://localhost:8080/2016.3-pe/fr/`
        - sales/my-sales-form shows on the home page
        - *NOTE: Be careful in case sales/my-sales-form is also read from existing e.g. MySQL, etc.*
        - admin ops for sales/my-sales-form
        - other forms don't have admin ops
        - Select → All, then Operation → Unpublish Local Forms ([#1380](https://github.com/orbeon/orbeon-forms/issues/1380))
            - check forms w/o access were not selected!
        - now that sales/my-sales-form is unavailable
            - check the link is disabled
            - check that /new returns 404
    - [ ] `http://localhost:8080/2016.3-pe/fr/orbeon/builder/summary`
        - open structured search (be aware of  [#878](https://github.com/orbeon/orbeon-forms/issues/878))
        - check only guest and sales forms are available
- [ ] browser 2
    - [ ] clear cookies
    - [ ] login as orbeon-user
    - [ ] can access
        - `http://localhost:8080/2016.3-pe/fr/sales/my-sales-form/new`
    - [ ] can't access
        - http://localhost:8080/2016.3-pe/fr/sales/my-sales-form/summary (403)
        - http://localhost:8080/2016.3-pe/fr/sales/my-sales-form/edit/... (403)
            - *NOTE: with eXist, can save, even repeatedly, but can't load /edit/…*
    - [ ] `http://localhost:8080/2016.3-pe/fr/`
        - NO admin ops for sales/my-sales-form
        - BUT admin ops for `guest/*` (create a `guest/test-guest` form)
        - CAN click on line and takes to `/new`
        - CAN do Review/Edit/PDF
- [ ] browser 1
    - remove all permissions for Anyone for this form, re-add Create for orbeon-sales, publish
    - check can still new/edit/view
- [ ] browser 2
    - can't access
        - `http://localhost:8080/2016.3-pe/fr/sales/my-sales-form/new` (403)
    - http://localhost:8080/2016.3-pe/fr/
        - form not visible
- [ ] browser 1
    - re-add Anyone → Create
    - add Owner → Read
    - check nothing changed
        - well, can do `/new` from Home
- [ ] browser 2
    - can access `http://localhost:8080/2016.3-pe/fr/sales/my-sales-form/summary`, but only see own data as readonly
    - /new, save
    - Summary shows forms in readonly mode
- [ ] access is rejected if user doesn't have any matching roles ([#1963](https://github.com/orbeon/orbeon-forms/issues/1963))
    - in `form-builder-permissions.xml`:
        ```xml
        <role name="dummy" app="sales" form="*"/>
        <!--<role name="*"            app="guest" form="*"/>-->
        ```
    - clear cookies
    - access to FB Summary page is rejected
    - access to FB New page is rejected
    - access to FB Edit page is rejected if form doesn't have matching role

## Form Runner

### Sample forms \[2016.3 ERIK DONE\]

- [x] load all
- [x] Controls
    - [x] wizard navigation
    - [x] autocomplete works
    - [x] dynamic data dropdown loads data
    - [x] review/PDF look ok
    - [x] check wrapping in view/pdf modes
        - enter [Lorum Ipsum](http://www.lipsum.com/feed/html) in input field
        - enter long text without space in input field and textarea
- [x] Bookshelf
    - Detail
        - [x] help works
        - [x] review and back works
        - [x] add/remove repeats works
            - also with keyboard
        - [x] change lang works

### Errors and warnings \[2016.3 ERIK DONE\]

- FB: create form
    - required field
    - non-required field
    - field with 1 warning and 1 info
- FR: error summary
    - shows errors, warning and info
    - links and focuses to controls, including XBL controls, but not invalid output controls
- FR: review page if no errors
- FR: review page shows review dialog if warning or info
- FR: come back to edit page

### Process buttons \[2016.3 ERIK DONE\]

- create and publish guest/test
    - 1 required field
- add [these properties](https://gist.github.com/ebruchez/5666643), and add `review` to the list of buttons in the first property
- check standard behavior of buttons
    - save-draft
        - can save w/ invalid data
    - save and save-final
        - cannot save w/ invalid data
    - submit
        - echoes PDF URL (try to download)
    - save 2
        - echoes XML
    - home/summary/edit/review
- send w/ replace all/none
    - set acme.submit.replace to none
    - must not navigate after submit

### Attachments/uploads \[2016.3 ERIK DONE\]

- [x] basic upload works
- [x] removing uploaded file works
- [x] large uploads fail (> 100 MB by default)
    - FR error dialog shows
    - control is back to empty
- [x] very small (a few KB) upload works multiple times in a row
- [x] with throttling (with Charles) (*NOTE: Proxy settings not useful as we are looking at browser/server traffic.*)
    ```xml
    <property
        as="xs:string"
        name="oxf.http.proxy.host"
        value="localhost"/>
    <property
        as="xs:integer"
        name="oxf.http.proxy.port"
        value="8888"/>
    ```
    - cancel midway works
    - progress indicator works

### Submit \[2016.3 ERIK DONE\]

- comment out custom submit button process (`oxf.fr.detail.process.submit`) in properties
- config
    ```xml
    <property
        as="xs:string"
        name="oxf.fr.detail.submit.go.uri-xpath.*.*"
        value="'http://xformstest.org/cgi-bin/echo.sh'"/>
    <property
        as="xs:string"
        name="oxf.fr.detail.buttons.*.*"
        value="home summary review save-draft save-final save submit workflow-send"/>
    ```
- FR: in new page, click Submit then
    - clear and close
    - keep values and close
    - OK: goes to echo page
    - close window [NOTE: Only if window was open with JS.]

### Email \[2016.3 ERIK DONE\]

- NOTE: if using 2-factor auth w/ GMail, must use app-specific password for SMTP
    - https://accounts.google.com/b/0/IssuedAuthSubTokens#accesscodes
- config (change ebruchez@gmail.com in the following properties to your email)

```xml
<property as="xs:string"  name="oxf.fr.detail.buttons.*.*"           value="save email"/>
<property as="xs:string"  name="oxf.fr.email.smtp.host.*.*"          value="smtp.gmail.com"/>
<property as="xs:string"  name="oxf.fr.email.from.*.*"               value="ebruchez@gmail.com"/>
<property as="xs:string"  name="oxf.fr.email.to.*.*"                 value="ebruchez@gmail.com"/>
<property as="xs:string"  name="oxf.fr.email.smtp.username.*.*"      value="ebruchez@gmail.com"/>
<property as="xs:string"  name="oxf.fr.email.smtp.credentials.*.*"   value="**********"/>
<property as="xs:string"  name="oxf.fr.email.smtp.encryption.*.*"    value="tls"/>

<property as="xs:string"  name="oxf.fr.detail.buttons.orbeon.controls">
    email
</property>

<property as="xs:string"  name="oxf.fr.detail.buttons.orbeon.dmv-14">
    email
</property>

<property as="xs:boolean" name="oxf.fr.email.attach-pdf.orbeon.controls"  value="true"/>
<property as="xs:boolean" name="oxf.fr.email.attach-tiff.orbeon.controls" value="true"/>
```
- `/fr/orbeon/controls/new`
- hit Email button from Controls and DMV-14
  - check email received
  - contains attachments, XML, PDF and TIFF [SINCE 2016.1]
  - PDF: check fields are filled [#2207](https://github.com/orbeon/orbeon-forms/issues/2207)
  - check attached PDF looks like PDF generated from detail page, including checkboxes/radio buttons, and images

### Misc \[2016.3 ERIK DONE\]

- switch language
- open/close sections (but not with wizard)
- repeats
    - check can access repeated grid/section button and menu via keyboard navigation

### Noscript mode \[2016.3 ERIK DONE\]

*NOTE: The noscript mode still works mostly, but has an increasing number of issues and is now deprecated.*

- orbeon/contact
- Contact form
    - property
        ```xml
        <property as="xs:string"
            name="oxf.fr.detail.buttons.orbeon.contact"
            value="refresh clear save pdf review"/>
        ```
    - Clear clears right away
    - PDF stays in tab
        - *NOTE: PDF not showing with 2016.2 (was it also with 2016.1?). [#2869](https://github.com/orbeon/orbeon-forms/issues/2869)*
    - errors prevent saving
    - Refresh icon works
    - go to form with `?fr-noscript=true`
- create form with `xxf:noscript-support="true"` in FB (just property doesn't work!)
- test w/ new form w/ image & file attachments
    - attachments work [NOTE: be aware of [#1405](https://github.com/orbeon/orbeon-forms/issues/1405)]
- be aware of
    - [#2355](https://github.com/orbeon/orbeon-forms/issues/2355)
    - [#2356](https://github.com/orbeon/orbeon-forms/issues/2356)

### Wizard \[2016.3 ERIK TODO\]

- [ ] `<property as="xs:string" name="oxf.fr.detail.view.appearance.wizard.*" value="wizard"/>`
- [ ] validated mode
    - `/fr/orbeon/w9/new`
    - check cannot click in TOC
    - check cannot navigate forward with error in current section
    - once all sections visited, can freely navigate
    - check back to edit allows navigating all sections
    - `strict` vs. `lax` mode
    - with `explicit` validation
- [ ] `/fr/orbeon/controls/new`
    - test errors in section template are highlighted in TOC
- [ ] check that Form Builder's Wizard option enables/disables the wizard
- [ ] separate TOC
    ```xml
    <property
        as="xs:boolean"
        name="oxf.xforms.xbl.fr.wizard.separate-toc.wizard.*"    
        value="true"/>
    ```
- [ ] subsection navigation
    ```xml
    <property
        as="xs:boolean"
        name="oxf.xforms.xbl.fr.wizard.subsections-nav.wizard.*"
        value="true"/>
    ```
- [ ] TOC subsections
    ```xml
    <property
        as="xs:string"  
        name="oxf.xforms.xbl.fr.wizard.subsections-toc.wizard.*"
        value="all"/>
    ```

### Captcha \[2016.3 ERIK DONE\]

- enable with property

```xml
<property
    as="xs:string"
    name="oxf.fr.detail.captcha.*.*"
    value="reCAPTCHA"/>
<property
    as="xs:string"
    name="oxf.xforms.xbl.fr.recaptcha.public-key"
    value="6LesxAYAAAAAAEF9eTyysdkOF6O2OsPLO9zAiyzX"/>
<property
    as="xs:string"
    name="oxf.xforms.xbl.fr.recaptcha.private-key"
    value="6LesxAYAAAAAAJIXoxMvErqbisKkt7W-CPoE_Huo"/>
```
- test reCAPTCHA
    - *NOTE: had to fix 1 regression with 2016.1.*
    - *NOTE: had to fix 2 bugs with 4.5.*
- test SimpleCaptcha

### Help popups/hint tooltips positioning \[2016.3 ERIK TODO\]

- create form to test general positioning
  - help on all elements ([Lorum Ipsum](http://www.lipsum.com/feed/html))
  - repeats
  - checkboxes/radios
    - add hints (see [#1649](https://github.com/orbeon/orbeon-forms/issues/1649))
- Bookshelf
    - try all helps (see [#1637](https://github.com/orbeon/orbeon-forms/issues/1637))

### Mobile and Responsive \[2016.3 TODO\]

*NOTE: Summary and Home are not responsive as of 2016.1.*

- [ ] setup
    - iPhone 6S or 6S Plus
    - can also test more using simulator
- [ ] default layout (Contact Form / Bookshelf Form)
    - looks ok
    - can navigate to `view` and back
    - PDF shows
    - upload book cover in Bookshelf
- [ ] wizard layout (Controls form)
    - looks ok (TOC at top, buttons at bottom)
    - can navigate sections via TOC at top (click and buttons)
    - Next/Prev buttons at bottom work
- [ ] Control Form
    - can enter data, select checkboxes/radio buttons
    - date picker works
    - can quickly select radio buttons/checkboxes (zoom in if needed, touch areas are small)
    - signature works
    - PDF
    - NOTE: Repeat not handled nicely.
- [ ] DMV-14 Form
    - NOTE: Repeat not handled nicely.
    - PDF / TIFF
- [ ] W-9 Form
    - Review looks good
    - PDF looks good with signature
- [ ] zoom
    - can pinch zoom
    - add error, save, must de-zoom before showing Error dialog
    - same for Clear dialog
- [ ] Number field
    - test that non-negative integers show keypad
    - other numbers show number pane
    - if decimal separator is `,`, show regular pane (if US settings)
        - set attributes by hand: `decimal-separator="," grouping-separator="'"`
- be aware of [open issues](https://github.com/orbeon/orbeon-forms/issues?q=is%3Aopen+is%3Aissue+label%3AMobile)
- be aware of [#2875](https://github.com/orbeon/orbeon-forms/issues/2875)

### Home Page \[2016.3 ERIK TODO\]

*See also Form Builder permissions above which already tests some of this.*

- [ ] `http://localhost:8080/2016.3-pe/fr/` lists deployed forms
- [ ] comment all roles in form-builder-permissions.xml
- [ ] no admin buttons/actions show
- [ ] changing language to French works
- [ ] set all Form Builder permissions
    ```xml
    <role name="*" app="*" form="*"/>
    ```
  - admin actions show
  - Available/Unavailable/Library labels show
  - publish/unpublish works
- [ ] "publish to production"
  - configure  remote server and production-server-uri
    - e.g. remote in Liferay Tomcat
    ```xml
    <property as="xs:string" name="oxf.fr.home.remote-servers">
        [
          {
            "label": "Remote server",
            "url":   "http://Eriks-MacBook-Pro.local:9090/orbeon/"
          }
        ]
    </property>
    ```
    - use orbeon-auth.war on remote
    ```xml
    <property
        as="xs:anyURI"
        processor-name="oxf:page-flow"
        name="authorizer"
        value="/orbeon-auth"/>
    ```
  - server asks for credentials if user has admin role
      - `orbeon-admin/x*` (with `liferay-portal-6.1.2-ce-ga3/tomcat-7.0.40`, `orbeon-admin` has `orbeon-service` role)
  - Cancel  → loads local forms
  - Connect → loads local and remote forms, sorted by mod date desc
  - Select menu works
  - Operation menu works
      - push/pull forms
      - check available on `/fr/` page on remote (e.g. in Liferay)
  -   add 2nd remote server to `oxf.fr.home.remote-servers` property and check user is asked when loading page
      ```xml
      <property as="xs:string"  name="oxf.fr.home.remote-servers">
          [
            { "label": "Public Demo Server", "url": "http://demo.orbeon.com/orbeon" },
            { "label": "Local Liferay", "url": "http://Eriks-MacBook-Pro.local:9090/orbeon/" }
          ]
      </property>
      ```
  - take form (could be previous `sales/my-sales-form` (see [Form Builder Permissions](../form-builder/images/permissions-enable.png)) but doesn't have to be)
    - attach static image
    - publish locally
    - push to remote
    - check attachment is pushed
    - load form `/new` on remote, make sure works and attachment is there
    - *NOTE: `/summary` should do 403 if user is not `orbeon-sales` on remote. For this, make sure form has permissions enabled and e.g. `orbeon-sales` only can read.*
    - pull back form
    - load form `/new` on local, make sure works and attachment is there
  - no checkbox for forms w/o admin access (e.g. set `<role name="*" app="orbeon" form="*"/>`)
- [ ] upgrade form definitions
  - upgrade local
  - upgrade remote
  - make sure forms still work

### Summary Page \[2016.3 ERIK DONE\]

- e.g. `http://localhost:8080/2016.3-pe/fr/orbeon/bookshelf/summary`
- list forms
- paging
  - create more than 10 instances if necessary
  - test going to the next page
  - check total are correct
- search "Scala" works
- search Author = grey works
- switch language
- pdf
  - template
  - automatic
    - try `?fr-language=en` vs. `?fr-language=fr` on PDF URL
- duplicate
- delete
- view

### Excel Import \[2016.3 ERIK DONE\]

- `http://localhost:8080/2016.3-pe/fr/orbeon/contact/import`
- import small doc first (`contact5.xlsx` on Dropbox)
  - check 2 out of  5 docs invalid
  - continue and check import passes: 3 documents were imported
- import larger document (`contact300.xlsx`)
  - check 120 out of 300 docs invalid
  - continue and check import passes: 180 documents were imported
- check % and ETA progress during validation and import
- check import completes

### Liferay Support \[2016.3 ERIK DONE\]

- versions as of Orbeon Forms 2016.3
  - ~~Liferay Portal Community Edition 6.1.1 CE GA2 (Paton / Build 6101 / July 31, 2012)~~
  - Liferay Portal Community Edition 6.1.2 CE GA3 (August 2013)
  - Liferay Portal Community Edition 6.2 CE GA6 (January 2016)
- [x] setup
  - remove existing `orbeon` and `proxy-portlet.war` webapps if present
  - copy `orbeon.war` and `proxy-portlet.war` to `deploy` folder
  - start Liferay:
    - `./liferay-portal-6.1.2-ce-ga3/tomcat-7.0.40/bin/catalina.sh run`
    - `./liferay-portal-6.2-ce-ga6/tomcat-7.0.62/bin/catalina.sh run`
  - try `http://localhost:9090/` from Firefox
  - login
    - test@liferay.com/liferay
    - *NOTE: Cannot seem to login with Chrome anymore. Tried removing JSESSIONID, still issue.*
- [x] proxy portlet
  - [x] set to point to `http://localhost:8080/2016.3-pe/`
  - [x] try out pages
    - New page
    - Summary page
    - Home page
  - [x] set Send Liferay language
    - check that language picker in FR doesn't show on 3 pages
    - change My Account → Display Settings → French
    - check that all 3 pages now show in French (be aware of [#1628](https://github.com/orbeon/orbeon-forms/issues/1628))
  - [x] set Send Liferay user
    - check w/ HttpScoop that user headers are sent to Form Runner
      - `Orbeon-Liferay-User-*`
  - [x] readonly mode (be aware of [#884](https://github.com/orbeon/orbeon-forms/issues/884))
  - [x] edit/review/back
  - [x] send to external page
  - [x] PDF loads
    - check that checkboxes appear correctly (see [#2046](https://github.com/orbeon/orbeon-forms/issues/2046))
  - [x] check that TinyMCE (rich text) appears ok, including with IE9 (icons)
  - [x] upload works
  - [x] attach image and save
  - [x] check singleton form works
  - [ ] organizations
  - *NOTE: noscript broken in Liferay* [#1041](https://github.com/orbeon/orbeon-forms/issues/1041)
- [x] full portlet
  - [x] all examples and Form Runner
  - [x] upload works
  - [x] PDF works
    - check that checkboxes appear correctly
    - *NOTE: Hit issue of double JSESSIONID once, check cookies if problems.*
  - [x] attach image and save, make sure image shows properly
  - ~~Image annotation control works (in Controls form)~~
  - *NOTE: noscript broken in Liferay* [#1041](https://github.com/orbeon/orbeon-forms/issues/1041)

### Organizations \[2016.3 ERIK TODO\]

Do this just after general Liferay testing (above).

Setup hierarchy:

    ```
    World
        └── California
            ├── orbeoncaliforniauser1@orbeon.com (org owner/admin)
            ├── Foster City
            │   ├── orbeonfostercityuser1@orbeon.com (org owner/admin)
            │   └── orbeonfostercityuser2@orbeon.com
            └── Foster City
                ├── orbeonsancarlosuser1@orbeon.com (org owner/admin)
                └── orbeonsancarlosuser2@orbeon.com            
    ```
- TODO

### Embedding \[2016.3 ERIK DONE\]

- [x] deploy `orbeon-embedding.war` into Tomcat
- [x] update `web.xml`:

    ```xml
    <init-param>
        <param-name>form-runner-url</param-name>
        <param-value>http://localhost:8080/2016.3-pe</param-value>
    </init-param>
    ````
- [x] navigate to `http://localhost:8080/2016.3-pe-embedding/`
- [x] go through demo forms and test
  - enter data
  - Save
  - PDF
  - repeats
  - help/hints
  - controls to check
      - upload
      - signature
      - number
      - autocomplete
  - *NOTE: There are limitations, for example navigation (Summary, Review) won't work.*
- [x] Form Builder
    - layout is ok
    - hover icons
    - dialogs
    - inserting controls
    - view source
    - Test button

### XForms Retry \[2016.3 TODO\]

1. [ ] Retry happens
    - [ ] setup
        - edit `resources/apps/xforms-sandbox/samples/dispatch-delay.xhtml`
            - change sleep service to use `sleep?delay=10` (sleep 10 s)
            - add to model
            ```xml
            <xf:setvalue
                event="xforms-submit-done"
                ref="/instance/count"
                value=". + 1"/>
            ```
        - set the following properties

            ```xml
            <property
                as="xs:integer"
                name="oxf.xforms.delay-before-ajax-timeout"
                value="2000"/>
            <property
                as="xs:integer"
                name="oxf.xforms.retry.delay-increment"
                value="2000"/>
            ```
    - [ ] test
        - open `http://localhost:8080/2016.3-pe/xforms-sandbox/sample/dispatch-delay`
        - in Chrome, open the Dev Tools, go to the Network tab (or use HttpScoop or Charles)
        - hit the *Manual save* button
        - check after ~10 seconds that the Ajax response succeeds with 200 (retry will return with 503 until the 10 s have elapsed)
        - can also hit the *Start* button, and notice the number incrementing after ~10s
        - (the loading indicator doesn't show while a retry is not in progress, which is somewhat unintuitive, but we'll fix this as part of [#1114](https://github.com/orbeon/orbeon-forms/issues/1114))
2. [ ] Request not reaching server
    - change back  sleep service to use `sleep?delay=5` (sleep 5 s)
    - set the following properties

        ```xml
        <property
            as="xs:integer"
            name="oxf.xforms.delay-before-ajax-timeout"
            value="30000"/>
        <property
            as="xs:integer"
            name="oxf.xforms.retry.delay-increment"
            value="0"/>
        <property
            as="xs:string"
            name="oxf.http.proxy.host"
            value="localhost"/>
        <property
            as="xs:integer"
            name="oxf.http.proxy.port"
            value="8888"/>
        ```
    - load page again
    - using Charles, go in Proxy / Breakpoints (⌘⇧K), enable breakpoints, and add:
      ![](images/test-charles-request.png)
    - click on *Manual save*
    - the request is intercepted by Charles
    - when you click on Abort, check that the client retries the request right away and that the request doesn't show in the server logs
    - finally click on *Execute*, and check the request runs on the server, and the response reaches the browser after 5 s with a 200
3. [ ] Response not reaching client
    - change back  sleep service to use `sleep?delay=5` (sleep 5 s)
    - in Charles, edit the breakpoint set above (see screenshot), and this time break on the response, i.e. uncheck the "request" checkbox and check the "response" checkbox
    - click on *Manual save*
    - check after 5 s the breakpoint is hit
    - Abort (make sure to abort Ajax response, not call to sleep service - no longer an issue with 4.7+)
    - check the request is made again right away by the browser and replayed right away by the server
    - *Execute*
    - check the response reaches the client
4. [ ] Unexpected HTML response
    - change back  sleep service to use `sleep?delay=5` (sleep 5 s)
    - click on *Manual save*
    - edit the response to contain non-valid XML, and *Execute*
    - check the client re-executes the request
5. [ ] File upload
    - setup
        - enable breakpoint on response for `/2016.3-pe/xforms-server/upload`
        - enable throttling in Charles (⌘⇧T) per the following configuration
          ![](images/test-charles-throttling.png)
        - download [this image](http://placekitten.com/g/2000/2000) (~200 KB)
    - `http://localhost:8080/2016.3-pe/xforms-upload/`
    - select image, and upload start in the background
    - abort the response to the background upload
    - check it interrupts the download (we're not retrying uploads) and message says "There was an error during the upload."

### Error Dialog \[2016.3 ERIK DONE\]

See [#1938](https://github.com/orbeon/orbeon-forms/issues/1938).

- [x] scenario 1
  - load page
  - remove JSESSIONID
  - do Ajax update
  - server must respond with XML error document (be aware of [#2212](https://github.com/orbeon/orbeon-forms/issues/2212))
  - client must show error dialog
  - check logs don't show full exception
- [ ] scenario 2
  - same but with other error
      - WHICH ONE?
  - same result except that exception must be logged

### Other Browsers \[2016.3 TODO\]

- [ ] main tests above with Google Chrome
    - 2016.2: 52.0.2743.82 and 53.0.2785.57 beta
    - 2016.1: 49.0.2623.112
    - 4.10: ??? and 46.0.2490.4 dev
    - 4.9: 42.0.2311.135
    - 4.8: 39.0.2171.95 and 41.0.2267.0 dev
    - 4.7: 37.0.2062.122
    - 4.6: 37.0.2062.0 dev
    - 4.5: 35.0.1897.8 dev
- [ ] Form Builder / Form Runner tests with latest Firefox
    - 2016.2: 48.0
    - 2016.1: 45.0.2
    - 4.10: 40.0.2
    - 4.9: 37.0.1
    - 4.8: 34
    - 4.7: 32
    - 4.6: 30
    - 4.5: 27.0.1 and 28
- [ ] Form Builder / Form Runner tests with latest Safari
    - 2016.2: 9.1.1 (11601.6.17)
    - 2016.1: 9.1 (11601.5.17.1)
    - 4.10: 8.x.x
    - 4.9: 8.0.5 (10600.5.17)
    - 4.8: 8.0.2 (10600.2.5)
    - 4.7: 7.0.6
    - 4.6: 7.0.4
    - 4.5: 7.0.2
- [ ] Form Builder / Form Runner tests with IE11 (since 4.5)
- [ ] Form Builder / Form Runner tests with latest Edge
    - 2016.2
        - Edge 25.10586.0.0
        - EdgeHTML 13.10586
    - 2016.1
        - Edge 25.10586.0.0
        - EdgeHTML 13.10586
- [ ] Form Runner run with
    - IE10: FB has warning, FR works and looks ok
    - IE9: FB has warning, FR works and looks ok
    - IE8: FB has warning, FR works and looks ok

### Other \[2016.3 TODO\]

Features to test, with all supported browsers:

- [ ] give CE version a quick run
- [ ] XForms filter
    - `http://localhost:8080/2016.3-pe/xforms-jsp/guess-the-number/`
    - `http://localhost:8080/2016.3-pe/xforms-jsp/flickr-search/`
- [ ] examples-cli in distribution work (fix/remove them if not)
    - `unzip orbeon-4.7.0.201409262231-PE.zip`
    - `cd orbeon-4.7.0.201409262231-PE`
    - `unzip -d orbeon orbeon.war`
    - `java -jar orbeon/WEB-INF/orbeon-cli.jar examples-cli/simple/stdout.xpl`
    - `java -jar orbeon/WEB-INF/orbeon-cli.jar examples-cli/transform/transform.xpl`
- [ ] check logs are clean
    - no debug information
    - no unwanted information
    - be aware of [#849](https://github.com/orbeon/orbeon-forms/issues/849)
