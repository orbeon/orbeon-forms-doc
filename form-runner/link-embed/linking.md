# Linking to Your Forms

## Rationale

When you create a form with Form Builder, you pick an *application name* and *form name* for that form. For instance, for a marriage registration, you might choose `clerk` as the application name, and `marriage-registration` as the form name.

When you publish the form, assuming you have Orbeon Forms deployed on a server on `http://www.city.gov/forms`, citizen will be able to fill out a new marriage registration by going to `http://www.city.gov/forms/fr/clerk/marriage-registration/new`.

In a typical deployment, users will access this page from another part of your web site or web application that contains a link to form served by Orbeon Forms. For instance, a city government might have on its web site a page listing forms citizen can fill out, which links to the marriage registration form on `http://www.city.gov/forms/fr/clerk/marriage-registration/new`.

<img alt="Page on your web site/app linking to a form" src="../images/linking-page-with-link.png" width="484">

## Technology agnostic

Linking doesn't make any assumption on the technology used by the web site or application you're linking from. Your site could use Drupal, WordPress, be served by IIS, using .NET, or any other technology. For instance, the diagram below is for a situation where your web site is served by Microsoft IIS, implemented in .NET, and links to forms served by Orbeon Forms.

<img alt="IIS front-end" src="../images/linking-iis-net.png" width="441">

## Paths

The `/fr/clerk/marriage-registration/new` in our example is what is referred to below as a *path*, and for a given form, multiple such paths exist. Knowing what those paths are is particulary important as this allows you to link from your web site or web application to forms your created with Form Builder. All the paths are relative to the *deployment context*, i.e. where you've deployed Orbeon Forms, which in our example was `https://www.city.gov/forms`.

| Description                              | Path                                                                           | Availability |
|------------------------------------------|--------------------------------------------------------------------------------|--------------|
| Summary page for a given form definition | `/fr/$app/$form/summary`                                                       |              |
| New empty form data                      | `/fr/$app/$form/new`                                                           |              |
| Edit existing form data                  | `/fr/$app/$form/edit/$document`                                                |              |
| Read-only HTML view                      | `/fr/$app/$form/view/$document`                                                |              |
| Read-only PDF view                       | `/fr/$app/$form/pdf/$document`                                                 |              |
| Read-only TIFF view                      | `/fr/$app/$form/tiff/$document`                                                | 2016.1       |
| Excel export with data                   | `/fr/$app/$form/export/$document?export-format=excel-with-named-ranges`        | 2023.1       |
| Excel export without data                | `/fr/$app/$form/export?export-format=excel-with-named-ranges`                  | 2023.1       |
| XML export with data                     | `/fr/$app/$form/export/$document?export-format=xml-form-structure-and-data`    | 2023.1       |
| XML export without data                  | `/fr/$app/$form/export?export-format=xml-form-structure-and-data`              | 2023.1       |

Where:

- `$app` is the form definition's application name
- `$form` is the form definition's form name
- `$document` is the form data's document id

By default, the latest published and available form definition version is used. You can request a specific form definition version using the `form-version` parameter. For example:

- `/fr/acme/order/new?form-version=2`
- `/fr/acme/order/edit/fc4c32532e8d35a2d0b84e2cf076bb070e9c1e8e?form-version=3`

## URL parameters

### Summary page

[SINCE Orbeon Forms 2018.2]

You can pass the `form-version` URL parameter:

```
/fr/$app/$form/summary?form-version=$version
```

Where:

- `$app` is the form definition's application name
- `$form` is the form definition's form name
- `$version` is the form definition version number

The page will return a "Not Found" error if the specified version is not found.

By default, the latest available version is selected.

### New empty form data

When using versioning, you can pass the `form-version` URL parameter:

```
/fr/$app/$form/new?form-version=$version
```

Where:

- `$app` is the form definition's application name
- `$form` is the form definition's form name
- `$version` is the form definition version number

By default, the latest available form definition version is used.

### Modes that load data

This applies to `edit`, `view`, `pdf`, and `tiff` modes.

- `draft`
    - `true`: loads the data for a draft
    - `false` (default): loads the data for a final document

### PDF, TIFF, and other export views

- `fr-language`
    - With automatic PDF and exports, selects the language to use when producing the PDF.
    - With PDF templates, choose the template with the specified language if available.

[SINCE Orbeon Forms 2018.1]

- `fr-use-pdf-template`
    - Whether to use the PDF template or not. Defaults to `true` if there is one or more PDF templates.
- `fr-pdf-template-name`
    - Selects a PDF template by name.
- `fr-pdf-template-lang`
    - Select a PDF template by language.

## See also

- [Multitenancy](/form-runner/feature/multitenancy.md)
- [Sending PDF and TIFF content: Controlling the format](../advanced/buttons-and-processes/actions-form-runner-send.md)
- [PDF templates](/form-runner/feature/pdf-templates.md)
