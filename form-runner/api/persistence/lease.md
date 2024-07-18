# Lease API

[SINCE Orbeon Forms 2017.2]

This page describes what you'll want to do to support the lease feature in your own implementation of the persistence API. If instead you'd like to *use* that feature, see the documentation on the [lease feature](/form-runner/feature/lease.md).

## Enabling support

[Custom persistence providers](custom-persistence-providers.md) may support the lease feature, but they are not required to. For this reason, if your implementation does support the lease feature, you must set the following property to `true` for your provider:

```xml
<property 
    as="xs:boolean" 
    name="oxf.fr.persistence.$provider.lease"                            
    value="true"/>
```

If your implementation advertises itself as supporting the lease feature, it must implement two additional methods, `LOCK` and `UNLOCK`, as described below. `LOCK` and `UNLOCK` are seen as an extension of the CRUD operations, so are issued to `/crud/$app/$form/data/$document/data.xml`.

## The LOCK method

Before assigning a lease for a document to a user, Form Runner issues a `LOCK` for that document.

### Request

- The duration of the lease is provided through the `Timeout: Second-600` header (here for a 10-minute lock), per RFC 2518 section 9.8.
- The body of the request contains a `<d:lockinfo>` XML document, which looks as follows. The `<d:lockscope>` and `<d:locktype>` are for compliance with RFC 2518, but for now only the values below are supported.

```xml
<d:lockinfo xmlns:d="DAV:" xmlns:fr="http://orbeon.org/oxf/xml/form-runner">
    <d:lockscope><d:exclusive/></d:lockscope>
    <d:locktype><d:write/></d:locktype>
    <d:owner>
        <fr:username>jsmith</fr:username>
        <fr:groupname>admin</fr:groupname>
    </d:owner>
</d:lockinfo>
```

### Response

Your implementation keeps track of what lease is assigned to what user, for what document, and until when the lease is granted. Your implementation can grant a lease per the request if any of the following conditions are met:

- No existing lease was previously granted for this document.
- A lease was granted for this document, and it was granted to the same user.
- A lease was granted for this document, and it has expired.

If the lease:

- Can be granted:
    - Then your implementation responds with a `200`.
- Cannot be granted:
    - Then it responds with a `423`, per RFC 2518 section 8.10.7.
    - The response must contain a `<d:lockinfo>` document with information about the owner of the lease (same document that was provided when the lease was acquired), and a `Timeout` header telling the caller for how much longer the lease is expected to stay in place.
    - This timeout value is only indicative as after your response the lease can be canceled by the user (see `UNLOCK` below) or renewed (by issuing another `LOCK`).

## The UNLOCK method

To release a lease on a document, Form Runner uses the `UNLOCK` method with a `<d:lockinfo>` document in the request body. Based on the information in this document, if following the same logic used for the `LOCK` method, the lease:

- Could be granted:
    - Then the implementation keeps track that nobody owns the lease on the document, and responds with a `200`.
- Couldn't be granted:
    - Then it responds with a `423`, a `Timeout` header, and a `<d:lockinfo>` document in the response body, similarly to what is done for the `LOCK` method.

## See also

- [Custom persistence providers](custom-persistence-providers.md)
