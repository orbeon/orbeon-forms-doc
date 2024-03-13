# Rich Text Editor Component

## Rationale

This components wraps the [TinyMCE](https://www.tiny.cloud/) rich text editor.

![](images/xbl-tinymce.png)

## Usage

Include the TinyMCE editor in your page with:

```xml
<fr:tinymce ref="..."/>
```

Since Orbeon Forms 2016.1, it is also enabled with:

```xml
<xf:textarea mediatype="text/html">
```

Until Orbeon Forms 4.10 included, `<xf:textarea mediatype="text/html">` was using the deprecated YUI 2 RTE editor.

### Configuration

#### Attribute, form, and property configuration

[\[SINCE Orbeon Forms 2023.1.1\]](/release-notes/orbeon-forms-2023.1.1.md)

The component can be configured with the `config` attribute. The attribute takes a JSON configuration string.

If no `config` attribute is set, the following property is used:

```xml
<property as="xs:string"  name="oxf.xforms.xbl.fr.tinymce.config">
    {{
        "inline"             : false,
        "hidden_input"       : false,
        "language"           : "en",
        "statusbar"          : false,
        "menubar"            : false,
        "plugins"            : "lists link fullscreen",
        "toolbar"            : "bold italic | bullist numlist outdent indent | link fullscreen",
        "browser_spellcheck" : true,
        "doctype"            : "&lt;!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">",
        "encoding"           : "xml",
        "entity_encoding"    : "raw",
        "forced_root_block"  : "div",
        "verify_html"        : true,
        "visual_table_class" : "fr-tinymce-table",
        "skin"               : false,
        "convert_urls"       : false,
        "content_css"        : "default"
    }}
</property>
```

You can also set properties specific to an app and/or form:

```xml
<property as="xs:string"  name="oxf.xforms.xbl.fr.tinymce.config.acme.sales">
    ...
</property>
```

Attribute and properties are interpreted as AVTs (like all attributes and properties that configure components), which explains the double brackets `{{...}}`.

For example, the following configuration restores the `iframe` mode (instead of the `inline` mode) for all rich text editors:

```xml
<property as="xs:string"  name="oxf.xforms.xbl.fr.tinymce.config">
    {{
        "inline"             : false,
        "hidden_input"       : false,
        "language"           : "en",
        "statusbar"          : false,
        "menubar"            : false,
        "plugins"            : "lists link fullscreen",
        "toolbar"            : "bold italic | bullist numlist outdent indent | link fullscreen",
        "browser_spellcheck" : true,
        "doctype"            : "&lt;!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">",
        "encoding"           : "xml",
        "entity_encoding"    : "raw",
        "forced_root_block"  : "div",
        "visual_table_class" : "fr-tinymce-table",
        "skin"               : false,
        "convert_urls"       : false,
        "content_css"        : "default",
        "content_style"      : "body.mce-content-body {{ margin: 0; color: #333 }} body.mce-content-body div {{font-family: 'Helvetica Neue'; font-size: 13px; padding: 4px 6px}} body.mce-content-body a {{ color: #0088cc }} body.mce-content-body p {{ margin: 0 0 10px }}"
    }}
</property>
``` 

In that example, the `content_style` property is used to set CSS to match the `inline` appearance of Form Runner.

For backward compatibility, if an `TINYMCE_CUSTOM_CONFIG` JavaScript variable (see below) is found in the page, it is used as the configuration.

#### Deprecated configuration

[\[DEPRECATED SINCE Orbeon Forms 2023.1.1\]](/release-notes/orbeon-forms-2023.1.1.md)

Prefer the `oxf.xforms.xbl.fr.tinymce.config` property above.

You can also customize the TinyMCE editor by adding JavaScript code to your form that defines a [TinyMCE configuration](https://www.tiny.cloud/docs/configure/) and assign it to the global `TINYMCE_CUSTOM_CONFIG` variable.

If you don't set this variable, the component uses a built-in default configuration. The default configuration limits the number of buttons shown to users.

### Read-only and relevant MIPs

The component supports being bound to a node which can be read-only or non-relevant, as defined by an `<xf:bind>`, and those properties can change dynamically, after the form is loaded. When bound to a read-only node, the toolbar and status bar are hidden, and the text can't be edited. When the bound node is non-relevant, the whole TinyMCE is hidden.

### Update heuristic

- Update of the bound node with the text typed by users — As users types in the editor, the TinyMCE generates change events, upon which the latest text is stored in the bound node. This happens when the editor loses the focus, but also at other key points while editing, for instance when changing formatting, or starting a new paragraph.
- Update of the editor with a new value stored in the bound node — Whenever the value of the bound node is changed, for instance with an `<xf:setvalue>`, the content of the editor is update accordingly, _unless_ the editor has the focus. This prevents the cursor moving back to the top of the editor in the middle of users entering text, for instance if you have XForms code that updates the HTML to perform cleanup.

## Limitations

- Because of the update heuristic (see above), by design the TinyMCE won't update if the value of the bound node changes while the focus is on the TinyMCE.
