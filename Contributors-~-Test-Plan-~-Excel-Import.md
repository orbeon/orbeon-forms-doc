> [[Home]] ▸ Contributors ▸ [[Test Plan|Contributors ~ Test Plan]]

- http://localhost:8080/46pe/fr/orbeon/contact/import
- import small doc first (`contact5.xlsx` on Dropbox)
  - check 2 out of  5 docs invalid
  - continue and check import passes: 3 documents were imported
- import larger document (`contact300.xlsx`)
  - check 120 out of 300 docs invalid
  - continue and check import passes: 180 documents were imported
- check % and ETA progress during validation and import
- check import completes