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
) as xs:string*
```

- `$control-name`
    - must correspond to an existing value control (statically)
- `$follow-indexes`
    - if missing, takes the value `false()`.
    - if `false()`
        - This finds the "closest" matching control without checking repeat indexes. When descending into repeat iterations, all repeat iterations are chosen.
    - if `true()`
        - This finds the "closest" matching control by following repeat indexes when possible. When descending into repeat iterations, the iteration matching the enclosing repeat's current index is chosen.

This function returns the value or values of a Form Runner control by name as a sequence of strings, or an empty sequence if the control or
value is not found.

[SINCE Orbeon Forms 2021.1.3]

```xpath
fr:control-string-value(
    $control-name   as xs:string,
    $follow-indexes as xs:boolean,
    $section-name   as xs:string
) as xs:string*
```

- `$section-name`
    - specifies the name of a form section identifying one or more (in case of repeated sections) section template instances included in the form
    - the search is limited to searching within those sections
    - if the section name does not identify a section containing a section template, the function returns the empty sequence

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
        - This finds the "closest" matching control without checking repeat indexes. When descending into repeat iterations, all repeat iterations are chosen. 
    - if `true()`
        - This finds the "closest" matching control by following repeat indexes when possible. When descending into repeat iterations, the iteration matching the enclosing repeat's current index is chosen.

For indexes in the array where it is not possible to return a typed value, the empty sequence is returned instead.

*NOTE: The reason this returns an XPath array and not an XPath sequence is that this lets the caller know which values are the empty sequence. This wouldn't be possible with an XPath sequence.* 

[SINCE Orbeon Forms 2021.1.3]

```xpath
fr:control-typed-value(
    $control-name   as xs:string,
    $follow-indexes as xs:boolean,
    $section-name   as xs:string
) as array(xs:anyAtomicType)
```

- `$section-name`
    - specifies the name of a form section identifying one or more (in case of repeated sections) section template instances included in the form
    - the search is limited to searching within those sections
    - if the section name does not identify a section containing a section template, the function returns the empty sequence

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
fr:dataset(
    $dataset-name as xs:string
) as node()?
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

### fr:form-runner-link

[SINCE Orbeon Forms 2022.1]

```xpath
fr:form-runner-link(
    $link-type as xs:string
) as xs:string
```

- `$link-type`
    - one of: `'edit'`, `'view'`, `'new'`, `'summary'`, `'home'`, `'pdf'`

<!-- TODO: "landing", "forms"? -->

Create a link back to Form Runner for the given page type. If applicable, the current form's application name, form name, form version, and document id are used.

Example:

```xpath
fr:form-runner-link('edit')
```

might return:

```
https://orbeon.acme.org/forms/fr/acme/sales/edit/3c67ec792809f100ec241d0341783a743fc0d7c1
```

The first part of the URL can be configured with the [`oxf.fr.external-base-url` property](/form-builder/template-syntax.md#links).

### fr:form-version()

```xpath
fr:form-version() as xs:integer
```

Return the form definition's current version. By default, when versioning is not enabled, or in `test` mode, return `1`. 

### fr:is-background()

[SINCE Orbeon Forms 2021.1]

```xpath
fr:is-background() as xs:boolean
```

Returns whether the form is running in the background.

See also [Run form in the background API](/form-runner/api/other/run-form-background.md).

### fr:is-browser-environment()

[SINCE Orbeon Forms 2021.1]

```xpath
fr:is-browser-environment() as xs:boolean
```

Returns whether the form runtime is running entirely in a browser environment.

As of Orbeon Forms 2021.1, this returns:

- `true()`: when the form runs in offline mode (for example using the "Test Offline" Form Builder button) 
- `false()`: in all other cases

### fr:is-design-time()

```xpath
fr:is-design-time() as xs:boolean
```

Return whether the form is shown at design time within Form Builder.

This function is particularly useful for XBL components.

### fr:is-embedded()

[SINCE Orbeon Forms 2021.1]

```xpath
fr:is-embedded() as xs:boolean
```

Returns whether the form is running in embedded mode. This includes embedding with:

- the [Form Runner Java embedding API](/form-runner/link-embed/java-api.md)
- the [Form Runner JavaScript Embedding API](/form-runner/link-embed/javascript-api.md)
- the [Form Runner Liferay Proxy Portlet](/form-runner/link-embed/liferay-proxy-portlet.md)
- the Offline mode [EXPERIMENTAL SINCE Orbeon Forms 2021.1]

This can, for example, be used to show and hide buttons or form sections only when the form is embedded.

[SINCE Orbeon Forms 2021.1.4 and 2022.1]

The function supports an optional parameter:

```xpath
fr:is-embedded($embedding-type as xs:string) as xs:boolean
```

Possible values:

- `portlet`: if passed, returns true if embedding is done via the Form Runner proxy portlet
- `java-api`: if passed, returns true if embedding is done via the Form Runner proxy portlet

Example:

```xpath
fr:is-embedded('java-api')
```

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

### fr:lang()

```xpath
fr:lang() as xs:string
```

Return the form's current language.

### fr:mode()

```xpath
fr:mode() as xs:string
```

Return the Form Runner [mode](/form-builder/formulas-examples.md#modes).

### fr:is-draft()

[SINCE Orbeon Forms 2020.1.6]

```xpath
fr:is-draft() as xs:boolean
```

Returns `true` if the user is currently editing a draft, `false` otherwise.

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

See also [PDF templates](/form-runner/feature/pdf-templates.md).

### fr:run-process()

```xpath
fr:run-process(
    $scope   as xs:string,
    $process as xs:string
) as xs:boolean?
```

Run the given process given a scope and process content.

This function returns `true()` if the process succeeded, and `false()` otherwise.

Example:

```xml
<xf:action type="xpath">
    fr:run-process('oxf.fr.detail.process', 'set-data-status(status = "safe")')
</xf:action>
```

[\[SINCE Orbeon Forms 2023.1.3\]](/release-notes/orbeon-forms-2023.1.3.md)

The function returns an empty sequence if the process contains at least one asynchronous action, and therefore the process result is yet unknown.

### fr:run-process-by-name()

```xpath
fr:run-process-by-name(
    $scope        as xs:string,
    $process-name as xs:string
) xs:boolean?
```

Run the given process given a scope and process name.

This function returns `true()` if the process succeeded, and `false()` otherwise.

Example:

```xml
<xf:action type="xpath">
    fr:run-process-by-name('oxf.fr.detail.process', 'save')
</xf:action>
```

[\[SINCE Orbeon Forms 2023.1.3\]](/release-notes/orbeon-forms-2023.1.3.md)

The function returns an empty sequence if the process contains at least one asynchronous action, and therefore the process result is yet unknown.

### fr:component-param-value()

[SINCE Orbeon Forms 2018.1]

```xpath
fr:component-param-value(
    $name as xs:string
) as xs:anyAtomicType?
```

This function is similar to `xxf:component-param-value()`, but is designed to be used in XBL components that are expected to run in the context of Form Runner. It searches parameters in this order:

| # | What                                                                                                                                                                                                                                                    | Used When          | Comment                       
|---|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|--------------------|-------------------------------|
| 1 | string value of the attribute of the current XBL component's bound node with a name matching the parameter                                                                                                                                              | attribute present  |                               |
| 2 | current form's metadata instance (see below)                                                                                                                                                                                                            | element present    | \[SINCE Orbeon Forms 2018.2\] |
| 3 | value of a property, taking into account the current app/form name. For instance, `xxf:component-param-value('theme')` called from the `fr:recaptcha` component uses the value of the `oxf.xforms.xbl.fr.recaptcha.theme.*.*`, following wildcard rules | property defined   |                               |
| 4 | value of a property without taking into account the current app/form name. For example `oxf.xforms.xbl.fr.recaptcha.theme` property, as done by `xxf:component-param-value()`                                                                           | property defined   |                               |

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

[SINCE Orbeon Forms 2022.1.1]

The function can take an optional second parameter, which is the id of a component control in scope.

```xpath
fr:component-param-value(
    $name         as xs:string,
    $component-id as xs:string
) as xs:anyAtomicType?
```

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

### fr:is-wizard-separate-toc()

[SINCE Orbeon Forms 2022.1]

```xpath
fr:is-wizard-separate-toc() as xs:boolean
```

Whether the wizard is configured to have a separate table of contents.

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

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

```xpath
fr:can-create() as xs:boolean
```

Whether the current user has the `create` permission for the current app/form name (detail page only).

### fr:can-delete()

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

```xpath
fr:can-delete() as xs:boolean
```

Whether the current user has the `delete` permission for the current form data (detail page only).

### fr:can-read()

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

```xpath
fr:can-read() as xs:boolean
```

Whether the current user has the `read` permission for the current app/form name (detail page only).

### fr:can-update()

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

```xpath
fr:can-update() as xs:boolean
```

Whether the current user has the `update` permission for the current form data (detail page only).

### fr:can-list()

[\[SINCE Orbeon Forms 2024.1.1\]](/release-notes/orbeon-forms-2024.1.1.md)

```xpath
fr:can-list() as xs:boolean
```

Whether the current user has the `list` permission for the current app/form name (detail page only).

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
