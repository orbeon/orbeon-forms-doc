# Database support

## Builtin and custom providers

Orbeon Forms provides persistence of form definitions and form data through *persistence providers*. There are two types of providers:

- __Built-in relational provider__: This provides built-in support for Oracle, MySQL, SQL Server, PostgreSQL, and DB2.
- __Custom persistence providers__: See [Custom persistence providers](../api/persistence/custom-persistence-providers.md) for details.

For relational database setup instructions, see [Using Form Runner with a relational database](relational-db.md).

## Feature matrix

With the built-in relational provider, some features are not available for all databases. The following table summarizes the support for each database.

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