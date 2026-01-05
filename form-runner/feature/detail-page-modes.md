# Form Runner Detail page modes

## Introduction

The Form Runner Detail page is the page where users create, edit, and view form data. This page can operate in several distinct *modes*. Each mode is designed for a specific task, such as creating new data, editing existing data, or simply viewing data in a read-only format.

[//]: # (TODO: image of a Detail page)

The primary modes are:

- `new`: For creating new form data.
- `edit`: For editing existing form data.
- `view`: For viewing form data in a read-only HTML format.

Other modes for viewing data include:

- `pdf`: For viewing form data as a PDF document.
- `tiff`: For viewing form data as a TIFF image.

You can link to a specific mode using the URL structure:

- `/fr/$app/$form/new`
- `/fr/$app/$form/edit/$document`
- `/fr/$app/$form/view/$document`
- `/fr/$app/$form/pdf/$document`
- `/fr/$app/$form/tiff/$document`

For more about linking, see [Linking to your forms](/form-runner/link-embed/linking.md).

## Process actions for switching modes

Some process actions are available to navigate between modes.

- `edit`: This action navigates to the `edit` mode from the `new` or `view` mode, or from a custom mode. For more information, see the [`edit` action documentation](/form-runner/advanced/buttons-and-processes/actions-form-runner.md#other-actions).
- `review`: This action navigates to the `view` mode from the `new` or `edit` mode, or from a custom mode. For more information, see the [`review` action documentation](/form-runner/advanced/buttons-and-processes/actions-form-runner.md#other-actions).

[\[SINCE Orbeon Forms 2025.1\]](/release-notes/orbeon-forms-2025.1.md)

The `change-mode()` action allows you to navigate to any mode, including custom modes. For more information, see the [`change-mode()` action documentation](custom-modes.md#the-change-mode-action).

## Custom modes

[\[SINCE Orbeon Forms 2025.1\]](/release-notes/orbeon-forms-2025.1.md)
Orbeon Forms allows you to define *custom modes* for the Detail page. This advanced feature lets you create different views of the same form data by writing a custom XBL component. Custom modes are useful for implementing workflows that involve external services, such as payment or signature providers.

For more details, see [Custom Detail page modes](custom-modes.md).

## Styling based on mode

You can apply different CSS styles based on the current mode. The `<body>` element of the Form Runner page includes a CSS class that reflects the current mode, in the format `fr-mode-$mode`. For example, in `edit` mode, the `<body>` element will have the class `fr-mode-edit`.

This allows you to write mode-specific CSS rules, like:

```css
.fr-mode-view .my-control {
    /* Special styles for view mode */
}
```

For more on styling, see the [CSS documentation](/form-runner/styling/css.md).

## See also

- [Linking to your forms](/form-runner/link-embed/linking.md)
- [Buttons and processes](/form-runner/advanced/buttons-and-processes/README.md)
- [Custom Detail page modes](custom-modes.md)
