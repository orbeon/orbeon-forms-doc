# Form Runner JavaScript Embedding API

## Availability

The Form Runner JavaScript Embedding API first shipped with Orbeon Forms PE 2020.1, and as of 2020.1, this feature is *experimental*: the API is very much subject to change, and we're interested in your feedback so we can continue to improve this feature.

## Rationale

If you have your own application and would like to embed a form created with Form Builder:

- If you have a Java web app, we recommend you use the [Java Embedding API](java-api.md).
- If you are using Liferay, we recommend you use the [Liferay proxy portlet](liferay-proxy-portlet.md).
- In all other cases, we recommend you use the JavaScript Embedding API described on this page. It offers the most flexibility, and will work irrelevant of the server-side technology or client-side libraries you are using.

## Usage

### Forwarding

If you're using the JavaScript embedding API, chances are that your application isn't Java on the server. This means that Orbeon Forms and your application might be running on different servers, and if on the same server, will be running on different ports.

All browser requests, whether for the page of your app that uses the embedding API, or for Orbeon Forms resources, will made to the same server and port. It is your responsibility to setup that server so requests to Orbeon Forms are forwarded to the Orbeon Forms server, as shown in the diagram below. Exactly how to do so will depend on the server-side technology you are using. For instance:
 
- If you're using the Apache HTTP Server, this can be done with the [mod_rewrite module](https://httpd.apache.org/docs/current/mod/mod_rewrite.html).
- If you're using Microsoft IIS, you configure this with the IIS Manager, by creating a Reverse Proxy rule. 

![Network setup](images/javascript-api-network.png)

#### Requests to forward

You can identify the requests made to Orbeon Forms based on their path, which is typically `/orbeon`. (With Java web apps, that first part of the path is referred to as the "context", and you can deploy Orbeon Forms on context other than `/orbeon`, say `/forms`. However, in what follows, we'll just assume you've kept `/orbeon`.)

#### Forwarding the `JSESSIONID` cookie

When forwarding HTTP requests, you need to make sure the `JSESSIONID` cookie is properly forwarded. You can for instance check this with the Chrome Dev Tools using the Network tab. Make sure that:

1. The first time the browser makes a request to Orbeon Forms, that is with a path starting with `/orbeon`, the response sets `JSESSIONID` cookie.
2. In every subsequent request made to Orbeon Forms, that `JSESSIONID` cookie set earlier is sent by the browser, and the server doesn't in turn set another `JSESSIONID` in the response. (I.e. the value of the `JSESSIONID` cookie sent by the browser to the server shouldn't change for the duration of the session.)

### JavaScript to include

In the page where you want to embed a form, include the following JavaScript by adding this element inside the page's `<head>`:

```html
<script 
    type="text/javascript" 
    src="/orbeon/xforms-server/baseline.js?updates=fr"></script>
````

### `embedForm()` API

You embed a form in the page by calling the following API:

```javascript
ORBEON.fr.API.embedForm(
  container,  
  context,    
  app,        
  form,       
  action,     
  documentId, 
  queryString
);
```

| Parameter   | Optional  | Type         | Example             | Description                                                   |
| ----------- | --------- | ------------ | ------------------- | ------------------------------------------------------------- |
| container   | No        | HTML element |                     | DOM element you want the form to be placed in                 |
| context     | No        | String       | `"/orbeon"`         | Context where Orbeon Forms is deployed, typically             |
| app         | No        | String       | `"human-resources"` | App name                                                      |
| form        | No        | String       | `"job-application"` | Form name                                                     |
| action      | No        | String       | `"new"`             | Either `"new"`, `"edit"`, or `"view"`                         |
| documentId  | See below | String       |                     | For actions other than `new`, the document to be loaded       |
| queryString | Yes       | String       | `"job=clerk"`       | Additional parameters to pass to the form as query parameters |    

The `documentId` parameter is mandatory for actions other than `new`, and must be `undefined` when the action is `new`. For `new`, if you don't need to pass a `queryString`, you can just omit the last 2 parameters in your call to `ORBEON.fr.API.embedForm()`, and if you need to pass a `queryString` then you must explicitly pass `undefined` as the value of `documentId`.

### `destroyForm()` API

To remove a form that you embedded earlier, call:

```
ORBEON.fr.API.destroyForm(container);
```

If you want to replace a form A shown in a given container by an other form B, you can just do so by calling `ORBEON.fr.API.embedForm()` a second time for form B, and don't need to explicitly first call `destroyForm()`.