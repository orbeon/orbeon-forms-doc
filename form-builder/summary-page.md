# The Summary page

## Purpose

The Form Builder summary page is your starting point when you work with Form Builder. It allows:

* Listing and searching form definitions
* Creating new form definitions
* Editing existing form definitions
* Reviewing form definitions in read-only mode
* Deleting form definitions

_NOTE: Form definitions are usually stored in a database on the server. You do not usually keep them on your own computer._

## Accessing the summary page

You access the summary page from your web browser through a URL (or web address) of the form:

```
http://[SERVER NAME]/orbeon/fr/orbeon/builder/summary
```

_NOTE: The exact URL might be provided by your IT organization._

Once you reach the summary page, you will either see an empty list, or a list containing existing form definitions, as shown in this screenshot.

![The Summary page](images/summary.png)

For each form definition, the list shows:

* Creation date
* Last modification date
* Application name
* Form name
* Form title
* Form description if any

If the list doesn't fit entirely on one page, you can navigate to further pages of results using the navigation arrows at the bottom of the results list.

If the list is too wide, a horizontal scrollbar appears at the bottom of the list and allows you to navigate horizontally.

By default, the summary page lists all the form definitions which you have access to.

_NOTE: Depending on your permissions, the Application Name field and/or Form Name field might be input fields or dropdown menus that restrict your choices._

### Searching and navigating form definitions

The top of the summary page features search fields. There are two types of searches:

* **Free text search.** This searches any text within the form definition.
* **Structured search.** This searches on Application Name, Form Name, Form Title, or Form Description.

To search: enter a search term and press the "Search" button or the "enter" key.

Tip: to clear the search and list all the form definitions again, clear all search fields and press the "Search" button or the "enter" key.

![](images/summary-search.png)

## Creating a new form definition

To create a new form definition, press the "New" button at the bottom of the page. This opens the Form Builder editor in a separate browser window or tab.

## Reviewing a form definition

[\[SINCE Orbeon Forms 2025.1\]](/release-notes/orbeon-forms-2025.1.md) Select the form definition you want to inspect, then press the "Review" button. Form Builder opens the form in *view mode*, which shows the complete designer interface while disabling actions that would modify the form, such as dragging controls, saving, or publishing. This is useful when you want to share the layout of a form for feedback without risking accidental edits.

## Deleting a form definition

Using the checkboxes that appear on each row, select the form definitions you wish to delete, then press the "Delete" button.

## Permissions

The Summary page follows the [access control rules for deployed forms](form-runner/access-control/deployed-forms.md). This means that a user accessing the Summary page will only see the data to which she has access to.
