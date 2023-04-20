# Access control and permissions

In this section, we'll go over how you can secure Form Runner and Form Builder. For access control, Orbeon Forms leverages and delegates some work to external security infrastructure. In particular, you define users and their roles/group outside of Orbeon Forms.

Access control touches on the following:

- __Form definitions__ – Which users (in this case also known as *form authors*), can create, edit, or view form definitions.
- __Published forms__ – Which users can access a deployed form? If they can access the form, what operations (such as creating, viewing, editing, deleting) are allowed?
- __Form fields__ – If users can access the form, can she access a particular field or group of fields? If so, can the field be changed or just viewed?

The following pages address specific topics:

- [Setup users for access control](users.md) - How to setup Orbeon Forms so that users and roles are provided.
- [Login & Logout](login-logout.md) - Optional user menu for providing links to login and logout functions.
- [Access control for deployed forms](deployed-forms.md) - How to control access to deployed forms.
- [Form fields](form-fields.md) - How to control access to specific form fields based on the user user's roles.
- [Access control for editing forms](editing-forms.md) - How to control access to Form Builder.
- [Owner and group member permissions](owner-group.md) - Access based on ownership and groups.
- [Organization-based permissions](organization.md) – Access based on organizational structure.
- [Scenarios](scenarios.md)
- [Token-based permissions](tokens.md) - Token-based permissions