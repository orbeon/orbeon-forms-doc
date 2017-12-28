# Section templates

<!-- toc -->

## Introduction

Form Builder supports defining reusable sections called section templates. Here is how they work:

- You create sections in a special form with name `library`.
- You publish this form.
- For each section in that form, Form Builder creates a reusable section component.
- The component is made available in the Form Builder toolbox under "Library" groups.
- The title of the section is used as the title of the component in the toolbox.

For example, you can create a generic "US Address" section and reuse it in multiple forms:

![Defining a section template in Form Builder](images/section-template.png)

Section templates can contain:

- nested grids, with or without repeats
- nested subsections, with or without repeats
- services and actions (see below)

When creating a form, the toolbox shows the available sections templates:

- Global templates, created in `orbeon/library` form.
- Application templates, created in the current application's `library` form.

When you click on a given section template, the section is inserted into the form after the currently selected section. You can then select a new title for the section. It is possible to include a section template more than once.

The section appears with read-only fields (which means that you cannot change properties of the controls once inserted):

![Using section templates in Form Builder](images/section-template-use.png)

When deploying the form, section templates appear like regular editable sections:

![Section templates in Form Runner](../form-runner/images/section-template.png)

## Services and actions

Actions involving controls in a given section are automatically included with the section template, along with the services called by the actions.

*NOTE: If an action involves controls in more than one section, the action will not properly work at runtime.*

## Updating section templates

When you open Form Builder, the latest version of the controls and section templates is retrieved from the database and shown in the toolbox.

If you make changes to section templates by modifying and publishing a library form, you must reload the toolbox in your form to reflect the latest changes using the "Reload Toolbox" icon at the top of the toolbox (Orbeon Forms 2017.1 and earlier) or the "Reload Toolbox" button in the "Advanced" tab (Orbeon Forms 2017.2).

<img src="images/advanced-menu.png" width="245">

For more about reloading, see [Reloading the toolbox](toolbox.md#reloading-the-toolbox)

Note that when you publish your form, the section templates *currently* loaded in Form Builder at the time of publishing are included with the published form. This means that changes to section templates after the deployment of a form do not affect the deployed form. If you need to update a deployed form with a new version of controls, you must re-publish the form.

## Merging section templates

[SINCE Orbeon Forms 2017.2]

You can merge section templates into your current form definition with the "Merge Section Template" icon associated with
the section:

![Unmerged section template](images/section-template-unmerged.png)

After activating the icon, a dialog shows:

![Merge dialog](images/section-template-merge-dialog.png)

The dialog shows a list of all control names within the section template and how they will be changed after the merge.
Since control names are unique within a form definition, two controls cannot have the same name. Names that are available
show in green. Names that conflict show in yellow and an automatic name is generated.

You can optionally set a prefix and/or a suffix for all names. This can help prevent automatic generation of names. For 
example, you could insert a US Address twice:

- first, with all control names prefixed by `shipping-` 
- second, with all control names prefixed by `billing-`

![Merge dialog](images/section-template-merge-dialog-prefix.png)

*NOTE: The enclosing section name itself is not part of this renaming process, as the section was already part of the
containing form definition and therefore its name was already unique.* 

After merging the section template, it becomes part of the current form definition and is completely unlinked from the
original section template. You can modify the section and its content as if it had been directly created by hand within
the form definition:

![Merged section template](images/section-template-merged.png)

This also means that if you make changes to the section templates library and reload the toolbox, the merged section template
does not update.   

## See also

- [Toolbox](toolbox.md)
- [HTTP services](http-services.md)
- [Database services](database-services.md)
- [Actions](actions.md)