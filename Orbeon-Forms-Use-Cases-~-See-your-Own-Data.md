> [[Home]] ▸ [[Orbeon Forms Use Cases| Orbeon Forms Use Cases]]

## Use case description

In this use case:

- Users are logged in (no anonymous data entry).
- Any logged in user can create and save data.
- Users cannot see other users' data.
- Users can come back to see their own data and modify it.
- Users can submit and email their form data.

## Implementing this use case with Orbeon Forms

### 1. Secure access to Orbeon Forms

There are many ways to do that. One of the easiest ways, if you don't already have infrastructure for that, is to use Tomcat and its `tomcat-users.xml` file. See also [[Access Control Setup| Form Runner ~ Access Control ~ Setup]].

### 2. Setup Owner/Group permissions for your forms.

See [[Owner Group| Form Runner ~ Access Control ~ Owner Group]]. Setup permissions this way:

- `Anyone` has `Create` permissions
- `Owner` can `Read`, `Update`, and possibly `Delete` data

### 3. Setup which buttons are visible on the form

See [[Buttons on the detail page| Form Runner ~ Configuration properties#buttons-on-the-detail-page]] to control which buttons appear on the page. You might want to add the `send` or `submit` button, in particular.

### 4. Specify what to do when the Send or Submit button is pressed

Orbeon Forms is quite flexible when it comes to configuring form submission. See [[Buttons-and-Processes|Form Runner ~ Buttons and Processes]].

For example, you can:

- send data to an external service with the `send` action
- email form data with the `email` action
- and much more
