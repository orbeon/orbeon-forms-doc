# Purging historical data

## Availability

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

## Overview

The "Purge" dialog is similar to the "Export" dialog, except that it allows you to delete form data instead of exporting it. A typical scenario will involve exporting the data first, e.g. for archival purpose, then deleting/purging it.

Like in the "Export" dialog, you can either purge the forms that were explicitly selected:

![Purge selected forms](../images/purge-selected-forms.png)

Or you can purge forms by specifying a list of application, form, and version filters:

![Purge following forms](../images/purge-following-forms.png)

## Limitations

At the moment, it is only possible to purge form data, not form definitions.

At the moment, once the "Purge" button has been clicked, the dialog will be closed and the purge will be performed in the background. This means that you won't be able to see the progress of the purge, and you won't be able to cancel it. This also means you shouldn't leave the current page until the zip file has been downloaded. This will be improved in a future version.

## See also

- [Revision history](revision-history.md)
- [Purging historical data](purging-historical-data.md)
- [Export of form definitions and form data](exporting-form-definitions-and-form-data.md)
- [Forms Admin page](forms-admin-page.md)
