# High Availabilty and Clustering

In certain envrionments calling for high availability, or in which one server running Orbeon Forms isn't enough to handle the load, you will want to deploy Orbeon Forms on multiple servers, often called *cluster*. When deployed in a cluster, you'll want to make sure that:

1. Only caches and session information is maintained at the level of each Orbeon Forms instance, and critical informatin is stored on a separate server, or servers, typically a database server. For forms you create with Form Builder, you'll want to make sure not to use the internal eXist database to store data, but instead to [setup Form Runner to point to a separate relational database](../form-runner/persistence/relational-db.md) or separate instance of eXist.

2. That you setup your reverse proxy, which determine to which server 
