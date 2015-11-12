# Auditing

<!-- toc -->

## Rationale

Auditing guarantees that all the operations on the database:

* Are performed in a non-destructive manner – This means that it is always possible for a DBA to revert changes that have been made.
* Every change has indication of when that change was made and who did that change (if the user is known by the system).

This means that a DBA can always revert changes, and see who did what and when.

This feature is only available when using the relational persistence layer.

## Usage

For  auditing to work, Form Runner needs to know who is presently using the application. There are two ways in which Form Runner can determine the current username:

* **Container** - It can ask the container (i.e. servlet container or application server). This is typically useful when users log into the application using basic or form-based authentication.
* **Header** - It can get the username from an HTTP header. This is typically useful when you are using some type of front-end which "knows" who the user is and thus can pass this information to Form Runner through a header.

For more details on this configuration, see [Access Control](../../form-runner/access-control/README.md).

## Implementation for relational databases

When you use the Oracle or MySQL persistence layer:

* Every table has the following 4 columns:
    * `created` tells you when given the data (e.g. attachment to a form for the `orbeon_form_data_attach`).
    * `last_modified` tells you when the data was last changed.
    * `username` tells you who did that change.
    * `deleted` tells you if the data is marked as deleted, and hence invisible to users.
* Once a row is added to a column, it is never updated or deleted. Only new rows are added.
* When data is first added to a table, `created` and `last_modified` have the same value. Then, when this data is modified, another row is added: `created` is copied over and `last_modified` is set to the current time stamp.
* When data is deleted by users, a new row is added. This row is a copy of the latest row for the data that is being deleted, except for `last_modified` which is set to the current time stamp and `deleted` which is set to `Y`.

## See also

- [Access Control](../../form-runner/access-control/README.md)
- [Relational Database Setup](../../form-runner/persistence/relational-db.md)
- [Database Support](../../form-runner/persistence/db-support.md)
