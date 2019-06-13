# Repeat settings

## Overview

Form Builder supports grids and sections with repeated content.

- With grids, you can repeat a single row (which is the most common case), or multiple heterogeneous rows (with different controls).
- With sections, you can repeat the entire content of the section a number of times.

Both repeated grids and repeated sections have *repeat settings* in the "Section/Grids Settings" dialog's "Repeated Content" tab.

![Repeated Content](images/repeat-settings.png)

## Repeat Content

Select this checkbox to enable repeated content.

## Minimum and maximum number of iterations

These settings can be predefined numbers or formulas when selecting "Other".

## Freeze repetitions

[SINCE Orbeon Forms 2018.2]

This setting can be a predefined number or a formula.

This allows *freezing* the first *N* iterations of a repeated grid or repeated section. Frozen iterations cannot be removed or moved by the user. The grid menus and buttons reflect that those operations are not possible.

The number of frozen iterations must be at most the minimal number of iterations. If that's not the case, the number of frozen iterations will be reset to the minimal number of iterations.

See also [Freezing rows in repeated grids or sections](https://blog.orbeon.com/2019/06/freezing-rows-in-repeated-grids-or.html).

## Simplified appearance

[SINCE Orbeon Forms 2019.1]

When selected, this enables a simplified appearance for a repeated grid or section.

The default is the "full" appearance, which features:

- visible borders
- menus for inserting, removing, and moving rows

When enabled, the simplified (or "minimal") appearance features:

- no visible borders
- no ability to insert a new row at a specific position
- no ability to reorder rows
- simple buttons/icons to add/remove rows

Before Orbeon Forms 2019.1, the simplified appearance was available but only controllable via a property. See [Appearance of repeated grids](/configuration/properties/form-runner.md#appearance-of-repeated-grids) and [Appearance of repeated sections](/configuration/properties/form-runner.md#appearance-of-repeated-sections).

## Initial value formulas

[SINCE Orbeon Forms 2016.1]

The "Apply initial value formulas when adding iterations" option specifies whether the "Initial Value" formulas for controls within the grid are evaluated for new iterations.

With the option enabled, new iterations can have dynamic initial values:

![Initial Values](images/iterations-initial-values.png)

With Orbeon Forms 2016.1, the option is enabled by default for new forms and new repeated grids. The option is disabled by default for grids created with previous versions of Orbeon Forms.

## Initial number of iterations uses template

[SINCE Orbeon Forms 2016.1]

The "Initial Number of Iterations Uses Template" option specifies, when an *enclosing repeated section* creates a new iteration, how many iterations this repeated grid will contains:

- when enabled: the number of iterations shown in Form Builder (which can be no iterations at all, one iteration, two iterations, etc.)
- when disabled: exactly one iteration

The following screenshot shows a case with a repeated grid within nested repeated sections. At first, when the form shows, there are two iterations of the repeated grid.

![](images/iterations-initial.png)

With the option enabled on the grid, adding a new iteration of _Repeated section 2_ causes the new iteration to contain a new repeated grid with two iterations:

![](images/iterations-template.png)

While, with the option disabled on the grid, adding a new iteration of _Repeated section 2_ causes the new iteration to contain a new repeated grid a single iterations:

![](images/iterations-single.png)

<!--

Example:

![Initial Iterations](images/)
-->

## See also

- [Repeated grids](repeated-grids.md)
- [Section settings](section-settings.md)
- [Grid component](/form-runner/component/grid.md)
- [Section component](/form-runner/component/section.md)
- Support for repeats lands in Form Builder: [older blog post](https://blog.orbeon.com/2012/04/support-for-repeats-lands-in-form.html)
- Inserting and reordering grid rows: [blog post](https://blog.orbeon.com/2013/11/inserting-and-reordering-grid-rows.html)
- Repeated sections: [blog post](https://blog.orbeon.com/2014/01/repeated-sections.html)
- Repeated grids and sections just got more subtle: [blog post](https://blog.orbeon.com/2015/10/repeated-grids-and-sections-just-got.html)
- Freezing rows in repeated grids or sections: [blog post](https://blog.orbeon.com/2019/06/freezing-rows-in-repeated-grids-or.html)
