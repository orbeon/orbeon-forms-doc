# Organization-based permissions

## How Orbeon Forms knows about organizations

### With Liferay

Liferay provides a UI for admins to [manage users](https://dev.liferay.com/discover/portal/-/knowledge_base/7-0/user-management), assign roles to 	users, and it also supports [creating and assigning users to organizations](https://dev.liferay.com/discover/portal/-/knowledge_base/7-0/adding-and-managing-organizations) . If you're using Orbeon Forms with Liferay, Orbeon Forms will automatically call Liferay's API to know about the current user, including their organization affiliation, so no particular setup is required on your part.

