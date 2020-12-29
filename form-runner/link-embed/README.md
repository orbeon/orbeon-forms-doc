# Linking and Embedding

## Rationale

"Embedding" refers to a deployment option where Orbeon Forms pages or forms appear *embedded* or *nested* within another application's page. This is in contrast to the default deployment setup where Orbeon Forms produces entire web pages, and you link to those page from your application.

Orbeon Forms supports several embedding methods:

- [Form Runner server-side Java embedding API](java-api.md)
- [Form Runner proxy portlet](liferay-proxy-portlet.md)
- [Form Runner full portlet](liferay-full-portlet.md)
- [Form Runner JavaScript Embedding API](javascript-api.md)

In all cases, also see [Securing Form Runner access when embedding](securing.md).

## Liferay

### Supported Liferay versions

The following versions of Liferay are supported:

|Orbeon Forms Version|Tested Liferay Version    |Proxy Portlet|Full Portlet|
|--------------------|--------------------------|-------------|------------|
|2020.1              |Liferay 7.2.1 CE GA2      |yes          |no          |
|2019.2.2            |Liferay 7.2.1 CE GA2      |yes          |no          |
|                    |Liferay 7.1.3 CE GA4      |yes          |no          |
|2019.2              |Liferay 7.1.3 CE GA4      |yes          |no          |
|2019.1              |Liferay 7.1.3 CE GA4      |yes          |no          |
|2018.2              |Liferay 7.1.2 CE GA3      |yes          |no          |
|                    |Liferay 7.0 CE GA5 / 7.0.4|yes          |no          |
|2018.1              |Liferay 7.0 CE GA5 / 7.0.4|yes          |no          |
|2017.2              |Liferay 7.0 CE GA5 / 7.0.4|yes          |no          |
|2017.1              |Liferay 6.2 CE GA6 / 6.2.5|yes          |yes         |
|                    |Liferay 7.0 CE GA3 / 7.0.2|yes          |no          |
|2016.3              |Liferay 6.2 CE GA6 / 6.2.5|yes          |yes         |
|2016.2              |Liferay 6.1.2 CE GA3      |yes          |yes         |
|                    |Liferay 6.2 CE GA6 / 6.2.5|yes          |yes         |
|4.9                 |Liferay 6.0               |yes          |yes         |
|                    |Liferay 6.1.2 CE GA3      |yes          |yes         |

### Servlet vs. Liferay portal deployment

In addition to deploying Orbeon Forms into a servlet container such as Tomcat, you can deploy it into the Liferay portal using the Orbeon Forms portlets.

Portals such as Liferay provide features such as:

* __Content aggregation__:  A single web page aggregates the output or user interface of several data sources or applications.
* __Personalization__: Users or administrators of the portal can customize the user interface. This often means not only customizing the look and feel, but also selecting a certain set of available functionality within the application.
* __Single sign-On__: The user logs in only once to access several applications in the portal.

### Known limitations

The following Form Runner limitations are known within Liferay:

* With Liferay 7/DXP, since Orbeon Forms 2017.1, the full portlet is not supported.

### Form Runner proxy portlet or full portlet?

The full portlet is not specifically tied to Form Runner and does not provide Form-Runner-specific features. Using the full portlet usually requires setting a custom landing page and possibly other custom configurations.
 
The Form Runner proxy portlet is designed to work with Form Runner only and to support a deployment mode where Form Runner can be installed within a separate application container, possibly on a separate server.

You can configure the proxy portlet via UI, making it is easier to create a setup with multiple forms on different portal pages.

Here is a comparison of the two options:

|Feature                               |Proxy Portlet|Full Portlet|Since Version|
|--------------------------------------|-------------|------------|-------------|
|Runs Form Runner forms                |yes          |yes         |             |
|Specifically designed for Form Runner |yes          |no          |             |
|Allows remote Form Runner             |yes          |no          |             |
|Options configurable via UI           |yes          |no          |             |
|Form selection via UI                 |yes          |no          |             |
|Form selection via URL parameters     |yes          |no          |4.7          |
|Can use portal user information       |yes          |no          |4.4          |
|Can use portal language preference    |yes          |no          |4.4          |
|Supports Liferay organizations        |yes          |no          |2016.3       |
|Runs custom XForms applications       |no           |yes         |             |
|Requires custom landing page          |no           |yes         |             |
