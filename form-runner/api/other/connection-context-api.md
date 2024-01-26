# Connection context API

## Availability

[\[SINCE Orbeon Forms 2023.1.1\]](/release-notes/orbeon-forms-2023.1.1.md)

## Rationale

Orbeon Forms can perform internal service connections that run on different threads than the incoming service thread. In case information related to the incoming service thread needs to be passed downstream, an implementation of this interface can be used. For example, information can be retrieved and set from and to `ThreadLocal`s.

Use of this API is expected to be fairly rare.

## API

### Java interface

The following describes the Java interface you need to implement:

```java
package org.orbeon.connection;

import java.net.URI;
import java.util.Map;

/**
 * Interface for a provider that can provide a connection context. Orbeon Forms can perform internal
 * service connections that run on different threads than the incoming service thread. In case information
 * related to the incoming service thread needs to be passed downstream, an implementation of this interface
 * can be used. For example, information can be retrieved and set from and to `ThreadLocal`s.
 *
 * @tparam Context The type of the context object, defined by the implementation
 */
public interface ConnectionContextProvider<Context extends Object> {

    /**
     * Get context information. This method is called on the incoming service thread.
     *
     * The returned object must not keep references to `Thread` or other mutable data, as the calling thread
     * can be returned to a thread pool before `pushContext()` is called.
     *
     * This can be called multiple times for the same request, for example if multiple outgoing connections
     * are made.
     *
     * In addition, this can be called once and followed by multiple calls to `pushContext()`/`popContext()`,
     * for example when saving a document with multiple attachments.
     *
     * Conversely, this might be called but not followed by any calls to `pushContext()`/`popContext()`, for
     * example if an error happens before the outgoing connection is made.
     *
     * @param extension immutable extension map, reserved for the future, currently empty and not `null`
     * @return instance of context information or `null`
     */
    Context getContext(Map<String, Object> extension);

    /**
     * Push context information. This method is called on the outgoing connection thread.
     *
     * If `getContext()` returned `null`, this method is not called.
     *
     * @param ctx       instance of context information, as returned by `getContext()`; not `null`
     * @param url       URI containing information about the connection
     * @param method    HTTP method name (`GET`, `PUT`, etc.)
     * @param headers   HTTP headers
     * @param extension immutable extension map, reserved for the future, currently empty and not `null`
     */
    void pushContext(
        Context ctx,
        URI url,
        String method,
        Map<String, String[]> headers,
        Map<String, Object> extension
    );

    /**
     * Pop context information. This method is called on the outgoing connection thread. The purpose is to
     * allow cleanup of resources allocated by `pushContext()`. For example, a `ThreadLocal` can be removed.
     *
     * If `getContext()` returned `null`, this method is not called.
     *
     * This is guaranteed to be called after the outgoing connection is done, even if an exception is thrown.
     *
     * @param ctx instance of context information, as returned by `getContext()`; not `null`
     */
    void popContext(Context ctx);
}
```

### Registering a provider

You implement an instance of the interface above, and register it with Orbeon Forms by adding a file called:

```
META-INF/services/org.orbeon.connection.ConnectionContextProvider
```

The file contains a single line with the fully-qualified name of the implementation class, for example:

```
com.example.MyConnectionContextProvider
```

This file and directory hierarchy can be placed within the web application:

- in a JAR file under `WEB-INF/lib`
- or directly under `WEB-INF/classes`

## See also

- [File scan API](file-scan-api.md)
- [Form Runner APIs](../README.md)
