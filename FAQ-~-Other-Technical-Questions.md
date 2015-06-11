> [[Home]] ▸ [FAQ](./FAQ)

## How can I remove the sample applications from my Orbeon Forms deployment?

See [[Creating a Production WAR|Installation ~ Creating a Production WAR]].

## How can I remove Form Runner / Form Builder from my Orbeon Forms deployment?

Under the WAR file's `WEB-INF/lib` directory, remove:

- if you are not using Form Builder: `orbeon-form-builder.jar`
- if you are using neither Form Builder nor Form Runner: `orbeon-form-runner.jar`

*NOTE: Form Builder also requires `orbeon-form-runner.jar`.*

See also [[Creating a Production WAR|Installation ~ Creating a Production WAR]].

## How can I know which version of Orbeon Forms I am running?

The full version number is logged on the server when the Orbeon Forms web application starts.

You can set the following property in your `properties-local.xml`:

```xml
<property
    as="xs:boolean"
    name="oxf.show-version"
    value="true"/>
```

This is done automatically if you run Orbeon Forms in `dev` mode.

You will see the version number appear at the bottom of Orbeon Forms pages.

## I am getting frequent Out of Memory errors with Orbeon Forms. What can I do?

The first thing to check is whether your Java VM has enough heap memory as documented in [[Installation|Installation ~ Basic]].

## What should I make of the SocketException I see in the logs?

In most cases, this exception doesn't reflect any problem. Browsers make requests to Orbeon Forms, which answers by sending data back to the browsers. 
Requests can be for web pages (e.g. forms), CSS files, images, and more. You will get this exception if, while Orbeon Forms sends data back to the browser,
the connection between the browser and the server is interrupted.

In most cases, this happens because the browser cut the connection, as a way to tell the server _don't bother, after all, I don't need this_. This can happen 
if users clicked on a link to load a page, and while the page is being loaded interrupt the browser, for instance by clicking on another link (assuming the 
previous page is still visible), or pressing the _escape_ key or the equivalent browser button, or closing the browser window or tab. Unless you also see 
another user-facing issue, you don't need to worry about this exception in the logs.

## I am concerned about the performance of Orbeon Forms. Can you provide performance numbers please?

As of 2015, we have tests showing that a modern Intel server CPU with 4-8 cores can handle 200-400 users concurrently filling form data.
 
However please note that this can vary widely depending on the complexity and size of the forms.
