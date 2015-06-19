## Introduction

### See also

- [[Form Runner Embedding|Form-Runner-~-Embedding]]
- [[Form Runner Liferay Proxy Portlet Guide|Form-Runner-~-Portal-~-Liferay-Proxy-Portlet-Guide]]
- [[Server-side Embedding API|Form Runner ~ APIs ~ Server side Embedding]]

Please make sure to check the [[Form Runner Liferay Proxy Portlet Guide|Form-Runner-~-Portal-~-Liferay-Proxy-Portlet-Guide]]
as well, as that is the recommended way to deploy Orbeon Forms into Liferay, and [[Form Runner Embedding|Form-Runner-~-Embedding]]
for a comparison of portlet deployments.

### Availability

This is an Orbeon Forms PE feature.

### Servlet vs. portlet deployment

In addition to deploying Orbeon Forms into a servlet container such as Tomcat, you can deploy it into the Liferay portal
using the Orbeon Forms portlet component.

Portals provide features such as:

* __Content aggregation__:  A single web page aggregates the output or user interface of several data sources or applications.
* __Personalization__: Users or administrators of the portal can customize the user interface. This often means not only customizing the look and feel, but also selecting a certain set of available functionalities within the application.
* __Single sign-On__: The user logs in only once to access several applications in the portal.

![][1]

_Bookshelf form deployed in Liferay_

### Status of portlet support in Orbeon Forms

As of Orbeon Forms 4.0, portlet support was tested with Liferay 6.0 and 6.1. We recommend Liferay 6.1 GA2 or newer.

The following Form Runner limitations are known:

* Form Builder is not supported within portlets (but you can run forms designed with Form Builder).
* The Noscript mode is not supported within portlets.

## Deploying into Liferay

### Configuration

The Orbeon Forms WAR can be directly deployed into the Liferay portal. By default, the Form Runner demo forms and XForms examples are immediately available within the portlet:

![_Orbeon Forms welcome page in Liferay_][2]

The following steps assume that:

* Liferay is deployed on port 8080
* the Orbeon Forms WAR is deployed under the name "orbeon" (which is the default if the WAR file is called orbeon.war)

Installation steps:

1. __Deploy orbeon.war:__ Start Liferay. When it is fully started move the `orbeon.war` into the Liferay deploy directory (e.g. `~/liferay/deploy`). At this point, you should see message indicating that Orbeon is being deployed. Monitor the Liferay output as well as the `logs/orbeon.log` for possible errors.


2. __Enable dynamic resource reloading (optional):__ Remove the file `webapps/orbeon/META-INF/context.xml` and restart Liferay. For more information on what this does, see note 2 below.


3. __Configuration for Form Runner (optional):__ You can skip this step if you do not intend to use Form Runner or Form Builder in a portlet. Otherwise, create a file `WEB-INF/resources/config/properties-local.xml` which declares the following properties (and other properties you might want to override):

    ```xml
    <properties xmlns:xs="http://www.w3.org/2001/XMLSchema"
                xmlns:oxf="http://www.orbeon.com/oxf/processors">

        <!-- Configure authentication properties through headers -->
        <property
            as="xs:string"
            name="oxf.fr.authentication.method"
            value="header"/>

        <!-- If you want the Liferay user email used for Form Runner authentication -->
        <property
            as="xs:string"
            name="oxf.fr.authentication.header.username"
            value="Orbeon-Liferay-User-Email"/>

        <!-- If you want Liferay roles used -->
        <property
            as="xs:string"
            name="oxf.fr.authentication.header.roles"
            value="Orbeon-Liferay-User-Roles"/>

    </properties>
    ```

    The host name (`localhost`), port (`8080`), and context path (`orbeon`) must be updated to match your local configuration.

4. __Add the Orbeon Forms portlet:__ Log into the portal, go to the Add menu:

    ![][3]

    Create a new page, for example _Orbeon Page_:

    ![][4]

    Go to the Add menu again and select _More…_:

    ![][5]

    Select the _Orbeon Forms Portlet_ entry:

    ![][6]

    Drag the portlet to the page. The Orbeon Forms home page will show.

NOTE:

When Liferay deploys Orbeon Forms, it changes some of the descriptors and adds a `META-INF/context.xml` which is not
present in the distribution of Orbeon Forms. This file contains:

```xml
<context antijarlocking="true" antiresourcelocking="true">
```

which causes Tomcat to, on startup, make a copy of the Orbeon web application into its temp directory and use that
copy instead of the files under `webapps/orbeon`. That copy is removed when the server shuts down, and will be
done again the next time the server is started. This makes starting Liferay slower, but more importantly this means
that changes you make to files under `webapps/orbeon` after the server started will never be picked up. So any
modification to the resources (`WEB-INF/resources`) of your application will require a restart of Liferay.
This  can be quite very time consuming and annoying, hence our recommendation to remove the `META-INF/context.xml`
generated by Liferay.

### Accessing liferay users and roles

When running in Liferay, you can access some specific user and roles information from XForms.

Getting the current user's email:

```ruby
xxf:get-request-header('orbeon-liferay-user-email')
```

Example:

`'test@liferay.com'`

Getting the current user's full name:

```ruby
xxf:get-request-header('orbeon-liferay-user-full-name')
```

Example:

`'Joe Bloggs'`

Getting the current user's role names:

```ruby
xxf:get-request-header('orbeon-liferay-user-roles')
```

This returns a sequence of strings, with one string per role.

Example:

`('Administrator', 'Power user', 'User')`

Alternatively, you can use the standard Orbeon-Username and Orbeon-Roles headers. See also [[Access Control|Form-Runner-~-Access-Control]].

### Performance tuning

For large pages, we have found that the Liferay strip filter can take an extremely long time to process a response.
You can disable that filter in your `portal-ext.properties` with:

```
com.liferay.portal.servlet.filters.strip.StripFilter=false
```

## Creating a new landing page for the portlet

By default, the portlet shows a list of Orbeon Forms sample forms and apps.

To change this, you need to:

1. Modify `WEB-INF/resources/page-flow-portlet.xml.`
2. Create a new landing page in XHTML format.
3. Change the default Orbeon theme so that no custom Orbeon CSS is added.

A simple way do implement this is as follows:

First, replace this line in `page-flow-portlet.xml`:

```xml
<page id="home" path-info="/home/" model="apps/home/page-flow.xml">
```

with:

```
<page id="home" path-info="/home/" view="home.xhtml">
```

Second, create a new file, `WEB-INF/resources/home.xhtml`, with content such as:

```html
<html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>Form Runner Home</title>
    </head>
    <body>
        <a href="/fr/">Link to the Form Runner home page</a>
        <a href="/fr/acme/form1/summary">Link to summary page of form acme/form1</a>
        <a href="/fr/acme/form2/new">Link to new page of form acme/form2</a>
    </body>
</html>
```

This is the landing page itself, and it can contain any XHTML you like. Typically would include links to specific Form Runner paths as shown in the example above.

Finally, to change the Orbeon portlet theme to the plain theme, set this property:

```xml
<property
  as="xs:anyURI"
  name="oxf.epilogue.theme.embeddable"
  value="oxf:/config/theme-embeddable.xsl"/>
```


## Technical information

### What is a portlet?

Orbeon Forms achieves deployment into portals by supporting a standard, namely the Java Portlet specification version 2 (also known as JSR-286).

According to the Java Portlet Specification, a portlet is a "Java technology based Web component, managed by a portlet container that processes requests and generates dynamic content. Portlets are used by portals as pluggable user interface components that provide a presentation layer to Information Systems". An implementation agnostic definition can be found in the Web Services for Remote Portals (WSRP) White Paper of 22 September 2002 "Portlets are user-facing, interactive Web application components rendering markup fragments to be aggregated and displayed by the portal."

In other words, a portlet is usually a web application that can be embedded within a portal, and shares web page real estate with other portlets. Traditionally portlets available in public portals have provided simple features such as stock quotes, news feeds, weather reports, etc. In particular thanks to the Java Portlet specification, there is no limit to the extent of the features provided by a portlet, and it is expected that complex interactive portlets will become more and more widespread.

### Orbeon Forms and portlets

Orbeon Forms hides the complexity of the Portlet API to allow most Orbeon Forms applications to work unmodified within portlet container. For the curious, the Portlet API requires:

- __Separation of faceless actions from rendering:__ Orbeon Forms allows actions to generate output while still adhering to the Java Portlet specification. Developers are obviously free to only write faceless actions. In the Page Flow Controller, such actions end with a <result page="some-page-id"> directive.
- __Getting rid of the familiar concept of URL path:__ Orbeon Forms abstracts this behavior and provides Orbeon Forms Portlet developers with the notion of a path, implicitly in the Web Application Controller, or explicitly with the Request Generator, while still adhering to the Java Portlet specification.
- __Getting rid of the familiar concept of URL redirection:__ Instead, portlet actions can set parameters to use in subsequent portlet rendering. Orbeon Forms abstracts this behavior and provides, indirectly in the Page Flow Controller, or explicitly with the Redirect Processor, the notion of redirecting to another page within the portlet.
- __Calling APIs to generate URLs:__ Orbeon Forms handles this by providing automatic URL rewriting.
- __Generating markup fragments:__ The default Orbeon Forms epilogue automatically extracts a fragment from an HTML document. This allows pages to remain unmodified for both servlet and portlet deployment.

### Portlet application configuration file

Configuration of portlets is done in a standard file called `portlet.xml` that sits in the same directory (`WEB-INF`) as your `web.xml`. The portlet-class element must always be:

`org.orbeon.oxf.portlet.OrbeonPortlet`

You can also configure non-Orbeon Forms portlets within the same `portlet.xml`.The main processor URI and optional inputs are specified with the `oxf.main-processor.name` and `oxf.main-processor.input.*` initialization parameters.

It is possible to configure several Orbeon Forms Portlets within the same portlet.xml, with the same or a different configuration. The `portlet-name` element however must be different for each portlet, as per the Java Portlet specification.

### Portlet output

The type of the portlet output is determined by the serializer. With the default Orbeon Forms epilogue in `config/epilogue-portlet.xpl`, the HTML serializer is used.

### Preferences

Portlet preferences can be retrieved with the `oxf:portlet-preferences-generator` processor.

To retrieve the preferences of your current portlet, use the following code:

```xml
<p:processor name="oxf:portlet-preferences">
    <p:output name="data" id="portlet-preferences"/>
</p:processor>
```

The generator outputs a document containing name / values in the following format:

```xml
<portlet-preferences>
    <preference>
        <name>name1</name>
        <value>value1</value>
    </preference>
    <preference>
        <name>name2</name>
        <value>value1</value>
        <value>value2</value>
        <value>value3</value>
    </preference>
</portlet-preferences>
```

For example:

```xml
<portlet-preferences>
    <preference>
        <name>max-items</name>
        <value>10</value>
    </preference>
    <preference>
        <name>url</name>
        <value>http://xml.newsisfree.com/feeds/42/1842.xml</value>
    </preference>
</portlet-preferences>
```

Portlet preferences can be saved with the `oxf:portlet-preferences-serializer processor`. [TODO: document]

### Limitations of Orbeon Forms portlets

The Orbeon Forms Portlet developer should be aware of the following limitations:

- __Redirection:__ In the Page Flow Controller, pages that are the target of a portlet render URL cannot end with a redirection. This in particular applies to the default portlet page ("/"). Developers have to make sure that a page exists for "/" that produces content and does not end in a redirect. Other pages can end with redirects by making sure that they are targeted by action URLs (by default, only the target of HTML or XHTML form submissions generate action URLs).
- __Portlet Mode and Window State hints:__ It is currently not possible to set a portlet mode or window state hint in a URL.
- __Content Type Hints:__ It is not possible for an Orbeon Forms Portlet to know which content types are supported by the portal.
- __Preferences:__ It is currently not possible to modify portlet preferences or store them from within a portlet.

[1]: http://wiki.orbeon.com/forms/_/rsrc/1300417103701/doc/developer-guide/release-notes/39/liferay-detail-shadow-small.png
[2]: http://wiki.orbeon.com/forms/_/rsrc/1299210978298/doc/developer-guide/admin/deployment-portlet/home-liferay-shadow-small.png
[3]: http://wiki.orbeon.com/forms/_/rsrc/1286410826460/doc/developer-guide/admin/deployment-portlet/01%20liferay-add.png
[4]: http://wiki.orbeon.com/forms/_/rsrc/1286410855066/doc/developer-guide/admin/deployment-portlet/02%20liferay-page-title.png
[5]: http://wiki.orbeon.com/forms/_/rsrc/1286410916238/doc/developer-guide/admin/deployment-portlet/03%20liferay-add-more.png
[6]: http://wiki.orbeon.com/forms/_/rsrc/1286410942865/doc/developer-guide/admin/deployment-portlet/04%20liferay-add-portlet.png