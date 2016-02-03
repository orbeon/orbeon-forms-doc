# Advanced Submissions - Asynchronous Submissions

<!-- toc -->

## Rationale

* Asynchronous submissions are specified in XForms 1.1.
* There are interesting use cases for them, including:
    * Background autosave
    * Background loading of initial data from multiple services in parallel

Orbeon Forms supports true asynchronous submissions (with some limitations due to the client-server architecture of Orbeon Forms).

## Declaring an asynchronous submission

You enable an asynchronous submission with `mode="asynchronous"`

```xml
<xf:submission 
    id="call-service" 
    mode="asynchronous"
    method="get" 
    resource="http://www.orbeon.com/ops/xforms-sandbox/service/zip-states"
    serialization="none" 
    xxf:cache="true"
    replace="instance" 
    targetref="instance('zip-states')"/>
```

This is useful for example to speed up the initial load of data from slow services when the page initializes because the background submissions can run in parallel, and at the same time the page can pursue its own initialization.

_NOTE: While XForms 1.1 specifies that `mode="asynchronous"` should be the default, in Orbeon Forms `mode="synchronous"` is the default._

## The xxf:join-submission action

### Availability

This is an [Orbeon Forms PE][1]

### Configuration

This extension action allows waiting until all pending background submissions have completed:

```xml
<xf:action ev:event="xforms-model-construct-done">
    <xf:send submission="query-people"/>
    <xf:send submission="query-places"/>
    <xxf:join-submissions/>
</xf:action>
```

In this example, upon `xforms-model-construct-done`, two background submissions are started. The `<xxf:join-submissions>` action waits until they both have completed.

This has two benefits:

* For performance reasons, it might be a good idea to wait until they terminate before allowing the controls to initialize.
* It might not make sense to show the initial page to the user if data for "people" and "places" is not available.

_NOTE: If processing background submissions causes new background submissions to run, their results are processed as well._

_NOTE: If the resulting instance is cacheable with `xxf:cache="true"` and already in cache, it is immediately retrieved and no background submission takes place.__

_NOTE: Optimized submissions and submissions with `replace="all" are not supported in asynchronous mode.`_

## How things work

The submissions are asynchronous in the following way:

* The request itself (`http:`, `https:`, `oxf:` and `file:` are supported) runs in a separate execution thread
* Deserialization of the response is also performed in that separate thread if needed:
    * for instance replacement, it creates an XML document (read-only or read-write)
    * for text replacement, it reads the text content as a String

The completion of asynchronous submission is checked at these time:

* Just before sending the initial HTML page to the client
* Upon receiving an Ajax request
* Just before completing an Ajax request

When completed submissions are found, the result of each submission is processed:

* Instance replacement or text replacement is done.
* `xforms-submit-done` / `xforms-submit-error` are dispatched.

If an asynchronous submission is still running (pending) at the time an client HTTP connexion returns (whether an initial page load or an Ajax request):

* The client is instructed to poll the server at regular intervals, or when an Ajax request is sent for other reasons.
* Only once a client request reaches the server are pending asynchronous submission results processed.

The polling delay in milliseconds can be configured with the following property:

```xml
<property 
    as="xs:integer" 
    name="oxf.xforms.submission-poll-delay" 
    value="10000"/>
```

[1]: http://www.orbeon.com/download

