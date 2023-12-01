# Summary page configuration properties

## Adding your own CSS files

[SINCE Orbeon Forms 2017.1]

In addition to `oxf.fr.css.custom.uri`, you can also use the following property, which apply only to the Summary page:

```xml
<property
    as="xs:string"
    name="oxf.fr.summary.css.custom.uri.*.*"
    value="/forms/acme/assets/acme-summary.css"/>
```

See also [Adding your own CSS](form-runner.md#adding-your-own-css).

## Adding your own JavaScript files

[SINCE Orbeon Forms 2017.1]

In addition to `oxf.fr.js.custom.uri`, you can also use the following property, which apply only to the Summary page:

```xml
<property
    as="xs:string"
    name="oxf.fr.summary.js.custom.uri.*.*"
    value="/forms/acme/assets/acme-summary.js"/>
```

See also [Adding your own JavaScript](form-runner.md#adding-your-own-javascript).

## Summary page size

```xml
<property
    as="xs:integer"
    name="oxf.fr.summary.page-size.*.*"
    value="10"/>
```

Number of rows shown in the Summary page.

## Created and Last Modified columns

By default, the Summary page shows a Created and Modified columns:

![Created and Last Modified](/form-runner/images/summary-created-last-modified.png)

You can remove either one of those columns by setting the appropriate property value to `false`:

```xml
<property
    as="xs:boolean"
    name="oxf.fr.summary.show-created.*.*"
    value="true"/>

<property
    as="xs:boolean"
    name="oxf.fr.summary.show-last-modified.*.*"
    value="true"/>
```

## Show the workflow stage

[SINCE Orbeon Forms 2020.1]

You can add this column by setting the following property value to `true`:

```xml
<property 
    as="xs:boolean" 
    name="oxf.fr.summary.show-workflow-stage.*.*"                           
    value="true"/>
```

## Show created by and last modified by users

[SINCE Orbeon Forms 2021.1]

You can add either one of those columns by setting the appropriate property value to `true`:

```xml
<property 
    as="xs:boolean" 
    name="oxf.fr.summary.show-created-by.*.*"                           
    value="true"/>
<property 
    as="xs:boolean" 
    name="oxf.fr.summary.show-last-modified-by.*.*"                   
    value="true"/>
```

## Buttons on the Summary page

See [Configuring Summary page buttons](/form-runner/advanced/buttons-and-processes/summary-page-buttons-and-processes.md#configuring-summary-page-buttons).

## Versioning

[SINCE Orbeon Forms 2020.1 and 2019.2.3] The following property allows you to configure whether the [summary page](form-runner/feature/summary-page.md) shows data:
 
- if set to `true`: for one version at a time, which is the default;
- if set to `false`: for all data across form definition versions, as it used to be the case up until Orbeon Forms 2018.1.

Even when set to `false`, if the `form-version` request parameter is passed to the summary page, it will not ignore the request parameter and will only show data created with that version of the form definition.

```xml
<property 
    as="xs:boolean"
    name="oxf.fr.summary.show-version-selector.*.*"
    value="false"/>
```

[SINCE Orbeon Forms 2023.1]

When the property is set to `false`, links to the summary page from the Published Forms and Landing pages will not include the `form-version` request parameter.