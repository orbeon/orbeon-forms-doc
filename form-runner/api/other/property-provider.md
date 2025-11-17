# Property provider API

## Availability

[\[SINCE Orbeon Forms 2025.1\]](/release-notes/orbeon-forms-2025.1.md)

## Rationale

Orbeon Forms allows [configuration through properties](/configuration/properties/README.md), which are typically defined in XML files, including the user-configurable `properties-local.xml`.

This Java API allows adding custom property providers to supply property values from alternative sources or through custom logic. Use cases include:

- providing configurations stored in a database
- providing passwords or keys from a secure vault or Key Management System (KMS)
- providing preferences linked to a user profile
- providing properties for a particular tenant in a multi-tenant setup

## Architecture

### Provider interface

You can provide one or more custom property providers by implementing the `org.orbeon.properties.api.PropertyProvider` interface. Your implementation is responsible for:

- checking the passed parameters to handle caching
- returning a list of properties when needed

At runtime, during a given request, Orbeon Forms queries each registered property providers in order of priority to obtain current properties, and merges the results. In order to make the process efficient, caching is supported.

### Caching

The idea of caching is fairly simple, and similar to HTTP caching:

- a property provider implementation can provide a cache key which identifies the current set of properties
    - in many cases, no cache key is needed if properties are shared amongst users
    - for use cases such as user preferences, a cache key can represent the user identity
    - for multi-tenant setups, a cache key can represent the tenant identity
- for a given cache key, the provider implementation must return an ETag, which identifies the current version of the properties for that cache key
    - if the properties change, the ETag must also change
    - if the properties do not change, the ETag must remain the same
- Orbeon Forms calls the provider implementation with the cache key and the last known ETag
    - if the ETag matches the current ETag for that cache key, the provider implementation indicates that properties have not changed
    - if the ETag does not match the current ETag for that cache key, the provider implementation must return the current list of properties

Orbeon Forms creates its own internal data structures to represent properties. However, the provider implementation must return properties using the `PropertyDefinition` interface.

### Context

For providers which depend on the current request context, information is passed to the provider implementation:

- the current user's credentials (if any)
- the current session (if any)
- the current request (if any)

In addition, a provider could use information stored into a `ThreadLocal` variable by earlier code in the request processing chain, for example to identify the current tenant in a multi-tenant setup. See also the [Connection context API](connection-context-api.md).

## API

### Java interface

The following describes the Java interface you need to implement:

```java
import java.util.Collection;
import java.util.Map;
import java.util.Optional;

public interface PropertyDefinitionsWithETag {
  Collection<PropertyDefinition> getProperties();
  String getETag();
}

public interface PropertyDefinition {
  String                 getName();
  String                 getValue();      
  String                 getType();       // `string | integer | boolean | nmtokens | anyURI`
  ju.Map<String, String> getNamespaces(); // immutable
  ju.Optional<String>    getCategory();   // EQName, for example "Q{http://www.orbeon.com/oxf/processors}page-flow"
}

public interface PropertyProvider {

    /**
     * Return a cache key to identify the properties for the given request and credentials.
     *
     * <p>If the provider does not handle properties that can vary with the user, this method does not have to be overridden.
     * A default implementation returns an empty {@code Optional}.</p>
     *
     * <p>If returned, the key must be a non-blank string, be reasonably small, and uniquely identify the user, tenant, or
     * other category of callers that share the same properties.</p>
     *
     * <p>The key is used by the caller to cache the ETag which is then passed to `getPropertiesIfNeeded()`.</p>
     */
    default Optional<String> getCacheKey(
        Optional<Request>     request,
        Optional<Credentials> credentials,
        Optional<Session>     session,
        Map<String, Object>   extension
    ) {
        return Optional.empty();
    }

    /**
     * Return a `Collection` of properties.
     *
     * <p>If `eTag` is empty or does not match the provider's current ETag for the properties, possibly based on `cacheKey`,
     * the provider must return up-to-date properties along with their ETag. On the other hand, if `eTag` matches
     * the provider's current ETag for the properties, possibly based on `cacheKey`, then the provider must return
     * an empty {@code Optional}, indicating that the caller can use cached properties.</p>
     *
     * <p>If the provider does not have any properties to return, it should return `PropertyDefinitionsWithETag` with an
     * empty `properties` collection and an appropriate ETag. It should not return an empty `Optional` in this case.</p>
     *
     * <p>WARNING: This method can be called concurrently from multiple threads.</p>
     */
    Optional<PropertyDefinitionsWithETag> getPropertiesIfNeeded(
        Optional<CacheKey>    cacheKey,
        Optional<ETag>        eTag,
        Optional<Request>     request,
        Optional<Credentials> credentials,
        Optional<Session>     session,
        Map<String, Object>   extension
    );
}
```

Note that in many cases, only `getPropertiesIfNeeded()` needs to be implemented, as `getCacheKey()` has a default implementation which returns an empty `Optional`. Also, several of the parameters to `getPropertiesIfNeeded()` might not be needed. However, `eTag` is a major component of the caching mechanism, so it must be handled properly.

In addition, the following context interfaces are provided to give access to information about the current request:

```java
import java.net.URI;
import java.util.Collection;
import java.util.Map;
import java.util.Optional;

public interface Organization {
    Collection<String> getLevels();
}

public interface Credentials {
    String                   getUsername();
    Optional<String>         getGroupname();
    Collection<String>       getRoles();
    Collection<Organization> getOrganizations();
}

public interface Session {
    String           getId();
    Optional<Object> getAttribute(String name);
    void             setAttribute(String name, Object value);
    void             removeAttribute(String name);
}

public interface Request {
    String                          getMethod();
    URI                             getRequestUri();
    Map<String, Collection<String>> getHeaders();
}
```

A provider implementation must not implement these interfaces: instead, they are meant for consumption by your property provider implementation. Orbeon Forms may extend these interfaces with additional methods in the future.

### Registering a provider

The connection context API uses the standard Java [`ServiceLoader`](https://docs.oracle.com/javase/8/docs/api/java/util/ServiceLoader.html) API with a provider name of `org.orbeon.properties.api.PropertyProvider`.

The provider must:

- be a public class
- have a public no-arguments constructor
- implement the `PropertyProvider` Java interface

To enable a provider with Orbeon Forms:

- create your provider as per the standard Java [`ServiceLoader`](https://docs.oracle.com/javase/8/docs/api/java/util/ServiceLoader.html) 
- create a JAR file containing the code or your provider
- place your JAR file under the Orbeon Forms `WEB-INF/lib` directory

The Orbeon Forms log files will log errors if any when starting if the provider was found but could not be instantiated.

For example, implement an instance of the interface above, and register it with Orbeon Forms by adding a file called:

```
META-INF/services/org.orbeon.properties.api.PropertyProvider
```

The file contains a single line with the fully-qualified name of the implementation class, for example:

```
com.example.MyPropertyProvider
```

This file and directory hierarchy are placed within the web application in a JAR file under `WEB-INF/lib`. 

### Enabling and priority of providers

Providers must be explicitly enabled to be used by Orbeon Forms to be used. In addition, when multiple providers are registered, their priority must be specified. Both of these functions are achieved using the following property:

```xml
<property
    as="xs:string"
    name="oxf.properties.providers.classnames"
    value=".*MyFirstProvider .*MySecondProvider"/>
```

Each space-separated token is a regular expression matching the fully-qualified class name of a provider.

_NOTE: If you use a fully-qualified class name, such as `org.acme.MyPropertyProvider`, keep in mind that `.` in a regular expression matches any character. Use `\.` or `[.]` to explicitly match a period. In general, this is not a problem, as it is unlikely that there are two providers in the classpath differing only by these characters._

- matching
    - providers must be listed in order to be used; providers that are not matched are ignored
    - a regular expression may match no provider, in which case it has no effect
- order
    - the leftmost providers have the highest priority
    - the rightmost providers have the lowest priority

If the 

Orbeon Forms includes a standard provider which has the lowest priority and is always included. This provider reads your `properties-local.xml` file and other built-in Orbeon Forms property files (which you generally do not directly deal with).

Some properties are always checked in the standard Orbeon Forms property provider, including:

- `oxf.properties.providers.classnames`
- `oxf.xforms.cache.provider`
- `oxf.xforms.store.provider`

## See also

- [Connection context API](connection-context-api.md)
- [File scan API](file-scan-api.md)
- [Form Runner APIs](../README.md)
