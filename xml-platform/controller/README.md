# Page Flow Controller

<!-- toc -->

## Introduction to the Page Flow Controller

![][1]

The Orbeon Forms Page Flow Controller (PFC) is the heart of your Orbeon Forms web application. It dispatches incoming user requests to individual pages built out of models and views, following the model / view / controller (MVC) architecture.

The PFC is configured with a file called a _page flow_. A page flow not only describes the pages offered by your application, it also declaratively describes an entire application's navigation logic, allowing the development of pages and XML services completely independently from each other.

The PFC encourages designing applications with a total separation between:

* **Site Logic** or **Page Flow**: when, and how to navigate from one page to another.
* **Page Logic** (the MVC page model): how data entered by the user is processed (for example validated, then fed to a backend), and how data is retrieved from a backend.
* **Page Layout** (the MVC page view): how information is displayed and presented to the user.
* **Site Presentation**: the layout and look and feel common to all pages in the web application or the web site, e.g.: site navigation menus, headers and footers, table backgrounds, or number formatting.

_NOTE: By default, the PFC is configured in `web.xml` as the main processor for the Orbeon Forms servlet and portlet. However, you are not required to use the PFC with Orbeon Forms: you can define your own main processor for servlets and portlets, as documented in [Packaging and Deployment][2]. For most web applications, the PFC should be used._

## Compatibility note

Starting Orbeon Forms 4.0, the following elements and attributes are introduced:

* `controller` element: replaces the `config` element`
* `path` attribute: replaces the `path-info` attribute
* `mediatype` attribute: replaces the `mime-type` attribute

If you are using builds prior to that date, including Orbeon Forms 3.9 and 3.9.1, you must use the old attributes. Starting Orbeon Forms 4.0, you should use the new attributes.

[1]: ../../images/legacy/reference-controller-oxf-app.png
[2]: http://wiki.orbeon.com/forms/doc/developer-guide/integration-packaging#main-processor
