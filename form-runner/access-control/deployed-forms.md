# Access Control for Deployed Forms

<!-- toc -->

## Enabling permissions

For forms created in Form Builder, you can restrict which users can access which forms, and what operations they can perform. Those restrictions apply to the forms you create once they are deployed, not to editing those forms in Form Builder (for the latter, see the section that follows: _Access control for editing forms_).

By default, no restriction is imposed on _who_ can do _what_ with forms you create in Form Builder. You enable permissions by going to the Form Builder sidebar, and under _Advanced_, clicking on _Set Permissions_.

![](images/fb-advanced-menu.png)

This shows the following dialog:

![](images/fb-permissions-enable.png)

## Setting permissions

After you click on the checkbox, you'll be able to set access restriction on the _create_, _read, update_, and _delete_ operations:

1. On the _Anyone_ line, set the operations allowed to all users.
2. On the _Owner_ line, set the operations allowed to the user who created the data. [SINCE Orbeon Forms 4.3]
3. On the_Group members_line, set the operations allowed to users in the same group as the owner. [SINCE Orbeon Forms 4.3] 
4. On the following lines, you can enter a role name, and define what operations users with that role can perform.

## Example

In the example below:

* Any user to fill out a new form.
* Users with the role _clerk_to read data.
* Users with the role _admin_ to do any operation, including deleting form data.

![](images/fb-permissions-dialog.png)

## Permissions dialog

* Permissions you set in the dialog are _additive_ –Say you defined permissions for two roles, where users with the_reader_role can read and users in the_clerk_role can delete, users with both roles (_reader_and_clerk_)are allowed to perform both operations (reading and deleting).
* Operation on _Anyone_ apply to all other rows – When you select a checkbox for a given operation on the first _Anyone_ row, that checkbox will be automatically checked and disabled so you can't change it, for any additional row, since you wouldn't want to authorize users with additional roles to perform less operations.
* _Update_ implies _read_ – On any row, if you check _update_, then _read_ will be automatically checked, as it wouldn't make sense to say that some users can update data, but can't read it, as when updating data, obviously, they must be shown the data they are updating.
* _Create_ can't be set for the _owner_ and _group members_ – The owner/group is a piece of information attached to existing form data, keeping track of the user who create the data, and the group in which this user is. This information is only known for existing data, so assigning the _create_ permission to the _owner_ or _group members_ doesn't make sense, and the dialog doesn't show that checkbox.
* Permissions for the _owner_ and _group members_ can be set independently– If you want data to be accessible only by people who created it, check read/update/delete for the owner but not for group members. If you want data to be accessible by all people in the same group,check read/update/delete for the group members and don't check them for the owner if you want the owner to lose access to that data in case the owner changes group. (The latter highlights the need for permissions owner and group member to be set independently.)

## Permissions for owner / group members

See [[Owner and Group Member Permissions|Form Runner ~ Access Control ~ Owner Group]].

## Access restrictions

Which operations the current user can perform drives what page they can access, and on some pages which buttons are shown:

* On the Form Runner _home_ page, all the forms on which the current user can perform at least one operation are displayed. Then, for each one of those forms:

    * If they can perform the _create_ operation on the form, a link to the _new_ page is shown.
    * If they can perform any of the _read_, _update_, or _delete_ operation on the form, a link to the _summary_ page for that form is shown.
* For the _summary_ page:
    * Access is completely denied if the current user can't perform any of the _read_, _update_, or _delete_ operations.
    * The _delete_ button is disabled if the current user can't perform the _delete_ operation.
    * The _review_ and _pdf_ button are disabled if the current user can't perform the _read_ operation.
    * Clicking in a row of the table will open the form in _edit_ mode if the current user can perform the _update_ operation, in _view_ mode if they can perform the _read_ operation, and do nothing otherwise.

* For the _view_ page, access is denied if the current user can't perform the _read_ operation.
* For the _new_ page, access is denied if the current user can't perform the _create_ operation.
* For the _edit_ page, access is denied if the current user can't perform the _update_ operation.

[SINCE 4.3] In Orbeon Forms 4.2 and earlier, role-based permissions set in Form Builder could only be driven by container-based roles and the value of the `oxf.fr.authentication.method` property was not taken into account. Since version 4.3, those permissions also apply if you are using header-driven roles.
