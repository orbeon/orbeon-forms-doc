<!-- toc -->

## Embedding Orbeon Forms 

"Embedding" refers to a deployment option where Orbeon Forms pages or forms appear *embedded* or *nested* within another application's page. This is in contrast to the default deployment setup where Orbeon Forms produces entire web pages, and you would, from your application [link to those pages produced by Orbeon Forms]().

Orbeon Forms supports several embedding methods:

- with the [[Server-side embedding API|Form Runner ~ APIs ~ Server side Embedding]] [Since Orbeon Forms 4.7]
- with the [[Form Runner proxy portlet|Form Runner ~ Portal ~ Liferay Proxy Portlet Guide]]
- with the [[Form Runner full portlet|Form Runner ~ Portal ~ Full Portlet Guide]]

## Portals

### Servlet vs. portlet deployment

In addition to deploying Orbeon Forms into a servlet container such as Tomcat, you can deploy it into the Liferay portal using the Orbeon Forms portlets.

Portals provide features such as:

* __Content aggregation__:  A single web page aggregates the output or user interface of several data sources or applications.
* __Personalization__: Users or administrators of the portal can customize the user interface. This often means not only customizing the look and feel, but also selecting a certain set of available functionalities within the application.
* __Single sign-On__: The user logs in only once to access several applications in the portal.

### Status of portlet support in Orbeon Forms

As of Orbeon Forms 4.9, portlet support is tested with Liferay 6.0 and 6.1. We recommend Liferay 6.1 GA2 or newer.

The following Form Runner limitations are known within portals:

* Form Builder proper is not supported (but you can run forms designed with Form Builder).
* The "noscript" mode is not supported.

### Form Runner proxy portlet or full portlet?

The full portlet is not specifically tied to Form Runner and does not provide Form-Runner-specific features. Using the full portlet usually requires setting a custom landing page and possibly other custom configurations.
 
The Form Runner proxy portlet is designed to work with Form Runner only and to support a deployment mode where Form Runner can be installed within a separate application container, possibly on a separate server.

You can configure the proxy portlet via UI, making it is easier to create a setup with multiple forms on different portal pages.

Here is a comparison of the two options:

|                                      |Proxy Portlet|Full Portlet
|--------------------------------------|-------------|------------
|Runs Form Runner forms                |yes          |yes
|Runs custom XForms applications       |no           |yes
|Requires custom landing page          |no           |yes
|Specifically designed for Form Runner |yes          |no
|Allows remote Form Runner             |yes          |no
|Options configurable via UI           |yes          |no
|Form selection via UI                 |yes          |no
|Form selection via URL parameters     |yes          |no
|Can use portal language preference    |yes          |no