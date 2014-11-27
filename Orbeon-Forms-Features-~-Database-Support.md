> [[Home]] ▸ [[Orbeon Forms Features]]

## Categories of databases

We have two categories of databases:

- XML: eXist
- relational: Oracle, MySQL, SQL Server, DB2

*NOTE: As of Orbeon Forms 4.4, the implementation of relational support is common to all databases. There used to be separate implementation for each relational database.*

## Feature matrix

Feature                              | eXist        | Oracle | MySQL | SQL Server | PostgreSQL | DB2
-------------------------------------|--------------|--------|-------|------------|------------|----
[Form controls and layouts including repeated grids and sections][^1]    | Y     | Y      |Y      |Y        |Y           |Y
[Versioning][^2]                     | N            | Y      |Y      |Y           |Y           |Y
[Owner/group-based permissions][^3]  | Y<sup>1</sup>| Y      |Y      |Y           |Y           |Y
[Autosave][^4]                       | N            | Y      |Y      |Y           |Y           |Y
[Flat view][^5]                      | N/A          | Y      |N      |N           |Y           |Y

1. Since Orbeon Forms 4.8.

[^1]: http://blog.orbeon.com/2014/01/repeated-sections.html
[^2]: http://blog.orbeon.com/2014/02/form-versioning.html
[^3]: http://wiki.orbeon.com/forms/doc/developer-guide/form-runner/access-control#TOC-Permissions-for-owner-group-members
[^4]: https://github.com/orbeon/orbeon-forms/wiki/Form-Runner-~-Autosave
[^5]: https://github.com/orbeon/orbeon-forms/wiki/Form-Runner-~-Persistence-~-Flat-View
