

<!-- toc -->

## What it does

The `fr:section` component organizes [grids](grid.md) under a header or title. Features:

- collapsible section content
- optional repetition of its content
  - configurable min/max number of iterations
  - can repeat over several heterogeneous rows
  - built-in icons and menus to add, remove, and move repeated rows

## Basic usage

### Non-repeated mode

TODO

### Repeated mode

TODO

Attributes:

TODO:

- `appearance` [SINCE Orbeon Forms 4.11]
  - `full`
    - the default appearance, as with Orbeon Forms 4.10 and earlier
    - iteration menu
      - reordering of iterations
      - insertion of iterations at specific points
      - removing of specific iterations
  - `minimal`
    - does not show the "+" button at the top left
    - does not show the iteration menu and associated features
    - instead just provides "Add another" and "Remove" links at the bottom

## Data format

TODO

## Events

[SINCE Orbeon Forms 4.11]

The following events are dispatched to the `fr:section` element:

| Event name | Description |
| --- | --- |
| `fr-iteration-added` | Dispatched when the user has just added an iteration |
| `fr-iteration-removed` | Dispatched when the user has just removed an iteration |

These events are not dispatched if the number of iterations changes by other means, for examle if the data is replaced, or inserts/deletes happen outside of the component.

## See also

