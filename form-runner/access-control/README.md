# Access Control

In this section, we'll go over how you can secure Form Runner and Form Builder. For access control, Orbeon Forms leverages and delegates some work to external security infrastructure. In particular, you define users and their roles/group outside of Orbeon Forms.

Access control touches on the following:

- __Form definitions__ – Which users (in this case also known as *form authors*), can create, edit, or view form definitions.
- __Published forms__ – Which users can access a deployed form? If they can access the form, what operations (such as creating, viewing, editing, deleting) are allowed?
- __Form fields__ – If users can access the form, can she access a particular field or group of fields? If so, can the field be changed or just viewed?

The following pages address specific topics:

- [Setup](setup.md) - How to setup Orbeon Forms so that users and roles are provided.
- [Editing Forms](editing-forms.md) - How to control access to Form Builder.
- [Deployed Forms](deployed-forms.md) - How to control access to deployed forms.
  - [Owner and Group Member permissions](owner-group.md) - How to control access based on ownership and groups.
- [Form Fields](form-fields.md) - How to control access to specific form fields, grids or sections.
