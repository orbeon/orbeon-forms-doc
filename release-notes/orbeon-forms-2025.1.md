# Orbeon Forms 2025.1

__Wednesday, December 31, 2025__  

Today we released Orbeon Forms 2025.1! This release introduces new features, many improvements, and bug-fixes!

## Major new features

### Built-in confirmation page

The Confirmation page is a page which is intended to show after form processing has completed. For example, a user might have submitted their data, and you want to show a confirmation message, along with next steps.

![Confirmation page example](/form-runner/images/confirmation-page-example.webp)

The Confirmation page is made of two parts:

- A user interface in Form Builder, where you can specify what message to show and some options.
- Configuration properties to enable and customize the confirmation page behavior.

Messages, like other information in your form, can be fully localized by language.

![Form Runner confirmation page configuration](/form-runner/images/confirmation-page-settings-templates.webp)

See also:

- Documentation: [Confirmation page](/form-runner/feature/confirmation-page.md)

### Dark mode

A new dark mode is now available in Orbeon Forms. This mode can be enabled using configuration properties.

![Orbeon Forms in dark mode](/configuration/images/dark-color-scheme.png)

See also:

- Documentation: [Color scheme](/configuration/properties/form-runner-detail-page.md#color-scheme)

### Keyboard Shortcuts dialog

[\[SINCE Orbeon Forms 2025.1\]](/release-notes/orbeon-forms-2025.1.md)

Form Builder now includes a dedicated Keyboard Shortcuts dialog that lists all available shortcuts, allowing you to discover them without leaving the app.

![Form Builder Keyboard Shortcuts](/form-builder/images/form-builder-keyboard-shortcuts-dialog.webp)

See also:

- Blog post: [Keyboard Shortcut Improvements in Form Builder](https://www.orbeon.com/2025/10/keyboard-shortcut-improvements)
- Documentation: [Form Builder Keyboard Shortcuts](/form-builder/keyboard-shortcuts.md)

### Pluggable detail page modes

With Orbeon Forms 2025.1, we are introducing custom modes. This enables two new different features for integrators:

- __Custom Views:__ Showing existing form data in custom ways, for example showing a subset of the form data for confirmation.
- __External Navigation:__ Navigating away from and back to Orbeon Forms, for example for calling external services such as payment and signature providers.
The good news is that Orbeon Forms handles for you the difficult parts: data management, state keeping, and permissions, in particular, while you can concentrate on your custom mode logic. The following diagram summarizes the overall flow:

![Custom Form Runner modes overview](/form-runner/images/custom-modes-workflow.webp)

See also:

- Blog post: [Custom Form Runner modes](https://www.orbeon.com/2025/12/custom-modes)
- Documentation: [Form Runner custom modes](/form-runner/feature/custom-modes.md)

### Improved languages support

In this version, we have greatly increased the number of supported languages in Orbeon Forms: 

- All languages that were only partially supported before are now fully supported.
- We've added Polish, Arabic, and Japanese support for both Form Runner and Form Builder.
- We've also added Catalan, Chinese (Simplified), Chinese (Traditional), Hungarian, and Turkish support for Form Runner.

Here is the full list:

| Language              | Form Runner | Form Builder |
|-----------------------|:-----------:|:------------:|
| Arabic                |      F      |      F       |
| Catalan               |      F      |      N       |
| Chinese (Simplified)  |      F      |      N       |
| Chinese (Traditional) |      F      |      N       |
| Danish                |      F      |      N       |
| Dutch                 |      F      |      F       |
| English               |      F      |      F       |
| Finnish               |      F      |      F       |
| French                |      F      |      F       |
| German                |      F      |      F       |
| Hungarian             |      F      |      N       |
| Italian               |      F      |      F       |
| Japanese              |      F      |      F       |
| Norwegian             |      F      |      F       |
| Polish                |      F      |      F       |
| Portuguese            |      F      |      F       |
| Russian               |      F      |      F       |
| Spanish               |      F      |      F       |
| Swedish               |      F      |      F       |
| Turkish               |      F      |      N       |

See also:

- Documentation: [Supported languages](/form-runner/feature/supported-languages.md#as-of-orbeon-forms-20251)

### CSS variables

In this version, we have refactored our CSS files to use CSS variables. This makes it easier to customize the look-and-feel of Orbeon Forms to your own needs. Variables control various aspects of the appearance of forms, including:

- font sizes
- colors

In the future, we plan to introduce configuration user interface for themes in Orbeon Forms.

See also:

- Documentation: [CSS variables](/form-runner/styling/css.md#overriding-css-variables)

### Allowing colors in PDF output

A new PDF color mode allows preserving colors, and CSS variables related to colors can be overridden in a custom `@media print` section and will be used by the PDF generator.

![Orbeon Forms PDF color mode](xxx)

See also:

- Documentation: [PDF color mode](/form-runner/feature/pdf-automatic.md#pdf-color-mode)

### New demo forms

In 2025, we have added a number of new demo forms to showcase various features of Orbeon Forms. These forms are available online as well as in the Orbeon Forms distribution:

- Building Permit Application ([builder](https://demo.orbeon.com/demo/fr/orbeon/builder/edit/73dfe9ffc8fe0356846ab6a7d1a400b99812fd72), [runner](https://demo.orbeon.com/demo/fr/orbeon/building-permit/edit/8ee48fe19bf77b0b1ac248fcdb038829129243e1))
- Vendor Application ([builder](https://demo.orbeon.com/demo/fr/orbeon/builder/edit/a9ccae9cb991ebabc507a0da9850bf1c33c1d910), [runner](https://demo.orbeon.com/demo/fr/orbeon/vendor-application/edit/18dc67f6ac5f239dec92a876fb4cc3c039388a7d))
- PTA Payment Authorization/Request for Reimbursement ([builder](https://demo.orbeon.com/demo/fr/orbeon/builder/edit/8fcdfe4ff49218923b6599ec9c7bb91488fcdf1a), [runner](https://demo.orbeon.com/demo/fr/orbeon/pta-payment-authorization/edit/dc858de075f523e64b7020a6bcd10353a0d1c8ce))
- Public Records Act Request Form ([builder](https://demo.orbeon.com/demo/fr/orbeon/builder/edit/f2d5187098bfd0e60de353ed7bb9a2f5021b3ee8), [runner](https://demo.orbeon.com/demo/fr/orbeon/public-records/edit/e7fb0c80221b191bf18e95e08d55ef9242ddcf73))
- Health History ([builder](https://demo.orbeon.com/demo/fr/orbeon/builder/edit/12553579e52f1008617b8d7a000e659db2b15133), [runner](https://demo.orbeon.com/demo/fr/orbeon/health-history/edit/13e1a4f2a555d31326d2b3bb041b11f4d8f95539))
- Medical Record Amendment ([builder](https://demo.orbeon.com/demo/fr/orbeon/builder/edit/7f9b4de690effbf69a5d3c5207b9c880eaa23524), [runner](https://demo.orbeon.com/demo/fr/orbeon/medical-record-amendment/edit/8c7a175dd8ada3e5292b4e993b230fcca1eee12b))

### Removal of Ehcache 2 and use of Infinispan

We have removed Ehcache 2.x as the default caching provider, as that library is no longer supported. Orbeon Forms now uses Infinispan as the default caching provider. Infinispan can also be used as a provider for a replication setup.

See [compatibility notes below](#ehcache-2x-changes-and-deprecation) for details and backward compatibility information.

See also:

- Documentation: [JCache configuration with Infinispan](/installation/caches.md#orbeon-forms-20251-and-newer-jcache-configuration-with-infinispan)

### Revision History API improvements

The Revision History API can now directly return differences.

See also:

- Documentation: [Revision History API: Parameters](/form-runner/api/persistence/revision-history.md#parameters)

### New and improved APIs

#### Sending emails with the SendGrid Email API

In addition to sending emails with SMTP servers, we are introducing support for sending emails through the SendGrid Email API.

See also:

- Documentation: [Form Runner email configuration properties](/configuration/properties/form-runner-email.md#email-transport-configuration)

#### Remote server APIs

This new API allows you to perform remote operations:

- Push to remote
- Pull from remote

See also:
 
- Documentation: [Remote server APIs](/form-runner/api/other/remote.md)

#### Property provider API

This new server-side Java API allows adding custom property providers to supply property values from alternative sources or through custom logic. Use cases include:

- providing configurations stored in a database
- providing passwords or keys from a secure vault or Key Management System (KMS)
- providing preferences linked to a user profile
- providing properties for a particular tenant in a multi-tenant setup

See also:

- Documentation: [Property provider API](/form-runner/api/other/property-provider.md)

## Features also included with Orbeon Forms 2024.1.x

### Features also included with Orbeon Forms 2024.1.1

- [Suggest control and section name from the control label](https://doc.orbeon.com/release-notes/orbeon-forms-2024.1.1#suggest-control-and-section-name-from-the-control-label)
- [Ability to filter itemset with a formula](https://doc.orbeon.com/release-notes/orbeon-forms-2024.1.1#ability-to-filter-itemset-with-a-formula)
- [Show a form's workflow stage in the Detail page](https://doc.orbeon.com/release-notes/orbeon-forms-2024.1.1#show-a-forms-workflow-stage-in-the-detail-page)
- [Store email attachments to AWS S3](https://doc.orbeon.com/release-notes/orbeon-forms-2024.1.1#store-email-attachments-to-aws-s3)
- [Include UI for the various appearances of buttons](https://doc.orbeon.com/release-notes/orbeon-forms-2024.1.1#include-ui-for-the-various-appearances-of-buttons)
- [JavaScript embedding API to support Form Builder](https://doc.orbeon.com/release-notes/orbeon-forms-2024.1.1#javascript-embedding-api-to-support-form-builder)
- [Ability to navigate grid cells using cursor up/down](https://doc.orbeon.com/release-notes/orbeon-forms-2024.1.1#ability-to-navigate-grid-cells-using-cursor-up-down)
- [Native time picker](https://doc.orbeon.com/release-notes/orbeon-forms-2024.1.1#native-time-picker)
- [New configuration properties](https://doc.orbeon.com/release-notes/orbeon-forms-2024.1.1#new-configuration-properties)

### Features also included with Orbeon Forms 2024.1.2

- [Components with built-in validation](https://doc.orbeon.com/release-notes/orbeon-forms-2024.1.2#components-with-built-in-validation)
- [Custom PDF template field names](https://doc.orbeon.com/release-notes/orbeon-forms-2024.1.2#custom-pdf-template-field-names)
- [Redis configuration](https://doc.orbeon.com/release-notes/orbeon-forms-2024.1.2#redis-configuration)
- [OIDC with WildFly](https://doc.orbeon.com/release-notes/orbeon-forms-2024.1.2#oidc-with-wildfly)
- [New localizations and translations](https://doc.orbeon.com/release-notes/orbeon-forms-2024.1.2#new-localizations-and-translations)
- [Other features](https://doc.orbeon.com/release-notes/orbeon-forms-2024.1.2#other-features)

### Features also included with Orbeon Forms 2024.1.3

- [Form Builder view mode](https://doc.orbeon.com/release-notes/orbeon-forms-2024.1.3#form-builder-view-mode)
- [Option to store attachments in S3](https://doc.orbeon.com/release-notes/orbeon-forms-2024.1.3#option-to-store-attachments-in-s3)
- [Ability to change the encryption key](https://doc.orbeon.com/release-notes/orbeon-forms-2024.1.3#ability-to-change-the-encryption-key)
- [Tab duplication detection](https://doc.orbeon.com/release-notes/orbeon-forms-2024.1.3#tab-duplication-detection)
- [Improved landing page configuration](https://doc.orbeon.com/release-notes/orbeon-forms-2024.1.3#improved-landing-page-configuration)
- [Other new features](https://doc.orbeon.com/release-notes/orbeon-forms-2024.1.3#improved-landing-page-configuration)

## Other new features and enhancements

Orbeon Forms 2025.1 also includes many other new features and enhancements, including all enhancements present in Orbeon Forms 2024.1.x releases. Some of the highlights are:

- Support for new custom validation "Maximum number of files", on multiple attachment controls ([\#6737](https://github.com/orbeon/orbeon-forms/issues/6737))
- Minimum Number of Files per Control validation for Multiple File Attachments ([\#7333](https://github.com/orbeon/orbeon-forms/issues/7333))
- Store attachment hash in form data, pass hash to persistence, store hash in relational database ([\#6982](https://github.com/orbeon/orbeon-forms/issues/6982))
- WebP support in PDF ([\#7109](https://github.com/orbeon/orbeon-forms/issues/7109))
- When the date format uses leading zeros, allow dates to be entered without separators ([\#7122](https://github.com/orbeon/orbeon-forms/issues/7122))
- Option to fully disable the landing page ([\#7201](https://github.com/orbeon/orbeon-forms/issues/7201)) (2024.1.3)
- Pager: add page selection dropdown/field ([\#7200](https://github.com/orbeon/orbeon-forms/issues/7200)) (2024.1.3)
- Improve wizard.focus() to handle pager ([\#7202](https://github.com/orbeon/orbeon-forms/issues/7202)) (2024.1.3)
- Use dropdown with search for language selector in Add Language dialog ([\#7233](https://github.com/orbeon/orbeon-forms/issues/7233))
- Include the form's schema (XSD) along form definition in the zip export ([\#7303](https://github.com/orbeon/orbeon-forms/issues/7303))
- Provide analyze-string() in JVM environment ([\#7354](https://github.com/orbeon/orbeon-forms/issues/7354)) (2024.1.4)
- xf:instance to support xxf:xinclude="true" ([\#7367](https://github.com/orbeon/orbeon-forms/issues/7367))
- Repeated grids/sections: support paging ([\#4137](https://github.com/orbeon/orbeon-forms/issues/4137)) (2024.1.3)
- JavaScript API for repeated section paging ([\#7183](https://github.com/orbeon/orbeon-forms/issues/7183)) (2024.1.3)
- Ability for form authors to select the default sort column ([\#7144](https://github.com/orbeon/orbeon-forms/issues/7144))

## Compatibility and upgrade notes

### Legacy custom XForms JavaScript events

These long undocumented events are no longer dispatched on the client side:

- `fullUpdateEvent`
- `typeChangedEvent`

### Log4j 1.x compatibility

Support for using Log4j 2.x in Log4j 1.x compatibility mode has been removed. If you were using Log4j 1.x configuration files (`log4j.xml`), please migrate to Log4j 2.x and use `log4j2.xml`.

### Removal of deprecated date/time functions

The following deprecated date/time functions (since 2017!) have been removed. Please use the suggested alternatives.

- `fr:created-date()`: use `fr:created-dateTime()`
- `fr:modified-date()`: use `fr:modified-dateTime()`

### Ehcache 2.x changes and deprecation

Support for Ehcache 2.x as a caching provider is deprecated and will be removed in a subsequent release. The Ehcache 2.x JAR file is no longer provided. If you are using Ehcache 2.x, please migrate to using a JCache provider, such as Infinispan, which is now the default.

If this is not convenient or possible in the short term, you can still, at of Orbeon Forms 2025.1, configure Ehcache 2.x as the caching provider by adding the Ehcache 2.x JAR file to Orbeon Forms' classpath and setting the following property in `properties-local.xml`:

```xml
<property
    as="xs:string"
    name="oxf.xforms.cache.provider"
    value="ehcache2"/>
```
See also:

- Documentation: [Infinispan configuration](/installation/caches.md#orbeon-forms-20251-and-newer-infinispan-configuration)
