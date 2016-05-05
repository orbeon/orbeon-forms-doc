# Wizard View

<!-- toc -->

## Basic usage

By default with Form Runner all the form sections appear in the same page, on top of each other. If your form is large that means that you have to scroll to fill out the entire form.

With the wizard view, top-level sections instead appear in a table of contents area to the left, and only a single top-level section is shown at any given time in a separate wizard "page":

![Form Runner Wizard](../images/wizard.png)

You can navigate between pages by clicking on a title in the table of contents, or you can use the navigation arrows. You can also use "Prev" and "Next" buttons when configured.

Errors on your form appear at the bottom as usual, and the title of pages that contain errors are highlighted in red. If you click on an error you are taken directly to the page and control containing the error.

The wizard view is optional - you can use the regular view instead, and you can enable this view per form, per app, or globally with a property:

```xml
<property
  as="xs:string"
  name="oxf.fr.detail.view.appearance.*.*"
  value="wizard"/>
```

## Modes

### Introduction

The wizard supports two mode:

- the *free* mode (which is the default mode)
- the *validated* mode [SINCE Orbeon Forms 4.9]

### Free mode

When using the free mode, you can freely:

- go back to the preceding page
- go forward to the next page
- change page from the table of contents
- leaving a page marks all fields on the given page as visited, ensuring that errors on that page, if any, show in the error summary

### Validated mode

When using the validated mode:

- you can freely go back to the preceding page
- but you can only go forward to the next page if
  - there are no errors on all preceding pages as well as the current page
  - if you have already visited the next page
- the table of contents only allows you to navigate
  - to pages you have already visited
  - or to the next page if there are no errors on all preceding pages as well as the current page [SINCE Orbeon Forms 2016.1]
- you should generally use the "Prev" or "Next" buttons for navigation
- any attempt to navigate to the next page marks all the fields of the preceding pages as well as the current page as visited, ensuring that errors on those pages, if any, show in the error summary

The following property enables the validated mode:

```xml
<property
  as="xs:boolean"
  name="oxf.xforms.xbl.fr.wizard.validate.*.*"
  value="true"/>
```

You can see in the following picture that the sections in the table of contents which have not yet been visited are grayed out:

![Wizard validated mode](../images/wizard-validated.png)

## Buttons

### Inner buttons

The following property allows specifying which buttons are presented *inside* the wizard, as opposed to the bottom of the page. This example places the wizard's "Prev" and "Next" buttons just under the current wizard section:

```xml
<property
    as="xs:string"
    name="oxf.fr.detail.buttons.inner.*.*"
    value="wizard-prev wizard-next"/>
```

![](../../form-runner/images/wizard-buttons.png)

### Buttons visibility

[SINCE Orbeon Forms 2016.1]

When the following conditions are met:

1. You are using inner buttons.
2. You are using the validated mode.

The following buttons are automatically hidden until the wizard shows the last top-level form section:

- `save-final`
- `submit`
- `send`
- `review`
- `pdf`
- `tiff`
- `email`

## See also

- [Wizard XBL component](../component/wizard.md)
- [Buttons and Processes](../../form-runner/advanced/buttons-and-processes/README.md)
- [Form Runner Wizard View](http://blog.orbeon.com/2012/12/form-runner-wizard-view.html): a blog post which introduces to the feature, with a video
- [New wizard validated mode](http://blog.orbeon.com/2015/03/new-wizard-validated-mode.html): a blog post which introduces thew validated mode.