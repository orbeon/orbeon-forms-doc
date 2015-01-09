[SINCE Orbeon Forms 4.0]

By default with Form Runner all the form sections appear in the same page, on top of each other. If your form is large that means that you have to scroll to fill out the entire form.

With the wizard view, top-level sections instead appear in a navigation area to the left, and only a single top-level section is shown at any given time:

![Form Runner Wizard](images/fr-wizard.png)

You can navigate between sections by clicking on a section title, or you can use the navigation arrows.

Errors on your form appear at the bottom as usual, and sections that contain errors are highlighted in red. If you click on an error you are taken directly to the section and control containing the error.

The wizard view is optional - you can use the regular view instead, and you can enable this view per form, per app, or globally with a property:

```xml
<property
  as="xs:string"
  name="oxf.fr.detail.view.appearance.*.*"
  value="wizard"/>
```

See also:

- [Form Runner Wizard View](http://blog.orbeon.com/2012/12/form-runner-wizard-view.html): a blog entry which introduces to the feature, including and a video