# Resource Managers

<!-- toc -->

## Introduction

A _resource manager_ is an Orbeon Forms component responsible for reading and writing XML and other resources like binary and text documents. A resource manager abstracts the actual mechanisms used to store resources. An important benefit of using such an abstraction is that it is possible to store all your application files in a sandbox which can be moved at will within a filesystem or between storage mechanisms. For instance, resources can be stored:

* As files on disk (using your operating system's file system)
* As resources within a WAR file
* As resources within one or more JAR files

A resource manager is both used:

* Internally by Orbeon Forms
* By your own Orbeon Forms applications through URLs with the `oxf:` protocol

This page describes the different types of resource managers and explains their configuration.

## General configuration

A single resource manager is initialized per Orbeon Forms web application. Configuration is handled in the web application descriptor (`web.xml`) by setting a number of context parameters. The first parameter indicates the resource manager factory:

```xml
<context-param>
    <param-name>oxf.resources.factory</param-name>
    <param-value>org.orbeon.oxf.resources.PriorityResourceManagerFactory</param-value>
</context-param>
```

Other properties depend on the resource manager defined by the factory. These properties are described in the following sections.

## Filesystem resource manager

### Configuration

|   |   |
|---|---|
| Purpose | Loading resources from a filesystem |
| Factory | `org.orbeon.oxf.resources.FilesystemResourceManagerFactory` |
| Properties | `oxf.resources.filesystem.sandbox-directory` |

The filesystem resource manager loads resources from a filesystem. This is especially useful during development, since no packaging of resources is necessary. The `oxf.resources.filesystem.sandbox-directory` property can be used to define a sandbox, and if used must point to a valid directory on a filesystem. If not specified, no sandbox is defined, and it is possible to access all the files on the filesystem.

Using the filesystem resource manager without a sandbox is particularly useful for command-line applications.

### Example

```xml
<context-param>
    <param-name>oxf.resources.factory</param-name>
    <param-value>org.orbeon.oxf.resources.FilesystemResourceManagerFactory</param-value>
</context-param>
<context-param>
    <param-name>oxf.resources.filesystem.sandbox-directory</param-name>
    <param-value>/home/user/oxf/myapp/resources</param-value>
</context-param>
```

## ClassLoader resource manager

### Configuration

|   |   |
|---|---|
| Purpose | Loading resources from a JAR file in the classpath |
| Factory | `org.orbeon.oxf.resources.ClassLoaderResourceManagerFactory` |
| Properties | None |

The class loader resource manager can load resource from a JAR file or from a directory in the classpath. This resource manager is required to load internal resources for Orbeon Forms.

### Example

```xml
<context-param>
    <param-name>oxf.resources.factory</param-name>
    <param-value>org.orbeon.oxf.resources.ClassLoaderResourceManagerFactory</param-value>
</context-param>
```

## WebApp resource manager

### Configuration

|   |   |
|---|---|
| Purpose | Loading resources from a WAR file or deployed Web Application |
| Factory | `org.orbeon.oxf.resources.WebAppResourceManagerFactory` |
| Properties | `oxf.resources.webapp.rootdir` |

This resource manager is useful when you want to package an application into a single WAR file for distribution and deployment. The configuration property indicates the path prefix of the resources directory inside a WAR file. It is recommended to store resources under the `WEB-INF` directory to make sure that the resources are not exposed to remote clients.

### Example

```xml
<context-param>
    <param-name>oxf.resources.factory</param-name>
    <param-value>org.orbeon.oxf.resources.WebAppResourceManagerFactory
</param-value>
</context-param>
<context-param>
    <param-name>oxf.resources.webapp.rootdir</param-name>
    <param-value>/WEB-INF/resources</param-value>
</context-param>
```

## URL resource manager

### Configuration

|   |   |
|---|---|
| Purpose | Loading resources from a URL |
| Factory | `org.orbeon.oxf.resources.URLResourceManagerFactory` |
| Properties | `oxf.resources.url.base` |

This resource manager is able to load resources form any URL (typically `http` or `https`). It can be used if your resources are located on a web server or a content management system with an HTTP interface.

### Example

```xml
<context-param>
    <param-name>oxf.resources.factory</param-name>
    <param-value>org.orbeon.oxf.resources.URLResourceManagerFactory</param-value>
</context-param>
<context-param>
    <param-name>oxf.resources.url.base</param-name>
    <param-value>http://www.somwhere.com/base/</param-value>
</context-param>
```

## Priority resource manager

### Configuration

|   |   |
|---|---|
| Purpose | Chains several resource managers in order |
| Factory | `org.orbeon.oxf.resources.PriorityResourceManagerFactory` |
| Properties | `oxf.resources.priority.1` … `oxf.resources.priority.n` |

With the priority resource manager you can chain several resource managers. It is crucial to be able to load resources from multiple sources since some resources are bundled in the Orbeon Forms JAR file. Thus, the class loader resource manager must always be in the priority chain. It usually has the lowest priority so the application developer can override system resources.

There can be any number of chained resource managers. They are configured by adding a `oxf.resources.priority.n` property, where `n` is an integer.

_NOTE: The priority resource manager is more efficient when most resources are found in the first resource manager specified._

### Examples

This is the standard configuration as of Orbeon Forms 4.10: 

```xml
<context-param>
    <param-name>oxf.resources.factory</param-name>
    <param-value>org.orbeon.oxf.resources.PriorityResourceManagerFactory</param-value>
</context-param>
<context-param>
    <param-name>oxf.resources.priority.2</param-name>
    <param-value>org.orbeon.oxf.resources.WebAppResourceManagerFactory</param-value>
</context-param>
<context-param>
    <param-name>oxf.resources.priority.2.oxf.resources.webapp.rootdir</param-name>
    <param-value>/WEB-INF/resources</param-value>
</context-param>
<context-param>
    <param-name>oxf.resources.priority.4</param-name>
    <param-value>org.orbeon.oxf.resources.ClassLoaderResourceManagerFactory</param-value>
</context-param>
```

This is the configuration for developers as of Orbeon Forms 4.10:

```xml
<context-param>
    <param-name>oxf.resources.factory</param-name>
    <param-value>org.orbeon.oxf.resources.PriorityResourceManagerFactory</param-value>
</context-param>
<context-param>
    <param-name>oxf.resources.priority.0</param-name>
    <param-value>org.orbeon.oxf.resources.FilesystemResourceManagerFactory</param-value>
</context-param>
<context-param>
    <param-name>oxf.resources.priority.0.oxf.resources.filesystem.sandbox-directory</param-name>
    <param-value>/Users/orbeon/Orbeon/OF/src/resources-local</param-value>
</context-param>
<context-param>
    <param-name>oxf.resources.priority.1</param-name>
    <param-value>org.orbeon.oxf.resources.FilesystemResourceManagerFactory</param-value>
</context-param>
<context-param>
    <param-name>oxf.resources.priority.1.oxf.resources.filesystem.sandbox-directory</param-name>
    <param-value>/Users/orbeon/Orbeon/OF/src/resources</param-value>
</context-param>
<context-param>
    <param-name>oxf.resources.priority.3</param-name>
    <param-value>org.orbeon.oxf.resources.FilesystemResourceManagerFactory</param-value>
</context-param>
<context-param>
    <param-name>oxf.resources.priority.3.oxf.resources.filesystem.sandbox-directory</param-name>
    <param-value>/Users/orbeon/Orbeon/OF/src/resources-packaged</param-value>
</context-param>
<context-param>
    <param-name>oxf.resources.priority.5</param-name>
    <param-value>org.orbeon.oxf.resources.FilesystemResourceManagerFactory</param-value>
</context-param>
<context-param>
    <param-name>oxf.resources.priority.5.oxf.resources.filesystem.sandbox-directory</param-name>
    <param-value>/Users/orbeon/Orbeon/OF/src/test/resources</param-value>
</context-param>
<context-param>
    <param-name>oxf.resources.priority.2</param-name>
    <param-value>org.orbeon.oxf.resources.WebAppResourceManagerFactory</param-value>
</context-param>
<context-param>
    <param-name>oxf.resources.priority.2.oxf.resources.webapp.rootdir</param-name>
    <param-value>/WEB-INF/resources</param-value>
</context-param>
<context-param>
    <param-name>oxf.resources.priority.4</param-name>
    <param-value>org.orbeon.oxf.resources.ClassLoaderResourceManagerFactory</param-value>
</context-param>
```