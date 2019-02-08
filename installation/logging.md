# Logging

## Introduction 

Orbeon Forms uses log4j for logging and has a logging configuration file under `WEB-INF/resources/config/log4j.xml`.

## Selecting a specific file path

By default, logging information is output to a file path relative to the directory where you start your application server.

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

You can change this by modifying the `File` parameter:

```xml
<appender name="SingleFileAppender" class="org.apache.log4j.FileAppender">
    <param name="File" value="/path/to/logs/orbeon.log"/>
    <param name="Append" value="false" />
    <param name="Encoding" value="UTF-8"/>
    <layout class="org.apache.log4j.PatternLayout">
        <param name="ConversionPattern" value="%d{ISO8601} %-5p %c{1} %x - %m%n"/>
    </layout>
</appender>
```

Note that on Windows, you must use forward slashes:

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

## Reducing the amount of logging

By default, Orbeon Forms logs quite a lot of information at the `info` level. In case this is too much, you can set the level to `warning` or even `error`:     

```xml
<appender name="SingleFileAppender" class="org.apache.log4j.FileAppender">
    <param name="File" value="/path/to/logs/orbeon.log"/>
    <param name="Append" value="false" />
    <param name="Encoding" value="UTF-8"/>
    <layout class="org.apache.log4j.PatternLayout">
        <param name="ConversionPattern" value="%d{ISO8601} %-5p %c{1} %x - %m%n"/>
    </layout>
    <filter class="org.apache.log4j.varia.LevelRangeFilter">
        <param name="LevelMin" value="error"/>
    </filter>
</appender>
```

## See also 

- [XForms logging](/configuration/advanced/xforms-logging.md)
- [Relational database logging](/configuration/troubleshooting/database-logging.md)