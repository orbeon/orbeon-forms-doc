> [[Home]] ▸ [[Installation]]

## What this is about

[SINCE Orbeon Forms 4.8]

The *limiter filter* is a servlet filter designed to limit the number of concurrent form processing requests, in order to reduce the likelihood of the server running out of resources.

Loading Form Runner pages and updating form state can be CPU and memory intensive, and a high level of concurrency for these operations is not desirable (even as we are working to improve the level of concurrency).

In the worst case, a large number of concurrent requests will compete for CPU and memory, causing slowdowns and possibly causing the server to run out of memory. In other cases, throughput can still be non-optimal.

It is therefore more efficient, past a certain level of concurrency, to *serialize* requests. Default Servlet containers settings are usually not adapted (Tomcat for example sets the default at 200 threads). They usually allow you to reduce the number of concurrent threads (Tomcat for example has settings on the `<Connector>` element), but these settings have drawbacks:

- Tomcat has a minimum of 10 concurrent threads.
- Other containers have settings which are very hard to figure out.
- Only *some* heavy requests must be serialized.

Therefore Orbeon Forms ships with a filter which implements the limiting mechanism internally.

## Configuration

The filter is enabled by default in Orbeon Forms 4.8. It is configured in `WEB-INF/web.xml`.

Here is a typical configuration:

```xml
<filter>
    <filter-name>orbeon-limiter-filter</filter-name>
    <filter-class>org.orbeon.oxf.servlet.LimiterFilter</filter-class>
    <!-- Include Form Runner pages and XForms Ajax requests -->
    <init-param>
        <param-name>include</param-name>
        <param-value>(/fr/.*)|(/xforms-server)</param-value>
    </init-param>
    <!-- Exclude resources not produced by services -->
    <init-param>
        <param-name>exclude</param-name>
        <param-value>(?!/([^/]+)/service/).+\.(gif|css|pdf|json|js|coffee|map|png|jpg|xsd|htc|ico|swf|html|htm|txt)</param-value>
    </init-param>
    <!-- Minimum, requested, and maximum number of concurrent threads allowed -->
    <!-- The `x` prefix specifies a multiple of the number of CPU cores reported by the JVM -->
    <init-param>
        <param-name>min-threads</param-name>
        <param-value>1</param-value>
    </init-param>
    <init-param>
        <param-name>num-threads</param-name>
        <param-value>x1</param-value>
    </init-param>
    <init-param>
        <param-name>max-threads</param-name>
        <param-value>x1</param-value>
    </init-param>
</filter>
<filter-mapping>
    <filter-name>orbeon-limiter-filter</filter-name>
    <url-pattern>/*</url-pattern>
    <dispatcher>REQUEST</dispatcher>
</filter-mapping>
```

The filter applies to all incoming requests. However, internally, the filter only limits requests matching paths which:

- match the `include` parameter regular expression
- and do *not* match the `exclude` parameter regular expression

The default settings are meant to apply to:

- requests for forms
- XForms Ajax requests

but *not* to:

- assets such as JavaScript, CSS and images files
- file uploads

The number of threads which can run concurrently is based on the number of CPUs advertised by the JVM. This typically includes hyperthreading: for example, a laptop with 4 cores advertises 8 "CPUs".

In the configuration, `x1` means the advertised number of CPUs, `x2` means twice that, `.5x` means half that, etc. A fixed integer number can also be set.

The filter ensures that the effective number of concurrent threads specified with `num-threads` is not less than that specified by `min-threads` and not larger than `max-threads`.

## Disabling the filter

Remove or comment-out the relevant `<filter-mapping>` in `WEB-INF/web.xml`.

## See also

- [Original issue](https://github.com/orbeon/orbeon-forms/issues/1971)
