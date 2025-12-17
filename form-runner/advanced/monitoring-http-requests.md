# Monitoring HTTP requests

## Introduction

When investigating issues, you often need to look at what goes "over the wire", in the HTTP requests between the client and the server. You may want to look at the HTTP requests as sent/received on the browser, on the application server, or somewhere in between. This page lists a few tool which might be of use in these cases.

### On the browser, the server, or in between

* [Charles](http://www.charlesproxy.com/) (see also this [blog post](https://blog.orbeon.com/2013/04/let-charles-help-you-monitor-http.html))
* [WireShark](http://www.wireshark.org/) is a workhorse. It can do a lot more than monitoring HTTP requests, but can also be intimidating. Lately, usability, including the installing process, has improved significantly. (See below for more details on WireShark.)
* [Apache TCPMon](http://ws.apache.org/commons/tcpmon/) is very versatile tool; highly recommended; see the [TCPMon tutorial](http://ws.apache.org/commons/tcpmon/tcpmontutorial.html)

### On the server

* [XForms logging](../../configuration/advanced/xforms-logging.html)
* \[SINCE Orbeon Forms 2020.1.6]
  * [Orbeon Forms `HttpLoggingFilter`](monitoring-http-requests.md#orbeon-forms-httploggingfilter)
* \[UNTIL Orbeon Forms 2020.1.5]
  * [Request Dumper Filter](monitoring-http-requests.md#request-dumper-filter)
  * [Tomcat Request Dumper Valve](monitoring-http-requests.md#tomcat-request-dumper-valve)

### On the browser

* Use the browser's dev tools (AKA "F12 Developer Tools" on IE)
* On Firefox, use [Firebug](https://addons.mozilla.org/en-US/firefox/addon/1843)'s Net tab, or the Console tab for Ajax requests.

## WireShark

After you install WireShark, click on _Capture Options_, and setup a setup the _Capture Filter_ to be `tcp port 8080`. (Replace `8080` with the port on which your application server or services are listening, as appropriate.)

![](../../.gitbook/assets/wireshark-capture-filter.png)

Click Start, and since you only interested about HTTP (versus TCP) traffic, in _Filter_ type `http` and press enter to apply.

![](../../.gitbook/assets/wireshark-filter.png)

Next WireShark will show you all the HTTP traffic that goes through the machine it is running on, to the port you specified (here 8080).

## Orbeon Forms `HttpLoggingFilter`

\[SINCE Orbeon Forms 2021.1, 2020.1.6] To enable, add the following to your `web.xml`, before all the other filters.

```xml
<filter>
    <filter-name>orbeon-http-logging-filter</filter-name>
    <filter-class>org.orbeon.oxf.servlet.HttpLoggingFilter</filter-class>
</filter>
<filter-mapping>
    <filter-name>orbeon-http-logging-filter</filter-name>
    <url-pattern>/xforms-server</url-pattern>
    <dispatcher>REQUEST</dispatcher>
</filter-mapping>
```

[\[SINCE Orbeon Forms 2023.1\]](../../release-notes/orbeon-forms-2023.1.md) If you are using Orbeon Forms 2023.1 or newer, and are running a servlet container that uses the Jakarta Servlet API (e.g. Tomcat 10+, WildFly 27+), you need to use the `org.orbeon.oxf.servlet.JakartaHttpLoggingFilter` servlet filter class instead of `org.orbeon.oxf.servlet.HttpLoggingFilter`.

## Request Dumper Filter

Note that the Request Dumper Filter only shows information about the HTTP headers of the request. If you want more information to be logged, use the Apache TCPMon.

1. The Request Dumper Filter comes with Tomcat, so even if you are not using Tomcat, start by [downloading Tomcat](https://tomcat.apache.org/download-70.cgi).
2. Unzip Tomcat, and copy the directory webapps/servlets-examples/WEB-INF/classes/filters to the WEB-INF/classes directory of your web application.
3. Edit WEB-INF/web.xml and add:
   *   After the declaration of the filter ops-xforms-filter:

       ```xml
       <filter>
           <filter-name>request-dumper-filter</filter-name>
           <filter-class>org.apache.catalina.filters.RequestDumperFilter</filter-class>
       </filter>
       ```
   *   After the filter mapping for ops-xforms-filter:

       ```xml
       <filter-mapping>
           <filter-name>request-dumper-filter</filter-name>
           <url-pattern>/*</url-pattern>
       </filter-mapping>
       ```

* Restart your application server (e.g. Tomcat).
* On Tomcat, requests will be logged to the logs/localhost log file.

## Tomcat Request Dumper Valve

The Request Dumper Valve doesn't log the body of `POST`s, it can only be used on Tomcat, and we found its output to be less readable than what you get with the Request Dumper Filter. But if you still want to experiment with the Request Dumper Valve, add the following in server.xml inside :

```xml
<valve classname="org.apache.catalina.valves.RequestDumperValve">
```

## See also

* [Let Charles help you monitor HTTP requests](https://blog.orbeon.com/2013/04/let-charles-help-you-monitor-http.html)
