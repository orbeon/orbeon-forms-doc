> [Wiki](Home) ▸ Installation

## Versions

We recommend using a current version of Tomcat. As of Orbeon Forms 4.6, those are:

- Tomcat 6
- Tomcat 7

*NOTE: Tomcat 8 hasn't been tested yet.*

On Unix systems, we recommend you don't use GIJ / GCG, as there are reports of issues with that runtime environment and Orbeon Forms. Instead, we recommend you use the Sun/Oracle runtime Java environment.

## Setup

1. Assuming that `TOMCAT_HOME` represents the location of your Tomcat installation: create a new `TOMCAT_HOME/webapps/orbeon` directory.
2. Unzip `orbeon.war` in the `orbeon` directory you just created.
3. If using Orbeon Forms PE, make sure the `license.xml` file is in place.
4. You can now start Tomcat, and access `http://localhost:8080/orbeon/` to test your installation (replacing `localhost` and `8080` with the host name and port number of your Tomcat installation if different from the default).
5. We recommend you add the `URIEncoding="UTF-8"` attribute on the `Connector`][2] element, in your `server.xml`, as [recommended in the Tomcat FAQ][3]. This will ensure that all characters get properly decoded on the URL, which is especially important if you're using non-ASCII characters in the app or form name in Form Builder.

Optional steps:

1. To setup Form Runner authentication:
    1. Open `TOMCAT_HOME/webapps/orbeon/WEB-INF/web.xml` and uncomment the `security-constraint`, `login-config` and `security-role` declarations at the end of the file.
    2. Open `TOMCAT_HOME/conf/server.xml` and make sure there is a `<Realm>` enabled. For example, by default with Tomcat 7:

    ```xml
    <Realm className="org.apache.catalina.realm.LockOutRealm">
        <Realm className="org.apache.catalina.realm.UserDatabaseRealm" resourceName="UserDatabase"/>
    </Realm>
    ```
    3. Edit `TOMCAT_HOME/conf/tomcat-users.xml` and replace the content of the file with:

    ```xml
    <tomcat-users>
        <user username="orbeon-user"  password="xforms" roles="orbeon-user"/>
        <user username="orbeon-admin" password="xforms" roles="orbeon-user,orbeon-admin"/>
    </tomcat-users>
    ```
2. If you are using basic authentication on Tomcat 6.0.21 or newer, or on Tomcat 7, then you need to add the following valve inside the `<Context>` corresponding to the Orbeon Forms web app in Tomcat's configuration:

    ```xml
    <Valve
        className="org.apache.catalina.authenticator.BasicAuthenticator"
        changeSessionIdOnAuthentication="false"/>
    ```

## Setting up a custom context

You can setup the Tomcat context directly within the Orbeon Forms WAR under `TOMCAT_HOME/webapps/orbeon/META-INF/context.xml`. For example:

```xml
<Resource name="jdbc/mysql" auth="Container" type="javax.sql.DataSource"
    initialSize="3" maxActive="10" maxIdle="20" maxWait="30000"
    driverClassName="com.mysql.jdbc.Driver"
    poolPreparedStatements="true"
    username="orbeon"
    password="password"
    url="jdbc:mysql://localhost:3306/orbeon?useUnicode=true&amp;characterEncoding=UTF8"/>
</Context>
```

## Setting up a custom context within server.xml

This is an alternate way of deploying with Tomcat, which gives you more control as to where the Orbeon Forms files are located.

First, unzip the Orbeon Forms WAR file into a directory of your choice, for example:

    /path/to/orbeon

This means that under the `orbeon` directory, you should have a `WEB-INF` directory.

Then create a context in Tomcat's `server.xml`, for example:

```xml
<Context
    path="/orbeon"
    docBase="/path/to/orbeon-war"
    reloadable="false"
    override="true"
    allowLinking="true"/>
```

Note that if you have a JDBC datasource, you can also place it inside:

```xml
<Context
    path="/orbeon"
    docBase="/path/to/orbeon-war"
    reloadable="false"
    override="true"
    allowLinking="true">
    <Resource name="jdbc/mysql" auth="Container" type="javax.sql.DataSource"
        initialSize="3" maxActive="10" maxIdle="20" maxWait="30000"
        driverClassName="com.mysql.jdbc.Driver"
        poolPreparedStatements="true"
        username="orbeon"
        password="password"
        url="jdbc:mysql://localhost:3306/orbeon?useUnicode=true&amp;characterEncoding=UTF8"/>
    </Context>
```

[2]: http://tomcat.apache.org/tomcat-7.0-doc/config/http.html
[3]: http://wiki.apache.org/tomcat/FAQ/CharacterEncoding#Q8