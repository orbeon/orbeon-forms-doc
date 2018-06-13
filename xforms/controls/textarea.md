# Text area control



## Basic usage

```xml
<xf:textarea ref="text"/>
```

## Unformatted text

By default, the text area control is rendered as a regular multi-line text area:

![Multi-line text area in Orbeon Forms](../images/xforms-textarea-unformatted.png)

### Attributes

With the standard appearance, the following attributes are available:

- `xxf:cols`: same as HTML input `cols` attribute (prefer using CSS for this)
- `xxf:rows`: same as HTML input `rows` attribute (prefer using CSS for this)
- `xxf:maxlength`: same as HTML 5 input `maxlength` attribute (not all browsers support this attribute, and some have bugs)

You can add the `appearance="xxf:autosize"` attribute on the `<xf:textarea>` to get the textarea height to automatically adjust to its content.

### Placeholder for label and hint

[SINCE Orbeon Forms 2017.1]

#### Per-control properties

The label or hint associated with `<xf:textarea>` may have the `minimal`  appearance:

```xml
<xf:textarea ref=".">
    <xf:label appearance="minimal">Comment</xf:label>
    <xf:hint>Hint</xf:hint>
</xf:textarea>

<xf:textarea ref=".">
    <xf:label>Comment</xf:label>
    <xf:hint appearance="minimal">Hint</xf:hint>
</xf:textarea>
```

This causes either the label or the hint to appear on the background of the field when it is empty. If both the label and hint have a `minimal` appearance, the label wins.

Orbeon Forms leverages the HTML 5 `placeholder` attribute for browsers that support it (Firefox 3.5+, Chrome, Safari, Opera), and simulates the HTML 5 `placeholder` functionality  in JavaScript for browsers that don't support it (IE8 and IE9). In that case, you can customize how the placeholder is displayed by overriding the CSS class `xforms-placeholder`.

#### Per-form properties

_NOTE: This works the same as the per-form properties for `<xf:input>`._

The XForms `oxf.xforms.label.appearance` or `oxf.xforms.hint.appearance` (or the corresponding `xxf:label.appearance` and `xxf:hint.appearance` attributes on the first `<xf:model>` element) allow setting a default for the labels and hint appearances for the entire form.

The default values is `full`:

```xml
<property
    as="xs:string"  
    name="oxf.xforms.label.appearance"                            
    value="full"/>
    
<property
    as="xs:string"  
    name="oxf.xforms.hint.appearance"                             
    value="full"/>
```

Supported values for `oxf.xforms.label.appearance`:

- `full`: labels show inline above the control (the default)
- `full minimal`: labels show as an HTML *placeholder* within the field when the field is empty

Supported values for `oxf.xforms.hint.appearance`:

- `full`: hints show inline below the control (the default)
- `full minimal`: hints show as an HTML *placeholder* within the field when the field is empty
- `tooltip`: hints show as tooltips upon mouseover
- `tooltip minimal`: hints show as an HTML *placeholder* within the field when the field is empty

When the global property includes `minimal`, it is possible to override the appearance on the control with `appearance="full"`:

```xml
<xf:textarea ref=".">
    <xf:label appearance="full">Comment</xf:label>
    <xf:hint>Hint</xf:hint>
</xf:textarea>

<xf:textarea ref=".">
    <xf:label>Comment</xf:label>
    <xf:hint appearance="full">Hint</xf:hint>
</xf:textarea>
```

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
2. The XML fragment goes through an XSLT transformation, which removes all the `<script>` and their content, and only keeps well-known elements in the HTML (such as `<b>`, `<p>`, `<a>`...). This is done in [`clean-html.xsl`](https://github.com/orbeon/orbeon-forms/blob/master/src/main/resources/ops/xforms/clean-html.xsl), which is bundled in `orbeon-core.jar` (prior to Orbeon Forms 2016.3, in `orbeon-resources-private.jar`). Should you want to change the way HTML is cleaned, you can override this file by placing your copy under the same path in your resources (typically `WEB-INF/resources/ops/xforms/clean-html.xsl`).

### Limitations

HTML Editor in incremental mode – When the HTML editor is in incremental mode, XForms code can't change the value shown by the editor as long as the editor has the focus.
