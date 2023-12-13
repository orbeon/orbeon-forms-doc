# Purging old data

## Rationale

The built-in implementation of the persistence API for relational databases never deletes saved data from your database. When users "delete" data from the UI, it marks the data as deleted in the database, and then ignores such data. This is done to increase safety, enable auditing, and allow an admin to "undelete" data that has been deleted by mistake.

While there are benefits to keeping data marked as deleted, there are cases where you might want to get rid of it, for instance to comply with regulatory requirements, or for larger systems to save space and improve performance.

This is done in 2 steps, starting by removing form data, and then the corresponding attachments. We recommend you follow the steps below only if you are familiar with SQL. Also, especially if you are going to run this on a production database, we highly recommend you first create a backup of that database.

## Form Builder data

The following queries purge data that users manually deleted. However, should you want to adapt those queries to also purge "historical data", keep in mind that forms you edit in Form Builder are also stored in the `orbeon_form_data` and `orbeon_form_data_attach` tables. This means that in your own version of those queries, you might want to avoid rows with with `app = 'orbeon' AND form = 'builder'`, so not to delete any Form Builder data and so not to loose the ability to open forms from the Form Builder summary page.

## Removing form data

The following statement is for Oracle (if you're using another database, replace `ADD_MONTHS(SYSDATE, -1)` by an equivalent function on that database). It will return the data marked for deletion more than 1 month ago. If you want to delete that data, replace `SELECT *` by `DELETE` in the statement, and run it again.

```sql
SELECT *
FROM   orbeon_form_data
WHERE  document_id IN
       (
           SELECT t.document_id
           FROM   orbeon_form_data t,
                  (
                      SELECT   max(last_modified_time) last_modified_time,
                               app, form, document_id
                      FROM     orbeon_form_data
                      GROUP BY app, form, document_id
                  ) m
           WHERE  -- Look at "last row" in the "journal"
                  t.last_modified_time = m.last_modified_time AND
                  t.app                = m.app                AND
                  t.form               = m.form               AND
                  t.document_id        = m.document_id        AND
                  -- Take deleted items
                  deleted = 'Y'                               AND
                  -- Deleted at least 1 month ago
                  t.last_modified_time <= ADD_MONTHS(SYSDATE, -1)
       )
```

## Removing form attachments

Next, we want to remove "orphan" attachments. Those are attachments no longer referenced by an existing document. As in the previous step, first check the data returned by the following query, and make sure this is data you want to delete. If that is the case, replace `SELECT *` by `DELETE` and run that statement again.

```sql
SELECT *
FROM   orbeon_form_data_attach
WHERE  document_id NOT IN
       (
           SELECT document_id
           FROM orbeon_form_data
       )
```

## Removing other content

The `orbeon_i_current` and `orbeon_i_control_text` tables may contain references, via the `data_id` column, to entries in `orbeon_form_data`. The content of these tables is recreated when a [full database reindex](/form-runner/feature/forms-admin-page.md#reindexing) is performed.

If the [lease feature](/form-runner/feature/lease.md) is enabled and in use, the `orbeon_form_data_lease` table may also contain references, via the `document_id` column, to entries in `orbeon_form_data`.

If that is the case, the relevant rows must also be deleted.

## See also 

- [Auditing](auditing.md)
- [Use Form Runner with a Relational Database](relational-db.md)
