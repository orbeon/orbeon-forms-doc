## Extension XForms events

### xxforms-upload-start

- Dispatched in response to: upload started on the client
- Target: `<xforms:upload>` element
- Bubbles: Yes
- Cancelable: Yes
- Context Info: None

### xxforms-upload-cancel

- Dispatched in response to: upload canceled by user on the client
- Target: `<xforms:upload>` element
- Bubbles: Yes
- Cancelable: Yes
- Context Info: None

### xxforms-upload-done

- Dispatched in response to: upload completed on the server
- Target: `<xforms:upload>` element
- Bubbles: Yes
- Cancelable: Yes
- Context Info: None

### xxforms-upload-error

- Dispatched in response to: upload ended with an error
- Target: `<xforms:upload>` element
- Bubbles: Yes
- Cancelable: Yes
- Context Info:
    - `event('xxf:error-type') as xs:string`
        - `size-error` if the upload was too large
        - `upload-error` if the cause of the error is unknown
        - *NOTE: other error types may be added in the future*
    - `event('xxf:permitted') as xs:integer?`
        - if `size-error`, and if known, maximum upload size allowed by configuration
    - `event('xxf:actual') as xs:integer?`
        - if `size-error`, and if known, actual upload size detected
