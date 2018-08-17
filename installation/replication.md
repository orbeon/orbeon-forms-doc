# Replication



## Availability

This is an [Orbeon Forms PE][1] feature.

This feature is available since Orbeon Forms 2017.2. It has been tested with the following software:

- Apache Tomcat 8.0.45
- HAProxy 1.7.4

## Introduction

The purpose of replication is to provide high-availability of Orbeon Forms with as little disruption as possible to
users currently filling out forms. This is achieved by replicating state between servers.

Consider a simple scenario of load balancing with two servers, with sticky sessions (that is, a given user's requests always
reach the same server). If one of the servers fails, new users will be assigned to the other server. So the system
remains operational from that point of view. However, users with active sessions will have their current work lost, as
the content will still in the failed server-memory.

Replication changes that by replicating state to one or more additional servers. So if a server goes down, the load
balancer can redirect users with active sessions to other servers, and because state was replicated there, users can
continue their work.

## Architecture

Orbeon Forms achieves replication by enabling the replication of servlet sessions and of caches. All current state in
memory, whether in the session or relevant caches, is replicated so that work can be resumed on replica servers when
needed.

Sessions are still sticky for performance reasons. Because Orbeon Forms stores a lot of information in memory, and there
are data structures associated with that information, there is a cost to recreate all necessary data structures at each
request. Therefore, requests for a given user must constantly reach the same server. However, if a server
fails, then there is a one-time cost to recreating data structures on the new server for the given user (in fact, for
a given form in use by that user).

Servers must be able to communicate via IP multicast. This means that they must typically be on the same network,
physical or virtual.

A load balancer is required. It is in charge of proxying client requests to specific servers, detect which servers
might have failed or are being brought back, and ensuring session affinity.


![Replication architecture](images/replication.png)

## Configuration

### Orbeon Forms configuration

#### Properties

Orbeon Forms has a single property enabling replication:

```xml
<property
    as="xs:boolean"
    name="oxf.xforms.replication"
    value="true"/>
```

By default, this property is set to `false`, because there is a cost to serializing the state of forms after each update in
memory.

In addition you might need to set the following property to point to the local Orbeon Forms instance without going
through the load balancer:

```xml
<property
    as="xs:anyURI"
    name="oxf.url-rewriting.service.base-uri"
    value="http://localhost:8080/orbeon"/>
```

#### web.xml

The application's `web.xml` must contain:

```xml
<distributable/> 
```

In addition, the `ReplicationServletContextListener` must be enabled. This is the case by default in the `web.xml`
that ships with Orbeon Forms.

#### Ehcache

The Orbeon Forms `WEB-INF/resources/config/ehcache.xml` must be modified to included replication settings, which are
turned off by default. This is similar to Tomcat session replication.

*NOTE: There isn't as single set of settings to replicate the Tomcat servlet session and Ehcache, as the two products
use different libraries for replication. But the idea is that both configuration should behave as closely as possible
from each other.* 

The keys to this configuration are:

- for relevant caches
    - `RMICacheReplicatorFactory` as `<cacheEventListenerFactory>`
    - `RMIBootstrapCacheLoaderFactory` as `<bootstrapCacheLoaderFactory>`
- global
    - `RMICacheManagerPeerProviderFactory` as `<cacheManagerPeerProviderFactory>`
    - `RMICacheManagerPeerListenerFactory` as `<cacheManagerPeerListenerFactory>`

Here is an example configuration:
 
<!--
[[TODO: This must be refined.]]
-->

```xml
<ehcache updateCheck="false" monitoring="off" dynamicConfig="true">

    <!-- Where the disk store will go -->
    <diskStore path="java.io.tmpdir/orbeon/cache"/>

    <!-- Default cache (not used by Orbeon) -->
    <defaultCache
            maxElementsInMemory="10000"
            eternal="false"
            timeToIdleSeconds="120"
            timeToLiveSeconds="120"
            overflowToDisk="true"
            diskSpoolBufferSizeMB="30"
            maxElementsOnDisk="10000000"
            diskPersistent="false"
            diskExpiryThreadIntervalSeconds="120"
            memoryStoreEvictionPolicy="LRU"
            statistics="false"/>

    <!-- XForms state store configuration. Only modify if you know what you are doing! -->
    <!-- NOTE: We set this as a disk cache, but follow the Ehcache doc and set maxElementsInMemory to 1 instead of 0. -->
    <cache name="xforms.state"
           maxElementsInMemory="1"
           memoryStoreEvictionPolicy="LFU"
           overflowToDisk="true"
           diskSpoolBufferSizeMB="10"
           eternal="false"
           timeToLiveSeconds="0"
           timeToIdleSeconds="18000"
           diskPersistent="false"
           maxElementsOnDisk="0"
           diskExpiryThreadIntervalSeconds="120">

        <cacheEventListenerFactory
            class="net.sf.ehcache.distribution.RMICacheReplicatorFactory"
            properties="replicateAsynchronously=false"/>

        <bootstrapCacheLoaderFactory
            class="net.sf.ehcache.distribution.RMIBootstrapCacheLoaderFactory"
            properties="bootstrapAsynchronously=false"/>
    </cache>

    <!-- XForms resources. Only modify if you know what you are doing! -->
    <cache name="xforms.resources"
           maxElementsInMemory="200"
           memoryStoreEvictionPolicy="LFU"
           overflowToDisk="true"
           diskSpoolBufferSizeMB="1"
           eternal="true"
           timeToLiveSeconds="0"
           timeToIdleSeconds="0"
           diskPersistent="true"
           maxElementsOnDisk="0"
           diskExpiryThreadIntervalSeconds="120">

        <cacheEventListenerFactory
            class="net.sf.ehcache.distribution.RMICacheReplicatorFactory"
            properties="replicateAsynchronously=false"/>

        <bootstrapCacheLoaderFactory
            class="net.sf.ehcache.distribution.RMIBootstrapCacheLoaderFactory"
            properties="bootstrapAsynchronously=false"/>

    </cache>

    <!-- XForms XBL cache. Only modify if you know what you are doing! -->
    <cache name="xforms.xbl"
           maxElementsInMemory="200"
           memoryStoreEvictionPolicy="LFU"
           overflowToDisk="false"
           eternal="false"
           timeToLiveSeconds="0"
           timeToIdleSeconds="0"/>

    <cacheManagerPeerProviderFactory
        class="net.sf.ehcache.distribution.RMICacheManagerPeerProviderFactory"
        properties="
            peerDiscovery=automatic,
            multicastGroupAddress=228.0.0.5,
            multicastGroupPort=4446,
            timeToLive=1"
    />

    <cacheManagerPeerListenerFactory
        class="net.sf.ehcache.distribution.RMICacheManagerPeerListenerFactory"/>

</ehcache>
```

### Servlet container configuration

The servlet container must be configured to replicate the session information.

With Tomcat, this is done in `server.xml` within the `<Engine>` element:

```xml
<Cluster
    className="org.apache.catalina.ha.tcp.SimpleTcpCluster"
    channelSendOptions="6">

    <Manager
        className="org.apache.catalina.ha.session.DeltaManager"
        expireSessionsOnShutdown="false"
        notifyListenersOnReplication="true"/>
        
    <Channel className="org.apache.catalina.tribes.group.GroupChannel">
        <Membership
            className="org.apache.catalina.tribes.membership.McastService"
            address="228.0.0.4"
            port="45564"
            frequency="500"
            dropTime="3000"/>
            
        <Receiver
            className="org.apache.catalina.tribes.transport.nio.NioReceiver"
            address="auto"
            port="5000"
            selectorTimeout="100"
            maxThreads="6"/>

        <Sender className="org.apache.catalina.tribes.transport.ReplicationTransmitter">
            <Transport className="org.apache.catalina.tribes.transport.nio.PooledParallelSender"/>
        </Sender>
        
        <Interceptor className="org.apache.catalina.tribes.group.interceptors.TcpFailureDetector"/>
        <Interceptor className="org.apache.catalina.tribes.group.interceptors.MessageDispatchInterceptor"/>
        <Interceptor className="org.apache.catalina.tribes.group.interceptors.ThroughputInterceptor"/>
    </Channel>

    <Valve
        className="org.apache.catalina.ha.tcp.ReplicationValve"
        filter=".*\.gif|.*\.js|.*\.jpeg|.*\.jpg|.*\.png|.*\.htm|.*\.html|.*\.css|.*\.txt"/>
           
    <ClusterListener className="org.apache.catalina.ha.session.ClusterSessionListener"/>
</Cluster>
``` 

In that configuration, the following can be changed:

- the IP multicast address, here `address="228.0.0.4"`
- the IP multicast port, here `port="45564`

For details about the Tomcat configuration, see [Clustering/Session Replication HOW-TO](https://tomcat.apache.org/tomcat-8.0-doc/cluster-howto.html).

### Load balancer configuration

With HAProxy, a simple configuration looks like this:

```
global
    daemon
    maxconn 256
    debug

defaults
    mode http
    timeout connect 5000ms
    timeout client 50000ms
    timeout server 50000ms

frontend http-in
    bind *:8080
    default_backend servers

backend servers
    cookie JSESSIONID prefix nocache
    server s1 127.0.0.1:8888 maxconn 32 cookie s1 check
    server s2 127.0.0.1:8889 maxconn 32 cookie s2 check

```

This configuration round-robins between two servers, `s1` and `s2`, on two ports, 8888 and 8889.

In this example, the servers are accessed at address 127.0.0.1, but in practice they might be on different
physical servers.

For testing, you can start HAProxy with the following command:

```
haproxy -db -f haproxy.conf
```

For details about the HAProxy configuration, see the [HAProxy Configuration Manual](https://cbonte.github.io/haproxy-dconv/1.7/configuration.html).

## Other considerations

### Individual server load

Consider a scenario where you have two servers with replication enabled, and one of them fails. This means that users
from the failed server are redirected by the load balancer to the server which is still working. If, at the time of
failure, both servers were nearing their full capacity, then suddenly the only remaining server will have to handle
all the load.

This means that the load balanced servers should not be allowed to reach full capacity so that, in case of failure of
a single server, the remaining server can handle all the load. Theoretically, this means that each server, in normal
use, should be at under 50 % of total capacity.

Using more than 2 replicated servers allows using more of the available capacity of all servers in the case of a single
server failure.     

## Limitations

### Uploaded files

Uploaded files which are not yet saved to a database are currently not replicated. If a use is switched from one server
to another, Form Runner:

- checks all unsaved attachments
- if there are any
  - clears the associated temporary file path
  - shows an alert to the user
  
This requires users with unsaved attachments to re-upload their attachments. This is not ideal but it is likely that
the user still have the attachment or attachments available. 
  
### Loss of state

If a server fails instantly before it had the chance to replicate the latest modifications to a form, and after an
Ajax response has been sent to the client, then state might be lost. The user is redirected by the load balancer
to another server, but state will be missing from that server. In such cases, the user will see an error, and won't
be able to continue working with the form. Unsaved data will be lost.

In such cases, enabling the [autosave feature][2] can alleviate the issue.

The `ehcache.xml` configuration provided above attempts to minimize this kind of issues by adding
`replicateAsynchronously=false`. 

## See also

- [Installation](README.md)
- Blog post: [High-Availability Thanks to State Replication](http://blog.orbeon.com/2018/03/high-availability-thanks-to-state.html)
- [Clustering and High Availability](../configuration/advanced/clustering.md)

[1]: https://www.orbeon.com/download
[2]: ../form-runner/persistence/autosave.md
