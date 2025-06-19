# Orbeon Forms 2018.2.4 PE

__Thursday, November 21, 2019__

Today we released Orbeon Forms 2018.2.4 PE. This maintenance release contains bug-fixes and is recommended for all users of:

- [Orbeon Forms 2018.2.3 PE](https://blog.orbeon.com/2019/05/orbeon-forms-201823-pe.html)
- [Orbeon Forms 2018.2.2 PE](https://blog.orbeon.com/2019/03/orbeon-forms-201822-pe.html)
- [Orbeon Forms 2018.2.1 PE](https://blog.orbeon.com/2019/02/orbeon-forms-201821-pe.html)
- [Orbeon Forms 2018.2 PE](https://blog.orbeon.com/2018/12/orbeon-forms-20182.html)

This release addresses the following issues since [Orbeon Forms 2018.2.3 PE](https://blog.orbeon.com/2019/05/orbeon-forms-201823-pe.html):

- Form Builder
    - Per-form/global settings for minimal Calculated Value field (<a href="https://github.com/orbeon/orbeon-forms/issues/4008">#4008</a>)
    - "Never Collapsible" section must still be openable in Form Builder (<a href="https://github.com/orbeon/orbeon-forms/issues/4071">#4071</a>)
- Form Runner:
    - Avoid alert showing twice in the error summary (<a href="https://github.com/orbeon/orbeon-forms/issues/4066">#4066</a>)
    - New actions: continuations can run multiple times (<a href="https://github.com/orbeon/orbeon-forms/issues/4068">#4068</a>)
    - fr:dataset-write doesn't create the associated xf:instance (<a href="https://github.com/orbeon/orbeon-forms/issues/3995">#3995</a>)
    - Section titles must support HTML in the wizard (<a href="https://github.com/orbeon/orbeon-forms/issues/4089">#4089</a>)
    - Redirect users to right page, avoid overly aggressive caching (<a href="https://github.com/orbeon/orbeon-forms/issues/4094">#4094</a>)
    - Date and time controls
        - date/time/datetime fields in Summary page (<a href="https://github.com/orbeon/orbeon-forms/issues/4082">#4082</a>, <a href="https://github.com/orbeon/orbeon-forms/issues/3880">#3880</a>)
        - Fix legacy date with unspecified language (<a href="https://github.com/orbeon/orbeon-forms/issues/4103">#4103</a>)
        - Allow switching months, reacting to `change` not `blur` (<a href="https://github.com/orbeon/orbeon-forms/issues/4163">#4163</a>)
    - PDF fixes
        - Populate PDF from template with phone number (<a href="https://github.com/orbeon/orbeon-forms/issues/4118">#4118</a>)
        - Populate PDF from template with currency (<a href="https://github.com/orbeon/orbeon-forms/issues/4119">#4119</a>)
        - W-9 demo form: EIN doesn't show in PDF (<a href="https://github.com/orbeon/orbeon-forms/issues/4150">#4150</a>)
        - "Page break before" adds blank page (<a href="https://github.com/orbeon/orbeon-forms/issues/4151">#4151</a>)
        - PDF page content should have padding (<a href="https://github.com/orbeon/orbeon-forms/issues/4152">#4152</a>)
        - PDF: Empty page before long rich text area content (<a href="https://github.com/orbeon/orbeon-forms/issues/2573">#2573</a>)
    - Service response must ensure that main model recalculates (<a href="https://github.com/orbeon/orbeon-forms/issues/4178">#4178</a>)
    - Incorrect `xxf:repeat-position()` value just after iteration move (<a href="https://github.com/orbeon/orbeon-forms/issues/4183">#4183</a>)
    - On iOS, input field doesn't loose focus when users interact with signature field (<a href="https://github.com/orbeon/orbeon-forms/issues/4196">#4196</a>)
    - Order of `<xf:action type="javascript">` and `<xf:load>` not respected (<a href="https://github.com/orbeon/orbeon-forms/issues/4195">#4195</a>) 
    - DMV-14: page shows error when loading (<a href="https://github.com/orbeon/orbeon-forms/issues/4187">#4187</a>) 
    - Autosave happening for user having failed to acquire a lease (<a href="https://github.com/orbeon/orbeon-forms/issues/4200">#4200</a>)
    - Autosave doesn't happen as long as user keep changing data within `autosave-delay` (<a href="https://github.com/orbeon/orbeon-forms/issues/4205">#4205</a>)
    - Don't require `orbeon` class on the body (<a href="https://github.com/orbeon/orbeon-forms/issues/4273">#4273</a>)
- Other fixes
    - Upgrade to Xerces 2.12 (<a href="https://github.com/orbeon/orbeon-forms/issues/4139">#4139</a>)

You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page.  

Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon), [Bluesky](https://bsky.app/profile/orbeon.bsky.social), or the [forum](https://groups.google.com/g/orbeon).

We hope you enjoy this release!
