> [[Home]] ▸ Contributors ▸ [[Test Plan|Contributors ~ Test Plan]]

See [#1938](https://github.com/orbeon/orbeon-forms/issues/1938).

- scenario 1
  - load page
  - remove JSESSIONID
  - do Ajax update
  - server must respond with XML error document
      - as of 4.9, says "An error has occurred", BUT response does have "Session has expired. Unable to process incoming request."
  - client must show error dialog
  - check logs don't show full exception
- scenario 2
  - same but with other error
      - WHICH ONE?
  - same result except that exception must be logged
