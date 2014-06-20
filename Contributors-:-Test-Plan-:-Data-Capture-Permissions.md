> [Wiki](Home) ▸ Contributors ▸ [Test Plan](./Contributors-:-Test-Plan)

- setup
    - with MySQL, Oracle, DB2
    - restore `form-builder-permissions.xml` to default
    - for container auth
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
        - set rewriting rules with Charles
            - for user clerk ([gist][16])
            - for user admin ([gist][17])
        - to switch between users in below steps
            - enable rewrite for clerk or admin headers, or disable rewrite
            - remove JSESSIONID when switching users
- in Form Builder
    - create new form acme/permissions
    - enable permissions for form and configure like on [doc page][18]
        - NOTE: doc on new table format is also on this page
    - save and publish
- make sure permissions are followed
    - anonymous user
        - home page: link goes to new page (not summary)
        - summary page: access rejected (fixed regression with [#1201][19])
        - detail page: only new mode accepted
    - logged in user
        - check permissions as clerk/clerk
            - remove `JSESSIONID` (i.e. with Dev Tools)
            - switch user
            - home page: link goes to the summary page
            - summary page
                - sees data previously entered by anonymous user, cannot delete
                - click on new button opens new page
                - click on existing data shows read-only view
                - PDF works
        - check permissions as admin/admin
            - remove `JSESSIONID` (i.e. with Dev Tools)
            - switch user
            - on click goes to summary page
            - on summary page
                - click on new opens new page
                - sees data previously entered by anonymous user, delete button enabled
                - on open data, can edit data
                - delete works

[16]: https://gist.github.com/ebruchez/10079296
[17]: https://gist.github.com/ebruchez/10079254
[18]: http://wiki.orbeon.com/forms/doc/developer-guide/form-runner/access-control#TOC-Enabling-permissions
[19]: https://github.com/orbeon/orbeon-forms/issues/1201