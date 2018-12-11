# Login & Logout

[SINCE Orbeon Forms 2018.2]

## Menu

Form Runner can show a *user menu* in the navigation bar, allowing users to login, logout, and, if necessary, register. You can enable the user menu by setting the following property to `true` (its default value is `false`):

```xml
<property
    as="xs:boolean"
    name="oxf.fr.authentication.user-menu.enable"
    value="true"/>
```

When enabled, before users login, the can contain 2 entries: "Login" and "Register", as shown in the screenshot below.

![User menu when logged out](../images/logout-logout-menu-logged-out.png)

Conversely, when users are logged in, the menu will show who the user is, as well as provide a way for users to logout.

![User menu when logged in](../images/logout-logout-menu-logged-in.png)

You can configure what page users will be taken to when they select "Login", "Logout", or "Register" by setting the properties below, shown here with their default value. Setting a property to the empty string disables hides the corresponding entry in the menu.

```xml
<property
    as="xs:string"
    name="oxf.fr.authentication.user-menu.uri.login"
    value="/fr/auth?source={xxf:get-request-path()}"/>
<property as="xs:string"
    name="oxf.fr.authentication.user-menu.uri.logout"
    value="/fr/logout"/>
<property as="xs:string"  
    name="oxf.fr.authentication.user-menu.uri.register"
    value=""/>
```