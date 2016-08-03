# Form Runner functions

<!-- toc -->

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

## Authentication functions

### fr:user-group()

```xpath
fr:user-group() as xs:string?
```

Return the current user's group if available. This function works with container- and header-driven methods. See [Form Runner Access Control Setup](../../form-runner/access-control/README.md).

*NOTE: This function is also available as `xxf:user-group()`. Prefer the Form Runner function with the `fr:` namespace prefix.*

### fr:user-roles()

```xpath
fr:user-roles() as xs:string*
```

Return the current user's groups if available. This function works with container- and header-driven methods. See [Form Runner Access Control Setup](../../form-runner/access-control/README.md).

*NOTE: This function is also available as `xxf:user-roles()`. Prefer the Form Runner function with the `fr:` namespace prefix.*

### fr:username()

```xpath
fr:username() as xs:string?
```

Return the current user's username if available. This function works with container- and header-driven methods. See [Form Runner Access Control Setup](../../form-runner/access-control/README.md).

*NOTE: This function is also available as `xxf:username()`. Prefer the Form Runner function with the `fr:` namespace prefix.*

## See also

- Blog post: [A stable function library for Form Runner](http://blog.orbeon.com/2016/08/a-stable-function-library-for-form.html)
