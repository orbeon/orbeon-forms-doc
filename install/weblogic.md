## Oracle WebLogic 11g

A version of the ANTLR library that ships with WebLogic 11g conflicts with the version required by Orbeon Forms. To run Orbeon Forms on WebLogic 10/11g, you need to instruct WebLogic to let Orbeon Forms use the version of ANTLR that ships with Orbeon Forms. You can do this in the WebLogic EAR descriptor, which means you need to encapsulate Orbeon Forms in an EAR before you deploy it:

1. Create the following directory structure in a temporary directory:

    ```
    orbeon-ear
        META-INF
            application.xml
            weblogic-application.xml
        orbeon
    ```
    Populate `application.xml` with:
    ```xml
    <?xml version="1.0"?>
    <j2ee:application xmlns:j2ee="http://java.sun.com/xml/ns/j2ee">
        <j2ee:display-name>Orbeon Forms</j2ee:display-name>
        <j2ee:module>
            <j2ee:web>
                <j2ee:web-uri>orbeon</j2ee:web-uri>
                <j2ee:context-root>/orbeon</j2ee:context-root>
            </j2ee:web>
        </j2ee:module>
    </j2ee:application>
    ```
    Populate `weblogic-application.xml` with:
    ```xml
    <?xml version="1.0" encoding="UTF-8"?>
    <!DOCTYPE weblogic-application PUBLIC
        "-//BEA Systems, Inc.//DTD WebLogic Application 8.1.0//EN"
        "http://www.bea.com/servers/wls810/dtd/weblogic-application_2_0.dtd">
    <weblogic-application>
        <prefer-application-packages>
            <package-name>antlr.*</package-name>
            <package-name>org.apache.commons.lang.*</package-name>
            <package-name>org.apache.commons.fileupload.*</package-name>
            <package-name>org.apache.lucene.*</package-name>
        </prefer-application-packages>
    </weblogic-application>
    ```
2. Uncompress the `orbeon.war` into the `orbeon-ear/orbeon` directory you created in step 1. After this, you should have a directory `orbeon-ear/orbeon/WEB-INF`.
3. Deploy the `orbeon-ear` directory. If you are running WebLogic in development mode, you can move it to `user_projects/domains/base_domain/autodeploy`.
4. Optionally, you might want to change where the `orbeon.log` is stored. You define the location of the file in `WEB-INF/resources/config/log4j.xml`, in the `SingleFileAppender`. By default the location of the file is defined as `../logs/orbeon.log`. If you start WebLogic with `user_projects/domains/base_domain/startWebLogic.sh`, the log will be located in `user_projects/domains/logs/orbeon.log`.