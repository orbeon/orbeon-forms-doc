> [Wiki](Home) ▸ Contributors ▸ [Test Plan](./Contributors-:-Test-Plan)

- see also acme/library with 1 service above
- create HTTP service AND database service which
    - database service
        - db
            - restore MySQL snapshot on RDS, call it orbeonmysql
            - set datasource in server.xml
            - create test table + data row if doesn't exist (can use IntelliJ Database tools)
        - start with sample form and scenario from [#1230][2]
        - sets service values on request
        - sets control values on response
        - set itemset values on response 
            - /*/*
            - concat(first, ' ', last)
            - id
    - HTTP service
        - using echo service is ok
            - POST to /fr/service/custom/orbeon/echo
        - test
            - call service upon form load and set control value upon response
            - same with button activation
            - same but set service values on request from control
            - set itemset values on response, e.g. use:

            ```xml
            <items>
                <item label="Foo" value="foo"/>
                <item label="Bar" value="bar"/>
            </items>
            ```