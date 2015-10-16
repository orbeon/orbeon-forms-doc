## IBM WebSphere 8.5

1. **Installing WebSphere** – Download [WebSphere 8.5 Liberty Profile](https://developer.ibm.com/wasdev/downloads/liberty-profile-using-non-eclipse-environments/). Run the command line mentioned on that page, e.g. `java -jar wlp-developers-runtime-8.5.5.0.jar`. In what follows, we'll refer to the directory where you installed WebSphere Liberty Profile as `WLP`.
2. **Running WebSphere** – Run the server: `cd WLP/bin ; ./server run`. This will create the directory structure under `WLP/usr/servers/defaultServer`.
3. **Deploying Orbeon Forms** – In `WLP/usr/servers/defaultServer/apps` create a `war` directory, and inside it an `orbeon` directory. Uncompress the Orbeon Forms war into that `orbeon` directory. Open `WLP/usr/servers/defaultServer/server.xml` in an editor, inside the `<server>` root element, add the following two lines. The first lines declared the Orbeon Form app. The second disables automatic application redeployment when files in Orbeon Forms war file change. You need this as by default Form Runner uses the embedded eXist database, which writes to `WEB-INF/exist-data`, inside the Orbeon Forms `war`, which would trigger the app to restart as data is written to disk.

    ```xml
    <application id="orbeon" name="orbeon" location="war/orbeon" type="war"/>
    <applicationMonitor updateTrigger="disabled"/>
    ```
4. **Testing your installation** – Check the console: you should see the that Orbeon Forms was deployed successfully. Then access `http://localhost:9080/orbeon/home/`, and you should see the Orbeon Forms home page. If you start WebSphere from the WLP/bin directory, as mentioned on step 2, you will find the Orbeon Forms log file in `WLP/usr/servers/logs/orbeon.log`.

[SINCE Orbeon Forms 4.3]

To setup a JDBC, for instance here for Oracle:

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
