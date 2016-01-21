# Dialog Control

<!-- toc -->

## Declaring a dialog control

You declare dialogs directly under the `<xh:body>` element (or under the `<fr:body>` element for Form Runner forms) with:

```xml
<xxf:dialog 
    id="my-dialog-id" 
    appearance="full | minimal" 
    level="modeless"
    close="true" 
    draggable="true" 
    visible="false">
    
    <xf:label>Dialog title</xf:label>
    
    Content of the dialog (XHTML + XForms)
    
</xxf:dialog>
```

* When you have `appearance="full"` on the dialog, you define the title of the dialog with the embedded `<xf:label>` element.
* Inside an `<xxf:dialog>` you can use all the XHTML and XForms elements you can normally use elsewhere on the page. You can have other XForms controls, or show anything you would like to with HTML.
* The attributes on the `<xxf:dialog>` are as follows:

| Attribute | Mandatory | Values | Comment |
|---|---|---|---|
| `id` |  Mandatory | ID |  The ID of the dialog. You reference this ID when opening the dialog with ` <xxf:show dialog="my-dialog-id">`.  |
| `appearance` |  Optional | `full` or `minimal`  | See details below. |
| `level` |  Optional | `modal` (default) or `modeless` | Can only be used `appearance="full"`. When set to `modal` the rest the page is grayed out and you can't interact with any control on the page outside of the dialog. When set to `modeless` you can still use other controls on the page.  |
| `close`  | Optional | `true` or `false`| Can only be used `appearance="full"`. A close box "x" is shown in the dialog title bar when `close="true"`. If you specify `close="false"`, then you should provide a way to close the dialog, for instance by having you own "Close" button inside the dialog. This is typically useful when you want to force users to enter some data before proceeding and you don't want them to cancel the current operation by closing the dialog.  |
| `draggable` |  Optional | `true` or `false`| Can only be used `appearance="full"`. When `draggable="false"`, you won't be able to move dialog on the page by using drag and drop in the dialog title bar.  |
| `visible`  |  Optional | `true` or `false`| Whether the dialog is initially visible when the page loads. When `visible="true"`, the dialog appears immediately when the page loads.  |
| `neighbor`  |  Optional | ID |  Use only with `minimal` appearance. The id of the control next to which the dialog should display when opening. |
| `class` |  Optional | CSS classes | Adds a class on the HTML element that contains the dialog, which you can use to style its content. Also, if amongst the classes you specify `xxforms-set-height`, then the height of the dialog is set to its max height, which is the viewport height minus 80 pixels. |


You can set the appearance to either `full` or `minimal`:

* The first screenshot below shows a dialog with `appearance="full"` while the second one shows a dialog with `appearance="minimal"`.
* In general, you will use the minimal dialog when you want to show a limited set of information which is related to a certain element in the page. The minimal dialog is sometime also referred to as a "drop-down dialog".
* Some of the other attributes on `<xxf:dialog>` can only be used for the full or the minimal dialog. You will find more details on this below.

## The `xxf:show` and `xxf:hide` actions

You open a dialog by using the `xxf:show` action:

```xml
<xf:trigger>
  <xf:label>Show Dialog</xf:label>
  <xxf:show ev:event="DOMActivate" dialog="hello-dialog"/>
</xf:trigger>
```

If the dialog is already open, no action takes place.

`xxf:show` supports the following attributes:

| Attribute | Mandatory | Values | Comment |
|---|---|---|---|
| `dialog` |  Mandatory | ID | The ID of an existing dialog to open. |
| `neighbor` |  Optional | ID | Use only with `appearance="minimal"`. The ID of the control next to which the dialog should display when opening.  |
| `constrain` |  Optional | `true` or `false` | Whether to constrain the dialog to the viewport. |

You close a dialog by using the `xxf:hide` action:

```xml
<xf:trigger>
  <xf:label>Hide Dialog</xf:label>
  <xxf:hide ev:event="DOMActivate" dialog="hello-dialog"/>
</xf:trigger>
```

If the dialog is already closed, no action takes place.

`xxf:hide` supports the following attributes:

| Attribute | Mandatory | Values | Comment |
|---|---|---|---|
| `dialog` |  Mandatory | ID | The ID of an existing dialog to close.  |

## Events

See [dialog control events](../events-extensions-events.md#dialog-control-events) for details.

