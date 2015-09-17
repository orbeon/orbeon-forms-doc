> [[Home]] ▸ [[Form Builder|Form Builder]]

## Related pages

- [[Introduction|Form Builder ~ Introduction]]
- [[Summary Page|Form Builder ~ Summary Page]]
- [[The Form Editor|Form Builder ~ The Form Editor]]
- [[Toolbox|Form Builder ~ Toolbox]]
    - [[Metadata|Form Builder ~ Toolbox ~ Metadata]]
- [[Form Area|Form Builder ~ Form Area]]
- [[Validation|Form Builder ~ Validation]]
- [[Control Settings|Form Builder ~ Control Settings]]
- [[Section Settings|Form Builder ~ Section Settings]]
- [[Section Templates|Form Builder ~ Section Templates]]
- [[Creating Localized Forms|Form Builder ~ Creating Localized Forms]]
- [[Formulas|Form Builder ~ Formulas]]
- [[Itemset Editor|Form Builder ~ Itemset Editor]]
- [[Lifecycle of a Form|Form Builder ~ Lifecycle of a Form]]
- [[PDF Production|Form Builder ~ PDF Production]]
    - [[PDF Templates|Form Builder ~ PDF Production ~ PDF Templates]]

## Introduction

Form Builder supports grids with repeated rows. You can repeat a single row (which is the most common case), or multiple heterogeneous (that is with different controls) rows.

## Creating a repeated grid

You insert a new repeated grid with the "New Repeated Grid" toolbox button.

Once the grid is inserted, you can add and remove (using the grid arrow and trash icons which appear on mouseover) columns and rows, and add controls to grid cells as you would in a regular non-repeated grid.

With only one row, the control labels are used as column headers and are not repeated within the grid. If present, control hints are also added to the column headers. That single row is repeatable with the "plus" icon.

![Repeating a single row](images/fb-repeated-grid-single.png)

You can add multiple heterogeneous rows with the arrow icons. In this case the entire group of rows is repeatable. Control labels and hints do not appear in column headers, but appear alongside the controls in the grid.

![Repeating multiple rows](images/fb-repeated-grid-multiple.png)

Whether there is a single or multiple repeatable rows, you can add and remove repetitions (iterations) of those rows with the "plus" icon. You typically let the user add iterations at runtime, but it is possible to create iterations in advance at design time as well.

## How things look at runtime

At runtime, notice how in the first grid a single row is repeated, and in the second grid the two rows are repeated.

![Repeated grids](images/fr-repeated-grids.png)

In review mode and PDF mode, icons and menus disappear and the grid appears entirely readonly.

![Repeated grids in review mode](images/fr-repeated-grids-view.png)

![Repeated grids in PDF mode](images/fr-repeated-grids-pdf.png)

## Grid settings

Once a grid is inserted, you can edit its properties with the "Grid Settings" icon.

![Grid Settings](images/fb-repeated-grid-settings-icon.png)

The "Visibility" and "Read-Only" formulas control whether the entire grid (including it's headers if any) is visible at all or whether its content is entirely readonly. 

*NOTE: Since Orbeon Forms 4.8, these settings properly apply to the entire grid. Previously, the grid's repeat headers did not hide properly for example when the grid was hidden. See issue [#635](https://github.com/orbeon/orbeon-forms/issues/635).*

![Grid Settings](images/fb-repeated-grid-settings-basic.png)

The repeat settings control whether to use a custom iteration name (not recommended in most cases), and the minimum/maximum number of repeat iterations allowed.

![Grid Settings](images/fb-repeated-grid-settings-repeat.png)

## See also

- Support for repeats lands in Form Builder: [older blog post](http://blog.orbeon.com/2012/04/support-for-repeats-lands-in-form.html)
- Inserting and reordering grid rows: [blog post](http://blog.orbeon.com/2013/11/inserting-and-reordering-grid-rows.html)
- Repeated sections: [blog post](http://blog.orbeon.com/2014/01/repeated-sections.html)