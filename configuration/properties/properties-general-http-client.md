# HTTP client configuration properties 

## Proxy setup

To configure an HTTP proxy to be used for all the HTTP connections established by Orbeon Forms, add the following two properties:

```xml
<property
    as="xs:string"
    name="oxf.http.proxy.host"
    value="localhost"/>

<property
    as="xs:integer"
    name="oxf.http.proxy.port"
    value="8090"/>

<property
    as="xs:boolean"
    name="oxf.http.proxy.use-ssl"
    value="false"/>

<property
    as="xs:string"
    name="oxf.http.proxy.exclude"
    value=""/>

<property
    as="xs:string"
    name="oxf.http.proxy.username"
    value=""/>

<property
    as="xs:string"
    name="oxf.http.proxy.password"
    value=""/>

<property
    as="xs:string"
    name="oxf.http.proxy.ntlm.host"
    value=""/>

<property
    as="xs:string"
    name="oxf.http.proxy.ntlm.domain"
    value=""/>
```

By default, the host and port properties are commented and Orbeon Forms doesn't use a proxy. Some of the use cases where you will want to define a proxy include:

- Your network setup requires you to go through a proxy.
- You would like see what goes through HTTP by using a tool that acts as an HTTP proxy, such as [Charles](https://www.charlesproxy.com/).

To connect to the proxy over HTTPS, instead of HTTP which is the default, set the `oxf.http.proxy.use-ssl` property to `true`.

[SINCE Orbeon Forms 4.6]

You can exclude host names from the proxy using the `oxf.http.proxy.exclude` property, which contains a space-delimited list of hostnames.

## SSL hostname verifier

When using HTTPS, you can specify how the hostname of the server is checked against the hostname in its certificate. You do so with the following property:

```xml
<property
    as="xs:string"
    name="oxf.http.ssl.hostname-verifier"
    value="strict"/>
```

The possible values are:

- `strict` — (the default) See [`StrictHostnameVerifier`](https://hc.apache.org/httpcomponents-client-4.5.x/current/httpclient/apidocs/org/apache/http/conn/ssl/StrictHostnameVerifier.html).
- `browser-compatible` — See [`BrowserCompatHostnameVerifier`](https://hc.apache.org/httpcomponents-client-4.5.x/current/httpclient/apidocs/org/apache/http/conn/ssl/BrowserCompatHostnameVerifier.html).
- `allow-all` — See [`AllowAllHostnameVerifier`](https://hc.apache.org/httpcomponents-client-4.5.x/current/httpclient/apidocs/org/apache/http/conn/ssl/AllowAllHostnameVerifier.html).

Typically, you'll leave this property to its default value (`strict`). However, you might need to set it to `allow-all` to be able to connect to a server with a self-signed certificate if the `cn` in the certificate doesn't match the hostname you're using to connect to that server.

## 2-way SSL

When using HTTPS, you might want Orbeon Forms to authenticate itself by presenting a client certificate. For this, you need the client to have a key and certificate in a keystore, and point Orbeon Forms to that keystore using the propertied below.

```xml
<property
    as="xs:anyURI"
    name="oxf.http.ssl.keystore.uri"
    value="oxf:/config/my.keystore"/>

<property
    as="xs:string"
    name="oxf.http.ssl.keystore.password"
    value="changeit"/>
```

- `oxf.http.ssl.keystore.uri`
    - Specifies the URI of the keystore file.
    - The URI can use the `file:` or [SINCE Orbeon Forms 2021.1] `oxf:` protocol.
    - Relationship to the truststore:
        - [SINCE Orbeon Forms 2021.1] Whether this property is specified or not, the server certificate is verified using the default truststore, which you override by setting the `javax.net.ssl.trustStore` property (more on this in the [JSSE Reference Guide](https://docs.oracle.com/en/java/javase/11/security/java-secure-socket-extension-jsse-reference-guide.html)).
        - [UNTIL Orbeon Forms 2020.1] If you specify a keystore, it is also used as a truststore. This is the case even if connecting to server whose key is signed by a recognized certificate authority (CA), which means that you need to add the certificate of the CA who signed the key of the server you want to connect to the keystore.  
        - If this property is blank, the default JSSE algorithm to find a truststore applies.
- `oxf.http.ssl.keystore.password`
    - Specifies the password needed to access the keystore file.

You might also want to:

- For Orbeon Forms to accept incoming connections using the same certificate, set up your servlet container, on Tomcat in the `server.xml` on the `<Connector>` used for HTTPS, to point to same keystore.
- [SINCE Orbeon Forms 2021.1] Set the `oxf.http.ssl.keystore.*` system property to point to a truststore that contains the certificate of the certificate authority who signed the certificate of the server you want to connect to. 

## Headers forwarding

When Orbeon Forms performs XForms submissions, or retrieves documents in XPL over HTTP, it has the ability to forward incoming HTTP headers. For example, if you want to forward the `Authorization` header to your services:

```xml
<property
    as="xs:string"
    name="oxf.http.forward-headers"
    value="Authorization"/>
```

For more, see [HTTP headers forwarding](/xforms/submission-extensions.md#http-headers-forwarding).

_WARNING: For security reasons, you should be careful with header forwarding, as this might cause non trusted services to receive client headers._

## Cookies forwarding

Similar to general headers forwarding, cookies can be forwarded. By default, the property is as follows:

```xml
<property
    as="xs:string"
    name="oxf.http.forward-cookies"
    value=""/>
```

If you need to forward, say, `JSESSIONID` and `JSESSIONIDSSO` to services, set this in properties-local.xml:

```xml
<property
    as="xs:string"
    name="oxf.http.forward-cookies"
    value="JSESSIONID JSESSIONIDSSO"/>
```

When a username for HTTP Basic authentication is specified, cookies are not forwarded. The first cookie in the list, typically `JSESSIONID`, is interpreted by Orbeon Forms to be the session cookie. If the value of the session cookie doesn't match the current session, say because the provided `JSESSIONID` has expired or is invalid, then the value of the cookie from the incoming request isn't forwarded. Instead, in that case, the new value of the session cookie is:
 
- [UP TO Orbeon Forms 2016.3] The session id.
- [SINCE Orbeon Forms 2017.1] The concatenation of the following 3 values: 
 
     1. The value of the `oxf.http.forward-cookies.session.prefix` property
     2. The session id
     3. The value of the `oxf.http.forward-cookies.session.suffix` property
     
By default, the value of the prefix and suffix properties is empty, as shown below, which works well with application servers like Tomcat that set the `JSESSIONID` directly to the session id.   

```xml
<property 
    as="xs:string"
    name="oxf.http.forward-cookies.session.prefix"         
    value=""/>
<property
    as="xs:string"
    name="oxf.http.forward-cookies.session.suffix"
    value=""/>
```

On the other hand, some application servers, add a prefix and suffix to the session id. For instance, WebSphere uses the *cache id* as prefix, and the colon character (`:`) followed by the *clone id* as suffix. So, on WebSphere, assuming that in your situation the *cache id* is always `0000`, and the *client id* (found in WebSphere's `plugin-cfg.xml`) is `123`, you will want to set those properties as shown below. Note how the value of the *client id* follows the colon character in the value of the suffix property.

```xml
<property 
    as="xs:string"
    name="oxf.http.forward-cookies.session.prefix"         
    value="0000"/>
<property
    as="xs:string"
    name="oxf.http.forward-cookies.session.suffix"
    value=":123"/>
```

_WARNING: For security reasons, you should be careful with cookies forwarding, as this might cause non trusted services to receive client cookies._

## Stale checking

This property is tied to the [HttpClient stale checking](http://hc.apache.org/httpclient-3.x/apidocs/org/apache/commons/httpclient/params/HttpConnectionParams.html#setStaleCheckingEnabled%28boolean%29):

> Defines whether stale connection check is to be used. Disabling stale connection check may result in slight performance improvement at the risk of getting an I/O error when executing a request over a connection that has been closed at the server side.

By default, Orbeon checks for stale HTTP connections. You can disabling stale connection checking by setting the following property to `false` (it is `true` by default):

```xml
<property
    as="xs:boolean"
    name="oxf.http.stale-checking-enabled"
    value="false"/>
```

## Socket timeout

This property is tied to the [HttpClient SO timeout](http://hc.apache.org/httpclient-3.x/apidocs/org/apache/commons/httpclient/params/HttpConnectionParams.html#setSoTimeout%28int%29):

> Sets the default socket timeout (SO_TIMEOUT) in milliseconds which is the timeout for waiting for data. A timeout value of zero is interpreted as an infinite timeout.

By default, Orbeon doesn't set a timeout with HttpClient. Setting a timeout can be potentially dangerous as it can lead to service calls that take longer to run than the timeout you specified to fail in a way that can be unpredictable, as it is possible for your services to sometimes return before the timeout and sometimes after. If, nevertheless, you need to set a timeout, you can do so by adding the following property, e.g. here setting a timeout at 1 minute:

```xml
<property
    as="xs:integer"
    name="oxf.http.so-timeout"
    value="60000"/>
```

_NOTE: These two headers are computed values and it is only possible to override them with constant values by using the properties above. In general we don't recommend overriding these headers by using the properties above._

## Request chunking

```xml
<property
    as="xs:boolean"
    name="oxf.http.chunk-requests"
    value="false"/>
```

## Expired and idle connections

[SINCE Orbeon Forms 2019.1]

Since the HTTP client uses connection pooling, some connections can be come stale, which can cause errors at inopportune times. Enabling expired and idle connections checking can help reduce this issue.

The `oxf.http.expired-connections-polling-delay` property sets the expired connection checking polling delay. The default is 5,000 milliseconds (5 seconds). 

```xml
<property 
    as="xs:integer" 
    name="oxf.http.expired-connections-polling-delay"      
    value="5000"/>
```

The `oxf.http.idle-connections-delay` property sets the idle connection time to live. The default is 30,000 milliseconds (30 seconds).

```xml
<property 
    as="xs:integer" 
    name="oxf.http.idle-connections-delay"
    value="30000"/> 
```

If `oxf.http.expired-connections-polling-delay` is commented out or not present, neither checks are performed.

If `oxf.http.idle-connections-delay` is commented out or not present, but `oxf.http.expired-connections-polling-delay` is present, then only the check for expired connections takes place.

Keeping expired and idle connections checking enabled can improve the reliability of connections to remote servers.

## See also

- [General configuration properties](general.md)
