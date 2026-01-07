# Installation

## Software requirements

### Basic requirements

Orbeon Forms runs on any platform that supports:

* A Java runtime environment (JRE)
* a Servlet 2.5 (or greater) container such as [Apache Tomcat](http://tomcat.apache.org/). For versions supported, see [Tomcat](tomcat.md).

### Java versions

| Orbeon Forms Version | Java Versions | Comment         |
|----------------------|---------------|-----------------|
| 2025.1               | 11, 17, 21    |                 |
| 2024.1               | 11, 17, 21    |                 |
| 2023.1               | 11, 17, 21    |                 |
| 2022.1               | 11, 17        |                 |
| 2021.1               | 11            |                 |
| 2020.1               | 8, 11         |                 |
| 2019.2               | 8, 10, 11     |                 |
| 2019.1               | 8, 10, 11     |                 |
| 2018.2               | 8, 10         | recommended     |
| 2018.2               | 7             | not recommended |
| Up to 2018.1         | 8             | recommended     |
| Up to 2018.1         | 7             | not recommended |

### Tomcat versions

See [Tomcat](tomcat.md).    

### Java Servlet and Jakarta Servlet APIs

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

Orbeon Forms supports both the Java Servlet and Jakarta Servlet APIs, without any extra configuration, which means that the same `orbeon.war` file can be deployed in Tomcat 9, Tomcat 10, WildFly 26, or WildFly 27+, for example.

To support both of those APIs, the various Orbeon servlet, filters, and listeners are now dynamically instantiated instead of being referenced in `web.xml`. If you need to disable that dynamic instantiation mechanism, you can remove the `servlet-container-initializer.jar` file from the `WEB-INF/lib` directory of the `orbeon.war` file.

## Hardware requirements

We recommend you run Orbeon Forms on a dedicated server or instance that satisfies the following requirements:

- CPU: recent 4-core, or more, Intel Xeon or Core i7 or newer. We don't recommend AMD CPUs prior to the [Ryzen](https://www.amd.com/en/ryzen) line.
- RAM: 4 GB of RAM, or more, available to the Java Virtual Machine (JVM heap size).

If using AWS EC2, we recommend you start with a c4.2xlarge instance. For most projects, it is safe to start with a configuration along those lines, but you might want to have more powerful or multiple servers or instances (or equivalent) for situations calling for high availability, or to handle more load. When running Orbeon Forms PE on multiple servers or instances, you need one [PE subscription](https://www.orbeon.com/pricing) per server or instance. 

For more details on sizing, see the section on [how much load Orbeon Forms can handle](../faq/form-builder-runner.md#how-much-load-can-orbeon-forms-handle).

## Java virtual machine configuration

Configure the Java VM with:

* `-Xmx` option for dedicated Java heap memory:
    * on a development machine: at least 1 GB of Java heap: `-Xmx1g`
    * on a production machine: at least 4 GB of Java heap: `-Xmx4g`
* Java 1.7 only (Orbeon Forms 2018.2 and earlier only): ` -XX:MaxPermSize` for "permgen" space:
    * use at least: `-XX:MaxPermSize=256m`

Also, make sure that you do *not* have tiered compilation when using Java 7. See [A dangerous Java 7 JVM option: TieredCompilation](https://blog.orbeon.com/2015/08/a-dangerous-java-7-jvm-option.html).

*NOTE: On Unix systems, GIJ / GCG is not supported as there are reports of issues with that runtime environment and Orbeon Forms. Instead, we recommend you use the Oracle runtime Java environment.*

## Database setup

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

Out-of-the-box, Orbeon Forms includes an SQLite embedded database with multiple demo forms. This setup is designed for a quick start, but for development or production use, you should configure Orbeon Forms to utilize a separate relational database. For more information, see [Relational Database](/form-runner/persistence/relational-db.md). A warning banner will display as a reminder to make this change.

Note that the SQLite demo database is stored in `WEB-INF/orbeon-demo.sqlite`, located where the Orbeon Forms' `.war` file is uncompressed. Be aware that updating the WAR file might overwrite the demo database, resulting in the loss of any saved data.

To disable the `sqlite` embedded database and demo forms, add the following property:

```xml
<property
    as="xs:boolean"
    name="oxf.fr.persistence.sqlite.active"
    value="false"/>
```

See also [Removing the built-in SQLite database](/configuration/advanced/production-war.md#removing-the-built-in-sqlite-database).

## License installation (Orbeon Forms PE only)

* If you are running Orbeon Forms CE, you don't need to install a license file.
* If you are running Orbeon Forms PE:
    * complete the steps for your application server below
    * you can obtain a full licence from Orbeon, or get a [trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new)
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

_NOTE: Orbeon Forms uses Java's `System.getProperty("user.home")` to identify the user's home directory. This corresponds to the user running the servlet container and not necessarily to the user of the developer or system administrator._

## Configuration properties

You must create a default `properties-local.xml` file. See [Properties](/configuration/properties/README.md).

In addition, you must set the [`oxf.crypto.password`](/configuration/properties/general.md#oxf.crypto.password) property to something different from the default.

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

Orbeon Forms will cause an error when starting if the default value for `oxf.crypto.password` is used. This is to prevent you from using the default value in production.

In addition, a password strength checker will also cause an error if the password is too weak. Ideally, use a randomly-generated strong password.

If you plan to use [Field-level encryption](/form-builder/field-level-encryption.md), also set `oxf.fr.field-encryption.password`. See [Field-level encryption configuration](/form-builder/field-level-encryption.md#configuration) for details.

If you plan to use [Access tokens](/form-runner/access-token.md), also set `oxf.fr.access-token.password`. See [Access tokens configuration](/form-runner/access-token.md#configuration) for details.

## Base URL for internal services

This step is sometimes optional.

Depending on your setup, if things don't work out of the box (for example if you have database errors with the sample forms) you might have to set the [oxf.url-rewriting.service.base-uri](../configuration/properties/general.md#oxfurl-rewritingservicebase-uri) configuration property in your `properties-local.xml` file.

Often, it is enough to set it to the following (adjusting for port and prefix):

```xml
<property
    as="xs:anyURI"
    name="oxf.url-rewriting.service.base-uri"
    value="http://localhost:8080/orbeon"/>
```

For more information about how to set configuration properties, see [Configuration Properties](../configuration/properties/README.md).

## Logging configuration

This step is optional. See [Logging](logging.md).

## Specific steps for your container / app server

- [Logging](logging.md)
- [Docker](docker.md)
- [Tomcat](tomcat.md)
- [WildFly](wildfly.md)
- [WebLogic](weblogic.md)
- [WebSphere](websphere.md)
- [GlassFish](glassfish.md)
