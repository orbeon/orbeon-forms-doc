> [[Home]] ▸ [[Form Runner|Form Runner]]

## Introduction

Form Runner supports implementing services using XPL, associated with an application or a specific form.

_NOTE: This is an advanced feature which requires programming._

## Placement of the XPL files

Assuming the following:

- App name: `acme`
- Form name: `order`

If you place a file called `foo.xpl` under

```
WEB-INF/resources/form/acme/service/
```

A service called `foo` is made available the following URL:

```
/fr/service/custom/acme/foo
````

Similarly, if you place a file called `bar.xpl` under

```
WEB-INF/resources/form/acme/order/service/
```

A service called `bar` is made available the following URL:

```
/fr/service/custom/acme/order/foo
````

## Implementation of the service

