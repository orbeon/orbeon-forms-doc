## Usage

Owner/group-based are useful when you want users to only see their own data, or maybe also the data of other users in the same group. For Orbeon Forms to be able to show a user only her data, Orbeon Forms needs to know who that user is, and hence this can only be used for authenticated users.

To use this feature for a form, in Form Builder, when editing a form, open the *Permissions* dialog, and check boxes on the *Owner* and *Group member* lines as appropriate for your situation.

![Permissions dialog](images/fr-permissions-dialog.png)

## Availability

This feature is supported:

- Since 4.3 on MySQL and DB2
- Since 4.4 on Oracle
- Since 4.6 on SQL Server
- Since 4.8 on eXist-db and PostgreSQL

For eXist, you also need to set the following property:

```xml
<property as="xs:string"
          name="oxf.xforms.forward-submission-headers"
          value="Orbeon-Username Orbeon-Roles Orbeon-Group"/>
```
On 4.3, this feature was not enabled by default. It is enabled by default since 4.4, so if you're using 4.4 or newer, you don't need to worry about this. But if you're specifically on 4.3, you should set the following 2 properties if you want enable owner/group-based permissions:

```xml
<property as="xs:boolean" name="oxf.fr.support-owner-group" value="true"/>
<property as="xs:boolean" name="oxf.fr.support-autosave"    value="true"/>
```