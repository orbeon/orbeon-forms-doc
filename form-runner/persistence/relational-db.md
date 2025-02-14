# Using Form Runner with a relational database

## Overview

Out-of-the-box, Orbeon Forms includes an SQLite embedded database with multiple demo forms. This setup is designed for a quick start, but for development or production use, you should configure Orbeon Forms to utilize a separate relational database.

The setup for the relational persistence layers is a 3-step process. The first two steps are database specific, so please refer to the relevant subsections below.

1. __Database setup__: You set up the database and create a schema with a few tables. This is typically be done by a DBA.
1. __Application server setup__: You configure your application server to use the database.
1. __Orbeon Forms setup__: You configure Orbeon Forms to use the relevant persistence layer.

Support for Oracle, SQL Server, and DB2 are [Orbeon Forms PE][1] features.

See also [Removing the built-in SQLite database](/configuration/advanced/production-war.md#removing-the-built-in-sqlite-database).

## Database setup

### Oracle database setup

1. Make sure that Oracle's Database Character Set is set to `AL32UTF8`, also as [recommended by Oracle][2].  You can see you database parameters by running the following query: `select * from nls_database_parameters`,  and the Database Character Set is identified by `nls_characterset`.
2. Create a user/schema in Oracle, for instance with the commands below. In this example "all privileges" are granted to the newly created user/schema, which is not strictly required. You might want to fine-tune permissions on your system as appropriate. If you had already created this schema and that the definition changed, or that you want to restart from scratch for some other reason, you can first delete the schema with all the data it contains with `drop user orbeon cascade`.

    ```sql
    > sqlplus sys/password as sysdba
    SQL> CREATE USER ORBEON IDENTIFIED BY password ;
    SQL> GRANT ALL PRIVILEGES TO orbeon ;
    SQL> GRANT UNLIMITED TABLESPACE TO orbeon ;
    ```
3. Run the following DDL to create or update your Orbeon database, and note that if upgrading to 2016.2, you need to [reindex your Orbeon database](/form-runner/feature/forms-admin-page.md#upgrading-to-20162).

| Orbeon Forms version(s) | DDL to create from scratch | DDL to upgrade from previous format |
|-------------------------|----------------------------|-------------------------------------|
| 2024.1 and newer        | [oracle-2024_1.sql]        | [oracle-2019_1-to-2024_1.sql]       |
| 2019.1 to 2023.1        | [oracle-2019_1.sql]        | [oracle-2018_2-to-2019_1.sql]       |
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

[oracle-2024_1.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2024.1/oracle-2024_1.sql
[oracle-2019_1.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2019.1/oracle-2019_1.sql
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
[oracle-2019_1-to-2024_1.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2024.1/oracle-2019_1-to-2024_1.sql 
[oracle-2018_2-to-2019_1.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2019.1/oracle-2018_2-to-2019_1.sql
[oracle-2017_2-to-2018_2.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2018.2/oracle-2017_2-to-2018_2.sql
[oracle-2017_1-to-2017_2.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2017.2/oracle-2017_1-to-2017_2.sql
[oracle-2016_3-to-2017_1.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2017.1/oracle-2016_3-to-2017_1.sql
[oracle-2016_2-to-2016_3.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2016.3/oracle-2016_2-to-2016_3.sql
[oracle-4_10-to-2016_2.sql]:   https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2016.2/oracle-4_10-to-2016_2.sql
[oracle-4_6-to-4_10.sql]:      https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/4.10/oracle-4_6-to-4_10.sql
[oracle-4_5-to-4_6.sql]:       https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/4.6/oracle-4_5-to-4_6.sql
[oracle-4_4-to-4_5.sql]:       https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/4.5/oracle-4_4-to-4_5.sql
[oracle-4_3-to-4_4.sql]:       https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/4.4/oracle-4_3-to-4_4.sql

#### Oracle binary XML storage

With Oracle 11.2, `XMLType` values are stored by default using the binary XML storage. The binary XML storage has numerous benefits over the basic file storage. In many respect, it is the "proper" way to store XML. However, we found that Oracle fails to properly save some documents when the binary XML storage is used. In particular, when documents have attributes with long values (several thousands of characters), when retrieving the document, the value of some attributes is missing. For this reason, until this issue is solved by Oracle, we recommend you store `XMLType` values as "basic file", per the above DDL.

### MySQL database setup

#### Supported MySQL versions

- [SINCE Orbeon Forms 2018.2]
	- __MySQL 5.7 and MySQL 8__: Since version 2018.2, Orbeon Forms uses the `utf8mb4` character set instead of the `utf8` character set. The reason being that MySQL's `utf8` character set can only store UTF-8-encoded symbols that consist of 1 to 3 bytes, that is characters in the Unicode [Basic Multilingual Plane](https://tinyurl.com/85a8weh), which means that none of the characters in the [Supplementary Multilingual Plane](https://tinyurl.com/yatmk5et), which include Emojis, could stored. However, the switch to the `utf8mb4` character set will prevent you from creating some indexes on MySQL 5.6 where default key prefix limit is 767 bytes. Hence we recommend you use MySQL 5.7, which raised the index key prefix length limit to 3072 bytes for InnoDB tables. If in your situation upgrading to MySQL 5.7 isn't an option, you can explore [enabling the `innodb_large_prefix` configuration option on your MySQL 5.6](https://dev.mysql.com/doc/refman/5.6/en/innodb-restrictions.html), or changing the DDL that ships with Orbeon Forms to use `utf8` instead of `utf8mb4`.
- [UP TO Orbeon Forms 2018.1]
	- __Minimum version__: The MySQL persistence layer relies on [XML functions][3] that have been introduced in MySQL 5.1, so you need to be using the MySQL 5.1 (which was released in November 2008) or newer.
	- __Recommended versions__: However, we recommend you use MySQL 5.6.4 or newer, as it supports [storing fractional seconds][4].
	- __MySQL 5.7__: With MySQL 5.7, since Orbeon Forms 2016.2, you must set the `sql_mode` to [`ALLOW_INVALID_DATES`](http://dev.mysql.com/doc/refman/5.7/en/sql-mode.html#sqlmode_allow_invalid_dates), or you might get errors while creating the database schema.

#### Unicode support

By default, the MySQL JDBC driver [uses](https://dev.mysql.com/doc/connector-j/5.1/en/connector-j-reference-charsets.html) the *character encoding* and *character collation* set on the server. [Up to MySQL 5.7](https://dev.mysql.com/doc/refman/5.7/en/charset-server.html) (included), the default character encoding is `latin1` and the default collation `latin1_swedish_ci`. [Since MySQL 8](https://dev.mysql.com/doc/refman/8.0/en/charset-server.html), the default character encoding is `utf8mb4` and the default collation `utf8mb4_0900_ai_ci`. So, if you're using MySQL 5.7 or earlier, you must specify the following 2 parameters when starting MySQL:

```
--character-set-server=utf8mb4
--collation-server=utf8mb4_0900_ai_ci
```

#### Setting up users and schema

1. Create a new user `orbeon`. Orbeon Forms will connect to MySQL as that user.

    ```sql
    mysql -u root
    mysql> CREATE USER 'orbeon'@'%' IDENTIFIED BY 'orbeon';
    ```
2. Create a new schema `orbeon`. This schema will contains the tables used to store your forms definitions and form data.

    ```sql
    mysql> CREATE schema orbeon;
    ```
3. If needed, grant permissions, for example:

   ```sql
   mysql> GRANT ALL PRIVILEGES ON *.* TO 'orbeon'@'%' WITH GRANT OPTION;
   ```
4. Run the following DDL to create or update your Orbeon database, and note that if upgrading to 2016.2, you need to [reindex your Orbeon database](/form-runner/feature/forms-admin-page.md#upgrading-to-20162).

| Orbeon Forms version(s) | DDL to create from scratch | DDL to upgrade from previous format |
|-------------------------|----------------------------|-------------------------------------|
| 2024.1 and newer        | [mysql-2024_1.sql]         | [mysql-2019_1-to-2024_1.sql]        |
| 2019.1 to 2023.1        | [mysql-2019_1.sql]         | [mysql-2018_2-to-2019_1.sql]        |
| 2018.2                  | [mysql-2018_2.sql]         | [mysql-2017_2-to-2018_2.sql]        |
| 2017.2, 2018.1          | [mysql-2017_2.sql]         | [mysql-2016_3-to-2017_2.sql]        |
| 2016.3, 2017.1          | [mysql-2016_3.sql]         | [mysql-2016_2-to-2016_3.sql]        |
| 2016.2                  | [mysql-2016_2.sql]         | [mysql-4_6-to-2016_2.sql]           |
| 4.6 to 4.10, 2016.2     | [mysql-4_6.sql]            | [mysql-4_5-to-4_6.sql]              |
| 4.5                     | [mysql-4_5.sql]            | [mysql-4_4-to-4_5.sql]              |
| 4.4                     | [mysql-4_4.sql]            | [mysql-4_3-to-4_4.sql]              |
| 4.3                     | [mysql-4_3.sql]            | -                                   |

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

### SQL Server database setup

[SINCE Orbeon Forms 4.6]

Orbeon Forms relies on SQL Server's full-text search, which is included out-of-the-box in all SQL Server editions, except the Express and Express with Tools. If you're using one of those two editions of SQL Server, you might want to look into getting Express with Advanced Services.

Run the following DDL to create or update your Orbeon database, and note that if upgrading to 2016.2, you need to [reindex your Orbeon database](/form-runner/feature/forms-admin-page.md#upgrading-to-20162).

| Orbeon Forms version(s) | DDL to create from scratch | DDL to upgrade from previous format |
|-------------------------|----------------------------|-------------------------------------|
| 2024.1 and newer        | [sqlserver-2024_1.sql]     | [sqlserver-2023_1-to-2024_1.sql]    |
| 2023.1                  | [sqlserver-2023_1.sql]     | [sqlserver-2019_1-to-2023_1.sql]    |
| 2019.1                  | [sqlserver-2019_1.sql]     | [sqlserver-2017_2-to-2019_1.sql]    |
| 2017.2                  | [sqlserver-2017_2.sql]     | [sqlserver-2016_3-to-2017_2.sql]    |
| 2016.3 to 2017.1        | [sqlserver-2016_3.sql]     | [sqlserver-2016_2-to-2016_3.sql]    |
| 2016.2                  | [sqlserver-2016_2.sql]     | [sqlserver-4_6-to-2016_2.sql]       |
| 4.6 to 2016.1           | [sqlserver-4_6.sql]        | -                                   |

#### Using `usql` to create an `orbeon` database

You can create an `orbeon` database and run the DDL to create the tables and indices used by Orbeon Forms using the command-line tool `usql`. Follow these steps, making sure to replace `PASSWORD` with the password for the `sa` user and `VERSION` with your desired DDL version.

- `usql mssql://sa:PASSWORD@localhost/`
- `CREATE DATABASE orbeon;`
- `\q`
- `usql mssql://sa:PASSWORD@localhost/orbeon`
- `\i sqlserver-VERSION.sql`

#### Verifying indexes

After upgrading your database, we recommend verifying that you have all the latest indexes. Use the following SQL query to get a list of indexes in your database. You can then compare that list to the indexes defined in the latest DDL linked above.

```sql
SELECT 
    t.name AS TableName,
    i.name AS IndexName,
    i.type_desc AS IndexType,
    STRING_AGG(c.name, ', ') WITHIN GROUP (ORDER BY ic.key_ordinal) AS IndexColumns
FROM 
    sys.tables t
    INNER JOIN sys.indexes i ON t.object_id = i.object_id
    INNER JOIN sys.index_columns ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id
    INNER JOIN sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
GROUP BY
    t.name, i.name, i.type_desc
ORDER BY 
    t.name, i.name;
```

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

### PostgreSQL database setup

[SINCE Orbeon Forms 4.8]

Run the following DDL to create or update your Orbeon database, and note that if upgrading to 2016.2, you need to [reindex your Orbeon database](/form-runner/feature/forms-admin-page.md#upgrading-to-20162).

| Orbeon Forms version(s) | DDL to create from scratch | DDL to upgrade from previous format |
|-------------------------|----------------------------|-------------------------------------|
| 2024.1 and newer        | [postgresql-2024_1.sql]    | [postgresql-2023_1-to-2024_1.sql]   |
| 2023.1                  | [postgresql-2023_1.sql]    | [postgresql-2019_1-to-2023_1.sql]   |
| 2019.1 to 2022.1        | [postgresql-2019_1.sql]    | [postgresql-2018_2-to-2019_1.sql]   |
| 2018.2                  | [postgresql-2018_2.sql]    | [postgresql-2017_2-to-2018_2.sql]   |
| 2017.2, 2018.1          | [postgresql-2017_2.sql]    | [postgresql-2016_3-to-2017_2.sql]   |
| 2016.3 to 2017.1        | [postgresql-2016_3.sql]    | [postgresql-2016_2-to-2016_3.sql]   |
| 2016.2                  | [postgresql-2016_2.sql]    | [postgresql-4_8-to-2016_2.sql]      |
| 4.8 to 2016.1           | [postgresql-4_8.sql]       | -                                   |

[postgresql-2024_1.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2024.1/postgresql-2024_1.sql
[postgresql-2023_1-to-2024_1.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2024.1/postgresql-2023_1-to-2024_1.sql
[postgresql-2023_1.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2023.1/postgresql-2023_1.sql
[postgresql-2019_1.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2019.1/postgresql-2019_1.sql
[postgresql-2018_2.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2018.2/postgresql-2018_2.sql
[postgresql-2017_2.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2017.2/postgresql-2017_2.sql
[postgresql-2016_3.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2016.3/postgresql-2016_3.sql
[postgresql-2016_2.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2016.2/postgresql-2016_2.sql
[postgresql-4_8.sql]:    https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/4.8/postgresql-4_8.sql
[postgresql-2019_1-to-2023_1.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2023.1/postgresql-2019_1-to-2023_1.sql
[postgresql-2018_2-to-2019_1.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2019.1/postgresql-2018_2-to-2019_1.sql
[postgresql-2017_2-to-2018_2.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2018.2/postgresql-2017_2-to-2018_2.sql
[postgresql-2016_3-to-2017_2.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2017.2/postgresql-2016_3-to-2017_2.sql
[postgresql-2016_2-to-2016_3.sql]: https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2016.3postgresql-2016_2-to-2016_3.sql
[postgresql-4_8-to-2016_2.sql]:    https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/2016.2/postgresql-4_8-to-2016_2.sql

### DB2 database setup

[SINCE Orbeon Forms 4.3]

Run the following DDL to create or update your Orbeon database, and note that if upgrading to 2016.2, you need to [reindex your Orbeon database](/form-runner/feature/forms-admin-page.md#upgrading-to-20162).


| Orbeon Forms version(s)  | DDL to create from scratch | DDL to upgrade from previous format |
|--------------------------|----------------------------|-------------------------------------|
| 2019.1  and newer        | [db2-2019_1.sql]           | [db2-2017_2-to-2019_1.sql]          |
| 2017.2                   | [db2-2017_2.sql]           | [db2-2016_3-to-2017_2.sql]          |
| 2016.3 to 2017.1         | [db2-2016_3.sql]           | [db2-2016_2-to-2016_3.sql]          |
| 2016.2                   | [db2-2016_2.sql]           | [db2-4_6-to-2016_2.sql]             |
| 4.6 to 2016.1            | [db2-4_6.sql]              | [db2-4_4-to-4_6.sql]                |
| 4.4                      | [db2-4_4.sql]              | [db2-4_3-to-4_4.sql]                |
| 4.3                      | [db2-4_3.sql]              | -                                   |

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

## Application server setup

### Tomcat datasource configuration

When using Tomcat, you setup a JDBC data source for your database instance either:

- in `server.xml` (not recommended by the Tomcat documentation because it is less flexible)
- or in a separate context XML file (such as `orbeon.xml`) for the web app (recommended).

In both cases, you define a `<Resource>` element containing several configuration attributes. We provide examples below for all the databases covered.

Here is a typical example:

```xml
<Resource
    name="jdbc/mysql"
    driverClassName="com.mysql.jdbc.Driver"

    auth="Container"
    type="javax.sql.DataSource"

    initialSize="3"
    maxActive="10"
    maxIdle="10"
    maxWait="30000"

    poolPreparedStatements="true"

    testOnBorrow="true"
    validationQuery="select 1"

    username="orbeon"
    password="orbeon"
    url="jdbc:mysql://localhost:3306/orbeon"/>
```

If you create a separate context file:

1. Give it the name of your web app, for example `orbeon.xml`.
2. Make sure the context is placed in the appropriate Tomcat folder.

Here is an example of a context file. Note the enclosing `<Context>` element:

```xml
<Context path="/orbeon">
    <Resource
        name="jdbc/mysql"
        driverClassName="com.mysql.jdbc.Driver"
    
        auth="Container"
        type="javax.sql.DataSource"
    
        initialSize="3"
        maxActive="10"
        maxIdle="10"
        maxWait="30000"
    
        poolPreparedStatements="true"
    
        testOnBorrow="true"
        validationQuery="select 1"
    
        username="orbeon"
        password="orbeon"
        url="jdbc:mysql://localhost:3306/orbeon"/>

</Context>
```  

See also the following external links:

- Tomcat documentation: [The Tomcat JDBC Connection Pool](https://tomcat.apache.org/tomcat-9.0-doc/jdbc-pool.html)
- Apache Commons documentation: [BasicDataSource Configuration Parameters](http://commons.apache.org/proper/commons-dbcp/configuration.html)
- Blog post: [Configuring jdbc-pool for high-concurrency](http://www.tomcatexpert.com/blog/2010/04/01/configuring-jdbc-pool-high-concurrency)

### Oracle application server setup

#### General

Assuming:

- `${HOST}`: the host Oracle server is running on, for example `oracle.acme.com`
- `${PORT}`: the port the Oracle server is running on, for example `1521`
- `${INSTANCE}`: the instance name, for example `orcl`
- `${USERNAME}`: the user/schema, for example `orbeon`
- `${PASSWORD}`: the password, for example `password`

#### Tomcat

Put the Oracle jar file that contains the JDBC driver (e.g. `ojdbc6_g.jar`, `xdb.jar`, and `xmlparserv2.jar`) in the appropriate directory for your application server (on Tomcat: `common/lib` or simply `lib`, depending on the version). If you don't already have it, you can download the Oracle JDBC driver from the Oracle site.

Your `Resource` element pointing to the your Oracle instance (see also [Tomcat datasource configuration](#tomcat-datasource-configuration) above). In the example below, the Oracle server is running on `localhost`, the instance name is `globaldb`, and the user/schema is `orbeon` with password `orbeon`. Those values are highlighted in the configuration below, and you'll most likely want to change them to fit your setup.

```xml
<Resource
    name="jdbc/oracle"
    driverClassName="oracle.jdbc.OracleDriver"

    auth="Container"
    type="javax.sql.DataSource"

    initialSize="3"
    maxActive="10"
    maxIdle="10"
    maxWait="30000"

    poolPreparedStatements="true"

    testOnBorrow="true"
    validationQuery="select * from dual"

    username="orbeon"
    password="orbeon"
    url="jdbc:oracle:thin:@//localhost:1521/globaldb"/>
```

#### JBoss 5.0.1 / JBoss EAP 5.0.1

1. Please follow the [JBoss documentation first][5], but here are some steps that work for us in our test environment.
2. Place `ojdbc5_g.jar` into `server/default/lib/`.
3. Create an Oracle datasource as `server/default/deploy/oracle-ds.xml`, for example:

    ```xml
    <datasources>
        <local-tx-datasource>
            <jndi-name>OracleDS</jndi-name>
            <connection-url>jdbc:oracle:thin:@//${HOST}:${PORT}/${INSTANCE}</connection-url>
            <driver-class>oracle.jdbc.driver.OracleDriver</driver-class>
            <user-name>${USERNAME}</user-name>
            <password>${PASSWORD}</password>
            <valid-connection-checker-class-name>org.jboss.resource.adapter.jdbc.vendor.OracleValidConnectionChecker</valid-connection-checker-class-name>
            <metadata>
                <type-mapping>Oracle9i</type-mapping>
            </metadata>
        </local-tx-datasource>
    </datasources>
    ```
4. Update `WEB-INF/jboss-web.xml` to:

    ```xml
    <jboss-web>
        <resource-ref>
            <res-ref-name>jdbc/oracle</res-ref-name>
            <jndi-name>java:/OracleDS</jndi-name>
        </resource-ref>
    </jboss-web>
    ```

### MySQL application server setup

1. [Download the MySQL JDBC driver][6], called Connector/J, e.g. `mysql-connector-java-5.1.39-bin.jar` (latest version as of 2016-06-20)
2. Copy it in the appropriate directory for your application server (on Tomcat: `common/lib` or simply `lib`, depending on the version).
3. Setup a JDBC data source for your MySQL schema (see also [Tomcat datasource configuration](#tomcat-datasource-configuration) above). In the example below, the MySQL server is running on `localhost` port 3306, the schema is `orbeon`, the username/password is `orbeon`/`orbeon`. Those values are highlighted in the configuration below, and you'll most likely want to change them to fit your setup. Also, on the JDBC URL you're telling the MySQL driver to use Unicode and the UTF-8 encoding when talking to the database, which we highly recommend you to do in order to avoid encoding issues with non-ASCII characters.

    ```xml
    <Resource
        name="jdbc/mysql"
        driverClassName="com.mysql.jdbc.Driver"

        auth="Container"
        type="javax.sql.DataSource"

        initialSize="3"
        maxActive="10"
        maxIdle="10"
        maxWait="30000"

        poolPreparedStatements="true"

        testOnBorrow="true"
        validationQuery="select 1"

        username="orbeon"
        password="orbeon"
        url="jdbc:mysql://localhost:3306/orbeon?useUnicode=true&amp;characterEncoding=UTF8"/>
    ```

### SQL Server application server setup

[SINCE Orbeon Forms 4.6]

1. [Download the Microsoft JDBC driver for SQL Server](https://docs.microsoft.com/en-us/sql/connect/jdbc/download-microsoft-jdbc-driver-for-sql-server?view=sql-server-ver15) (as of 2020-05-04, this is version 8.2 of the driver)
2. Uncompress the zip file, and copy the `sqljdbc4.jar` it contains to the appropriate directory for your application server (on Tomcat: `common/lib` or simply `lib` with newer Tomcat version).
3. When using Java 11 or newer, you might need to add the JAXB API, which was present in earlier versions of Java. Download the JAR file from [Maven](https://repo1.maven.org/maven2/javax/xml/bind/jaxb-api/2.3.0/jaxb-api-2.3.0.jar) and place it in the same directory you placed the JDBC driver.
4. Set up the JDBC data source for your SQL Server instance (see also [Tomcat datasource configuration](#tomcat-datasource-configuration) above). Example:

    ```xml
    <Resource
        name="jdbc/sqlserver"
        driverClassName="com.microsoft.sqlserver.jdbc.SQLServerDriver"

        auth="Container"
        type="javax.sql.DataSource"

        initialSize="3"
        maxActive="10"
        maxIdle="10"
        maxWait="30000"

        poolPreparedStatements="true"

        validationQuery="select 1"
        testOnBorrow="true"

        username="orbeon"
        password="orbeon"
        url="jdbc:sqlserver://server;databaseName=orbeon;trustServerCertificate=true"/>
    ```

### PostgreSQL application server setup

[SINCE Orbeon Forms 4.8]

1. [Download the PostgreSQL JDBC driver][8].
2. Copy the driver jar to the appropriate directory for your application server (on Tomcat: `common/lib` or simply `lib`, depending on the version).
3. Setup the JDBC data source for your PostgreSQL instance (see also [Tomcat datasource configuration](#tomcat-datasource-configuration) above). Example:

    ```xml
    <Resource
        name="jdbc/postgresql"
        driverClassName="org.postgresql.Driver"

        auth="Container"
        type="javax.sql.DataSource"

        initialSize="3"
        maxActive="10"
        maxIdle="10"
        maxWait="30000"

        poolPreparedStatements="true"

        validationQuery="select 1"
        testOnBorrow="true"

        username="orbeon"
        password="orbeon"
        url="jdbc:postgresql://server:5432/database?useUnicode=true&amp;characterEncoding=UTF8&amp;socketTimeout=30&amp;tcpKeepAlive=true"/>
    ```

    The following attributes of the datasource need to be configured as needed:

    - `username`
    - `password`
    - `url`: including the `server` and `database` parts of the path

### DB2 application server setup

[SINCE Orbeon Forms 4.3]

1. [Download the DB2 JDBC driver][9] for the version of DB2 you're using.
2. Uncompress the zip file, and copy the `db2jcc4.jar` it contains to the appropriate directory for your application server (on Tomcat: `common/lib` or simply `lib`, depending on the version).
3. Setup the JDBC data source for your DB2 instance (see also [Tomcat datasource configuration](#tomcat-datasource-configuration) above). Example:

    ```xml
    <Resource
        name="jdbc/db2"
        driverClassName="com.ibm.db2.jcc.DB2Driver"

        auth="Container"
        type="javax.sql.DataSource"

        initialSize="3"
        maxActive="10"
        maxIdle="10"
        maxWait="30000"

        poolPreparedStatements="true"

        validationQuery="select 1 from sysibm.sysdummy1"
        testOnBorrow="true"

        username="db2inst1"
        password="password"
        url="jdbc:db2://localhost:50000/sample"/>
    ```

## Orbeon Forms setup

What follows applies to Orbeon Forms 4.0 and newer. For Orbeon Forms 3.9, see this [legacy documentation](http://wiki.orbeon.com/forms/doc/developer-guide/form-runner/oracle-and-mysql-persistence-layers).

### Disabling the embedded SQLite provider

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

Orbeon Forms comes with an embedded SQLite database in order to show demo forms, and also to allow to quickly get started with Orbeon Forms. When configuring your own provider or providers, you should disable the embedded SQLite database. To do so, add the following properties to your `properties-local.xml`, assuming here that your new persistence provider is called `postgresql`:

```xml
<property
    as="xs:boolean"
    name="oxf.fr.persistence.sqlite.active"
    value="false"/>
<property 
    as="xs:string"  
    name="oxf.fr.persistence.provider.orbeon.*.form"                     
    value="postgresql"/>
<property 
    as="xs:string"  
    name="oxf.fr.persistence.provider.orbeon-features.*.form"                     
    value="postgresql"/>
```

### With a single schema

In your `properties-local.xml`, you map an app / form / form type to the implementation of the persistence API you're using with the `oxf.fr.persistence.provider.*.*.*` [wildcard property](../../configuration/properties/README.md). For instance, if using PostgreSQL, set the property to:

```xml
<property
    as="xs:string"
    name="oxf.fr.persistence.provider.*.*.*"
    value="postgresql"/>
```

### With multiple schemas

The single schema configuration described in the previous section uses the predefined `oracle` and `mysql` providers. To use multiple schemas you need to define you own provider names. For instance, assume that you have two apps, `hr` and `finance`, and would like both the form definition and data for those apps to be stored in two separate schemas:

1. In your application server configuration, you setup two data sources ; let's call them `hr-datasource` and `finance-datasource`.
2. In `properties-local.xml`, you use the following properties to define two providers `hr` and `finance` that you configure to use the desired persistence layer implementation (Oracle in this example) and data source:

    ```xml
    <!-- HR provider -->
    <property
        as="xs:anyURI"
        name="oxf.fr.persistence.hr.uri"
        value="/fr/service/oracle"/>
    <property
        as="xs:string"
        name="oxf.fr.persistence.hr.datasource"
        value="hr-datasource"/>

    <!-- Finance provider -->
    <property
        as="xs:anyURI"
        name="oxf.fr.persistence.finance.uri"
        value="/fr/service/oracle"/>
    <property
        as="xs:string"
        name="oxf.fr.persistence.finance.datasource"
        value="finance-datasource"/>
    ```
3. Still in `properties-local.xml`, you map the `hr` and `finance` app to the respective provider:

    ```xml
    <property
        as="xs:string"
        name="oxf.fr.persistence.provider.hr.*.*"
        value="hr"/>
    <property
        as="xs:string"
        name="oxf.fr.persistence.provider.finance.*.*"
        value="finance"/>
    ```

## Flat view or table

Orbeon Forms stores form data as XML in relational databases, which gives it a lot of flexibility. However, it might be harder for other tools to access this XML data. For this reason, you might want to provide other tools a way to access the XML data through another "flat" table or view that has one column per form field.

### Flat view support

See [Flat View](/form-runner/persistence/flat-view.md).

### Manual relational table setup with MySQL

Orbeon Forms doesn't provide a way to have a table or view automatically created for a form upon publishing in MySQL. However, you can do this manually. For instance, assume you want to create a "flat" `bookshelf` table for the sample [bookshelf form][11]. You want that table to have 3 columns:

- `title` corresponds to the title form field;
- `author` corresponds to the author form field;
- `document_id` corresponds to the column with the same name in `orbeon_form_data`.

Start by creating the `bookshelf` table:

```sql
create table bookshelf (
    document_id varchar(255),
    title  text,
    author text
);
```

Choose an appropriate type for your columns, depending on the maximum length for the fields. Then create a trigger, which will update your `bookshelf` table when form data is saved in `orbeon_form_data`:

```sql
delimiter |
create trigger bookshelf_trigger before insert on orbeon_form_data for each row begin
    if new.app = 'orbeon' and new.form = 'bookshelf' then
        delete from bookshelf where document_id = new.document_id;
        if new.deleted = 'N' and new.draft = 'N' then
            insert into bookshelf set document_id = new.document_id,
                title = extractValue(new.xml, '/form/details/title'),
                author = extractValue(new.xml, '/form/details/author');
        end if;
    end if;
end;
|
```

Since you are interested in data for Bookshelf form, which is in the app `orbeon` form `bookshelf`, the trigger only does something if `new.app = 'orbeon' and new.form = 'bookshelf'`. To enable auditing, the MySQL persistence layer never deletes or updates data; it only inserts new row. So your trigger only needs to be concerned about updates. On insert, you want to make sure you are not creating duplicates in your `bookshelf` table, hence the `delete` statement. When a newly inserted row has `delete = 'N'`, this indicates that a user deleted that document, in which case you don't want to insert a row in your `bookshelf` table, hence the `if` test.

## Auditing

See [Auditing](form-runner/persistence/auditing.md).

## See also

- [Database support](db-support.md)
- [Auditing](auditing.md)
- [Revision history](/form-runner/feature/revision-history.md)
- [Purging historical data](/form-runner/feature/purging-historical-data.md)
- [Purging old data using SQL](/form-runner/persistence/purging-old-data.md)
- [Relational Database Logging](/configuration/troubleshooting/database-logging.md)
- [Removing the built-in SQLite database](/configuration/advanced/production-war.md#removing-the-built-in-sqlite-database)

[1]: https://www.orbeon.com/pricing
[2]: http://docs.oracle.com/cd/B19306_01/appdev.102/b14259/xdb03usg.htm#sthref263
[3]: http://dev.mysql.com/doc/refman/5.5/en/xml-functions.html
[4]: http://dev.mysql.com/doc/refman/5.6/en/fractional-seconds.html
[5]: http://docs.jboss.org/jbossas/docs/Installation_And_Getting_Started_Guide/5/html/Using_other_Databases.html#Configuring_a_datasource_for_Oracle_DB
[6]: http://dev.mysql.com/downloads/connector/j/
[8]: https://jdbc.postgresql.org/download/
[9]: http://www-01.ibm.com/support/docview.wss?uid=swg21363866
[11]: http://demo.orbeon.com/orbeon/fr/orbeon/bookshelf/summary
