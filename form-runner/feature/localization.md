# Localization

<!-- toc -->

## Introduction

There are three distinct categories of languages to consider:

1. The languages of the forms you create.
2. The languages of the Form Builder user interface.
3. The languages of the Form Runner user interface.

This means that a form author could use Form Builder in the French language, create a form in Mandarin Chinese, and run it with the Form Runner's English user interface.

For more on how to localize your forms in Form Builder, see [Form Localization in Form Builder](../../form-builder/localization.md). For more on how to localize the Form Builder or Form Runner user interface, see [Localizing Orbeon Forms](../../contributors/localizing-orbeon-forms.html). In what follows, we'll discuss how Form Runner picks which language to use at runtime, and what languages are supported by Form Builder and Form Runner out-of-the-box.

## Language picked at runtime

Several factors can impact what language is being used by Form Runner. To determine what language to use, Form Runner goes does the current list and picks the first language it finds *and* in which your form is available:

1. If Orbeon Forms is used with Liferay, the language coming from Liferay. <!-- TODO: provide mode details -->
2. If present, the language specified by the `fr-language` request parameter.
3. If present, the language specified by the `fr-language` servlet session attribute.
4. If present, the language specified by the [`oxf.fr.default-language.*.*` property](../../configuration/properties/form-runner.md#default-language).
5. Finally, if everything else fails, English (`en`) is used.

## Form Builder and Form Runner user interface

- F: full support
- P: partial support
- N: no support

See also [[Localizing Orbeon Forms|Contributors-~-Localizing-Orbeon-Forms]] for information about how to localize Form Builder and Form runner in additional languages. Your contributions are welcome!

### As of Orbeon Forms 4.10

Language  | Calendar | Numberer | XBL components | Form Runner   | Form Builder
----------|:--------:|:--------:|:--------------:|:-------------:|:-----------:
English   | F        | F        | F              | F             | F
French    | F        | F        | F              | F             | F
Swedish   | F        | F        | F              | F             | F
Portuguese| F        | F        | F <sup>1</sup> | F<sup>1</sup> | F<sup>1</sup>
Italian   | F        | F        | F <sup>1</sup> | F<sup>1</sup> | F<sup>1</sup>
German    | F        | F        | F <sup>1</sup> | F<sup>1</sup> | F<sup>1</sup>
Spanish   | F        | F        | F <sup>1</sup> | P             | P
Finnish   | F        | F        | P              | F<sup>1</sup> | F<sup>1</sup>
Dutch     | F        | F        | F <sup>1</sup> | F<sup>1</sup> | N
Norwegian | F        | F        | P              | P             | P
Russian   | F        | F        | P              | P             | P
Polish    | N        | F        | N              | N             | N

1. A few resources are missing.

### As of Orbeon Forms 4.9

Language  | Calendar | Numberer | XBL components | Form Runner   | Form Builder
----------|:--------:|:--------:|:--------------:|:-------------:|:-----------:
English   | F        | F        | F              | F             | F
French    | F        | F        | F              | F             | F
Swedish   | F        | F        | F              | F             | F
Portuguese| F        | F        | F              | F<sup>1</sup> | F<sup>1</sup>
Italian   | F        | F        | F              | F<sup>1</sup> | F<sup>1</sup>
German    | F        | F        | F              | F<sup>1</sup> | F<sup>1</sup>
Spanish   | F        | F        | F              | P             | P
Finnish   | F        | F        | P              | F<sup>1</sup> | F<sup>1</sup>
Dutch     | F        | F        | F              | F             | N
Norwegian | F        | F        | P              | P             | P
Russian   | F        | F        | P              | P             | P
Polish    | N        | F        | N              | N             | N

1. A few resources are missing.

### As of Orbeon Forms 4.6.2, 4.7 and 4.8

Language  | Calendar | Numberer | XBL components | Form Runner   | Form Builder
----------|:--------:|:--------:|:--------------:|:-------------:|:-----------:
English   | F        | F        | F              | F             | F
French    | F        | F        | F              | F             | F
Portuguese| F        | F        | F              | F<sup>1</sup> | F<sup>1</sup>
Italian   | F        | F        | F              | F<sup>1</sup> | F<sup>1</sup>
German    | F        | F        | F              | F<sup>1</sup> | F<sup>1</sup>
Swedish   | F        | F        | F              | F<sup>1</sup> | F<sup>1</sup>
Spanish   | F        | F        | F              | P             | P
Finnish   | F        | F        | P              | F<sup>1</sup> | F<sup>1</sup>
Dutch     | F        | F        | F              | F             | N
Norwegian | F        | F        | P              | P             | P
Russian   | F        | F        | P              | P             | P
Polish    | N        | F        | N              | N             | N

1. In 4.8, a few resources related to the new "singleton form" feature are missing.

*NOTE: In 4.6.0, Spanish, Italian and German have a few missing localized resources. In 4.6.2 PE, Italian and German resources are complete again. We hope to have Spanish resources fully updated for 4.9. Please let us know if you would like to help!*

### As of Orbeon Forms 4.5

Language  | Calendar | Numberer | XBL components | Form Runner | Form Builder
----------|:--------:|:--------:|:--------------:|:-----------:|:-----------:
English   | F | F | F | F | F
French    | F | F | F | F | F
Spanish   | F | F | F | F | F
Swedish   | F | F | F | F | N
Italian   | F | F | F | P | P
German    | F | F | F | P | P
Finnish   | F | F | P | P | P
Norwegian | F | F | P | P | P
Russian   | F | F | P | P | P
Polish    | N | F | N | N | N

### As of Orbeon Forms 4.3

Language  | Calendar | Numberer | XBL components | Form Runner | Form Builder
----------|:--------:|:--------:|:--------------:|:-----------:|:-----------:
English   | F | F | F | F | F
French    | F | F | F | F | F
Italian   | F | F | F | F | F
German    | F | F | F | F | F
Spanish   | F | F | P | P | P
Finnish   | F | F | P | P | P
Norwegian | F | F | P | P | P
Russian   | F | F | P | P | P

### As of Orbeon Forms 4.2

Language  | Calendar | Numberer | XBL components | Form Runner | Form Builder
----------|:--------:|:--------:|:--------------:|:-----------:|:-----------:
English   | F | F | F | F | F
French    | F | F | F | F | F
Spanish   | F | F | P | P | P
Norwegian | F | F | P | P | P
Russian   | F | F | P | P | P

## Known limitations

Form Builder (Orbeon Forms PE only) lets you create forms in multiple languages. Most world languages are available, with some limitations described below.

1. PDF output: There are some  known issues with ligatures in some Indian languages such as Hindi or Tamil.
2. Right-to-left languages are not officially supported.

We are glad to get help to address these two limitations.
