## Rationale

We describe a few usage scenarios for Orbeon Forms below, with the intent of answering the question "For what purpose can I use Orbeon Forms?".

## Forms for a local government

A local government publishes a number of forms for its constituents. Orbeon Forms is used for the capture of data as well as consultation by government employees.

- Constituents are provided with a catalog of available forms (e.g. linked from a web page).
    - They can fill-out and submit relevant forms.
    - The data is saved to a database and/or sent to other systems or workflows.
- The relevant government offices have access to the captured data for further processing.
    - Data entered by the citizen can be viewed.
    - Additional data and notes can be provided by the government employee.

### When a signature on paper is required

Not all workflows can be handled 100% online. Yet. But even when constituents will ultimately need to sign a document on paper, a web form can help. Here is how the workflow looks like:

- Constituents:
    - Fill out the form online, and the data is saved in a database, as above.
    - Download a PDF of the form, with the data they entered.
    - Sign it, and send the signed copy to the government.
- The government:
    - Receives the signed copy.
    - The generated and printed PDF contains a bar code (if enabled), so the government can correlate the printed form with information in the database, and thus avoid having to re-enter the information in the form.

While this isn't as straightforward as a 100% online solution, this use case has the following benefits:

- It fits right into existing practices, and can be offered to constituents as an option to just filling out the paper form by themselves without the help of the web form, as they might already be used to.
- The web form can better guide constituents. Its logic can be improved, even if in the end the same paper form is generate. It can check for errors as constituents fill out of the form, and avoid further frustrating and expensive back-and-forth.
- Because the data can be saved in a database, there is no need to re-enter it, thus saving time and avoiding potential mistakes. 

## Forms for an organization's internal use

Within an organization, employees capture, consult, and modify data.

- Depending on permissions, different users can:
  - enter data only
  - view their own data, or that of a group they belong to
  - view all the data
  - amend (edit) or delete data
- Data can be kept in the database, or submitted to internal web services.

## Anonymous data capture

An form is provided online and allows data to be captured anonymously. Your organization can then consult the data. An example of this is the [Orbeon Forms PE Trial License form](http://demo.orbeon.com/orbeon/fr/orbeon/register/new).

