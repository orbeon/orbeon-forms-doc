# Offline test

## Availability

[SINCE Orbeon Forms 2021.1]

## Rationale

Since its inception, Orbeon Forms has had a hybrid architecture for forms:

- the user interface runs in the browser, implemented in JavaScript (and more recently Scala.js)
- the form's logic and validations runs on the server

This architecture has benefits, such as protecting the confidentiality of internal data that never leaves the server.

This is still the case with the Orbeon Forms 2021.1, however we made lots of internal changes to support running Form Runner in a pure JavaScript environment, and we sometimes refer to this mode as the "Offline mode", even though it doesn't always imply being offline. This should, however, in the future, allow running forms entirely offline, as well as embedded within mobile apps.

For API details, see [Offline embedding API](/form-runner/api/other/offline-embedding-api.md).

## Preview

Orbeon Forms allows testing that most aspects of a form work in . From Form Builder, simply use the new "Test Offline" button to see whether your form operates and renders properly in this new mode.

![The "Test Offline" button](/form-builder/images/test-offline-button.png)

When you do this:

- The form definition edited in Form Builder is compiled to a serialized representation.
- The JavaScript-based form runtime is loaded in the Form Builder test window, loads the compiled form, and renders it. 

From the user's perspective, this works almost exactly like the "Test" button which has always been present in Form Builder.

As of Orbeon Forms 2023.1, there are limitations, including the following:
 
- The APIs to compile and embed forms are not fully documented.
- Some controls are not fully supported, including the Formatted Text Area as well as attachment controls.
- Some formulas might not work.
- Performance needs some improvements.
- There is no XML Schema support for datatype validation (although this is rarely used).

## See also

- [Offline embedding API](/form-runner/api/other/offline-embedding-api.md)