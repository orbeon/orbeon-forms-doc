# Loading initial form data

<!-- toc -->

## The problem   

You just created a nice form with XForms. But how do you get it to load initial data, for example coming from a database or a service? You have two ways of doing this, depending on whether a _push_ or _pull_ method is more suitable to your situation.

If you design your forms with Form Builder, rather than writing XForms by hand, see how to [load an initial instance in Form Runner](../../configuration/properties/form-runner-detail-page.html#initial-data).

## The "push" solution 

Many frameworks use a solution that goes like this:  

- The "model" layer or backend fetches data from services and databases.
- It stores them in objects.
- It then calls up the "view" layer, which somehow accesses those objects.

In other words, the application _pushes_ its data to the view.

With Orbeon Forms, you can achieve something similar when using Java and the [Orbeon XForms filter](../filter.md):  

- Store data in the request in the form of a DOM or a String containing serialized XML.
- Extract the data upon form initialization with the `xxf:get-request-attribute()` function.

Suppose the following initially empty instance:

```xml
<xf:instance id="user-data">
    <registration>
        <first-name>
        <last-name>
    </registration>
</xf:instance>
```
  
Your Java code stores the property into the request, e.g.:

```java
request.setAttribute("my-user-data", myDataAsXML)
```
  
In the XForms page, the following action reads the attribute and uses it to replace the initial content of the `user-data` instance:

```xml
<xf:insert
    event="xforms-model-construct-done"
    ref="instance('user-data')"
    origin="xxf:get-request-attribute('my-user-data')">
```

## The "pull" solution

With XForms, another solution often feels more natural, maybe because XForms has a natural fit for web services. It goes like this:  

- The XForms page is loaded (not necessarily involving any Java code at all).
- Upon initialization, the page calls a service (with `<xf:submission>`) which fetches the initial data

In other words, the XForms page _pulls_ the data from a service.

You do this simply with a submission that runs during form initialization:

```xml
<xf:send 
    event="xforms-model-construct-done" 
    submission="load-data-submission">
  
<xf:submission
    id="load-data-submission"
    method="get" 
    serialization="none" 
    resource="http://example.org/service/load-initial-form-data-pull-instance" 
    replace="instance" 
    instance="user-data"/>
```
  
Often, initial instance data depends on parameters. A submission running during initialization can get and set URL parameters:

```xml
<xf:submission 
    id="load-data-submission"
    method="get" 
    serialization="none" 
    resource="http://example.org/service/load-initial-form-data-pull-instance?customerId={
        xxf:get-request-parameter('customerId')
    }" 
    replace="instance" 
    instance="user-data"/>
```
  
The above submission passes through to the called service a parameter called `customerId`:  

- It reads it from the current request parameter using the `xxf:get-request-parameter()` function.  
- It appends it to the submission URL so that the service can return data specific to the given customer.  

_NOTE: In the submission, you are not limited to using the HTTP GET method and request parameters: you can configure the submission to obtain data through POST, for example._

If all the submission is doing is using HTTP GET and if the URL of the service is static, you can get by with a simple `src` or `resource` attribute on `<xf:instance>`:

```xml
<xf:instance 
    id="user-data" 
    src="http://example.org/service/load-initial-form-data-pull-instance"/>
```
  
The simple `<xf:instance>` solution works in scenarios where the data to retrieve is static, or only dependent on the time it is retrieved, or some parameters to the current request.
  
_NOTE: In that case, you cannot append dynamic request parameters as with `<xf:submission>`._  

## The "POST" solution

If your XForms page responds to an HTTP POST containing XML, then it can access the content of the POST data with a special URL called `input:instance`:

```xml
<xf:instance 
    id="user-data" 
    src="input:instance"/>
```
  
This results in the `user-data` instance being populated with the XML data posted to the XForms page. It's as easy as this!

_NOTE: Nothing prevents you to combine this method with getting data from the request or a service._  

## The "dynamically generated page" solution  

In theory, you could also, when using Java and/or JSP, directly insert the right instance data into a dynamically-generated form.

This however has some negative performance impact, because this means that the XForms engine can no longer cache the page definition, because it is likely to change at every request.

Therefore, we do not recommend this solution, and instead using a request object or a service as described above.
