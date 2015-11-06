# Form Metadata

<!-- toc -->

## Related pages

- [[Overview|Form Runner ~ APIs ~ Persistence ~ Overview]]
- [[CRUD|Form Runner ~ APIs ~ Persistence ~ CRUD]]
- [[Search|Form Runner ~ APIs ~ Persistence ~ Search]]
- [[Versioning|Form Runner ~ APIs ~ Persistence ~ Versioning]]
- [[Implementing a Persistence Service|Form Runner ~ APIs ~ Persistence ~ Implementing a Persistence Service]]

## Purpose

This is the API used, in particular, by the [[Form Runner Home page|Form Runner ~ Home Page]], accessible to users on `/fr/`. The Form Runner home page lists all the *published* forms the user has access to, and for each one it provides a link to create a new instance of that form, and to the summary page for that form. Either one of those links might be missing, depending on [[the user's permissions|Form Runner ~ Access Control]].

## API

You get the list of all the published forms with a GET on `/fr/service/persistence/form`. This will, in turn call the corresponding API for each persistence API implementation defined in the properties, since different forms can be published on different persistence implementations. For instance, this might call MySQL implementation doing a GET on `/fr/service/mysql/form` and the eXist implementation with another GET on `/fr/service/exist/form`, finally aggregating the results returned by each implementation.

The document returns by this API looks like this:

```xml
<forms>
    <form>
    <form operations="*">
        <application-name>orbeon</application-name>
        <form-name>bookshelf</form-name>
        <title xml:lang="en">Orbeon Forms Bookshelf</title>
        <description xml:lang="en">Orbeon Forms Bookshelf is a simple form …</description>
        <title xml:lang="fr">Orbeon Forms Bookshelf</title>
        <description xml:lang="fr">Orbeon Forms Bookshelf présente un formulaire simple…</description>
        <last-modified-time>2014-06-04T11:21:33.043-07:00</last-modified-time>
    </form>
    <form operations="*">
        <application-name>orbeon</application-name>
        <form-name>w9</form-name>
        <title xml:lang="en">Request for Taxpayer Identification Number and Certification</title>
        <description xml:lang="en"/>
        <last-modified-time>2014-06-04T11:21:34.051-07:00</last-modified-time>
    </form>
    <form operations="create read update">
        <application-name>acme</application-name>
        <form-name>order</form-name>
        <title xml:lang="en">ACME Order Form</title>
        <description xml:lang="en">This is a form to order new stuff from ACME, Inc.</description>
        <title xml:lang="fr">Formulaire de commande ACME</title>
        <description xml:lang="fr">Ceci est un formulaire de commande pour ACME, Inc.</description>
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
    </form>
</forms>
```

Each `<form>` element contains:

* All the elements inside the form metadata instance of the corresponding form definition, which can be retrieved with the following XPath expression: `/xh:html/xh:head/xf:model/xf:instance[@id = 'fr-form-metadata']/metadata/*`.
* A `<last-modified-time>` element. [SINCE Orbeon Forms 4.4]

[SINCE: Orbeon Forms 4.3] Optionally, an app name or both an app name and form name can be specified on the URL. In that case, the API only returns information about published forms in that specific app, or that specific app and form is returned.

* When an app specified, the URL looks like `/fr/service/persistence/form/[APP_NAME]`.
* When both an app and form name are specified, the URL looks like `/fr/service/persistence/form/[APP_NAME]/[FORM_NAME]`.
