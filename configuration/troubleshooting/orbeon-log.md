# Troubleshooting with the `orbeon.log`

When you get an unexpected behavior, like an error message for Form Builder or when running a form, you can often find more information about what went wrong in an Orbeon Forms log file generally referred to as `orbeon.log`. To check this log:

1. Find where your `orbeon.log` is located. Out-of-the-box, it is written in `../logs/orbeon.log`, relative to the current directory at the time you're starting your servlet container or application server. For instance, if you're starting Tomcat from it `bin` directory, the log file will be in Tomcat's `logs` directory. If you can't find the `orbeon.log`, or would like it to be created in a different location, in Orbeon Forms edit the `WEB-INF/resouces/config/log4j.xml`, locate the `SingleFileAppender`, and in `<param name="File" value="../logs/orbeon.log"/>` replace `../logs/orbeon.log` with the location where you'd like the `orbeon.log` to be stored. Using an absolute path, like `/opt/tomcat/logs/orbeon.log` is often a good idea.
2. Update the Orbeon Forms `WEB-INF/resouces/config/log4j.xml` and `WEB-INF/resouces/config/properties-local.xml` per the [development configuration](/configuration/advanced/xforms-logging.md#development-configuration). This will increase the amount of information logged by Orbeon Forms, and it is generally a good idea to keep that configuration in place for as long as you are developing forms or troubleshooting a problem.
3. On Linux or macOS, run `tail -f orbeon.log` so you can watch information appended to the `orbeon.log`. On Windows, you can use a tool like [LogExpert](https://github.com/zarunbal/LogExpert) ([download](https://github.com/zarunbal/LogExpert/releases)). Reproduce the problem, at the same time watching your `orbeon.log`, and see if any error is being reported. If so analyze the error message, and see if this helps you find the source of the problem.
4. If you can't seem to find the source of the problem based on what you are seeing in your `orbeon.log`, when reporting a problem, it is a good idea to submit your `orbeon.log` with your report. To minimize the size of the file, stop your servlet container or application server (e.g. Tomcat), delete your `orbeon.log`, restart your servlet container or application server, reproduce the problem, and send the `orbeon.log` you have right after reproducing the issue.