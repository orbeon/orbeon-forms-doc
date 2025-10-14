# Form Runner Liferay Proxy Portlet

## Availability

This is an Orbeon Forms PE feature.

## Supported Liferay versions

See [Supported Liferay versions](README.md#supported-liferay-versions).

## Known Liferay 7/DXP issues

These issues apply to Orbeon Forms 2017.1 to 2019.1.

- __The Form Runner language selector doesn't work__
    - See [issue #3257](https://github.com/orbeon/orbeon-forms/issues/3257)
    - Workaround: Select the "Send Liferay language" option. When this option is sent, the Form Runner language selector doesn't show. Orbeon Forms uses instead the current Liferay user's language preference.
- __The Liferay navigation menu doesn't navigate to Form Runner__
    - See [issue #3256](https://github.com/orbeon/orbeon-forms/issues/3256)
    - Workarounds:
        1. Don't use the Liferay navigation menu to reach the Form Runner proxy portlet.
        2. Or disable Liferay's SPA support portal-wide (see the [Disabling SPA](https://dev.liferay.com/develop/tutorials/-/knowledge_base/7-0/automatic-single-page-applications#disabling-spa)).
        
We plan to lift these limitations in a future release of Orbeon Forms.

## Introduction

Form Runner can be deployed [directly](../../form-runner/link-embed/liferay-full-portlet.md) into Liferay. However in some cases, it is desirable to deploy Form Runner *separately*. The Form Runner proxy portlet provides this type of deployment, with the following benefits:

- You configure a specific form to show in the portlet via portlet preferences.
- Form Runner can be maintained and upgraded separately from the portal.
- The Form Runner application itself does not have to be exposed to the outside world.

This guide describes how to install, administer and use the Form Runner proxy portlet.

![Controls form through the Liferay proxy portlet](images/liferay-proxy-controls-liferay7.png)

## Security considerations

{% hint style="warning" %}
Even though you can configure the proxy portlet to show a give app and form, the proxy portlet by default only delegates to Form Runner. It is important that you secure Form Runner with proper form permissions, in particular.
{% endhint %}

[\[SINCE Orbeon Forms 2024.1.3\]](/release-notes/orbeon-forms-2024.1.3.md)

The proxy portlet now performs filtering of incoming paths, and rejects access to disallowed pages as follows:

| Configuration  | Landing Page | Summary Page | New Page | Edit Page | View Page | Other Pages | Notes                |
|----------------|--------------|--------------|----------|-----------|-----------|-------------|----------------------|
| _Home Page_    | ✅            | ✅            | ✅        | ✅         | ✅         | ❌           | all forms            |
| _Summary Page_ | ❌            | ✅            | ✅        | ✅         | ✅         | ❌           | configured form only |
| _New Page_     | ❌            | ❌            | ✅        | ❌         | ❌         | ❌           | configured form only |

For example, if you configure the proxy portlet as follows:

- __Form Runner Page__: New Page
- __Form Runner app name__: `acme`
- __Form Runner form name__: `sales`

The user:

- will be shown the `acme`/`sales` form's New Page
- will not have access, through links and navigation, to the Summary Page, Landing Page, or other Form Runner pages
- will not have access to the New Page or other pages of other forms

{% hint style="warning" %}
It is still possible for Form Runner to be configured to navigate to other pages through processes and actions. If you configure custom processes and actions that can take the user to such pages, enabling form access permissions should be strongly considered.
{% endhint %}

## Architecture

The solution is comprised of:

- __The proxy portlet:__ a Liferay portlet (JSR-286 portlet) available as a portlet application to deploy into Liferay
- __The Form Runner server:__ an Orbeon Forms instance deployed as a web application into a servlet container

The proxy portlet communicates with the Form Runner server using HTTP.

The Form Runner server can be entirely hidden behind a firewall as it doesn't need direct access from the user's web browser.

## Installation

[Install](../../installation/README.md) and deploy Orbeon Forms as usual. You can deploy it:

- within the same container as Liferay
- in a separate servlet container

Note the URL at which Form runner is deployed. The default is assumed to be:

    http://localhost:8080/orbeon/

### Configure Form Runner

For Orbeon Forms 4.0 and newer, no particular configuration is needed.

### Deploy the proxy portlet

1. Deploy `orbeon-proxy-portlet.war` or `proxy-portlet.war` (formerly `orbeon-PE-proxy-portlet.war`) into Liferay
2. Add an instance of Orbeon Forms → Form Runner to a page:

    <img alt="" src="images/liferay-applications.png" width="350">

3. The portlet will load with its default configuration. If Orbeon Forms is deployed at the default address, a form shows.

    If Orbeon Forms is _not_ deployed at the default address, the proxy portlet will initially show an error. You can
    ignore this error and proceed to the proxy portlet configuration below.

### Configure the proxy portlet

1. Open the portlet preferences page

    ![](images/liferay-proxy-open-preferences.png)

2. Configure the preferences

    ![](images/liferay-proxy-preferences.png)

    The preferences are as follows:

    - __Form Runner Page__
        - Initial Form Runner action (page) to show
        - Possible values
            - _New Page_: show the "new" page for the given app/form
            - _Summary Page_: show the "summary" page for the given app/form
            - _Home Page_: Form Runner Home page showing the form list [SINCE Orbeon Forms 4.4]
    - __Form Runner URL__
        - URL, including servlet context, where the Form Runner instance is deployed
        - Example: `http://localhost:8080/orbeon/`
    - __Form Runner app name__
        - Initial Form Runner application name to show
        - Example: `orbeon`
    - __Form Runner form name__
        - Initial Form Runner form name to show
        - Example: `controls`
    - __Readonly access__
        - Whether the user is able to edit forms
        - To ensure read-only access, it is also important to configure a `oxf.fr.detail.buttons.view.*.*` property without the `workflow-edit` button.
    - __Send Liferay language__ [SINCE Orbeon Forms 4.4]
        - Whether Form Runner should use the Liferay user's language
        - This also hides the Form Runner language selector
    - __Send Liferay user__ [SINCE Orbeon Forms 4.4]
        - Whether the Liferay user information is sent to Form Runner
        - This sends the following headers to Form Runner
            - `Orbeon-Liferay-User-Id`
            - `Orbeon-Liferay-User-Screen-Name`
            - `Orbeon-Liferay-User-Full-Name`
            - `Orbeon-Liferay-User-First-Name` [SINCE Orbeon Forms 2018.1]
            - `Orbeon-Liferay-User-Middle-Name` [SINCE Orbeon Forms 2018.1]
            - `Orbeon-Liferay-User-Last-Name` [SINCE Orbeon Forms 2018.1]
            - `Orbeon-Liferay-User-Email`
            - `Orbeon-Liferay-User-Group-Id`
            - `Orbeon-Liferay-User-Group-Name`
            - `Orbeon-Liferay-User-Roles`

    When read-only access is enabled, if the Form Runner summary page is enabled and accessed, selecting a form takes the user to the Review page instead of the Edit page for a given form.

    Press the "Save" or "Cancel" button to save/cancel and return to the portlet.

    The default values of the preferences are provided via initialization parameters in the `portlet.xml` file. The latest version of this file is [available here][8].

### Configuring header and URL parameter forwarding

[SINCE Orbeon Forms 4.1]

Specific client URL parameters and client request headers can be forwarded to Form Runner with the following portlet init parameters:

```xml
<init-param>
    <name>forward-parameters</name>
    <value>param1 param2</value>
</init-param>
<init-param>
    <name>forward-headers</name>
    <value>My-Header-1 My-Header-2</value>
</init-param>
```

The value of the parameters is  a space-separated list of parameter names or header names.

This makes these headers and URL parameters available to Form Runner, for example with the `xxf:get-request-header()` and `xxf:get-request-parameter()` functions.

[SINCE Orbeon Forms 4.2]

Headers forwarded now follow the capitalization specified in the `forward-headers` property. For example, if the incoming header has name `mY-hEaDeR-1,` and the property specifies `My-Header-1`, the header will be forwarded under the name `My-Header-1`.

[SINCE Orbeon Forms 4.10]

You can forward portlet request properties as headers to Form Runner.

```xml
<init-param>
    <name>forward-properties</name>
    <value>My-Property-1 My-Property-2</value>
</init-param>
```

This is useful for example to set custom security headers in the proxy portlet:

- create a portlet filter overriding `getPropertyNames`, `getProperty`, and `getProperties` to add custom properties (see an example of wrapper in Scala [here](https://github.com/orbeon/orbeon-forms/blob/master/portlet-support/src/main/scala/org/orbeon/oxf/portlet/wrappers.scala) and examples of filters [here](https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/scala/org/orbeon/oxf/portlet/liferay/FormRunnerAuthFilter.scala))
- use the `forward-properties` parameter to specify that the proxy portlet must send the names and values of these properties as headers to Form Runner

### Preserving portal parameters

[SINCE Orbeon Forms 2018.1]

Portal URL parameters specified with the `keep-parameters` portlet init parameter are read from the portal URL and
re-appended to action, render and resource URLs produced by the proxy portlet: 

```xml
<init-param>
    <name>keep-parameters</name>
    <value>param1 param2</value>
</init-param>
```

### Configuring Form Runner to use Liferay user information

[SINCE Orbeon Forms 4.4]

When "Send Liferay user" is enabled, you can configure Form Runner to use the HTTP headers sent by the proxy portlet to handle forms access control. For this, you must use the "Header-driven method". Set the following property:

```xml
<property
    as="xs:string"
    name="oxf.fr.authentication.method"
    value="header"/>
```

In addition to this, depending on the version of Orbeon Forms you're using, set the properties listed in the following sections.

#### Since Orbeon Forms 2016.3

```xml
<property
    as="xs:string"
    name="oxf.fr.authentication.header.credentials"
    value="Orbeon-Liferay-User-Credentials"/>
```

In Liferay, you can configure whether users authenticate using their email, user id, or screen name. The Orbeon Forms proxy portlet takes the username to be the user's email, user id, or screen name, depending on that configuration.

#### From Orbeon Forms 4.4 to 2016.2

```xml
<property
    as="xs:string"
    name="oxf.fr.authentication.header.username"
    value="Orbeon-Liferay-User-Screen-Name"/>
    
<property
    as="xs:string"
    name="oxf.fr.authentication.header.group"
    value="Orbeon-Liferay-User-Group-Name"/>
    
<property
    as="xs:string"
    name="oxf.fr.authentication.header.roles"
    value="Orbeon-Liferay-User-Roles"/>
```

For the `oxf.fr.authentication.header.username` header, you can choose any of the Liferay headers associated with the user, but typically this will be:

- `Orbeon-Liferay-User-Id`
- `Orbeon-Liferay-User-Screen-Name`
- or possibly `Orbeon-Liferay-User-Email`

Prior to Orbeon Forms 4.9, you need the following configuration in your [properties](../../configuration/properties/README.md):

```xml
<property
    as="xs:string"
    name="oxf.http.forward-headers"
    value="Orbeon-Liferay-User-Screen-Name Orbeon-Liferay-User-Group-Name Orbeon-Liferay-User-Roles"/>
    
<property
    as="xs:string"
    name="oxf.xforms.forward-submission-headers"
    value="Orbeon-Liferay-User-Screen-Name Orbeon-Liferay-User-Group-Name Orbeon-Liferay-User-Roles"/>
```

### Enabling form selection via URL parameters

[SINCE Orbeon Forms 4.7]

You can enable form selection via URL parameters by setting the `enable-url-parameters` portlet parameter in `portlet.xml` to `true`:

```xml
<init-param>
    <name>enable-url-parameters</name>
    <value>true</value>
</init-param>
```

When this is enabled, the following portal URL parameters are propagated to Orbeon Forms to allow form selection:

- `orbeon-app`: the Form Runner app name
- `orbeon-form`: the Form Runner form name
- `orbeon-document`: the Form Runner document (for `edit` and `view` pages)
- `orbeon-page`: the Form Runner page (`new`, `edit`, or `view`)
- `orbeon-draft`
    - [\[SINCE Orbeon Forms 2023.1.1\]](/release-notes/orbeon-forms-2023.1.1.md)
    - when `orbeon-document` is set, whether the document is a draft or not (`true` or `false`)

### Disabling short namespaces

[SINCE Orbeon Forms 2019.2]

Short namespaces are no longer supported and therefore entirely disabled.

[SINCE Orbeon Forms 2017.2.3 and 2018.1]

Short namespaces are disabled by default in `portlet.xml`:

```xml
<init-param>
    <name>use-short-namespaces</name>
    <value>false</value>
</init-param>
```

We recommend disabling this feature as it is known to cause issues. See [#3682](https://github.com/orbeon/orbeon-forms/issues/3682).

[SINCE Orbeon Forms 2016.2]

You can disable the use of short namespaces by setting the `use-short-namespaces` portlet parameter in `portlet.xml` to `false`:

```xml
<init-param>
    <name>use-short-namespaces</name>
    <value>false</value>
</init-param>
```

By default (and this was the case in previous Orbeon Forms versions as well), short namespaces are enabled to reduce the size of the resulting HTML page and browser DOM.

However, in some cases of load balancing, this can be an issue. In such cases, you can disable short namespaces with this option. See [issue #2764](https://github.com/orbeon/orbeon-forms/issues/2764) for details.

### HTTP client configuration

[SINCE Orbeon Forms 4.7]

This is the same as the [server-side embedding configuration](java-api.md).

Be aware that `web.xml` uses `<param-name>` and `<param-value>`, but `portlet.xml` uses `<name>` and `<value>`.

## Securing Form Runner with an IP filter

### Rationale

The proxy portlet can be secured with Liferay. On the other hand, the Form Runner server runs as a servlet and whether it runs within the same container as Liferay or on a different machine, direct accesses from end clients must be prevented.

One first step to achieve this is to place the Form Runner server behind a firewall, where it can be accessed from the portal but not from end clients.

Additional security steps can be desirable depending on your environment. One such step is to use an IP filter in order to:

- Allow accesses from the proxy portlet and from Form Runner itself.
- Disallow accesses from any other sources.

The following describes such a configuration.

### Example configuration

There are many IP filter options out there. One option is to use the very flexible [UrlRewriteFilter][9]. You can configure it this way:

#### Put urlrewrite-3.2.0.jar in the Orbeon Forms WEB-INF/lib folder

#### Configure the filter in Orbeon Forms WEB-INF/web.xml

```xml
<filter>
    <filter-name>UrlRewriteFilter</filter-name>
    <filter-class>org.tuckey.web.filters.urlrewrite.UrlRewriteFilter</filter-class>
</filter>
<filter-mapping>
    <filter-name>UrlRewriteFilter</filter-name>
    <url-pattern>/*</url-pattern>
    <dispatcher>REQUEST</dispatcher>
    <dispatcher>FORWARD</dispatcher>
</filter-mapping>
```

This is a generic configuration which enables the filter for any incoming path.

#### Put the `urlrewrite.xml` configuration in the Orbeon WEB-INF folder

Here is an example configuration:

```xml
<?xml version="1.0" encoding="utf-8"?>
<urlrewrite>
    <rule>
        <condition type="remote-addr" operator="notequal">0:0:0:0:0:0:0:1%0</condition>
        <condition type="remote-addr" operator="notequal">127.0.0.1</condition>
        <set type="status">403</set>
        <to>null</to>
    </rule>
</urlrewrite>
```

This configuration encodes the following rule: "for any incoming path, if the remote IP is different from `0:0:0:0:0:0:0:1%0` (IPv6) and different from `127.0.0.1` (IPv4), then set the status to 403 and redirect to the path /unauthorized".

You should modify the IP address(es) to fit your network configuration, as the originating address might not be `127.0.0.1`, as well as modify the redirection path.

Enabling UrlRewriteFilter's logging is helpful to see what's happening during development. To do this, modify the `` config to:

```xml
<filter>
    <filter-name>UrlRewriteFilter</filter-name>
    <filter-class>org.tuckey.web.filters.urlrewrite.UrlRewriteFilter</filter-class>
    <init-param>
        <param-name>confReloadCheckInterval</param-name>
        <param-value>0</param-value>
    </init-param>
    <init-param>
        <param-name>logLevel</param-name>
        <param-value>DEBUG</param-value>
    </init-param>
</filter>
```

You could also set up filtering on the type "remote-host" and check on the value "localhost" instead of filtering on IP addresses, however often reverse DNS lookups are not enabled in servlet containers for performance reasons, which means you need to filter by IP address instead.

#### Test the setup

Make sure accesses from the proxy portlet work as before, and test that accesses from machines other than the portal and/or directly from the client browser get rejected.

## Scenarios

### Scenario: create and save form data

In this scenario, the user of the portlet is only allowed to capture and save form data.

Portlet preferences:

- __Form Runner action:__ `new`
- __Read-Only access:__ `false`

Configuration properties in `properties-local.xml` (here for the orbeon/contact form):

```xml
<property
    as="xs:string"
    name="oxf.fr.detail.buttons.orbeon.contact"
    value="clear submit"/>
<property
    as="xs:string"
    name="oxf.fr.detail.submit.buttons.orbeon.contact"
    value="clear"/>
```

### Scenario: list, edit, create and save form data

In this scenario, the user of the portlet can, in addition to capture and save, also list and edit submitted form data.

Portlet preferences:

- __Form Runner action:__ `summary`
- __Read-Only access:__ `false`

Configuration properties in `properties-local.xml` (here for the orbeon/contact form):

```xml
<property
    as="xs:string"
    name="oxf.fr.summary.buttons.orbeon.contact"
    value="new edit delete"/>
<property
    as="xs:string"
    name="oxf.fr.detail.buttons.orbeon.contact"
    value="close clear submit"/>
<property
    as="xs:string"
    name="oxf.fr.detail.submit.buttons.orbeon.contact"
    value="clear"/>
```

Remove the `delete` token if the Delete button is not needed.

### Scenario: list and review form data

In this scenario, the user of the portlet is only allowed to capture and save form data.

Portlet preferences:

- __Form Runner action:__ `summary`
- __Read-Only access:__ `true`

Configuration properties in `properties-local.xml` (here for the orbeon/contact form):

```xml
<property
    as="xs:string"
    name="oxf.fr.summary.buttons.orbeon.contact"
    value="print pdf"/>
<property
    as="xs:string"
    name="oxf.fr.detail.buttons.view.orbeon.contact"
    value="close"/>
```

## Performance tuning

See the Performance Tuning section of the [Full Portlet Guide](../../form-runner/link-embed/liferay-full-portlet.md#performance-tuning).

## Limitations

### Form Runner configuration properties are global

The Form Runner configuration properties are global, i.e. a given Form Runner server cannot have configuration properties per portlet (although it can have them per app/form name). This means for example that for a given app/form/action, it is not possible to have a different set of buttons.

Concretely, this means that you cannot have the  following portlet instances at the same time:

- `orbeon/contact` form in "list, edit, create and save form data" scenario
- `orbeon/contact` form in "list and review form data" scenario

This is because both scenarios need a different configuration for the summary page buttons.

## See also

- [Organizations](../../form-runner/access-control/organization.md)
