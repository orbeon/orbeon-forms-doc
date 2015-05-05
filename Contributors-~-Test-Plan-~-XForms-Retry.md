> [[Home]] ▸ Contributors ▸ [[Test Plan|Contributors ~ Test Plan]]

## 1. Retry happens

- edit `resources/apps/xforms-sandbox/samples/dispatch-delay.xhtml`
    - change sleep service to use `sleep?delay=10` (sleep 10 s)
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
    - in Chrome, open the Dev Tools, go to the Network tab (or use HttpScoop or Charles)
    - hit the *Manual save* button
    - check after ~10 seconds that the Ajax response succeeds with 200 (retry will return with 503 until the 10 s have elapsed)
    - can also hit the *Start* button, and notice the number incrementing after ~10s
    - (the loading indicator doesn't show while a retry is not in progress, which is somewhat unintuitive, but we'll fix this as part of [#1114][2])
        
## 2. Request not reaching server

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
    <property
        as="xs:string"
        name="oxf.http.proxy.host"
        value="localhost"/>
    <property
        as="xs:integer"
        name="oxf.http.proxy.port"
        value="8888"/>
    ```
- load page again
- using Charles, go in Proxy / Breakpoints, enable breakpoints, and add:  
  ![][3]
- click on *Manual save*
- the request is intercepted by Charles where you click on Abort, check that the client retries the request right away and that the request doesn't show in the server logs
- finally click on *Execute*, and check the request runs on the server, and the response reaches the browser after 5 s

## 3. Response not reaching client

- change back  sleep service to use `sleep?delay=5` (sleep 5 s)
- in Charles, edit the breakpoint set above (see screenshot), and this time break on the response, i.e. uncheck the "request" checkbox and check the "response" checkbox
- click on *Manual save*
  - check after 5 s the breakpoint is hit
  - Abort (make sure to abort Ajax response, not call to sleep service - no longer an issue with 4.7+)
  - check the request is made again right away by the browser and replayed right away by the server
  - *Execute*
  - check the response reaches the client
      
## 4. Unexpected HTML response

- change back  sleep service to use `sleep?delay=5` (sleep 5 s)
- edit the response to contain non-valid XML, and *Execute*
- check the client re-executes the request

## 5. File upload

- setup
    - enable breakpoint on response for `/49pe/xforms-server/upload`
    - enable throttling in Charles per the following configuration  
      ![][4]
    - download [this image][5] (~200 KB)
- http://localhost:8080/49pe/xforms-upload/
- select image, and upload start in the background
- abort the response to the background upload
- check it interrupts the download (we're not retrying uploads) and message says "There was an error during the upload."

[1]: ./images/test-chrome-timeline.png
[2]: https://github.com/orbeon/orbeon-forms/issues/1114
[3]: ./images/test-charles-request.png
[4]: ./images/test-charles-throttling.png
[5]: http://placekitten.com/g/2000/2000