# Form Runner functions

## Availability

[SINCE Orbeon Forms 2016.2]

These functions are only available within the context of Form Runner.

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
          iterations, all repeat iterations is chosen. 
    - if `true()`
        - This finds the "closest" matching control by following repeat indexes when possible. When descending into repeat
          iterations, the iteration matching the enclosing repeat's current index is chosen.

### fr:created-dateTime()

[SINCE Orbeon Forms 2016.3]

```xpath
fr:created-dateTime() as xs:dateTime?
```

Return the creation date of the current form data *as of the last read from the database* if available, and the empty sequence otherwise (such as in "new" mode).


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

### fr:is-readonly-mode()

```xpath
fr:is-readonly-mode() as xs:boolean
```

Return whether the current page is in a readonly mode such as `view`, `pdf`, or `email`.

### fr:is-wizard-body-shown()

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

Whether the currently-shown wizard page is the last relevant page.

### fr:is-wizard-toc-shown()

```xpath
fr:is-wizard-toc-shown() as xs:boolean
```

Whether the wizard's table of content is visible.

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

This function is in a way similar to `xxf:component-param-value()`, but is designed to be used in XBL components that are expected to run in the context of Form Runner:

1. The function starts by searching for a property taking into account the current app/form name. For instance, `xxf:component-param-value('theme')` called from the `fr:recaptcha` component uses the value of the `oxf.xforms.xbl.fr.recaptcha.theme.*.*`, following wildcard rules.
2. If it can't find such a property, it falls back to using the value of the `oxf.xforms.xbl.fr.recaptcha.theme` property, as done by `xxf:component-param-value()`.

This allows authors of XBL components:

- To replace an existing call to `xxf:component-param-value()` by `fr:component-param-value()` while maintaining backward compatibility.
- To enable users of the component to have different values for the property by app/form, should they need to.
- To still allow the component to be used outside of Form Runner. For this, the user of the XBL component needs to import the Form Runner function library by adding the following attribute on their first `<xf:model>`:

  ```
  xxf:function-library="org.orbeon.oxf.fr.library.FormRunnerFunctionLibrary"
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

- Blog post: [A stable function library for Form Runner](http://blog.orbeon.com/2016/08/a-stable-function-library-for-form.html)
