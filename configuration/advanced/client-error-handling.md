# Client-side error handling

<!-- toc -->

## Disabling the standard error dialog

By default, when an Ajax error happens, Orbeon Forms shows users an error dialog. You can disable this behavior by adding this property to your `properties-local.xml`:

```xml
<property
    as="xs:boolean"
    name="oxf.xforms.show-error-dialog"
    value="false">
```

## JavaScript event handler

Orbeon Forms exposes a [custom JavaScript event](http://developer.yahoo.com/yui/event/#customevent): `ORBEON.xforms.Events.errorEvent`. You can register your own listener on that event, and when fired, send users to a page you choose, as done in the following snippet, which sends users to the Orbeon home page:

```javascript
ORBEON.xforms.Events.errorEvent.subscribe(function(eventName, eventData) {
    // your code here
});
```

## Example

In case the user session expires, or some other error happens, you would like to redirect them a page you created that will, for instance, tell users to log in and try again, and if the problem persists to contact customer support.

```javascript
ORBEON.xforms.Events.errorEvent.subscribe(function(eventName, eventData) {`  
    window.location.href = "http://www.example.org/";
});
```
