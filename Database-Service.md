> [[Home]] ▸ [[Form Builder|Form Builder]]

## Overview

Database services allow you to use data stored in a relational database, in your own table, for instance to dynamically populate a dropdown or other selection control, or to pre-populate fields based on a value entered by users.

## Populating a dropdown

In what follows, we'll see how you can populate a *Department* dropdown in your form using values stored in an `departments` table of your relational database.

### 1. Connect with the database

Say you want to populate a *Department* dropdown in your form using values stored in an `departments` table of your relational database. You'll start doing this Form Builder's *Database Service Editor*. There, under *Datasource* you type the name of a datasource you setup in your application server. If you're using Tomcat, the simplest way of doing this is to edit Tomcat's `server.xml`, there a `<Context>` for Orbeon Forms if you don't have one already, and inside it add a `<Resource>` pointing to your database. On Tomcat, you also need put the database JDBC driver in Tomcat's `lib` directory.

![Database Service - Connect to the database](https://orbeon.mybalsamiq.com/mockups/3492353.png?key=5b6d8a77397e4b7de268cf14dea4e60c694555de)

### 2. Write the SQL query

Still in the *Database Service Editor*, you write the SQL query to run in the database. When that query runs, Orbeon Forms creates an XML document with the data returned by the query, and you'll be referring to parts of that document when linking the database service to a specific dropdown. In essence, root element is always `<response>`, it contains one nested `<row>` element per row, which contains one element per column, with the element name being derived from the column name, and underscores replaced by dashes. So values for the `dept_no` column end up in the `<dept-no>` element.

![Database Service - Run a query](https://orbeon.mybalsamiq.com/mockups/3492410.png?key=5932ce2360c24e025c7089374d153a38e837d72c)

### 3. Link to the dropdown

To "link" the service to the dropdown, you create a new action, set it to run on *Form Load*, call the service you earlier named `list-departments`. With the result of the service you want to set the list of possible values of an a *Department* dropdown you have in the form. This is where you need to the XML document created based on what was returned by your SQL query, and you do this with 3 XPath expressions. The first points to the values, and will almost always be set to `/response/row` to match the format of the document created by Orbeon Forms. Next, you need to tell Orbeon Forms where it can find, inside the `<row>`, the label (the text shown to users) and the value (what is stored in data when users make a selection).

![Database Service - Link to the dropdown](https://orbeon.mybalsamiq.com/mockups/3495548.png?key=f4f2e69b9a6fa9f8b95b4374cd5d916e1d20021e)

### 4. User selects a value

Finally, when your form runs and users make a selection in the dropdown, the value you pointed to 

![Database Service - User selects value](https://orbeon.mybalsamiq.com/mockups/3495565.png?key=841eef62620e9825a0008b66db449881ef52faf0)