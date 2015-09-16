1. Since Orbeon Forms 4.5, Form Runner and Form Builder support versioning of _form definitions_ ([blog post](http://blog.orbeon.com/2014/02/form-versioning.html)).

2. In addition (even prior to Orbeon Forms 4.5), when retrieving `form.xhtml`, Form Runner passes a URL parameter when possible:

    ```
    .../form.xhtml?document=6E6FC50F4BB945235EB5B573F2C7E695
    ```

    This parameter is passed when:

    * editing form data
    * viewing form data
    * producing a PDF

    This parameter is NOT passed when no document information is available, e.g. when:

    * creating new form data
    * listing form data on the summary page

    The value of the parameter is the id of the form data (document) being edited. This allows the persistence layer to retrieve the form definition associated with that document, in case the persistence layer handles versioning itself.

3. In addition, the relational database implementations support an auditing trail, keeping older versions of form definitions and form data. However this is not visible at the level of the REST API.