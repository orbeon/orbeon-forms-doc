# Relational Database Logging

## Introduction

In case of issues when using Orbeon Forms with a relational database, you might want to see what's happening between Orbeon Forms and said database. We have found that [P6Spy](https://github.com/p6spy/p6spy) is a useful tool for troubleshooting this scenario.

## Configuration

Below, you'll find a summary of the steps you can follow to install and configure P6Spy. For more options, or if you're using an application server  other than Tomcat, you'll most likely want to refer to the P6Spy documentation, and specifically their [installation](http://p6spy.readthedocs.io/en/latest/install.html) and [configuration](http://p6spy.readthedocs.io/en/latest/configandusage.html) instructions.

1. [Download](https://search.maven.org/search?q=g:p6spy) the P6Spy jar file ([files](https://central.sonatype.com/artifact/p6spy/p6spy/versions)).
2. Move the P6Spy jar file to Tomcat's `lib` directory, or the equivalent directory on your application server. This should be the same directory where you installed your database driver jar file.
3. In the same directory, create a `spy.properties` as follows. On the first line replace `/var/log/tomcat` by the directory where your log files are stored. If you're using a database other than MySQL, on the second line replace `com.mysql.cj.jdbc.Driver` by the corresponding JDBC driver class name for your database.

   ```
   logfile=/var/log/tomcat/spy.log
   driverlist=com.mysql.cj.jdbc.Driver
   dateformat=MM-dd-yy HH:mm:ss:SS
   logMessageFormat=com.p6spy.engine.spy.appender.CustomLineFormat
   customLogMessageFormat=%(currentTime)|%(executionTime)|%(category)|connection%(connectionId)\n%(sql)
   ```
    
4. Where you define the datasource for Orbeon Forms, replace the driver class name by `com.p6spy.engine.spy.P6SpyDriver` and prefix the URL by `jdbc:p6spy:`. On Tomcat, this is done by editing Tomcat's `server.xml`, and in that file, inside the `Context` you have defined for Orbeon Forms, change the `Resource` setting the value of the `driverClassName` attribute to `com.p6spy.engine.spy.P6SpyDriver`, and prefixing the value of the `url` attribute by `jdbc:p6spy:`, as in:

   ```xml
   <Resource
       name="jdbc/mysql"
       driverClassName="com.p6spy.engine.spy.P6SpyDriver"
   
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
       password=""
       url="jdbc:p6spy:mysql://localhost:3306/orbeon?useUnicode=true&amp;characterEncoding=UTF8"/>
   ```

5. Restart Tomcat or the application server you're using. Check that no error messages show on the console, `catalina.out`, or equivalent with your setup. If none show and Orbeon Forms starts properly, run `tail -f spy.log` in a terminal window, or equivalent on your operating system, and check that as you access, say, the Form Builder summary page, SQL statement are properly being logged.

## See also

- [Using Form Runner with a Relational Database](/form-runner/persistence/relational-db.md)
- [Troubleshooting with the orbeon.log](/configuration/troubleshooting/orbeon-log)
- [Logging](/installation/logging.md)
- [XForms logging](/configuration/advanced/xforms-logging.md)
