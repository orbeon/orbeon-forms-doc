# Home page

## Compatibility

Until Orbeon Forms 2021.1, the Home page was a combination of the [Published Forms page](published-forms-page.md) and the [Forms Admin page](forms-admin-page.md) and accessible at the path `/fr/`. Starting with Orbeon Forms 2022.1, the Home page is replaced with separate Published Forms and Forms Admin pages at paths `/fr/forms` and `/fr/admin`. The `/fr/` path now reaches the [Landing page](landing-page.md).

If you are using a version of Orbeon Forms prior to 2022.1, you access the combined Home page at `/fr/`, and whether that page functions as the [Published Forms page](published-forms-page.md) or the [Forms Admin page](forms-admin-page.md) depends on whether you have admin permissions.

## Permissions

[SINCE Orbeon Forms 4.3]

[UNTIL Orbeon Forms 2021.1]

If the user doesn't have any permissions set via `form-builder-permissions.xml`, as [documented here](/form-runner/access-control/editing-forms.md#form-builder-permissions), a simple user view is presented:

![Home page](../images/home-simple-view.png)

If the user has permissions set in `form-builder-permissions.xml`, a view with admin privileges is presented:

![Home page](../images/home-admin-view.png)

The list of forms listed depends on the roles set in `form-builder-permissions.xml`. For example, with:

```xml
<role name="*" app="*" form="*"/>
```

the user can perform any admin operation on any form. But with:

```xml
<role name="orbeon-user" app="acme" form="*"/>
```

the user can perform admin operations on `acme` forms only.

## See also

- [Published Forms page](published-forms-page.md)
- [Forms Admin page](forms-admin-page.md)
- [Landing page](landing-page.md)
- [Summary page](summary-page.md)
- [Access control for deployed forms](/form-runner/access-control/deployed-forms.md)
- [Form Builder permissions](/form-runner/access-control/editing-forms.md#form-builder-permissions)
