# Orbeon Forms Caches

## In-memory caches

Orbeon Forms uses in-memory caches for data structures associated with forms and form state:

- Compiled forms
    - This is the result of compiling the form definition. It is a data structure shared amongst all users of a form.
    - The size of this cache is controlled by the `oxf.xforms.cache.static-state.size` property and defaults to 50.
    - If a compiled form is evicted from the cache, it is serialized to the `xforms.state` store, from which it can be reloaded and reconstructed when needed.
- Form sessions
    - A form session represents one interactive session of a specific user with a specific form. This is *not* shared between users of a given form. 
    - The size of this cache is controlled by the `oxf.xforms.cache.documents.size` property and defaults to 50.
    - For testing purposes, this can be turned off with the `oxf.xforms.cache.document` property, but this is not recommended.
    - If a form session is evicted from the cache, it is stored in the `xforms.state` store, from which it can be reloaded and reconstructed when needed.
    - Whe a user session expires (that is, an application server session expires), the corresponding form sessions are evicted from the cache.

The default size of `oxf.xforms.cache.static-state.size` or `oxf.xforms.cache.documents.size` is fairly small. You definitely will want to consider what size to use for your deployment. If possible, set:

- `oxf.xforms.cache.static-state.size` to the number of distinct published form definitions you have in production (or slightly larger, for example 10-25% larger).
- `oxf.xforms.cache.documents.size` to the number of concurrent users you expect to have (or slightly larger, for example 10-25% larger).

__IMPORTANT: Performance will suffer if any of the data in the in-memory caches needs to be reconstructed from the `xforms.state` store.__

However, there is a trade-off between performance and memory usage. The larger the cache, the more memory is used.

For more information on these configuration properties, see [Configuring state handling](/contributors/state-handling.md#configuring-state-handling).

## Other caches used by Orbeon Forms

Orbeon Forms also uses the following caches that use an underlying cache implementation and configuration, such as Ehcache or through the JCache API (see [Supported cache implementations](#supported-cache-implementations) below):

- Form state
    - This includes form data and other form state as the user interacts with a form.
    - This cache acts as a store: some information cannot be reconstructed from other sources.
    - This is configured by the `xforms.state` cache.
    - This must be replicated when [replication](replication.md) is enabled.
- Mapping of some web resources
    - This is configured by the `xforms.resources` cache.
    - This cache acts as a store: some information cannot be reconstructed from other sources.
    - This must be replicated when [replication](replication.md) is enabled.
- Caching of form definition metadata in the persistence layer
    - [\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)
    - This is configured by the `form-runner.persistence.form-definition` and `form-runner.persistence.form-metadata` caches.
    - This is a true cache, which doesn't need to be replicated.
    - See [Persistence layer caching of form definition metadata](#persistence-layer-caching-of-form-definition-metadata) below.

## Supported cache implementations

Orbeon Forms has traditionally used Ehcache 2.x. In recent versions, JCache API (JSR-107) support has been added.

| Orbeon Forms Version | Ehcache 2.x Support | JCache Support | Default     |
|----------------------|---------------------|----------------|-------------|
| 2023.1 and newer     | Yes                 | Yes            | Ehcache 2.x |
| 2022.1.5 and newer   | Yes                 | Yes            | Ehcache 2.x |
| 2021.1.9 and newer   | Yes                 | Yes            | Ehcache 2.x |
| 2022.1.4 and earlier | Yes                 | No             | Ehcache 2.x |
| 2021.1.8 and earlier | Yes                 | No             | Ehcache 2.x |
| 2020.1.x and earlier | Yes                 | No             | Ehcache 2.x |

## Using the default Ehcache 2.x configuration

By default, the following property is set and enables Ehcache 2.x:

```xml
<property
    as="xs:string"
    name="oxf.xforms.cache.provider"
    value="ehcache2"/>
```

An internal `ehcache.xml` configuration file is used. Here are the default configuration files:

- [as of Orbeon Forms 2022.1.4](https://github.com/orbeon/orbeon-forms/blob/0f5bcf02178009c8a33868227c9b1d03e019e80d/src/main/resources/config/ehcache.xml)
- [as of Orbeon Forms 2023.1](https://github.com/orbeon/orbeon-forms/blob/2023.1-ce/src/main/resources/config/ehcache.xml)

You can update that configuration by placing your own `ehcache.xml` file in the `WEB-INF/resources/config` directory of your web app. This is rarely needed, except for enabling [replication](replication.md).

## Enabling JCache providers

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

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
<property as="xs:string"  name="oxf.xforms.cache.jcache.classname" value=""/>
<property as="xs:string"  name="oxf.xforms.cache.jcache.resource"  value=""/>
<property as="xs:string"  name="oxf.xforms.cache.jcache.uri"       value=""/>
```

- `oxf.xforms.cache.jcache.classname` specifies a regular expression that must match the provider fully-qualified class name. This is useful in case there is more than one cache provider in the classpath and you need to select a specific one.
- `oxf.xforms.cache.jcache.resource` specifies the path to a webapp resource. The configuration can be stored under the web app's WEB-INF or a JAR file included in the webapp.
- `oxf.xforms.cache.jcache.uri` specifies the URI to a configuration.

Example:

```xml
<property
    as="xs:string"
    name="oxf.xforms.cache.jcache.classname"                
    value=".*EhcacheCachingProvider"/>
```

The `resource` property is checked first, then the `uri` property. A blank property is ignored.

For example, to point to the built-in Ehcache 3.x configuration, set:

```xml
<property 
    as="xs:string"  
    name="oxf.xforms.cache.jcache.resource" 
    value="/ehcache3.xml"/>
```

_NOTE: The Ehcache 3 JAR file is already included in Orbeon Forms. Java packages are different, and therefore the Ehcache 2.x and Ehcache 3.x JAR files do not conflict._

If you are using a different JCache-compatible implementation:

- Different values for the `oxf.xforms.cache.jcache.resource` and/or `oxf.xforms.cache.jcache.uri` properties might be needed.
- Add the property mentioned below to disable caching of form definitions, as we don't want cached form definitions to be replicated.

## Persistence layer caching of form definition metadata

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

When you call the [Form Runner persistence API](/form-runner/api/persistence/README.md), Form Runner can cache accesses to form definition metadata. This is done to avoid having to load the form definition from the database on each persistence API call. This cache is not replicated, and is local to each Orbeon Forms instance.

This cache is enabled by default. To disable it, set the following property:

```xml
<property
    as="xs:boolean"
    name="oxf.fr.persistence.form-definition-cache.enable"
    value="false"/>
```

One reason to disable this cache might be if you perform accesses to the database from outside of Orbeon Forms, and you want to make sure that you always get the latest form definition. However, we recommend using the [Form Runner persistence API](/form-runner/api/persistence/README.md) instead.

This property enables access to caches with the following names, configured either in `ehcache.xml` or in the JCache configuration:

- `form-runner.persistence.form-definition`
- `form-runner.persistence.form-metadata`

## See also

- [State Handling](/contributors/state-handling.md)
- [Installation](README.md)
- [Replication](replication.md)
- Blog post: [High-Availability Thanks to State Replication](https://blog.orbeon.com/2018/03/high-availability-thanks-to-state.html)
- [Clustering and High Availability](/configuration/advanced/clustering.md)
- [Form Runner persistence API](/form-runner/api/persistence/README.md)