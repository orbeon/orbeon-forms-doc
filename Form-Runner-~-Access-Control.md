## Introduction

This page describes how to secure Form Runner depending on your usage scenario.

## See also

- [Securing Form Runner](http://wiki.orbeon.com/forms/doc/user-guide/form-runner-user-guide#TOC-Securing-Form-Runner)
- [Legacy configuration for Orbeon Forms 3.9](http://wiki.orbeon.com/forms/doc/developer-guide/form-runner/access-control)

## Overview

When it comes to access control, Orbeon Forms leverages and delegates as much as possible to your existing security infrastructure:

* You define users and roles outside of Orbeon Forms.
* Whenever possible, access control is path-based, so you can define who can access what based on the path in the URL with your existing security infrastructure.

You can have access control at two levels:

* __Form level__ – Can the current user access this form?
* __Field level__ – If the current user can access the form, can they access a particular field? If they can, can they change the field or just read its value?

You implement the later in your form definition, by using the [`$fr-roles`][1] in the in the _visibility_ and _read-only_ XPath expressions of the [Form Builder control validation dialog][2]. You can make a control non-visible to the current user by defining a _visibility_ expression that returns false. If the control is visible, you can make it read-only to current user by defining a _read-only_ expression that returns true. The rest of this page focuses on form-level access control.

## Providing username, group, and roles

Form Runner uses the username, group, and roles to control who can access Form Builder and the forms you create with Form Builder (see the two sections above for more details on how those are setup). Form Runner can obtain this information by calling a standard servlet API implemented by your application server (_container-driven method_) or by using HTTP headers (_header-driver method_), typically set by a front-end (e.g. Apache module) or a servlet filter.

### Container-driven method

[SINCE 2011-07-01]

You want to use container roles if your users are setup at the application server level, with container managed security. In Tomcat, this would correspond to using a [security realm][10], which in its simplest form gets users from Tomcat's `conf/tomcat-users.xml`. To setup container-driven roles, configure your `form-builder-permissions.xml` as described above, then:


1. __Enable container-driven method__ – To do so, set the following property in your `properties-local.xml`:

    ```xml
    <property
        as="xs:string"
        name="oxf.fr.authentication.method"
        value="container"/>
    ```
2. __List possible roles__ – There is no container API for Orbeon Forms to ask for all the roles for the current user; instead Orbeon Forms can only ask if the current user has a specific role. Because of this, you need to list the possible roles in the following property. For instance, if you have two roles `form-builder-hr` and `form-builder-finance` define it as:

    ```xml
    <property
        as="xs:string"
        name="oxf.fr.authentication.container.roles"
        value="form-builder-hr form-builder-finance"/>
    ```
There is no container API for Orbeon Forms to get the user's group; in fact the concept of _group_is foreign the container API. So, when using container-driven method, Orbeon Forms takes the first role listed in `oxf.fr.authentication.container.roles` that the user has as the user's group. If you need more flexibility in determining what the user's group is, you might want to use _header-driven method_ instead, which allows you to explicitly set through a header what the user's group is (more on this below).
3. __Require authentication__ – You'll also want to have another role, say `form-builder`, that you grant to all the users who can access Form Builder. Hence, in our example, users will have either the two roles `form-builder` and `form-builder-hr`, or the two roles `form-builder` and `form-builder-finance`. In Orbeon Forms `WEB-INF/web.xml`, add the following to require users to login to access Form Builder. This assumes that you're using basic authentication:

    ```xml
    <security-constraint>
        <web-resource-collection>
            <web-resource-name>Form Builder</web-resource-name>
            <url-pattern>/fr/orbeon/builder/*</url-pattern>
        </web-resource-collection>
        <auth-constraint>
            <role-name>form-builder</role-name>
        </auth-constraint>
    </security-constraint>
    <login-config>
        <auth-method>BASIC</auth-method>
    </login-config>
    <security-role>
        <role-name>form-builder</role-name>
    </security-role>
    ```

### Header-driven method

You want to use header-driven method if you have a servlet filter, single sign-on software, or other system that sets the roles for the current user in an HTTP header. To use header-driven method, configure your `form-builder-permissions.xml` as described above, then:

1. __Enable header-driven method__ – To do so, set the following property in your `properties-local.xml`:

    ```xml
    <property
        as="xs:string"
        name="oxf.fr.authentication.method"
        value="header"/>
    ```
2. __Header name__ – Tell Orbeon Forms what is the name of the HTTP headers that will contain the username, group, and roles for the current user.

    ```xml
    <property as="xs:string" name="oxf.fr.authentication.header.username" value="My-Username-Header"/>
    <property as="xs:string" name="oxf.fr.authentication.header.group"    value="My-Group-Header"/>
    <property as="xs:string" name="oxf.fr.authentication.header.roles"    value="My-Roles-Header"/>
    ```
The header `oxf.fr.authentication.header.roles` consists of a list of comma- or pipe-separated role names, for example: "Administrator, Power User, User" or"Administrator | Power User | User". White space around the commas or pipes is ignored. In addition or alternatively, multiple role headers can be provided, and each of them is split according to those roles. The resulting set of roles is the combination of all roles extracted from all role headers.
3. __Forwarding headers__ — When using header-based authentication, in addition to defining the name of the headers Form Runner gets the username and role from `oxf.fr.authentication.header.username` and `oxf.fr.authentication.header.roles`, you need to add those header names to the `oxf.xforms.forward-submission-headers` property, so the headers are forwarded by the XForms engine to Form Runner. For instance:

    ```xml
    <property
        as="xs:string"
        name="oxf.xforms.forward-submission-headers"
        value="My-Username-Header My-Group-Header My-Roles-Header"/>
    ```
4. __LDAP-style header syntax (Optional)__ – The value of the header is a list of roles separated by spaces, commas, or pipes (`|`). Furthermore, they can optionally be composed of properties in the form of `name=value`, where `name` is specified by a configuration property, and `value` is the value of the role. This is typically useful the value if the header follows an LDAP-style syntax, for instance: `cn=role1,dc=acme,dc=ch|cn=role2,dc=acme,dc=ch`. If your header follows a LDAP-style syntax, set the following property to configure what "name" contains the header, which in this example is `cn`:

    ```xml
    <property
        as="xs:string"
        name="oxf.fr.authentication.header.roles.property-name"
        value="cn"/>
    ```

## Accessing the username and roles

[SINCE 2011-05-18]

* __Username/role from headers or container__ — Orbeon Forms automatically adds two headers, which are available within Orbeon Forms applications, in particularForm Runner:

* `Orbeon-Username` — if present, the value contains the current username`
`
    * if `oxf.fr.authentication.method == "container"`:
        * obtained through the servlet/portlet container's `getRemoteUser()` function
    * if `oxf.fr.authentication.method == "header"
        * obtained via the header specified by `oxf.fr.authentication.header.username`
* `Orbeon-Roles` — if present, is a list of values, each with one role
    * if `oxf.fr.authentication.method == "container"`:
        * each role listed in `oxf.fr.authentication.container.roles` is checked against the container's `isUserInRole()` function
    * if `oxf.fr.authentication.method == "header"`
        * obtained via the header specified by `oxf.fr.authentication.header.roles`
* __Persistence__ — These headers are forwarded to the persistence layer, which can make use of them. In particular, the [relational persistence layers][3] store the current username when doing any database update.

See also: [Accessing liferay users and roles](http://wiki.orbeon.com/forms/doc/developer-guide/admin/deployment-portlet#TOC-Accessing-liferay-users-and-roles).

## Access control for deployed forms

[SINCE Orbeon Forms 4.0]

### Enabling permissions

For forms created in Form Builder, you can restrict which users can access which forms, and what operations they can perform. Those restrictions apply to the forms you create once they are deployed, not to editing those forms in Form Builder (for the latter, see the section that follows: _Access control for editing forms_).

By default, no restriction is imposed on _who_ can do _what_ with forms you create in Form Builder. You enable permissions by going to the Form Builder sidebar, and under _Advanced_, clicking on _Set Permissions_.

![][5]

This shows the following dialog:

![][6]

### Setting permissions

After you click on the checkbox, you'll be able to set access restriction on the _create_, _read, update_, and _delete_ operations:

1. On the _Anyone_ line, set the operations allowed to all users.
2. On the _Owner_ line, set the operations allowed to the user who created the data. [SINCE Orbeon Forms 4.3]
3. On the_Group members_line, set the operations allowed to users in the same group as the owner. [SINCE Orbeon Forms 4.3] 
4. On the following lines, you can enter a role name, and define what operations users with that role can perform.

### Example

In the example below:

* Any user to fill out a new form.
* Users with the role _clerk_to read data.
* Users with the role _admin_ to do any operation, including deleting form data.

![][7]

### Permissions dialog

* Permissions you set in the dialog are _additive_ –Say you defined permissions for two roles, where users with the_reader_role can read and users in the_clerk_role can delete, users with both roles (_reader_and_clerk_)are allowed to perform both operations (reading and deleting).
* Operation on _Anyone_ apply to all other rows – When you select a checkbox for a given operation on the first _Anyone_ row, that checkbox will be automatically checked and disabled so you can't change it, for any additional row, since you wouldn't want to authorize users with additional roles to perform less operations.
* _Update_ implies _read_ – On any row, if you check _update_, then _read_ will be automatically checked, as it wouldn't make sense to say that some users can update data, but can't read it, as when updating data, obviously, they must be shown the data they are updating.
* _Create_ can't be set for the _owner_ and _group members_ – The owner/group is a piece of information attached to existing form data, keeping track of the user who create the data, and the group in which this user is. This information is only known for existing data, so assigning the _create_ permission to the _owner_ or _group members_ doesn't make sense, and the dialog doesn't show that checkbox.
* Permissions for the _owner_ and _group members_ can be setindependently– If you want data to be accessible only by people who created it, check read/update/delete for the owner but not for group members. If you want data to be accessible by all people in the same group,check read/update/delete for the group members and don't check them for the owner if you want the owner to loose access to that data in case the owner changes group. (The latter highlights the need for permissions owner and group member to be set independently.)

### Permissions for owner / group members

This part of the documentation has [moved][8].

### Access restrictions

Which operations the current user can perform drives what page they can access, and on some pages which buttons are shown:

* On the Form Runner _home_ page, all the forms on which the current user can perform at least one operation are displayed. Then, for each one of those forms:

    * If they can perform the _create_ operation on the form, a link to the _new_ page is shown.
    * If they can perform any of the _read_, _update_, or _delete_ operation on the form, a link to the _summary_ page for that form is shown.
* For the _summary_ page:
    * Access is completely denied if the current user can't perform any of the _read_, _update_, or _delete_ operations.
    * The _delete_ button is disabled if the current user can't perform the _delete_ operation.
    * The _review_ and _pdf_ button are disabled if the current user can't perform the _read_ operation.
    * Clicking in a row of the table will open the form in _edit_ mode if the current user can perform the _update_ operation, in _view_ mode if they can perform the _read_ operation, and do nothing otherwise.

* For the _view_ page, access is denied if the current user can't perform the _read_ operation.
* For the _new_ page, access is denied if the current user can't perform the _create_ operation.
* For the _edit_ page, access is denied if the current user can't perform the _update_ operation.

[SINCE 4.3] In Orbeon Forms 4.2 and earlier, role-based permissions set in Form Builder could only be driven by container-based roles and the value of the `oxf.fr.authentication.method` property was not taken into account. Since version 4.3, those permissions also apply if you are using header-driven roles.

## Access control for editing forms

### Access to Form Builder as a whole


Given the assumptions made in the previous section, the paths used by Form Builder look as follows:


|Path|Description|
|----|-----------|
| `/orbeon/fr/orbeon/builder/new` |  To create a new form. |
| `/orbeon/fr/orbeon/builder/edit/{id}` |  To edit a form with the given id. |
| `/orbeon/fr/orbeon/builder/summary` |  To view all the editable forms. |

If you have multiple classes want to give access to Form Builder to one class of users, and those users are able to edit any form in any app, the you can use path-based access restrictions, as described the previous section.

### Access to specific apps/forms in Form Builder

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

### Form Runner Home page

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

## Scenarios

### 1. Anonymous data capture with administrator

1. Setup at least one user and role for your container.
    - The easiest way to do this with Tomcat, if you don't have already users setup within your system (via a Tomcat realm for example), is to modify Tomcat's `tomcat-users.xml` file, for example as follows:

    ```xml
    <tomcat-users>
      <user
        username="orbeon-admin"
        password="Secret, change me!" 
        roles="orbeon-admin"/>
    </tomcat-users>
    ```

1. Enumerate the role in the following property:

    ```xml
    <property
      as="xs:string"
      name="oxf.fr.authentication.container.roles"
      value="orbeon-admin"/>
    ```
1. Protect Form Builder and, optionally, the Form Runner Home page
    - In web.xml, uncomment the permissions section.
    - Replace:

    ```xml
    <url-pattern>/fr/*</url-pattern>
    ```

    with:

    ```xml
    <url-pattern>/fr/orbeon/builder/*</url-pattern>
    <!-- Optional, to prevent anonymous users from accessing the Form Runner Home Page -->
    <url-pattern>/fr/</url-pattern>
    ```
    and replace the role name:

    ```xml
    <role-name>orbeon-user</role-name>
    ```
1. Set, in `form-builder-permissions.xml`:

    ```xml
    <role name="orbeon-admin" app="*" form="*"/>
    ```
1. Remove demo forms and apps from Orbeon Forms.
  - See [Creating a Production WAR](https://github.com/orbeon/orbeon-forms/wiki/Installation-~-Creating-a-Production-WAR).
1. Within Form Builder
  - make sure that all your forms have permissions enabled (PE feature only)
  - set the `create` permission for all users
  - set all permissions for the role `orbeon-admin`
  - republish your forms
  - see also [#1860](https://github.com/orbeon/orbeon-forms/issues/1860))
1. Configure forms' buttons
  - You will want only a "Send" or "Submit" button, as a plain "Save" button doesn't make sense in this case.

With this setup:

- Published forms are not protected by the container. They are protected by Form Runner permissions.
- Form Builder is protected by the container so that anonymous users can't create new forms.
- Form Builder also requires `orbeon-admin` at the Orbeon Forms level.
- Any user, logged in or anonymous, can create form data from any published form.
- All other operations (`read`, `update`, `delete`) are not available to anonymous users or logged in users without the `orbeon-admin` role.
Users with the `orbeon-admin` role have
- Users with the `orbeon-admin` role can perform any operations on the form data after they are logged in.

Limitations:

- Anonymous data entry does not support autosave.
- As an administrator, you first have to login, for example by accessing Form Builder, before accessing published forms' Summary page (issue [#1292](https://github.com/orbeon/orbeon-forms/issues/1292)).

[1]: http://wiki.orbeon.com/forms/doc/user-guide/form-builder-user-guide/xpath-expressions#TOC-Scenario:-checking-the-role-s-of-the-current-user
[2]: http://wiki.orbeon.com/forms/doc/user-guide/form-builder-user-guide/control-validation-dialog
[3]: http://wiki.orbeon.com/forms/doc/developer-guide/form-runner/oracle-and-mysql-persistence-layers
[4]: http://docs.oracle.com/cd/E19798-01/821-1841/bncbk/index.html
[5]: http://wiki.orbeon.com/forms/_/rsrc/1357774269110/doc/developer-guide/form-runner/access-control/advanced1.png
[6]: http://wiki.orbeon.com/forms/_/rsrc/1371575909134/doc/developer-guide/form-runner/access-control/Screen%20Shot%202013-06-18%20at%2010.17.17%20AM.png
[7]: http://wiki.orbeon.com/forms/_/rsrc/1371576079151/doc/developer-guide/form-runner/access-control/Screen%20Shot%202013-06-18%20at%2010.20.25%20AM.png
[8]: https://github.com/orbeon/orbeon-forms/wiki/Form-Builder-~-Permissions-~-Owner-Group
[9]: http://wiki.orbeon.com/forms/_/rsrc/1309558021753/doc/developer-guide/form-runner/access-control/20110701-150512.png
[10]: http://tomcat.apache.org/tomcat-6.0-doc/realm-howto.html