# Form Runner navigation bar

## Introduction

The Form Runner navigation bar shows at the top of the page when Orbeon Forms is not [embedded](../link-embed/) within another application.

![Form Runner navigation bar](../../.gitbook/assets/navbar.png)

It features the following elements:

* The logo, which can be customized
* The form title
  * This always shows
* The language selector
* The login menu
  * \[SINCE Orbeon Forms 2018.2]
  * this is disabled by default
* The link to the Landing page
  * \[SINCE Orbeon Forms 2022.1]
  * this is enabled by default
* The Revision History button
  * [\[SINCE Orbeon Forms 2023.1\]](../../release-notes/orbeon-forms-2023.1.md)
  * this is disabled by default
  * available only on the Detail page in Edit mode
* The Share button
  * [\[SINCE Orbeon Forms 2023.1\]](../../release-notes/orbeon-forms-2023.1.md)
  * this is disabled by default
  * available only on the Detail page in Edit/View modes

## Configuration

### Logo

See [Default logo](../../configuration/properties/form-runner.md#default-logo) for details.

### Language selector

The language selector is enabled by default. You can disable it globally by setting the following property to `false`:

```xml
<property
    as="xs:boolean"
    name="oxf.fr.navbar.language-selector.enable"
    value="false"/>
```

### Login menu

\[SINCE Orbeon Forms 2018.2]

See [Login & Logout](../access-control/login-logout.md) for details.

### Landing page link

\[SINCE Orbeon Forms 2022.1]

This link points to the Landing page. It is enabled by default, but you can disable it globally by setting the following property to `false`:

```xml
<property
    as="xs:boolean"
    name="oxf.fr.navbar.home-link.enable"
    value="false"/>
```

### Revision History button

[\[SINCE Orbeon Forms 2023.1\]](../../release-notes/orbeon-forms-2023.1.md)

This button opens the Revision History pane. It is disabled by default, but you can enable it or disable it by app/form by setting the following property to `true`:

```xml
<property
    as="xs:boolean"
    name="oxf.fr.navbar.data-history.enable.*.*"
    value="true"/>
```

Form Builder explicitly disables it by default, so if you'd like to enable the Revision History button for Form Builder, you must explicitly enable it:

```xml
<property
    as="xs:boolean"
    name="oxf.fr.navbar.data-history.enable.orbeon.builder"
    value="true"/>
```

### Share button

[\[SINCE Orbeon Forms 2023.1\]](../../release-notes/orbeon-forms-2023.1.md)

See [Token-based permissions](../access-control/tokens.md#using-the-share-dialog) for details.

## See also

* [Default logo](../../configuration/properties/form-runner.md#default-logo)
* [Login & Logout](../access-control/login-logout.md)
* [Token-based permissions](../access-control/tokens.md#using-the-share-dialog)
* [Revision history](revision-history.md)
