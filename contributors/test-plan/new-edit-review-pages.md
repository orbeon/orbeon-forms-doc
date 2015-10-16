

### Errors and warnings [4.10 DONE]

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

### Process buttons [4.10 DONE]

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

### Attachments/uploads [4.10 DONE]

- basic upload works
- removing uploaded file works
- large uploads fail (> 100 MB by default)
    - FR error dialog shows
    - control is back to empty
- very small (a few KB) upload works multiple times in a row
- with throttling (with Charles) (*NOTE: Proxy settings not really useful as we are looking at browser/server traffic.*)

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

### Submit [4.10 DONE]

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

### Email [4.10 DONE]

- NOTE: if using 2-factor auth w/ GMail, must use app-specific password for SMTP
    - https://accounts.google.com/b/0/IssuedAuthSubTokens#accesscodes
- config (change ebruchez@gmail.com in the following properties to your email)

```xml
<property as="xs:string"  name="oxf.fr.detail.buttons.*.*"           value="save email"/>
<property as="xs:string"  name="oxf.fr.email.smtp.host.*.*"          value="smtp.gmail.com"/>
<property as="xs:string"  name="oxf.fr.email.from.*.*"               value="ebruchez@gmail.com"/>
<property as="xs:string"  name="oxf.fr.email.to.*.*"                 value="ebruchez@gmail.com"/>
<property as="xs:string"  name="oxf.fr.email.smtp.username.*.*"      value="ebruchez@gmail.com"/>
<property as="xs:string"  name="oxf.fr.email.smtp.credentials.*.*"   value="**********"/>
<property as="xs:string"  name="oxf.fr.email.smtp.encryption.*.*"    value="tls"/>

<property as="xs:string"  name="oxf.fr.detail.buttons.orbeon.controls">
    refresh pdf email wizard-prev wizard-next review
</property>

<property as="xs:boolean" name="oxf.fr.email.attach-pdf.orbeon.controls"  value="true"/>
<property as="xs:boolean" name="oxf.fr.email.attach-tiff.orbeon.controls" value="true"/>
```
- /fr/orbeon/controls/new
- hit Email button
  - check email received
  - contains attachments, XML, PDF and TIFF [SINCE 4.11]
  - check attached PDF looks like PDF generated from detail page, including checkboxes/radio buttons, and images

### Misc [4.10 DONE]

- switch language
- open/close sections
- repeats
    - check can access repeated grid/section button and menu via keyboard navigation

### Noscript mode [4.10 DONE]

- orbeon/contact
- create form with `xxf:noscript-support="true"` in FB (just property doesn't work!)
- go to form with ?fr-noscript=true
- test w/ new form w/ image & file attachments
    - attachments work [NOTE: be aware of [#1405][3]]
- be aware of
    - [#2355](https://github.com/orbeon/orbeon-forms/issues/2355)
    - [#2356](https://github.com/orbeon/orbeon-forms/issues/2356)
- Contact form
    - Clear clears right away
    - PDF stays in tab
    - errors prevent saving
    - Refresh icon works

### Wizard [4.10 DONE]

- validated mode
    - `/fr/orbeon/w9/new`
    - check cannot click in TOC
    - check cannot navigate forward with error in current section
    - once all sections visited, can freely navigate
- /fr/orbeon/controls/new
    - test errors in section template are highlighted in TOC

### Captcha [4.10 DONE]

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

### Help popups/hint tooltips positioning [4.10 DONE]

- create form to test general positioning
  - help on all elements
  - repeats
  - hints on checkboxes/radios
- help: see [#1637][5]
- hints: see [#1649][6]
- test Bookshelf help
- create form with fields, including checkboxes/radio buttons

[2]: https://gist.github.com/ebruchez/5666643
[3]: https://github.com/orbeon/orbeon-forms/issues/1405
[5]: https://github.com/orbeon/orbeon-forms/issues/1637
[6]: https://github.com/orbeon/orbeon-forms/issues/1649
