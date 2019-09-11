# Organization-based permissions

[SINCE Orbeon Forms 2016.3]

## Roles can be tied to organizations

Let's consider that your company has the following hierarchical organizational structure. We refer to each box in this diagram as an *organization*, e.g the *Engineering organization*.

![Organization hierarchy](../images/organization-hierarchy.png)

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
    
### With other systems

If your information about users and organizations is stored in a system other than Liferay, it is then up to you to produce the JSON and pass it to Orbeon Forms through a header. For more about the JSON format expected by Form Runner, and how to tell Form Runner the name of the header you are using, see the section about [using a single header with JSON](users.md#if-using-a-single-header-with-json).

## Constraints

When a user fills out a form, when data for that form is saved, Form Runner also stores, along with the data, information about the user. This includes the username, but also the organizations the user is a member of, and the ancestors of those organizations. For instance, say Linda saves data, and the name of the organizations she is a member of, the `Support` and `iOS` organizations, along with their ancestors in the organizational structure, are stored in the database:

![Owner information stored with the form data](../images/organization-constraints.png)

If the form definition grants access to managers, and John is a "manager of Engineering", because of the information available and the way it is stored, Form Runner can efficiently determine what data John has access to. In this example, John can be granted access to Linda's data as Linda is a member of the `iOS` organization, which is under the `Engineering` organization, of which John is a manager.

The built-in implementation of the persistence API for relational databases stores information about organizations in the `orbeon_organization` table. Each organization has a unique `id` and is represented in `orbeon_organization` by as many rows as the depth of the organization. For instance, the `iOS` could be stored with id `123` as follows:

| id  | depth | pos | name        |
| --- | ----- | --- | ----------- |
| 123 | 3     | 1   | Acme        |
| 123 | 3     | 2   | Engineering |
| 123 | 3     | 3   | iOS         |

Organizations are created as needed when users save data. So if an entry for the `iOS` organization didn't already exist, the first time Linda saves data, the above rows will be created. However no id will be generated for the parent organizations; this will only happen when, say, John, the manager of the `Engineering` organization saves data, at which point rows for the `Engineering` organization will be created, for instance as follows:

| id  | depth | pos | name        |
| --- | ----- | --- | ----------- |
| 456 | 2     | 1   | Acme        |
| 456 | 2     | 2   | Engineering |

The way organizations are used and stored has the following consequences:

- If an organization name changes, for instance `Support` is renamed `Customer satisfaction`, then data in the database needs to be changed.
- If the organization structure changes, say `Support` isn't under `Engineering` but under `Operations`, then information in the database needs to be changed.
- If a user switches to another organization, existing data will still be tied to her previous organizations. Another way to look at it is that, by default, data stays with the organizations where it was created, irrelevant of where the user who created that data moves. This may or may not be what you want, depending on the scenario. For instance, say a user in organization A submits an expense report, and shortly after that moves to organization B. By default, it will still be the manager in organization A who will be in charge of approving that expense report. If instead you want to data to move along with the user, it is up to you to change the organization associated with the data for that user in the database.

## See also

- [Form Runner Liferay Proxy Portlet](/form-runner/link-embed/liferay-proxy-portlet.md)
- [Setup users for access control](users.md) - How to setup Orbeon Forms so that users and roles are provided.
- [Login & Logout](login-logout.md) - Optional user menu for providing links to login and logout functions.
- [Access control for deployed forms](deployed-forms.md) - How to control access to deployed forms.
- [Form fields](form-fields.md) - How to control access to specific form fields based on the user user's roles.
- [Access control for editing forms](editing-forms.md) - How to control access to Form Builder.
    - [Owner and group member permissions](owner-group.md) - Access based on ownership and groups.
- [Scenarios](scenarios.md)
