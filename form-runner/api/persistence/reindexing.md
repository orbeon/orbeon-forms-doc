# Reindexing API

## Availability

[SINCE Orbeon Forms 2018.1 and 2017.2.1 PE]

## Overview

This page describes what to do if:

- You want to call the Orbeon Forms reindexing API from your own code or application.
- You are writing your own implementation of the persistence API, and your implementation supports its own internal index.

If instead you want to *use* this feature and reindex your database as an administrator, see how to [reindex your database from the Forms Admin page](/form-runner/feature/forms-admin-page.md#reindexing).

## Calling the reindexing API

### Reindexing the entire database

You call the reindexing API by sending a `GET` request to the `/fr/service/persistence/reindex` endpoint.

[\[SINCE Orbeon Forms 2024.1.2\]](/release-notes/orbeon-forms-2024.1.2.md)

Orbeon Forms also support a `POST` request with an empty body. `POST` makes more sense than `GET` as it is a side-effecting operation, and in fact only supporting `GET` was a bug. `GET` is still supported for backward compatibility by the Orbeon Forms builtin relational provider, but it is recommended to use `POST` instead.

The API responds with a 200 OK when the reindexing is done. Note that the reindexing can be a long operation.

### Reindexing a specific form

[\[SINCE Orbeon Forms 2024.1.2\]](/release-notes/orbeon-forms-2024.1.2.md)

You can also reindex a specific form by sending a `POST` request to the `/fr/service/persistence/reindex/$app/$form` endpoint, where `$app` is the form definition's application name and `$form` is the form definition's form name. You must also, if the persistence provider supports versioning, pass the `Orbeon-Form-Definition-Version` header with the form definition version to reindex.

_NOTE: `GET` is not supported for reindexing a specific form._

## Custom provider requirements

### Declaring an implementation supports reindexing

The Form Runner Admin page allows admins to reindex the database, however this feature isn't implemented by the Form Runner home page itself, but by the respective implementations of the persistence API. Of course, reindexing only makes sense for implementations of the persistence API that maintain their own index. Since this is an optional feature, the Form Runner home page needs to know which implementations supports reindexing, so it can call them if they do when admins ask for the data to be reindexed. You declare that your implementation supports reindexing by adding the following property:

```xml
<property 
    as="xs:boolean" 
    name="oxf.fr.persistence.[provider].reindex"                            
    value="true"/>
```

### Reindexing endpoint

An implementation supporting reindexing needs to support a `GET` request at `/reindex`, and it is expected to respond with a 200 OK when the reindexing is done.

[\[SINCE Orbeon Forms 2024.1.2\]](/release-notes/orbeon-forms-2024.1.2.md)

An implementation supporting reindexing must also support a `POST` request at `/reindex` with an empty body.

[\[SINCE Orbeon Forms 2024.1.2\]](/release-notes/orbeon-forms-2024.1.2.md)

An implementation supporting reindexing a specific form must also support a `POST` request at `/reindex/$app/$form`, where `$app` is the form definition's application name and `$form` is the form definition's form name. The implementation must also support the `Orbeon-Form-Definition-Version` header with the form definition version to reindex, if versioning is supported.

## Limitations

The Form Runner home page supports stopping the reindexing, and showing a progress while the reindexing happens. For now, while those features are implemented by the built-in implementation of the persistence API for relational databases, they are not exposed through the API, and can't yet be implemented by third-parties.

## See also

- [CRUD](crud.md)
- [Search](search.md)
- [List form data attachments](list-form-data-attachments.md)
- [Form metadata](forms-metadata.md)
- [Caching](caching.md)
- [Versioning](versioning.md)
- [Custom persistence providers](custom-persistence-providers.md)
