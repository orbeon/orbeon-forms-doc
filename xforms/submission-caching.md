# Advanced Submissions - Caching

<!-- toc -->

## Caching of XForms instances with xxf:cache

### Availability

This is an [Orbeon Forms PE](http://www.orbeon.com/download) feature.

### Configuration

Orbeon Forms supports a boolean extension attribute, `xxf:cache`, on the `<xf:instance>` and `<xf:submission>` elements. This attribute can be used with instances that are read-only or read-write. When set to true:

* The instance can be shared at the application level. It is identified by its source URL and, in the case of a submission with POST or PUT method, by the body of the request sent.
* The instance is not stored into the XForms document's state, but in a global cache, therefore potentially saving memory. If, upon loading an XForms instance or running a submission, the instance is found in the cache, it is directly retrieved from the cache. This can save time especially if the URL can take significant time to load.
* The instance stored in cache is read-only. If the `xxf:readonly` attribute is set to true (on `<xf:instance>` or `<xf:submission>`), a single copy of the instance is used in memory. Otherwise, a read-write copy is made.
* In general, the URL should refer to a constant or rarely-changing XML document, and authorization credentials such as username and password should not cause different data to be loaded.

_NOTE: This attribute deprecates the `xxf:shared` attribute. Using `xxf:cache="true"` is equivalent to using `xxf:shared="application"`. Using `xxf:cache="false"` is equivalent to using` xxf:shared="document"`._

Here is how you use the attribute on `<xf:instance>`:

```xml
<xf:instance
  id="resources-instance"
  src="/forms/resources/en"
  xxf:readonly="true"
  xxf:cache="true"/>
```

When used on `<xf:submission>`, the submission has to use `method="get"`, `method="post"` or `method="put"` method and `replace="instance"`:

```xml
<xf:submission
  serialization="none"
  resource="/forms/resources/fr"
  method="get"
  replace="instance"
  instance="resources-instance"
  xxf:readonly="true"
  xxf:cache="true"/>
```

_NOTE: Since POST and PUT are not meant to be idempotent methods, you should use `xxf:cache="true"` with these methods carefully, typically for calls to services you know are idempotent. Otherwise, incorrect or stale data might be returned by the submission._

You set the size of the shared instances cache using a property in `properties.xm`l:

```xml
<property as="xs:integer" name="oxf.xforms.cache.shared-instances.size" value="50"/>
```

You can force the XForms engine to remove a shared instance from the cache by dispatching the `xxforms-instance-invalidate` event to it. The next time an XForms document requires this instance, it will not be found in the cache and therefore reloaded. Example:

```xml
<xf:action ev:event="DOMActivate">
    <xf:send submission="save-submission"/>
    <xf:dispatch name="xxforms-instance-invalidate" target="data-to-save-instance"/>
</xf:action>
```

The `xxf:invalidate-instance` action allows invalidating a shared instance by resource URI, for example:

```xml
<xxf:invalidate-instance ev:event="DOMActivate" resource="/forms/resources/fr"/>
```

This action also supports the `xinclude` attribute, which if present will only invalidate the instance with the given resource if it was loaded with a matching `xxf:xinclude` attribute.

Submission loading a shared instance and enabling XInclude processing:

```xml
<xf:submission
    serialization="none" 
    resource="/forms/resources/fr"
    method="get"
    replace="instance" 
    instance="resources-instance"
    xxf:readonly="true" 
    xxf:cache="true" 
    xxf:xinclude="true"/>
```

Action invalidating only the instance which was loaded with `xxf:xinclude="true"`:

```xml
<xxf:invalidate-instance 
    ev:event="DOMActivate" 
    resource="/forms/resources/fr" 
    xinclude="true"/>
```

If the `xinclude` attribute is not specified, any shared instance matching the resource URI is invalidated.

It is also possible to remove all shared instances from the cache by using the `xxf:invalidate-instances` action, for example:

```xml
<xxf:invalidate-instances ev:event="DOMActivate"/>
```

The `xxf:ttl attribute` can be used to set a time to live for the instance in cache. This duration is expressed in milliseconds and has to be greater than zero. When a shared instance if found in cache but has an associated time to live, if it was put in the cache more than time to live milliseconds in the past, then the instance is discarded from the cache and retrieved again by URI as if it had not been found in cache at all. The following example expires the shared instance after one hour:

```xml
<xf:instance
  id="resources-instance"
  src="/forms/resources/en"
  xxf:readonly="true"
  xxf:cache="true"
  xxf:ttl="3600000"/>
```

_SECURITY NOTE: When using `xxf:cache="true"`, be sure that the data contained in the instance does not contain information that could be inadvertently shared with other XForms documents. It is recommended to use it to load localized resources or similar types of data._
