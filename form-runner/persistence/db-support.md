# Database support

## Categories of databases

We have two categories of databases:

- __Relational__: Oracle, MySQL, SQL Server, PostgreSQL, DB2
- __XML__: eXist (deprecated since Orbeon Forms 2019.1, removed since Orbeon Forms 2023.1)

Since Orbeon Forms 4.4, the implementation of relational support is common to all databases. There used to be separate implementation for each relational database.

For setup instructions, see [Using Form Runner with a relational database](relational-db.md).

## Feature matrix

| Feature                                                                  | eXist | Oracle | MySQL | SQL Server | PostgreSQL | DB2 |
|--------------------------------------------------------------------------|-------|--------|-------|------------|------------|-----|
| [Form controls and layouts including repeated grids and sections][blog1] | Y     | Y      | Y     | Y⁴         | Y⁶         | Y¹  |
| [Versioning][blog2]                                                      | N     | Y³     | Y³    | Y⁴         | Y⁶         | Y³  |
| [Owner/group-based permissions](../access-control/owner-group.md)        | Y⁶    | Y²     | Y¹    | Y⁴         | Y⁶         | Y¹  |
| [Autosave](autosave.md)                                                  | N     | Y²     | Y¹    | Y⁴         | Y⁶         | Y¹  |
| [Flat view](flat-view.md)                                                | N/A   | Y      | N     | Y⁷         | Y⁶         | Y⁵  |
| Orbeon Forms PE support                                                  | Y     | Y      | Y     | Y          | Y          | Y   |
| Orbeon Forms CE support                                                  | Y     | N      | Y     | N          | Y          | N   |

1. Since Orbeon Forms 4.3.
2. Since Orbeon Forms 4.4.
3. Since Orbeon Forms 4.5.
4. Since Orbeon Forms 4.6.
5. Since Orbeon Forms 4.7.
6. Since Orbeon Forms 4.8.
7. Since Orbeon Forms 2016.2.

## Third-party implementations

A third-party [MarkLogic persistence layer for Orbeon Form Runner](https://gitlab.dyomedea.com/marklogic/orbeon-form-runner-persistence-layer/tree/master) is available. As of 2015-03-02, this does not support versioning and owner/group-based permissions. Also please note that this is currently not officially supported by Orbeon.

[blog1]: https://blog.orbeon.com/2014/01/repeated-sections.html
[blog2]: https://blog.orbeon.com/2014/02/form-versioning.html

## See also 

- [Using Form Runner with a relational database](relational-db.md)

