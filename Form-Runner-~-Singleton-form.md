## Use case

Most forms are filled out for a given purpose, and then submitted. When the initial purpose comes up again, the form is filled and submitted one more time. For instance, a *car registration* form would be such an example. However, certain forms are often used more like "documents", that users create, and then come back to update, without creating another instance of that form/document. Those forms are here referred to as *singleton forms*.

## In Form Builder

As a form author, you can mark a form as *singleton* in Form Builder by opening the *Form Settings* dialog, and clicking on the *Singleton form* checkbox.

## In Form Running

At runtime, when accessing the `/new` page of a singleton form, the following will happen depending on the number of form data the current user can access for that form:

- 0: if she can't access any existing data for the current form, then she stays on *new* page for the form.
- 1: if she can access exactly one form data, she is redirected to the *edit* page for that form.
- 2 or more: a dialog warns her she can't create any additional form data, and links to the *summary* page for that form, which she can use to pick the form data she wants to edit.

The simplest use case, described above, calls for having at most 1 form data per user, however, since the *singleton* aspect is driven by what users can see, you can use permissions to control whether you want to have one form per user, per group, per users having a given role, or even in the whole system.

This also means that it might still be possible for certain users to see multiple forms, hence the third case above (*2 or more* form data), e.g. if you setup permissions so regular users can only see their own data, hence they will be able to create at most 1 form data, but someone with the `admin` role can view all the data.