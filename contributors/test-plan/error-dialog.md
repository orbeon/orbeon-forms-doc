

See [#1938](https://github.com/orbeon/orbeon-forms/issues/1938).

- scenario 1
  - load page
  - remove JSESSIONID
  - do Ajax update
  - server must respond with XML error document (be aware of [#2212](https://github.com/orbeon/orbeon-forms/issues/2212))
  - client must show error dialog
  - check logs don't show full exception
- scenario 2
  - same but with other error
      - WHICH ONE?
  - same result except that exception must be logged
