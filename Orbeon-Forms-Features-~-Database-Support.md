> [[Home]] ▸ [[Orbeon Forms Features]]

## Categories of databases

We have two categories of databases:

- XML: eXist
- Relational: Oracle, MySQL, SQL Server, DB2

*NOTE: As of Orbeon Forms 4.4, the implementation of relational support is common to all databases. There used to be separate implementation for each relational database.*

## Feature matrix

Feature                                                              |eXist        |Oracle       |MySQL        |SQL Server   |PostgreSQL   |DB2
---------------------------------------------------------------------|-------------|-------------|-------------|-------------|-------------|-------------
[Form controls and layouts including repeated grids and sections][^1]|Y            |Y            |Y            |Y<sup>4</sup>|Y<sup>6</sup>|Y<sup>1</sup>
[Versioning][^2]                                                     |N            |Y<sup>3</sup>|Y<sup>3</sup>|Y<sup>4</sup>|Y<sup>6</sup>|Y<sup>3</sup>
[Owner/group-based permissions][^3]                                  |Y<sup>6</sup>|Y<sup>2</sup>|Y<sup>1</sup>|Y<sup>4</sup>|Y<sup>6</sup>|Y<sup>1</sup>
[Autosave][^4]                                                       |N            |Y<sup>2</sup>|Y<sup>1</sup>|Y<sup>4</sup>|Y<sup>6</sup>|Y<sup>1</sup>
[Flat view][^5]                                                      |N/A          |Y            |N            |N            |Y<sup>6</sup>|Y<sup>5</sup>

1. Since Orbeon Forms 4.3.
1. Since Orbeon Forms 4.4.
1. Since Orbeon Forms 4.5.
1. Since Orbeon Forms 4.6.
1. Since Orbeon Forms 4.7.
1. Since Orbeon Forms 4.8.

[^1]: http://blog.orbeon.com/2014/01/repeated-sections.html
[^2]: http://blog.orbeon.com/2014/02/form-versioning.html
[^3]: http://wiki.orbeon.com/forms/doc/developer-guide/form-runner/access-control#TOC-Permissions-for-owner-group-members
[^4]: https://github.com/orbeon/orbeon-forms/wiki/Form-Runner-~-Autosave
[^5]: https://github.com/orbeon/orbeon-forms/wiki/Form-Runner-~-Persistence-~-Flat-View

## Third-party support

A third-party [MarkLogic persistence layer for Orbeon Form Runner](https://gitlab.dyomedea.com/marklogic/orbeon-form-runner-persistence-layer/tree/master) is available. Please not that this is currently not officially supported by Orbeon.
