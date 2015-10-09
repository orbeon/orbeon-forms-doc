# Installation

<!-- toc -->

## Installing and configuring Orbeon Forms

### Downloading and installing Java

Java provides the cross-platform environment in which Orbeon Forms runs.

If you don't have Java installed yet, download it from http://www.oracle.com/technetwork/java/javase/downloads/index.html.

_NOTE: If you use a Mac with Mac OS X, you probably have Java already installed on your machine, but if not visit <http: support.apple.com="" kb="" dl1572="">. Then follow the instructions to install Java._

### Downloading and installing Apache Tomcat

Tomcat is the container application into which Orbeon Forms deploys. Follow these steps to download and install Tomcat if you don't have it installed yet:

1. Download Tomcat 6 from the Apache web site at http://tomcat.apache.org/download-60.cgi..

2. Install Tomcat as per the instructions. If you downloaded the installer version (Windows only), run the installer. If you downloaded a compressed archive, uncompress it to the location of your choice. We call the install location `TOMCAT_HOME` (on windows, this could be `c:/Program Files/Apache/Tomcat`, on a Unix system, `/home/jdoe/tomcat`, etc.).

3. Check that your Tomcat installation is working correctly:
    * Run the Tomcat startup script under `TOMCAT_HOME/bin` (`startup.sh` or `startup.bat` depending on your platform), or start Tomcat with the control application (Windows only).
    * Open a web browser and access the following URL:
    ```xml
    http://localhost:8080/
    ```

    You should see the Tomcat welcome page.

    ![][3]

_NOTE: We recommend using Tomcat for this tutorial, but Orbeon Forms can deploy into containers other than Tomcat._

### Downloading and installing Orbeon Forms

Follow these steps to download and install Orbeon Forms:

1. [Download][4] Orbeon Forms.

2. Uncompress the archive into a directory of your choice. We call that directory `ORBEON_FORMS_HOME`.

3. Under `ORBEON_FORMS_HOME`, you find a file called `orbeon.war`. This is the file to deploy into Tomcat. To do so, just copy it under `TOMCAT_HOME/webapps` (alternatively, if you know what you are doing, you can uncompress it at a location of your choice and configure a context in `TOMCAT_HOME/conf/server.xml`). The `webapps` directory is already present after you have installed Tomcat.

### Testing your setup

Make sure you restart Tomcat (run the shutdown script under `TOMCAT_HOME/bin`, and then the startup script again). Then open up with a web browser the following URL:

```xml
http://localhost:8080/orbeon/
```

You should see the Orbeon Forms welcome page:

![][5]

[3]: https://raw.github.com/wiki/orbeon/orbeon-forms/images/tutorial/01.png
[4]: http://www.orbeon.com/download
[5]: https://raw.github.com/wiki/orbeon/orbeon-forms/images/tutorial/02.png