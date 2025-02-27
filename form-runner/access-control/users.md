# Setup users for access control

## Providing information about the user

### Why Orbeon Forms might need user's information?

Form Runner can use information about the user to control whether that user can access:

- Form Builder to edit forms – for more on this, see [access control for editing forms](editing-forms.md);
- Deployed forms you created with Form Builder – for more on this, see [access control for deployed Forms](deployed-forms.md).

Form Runner can obtain this information either by:

- Calling a standard servlet API implemented by your application server, referred to as _container-driven method_.
- Using HTTP headers set by a reverse proxy or a servlet filter, referred to as _header-driven method_.

### Container-driven or header-driven, which to choose?

1. Are you using the Liferay proxy portlet? In this case, you'll be using the header-driven method, since the Orbeon Forms Liferay proxy portlet [uses headers to pass information about the user to Form Runner](../link-embed/liferay-proxy-portlet.md#configuring-form-runner-to-use-liferay-user-information).
2. Otherwise, are your permissions dependent on more than users being authenticated and on their roles? In this case you need to use header-based permissions. This would for instance be the case if:
    - You are using [group-based permissions](owner-group.md) and you need finer-grained control over what the user's group is. More specifically, with container-based permissions, users information is obtained through the servlet API, which doesn't have a notion of user's group. So in that case, Form Runner takes the first role to be the group, which is fine in certain use cases, but not in others that require more control over what the user's group is.
    - You are using [organization-based permissions](organization.md), as the servlet API doesn't have any support for organizations.
3. Otherwise, do you want to use the [login and logout pages](login-logout.md) provided by Form Runner? If so, you'll want to go with container-based permissions, as those pages rely on facilities provided by the container to do the authentication.
4. Otherwise, you can use either container-based or header-based permissions, going with the one that is the most convenient for you. If your information about users is stored in a system supported by your application server, e.g. you are using LDAP and Tomcat, then container-based is most likely the simplest option. If not, you could do such an integration, e.g. creating a custom security realm for Tomcat, and user container-based permissions, but it is in that case most likely simpler for you to go with header-based permissions and set headers in servlet filter or reverse proxy.

## Container driven method

With the container-driven method, Orbeon Forms uses a standard API to ask the container, typically Tomcat, about the current user. Users are typically setup in a directory service, like Active Directory or LDAP, and you setup the container to interface with that directory service. With Tomcat:

- See [Tomcat's Windows Authentication How-To](https://tomcat.apache.org/tomcat-8.0-doc/windows-auth-howto.html) for more on how to setup Tomcat with Active Directory.
- See [Tomcat's JNDIRealm](https://tomcat.apache.org/tomcat-8.0-doc/realm-howto.html#JNDIRealm) for more on how to setup Tomcat with LDAP.

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
3. __Setup groups__ – There is no container API for Orbeon Forms to get the user's group; in fact the concept of _group_ is foreign the container API. So, when using container-driven method, Orbeon Forms takes the first role listed in `oxf.fr.authentication.container.roles` that the user has as the user's group. If you need more flexibility in determining what the user's group is, you might want to use the _header-driven method_ instead, which allows you to explicitly set through a header what the user's group is (more on this below).
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

[\[SINCE Orbeon Forms 2024.1\]](/release-notes/orbeon-forms-2024.1.md) When using Orbeon Forms with WildFly's native Open ID Connect (OIDC) support, i.e. with an `<auth-method>OIDC</auth-method>` authentication method, Orbeon Forms will automatically try and extract the `roles` claim from the OIDC tokens, and use it to determine the user's roles. WildFly's OIDC implementation currently doesn't perform this step. Please note that OIDC providers such as [Microsoft Entra ID](/installation/azure.md) can be configured to include the users' groups as roles in the OIDC tokens, which can be useful in this context.

## Header-driven method

### Overview

<img alt="" src="/form-runner/images/users-headers.png" width="519">

With the header-driven method, you are responsible for, in the block labeled as "reverse proxy" in the above diagram, to:

1. Authenticate users, preventing requests from going through to Orbeon Forms if unauthenticated access isn't authorized (e.g. redirecting users to a login page if they haven't logged in yet, or otherwise issuing a 403).
2. Set the headers discussed below to pass information about the user to Orbeon Forms.

When using the header-driven method, as it is your reverse proxy performing that performs user authentication, and not the container, you must remove or comment out the `<security-constraint>` in the `web.xml`.

### Individual headers or single header with JSON?

You can pass information about the user either using:

- 3 headers, one for the username, one for the user's roles, and one for user's group.
- 1 header, that contains the user's information in a JSON format specified below.

The following should help you choose whether to use individual headers or a single header with JSON:

1. If using Orbeon Forms 2016.2 or earlier, go for individual headers. (The single header with JSON was introduced in Orbeon Forms 2016.3.)
2. If using [organization-based permissions](organization.md) you'll need to use the single header with JSON, as this is the only way to pass organization-related information to Orbeon Forms.
3. Otherwise, you can whichever technique is more convenient for you, and in most cases using individual headers might be simpler.

### When to set the headers

- [SINCE Orbeon Forms 2020.1] You should provide the headers with every request made to Orbeon Forms for which you want the user to be authenticated, including Ajax requests. If you set the authentication headers for some requests, and then stop passing them, Orbeon Forms will assume the user has logged out, and clear the user's session. You can get the behavior implemented until Orbeon Forms 2019.2 (described below), by setting the `oxf.fr.authentication.header.sticky` property to `true`.
- [UNTIL Orbeon Forms 2019.2] When you provide information about the user through headers, Orbeon Forms stores that information in the session. So for any subsequent request in the same session, you don't need to provide the headers anymore, and in fact even if you do they will be ignored.

```xml
<property
    as="xs:boolean"
    name="oxf.fr.authentication.header.sticky"
    value="true"/>
```

### Enable header-driven method

Whether using individual headers or a single header with JSON, in all cases you need to enable header-based permissions with the following property in your `properties-local.xml`:

```xml
<property
    as="xs:string"
    name="oxf.fr.authentication.method"
    value="header"/>
```

### If using individual headers

#### Header names

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

#### Forwarding headers with 4.6 and earlier

*NOTE: This step is not necessary for Orbeon Forms 4.7 and newer.*

When using header-based authentication, in addition to defining the name of the headers Form Runner gets the username and role from `oxf.fr.authentication.header.username` and `oxf.fr.authentication.header.roles`, you need to add those header names to the `oxf.xforms.forward-submission-headers` property, so the headers are forwarded by the XForms engine to Form Runner. For instance:

```xml
<property
    as="xs:string"
    name="oxf.xforms.forward-submission-headers"
    value="My-Username-Header My-Group-Header My-Roles-Header"/>
```

#### LDAP-style header syntax

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

### If using a single header with JSON

Tell Orbeon Forms the name of the HTTP header that contain the user's information in JSON format:

```xml
<property
    as="xs:string"
    name="oxf.fr.authentication.header.credentials"
    value="My-Credentials-Header"/>
```

The value of the header must be valid JSON, and follow the format described below. An example 

```json
{
  "username"      : "ljohnson",
  "groups"        : [ "employee" ],
  "roles"         : [
                      { "name": "Power User"                                  },
                      { "name": "Full-time"                                   },
                      { "name": "Manager",      "organization": "iOS"         },
                      { "name": "Scrum master", "organization": "Engineering" }
                    ],
  "organizations" : [
                      [ "Acme", "Engineering", "iOS" ],
                      [ "Acme", "Support"            ]
                    ]
}
```

- `username` is mandatory.
- `groups` is optional. If present, its value must be an array with one string, representing the user's group. (An array is used here as we can envision futures version of Orbeon Forms supporting users being part of more than one group.)
- `roles` is optional. If present, its value must be an array of *roles*. Each *role* is an object with a mandatory `name` attribute, and an optional `organization` attribute. When the latter is present, it ties the role to the specified organization, for instance: "Linda is the manager of the iOS organization". For more on the latter, see [Organization-based permissions](organization.md).
- `organizations` is optional. If present, its value must be an array. Each element of the array must in turn be an array, in which the last element is the organization the user is a member of, and preceding elements list where that organization is in the organization hierarchy. For instance, `["Acme", "Engineering", "iOS"]` signifies that the user is a member of the "iOS" organization, and that, in the organization hierarchy, "iOS" is a child organization of "Engineering", and "Engineering" is a child organization of "Acme".

### Beware of ModHeader

If using the ModHeader extension to test sending custom headers to Orbeon Forms, you also need to disable both JavaScript and CSS source maps in Chrome, or you'll get the following error:

> `SessionListeners` object not found in session. `OrbeonSessionListener` might be missing from web.xml.

This is because, out-of-the-box, that is unless you set `oxf.fr.authentication.header.sticky` to `true`, you are expected to send the authentication headers with every request. Orbeon Forms takes missing authentication headers as a sign that you want to tell Orbeon Forms that the user logged out, and consequently Orbeon Forms kills the "previous" user's session. ModHeader sends the headers you specified with every request, except for source maps. So after Chrome makes a request for a source maps, the session is cleared, and you'll have a problem on the next request that relies on a session, like an Ajax request.

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

See also [Accessing liferay users and roles](../../form-runner/link-embed/liferay-full-portlet.md#accessing-liferay-users-and-roles).

## See also

- [Login & Logout](login-logout.md) - Optional user menu for providing links to login and logout functions.
- [Access control for deployed forms](deployed-forms.md) - How to control access to deployed forms.
- [Form fields](form-fields.md) - How to control access to specific form fields based on the user's roles.
- [Access control for editing forms](editing-forms.md) - How to control access to Form Builder.
    - [Owner and group member permissions](owner-group.md) - Access based on ownership and groups.
    - [Organization-based permissions](organization.md) – Access based on organizational structure.
    - [Token-based permissions](tokens.md) - Token-based permissions
- [Scenarios](scenarios.md)
