# Publishing

## Introduction

The notion of _publishing_ is central to Form Builder/Form Runner.

- As a form author, you first work on a form definition in a special space where the form can be modified, saved, and tested.
- Once the form definition is ready, you _publish it_ to Form Runner.
- After that moment:
    - the form becomes available by form users for data entry
    - Form Builder is no longer part of the equation

## First publish

When you start publishing a form with the Publish button at the bottom of Form Builder, a dialog opens to confirm the application name and form name:

<img alt="Creating a new version" src="images/publish-initial-no-versioning.png" width="502">

If you decide to go ahead with publishing, simply use the Publish button.

[SINCE Orbeon Forms 2017.2]

You can decide whether the published form is *available* to end users with the "Make published form available" option. You can later change the availability of the published form either in Form Builder by re-publishing it, or from the [Forms Admin page](/form-runner/feature/forms-admin-page.md#controlling-form-definitions-availability). By default, the form is made available.   

## Versioning

### New version or overwrite

When versioning is enabled, you have a choice, when publishing, of whether to *create a new form version* or to *overwrite an existing one*.

<img alt="Creating a new version" src="images/publish-version-next.png" width="501">

When creating a new form version, publishing the form definition creates a new version of the form definition in the database. Data which already exists in the database will still be viewed and edited with the previous versions of this form definition which are associated with that data. The new form definition version will be used to create, edit and view new data.

<img alt="Overwriting an existing version" src="images/publish-version-overwrite.png" width="503">

When overwriting an existing form definition, if the changes you have made to the form definition are small and non-structural, such as changing control labels only, the form definition will be compatible with existing form data. But if you have made structural changes, such as adding, removing, renaming, or moving controls, the form definition might be incompatible with existing data. We recommend being
careful when overwriting an existing form definition version.

See also [versioning](/form-runner/feature/versioning.md) for details about the implications of these options.

[SINCE Orbeon Forms 2016.1]

You can overwrite not only the latest published version, but any previous version.

### Versioning comments

[SINCE Orbeon Forms 2016.2]

You can add or update a textual comment associated with the given published version. Adding a comment is not required, but setting a comment can help the form author understand what changes a given form version includes.

When creating a new version, the field is initially empty:

<img alt="Empty comment" src="images/publish-comment-1.png" width="502">

You can set an explanatory comment:
 
<img alt="Empty comment" src="images/publish-comment-2.png" width="502">

When overwriting an existing form version, the existing comment, if any, is read back from the published form definition and you can update it before publishing:

<img alt="Empty comment" src="images/publish-comment-5.png" width="502">

## See also

- [Application name and form name](/form-runner/overview/terminology.md#application-name-and-form-name)
- [Versioning](/form-runner/feature/versioning.md)
- Blog posts:
    - [Form versioning](https://blog.orbeon.com/2014/02/form-versioning.html)
    - [Choosing the best versioning option when publishing a form](https://blog.orbeon.com/2015/01/choosing-best-versioning-option-when.html)
    - [Versioning comments](https://blog.orbeon.com/2016/09/versioning-comments.html)

