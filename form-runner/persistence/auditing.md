# Auditing

## Rationale

Auditing (also known as the *auditing trail*) guarantees that all the operations on the database:

- Are performed in a non-destructive manner.
    - This means that it is possible for a DBA to revert changes that have been made.
- Every change has indication of when that change was made and who did that change (if the user is known by the system).
    - This means that you can see who did what and when.

This feature is only available when using a [relational persistence layer](relational-db.md).

Note that Orbeon Forms as of Orbeon Forms 2018.2 doesn't provide a user interface for this feature.

## Usage

For auditing to work meaningfully, Form Runner needs to know who is presently using the application.

For details, see [Setup users for access control](/form-runner/access-control/users.md).

## Implementation for relational databases

When you use a relational persistence layer:

- Every table has the following 4 columns:
    - `created` tells you when given the data (e.g. attachment to a form for the `orbeon_form_data_attach`).
    - `last_modified` tells you when the data was last changed.
    - `username` tells you who did that change.
    - `deleted` tells you if the data is marked as deleted, and hence invisible to users.
- Once a row is added to a column, it is never updated or deleted. Only new rows are added.
- When data is first added to a table, `created` and `last_modified` have the same value. Then, when this data is modified, another row is added: `created` is copied over and `last_modified` is set to the current time stamp.
- When data is deleted by users, a new row is added. This row is a copy of the latest row for the data that is being deleted, except for `last_modified` which is set to the current time stamp and `deleted` which is set to `Y`.

## Disabling auditing

As of Orbeon Forms 2018.2, it is not possible to disable this feature. However, you can [purge old data using SQL](/form-runner/persistence/purging-old-data.md) as needed.

[SINCE Orbeon Forms 2023.1]

You can also use the [Purge function](/form-runner/feature/purging-historical-data.md) from the Forms Admin page.

## See also

- [Revision history](/form-runner/feature/revision-history.md)
- [Purging historical data](/form-runner/feature/purging-historical-data.md)
- [Purging old data using SQL](/form-runner/persistence/purging-old-data.md)
- [Access Control](/form-runner/access-control/README.md)
- [Setup users for access control](/form-runner/access-control/users.md)
- [Using Form Runner with a relational database](relational-db.md)
- [Database Support](db-support.md)
- [Versioning](/form-runner/feature/versioning.md)
