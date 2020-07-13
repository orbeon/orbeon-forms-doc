# Undo and redo

## Availability

[SINCE Orbeon Forms 2017.2]

## Undo and redo icons

The toolbox shows familiar undo and redo icons.

<img alt="Undo and redo icons" src="images/xcv.png" width="210">

## Using undo and redo

When hovering over the icons, a tooltip tells you which operation is undone or redone.

You can undo most operations which change important aspects of the form:

## Keyboard shortcuts

[SINCE Orbeon Forms 2020.1]

You can use the following keyboard shortcuts:

macOS:

- `⌘-Z`: undo the last operation
- `⌘-⇧-Z`: redo the last operation

All operating systems:

- `⌃-Z`: undo the last operation
- `⌃-Y`: redo the last operation

## Supported undo/redo actions

- insertion and deletion
    - control, grid, grid row, section and section template 
- moving
    - control (via drag and drop)
    - section (up/down/right/left arrows)
- settings
    - rename control, grid or section
    - other control, grid or section settings, including itemsets, labels, hint, and validations
- section template merging (new in 2017.2)

## Limitations

In this initial version, it is not yet possible to undo changes to the form definition's source code with Edit Source, or changes to actions, services, and other global form settings. We hope to support undoing these operations in the future.

## See also 

- Blog post: [New Orbeon Forms 2017.2 feature: undo and redo](https://blog.orbeon.com/2017/12/new-orbeon-forms-20172-feature-undo-and.html)
