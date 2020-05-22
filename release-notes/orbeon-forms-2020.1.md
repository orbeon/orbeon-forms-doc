# Orbeon Forms 2020.1

## Compatibility notes

### Navigation from the `view` to the `edit` page

When users load the `view` page for a form and click on the "Edit" button, to edit the data they are currently viewing, starting with Orbeon Forms 2020.1, the browser loads the `edit` page as if users were to paste the URL of the edit page in the location bar of their browser, while up to Orbeon Forms 2019.2, the data loaded by the `view` page was sent to the `edit` page. In general, this won't make a difference, but it could, if:

- Between the time a user A loads the `view` page and the time user A clicks on the "Edit" button, user B edits, changes, and saves the data. Since Orbeon Forms 2020.1, user A will be editing the new data, while up to Orbeon Forms 2019.2, user A would have been editing outdated data.
- After a user loads the `view` page, the data changes somehow. Then the user clicks on "Edit". At this point, since Orbeon Forms 2020.1 they will see the data without the change done while viewing, since the data is reloaded from the database, while up to Orbeon Forms 2019.2 the data would include the change done on the `view` page. Changing the data on the view page is rare, and unless the data is immediately saved, this was considered to be a bad practice, and isn't supported anymore.
