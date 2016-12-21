# Organization-based permissions

## How Orbeon Forms knows about organizations

### With the Liferay proxy portlet

Liferay provides a UI for admins to [manage users](https://dev.liferay.com/discover/portal/-/knowledge_base/7-0/user-management), assign roles to 	users, and it also supports [creating and assigning users to organizations](https://dev.liferay.com/discover/portal/-/knowledge_base/7-0/adding-and-managing-organizations) . If you're using Orbeon Forms with Liferay, Orbeon Forms will automatically call Liferay's API to know about the current user, including their organization affiliation.

For Liferay's user information to be passed to Orbeon Forms, you need to:

1. Use the Liferay proxy portlet. The Liferay full portlet doesn't support passing Liferay's user information to Orbeon Forms.
2. In the [proxy portlet preferences](../link-embed/liferay-proxy-portlet.md#configure-the-proxy-portlet), check the box *Send Liferay user*.
3. Add to your `properties-local.xml` the [necessary properties](../link-embed/liferay-proxy-portlet.md#configuring-form-runner-to-use-liferay-user-information) so Orbeon Forms knows how to extract the user's information from the headers set by the Liferay proxy portlet.
4. Add to your `properties-local.xml` the following property, so form authors can assign rights to user designated as organization owner in Liferay:
```xml
<property as="xs:string" name="oxf.fb.permissions.role.always-show">
    ["Organization owner"]
</property>
```
    
