 # Orbeon Forms Caches

## Caches used by Orbeon Forms

Orbeon Forms uses caches for the following purposes:

- Form state
    - This includes form data and other form state as the user interacts with a form.
    - This cache acts as a store.
    - This must be replicated when [replication](replication.md) is enabled. 
- Mapping of some web resources
    - This must be replicated when [replication](replication.md) is enabled.
- Caching of form definition metadata in the persistence layer
    - [SINCE Orbeon Forms 2023.1]
    - This is a true cache, which doesn't need to be replicated.

## Cache implementations

Until Orbeon Forms 2022.1.x, Orbeon Forms uses Ehcache 2.x. This cache library also supports [replication](replication.md).

Since Orbeon Forms 2021.1.9 PE, 2022.1.5 PE, and 2023.1, Orbeon Forms includes JCache API (JSR-107) support, so that other cache implementations can be used.

With Orbeon Forms 2021.1.9 PE, 2022.1.5 PE, JCache is disabled by default and Ehcache 2.x is still used by default.

## Using the default Ehcache 2.x configuration

By default, the following property is set and enables Ehcache 2.x:

```xml
<property
    as="xs:string"
    name="oxf.xforms.cache.provider"
    value="ehcache2"/>
```

An internal `ehcache.xml` configuration file is used. Here is the [file as of Orbeon Forms 2022.1.4](https://github.com/orbeon/orbeon-forms/blob/0f5bcf02178009c8a33868227c9b1d03e019e80d/src/main/resources/config/ehcache.xml).

You can update that configuration by placing your own `ehcache.xml` file in the `WEB-INF/resources/config` directory of your web app. This is rarely needed, except for enabling [replication](replication.md).

## Enabling JCache support

With the Orbeon Forms versions that support it, you can enable JCache support with the following:

```xml
<property
    as="xs:string"
    name="oxf.xforms.cache.provider"
    value="jcache"/>
```

When this is set to `jcache`, Orbeon Forms uses the *default caching provider* as provided by the JCache API. This means that you must provide, in the Orbeon Forms web app, your own JCache-compatible implementation. Orbeon Forms comes by default with Ehcache 3.x as implementation, but you do not have to use Ehcache 3.x.

You can further specify the configuration to use with the following properties:

```xml
<property as="xs:string"  name="oxf.xforms.cache.resource" value=""/>
<property as="xs:string"  name="oxf.xforms.cache.uri"      value=""/>
```

- `oxf.xforms.cache.resource` specifies the path to a webapp resource. The configuration can be stored under the web app's WEB-INF or a JAR file included in the webapp.
- `oxf.xforms.cache.uri` specifies the URI to a configuration.

The `resource` property is checked first, then the `uri` property. A blank property is ignored.

For example, to point to the built-in Ehcache 3.x configuration, set:

```xml
<property 
    as="xs:string"  
    name="oxf.xforms.cache.resource" 
    value="/ehcache3.xml"/>
```

If you are using a different JCache-compatible implementation, different values for the `oxf.xforms.cache.resource` and/or `oxf.xforms.cache.uri` properties might be needed.

## See also

- [Installation](README.md)
- [Replication](replication.md)
- Blog post: [High-Availability Thanks to State Replication](https://blog.orbeon.com/2018/03/high-availability-thanks-to-state.html)
- [Clustering and High Availability](/configuration/advanced/clustering.md)
