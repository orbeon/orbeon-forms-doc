> [[Home]] ▸ Contributors ▸ [[Test Plan|Contributors ~ Test Plan]]

- setup
    - with eXist, Oracle, MySQL, SQL Server, DB2
    - restore `form-builder-permissions.xml` to default
    - for container auth
        - `<property as="xs:string"  name="oxf.fr.authentication.method" value="container"/>`
        - in `web.xml`
            - uncomment security section towards the end
            - change first `<url-pattern>` from `/fr/*` to `/auth` (it doesn't matter that page doesn't exist, it's just a path to force authentication)
        - in `tomcat-users.xml`, setup users:
            - `<user username="clerk" password="clerk" roles="orbeon-user,clerk"/>`
            - `<user username="admin" password="admin" roles="orbeon-user,admin"/>`
        - properties
            - `<property
    as="xs:string"
    name="oxf.fr.authentication.container.roles"
    value="orbeon-user orbeon-sales orbeon-admin clerk admin"/>`
    - for headers-based  auth
        - `<property as="xs:string"  name="oxf.fr.authentication.method" value="header"/>`
        - set rewriting rules with Charles (⌘⇧W)
            - for user clerk ([gist][16])
            - for user admin ([gist][17])
        - to switch between users in below steps
            - enable rewrite for clerk or admin headers, or disable rewrite
            - remove JSESSIONID when switching users
- in Form Builder
    - create new form `oracle/permissions`, `mysql/permissions`, `sqlserver/permissions`, `db2/permissions` (create 1 form then use Duplicate button)
    - enable permissions for form and configure like on [doc page][18]
        - NOTE: doc on new table format is also on this page
    - save and publish
- make sure permissions are followed
    - anonymous user
        - home page: link goes to new page (not summary)
        - summary page: unauthorized (fixed regression with [#1201][19])
        - detail page: only `new` accepted, `edit`, `view`, `pdf` are unauthorized
    - logged in user
        - check permissions as clerk/clerk
            - remove `JSESSIONID` (i.e. with Dev Tools)
            - switch user
            - home page: link goes to the summary page
            - summary page
                - sees data previously entered by anonymous user, cannot delete
                - click on existing data created by anonymous user shows read-only view
                - PDF works
                - click on new button opens new page
            - new/edit
                - save data works
                - user is owner so can edit his own data
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
[18]: http://wiki.orbeon.com/forms/doc/developer-guide/form-runner/access-control#TOC-Enabling-permissions
[19]: https://github.com/orbeon/orbeon-forms/issues/1201