# Core actions

<!-- toc -->

## success

Complete the process right away successfully.

## process

You can run a sub-process with the `process` action:

```xml
<property
  as="xs:string"
  name="oxf.fr.detail.process.home.*.*"
  value='process("orbeon-home")'/>
```

You can also run a sub-process directly by name.

Example:

```xml
<!-- Define a sub-process which navigates to the "/" URL -->
<property
  as="xs:string"
  name="oxf.fr.detail.process.orbeon-home.*.*"
  value='navigate("/")'/>

<!-- Use that sub-process from the "home" process -->
<property
  as="xs:string"
  name="oxf.fr.detail.process.home.*.*"
  value='orbeon-home'/>
```

## suspend

[SINCE Orbeon Forms 4.3]

Suspend the current process. The continuation of the process is associated with the current form session.

## resume

[SINCE Orbeon Forms 4.3]

Resume the process previously suspended with `suspend`.

## abort

[SINCE Orbeon Forms 4.3]

Abort the process previously suspended with `suspend`. This clears the information associated with the process and it won't be possible to resume it with `resume`.

## nop

[SINCE Orbeon Forms 4.3]

Don't do anything and return a success value.
