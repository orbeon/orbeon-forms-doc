# Core actions

## Introduction

These core actions are actions directly supported by the process interpreter. They relate to how a process runs, completes, or fails.

## Running a sub-process explicitly

### `process`

You can run a sub-process with the `process` action:

```xml
<property as="xs:string"  name="oxf.fr.detail.process.home.*.*">
    process("orbeon-home")
</property>
```

You can also run a sub-process directly by name:

```xml
<!-- Define a sub-process which navigates to the "/" URL -->
<property as="xs:string" name="oxf.fr.detail.process.orbeon-home.*.*">
    navigate("/")
</property>

<!-- Use that sub-process from the "home" process -->
<property as="xs:string" name="oxf.fr.detail.process.home.*.*">
    orbeon-home
</property>
```

## Terminating a process

### `terminate-with-success`

[\[SINCE Orbeon Forms 2024.1\]](/release-notes/orbeon-forms-2024.1.md)

Complete the top-level process right away and return a success value.

*The `terminate-with-success` action replaces the `success` action since Orbeon Forms 2024.1.*


### `terminate-with-failure`

[\[SINCE Orbeon Forms 2024.1\]](/release-notes/orbeon-forms-2024.1.md)

Complete the top-level process right away and return a failure value.

*The `terminate-with-failure` action replaces the `failure` action since Orbeon Forms 2024.1.*

### `success`

[\[DEPRECATED SINCE Orbeon Forms 2024.1\]](/release-notes/orbeon-forms-2024.1.md)

Complete the top-level process right away and return a success value.

*The `success` action name is deprecated. Use `terminate-with-success` instead since Orbeon Forms 2024.1.*

### `failure`

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

[\[DEPRECATED SINCE Orbeon Forms 2024.1\]](/release-notes/orbeon-forms-2024.1.md)

Complete the top-level process right away and return a failure value.

*The `failure` action name is deprecated. Use `terminate-with-failure` instead since Orbeon Forms 2024.1.*

## Continuing a process

### `nop`

### `continue-with-success`

[\[SINCE Orbeon Forms 2024.1\]](/release-notes/orbeon-forms-2024.1.md)

Don't do anything and return a success value.

*The `continue-with-success` action replaces the `nop` action since Orbeon Forms 2024.1.*

### `continue-with-failure`

[\[SINCE Orbeon Forms 2024.1\]](/release-notes/orbeon-forms-2024.1.md)

Don't do anything and return a failure value.

[SINCE Orbeon Forms 4.3]

[\[DEPRECATED SINCE Orbeon Forms 2024.1\]](/release-notes/orbeon-forms-2024.1.md)

Don't do anything and return a success value.

*The `nop` action name is deprecated. Use `continue-with-success` instead since Orbeon Forms 2024.1.*

## Suspending and resuming a process

### `suspend`

[SINCE Orbeon Forms 4.3]

Suspend the current process. The continuation of the process is associated with the current form session.

### `resume-with-success`

[\[SINCE Orbeon Forms 2024.1\]](/release-notes/orbeon-forms-2024.1.md)

Resume the process previously suspended with `suspend`.

*The `resume-with-success` action replaces the `resume` action since Orbeon Forms 2024.1.*

### `resume-with-failure`

[\[SINCE Orbeon Forms 2024.1\]](/release-notes/orbeon-forms-2024.1.md)

Resume the process previously suspended with `suspend`, but indicate that the resumption is a failure.

### `abort`

[SINCE Orbeon Forms 4.3]

Abort the process previously suspended with `suspend`. This clears the information associated with the process. It won't be possible to resume it with `resume-with-success` or `resume-with-failure`.

### `resume`

[SINCE Orbeon Forms 4.3]

[\[DEPRECATED SINCE Orbeon Forms 2024.1\]](/release-notes/orbeon-forms-2024.1.md)

Resume the process previously suspended with `suspend`.

*The `resume` action is deprecated. Use `resume-with-success` instead since Orbeon Forms 2024.1.*