> [[Home]] ▸ Contributors ▸ [[Test Plan|Contributors ~ Test Plan]]

## Database service

- setup db
    - use MySQL on RDS (`jdbc:mysql://mysql.c4pgtxbv1cuq.us-east-1.rds.amazonaws.com:3306/orbeon?useUnicode=true&amp;characterEncoding=UTF8`)
    - set datasource in `server.xml`
    - create test table + data row if doesn't exist (can use IntelliJ Database tools)

    ```sql
    create table orbeon_address_book (
      id      integer not null primary key,
      first   varchar(255) not null,
      last    varchar(255) not null,
      phone   varchar(255) not null
    );
    insert into orbeon_address_book values(1, "John", "Smith", "5551231234");
    insert into orbeon_address_book values(2, "Mary", "Smith", "5551111111");
    ```
- create db service

    ```sql
    SELECT * FROM orbeon_address_book
    WHERE id = <sql:param type="xs:string" select=""/>
    ```
- create action
    - sets service value on request for param `1`
    - sets control values on response, e.g. `concat(/*/*/first, /*/*/last)`
    - set itemset values on response
        - `/*/*`
        - `concat(first, ' ', last)`
        - `id`

## HTTP service

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