# Run form in the background API

## Availability

Since Orbeon Forms 2017.2.

This is an Orbeon Forms PE feature.

## Purpose

The purpose of this service is to run a form definition, either in "new" mode or in "edit" mode, but in the background,
optionally running one or more processes in addition to the regular execution of the form.

This allows scenarios such as:

- create new form data in the database
- read, modify and update existing form data 

## Usage

### HTTP request and response

- URLs:
    - `/fr/service/$app/$form/new`
    - `/fr/service/$app/$form/edit/$document`
- Method: `POST`

Optional request body when using `/new`:

- `Content-Type: application/xml`
- XML data to `POST` to the form, like when [`POST`ing to the detail page](/configuration/properties/form-runner-detail-page.md#initial-data-posted-to-the-new-form-page)

When not `POST`ing any XML data, just `POST` an empty request body to the service.

Response body:

- `Content-Type: application/xml`
- contains
    - the newly created or existing document id
    - whether all processes were successful

Example response body:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<response>
    <document-id>9eff349bfd95aab8d4d5e048bd25a815</document-id>
    <process-success>true</process-success>
</response>
```

### Running processes

The following property controls what process(es) to run:

```
oxf.fr.detail.process.
  after-controls|after-data|before-data.
  background.
  new|edit.
  $app.
  $form
```

where `$app` and `$form` represent a Form Runner application name and/or form name or `*` wildcards, as is usual with Form Runner configuration properties.
    
For detail on the process names and more, see [Running processes upon page load
](../../../configuration/properties/form-runner-detail-page.md#running-processes-upon-page-load).

### Returning form data

[SINCE Orbeon Forms 2021.1]

This must first be enabled with the following property:

```xml
<property 
    as="xs:boolean"
    name="oxf.fr.detail.service.background.enable-return-data.*.*" 
    value="true"/>
```

Then, if you pass the URL parameter `return-data=true` to the service, form data is returned:

```xml
<response>
    <document-id>9eff349bfd95aab8d4d5e048bd25a815</document-id>
    <data>
        <form>
            <my-section>
                <my-control>Cat</my-control>
            </my-section>
        </form>
    </data>
    <process-success>true</process-success>
</response>
```

By default, the data is returned in the 4.0.0 data format. You can override this by passing the `data-format-version` parameter:

- `data-format-version`
    - `edge`: send the data in the latest internal format
    - `2019.1.0`: send the data in the Orbeon Forms 2019.1-compatible format
    - `4.8.0`: send the data in the Orbeon Forms 4.8-compatible format
    - `4.0.0`: send the data in the Orbeon Forms 4.0-compatible format (the default)

The `prune-metadata` parameter can be used to control production of metadata:

- `true` to remove all occurrences of `fr:`-prefixed elements and attributes
- `false` to leave such occurrences
- default
    - `false` when `data-format-version` is set to `edge`
    - `true` otherwise 

## Examples

### Create initial data

The following example saves new instance data to the database for the form `acme/sales` when the service is called with `/fr/service/acme/sales/new`:

```xml
<property 
    as="xs:string" 
    name="oxf.fr.detail.process.after-controls.background.new.acme.sales">
    save
</property>
```

### Update existing data

The following example updates existing instance data with the current time for the form `acme/sales` and saves it to the database when the service is called with `/fr/service/acme/sales/edit/$document`, where `$document` represents an existing form data document id:

```xml
<property 
    as="xs:string" 
    name="oxf.fr.detail.process.after-controls.background.edit.acme.sales">
    xf:setvalue(ref ="//current-time", value = "current-dateTime()") then save
</property>
```

### Validate data

The following process allows you to `POST` XML data to the page:

```xml
<property as="xs:string" name="oxf.fr.detail.process.after-controls.background.new.*.*">
    validate("error")
</property>
```

*NOTE: Make sure you use `validate("error")` instead of `require-valid`, as the latter always returns a success value.* 

### Using curl

The following examples use the [curl](https://curl.haxx.se/) command-line utility. They are indented on multiple lines for clarity but in practice each command must be written on a single line.

```
curl
  -v
  -k
  -X POST
  http://localhost:9090/orbeon/fr/service/acme/order/new
```

This runs the `acme/order` form in `new` mode in the background, and
runs the configured processes if any.

Here is an example XML response:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<response>
    <document-id>eb9a48f2bae8919139c117c3424a3498f71c3d6f</document-id>
    <process-success>true</process-success>
</response>
```

## Permissions

- The caller must either call the service internally or have [authorized the service](/xml-platform/controller/authorization-of-pages-and-services.md).
- Appropriate container or permission headers must also be set to allow accessing the form definition and data.  

## See also 

- [Running processes upon page load](../../../configuration/properties/form-runner-detail-page.md#running-processes-upon-page-load)
