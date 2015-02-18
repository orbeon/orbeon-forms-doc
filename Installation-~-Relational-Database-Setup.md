> [[Home]] ▸ [[Installation]]

## Overview

The setup for the relational persistence layers is a 3 step process. The first two steps are database specific, so please refer to the relevant subsections below.

1. __Database setup__: You setup the database and create a schema with a few tables. This is typically be done by a DBA.
1. __Application server setup__: You configure your application server to use the database.
1. __Orbeon Forms setup__: You configure Orbeon Forms to use the relevant persistence layer.

Support for Oracle, SQL Server, and DB2 are [Orbeon Forms PE][1] features.

## Database setup

### Oracle   

1. Make sure that Oracle's Database Character Set is set to `AL32UTF8`, also as [recommended by Oracle][2].  You can see you database parameters by running the following query: `select * from nls_database_parameters`,  and the Database Character Set is identified by `nls_characterset`.
2. Create a user/schema in Oracle, for instance with the commands below. In this example "all privileges" are granted to the newly created user/schema, which is not strictly required. You might want to fine-tune permissions on your system as appropriate. If you had already created this schema and that the definition changed, or that you want to restart from scratch for some other reason, you can first delete the schema with all the data it contains with `drop user orbeon cascade`.

```sql
> sqlplus sys/password as sysdba
SQL> create user orbeon identified by password ;
SQL> grant all privileges to orbeon ;
```
3. Create the tables and indexes used by Orbeon Forms:

    - With Orbeon Forms 4.6, 4.7 and 4.8:
        - [DDL to create the tables from scratch](https://github.com/orbeon/orbeon-forms/blob/master/src/resources/apps/fr/persistence/relational/ddl/oracle-4_6.sql)
        - [DDL to upgrade your database created for Orbeon Forms 4.5](https://github.com/orbeon/orbeon-forms/blob/master/src/resources/apps/fr/persistence/relational/ddl/oracle-4_5-to-4_6.sql)
    - With Orbeon Forms 4.5:
        - [DDL to create the tables from scratch](https://github.com/orbeon/orbeon-forms/blob/master/src/resources/apps/fr/persistence/relational/ddl/oracle-4_5.sql)
        - [DDL to upgrade your database created for Orbeon Forms 4.4](https://github.com/orbeon/orbeon-forms/blob/master/src/resources/apps/fr/persistence/relational/ddl/oracle-4_4-to-4_5.sql)
    - With Orbeon Forms 4.4:
        - [DDL to create the tables from scratch](https://github.com/orbeon/orbeon-forms/blob/master/src/resources/apps/fr/persistence/relational/ddl/oracle-4_4.sql)
        - [DDL to upgrade your database created for Orbeon Forms 4.3 or earlier](https://github.com/orbeon/orbeon-forms/blob/master/src/resources/apps/fr/persistence/relational/ddl/oracle-4_3-to-4_4.sql)
    - With Orbeon Forms 4.3 or earlier:
        - [DDL to create the tables from scratch](https://github.com/orbeon/orbeon-forms/blob/master/src/resources/apps/fr/persistence/relational/ddl/oracle-4_3.sql)

With Oracle 11.2, `XMLType` values are stored by default using the binary XML storage. The binary XML storage has numerous benefits over the basic file storage. In many respect, it is the "proper" way to store XML. However, we found that Oracle fails to properly save some documents when the binary XML storage is used. In particular, when documents have attributes with long values (several thousands of characters), when retrieving the document, the value of some attributes is missing. For this reason, until this issue is solved by Oracle, we recommend you store `XMLType` values as "basic file", per the above DDL.

### MySQL

The MySQL persistence layer relies on [XML functions][3] that have been introduced in MySQL 5.1, so you need to be using the MySQL 5.1 (which was released in November 2008) or newer. However, we recommend you use MySQL 5.6.4 or newer, as it supports [storing fractional seconds][4].  

1. Create a new user `orbeon`. Orbeon Forms will connect to MySQL as that user.  

```sql
mysql -u root
mysql> CREATE USER orbeon IDENTIFIED BY ${PASSWORD};
```
2. Create a new schema `orbeon`. This schema will contains the tables used to store your forms definitions and form data.  

```sql
mysql> CREATE schema orbeon;
```
3. Create the tables used for Orbeon Forms in the `orbeon` schema:

    - With Orbeon Forms 4.6, 4.7 and 4.8:
        - [DDL to create the tables from scratch](https://github.com/orbeon/orbeon-forms/blob/master/src/resources/apps/fr/persistence/relational/ddl/mysql-4_6.sql)
        - [DDL to upgrade your database created for Orbeon Forms 4.5](https://github.com/orbeon/orbeon-forms/blob/master/src/resources/apps/fr/persistence/relational/ddl/mysql-4_5-to-4_6.sql)
    - With Orbeon Forms 4.5:
        - [DDL to create the tables from scratch](https://github.com/orbeon/orbeon-forms/blob/master/src/resources/apps/fr/persistence/relational/ddl/mysql-4_5.sql)
        - [DDL to upgrade your database created for Orbeon Forms 4.4](https://github.com/orbeon/orbeon-forms/blob/master/src/resources/apps/fr/persistence/relational/ddl/mysql-4_4-to-4_5.sql)
    - With Orbeon Forms 4.4:
        - [DDL to create the tables from scratch](https://github.com/orbeon/orbeon-forms/blob/master/src/resources/apps/fr/persistence/relational/ddl/mysql-4_4.sql)
        - [DDL to upgrade your database created for Orbeon Forms 4.3 or earlier](https://github.com/orbeon/orbeon-forms/blob/master/src/resources/apps/fr/persistence/relational/ddl/mysql-4_3-to-4_4.sql)
    - With Orbeon Forms 4.3 or earlier:
        - [DDL to create the tables from scratch](https://github.com/orbeon/orbeon-forms/blob/master/src/resources/apps/fr/persistence/relational/ddl/mysql-4_3.sql)

### SQL Server

[SINCE Orbeon Forms 4.6]

Create the tables used for Orbeon Forms in the `orbeon` schema:

- With Orbeon Forms 4.6, 4.7 and 4.8:
    - [DDL to create the tables from scratch](https://github.com/orbeon/orbeon-forms/blob/master/src/resources/apps/fr/persistence/relational/ddl/sqlserver-4_6.sql)

### PostgreSQL

[SINCE Orbeon Forms 4.8]

Create the tables used for Orbeon Forms in the `orbeon` schema:

- With Orbeon Forms 4.8:
    - [DDL to create the tables from scratch](https://github.com/orbeon/orbeon-forms/blob/master/src/resources/apps/fr/persistence/relational/ddl/postgresql-4_8.sql)

### DB2

[SINCE Orbeon Forms 4.3]

Create the tables used for Orbeon Forms in the `orbeon` schema:

- With Orbeon Forms 4.6, 4.7 and 4.8:
    - [DDL to create the tables from scratch](https://github.com/orbeon/orbeon-forms/blob/master/src/resources/apps/fr/persistence/relational/ddl/db2-4_6.sql)
    - [DDL to upgrade your database created for Orbeon Forms 4.4](https://github.com/orbeon/orbeon-forms/blob/master/src/resources/apps/fr/persistence/relational/ddl/db2-4_4-to-4_6.sql)
- With Orbeon Forms 4.4 or 4.5:
    - [DDL to create the tables from scratch](https://github.com/orbeon/orbeon-forms/blob/master/src/resources/apps/fr/persistence/relational/ddl/db2-4_4.sql)
    - [DDL to upgrade your database created for Orbeon Forms 4.3 or earlier](https://github.com/orbeon/orbeon-forms/blob/master/src/resources/apps/fr/persistence/relational/ddl/db2-4_3-to-4_4.sql)
- With Orbeon Forms 4.3 or earlier:
    - [DDL to create the tables from scratch](https://github.com/orbeon/orbeon-forms/blob/master/src/resources/apps/fr/persistence/relational/ddl/db2-4_3.sql)

## Application server setup

### Oracle   

#### General

Assuming:

- `${HOST}`: the host Oracle server is running on, for example `oracle.acme.com`
- ``${PORT}`: the port the Oracle server is running on, for example `1521`
- `${INSTANCE}`: the instance name, for example `orcl`
- `${USERNAME}`: the user/schema, for example `orbeon`
- `${PASSWORD}`: the password, for example `password`

#### Tomcat

Put the Oracle jar file that contains the JDBC driver (e.g. `ojdbc6_g.jar`, `xdb.jar`, and `xmlparserv2.jar`) in the appropriate directory for your application server (on Tomcat: `common/lib` or simply `lib`, depending on the version). If you don't already have it, you can download the Oracle JDBC driver from the Oracle site.

Setup a JDBC data source for your Oracle instance. With Tomcat, this is done in `server.xml`, where you define a `Resource` pointing to the your Oracle instance. In the example below, the Oracle server is running on `localhost`, the instance name is `globaldb`, and the user/schema is `orbeon` with password `orbeon`. Those values are highlighted in the configuration below, and you'll most likely want to change them to fit your setup.

```xml
<Resource
    name="jdbc/oracle"
    auth="Container"
    type="javax.sql.DataSource"
    initialSize="3"
    maxActive="10"
    maxIdle="20"
    maxWait="30000"
    driverClassName="oracle.jdbc.OracleDriver"
    poolPreparedStatements="true"
    validationQuery="select * from dual"
    testOnBorrow="true"
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

### MySQL

1. [Download the MySQL JDBC driver][6], called Connector/J, e.g. mysql-connector-java-5.1.29-bin.jar (latest version as of 2014-02-03)
2. Copy it in the appropriate directory for your application server (on Tomcat: `common/lib` or simply `lib`, depending on the version).
3. Setup a JDBC data source for your MySQL schema. With Tomcat, you can do this in `conf/server.xml`, where you define a `Resource` pointing to your MySQL database and schema. In the example below, the MySQL server is running on `localhost` port 3306, the schema is `orbeon`, the username/password is `orbeon`/`orbeon`. Those values are highlighted in the configuration below, and you'll most likely want to change them to fit your setup. Also, on the JDBC URL you're telling the MySQL driver to use Unicode and the UTF-8 encoding when talking to the database, which we highly recommend you to do in order to avoid encoding issues with non-ASCII characters.

    ```xml
    <Resource
        name="jdbc/mysql"
        auth="Container"
        type="javax.sql.DataSource"
        initialSize="3"
        maxActive="10"
        maxIdle="20"
        maxWait="30000"
        driverClassName="com.mysql.jdbc.Driver"
        poolPreparedStatements="true"
        validationQuery="select 1 from dual"
        testOnBorrow="true"
        username="orbeon"
        password="orbeon"
        url="jdbc:mysql://localhost:3306/orbeon?useUnicode=true&amp;characterEncoding=UTF8"/>
    ```

### SQL Server

[SINCE Orbeon Forms 4.6]

1. [Download the Microsoft JDBC driver for SQL Server][7]. 
2. Uncompress the zip file, and copy the `sqljdbc4.jar` it contains to the appropriate directory for your application server (on Tomcat: `common/lib` or simply `lib`, depending on the version).
3. Setup the JDBC data source for your DB2 instance. On Tomcat, you typically do this by editing Tomcat's `server.xml`, and within the `<context>` for Orbeon Forms adding a `<resource>` element similar to the one that follows.  

    ```xml
    <Resource
        name="jdbc/sqlserver"
        auth="Container"
        type="javax.sql.DataSource"
        initialSize="3"
        maxActive="10"
        maxIdle="20"
        maxWait="30000"
        driverClassName="com.microsoft.sqlserver.jdbc.SQLServerDriver"
        poolPreparedStatements="true"
        validationQuery="select 1"
        testOnBorrow="true"
        username="orbeon"
        password="orbeon"
        url="jdbc:sqlserver://server"/>
    ```

### PostgreSQL

[SINCE Orbeon Forms 4.8]

1. [Download the PostgreSQL JDBC driver][8].
2. Copy the driver jar to the appropriate directory for your application server (on Tomcat: `common/lib` or simply `lib`, depending on the version).
3. Setup the JDBC data source for your PostgreSQL instance. On Tomcat, you typically do this by editing Tomcat's `server.xml`, and within the `<context>` for Orbeon Forms adding a `<resource>` element similar to the one that follows.

    ```xml
    <Resource
        name="jdbc/postgresql"
        auth="Container"
        type="javax.sql.DataSource"
        initialSize="3"
        maxActive="10"
        maxIdle="20"
        maxWait="30000"
        driverClassName="org.postgresql.Driver"
        validationQuery="select 1"
        testOnBorrow="true"
        poolPreparedStatements="true"
        username="orbeon"
        password="orbeon"
        url="jdbc:postgresql://server:5432/?useUnicode=true&amp;characterEncoding=UTF8&amp;socketTimeout=30&amp;tcpKeepAlive=true"/>
    ```

### DB2

[SINCE Orbeon Forms 4.3]

1. [Download the DB2 JDBC driver][9] for the version of DB2 you're using.
2. Uncompress the zip file, and copy the `db2jcc4.jar` it contains to the appropriate directory for your application server (on Tomcat: `common/lib` or simply `lib`, depending on the version).
3. Setup the JDBC data source for your DB2 instance. On Tomcat, you typically do this by editing Tomcat's `server.xml`, and within the `<context>` for Orbeon Forms adding a `<resource>` element similar to the one that follows.

    ```xml
    <Resource
        name="jdbc/db2"
        auth="Container"
        type="javax.sql.DataSource"
        initialSize="3"
        maxActive="10"
        maxIdle="20"
        maxWait="30000"
        driverClassName="com.ibm.db2.jcc.DB2Driver"
        poolPreparedStatements="true"
        validationQuery="select 1 from sysibm.sysdummy1"
        testOnBorrow="true"
        username="db2inst1"
        password="password"
        url="jdbc:db2://localhost:50000/sample"/>
    ```

## Orbeon Forms setup

### With Orbeon Forms 3.9

See [legacy documentation](http://wiki.orbeon.com/forms/doc/developer-guide/form-runner/oracle-and-mysql-persistence-layers).

### With Orbeon Forms 4.0

#### With a single schema

In your `properties-local.xml`:  

1. Map an app, form, form type to the Oracle or MySQL persistence layer using the `oxf.fr.persistence.provider.*.*.*` [wildcard property][10], For instance, the following indicates that all the form definition and form data in the `acme` "app" are stored using the Oracle provider, use:

    ```xml
    <property as="xs:string" name="oxf.fr.persistence.provider.acme.*.*" value="oracle">
    ```

2. Set the value of the property `oxf.fr.persistence.oracle.datasource` for Oracle or `oxf.fr.persistence.mysql.datasource` for MySQL to match the name of the resource you setup in `server.xml`. For instance, if in `server.xml` the resource name is `jdbc/oracle`, then the property should be set to just `oracle`, as in:

    ```xml
    <property as="xs:string" name="oxf.fr.persistence.oracle.datasource" value="oracle">
    ```

#### With multiple schemas   

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
        value="fiance-datasource"/>
    ```
3. Still in `properties-local.xml`, you map the `hr` and `finance` app to the respective provider:

    ```xml
    <property as="xs:string" name="oxf.fr.persistence.provider.hr.*.*"      value="hr">
    <property as="xs:string" name="oxf.fr.persistence.provider.finance.*.*" value="finance">
    ```

## Flat view or table   

Orbeon Forms stores form data as XML in relational databases, which gives it a lot of flexibility. However, it might be harder for other tools to access this XML data. For this reason, you might want to provide other tools a way to access the XML data through another "flat" table or view that has one column per form field.

### Flat view support

See [[Flat View|Form-Runner-~-Persistence-~-Flat-View]].

### Manual relational table setup with MySQL

As of Orbeon Forms 4.8, Orbeon Forms doesn't provide a way to have a table or view automatically created for a form upon publishing in MySQL. However, you can do this manually. For instance, assume you want to create a "flat" `bookshelf` table for the sample [bookshelf form][11]. You want that table to have 3 columns:

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
        if new.deleted = 'N' then  
            insert into bookshelf set document_id = new.document_id,  
                title = extractValue(new.xml, '/book/details/title'),  
                author = extractValue(new.xml, '/book/details/author');  
        end if;  
    end if;  
end;  
|
```

Since you are interested in data for Bookshelf form, which is in the app `orbeon` form `bookshelf`, the trigger only does something if `new.app = 'orbeon' and new.form = 'bookshelf'`. To enable auditing, the MySQL persistence layer never deletes or updates data; it only inserts new row. So your trigger only needs to be concerned about updates. On insert, you want to make sure you are not creating duplicates in your `bookshelf` table, hence the `delete` statement. When a newly inserted row has `delete = 'N'`, this indicates that a user deleted that document, in which case you don't want to insert a row in your `bookshelf` table, hence the `if` test.

## Auditing and versioning

The relational persistence implementations never deletes old form definitions or form data:

- when a new form definition or form data is saved, it simply adds a new row to the table with a newer time stamp
- when a form definitions or form data is deleted, it marks the row as deleted but does not remove the row

Note however that, Orbeon Forms at this point doesn't provide a user interface for this feature.

[1]: http://www.orbeon.com/pricing
[2]: http://docs.oracle.com/cd/B19306_01/appdev.102/b14259/xdb03usg.htm#sthref263
[3]: http://dev.mysql.com/doc/refman/5.5/en/xml-functions.html
[4]: http://dev.mysql.com/doc/refman/5.6/en/fractional-seconds.html
[5]: http://docs.jboss.org/jbossas/docs/Installation_And_Getting_Started_Guide/5/html/Using_other_Databases.html#Configuring_a_datasource_for_Oracle_DB
[6]: http://dev.mysql.com/downloads/connector/j/
[7]: http://www.microsoft.com/en-us/download/details.aspx?id=11774
[8]: http://jdbc.postgresql.org/download.html
[9]: http://www-01.ibm.com/support/docview.wss?uid=swg21363866
[10]: http://wiki.orbeon.com/forms/doc/developer-guide/configuration-properties#TOC-Wildcards-in-properties
[11]: http://demo.orbeon.com/orbeon/fr/orbeon/bookshelf/summary