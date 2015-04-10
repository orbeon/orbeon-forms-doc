## Rationale

This page explains some terms which occur often in the Orbeon Forms documentation.

## Form definitions and form data

We make a distinction between:  

* **Form definitions**: for example an "address" form vs. a "claim" form
* **Form data**: for example a given instance of the "address" form filled out by a particular user.

## Application name and form name

In Form Builder, a form definition is identified by a two-level hierarchy of names:

- **Application name**: a name which allows grouping form together. The application name (or app name) can be, for example:
  - a company name such as "orbeon" or "acme"
  - a company entity such as "hr" or "sales" or "engineering"
  - a project name such as "mercury", "foobar"
- **Form name**: a name which is local to an application name, for example "address" or "claim".

This two-level hierarchy allows for easy grouping of forms, and allows using a single instance of Form Builder to host distinct applications.

Form data is identified by a three-level hierarchy which includes:  

- the **application name/form name** couple that identifies the form definition
- plus a **unique form data id** or **document id** provided by Form Runner for each instance of form data