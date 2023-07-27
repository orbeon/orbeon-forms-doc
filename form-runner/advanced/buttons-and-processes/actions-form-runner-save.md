# Form Runner save action

## Introduction

This action saves data and attachments via the persistence layer.

## Behavior

The `save` actions performs the following, in order:

- dispatch `fr-data-save-prepare` to `fr-form-model`
- save attachments
- save XML
- dispatch `fr-data-save-done` to `fr-form-model`
- switch to `edit` mode
    - [UNTIL Orbeon Forms 2016.3] This is part of the `save` action. 
    - [SINCE Orbeon Forms 2017.1] This is done using the separate `new-to-edit` action.

## Parameters

- `draft`: "true" if must be saved as a draft [SINCE Orbeon Forms 4.4]
- `query`: additional query parameters to pass the persistence layer (is an XPath value template) [SINCE Orbeon Forms 4.6.1]
- `prune-metadata`:
    - [SINCE Orbeon Forms 2017.2]
    - "true" if any `fr:*` metadata must be pruned before saving
    - the default is "false"
    - this must be used with care
        
## Examples

Example of use of the `query` parameter:

```xml
<property as="xs:string"  name="oxf.fr.detail.process.save-final.*.*">
    require-uploads
    then validate-all
    then save(query = "foo=bar&amp;title={//title}")
    then success-message("save-success")
    recover error-message("database-error")
</property>
```

The full URL, for attachments as well as for the XML data, is composed of:

- the URL pointing to the persistence layer path, including the file name
- the following URL parameter
    - `valid`: whether the data sent satisfies validation rules

*NOTE: The `save` action doesn't check data validity before running.*

Example:

    http://example.org/orbeon/fr/service/persistence/crud/
        orbeon/
        bookshelf/
        data/891ce63e59c17348f6fda273afe28c2b/data.xml?
        valid=true
        
## See also

- [Form Runner actions](actions-form-runner.md)
- [Form Runner send action](actions-form-runner-send.md)
- [Form Runner email action](actions-form-runner-email.md)
