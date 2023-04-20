# Owner and group member permissions



## Availability

See [Database Support](../../form-runner/persistence/db-support.md).

## Usage

Owner-based and group-based permissions are useful when you want users to only see their own data, or maybe also the data of other users in the same group. For Orbeon Forms to be able to show a user only her data, Orbeon Forms needs to know who that user is, and hence this can only be used for authenticated users.

To use this feature for a form, in Form Builder, when editing a form, open the *Permissions* dialog, and check boxes on the *Owner* and *Group member* lines as appropriate for your situation.

<img alt="The Permissions dialog" src="../../form-builder/images/permissions-dialog.png" width="502">

## Configuration

### Since Orbeon Forms 4.9

There is no particular configuration.

### With Orbeon Forms 4.8.x (eXist database only)

When using this features with eXist, you need to set the following property:

```xml
<property 
    as="xs:string"
    name="oxf.xforms.forward-submission-headers"
    value="Orbeon-Username Orbeon-Roles Orbeon-Group"/>
```

### With Orbeon Forms 4.3

With Orbeon Forms 4.3, owner/group-based permissions were not enabled by default, and you must set the following 2 properties to enable them:

```xml
<property as="xs:boolean" name="oxf.fr.support-owner-group" value="true"/>
<property as="xs:boolean" name="oxf.fr.support-autosave"    value="true"/>
```

## See also

- Blog post: [Owner/group-based permissions AKA "See your own data"](https://blog.orbeon.com/2013/09/ownergroup-based-permissions-aka-see.html)
- [Setup users for access control](users.md) - How to setup Orbeon Forms so that users and roles are provided.
- [Login & Logout](login-logout.md) - Optional user menu for providing links to login and logout functions.
- [Access control for deployed forms](deployed-forms.md) - How to control access to deployed forms.
- [Form fields](form-fields.md) - How to control access to specific form fields based on the user user's roles.
- [Access control for editing forms](editing-forms.md) - How to control access to Form Builder.
    - [Organization-based permissions](organization.md) – Access based on organizational structure.
    - [Token-based permissions](tokens.md) - Token-based permissions
- [Scenarios](scenarios.md)
