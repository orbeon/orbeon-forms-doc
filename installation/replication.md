# Replication

<!-- toc -->

## Availability

This feature is available since Orbeon Forms 2017.2. This feature has been tested with the following software:

- Apache Tomcat 8.0.45
- HAProxy 1.7.4

## Introduction

The purpose of replication is to provide high-availability of Orbeon Forms with as little disruption as possible to
users currently filling out forms. This is achieved by replicating state between servers.

Consider a simple scenario of load-balancing with two servers, with sticky sessions (that is, a given user's requests always
reach the same server). If one of the servers fails, new users will be assigned to the other server. So the system
remains operational from that point of view. However, users with active sessions will have their current work lost, as
the content will still in the failed server-memory.

Replication changes that by replicating state to one or more additional servers. So if a server goes down, the load-
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

<!-- TODO: diagram -->

## Configuration

### Orbeon Forms configuration

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


The application's `web.xml` must contain:

```xml
<distributable/> 
```

In addition, the `ReplicationServletContextListener` must be enabled. This is the case by default in the `web.xml`
that ships with Orbeon Forms.

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


<!--
## See also

- xxx
-->