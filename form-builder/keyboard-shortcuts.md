# Form Builder keyboard shortcuts

## Availability

[SINCE Orbeon Forms 2020.1]

The first keyboard shortcuts have been added with Orbeon Forms 2020.1. New shortcuts have been added since. See below for details.

## Rationale

When you start being comfortable with the Form Builder user interface, you might want to use keyboard shortcuts to speed up your work. Orbeon Forms is progressively introducing keyboard shortcuts to help you with this.

## Discoverability

[\[SINCE Orbeon Forms 2023.1.3\]](/release-notes/orbeon-forms-2023.1.3.md)

Most keyboard shortcut hints now show when you hover over the Form Builder toolbox, the buttons bar, or icons.

[\[SINCE Orbeon Forms 2023.1.6\]](/release-notes/orbeon-forms-2023.1.6.md)

If you want to disable hover hints because you find them distracting or for any other reason, set the following property to `false`. Its default value is `true`, meaning hints are shown by default for all forms.

```xml
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

The grid navigation shortcuts use the keyboard cursor (arrow) keys.

| Key       | Description                                                               | Since                                                                |
|-----------|---------------------------------------------------------------------------|----------------------------------------------------------------------|
| `←`/`→`   | Move to the previous or next grid cell, including empty cells.            | [\[Orbeon Forms 2020.1\]](/release-notes/orbeon-forms-2020.1.md)     |
| `⇧←`/`⇧→` | Move to the previous or next grid cell, skipping empty cells.             | [\[Orbeon Forms 2020.1\]](/release-notes/orbeon-forms-2020.1.md)     |
| `↑`/`↓`   | Move to the previous or next grid cell vertically, including empty cells. | [\[Orbeon Forms 2024.1.1\]](/release-notes/orbeon-forms-2024.1.1.md) |

## Inserting form structure

[\[SINCE Orbeon Forms 2023.1.3\]](/release-notes/orbeon-forms-2023.1.3.md)

The mnemonics for these shortcuts are:

- `I`: insert
- `F`: form structure (for a while, this was `S`)

Available shortcuts

- `I F S`: [insert a new section](/form-builder/toolbox.md)
- `I F G`: [insert a new grid](/form-builder/toolbox.md)
- `I F R`: [insert a new repeated grid](/form-builder/toolbox.md)

## Inserting form controls

[\[SINCE Orbeon Forms 2024.1\]](/release-notes/orbeon-forms-2024.1.md)

You can quickly insert form controls into a form at or after the current cell position. The mnemonics for these shortcuts are:

- `I`: insert
- category of control
    - `C`: control
    - `U`: utility controls
    - `T`: typed controls
    - `S`: selection controls
    - `A`: attachment controls

Available shortcuts:

- `I C I`: Text Field
- `I C T`: Text Area
- `I C F`: Formatted Text Area
- `I U E`: Explanatory Text
- `I U C`: Calculated Value
- `I U H`: Hidden Field
- `I T N`: Number
- `I T E`: Email Field
- `I T D`: Date Field
- `I T T`: Time Field
- `I S D`: Dropdown
- `I S R`: Radio Buttons
- `I S C`: Checkboxes
- `I A F`: Attachment
- `I A I`: Image Attachment
- `I A V`: Video Attachment

## Reloading the toolbox

[\[SINCE Orbeon Forms 2023.1.3\]](/release-notes/orbeon-forms-2023.1.3.md)

- `⌘⇧R`/`⌃⇧R`: [reload the toolbox](/form-builder/toolbox.md#reloading-the-toolbox)

## See also

- Blog post: [Adding keyboard shortcuts to Form Builder](https://www.orbeon.com/2021/01/adding-keyboard-shortcuts-to-form.html)
- Blog post: [Improved Keyboard Shortcuts](https://www.orbeon.com/2024/07/keyboard-shortcuts)
