# Workflows in Orbeon Forms

Many applications of forms require some kind of workflow. The typical example is that of an expense report:

- the employee submits the expense report
- a manager reviews the expense report and approves or reje ct it

In this kind of scenarios, there are several users involved, with different access rights to different parts of the form.

Orbeon Forms has several workflow-related features, which we cover in this page. We will examine the example of a construction permit application.

## General workflow

Here is a diagram that shows a concrete workflow, similar to the expense report example, but for a construction permit application:

![Workflow diagram](/form-runner/images/permit-workflow-diagram.webp)

In this workflow, there are two types of users:

1. A citizen applying for the permit 
2. A city employee reviewing the application.

The steps, or stages, are as follows:

- The citizen:
    - enters data into the form (no account is required)
    - submits it for review
    - receives a confirmation email
- The city employee:
    - reviews the application
    - either approve or reject it, possibly with comments
- If the permit is approved:
    - A permit number is generated.
    - The citizen receives a confirmation email
    - An official city permit in PDF format is attached to the email, including the permit number.
- If the permit is rejected:
    - The citizen receives a notification email with the comments from the city employee.
    - The citizen can then amend their submission and resubmit it.

## Challenges

In such a workflow, there are several challenges:

- The different types of users should only have access to the parts of the form that are relevant to them. For example, the city employee should not be able to modify the citizen's submission, only add a comment, and the citizen should not be able to modify the city employee's comments.
- Different types of email confirmations are sent at different steps of the workflow, with different content and attachments.
- The final permit should look official and contain the data from the citizen's submission, as well as the city employee's comments.

## Orbeon Forms to the rescue

Orbeon Forms solves these challenges with the following features:

1. __Form creation__: Orbeon Forms, with Form Builder, allows you to create complex forms with different sections.
2. __Workflow stages__ Orbeon Forms supports a native notion of *workflow stages*, which can be used to track the state of the workflow.
3. __Roles and conditions__ Depending on the user role (citizen or city employee) and the state of the workflow, different parts of the form can be shown or hidden.
4. __Processes:__ Orbeon Forms supports *processes*, which can be used to automate actions such as sending emails or generating PDFs at different steps of the workflow.
5. __Configurable buttons:__ Action buttons at the bottom of the form can be configured to trigger different processes, and to be shown or hidden based on the user role and workflow stage.
6. __PDF generation:__ Orbeon Forms can generate PDF documents based on the form data, which can be used to create the official city permit, as well as intermediate documents attache to emails.
7. __PDF templates:__ Orbeon Forms supports PDF templates, which can be used to create official-looking documents based on the form data.
8. __Email templates:__ Orbeon Forms supports email templates, which can be used to create different email content based on the workflow stage and user role.
9. __Confirmation page templates:__ Orbeon Forms supports confirmation page templates, which can be used to create different confirmation pages based on the workflow stage and user role.

## Concrete use of Orbeon Forms features

### 1. Form creation

You create a form as usual with Form Builder. In this example, we used top-level sections to match steps in the workflow:

- Step 1: Submitter and permit information
- Step 2: Permit review by the city
- Step 3: Permit status

This allows us to easily show/hide/make readonly different sections of the form based on the user role and workflow stage.

![Step 1](/form-runner/images/permit-workflow-step1.webp)

### 2. Workflow stages

In this example, we define the following workflow stages:

- `initial`: the initial stage when the citizen is filling out the form
- `review`: the stage when the city employee is reviewing the application
- `approved`: the stage when the application is approved
- `amend`: the stage when the application is rejected and the citizen needs to amend it

These workflow stages appear in:

- visibility and readonly conditions for form sections and buttons
- processes, which can test on the current stage and update the stage as needed

_NOTE: As of Orbeon Forms 2025.1, there is no central location for the workflow stages. Processes just use them._ 

### 3. Roles and conditions

In this example, we define the following user roles:

- `submitter`: the citizen applying for the permit
- `reviewer`: the city employee reviewing the application

In Form Builder, you can control the visibility and access to different parts of the form based on the user role and workflow stage. For example, the "Step 1: Submitter and permit information" section can be made readonly as follows:

![Conditional readonly access to a form section](/form-runner/images/permit-workflow-section-readonly.webp)

### 4. Processes

Processes in Orbeon Forms are sequences of actions on a form, which are triggered by buttons. They are defined by a custom language (a UI is planned for a future release) that allows you to specify the actions to be performed, as well as error handling and success messages. Conditions and error handling are supported.

Here are realistic examples of processes for the `submit`, `approve`, and `reject` buttons in our construction permit application workflow. They handle:

- testing on the current state of the form
- setting the next workflow stage
- sending different emails with different content and attachments
- generating a PDF document based on the form data and attaching it to an email
- displaying different success messages based on the outcome of the process
- handling errors and displaying error messages
- updating form data, including the date and permit number
- saving the data

These processes run entirely on the server.

![Submission process](/form-runner/images/permit-workflow-submit.webp)

```xml
<!-- `submit` process -->
<property as="xs:string"  name="oxf.fr.detail.process.submit.orbeon-features.permit-workflow">
    require-uploads
    then validate-all
    then
    <!-- Button only shows in ('initial', 'amend') modes anyway so `if` is redundant -->
    if ("empty(fr:workflow-stage-value()) or fr:workflow-stage-value() = ('initial', 'amend')") then (
        set-workflow-stage(name = "review")
        then (
            if ("xxf:non-blank(//submitter-email-address)") then
                email(template = "submitter-confirmation", use-pdf-template = "false")
        )
        then change-mode(mode = "confirmation")
    )
    then save
    then success-message(message = "Permit application submitted.")
    recover error-message("database-error")
</property>
```

![Approval process](/form-runner/images/permit-workflow-approved.webp)

```xml
<!-- `approve` process -->
<property as="xs:string"  name="oxf.fr.detail.process.approve.orbeon-features.permit-workflow">
    require-uploads
    then validate-all
    then
    <!-- Button only shows in `review` mode anyway so `if` is redundant -->
    if ("fr:workflow-stage-value() = 'review'") then (
        set-workflow-stage(name = "approved")
        then xf:setvalue(ref = "//permit-approved-date", value = "adjust-date-to-timezone(current-date(), ())")
        then xf:setvalue(ref = "//permit-number",        value = "concat('X-', substring(upper-case(fr:document-id()), 1, 10))")
        then (
            if ("xxf:non-blank(//submitter-email-address)") then
                email(template = "permit-approved", pdf-template-name = "approved")
        )
    )
    then save
    then success-message(message = "Permit saved and approved, submitter notified.")
    recover error-message("database-error")
</property>
```

![Submission process](/form-runner/images/permit-workflow-approved.webp)

```xml
<!-- `reject` process -->
<property as="xs:string"  name="oxf.fr.detail.process.reject.orbeon-features.permit-workflow">
    require-uploads
    then validate-all
    then
    <!-- Button only shows in `review` mode anyway so `if` is redundant -->
    if ("fr:workflow-stage-value() = 'review'") then (
        set-workflow-stage(name = "amend")
        then (
            if ("xxf:non-blank(//submitter-email-address)") then
                email(template = "permit-rejected", use-pdf-template = "false")
        )
    )
    then save
    then success-message(message = "Permit rejected, submitter notified.")
    recover error-message("database-error")
</property>
```

### 5. Configurable buttons

In this example, we decided to have "Submit", "Approve", and "Reject" buttons at the bottom of the form, which trigger the processes defined above. We want these buttons to be visible only to the relevant users and at the relevant stages of the workflow. Here are the properties that define the visibility of these buttons based on the user role and workflow stage:

```xml
<!-- `submit` button -->
<property as="xs:string"  name="oxf.fr.detail.button.submit.visible.orbeon-features.permit-workflow">
    fr:user-role() = 'submitter' and
    (empty(fr:workflow-stage-value()) or fr:workflow-stage-value() = ('initial', 'amend'))
</property>

<!-- `approve` button -->
<property as="xs:string"  name="oxf.fr.detail.button.approve.visible.orbeon-features.permit-workflow">
    fr:user-role() = 'reviewer' and
    fr:workflow-stage-value() = 'review'
</property>

<!-- `reject` button -->
<property as="xs:string"  name="oxf.fr.detail.button.reject.visible.orbeon-features.permit-workflow">
    fr:user-role() = 'reviewer' and
    fr:workflow-stage-value() = 'review'
</property>
```

### 6. and 7. Generating PDF files

Orbeon Forms can generate PDF documents automatically based on the form data. Here, this can be used to create a document indicating that the permit application has been submitted.

For the final permit, a PDF template can be attached to the form, and filled by Orbeon Forms.

![Form Builder PDF template UI](/form-runner/images/permit-workflow-pdf-template-ui.webp)

This allows creating official-looking documents based on the form data.

![Empty PDF template](/form-runner/images/permit-workflow-pdf-template.webp)

In processes, the `email` action can be configured to use a PDF template or an automatic PDF. For example, the following uses the `permit-rejected` email template, but no PDF template:

```
email(
    template         = "permit-rejected",
    use-pdf-template = "false"
)
```

But the following uses the `permit-approved` email template, and attaches a PDF generated from the `approved` PDF template:

```
email(
    template          = "permit-approved",
    pdf-template-name = "approved"
)
```

### 8. and 9. Email and Confirmation Page templates

Emails and the Confirmation Page share a very similar configuration, as they both contain similar content. With Form Builder, you can configure both types of templates. Here is the rejection email template:

![Rejection email template](/form-runner/images/permit-workflow-email-settings-rejected.webp)

An email can include control values, as well as links pointing back to the form for later editing. Here, we use this feature to allow amending the permit request.

Here is the confirmation page template:

![Conformation page template](/form-runner/images/permit-workflow-confirmation-template.webp)

The Confirmation page can also let the user download a PDF document.

## Conclusion

Orbeon Forms provides a rich set of features to implement complex workflows, with different user roles, email notifications, PDF generation, and more. The workflow features are flexible and can be adapted to a wide variety of use cases beyond the construction permit application example shown here.