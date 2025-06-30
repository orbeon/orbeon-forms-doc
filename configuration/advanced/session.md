# Session Management

## Who should read this

You'll most likely be interested in the information on this page if:

- You are able to load a form produced by Orbeon Forms, but then Ajax requests sent by the browser occasionally or systematically get an HTTP 440 response.
- In your environment, requests from the browser don't immediately reach the app server running Orbeon Forms, but instead go through some other software. For instance, this is the case if you're using a reverse proxy or have your own code embedding forms produced by Orbeon Forms in your web pages.
- You want to learn more about session management in Orbeon Forms.

[BEFORE Orbeon Forms 2022.1.5], Orbeon Forms returned an HTTP 403 error code instead of 440 for this scenario. The rest of this page refers to the 440 code; that same information is valid with the 403 code if you are using an earlier version.

## `JSESSIONID` and `UUID`

### Normal operation

1. The first time a browser requests a web page from Orbeon Forms, Orbeon Forms creates a session, and the HTTP response has a header with `Set-Cookie: JSESSIONID=123`, where `123` is a unique identifier. (The specific cookie name may differ depending on how you configured your app server, but typically `JSESSIONID` is the default and for simplicity we'll use that name in the rest of this document.) From that point, any subsequent requests issued by the browser will have a header that looks like `Cookie: JSESSIONID=123`.

2. When Orbeon Forms generates a web page for a form, it produces a unique UUID, and stores *state* in the session related to this UUID. If the user reloads the form, Orbeon Forms generated a different UUID.

3. When the Orbeon Forms client-side code sends an Ajax request, it includes that UUID in the body of the request, and the browser passes the `JSESSIONID`. On the server, Orbeon Forms uses that information to find the *state* it stored in step 1. The UUID sent by Orbeon Forms in the Ajax request looks like:

    ```xml
    <xxf:uuid>abc</xxf:uuid>
    ```
    
### Orbeon Forms requirement

For Orbeon Forms to operate normally, for a given web page loaded by the browser from Orbeon Forms, the `JSESSIONID` set in the HTTP response produced by Orbeon Forms (with `Set-Cookie: …`), or if none is set the `JSESSIONID` on in the HTTP request received by Orbeon Forms must also be the one Orbeon Forms receives in all subsequent Ajax requests issued by that page.

![Which HTTP requests/responses we are interested in](../images/session-where.png)

Note that those requirements apply to the HTTP requests and responses sent to and coming from Orbeon Forms. As illustrated in the above diagrams, if you have reverse proxy or embedding code those will be different from the HTTP requests and responses made by and received by the browser, and the `JSESSIONID` (or equivalent) used between the browser and the proxy / embedding code is likely to be different from the `JSESSIONID` used between the proxy / embedding code and Orbeon Forms. Again, Orbeon Forms' requirement apply to the latter.

## HTTP 440

When Orbeon Forms receives an Ajax request (see the third step in the previous section), if it can't find the state associated to the UUID in the session, it responds with an [HTTP 440 error](https://en.wikipedia.org/wiki/List_of_HTTP_status_codes#4xx_client_errors) (Login Timeout).

### Normal cases

The following 2 cases can happen resulting in a 440 response, but those situations should happen very rarely:

1. The user's session has expired – As long as users keep a form open in their browser, by default the Orbeon Forms client-side code makes sure to send a "session heartbeat" Ajax request before the session expires to keep it alive. However:
    - It's possible for you to disable the [session heartbeat](state-handling.md#session-heartbeat) feature. If you do, it is possible for the session to expire even if users keep a browser window with the form open.
    - In situations where the browser looses connectivity to the server for an extended period of time, the session heartbeat mechanism might not be able to contact the server before the session expires. For instance, this could happen if a user were to load a form on her laptop, close the lid, commute home, open the lid, and try to continue filling out the same form.
2. A user loads the form, the server running Orbeon Forms is restarted, and then the user tries to interact with the form.

In both cases, if, after getting the error, users reload the form and get the error again, then you might have hit one of the "problematic cases" described in the section below.

### Problematic cases

If you're getting 440 errors more regularly, and you have software sitting between the browser and Orbeon Forms, it is a sign that this software does not handle cookies properly. That software can be:

- A reverse proxy, for load balancing, instance used to provide authentication, single sign-on, serve assets more efficiently, or provide security services.
- Code running in a web app used to embed a form produced by Orbeon Forms into a page produced by the web app.

In those cases, you can solve the issue by either:

- When using a [load balancer](clustering.md) that dispatches requests across multiple servers, ensure session affinity (sticky sessions) is configured so that the initial page load and all subsequent Ajax requests from the same page load are routed to the same server instance. Some load balancers may, even with session affinity configured, decide to send a subsequent request to a different server if they determine that the original target server is too slow to respond and might not be operational. Unless you have set up session replication, you must ensure that your load balancer configuration prevents this failover behavior from occurring.
- If you have own Java code to embed forms created with Form Builder, switch to using the built-in [Form Runner Java Embedding API](../../form-runner/link-embed/java-api.md), which will handle cookies properly.
- Debug and fix the issue is the said software, armed with a better understanding of the Orbeon Forms' requirements when it comes to cookies, based on this information on this page.
- If using Tomcat, in Tomcat's directory edit `conf/context.xml`, and add `sessionCookiePath="/"` on the root element, so it looks as follows: `<Context sessionCookiePath="/">`. This will make the job of any reverse proxy or embedding code you might have much simpler, and could help you get around bugs in that code.