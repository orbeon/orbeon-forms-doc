# Form Runner functions

<!-- toc -->

## Availability

[SINCE Orbeon Forms 2016.2]

These functions are only available within the context of Form Runner.

## Base functions

## fr:app-name()

Return the form's application name:

```xpath
fr:app-name() as xs:string
```

## fr:document-id()

Return the form's document id:

```xpath
fr:document-id() as xs:string
```

## fr:form-name()

Return the form's form name:

```xpath
fr:form-name() as xs:string
```

## fr:form-version()

Return the form's current version: 

```xpath
fr:form-version() as xs:string
```

## fr:is-design-time()

Return whether the form is shown at design time within Form Builder:

```xpath
fr:is-design-time() as xs:boolean
```

This is useful for XBL components.

## fr:is-noscript()

Return whether the form is in noscript mode.

```xpath
fr:is-noscript() as xs:boolean
```

## fr:is-readonly-mode()

Return whether the current page is in a readonly mode such as `view`:

```xpath
fr:is-readonly-mode() as xs:boolean
```

## fr:lang()

Return the form's current language:

```xpath
fr:lang() as xs:string
```


## fr:mode()

Return the Form Runner mode:

```xpath
fr:mode() as xs:string
```

## Authentication functions

### fr:user-group()

```xpath
fr:user-group() as xs:string?
```

Return the current user's group if available. This function works with container- and header-driven methods. See [Form Runner Access Control Setup](../../form-runner/access-control/README.md).

### fr:user-roles()

```xpath
fr:user-roles() as xs:string*
```

Return the current user's groups if available. This function works with container- and header-driven methods. See [Form Runner Access Control Setup](../../form-runner/access-control/README.md).

### fr:username()

```xpath
fr:username() as xs:string?
```

Return the current user's username if available. This function works with container- and header-driven methods. See [Form Runner Access Control Setup](../../form-runner/access-control/README.md).
