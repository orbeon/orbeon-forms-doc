## Integration points  

Form Builder and Form Runner integrate with other systems through the following means:  

- plain URLs, through which you access Form Runner and Form Builder's pages
    - The URLs can be accessed simply by using hyperlinks or redirects from another application.
- a [configurable persistence API](http://wiki.orbeon.com/forms/doc/developer-guide/form-runner/persistence-api) based on REST (that is, through HTTP)
- HTTP services
  - called from forms via the Service Editors
  - called by Form Runner to load initial XML data
- [[embedding|Form-Runner ~ Embedding]] (including within a portal)

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

See also [[Form Builder ~ Integration|Form Builder ~ Integration]].

_NOTE: All paths above are relative to the deployment context, e.g the actual URLs start with http://localhost:8080/orbeon/fr/..._

### Persistence API

See [Persistence API](http://wiki.orbeon.com/forms/doc/developer-guide/form-runner/persistence-api).

### XML representation of form data

### Basics

As you create a form definition with Form Builder, an XML representation for the data to capture is automatically created. It is organized as follows:  

* A root element:  
    `<form>`
* Within that element, for each section, a sub-element named after the section name:  
    `<address>`
* Within a section element, a sub-element for each control in the section, named after the control name:  
    `<first-name>`
* Within each control element, the value of the control is stored:  
    `<first-name>Alice</first-name>`

Example:

```xml
<form>
    <details>
        <title/>
        <author/>
        <language/>
        <link/>
        <rating/>
        <publication-year/>
        <review/>
        <image filename="" mediatype="" size=""/>
    </details>
    <notes>
        <note/>
    </notes>
</form>
```

### Nested sections

TODO

### Repeated grids and sections

TODO: Specify how nested grids and nested sections are represented.

### Attachments

For attachments, the control element is slightly different:

- the text content is a URL pointing to the location of the attachment in the persistence layer
- attributes are used for storing
    - the file name
    - the file media type
    - the file size  

Example:

```xml
<my-attachment filename="book.png" mediatype="image/png" size="13245">
/fr/service/exist/crud/orbeon/builder/data/5277.../book.png
</my-attachment>
```

_NOTE: Non-repeated grids do not create containing elements._

## See also

- [[Form Builder Integration|Form Builder ~ Integration]]