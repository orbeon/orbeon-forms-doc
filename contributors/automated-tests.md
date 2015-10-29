# Automated Tests

## Removing leftover databases on SQL Server

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
