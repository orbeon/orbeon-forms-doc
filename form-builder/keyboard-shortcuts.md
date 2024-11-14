# Keyboard shortcuts

## Availability

[SINCE Orbeon Forms 2020.1]

The first keyboard shortcuts have been added with Orbeon Forms 2020.1. New shortcuts have been added since. See below for details.

## Rationale

When you start being comfortable with the Form Builder user interface, you might want to use keyboard shortcuts to speed up your work. Orbeon Forms is progressively introducing keyboard shortcuts to help you with this.

## Discoverability

[\[SINCE Orbeon Forms 2023.1.3\]](/release-notes/orbeon-forms-2023.1.3.md)

Most keyboard shortcut hints now show when you hover over the Form Builder toolbox, the buttons bar, or icons.

[\[SINCE Orbeon Forms 2023.1.6\]](/release-notes/orbeon-forms-2023.1.6.md)

If you find the hover hints distracting or want to disable them, you can set the following property, which is enabled by default.

```
<property
    as="xs:boolean"
    name="oxf.fr.keyboard-shortcuts.show-hints.*.*"
    value="false"/>
```

##  Buttons shortcuts

You can use the following keyboard shortcuts:

Apple operating systems:

- `⌘S` (Command-S): save the form definition (like the "Save" button)
- `⌘P`: open the [Publish dialog](/form-builder/publishing.md)

Other operating systems:

- `⌃S` (Ctrl-S): save the form definition (like the "Save" button)
- `⌃P`: open the [Publish dialog](/form-builder/publishing.md)

## Cut, copy and paste

You can use the following keyboard shortcuts:

Apple operating systems:

- `⌘X`: cut the current control
- `⌘C`: copy the current control
- `⌘V`: paste from the toolbox

Other operating systems:

- `⌃X`: cut the current control
- `⌃C`: copy the current control
- `⌃V`: paste from the toolbox

See also [Cut, copy and paste](/form-builder/cut-copy-paste.md).

## Undo and redo

You can use the following keyboard shortcuts:

Apple operating systems:

- `⌘Z`: undo the last operation
- `⌘⇧Z`: redo the last operation

Other operating systems:

- `⌃Z`: undo the last operation
- `⌃Y`: redo the last operation

See also [Undo and redo](/form-builder/undo-redo.md).

## Dialogs shortcuts

- `⇧↵` (Shift-Enter or Shift-Return): open the [Control Settings dialog](/form-builder/control-settings.md)
- `⌘J`/`⌃J`
    - [SINCE Orbeon Forms 2021.1]
    - Opens the [Quick control search](/form-builder/quick-control-search.md)

[\[SINCE Orbeon Forms 2023.1.3\]](/release-notes/orbeon-forms-2023.1.3.md)

- Opening settings dialogs:
    - `O F`: [open Form Settings](/form-builder/form-settings.md)
    - `O P`: [open Permissions](/form-runner/access-control/deployed-forms.md)
    - `O E`: [open Email Settings](/form-builder/email-settings.md)
    - `O M`: [open Messages](/form-builder/messages.md)
    - `O S`: [open Edit Source](/form-builder/edit-source.md)
- Opening test dialogs:
    - `T W`: test the web form
    - `T P`: [test PDF production](/form-builder/pdf-test.md)
    - `T O`: [test the offline form](/form-builder/offline-test.md)
    - `T F`: [inspect formulas](/form-builder/formulas-inspector.md)

## Grid navigation shortcuts

[SINCE Orbeon Forms 2020.1]

You can use the left and right cursor keys to navigate between grid cells.

## Inserting form structure

[\[SINCE Orbeon Forms 2023.1.3\]](/release-notes/orbeon-forms-2023.1.3.md)

- `I S S`: [insert a new section](/form-builder/toolbox.md)
- `I S G`: [insert a new grid](/form-builder/toolbox.md)
- `I S R`: [insert a new repeated grid](/form-builder/toolbox.md)

## Reloading the toolbox

[\[SINCE Orbeon Forms 2023.1.3\]](/release-notes/orbeon-forms-2023.1.3.md)

- `⌘⇧R`/`⌃⇧R`: [reload the toolbox](/form-builder/toolbox.md#reloading-the-toolbox)

## See also

- Blog post: [Adding keyboard shortcuts to Form Builder](https://www.orbeon.com/2021/01/adding-keyboard-shortcuts-to-form.html)
- Blog post: [Improved Keyboard Shortcuts](https://www.orbeon.com/2024/07/keyboard-shortcuts)
