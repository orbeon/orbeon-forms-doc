> [[Home]] ▸ [[Form Runner|Form Runner]]

## Introduction

Form Runner supports implementing services using XPL (the XML pipeline language), associated with an application or a specific form.

_NOTE: This is an advanced feature which requires programming._

## Mapping and XPL

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
/fr/service/custom/acme/order/bar
````

## Implementation of the service

The service takes an `instance` input, containing any XML data posted to the service. This service produces an XML result on its `data` output.

The following is an example service implementation in XPL with just echoes the incoming data:

```xml
<p:config
    xmlns:p="http://www.orbeon.com/oxf/pipeline"
    xmlns:oxf="http://www.orbeon.com/oxf/processors">

    <p:param type="input" name="instance"/>
    <p:param type="output" name="data"/>

    <p:processor name="oxf:identity">
        <p:input name="data" href="#instance"/>
        <p:output name="data" ref="data"/>
    </p:processor>

</p:config>
```
