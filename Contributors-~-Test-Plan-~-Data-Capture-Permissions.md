> [[Home]] ▸ Contributors ▸ [[Test Plan|Contributors ~ Test Plan]]

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