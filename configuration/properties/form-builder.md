# Form Builder configuration properties

## Default values

For the latest default values of Form Builder properties, see [properties-form-builder.xml](https://github.com/orbeon/orbeon-forms/blob/master/src/main/resources/config/properties-form-builder.xml).

## Toolbox

### Groups of controls

You configure the contents of the toolbox by configuring properties in this format:

```xml
<property as="xs:string"  name="oxf.fb.toolbox.group.$GROUPNAME.uri.*.*">
    $URLS
</property>
```

In that property, the value of `$GROUPNAME` determines a grouping of the controls in the toolbox. The controls are defined by a list of XBL file URLs specified by `$URLS`. For example:

```xml
<property as="xs:string" name="oxf.fb.toolbox.group.text.uri.*.*">
    oxf:/forms/orbeon/builder/xbl/text-controls.xbl
    oxf:/xbl/orbeon/tinymce/tinymce.xbl
    oxf:/xbl/orbeon/explanation/explanation.xbl
</property>
```

*NOTE: With Orbeon Forms 4.5 and earlier, values must be placed in the `value` attribute.*

```xml
<property
    as="xs:string"
    name="oxf.fb.toolbox.group.text.uri.*.*"
    value="oxf:/forms/orbeon/builder/xbl/text-controls.xbl
           oxf:/xbl/orbeon/tinymce/tinymce.xbl
           oxf:/xbl/orbeon/explanation/explanation.xbl"/>
```

To properly show up in the toolbox, XBL files need to include the appropriate [component metadata](../../form-builder/metadata.md).

### Other toolbox features

In addition to controls, the toolbox has other features which you can enable or disable with the following properties:

```xml
<property as="xs:boolean" name="oxf.fb.menu.schema"                  value="true"/>
<property as="xs:boolean" name="oxf.fb.menu.pdf"                     value="true"/>
<property as="xs:boolean" name="oxf.fb.menu.edit-source"             value="true"/>
<property as="xs:boolean" name="oxf.fb.menu.permissions"             value="true"/>
<property as="xs:boolean" name="oxf.fb.menu.permissions.owner-group" value="true"/>
<!-- [SINCE Orbeon Forms 4.6] -->
<property as="xs:boolean" name="oxf.fb.menu.services.http"           value="true"/>
<property as="xs:boolean" name="oxf.fb.menu.services.database"       value="true"/>
<property as="xs:boolean" name="oxf.fb.menu.actions"                 value="true"/>
```

- [SINCE Orbeon Forms 2022.1 and 2021.1.6] `oxf.fb.menu.permissions.owner-group` allows you, by setting the value of the property to `false`, to disable the two lines in the permissions dialog related to owner and group permissions. This is useful if you're always using a custom implementation of the persistence API that doesn't support owner and group permissions and you prefer to hide those options from form authors to avoid possible confusions.

## Publish dialog

### Links to new and summary pages

[SINCE Orbeon Forms 4.6]

By default, the Publish dialog proposes, upon successful publication of a form definition, shortcuts to navigate to the published form's New or Summary pages.

![Publish dialog](/form-builder/images/publish-initial-after.png)

This property allows specifying which of these actions are available:

```xml
<property
    as="xs:string"
    name="oxf.fb.publish.buttons"
    value="new summary"/>
```

### The Version dropdown

[SINCE Orbeon Forms 2022.1, 2021.1.3]

When versioning is supported, you can use the following property to control the behavior of the Version dropdown. If versioning isn't supported, this property has no effect. Also, the property has no effect if no version of this form has ever been published: in that case, version 1 will be published. The property can be set to:

- `default-to-select` – The dropdown is enabled, and users need to select whether they want to create a new version or overwrite an existing version. This is the default.
- `default-to-next` – The dropdown is enabled, and defaults to the choice to create a new version.
- `default-to-latest` – The dropdown is enabled, and defaults to overwriting the latest version.
- `force-next` – The dropdown is read-only, and a new version will be created on publish.
- `force-latest` – The dropdown is read-only, and the latest published version will be overwritten on publish.

```xml
<property
    as="xs:string"
    name="oxf.fb.publish.version"
    value="default-to-select"/>
```

## Maximum number of columns 

```xml
<property
    as="xs:integer"
    name="oxf.fb.grid.max-columns"
    value="4"/>
```

Use this property to change the default maximum number of grid columns form authors can create. The more columns there are, the more narrow each column is, and when columns become too narrow, some less "elastic" controls might not have enough space to render properly. You want to set this property to a "reasonable" value to reduce the chance of form authors ending up with columns that are too narrow to accommodate certain controls.

## Closing sections

```xml
<property
    as="xs:integer"
    name="oxf.fb.section.close"
    value="100"/>
```

Closing sections in Form Builder can improve responsiveness. This property sets the number of controls after which Form Builder will close all sections except the first one when loading a form. Below that number of controls, all sections are open by default.

## Action buttons in Form Builder

### Action buttons on the Form Builder summary page

This controls which buttons appear on the Form Builder summary page.

```xml
<property as="xs:string"  name="oxf.fr.summary.buttons.orbeon.builder">
    home delete duplicate new
</property>
```

### Action buttons on the Form Builder detail page

This controls which buttons appear on the Form Builder detail page.

```xml
<property as="xs:string"  name="oxf.fr.detail.buttons.orbeon.builder">
    summary new test publish save
</property>
```

## Available languages

```xml
<property
    as="xs:string"
    name="oxf.fr.available-languages.orbeon.builder"
    value="en fr es it de"/>
```

This controls which Form Builder user interface languages appear in the language selector.

## Permissions dialog

The permissions dialog allows form authors, amongst other things, to assign permissions to users having a specific roles. In that dialog, form authors can type in a text field the name of the role(s) they want to assign those permissions to. If you're systematically using certain roles, want to save form authors from having to type them, and prevent possible mistakes in the process, you can use the following property to list the role names you want the Form Builder permissions dialog to always show in that dialog:

```xml
<property as="xs:string" name="oxf.fb.permissions.role.always-show">
    ["Organization Owner"]
</property>
```

The value of this property is an array of strings in the JSON format. For instance, the following screenshot shows the dialog with the above property set (see the line for "Organization owner"), and where the form author added a line for an "admin" role.

![Permissions dialog with Organization Owner role](../images/fb-permissions-organization-owner.png)

See also [Access control for deployed forms](/form-runner/access-control/deployed-forms.md).

## Access control 

If you'd like to have multiple classes of Form Builder users where some can edit, say, forms in the `hr` app, while others can edit forms in the `sales` app, see [Access control for editing forms](../../form-runner/access-control/editing-forms.md#form-builder-permissions).

## Formatted text configuration

[\[SINCE Orbeon Forms 2023.1.1\]](/release-notes/orbeon-forms-2023.1.1.md)

You can configure the TinyMCE editor used for Formatted Text (AKA [Rich Text Editor](/form-runner/component/rich-text-editor.md)) component for the following:

- Explanatory Text content in the form area
- control labels with "Use HTML"  in Form Builder dialogs
- control hints with "Use HTML"  in Form Builder dialogs
- control help with "Use HTML"  texts in Form Builder dialogs

Use the following property: 

```xml
<property as="xs:string"  name="oxf.xforms.xbl.fr.tinymce.config.orbeon.builder">
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
        "convert_urls"       : false
    }}
</property>
```

The example above switches to the `iframe` mode (instead of the `inline` mode) and adds the fullscreen plugin.

For backward compatibility, if a non-blank `oxf.fb.tinymce.config` property is present, it will be used.

Note that `oxf.xforms.xbl.fr.tinymce.config.orbeon.builder` is evaluated as an AVT, while `oxf.fb.tinymce.config` is not.

## Explanatory Text TinyMCE configuration

[\[DEPRECATED SINCE Orbeon Forms 2023.1.1\]](/release-notes/orbeon-forms-2023.1.1.md)

Prefer the `oxf.xforms.xbl.fr.tinymce.config.orbeon.builder` property above.

Note that `oxf.xforms.xbl.fr.tinymce.config.orbeon.builder` controls the appearance of the Explanatory Text, as well as labels, hints, and help texts in Form Builder dialogs, while `oxf.fb.tinymce.config` only controls the appearance of the Explanatory Text.

[SINCE Orbeon Forms 2018.1]

When form authors edit an Explanatory Text, Form Builder utilizes the TinyMCE component. You can [configure TinyMCE](https://www.tiny.cloud/docs/configure/) by supplying your own configuration in JSON as the value for the `oxf.fb.tinymce.config` property. If you do not set the `oxf.fb.tinymce.config` property, Form Builder defaults to a configuration, which can be found at the bottom of [`TinyMce.scala`](https://github.com/orbeon/orbeon-forms/blob/master/web-facades/src/main/scala/org/orbeon/facades/TinyMce.scala).   

```xml
<property as="xs:string"  name="oxf.fb.tinymce.config">
    {
        "mode"              : "exact",
        "language"          : "en",
        ...
    }
</property>
```

## See also

- [Form Builder toolbox properties](/configuration/properties/form-builder.md#toolbox)
- [Formatted Text / Rich Text Editor](/form-runner/component/rich-text-editor.md)
