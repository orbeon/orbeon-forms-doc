

If when accessing the Form Runner summary page, the data you're seeing looks garbled, then run the following in on your MySQL database:

```sql
alter table orbeon_form_definition change xml xml mediumtext collate utf8_unicode_ci;
alter table orbeon_form_data       change xml xml mediumtext collate utf8_unicode_ci;
```

This instructs MySQL to use the `utf8_unicode_ci` collation instead of the default `utf8_bin`, and fixes this issue ([#1607]). Note that no data was lost; data was always safe in the database, and this only impacted how it was shown on the summary page. The [MySQL DDL for Orbeon Forms 4.5][mysql-45-ddl] has been updated after the 4.5 release, so if you're today installing Orbeon Forms 4.5, you can safely use that DDL and don't need to run the above commands.

  [#1607]: https://github.com/orbeon/orbeon-forms/issues/1607
  [mysql-45-ddl]: https://github.com/orbeon/orbeon-forms/blob/master/src/resources/apps/fr/persistence/relational/ddl/mysql-4_5.sql
