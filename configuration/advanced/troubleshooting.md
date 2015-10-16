> [[Home]] ▸ [[Installation]]

## Session not found when running both Tomcat and WebLogic

This issue can also manifest itself with a dialog titled _Session has expired. Unable to process incoming request._ showing up as you try to interact with a form. This comes from the fact that Tomcat and WebLogic handle the `JSESSIONID` cookie used to track sessions differently:

* Tomcat creates one `JSESSIONID` per web application, with the cookie path set to the context of the application. When an application invalidates the session, Tomcat sends a new `JSESSIONID` to the browser.
* WebLogic stores one cookie `JSESSIONID` with cookie path `/` for all the applications. This cookie doesn't change when a session is invalidated, and hence there is no one-to-one mapping between a `JSESSIONID` cookie and a session in WebLogic.
The error can happen when:
  1. You first access your application deployed on `/myapp` with Tomcat. Tomcat sets a `JSESSIONID` cookie for `/myapp`.
  2. You then access your application on the same server deployed on `/myapp` with WebLogic. Tomcat sets a `JSESSIONID` cookie for `/`.
  3. In subsequent requests, the browser sends the Tomcat `JSESSIONID` as it is more specific (for `/myapp` instead of just `/`), but WebLogic doesn't recognize it, hence the error you're getting.

The solution is simply to clear in your browser all the `JSESSIONID` cookies for the host you are trying to access.
