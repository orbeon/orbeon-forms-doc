> [[Home]] ▸ [[Form Builder|Form Builder]]

## Overview

Say you want to populate a *Department* dropdown in your form using values stored in an `departments` table of your relational database.

## How-To

### 1. Connect with the database

Say you want to populate a *Department* dropdown in your form using values stored in an `departments` table of your relational database. You'll start doing this Form Builder's *Database Service Editor*. There, under *Datasource* you type the name of a datasource you setup in your application server. If you're using Tomcat, the simplest way of doing this is to edit Tomcat's `server.xml`, there a `<Context>` for Orbeon Forms if you don't have one already, and inside it add a `<Resource>` pointing to your database. On Tomcat, you also need put the database JDBC driver in Tomcat's `lib` directory.

![Form Builder to database](https://orbeon.mybalsamiq.com/mockups/3492353.png?key=5b6d8a77397e4b7de268cf14dea4e60c694555de)