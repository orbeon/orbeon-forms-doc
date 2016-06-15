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

Return the form's application name:

### fr:document-id()

```xpath
fr:document-id() as xs:string
```

Return the form's document id:

### fr:form-name()

```xpath
fr:form-name() as xs:string
```

Return the form's form name:

### fr:form-version()

```xpath
fr:form-version() as xs:string
```

Return the form's current version: 

### fr:is-design-time()

```xpath
fr:is-design-time() as xs:boolean
```

Return whether the form is shown at design time within Form Builder:

This function is particularly useful for XBL components.

### fr:is-noscript()

Return whether the form is in noscript mode.

```xpath
fr:is-noscript() as xs:boolean
```

### fr:is-readonly-mode()

```xpath
fr:is-readonly-mode() as xs:boolean
```

Return whether the current page is in a readonly mode such as `view`:

### fr:lang()

```xpath
fr:lang() as xs:string
```

Return the form's current language:


### fr:mode()

```xpath
fr:mode() as xs:string
```

Return the Form Runner mode:

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