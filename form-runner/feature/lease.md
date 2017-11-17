# Lease

## Overview

[SINCE Orbeon Forms 2017.2] When the lease feature is enabled, Form Runner prevents multiple users from concurrently editing the same form instance:

1. When a first user, let's call him Homer, opens a form instance for editing, Homer is assigned a *lease* on that form instance for a given duration, say 10 minutes. The lease gets automatically extended when Homer updates the form, say by changing the value of a field, or when Homer clicks on a button to request a lease extension (more on this below).
2. If a second user, let's call her Marge, opens the same form instance for editing while Homer has a lease, Marge will be told she can't edit this form right now, has Homer has a lease. She can click on a button to try to acquire the lease again.
3. The lease given to Homer will end either because it expires without being renewed, or because Homer clicked on a button to explicitly relinquish the lease.

## Enabling the lease feature

By default, the lease feature is disabled. It is enabled when both conditions below are met:

- You've set the property `oxf.fr.detail.lease.enabled.*.*` to `true`, as shown below.
- The implementation of the persistence API used by the current supports the lease feature. As of Orbeon Forms 2017.2, this is the case of all the built-in implementations of the persistence API for relational databases, but not of the implementation of the persistence API for eXist.

If the lease feature is enabled, when non-authenticated users try to edit the data, they receive receive a [403 Forbidden](https://en.wikipedia.org/wiki/HTTP_403).

```xml
<property 
    as="xs:boolean" 
    name="oxf.fr.detail.lease.enabled.*.*"
    value="true"/>
```
