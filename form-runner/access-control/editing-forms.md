# Access control for editing forms

## Form Builder permissions

Specific Form Builder permissions are configured with `WEB-INF/resources/config/form-builder-permissions.xml`.

If you'd like to have multiple classes of Form Builder users where some case edit, say, form in the `hr` app, while other can edit forms in the `sales` app, then you'll want to setup the `form-builder-permissions.xml`.

_NOTE: The file used to be called `form-runner-roles.xml`._

In this file you map role names to applications and forms. For instance, the following tells Orbeon Forms that only users with the role `hr-form-editor` can edit or create forms in the `hr` app, and only users with the role `sales-form-editor` can edit or create forms in the `sales` app. As you can infer from the syntax, you could be even more specific and only give access to users with a given role to a _specific_ form in a _specific_ app.

```xml
<roles>
  <role name="hr-form-editor"    app="hr" form="*"/>
  <role name="sales-form-editor" app="sales" form="*"/>
</roles>
```

_NOTE: Each `<role>` element refers to as single role name. It is __not__ possible to place multiple space-separated roles in the `name` attribute._

Orbeon Forms can infer the roles for the current user either based on information it gets from the container or from an HTTP header. Those two cases are detailed in the following two sections. Once you've defined your `form-builder-permissions.xml` and done the appropriate setup for container-driven or header-driven roles, as described below:

1. The Form Builder summary page will only show the forms users have access to.
2. When they create a new form, if users don't have the right to create a form in any app, instead of seeing a text field where they can enter the application name, they will see a drop-down listing the possible application, as shown in the following screenshot:

<img src="../../form-builder/images/new-form-app-dropdown.png" width="603">

LIMITATION: Restrictions on the form name in `form-builder-permissions.xml` are at this point not supported; only restrictions on the app name are supported. This means that you should always use `form="*"`. If you define a restriction on the form name, it won't be enforced at the time the form is created, allowing users to create, save, and publish a form with an undesirable name. However they then won't be able to see the form they created when going back to the summary page.

## Form Runner Home page

[SINCE Orbeon Forms 4.3]

`form-builder-permissions.xml` also impacts the Forms Admin page, which supports making forms available and unavailable.

If the configuration is unchanged, by default users cannot unpublish/publish from the Home page. In order to allow this feature, you must configure at least one `<role>`.

In general, the behavior is as follows:

* by default no `<role>` elements are present
    * for the Form Builder Summary and New pages, this is equivalent to:

    ```xml
    <role name="*" app="*" form="*">
    ```
* for the Form Runner Home page
    * no `<role>` elements
        * user cannot unpublish/publish any forms
        * user cannot see unavailable forms
    * with `<role name="*" app="*" form="*">`:
        * user can unpublish/publish any forms
        * user sees unavailable forms

This logic ensures:

* that Form Builder is usable out of the box even without setting Form Builder permissions
* that the Form Runner Home page, which can by default be accessed by any user unless it is explicitly protected, does not inadvertently provide access to administrative functions

## Path-based permissions

The paths used by Form Builder look as follows:

| Path                                  | Description                   |
|---------------------------------------|-------------------------------|
| `/orbeon/fr/orbeon/builder/new`       | Create a new form             |
| `/orbeon/fr/orbeon/builder/edit/{id}` | Edit a form with the given id |
| `/orbeon/fr/orbeon/builder/summary`   | View all the editable forms   |

Path-based access restrictions can also be implemented to fully or partially protect Form Builder access.

Orbeon Forms does not specifically provide a mechanism to protect access based on paths, but your container or web server might.

## See also

- [Setup users for access control](users.md) - How to setup Orbeon Forms so that users and roles are provided.
- [Login & Logout](login-logout.md) - Optional user menu for providing links to login and logout functions.
- [Access control for deployed forms](deployed-forms.md) - How to control access to deployed forms.
- [Form fields](form-fields.md) - How to control access to specific form fields based on the user user's roles.
- Access control for editing forms - How to control access to Form Builder.
    - [Owner and group member permissions](owner-group.md) - Access based on ownership and groups.
    - [Organization-based permissions](organization.md) – Access based on organizational structure.
    - [Token-based permissions](tokens.md) - Token-based permissions
- [Scenarios](scenarios.md)
