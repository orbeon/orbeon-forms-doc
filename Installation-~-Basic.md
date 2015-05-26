> [[Home]] ▸ [[Installation]]

## Software requirements

Orbeon Forms 4 runs on any platform that supports:

* Java 6, 7 (recommended), or 8
* a Servlet 2.5 container or greater (such as [Apache Tomcat][1] 6, 7 (recommended) or greater)

## Hardware requirements

Orbeon Forms is best installed on hardware with:

* a reasonably fast CPU, e.g. as of early 2011:
    * Intel Core i7 or better (desktop-grade)
    * Intel Xeon (server-grade)
    * As of 2015, we don't recommend AMD CPUs, which tend to be 2-4 times slower than Intel CPUs per core.
* at least 1.5 GB of available RAM

## Java virtual machine configuration

Configure the Java VM with:

* `-Xmx` option for dedicated Java heap memory:
    * on a development machine: at least 512 MB of Java heap: `-Xmx512m`
    * on a production machine: at least 1 GB of Java heap: `-Xmx1024m`
* ` -XX:MaxPermSize` for "permgen" space:
    * use at least: `-XX:MaxPermSize=256m`

## License installation (Orbeon Forms PE only)

* If you are running Orbeon Forms CE, you don't need to install a license file.
* If you are running Orbeon Forms PE:
    * complete the steps for your application server below
    * you can obtain a full licence from Orbeon, or get a [trial license](http://www.orbeon.com/orbeon/fr/orbeon/register/new)
    * before starting your servlet container, copy your license file under the Orbeon Forms WAR file as:
    ```
    WEB-INF/resources/config/license.xml
    ```

With Orbeon Forms 4.1 and newer, you can also place license.xml file under the user's home directory. For example, on Unix systems:

```
~/.orbeon/license.xml
```

Orbeon Forms first searches for the license file within the WAR, and if not found attempts to find it under the home directory.

The benefit of this approach is that you don't have to find where the WAR file is deployed in your container, or to uncompress and recompress the WAR file with the license.

_NOTE:  Orbeon Forms uses Java's `System.getProperty("user.home")` to identify the user's home directory.__  This corresponds to the user running the servlet container and not necessarily to the user of the developer or system administrator._

## Logging configuration

This step is optional.

Orbeon Forms has a logging configuration file under WEB-INF/resources/config/log4j.xml. By default, logging information is output to a file path relative to the directory where you start your application server.

```xml
<appender name="SingleFileAppender" class="org.apache.log4j.FileAppender">
    <param name="File" value="../logs/orbeon.log"/>
    <param name="Append" value="false" />
    <param name="Encoding" value="UTF-8"/>
    <layout class="org.apache.log4j.PatternLayout">
        <param name="ConversionPattern" value="%d{ISO8601} %-5p %c{1} %x - %m%n"/>
    </layout>
</appender>
```

You can change this by modifying the file parameter. Notes that on Windows, you must use forward slashes:

```xml
<appender name="SingleFileAppender" class="org.apache.log4j.FileAppender">
    <param name="File" value="C:/My Path/To/Logs/orbeon.log"/>
    <param name="Append" value="false" />
    <param name="Encoding" value="UTF-8"/>
    <layout class="org.apache.log4j.PatternLayout">
        <param name="ConversionPattern" value="%d{ISO8601} %-5p %c{1} %x - %m%n"/>
    </layout>
</appender>
```

The benefit of changing this configuration is that you know exactly where the file is stored. This can be really handy when trying to troubleshoot issues.

## Installing on your application server

### Apache Tomcat

See [[Tomcat Installation|Installation ~ Tomcat]]

### JBoss 7 and JBoss EAP 6 with Orbeon Forms 4

[SINCE 2012-07-13]

#### Deploy Orbeon Forms

This was tested with JBoss AS 7.1.1.Final "Brontes".

To install Orbeon Forms:

1. For Orbeon Forms PE
    * Unzip `orbeon.war`
place your `license.xml` file under `WEB-INF/resources/config/license.xml`
    * Re-zip `orbeon.war`
2. Start a standalone server with `bin/standalone.sh`
3. Drop `orbeon.war` into the JBoss `standalone/deployments` folder

#### Setup a JDBC datasource

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

#### Using the Oracle persistence layer

[SINCE Orbeon Forms 4.0 beta 2]

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

### JBoss 7 with Orbeon Forms 3.9.1

[SINCE 2012-07-13]

This was tested with JBoss AS 7.1.1.Final "Brontes".

Follow the following steps:

* unzip `orbeon.war`
* under `WEB-INF/lib`
    * remove `msv-xsdlib-20070407_orbeon_20100309.jar`
    * add `msv-xsdlib-20070407_orbeon_20120712.jar ([download][6])`
* in `WEB-INF/jboss-web.xml`
    * comment out the `<resource-ref>` entry (see details below older versions of JBoss)
* for Orbeon Forms PE
place your `license.xml` file under `WEB-INF/resources/config/license.xml`
* re-zip `orbeon.war`
* `start a standalone server with `bin/standalone.sh``
* drop the updated `orbeon.war` into the JBoss `standalone/deployments` folder

### With JBoss 6

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

### WebLogic

#### Oracle WebLogic 11g

A version of the ANTLR library that ships with WebLogic 11g conflicts with the version required by Orbeon Forms. To run Orbeon Forms on WebLogic 10/11g, you need to instruct WebLogic to let Orbeon Forms use the version of ANTLR that ships with Orbeon Forms. You can do this in the WebLogic EAR descriptor, which means you need to encapsulate Orbeon Forms in an EAR before you deploy it:

1. Create the following directory structure in a temporary directory:
    ```
    orbeon-ear
        META-INF
            application.xml
            weblogic-application.xml
        orbeon
    ```
    Populate `application.xml` with:
    ```xml
    <?xml version="1.0"?>
    <j2ee:application xmlns:j2ee="http://java.sun.com/xml/ns/j2ee">
        <j2ee:display-name>Orbeon Forms</j2ee:display-name>
        <j2ee:module>
            <j2ee:web>
                <j2ee:web-uri>orbeon</j2ee:web-uri>
                <j2ee:context-root>/orbeon</j2ee:context-root>
            </j2ee:web>
        </j2ee:module>
    </j2ee:application>
    ```
    Populate `weblogic-application.xml` with:
    ```xml
    <?xml version="1.0" encoding="UTF-8"?>
    <!DOCTYPE weblogic-application PUBLIC
        "-//BEA Systems, Inc.//DTD WebLogic Application 8.1.0//EN"
        "http://www.bea.com/servers/wls810/dtd/weblogic-application_2_0.dtd">
    <weblogic-application>
        <prefer-application-packages>
            <package-name>antlr.*</package-name>
            <package-name>org.apache.commons.lang.*</package-name>
            <package-name>org.apache.commons.fileupload.*</package-name>
            <package-name>org.apache.lucene.*</package-name>
        </prefer-application-packages>
    </weblogic-application>
    ```
2. Uncompress the `orbeon.war` into the `orbeon-ear/orbeon` directory you created in step 1. After this, you should have a directory `orbeon-ear/orbeon/WEB-INF`.
3. Deploy the `orbeon-ear` directory. If you are running WebLogic in development mode, you can move it to `user_projects/domains/base_domain/autodeploy`.
4. Optionally, you might want to change where the `orbeon.log` is stored. You define the location of the file in `WEB-INF/resources/config/log4j.xml`, in the `SingleFileAppender`. By default the location of the file is defined as `../logs/orbeon.log`. If you start WebLogic with `user_projects/domains/base_domain/startWebLogic.sh`, the log will be located in `user_projects/domains/logs/orbeon.log`.

### IBM WebSphere

#### IBM WebSphere 8.5

1. **Installing WebSphere** – Download [WebSphere 8.5 Liberty Profile][7]. Run the command line mentioned on that page, e.g. `java -jar wlp-developers-runtime-8.5.5.0.jar`. In what follows, we'll refer to the directory where you installed WebSphere Liberty Profile as `WLP`.
2. **Running WebSphere** – Run the server: `cd WLP/bin ; ./server run`. This will create the directory structure under `WLP/usr/servers/defaultServer`.
3. **Deploying Orbeon Forms** – In `WLP/usr/servers/defaultServer/apps` create a `war` directory, and inside it an `orbeon` directory. Uncompress the Orbeon Forms war into that `orbeon` directory. Open `WLP/usr/servers/defaultServer/server.xml` in an editor, inside the `<server>` root element, add the following two lines. The first lines declared the Orbeon Form app. The second disables automatic application redeployment when files in Orbeon Forms war file change. You need this as by default Form Runner uses the embedded eXist database, which writes to `WEB-INF/exist-data`, inside the Orbeon Forms `war`, which would trigger the app to restart as data is written to disk.
    ```xml
    <application id="orbeon" name="orbeon" location="war/orbeon" type="war"/>
    <applicationMonitor updateTrigger="disabled"/>
    ```
4. **Testing your installation** – Check the console: you should see the that Orbeon Forms was deployed successfully. Then access `http://localhost:9080/orbeon/home/`, and you should see the Orbeon Forms home page. If you start WebSphere from the WLP/bin directory, as mentioned on step 2, you will find the Orbeon Forms log file in `WLP/usr/servers/logs/orbeon.log`.

[SINCE Orbeon Forms 4.3] To setup a JDBC, for instance here for Oracle:

1. **Install the database driver** – Create a directory `WLS/usr/servers/defaultServer/lib`, and inside it place the database driver jar file, for instance `ojdbc6_g.jar`. On WebSphere, Orbeon Forms requires a JDBC 4 driver (e.g. for Oracle , use `ojdbc6_g.jar` or `ojdbc6.jar` but not `ojdbc5_g.jar` or `ojdbc5.jar`).
2. **Setup a datasource in WebSphere** – Open your `WLP/usr/servers/defaultServer/server.xml` in an editor. Your server.xml should look like the one below.  The `jdbc-4.0` feature in included, a top level library is declared pointing to the driver jar (in this case `ojdbc6_g.jar`), a datasource is defined, and the JNDI name set to `jdbc/oracle`, and both the data source and the application point to the same top level library, which is particularly important so WebSphere loads the driver classes with a single shared class loader.
    ```xml
    <server description="new server">

        <featureManager>
            <feature>jsp-2.2</feature>
            <feature>jdbc-4.0</feature>
        </featureManager>

        <httpEndpoint id="defaultHttpEndpoint"
                      host="localhost"
                      httpPort="9080"
                      httpsPort="9443" />

        <library id="oracle-lib">
            <fileset dir="lib" includes="ojdbc6_g.jar"/>
        </library>

        <dataSource id="oracle-ds" jndiName="jdbc/oracle" type="javax.sql.DataSource">
            <jdbcDriver libraryRef="oracle-lib" id="oracle-driver"/>
            <connectionManager numConnectionsPerThreadLocal="10" id="ConnectionManager" minPoolSize="1"/>
            <properties.oracle user="orbeon" password="password"
                               url="jdbc:oracle:thin:@//localhost:1521/orbeon"/>
        </dataSource>

        <applicationMonitor updateTrigger="disabled"/>

        <application id="orbeon" name="orbeon" location="war/orbeon" type="war">
            <classloader delegation="parentLast" commonLibraryRef="oracle-lib"/>
        </application>

    </server>
    ```
3. **Setup the datasource the Orbeon Forms web app** – In `WLP/usr/servers/defaultServer/apps/war/orbeon/WEB-INF` create a file `ibm-web-bnd.xml` with the following content:
    ```xml
        <web-bnd
            xmlns="http://websphere.ibm.com/xml/ns/javaee"
            xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
            xsi:schemaLocation="http://websphere.ibm.com/xml/ns/javaee
                            http://websphere.ibm.com/xml/ns/javaee/ibm-web-bnd_1_0.xsd"
            version="1.0">
        <virtual-host name="default_host"/>
        <resource-ref name="jdbc/oracle" binding-name="jdbc/oracle"/>
    </web-bnd>
    ```
    Then, edit the `web.xml` in the same directory, and uncomment the `<resource-ref>`, which should look as follows:
    ```xml
    <resource-ref>
        <description>DataSource</description>
        <res-ref-name>jdbc/oracle</res-ref-name>
        <res-type>javax.sql.DataSource</res-type>
        <res-auth>Container</res-auth>
    </resource-ref>
    ```
4. **Setup Form Runner** – If you're doing this setup so Form Runner (i.e. the forms you create with Form Builder) stores data in a relational database, then you also need to add the following property to instruct Form Runner to use the appropriate persistence implementation. The value of the property will typically be `oracle`, `mysql`, or `db2`.
    ```xml
    <property as="xs:string"  name="oxf.fr.persistence.provider.*.*.*" value="oracle"> `
    ```

### GlassFish 3.1

On Glassfish, you need to do the following setup to avoid a `java.security.UnrecoverableKeyException` with the message _Password must not be null_:

1. Edit your domain's `domain.xml` (e.g. in `domains/domain1/config/domain.xml`).
2. Search for the section of the file that contains `<jvm-options>` elements, and there, add: `<jvm-options>-Djavax.net.ssl.keyStorePassword=changeit</jvm-options>`. If you changed your Glassfish [master password][8], set this property to your new password.

##  Installation and runtime issues

### Session not found when running both Tomcat and WebLogic

This issue can also manifest itself with a dialog titled _Session has expired. Unable to process incoming request._ showing up as you try to interact with a form. This comes from the fact that Tomcat and WebLogic handle the `JSESSIONID` cookie used to track sessions differently:

* Tomcat creates one `JSESSIONID` per web application, with the cookie path set to the context of the application. When an application invalidates the session, Tomcat sends a new `JSESSIONID` to the browser.
* WebLogic stores one cookie `JSESSIONID` with cookie path `/` for all the applications. This cookie doesn't change when a session is invalidated, and hence there is no one-to-one mapping between a `JSESSIONID` cookie and a session in WebLogic.
The error can happen when:

1. You first access your application deployed on `/myapp` with Tomcat. Tomcat sets a `JSESSIONID` cookie for `/myapp`.
2. You then access your application on the same server deployed on `/myapp` with WebLogic. Tomcat sets a `JSESSIONID` cookie for `/`.
3. In subsequent requests, the browser sends the Tomcat `JSESSIONID` as it is more specific (for `/myapp` instead of just `/`), but WebLogic doesn't recognize it, hence the error you're getting.

The solution is simply to clear in your browser all the `JSESSIONID` cookies for the host you are trying to access.

[1]: http://tomcat.apache.org/
[2]: http://dev.mysql.com/downloads/connector/j/
[3]: https://code.google.com/p/adf-samples-demos/downloads/detail?name=demoscripts.zip&amp;can=2&amp;q=
[4]: http://wiki.orbeon.com/forms/doc/developer-guide/form-runner/oracle-and-mysql-persistence-layers#TOC-Oracle
[5]: http://wiki.orbeon.com/forms/doc/developer-guide/form-runner/oracle-and-mysql-persistence-layers#TOC-With-Orbeon-Forms-4.0
[6]: https://github.com/orbeon/orbeon-forms/blob/master/lib/src/msv-xsdlib-20070407_orbeon_20120712.jar?raw=true
[7]: https://www.ibmdw.net/wasdev/downloads/websphere-application-server-liberty-profile/
[8]: https://wikis.oracle.com/display/GlassFish/3.1+Master+Password