# Workflows

## About workflows

Forms are often participate in a *workflow*, allowing collaboration between different users often having different tasks to perform depending on their role. In Orbeon Forms, workflows are implemented using a combination of the following capabilities:

- [Authentication](/form-runner/access-control/users.md) can be required to access certain forms.
- [Permissions](/form-runner/access-control/deployed-forms.md) can be defined, in Form Builder, to limit access to a given form.
- [Buttons on the form can be enabled or disabled](/configuration/properties/form-runner/form-runner-detail-page.md#hiding-and-disabling-buttons) depending on the role of the user viewing the form.
- [Formulas](/form-builder/formulas.md) can be used to decide whether certain fields should be hidden or made read-only, for instance based on who the user is. The same can be done not just for fields, but for entire sections of the form.

As an example, in the following section we’ll explore how those features to implement a specific simple workflow.

## Form filled by the public, reviewed by staff

Consider the case of a city government who wants to put on their website a form allowing residents to report potholes. In that case, you would have 2 classes of users:

- Residents: they can fill and submit the form, most likely without having to be logged into the system.
- City staff: they will be logged into the system, can see all the submissions done by residents, and take further actions based on the reports. We’ll assume you have an authentication system in place for city employees, and that the subset of those employees who should be granted access to the reports submitted by citizen have the role `public-works-staff`.

In Form Builder, in the left sidebar, switch to the Advanced tab, click on Permissions, and fill out the dialog as follows:

![Permissions for pothole submission workflow](/configuration/images/workflows-pothole.png)

This will ensure that only users with the role `public-works-staff` can see any of the submitted data. Next, you’ll want to requires users accessing submissions to be authenticated. Say the form app name is `public-works` and form name `report-pothole`. Then:

- The page to fill out a new instance of the form, often called the *new page*, is at `/fr/public-works/report-pothole/new`.
- The page to access all the submissions, often call the *summary page*, is at  `/fr/public-works/report-pothole/summary`.

So you’ll want to require any user accessing the summary page to have the `public-works-staff` role. This can be done by editing the Orbeon Forms `web.xml`, adding the following, inside the root element `<web-app>`:

```xml
<security-constraint>
    <web-resource-collection>
        <web-resource-name>Public Works</web-resource-name>
        <url-pattern>/fr/public-works/report-pothole/summary</url-pattern>
    </web-resource-collection>
    <auth-constraint>
        <role-name>public-works-staff</role-name>
    </auth-constraint>
</security-constraint>
<login-config>
    <auth-method>FORM</auth-method>
    <form-login-config>
        <form-login-page>/fr/login</form-login-page>
        <form-error-page>/fr/login-error</form-error-page>
    </form-login-config>
</login-config>
<security-role>
    <role-name>orbeon-user</role-name>
</security-role>
```

Alternatively, if requests to your application server or servlet container (e.g. Tomcat) first go through a frontend web server, like Apache HTTP or IIS, you can also set this up at that level, instead of relying on the above configuration in the `web.xml`.

That’s it! With this, anyone will be able to access your form new page to submit new potholes, while only authorized city staff will be able to view the submissions that have been made through the form.
