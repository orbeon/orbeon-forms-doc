

<!-- toc -->

## What it does

The `fr:section` component organizes [grids](grid.md) under a header or title. Features:

- configurable number of rows and columns
- optional repetition of its content
  - configurable min/max number of iterations
  - can repeat over several heterogeneous rows
  - built-in icons and menus to add, remove, and move repeated rows
  - supports `relevant` and `readonly` MIPs [SINCE Orbeon Forms 4.8]

## Basic usage

Attributes:

TODO:

- `appearance` [SINCE Orbeon Forms 4.11]
  - `full`
    - the default appearance, as with Orbeon Forms 4.10 and earlier
    - row menu
      - reordering of rows
      - insertion of rows at specific points
      - removing of specific rows
  - `minimal`
    - does not show the "+" button at the top left
    - does not show the row menu and associated features
    - instead just provides "Add another" and "Remove" links at the bottom