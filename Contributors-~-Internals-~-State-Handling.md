# XForms - State Handling - forms

## Rationale

The Orbeon Forms XForms engine requires keeping processing state while operating on an XForms page. Such state includes the current values of XForms instances, selected repeated elements, and more. With Orbeon Forms, by default XForms state information is stored on the server.

## Configuring state handling

### XForms document and static state caches

A property controls whether the XForms engine should try to optimize state reconstruction by using a cache. This property should usually be set to `true`:

```xml
<property as="xs:boolean" name="oxf.xforms.cache.document" value="true">
```

The global configuration can be overridden for each page by setting the `xxf:cache.document` attribute in the page. This attribute can be set on the root element of the XHTML page, or on the first `xf:model` element. Only the first such attribute encountered by the XForms engine is used:

```xml
<xf:model xxf:cache.document="false">
```

If `oxf.xforms.cache.document` is set to `true`, the number of XForms documents that can be held in that document cache at a given time is configured with the following property:

```xml
<property
    as="xs:integer"
    name="oxf.xforms.cache.documents.size"
    value="50">
```

*_NOTE: This property is global and cannot be overridden on a page by page basis._*

*_NOTE: If a user loads the same XForms page twice, two entries are created in the document cache._*

The static state (or static analysis) of an XForms page is stored in a separate cache, controlled with this property:

```xml
<property
    as="xs:integer"
    name="oxf.xforms.cache.static-state.size"
    value="50">
```

This property should be set to a number equal to or greater than the number of distinct XForms pages you have.

### XForms state store configuration

When entries are evicted from the XForms document cache or the XForms static state cache, they are migrated to the XForms state store.

The XForms state store is configured via [`oxf:/config/ehcache.xml`](https://github.com/orbeon/orbeon-forms/blob/master/src/resources-packaged/config/ehcache.xml).

## Session heartbeat

See also the [blog post](http://blog.orbeon.com/2008/01/keeping-your-session-alive_29.html).

If you happen to leave a browser window open on your computer, chances are that you will get back to that window and keep using the application. The last thing you want to happen when you come back is lose your session and therefore your data.

This is not always a correct guess of course: you may just happen to leave a window or tab open without planning to use it again. Conversely you may have a page which is not actually visible, for example in your browser history, yet you will come back to it. This approach wouldn't be good for banking applications either. Still, in many situations, such as filling-out large forms, it sounds like a good idea to keep your session alive for open pages.

To achieve this goal you could make all server sessions longer. However this is harder to configure for the application developer, and this won't discriminate between pages that are actually open on a client and the ones that are not. And while it may be ideal to have infinitely long sessions, unfortunately many applications are not ready for this kind of approach.

So Orbeon Forms supports a "session heartbeat" feature. Here is how this works:

- When this feature is enabled (the default), an open XForms page in a client browser regularly pings the server through Ajax to keep the current server session alive.
- The ping delay is automatically computed based on the server's session timeout. The client pings the server at 80% of the session expiration time after the last interaction with the server.
- We are careful not to hit the XForms engine too much, in fact we do a very limited amount of work on the server for each ping, so they should run fast.
- XForms state information for pages hit with the heartbeat just migrates to the disk store over time if RAM is used by other pages, so keeping even large numbers of pages open should not have any negative impact on server RAM.
- When a user gets back to using the page, state information migrates back from disk to RAM, and the page will be live again.
- Sessions do eventually expire as nobody keeps a browser open forever.

Note that whenever an application keeps sessions alive for a long time, it is a good idea to keep as little data as possible in the session. The Orbeon Forms XForms engine itself uses a global state store and does not use session objects for storage, but do remember to keep your sessions small!

The session heartbeat should help prevent many occurrences of "session expired" error messages. As an Orbeon Forms application developer you don't have to worry about anything: the session heartbeat is enabled by default. You can configure it globally in `properties-local.xml`:

```xml
<property as="xs:boolean" name="oxf.xforms.session-heartbeat" value="true">
```

You can also override the global default by specifying a property on the first XForms model of a page:

```xml
<xf:model id="my-model" xxf:session-heartbeat="false">
```

## Browser navigation (back and forward) handling

When visiting an XForms page by using your browser's Back and Forward buttons, or other browser-history mechanisms, Orbeon Forms by default restores the appearance of that page as it was when you left it. (Browsers don't automatically handle this behavior with Ajax applications!) This behavior best matches the usual user experience obtained when navigating regular web pages.

In certain situations, it can be useful instead to ask the XForms page to reload entirely. You control this by using the `xxf:revisit-handling` attribute on the first XForms model of the page you want to reload. This attribute supports two values: `restore` (the default) and `reload`. Example:

```xml
<xf:model xxf:revisit-handling="reload">
```

*_NOTE: It is recommended to use the `reload` value carefully, as reloading pages upon browser navigation often does not match the expectation of the user._*
