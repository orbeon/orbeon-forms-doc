> [[Home]] ▸ Contributors ▸ [[Test Plan|Contributors ~ Test Plan]]

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
    </tomcat-users>
    ```
    - `properties-local.xml`

    ```xml
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
- browser 1
    - http://localhost:8080/410pe/fr/orbeon/builder/new
    - login as orbeon-sales
    - must see guest and sales as app names
    - create sales/my-sales-form
    - set permissions
        - Anyone → Create
        - orbeon-sales → Read and Update
    - save and publish
    - can access
        - http://localhost:8080/410pe/fr/sales/my-sales-form/summary
        - http://localhost:8080/410pe/fr/sales/my-sales-form/new
    - new
        - enter data and save
    - summary
        - check that saved in summary
        - check can edit and duplicate
        - check Delete button is absent or disabled
        - check PDF works
    - http://localhost:8080/410pe/fr/
        - sales/my-sales-form shows on the home page
        - *NOTE: Be careful in case sales/my-sales-form is also read from existing e.g. MySQL, etc.*
        - admin ops for sales/my-sales-form
        - other forms don't have admin ops
        - Select → All, then Operation → Unpublish Local Forms ([#1380][7])
            - check forms w/o access were not selected!
        - now that sales/my-sales-form is unavailable
            - check the link is disabled
            - check that /new returns 404
    - http://localhost:8080/410pe/fr/orbeon/builder/summary
        - open structured search (be aware of  [#878][8])
        - check only guest and sales forms are available
- browser 2
    - login as orbeon-user
    - can access
        - http://localhost:8080/410pe/fr/sales/my-sales-form/new
    - can't access
        - http://localhost:8080/410pe/fr/sales/my-sales-form/summary (403)
        - http://localhost:8080/410pe/fr/sales/my-sales-form/edit/... (403)
            - *NOTE: with eXist, can save, even repeatedly, but can't load /edit/…*
    - http://localhost:8080/410pe/fr/
        - NO admin ops for sales/my-sales-form
        - BUT admin ops for guest/*
        - CAN click on line and takes to /new
        - CAN do Review/Edit/PDF
- browser 1
    - remove all permissions for Anyone for this form, re-add Create for orbeon-sales, save, publish
    - check can still new/edit/view
- browser 2
    - can't access
        - http://localhost:8080/410pe/fr/sales/my-sales-form/new (403)
    - http://localhost:8080/410pe/fr/
        - form not visible
- browser 1
    - re-add Anyone → Create
    - add Owner → Read
    - check nothing changed
- browser 2
    - can access http://localhost:8080/410pe/fr/sales/my-sales-form/summary, but only see own data as readonly
    - /new, save
    - Summary shows forms in readonly mode
- access is rejected if user doesn't have any matching roles ([#1963](https://github.com/orbeon/orbeon-forms/issues/1963))
  - setup `dummy` role only in `form-builder-permissions.xml`
  - access to FB Summary page is rejected
  - access to FB New page is rejected
  - access to FB Edit page is rejected if form doesn't have matching role

[7]: https://github.com/orbeon/orbeon-forms/issues/1380
[8]: https://github.com/orbeon/orbeon-forms/issues/878
