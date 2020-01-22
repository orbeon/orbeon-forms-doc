# Form Runner JavaScript Embedding API

## Availability

Since Orbeon Forms PE 2020.1. As of 2020.1, this feature is *experimental*: the API is very much subject to change, and we're interested in your feedback so we can continue to improve this feature.

## Rationale

Server-side application using Java can use the 

## Usage

### Setup forwarding

If you're using the JavaScript embedding API, chances are that your application isn't running on Java. This means that Orbeon Forms and your application might be running on different servers, and if on the same server, will be running on different ports.

All the requests made by browser, whether for the page of your app that uses the embedding API, or for Orbeon Forms resources, will made to the same server and port. So it is your responsibility to setup that server so requests to Orbeon Forms are forwarded to the Orbeon Forms server, as shown in the diagram below.

![Network setup](images/javascript-api-network.png)

#### Requests to forward

You can identify the requests made to Orbeon Forms based on their path, which is typically `/orbeon`. (With Java web apps, that first part of the path is referred to as the "context", and you can deploy Orbeon Forms on context other than `/orbeon`, say `/forms`. However, in what follows, we'll just assume you've kept `/orbeon`.)

#### Forwarding the `JSESSIONID` cookie

When forwarding HTTP requests, you need to make sure the `JSESSIONID` cookie is properly forwarded. You can for instance check this with the Chrome Dev Tools using the Network tab. Make sure that:

1. The first time the browser makes a request to Orbeon Forms, that is with a path starting with `/orbeon`, the response sets `JSESSIONID` cookie.
2. In every subsequent request made to Orbeon Forms, that `JSESSIONID` cookie set earlier is sent by the browser, and the server doesn't in turn set another `JSESSIONID` in the response. (I.e. the value of the `JSESSIONID` cookie sent by the browser to the server shouldn't change for the duration of the session.)

### Include Orbeon Forms' embedding JavaScript

In the page where you want to embed a form, include the following JavaScript by adding this element inside the page's `<head>`:

```html
<script 
    type="text/javascript" 
    src="/orbeon/xforms-server/baseline.js?updates=fr"></script>
````

### Call the Form Runner API

You you embed a form in the page by calling the following API:

```javascript
ORBEON.fr.API.embedForm(
  container   : html.Element,
  context     : String,
  app         : String,
  form        : String,
  action      : String,
  documentId  : js.UndefOr[String],
  queryString : js.UndefOr[String]
)
```

| Parameter | Optional | Type | Description                 |
| --------- | -------- | ---- | --------------------------- |
| container | No       | HTML element | Element in the DOM that you want                      the form to be place in |
| context   | No       | String | Context where Orbeon Forms is deployed, typically `"/orbeon"` |
| app   | No       | String | Name of the app, e.g. `"human-resources"` |
| form   | No       | String | Name of the form, e.g. `"job-application"` |


