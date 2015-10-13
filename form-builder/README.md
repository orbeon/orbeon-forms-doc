# Orbeon Form Builder

<!-- toc -->

## Introduction

Orbeon Form Builder is a visual form designer which allows you to build and deploy forms in minutes right from your web browser.

A few key Form Builder features:

* **100% web-based.** Form Builder does not require installing any software on the user's computer: all that is needed is a recent web browser such as Firefox, Safari, Google Chrome or Internet Explorer, without installing browser plugins or extensions.
* **Easy grid-based layout.** Form Builder uses an easy to understand layout based on sections and grids, onto which you place your form elements. You concentrate on the data you want to capture, not the pesky details.
* **Rich data validation and controls.** Form Builder supports common datatypes for validation and user interface controls, as well as attachments and pictures. It also lets you import your own XML Schema and use the imported types.
* **Easy HTML and PDF output.** The forms you build with Form Builder automatically produce nice-looking HTML and PDF output.
* **Full internationalization.** With Form Builder, any form is easily designed in multiple languages, including labels, help, and error messages.
* **Expert mode with XForms.** For experts, Form Builder lets your form talk to the outside world with its built-in Web Services editor, and advanced form authors can provide their own XForms markup.
* **Accessible forms.** Forms produce use either Ajax or a more accessible mode without script.
* **Built-in runtime environment.** With a single click, your form is deployed into the Form Runner runtime environment and users can start filling-out data.
* **Services and actions.** Build simple services and actions right from the editor without writing code.

## Prerequisite knowledge

Form Builder can be used without much technical knowledge to build a vast range of forms. Some of the advanced features do require technical knowledge, but you don't necessarily have to use them, and if you do you can delegate the task as Form Builder lends itself to team work.

## Lifecycle of a form

Deployment use cases for Form Builder and Form Runner can vary depending on configuration, but here is a typical life for a form:

* **Design time** — The form author
    * Initiates the creation of a new form definition from the Form Builder summary page
    * Edits the form definition from the Form Builder editor
    * Saves the form definition
    * Tests the form definition
    * Multiple edit/save/test cycles can take place
    * Publishes the form definition

* **Runtime** — The form user
    * Initiates the creation of new form data from the form's summary page
    * Enters data into the form
    * Reviews, saves, submits, or downloads form data

Form definitions, as well as form data, can also be searched and deleted.

## Terminology

A few useful terms used in this document:

* **Form control.** A form control is a form user interface element such as a text line, text field, group of radio buttons, email or currency field, etc.
* **Form definition.** A form definition (often simply called a form) includes a set of form controls, a layout of these controls on the screen, a representation of the data to capture (e.g. an XML document format), and optionally events and actions defining behavior when the user interacts with the controls, as well as rules that can apply to the data.
* **Form data.** Form data is data that is captured or edited by a form definition.
* **Form author.** The form author is the person creating a form definition (either using a visual tool, or by writing code).
* **Data entry.** Act of entering data into a form.
* **Form user.** As opposed to the form _author_, the form _user_ is the person entering data into a form.
* **Form Builder.** The software, described in this document, used by the form author to create a form definition.
* **Form Runner.** The Orbeon Forms runtime environment, which takes care of presenting to an end user the form definitions created by the form author with Form Builder.
* **Design time.** The time during which the form is edited in Form Builder.
* **Runtime.** The time during which the form is executed by Form Runner, and where end users can enter data.

## Software requirements

On the client, Form Builder requires a modern web browser. For specifics, see the release notes of the Orbeon Forms release you are using.

The faster the browser and the computer, the better your authoring experience will be.