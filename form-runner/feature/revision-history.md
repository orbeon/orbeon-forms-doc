# Revision history

## Availability

[SINCE Orbeon Forms 2023.1]

## Introduction

Each time Form Runner saves data to the database, it saves a new *revision* of the data. Each revision of the data has an associated timestamp.

Until Orbeon Forms 2023.1, this revision was only accessible through the database, for [auditing purposes](/form-runner/persistence/auditing.md). Starting with Orbeon Forms 2023.1, Form Runner provides a user interface to access the revision history of a given form data.

Note that this feature is distinct from [form definition versioning](/form-runner/feature/versioning.md), which regards the versioning of published form definitions.

## Enabling the feature

The feature is disabled by default. You can enable it globally by setting the following property to `true`:

```xml
<property
    as="xs:boolean"
    name="oxf.fr.navbar.revision-history.enable.*.*"
    value="true"/>
```

This adds a "Revision History" icon in the [navigation bar](navbar.md).

![Revision History icon](../images/fr-revision-history-icon.png)

Activating the icon opens the Revision History pane:

![Revision History pane](../images/fr-revision-history-pane.png)

## The Revision History pane

The Revision History pane shows the list of revisions for the current form data, with the following information:

- date and time of the change
- username of the user that made the change, or "Anonymous user"
- a description of the changes, including
    - value entered
    - value cleared
    - value changed
    - iteration added
    - iteration removed
    - other change
    - no change

For a given revision, you can click on the "View this revision" link to view the form data as it was at the time of that revision.

![Viewing a specific revision](../images/fr-revision-history-view-revision.png)

## See also

- [Form Runner navigation bar](navbar.md)
- [Form definition versioning](/form-runner/feature/versioning.md)
- [Auditing](/form-runner/persistence/auditing.md)