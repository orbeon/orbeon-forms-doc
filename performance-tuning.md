# Performance tuning

NOTE: Not linked yet.


## xxxx

[SINCE Orbeon Forms 2018.1]

xxx very large forms: use wizard

Large forms (with hundreds or thousands of fields) using the wizard view now use much less HTML markup, as only the content of the visible page is sent to the web browser (lazy page load). In order for this to be effective, your form should be split into sections and subsections.

## xxxx






TODO: separate items which are useful to Form Runner/Form Builder user deployments first. Make plain XForms/XSLT/XPL separate.

## Sizing

### Parameters

When sizing an Orbeon Forms application, or considering whether you need to improve the performance of your application, you need to take into account the following parameters:  

* The first three parameters depend on how your application is being used. The ideal way to estimate those values is to get actual end users to interact with your forms (not tester, who will most likely be much faster than typical users), and infer those values based on the logs:
    * **Page load frequency** (_PF_) – How often does a single active user stay on a given page before loading another page. This could go, say, from 30 minutes (or longer!) for complex forms to 30 seconds for super-simple forms, or pages that don't really look like forms (publishing).
    * **Ajax request frequency** (_AF_) – How often does the browser of one active user issue Ajax requests to the server, e.g. 30 seconds.
    * **Peak active users** (_AU_) – How many concurrent users do you expect to have at peak time during the day.
* The next two parameters depend on your application, the type of server it is deployed on, and how efficient Orbeon Forms is. These two values must only include the time spent in Orbeon Forms, not the time spent waiting for any third-party system (e.g. database, REST or web service) to respond:
    * **Page load time** (_PT_) – How long the server takes to generate a page for a single user.  
    * **Ajax request time** (_AT_) – How long the server takes to service an Ajax request, not counting time spent. 

### Number of cores needed

Roughly, the number of CPU cores you will need will be:

_C_ = (_PT_ / _PF_ \+ _AT_ / _AF_) * _AU_  

  
This is of course an approximation, and assumes that if for a single user, a CPU core take 100 ms to respond, under load it will be able to handle 10 requests per seconds. The actual number of requests per second per core can be slightly higher (e.g. thanks to [hyper-threading][1] on modern processors) or slightly lower if there is contention. Note that excessive contention, i.e. if the number of requests handled on a given server under load is significantly lower than 1 divided by the time the server takes to handle one request for a single user, then take this as a sign that you need to improve the system configuration to avoid contention.

### Examples

* **Large, complex form** – Because the form is quite large, users spend quite a bit of time one it (say, 30 minutes). Maybe this is even the only form that people fill out, which is factored in the peak number of concurrent active users. Because the form is rather complex, Ajax requests are also relatively expensive (say, 250 ms). Imagine in this case the parameters are: _PF_ = 1800 (30 minutes), _AF_ = 30, _AU_ = 100, _PT_ = 1, _AT_ = 0.2. Then this application should run comfortably with even just 1 core, as:

_C_ = (1/1800 + 0.2/30) * 100 = 0.72

* **Simple form** (publishing-like) – Here your forms are quite simple, they just have a few fields, so users spend less time on a given page, and so load more pages. Consider you have _PF_ = 60 (1 minute), _AF_ = 30, _AU_ = 100, _PT_ = 0.25, _AT_ = 0.100.

_C_ = (0.25/60 + 0.1/30) * 100 = 0.75
  
Most likely, your application sit somewhere between the first case (very large forms: lots of time spent on the form, few pages loaded) and the second case (simple forms: very little time spent on the form, lots of page loads). The significant difference is in what contributes to the load. In the first case, 92% of the load comes from Ajax requests, while in the second case only 44% of the load comes from Ajax requests. With this knowledge, if an optimization is needed, you can determine whether page loads or Ajax requests need more of your attention.

## Tuning the Java Virtual Machine (JVM)

### Set -Xms and -Xmx to the same value

The heap is a section of memory used by the JVM to store Java objects. You can set constraints on the size of the heap with two parameters passed to the JVM at startup: -Xms sets the initial size of the heap and -Xmx sets the maximum size of the heap. If you set those two parameters to different values, say 512MB and 1GB, you tell the JVM to start with a heap size of 512MB and increase the size up to 1GB "as necessary". In this case, the JVM has to balance two conflicting constraints: not request too much memory from the operating system (getting too fast to 1GB), and not request too little as it would increase the amount of time the to spends on garbage collection which would will reduce the performance of your application.

Asking the JVM to balance memory usage and performance by setting different values for -Xms and -Xmx is very reasonable for desktop applications where a number of applications are running and competing for resources, in particular memory. However, in a server environment you often have one or two major applications running on the server, like the JVM for your application server and maybe a database server. In this situation you have more control over how much memory can be used by each application, and we recommend you set both -Xms and -Xmx to the same value.

### Allocate a large heap but don't cause swapping

The larger the heap, the faster your application will be get. This for two reasons: first, the JVM garbage collector works more efficiently with a larger heap, and second, this enables you to increase the size of the Orbeon Forms cache (more on this later) which will also improve the performance of your application. However, don't use a heap size so large that it would cause swapping, as this would then drastically reduce the performance of your application.

We recommend that you first set the heap size based on how much memory the server has and what other major applications are running. Say you have 2GB of physical memory, and no other major application: then you could set the heap to 1.5 GB, which leaves 512 MB to the operating system and minor applications. Say you have 4 GB of physical memory and also a database running on the same server, then you can set the heap size to 2 GB, assign 1.5 GB to the database server, and leave 512 MB to the operating and minor applications.

Then, with a "reasonable" setting in place, monitor the server under normal load and look if the machine is swapping or if on the contrary the operating system is reporting a lot of available memory. In the first case, reduce the heap size. In the second, increase it.  

### Permgen errors

If you encounter JVM permgen errors:

* make sure to increase your VM's "permgen" space with `-XX:MaxPermSize=128m` or higher
* also check this thread on the ops-users mailing-list:
  [Tomcat Java memory optimization][2]
  
*NOTE: Java 8 no longer has a permgen setting.*

### Tuning the stack

The maximum stack size per thread is not dynamic in the Sun/Oracle JVMs as of 2011-11. This means that code with deep call stacks, for example code using complex XPL and/or XSLT, can cause errors. You might get plain `java.lang.StackOverflowError` errors, or indirect errors such as the following XSLT exception:

```xml
org.orbeon.saxon.trans.XPathException: Too many nested apply-templates calls. The stylesheet may be looping.
```

The latter may indicate infinite recursion, but it can also indicate that the JVM stack is simply too small. This is what Oracle says about the default size of the stack:

_"In Java SE 6, the default on Sparc is 512k in the 32-bit VM, and 1024k in the 64-bit VM. On x86 Solaris/Linux it is 320k in the 32-bit VM and 1024k in the 64-bit VM. On Windows, the default thread stack size is read from the binary (java.exe). As of Java SE 6, this value is 320k in the 32-bit VM and 1024k in the 64-bit VM."_

In such cases, you can try increasing the stack size from the current or default value, for example with the following JVM parameter:

```xml
-Xss1024k

or

-Xss2048k
```

The value to specify should be something higher than what your JVM is running when causing the error.

The drawback of increasing the stack size is that threads require more memory. That might not be a big issue unless you are running hundreds or thousands of threads, which is not typical.

## Tuning the application server

### WebLogic: Disable automatic redeployment

WebLogic can be configured to check on a regular basis if the files of your application have changed on disk, and redeploy the application if they did. Redeploying at application server level is useful when you change the JAR files or some of the underlying configuration files, like the web.xml. As checking if files have changed is incredibly time consuming with WebLogic, and as you are pretty unlikely to change any of those files on a regular basis, we recommend you disable the automatic web application redeployment feature, which is enabled by default.

To do this, after you have installed your Orbeon Forms application, stop WebLogic, and open the `config.xml` file in an editor. Look for the `<webappcomponent name="orbeon">` element and add the attribute: `ServletReloadCheckSecs="-1"`.

### Disable DNS lookup

You can configure your application server to perform a DNS lookup for every HTTP request. The server always know the IP address of the machine where the HTTP request originated. However, to get the name, the application server needs to send a DNS lookup query to the DNS server. In most cases, performing this query only has a negligible impact on performance. However, the request can take a significant amount of time in certain cases where the network from which the request originated is badly configured. In most case, the application server is doing DNS lookups for "aesthetic reasons": that is to able to in include in the logs the name of the client's machines, instead of their IP address (note that web analysis tools can usually do this reverse DNS lookup much more efficiently when analyzing log files subsequently, typically on a daily basis). So we recommend you change the configuration of your application server to disable DNS lookup, which is in general enabled by default.

On Tomcat 5.5 ([external documentation][3]), look for the `enableLookups` attribute on the `<connector>` element and set it to false. If the attribute is not present, add it and set it to `false` (the default value is true).

### Enable gzip compression for generated text content

HTML and XML content usually compresses extremely well using gzip. You can obtain sizes for the content sent by the server to the web browser that are up to 10 times smaller. A very complex XForms page taking 100 KB, for example, may end up taking only about 10 KB. This means that the data will reach the web browser up to 10 times faster than without gzip compression.

Most web and application servers support gzip compression. For example, Tomcat 5.5 supports the attribute gzip on the <connector> element. For more information, please see the [Tomcat HTTP connector documentation][3].

### Reduce the number of concurrent processing threads

Servlet containers like Tomcat, or application servers like WebLogic, by default allow a very large number of concurrent threads to enter a servlet. For Tomcat, the default is 200. This means that memory usage cannot be effectively bound. It is enough to have a few requests slightly overlapping to cause extra memory consumption that can lead to Out of Memory errors. In addition, extra memory usage leads to poorer performance. As a rule of thumb, you can set the maximum number of concurrent threads to be twice the number of cores available on the current machine (assuming this is the only Java VM and application you run on that machine). For instance, on a machine with 2 dual core CPUs, you would typically set the maximum number of concurrent threads to 8. If your application spends a significant amount of time waiting for external resources (like a database or REST/web services), then you might need to use a higher value.  

#### Tomcat configuration

Tomcat appears to accept only a minimum of 10 for the `maxThreads` configuration parameter. Hence Tomcat does not effectively allow you to reduce the number of concurrent processing threads to less than 10:  

* In most cases, a value of 10 or higher is not optimal, which means that with Tomcat, you need to limit the number of concurrent threads entering Tomcat through some other mechanism, typically on your front-end HTTP server or load balancer. If you are using Apache as a load balancer, see [Apache configuration][4] below. If you really have no way of setting this up on a front-end HTTP server or load balancer, setting `maxThreads` to 10 is still better than doing nothing.  
* Should a value of 10 or higher be acceptable in your case, you can configure it in your `server.xml`, by adding a `maxThread="10"` on the `<connector``>` element.
  

#### Apache configuration

When doing load balancing with Apache's [mod_proxy][5], you should use the `max` parameter to set the hard maximum number of connection allowed to any given Tomcat. For instance, a configuration that limits the number of concurrent connections to each one of your back-end Tomcat server to 4 would look like:

```xml
ProxyPass / balancer://mycluster/ stickysession=JSESSIONID|jsessionid
<Proxy balancer://mycluster>
    BalancerMember ajp://192.168.0.100:8009 max=4
    BalancerMember ajp://192.168.0.101:8009 max=4
    BalancerMember ajp://192.168.0.102:8009 max=4
    BalancerMember ajp://192.168.0.103:8009 max=4
</Proxy>
```

Unfortunately, within Apache, the value of `max` is per process. So, you can only effectively limit the number of connections to your back-end servers if Apache has one process and uses threads instead of processes to handle multiple connections, which depends on what [MPM][6] is being used by Apache:  

* **On Windows**, you should be all good and most likely don't have to worry about this, as the [winnt MPM][7] uses one process which in turn creates threads to handle requests.
* **On UNIX**, if you're using the Apache that came with your OS, unfortunately there is a good chance you have [prefork MPM][8] Apache, which creates one process per request, and with which the `max` parameter wouldn't work:
    * To check what MPM you have, run `apachectl -l`.
    * In the list, if you see `worker.``c` or `event.c`, then you are almost good: you now just need to make sure that Apache creates only one process. For this, set `ThreadsPerChild` and `MaxClients` to the same value, which will be the total number of concurrent connections your Apache will be able to process. Also set `ServerLimit` to 1.  
    * In the list, if you see `prefork.c`, then you first need to replace your Apache with the worker or event MPM Apache. You can do so by either recompiling Apache yourself (the MPM is not a run-time configuration parameter), or getting a existing package for your platform. Then, go to step 2.

#### WebLogic configuration

With WebLogic, edit the `WEB-INF/weblogic.xml`, and add the following elements:

```xml
<wl-dispatch-policy>OrbeonWorkManager</wl-dispatch-policy>

<work-manager>
    <name>OrbeonWorkManager</name>
    <max-threads-constraint>
        <name>MaxThreadsConstraint</name>
        <count>4</count>
    </max-threads-constraint>
</work-manager>
```

The `<work-manager>` element defines a new work manager with a constraint of a maximum of 4 concurrent threads. Then the `<wl-dispatch-policy>` instructs WebLogic to use the work manager you defined for the current web application.

#### Testing your configuration

You want to test your configuration to make sure it is effective. For this you issue concurrent requests to `/xforms-sandbox/service/image-with-delay`, which serves an image after a delay of 5 seconds. Use a tool like JMeter or the [Apache HTTP Server Benchmarking Tool][9] (`ab`) to issue 10 requests at the same time. With `ab`, for instance, run:

```xml
ab -n 10 -c 10  http://localhost/orbeon/xforms-sandbox/service/image-with-delay
```
  
Assuming you set the maximum number of concurrent processing threads to 2, you will then see in the logs that the request are handle 2 by 2:

```xml
INFO  ProcessorService  - /xforms-sandbox/service/image-with-delay - Received request
INFO  ProcessorService  - /xforms-sandbox/service/image-with-delay - Received request
INFO  ProcessorService  - /xforms-sandbox/service/image-with-delay - Timing: 5042 - Cache hits for cache.main: 289, fault: 1, adds: 0, expirations: 0, success rate: 99%
INFO  ProcessorService  - /xforms-sandbox/service/image-with-delay - Timing: 5042 - Cache hits for cache.main: 289, fault: 1, adds: 0, expirations: 0, success rate: 99%
INFO  ProcessorService  - /xforms-sandbox/service/image-with-delay - Received request
INFO  ProcessorService  - /xforms-sandbox/service/image-with-delay - Received request
INFO  ProcessorService  - /xforms-sandbox/service/image-with-delay - Timing: 5035 - Cache hits for cache.main: 289, fault: 1, adds: 0, expirations: 0, success rate: 99%
INFO  ProcessorService  - /xforms-sandbox/service/image-with-delay - Timing: 5034 - Cache hits for cache.main: 289, fault: 1, adds: 0, expirations: 0, success rate: 99%
INFO  ProcessorService  - /xforms-sandbox/service/image-with-delay - Received request
INFO  ProcessorService  - /xforms-sandbox/service/image-with-delay - Received request
INFO  ProcessorService  - /xforms-sandbox/service/image-with-delay - Timing: 5031 - Cache hits for cache.main: 289, fault: 1, adds: 0, expirations: 0, success rate: 99%
INFO  ProcessorService  - /xforms-sandbox/service/image-with-delay - Timing: 5025 - Cache hits for cache.main: 289, fault: 1, adds: 0, expirations: 0, success rate: 99%
INFO  ProcessorService  - /xforms-sandbox/service/image-with-delay - Received request
INFO  ProcessorService  - /xforms-sandbox/service/image-with-delay - Received request
INFO  ProcessorService  - /xforms-sandbox/service/image-with-delay - Timing: 5035 - Cache hits for cache.main: 289, fault: 1, adds: 0, expirations: 0, success rate: 99%
INFO  ProcessorService  - /xforms-sandbox/service/image-with-delay - Timing: 5041 - Cache hits for cache.main: 289, fault: 1, adds: 0, expirations: 0, success rate: 99%
INFO  ProcessorService  - /xforms-sandbox/service/image-with-delay - Received request
INFO  ProcessorService  - /xforms-sandbox/service/image-with-delay - Received request
INFO  ProcessorService  - /xforms-sandbox/service/image-with-delay - Timing: 5031 - Cache hits for cache.main: 289, fault: 1, adds: 0, expirations: 0, success rate: 99%
INFO  ProcessorService  - /xforms-sandbox/service/image-with-delay - Timing: 5044 - Cache hits for cache.main: 289, fault: 1, adds: 0, expirations: 0, success rate: 99%
```

If you are using JMeter, it will also show you that while all the requests have been sent at the same time, the results come back 2 by 2 at 5 seconds interval:

![][10]

## Tuning Orbeon Forms

### Where does the time go?

Lots of different things can take time when loading an Orbeon Forms page, including:

* loading the form definition and/or data from the database (typically with Form Runner)
* XForms processing: complex forms are more resource-intensive
* loading of resources like CSS, JavaScript and images
* calling services from XForms
* client-side JavaScript processing in the browser

When there is a performance issue, it's a good idea to start by trying to figure out where the time is spent. Here are some tools to help, including:

* Tools to [monitor HTTP requests][?]
* [XForms logging][?]
* Browser development tools (see [JavaScript Development][?])

### Reduce the number of Ajax requests 

By default, events produced by users' interactions with a form are sent by the browser to the server right after the interaction happens. Events are sent by the browser to the server through Ajax requests. A combination of large forms and high traffic can result in a high number of Ajax requests hitting your server, which in turn can impact your site performance.

#### Filtering out specific events

[DEPRECATED: This feature is deprecated as of Orbeon Forms 4.5, as it is incompatible with forms created with Form Builder or executed by Form Runner.]

You may be able to reduce the number of Ajax events by filtering out the `xforms-focus` event. Obviously, you shouldn't filter out this event if your form relies on it to function properly. This is the case of forms created with Form Builder or executed by Form Runner, as these forms rely on xforms-focus to keep track of the fields that have been visited, and show only errors for those fields. So if you are using Form Builder or Form Runner, you should _not_ change the default value of this property.

Filtering out the `xforms-focus` event will:  

* Reduce the number of Ajax requests, as you will avoid having requests made in cases where users just tab through fields without changing any value.
* Reduce the size of the Ajax requests, which in turn reduces bandwidth usage between the browser and the server, and CPU usage on the server.

You configure this by setting the `oxf.xforms.client.events.filter` property. Its value is a space-separated list of event names to be filtered. By default that list is empty. To filter the xforms-focus event, set it to:

```xml
<property as="xs:string"  name="oxf.xforms.client.events.filter" value="xforms-focus"/>
```

If you don't want this property to act globally for all your forms, you can also set this property specifically on your form's `<xf:model>` element, i.e. `<xf:model ...="" xxf:client.events.filter="xforms-focus">.`

#### Indicate for what controls events should be sent to the server

A more aggressive step consists in setting the _client events mode_ to _differed_. By default, every time users change a value and tab to another field, an Ajax event is sent to server. Those events are useful is something else in the form can change based on the new value; for instance, you might enable a set of fields when users click on a checkbox. But for some forms, most of the values entered by users have no impact on the rest of the form. If this is the case for your form, and you notice that the load from Ajax requests impacts performance, then:  

1. Set _client events mode_ to _differed_. You can do this either by:
    * Setting the `oxf.xforms.client.events.mode` property to `deferred` in your `properties-local.xml`.
    * Adding the following attribute on the first `<xf:model>` of your form: `xxf:client.events.mode="deferred"`.
2. Add the class `xxforms-events-mode-default` on every control for which a value change can impact the rest of the form.  

Note:  

* If you have a group of controls for which Ajax requests need to be sent, instead of adding `xxforms-events-mode-default` on every one of those controls, you can put that class on a parent of those control.
* You don't need to add the class `xxforms-events-mode-default` to triggers; Ajax requests will be sent to the server when users click on buttons and links.
* This class is a hint you're giving to the XForms engine. This hint might become deprecated in the future as the XForms engine becomes "smarter" and manages to figure this out without you having to add this hint.

### Tune the Orbeon Forms cache size

One way to increase the performance of your application is to increase the size of the Orbeon Forms cache. You setup the size of the Orbeon Forms cache with the oxf.cache.size property. Due to limitations of the JVM, you cannot set the size of the Orbeon Forms cache in MB. Instead, the value you specify the maximum number of objects that Orbeon Forms can store in cache. As the size of each object stored in cache is different and the average size of those objects can change widely depending on your application, we can't give you an equivalence between number of objects and memory used. Instead, we recommend you follow the suggestions below to tune your Orbeon Forms cache size.

1. Keep the default value for the Orbeon Forms cache size, use the `-verbosegc` parameter when starting your JVM. With this option enabled, the JVM will log the garbage collection it does.  
2. Start hitting your application using a tool like JMeter, and monitor how the heap usage varies. In particular, pay a close attention to how much memory is freed by the full GCs done when the memory used gets close the heap size. A full GC will not get rid of objects in the Orbeon Forms cache.
    * So assuming your heap size is 2 GB, if after getting close to 2 GB a full GC brings back the heap usage to 0.5 GB, as the cached objects are included in those 0.5 GB, it means you can and should to increase the size of your Orbeon Forms cache.
    * However, you should not increase the size to a point where a full GC can only reclaim a small amount of memory, as this will trigger more frequent full GC and so degrade the performance of your application. Typically, after the application has been used for a while, you want a full GC to be able to reclaim 20 to 30% of the heap. If your heap size is 2 GB, this means reducing the heap usage to a value around 1.5 GB range. The optimal value will change depending on your JVM, server, and application, so you might want to try multiple values and see how they impact performance.
  

### Disable Saxon source location   

Make sure you are not overriding the following property in your `properties-local.xml` (the default is `none` which is what you want to have optimal efficiency):

```xml
<property as="xs:string"  processor-name="oxf:builtin-saxon"        name="location-mode" value="none"/>
<property as="xs:string"  processor-name="oxf:unsafe-builtin-saxon" name="location-mode" value="none"/>
<!-- This property was used prior to January 2010 builds -->
<property as="xs:string"  processor-name="oxf:saxon8"               name="location-mode" value="none"/>
```

You might want to override these properties and set them to `smart` during development in order to obtain better line number information. But keep in mind that this has a performance impact. If you have changed this property for development, make sure to set it back to `none` when testing and deploying your application.

## Tuning your application

### Reduce the size of your XML documents

With XML, it is very easy to add data to an existing document and then extract just the data you need from that document. This creates a tendency for the size of the documents manipulated by your application to grow as you progress on the development of your application. Who has never said "let's just add this information to this existing document", or "let's keep this information in the document and pass it around; you never know, we might need it in the future". While this might be just fine in some cases, you need to make sure that the size of your documents does not increase to the point where performance is impacted. If you uncover a performance issue, you should check the size of the documents you manipulate and reduce it when possible.

If you need to be further convinced, consider an application where pages are generated based on some information contained in an XML schema. This XML schema is stored in an XML database and takes about a 100 KB or 4000 lines when serialized. Because data contained in the file is needed in multiple locations, the file is passed around in a number of pipelines while generating a page, and is used overall as input to 10 processors. Each processor will create its own representation of the data in memory, which can take 10 times the size of the serialized XML. That means that each processor has to allocate 1 MB of objects and do some processing one those objects. At the end of the request, 10 MB of memory have been allocated to process this data, and the garbage collector will eventually have to spend CPU cycles on freeing this memory. What if out of the 4000 lines, only 400 are actually used? Starting by extracting those 400 lines and then passing only those to the processors means that the processors now need to do only one tenth of the work they were doing before. Clearly this type of modification can drastically improve performance.

If you are required to work with large documents, also consider using an XML database such as the open source eXist database, and delegate complex queries to the database: this should be more efficient than continually retrieving large XML documents and processing them in Orbeon Forms.

### Tune your XSLT code

Some operations in XSLT or XPath can be very expensive. This in particular the case for XPath expressions where the engine has to go through the whole input document to evaluate the expression. Consider this XPath expression: //person. To evaluate it, the engine iterates over every element in the document looking for a <person>. If you know that given the structure of the document, a person is inside a department, which is in a company, you can rewrite this as /company/department/person, which will typically run more efficiently.

If you can, also try to avoid running many XSLT transformations. In particular, you may be able to avoid running a theme stylesheet entirely. See also [Customize the standard epilogue][11].

### Customize the standard epilogue

The standard Orbeon Forms epilogue can be optimized for your own needs. For example:

* If you do not use the old-style (pre-Orbeon Forms 3.8) XSLT "widgets" (i.e. widget:tabs, which ws the only standard "widget"), you can remove the inclusion of `xforms-widgets.xsl.`
* If you are not using portlets (which is likely), you can remove the test for portlets.
* If you are using the `theme-plain.xsl` theme and have not modified it, you can bypass completely the theme stylesheet. You can do this with the following property:
```xml
<property as="xs:boolean" name="oxf.epilogue.use-theme" value="false">  
```

### Make sure you remove all your XPL debug attributes   

While using debug attributes is one of the best ways to debug XPL pipelines, those also have an impact on performance as they locally disable XPL caching and also require time to serialize XML documents to your logger. For performance testing and production, always remove all the debug attributes.

### Don't serve your static files through Orbeon Forms

It is overkill to serve static files such as static images through Orbeon Forms. Instead, use your servlet container's facilities for serving static files, or even better, [use a simple web server][13] such as Apache Server.

### Delay expensive submissions

There are times when you need to perform an expensive call to your backend to load data which is shown on your form. Typically, you do this by running an `<xf:submission> on xforms-model-construct-done`. If running that submission is really expensive (say, taking seconds), you might want to consider serving your form to the browser without that data, and loading it through Ajax as soon as the form displayed in the browser. In essence, you will:

1. In your page, where the data would normally show, have some sort of loading indicator displayed when the data is not present in the instance. You will typically use a spinning "loading" icon, or some text such as "Loading...".
2. On `xforms-model-construct-done`, dispatch an event to your model with JavaScript. Doing so in JavaScript ensures that the event is not dispatched right away, but instead dispatched from the browser, after the page has loaded. For instance, assuming your model has an ID my-model and the event you choose to dispatch is called `my-load-initial-data`:

```xml
<xxf:script ev:event="xforms-model-construct-done">  
    ORBEON.xforms.Document.dispatchEvent("my-model", "my-load-initial-data");  
</xxf:script>  
```

3. Allow this event to be dispatched by client-side code by adding xxf:external-events="my-load-initial-data" on your <xf:model>.  
4. Implement an event handler for this event inside your model. That handler then runs the submission which loads the data:

```xml
<xf:action ev:event="my-load-initial-data">  
    <xf:send submission="my-expensive-submission"/>  
</xf:action>  
```

### Use xf:instance to load dynamic instances

There are several ways of initialization XForms instances. For instances whose content is generated dynamically, run a submission on xforms-model-construct-done to load the instance instead of using XSLT or XInclude. This helps ensures that the source XForms page is cacheable.

### Perform initialization on xforms-model-construct-done

If you have initialization tasks to perform upon page initialization, try using xforms-model-construct-done instead of xforms-ready. This will cause less updates to controls and may yield better performance. See also the XForms reference for details.

## Tuning the Orbeon XForms engine  

### Enable minimal, combined, versioned resources  

* Configure _versioned resources_, to maximize client-side caching of CSS, JavaScript, and other resources. Versioned resources are not enabled by default.
* Make sure that the _minimal resources_ property is set to true (it is by default). When enabled, minimized versions of the JavaScript and CSS resources, are produced. Minimal resources load faster because downloading them takes less bandwidth, but also because being smaller the browser can then process them faster. See the XForms reference for details. Also enable gzip compression in addition.
* Make sure that _combined resources_ property is set to true (it is by default).  When enabled, multiple JavaScript files will be combined into one, and multiple CSS files will be combined into one.

For more information on this, see [XForms - JavaScript and CSS Resources][?].

### Consider using read-only and cached instances  

Some XForms instances that never change or that are simply replaced during a submission can be set as read-only, and can also optionally be cached between pages and even applications. Such instances take less memory and are more efficient to build. However they cannot hold data that changes over time and they cannot use XForms Model Item Properties (MIPs).

For more information on this, see [XForms - Performance Settings][14].

### Consider using asynchronous submissions

[TODO]  

### Consider not refreshing sets of items on selection controls  

The xxf:refresh-items attribute on selection controls allows for performance improvements in certain cases. See Controlling Item Sets Refreshes with xxf:refresh-items.

For more information on this, see [XForms - Performance settings][14].  

### Use server-side XForms state handling

The [XForms engine state][15] can be either stored on the server, or sent to the client and exchanged with the server with each request. By default, the XForms state is stored on the server. Make sure this is enabled.

### Tune XForms caches

[TODO]

### Control Ajax updates

See [Xforms - Performance settings][14]

### Check whether your XForms document is cacheable

The XForms engine performs some analysis (called static analysis) on XForms pages before rendering them. This includes:

* extraction into data structures of models, instances, controls, event handlers, actions, etc.
* processing of XBL templates
* analysis of XPath expressions

If the analysis can be stored in cache, performance is typically enhanced. Therefore it is important to ensure that caching occurs.

Here is how you can check whether a given XForms document can cache its static analysis:

1. Temporarily [enable logging][15], and make sure that the oxf.xforms.logging.debug property in your `properties-local.xml` contains `analysis`.
2. Make a request to one your page.
3. Then make the same request _again_ by reloading the page (click on the URL bar, and press enter) and check that static state is cached by looking at the `orbeon.log`. You should first see a line such as (instead of `your-page`, you will see `xforms-renderer` if you are using separate deployment):

```xml
INFO  ProcessorService  - /your-page - Received request
```

In the best case scenario, the entire input document is cacheable. The XForms engine can not only find the static state in cache but also obtain the static state digest without reading its input. You'll see:

```xml
DEBUG XFormsServer  - annotated document and static state digest obtained from cache {digest: "e709c987ea70dd6270e1065119b09eec"}
DEBUG XFormsServer  - found static state by digest in cache
```

In the second-best scenario, the input document must be read, but a digest is computed and the static analysis is found in cache. You will see:

```xml
DEBUG XFormsServer  - start reading input {}
DEBUG XFormsServer  - end reading input {time (ms): "324", computed digest: "e709c987ea70dd6270e1065119b09eec"}
DEBUG XFormsServer  - found static state by digest in cache
DEBUG XFormsServer  - annotated document and static state digest not obtained from cache
```

In the worst case scenario, the static state is never found in cache:

```xml
DEBUG XFormsServer  - did not find static state by digest in cache
```
  
This likely means that the XForms document changes at every request.

_NOTE: If your XForms page is generated from XSLT or JSP, and you insert changing inline instance data into it, then it is likely not cacheable._

It is a good idea to test the scenarios above also from multiple users (if your application handles multiple users), in order to make sure that a change in user keeps caching active.

### Making your XForms document is cacheable

The key to make the XForms document cacheable is to make sure the document that is fed to the XForms processor "doesn't change" between requests.

In Orbeon Forms, "change" usually means making changes to a file or one of its dependencies.

The absolute safest way to make the document cacheable is to keep it as a single static file on disk!

But this is not the only way. The document will also be cacheable if it depends on other documents which themselves are cacheable. For example, XSLT and XInclude transformations support this and in general allow caching of their resulting document, if the XSLT or XInclude document doesn't change and if all their dependencies don't change either.

_NOTE: There are exceptions, like using the `doc()` or `document()` functions in XSLT with with a dynamic parameter. In this case, caching is not possible._

More generally content that is provided with key/validity information that doesn't change (for example, the `oxf:scope-generator` processor) is also cacheable.

The general rule is the following:

    **_To keep your XForms document cacheable, don't construct it out of parts computed dynamically at each request._**

For example:

* don't create part of the XForms page from data coming from a database
* don't dynamically insert an XML instance coming from a database into `<xf:instance>` element

There are cases where the above can be done (for example if the data is made cacheable and rarely changes), but you have to be very careful about it.

For dynamic aspects in XForms, you can instead use `instance/@src`, submissions, functions like `xxf:get-request-parameter()`, etc.

See also [Load initial form data][?].

[TODO: explain how to load instance data efficiently]

## Other recommendations

### Use a performance analysis tool

To obtain your numbers, use a tool such as Apache JMeter. Be sure to warm up your Java VM first and to let the tool run for a significant number of sample before recording your performance numbers.

If you feel comfortable with the source code of Orbeon Forms, you can also use a Java profiler such as YourKit to figure out if a particular part of the Orbeon Forms platform is a bottleneck.

### Make sure of what you are measuring

If you are testing the performance of an application that talks to a database or backend services, be sure to be able to determine how much time your front-end or presentation layer, versus your backend and data layers, are respectively taking.

[1]: http://en.wikipedia.org/wiki/Hyper-threading
[2]: http://orbeon-forms-ops-users.24843.n4.nabble.com/Tomcat-Java-memory-optimization-td44935.html
[3]: http://tomcat.apache.org/tomcat-5.5-doc/config/http.html
[4]: http://wiki.orbeon.com/forms/doc/developer-guide/admin/performance-tuning#TOC-Apache-configuration
[5]: http://httpd.apache.org/docs/2.2/mod/mod_proxy.html
[6]: http://httpd.apache.org/docs/2.2/mpm.html
[7]: http://httpd.apache.org/docs/2.2/mod/mpm_winnt.html
[8]: http://httpd.apache.org/docs/2.1/mod/prefork.html
[9]: http://httpd.apache.org/docs/2.0/programs/ab.html
[10]: http://wiki.orbeon.com/forms/_/rsrc/1278373666668/doc/developer-guide/admin/performance-tuning/jmeter-concurrent.png
[11]: http://wiki.orbeon.com/forms/doc/developer-guide/admin-tuning#TOC-Customize-the-standard-epilogue
[13]: https://blog.orbeon.com/2006/12/configuring-apache-front-end-for-orbeon_27.html
[14]: http://wiki.orbeon.com/forms/doc/developer-guide/xforms-performance-settings
[15]: contributors/state-handling.md
[16]: http://wiki.orbeon.com/forms/doc/developer-guide/xforms-logging
