# Error Handling - Detailed Behavior

See also:

- [XForms Error Handling](../xforms/error-handling.md)

## Philosophy of error handling

* Upon initial page load:
    * It is acceptable if not desirable to immediately show a static error, as the user hasn't yet interacted with the page.
    * For dynamic XForms, it is better not to show an error immediately (case of Form Builder and `xxf:dynamic`).
* Upon subsequent interactions:
    * The XForms engine attempts to recover from errors occurring with XPath expressions, bindings, and actions.

## How the XForms engine recovers from errors

1. For control bindings, model bindings, values, variables
    * Upon XPath errors, a default value is chosen:
        * the empty sequence for bindings and variable values
        * the empty string for string values
    * Upon binding errors with the `bind` attribute
        * the binding resolves to the empty sequence
    * Upon binding errors with the `model` attribute
        * the model doesn't change, as if the `model` attribute was missing
1. For XPath MIP values
    * Upon XPath errors for  `calculate` and `xxforms:default` [SINCE 2012-12-06]
        * the destination value is set to blank
        * if `xxforms:expose-xpath-types="true"` and there is an error accessing a typed value
            * the error is logged at debug level
        * else if `xxforms:expose-xpath-types="false"` or there is any other dynamic error
            * `xxforms-xpath-error` is dispatched to the model
    * Upon XPath errors for other MIPs
        * the MIP is not modified, as if the attribute specifying the property was missing
        * `xxforms-xpath-error` is dispatched to the model
    * Upon binding errors with complex or readonly content (`calculate` or `xxforms:default` only)
        * the instance value is not modified
        * `xxforms-binding-error` is dispatched to the model
1. For the submission `instance` and `xxforms:instance` attributes
    * Upon incorrect instance id
        * `xforms-submit-error` is dispatched (as in the case of a target error with the `targetref` attribute)
    * Upon binding errors with complex or readonly content
        * `xforms-submit-error` is dispatched
1. For actions
    * Any error taking place during action processing stops the outermost action handler, including:
        * XPath errors
        * binding errors with the `bind` or `model` attribute
        * binding errors with complex or readonly content
        * missing attributes or unsupported attribute values on action elements
    * `xxforms-action-error` event is dispatched to observer of the action
    * _NOTE: Some actions silently ignore some error conditions, including:_
        * `<setvalue>` pointing to an empty sequence or to an atomic item (such as a string) instead of a node
        * <delete> with an empty sequence or an empty overridden context
        * <insert> with an empty or non-element insert context, an empty overridden context, or an empty origin
        * actions with AVTs evaluating to the empty sequence
    * `<dispatch>`, `<send>`, `<setfocus>`, `<setindex>`, `<toggle>` when the target element is not found

In all cases except typed value access XPath errors on MIPs:

* errors are logged at `debug` level [`warning` level until Orbeon Forms 4.8]
* errors are added to a list of errors to send to the client
* the client shows an error dialog which the user can discard (when enabled via properties)

In the case of typed value access XPath errors on MIPs:

* errors are logged at `debug` level

## Events dispatched

The `xxforms-xpath-error` event is dispatched:

*  to the model, upon encountering an XPath error during processing of an XPath model item property (MIP)

The `xxforms-binding-error` event is dispatched:

* to the model, if a `calculate` or `xxforms:default` MIP points to complex or readonly content
* to the control, if an attempt to store an external value (control value, filename, metadata or size) on a control to a node with complex or readonly content takes place
_NOTE: This should ideally not occur as the value control binding should not point to complex content, and readonly is disallowed._

The `xxforms-action-error` event is dispatched:

* [SINCE 2012-06-08]
    * to the _observer_ of the action, upon encountering an error during processing of an action
    * this includes: controls, model, instance, and submission
* [PRIOR TO 2012-06-08]
    * to the top-level _document_, upon encountering an error during processing of an action

_NOTE: The fatal, non-cancelable XForms `xforms-compute-exception` and `xforms-binding-exception` are no longer dispatched by Orbeon Forms._

## Reference: kinds of errors that can occur

* XForms errors
    * static XPath errors
        * some are detected during the static analysis phase (PE)
        * some expressions are not analyzed during static analysis of the page, and so can occur at runtime
    * dynamic XPath errors
    * binding errors with the `bind` or `model` attributes, e.g.: `bind="foobar"` where id `foobar` doesn't exist
        * some are detected during the static analysis phase
        * some are not analyzed during static analysis of the page, and so can occur at runtime
    * binding errors with complex or readonly content
        * XForms disallows setting the value of an element with complex content
        * XForms disallows setting the value of a readonly element or attribute
* errors outside of XForms, for example:
    * in the XPL (XML pipeline) engine
    * in the controller
* other server errors:
    * major errors such as the server running out of resources
    * server bugs!
