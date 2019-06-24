# CSS

## Rationale

Form Runner has a built-in theme for forms. This page documents how you can change that default.

CSS stands for Cascading Style Sheets, and is the standard technology to add style to web pages and web applications. Orbeon Forms supports custom CSS files which allows you to change your forms' appearance. This assumes that you have some CSS knowledge.

## The default

### Twitter Bootstrap

Since Orbeon Forms 4.0, Form Runner uses [Twitter Bootstrap][1] for some aspects of its layout and styling.

_NOTE: If you have custom CSS which works with Orbeon Forms 3.9, it is likely that you will have to update it to work with Orbeon Forms 4.0._

### Strength of CSS rules

Bootstrap, XForms engine, Form Runner and Form Builder rules are contained within an enclosing `.orbeon` CSS class. This ensures that the Orbeon CSS rules only apply within an element with that class. It also makes Orbeon CSS rules a bit stronger than before. You might have to update your custom CSS to take this into account.

## Writing your own CSS

### Where to put your CSS

You can either:

1. Store your CSS is a separate CSS file, which you either provide in addition or that overrides the default CSS provided by Orbeon Forms. For more on this, see the [`oxf.fr.css.custom.uri`](/configuration/properties/form-runner.md#adding-your-own-css) configuration property. This is the recommended technique if your CSS is intended to be shared by several forms.

2. Put your CSS inline, in the form. Uf your CSS is quite short, and specific to a given form (not to be shared amongst forms), this is a possibility. For this, put the rules within your own `<style>` section of the form:
    ```xml
    <xh:title>My Form Title</xh:title>
    <xh:style type="text/css">
        #fr-view .fr-grid .fr-grid-content .my-class input.xforms-input-input {
            width: 8em
        }
    </xh:style>
    ```
    
_NOTE: Since 2018.1, it is no longer recommended to place any inline CSS, as some servers use the [`Content-Security-Policy` header](https://en.wikipedia.org/wiki/Content_Security_Policy) to disable inline scripts and CSS. Orbeon Forms 2018.1 doesn't include any inline scripts and CSS anymore by default._

### Styling specific controls

You style specific controls in your form, say to set the width of an input field, by adding a CSS class on that control. To do so:

1. Edit the source code for the form (in Form Builder, from the left sidebar, under _Advanced_, click on _Edit Source_).
2. Locate the XForms control you need to modify (for instance `<xf:input>` for an input field).
3. If not present, add a class attribute (for instance: `<xf:input class="" ...="">`).
4. Add the space-separated class or classes in the `class` attribute (for instance: `<xf:input class="my-class" ...>`).

The CSS class you use can either be one you define, or one of the following built-in classes:

- `fr-width-2em`: sets the field with to 2 em
- `fr-width-3em`: sets the field with to 3 em
- `fr-width-5em`: sets the field with to 5 em
- `fr-width-7em`: sets the field with to 7 em
- `fr-width-10em`: sets the field with to 10 em
- `fr-width-15em`: sets the field with to 15 em

_NOTE: Since Orbeon Forms 2018.1, it is no longer recommended to use those CSS classes. Instead, vary the width of the 12-column grid cell in Form Builder to the desired size._

If you define your own CSS class, then write a CSS rule for that class. Make sure to use a strong selector, so its precedence is higher than CSS provide by Orbeon Forms, e.g. something like:

```css
#fr-view .fr-grid .fr-grid-content .my-class input.xforms-input-input {
    ...rule...
}
```

When doing CSS work, make sure to use a tool like the Chrome Dev Tools (or other browsers' similar tools) that shows what CSS rules apply to an element. This will be your best friend for CSS development (in general, not only for Orbeon Forms!).

## Changing the page width

The [default width with Bootstrap][4] is 940px, but you can change this by overriding the Bootstrap/Orbeon CSS. As usual, to do so, we recommend you use a tool like the [Chrome DevTools][5] to find the exact rules you need to override in your environment. For instance, with an out-of-the-box Orbeon Forms deployed as a servlet, you can set the width to 720px with:

```css
.orbeon .container, .orbeon .span12 { width: 720px }
```

## Configuring the presentation of automatic PDF output

See [Automatic PDF](automatic-pdf.md).

## See also

- [Grids CSS](grids.md)
- [Automatic PDF](automatic-pdf.md)

[1]: http://getbootstrap.com/2.3.2/
[4]: http://getbootstrap.com/2.3.2/scaffolding.html
[5]: https://developer.chrome.com/devtools
