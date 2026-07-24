# Form Runner log action

## Availability

[\[SINCE Orbeon Forms 2025.1.3\]](/release-notes/orbeon-forms-2025.1.3.md)

##  Introduction

The `log` process action logs a message on the server.

## Syntax and parameters

### General syntax

```
log(
    "Hello, logs!",
    level = "info"
)
```

or:

```
log(
    message = "Hello, logs!",
    level   = "info"
)
```

### Parameters

| Parameter      | Mandatory | Value                                                   | Comment      | AVT  |
|----------------|-----------|---------------------------------------------------------|--------------|------|
| `message`      | Yes       | message content (default parameter)                     | message text | Yes  |
| `level`        | No        | `debug`, `info`, `warn`, or `error` (default is `info`) | log level    | Yes  |
