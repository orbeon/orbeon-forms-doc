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

[1]: http://wiki.orbeon.com/forms/doc/user-guide/form-builder-user-guide/xpath-expressions#TOC-Scenario:-checking-the-role-s-of-the-current-user
[2]: http://wiki.orbeon.com/forms/doc/user-guide/form-builder-user-guide/control-validation-dialog
[3]: http://wiki.orbeon.com/forms/doc/developer-guide/form-runner/oracle-and-mysql-persistence-layers
[10]: http://tomcat.apache.org/tomcat-6.0-doc/realm-howto.html