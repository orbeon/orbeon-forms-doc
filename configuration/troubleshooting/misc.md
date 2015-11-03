# Misc troubleshooting

<!-- toc -->

## Session not found when running both Tomcat and WebLogic

This issue can also manifest itself with a dialog titled _Session has expired. Unable to process incoming request._ showing up as you try to interact with a form. This comes from the fact that Tomcat and WebLogic handle the `JSESSIONID` cookie used to track sessions differently:

* Tomcat creates one `JSESSIONID` per web application, with the cookie path set to the context of the application. When an application invalidates the session, Tomcat sends a new `JSESSIONID` to the browser.
* WebLogic stores one cookie `JSESSIONID` with cookie path `/` for all the applications. This cookie doesn't change when a session is invalidated, and hence there is no one-to-one mapping between a `JSESSIONID` cookie and a session in WebLogic.
The error can happen when:
  1. You first access your application deployed on `/myapp` with Tomcat. Tomcat sets a `JSESSIONID` cookie for `/myapp`.
  2. You then access your application on the same server deployed on `/myapp` with WebLogic. Tomcat sets a `JSESSIONID` cookie for `/`.
  3. In subsequent requests, the browser sends the Tomcat `JSESSIONID` as it is more specific (for `/myapp` instead of just `/`), but WebLogic doesn't recognize it, hence the error you're getting.

The solution is simply to clear in your browser all the `JSESSIONID` cookies for the host you are trying to access.

## Data looks garbled on the summary page with MySQL

If when accessing the Form Runner summary page, the data you're seeing looks garbled, then run the following in on your MySQL database:

```sql
alter table orbeon_form_definition change xml xml mediumtext collate utf8_unicode_ci;
alter table orbeon_form_data       change xml xml mediumtext collate utf8_unicode_ci;
```

This instructs MySQL to use the `utf8_unicode_ci` collation instead of the default `utf8_bin`, and fixes this issue ([#1607]). Note that no data was lost; data was always safe in the database, and this only impacted how it was shown on the summary page. The [MySQL DDL for Orbeon Forms 4.5][mysql-45-ddl] has been updated after the 4.5 release, so if you're today installing Orbeon Forms 4.5, you can safely use that DDL and don't need to run the above commands.

  [#1607]: https://github.com/orbeon/orbeon-forms/issues/1607
  [mysql-45-ddl]: https://github.com/orbeon/orbeon-forms/blob/master/src/resources/apps/fr/persistence/relational/ddl/mysql-4_5.sql
