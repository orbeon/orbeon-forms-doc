# Tomcat

<!-- toc -->

## Supported Tomcat versions

As of Orbeon Forms 2016.1 and 2016.2, the following versions of Tomcat are supported:

- Tomcat 6 (not recommended, see below)
- Tomcat 7
- Tomcat 8

*NOTES:*

- The Apache Tomcat team has [announced](https://tomcat.apache.org/tomcat-60-eol.html) that support for Apache Tomcat 6.0.x will end on December 31, 2016. We don't recommend using Tomcat 6 after that date.
- [TIFF support](http://doc.orbeon.com/form-runner/feature/tiff-production.html) is not available when using Tomcat 6 (see [#2717](https://github.com/orbeon/orbeon-forms/issues/2717)).


## Setup

We assume below that `TOMCAT_HOME` represents the location of your Tomcat installation.

If using Orbeon Forms PE, make sure the `license.xml` file is [in place](./README.md#license-installation-orbeon-forms-pe-only).

### Quick setup

1. Create a new `TOMCAT_HOME/webapps/orbeon` directory.
2. Unzip `orbeon.war` in the `orbeon` directory you just created. So now you should have a directory `TOMCAT_HOME/webapps/orbeon/WEB-INF`. 
3. You can now start Tomcat, and access `http://localhost:8080/orbeon/` to test your installation (replacing `localhost` and `8080` with the host name and port number of your Tomcat installation if different from the default).

## Optional steps

### Custom context within server.xml

Edit `TOMCAT_HOME/conf/server.xml`, and inside the `<Host>` create a `<Context>` as follows, changing the value of the `docBase` attribute as appropriate on your system:

```xml
<Context
    path="/orbeon"
    docBase="TOMCAT_HOME/webapps/orbeon"
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
        password="password"
        url="jdbc:mysql://localhost:3306/orbeon?useUnicode=true&amp;characterEncoding=UTF8"/>
</Context>
```

### URIEncoding

We recommend you add the `URIEncoding="UTF-8"` attribute on the [`<Connector>`](http://tomcat.apache.org/tomcat-7.0-doc/config/http.html) element, in your `server.xml`, as [recommended in the Tomcat FAQ](http://wiki.apache.org/tomcat/FAQ/CharacterEncoding#Q8). This will ensure that all characters get properly decoded on the URL, which is especially important if you're using non-ASCII characters in the app or form name in Form Builder.

### Form Runner authentication

To setup Form Runner authentication:

1. Open `TOMCAT_HOME/webapps/orbeon/WEB-INF/web.xml` and uncomment the `security-constraint`, `login-config` and `security-role` declarations at the end of the file.
2. Open `TOMCAT_HOME/conf/server.xml` and make sure there is a `<Realm>` enabled. For example, by default with Tomcat 7:

    ```xml
    <Realm className="org.apache.catalina.realm.LockOutRealm">
        <Realm
            className="org.apache.catalina.realm.UserDatabaseRealm"
            resourceName="UserDatabase"/>
    </Realm>
    ```
3. Edit `TOMCAT_HOME/conf/tomcat-users.xml` and replace the content of the file with:

    ```xml
    <tomcat-users>
        <user
            username="orbeon-user"
            password="Secret, change me!"
            roles="orbeon-user"/>
        <user
            username="orbeon-admin"
            password="Secret, change me!"
            roles="orbeon-user,orbeon-admin"/>
    </tomcat-users>
    ```
4. Enumerate the roles in the following property:

    ```xml
    <property
      as="xs:string"
      name="oxf.fr.authentication.container.roles"
      value="orbeon-user orbeon-admin"/>
    ```

### BASIC authentication

If you are using BASIC authentication on Tomcat 6.0.21 or newer, or on Tomcat 7, then you need to add the following valve inside the `<Context>` corresponding to the Orbeon Forms web app in Tomcat's configuration:

```xml
<Valve
	className="org.apache.catalina.authenticator.BasicAuthenticator"
	changeSessionIdOnAuthentication="false"/>
```
