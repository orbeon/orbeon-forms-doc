> [[Home]] ▸ Form Runner ▸ [[XBL Components|Form Runner ~ XBL Components]]

## What it does

The `fr:wizard` component presents the nested list of `fr:section` elements as a series of navigable steps. Only one section is visible at a time.

The component shows a table of contents and navigation.

See also the [blog post](http://blog.orbeon.com/2012/12/form-runner-wizard-view.html).

## Basic usage

```xml
<fr:wizard>
    <fr:section id="personal-data-section">
        ...
    </fr:section>
    <fr:section id="comments-section">
        ...
    </fr:section>
</fr:wizard>
```

[SINCE Orbeon Forms 4.8]

The `sectionref` attribute allows reading and writing the current section *name* to instance data.

```xml
<fr:wizard sectionref="instance('foo')/bar">
    <fr:section id="personal-data-section">
        ...
```

When the wizard shows for the first time, the current section value is read from instance data. If it matches one of the section names, the given section is shown first. When the user navigates between sections, the current section name is stored into instance data.

*NOTE: The section name is obtained from the section id by removing its `-section` or `-control` suffix. So in this example, the value stored will be `personal-data` or `comments`*

## See also

- [[Form Runner Wizard View|Form Runner ~ Wizard View]]: the relevant Form Runner documentation
- [Form Runner Wizard View](http://blog.orbeon.com/2012/12/form-runner-wizard-view.html): a blog post which introduces to the feature, including and a video