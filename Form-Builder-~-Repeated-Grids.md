> [[Home]] ▸ Form Builder

## Introduction

Form Builder supports grids with repeated rows. You can repeat a single row (which is the most common case), or multiple rows.

You insert a new repeated grid with the "New Repeated Grid" toolbox button.

Once a grid is inserted, you can edit its properties with the "Grid Settings" icon.

![Grid Settings](images/fb-repeated-grid-settings-icon.png)

## See also

- [Inserting and reordering grid rows](http://blog.orbeon.com/2013/11/inserting-and-reordering-grid-rows.html)
- [Repeated sections](http://blog.orbeon.com/2014/01/repeated-sections.html)

## Grid settings

The "Visibility" and "Read-Only" formulas control whether the entire grid (including it's headers if any) is visible at all or whether its content is entirely readonly. 

*NOTE: Since Orbeon Forms 4.8, these settings properly apply to the entire grid. Previously, the grid's repeat headers did not hide properly for example when the grid was hidden. See issue [#635](https://github.com/orbeon/orbeon-forms/issues/635).*

![Grid Settings](images/fb-repeated-grid-settings-basic.png)

The repeat settings control whether to use a custom iteration name (not recommended in most cases), and the minimum/maximum number of repeat iterations allowed.

![Grid Settings](images/fb-repeated-grid-settings-repeat.png)