> [[Home]] ▸ [[Form Builder|Form Builder]]

## Related pages

- [[Introduction|Form Builder ~ Introduction]]
- [[Summary Page|Form Builder ~ Summary Page]]
- [[The Form Editor|Form Builder ~ The Form Editor]]
- [[Toolbox|Form Builder ~ Toolbox]]
    - [[Repeated Grids|Form Builder ~ Repeated Grids]]
    - [[Metadata|Form Builder ~ Toolbox ~ Metadata]]
- [[Form Area|Form Builder ~ Form Area]]
- [[Validation|Form Builder ~ Validation]]
- [[Control Settings|Form Builder ~ Control Settings]]
- [[Section Settings|Form Builder ~ Section Settings]]
- [[Section Templates|Form Builder ~ Section Templates]]
- [[Formulas|Form Builder ~ Formulas]]
- [[Itemset Editor|Form Builder ~ Itemset Editor]]
- [[Lifecycle of a Form|Form Builder ~ Lifecycle of a Form]]
- [[PDF Production|Form Builder ~ PDF Production]]
    - [[PDF Templates|Form Builder ~ PDF Production ~ PDF Templates]]

## Availability

This is an [Orbeon Forms PE][60] feature.

## Working with localization

Form Builder has localization support. This means that your form's titles, labels, help messages, etc. can be specified in multiple languages. At runtime, the form user is presented with a default language and can switch the most appropriate language.

By default, only one language is present, typically English. The default language is configurable by the Form Builder system administrator.

![][61]

By pressing the "+" icon, a dropdown dialog shows.

![][62]

The dropdown list allows you to pick a new language to add to the list of languages of the form.

![][63]

When a new language is added:

* It appears in the list of languages at the top right corner of the WYSIWYG area
* All the resources of the previously selected language are copied into the new language

You switch between languages by clicking on the language to select:

* The currently selected language is recognizable because it does not appear like an underlined link.
* All localizable resources edits impact the currently selected language.

![][64]

You can remove the currently selected language by pressing the "-" icon. This will remove all the resources associated with that language, so you must be careful before proceeding. A warning dialog will appear before the deletion is completed.

Here is how you typically proceed to create a form in two languages:

* Create the form in the primary language and add all localizable resources such as labels, help messages, hints, etc.
* Add the secondary language.
* Translate all localizable resources now visible on the form.
* When testing the form, you can switch between the two languages to make sure no resource was missed.