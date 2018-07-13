# Home Page



## Introduction

The Form Runner Home page is a start page for accessing forms deployed in Form Runner.

You access the Form Runner Home page by adding `fr/` to the path on which you deployed Orbeon Forms. If you deployed Orbeon Forms on `http://www.example.com/orbeon/`, then you can access the From Runner home page at `http://www.example.com/orbeon/fr/`.

## User and admin views

[SINCE Orbeon Forms 4.3]

If the user doesn't have any permissions set via `form-builder-permissions.xml`, as [documented here](../access-control/editing-forms.md#form-builder-permissions), a simple user view is presented:

![Home Page](../images/home-simple-view.png)

If the user has permissions set in `form-builder-permissions.xml`, a view with admin privileges is presented:

![Home Page](../images/home-admin-view.png)

The list of forms listed depends on the roles set in `form-builder-permissions.xml`. For example, with:

```xml
<role name="*" app="*" form="*"/>
```

the user can perform any admin operation on any form. But with:

```xml
<role name="orbeon-user" app="acme" form="*"/>
```

the user can perform admin operations on `acme` forms only.

## User view

In this view:

- Each line shows only forms that are *published and available*.
- For each form, the app name, form name and title in the current language are shown.
- You can navigate to the form's Summary, New or View page, depending on permissions, by clicking on a line.
- Forms are sorted by last modification time.
- Libraries are never shown in this view.

*NOTE: Only deployed forms are visible. Forms that have been created with Form Builder and which have been just saved but never deployed are not visible.*

## Admin view

### Admin operations on forms

When the user has access to admin operations, new information is shown:

- whether the form is available or unavailable ("unpublished")
- whether the form is a library form

Unavailable forms remain on the server, but any user access to an unavailable form behaves as if the form definition had never been published. Data associated with the form definition is not touched, but cannot be read or updated via the form definition when it is unavailable.

Administrators can select forms in two ways:

- by using the checkboxes next to the form
- by using the *Select* menu to automatically select all forms matching a certain condition

![Select Menu](../images/home-select-menu.png)

The operations listed in the Operations menu are then available depending on the current selection:

![Operations Menu](../images/home-operation-menu.png)

### Controlling form definitions availability

- __Publish local forms:__ make an unavailable form definition available again.
- __Unpublish local forms:__ make a published form definition unavailable.

Initially, when publishing a form definition with Form Builder, the form definition is available.

![Unavailable](../images/home-unavailable.png)

### Upgrading form definitions

#### Introduction

[SINCE Orbeon Forms 4.6]

- __Upgrade local form definitions:__ upgrade the selected form definitions, including using the latest published section templates.
 
Specifically, this operation, for each selected form definition:

1. Reads the latest version of the published form definition, whether it is available or unavailable.
1. Loads the latest versions of the published section templates that might be associated with that form definition.
1. Upgrades the form definition markup to the latest format, as if loading it in Form Builder and saving it again.
1. Includes the latest section templates loaded above into the form definition.
1. Saves back the form definition, either as a new version or as the same version (see below for details).

#### Upgrading with section templates and versioning

Important notes when [form definition versioning](http://blog.orbeon.com/2014/02/form-versioning.html) is used:

- From Orbeon Forms 4.6 (included) to 2016.1 (excluded): a *new* form definition version is created when the form definition is upgraded.
- From Orbeon Forms 2016.1 (included): the upgraded form definition *overrides* the previously-published form definition. 

When form definition versioning is used section templates are in use, you have to be very careful because the format of the data covered by section templates might have changed and might make the upgraded form incompatible with existing data. See also [Publishing](http://doc.orbeon.com/form-builder/publishing.html#versioning).

We advise that you only republish form definitions which use both form definition versioning and section templates if you know that the format of the data covered by the section templates remains compatible.

#### Upgrading section templates

Here is how you can use this operation to update your form definitions' section template to the latest published section templates:

1. Publish your section templates from Form Builder (see [Section templates](http://doc.orbeon.com/form-builder/section-templates.html)). 
2. In the Home page, select which forms you want to update.
3. Run the "Upgrade local form definitions" action.

### Remote server operations

[SINCE Orbeon Forms 4.4]

This is an Orbeon Forms PE feature.

#### Introduction

This feature allows you to configure access to a remote server and to publish, unpublish, and transfer forms between the local and remote server.

#### Configuration on the local server

In order to configure a remote server, you need to setup a property.

With Orbeon Forms 4.7 and newer, use the `oxf.fr.home.remote-servers` property, which allows configuring multiple remote servers:

```xml
<property as="xs:string" name="oxf.fr.home.remote-servers">
  [
    {
      "label": "Staging",
      "url":   "http://staging.example.org:8080/orbeon"
    },
    {
      "label": "Production",
      "url":   "http://prod.example.org:9090/orbeon"
    }
  ]
</property>
```

![Multiple Remote Servers](../images/remote-server-credentials.png)

The format is a JSON array containing any number of JSON objects. Each object must have a non-empty `label` property indicating the label to display to the user when selecting a remote server, and a non-empty `url` property indicating the location of the remote server.

With Orbeon Forms 4.4 to 4.6, use the `oxf.fr.production-server-uri` property:

```xml
<property
  as="xs:anyURI"
  name="oxf.fr.production-server-uri"
  value="http://remote.server:8080/orbeon/"/>
```

*NOTE: As of Orbeon Forms 4.7, the `oxf.fr.production-server-uri` is deprecated. With Orbeon Forms 4.7 and newer, if the `oxf.fr.production-server-uri` is set and not empty, it takes precedence over the new `oxf.fr.home.remote-servers` property, for backward compatibility.*

#### Configuration on the remote server

You also need to authorize the remote server to accept incoming connections for services. One way of doing this is to use the Orbeon Forms delegating `orbeon-auth.war`, documented [here](../../xml-platform/controller/authorization-of-pages-and-services.md).

You deploy this WAR file alongside `orbeon.war` on the remote server, and you add this property to the remote server's `properties-local.xml`:

```xml
<property
  as="xs:anyURI"
  processor-name="oxf:page-flow"
  name="authorizer"
  value="/orbeon-auth"/>
```

This tells the remote server to use the `orbeon-auth` webapp to authenticate requests for services or pages which are not public.

By default, `orbeon-auth` requires that all external requests to Form Runner services are authenticated with `BASIC` authentication and have the `orbeon-service` role. It blocks any other request.

If you are using Tomcat, you can then configure a user with role `orbeon-service`. For example, in `tomcat-users.xml`:

```xml
<role rolename="orbeon-service"/>
<user username="orbeon-admin" password="changeme" roles="orbeon-service"/>
```

Then, on the local server, you would use username `orbeon-admin` and password `changeme` when prompted.

With this configuration, the local Orbeon Forms connects to services on the remote Orbeon Forms, which calls up `orbeon-auth` to authenticate the connection. `orbeon-auth` requires that the username/password provided authenticate as a valid Tomcat user with the `orbeon-service` role. If that's successful, then the service proceeds, otherwise it fails.

#### Remote operations

When the remote server is configured as above, the first time you go to the Form Runner Home page you are prompted for credentials:

![Credentials](../images/home-credentials.png)

Once the credentials are correct, the Home page retrieves the remote server's list of deployed forms and metadata, which appears in a new *Remote* column group. You then have access to new operations:

- __Publish remote form:__ make an unavailable form available again on the remote server.
- __Unpublish remote form:__ make a previously published form unavailable on the remote server.
- __Push to remote:__ copy a form definition and its attachments from the local to the remote server.
- __Pull from remote:__ copy a form definition and its attachments from the remote to the local server.
- __Upgrade remote form definitions:__ upgrade the remote form definition (see [Upgrading form definitions](#upgrading-form-definitions)).

![Push to Remote](../images/home-push.png)

You are always prompted to confirm the operation to perform:

![Confirmation](../images/home-confirmation.png)

In addition, if the latest modification time of the form definitions differ, a *Newer* label appears:

![Newer](../images/home-newer.png)

### Re-encryption

[SINCE Orbeon Forms PE 2018.1]<br>Also see [Field-level encryption](../../form-builder/field-level-encryption.md).

#### Limitations to re-encryption

Currently, re-encryption is only supported in the built-in implementation of the persistence API for relational databases. The built-in implementation of the persistence API for eXist doesn't support re-encryption, and custom implementations can't yet support re-encryption.

#### Creating a database backup before re-encryption

Unlike typical update and delete operations that are performed at the database level by adding a new row, thus keeping the old data, re-encryption is done *in-place*. All data is being re-encrypted, including historical data that is part of the "journal". This means that you could loose data if something were to go wrong during re-encryption. As such, we strongly recommend you create a backup of your data before re-encryption.

#### Starting re-encryption

To re-encrypt all the data for certain forms, from the Form Runner home page in admin view:

1. Mark the checkboxes corresponding to the relevant forms.
2. Click on the "Operations" button, and in the menu click on "Re-encrypt form definitions". If that entry is greyed out, make sure that you haven't selected a form for which data is stored with a persistence layer that doesn't support re-encryption.
3. After you confirm you desire to go ahead with the re-encryption, you will see, in the "Status" column:
	- "Re-encryption started," right after you triggered the re-encryption.
	- "Re-encrypting 392/2401," while the re-encryption is in progress, here assuming it has already re-encrypted 391 documents out of 2401 it needs to re-encrypt.
	- "Re-encryption finished," when the re-encryption is done for that form.

### Reindexing

[SINCE Orbeon Forms 2016.2]

#### Upgrading to 2016.2

Orbeon Forms 2016.2 adds two index tables: `orbeon_i_current` and `orbeon_i_control_text`, so when upgrading to 2016.2, for Form Runner to work properly, you'll need to initially populate those tables in an operation referred to as *reindexing*. After you've upgraded and done the initial reindexing, you shouldn't need to reindex the database manually, as Form Runner will incrementally update the index when needed.

Depending on how much data you have, the reindexing operation can take a while, and during reindexing some features of Orbeon Forms 2016.2, like Form Runner summary pages and the [search API](../api/persistence/search.md), won't work as expected. Because of this, upgrading requires some additional steps if you're using those features. If end users don't access Form Runner summary pages on your production system, and you don't have custom code calling the search API (which is rare), then go to point 1 below; otherwise, go with point 2 below.

1. If you're not relying on those features in production, you can upgrade Orbeon Forms as you normally would when you need to make changes to the database: take the previous version of Orbeon Forms offline, run the [relevant DDL to upgrade your database schema](../persistence/relational-db.md), and take the new version of Orbeon Forms online. This requires Orbeon Forms to be offline, but this, typically, only for a matter of a few minutes.
2. If you're relying on those features in production, since reindexing can take a while and you can't run Orbeon Forms 2016.2 while reindexing, we suggest you start by cloning your database, run the [relevant DDL to upgrade your database schema](../persistence/relational-db.md) on that cloned database, run 2016.2 on another server, and have it reindex the cloned database. This will give you an idea of how long reindexing takes, and how long of an offline window you need. With this information in mind, schedule an offline window of the appropriate length, and at that time, do the upgrade procedure again, but this time on the production database.

#### UI

In admin view, the Form Runner home page shows a reindex button:

![Reindex button](../images/home-reindex-button.png)

After you click it, and confirm you want to go ahead, if this is indeed the what you want to do (see above paragraph for more information about what happens during reindexing), the home page shows the reindexing status:

![Reindex status](../images/home-reindex-status.png)

If, for some reason, you want to stop reindexing, you can do so by clicking on the *Stop reindexing* button. When doing so, the index is left in an incoherent state, and you should restart indexing at a latter time. While reindexing stops, you'll see the following message:

![Reindex stopping](../images/home-reindex-stopping.png)

Finally, once reindexing is done, you'll see:

![Reindex done](../images/home-reindex-done.png)

#### Logging

While reindexing happens, the indexer writes to the log:

- When indexing starts and ends.
- Which providers it will reindex.
- For each provider how many document it will reindex.
- A "progress message" for each document being reindexed.

The last message is logged at the `debug` level, to avoid your log growing unnecessarily in case you have lots of documents, while all the other messages are logged at the `info` level. For instance, you'll see something along those lines in your `orbeon.log`:

```
INFO  - Reindex status - Starting, will index [oracle_dev, oracle_staging]
INFO  - Reindex status - Indexing oracle_dev 1/2
INFO  - Reindex status - Indexing oracle_dev 1/2, 8475 documents
DEBUG - Reindex status - Indexing oracle_dev 1/2, document 1/8475
DEBUG - Reindex status - Indexing oracle_dev 1/2, document 2/8475
…
INFO  - Reindex status - Stopped
```

If you'd like to enable logging at the `debug` level, add the following to your `log4j.xml`:

```xml
<category name="org.orbeon.relational">
    <priority value="debug"/>
</category>
```

## Configuration properties

### Page size

The number of forms shown on a given page can be set with the following property:

```xml
<property
    as="xs:integer"
    name="oxf.fr.home.page-size"
    value="20"/>
```

### Link to the summary or new page

The table listing the forms links, for each form, either to the `summary` or the `new` page, based on which page the user has access to. If the user has access to both, then it links to the `summary` page, in essence giving the priority to the `summary` page. [SINCE Orbeon Forms 2017.2] You can also change this priority by setting the value of the following property, of which the default is shown below.

```xml
<property
    as="xs:string"
    name="oxf.fr.home.table.link-to"
    value="summary new"/>
```

For instance, if you set it to `new summary`, the priority will be given to the `new` instead of the `summary` page. If you list only one page, say `new`, then entries in the table will only link to the `new` page, of course if the user has access to it. If you leave the value blank, the forms will only be listed, with no link.

## Orbeon Forms 4.0 to 4.2

For each form definition the current user has access to, the following links are shown if allowed:

- Link to the summary page: shown if the current user can perform either one of the read, update, or delete operations on the form.
- Link to the new page: shown if the current user can perform the create operation on the form.

![Home Page](../images/home.png)

## See also

- [Access control and permissions](../access-control-and-permissions.md)
- [Form Builder permissions](../access-control/editing-forms.md#form-builder-permissions)
