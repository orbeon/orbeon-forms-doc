# Logging

## Introduction 

Most applications support some form of [*logging*](https://en.wikipedia.org/wiki/Logging_(software)). Orbeon Forms is no different.

- Internally, the Orbeon Forms web application uses the [SLF4J](http://www.slf4j.org/) API, which allows using various Java logging implementations such as Log4j or logback.
- Out of the box, Orbeon Forms uses [Log4j](https://logging.apache.org/log4j/2.x/) as logging implementation.

## Version of Log4j

Due to late 2021 security vulnerabilities with Log4j, and even though Orbeon Forms was not directly affected by those vulnerabilities, Orbeon Forms switched from Log4j 1.x to the latest [Log4j 2.x](https://logging.apache.org/log4j/2.x/) version in order to respond faster to future vulnerabilities should they arise. See the following blog posts:

- [Vulnerability in the log4j library](https://blog.orbeon.com/2021/12/vulnerability-in-log4j-library.html)
- [Orbeon Forms PE Log4j maintenance releases](https://blog.orbeon.com/2021/12/orbeon-forms-pe-log4j-maintenance.html)
- [More Orbeon Forms PE Log4j maintenance releases](https://blog.orbeon.com/2021/12/more-orbeon-forms-pe-log4j-maintenance.html)

The following versions of Orbeon Forms use Log4j 2.x:

- 2022.1 CE and PE and newer
- 2021.1 CE and PE and newer
- 2020.1.6 PE and newer
- 2019.2.4 PE and newer
- 2019.1.2 PE and newer
- 2018.2.5 PE and newer
- 2018.1.4 PE and newer

## Log4j configuration

Log4j 2.x uses different configuration files than Log4j 1.x. However, Orbeon Forms provides backward compatibility support for the older Log4j 1.x configuration file. This means that in most cases, you do not have to update your configuration file immediately if you are upgrading from an older version of Orbeon Forms.

Here is the location and names of the configuration files:

| Log4j version | Location and Name                     |
|---------------|---------------------------------------|
| Log4j 1.x     | `WEB-INF/resources/config/log4j.xml`  |
| Log4j 2.x     | `WEB-INF/resources/config/log4j2.xml` |

Versions of Orbeon Forms that support Log4j 2.x no longer ship with a `log4j.xml` configuration file, but ship with a `log4j2.xml` configuration file.

- If you have pre-existing `log4j.xml` configuration file, for example because you are upgrading from an older version, you can still use that configuration file, which will take precedence over the new `log4j2.xml` file. However we do recommend that you consider moving to a `log4j2.xml` configuration file.
- If you do not yet have an existing `log4j.xml` file, we recommend that you update the `log4j2.xml` configuration file that ships with Orbeon Forms and that you do not create a `log4j.xml`.
    
_WARNING: With version of Orbeon Forms that use Log4j 2.x, and whether you are using `log4j.xml` or `log4j2.xml`, you must make sure that you do not have __duplicate log file names in the configuration__, even if some of them are unused, or Log4j 2.x will complain about that and ignore the configuration. Log4j 1.x did not use to consider this an error, but Log4j 2.x does._

_WARNING: If you are creating or updating a `log4j2.xml` file, you __cannot__ simply copy the contents of an existing `log4j.xml` to `log4j2.xml` as the two formats are incompatible! Instead, start with the `log4j2.xml` provided, and visit the [Log4j 2 configuration](https://logging.apache.org/log4j/2.x/manual/configuration.html) online to understand and make changes._

## How Orbeon Forms initializes logging

First, when the Orbeon Forms web application starts, it attempts to initialize a minimal, predefined Log4j configuration:

- Appender: `<Console>`
- Level: `info`
- Pattern: `"%date{ISO8601} %-5level %logger{1} - %message%n"`

Second, once Orbeon Forms is able to read `log4j.xml` or `log4j2.xml`, it reconfigures Log4j with the specified configuration. 

You can disable Orbeon Forms's Log4j initialization in `WEB-INF/web.xml` with:

```xml
<context-param>
    <param-name>oxf.initialize-logging</param-name>
    <param-value>false</param-value>
</context-param>
```

Doing so is necessary if you want to configure Log4j with your own configuration files, or if you want to remove Log4j and use a different SLF4J backend.

## Selecting a specific file path

By default, logging information is output to a file path relative to the directory where you start your application server. This is usually not what you want, as that makes it hard to know where the log file is.

Log4j 2.x (`WEB-INF/resources/config/log4j2.xml`):

```xml
<File
    name="SingleFileAppender"
    fileName="../logs/orbeon.log"
    append="false">
    <PatternLayout pattern="%date{ISO8601} - %tid - %-5level %logger{1} %X{orbeon-incoming-http-header-host} - %message%n"/>
</File>
```

Log4j 1.x (`WEB-INF/resources/config/log4j.xml`):

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

You can change this by modifying the `fileName` (Log4j 2.x) or `File` (Log4j 1.x) parameter and set an _absolute file path_ to the log file.

Log4j 2.x (`WEB-INF/resources/config/log4j2.xml`):

```xml
<File
    name="SingleFileAppender"
    fileName="/path/to/logs/orbeon.log"
    append="false">
    <PatternLayout pattern="%date{ISO8601} - %tid - %-5level %logger{1} %X{orbeon-incoming-http-header-host} - %message%n"/>
</File>
```

Log4j 1.x (`WEB-INF/resources/config/log4j.xml`):

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

Note that on Windows, you must use forward slashes.

Log4j 2.x (`WEB-INF/resources/config/log4j2.xml`):

```xml
<File
    name="SingleFileAppender"
    fileName="C:/My Path/To/Logs/orbeon.log"
    append="false">
    <PatternLayout pattern="%date{ISO8601} - %tid - %-5level %logger{1} %X{orbeon-incoming-http-header-host} - %message%n"/>
</File>
```

Log4j 1.x (`WEB-INF/resources/config/log4j.xml`):

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

By default, Orbeon Forms logs quite a lot of information at the `info` level. In case this is too much, you can set the level to `warning` or even `error`. Similarly, for debugging, you can set the level to the more verbose `debug`.

Log4j 2.x (`WEB-INF/resources/config/log4j2.xml`):

```xml
<File
    name="SingleFileAppender"
    fileName="/path/to/logs/orbeon.log"
    append="false">
    <PatternLayout pattern="%date{ISO8601} - %tid - %-5level %logger{1} %X{orbeon-incoming-http-header-host} - %message%n"/>
    <ThresholdFilter level="error"/>
</File>
```

Log4j 1.x (`WEB-INF/resources/config/log4j.xml`):

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

## Logging HTTP headers

[SINCE Orbeon Forms 2022.1]

You can set the following property to ask Orbeon Forms to add all HTTP headers to the [Log4j Thread Context](https://logging.apache.org/log4j/2.x/manual/thread-context.html).

```xml
<property 
    as="xs:boolean"
    name="oxf.log4j.thread-context.http-headers"                
    value="true"/>
```

When this property is set, you can then log specific headers using the `%X{}` syntax in Log4j pattern, prefixing the header name in lower case by `orbeon-incoming-http-header-`. For instance, adding the following to your pattern will log the value of the `Host` header. 

```
%X{orbeon-incoming-http-header-host}
```

## Client-side logging

As more work is getting done on the client (web browser), Orbeon Forms has added logging abilities there as well. In general, Orbeon Forms doesn't log much except in case of error, and then it logs to the JavaScript console, which is usually not consulted by the end-user.

As of Orbeon Forms 2021.1, Orbeon Forms uses [Log4s](https://github.com/Log4s/log4s) and [Scribe](https://github.com/outr/scribe) to log on the client. However, the logging configuration is currently not customizable by the user.  

## See also 

- [XForms logging](/configuration/advanced/xforms-logging.md)
- [Relational database logging](/configuration/troubleshooting/database-logging.md)
- Blog posts:
    - [Vulnerability in the log4j library](https://blog.orbeon.com/2021/12/vulnerability-in-log4j-library.html)
    - [Orbeon Forms PE Log4j maintenance releases](https://blog.orbeon.com/2021/12/orbeon-forms-pe-log4j-maintenance.html)