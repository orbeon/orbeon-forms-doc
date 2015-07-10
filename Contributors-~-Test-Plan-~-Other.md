> [[Home]] ▸ Contributors ▸ [[Test Plan|Contributors ~ Test Plan]]

Features to test, with all supported browsers:

- give CE version a quick run
- XForms filter
    - http://localhost:8080/orbeon/xforms-jsp/guess-the-number/
    - http://localhost:8080/orbeon/xforms-jsp/flickr-search/
- examples-cli in distribution work (fix/remove them if not)
    - `unzip orbeon-4.7.0.201409262231-PE.zip`
    - `cd orbeon-4.7.0.201409262231-PE`
    - `unzip -d orbeon orbeon.war`
    - `java -jar orbeon/WEB-INF/orbeon-cli.jar examples-cli/simple/stdout.xpl`
    - `java -jar orbeon/WEB-INF/orbeon-cli.jar examples-cli/transform/transform.xpl`
- check logs
    - no debug information
    - no unwanted information
    - be aware of [#849][1]
- Test sample forms on iOS

[1]: https://github.com/orbeon/orbeon-forms/issues/849