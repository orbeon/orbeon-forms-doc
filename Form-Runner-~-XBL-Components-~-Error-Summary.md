> [[Home]] ▸ Form Runner ▸ [[XBL Components|Form Runner ~ XBL Components]]

## What it does

The Error Summary component is a reusable component listing the errors present on your form (or a sub-form):

![](images/xbl-error-summary-errors.png)

Main features:

* Configurable title at the top
* Can handle your entire form or a sub-form
* Lists invalid fields, with associated alert messages
* Click on an entry focuses input on the invalid field (or links near it in noscript mode)
* Keeps track of visited controls, and shows error only for those visited while keeping track of all errors
* Handles repeats: if an iteration is moved, the error summary updates
* More than one error summary can be placed in the page

## Usage

### Basic usage

The minimal configuration looks like this:

```xml
<fr:error-summary observer="my-group">
    <fr:label>Your Form Contains the Following Errors</fr:label>
</fr:error-summary>
```

The mandatory `observer` attribute points to a control to observe. That control and all the descendants of that control are surveyed for changes in validity:

```xml
<xf:group id="my-group">
    <xf:input>
        <xf:label>Title</xf:label>
        <xf:alert>The book title is missing</xf:alert>
    </xf:input>
    <xf:input>
        <xf:label>Author</xf:label>
        <xf:alert>The author name is missing</xf:alert>
    </xf:input>
    <xf:input>
        <xf:label>Link</xf:label>
        <xf:alert>The link must be a valid HTTP or HTTPS URL </xf:alert>
    </xf:input>
</xf:group>
```

_NOTE: This means that unless the error summary only observes a single control, you need a grouping control in your form, such as `<xf:group>`._

_NOTE: For a control to visually appear in the summary, it must have a non-empty `<xf:alert>` element. However, if it doesn't have one, the error summary still adds the control to the count of current errors._

The title can be dynamic, e.g. for localization purposes:

```xml
<fr:error-summary observer="my-group">
    <fr:label><xf:output value="instance('resources')/summary-title"/></fr:label>
</fr:error-summary>
```

### Getting information from the error summary

If specified, the following attributes point to nodes into which the error summary makes useful information available to you:

* `error-count-ref:` current number of errors in the sub-form `(xs:integer)`.
* `visible-errors-count-ref:` current number of visible errors in the sub-form.  An error for a control is visible if the control has been visited by the user `(xs:integer`).
* `valid-ref:` whether the sub-form is valid or not `(xs:boolean)`.

```xml
<fr:error-summary observer="my-group" errors-count-ref="instance('errors')/errors-count" valid-ref="instance('errors')/valid">
    <fr:label>Your Form Contains the Following Errors</fr:label>
</fr:error-summary>
```

You can use this information for example to show a status icon:

```xml
<xf:group ref=".[not(instance('errors')/valid = 'true')]">
    <xhtml:img src="/apps/my-app/images/warning.png" alt="Form is invalid"/>
</xf:group>
```

### Marking all controls as visited

You can dispatch the `fr-visit-all` event to the error summary. This:

* makes the summary consider all controls under the configured observer(s) as visited
* marks all controls under the configured observer(s) as visited by adding the `xforms-visited and `xforms-alert-active-visited` classes`

This is useful when the user, for example, presses a "Save" button: in that case, you might want to show all the errors on the form right away:

```xml
<xf:trigger>
    <xf:label>Save</xf:label>
    <xf:dispatch ev:event="DOMActivate" name="fr-visit-all" targetid="my-error-summary"/>
</xf:trigger>
```

When you do this, make sure that `<fr:error-summary>` has an id:

```xml
<fr:error-summary observer="my-group" id="my-error-summary">
    ...
</fr:error-summary>
```

Then, to disable the alert icon on invalid controls, until either users went through the control, or users did an action (such as "Save") that dispatched an fr-visit-all, add the following CSS. The first rule disables the background image and the second one enables it, overriding the first rule in cases where the alert is both active and the control is visited. Most likely, you will need to modify the relative path in the second rule, depending on the location of your CSS file. The path should resolve to `/your-all/ops/images/xforms/exclamation.png` if you are not in separate deployment, and `/your-app/orbeon/ops/images/xforms/exclamation.png` if you use separate deployment. Note that it is better here to have a relative URL (adjusting the number of `..` in the path) rather than an absolute URL (starting with `/your-app`), as a relative URL allows you to change the context of your application without having to change the CSS.

```css
.xforms-alert-active         { background-image: none }
.xforms-alert-active-visited { background-image: url(../../../ops/images/xforms/exclamation.png) }
```

### Marking all controls as not visited

You can dispatch the `fr-unvisit-all` event to the error summary. This:

* makes the summary consider all controls under the configured observer(s) as not visited
* marks all controls under the configured observer(s) as not visited by removing the `xforms-visited` and `xforms-alert-active-visited` classes

### Adding a header and a footer

When there are no visible errors, the entire body of the error summary is hidden. You can had your own header and footer content within that body so it hides and shows depending on whether there are errors or not. Just add the `<fr:header>` and `<fr:footer>` elements:

```xml
<fr:error-summary observer="my-group">
    <fr:label>Your Form Contains the Following Errors</fr:label>
    <fr:header><xhtml:div class="fr-separator">&#160;</xhtml:div></fr:header>
</fr:error-summary>
```

### Global errors

In addition to errors related to controls, the error summary can handle global errors:

```xml
<fr:error-summary observer="my-group">
    <fr:errors nodeset="instance('errors')/error">
        <fr:label ref="label"/>
        <fr:alert ref="alert"/>
    </fr:errors>
</fr:error-summary>
```

The `fr:errors` element takes a `nodeset` attribute and iterates on a list of nodes containing information about global errors. If the node-set returned is empty, no global error is displayed.

The nested `fr:label` (optional) and `fr:alert` elements are evaluated relative to each node in the node-set. They return respectively:

* A label text displayed to the left
* An alert text displayed to the right

### Non-incremental mode

By default the error summary updates the list of error as they occur on the form.

By specifying the `incremental="false"` attribute, errors only show on demand with the `fr-update` and `fr-clear` events.

```xml
<fr:error-summary observer="my-group" id="my-error-summary" incremental="false">
    ...
</fr:error-summary>
```

To update the visible list of errors with the actual errors in the form, dispatch the `fr-update` event:

```xml
<xf:dispatch name="fr-update" targetid="my-error-summary"/>
```

To clear the visible list of errors, dispatch the `fr-clear` event:

```xml
<xf:dispatch name="fr-clear" targetid="my-error-summary"/>
```

To properly update the error summary within a submission response, you might need an explicit `<xf:refresh>` action before dispatching `fr-update`, so that the UI captures all the valid/invalid states:

```xml
<xf:action ev:event="xforms-submit-done">
    <xf:dispatch name="fr-visit-all" targetid="error-summary"/>
    <xf:refresh/>
    <xf:dispatch name="fr-update" targetid="error-summary"/>
</xf:action>
```
