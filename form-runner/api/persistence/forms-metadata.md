# Form Metadata API

## Service endpoint

HTTP `GET` to the following path:

```
/fr/service/persistence/form
```

## Purpose

This is the API used, in particular, by the [Form Runner Home page](../../feature/home-page.md), accessible to users on `/fr/`. The Form Runner home page lists all the *published* forms the user has access to, and for each one it provides a link to create a new instance of that form, and to the summary page for that form. Either one of those links might be missing, depending on the [user's permissions](../../../form-runner/access-control/README.md).

## API

### Request

#### Basics

You get the list of all the published forms with a `GET` on:

``` 
/fr/service/persistence/form
```

This, in turn, calls the corresponding API for each persistence API implementation defined in the properties as [*active*](../../../configuration/properties/persistence.md#property_active), since different forms can be published on different persistence implementations. For example, this might call MySQL implementation doing a `GET` on:
 
    /fr/service/mysql/form
    
Then it might call the eXist implementation with another `GET` on:
 
    /fr/service/exist/form
    
Finally it aggregates the results returned by each implementation.

#### Restricting by app/form name

[SINCE Orbeon Forms 4.3]

Optionally, an app name or both an app name and form name can be specified on the URL. In that case, the API only returns information about published forms in that specific app, or that specific app and form is returned.

* When an app specified, the URL looks like:  
  `/fr/service/persistence/form/[APP_NAME]`
* When both an app and form name are specified, the URL looks like:  
  `/fr/service/persistence/form/[APP_NAME]/[FORM_NAME]`
  
#### Returning all form definition versions
  
[SINCE Orbeon Forms 2016.2]

Optionally, you can pass the URL parameter `all-versions`:

- when set to `true`
    - All form definition versions are returned.
- when omitted or set to `false`
    - Only the form definition with the highest published version number is returned.

#### Returning all form definitions

[SINCE Orbeon Forms 2019.1, 2018.2.1, 2018.1.4, 2017.2.3]

Optionally, you can pass the URL parameter `all-forms`:

- when set to `true`
    - all forms are returned without checking for permissions or other criteria
- when omitted or set to `false` (which was the behavior before `all-forms` was supported)
    - Only forms to which the user has "access" are returned.
    - If the user has access as per `form-builder-permissions.xml`, then all form definitions are returned.
        - [SINCE Orbeon Forms 2022.1], this logic is ignored if `ignore-admin-permissions` is set to `true`.
    - Otherwise:
        - `library` form definitions are excluded 
        - forms which have permissions defined and for which the user has no permissions at all are excluded
        - unavailable form definitions are excluded

#### Filtering by date

[SINCE Orbeon Forms 2021.1]

Optionally, you can pass the URL parameter `modified-since`:

- this must be an ISO date/time
- when passed, only form definitions which have been modified since the given date are returned

For example:

```
/fr/service/persistence/form?modified-since=2021-09-09T04:56:42.257Z
```

#### Ignoring admin permissions

[SINCE Orbeon Forms 2022.1]

Optionally, you can pass the URL parameter `ignore-admin-permissions`:

- when set to `true`
    - Even if the user has access to the form as per `form-builder-permissions.xml`, the forms are filtered as [documented](#returning-all-form-definitions).
- when omitted or set to `false` (the default)
    - The behavior is the same as before this parameter was supported.

If the `all-forms` parameter is set to `true`, then this parameter is ignored, and all forms are returned without checking for permissions or other criteria as [documented](#returning-all-form-definitions).

### Response

#### Response format 

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
- `<available>`
    - set to `false` when the form definition is marked as not available

#### Latest published version

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

#### All versions

The document returned by this API looks like this, here for the `acme/order` form:

```xml
<forms>
    <form operations="admin *">
        <application-name>acme</application-name>
        <form-name>order</form-name>
        <last-modified-time>2018-11-02T21:52:46.173Z</last-modified-time>
        <form-version>1</form-version>
        <title xml:lang="en">Contact</title>
        <title xml:lang="fr">Contact</title>
    </form>
    <form operations="admin *">
        <application-name>acme</application-name>
        <form-name>order</form-name>
        <last-modified-time>2018-11-02T21:54:22.481Z</last-modified-time>
        <form-version>2</form-version>
        <title xml:lang="en">Contact</title>
        <title xml:lang="fr">Contact</title>
    </form>
    <form operations="admin *">
        <application-name>acme</application-name>
        <form-name>order</form-name>
        <last-modified-time>2018-12-05T23:31:33.320Z</last-modified-time>
        <form-version>3</form-version>
        <title xml:lang="en">Contact</title>
        <title xml:lang="fr">Contact</title>
    </form>
    <form operations="admin *">
        <application-name>acme</application-name>
        <form-name>order</form-name>
        <last-modified-time>2018-12-13T00:12:11.650Z</last-modified-time>
        <form-version>4</form-version>
        <title xml:lang="en">Contact</title>
        <title xml:lang="fr">Contact</title>
    </form>
    <form operations="admin *">
        <application-name>acme</application-name>
        <form-name>order</form-name>
        <last-modified-time>2018-12-20T19:30:10.435Z</last-modified-time>
        <form-version>5</form-version>
        <title xml:lang="en">Contact</title>
        <title xml:lang="fr">Contact</title>
        <available>false</available>
    </form>
</forms>
```

Note that the versions do not have to be in order and some versions can be missing.

Here note that version 5 is marked as not available with:

```xml
<available>false</available>
```

## See also

- [CRUD](crud.md)
- [Search](search.md)
- [Form metadata](forms-metadata.md)
- [Caching](caching.md)
- [Versioning](versioning.md)
- [Implementing a persistence service](implementing-a-persistence-service.md)
