## JBoss 7 and JBoss EAP 6

### Status

These steps have been tested with JBoss AS 7.1.1.Final "Brontes".

### Deploy Orbeon Forms

To install Orbeon Forms:

1. For Orbeon Forms PE
    * Unzip `orbeon.war`
place your `license.xml` file under `WEB-INF/resources/config/license.xml`
    * Re-zip `orbeon.war`
2. Start a standalone server with `bin/standalone.sh`
3. Drop `orbeon.war` into the JBoss `standalone/deployments` folder

### Setup a JDBC datasource

To setup a datasource, if you'd like Orbeon Forms to connect to your relational database, here for MySQL:

1. Setup Orbeon Forms to use a JBoss datasource (configured in the following steps):``
    1. Unzip `orbeon.war`,
    2. Edit `WEB-INF/w``eb.xml` to uncomment the `<resource-ref>`.
    3. Edit `WEB-INF/jboss-web.xml` to uncomment the `<resource-ref>`. Change the `<jndi-name>` to `java:/comp/env/jdbc/mysql`.
2. In JBoss, install the JDBC driver as a module:
    1. In `modules/com`, create a directory `mysql/main`.
    2. [Download the MySQL JDBC driver][2], say `mysql-connector-java-5.1.22-bin.jar`, and place it in the `main` directory.
    3. In the `main` directory, create a file named `module.xml` with the following content. Update the value of the `path` attribute to match the name of the file you download in the previous step.
        ```xml
        <module xmlns="urn:jboss:module:1.0" name="com.mysql">
            <resources>
                <resource-root path="mysql-connector-java-5.1.22-bin.jar"/>
            </resources>
            <dependencies>
                <module name="javax.api"/>
            </dependencies>
        </module>
        ```
3. In JBoss, define the datasource:
    1. Editing `standalone/configuration/standalone.xml`, and replace the `<datasources>` with the following.
        ```xml
        <datasources>
            <datasource jndi-name="java:/comp/env/jdbc/mysql" pool-name="mysql" enabled="true">
                <connection-url>jdbc:mysql://localhost:3306/orbeon?useUnicode=true&amp;characterEncoding=UTF8</connection-url>
                <driver>com.mysql</driver>
                <transaction-isolation>TRANSACTION_READ_COMMITTED</transaction-isolation>
                <pool>
                    <min-pool-size>10</min-pool-size>
                    <max-pool-size>100</max-pool-size>
                    <prefill>true</prefill>
                </pool>
                <security>
                    <user-name>orbeon</user-name>
                    <password>orbeon</password>
                </security>
                <statement>
                    <prepared-statement-cache-size>32</prepared-statement-cache-size>
                    <share-prepared-statements>true</share-prepared-statements>
                </statement>
            </datasource>
            <drivers>
                <driver name="com.mysql" module="com.mysql">
                    <xa-datasource-class>com.mysql.jdbc.jdbc2.optional.MysqlXADataSource</xa-datasource-class>
                </driver>
            </drivers>
        </datasources>
        ```
    2. In `<connection-url>`, change the host and database name, and under `<security>` the user name and password as appropriate.
4. (Optional) Check that the module and datasource are configured properly:
    1. Starting JBoss (bin/standalone.sh) and verifying you see the following two lines in the console:
    ```
    [org.jboss.as.connector.subsystems.datasources] (ServerService Thread Pool -- 27) JBAS010404: Deploying non-JDBC-compliant driver class com.mysql.jdbc.Driver (version 5.1)
    [org.jboss.as.connector.subsystems.datasources] (MSC service thread 1-4) JBAS010400: Bound data source [java:/comp/env/jdbc/mysql]
    ```
    2. Check the datasource properly shows in the JBoss Management console:

        1. If you haven't done so already, create a user in the `ManagementRealm` by running `bin/add-user.sh`, and creating a new user, say `admin`/`password`.
        2. Go to `http://127.0.0.1:9990/console/`. Choose _Profile_ on to top right of the page. Click on _Connector / Datasources_ in the left sidebar. Check that you have an enabled datasource with JNDI name `java:/comp/env/jdbc/mysql`. In the _Connection_ tab, click on _Test Connection_, and a dialog the message "Successfully created JDBC connection" should show.
    3. (Requires Orbeon Forms PE) Check you can create a database service in Form Builder :
        1. Create a new form in Form Builder, give it any app/form name.
        2. In the sidebar, add a _Database service_. Assuming you have an `employee` table in your database, name the service `employees`, use `db` for the datasource, and `select emp_no, first_name from employees limit 10` for the SQL query. (If you need sample data to populate your database, you can use data from these [demo scripts][3].)
        3. Add a _Dropdown Menu_ control to the form (not _Dynamic Data Dropdown_), click on the gear icon, and name it `employees`.
        4. In the sidebar, add an _Action_, name it `populate-employees`, have it react to from load, calling the `employees` service, under _Set Response Selection Control Items_ (which is at the bottom of the dialog) click the plus icon, select the `employees` control, for _Items_ use `/response/row`, for _Label_ use `first-name`  and for _Value_ use `emp-no`.
        5. Click the _Test_ button, and check that the list name shows in the dropdown.

### Using the Oracle persistence layer

1. Follow the steps in the previous section, using the Oracle driver instead of the MySQL driver.
2. Add to the `orbeon.war`, in the `WEB-INF` directory, a file named `jboss-deployment-structure.xml` with the content that follows. This assumes, that in the previous step, you named the module `com.oracle`.
    ```xml
    <jboss-deployment-structure>
        <deployment>
            <dependencies>
                <module name="com.oracle"/>
                <module name="org.jboss.ironjacamar.jdbcadapters"/>
            </dependencies>
        </deployment>
    </jboss-deployment-structure>
    ```
Orbeon Forms doesn't come by default with this file, so deployment on JBoss doesn't fail for those who are not using the Oracle persistence layer, and thus haven't create a `com.oracle` module.
3. Create the [Orbeon tables][4] in Oracle.
4. Setup Orbeon Forms to [use the Oracle persistence layer][5]. At the minimun, you'll need to add the following two properties to your `properties-local.xml`:
    ```xml
    <property as="xs:string"  name="oxf.fr.persistence.provider.*.*.*"     value="oracle"/>
    <property as="xs:string"  name="oxf.fr.persistence.oracle.datasource"  value="db"/>
    ```

## With JBoss 6

1. Assuming that `JBOSS_HOME` represents the location of your JBoss installation: create a new `JBOSS_HOME/server/default/deploy/orbeon.war` directory.

2. Unzip the `orbeon.war` file in the `orbeon.war` directory you just created.

3. Depending on the version of JBoss you are using:
    * Create a file `orbeon.war/WEB-INF/jboss-scanning.xml` with the following content. This is get around a bug happening in the JBoss scanner when it goes through Scala classes. With Orbeon Forms 3.9:
        ```xml
        <scanning xmlns="urn:jboss:scanning:1.0">
            <path name="WEB-INF/lib/scala-library-2.9.2.jar">
                <exclude name="scala" recurse="true"/>
            </path>
        </scanning>
        ```
        Make sure you replace `scala-library-*.jar` with the actual version number in `WEB-INF/lib`.
        *NOTE: Orbeon Forms 4.0 already includes this setting.*

    * Edit `orbeon.war/WEB-INF/jboss-web.xml` and comment the 4 lines that start with `<resource-ref>` and end with `</resource-ref>`. This `resource-ref` is only useful if you want to setup Orbeon Forms to store data in a relational database. If this is the first time you are installing Orbeon Forms on JBoss, even if you ultimately want Orbeon Forms to store data in a relation database, we recommend you first get it up and running without this configuration. Once everything works, you can come back, uncomment this, and follow the steps in point #7 below to get Orbeon Forms to access your relational database.
4. Start JBoss by running `JBOSS_HOME/bin/run.bat` (or `run.sh` on UNIX).
5. Run and modify the example applications.
    1. Go to `http://localhost:8080/orbeon/`
    2. You can modify the example applications resources as the application sever is running and see the results of your modifications on the fly. The resources are stored under `JBOSS_HOME/server/default/deploy/orbeon.war/WEB-INF/resources`.
6. Optionally, to run the authentication sample:
    1. Open `JBOSS_HOME/server/default/deploy/orbeon.war/WEB-INF/web.xml` and uncomment the `security-constraint`, `login-config` and `security-role` declarations at the end of the file.
    2. Open `JBOSS_HOME/server/default/deploy/orbeon.war/WEB-INF/jboss-web.xml` and uncomment the security-domain element near the end of bottom of the file.
    3. Open `JBOSS_HOME/server/default/conf/login-config.xml` and add the following aplication policy to the list of policies:
    ```xml
    <application-policy name="orbeon-demo">
        <authentication>
            <login-module code="org.jboss.security.auth.spi.UsersRolesLoginModule" flag="required">
                <module-option name="usersProperties">jboss-orbeon-example-users.properties</module-option>
                <module-option name="rolesProperties">jboss-orbeon-example-roles.properties</module-option>
            </login-module>
        </authentication>
    </application-policy>
    ```
7. Optionally, you might want to setup a JDBC data source if your application is using the SQL Processor. What follows assumes you are configuring the SQL Processor with `<sql:datasource>my-datasource</sql:datasource>`.
    1. Look at the files `JBOSS_HOME/docs/examples/jca/*-ds.xml`. You should find one that correspond to the database you are using. Copy it to `JBOSS_HOME/server/default/deploy`.
    2. Edit the file you copied and change the parameters to match your database configuration. Also assign a JNDI name to this data source with: `<jndi-name>my-database</jndi-name>` (instead of `my-database` you might want to use a name which is descriptive of your database).
    3. Edit `WEB-INF/web.xml` and uncomment the `<resource-ref>`. Also change there the content of `<res-ref-name>` to match the name you are using in the SQL Processor prefixed with `jdbc/: <res-ref-name>jdbc/my-datasource</res-ref-name>`.
    4. Edit `WEB-INF/jboss-web.xml`. In that file you should have `<res-ref-name>jdbc/my-datasource</res-ref-name>` (the same name you use to configure the SQL Processor and that you have in the `web.xml`) and `<jndi-name>java:/my-database</jndi-name>` (the same name you declared in the `...-ds.xml` file).
    5. Copy the JAR files with the JDBC driver for your database in `JBOSS_HOME/server/default/lib`.

[2]: http://dev.mysql.com/downloads/connector/j/
[3]: https://code.google.com/p/adf-samples-demos/downloads/detail?name=demoscripts.zip&amp;can=2&amp;q=
[4]: http://wiki.orbeon.com/forms/doc/developer-guide/form-runner/oracle-and-mysql-persistence-layers#TOC-Oracle
[5]: http://wiki.orbeon.com/forms/doc/developer-guide/form-runner/oracle-and-mysql-persistence-layers#TOC-With-Orbeon-Forms-4.0