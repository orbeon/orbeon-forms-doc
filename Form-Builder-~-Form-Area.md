## The Form Builder WYSIWYG area

## Introduction

The Form Builder form area is meant to looks as much as possible like the published form (What You See is What You Get, AKA WYSIWYG).

Form Builder is built around as simple layout concept: _sections_ and _grids_. This is thought to be a good alternative to:

* absolute positioning, which is rarely appropriate for web forms
* complex layouts, which often confuse form authors

In the future, Form Builder might add extended layout options.

## In-place editing

Form Builder allows you to edit certain text information in-place. This just means that text appears as it would in the published form, and is editable when you click on it.

To edit such text:

* Click on the text (or placeholder text)
* An input field appears
* Enter the text
* Press the Apply button

![][50]

To indicate that the text is editable, it highlights when the mouse hovers over it.

![][51]

This mechanism is how you edit:

* Form title
* Form description
* Section title
* Field label
* Field hint

## Sections and grids

Form Builder represents every form as a series of _sections:_

* A section is simply a logical grouping of form controls. For example, your form may have an "address" section, and a "personal details" section.
* Sections are presented in order, on top of each other.
* Every form has at least one section.
* There is no maximum number of sections within a form.

You can perform the following operations on sections.

* Set the section's title by clicking on it. The title is localizable.
* Delete the section by clicking on the Trashcan icon. If the section contains controls, a confirmation dialog appears.
* Set an optional help message for the section by clicking on the help icon. The help is localizable.
* Set other section properties.
* Open or collapse the section by clicking on the arrow to the left of the section title.
* Move the section up or down by clicking on the up/down arrows. These appear as needed if there is more than one section.

Each section contains one or more _grids_:

* A grid is a logical grouping of form controls organized in rows and columns of cells.
* A grid has between 1 and 4 columns.
* A grid may have any number of rows.
* Each grid cell may contain a single form control, or remain empty.
* A grid cell might span multiple rows.
* Unlike a section, a grid does not have a title or properties.

When your mouse pointer hovers over a grid, the grid boundaries, cells and icons are highlighted.

The following screenshot shows two sections, "Section 1" and "Section 2". The first section contains a 3 columns by 2 rows grid.

![][52]

You can perform the following operations on grids:

* Add grid columns by clicking on the left and right arrows at the top of each column.
* Add grid rows by clicking on the up and down arrows on the left of each row.
* Delete a column by clicking on the trashcan icon at the top of a column.
* Delete a row by clicking on the trashcan icon on the left of a row.
* Delete the entire grid by clicking on the trashcan icon on the top left corner of the grid.

For delete operations, a warning dialog shows if controls will be deleted as a result.

![][53]

## Grid cells and controls

Each grid cell can contain a single form control, or no control at all. If a control is present, the following actions related to the control are possible:

* **Set control label:** click on the label (in-place edition)
    * A control's label appears on top of the control. It is intended to provide a descriptive label for  the form control.
    * Examples: "First Name", "Street", "Phone Number".
* **Set control help message:** click on the "question mark" icon to open the Edit Help dialog
* **Set control hint:** click on the hint (in-place edition)
    * A control's hint usually appears under the control. It is intended to provide a short indication to the form user of how to fill-out the form control.
    * Examples: "Your first name", "Date in mm/dd/yyyy format".
* **Set control default value:** simply enter text or select a value
* **Edit the control's items:** click on the "Edit Items" icon or test to open the Itemset Editor dialog (selection controls only)

When your mouse pointer hovers over a grid cell containing a control, some icons allowing for further actions appear:

* **Delete Control icon:** deletes the control.
    * This also removes information associated with the control, including validation properties and XML representation.
* **Control Settings icon:** opens the Control Settings dialog.
* **Validation Settings icon:** opens the Validation Settings dialog.
* **Expand icon:** expands the cell down if possible.
    * This allows the control to take two or more rows of space in the grid, for example for taller lists of radio buttons.
    * If the control takes more than one grid row, then a Collapse icon performs the opposite operation.
* **Switch Appearance icon:** Switch between selection control types (selection controls only)

![][54]