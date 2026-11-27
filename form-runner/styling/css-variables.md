# Form Runner CSS variables

## Overview

The styles of Form Runner and Form Builder use CSS variables, such as `--orbeon-accent-color` or `--orbeon-base-font-size`. The theme in use sets their values. The `theme.less` file of each built-in theme lists them: [2026 theme](https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/assets/apps/fr/themes/2026/theme.less), [2025 theme](https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/assets/apps/fr/themes/2025/theme.less).

You can change the value of a variable:

- with a property, without writing a CSS file, as described on this page;
- in a custom CSS file, see [Overriding CSS variables](css.md#overriding-css-variables).

## Setting variables with properties

### Basic usage

[\[SINCE Orbeon Forms 2026.1\]](/release-notes/orbeon-forms-2026.1.md)

The name of the property is `oxf.fr.style.variable.`, followed by the name of the variable without its `--orbeon-` prefix, and by the app and form names or wildcards. For example:

```xml
<property
    as="xs:string"
    name="oxf.fr.style.variable.accent-color.*.*"
    value="#b3003b"/>

<property
    as="xs:string"
    name="oxf.fr.style.variable.base-font-size.acme.*"
    value="15px"/>
```

With these properties, `--orbeon-accent-color` is `#b3003b` on all pages, Form Builder included, and `--orbeon-base-font-size` is `15px` for the forms of the `acme` app.

Each variable is resolved on its own, and the most specific property wins, as with other Form Runner properties. A blank value cancels, for an app or a form, a property set at a more general level. For example, to keep the accent color of the theme in Form Builder:

```xml
<property
    as="xs:string"
    name="oxf.fr.style.variable.accent-color.orbeon.builder"
    value=""/>
```

The value is a CSS value, which can use CSS functions such as `var()`, `calc()`, or `color-mix()`. A value which could break the stylesheet, for example with a `;` or a `}`, is ignored, and a warning is logged.

### Dark color scheme

To set a value for the [dark color scheme](/form-runner/feature/dark-mode.md), add `dark` after the name of the variable:

```xml
<property
    as="xs:string"
    name="oxf.fr.style.variable.accent-color.dark.*.*"
    value="#ff7aa5"/>
```

Without a `dark` property, a variable for which the theme has a dark value, as most colors, keeps the dark value of the theme.

### PDF files and printing

To set a value for [automatic PDF files](/form-runner/feature/pdf-automatic.md) and for printing from the browser, add `print` after the name of the variable:

```xml
<property
    as="xs:string"
    name="oxf.fr.style.variable.base-font-size.print.*.*"
    value="14px"/>
```

Without a `print` property, a variable which the theme sets for printed output, in the `@media print` block of its `theme.less`, keeps the value of the theme. This way, you can change the look of your forms on screen without changing their PDF files. `dark` values never apply to PDF files.

The PDF renderer supports fewer CSS functions than browsers: for PDF files, use plain values, such as a hexadecimal color or a length in `px`.

### Stable variables

You can set any variable of the theme. The following variables are stable: each built-in theme declares them, and future versions of Orbeon Forms will keep them with the same name. Other variables can be renamed or removed in a future version.

| Variable                | Use                                |
|-------------------------|------------------------------------|
| `accent-color`          | Accent color                       |
| `base-color`            | Color of the text                  |
| `base-font-family`      | Font family of the text            |
| `base-font-size`        | Font size of the text              |
| `page-background-color` | Background color of the page       |
| `form-background-color` | Background color of the form       |
| `base-border-color`     | Color of borders                   |
| `control-border-color`  | Color of the borders of fields     |
| `control-border-radius` | Radius of the corners of fields    |
| `button-border-radius`  | Radius of the corners of buttons   |
| `error-color`           | Color of errors                    |
| `link-color`            | Color of links                     |

If a property sets a variable which the theme in use doesn't declare, for example because of a typo, Orbeon Forms logs a warning.

## Order of precedence

For a given variable, from the lowest to the highest precedence:

1. the value of the theme;
2. the value of the property without `dark` or `print`;
3. the dark and print values of the theme;
4. the value of the property with `dark` or `print`;
5. the value set by a custom CSS file.

The built-in themes and the CSS produced for the properties implement this order with [cascade layers](https://developer.mozilla.org/en-US/docs/Web/CSS/@layer), declared in this order: `orbeon-theme`, `orbeon-properties`, `orbeon-theme-variants`, and `orbeon-properties-variants`. [Custom CSS files](css.md#adding-your-own-css-files) are not in a layer, so they take precedence over CSS in layers.

### Custom themes

If the theme in use doesn't put its values in the `orbeon-theme` layer, for example a theme of your own made from the `theme.less` of Orbeon Forms 2025.1, Orbeon Forms writes the values of the properties outside of any layer, so that they still apply. The order above is then not guaranteed.

To get the order above, make your theme from the `theme.less` of a built-in theme of Orbeon Forms 2026.1, which uses the layers.

## See also

- [CSS](css.md)
- [Automatic PDF styling and CSS](automatic-pdf.md)
- [Dark mode](/form-runner/feature/dark-mode.md)
- [Automatic PDF](/form-runner/feature/pdf-automatic.md)
