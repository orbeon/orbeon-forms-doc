# Reindexing API

[SINCE Orbeon Forms 2018.1 and 2017.2.1 PE] This page describes what you'll want to do if you are writing your own implementation of the persistence API, and your implementation supports its own internal index. If instead you want to *use* this feature and reindex your database, see how to [reindex your database from the Form Runner home page](https://doc.orbeon.com/form-runner/feature/home-page.html#reindexing).

## Declaring an implementation supports reindexing

The Form Runner home page allows admins to "reindex the database", however this feature isn't implemented by the Form Runner home page itself, but by the respective implementations of the persistence API. Of course, reindexing only makes sense for implementations of the persistence API that maintain their own index. Since this is an optional feature, the Form Runner home page needs to know which implementations supports reindexing, so it can call them if they do when admins ask for the data to be reindexed. You declare that your implementation supports reindexing by adding the following property:

```xml
<property 
    as="xs:boolean" 
    name="oxf.fr.persistence.[provider].reindex"                            
    value="true"/>
```

## Reindexing endpoint

If you declared that your implementation supports reindexing, when admins request reindexing from the Form Runner home page, your implementation will get a GET request at `/reindex`, and is expected to respond with a 200 OK when the reindexing is done.

## Limitations

The Form Runner home page supports stopping the reindexing, and showing a progress while the reindexing happens. For now, while those features are implemented by the built-in implementation of the persistence API for relational databases, they are not exposed through the API, and can't yet be implemented by a 3rd-party implementation.