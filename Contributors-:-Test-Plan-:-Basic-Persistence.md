> [Wiki](Home) ▸ Contributors ▸ [Test Plan](./Contributors-:-Test-Plan)

We do the following just for eXist and DB2, as automated tests already test most of this, and the code running for DB2 is almost identical to the code running for other relational databases.

1. Setup: in `properties-local.xml`, add:

    ```xml
    <property as="xs:string" name="oxf.fr.persistence.provider.db2.*.*"   value="db2"/>
    <property as="xs:string" name="oxf.fr.persistence.provider.exist.*.*" value="exist"/>
    ```
2. Create same form in 2 apps: `exist/a`, `db2/a`.
    - Use Duplicate button in FB Summary
    - Then change app name
3. pages
    - FB: create form, publish
    - FR: check it shows on http://localhost:8080/orbeon/fr/
    - FR: create new form, review, back to edit (#1643)
    - FR: enter data, save
    - FR: check it shows in the summary page

