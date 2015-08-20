> [[Home]] ▸ Contributors ▸ [[Test Plan|Contributors ~ Test Plan]]

- create new form
- insert sections, grids, repeated grids
- rename sections and controls
    - check renamed in dialog [automated for 4.8, see [`BasicControls`](https://github.com/orbeon/orbeon-forms/blob/master/src/test/scala/org/orbeon/oxf/client/fb/BasicControls.scala)]
    - check renamed in source
- move sections
    - up/down
    - right/left (subsections) (be aware of [#2031](https://github.com/orbeon/orbeon-forms/issues/2031))
- repeated grid
    - set min/max as ints
    - set min/max as XPath expressions, e.g. `1 + 2`
- make section repeated
    - insert/move/remove iterations
    - set min/max as ints
    - set min/max as XPath expressions, e.g. `1 + 2`
- set control label, hint, items
    - plain
    - HTML
    - check HTML label appears correct in summary page / search
- set control help ([lorem ipsum][2])
    - plain
    - HTML
    - check help icon appears when help is set, and disappears when help is blanked
- set section help
    - check help icon appears when help is set, and disappears when help is blanked ([#1160][3] is a known issue)
- cut/copy/paste
    - copy control with help, required, constraint, and warning
    - paste control
    - check in source that all elements have been renamed
      - including `$form-resources` references (see [#1820](https://github.com/orbeon/orbeon-forms/issues/1820))
      - including `@validation` and `xf:constraint/@id` (see [#1785](https://github.com/orbeon/orbeon-forms/issues/1785))
  - check that form runs and new control validates constraints properly
- set control validation
    - set custom error constraint and alert
    - set custom warning constraint and alert
    - set required
    - check that if control is required but empty, generic message shows, not constraint message ([#1829](https://github.com/orbeon/orbeon-forms/issues/1829))
    - check that if control is required but empty and there is an unmet constraint, generic message shows ([#1830](https://github.com/orbeon/orbeon-forms/issues/1830))
- set control MIPs and properties
    - check required star appears with required set to true()
    - check Show in Summary/Search work when form deployed
- set section MIPs
    - check show/hide based on control value e.g. $fortytwo = '42'
- edit/modify source
    - change e.g. control label
- image annotation control
  - create simple form and test works, saves, loads
- i18n (PE)
    - check en/fr/es/it/de (languages with full support)
    - switch FB language and check language changes
    - add language
    - edit label and items and switch languages
    - edit source and change top-level language, make sure language selector switches
    - remove language
    - [#1223][4]
        - add lang not fully supported (e.g. Afrikaans) , remove all other languages, enter some labels
        - Test and Publish/new -> must show Afrikaans labels, not blank
- FB Summary page
    - check that search in Summary page updates title/description when FR language is changed (e.g. on Bookshelf)
    - be aware of [#2348](https://github.com/orbeon/orbeon-forms/issues/2348)
- set form title/description
- test form
- save
- publish form
    - check that attachments are published too (e.g. attach static img, dynamic img, and PDF file attachment)
- warning dialog if attempt to close page when unsaved
- serialization/deserialization [#1894][5]
    - set properties  
    ```xml
    <property 
        as="xs:integer" 
        name="oxf.xforms.cache.documents.size"    
        value="1"/>
    <property 
        as="xs:integer" 
        name="oxf.xforms.cache.static-state.size" 
        value="1"/>
    ```
    - visit http://localhost:8080/orbeon/fr/orbeon/builder/new
    - enter a/a to go to editor
    - visit http://localhost:8080/orbeon/fr/orbeon/contact/new
    - back to http://localhost:8080/orbeon/fr/orbeon/builder/new
    - insert control
    - Check there is no JS error

[2]: http://www.lipsum.com/
[3]: https://github.com/orbeon/orbeon-forms/issues/1160
[4]: https://github.com/orbeon/orbeon-forms/issues/1223
[5]: https://github.com/orbeon/orbeon-forms/issues/1894