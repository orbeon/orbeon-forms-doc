# Initial data properties

When creating new form data (for instance going to the URL `/fr/orbeon/bookshelf/new`), the initial data in the form can come from the following sources:

1. Data specified in Form Builder when creating the form definition:
    - statically, by entering data in fields in Form Builder
    - dynamically, by using the "Initial Value" formula in "Control Settings"
2. Data `POST`ed to the "new form" page.
3. Data retrieved by calling a service.

## Initial data posted to the page

The data provided in the form definition is used by default, and the `POST`ed data is used if present.

Initial data can be `POST`ed in two ways:

1. As a direct `POST` of the XML document
2. As an HTML form `POST` parameter called `fr-form-data`

For #2, this behaves as if a browser was submitting an HTML form that looks like the following, with the value of the `fr-form-data` request parameter being the Base64-encoded XML document:

```xml
<form method="post" action="/path/to/new">
    <input type="hidden" name="fr-form-data" value="Base64-encoded XML"/>
</form>
```

[SINCE Orbeon Forms 4.8]

The format of the data follows the Orbeon Forms 4.0.0 format by default. You can change this behavior to `POST` data in the latest internal format by specifying the `data-format-version=edge` request parameter. This is useful if you obtained the data from, for example, a [`send()` action](/form-runner/advanced/buttons-and-processes/actions-form-runner.md#send) using `data-format-version = "edge"`.

Use the authorization mechanism for services (see [Authorization of pages and services](/xml-platform/controller/authorization-of-pages-and-services.md)) to enable submitting initial instances to the new page:

- Your external application must provide credentials (e.g. BASIC authorization, a secret token, etc.) when `POST`ing to Form Runner.
-Your authorizer service must validate those credentials.

[SINCE Orbeon Forms 2017.1]

If `data-format-version=edge` is *not* specified, then the data `POST`ed is assumed to be in 4.0.0 format.

[SINCE Orbeon Forms 2022.1]

When `POST`ing data as described above, the data can now be incomplete. Say the 4.0.0 format of your form data is:

```xml
<form>
  <contact>
    <first-name/>
    <last-name/>
    <email/>
    <phone/>
  </contact>
  <message>
    <order-number/>
    <topic/>
    <comments/>
  </message>
</form>
```

Let's say that you just want to pass the `<last-name>` and `<order-number>` comments. You can now just `POST`:

```xml
<form>
  <contact>
    <last-name>Washington</last-name>
  </contact>
  <message>
    <order-number>3141592</order-number>
  </message>
</form>
```

All other elements are automatically added.

_NOTE: If the `POST`ed data contains extra XML elements in no namespace that are not supported by the form, an error is returned. However, extra XML elements in a custom namespace are allowed._

_Compatibility: If the data posted contains extra elements in no namespace, those elements were ignored prior to Orbeon Forms 2022.1. With Orbeon Forms 2022.1 and newer, their presence causes an error._

## Initial data from service

With the following properties, you can configure Form Runner to call an HTTP service instead of using the default instance provided as part of the form:

```xml
<property
    as="xs:boolean"
    name="oxf.fr.detail.new.service.enable.*.*"
    value="false"/>

<property
    as="xs:string"
    name="oxf.fr.detail.new.service.uri.*.*"
    value="/fr/service/custom/my-app/new"/>
```

Set the first property above to `true` to enable this behavior and have the second property point to your service.

The service is called with a `GET` HTTP method.

The service must either:

- return a successful HTTP response containing XML data in the `4.0.0` format for the given form
- return an empty body, in which case no error is produced (see also issue [\#3935](https://github.com/orbeon/orbeon-forms/issues/3935))
- return an error HTTP response or malformed XML response, in which case an error is produced and the form doesn't initialize

The following property defines a space-separated list of request parameters to be passed to the service. Say the new page was invoke with request parameters `foo=42` and `bar=84`, if you set the value of this property to `foo bar`, these two request parameters will be passed along as request parameters to the service. The request parameters can either get to the new page in a `POST` or `GET` request. The service is always called with a `GET`, consequently request parameters will be passed on the URI.

```xml
<property
    as="xs:string"
    name="oxf.fr.detail.new.service.passing-request-parameters.*.*"
    value="foo bar"/>
```

The `oxf.fr.persistence.*.data-format-version` property does not affect `oxf.fr.detail.new.service.enable` and the data returned by the service must still be in `4.0.0` format in all cases.

Enabling `oxf.fr.detail.new.service.enable` doesn't change the behavior with regard to `POST`ed instance: even if you are calling a service to get the initial instance, the `POST`ed instance will be used when a document is `POST`ed to the corresponding "new form" page.

## See also 

- Configuration properties
    - [Detail page](form-runner-detail-page.md)
    - [Attachments](form-runner-attachments.md)
    - [PDF](form-runner-pdf.md)
    - [Table of contents](form-runner-toc.md)