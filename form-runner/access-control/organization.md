# Organization-based permissions

Let's consider that your company has the following hierarchical organizational structure. We refer to each box in this diagram as an *organization*, e.g the *Engineering organization*.

![Organization hierarchy](../images/organization-hierarchy.png)

## Roles can be tied to organizations

With organization-based permissions. Users can have roles that are not just *global*, but *tied to an organization*. For instance, consider that a form author defines that for a given form, users with the role `admin` can read, update, and delete any form data. You might have admins who should get this permission company-wide. But you might also want to restict that permissions to form data created by users in certain parts of the company; for instance, a given user might be "admin for the Engineering organization", and she should only have the aformentioned permissions on form data created by users who are either directly in the Engineering organization, or any of its children organizations. This is particularly relevant for roles that are inherently tied to an organization, like "manager", where you're likely to want to say that the permissions you grant to a manager are limited to the data created by the people they manage.

## Permissions apply to sub-organizations

As alluded to in the previous section, if the form author grants permissions for a given role, and that a user has this role for a given organization, then the user is granted those permissions on data created by users in that organization, as well as users in all its sub-organizations.

Say you have an "expense report" form, and that the form author granted the right to managers to access data created with that form. Say, Tom, in the iOS organization creates an expense report. Then, his manager, Mary will be able to access it, and so will John, the VP of engineering, defined in the system as manager of the "Engineering" organization, and so will Carla, the CEO, defined in the system as manager of Acme, which sits at the root of the organizational structure.

![Transitive permissions](../images/organization-transitive.png)

## Information about users

To be able to apply permissions defined by form authors, Orbeon Forms needs to know, for each user:

- What organizations this user is a member of. Orbeon Forms supports users being a member of zero, one, or more organizations. For instance, Linda could be a member of the "iOS" and "Support" organizations.
- What organization roles a user has. Here again, a user can have zero, one, or more organization roles. Also, those roles don't need to be tied to the organizations the user is a member of. For instance, a user in the "IT" organization could have the role `admin` for the "HR" organization.

## How information about users is passed to Orbeon Forms

How does Orbeon Forms know what organizations a user is a member of, and what organization roles that user has? This information is passed to Orbeon Forms in JSON format through an HTTP header.

### With the Liferay proxy portlet

Liferay provides a UI for admins to [manage users](https://dev.liferay.com/discover/portal/-/knowledge_base/7-0/user-management), assign roles to users, and it also supports [creating and assigning users to organizations](https://dev.liferay.com/discover/portal/-/knowledge_base/7-0/adding-and-managing-organizations). If you're using Orbeon Forms with Liferay, Orbeon Forms will automatically call Liferay's API to know about the current user, including their organization affiliation, and pass that information to Form Runner in the afordmentioned JSON format.

For Liferay's user information to be passed to Orbeon Forms, you need to:

1. Use the Liferay proxy portlet. (The Liferay full portlet doesn't support passing Liferay's user information to Orbeon Forms.)
2. In the [proxy portlet preferences](../link-embed/liferay-proxy-portlet.md#configure-the-proxy-portlet), check the box *Send Liferay user*.
3. Add to your `properties-local.xml` the [necessary properties](../link-embed/liferay-proxy-portlet.md#configuring-form-runner-to-use-liferay-user-information) so Orbeon Forms knows how to extract the user's information from the headers set by the Liferay proxy portlet.
4. Add to your `properties-local.xml` the following property, so form authors can assign rights to user designated as Organization Owner in Liferay:
```xml
<property as="xs:string" name="oxf.fb.permissions.role.always-show">
    ["Organization Owner"]
</property>
```
    
