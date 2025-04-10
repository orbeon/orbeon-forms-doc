# HTTP services

## Introduction

The HTTP Service Editor allows you to create simple REST services. The idea is that a form can call a service, typically passing XML back and forth.

To create a new HTTP service, click the Add icon under "HTTP Services". The HTTP Service Editor opens.

The following screenshot shows an example of filled-out service:

<img src="images/service-definition.png" alt="Definition tab" width="561">

## Service definition

### Basic settings

The "Definition" tab allows you to set the basic service parameters:

- **Service Name**
    - This is the name of the service, as seen by Form Builder. Must start with a letter, and may not contain spaces.
    - [SINCE Orbeon Forms 2020.1] The service can be renamed, and actions that use the service are automatically updated.
- **Resource URL**
    - `HTTP` or `HTTPS` URL to which the service must be called.
    - The value is an XPath Value Template, which means that the URL can be dynamic. For example, with Orbeon Forms 2018.1's `fr:control-string-value()` function:
      ```xpath
      https://example.org/{fr:control-string-value('control-1')}
      ```
  - [\[SINCE Orbeon Forms 2024.1\]](/release-notes/orbeon-forms-2024.1.md) The resource URL can contain variable references to refer to the values of form controls (e.g. `$control-1`).
- **Method**
    - The HTTP method to use: `GET`, `POST`, `PUT` or `DELETE`.
- **Request Body**
    - This applies to the `POST` and `PUT` methods.
    - The XML document to send to the service. When using the "HTML Form" serialization, this is converted to name/value pairs.

### Serializations

#### Basics

Serializations apply to the `POST` and `PUT` methods only.

#### XML serialization
    
With the "XML" serialization, the XML data is passed using the [external data format](/form-runner/data-format/form-data.md).

On the wire, this serialization uses the `application/xml` content type.

#### HTML Form serialization

With the "HTML Form" serialization, leaf XML elements in the XML request body are converted to name/value pairs.

On the wire, this serialization uses the `application/x-www-form-urlencoded` encoding, like for standard web forms.

### URL parameters

\[SINCE Orbeon Forms 2016.1\]

- This applies to the `GET` and `DELETE` methods.
- You can add as many URL parameters as needed.
- A non-blank URL parameter specifies a default value for the parameter.
- An action can set the value of a parameter.

Here is [how to set URL parameters from an action](actions.md#passing-url-parameters-to-get-and-delete-methods).

### JSON support

\[SINCE Orbeon Forms 2016.1\]

A JSON *response* from a service is processed and converted to an XML document following the XForms 2.0 scheme (see [JSON support](../xforms/submission-json.md)).

See [#2480](https://github.com/orbeon/orbeon-forms/issues/2480) for details about sending JSON to services.

### URL Parameters before Orbeon Forms 2016.1

Prior to Orbeon Forms 2016.1, a "request body" is mandatory for the `GET` and `DELETE` methods. The body is not sent to the service, but instead is used to configure request parameters.

The content of the "Request Body" form has to be a well-formed XML document. The name of the root element doesn't matter, but usually `params`or `request` is used. Each child element defines a parameter as shown in the following example:

```xml
<params>
    <userId>1</userId>
    <userName>test</userName>
</params>
```
Here Orbeon invokes the URL:
```
$<Resource URL>?userId=1&userName=test
```
where `$<Resoure URL>` is the content of the input field "Resource URL".

Make sure to select `HTML Form` in the `Serialization` dropdown, otherwise the URL parameters are not appended to the request URL.

## Advanced parameters

The "Advanced" tab allows you to set advanced service parameters:

- **Service Response has Binary Content**
    - [SINCE Orbeon Forms 2020.1]
    - Check this if you know that the service returns binary content, such as an image. This is necessary if the [`<fr:control-setattachment>`](/form-builder/actions-syntax.md#setting-the-value-of-an-attachment-control) action makes use of the response.
- **HTTP Authentication**
    - Whether to use HTTP authentication.
    - **Username:** Username to use.
    - **Password:** Password to use.
- **SOAP Service**
    - Whether this is a SOAP service
- **SOAP Action**
    - If selected, the value of the `SOAPAction` header.

<img src="images/service-definition-advanced.png" alt="Advanced tab" width="562">

## Testing a service

### Basics

The "Test" button allows you to test the service. Before doing this, you have to set data in the request body for a `POST` or `PUT` request, or you might want to set URL parameters for a `GET` or `DELETE`. Form Builder executes the service, and then provides information about the response returned, including:

- Whether an error occurred (green or red highlight)
- URL called
- Response status code
- Response headers
- Response body

This helps you troubleshoot the service call.

<img src="images/service-definition-test.png" alt="Test Results tab with response headers" width="562">

<img src="images/service-definition-test-original-response-body.png" alt="Test Results tab with response body" width="562">

### XML view of JSON responses

[SINCE Orbeon Forms 2019.1]

When the service response is in JSON format, you can view the JSON response *translated into XML* following [XForms 2.0](https://www.w3.org/community/xformsusers/wiki/XForms_2.0#External_JSON_values).

This is useful when you want to use XPath expressions to access a JSON response, for example with [datasets](/form-runner/feature/datasets.md) or [item choices](actions.md#setting-the-choices-of-a-selection-control).

See also [JSON support](/xforms/submission-json.md).

<img src="images/service-definition-test-xml-response-body.png" alt="Test Results tab with XML response body" width="562">

## Saving the service

Once your service is defined, the "Save" buttons saves it to the form. You can come back to it and modify it later by clicking on the "Edit" icon next to the service name. You can also delete the service using the trashcan icon.

## Deleting a service

You can delete a saved service using the "Remove" button.

[SINCE Orbeon Forms 2020.1]

Orbeon Forms warns you if there are existing actions that refer to the service. 

## See also

- Blog post: [Making sense of Form Runner Actions](https://www.orbeon.com/2024/09/making-sense-form-runner-actions)
- [Simple Actions](actions.md)
- [Database services](database-services.md)
- [Synchronizing repeated content](synchronize-repeated-content.md)
- [Datasets](/form-runner/feature/datasets.md)
- [JSON support](/xforms/submission-json.md)