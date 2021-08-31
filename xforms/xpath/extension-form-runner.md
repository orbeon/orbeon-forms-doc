# Form Runner functions

## Availability

[SINCE Orbeon Forms 2016.2]

These functions are only available within the context of Form Runner.

## Configuration

These functions are automatically available from Form Runner.

For plain XForms, the user needs to import the Form Runner function library by adding the following attribute on the first `<xf:model>`:

```
xxf:function-library="org.orbeon.oxf.fr.library.FormRunnerFunctionLibrary"
```

## Namespace

These functions are in the Form Runner namespace:

```
http://orbeon.org/oxf/xml/form-runner
```

This namespace is usually bound to the `fr:` prefix:

```
xmlns:fr="http://orbeon.org/oxf/xml/form-runner"
```

## Base functions

### fr:app-name()

```xpath
fr:app-name() as xs:string
```

Return the form's application name.

### fr:control-string-value()

[SINCE Orbeon Forms 2018.1]

```xpath
fr:control-string-value(
    $control-name   as xs:string,
    $follow-indexes as xs:boolean = false()
) as xs:string?
```

- `$control-name`
    - must correspond to an existing value control (statically)
- `$follow-indexes`
    - if missing, takes the value `false()`.
    - if `false()`
        - This finds the "closest" matching control without checking repeat indexes. When descending into repeat iterations, the first repeat iteration is chosen. 
    - if `true()`
        - This finds the "closest" matching control by following repeat indexes when possible. When descending into repeat iterations, the iteration matching the enclosing repeat's current index is chosen. 

This function returns the value of a Form Runner control by name as a string, or an empty sequence if the control or
value is not found.

### fr:control-typed-value()

[SINCE Orbeon Forms 2018.1]

```xpath
fr:control-typed-value(
    $control-name   as xs:string,
    $follow-indexes as xs:boolean = false()
) as array(xs:anyAtomicType)
```

Like `fr:control-string-value()` (see above), but it returns:

- an XPath `array()` of all the values found 
- each value has an XPath type when possible, including:
    - `xs:string`
    - `xs:boolean`
    - `xs:integer`
    - `xs:decimal`
    - `xs:date`
    - `xs:time`
    - `xs:dateTime`
- `$follow-indexes`
    - if missing, takes the value `false()`.
    - if `false()`
        - This finds the "closest" matching control without checking repeat indexes. When descending into repeat
          iterations, all repeat iterations are chosen. 
    - if `true()`
        - This finds the "closest" matching control by following repeat indexes when possible. When descending into repeat
          iterations, the iteration matching the enclosing repeat's current index is chosen.

For indexes in the array where it is not possible to return a typed value, the empty sequence is returned instead.

*NOTE: The reason this returns an XPath array and not an XPath sequence is that this lets the caller know which values are the empty sequence. This wouldn't be possible with an XPath sequence.* 

### fr:created-dateTime()

[SINCE Orbeon Forms 2016.3]

```xpath
fr:created-dateTime() as xs:dateTime?
```

Return the creation date of the current form data *as of the last read from the database* if available, and the empty sequence otherwise (such as in "new" mode).

### fr:workflow-stage-value()

[SINCE Orbeon Forms 2020.1]

```xpath
fr:workflow-stage-value() as xs:string?
```

If a workflow stage has been set for the current form data, then the function returns that workflow stage, as a string. And if no workflow stage has been set, the function returns an empty sequence. Also see the [`set-workflow-stage` action](/form-runner/advanced/buttons-and-processes/actions-form-runner.md#set-workflow-stage).

The function name has a `value` suffix as we anticipate future versions of Orbeon Forms to associate to the workflow stage a *label*, and also possibly a *description*, for cases when the workflow stage needs to be made visible to users. Thus the *value* will be only used to keep track of the current workflow stage, while the *label* and *description* will be user facing, and if needed localized.

### fr:dataset()

[SINCE Orbeon Forms 2017.1]

```xpath
fr:dataset($dataset-name as xs:string) as node()?
```

Return the root element of the XML document containing the given dataset.

If the dataset does not exist, return the empty sequence. 

For more on datasets, see [Datasets](../../form-runner/feature/datasets.md).

### fr:document-id()

```xpath
fr:document-id() as xs:string
```

Return the form's document id.

### fr:form-name()

```xpath
fr:form-name() as xs:string
```

Return the form's form name.

### fr:form-title()

[SINCE Orbeon Forms 2018.1]

```xpath
fr:form-title() as xs:string?
```

Return the form's title for the current form language.

### fr:form-version()

```xpath
fr:form-version() as xs:integer
```

Return the form definition's current version. By default, when versioning is not enabled, or in `test` mode, return `1`. 

### fr:is-design-time()

```xpath
fr:is-design-time() as xs:boolean
```

Return whether the form is shown at design time within Form Builder.

This function is particularly useful for XBL components.

### fr:is-form-data-saved()

```xpath
fr:is-form-data-saved() as xs:boolean
```

Return whether the form data has been saved (including after [the `set-data-status` action](../../form-runner/advanced/buttons-and-processes/actions-form-runner.md#set-data-status) was called with a `status` parameter set to `safe`).

### fr:is-form-data-valid()

```xpath
fr:is-form-data-valid() as xs:boolean
```

Return whether the form data is valid, that is the form data does not contain any errors.

### fr:is-noscript()

Return whether the form is in noscript mode.

```xpath
fr:is-noscript() as xs:boolean
```

*NOTE: Starting with Orbeon Forms 2018.1, this always returns `false`.*

### fr:is-pe()

[SINCE Orbeon Forms 2018.2]

```xpath
fr:is-pe() as xs:boolean
```

Return whether the current version Orbeon Forms is the Professional Edition (PE).

### fr:is-readonly-mode()

```xpath
fr:is-readonly-mode() as xs:boolean
```

Return whether the current page is in a readonly mode such as `view`, `pdf`, or `email`.

### fr:is-embedded()

[SINCE Orbeon Forms 2021.1]

```xpath
fr:is-embedded()
```

Returns whether the form is running in embedded mode. This includes embedding with:

- the [Form Runner Java embedding API](/form-runner/link-embed/java-api.md)
- the [Form Runner JavaScript Embedding API](/form-runner/link-embed/javascript-api.md)
- the [Form Runner Liferay Proxy Portlet](/form-runner/link-embed/liferay-proxy-portlet.md)
- the Offline mode [EXPERIMENTAL SINCE Orbeon Forms 2021.1]

This can, for example, be used to show and hide buttons or form sections only when the form is embedded. 

### fr:lang()

```xpath
fr:lang() as xs:string
```

Return the form's current language.

### fr:mode()

```xpath
fr:mode() as xs:string
```

Return the Form Runner mode.

### fr:modified-dateTime()

[SINCE Orbeon Forms 2016.3]

```xpath
fr:modified-dateTime() as xs:dateTime?
```

Return the modification date of the current form data *as of the last read from the database* if available, and the empty sequence otherwise (such as in "new" mode).

### fr:pdf-templates()

[SINCE Orbeon Forms 2018.1]

```xpath
fr:pdf-templates() as map(xs:string, xs:string?)*
```

Return the list of section templates associate with the current form definition. The return value is a sequence of
`map(xs:string, xs:string?)`, where keys map to values as follows:

- `path`: the path to the PDF template in the persistence layer
- `name`: the name of the PDF template, or the empty sequence if none was provided
- `lang`: the language of the PDF template, or the empty sequence if none was provided

This example shows a dropdown menu which lists the templates with a name:

```
<xf:select1 xmlns:map="http://www.w3.org/2005/xpath-functions/map">
    <xf:label>PDF templates</xf:label>
    <xf:itemset ref="fr:pdf-templates()[exists(map:get(., 'name'))]">
        <xf:label ref="map:get(., 'name')"/>
        <xf:value ref="map:get(., 'path')"/>
    </xf:itemset>
</xf:select1>
```

See also [PDF templates](../../form-builder/pdf-templates.md).

### fr:run-process()

```xpath
fr:run-process-by-name($scope as xs:string, $process as xs:string) as item()?
```

Run the given process given a scope and process content.

This function returns the empty sequence.

Example:

```xml
<xf:action type="xpath">
    fr:run-process('oxf.fr.detail.process', 'set-data-status(status = "safe")')
</xf:action>
```

### fr:run-process-by-name()

```xpath
fr:run-process-by-name($scope as xs:string, $process as xs:string) as item()?
```

Run the given process given a scope and process name.

This function returns the empty sequence.

Example:

```xml
<xf:action type="xpath">
    fr:run-process-by-name('oxf.fr.detail.process', 'save')
</xf:action>
```

### fr:component-param-value()

[SINCE Orbeon Forms 2018.1]

```xpath
fr:component-param-value(
    $name as xs:string
) as xs:anyAtomicType?
```

This function is similar to `xxf:component-param-value()`, but is designed to be used in XBL components that are expected to run in the context of Form Runner. It searches parameters in this order:

|#  |What|Used When|Comment
|---|---|---|---|
|1  |string value of the attribute of the current XBL component's bound node with a name matching the parameter|attribute present| |
|2  |current form's metadata instance (see below)|element present|\[SINCE Orbeon Forms 2018.2\]|
|3  |value of a property, taking into account the current app/form name. For instance, `xxf:component-param-value('theme')` called from the `fr:recaptcha` component uses the value of the `oxf.xforms.xbl.fr.recaptcha.theme.*.*`, following wildcard rules|property defined| |
|4  |value of a property without taking into account the current app/form name. For example `oxf.xforms.xbl.fr.recaptcha.theme` property, as done by `xxf:component-param-value()`|property defined| |

This allows authors of XBL components to:

- replace an existing call to `xxf:component-param-value()` by `fr:component-param-value()` while maintaining backward compatibility.
- enable users of the component to have different values for the property by app/form, should they need to.
- still allow the component to be used outside of Form Runner.

The Form Runner metadata instance looks like this:

```xml
<xf:instance xxf:readonly="true" id="fr-form-metadata" xxf:exclude-result-prefixes="#all">
    <metadata>
        <application-name>acme</application-name>
        <form-name>order</form-name>
        <title xml:lang="en">ACME Order Form</title>
        <description xml:lang="en"/>
        <xbl>
            <fr:number>
                <grouping-separator>"</grouping-separator>
            </fr:number>
        </xbl>
    </metadata>
</xf:instance>
```

_NOTE: The Form Runner metadata instance is maintained by Form Builder. We do not recommend making changes to that instance._

[SINCE Orbeon Forms 2018.2]

The resulting value is always evaluated as an AVT.

## Wizard functions

### fr:is-wizard-body-shown()

[SINCE Orbeon Forms 2016.2]

```xpath
fr:is-wizard-body-shown() as xs:boolean
```

Whether the wizard's body is visible.

### fr:is-wizard-first-page()

[SINCE Orbeon Forms 2016.3]

```xpath
fr:is-wizard-first-page() as xs:boolean
```

Whether the currently-shown wizard page is the first relevant page.

### fr:is-wizard-last-page()

[SINCE Orbeon Forms 2016.3]

```xpath
fr:is-wizard-last-page() as xs:boolean
```

Whether the currently-shown wizard page is the last relevant page. For instance, when using the wizard view, you can use this function to disable buttons until users have reached the last page of the wizard; the following property does so for the "Send" button:

```xml
<property
    as="xs:string"
    name="oxf.fr.detail.button.send.visible.*.*" 
    value="fr:is-wizard-last-page()"/>
```

### fr:is-wizard-toc-shown()

[SINCE Orbeon Forms 2016.2]

```xpath
fr:is-wizard-toc-shown() as xs:boolean
```

Whether the wizard's table of content is visible.

### fr:wizard-current-page-name()

[SINCE Orbeon Forms 2019.1]

```xpath
fr:wizard-current-page-name() as xs:string?
```

- If a wizard page is being shown, the function returns the name of the current wizard page name, which is the section name used in Form Builder.
- Otherwise, it returns the empty sequence. So an empty sequence is returned if the form is not using the wizard view. This can be used as follows to show a button, say the Submit button, only the last page of the wizard if the form is using the wizard view, but to always show that Submit button if the form isn't using the wizard view.

```xml
<property
        as="xs:string" 
        name="oxf.fr.detail.button.submit.enabled.*.*">
    xxf:is-blank(fr:wizard-current-page-name()) or
    fr:is-wizard-last-page()
</property>
```

## Authentication functions

### fr:can-create()

[SINCE Orbeon Forms 2017.1]

```xpath
fr:can-create() as xs:boolean
```

Whether the current user has the `create` permission for the current app/form name (detail page only).

### fr:can-delete()

[SINCE Orbeon Forms 2017.1]

```xpath
fr:can-delete() as xs:boolean
```

Whether the current user has the `delete` permission for the current form data (detail page only).

### fr:can-read()

[SINCE Orbeon Forms 2017.1]

```xpath
fr:can-read() as xs:boolean
```

Whether the current user has the `read` permission for the current app/form name (detail page only).

### fr:can-update()

[SINCE Orbeon Forms 2017.1]

```xpath
fr:can-update() as xs:boolean
```

Whether the current user has the `update` permission for the current form data (detail page only).

### fr:user-ancestor-organizations()

[SINCE Orbeon Forms 2016.3]

```xpath
fr:user-ancestor-organizations(
    $leaf-org as xs:string
) as xs:string*
```

See[`xxf:user-ancestor-organizations()`](extension-http.md#xxfuser-ancestor-organizations).

### fr:user-group()

```xpath
fr:user-group() as xs:string?
```

See[`xxf:user-group()`](extension-http.md#xxfuser-group).

### fr:user-organizations()

[SINCE Orbeon Forms 2016.3]

```xpath
fr:user-organizations() as xs:string*
```

See[`xxf:user-organizations()`](extension-http.md#xxfuser-organizations).

### fr:user-roles()

```xpath
fr:user-roles() as xs:string*
```

See[`xxf:user-roles()`](extension-http.md#xxfuser-roles).

### fr:username()

```xpath
fr:username() as xs:string?
```

See[`xxf:username()`](extension-http.md#xxfusername).

## See also

- Blog post: [A stable function library for Form Runner](https://blog.orbeon.com/2016/08/a-stable-function-library-for-form.html)
