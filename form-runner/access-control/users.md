# Setup Users for Access Control

<!-- toc -->

## Providing username, group, and roles

Form Runner uses the username, group, and roles to control who can access:

- Form Builder to edit forms (for more on this, see [access control for editing forms](editing-forms.md));
- Deployed forms you created with Form Builder (for more on this, see [access control for deployed Forms](deployed-forms.md)).

Form Runner can obtain information about username, group, and roles by calling a standard servlet API implemented by your application server (_container-driven method_) or by using HTTP headers (_header-driver method_), typically set by a front-end (e.g. Apache module) or a servlet filter.

### Container driven method

With the container-driven method, Orbeon Forms uses a standard API to ask the container, typically Tomcat, about the current user. Users are typically setup in a directory service, like Active Directory or LDAP, and you setup the container to interface with that directory service. With Tomcat:

- See [Tomcat's Windows Authentication How-To](https://tomcat.apache.org/tomcat-8.0-doc/windows-auth-howto.html) for more on how to setup Tomcat with Active Directory.
- See [Tomcat's JNDIRealm](https://tomcat.apache.org/tomcat-8.0-doc/realm-howto.html#JNDIRealm) for nore on how to setup Tomcat with LDAP.

In addition to the configuration at the container level, at the Orbeon Forms level, you'll want to:

1. __Enable container-driven method__ – To do so, set the following property in your `properties-local.xml`:

    ```xml
    <property
        as="xs:string"
        name="oxf.fr.authentication.method"
        value="container"/>
    ```
2. __List possible roles__ – There is no container API for Orbeon Forms to ask for all the roles for the current user; instead Orbeon Forms can only ask if the current user has a specific role. Because of this, you need to list the possible roles in the following property. For instance, if you have two roles `form-builder-hr` and `form-builder-finance` define them as:

    ```xml
    <property
        as="xs:string"
        name="oxf.fr.authentication.container.roles"
        value="form-builder-hr form-builder-finance"/>
    ```
    Header names are split on commas, pipes, and white space (using the regular expression `,|\s+`).
    [SINCE Orbeon Forms 4.9] The splitting of header names can be overridden with the following property:

    ```xml
    <property
        as="xs:string"
        name="oxf.fr.authentication.container.roles.split"
        value=",|\s+"/>
    ```
3. __Setup groups__ –
There is no container API for Orbeon Forms to get the user's group; in fact the concept of _group_is foreign the container API. So, when using container-driven method, Orbeon Forms takes the first role listed in `oxf.fr.authentication.container.roles` that the user has as the user's group. If you need more flexibility in determining what the user's group is, you might want to use the _header-driven method_ instead, which allows you to explicitly set through a header what the user's group is (more on this below).
4. __Require authentication__ – You'll also want to have another role, say `form-builder`, that you grant to all the users who can access Form Builder. Hence, in our example, users will have either the two roles `form-builder` and `form-builder-hr`, or the two roles `form-builder` and `form-builder-finance`. In Orbeon Forms `WEB-INF/web.xml`, add the following to require users to login to access Form Builder. This assumes that you're using basic authentication:

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

### Header driven method

You want to use header-driven method if you have a servlet filter, single sign-on software, or other system that sets the roles for the current user in an HTTP header.

#### 1. Enable header-driven method

Set the following property in your `properties-local.xml`:

```xml
<property
    as="xs:string"
    name="oxf.fr.authentication.method"
    value="header"/>
```

#### 2. Header names

Tell Orbeon Forms the name of the HTTP headers that contain the username, group, and roles for the current user.

```xml
<property
    as="xs:string"
    name="oxf.fr.authentication.header.username"
    value="My-Username-Header"/>
<property
    as="xs:string"
    name="oxf.fr.authentication.header.group"
    value="My-Group-Header"/>
<property
    as="xs:string"
    name="oxf.fr.authentication.header.roles"
    value="My-Roles-Header"/>
```

The header `oxf.fr.authentication.header.roles` consists of a list of comma- or pipe-separated role names (using the regular expression `(\s*[,\|]\s*)+`), for example:

    Administrator, Power User, User

or:

    Administrator | Power User | User

White space around the commas or pipes is ignored.

In addition or alternatively, multiple role headers can be provided, and each of them is split according to those roles. The resulting set of roles is the combination of all roles extracted from all role headers.

[SINCE Orbeon Forms 4.9] The splitting of header names can be overridden with the following property:

```xml
<property
    as="xs:string"
    name="oxf.fr.authentication.header.roles.split"
    value="(\s*[,\|]\s*)+"/>
```

#### 3. Forwarding headers (Orbeon Forms 4.6 and earlier)

*NOTE: This step is not necessary for Orbeon Forms 4.7 and newer.*

When using header-based authentication, in addition to defining the name of the headers Form Runner gets the username and role from `oxf.fr.authentication.header.username` and `oxf.fr.authentication.header.roles`, you need to add those header names to the `oxf.xforms.forward-submission-headers` property, so the headers are forwarded by the XForms engine to Form Runner. For instance:

```xml
<property
    as="xs:string"
    name="oxf.xforms.forward-submission-headers"
    value="My-Username-Header My-Group-Header My-Roles-Header"/>
```

#### 4. LDAP-style header syntax (Optional)

The value of the header is a list of roles separated by spaces, commas, or pipes (`|`). Furthermore, they can optionally be composed of properties in the form of `name=value`, where `name` is specified by a configuration property, and `value` is the value of the role. This is typically useful the value if the header follows an LDAP-style syntax, for instance:

```
cn=role1,dc=acme,dc=ch|cn=role2,dc=acme,dc=ch
```

If your header follows a LDAP-style syntax, set the following property to configure what "name" contains the header, which in this example is `cn`:

```xml
<property
    as="xs:string"
    name="oxf.fr.authentication.header.roles.property-name"
    value="cn"/>
```

## Accessing username, group and roles in Orbeon Forms

* __Username/role from headers or container__ — Orbeon Forms automatically adds two headers, which are available within Form Runner:
  * `Orbeon-Username` — if present, the value contains the current username
    * if `oxf.fr.authentication.method == "container"`:
        * obtained through the servlet/portlet container's `getRemoteUser()` function
    * if `oxf.fr.authentication.method == "header"`
        * obtained via the header specified by `oxf.fr.authentication.header.username`
  * `Orbeon-Group` — if present, the value contains the current group
  * `Orbeon-Roles` — if present, is a list of values, each with one role
    * if `oxf.fr.authentication.method == "container"`:
        * each role listed in `oxf.fr.authentication.container.roles` is checked against the container's `isUserInRole()` function
    * if `oxf.fr.authentication.method == "header"`
        * obtained via the header specified by `oxf.fr.authentication.header.roles`
* __Persistence__ — These headers are forwarded to the persistence layer, which can make use of them. In particular, the [relational persistence layers](../../form-runner/persistence/relational-db.md) store the current username when doing any database update.

See also: [Accessing liferay users and roles](../../form-runner/link-embed/liferay-full-portlet.md#accessing-liferay-users-and-roles).

[1]: http://wiki.orbeon.com/forms/doc/user-guide/form-builder-user-guide/xpath-expressions#TOC-Scenario:-checking-the-role-s-of-the-current-user
[2]: http://wiki.orbeon.com/forms/doc/user-guide/form-builder-user-guide/control-validation-dialog
[10]: http://tomcat.apache.org/tomcat-6.0-doc/realm-howto.html
