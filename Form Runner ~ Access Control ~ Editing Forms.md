> [[Home]] ▸ Form Runner ▸ [[Access Control |Form Runner ~ Access Control]]

## Access to specific apps/forms in Form Builder

This is configured with `form-builder-permissions.xml`.

If you'd like to have multiple classes of Form Builder users where some case edit, say, form in the `hr` app, while other can edit forms in the `sales` app, then you'll want to setup the `form-builder-permissions.xml`.

_NOTE: [SINCE 2011-09-07] The file is now called `form-builder-permissions.xml` file. It replaces the file called `form-runner-roles.xml`. For backward compatibility, `form-runner-roles.xml` is still supported._

In this file you map role names to applications and forms. For instance, the following tells Orbeon Forms that only users with the role `hr-form-editor` can edit or create forms in the `hr` app, and only users with the role `sales-form-editor` can edit or create forms in the `sales` app. As you can infer from the syntax, you could be even more specific and only give access to users with a given role to a _specific_ form in a _specific_ app.

```xml
<roles>
  <role name="hr-form-editor"    app="hr" form="*"/>
  <role name="sales-form-editor" app="sales" form="*"/>
</roles>
```

_NOTE: Each `<role>` element refers to as single role name. It is __not__ possible to place multiple space-separated roles in the `name` attribute._

Orbeon Forms can infer the roles for the current user either based on information it gets from the container or from an HTTP header. Those two cases are detailed in the following two sections. Once you've defined your `form-builder-permissions.xml` and done the appropriate setup for container-driven or header-driven roles, as described below:


1. The Form Builder summary page will only show the forms users have access to.
2. When they create a new form, if users don't have the right to create a form in any app, instead of seeing a text field where they can enter the application name, they will see a drop-down listing the possible application, as shown in the following screenshot:

![][9]

[LIMITATION] Restrictions on the form name in `form-builder-permissions.xml` are at this point not supported; only restrictions on the app name are supported. This means that you should always use `form="*"`. If you define a restriction on the form name, it won't be enforced at the time the form is created, allowing users to create, save, and publish a form with an undesirable name. However they then won't be able to see the form they created when going back to the summary page.

## Form Runner Home page

[SINCE Orbeon Forms 4.3]

`form-builder-permissions.xml` also impacts the Form Runner Home page, which supports unpublishing and publishing forms.

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

|Path|Description|
|----|-----------|
| `/orbeon/fr/orbeon/builder/new` |  To create a new form. |
| `/orbeon/fr/orbeon/builder/edit/{id}` |  To edit a form with the given id. |
| `/orbeon/fr/orbeon/builder/summary` |  To view all the editable forms. |

If you have multiple classes of Form Builder users and want to give access to Form Builder to one class of users, and those users are able to edit any form in any app, the you can use path-based access restrictions.

Orbeon Forms does not specifically provide a mechanism to protect access based on paths, but your container or web server might.

[9]: http://wiki.orbeon.com/forms/_/rsrc/1309558021753/doc/developer-guide/form-runner/access-control/20110701-150512.png
