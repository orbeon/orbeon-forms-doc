> [[Home]] ▸ Contributors ▸ [[Test Plan|Contributors ~ Test Plan]]

- deploy `orbeon-embedding.war` into Tomcat
- update `web.xml`:

    ```xml
    <init-param>
        <param-name>form-runner-url</param-name>
        <param-value>http://localhost:8080/49pe</param-value>
    </init-param>
    ````
- navigate to `http://localhost:8080/orbeon-embedding/`
- go through demo forms and test
  - enter data
  - Save
  - PDF
  - repeats
  - help/hints
  - uploads
  - *NOTE: There are limitations, for example navigation (Summary, Review) won't work.*