# Automated Tests

## Levels of tests

Every build of Orbeon Forms runs a series of automated tests.

- unit tests
- integration tests, including
    - [replication](/installation/replication.md) feature
    - database tests
    
See also [RFE #2743](https://github.com/orbeon/orbeon-forms/issues/2743) for further plans.

## Misc

### Removing leftover databases on SQL Server

*Q: Is this relevant anymore?*

```sql
BEGIN
    DECLARE @qry nvarchar(max);
    SELECT @qry = 
        (SELECT 'DROP DATABASE ' + name + '; ' 
         FROM sys.databases 
         WHERE name LIKE 'orbeon_%_tomcat'
         FOR XML PATH(''));
    EXEC sp_executesql @qry;
END;  
```
