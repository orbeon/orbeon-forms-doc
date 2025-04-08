# Relational database schema

## Overview

Orbeon Forms uses, out of the box, a relational database persistence provider. This page describes the purpose of the tables and sequences used by this provider, as well as points to current and historical DDL (Data Definition Language) files for the supported versions of Orbeon Forms.

For more about the relational database setup, see [Using Form Runner with a relational database](relational-db.md).

## Tables

| Table Name                      | Description                                                                     | See also                                                            |
|---------------------------------|---------------------------------------------------------------------------------|---------------------------------------------------------------------|
| `orbeon_form_definition`        | Holds _published_ form definitions.<sup>1</sup>                                 |                                                                     |
| `orbeon_form_definition_attach` | Holds attachments related to _published_ form definitions.<sup>1</sup>          |                                                                     |
| `orbeon_form_data`              | Holds saved and draft form data.<sup>2</sup>                                    |                                                                     |
| `orbeon_form_data_attach`       | Holds attachments associated with submitted form data.<sup>2</sup>              |                                                                     |
| `orbeon_form_data_lease`        | Holds information about active leases on form data.                             | [Lease feature](../feature/lease.md)                                |
| `orbeon_organization`           | Holds information about organizations.                                          | [Organization-based permissions](../access-control/organization.md) |
| `orbeon_i_current`              | Holds references to the current form data, as opposed to historical data.       |                                                                     |
| `orbeon_i_control_text`         | Holds the values of indexed form controls for search and the Summary page.      | [Search API](../api/persistence/search.md)                          |
| `orbeon_seq`                    | Used to generate organization IDs (except for Oracle where it is a `sequence`). | [Organization-based permissions](../access-control/organization.md) |

1. The `orbeon_form_definition` and `orbeon_form_definition_attach` also hold _published_ section template libraries and their attachments if any.
2. The `orbeon_form_data` and `orbeon_form_data_attach` tables also hold _unpublished_ form definitions, unpublished libraries, as well as form templates, and their attachments if any. These form definitions and libraries are visible when you go to the [Form Builder Summary page](/form-builder/summary-page.md), and which you can edit with Form Builder.

See also:

- [Terminology](/form-runner/overview/terminology.md)
- [Publishing](/form-builder/publishing.md)

## Sequences

| Sequence Name | Description                                                                |
|---------------|----------------------------------------------------------------------------|
| `orbeon_seq`  | Oracle database only: Used to generate organization IDs and form data IDs. |
 
## Current DDL

### Introduction

The following sections list the DDL (Data Definition Language) files for the latest supported versions of Orbeon Forms. The DDL files are used to create the database schema from scratch or to upgrade an existing schema to a newer version.

For a list of supported Orbeon Forms versions, see [Release History](/release-history.md).

### Oracle

| Orbeon Forms version(s) | DDL to create from scratch | DDL to upgrade from previous format |
|-------------------------|----------------------------|-------------------------------------|
| 2024.1 and newer        | [oracle-2024_1.sql]        | [oracle-2019_1-to-2024_1.sql]       |
| 2022.1 to 2023.1        | [oracle-2019_1.sql]        | [oracle-2018_2-to-2019_1.sql]       |

### MySQL

| Orbeon Forms version(s) | DDL to create from scratch | DDL to upgrade from previous format |
|-------------------------|----------------------------|-------------------------------------|
| 2024.1 and newer        | [mysql-2024_1.sql]         | [mysql-2019_1-to-2024_1.sql]        |
| 2022.1 to 2023.1        | [mysql-2019_1.sql]         | [mysql-2018_2-to-2019_1.sql]        |

### SQL Server

| Orbeon Forms version(s) | DDL to create from scratch | DDL to upgrade from previous format |
|-------------------------|----------------------------|-------------------------------------|
| 2024.1 and newer        | [sqlserver-2024_1.sql]     | [sqlserver-2023_1-to-2024_1.sql]    |
| 2023.1                  | [sqlserver-2023_1.sql]     | [sqlserver-2019_1-to-2023_1.sql]    |
| 2022.1                  | [sqlserver-2019_1.sql]     | [sqlserver-2017_2-to-2019_1.sql]    |

### PostgreSQL

| Orbeon Forms version(s) | DDL to create from scratch | DDL to upgrade from previous format |
|-------------------------|----------------------------|-------------------------------------|
| 2024.1 and newer        | [postgresql-2024_1.sql]    | [postgresql-2023_1-to-2024_1.sql]   |
| 2023.1                  | [postgresql-2023_1.sql]    | [postgresql-2019_1-to-2023_1.sql]   |
| 2022.1                  | [postgresql-2019_1.sql]    | [postgresql-2018_2-to-2019_1.sql]   |

### DB2

| Orbeon Forms version(s) | DDL to create from scratch | DDL to upgrade from previous format |
|-------------------------|----------------------------|-------------------------------------|
| 2022.1 and newer        | [db2-2019_1.sql]           | [db2-2017_2-to-2019_1.sql]          |

## Historical DDL

### Introduction

The following sections list the DDL (Data Definition Language) files for older and non-supported versions of Orbeon Forms.

For a list of supported Orbeon Forms versions, see [Release History](/release-history.md).

### Oracle

| Orbeon Forms version(s) | DDL to create from scratch | DDL to upgrade from previous format |
|-------------------------|----------------------------|-------------------------------------|
| 2019.1 to 2021.1        | [oracle-2019_1.sql]        | [oracle-2018_2-to-2019_1.sql]       |
| 2018.2                  | [oracle-2018_2.sql]        | [oracle-2017_2-to-2018_2.sql]       |
| 2017.2, 2018.1          | [oracle-2017_2.sql]        | [oracle-2017_1-to-2017_2.sql]       |
| 2017.1                  | [oracle-2017_1.sql]        | [oracle-2016_3-to-2017_1.sql]       |
| 2016.3                  | [oracle-2016_3.sql]        | [oracle-2016_2-to-2016_3.sql]       |
| 2016.2                  | [oracle-2016_2.sql]        | [oracle-4_10-to-2016_2.sql]         |
| 4.10, 2016.1            | [oracle-4_10.sql]          | [oracle-4_6-to-4_10.sql]            |
| 4.6, 4.7, 4.8, 4.9      | [oracle-4_6.sql]           | [oracle-4_5-to-4_6.sql]             |
| 4.5                     | [oracle-4_5.sql]           | [oracle-4_4-to-4_5.sql]             |
| 4.4                     | [oracle-4_4.sql]           | [oracle-4_3-to-4_4.sql]             |
| 4.3                     | [oracle-4_3.sql]           | -                                   |

### MySQL

| Orbeon Forms version(s) | DDL to create from scratch | DDL to upgrade from previous format |
|-------------------------|----------------------------|-------------------------------------|
| 2019.1 to 2021.1        | [mysql-2019_1.sql]         | [mysql-2018_2-to-2019_1.sql]        |
| 2018.2                  | [mysql-2018_2.sql]         | [mysql-2017_2-to-2018_2.sql]        |
| 2017.2, 2018.1          | [mysql-2017_2.sql]         | [mysql-2016_3-to-2017_2.sql]        |
| 2016.3, 2017.1          | [mysql-2016_3.sql]         | [mysql-2016_2-to-2016_3.sql]        |
| 2016.2                  | [mysql-2016_2.sql]         | [mysql-4_6-to-2016_2.sql]           |
| 4.6 to 4.10, 2016.2     | [mysql-4_6.sql]            | [mysql-4_5-to-4_6.sql]              |
| 4.5                     | [mysql-4_5.sql]            | [mysql-4_4-to-4_5.sql]              |
| 4.4                     | [mysql-4_4.sql]            | [mysql-4_3-to-4_4.sql]              |
| 4.3                     | [mysql-4_3.sql]            | -                                   |

### SQL Server

| Orbeon Forms version(s) | DDL to create from scratch | DDL to upgrade from previous format |
|-------------------------|----------------------------|-------------------------------------|
| 2019.1 to 2021.1        | [sqlserver-2019_1.sql]     | [sqlserver-2017_2-to-2019_1.sql]    |
| 2017.2                  | [sqlserver-2017_2.sql]     | [sqlserver-2016_3-to-2017_2.sql]    |
| 2016.3 to 2017.1        | [sqlserver-2016_3.sql]     | [sqlserver-2016_2-to-2016_3.sql]    |
| 2016.2                  | [sqlserver-2016_2.sql]     | [sqlserver-4_6-to-2016_2.sql]       |
| 4.6 to 2016.1           | [sqlserver-4_6.sql]        | -                                   |

### PostgreSQL

| Orbeon Forms version(s) | DDL to create from scratch | DDL to upgrade from previous format |
|-------------------------|----------------------------|-------------------------------------|
| 2019.1 to 2021.1        | [postgresql-2019_1.sql]    | [postgresql-2018_2-to-2019_1.sql]   |
| 2018.2                  | [postgresql-2018_2.sql]    | [postgresql-2017_2-to-2018_2.sql]   |
| 2017.2, 2018.1          | [postgresql-2017_2.sql]    | [postgresql-2016_3-to-2017_2.sql]   |
| 2016.3 to 2017.1        | [postgresql-2016_3.sql]    | [postgresql-2016_2-to-2016_3.sql]   |
| 2016.2                  | [postgresql-2016_2.sql]    | [postgresql-4_8-to-2016_2.sql]      |
| 4.8 to 2016.1           | [postgresql-4_8.sql]       | -                                   |

### DB2

| Orbeon Forms version(s) | DDL to create from scratch | DDL to upgrade from previous format |
|-------------------------|----------------------------|-------------------------------------|
| 2019.1 to 2021.1        | [db2-2019_1.sql]           | [db2-2017_2-to-2019_1.sql]          |
| 2017.2 to 2018.2        | [db2-2017_2.sql]           | [db2-2016_3-to-2017_2.sql]          |
| 2016.3 to 2017.1        | [db2-2016_3.sql]           | [db2-2016_2-to-2016_3.sql]          |
| 2016.2                  | [db2-2016_2.sql]           | [db2-4_6-to-2016_2.sql]             |
| 4.6 to 2016.1           | [db2-4_6.sql]              | [db2-4_4-to-4_6.sql]                |
| 4.4 to 4.5              | [db2-4_4.sql]              | [db2-4_3-to-4_4.sql]                |
| 4.3                     | [db2-4_3.sql]              | -                                   |

## See also

- [Database support](db-support.md)
- [Using Form Runner with a relational database](relational-db.md)
- [Custom persistence providers](../api/persistence/custom-persistence-providers.md)
- [Search API](../api/persistence/search.md)
- [Lease feature](../feature/lease.md)
- [Organization-based permissions](../access-control/organization.md)
- [Release History](/release-history.md)

[mysql-2024_1.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2024.1/mysql-2024_1.sql
[mysql-2019_1-to-2024_1.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2024.1/mysql-2019_1-to-2024_1.sql
[mysql-2018_2-to-2019_1.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2019.1/mysql-2018_2-to-2019_1.sql
[mysql-2019_1.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2019.1/mysql-2019_1.sql

[mysql-2017_2-to-2018_2.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2018.2/mysql-2017_2-to-2018_2.sql
[mysql-2018_2.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2018.2/mysql-2018_2.sql
[mysql-2016_3-to-2017_2.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2017.2/mysql-2016_3-to-2017_2.sql
[mysql-2017_2.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2017.2/mysql-2017_2.sql
[mysql-2016_2-to-2016_3.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2016.3/mysql-2016_2-to-2016_3.sql
[mysql-2016_3.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2016.3/mysql-2016_3.sql
[mysql-2016_2.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2016.2/mysql-2016_2.sql
[mysql-4_6-to-2016_2.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2016.2/mysql-4_6-to-2016_2.sql
[mysql-4_6.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/4.6/mysql-4_6.sql
[mysql-4_5-to-4_6.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/4.6/mysql-4_5-to-4_6.sql
[mysql-4_5.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/4.5/mysql-4_5.sql
[mysql-4_4-to-4_5.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/4.5/mysql-4_4-to-4_5.sql
[mysql-4_4.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/4.4/mysql-4_4.sql
[mysql-4_3-to-4_4.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/4.4/mysql-4_3-to-4_4.sql
[mysql-4_3.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/4.3/mysql-4_3.sql

[oracle-2024_1.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2024.1/oracle-2024_1.sql
[oracle-2019_1.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2019.1/oracle-2019_1.sql
[oracle-2019_1-to-2024_1.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2024.1/oracle-2019_1-to-2024_1.sql 
[oracle-2018_2-to-2019_1.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2019.1/oracle-2018_2-to-2019_1.sql

[oracle-2018_2.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2018.2/oracle-2018_2.sql
[oracle-2017_2.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2017.2/oracle-2017_2.sql
[oracle-2017_1.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2017.1/oracle-2017_1.sql
[oracle-2016_3.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2016.3/oracle-2016_3.sql
[oracle-2016_2.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2016.2/oracle-2016_2.sql
[oracle-4_10.sql]:   https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/4.10/oracle-4_10.sql
[oracle-4_6.sql]:    https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/4.6/oracle-4_6.sql
[oracle-4_5.sql]:    https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/4.5/oracle-4_5.sql
[oracle-4_4.sql]:    https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/4.4/oracle-4_4.sql
[oracle-4_3.sql]:    https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/4.3/oracle-4_3.sql
[oracle-2017_2-to-2018_2.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2018.2/oracle-2017_2-to-2018_2.sql
[oracle-2017_1-to-2017_2.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2017.2/oracle-2017_1-to-2017_2.sql
[oracle-2016_3-to-2017_1.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2017.1/oracle-2016_3-to-2017_1.sql
[oracle-2016_2-to-2016_3.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2016.3/oracle-2016_2-to-2016_3.sql
[oracle-4_10-to-2016_2.sql]:   https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2016.2/oracle-4_10-to-2016_2.sql
[oracle-4_6-to-4_10.sql]:      https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/4.10/oracle-4_6-to-4_10.sql
[oracle-4_5-to-4_6.sql]:       https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/4.6/oracle-4_5-to-4_6.sql
[oracle-4_4-to-4_5.sql]:       https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/4.5/oracle-4_4-to-4_5.sql
[oracle-4_3-to-4_4.sql]:       https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/4.4/oracle-4_3-to-4_4.sql

[sqlserver-2024_1.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2024.1/sqlserver-2024_1.sql
[sqlserver-2023_1-to-2024_1.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2024.1/sqlserver-2023_1-to-2024_1.sql
[sqlserver-2023_1.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2023.1/sqlserver-2023_1.sql
[sqlserver-2019_1-to-2023_1.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2023.1/sqlserver-2019_1-to-2023_1.sql

[sqlserver-2017_2-to-2019_1.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2019.1/sqlserver-2017_2-to-2019_1.sql
[sqlserver-2019_1.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2019.1/sqlserver-2019_1.sql
[sqlserver-2016_3-to-2017_2.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2017.2/sqlserver-2016_3-to-2017_2.sql
[sqlserver-2017_2.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2017.2/sqlserver-2017_2.sql
[sqlserver-2016_2-to-2016_3.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2016.3/sqlserver-2016_2-to-2016_3.sql
[sqlserver-2016_3.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2016.3/sqlserver-2016_3.sql
[sqlserver-2016_2.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2016.2/sqlserver-2016_2.sql
[sqlserver-4_6-to-2016_2.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2016.2/sqlserver-4_6-to-2016_2.sql
[sqlserver-4_6.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/4.6/sqlserver-4_6.sql

[postgresql-2024_1.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2024.1/postgresql-2024_1.sql
[postgresql-2023_1-to-2024_1.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2024.1/postgresql-2023_1-to-2024_1.sql
[postgresql-2023_1.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2023.1/postgresql-2023_1.sql
[postgresql-2019_1.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2019.1/postgresql-2019_1.sql
[postgresql-2019_1-to-2023_1.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2023.1/postgresql-2019_1-to-2023_1.sql
[postgresql-2018_2-to-2019_1.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2019.1/postgresql-2018_2-to-2019_1.sql

[postgresql-2018_2.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2018.2/postgresql-2018_2.sql
[postgresql-2017_2.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2017.2/postgresql-2017_2.sql
[postgresql-2016_3.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2016.3/postgresql-2016_3.sql
[postgresql-2016_2.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2016.2/postgresql-2016_2.sql
[postgresql-4_8.sql]:    https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/4.8/postgresql-4_8.sql
[postgresql-2017_2-to-2018_2.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2018.2/postgresql-2017_2-to-2018_2.sql
[postgresql-2016_3-to-2017_2.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2017.2/postgresql-2016_3-to-2017_2.sql
[postgresql-2016_2-to-2016_3.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2016.3postgresql-2016_2-to-2016_3.sql
[postgresql-4_8-to-2016_2.sql]:    https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2016.2/postgresql-4_8-to-2016_2.sql

[db2-2019_1.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2019.1/db2-2019_1.sql
[db2-2017_2-to-2019_1.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2019.1/db2-2017_2-to-2019_1.sql

[db2-2016_3-to-2017_2.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2017.2/db2-2016_3-to-2017_2.sql
[db2-2017_2.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2017.2/db2-2017_2.sql
[db2-2016_2-to-2016_3.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2016.3/db2-2016_2-to-2016_3.sql
[db2-2016_3.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2016.3/db2-2016_3.sql
[db2-2016_2.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2016.2/db2-2016_2.sql
[db2-4_6-to-2016_2.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2016.2/db2-4_6-to-2016_2.sql
[db2-4_6.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/4.6/db2-4_6.sql
[db2-4_4-to-4_6.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/4.6/db2-4_4-to-4_6.sql
[db2-4_4.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/4.4/db2-4_4.sql
[db2-4_3-to-4_4.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/4.4/db2-4_3-to-4_4.sql
[db2-4_3.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/4.3/db2-4_3.sql