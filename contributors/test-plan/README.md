# Test Plan

For each release of Orbeon Forms, we follow this test plan, which tests functionality in addition to the ~800 automatic unit tests which run with every build of Orbeon Forms. In the future, we want to [automate most of this](https://github.com/orbeon/orbeon-forms/issues/227).

## Misc

### Distribution [4.10 DONE]

- README.md is up to date
  - links not broken (use Marked to save HTML, then check w/ Integrity)
  - latest release year
  - version number is correct
  - links to release notes (include link to new version even if blog post not up yet)
- file layout is correct in zip and wars
- check WAR files have reasonable sizes (sizes as of 4.8)
  - orbeon-auth.war (< 10 KB)
  - orbeon-embedding.war (1-2 MB)
  - proxy-portlet.war (1-2 MB)
  - orbeon.war (65 MB)
- check CE zip doesn't have
  - orbeon-embedding.war
  - proxy-portlet.war
- dropping the WAR file (with license included or in ~/.orbeon/license.xml) into Tomcat and Liferay works - out of the box
- make sure the PE license is not included

### Landing Page [4.10 DONE]

- version number is correct in logs when starting
- home page
  - layout of FR examples
  - layout of XForms examples
- XForms examples
  - load, look reasonable, and work

    - [PE Features Availability](pe-features-availability.md) [4.10 DONE]

## Persistence

    - [Basic Persistence](basic-persistence.md) [4.10 DONE]
    - [Versioning](versioning.md)
    - [Data Capture Permissions](data-capture-permissions.md) [4.10 DONE]
    - [Autosave and Permissions Test](autosave-and-permissions.md) [4.10 DONE]
    - [Other Database Tests](other-database-tests.md) [4.10 DONE]

## Form Builder

    - [Basic Features](basic-features.md) [4.10 DONE]
    - [Schema Support](schema-support.md) [4.10 DONE]
    - [Services and Actions](services-and-actions.md) [4.10 DONE]

## Form Builder / Form Runner

    - [Section Templates](section-templates.md) [4.10 DONE]
    - [PDF Automatic](pdf-automatic.md) [4.10 DONE]
    - [PDF Template](pdf-template.md) [4.10 DONE]
    - [Form Builder Permissions](form-builder-permissions.md) [4.10 DONE]

## Form Runner

    - [Sample forms](sample-forms.md) [4.10 DONE]
    - [New, Edit, Review Pages](new-edit-review-pages.md) [4.10 DONE]
    - [Responsive](responsive.md) [4.10 DONE]
    - [Home Page](home-page.md) [4.10 DONE]
    - [Summary Page](summary-page.md) [4.10 DONE]
    - [Excel Import](excel-import.md) [4.10 DONE]
    - [Liferay Support](liferay-support.md) [4.10 DONE]
    - [Embedding](embedding.md) [4.10 DONE]
    - [XForms Retry](xforms-retry.md) [4.10 DONE]
    - [Error Dialog](error-dialog.md) [4.10 DONE]
    - [Other Browsers](other-browsers.md) [4.10 DONE]
    - [Other](other.md) [4.10 DONE]
