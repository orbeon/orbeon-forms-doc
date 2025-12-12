# Orbeon Forms 2025.1

__xxx, December xx, 2025__  

Today we released Orbeon Forms 2025.1! This release is absolutely packed with new features and bug-fixes!

## Major new features

### Introduction

With Orbeon Forms 2025.1, we are introducing a number of major new features. Several of them contribute to improving usability, navigability, and visibility of forms and form data.

In addition, some features improve the deployment of Orbeon Forms, including cloud deployment, and deployment with JavaScript embedding. 

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

### xxx

Ehcache2 provider: don't use `oxf:/`, but resource path `/config/ehcache.xml`
xxx also: deprecated
