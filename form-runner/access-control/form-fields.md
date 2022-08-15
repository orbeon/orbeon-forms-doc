# Form Fields

## Overview

You can control access to specific form fields based on the user user's roles.

## Using and accessing roles

The `$fr-roles` XPath variable can be used in formulas controlling whether a field or section is visible or readonly. `$fr-roles` contains the list (as an XPath sequence) of roles of the current user, if any. Each role is represented as a string.

You can make a control non-visible to the current user by defining a _visibility_ expression that returns `false()`. If the control is visible, you can make it readonly to current user by defining a _readonly_ expression that returns `true()`.

## Examples

The following "Visibility" expression makes a section visible only if one of the roles has value `admin`:

```xpath
fr:user-roles() = 'admin'
```

Due to the logic of XPath comparison on sequences, this expression returns `true()` if at least one of the roles is `admin`, even if there are other roles available.

*NOTE: Until Orbeon Forms 2016.1, use the following instead.*

```xpath
$fr-roles = 'admin'
```

<!--
TODO: more examples (in particular using `xxf:get-header('Orbeon-Username')`, etc.).
-->

## See also

- [Setup users for access control](users.md) - How to setup Orbeon Forms so that users and roles are provided.
- [Login & Logout](login-logout.md) - Optional user menu for providing links to login and logout functions.
- [Access control for deployed forms](deployed-forms.md) - How to control access to deployed forms.
- [Access control for editing forms](editing-forms.md) - How to control access to Form Builder.
    - [Owner and group member permissions](owner-group.md) - Access based on ownership and groups.
    - [Organization-based permissions](organization.md) – Access based on organizational structure.
- [Scenarios](scenarios.md)
