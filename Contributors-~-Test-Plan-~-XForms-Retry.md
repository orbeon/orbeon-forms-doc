> [[Home]] ▸ Contributors ▸ [[Test Plan|Contributors ~ Test Plan]]

- check retry happens
    - edit `resources/apps/xforms-sandbox/samples/dispatch-delay.xhtml`
        - change sleep service to use `sleep?delay=20` (sleep 20 s)
        - add to model
        ```xml
        <xf:setvalue
            event="xforms-submit-done"
            ref="/instance/count"
            value=". + 1"/>
        ```
    - set the following properties

        ```xml
        <property
            as="xs:integer"
            name="oxf.xforms.delay-before-ajax-timeout"
            value="2000"/>
        <property
            as="xs:integer"
            name="oxf.xforms.retry.delay-increment"    
            value="2000"/>
        ```
    - open 
        - http://localhost:8080/orbeon/xforms-sandbox/sample/dispatch-delay
        - in Chrome, open the Dev Tools, go to the Network tab
        - hit the *Manual save* button
        - check after 20 seconds that the Ajax response succeeds with 200 (retry will return with 503 until the 20 s have elapsed)
        - check in the Dev Tools the requests look as follows
        - (the loading indicator doesn't show while a retry is not in progress, which is somewhat unintuitive, but we'll fix this as part of [#1114][2])
- test request not reaching server
    - change back  sleep service to use `sleep?delay=5` (sleep 5 s)
    - set the following properties

        ```xml
        <property
            as="xs:integer"
            name="oxf.xforms.delay-before-ajax-timeout"
            value="30000"/>
        <property
            as="xs:integer"
            name="oxf.xforms.retry.delay-increment"
            value="0"/>
        ```
    - load page again
    - using Charles, go in Proxy / Breakpoints, enable breakpoints, and add:  
      ![][3]
    - click on *Manual save*
    - the request is intercepted by Charles where you click on Abort, check that the client retries the request right away and that the request doesn't show in the server logs

    - finally click on *Execute*, and check the request runs on the server, and the counter is incremented in the browser after 5 s

- response not reaching client
    - change back  sleep service to use `sleep?delay=5` (sleep 5 s)
    - in Charles, edit the breakpoint set above (see screenshot), and this time break on the response, i.e. uncheck the "request" checkbox and check the "response" checkbox
    - click on *Manual save*
      - check after 5 s the breakpoint is hit
      - Abort (make sure to abort Ajax response, not call to sleep service!)
      - check the request is made again right away by the browser
      - *Execute*
      - check the value is incremented in the UI
- unexpected HTML response
    - change back  sleep service to use `sleep?delay=5` (sleep 5 s)
    - edit the response to contain non-valid XML, and *Execute*
    - check the client re-executes the request
- file upload
    - setup
        - disable Charles breakpoints
        - enable throttling in Charles per the following configuration  
          ![][4]
        - download [this image][5] (~300 KB, with this setup the upload will take about a minute)
        - enable the above breakpoint on response
    - http://localhost:8080/orbeon/xforms-upload/
    - select image, and upload start in the background
    - execute all the upload progress requests, but abort the final response to the background upload
    - check it interrupts the download (we're not retrying uploads)

[1]: ./images/test-chrome-timeline.png
[2]: https://github.com/orbeon/orbeon-forms/issues/1114
[3]: ./images/test-charles-request.png
[4]: ./images/test-charles-throttling.png
[5]: http://placekitten.com/g/2000/2000