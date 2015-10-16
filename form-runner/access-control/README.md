# Access Control

<!-- toc -->

## Introduction

In this section, we'll go over how you can secure Form Runner and Form Builder. For access control, Orbeon Forms leverages and delegates some work to external security infrastructure. In particular, you define users and their roles/group outside of Orbeon Forms.

Access control touches on the following:

- __Form definitions__ – Can the user create, edit, or view form definitions.
- __Published forms__ – Can the user access this deployed form? If so, what operations (such as creating, viewing, editing, deleting) are allowed?
- __Form fields__ – If the user can access the form, can she access a particular field or group of fields? If so, can the field be changed or just viewed?

The following pages address specific topics:

- [Setup](setup.md) - How to setup Orbeon Forms so that users and roles are provided.
- [Editing Forms](editing-forms.md) - How to control access to Form Builder.
- [Deployed Forms](deployed-forms.md) - How to control access to deployed forms.
  - [Owner and Group Member permissions](owner-group.md) - How to control access based on ownership and groups.
- [Form Fields](form-fields.md) - How to control access to specific form fields, grids or sections.

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
