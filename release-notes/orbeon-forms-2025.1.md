# Orbeon Forms 2025.1

__xxx, December xx, 2025__  

Today we released Orbeon Forms 2025.1! This release is absolutely packed with new features and bug-fixes!

## Major new features

### Introduction

With Orbeon Forms 2025.1, we are introducing a number of major new features. Several of them contribute to improving usability, navigability, and visibility of forms and form data.

In addition, some features improve the deployment of Orbeon Forms, including cloud deployment, and deployment with JavaScript embedding. 

### Language Support

Orbeon Forms 2025.1 brings major improvements to language support: 

- All languages that were only partially supported before are now fully supported.
- We've added Polish, Arabic, and Japanese support for both Form Runner and Form Builder.
- We've also added Catalan, Chinese (Simplified), Chinese (Traditional), Hungarian, and Turkish support for Form Runner.

### Pluggable detail page modes

xxx

### xxx

xxx

## Other new features and enhancements

Orbeon Forms 2025.1 also includes many other new features and enhancements, including:


## Compatibility and upgrade notes

### Legacy custom XForms JavaScript events

These long undocumented events are no longer dispatched on the client side:

xxx fullUpdateEvent
xxx typeChangedEvent

### Log4j 1.x compatibility

Support for using Log4j 2.x in Log4j 1.x compatibility mode has been removed. If you were using Log4j 1.x configuration files (`log4j.xml`), please migrate to Log4j 2.x and use `log4j2.xml`.

### Removal of deprecated date/time functions

xxx was planned for removal in 2017!
fr:created-date(), use fr:created-dateTime()
fr:modified-date(), use fr:modified-dateTime()

### Ehcache 2.x changes and deprecation

Support for Ehcache 2.x as a caching provider is deprecated and will be removed in a subsequent release. The Ehcache 2.x JAR file is no longer provided. If you are using Ehcache 2.x, please migrate to using a JCache provider, such as Infinispan, which is now the default.

If this is not convenient or possible in the short term, you can still, at of Orbeon Forms 2025.1, configure Ehcache 2.x as the caching provider by adding the Ehcache 2.x JAR file to Orbeon Forms' classpath and setting the following property in `properties-local.xml`:

```xml
<property
    as="xs:string"
    name="oxf.xforms.cache.provider"
    value="ehcache2"/>
```
See also:

- Documentation: [JCache configuration with Infinispan](/installation/caches.md#orbeon-forms-20251-and-newer-jcache-configuration-with-infinispan)
