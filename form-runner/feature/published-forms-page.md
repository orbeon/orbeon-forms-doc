# Published Forms page

## Introduction

The Form Runner Published Forms page allows you to access your deployed forms.

You access the Form Runner Published Forms page by adding `fr/forms` to the path on which you deployed Orbeon Forms. If you deployed Orbeon Forms on `http://www.example.com/orbeon/`, then you can access the Published Forms page at `http://www.example.com/orbeon/fr/forms`.

This page is also accessible directly from the [Landing page](landing-page.md).

![The Published Forms page](/release-notes/images/form-runner-forms.png)

## Availability and compatibility

[SINCE Orbeon Forms 2022.1]

Until Orbeon Forms 2021.1, this page was combined with the [Forms Admin page](forms-admin-page.md) under the name [Home page](home-page.md) and accessible at the path `/fr/`. Starting with Orbeon Forms 2022.1, the Home page is replaced with separate Published Forms and Forms Admin pages at paths `/fr/forms` and `/fr/admin`. The `/fr/` path now reaches the [Landing page](landing-page.md).

## Permissions

Depending on your [permissions](/form-runner/access-control/deployed-forms.md), only the subset of the forms that you have access to is shown.

## Published forms

The page shows a table with the following data:

- Each line shows only forms that are *published and available*.
- For each form, the app name, form name and title in the current language are shown.
- The form version is also shown. [SINCE Orbeon Forms 2020.1]
- You can navigate to the form's Summary, New or View page, depending on permissions, by clicking on a line.
- Forms are sorted by last modification time.
- Libraries are never shown in this view.

*NOTE: Only deployed forms are visible. Forms that have been created with Form Builder and which have been just saved but never deployed are not visible.*

## Search and sorting

[\[SINCE Orbeon Forms 2024.1\]](/release-notes/orbeon-forms-2024.1.md)

The Published Forms page allows you to search and sort forms by last modification date/time, application name, form name, version, and title.

To display search fields, click on the "Show Search Options" button.

To sort the forms, click on the column headers.

## Configuration properties

### Page size

The number of forms shown on a given page can be set with the following property:

```xml
<property
    as="xs:integer"
    name="oxf.fr.home.page-size"
    value="20"/>
```

### Link to the summary or new page

The table listing the forms links, for each form, either to the `summary` or the `new` page, based on which page the user has access to. If the user has access to both, then it links to the `summary` page, in essence giving the priority to the `summary` page. [SINCE Orbeon Forms 2017.2] You can also change this priority by setting the value of the following property, of which the default is shown below.

```xml
<property
    as="xs:string"
    name="oxf.fr.home.table.link-to"
    value="summary new"/>
```

For instance, if you set it to `new summary`, the priority will be given to the `new` instead of the `summary` page. If you list only one page, say `new`, then entries in the table will only link to the `new` page, of course if the user has access to it. If you leave the value blank, the forms will only be listed, with no link.

## Orbeon Forms 4.0 to 4.2

For each form definition the current user has access to, the following links are shown if allowed:

- Link to the summary page: shown if the current user can perform either one of the read, update, or delete operations on the form.
- Link to the new page: shown if the current user can perform the create operation on the form.

![Published Forms page](../images/home.png)

## See also

- [Forms Admin page](forms-admin-page.md)
- [Landing page](landing-page.md)
- [Summary Page](summary-page.md)
- [Access control for deployed forms](/form-runner/access-control/deployed-forms.md)
- [Form Builder permissions](/form-runner/access-control/editing-forms.md#form-builder-permissions)
