# Landing page

## Availability

[SINCE Orbeon Forms 2022.1]

## Introduction

The Landing page is organized in cards which provides quick access to:

- Quick links, including Form Builder and the Administration page
- Your published forms
- Your in-progress Form Builder forms
- Demo forms

![The Landing page](/form-runner/images/landing-form-data-card.png)

Some cards directly list content, including the list of published forms and the list of in-progress Form Builder forms.

You can configure whether you want to have a particular card on the Landing page. For example, you can hide the demo forms for production deployment.

All Form Runner and Form Builder navigation bars now provide a direct link to the Landing page. This can be disabled if not desired.

![Landing page navigation](/release-notes/images/summary-navigation.png)

## Configuration properties

The following property control which cards are shown on the Landing page:

```xml
<property
    as="xs:string"
    name="oxf.fr.landing.cards"
    value="quick-links published-forms form-builder-forms demo-forms"/>
```

[\[SINCE Orbeon Forms 2024.1\]](/release-notes/orbeon-forms-2024.1.md)

You can add cards showing the latest data for specific forms. You do so by adding a token of the form `$app/$form/$version`, where:

- `$app` is the application name
- `$form` is the form name
- `$version` is the form version

For example, to show the latest data for the form `acme/sales/1`, you would use:

```xml
<property
    as="xs:string"
    name="oxf.fr.landing.cards"
    value="quick-links published-forms form-builder-forms acme/sales/1"/>
```

[\[SINCE Orbeon Forms 2024.1.1\]](/release-notes/orbeon-forms-2024.1.1.md)

The following property controls the number of rows available on cards shown on the Landing page:

```xml
<property
    as="xs:integer"
    name="oxf.fr.landing.page-size"
    value="8"/>
```

## See also 

- [Published Forms page](published-forms-page.md)
- [Forms Admin page](forms-admin-page.md)
- [Summary Page](summary-page.md)
    - [Summary page configuration properties](/configuration/properties/form-runner-summary-page.md)
    - [Summary page buttons and processes](/form-runner/advanced/buttons-and-processes/summary-page-buttons-and-processes.md)
    - [Form Builder Summary Page](/form-builder/summary-page.md)
- [Access control for deployed forms](/form-runner/access-control/deployed-forms.md)
- [Form Builder permissions](/form-runner/access-control/editing-forms.md#form-builder-permissions)
- [Versioning](/form-runner/feature/versioning.md)
- [Control Settings dialog](/form-builder/control-settings.md)
- Blog post: [Summary page versioning support](https://blog.orbeon.com/2019/05/summary-page-versioning-support.html)