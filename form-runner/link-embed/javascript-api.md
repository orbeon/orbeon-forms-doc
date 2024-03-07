# Form Runner JavaScript Embedding API

## Availability

- [SINCE Orbeon Forms 2020.1]
- The Form Runner JavaScript Embedding API is a PE feature.

## Rationale

If you have your own application and would like to embed a form created with Form Builder:

- If you have a Java web app, we recommend you use the [Java Embedding API](java-api.md).
- If you are using Liferay, we recommend you use the [Liferay proxy portlet](liferay-proxy-portlet.md).
- If you are using React, we recommend you use the [React component](react-component.md).
- In all other cases, we recommend you use the JavaScript Embedding API described on this page. It offers the most flexibility, and will work irrelevant of the server-side technology you are using.

## Options

When using the JavaScript embedding API, the browser must be able to communicate with both your app server running the web application from which you are doing the embedding, and the Orbeon Forms server. Your app server and the Orbeon Forms server are probably running on different servers or on different ports, so running on different origins (the combination of the scheme , e.g. `http`, `https`, the host and the port). Cross-origin requests, where a given web page makes requests to different origins, are possible but are restricted by browsers due to potential security risks such as cross-site request forgery and cross-site script inclusion. You can deal with this situation by either avoiding cross-origin requests by having the browser always talk to a single server and having that server forward requests to Orbeon Forms accordingly (see "Option 1: Forwarding" below), or by doing the necessary setup to allow cross-origin requests (see "Option 2: Cross-origin" below).

![Network setup](images/javascript-api-network.png)

### Option 1: Forwarding

#### Deployment

With this setup, all browser requests, whether for the page of your application using the embedding API or for Orbeon Forms, will be made to the same origin (scheme, server and port). It is your responsibility to configure this server so that requests to Orbeon Forms are forwarded to the Orbeon Forms server.

Exactly how to do this depends on the server-side technology you are using. For example, if you're using the Apache HTTP Server, this can be done with the [mod_rewrite module](https://httpd.apache.org/docs/current/mod/mod_rewrite.html), and if you're using Microsoft IIS, you configure this using the IIS Manager by creating a Reverse Proxy rule.

You can identify the requests you need to forward by their path, which is typically `/orbeon`. In Java web applications, this first part of the path is called the "context", and you can deploy Orbeon Forms in a context other than `/orbeon`, such as `/forms`, but in the following we'll just assume that you've kept `/orbeon`. When forwarding HTTP requests, you need to make sure that the `JSESSIONID` cookie is forwarded correctly. You can check this using the Network tab in the Chrome Dev Tools. Make sure this is the case:

1. The first time the browser makes a request to Orbeon Forms, that is with a path starting with `/orbeon`, the response sets `JSESSIONID` cookie.
2. In every subsequent request made to Orbeon Forms, that `JSESSIONID` cookie set earlier is sent by the browser, and the server doesn't in turn set another `JSESSIONID` in the response. (I.e. the value of the `JSESSIONID` cookie sent by the browser to the server shouldn't change for the duration of the session.)

#### Users and authentication

Users will be accessing your application, so you can continue to authenticate them as usual. If you're only requiring authentication for certain paths, make sure you include everything under `/orbeon`. If you don't require users to be authenticated to access that path, they may be able to bypass the authentication you've set up for your application, say under `/app`, and instead access Orbeon Forms directly, making requests to paths under `/orbeon`.

If your users are authenticated, you'll probably also want Orbeon Forms to know who the current user is, so that Orbeon Forms can [control access to forms and enforce permissions](/form-runner/access-control/README.md). In the context of the JavaScript embedding API, this is typically done by having your redirect code pass information about the current user to Orbeon Forms using headers, and setting up Orbeon Forms to use this information it receives in what is called the [header-driven method](/form-runner/access-control/users.md#header-driven-method) (you will find all the details on which headers you need to pass, and how to set up Orbeon Forms to use header-based authentication on this page).

### Option 2: Cross-origin

[SINCE Orbeon Forms 2022.1.5]

#### Deployment

When calling `embedForm()`, the value of the `context` parameter must be the full URL of the Orbeon Forms server (like `https://forms.example.org/orbeon`), not a relative URL (like `/orbeon`).

For the Orbeon Forms server to respond with the appropriate CORS headers, and to support preflight requests, install and configure the UrlRewriteFilter as follows. If you already have a piece of software active as a reverse proxy in front of Orbeon Forms, feel free to use it, to achieve the same result.  

1. Place the UrlRewriteFilter jar in the Orbeon Forms `WEB-INF/lib`.
    - If your container implements the Servlet 4.0 API (or earlier), like Tomcat 9 (and earlier):
        - Use [`urlrewritefilter-4.0.3.jar`](https://repo1.maven.org/maven2/org/tuckey/urlrewritefilter/4.0.3/urlrewritefilter-4.0.3.jar).
        - Using version 5.x of the UrlRewriteFilter will result in the error `java.lang.NoClassDefFoundError: jakarta/servlet/Filter` because it is designed for containers that implement the Servlet 5.0 API (or later).
    - If your container implements the Servlet 5.0 API (or later), like Tomcat 10 (and later):
        - Use [`urlrewritefilter-5.1.3.jar`](https://repo1.maven.org/maven2/org/tuckey/urlrewritefilter/5.1.3/urlrewritefilter-5.1.3.jar).
        - Using version 4.x of the UrlRewriteFilter will result in the error `java.lang.NoClassDefFoundError: javax/servlet/Filter` because it is designed for containers that implement the Servlet 4.0 API (or earlier).
        - Orbeon Forms has supported such containers since Orbeon Forms 2023.1.
2. Edit the `WEB-INF/web.xml` to add the following `<filter>` and `<filter-mapping>`.
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
3. Create a `WEB-INF/urlrewrite.xml` file with the following content.    
    ```xml
    <?xml version="1.0" encoding="utf-8"?>
    <!DOCTYPE urlrewrite PUBLIC "-//tuckey.org//DTD UrlRewrite 4.0//EN" "http://www.tuckey.org/res/dtds/urlrewrite4.0.dtd">
    <urlrewrite>
        <rule>
            <set type="response-header" name="Access-Control-Allow-Origin">https://app.orbeon.com</set>
            <set type="response-header" name="Access-Control-Allow-Credentials">true</set>
            <set type="response-header" name="Access-Control-Allow-Methods">*</set>
            <set type="response-header" name="Access-Control-Allow-Headers">orbeon-client, content-type</set>
        </rule>
        <rule>
            <condition type="method">OPTIONS</condition>
            <set type="status">200</set>
            <to>null</to>
        </rule>
    </urlrewrite>
    ```

#### Users and authentication

When users authenticate to your application server, it will typically set a cookie, such as `UserToken`, that allows it to keep track of who the user is. After a user successfully authenticates, the application server will typically set `UserToken` to a given value, and when it gets that token back on a subsequent request, it can know that the user authenticated and who the user is.

This token will also be sent by the browser to the Orbeon Forms server. If you want Orbeon Forms to know who the user is, or if you want to prevent it from handling unauthenticated requests, you will need to set up a component running before Orbeon Forms, such as a reverse proxy, to use the `UserToken` cookie and to pass information about the current user to Orbeon Forms, typically using the [header-driven method](/form-runner/access-control/users.md#header-driven-method). This component can be a servlet filter or a reverse proxy.

## JavaScript to include

In the page where you want to embed a form, include the following JavaScript by adding this element inside the page's `<head>`:

```html
<script 
    type="text/javascript" 
    src="/orbeon/xforms-server/baseline.js?updates=fr"></script>
```

## API

### `embedForm()`

You embed a form in the page by calling the following API:

```javascript
ORBEON.fr.API.embedForm(
    container,  
    context,    
    app,        
    form,       
    mode,     
    documentId, 
    queryString,
    headers
);
```

| Parameter   | Optional          | Type         | Example                         | Description                                                   |
|-------------|-------------------|--------------|---------------------------------|---------------------------------------------------------------|
| container   | No                | HTML element |                                 | DOM element you want the form to be placed in                 |
| context     | No                | String       | `"/orbeon"`                     | Context where Orbeon Forms is deployed, typically `"/orbeon"` |
| app         | No                | String       | `"human-resources"`             | App name                                                      |
| form        | No                | String       | `"job-application"`             | Form name                                                     |
| mode        | No                | String       | `"new"`                         | Either `"new"`, `"edit"`, or `"view"`                         |
| documentId  | See point 1 below | String       |                                 | For modes other than `new`, the document to be loaded         |
| queryString | Yes               | String       | `"job=clerk"`                   | Additional query parameters                                   |
| headers     | Yes               | [Headers][h] | `new Headers({ 'Foo': 'bar' })` | Additional HTTP headers; see point 2 below                    |

1. The `documentId` parameter is mandatory for modes other than `new`, and must be `undefined` when the mode is `new`. For `new`, if you don't need to pass any of the parameters after `documentId`, you can just omit the `documentId` and all subsequent parameters in your call to `ORBEON.fr.API.embedForm()`; otherwise, you must explicitly pass `undefined` as the value of `documentId`.
2. The `headers` parameter is supported [SINCE Orbeon Forms 2021.1.1]. You can also access the value of such headers in the form you're embedding with the function [`xxf:get-request-header()`](/xforms/xpath/extension-http.md#xxfget-request-header).

[SINCE Orbeon Forms 2022.1]

`embedForm()` returns a JavaScript `Promise` object. This allows you to know when the embedding has succeeded or failed. For example:

```javascript
ORBEON.fr.API.embedForm(
    document.getElementById("my-container-element"),
    "/orbeon",
    "human-resources",
    "job-application",
    "new"
)
.then(() => {
    console.log("`embedForm()` successfully loaded the form");
})
.catch((e) => {
  console.log("`embedForm()` returned an error");
  console.log(e);
});
```

Note that with earlier versions, `embedForm()` always returned the JavaScript `undefined` value.

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

`embedForm()` returns a JavaScript `Promise` object representing the form. The object supports functions documented in [The `FormRunnerForm` object](/form-runner/api/other/form-runner-javascript-api.md#the-formrunnerform-object).

See also:

- [The `FormRunnerForm` object](/form-runner/api/other/form-runner-javascript-api.md#the-formrunnerform-object)
- [`callback()` action](/form-runner/advanced/buttons-and-processes/actions-form-runner.md#callback)

### `destroyForm()`

To remove a form that you embedded earlier, call:

```
ORBEON.fr.API.destroyForm(container);
```

If you want to replace a form A shown in a given container by another form B, you can just do so by calling `ORBEON.fr.API.embedForm()` a second time for form B, and don't need to explicitly first call `destroyForm()`.

[SINCE Orbeon Forms 2022.1] Like `embedForm()`, `destroyForm()` returns a JavaScript `Promise` object. See the above section about `embedForm()` for more information about how to use the returned `Promise`. 

## Limitations

The JavaScript embedding API has the same [limitations as the Java embedding API](java-api.md#limitations).

## See also

- [XForms JavaScript API](/xforms/client-side-javascript-api.md)
- [Form Runner JavaScript API](/form-runner/api/other/form-runner-javascript-api.md)

[h]: https://developer.mozilla.org/en-US/docs/Web/API/Headers
