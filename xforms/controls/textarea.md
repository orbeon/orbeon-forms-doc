# Text area

<!-- toc -->

## Basic usage

```xml
<xf:textarea ref="text"/>
```

## Unformatted text

By default, the text area control is rendered as a regular multi-line text area:

![Multi-line text area in Orbeon Forms](../images/xforms-textarea-unformatted.png)

With the standard appearance, the following attributes are available:

- `xxf:cols`: same as HTML input `cols` attribute
- `xxf:rows`: same as HTML input `rows` attribute
- `xxf:maxlength`: same as HTML 5 input `maxlength` attribute (not all browsers support this attribute as of 2011-03)

You can add the `appearance="xxf:autosize"` attribute on the `<xf:textarea>` to get the textarea height to automatically adjust to its content.

## Formatted text

### Enabling the rich text editor

If you add the attribute `mediatype="text/html"` on your `<xf:textarea>`, then the text area will be rendered as an HTML editor:

![Rich text editor in Orbeon Forms](../images/xforms-textarea-formatted.png)

### HTML cleanup

When using the attribute `mediatype="text/html"`, the HTML area will clean-up the HTML typed or pasted in the editor. This is done for two reasons:

- **Avoiding a "tag soup"** – The HTML received from the browser can be grossly invalid, and contain foreign elements, in particular when pasting text copied from another application (e.g. Microsoft Word) into the editor. If kept as-is, the "HTML" you capture this way is then harder to process.
- **Security** – If you don't clean up the HTML, and if your application sometimes shows HTML entered by one user to another user, your application can pose a security threat. A malicious user M can insert in the HTML some JavaScript that exploits a security bug in a given browser. When a victim V loads a page that displays the HTML added by M, that HTML (with the script it contains) runs and can potentially compromise V's computer.

The cleanup is performed in two stages:

1. The HTML goes through TagSoup and converted into a well-formed XML fragment.
2. The XML fragment goes through an XSLT transformation, which removes all the `<script>` and their content, and only keeps well-known elements in the HTML (such as `<b>`, `<p>`, `<a>`...). This is done in [`clean-html.xsl`](https://github.com/orbeon/orbeon-forms/blob/master/src/resources-packaged/ops/xforms/clean-html.xsl), which is bundled in `orbeon-resources-private.jar`. Should you want to change the way HTML is cleaned, you can override this file by placing your copy under the same path in your resources (typically `WEB-INF/resources/ops/xforms/clean-html.xsl`).

## Limitations

HTML Editor in incremental mode – When the HTML editor is in incremental mode, XForms code can't change the value shown by the editor as long as the editor has the focus.
