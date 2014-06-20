> [Wiki](Home) ▸ Contributors ▸ [Test Plan](./Contributors-:-Test-Plan)

- [x] setup
    - [x] with MySQL, Oracle, DB2
    - [x] restore `form-builder-permissions.xml` to default
    - [x] for container auth
        - [x] in `web.xml`
            - [x] uncomment security section towards the end
            - [x] change first `<url-pattern>` from `/fr/*` to `/auth` (it doesn't matter that page doesn't exist, it's just a path to force authentication)
        - [x] in `tomcat-users.xml`, setup users:
            - [x] `<user username="clerk" password="clerk" roles="orbeon-user,clerk"/>`
            - [x] `<user username="admin" password="admin" roles="orbeon-user,admin"/>`
        - [x] properties
            - [x] `<property
    as="xs:string"
    name="oxf.fr.authentication.container.roles"
    value="orbeon-user orbeon-sales orbeon-admin clerk admin"/>`
    - [x] for headers-based  auth
        - [x] set rewriting rules with Charles
            - [x] for user clerk ([gist][16])
            - [x] for user admin ([gist][17])
        - [x] to switch between users in below steps
            - [x] enable rewrite for clerk or admin headers, or disable rewrite
            - [x] remove JSESSIONID when switching users
- [x] in Form Builder
    - [x] create new form acme/permissions
    - [x] enable permissions for form and configure like on [doc page][18]
        - [x] NOTE: doc on new table format is also on this page
    - [x] save and publish
- [x] make sure permissions are followed
    - [x] anonymous user
        - [x] home page: link goes to new page (not summary)
        - [x] summary page: access rejected (fixed regression with [#1201][19])
        - [x] detail page: only new mode accepted
    - [x] logged in user
        - [x] check permissions as clerk/clerk
            - [x] remove `JSESSIONID` (i.e. with Dev Tools)
            - [x] switch user
            - [x] home page: link goes to the summary page
            - [x] summary page
                - [x] sees data previously entered by anonymous user, cannot delete
                - [x] click on new button opens new page
                - [x] click on existing data shows read-only view
                - [x] PDF works
        - [x] check permissions as admin/admin
            - [x] remove `JSESSIONID` (i.e. with Dev Tools)
            - [x] switch user
            - [x] on click goes to summary page
            - [x] on summary page
                - [x] click on new opens new page
                - [x] sees data previously entered by anonymous user, delete button enabled
                - [x] on open data, can edit data
                - [x] delete works

[16]: https://gist.github.com/ebruchez/10079296
[17]: https://gist.github.com/ebruchez/10079254
[18]: http://wiki.orbeon.com/forms/doc/developer-guide/form-runner/access-control#TOC-Enabling-permissions
[19]: https://github.com/orbeon/orbeon-forms/issues/1201