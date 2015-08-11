> [[Home]] ▸ [[Form Runner|Form Runner]]

## Integration points  

Form Builder and Form Runner integrate with other systems through the following means:  

- __Plain URLs__, through which you access Form Runner and Form Builder's pages
    - The URLs can be accessed simply by using hyperlinks or redirects from another application.
- __A [configurable persistence API](http://wiki.orbeon.com/forms/doc/developer-guide/form-runner/persistence-api)__ based on REST (that is, through HTTP)
- __External user management__, to connect to a directory of users with associated roles.
- __HTTP services__
  - called from forms via the Service Editors
  - called by Form Runner to load initial XML data
- __[[Embedding|Form-Runner ~ Embedding]]__, including within a portal

The persistence API can be implemented either within Orbeon Forms (like for example the built-in eXist persistence layer), or within an external system.  

### Form Runner URLs   

Form Runner and Form builder attempt to use friendly URLs.

The following URL patterns are followed:

* Summary page for a given form definition:  
    `/fr/[APPLICATION_NAME]/[FORM_NAME]/summary`
* New empty form data:  
    `/fr/[APPLICATION_NAME]/[FORM_NAME]/new`
* Edit existing form data:  
    `/fr/[APPLICATION_NAME]/[FORM_NAME]/edit/[DOCUMENT_ID]`
* Read-only HTML view:  
    `/fr/[APPLICATION_NAME]/[FORM_NAME]/view/[DOCUMENT_ID]`
* Read-only PDF view:  
    `/fr/[APPLICATION_NAME]/[FORM_NAME]/pdf/[DOCUMENT_ID]`
* Read-only TIFF view: [SINCE Orbeon Forms 4.11]
    `/fr/[APPLICATION_NAME]/[FORM_NAME]/tiff/[DOCUMENT_ID]`

See also [[Form Builder Integration|Form Builder ~ Integration]].

_NOTE: All paths above are relative to the deployment context, e.g the actual URLs start with http://localhost:8080/orbeon/fr/..._

### Persistence API

The persistence API is used to store and retrieve *form definitions* and *form data*.

Orbeon Forms ships out of the box with support for a number of databases (see [[Database Support|Orbeon Forms Features ~ Database Support]]). Integrators can provide persistence via the [persistence API](http://wiki.orbeon.com/forms/doc/developer-guide/form-runner/persistence-api).

### External user management

See [[Access Control|Form Runner ~ Access Control]].

### XML representation of form data

See [[Data Format|Form Runner ~ Data Format]]

_NOTE: Non-repeated grids do not create containing elements._

## See also

- [[Form Builder Integration|Form Builder ~ Integration]]
- [[Form Runner Embedding|Form-Runner ~ Embedding]]
- [Persistence API](http://wiki.orbeon.com/forms/doc/developer-guide/form-runner/persistence-api)
- [[Access Control|Form Runner ~ Access Control]].
- [[Data Format|Form Runner ~ Data Format]]