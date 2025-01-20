# GlassFish

## Introduction

Installing Orbeon Forms on GlassFish is only one of the possibilities. You can also install Orbeon Forms on other Servlet containers. You can also use Docker containers. See also:

- Docker
    - [Blog post](https://www.orbeon.com/2024/10/orbeon-forms-docker-images) 
    - [Detailed documentation](docker.md)
- Servlet containers
    - [Tomcat](tomcat.md)
    - [WildFly](wildfly.md)
    - [WebLogic](weblogic.md)
    - [WebSphere](websphere.md)

## Installation notes

On GlassFish, you need to do the following setup to avoid a `java.security.UnrecoverableKeyException` with the message _Password must not be null_:

1. Edit your domain's `domain.xml` (e.g. in `domains/domain1/config/domain.xml`).
2. Search for the section of the file that contains `<jvm-options>` elements, and there, add: `<jvm-options>-Djavax.net.ssl.keyStorePassword=changeit</jvm-options>`. If you changed your Glassfish [master password](http://docs.oracle.com/cd/E18930_01/html/821-2435/ghgrp.html), set this property to your new password.

The above steps have been verified on GlassFish 3.1 and 4.1.1.
