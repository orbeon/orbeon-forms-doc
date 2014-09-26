> [[Home]] ▸ Contributors ▸ [[Test Plan|Contributors ~ Test Plan]]

Features to test, with all supported browsers:

- XForms filter
    - http://localhost:8080/orbeon/xforms-jsp/guess-the-number/
    - http://localhost:8080/orbeon/xforms-jsp/flickr-search/
    - plain XHTML
        - move `flickr-search/index.jsp` into `resources/apps/xforms-jsp/guess-the-number/index.html`
        - edit that file to remove the JSP markup at the beginning, and hardcode the answer to 42
        - access http://localhost:8080/orbeon/xforms-jsp/guess-the-number/index.html
- examples-cli in distribution work (fix/remove them if not)
- check logs
    - no debug information
    - no unwanted information
    - be aware of [#849][1]

[1]: https://github.com/orbeon/orbeon-forms/issues/849