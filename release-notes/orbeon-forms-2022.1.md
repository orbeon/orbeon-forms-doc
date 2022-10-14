# Orbeon Forms 2022.1

TODO

## Compatibility notes

### Internet Explorer support

This release no longer supports Internet Explorer. In particular, Internet Explorer 11 is no longer supported.

### Extra elements in initial data POSTed to form

If the data posted contains extra elements, those were ignored prior to Orbeon Forms 2022.1. With Orbeon Forms 2022.1 and newer, they cause an error.

See [Initial data posted to the New Form page](/configuration/properties/form-runner-detail-page.md#initial-data-posted-to-the-new-form-page).

### `xf:load` handling of `xxf:show-progress`

Before Orbeon Forms 2022.1, when loading a `javascript:` URI, `xf:load` was ignoring the value of the `xxf:show-progress` attribute, and always behaving as if the attribute was set to `false`. Instead, starting with Orbeon Forms 2022.1, if you don't specify `xxf:show-progress`, it defaults to `false` for `javascript:` URIs, and to `true` otherwise.

This allows you to keep the progress indicator when using `xf:load` to run JavaScript that loads a page. Conversely, in the unlikely case you had some code doing a `<xf:load resource="javascript: …" xxf:show-progress="true"/>` but didn't want the progress indicator to be kept, then you will need to either remove the `xxf:show-progress="true"` or change the value of the attributes to `false`.