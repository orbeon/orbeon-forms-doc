# Flat view

## Usage

### Rationale

If you're using Oracle, SQL Server [SINCE Orbeon Forms 2016.2], DB2 [SINCE Orbeon Forms 4.7], PostgreSQL [SINCE Orbeon Forms 4.8], or MySQL [SINCE Orbeon Forms 2024.1], when you deploy a form created in Form Builder, Orbeon Forms can create a form-specific view of your data, with one column for each form field.

### Property to enable

You enable this feature by setting the relevant property listed below to `true`.

| Database   | Property                                         |
|------------|--------------------------------------------------|
| Oracle     | `oxf.fr.persistence.oracle.create-flat-view`     |
| SQL Server | `oxf.fr.persistence.sqlserver.create-flat-view`  |
| DB2        | `oxf.fr.persistence.db2.create-flat-view`        |
| PostgreSQL | `oxf.fr.persistence.postgresql.create-flat-view` |

For instance, if using  Oracle, you set:

```xml
<property
    as="xs:boolean"
    name="oxf.fr.persistence.oracle.create-flat-view"
    value="true"/>
```

## Generated names

### View name

When you enable this property, upon publishing a form, the persistence layer creates a view specific to that form. The name of the view is based on the app name, form name, and [SINCE Orbeon Forms 2021.1] form version. It has the form:

```
[SINCE Orbeon Forms 2021.1] orbeon_f_#{app}_#{form}_#{form_version}
[UP TO Orbeon Forms 2020.1] orbeon_f_#{app}_#{form}
```
 
For instance, on Orbeon Forms 2021.1 and newer, if your app is `hr`, your form is `expense`, and you are publishing version 2 of that form, then the view is named `orbeon_f_hr_expense_2`. If upon publishing, there is already a view with that name, the persistence layer deletes it before recreating a new view.

[SINCE Orbeon Forms 2024.1] One extra view will be created for each repeated section and grid, if any. In that case, the view name will be suffixed with the name of the repeated section or grid.

```
orbeon_f_#{app}_#{form}_#{form_version}_#{repeated_section_or_grid}
```

### Metadata column names

Each view always has one or more metadata columns, with information copied from the equivalent columns in `orbeon_form_data`. For views related to repeated sections or grids, only the `metadata_document_id` column is included.

- [UP TO Orbeon Forms 4.3]
    - `metadata_document_id`
    - `metadata_created`
    - `metadata_last_modified`
    - `metadata_username`
- [SINCE Orbeon Forms 4.4]
    - `metadata_document_id`
    - `metadata_created`
    - `metadata_last_modified_time`
    - `metadata_last_modified_by`

Note that there is no `metadata_draft` column, as drafts are not included the view. (Before 4.7 they were, incorrectly, see [issue 1870](https://github.com/orbeon/orbeon-forms/issues/1870).)

### Repetition column names

[SINCE Orbeon Forms 2024.1] For repeated sections and grids, the view includes the repetition number for the current repeated section or grid, as well as for any enclosing repeated section. The column names are generated as follows:

```
#{repeated_section_or_grid}_repetition
```

### Data column names

In addition to the metadata and repetition columns, one column is created for each form field. Each column is named using the control name, optionally prefixed with enclosing section/grid names. See below for details on how column names are generated depending on the version of Orbeon Forms you are using.

As databases limit the length of column names, the persistence layer truncates them if needed. It also converts dashes to underscores, and removes any non-alphanumeric character except inner underscores.

#### With Orbeon Forms 2024.1 and newer

By default, the column names are generated using the control names only, unless they are located in a section template, in which case they will be prefixed with the name of the template section.

To get the previous behavior (Orbeon Forms 2023.1 and earlier), where the names of all enclosing sections/grids are included in the column names, the following property can be changed from `false` to `true`:

```xml
<property
  as="xs:boolean"
  name="oxf.fr.persistence.[provider].flat-view.prefixes-in-main-view-column-names"
  value="false"/>
```

This property doesn't affect column names in views for repeated sections and grids. In those views, the names of enclosing sections/grids are never included in the column names.

In previous versions, column names were always truncated to 30 characters. With Orbeon Forms 2024.1 and newer, they are now truncated to a number of characters which depends on the database used. Those limits are defined in properties, which can be overridden if needed:

```xml
<property
  as="xs:integer"
  name="oxf.fr.persistence.oracle.flat-view.max-identifier-length"
  value="128"/>
<property
  as="xs:integer"
  name="oxf.fr.persistence.mysql.flat-view.max-identifier-length"
  value="64"/>
<property
  as="xs:integer"
  name="oxf.fr.persistence.postgresql.flat-view.max-identifier-length"
  value="63"/>
<property
  as="xs:integer"
  name="oxf.fr.persistence.db2.flat-view.max-identifier-length"
  value="128"/>
<property
  as="xs:integer"
  name="oxf.fr.persistence.sqlserver.flat-view.max-identifier-length"
  value="128"/>
```

#### With Orbeon Forms 4.5 and newer

Orbeon Forms 4.5 introduces a new truncation algorithm so names are not cut short unnecessarily, and a numerical *suffix* is used instead for those columns which would introduce duplicates.

Examples:

- Section name: `personal-information`
    - Control name: `first-name` ⇒ column name: `personal_informatio_first_name`
    - Control name: `last-name` ⇒ column name: `personal_information_last_name`
    - Control name: `address` ⇒ column name: `personal_information_address`
- Section name: `company`
    - Control name: `name` ⇒ column name: `company_name`
    - Control name: `industry` ⇒ column name: `company_industry`
- Section name: `section-with-long-name`
    - Control name: `my-control-with-a-pretty-long-name` ⇒ column name: `section_with_l_my_control_with`
    - Control name: `my-control-with-a-pretty-long-name-too` ⇒ column name: `section_with_l_my_control_wit1`
    - Control name: `my-control-with-a-pretty-long-name-really` ⇒ column name: `section_with_l_my_control_wit2`

Enclosing section and grid names are always included in the column names. A truncation limit of 30 characters is enforced.

#### With Orbeon Forms 4.4 and earlier

The section name is truncated to 14 characters, the control name to 15 characters, and both are combined with an underscore in between. In the vast majority of the cases, this will result in distinct and recognizable column names. In cases where two or more columns would end up having the same name or conflict with one of the metadata column, the persistence layer adds a number prefix of the form `001_`, `002_`, `003_`… to each column to make it unique. If this happens, you might want to change your section and/or control names to have more recognizable column names.

Examples:

| Section name           | Control name               | Column name                 |
|------------------------|----------------------------|-----------------------------|
| `personal-information` | `first-name`               | `PERSONAL_INFOR_FIRST_NAME` |
| `last-name`            | `PERSONAL_INFOR_LAST_NAME` |                             |
| `address`              | `PERSONAL_INFOR_ADDRESS`   |                             |
| `company`              | `name`                     | `COMPANY_NAME`              |
| `industry`             | `COMPANY_INDUSTRY`         |                             |

## Limitations

- The Multiple File Attachments control is not supported, see [issue 6296](https://github.com/orbeon/orbeon-forms/issues/6296).
- Only the default [form data format](https://doc.orbeon.com/form-runner/api/data-formats/form-data) is supported, see [issue 4440](https://github.com/orbeon/orbeon-forms/issues/4440).
- Fields inside repeated sections or grids are not supported with MySQL.
- Flat views are not supported at all with SQLite.
- [SINCE Orbeon Forms 2016.1] Fields inside nested sections and nested section templates are supported.
- [SINCE Orbeon Forms 2024.1] Fields inside repeated sections or grids are also supported.