# Database services

<!-- toc -->

## Overview

By using *database services*, a [PE feature](https://www.orbeon.com/pricing), you can use data stored in any table of a relational database, for instance to dynamically populate a dropdown, or to pre-populate fields based on a value entered by users.

## Populating a dropdown

In what follows, we'll see how you can populate a *Department* dropdown in your form using values stored in an `departments` table of your relational database.

![Dropdown](images/database-services-dropdown.png)

### 1. Connect with the database

You start, in Form Builder, by creating a new database service, clicking *Add* in the left sidebar under *Database Services*. This opens the *Database Service Editor*.

Under *Datasource* you type the name of a datasource you setup in your application server. This is the the JNDI name of the datasource, without the `jdbc/` part. If you type `employees`, Orbeon Forms will look for `java:comp/env/jdbc/employees`.

If you're using Tomcat, the simplest way of setting up a datasource is to edit Tomcat's `server.xml`, there add a `<Context>` for Orbeon Forms if you don't have one already, and inside it add a `<Resource>` pointing to your database. On Tomcat, you also need to put the database JDBC driver in Tomcat's `lib` directory.

![Connect to the database](images/database-services-connect-db.png)

### 2. Write the SQL query

Still in the *Database Service Editor*, you write the SQL query to run in the database. When that query runs, Orbeon Forms creates an XML document with the returned data, and you'll be referring to parts of that document when linking the database service to a specific dropdown. In essence, that document has a root element which is always `<response>`, it contains one nested `<row>` per row, which in turn contains one element per column, with the element name being derived from the column name converted in lowercase and underscores replaced by dashes. So values for the `dept_no` column end up in `<dept-no>`.

![Run a query](images/database-services-run-query.png)

### 3. Link the dropdown

To "link" the service to the dropdown, you create a new action, set it to run on *Form Load*, call the service you earlier named `list-departments`. With the result of the service you want to set the list of possible values of an a *Department* dropdown you have in the form. This is where you extract data from the XML document seen earlier, and you do this with 3 XPath expressions. The first points to the "rows", and will almost always be `/response/row`. Next, you need to tell Orbeon Forms where it can find, inside the `<row>`, the label (the text shown to users) and the value (what is stored in data when users make a selection).

![Link to the dropdown](images/database-services-link-to-dropdown.png)

### 4. Select a value

Finally, when your form runs and users make a selection in the dropdown, what you defined to be the *value*, here the content of the `<dept-no>` element is used to populate the element corresponding to the field in the form data.

![User selects value](images/database-services-link-to-dropdown.png)

## Populating fields using another field value

Say that when users enter a value in *employee number*, you want to lookup the corresponding employee in your database and populate other fields, *First name* and *Last name*, based on the information you find about that employee. We've already know how to establish a [connection with the database](#1-connect-with-the-database), so let's start by seeing how we can use the value of a field in a SQL query.

![Populate fields](images/database-services-poulate-fields.png)

### 1. Set a service parameter

Your SQL query can contain *parameters*. Those look like: `<sql:param type="xs:string" select=""/>`. The `type` attribute corresponds to the SQL type to use (e.g. `xs:string`, `xs:decimal`, …). The `select` attribute must be left blank; it is filled-out by the Actions Editor when the service is called.

When the query runs, the value of each parameter is set to the current value of a form field, and you define the mapping between parameter in the SQL query and form field when you create an action. That mapping is done by position; e.g. in the above query, you'll want to set parameter 1 to the control containing an employee id.

![Set service parameter](images/database-services-set-service-parameter.png)

### 2. Set control values

Finally, you use the *Set Response Control Values* section of the *Actions Editor* dialog to extract the information you're interested in from the XML produced based on the result from the query, and populate fields in the form.

![Set control values](images/database-services-set-control-values.png)

## See also

- [HTTP services](http-services.md)
- [Actions](actions.md)