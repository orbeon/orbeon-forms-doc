# Wizard Component



## What it does

The `fr:wizard` component presents the nested list of `fr:section` elements as a series of navigable steps, with only one section is visible at a time. The component also shows a table of contents and buttons allowing users to navigate between sections.

[SINCE Orbeon Forms 2016.2]

`fr:wizard` also supports optional navigation through the first level of subsections.

## Basic usage

```xml
<fr:wizard id="my-wizard">
    <fr:section id="personal-data-section">
        ...
    </fr:section>
    <fr:section id="comments-section">
        ...
    </fr:section>
</fr:wizard>
```

## The sectionref attribute

[SINCE Orbeon Forms 4.8]

The `sectionref` attribute allows reading and writing the current top-level section *name* to instance data.

```xml
<fr:wizard sectionref="instance('foo')/bar">
    <fr:section id="personal-data-section">
        ...
```

When the wizard shows for the first time, the current section value is read from instance data. If it matches one of the section names, the given section is shown first. When the user navigates between sections, the current section name is stored into instance data.

The section name is obtained from the section id by removing its `-section` or `-control` suffix. So in this example, the value stored will be `personal-data` or `comments`.

## The subsections-nav attribute

[SINCE Orbeon Forms 2016.3]

The `subsections-nav` attribute on top-level `fr:section` elements can be used to explicitly disable subsection navigation with:
   
```xml
<fr:section subsections-nav="false">
```

Top-level repeated sections do no support subsection navigation and behave as if they have `subsections-nav="false"`.

## Events supported by fr:wizard

### fr-prev and fr-next

You can dispatch the `fr-prev` and `fr-next` events to the wizard component to navigate forward/backward:

```xml
<xf:dispatch name="fr-next" targetid="my-wizard"/>
```

If the wizard cannot navigate, the event has no effect.

## Events dispatched by fr:wizard

### The fr-section-shown event

[SINCE Orbeon Forms 2016.2]

When the wizard component shows a section, it dispatches the `fr-section-shown` event with the following properties:
 
| Property       | Type         | Description                                              |
|----------------|--------------|----------------------------------------------------------|
| `section-name` | `xs:string`  | name of the Form Runner section being shown              |
| `validate`     | `xs:boolean` | whether the wizard is in validated mode                  |
| `separate-toc` | `xs:boolean` | whether the wizard is in separate table of contents mode |

In separate table of contents mode, if no section shows initially, this event is not dispatched until the first section shows. 

### The fr-toc-shown event

[SINCE Orbeon Forms 2016.2]

When the wizard component shows its table of contents, it dispatches the `fr-toc-shown` event with the following properties:

| Property       | Type         | Description                                               |
|----------------|--------------|-----------------------------------------------------------|
| `validate`     | `xs:boolean` | whether the wizard is in validated mode                   |
| `separate-toc` | `xs:boolean` | whether the wizard is in separate table of contents mode  |

In separate table of contents mode, this event can be dispatched multiple times as the user goes back and forth to the table of contents.

## See also

- [Form Runner Wizard View](../feature/wizard-view.md): the relevant Form Runner documentation
- [Form Runner Wizard View](https://blog.orbeon.com/2012/12/form-runner-wizard-view.html): a blog post which introduces to the feature, with a video
