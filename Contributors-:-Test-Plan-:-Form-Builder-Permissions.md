> [Wiki](Home) ▸ Contributors ▸ [Test Plan](./Contributors-:-Test-Plan)

- NOTES 2014-03-20:
    - Would be really nice to have automated for this!
    - Should do for eXist, SQL Server and Oracle.
- setup
    - "Uncomment this for the Form Runner authentication" in web.xml
    - `tomcat-users.xml`

    ```
    <tomcat-users>
        <role rolename="orbeon-user"/>
        <role rolename="orbeon-sales"/>
        <role rolename="orbeon-admin"/>
        <user username="orbeon-user"  password="xforms" roles="orbeon-user"/>
        <user username="orbeon-sales" password="xforms" roles="orbeon-user,orbeon-sales"/>
        <user username="orbeon-admin" password="xforms" roles="orbeon-user,orbeon-admin"/>
    </tomcat-users>
    <property
        as="xs:string"
        name="oxf.fr.authentication.container.roles"
        value="orbeon-user orbeon-sales orbeon-admin"/>
    ```
    - `form-builder-permissions.xml`

    ```
    <roles>
        <role name="*"            app="guest" form="*"/>
        <role name="orbeon-sales" app="sales" form="*"/>
    </roles>
    ```
- browser 1
    - http://localhost:8080/orbeon/fr/orbeon/builder/new
    - login as orbeon-sales
    - must see guest and sales as app names
    - create sales/my-sales-form
    - set permissions
        - Anyone → Create
        - orbeon-sales → Read and Update
    - save and publish
    - can access
        - http://localhost:8080/orbeon/fr/sales/my-sales-form/summary
        - http://localhost:8080/orbeon/fr/sales/my-sales-form/new
    - new
    - summary
        - check  that saved in summary
        - check can edit and duplicate
        - check Delete button is absent or disabled
        - check PDF works
    - http://localhost:8080/orbeon/fr/
        - Summary/New operations visible for sales/my-sales-form
        - admin ops for sales/my-sales-form
        - other forms don't have admin ops
        - Select -> All, then Operation -> Unpublish Local Forms ([#1380][7])
            - check forms w/o access were not selected!
        - Summary/New not visible for sales/my-sales-form
    - FB summary page
        - open structured search (be aware of  [#878][8])
        - check only guest and sales forms are available
- browser 2
    - login as orbeon-user
    - can access
        - http://localhost:8080/orbeon/fr/sales/my-sales-form/new
    - can't access
        - http://localhost:8080/orbeon/fr/sales/my-sales-form/summary (403) (see [#1386][9] with eXist)
        - http://localhost:8080/orbeon/fr/sales/my-sales-form/edit/... (403)
            - NOTE: with eXist, can save, even repeatedly, but can't load /edit/…
    - http://localhost:8080/orbeon/fr/
        - NO admin ops for sales/my-sales-form
        - CAN click on line and takes to /new
- browser 1
    - remove all permissions for Anyone for this form, re-add Create for orbeon-sales, save, publish
    - check can still new/edit/view
- browser 2
    - can't access
        - http://localhost:8080/orbeon/fr/sales/my-sales-form/new (403)
    - http://localhost:8080/orbeon/fr/
        - form not visible
- browser 1
    - re-add Anyone → Create
    - add Owner → Read
    - check nothing changed
- browser 2
    - check still can't access http://localhost:8080/orbeon/fr/sales/my-sales-form/summary (403) (see [#1384][10] with eXist)