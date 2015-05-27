## Rationale

The built-in implementation of the persistence API for relational databases never deleted saved data from your database. When users "delete" data from the UI, it marks the data as deleted in the database, and then ignores data marked as deleted. This is done to increase safety, to enable auditing, and allow an admin to "undelete" data that has been deleted by mistake.

While keeping data marked as deleted as deleted has benefits, there are cases where you might want to get rid of it, for instance to comply with regulatory requirements, or save space and improve performance, the case of larger systems.

This is done in 2 steps, starting by removing form data, and corresponding attachments. We recommend you follow the below only if you are familiar with SQL. Also, especially if you are going to run this on a production database, we highly recommend you first create a backup of that database.

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
                               app,
                               form,
                               document_id
                      FROM     orbeon_form_data
                      GROUP BY app, form, document_id
                  ) m
           WHERE  -- Look at "last row" in the journal                                                                                                                                                                                                                                                         
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
