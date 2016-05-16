# Form Metadata

<!-- toc -->

## Purpose

This is the API used, in particular, by the [Form Runner Home page](../../feature/home-page.md), accessible to users on `/fr/`. The Form Runner home page lists all the *published* forms the user has access to, and for each one it provides a link to create a new instance of that form, and to the summary page for that form. Either one of those links might be missing, depending on the [user's permissions](../../../form-runner/access-control/README.md).

## API

### Request

### Basics

You get the list of all the published forms with a GET on:
 
    /fr/service/persistence/form
    
This, in turn, calls the corresponding API for each persistence API implementation defined in the properties as [*active*](../../../configuration/properties/persistence.md#property_active), since different forms can be published on different persistence implementations. For example, this might call MySQL implementation doing a GET on:
 
    /fr/service/mysql/form
    
Then it might call the eXist implementation with another GET on:
 
    /fr/service/exist/form
    
Finally it aggregates the results returned by each implementation.

### Restricting by app/form name

[SINCE: Orbeon Forms 4.3]

Optionally, an app name or both an app name and form name can be specified on the URL. In that case, the API only returns information about published forms in that specific app, or that specific app and form is returned.

* When an app specified, the URL looks like:  
  `/fr/service/persistence/form/[APP_NAME]`
* When both an app and form name are specified, the URL looks like:  
  `/fr/service/persistence/form/[APP_NAME]/[FORM_NAME]`
  
### Returning all form definition versions
  
[SINCE Orbeon Forms 2016.2]

Optionally, you can pass the URL parameter `all-versions`:

- when set to `true`, all form definition versions are returned
- when omitted or set to `false`, only the highest published version number is returned

### Response

The document returned by this API looks like this:

```xml
<forms>
    <form operations="*">
        <application-name>orbeon</application-name>
        <form-name>bookshelf</form-name>
        <title xml:lang="en">Orbeon Forms Bookshelf</title>
        <title xml:lang="fr">Orbeon Forms Bookshelf</title>
        <last-modified-time>2014-06-04T11:21:33.043-07:00</last-modified-time>
        <form-version>1</form-version>
    </form>
    <form operations="*">
        <application-name>orbeon</application-name>
        <form-name>w9</form-name>
        <title xml:lang="en">Request for Taxpayer Identification Number and Certification</title>
        <last-modified-time>2014-06-04T11:21:34.051-07:00</last-modified-time>
        <form-version>3</form-version>
    </form>
    <form operations="create read update">
        <application-name>acme</application-name>
        <form-name>order</form-name>
        <title xml:lang="en">ACME Order Form</title>
        <title xml:lang="fr">Formulaire de commande ACME</title>
        <permissions>
            <permission operations="delete">
                <group-member/>
            </permission>
            <permission operations="delete">
                <owner/>
            </permission>
            <permission operations="create read update"/>
        </permissions>
        <last-modified-time>2014-08-21T16:52:24.429-07:00</last-modified-time>
        <form-version>2</form-version>
    </form>
</forms>
```

Each `<form>` element contains:

- `<application-name>`
- `<form-name>`
- All the elements inside the *form metadata* instance of the corresponding form definition
    - This can be retrieved with the following XPath expression:  
      `/xh:html/xh:head/xf:model/xf:instance[@id = 'fr-form-metadata']/metadata/*`
    - [SINCE Orbeon Forms 2016.1]
        - The `<description>` and `<migration>` elements are excluded.
- `<last-modified-time>`
    - [SINCE Orbeon Forms 4.4]
    - [UNTIL Orbeon Forms 4.10.x]
        - last modification date/time for the app/form combination
    - [SINCE Orbeon Forms 2016.1]
        - last modification date/time for the app/form/version combination
- `<form-version>`
    - [SINCE Orbeon Forms 2016.1]
    - contains the version number of the given `<form>`
    - *NOTE: This is only returned when using a relational database, as the implementation of the persistence API for eXist [doesn't support versioning yet](https://github.com/orbeon/orbeon-forms/issues/1524).*
    