# CSS

<!-- toc -->

## Rationale

Form Runner has a built-in theme for forms. This page documents how you can change that default.

## The default

### Twitter Bootstrap

Since Orbeon Forms 4.0, Form Runner uses [Twitter Bootstrap][1]. If you have custom CSS which works with Orbeon Forms 3.9, it is likely that you will have to update it to work with Orbeon Forms 4.0.

### Strength of CSS rules

Bootstrap, XForms engine, Form Runner and Form Builder rules are contained within an enclosing `.orbeon` CSS class. This ensures that the Orbeon CSS rules only apply within an element with that class. It also makes Orbeon CSS rules a bit stronger than before. You might have to update your custom CSS to take this into account.

## Writing your own CSS

### Where to put your CSS

You can either:

1. Store your CSS is a separate CSS file, which you either provide in addition or that overrides the default CSS provided by Orbeon Forms. For more on this, see the [`oxf.fr.css.custom.uri`][2] configuration property. This is the recommended technique if your CSS is intended to be shared by several forms.

2. Put your CSS inline, in the form. This is the recommended technique if your CSS is quite short, and specific to a given form (not to be shared amongst forms). For this, put the rules within your own `<style>` section of the form:
    ```xml
    <xh:title>My Form Title</xh:title>
    <xh:style type="text/css">
        #fr-view .fr-grid .fr-grid-content .my-class input.xforms-input-input {
            width: 8em
        }
    </xh:style>
    ```

### Styling specific controls

You style specific controls in your form, say to set the width of an input field, by adding a CSS class on that control. To do so:

1. Edit the source code for the form (in Form Builder, from the left sidebar, under _Advanced_, click on _Edit Source_).
2. Locate the XForms control you need to modify (for instance `<xf:input>` for an input field).
3. If not present, add a class attribute (for instance: `<xf:input class="" ...="">`).
4. Add the space-separated class or classes in the `class` attribute (for instance: `<xf:input class="my-class" ...>`).

The CSS class you use can either be one you define, or one of the following class provided for convenience in `form-runner-base.css`:

- `fr-width-2em``:` sets the field with to 2 em
- `fr-width-3em``:` sets the field with to 3 em
- `fr-width-5em``:` sets the field with to 5 em
- `fr-width-7em``:` sets the field with to 7 em
- `fr-width-10em``:` sets the field with to 10 em
- `fr-width-15em``:` sets the field with to 15 em

If you define your own CSS class, then write a CSS rule for that class. Make sure to use a strong selector, so it precedence over CSS provide by Orbeon Forms one. E.g. something like:

```css
#fr-view .fr-grid .fr-grid-content .my-class input.xforms-input-input {
    ...rule...
}
```

When doing CSS work, make sure to use a tool like Firebug that shows what CSS rules apply to an element. This will be your best friend for CSS development (in general, not only for Orbeon Forms!).


## Changing the page width

The [default width with Bootstrap][4] is 940px, but you can change this by overriding the Bootstrap/Orbeon CSS. As usual, to do so, we recommend you use a tool like the [Chrome DevTools][5] to find the exact rules you need to override in your environment. For instance, with an out-of-the-box Orbeon Forms deployed as a servlet, you can set the width to 720px with:

```css
.orbeon .container, .orbeon .span12 { width: 720px }
```

## Configuring the presentation of automatic PDF output

_NOTE: This doesn't apply if you associate a PDF template with a form. This only applies to the automatic PDF output feature of Form Runner._

The automatic PDF output feature of Form Runner uses the Flying Saucer library, which is based on HTML and CSS. This means that you can configure the PDF appearance by adding your own CSS to Form Runner, very much in the same way you configure the appearance of Form Runner in your web browser.

First, add your own CSS file to the list of Form Runner CSS files, with the `oxf.fr.css.custom.uri` property.

In your CSS file, write CSS specific to the print format, under a section like this:

```css
@media print {
    .. your CSS here...
}
```

This way you can write CSS that only impact the appearance of the PDF, not that of the HTML.


[1]: http://getbootstrap.com/2.3.2/
[2]: ../../configuration/properties/form-runner.md#adding-your-own-css
[4]: http://getbootstrap.com/2.3.2/scaffolding.html
[5]: https://developer.chrome.com/devtools
