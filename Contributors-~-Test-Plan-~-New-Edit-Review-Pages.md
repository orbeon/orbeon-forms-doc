> [[Home]] ▸ Contributors ▸ [[Test Plan|Contributors ~ Test Plan]]

- errors and warnings [4.9 done]
    - FB: create form 
        - required field
        - non-required field
        - field with 1 warning and 1 info
    - FR: error summary
        - shows errors, warning and info
        - links and focuses to controls, including XBL controls, but not invalid output controls
    - FR: review page if no errors
    - FR: review page shows review dialog if warning or info
    - FR: come back to review page
- process buttons
    - create and publish guest/test
    - add [these properties][2], and add `review` the list of buttons in the first property
    - check standard behavior of buttons
        - save-draft
            - can save w/ invalid data
        - save and save-final
            - cannot save w/ invalid data
        - submit
            - echoes PDF URL (try to download)
        - save 2
            - echoes XML
        - home/summary/edit/review
    - send w/ replace all/none
        - set acme.submit.replace to none
        - must not navigate after submit
- attachments/uploads
    - basic upload works
    - removing uploaded file works
    - large uploads fail (> 100 MB by default)
        - FR error dialog shows
        - control is back to empty
    - very small (a few KB) upload works multiple times in a row
    - with throttling (with Charles)

        ```xml
        <property
            as="xs:string"
            name="oxf.http.proxy.host"
            value="localhost"/>
        <property
            as="xs:integer"
            name="oxf.http.proxy.port"
            value="8888"/>
        ```
        - cancel midway works
        - progress indicator works
- submit
    - comment out custom submit button process (`oxf.fr.detail.process.submit`) in properties
    - config

    ```xml
    <property
        as="xs:string"
        name="oxf.fr.detail.submit.go.uri-xpath.*.*"
        value="'http://xformstest.org/cgi-bin/echo.sh'"/>
    <property
        as="xs:string"
        name="oxf.fr.detail.buttons.*.*"
        value="home summary review save-draft save-final save submit workflow-send"/>
    ```
    - FR: in new page, click Submit then
        - clear and close
        - keep values and close
        - OK: goes to echo page
        - close window [NOTE: Only if window was open with JS.]
- email
    - NOTE: if using 2-factor auth w/ GMail, must use app-specific password for SMTP
        - https://accounts.google.com/b/0/IssuedAuthSubTokens#accesscodes
    - NOTE: doesn't work if doc hasn't been saved
    - config (change ebruchez@gmail.com in the following properties to your email)

    ```xml
    <property as="xs:string"  name="oxf.fr.detail.buttons.*.*"           value="save email"/>
    <property as="xs:string"  name="oxf.fr.email.smtp.host.*.*"          value="smtp.gmail.com"/>
    <property as="xs:string"  name="oxf.fr.email.from.*.*"               value="ebruchez@gmail.com"/>
    <property as="xs:string"  name="oxf.fr.email.to.*.*"                 value="ebruchez@gmail.com"/>
    <property as="xs:string"  name="oxf.fr.email.smtp.username.*.*"      value="ebruchez@gmail.com"/>
    <property as="xs:string"  name="oxf.fr.email.smtp.credentials.*.*"   value="**********"/>
    <property as="xs:string"  name="oxf.fr.email.smtp.encryption.*.*"    value="tls"/>
    ```
    - hit send button
      - check email received
      - contains XML and PDF
      - check attached PDF looks like PDF generated from detail page, including checkboxes/radio buttons, and images
- switch language
- open/close sections
- repeats
  - check can access repeated grid/section button and menu via keyboard navigation
- noscript mode
    - orbeon/contact
    - create form with `xxf:noscript-support="true"` in FB (just property doesn't work!)
    - go to form with ?fr-noscript=true
    - test w/ new form w/ image & file attachments
        - attachments work [NOTE: be aware of [#1405][3]]
- wizard
    - add property
        - `<property as="xs:string"  name="oxf.fr.detail.view.appearance.*.*" value="wizard"/>`
    - test errors in section template are highlighted in TOC (be aware of [#943][4])
- captcha
    - enable with property
    
    ```xml
    <property
        as="xs:string"
        name="oxf.fr.detail.captcha.*.*"
        value="reCAPTCHA"/>
    <property
        as="xs:string"
        name="oxf.xforms.xbl.fr.recaptcha.public-key"
        value="6LesxAYAAAAAAEF9eTyysdkOF6O2OsPLO9zAiyzX"/>
    <property
        as="xs:string" 
        name="oxf.xforms.xbl.fr.recaptcha.private-key"
        value="6LesxAYAAAAAAJIXoxMvErqbisKkt7W-CPoE_Huo"/>
    ```
    - test reCAPTCHA [NOTE: had to fix 2 bugs with 4.5!]
    - test SimpleCaptcha
- help popups/hint tooltips positioning
    - create form to test general positioning
      - help on all elements
      - repeats
      - hints on checkboxes/radios
    - help: see [#1637][5]
    - hints: see [#1649][6]
    - test Bookshelf help
    - create form with fields, including checkboxes/readio buttons

[2]: https://gist.github.com/ebruchez/5666643
[3]: https://github.com/orbeon/orbeon-forms/issues/1405
[4]: https://github.com/orbeon/orbeon-forms/issues/943
[5]: https://github.com/orbeon/orbeon-forms/issues/1637
[6]: https://github.com/orbeon/orbeon-forms/issues/1649