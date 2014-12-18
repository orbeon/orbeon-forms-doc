> [[Home]] ▸ Form Runner ▸ [[Access Control |Form Runner ~ Access Control]]

## Overview

You can control access to specific form fields based on the user user's roles.

## Using and accessing roles

The [`$fr-roles`][1] XPath variable can be used in formulas controlling whether a field or section is visible or readonly. See the [Form Builder control validation dialog][2].

You can make a control non-visible to the current user by defining a _visibility_ expression that returns `false()`. If the control is visible, you can make it readonly to current user by defining a _readonly_ expression that returns `true()`.

## Examples

TODO

[1]: http://wiki.orbeon.com/forms/doc/user-guide/form-builder-user-guide/xpath-expressions#TOC-Scenario:-checking-the-role-s-of-the-current-user
[2]: http://wiki.orbeon.com/forms/doc/user-guide/form-builder-user-guide/control-validation-dialog