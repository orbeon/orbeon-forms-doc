# Terminology

## Rationale

This page explains some terms which occur often in the Orbeon Forms documentation.

## Form controls

A form "control" is an interactive user interface element that allows the user to enter or view data. Examples include text fields, dropdown menus, radio buttons, and more.

With Orbeon Forms, not all form controls are called "fields", and we reserve that latter word for form controls where the user enters text, usually on a single line.

## Form definitions and form data

We make a distinction between:

* A __Form definition__ is the description of a given form, including which sections, grids, and fields it contains, and all associated properties. You typically create a form definition using Form Builder. With Orbeon Forms, a form definition is encoded as an XML document using XHTML and XForms. Examples of form definitions include an "Address" form, or a "Claim" form.
* __Form data__ is a set of data entered by a user via a form definition. For example, Hillary might have used the "Claim" form to file a claim. With Orbeon Forms, form data is encoded as an XML document.

## Application name and form name

In Form Builder, a form definition is identified by a two-level hierarchy of names:

- __Application name__: a name which allows grouping form together. The application name (or app name) can be, for example:
  - a company name such as "orbeon" or "acme"
  - a company entity such as "hr" or "sales" or "engineering"
  - a project name such as "mercury" or "phoenix"
- __Form name__: a name which is local to an application name, for example "address" or "claim".

This two-level hierarchy allows for easy grouping of forms, and allows using a single instance of Form Builder to host distinct applications. See [Multitenancy](../feature/multitenancy.md).

Application names and form names don't have to be human-readable, although it is generally convenient to make them human-readable.

Form data is identified by a three-level hierarchy which includes:

- the __application name/form name__ couple that identifies the form definition
- plus a __unique form data id__ or __document id__ provided by Form Runner for each instance of form data

## Summary page, Detail page and Home page

Form Runner and Form Builder have a few pages (or screens) which are referred to as follows:

- Form Builder
  - __Summary page__:
    - This page is for *form authors*.
    - It lists and allows searching the *form definitions* created with Form Builder. These might not have been *published* yet.
    - It is also the starting point for the Form Builder Detail page (see below).
  - __Detail page__:
    - This page is for *form authors*.
    - It is the actual Form Builder editor, which allows you to create, edit and publish form definitions.
- Form Runner
  - __Summary page__:
    - This page is usually for *end users*.
    - It lists and allows searching *form data* for a given form definition.
    - It is also the starting point for the Form Runner Detail page (see below).
  - __Detail page__:
    - This page is usually for *end users*.
    - This page allows creating, editing, viewing, saving, and submitting *form data*.
  - __Published Forms page__:
    - This page shows *published form definitions*.
    - This page is useful for *end users* to see which *form definitions* they have access to.
    - It can be a starting point to create new form data or to access the Form Runner Summary page.
    - See [Published Forms page](/form-runner/feature/published-forms-page.md) for more.
- __Forms Admin page__:
    - This page is useful for *admins*, who can set form availability, move forms between servers, and more.
    - See [Forms Admin page](/form-runner/feature/forms-admin-page.md) for more.

See also [The Form Builder Summary page and Form Runner home page](https://blog.orbeon.com/2014/06/the-form-builder-summary-page-and-form.html).

## See also 

- [Multitenancy](/form-runner/feature/multitenancy.md)

