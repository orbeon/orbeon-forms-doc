# Memory and threads

## Memory

If you suspect that Orbeon Forms is running out of memory, add the following to your JVM options:

```
-verbosegc -XX:+PrintGCDateStamps -XX:+PrintGCTimeStamps -XX:+PrintGCDetails
```

This will output GC information to the Tomcat logs. The next time Orbeon Forms locks or gets slow, check the GC information in the servlet container logs. This might indicate whether Orbeon Forms blocks because of an actual memory issue or not.

## Deadlocks

If you suspect that Orbeon Forms is encountering a deadlock, obtain a JVM thread dump. The easiest is to use the `kill` command on Linux/Unix. See for example [How do I generate a Java thread dump on Linux/Unix?](https://access.redhat.com/solutions/18178).
