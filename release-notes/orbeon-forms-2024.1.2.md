# Orbeon Forms 2024.1.2

__Thursday, June xx, 2025__

Today we released Orbeon Forms 2024.1.2! This maintenance release contains more than a hundred bug-fixes and some new features, and is recommended for all users of:

- [Orbeon Forms 2024.1.1 PE](orbeon-forms-2024.1.1.md)
- [Orbeon Forms 2024.1 PE](orbeon-forms-2024.1.md)

TODO:

- check issues numbers on branch vs. on GitHub issues

## New features

### Components with built-in validation

Orbeon Forms is built on top of a component system which allows adding new form controls with a little bit of programming. In fact, this is how most of the form controls which whip with Orbeon Forms are made. In this release, we are improving support for components which provide their own validation, and introducing new components.

![New components with builtin validation](/form-runner/component/images/xbl-ssn-ein-isin-lei.webp)

- [US Employer Identification Number (EIN)](/form-runner/component/us-ein.md)
- [US Social Security Number (SSN)](/form-runner/component/us-ssn.md)
- [Legal Entity Identifier (LEI)](/form-runner/component/lei.md)
- [International Securities Identification Number (ISIN)](/form-runner/component/isin.md)

See also:

- Blog post: [SSN and EIN Form Controls](https://www.orbeon.com/2025/06/ssn-and-ein-form-controls)
- Blog post: [ISIN and LEI Form Controls](https://www.orbeon.com/2025/06/isin-and-lei-form-controls)

### Custom PDF template field names

Orbeon Forms supports PDF templates. This feature requires mapping form fields to PDF fields. To achieve this, until now, you had to name the fields in the PDF to match the Form Runner fields.

![Custom PDF field name](/form-builder/images/control-settings-pdf-field-name-from-field.png)

The "Control Settings" dialog now lets you set a custom PDF field name for each form field. This makes it easier to manage the mapping between form fields and PDF fields.

See also:

- Blog post: [Custom PDF Field Names](https://www.orbeon.com/2025/05/custom-pdf-templates-field-names)
- [PDF Templates](/form-runner/feature/pdf-templates.md)

### New demo forms

We are continuing to add to our collection of demo forms, which you can use directly, as templates for your own forms, or to learn about Orbeon Forms features and capabilities. In this release, we are adding the following demo forms:

- Vendor Application (builder, runner)
- TODO: 1 more?

In addition, we have updated the following demo forms which showcase features:

- All Form Controls
- Examples of Formulas

### xxx

xxx

- Update Spanish translation resources-ca.xml #6997
- Update Catalan translation resources-ca.xml #6996
- Form Builder to support properties to hide buttons based on formula #6805
- Arabic resources for Form Runner and Form Builder #7055
- PDF templates: ability to specify field names #6932
- Add `FormRunnerForm.getControlValue()` #6131
- Form controls/components
    - Dynamic Dropdown With Search: option to store label in all cases #7014

## Issues addressed

xxx

- Form Builder
    - In Control Settings, set focus on Control Name after clicking "Use suggestion" #7060
    - Variable completion in formulas doesn't show after `$a+` #6874
    - Control Settings: long title causes fat dialog title #7035
    - Repeated grid menu incorrectly positioned in PDF Templates and Edit Choices dialogs #7034
    - "java.lang.StringIndexOutOfBoundsException: String index out of range: -1" in Form Builder #7031
    - Duplicate "Dropdown" appearance in Control Settings #7026
    - FB: Repeated grids in dialogs don't show #7005
    - "Service supports paging" option doesn't show correctly #7003
    - Dynamic Dropdown: "Service performs search" missing #7002
    - Actions Editor to make it clear why the Set Control Value isn't available #6994
    - Control Settings Next sometimes switches tab #6973
    - Appearance/description missing for Component/Schema type #6967
    - Support binding `appearance` and another attribute #2479
    - Number: user must not be able to select same decimal and grouping separator #6961
    - Dropdown with search empty in Actions Editor #6959
    - Form Settings: add language selector for title/description #3613
    - FB: crash when passing valid app/form names in URL #6957
    - Browser not to suggest email in Email Settings Email Subject field #6944
    - Test PDF dialog: show PDF template filename #6941
    - FB: icons occasionally showing in the wrong place #6928
    - FB: crash when pasting a section containing a section template #6925
    - "Generate token URL parameter" option in email template parameters not persisted correctly #6919
    - Form Builder doesn't show repeated grid or section iterations with custom name #7067
    - After pasting form definition, Actions Editor dialog appears partially empty #7010
- Form Runner
    - Initial value not set when coming from service with a request parameter #7083
    - `/reindex` API should be a `POST`, not a `GET` #6174
    - In case of XPath error, log expression even if we don't know in which file is came from #7059
    - User without create but with update permission can't attach new files to existing data #7057
    - `$form-resources` doesn't point to the form resources #7056
    - Simple Actions: ability to call async submission provider #7053
    - Draft filesystem attachments are not supported #7051
    - `SocketException`, `IOException`, "Broken pipe" in request/response must not pollute log files #1992
    - Option not to kill form session if request sequence number is unexpected #6965
    - Test formatting Chinese time failing #6986
    - Demo forms: Use WebP for thumbnails #6979
    - Search API response doesn't include `Content-Type` #6974
    - If properties initialization fails, a missing license error also shows #6968
    - Error with "{a}" in message template #6962
    - Control variables in email template parameter formulas not interpreted #6899
    - Action Syntax: custom `<xf:var>` doesn't work #6943
    - Form metadata API: make `<created>` element optional #6942
    - Can't use `doc()` in Calculated Value #6836
    - Icon to copy link with token doesn't show right after saving #6926
    - Mark data unsafe for field being edited #6960
    - Allow list of choices to come from `@fr:itemsetid` #7058
    - Offline: ability to pass user credentials #7072
    - Processes: support `$foo` and `fr:control-string-value()` #7068
    - Purge deletes first 100 items and then stops #7025
    - Import
        - Excel Import: progress shows incorrectly during large import #7029
        - Import: error when choosing to delete existing records #7022
    - PDF
        - Input field with character counter doesn't show in PDF template output #5025
        - Calculated value doesn't show in PDF template #6951
        - PDF templates: signature field is not populated #6922
        - PDF: Empty optional signature can overflow #6954
    - Form controls/components
        - Dynamic Dropdown With Search: option to store label in all cases #7014 
        - Support `fr:is-readonly-mode()` outside of Form Runner, to support `fr:section` in plain XForms #6983
        - Dynamic dropdown with search not to auto-select when readonly #6958
        - Dynamic dropdown with "With Search" and "Auto-select unique choice" doesn't show readonly #6955
        - Hint doesn't work for Dynamic Dropdown #6920
    - Embedding
        - Embedding demo to show Form Builder with JavaScript embedding API #7028
        - Embedding: Form Builder icons don't show upon hover #7009
        - Embedding: error when loading Form Builder #7008
        - Java embedding API doesn't work with Tomcat 10+ / WildFly 27+ (jakarta vs javax) #7004
        - Add Angular and React demos #6938
    - Offline
        - Offline: edit mode fails to load data #7021
        - Offline: attachments don't work anymore #7006
        - Offline: exception when running on the default HTTP port #6987
    - Performance
        - Add dependency support for `fr:app-name()`, etc. #7000
        - Add dependency support for pure functions #6999
        - Add dependency support for `xxf:property()` #6998
        - Add dependency support for `xxf:repeat-position()` #6981
        - `fr:grid`/`fr:section`: don't depend on `*` #6995
        - Add dependency support for `fr:component-param-value()` #6980
        - Cache `BindingIndex` #6972
        - Reindex: don't attempt to reindex data upon DELETE of form definition #6915
        - Reindex is triggered for attachments #6913
- WebSphere, WildFly, DB2
    - WebSphere: missing current servlet error #7052
    - Async I/O requests can fail in Jakarta EE environment #7030
    - Support getting context path of the original request in Tomcat 10+ #6990
    - Db2: add indexes to DDL #6985
    - WildFly: exception when saving #7033
    - Integration with WildFly OIDC to support Keycloak and Cognito in addition to Entra ID #6871
- Infrastructure
    - Run local HTTPBin during tests #7039 
    - Docker error when mounting `context.xml` #6904
    - orbeon-embedding.war missing some dependencies #7011
    - XML test comparison to treat non-breaking spaces as equivalent to regular spaces #6984
    - Include JARs for Redis replication in Docker images #6966
    - Create PDF template with all controls #1993
    - Error with `ResizeObserver` in tests #6952
    - Build failing, maybe after Update fs2-core, fs2-io to 3.12.0 #6939
    - "showModal is not a function" error when running a replication test on 2024.1-pe branch #6905
    - Test result of PDF output #5670
    - third-party library updates

## Compatibility and upgrade notes

### Support of `POST` for `/reindex` API

xxxx also app/form/version

You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page, or use our Docker images.

Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon), [Mastodon](https://mastodon.social/@orbeon), or the [forum](https://www.orbeon.com/community).

We hope you enjoy this release!
