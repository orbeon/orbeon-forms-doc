> [[Home]] ▸ [[Form Runner|Form Runner]]

## Rationale

Allowing Form Runner to cache form definitions is important to help with performance. Form Runner interfaces with the persistence layer through a [[REST API|Form Runner ~ APIs ~ Persistence]]. Standard HTTP caching mechanisms are introduced to allow caching at that level.

The idea is that when Form Runner requests a form definition, it is able, if it holds the form definition in its local cache, to perform a so-called HTTP conditional GET. This is exactly the same thing that web browsers do to revalidate some resources that they cache on the client. But here it is about Form Runner caching form definitions obtained from the persistence layer.

## Audience

This page is intended for programmers implementing their own persistence layer (or for anybody curious about how things work!).

## Status

As of Orbeon Forms 4.0, there is minimal support in Form Runner for conditional GETs with the `Last-Modified` and `If-Modified-Since` HTTP headers. However the built-in eXist and relational persistence layer implementations do not support these headers properly yet.

As we work towards the improvements described in issue [#1239](https://github.com/orbeon/orbeon-forms/issues/1239), we hope to see even better performance when the persistence layer supports these headers. We also plan to add support for `ETag` and `If-None-Match`, which will be required for persistence layers that support versioning.

## Supporting Last-Modified and If-Modified-Since

The idea is very simple: the HTTP request may contain an `If-Modified-Since` HTTP header, for example:

    If-Modified-Since: Thu, 16 Jan 2014 01:16:27 GMT

The server (here the persistence layer), when seeing this header, SHOULD first try to check whether the resource requested (typically the form definition) has been modified since the given date (this can happen for example if the form is republished by Form Builder).

- If the resource is newer, then the persistence layer must do the usual and serve the form definition with an HTTP `200` status code.
- If the resource has the same date or is older, the persistence layer should simply return a `304` ("not modified") status code, without the need to send any response body.

The benefits are that:

- The persistence layer doesn't need to actually retrieve the form definition from a database.
- The form definition doesn't need to be sent over the wire and parsed by Form Runner.

For this to work, your persistence layer MUST send, with every form definition, a correct `Last-Modified` header, for example:

    Last-Modified: Thu, 16 Jan 2014 01:16:27 GMT

The above only needs to be supported for the HTTP `GET` method and, optionally, for the `HEAD` method (which Form Runner doesn't use).

## Supporting ETag and If-None-Match

TODO
