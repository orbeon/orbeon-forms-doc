# Search API

## Service endpoint

HTTP `POST` to the following path:

```
/fr/service/persistence/search/$app/$form
```

## Example query

A search query uses HTTP `POST` to provide an XML document containing the search criteria as well as information about the response to provide.

The request MUST have a `Content-Type: application/xml` header.

Here is an example of a very simple query:

```xml
<search>
    <query/>
    <query path="details/title" match="substring">Peace</query>
    <page-size>10</page-size>
    <page-number>1</page-number>
</search>
```

The following shows for example a search from the demo Bookshelf form:

```xml
<search>
    <!-- Free-text search query -->
    <query/>
    <!-- Structured search query -->
    <query path="details/title" match="substring">Peace</query>
    <query path="details/author"/>
    <query path="details/language" match="exact">en</query>
    <drafts>include</drafts>
    <!-- Paging -->
    <page-size>10</page-size>
    <page-number>1</page-number>
</search>
```

## Query elements

The `query` element is the most complex element. It is used for full-text and structured search. Full-text search and structured search are exclusive: either Form Runner performs one, or the other.

### Full-text search

The first `<query>` element is used for free text search: its attributes, if any, are ignored, and its text content, if present, is taken to be the text of the search. The result details to return is still determined by the subsequent `<query>` elements with summary-field set to `true`. See below for more information about the response format. The exact semantic of the full-text search is implementation-dependent.

[SINCE Orbeon Forms 2022.1]

The `<query>` element used for full-text search is no longer necessarily the first element. Instead, it is the first element that doesn't specify a `path` attribute. Similarly, `<query>` elements used for structured search are those with a `path` attribute. This means that it is no longer required to have an empty `<query>` element at the beginning. 

### Structured search

When building a structured search query, Form Runner first looks at all controls in the source form that have the class `fr-summary` or `fr-search`. These are used to build the request.

For each such control found, a `<query>` element is added to the request, with the following attributes and text value:

As of Orbeon Forms 2020.1:

| XML Node            | Required | Function                                                                      |
|---------------------|----------|-------------------------------------------------------------------------------|
| `path` attribute    | Yes      | path to the XML element (see *Search paths* below)                            |
| `match` attribute   | No       | match type: `substring`, `exact`, `token` (see *Match types* below)           |
| `control` attribute | No       | control type (`input`, `textarea`, `select`, etc.), used if `match` is absent |
| text value          | No       | search string                                                                 |

If the `match` attribute is absent, the `control` attribute is used to determine the match type:

- `input` or `textarea`: implies `match="substring"`
- `select` or end with `-select`: implies `match="token"`
- any other value: implies `match="exact"`

Historical attributes, which were informative only:

| Attribute      | Function                                                      |
|----------------|---------------------------------------------------------------|
| `name`         | control name as entered in Form Builder                       |
| `label`        | control label as entered in Form Builder                      |
| `type`         | datatype associated with the control in Form Builder          |
| `search-field` | indicate whether the control must show as Summary page search |
| `summary-field`| indicate whether the control must show as Summary page column |

### Match types

[SINCE Orbeon Forms 2020.1] for relational databases
 
The `match` attribute can have the following values:

- `substring`: a substring match is requested (default)
- `exact`: an exact match is requested
- `token`: a token match is requested (for multiple selection controls)

_NOTE: The exact semantics for `substring` is not specified at the moment (the search implementation may use a "starts-with", a "contains" logic, or something else)._

### Search paths

The `path` attribute can be looked at as a search key. Say your documents looks like this:

```xml
<form>
    <personal-info>
        <first-name>John</first-name>
        <last-name>Doe</last-name>
        <birth-date>1980-01-01</birth-date>
    </personal-info>
</form>
```

Then the path `personal-info/first-name` matches XML elements following the XPath notation, relative to the root element of the document.

Note that for a given form definition, there is a limited number of search paths, which are fully determined by the XPath expressions present on the form definition's control bindings and model binds for controls that have the `fr-summary` or `fr-search` classes.

The paths sent by Form Runner are usually simple paths, but there are some exceptions:

- Form Builder uses more complex paths out of the box, for example:

    ```xml
    xhtml:head/
      xforms:model[@id = 'fr-form-model']/
        xforms:instance[@id = 'fr-form-metadata']/
          */
            description[@xml:lang = $fb-lang]
    ```

- Forms where user has entered XPath expressions manually to bind controls to XML data will contain those paths.

If the persistence layer is able to run XPath expressions (as eXist or other XML databases can), then the path provided by the search request can be executed. However, XPath evaluation is not absolutely required. For example with non-XML (relational, key/value) data stores, it is easier to consider the search path as an opaque search key that uniquely identifies the control to search.

- [SINCE Orbeon Forms 2016.2]
    - Paths generated by Form Runner (including from the Summary page) no longer contain `[1]` suffixes.
    - The built-in relational search implementation assumes paths which do not contain such suffixes. Orbeon Forms 2016.2.1 strips such suffixes for backward-compatibility (see [#2922](https://github.com/orbeon/orbeon-forms/issues/2922)).
- [UNTIL Orbeon Forms 2016.1 included]
    - Paths generated by Form Runner (including from the Summary page) contain `[1]` after element names, to guarantee the query only returns one node.
    - This is done to make it easier to implement the persistence layer for databases like MySQL that don't allow multiple values to be handled in a custom way. (Specifically with MySQL, if multiple values are found, [extractValue](http://dev.mysql.com/doc/refman/5.6/en/xml-functions.html#function_extractvalue) always returns a space separated string.)

### Old query elements

- 2013-05-24: Removal
    - All the previously deprecated elements mentioned below have been removed, and are not sent to the search API.
- 2011-11-22: Deprecation
    - `app` and `form` elements:
        - these elements are deprecated and will be removed in the future. Since Orbeon Forms 3.9, they are present but empty
        - to obtain the app and form name being queried, extract them instead from the search URL
    - `sort-key` element:
        - this element was present but never used and will be removed in the future

## Returning all indexed controls

[SINCE Orbeon Forms 2022.1]

You can return all the form's indexed controls, even if they don't have any matching `<query>` element, by specifying `return-all-indexed-fields="true"` on the root element of the query:

```xml
<search return-all-indexed-fields="true">
    <query path="details/title" match="substring">Peace</query>
    <page-size>10</page-size>
    <page-number>1</page-number>
</search>
```

For the search response in this case, see further below.

## Metadata queries

[\[SINCE Orbeon Forms 2024.1\]](/release-notes/orbeon-forms-2024.1.md)

`query` elements can also be used to add constraints on metadata fields, using the `metadata` attribute, which can take one of the following values:

- `created`: creation date/time
- `created-by`: username of the user who created the data
- `last-modified`: last modification date/time
- `last-modified-by`: username of the user who last modified the data
- `workflow-stage`: name of the workflow stage associated with the data

In that case, the `path` attribute must be absent and the `match` attribute must be present, with one of the following values:

- `gte`: greater than or equal to
- `lt`: less than
- `exact`: exact match

The `gte` and `lt` match values are used for date/time fields (creation or last modification date/time), and the `exact` match value is used for string fields (created by, last modified by, or workflow stage). Date/time values are expected to be in ISO format.

Examples of metadata queries:

```xml
<query metadata="created" match="gte">2022-01-01T00:00:00Z</query>
<query metadata="last-modified-by" match="exact">admin</query>
```

The absence or presence of a metadata query element doesn't affect the inclusion of the `created`, `created-by`, `last-modified`, `last-modified-by`, and `workflow-stage` attributes on the `<document>` elements in the search response.

## Sorting

[\[SINCE Orbeon Forms 2024.1\]](/release-notes/orbeon-forms-2024.1.md)

By default, search results are sorted by last modification date/time, from most recent to oldest. It is possible to specify another sort order by adding a `sort` attribute to the search query, on one of the `<query>` elements, with one of the following values:

- `asc`: ascending order
- `desc`: descending order

Example (query element also used to match a substring):

```xml
<query path="details/title" match="substring" sort="asc">Peace</query>
```

Example (query element used only for sorting):

```xml
<query path="details/title" sort="asc"/>
```

At the moment, only a single `sort` attribute is supported, but the last modification date/time is always used as a secondary sort key.

## Paging

The `page-size` and `page-number` elements control paging.

- `page-size` attribute: how many results to return at the most
- `page-number` attribute: page number to result, starting with 1

## Language

### Since 2016.2

The Form Runner home page doesn't send the `<lang>` element, and the implementations of the API that ship with Orbeon Forms don't use it even if provided. Instead, when the persistence knows about multiple values corresponding to different languages for a "control", it returns all the values. In turn, the summary page displays all the values. This is inline with what is done for controls inside repeated grids or repeated sections, which can have multiple values.

### Up to 2016.1

The `lang` element contains the current language used by the user in the summary page. If the form contains language-dependent controls, this information can be used to refine the search. Only Form Builder makes use of this capability.

For example, Form Builder allows storing a form title and description in more than one language. When searching the form title control, the expectation of the user is likely to be that, if the user interface is in English, the English title will be searched and not, for example, the Italian title. In this case, the search path contains a special variable, `$fb-lang`, which allows the persistence implementation to discriminate on the language:

```xml
xhtml:head/
  xforms:model[@id = 'fr-form-model']/
    xforms:instance[@id = 'fr-form-metadata']/
      */
        description[@xml:lang = $fb-lang]
```

In persistence implementations that support XPath, the variable can be either scoped or replaced in the path by the value provided by the incoming `<lang>` element.

In persistence implementations that don't support XPath, this kind of queries are a little more tricky. The path can:

- either be used as a plain key and ignore the `<lang>` element (therefore ignoring the language-sensitive search)
- or be translated to the native query language of the persistence implementations

## Drafts

The `<drafts>` element is used to tell the implementation of the persistence API which documents to return with respect to drafts:

- If the element is present, it must contain one of the following values:
    - `exclude`: don't include drafts. This value isn't used by Form Runner, but could be used in the future, and [SINCE 2016.2] is supported by the persistence for relational databases.
    - `only`: only return drafts.
    - `include`: return both drafts and non-drafts. This is the default if the `<drafts>` element is missing.
- If the element isn't present, then the persistence must return drafts and non-drafts alike.

_NOTE: `exclude` is not used in 4.6.2, but it could be used in the future._

If the value of `<drafts>` is `only`, then the `<drafts>` element can optionally have one of the following attributes:

- `for-document-id="$document"` instructs the persistence to only return drafts for the given document id, of which there can be 0 or 1. This is used by Form Runner's `/edit` page to check if a draft exists for a given document that is being opened, so it can, when appropriate, ask users if they prefer to open the draft or non-draft document.
- `for-never-saved-document="true"` instructs the persistence to only return drafts for documents that were never saved. This is used by Form Runner's:
    - `/new` page to check if existing drafts already exist, in which case it might ask users if they prefer to start a new document or edit one of those drafts.
    - `/summary` page if that page gets `drafts-for-never-saved-document=true` in the URL.

Here are examples of requests/response in `edit` mode. Only zero or one document can be returned:

```xml
<search>
    <drafts for-document-id="fbba3db82e7fb1e0054e97d49026b5d303a1fa2f">only</drafts>
    <page-size>10</page-size>
    <page-number>1</page-number>
    <lang>en</lang>
</search>

<documents search-total="1">
    <document created="2014-08-15T17:12:04.570Z" last-modified="2014-08-15T17:18:50.993Z" draft="true"
              name="fbba3db82e7fb1e0054e97d49026b5d303a1fa2f" operations="*">
        <details/>
    </document>
</documents>
```

And in `new` mode, zero or more documents can be returned:

```xml
<search>
    <drafts for-never-saved-document="true">only</drafts>
    <page-size>10</page-size>
    <page-number>1</page-number>
    <lang>en</lang>
</search>

<documents search-total="2">
    <document created="2014-08-15T17:05:11.563Z" last-modified="2014-08-15T17:05:11.563Z" draft="true"
              name="dac2971cca0e71e36880e890297ab8818a5298e0" operations="*">
        <details/>
    </document>
    <document created="2014-08-15T17:04:55.383Z" last-modified="2014-08-15T17:04:55.383Z" draft="true"
              name="b0e28c1ed4ea6cfab445b40bb9dcb8bc6c296c92" operations="*">
        <details/>
    </document>
</documents>
```

## Operations

[SINCE Orbeon Forms 2019.1] The `<operations>` element is used to tell the implementation of the persistence API which documents to return with respect to operations the user can perform. 

- When the element isn't present, or before Orbeon Forms 2019.1 (when it wasn't supported), the search API returns any document the user can either read, update, or delete.
- When specified, the element must take the form `<operations any-of="update delete"/>`, where the value of the `any-of` attribute is a space-separated list of one or more operations. In this example, the search API will only return documents the user can either update or delete, but not documents the user can only read.

## Versioning

- [SINCE Orbeon Forms 2018.2] If the implementation of the persistence API supports form versioning, the `Orbeon-Form-Definition-Version` request header tells which version of the form definition is requested. Possible values are:
    - missing: indicates the latest published version
    - a specific version number (must be a positive integer): to indicate that the form definition with that version must be searched
    - `all`: [SINCE Orbeon Forms 2020.1 and 2019.2.3] to do a search across all versions of the form definition, which used to be what was done until Orbeon Forms 2018.1; while this can lead to unexpected results (see below), callers of the API who relied on this behavior might find this backward compatible mode useful; if you are interested in this in the context of the summary page, see the [`oxf.fr.summary.show-version-selector.*.*` property](/configuration/properties/form-runner-summary-page.md#versioning).
- [UNTIL Orbeon Forms 2018.1] The search is done across all versions of the form definition. This can lead to unexpected results as different form definition versions may not have the same controls.

## Query response

### Example

The persistence layer must return a document of this shape:

```xml
<documents search-total="4" page-size="10" page-number="1" query="">
    <document created="2011-05-06T14:58:40.376-07:00"
              last-modified="2011-09-12T12:05:07.3-07:00"
              name="e8bfd3ba63fa12a8b59cdd5c08369a35"
              draft="false"
              operations="create read update delete">
        <details>
            <detail>The Terror</detail>
            <detail>Dan Simmons</detail>
            <detail>en</detail>
        </details>
    </document>
    <document created="2011-05-06T14:58:39.611-07:00"
              last-modified="2011-09-12T12:05:06.914-07:00"
              name="9531a191c77b75c417e9874427fa21f7"
              draft="false" 
              operations="create read update delete">
        <details>
            <detail>The Little Prince</detail>
            <detail>Antoine de Saint-Exupéry</detail>
            <detail>fr</detail>
        </details>
    </document>
    <!-- ... more <document> elements ... -->
</documents>
```

### Root element

The root element contains these attributes:

- `search-total` attribute: number of documents matched by the current search.
- \[Up to 2016.1\] `total` attribute: total number of documents in the database for this app/form.
- \[Up to 2016.1\] `page-size` attribute: echos of the query's attribute.
- \[Up to 2016.1\] `page-number` attribute: echos of the query's attribute.
- \[Up to 2016.1\] `query` attribute: echos of the full-text query text.

### Document elements

For each of the documents found, a `<document>` element is returned with the following attributes:

- `created`
    - creation date in ISO format
- `last-modified`
    - last modification date in ISO format
- `name`
    - document identifier,
    - this must match the identifier created by Form Runner when saving the data
- `draft`
    - `true` or `false`, depending on whether the form data is a draft (autosaved) or not
- `operations`
    - a space separated subset of the following token: `read`, `write`, `update`, and `delete`, whichever operations the user is allowed to perform; also see [Supporting permissions in your persistence API implementation](https://blog.orbeon.com/2013/10/supporting-permissions-in-your.html)
- `created-by`
    - [SINCE Orbeon Forms 2021.1] 
    - username of the user who created the data, or missing if there is no user information
- `created-by-groupname`
    - [SINCE Orbeon Forms 2021.1] 
    - group name of the user who created the data, or missing if there is no user information
- `last-modified-by`
    - [SINCE Orbeon Forms 2021.1] 
    - username of the user who last modified the data, or missing if there is no user information
- `workflow-stage`
    - [SINCE Orbeon Forms 2020.1]
    - name of the workflow stage associated with the data, or missing if there is no stage information

### Detail elements

Each document contains one `<detail>` element in the order determined by the `<query>` elements with a summary-field set to `true` in the request. The text value of the `<detail>` element is the value of the control in the document found.

```xml
<document created="2022-03-30T18:42:38.168Z" 
          last-modified="2022-03-30T18:42:38.168Z" 
          name="e8bfd3ba63fa12a8b59cdd5c08369a35" 
          draft="false" 
          operations="create read update delete">
    <details>
        <detail>The Terror</detail>
        <detail>Dan Simmons</detail>
        <detail>en</detail>
    </details>
</document>
```

[SINCE Orbeon Forms 2022.1]

All elements contain a `path` attribute that identifies the control.

```xml
<document created="2022-03-30T18:42:38.168Z" 
          last-modified="2022-03-30T18:42:38.168Z" 
          name="e8bfd3ba63fa12a8b59cdd5c08369a35" 
          draft="false" 
          operations="create read update delete">
    <details>
        <detail path="details/title">The Terror</detail>
        <detail path="details/author">Dan Simmons</detail>
        <detail path="details/language">en</detail>
    </details>
</document>
```

Prior to Orbeon Forms 2022.1, the controls had to be identified by position.

When `return-all-indexed-fields="true"` is specified in the search query, all indexed controls are returned as separate `<detail>` elements, and not only the controls specified in the search query with `<query>`, if any.

Indexed controls are controls that are marked, in Form Builder, as:

- "Index control"
- "Show on Summary page"
- "Allow bulk edit"

## See also

- [CRUD](crud.md)
- [Reindexing](reindexing.md)
- [List form data attachments](list-form-data-attachments.md)
- [Form metadata](forms-metadata.md)
- [Caching](caching.md)
- [Versioning](versioning.md)
- [Custom persistence providers](custom-persistence-providers.md)
