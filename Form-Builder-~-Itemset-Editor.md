> [[Home]] ▸ [[Form Builder|Form Builder]]

The itemset editor allows you to enter items for selection controls.

Each item has:

* A localized _label_, which appears to the form user.
* A _value_, which will is stored in the database.

![][59]

At first, there is only one blank item available for the control. You add items using the "+" button, and remove them using the trashcan icons.

Usability notes:

* When in a label field, pressing the "tab" key into an empty value field automatically creates a default value. For example:
    * "Apple" becomes "apple"
    * "Wax Apple" becomes "wax-apple"
* When in a value field, pressing the "enter" key automatically adds a new item after the current item.

A value has constraints:

* For single selection controls (e.g. radio buttons), the value can be any string of characters.
* For multiple selection controls (e.g. checkboxes), the value must not contain spaces.

For convenience, the itemset editor allows you to switch between the list of available languages without quitting the editor.

Item values are not localizable: they remain the same for each language. On the other hand, item labels can be localized. For example:

* English
    * Name: "Apple"
    * Value: "apple"
* French
    * Name: "Pomme"
    * Value: apple

This ensures that the data captured is machine-readable even if the user interface language changes.