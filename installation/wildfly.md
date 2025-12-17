# WildFly

## Introduction

Installing Orbeon Forms on WildFly is only one of the possibilities. You can also install Orbeon Forms on other Servlet containers. You can also use Docker containers. See also:

* Docker
  * [Blog post](https://www.orbeon.com/2024/10/orbeon-forms-docker-images)
  * [Detailed documentation](docker.md)
* Servlet containers
  * [Tomcat](tomcat.md)
  * [WebLogic](weblogic.md)
  * [WebSphere](websphere.md)
  * [GlassFish](glassfish.md)

## Versions

WildFly was formerly known as JBoss.

The following instructions should work with recent versions of WildFly.

## Deploy Orbeon Forms

To install Orbeon Forms:

1. For Orbeon Forms PE only, either:
   * place your license file under `~/.orbeon/license.xml` (see [License installation](./#license-installation-orbeon-forms-pe-only)),
   * or add your `license.xml` to the `orbeon.war` under `WEB-INF/resources/config/license.xml`
2. Start a standalone server with `bin/standalone.sh`
3. Move the `orbeon.war` file into the WildFly `standalone/deployments` folder
4. Check whether the deployment was successful by watching `standalone/log/server.log`

## Creating an jboss-deployment-structure.xml

With Orbeon Forms 2018.2.2 and earlier, with some versions of WildFly, a `jboss-deployment-structure.xml` under the Orbeon Forms WAR's `WEB-INF` directory is needed:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<jboss-deployment-structure xmlns="urn:jboss:deployment-structure:1.1">
    <deployment>
        <dependencies>
            <system export="true">
                <paths>
                    <path name="org/w3c/dom/css"/>             
                </paths>
            </system>
        </dependencies>
    </deployment>
</jboss-deployment-structure>
```

Orbeon Forms 2018.2.3 and later, as well as Orbeon Forms 2019.1, already include this descriptor.

## Setup a JDBC datasource

To setup a datasource, if you'd like Orbeon Forms to connect to your relational database, do the following:

1. Setup Orbeon Forms to use a WildFly datasource (configured in the following steps):
   1. Set the `oxf.fr.persistence.provider.*.*.*` property in your `properties-local.xml`
      1.  If you already created a `WEB-INF/resources/config/properties-local.xml` unzip it and add the property per the example below. Otherwise create that file with the following content:

          ```xml
          <properties xmlns:xs="http://www.w3.org/2001/XMLSchema"
                      xmlns:oxf="http://www.orbeon.com/oxf/processors">
              <property as="xs:string"
                        name="oxf.fr.persistence.provider.*.*.*"
                        value="oracle"/>
          </properties>
          ```
      2. Change the value of the property according to the database you're using, setting it either to `oracle`, `mysql`, `sqlserver`, `postgresql`, or `db2`.
      3. Update `WEB-INF/resources/config/properties-local.xml` inside the `orbeon.war` with the version you edited.
   2. Update the `web.xml`
      1. Unzip the `WEB-INF/web.xml` inside the `orbeon.war`.
      2.  Editing `WEB-INF/web.xml`, towards the end of the file, uncomment the following:

          ```xml
          <resource-ref>
              <description>DataSource</description>
              <res-ref-name>jdbc/oracle</res-ref-name>
              <res-type>javax.sql.DataSource</res-type>
              <res-auth>Container</res-auth>
          </resource-ref>
          ```
      3. Inside `<resource-ref>`, replace `oracle` by the name of your database.
      4. Update `WEB-INF/web.xml` inside the `orbeon.war` with the version you edited.
   3. Update the `jboss-web.xml`
      1. Unzip the `WEB-INF/jboss-web.xml` inside the `orbeon.war`.
      2.  Editing `WEB-INF/jboss-web.xml`, uncomment the following:

          ```xml
          <resource-ref>
              <res-ref-name>jdbc/oracle</res-ref-name>
              <jndi-name>java:jboss/datasources/oracle</jndi-name>
          </resource-ref>
          ```
      3. Change the `<res-ref-name>` to match what the `<res-ref-name> in your` web.xml\`.
      4. In `<jndi-name>java:jboss/datasources/oracle</jndi-name>`, replace `oracle` by the database name you used in `<res-ref-name>`.
      5. Update `WEB-INF/jboss-web.xml` inside the `orbeon.war` with the version you edited.
2. In WildFly, install the JDBC driver:
   1. Download the MySQL JDBC driver, say `oracle-driver.jar`, and place it in the `standalone/deployments` directory.
   2. Start the server, and check you see the message `Deployed "oracle-driver.jar" (runtime-name : "oracle-driver.jar")`.
3. In WildFly, define the datasource:
   1.  Editing `standalone/configuration/standalone.xml`, inside the `<datasources>` add the following:

       ```xml
       <datasource jndi-name="java:jboss/datasources/oracle" pool-name="oracle" enabled="true">
           <connection-url>…</connection-url>
           <driver>…</driver>
           <security>
               <user-name>…</user-name>
               <password>…</password>
           </security>
       </datasource>
       ```
   2. In the `jndi-name` attribute, replace `oracle` by the name of your database. The value of this attribute must match the value you set earlier inside `<jndi-name>` when editing the `jboss-web.xml`.
   3. In `<connection-url>`, put the JDBC URL to your database.
   4. In `<driver>`, put the "runtime-name" of your driver as it shows in the log (it was `oracle-driver.jar` in our example above).
   5. In `<security>`, fill in the proper username and password.

Finally, you might want to double check the configuration you just did, ensuring names match across files, per the following diagram.

![Configuration files that need to be in sync](../.gitbook/assets/jboss.png)
