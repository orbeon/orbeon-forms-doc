# List forms data attachments API

## Availability

Since Orbeon Forms 4.7.

## Internal uses

None.

## Purpose

The purpose of the `attachments` API is to return information about form data attachments.

## Interface

- URL: `/fr/service/$app/$form/attachments?document=$document`
- Method: `GET`

Where:

- `$app` is the form definition's application name
- `$form` is the form definition's form name
- `$document` is the form data's document id

Request parameters:

- `document`: the id of the document (form data) to retrieve

Request body:

- none

Response body:

- `Content-Type: application/xml`
- each child element
    - has the following attributes
        - `name` attribute: the associated control name
        - `filename`, `mediatype`, `size`: attachment metadata
        - `fr:relevant="false"`: if the corresponding control was non-relevant when data was saved, either directly because inside a non-relevant grid or section [SINCE Orbeon Forms 2024.1.1]
    - contains, as text data, the path to the attachment

Example response body:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<attachments>
    <attachment name="my-image" filename="sunset.png" mediatype="image/png" size="19354">
        /fr/service/persistence/crud/a/a/data/cff2bb5313f6e28fa4fc5b96504102931359e902/51c42c10beec2a7a428667c84c4df998ddec2322.bin
    </attachment>
    <attachment name="other-image" filename="africa.jpg" mediatype="image/jpeg" size="169202">
        /fr/service/persistence/crud/a/a/data/cff2bb5313f6e28fa4fc5b96504102931359e902/11b03cbe6d2dab4876c97229dacf9cbb76df5bb7.bin
    </attachment>
</attachments>
```

## Permissions

The caller must either call the service internally or have proper credentials to access the data (username, group, roles).

## See also

- [CRUD](crud.md)
- [Search](search.md)
- [Form metadata](forms-metadata.md)
- [Caching](caching.md)
- [Versioning](versioning.md)
- [Custom persistence providers](custom-persistence-providers.md)