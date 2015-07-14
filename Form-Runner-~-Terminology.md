> [[Home]] ▸ [[Form Runner|Form Runner]]

## Rationale

This page explains some terms which occur often in the Orbeon Forms documentation.

## Form definitions and form data

We make a distinction between:  

* A **Form definition** is the description of a given form, including which sections, grids, and fields it contains, and all associated properties. You typically create a form definition using Form Builder. With Orbeon Forms, a form definition is encoded as an XML document using XHTML and XForms. Examples of form definitions include an "Address" form, or a "Claim" form.
* **Form data** is a set of data entered by a user via a form definition. For example, Hillary might have used the "Claim" form to file a claim. With Orbeon Forms, form data is encoded as an XML document.

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