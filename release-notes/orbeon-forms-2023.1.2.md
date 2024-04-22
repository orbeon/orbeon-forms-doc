# Orbeon Forms 2023.1.2

__Tuesday, April xx, 2024__

Today we released Orbeon Forms 2023.1.1 PE! This maintenance release contains bug-fixes and minor new features and is recommended for all users of:

- [Orbeon Forms 2023.1.1 PE](orbeon-forms-2023.1.1.md)
- [Orbeon Forms 2023.1 PE](orbeon-forms-2023.1.md)

This release addresses the following issues since [Orbeon Forms 2023.1.1 PE](orbeon-forms-2023.1.1.md):

- Security
    - Upgrade to TinyMCE 6 #6249
- Accessibility
    - Multiple elements with `id="xforms-select-full-template"` and `id="xforms-select1-full-template"` #6224
    - `role` and ARIA attributes for radio buttons and checkboxes in static readonly #6235
    - Don't produce `role="textbox"` on `input` for date and time #6270
- Form Builder
    - Type with custom error message must not store `xs:string` #6252
    - "Test Offline" uses inline script #6242
- Form Runner
    - `ORBEON.fr.API.wizard.focus()` to support the `lax` and `strict` modes #6240
    - Excel export: Boolean `xf:input` doesn't export possible item values #6244
    - Import: validate that Boolean values are `false`/`true` but reject `0`/`1` #6250
    - Requests to persistence API not taking `oxf.http.forward-headers` into account #6255
    - Dropdown part in Dropdown with "Other" overflowing when labels are very long #6233
    - Ability to configure the plain dropdown with search with a minimum input length #6245
    - Rapid changes on Angular component can lead to multiple forms being embedded in the page #6251
    - JavaScript error when going to "All Form Controls" review page #6256
    - Improvements to configuration warning banner #6267
    - Don't create session when loading font files and for `OPTIONS` #6019
    - Radio button, checkboxes incorrectly checked #6272
    - `StackOverflowError` with complex form #6260

You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page.

Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon), [Mastodon](https://mastodon.social/@orbeon), or the [forum](https://www.orbeon.com/community).

We hope you enjoy this release!
