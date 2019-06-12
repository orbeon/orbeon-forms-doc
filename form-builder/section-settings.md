# Section settings

## Settings shared with grids

See [Section and grid settings](container-settings.md).

## Basic settings

### Overview

![](images/section-settings.png)

### Section collapsing

\[SINCE Orbeon Forms 2016.1\]

The following only applies when the form doesn't use the [Wizard view](../form-runner/feature/wizard-view.md).

A section can be open/expanded or closed/collapsed. By default, sections are open when the form loads, unless the "Initially open" checkbox is *deselected*.

The "Collapsible" radio buttons control whether the user can collapse and expand sections:

- __Use property:__ use  the configuration specified with the `oxf.xforms.xbl.fr.section.collapsible` property
- __Always:__ the section is collapsible no matter what the `oxf.xforms.xbl.fr.section.collapsible` property specifies
- __Never:__ the section is not collapsible no matter no matter what the `oxf.xforms.xbl.fr.section.collapsible` property specifies

## Repeat settings

See [Repeat settings](repeat-settings.md).

## Label and help message

The label can be configured directly in the form area, by clicking on the section title, or in the "Label" tab.

The help message is configured in the "Help Message" tab. Similarly, the help message can be in plain text or use rich text (HTML). 

### Dynamic label and help message

[SINCE Orbeon Forms 2018.1]

Sections support *dynamic* labels and help messages. This means that, instead of being specified once and for all at form design time, labels and help messages can incorporate dynamic parts such as control values and other custom expressions.

For more, see [Template syntax](template-syntax.md).

![](images/section-settings-label.png)

![](images/section-settings-help.png)

## See also

- [Section and grid settings](container-settings.md)
- [Repeat settings](repeat-settings.md)
- [Repeated grids](repeated-grids.md)
- [Formulas](formulas.md)
- [Section component](/form-runner/component/section.md)
- [Template syntax](template-syntax.md)
- [Wizard view](/form-runner/feature/wizard-view.md)
