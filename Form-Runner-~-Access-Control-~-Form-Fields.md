> [[Home]] ▸ Form Runner ▸ [[Access Control |Form Runner ~ Access Control]]

## Overview

You can control access to specific form fields based on the user user's roles.

## Using and accessing roles

The [`$fr-roles`][1] XPath variable can be used in formulas controlling whether a field or section is visible or readonly. `$fr-roles` contains the list (as an XPath sequence) of roles of the current user, if any. Each role is represented as a string.

You can make a control non-visible to the current user by defining a _visibility_ expression that returns `false()`. If the control is visible, you can make it readonly to current user by defining a _readonly_ expression that returns `true()`.

## Examples

The following "Visibility" expression makes a section visible only if one of the roles has value `admin`:

``` ruby
$fr-roles = 'admin'
```

Due to the logic of XPath comparison on sequences, this expression returns `true()` if at least one of the roles is `admin`, even if there are other roles available.

TODO: more examples.

[1]: http://wiki.orbeon.com/forms/doc/user-guide/form-builder-user-guide/xpath-expressions#TOC-Scenario:-checking-the-role-s-of-the-current-user
[2]: http://wiki.orbeon.com/forms/doc/user-guide/form-builder-user-guide/control-validation-dialog