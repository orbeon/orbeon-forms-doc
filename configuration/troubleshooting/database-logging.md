# Relational Database Logging



## Introduction

In case of issues when using Orbeon Forms with a relational database, you might want to see what's happening between Orbeon Forms and said database. We have found that [log4jdbc](https://github.com/arthurblake/log4jdbc) is a useful tool for troubleshooting this scenario.

## Configuration

[Download](https://github.com/arthurblake/log4jdbc/releases) and [install](https://code.google.com/p/log4jdbc/) [log4jdbc](https://github.com/arthurblake/log4jdbc).

Then update your datasource configuration. For example, with Tomcat and Oracle:

```xml
<Resource
    name="jdbc/oracle"
    driverClassName="net.sf.log4jdbc.DriverSpy"
    
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
    password="**password**"
    url="jdbc:log4jdbc:oracle:thin:@//localhost:1521/globaldb"/>
```

Note the `driverClassName` set to `net.sf.log4jdbc.DriverSpy`, and the `url` starting with `jdbc:log4jdbc:`, followed by the usual driver prefix, here `oracle:`.

## See also

- [Using Form Runner with a Relational Database](/form-runner/persistence/relational-db.md)
