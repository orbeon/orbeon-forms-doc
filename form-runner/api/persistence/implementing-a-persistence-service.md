# Implementing a Persistence Service

<!-- toc -->

## Scenario

This scenario describes how company Acme can go about implementing their own persistence service.

First, configure `properties-local.xml`, for example:

```xml
<property
  as="xs:string"
  name="oxf.fr.persistence.provider.acme.*.data"
  value="my-persistence"/>

<property
  as="xs:anyURI"
  name="oxf.fr.persistence.my-persistence.uri"
  value="http://example.com/my-persistence"/>
```

What this does is tell Form Runner to dispatch all persistence API calls for applications with name "acme" to the specified URL root. Replace the URL as appropriate, but it must point to a server able to implement the persistence API's behavior. It could be implemented with Java, .NET, PHP, Ruby, Orbeon Forms itself, etc.

You must then implement a server component responding to the `/my-persistence` path on server `example.com`. Upon receiving a request, it must:

* Check the HTTP method to determine the operation to perform (which CRUD operation or search operation)
* Check the requested path to determine the location of the resource
* In the case of PUT, read the request body and store it appropriately. This might require writing data into a CLOB or XML type column in a relational database, for example.
* In the case of GET, read the storage and return the appropriate resource.
* In the case of DELETE, delete the appropriate resource in storage
* In the case of POST, perform a search. This might require generating an SQL query, for example.

Depending on the type of storage chosen, storage operations may be more or less complex.
