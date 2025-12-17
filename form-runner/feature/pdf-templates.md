# PDF templates

## How PDF templates work

The purpose of this feature is to allow using existing PDF forms and using Form Runner to fill them out. This has the benefit that your organization can reuse existing forms, which might have been designed carefully for printing or archival purposes.

A PDF template must include Acrobat form fields. You can create these with Acrobat Professional. At runtime, when producing a filled-out PDF, Form Runner performs the following operations:

* read the PDF template
* read the filled-out form data
* fill out the fields in the PDF template based on the data

![Example of filled PDF template](../../.gitbook/assets/pdf-dmv14-shadow.png)

## Attaching a PDF template

You attach one or more PDF templates to a form definition in Form Builder using the "PDF Templates" button in the "Advanced" tab of the toolbox.

<figure><img src="../../.gitbook/assets/pdf-templates-dialog.png" alt="" width="702"><figcaption><p>PDF Templates Dialog</p></figcaption></figure>

\[SINCE Orbeon Forms 2018.1]

You can attach more than one PDF template. Templates are differentiated by name and/or language. It is not possible to have duplicate name/language combinations. The idea is that you can create separate PDF templates:

* per language
* and/or per a given name, selected when creating the PDF

The rules that govern which of the available PDF templates is selected when sending a PDF document are documented [here](../advanced/buttons-and-processes/actions-form-runner-send.md).

## Creating a PDF template for use with Form Builder

### Steps

In order to create a template, you need Acrobat Professional or a similar tool to edit PDF fields. Here we assume Acrobat Professional.

1. Open an existing PDF document
2. Perform automatic field detection
3. Remove incorrectly detected areas
4. Set all field names, as per the `my-section$my-field` explained below

_NOTE: For languages like French, make sure that fields in the form have an Arial font, not Helvetica. Not using Arial can cause issue with accents._

![Example of form fields in Acrobat Professional](../../.gitbook/assets/pdf-template.png)

### Naming fields

Form Runner identifies the PDF fields to fill-out based on their name. Those names must follow the following convention:

* Let's assume you the name of a field to `my-field` and the name of the section in which that field is to `my-section`.
* The name of the PDF field must be: `my-section$my-field`.

\[LIMITATION] It has been [reported](http://discuss.orbeon.com/Creating-a-PDF-template-for-use-with-Form-Builder-td931856.html) that Adobe LiveCycle Designer does not support the `$` character in field names.

\[SINCE Orbeon Forms 2018.2]

The PDF field name is shown in the ["Control Settings"](../../form-builder/control-settings.md) dialog under "Name of the PDF field".

In the case of repeated fields, note that the name shows an example _suffix_ containing the repeat iterations, such as `$1` or `$1-1`. To target individual iterations, use the appropriate repetition number, for example:

* `section-1$location$1`
* `section-1$location$2`

or, for a control in a nested repeated section or grid:

* `section-1$location$1-1`
* `section-1$location$1-2`
* `section-1$location$1-3`
* `section-1$location$2-1`
* `section-1$location$2-2`

etc.

![Name of the PDF field in the Control Settings dialog](../../.gitbook/assets/control-settings.png)

[\[SINCE Orbeon Forms 2024.1.2\]](../../release-notes/orbeon-forms-2024.1.2.md)

Orbeon Forms allows you to set a custom PDF field name. If there is at least one PDF template attached to the form, you can pick a field name from the list.

<figure><img src="../../.gitbook/assets/control-settings-pdf-field-name-from-list.png" alt="" width="481"><figcaption><p>Custom PDF field name from the dropdown</p></figcaption></figure>

In any case, you can also enter you own custom field name.

<figure><img src="../../.gitbook/assets/control-settings-pdf-field-name-from-field.png" alt="" width="481"><figcaption><p>Custom PDF field name</p></figcaption></figure>

## Controls

### Multi-line text

* Enable multi-line in Acrobat form field
* Set a white background if necessary to hide dotted lines from the original form

### Exclusive checkboxes or radio buttons

In the PDF, create multiple fields of type radio button (which can visually appear like checkboxes or radio buttons - the defining feature is that they are exclusive), all with the same name: `my-section$my-field`, but each with an _Export value_ matching the corresponding value in your form. In summary:

* Use PDF radio buttons with the desired appearance.
* Use the _same name_ for all controls.
* Use _different export values_ for each control.
* The checkbox value (and _not_ the label) is used by Form Runner to match on the export value.

### Non-exclusive checkboxes

* Create multiple PDF checkboxes.
* Each checkbox must have a different name of the form `my-section$my-field$value`, where `value` corresponds to the item value in your form.
* Set an _export value_ for all the checkboxes of `true`.

### Boolean checkboxes

For a boolean checkbox:

* Create a single non-exclusive PDF checkbox.
* Name it `my-section$my-field` or `my-section$my-field$true`.
* Set an _export value_ of `true`.

### Image attachments

To place an image attachment on your PDF, simply add a placeholder Acrobat form field.

* This field will be used by the PDF engine to determine the image location and dimensions.
* Name the field using the `my-section$my-field` format, where `my-field` is the name of the image attachment field in Form Builder.

When the resulting PDF is generated by Form Runner, the field will not contain text, but the image attachment will be placed within the space defined by the field.

### Repeats

When a control is within a repeat:

* the identifier of the repeat must be included in the acrobat field name
* the repeat iteration must be appended to the acrobat field name

So for example, if you have the hierarchy:

* `my-section`
* `my-repeat`
* `my-input`

The field names must look like:

* `my-section$my-repeat$my-input$1` for the first iteration
* `my-section$my-repeat$my-input$2` for the second iteration
* etc.

When using a PDF template, Orbeon Forms can only fill out fields that are already present in the PDF template. It cannot add new iterations. This means that the maximum number of allowed iterations in the form must match the number of iterations present in the PDF template.

![Example of repeated fields](images/pdf-template-repeat.png)

## Font configuration and embedding

In template mode, fonts can be specified to provide glyphs which are not present in the PDF's original font(s). Several fonts can be specified, separated by spaces:

```xml
<property
    as="xs:string"
    name="oxf.fr.pdf.template.font.paths"
    value="/path/to/font1.ttf /path/to/font2.ttf"/>
```

## PDF template selection

\[SINCE Orbeon Forms 2018.1]

By default, the `pdf` button runs the following process:

```xml
<property as="xs:string"  name="oxf.fr.detail.process.pdf.*.*">
    open-rendered-format(format = "pdf")
</property>
```

This will, by default, use a PDF template if there is one, otherwise use the automatic PDF. If there are multiple PDF templates, then matching by form language takes place.

You can, in your `properties-local.xml`, specify your own process, and, using the `use-pdf-template`, `pdf-template-name`, and `pdf-template-lang` action parameters, be specific about which template you'd like to use, if any. For more about these parameters and the `open-rendered-format` action, see [the `open-rendered-format` action](../advanced/buttons-and-processes/actions-form-runner.md#open-rendered-format).

## See also

* [PDF Production](pdf-production.md)
* [Automatic PDF](pdf-automatic.md)
* [PDF configuration properties](../../configuration/properties/form-runner-pdf.md)
* [The `open-rendered-format` action](../advanced/buttons-and-processes/actions-form-runner.md#open-rendered-format)
* [Testing PDF production](../../form-builder/pdf-test.md)
* [TIFF Production](tiff-production.md)
* [Sending PDF and TIFF content: Controlling the format](../advanced/buttons-and-processes/actions-form-runner-send.md)
* Blog post: [New layout choices for PDF and browser views](https://blog.orbeon.com/2019/11/new-layout-choices-for-pdf-and-browser.html)
* Blog post: [PDF Production](https://www.orbeon.com/2025/02/pdf-production)
