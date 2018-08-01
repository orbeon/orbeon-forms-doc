# Automatic PDF

## Context

Form Runner has two modes of [PDF production](../../form-builder/pdf-production.md). When using the automatic mode, the PDF is produced from HTML and CSS using the Flying Saucer PDF library.

It is sometimes desirable to modify the appearance of the PDF produced. This typically involves adding custom CSS. The CSS has an effect on the PDF output very much like CSS has an effect on the HTML which appears in your web browser.

_NOTE: This doesn't apply if you associate a PDF template with a form. This only applies to the automatic PDF output feature of Form Runner._

## How to add custom CSS

See [CSS](css.md) and the [`oxf.fr.css.custom.uri` property](../../configuration/properties/form-runner.md#adding-your-own-css) and related.

In your CSS file, write CSS specific to the print format, under a section like this:

```css
@media print {
    .. your CSS here...
}
```

This way you can write CSS that only impact the appearance of the PDF output, and not that of the regular HTML which appears in your web browser.

## How to figure out what CSS is applied to the PDF?

Because there are no browser developer tools which let you see how CSS rules apply to the PDF, unlike when doing work in the browser, some guesswork is sometimes needed. 

However, the closest from the PDF is the Form Runner Review (or `view`) mode (not the `new` or `edit` mode). Compare
[this page](http://demo.orbeon.com/orbeon/fr/orbeon/bookshelf/view/891ce63e59c17348f6fda273afe28c2b) with the PDF version. In both cases, the HTML markup is about the same, and you can use browser developer tools to inspect how CSS rules are applied.

Note that in particular, the `view` mode does not have `input` or other HTML form fields. Instead, you can
target nested `.xforms-field` elements.

With Chrome, in particular, you can also put the browser in `@media print` emulation:

![Chrome emulation settings](../images/chrome-media-emulation.png)

With that setting enabled, the main remaining difference between the Review page and the PDF is how browsers and the PDF library Orbeon Forms uses interpret CSS, as there are differences. Often you have to go by trial and error.

## See also

- [CSS](css.md)
- [Grids CSS](grids.md)
