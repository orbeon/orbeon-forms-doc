# Run form in the background

<!-- toc -->

## Availability

Since Orbeon Forms 2017.2.

This is an Orbeon Forms PE feature.

## Purpose

The purpose of this service is to run a form definition, either in "new" mode or in "edit" mode, but in the background,
optionally running one or more processes in addition to the regular execution of the form.

This allows scenarios such as:

- create new form data in the database
- read, modify and update existing form data 

## Interface

- URLs:
    - `/fr/service/$app/$form/new`
    - `/fr/service/$app/$form/edit/$document`
- Method: `POST`

Optional request body when using `/new`:

- `Content-Type: application/xml`
- XML data to POST to the form, like when [POSTing to the detail page](/configuration/properties/form-runner-detail-page.md#initial-data-posted-to-the-new-form-page)

When not POSTing any XML data, just POST an empty request body to the service.

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

## Running processes

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

The following process names apply:

- `after-controls`: run after the controls are ready:
    - The process runs when controls are "live", which means you can set their values and list of choices.
- `after-data`: run when the data is ready:
    - It has been loaded from the database if needed ("edit" mode).
    - Its initial values and calculations are up to date.
- `before-data`: run before the data's initial values are calculated:
    - The process runs before the data is ready.
    - You cannot set control values and list of choices as a result.
    
The following example saves new instance data to the database when the service is called with `/fr/service/$app/$form/new`:

```xml
<property 
    as="xs:string" 
    name="oxf.fr.detail.process.after-controls.background.new.acme.sales">
    save
</property>
```

The following example updates existing instance data with the current time and saves it to the database when the service is called with `/fr/service/$app/$form//edit/$document`, where `$document` represents an existing form data document id:

```xml
<property 
    as="xs:string" 
    name="oxf.fr.detail.process.after-controls.background.edit.acme.sales">
    xf:setvalue(ref ="//current-time", value = "current-dateTime()") then save
</property>
```

## Permissions

- The caller must either call the service internally or have [authorized the service](/xml-platform/controller/authorization-of-pages-and-services.md).
- Appropriate container or permission headers must also be set to allow accessing the form definition and data.  
