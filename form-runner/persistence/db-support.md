# Database support

## Categories of databases

We have two categories of databases:

- __Built-in relational support__: Built-in support for Oracle, MySQL, SQL Server, PostgreSQL, and DB2.
- __Through custom persistence providers__: See [Custom persistence providers](../api/persistence/custom-persistence-providers.md).

For setup instructions, see [Using Form Runner with a relational database](relational-db.md).

## Feature matrix

| Feature                                                                  | Oracle | MySQL | SQL Server | PostgreSQL | DB2 |
|--------------------------------------------------------------------------|--------|-------|------------|------------|-----|
| [Form controls and layouts including repeated grids and sections][blog1] | Y      | Y     | Y          | Y          | Y   |
| [Versioning][blog2]                                                      | Y      | Y     | Y          | Y          | Y   |
| [Owner/group-based permissions](../access-control/owner-group.md)        | Y      | Y     | Y          | Y          | Y   |
| [Autosave](autosave.md)                                                  | Y      | Y     | Y          | Y          | Y   |
| [Flat view](flat-view.md)                                                | Y      | N     | Y          | Y          | Y   |
| Orbeon Forms PE support                                                  | Y      | Y     | Y          | Y          | Y   |
| Orbeon Forms CE support                                                  | N      | Y     | N          | Y          | N   |

## Older database support

Support for the eXist database was deprecated since Orbeon Forms 2019.1 and removed since Orbeon Forms 2023.1.

## See also 

- [Using Form Runner with a relational database](relational-db.md)
- [Relational database schema](relational-db-schema.md)
- [Custom persistence providers](../api/persistence/custom-persistence-providers.md)

[blog1]: https://blog.orbeon.com/2014/01/repeated-sections.html
[blog2]: https://blog.orbeon.com/2014/02/form-versioning.html