# Allowed XPath functions in formulas and expressions

[\[SINCE Orbeon Forms 2026.1\]](../../release-notes/orbeon-forms-2026.1.md)

## Rationale

Form authors can specify XPath formulas for control calculations, validations, visibility, initial values, and actions. In multi-tenant environments or setups where forms are authored by users who should not have access to server internals, allowing arbitrary XPath functions can pose a security risk.

Starting with Orbeon Forms 2026.1, Form Runner enforces a secure-by-default list of allowed functions on all user-defined formulas and expressions.

## Where restrictions apply

Form Runner automatically applies function restrictions to all user-defined forms on Detail pages (in `new`, `edit`, `view`, and `pdf` modes), wherever formulas can appear:

* **Calculated and validation formulas**
* **Form controls and views**
* **User actions and submissions**
* **Section templates**

Form Runner automatically strips any author-injected `xxf:functions` or `xxf:allowed-functions` attributes in user forms to prevent bypassing these restrictions.

Internal Form Runner pages (such as Form Builder itself, the Summary page, the Landing page, and the Forms Admin page) are **not** restricted and operate with the full function library.

## Default baseline configuration

Form Runner defines an internal baseline configuration:

**Allowed by default:**

* Math functions: `math:*`
* Standard XPath 2.0 / 3.0 functions: `fn:*` (excluding the restricted file/system functions below)
* Standard XForms functions: `xf:*`
* XML Schema constructor types: `xs:*`
* XPath 3.1 map and array functions: `map:*`, `array:*`
* Safe Orbeon extension functions: `xxf:*`, `exf:*`, `fr:*` (excluding the restricted functions below)

**Restricted (disallowed) by default:**

| Category                            | Disallowed Functions                                                                                                                                                                                                                              | Recommended Alternative / Reason                                                                                                                          |
|-------------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------|
| **File and I/O access**             | `fn:doc`, `fn:doc-available`, `fn:collection`, `fn:unparsed-text`, `fn:unparsed-text-available`, `fn:unparsed-text-lines`, `fn:uri-collection`, `xxf:doc`, `xxf:doc-base64`, `xxf:doc-base64-available`                                           | Prevents unauthorized reading of server filesystem or arbitrary network resources. Use Form Runner HTTP Services or Datasets instead.                     |
| **Environment variables**           | `fn:environment-variable`, `fn:available-environment-variables`                                                                                                                                                                                   | Prevents leaking system environment variables.                                                                                                            |
| **Dynamic code execution**          | `fn:load-xquery-module`, `fn:transform`, `xxf:evaluate`, `xxf:evaluate-in-context`, `xxf:call-xpl`                                                                                                                                                | Prevents arbitrary code execution and pipeline invocation.                                                                                                |
| **Direct instance access**          | `xxf:instance`                                                                                                                                                                                                                                    | Prevents unauthorized access to Form Runner internal models, instances, and credentials. Use `$control-name` or `fr:dataset('name')` to access form data. |
| **Configuration properties**        | `xxf:property`, `xxf:properties-start-with`                                                                                                                                                                                                       | Prevents reading server configuration.                                                                                                                    |
| **Request & session attributes**    | `xxf:get-session-attribute`, `xxf:set-session-attribute`, `xxf:get-request-attribute`, `xxf:set-request-attribute`, `xxf:get-document-attribute`, `xxf:set-document-attribute`, `xxf:remove-document-attribute`, `xxf:remove-document-attributes` | Prevents unauthorized access or tampering with session state. Use HTTP service headers or Form Runner parameters.                                         |
| **Internal Form Runner operations** | `fr:evaluate-from-property`, `fr:run-process`, `fr:save-state`, `fr:transform-uploaded-image`, `xxf:image-metadata`                                                                                                                               | Prevents triggering internal Form Runner execution steps outside the standard action lifecycle.                                                           |
| **Java reflection**                 | All `java:*` calls                                                                                                                                                                                                                                | Blocked by default to prevent arbitrary Java code execution.                                                                                              |

## Customizing allowed functions

You can customize the allowed functions list using the `oxf.fr.detail.formula.allowed-functions.*.*` property in your `properties-local.xml`.

The property value uses the [allowed functions syntax](xforms.md#specification-syntax), with tokens applied **on top of** the baseline configuration.

### Property name format

Like standard Form Runner properties, you can configure it globally or override it per application and per form name:

* Globally: `oxf.fr.detail.formula.allowed-functions.*.*`
* Per application: `oxf.fr.detail.formula.allowed-functions.<app>.*`
* Per form: `oxf.fr.detail.formula.allowed-functions.<app>.<form>`

### Allowing functions for specific forms (backward compatibility)

If you have existing forms that legitimately rely on functions that are now restricted (such as `xxf:instance`), you can selectively re-allow them for those specific forms without opening up security for all forms:

```xml
<property
    as="xs:string"
    name="oxf.fr.detail.formula.allowed-functions.acme.expense-report"
    xmlns:xxf="http://orbeon.org/oxf/xml/xforms"
    xmlns:fn="http://www.w3.org/2005/xpath-functions"
    value="+xxf:instance"/>
```

### Denying additional functions

To forbid additional functions or entire namespaces across all forms:

```xml
<property
    as="xs:string"
    name="oxf.fr.detail.formula.allowed-functions.*.*"
    xmlns:fr="http://orbeon.org/oxf/xml/form-runner"
    value="-fr:dataset"/>
```

### Allowing a specific Java method

If your organization has custom Java or Scala utility functions, you can allow specific methods for trusted forms:

```xml
<property
    as="xs:string"
    name="oxf.fr.detail.formula.allowed-functions.hr.employee-review"
    value="+java:com.example.hr.PayrollUtils:calculatePension"/>
```

### Allowing all functions (disabling restrictions)

To completely disable restrictions for a specific legacy form or globally:

```xml
<property
    as="xs:string"
    name="oxf.fr.detail.formula.allowed-functions.acme.legacy-form"
    value="-* *"/>
```

## Form Builder integration

When editing forms in Form Builder, disallowed functions are caught at design time:

* Formula fields in the Control Settings and Section/Grid Settings dialogs validate expressions against the allowed functions policy.
* If a disallowed function is used, Form Builder displays a validation error indicating that the function is not permitted.
* This ensures authors discover disallowed functions immediately rather than encountering errors at runtime.

## See also

* [Low-level XForms allowed functions configuration](xforms.md#allowed-xpath-functions)
* [Form Formulas](../../form-builder/formulas.md)
* [Security](../advanced/security.md#allowed-xpath-functions-in-user-forms)
