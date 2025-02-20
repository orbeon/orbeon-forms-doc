# Autosave

## Support

See [Database Support](/form-runner/persistence/db-support.md) for the detail of which persistence layers and Orbeon Forms versions support the autosave feature.

## What it is

Orbeon Forms has the ability to autosave data as the user fills in a form. This helps ensure that there is no data loss, even if the user hasn't saved or submitted the form.

When form data is autosaved, it creates *autosave drafts*:

- __Edit page:__ Only a single autosave draft can be associated with the data's document id.
- __New page:__ Multiple autosave drafts can be created.

Orbeon Forms gives the user the ability to see and recover data from autosave drafts. More details are provided below.

## Enabling autosave

The following conditions must be met for autosave to happen:

- The user must be logged in.
- The user must have update permissions (if permissions are enabled for the form).
- The form mode must be `new` or `edit`.
- The persistence layer must support autosave and have the `oxf.fr.persistence.$name.autosave` property set to `true` (`true` by default for the built-in relational databases).
- The autosave delay (set with `oxf.fr.detail.autosave-delay`) must be greater than `0` (which is the case by default).

*NOTE: Form Builder doesn't yet support autosave as of Orbeon Forms 2019.1.*

## Disabling autosave

If the following conditions above are met, autosave will be enabled. In this case, if you wish to disable autosave, you can set the `oxf.fr.detail.autosave-delay` to 0:

```xml
<property 
    as="xs:integer" 
    name="oxf.fr.detail.autosave-delay.*.*" 
    value="0"/>
```

You can as usual disable it for specific forms only, for example:

```xml
<property 
    as="xs:integer" 
    name="oxf.fr.detail.autosave-delay.acme.order" 
    value="0"/>
```

## How autosave works

When autosave is enabled and you are an authenticated user, form data is automatically saved as *drafts* in the background as you enter and modify form data. This reduces the chance that you will lose data if something goes wrong and you haven't explicitly saved the data.

### Summary page

The summary page shows the drafts, on separate lines and clearly marked as such. From the summary page, users can click on a draft to open it, or select it to then delete it, assuming they have the permission to do so.

![Summary page](../images/autosave-summary.png)

### Edit page

If users edit a form for which there is a draft, they will be asked whether they want to open the saved data, or start from the autosaved draft.

![Edit page](../images/autosave-open.png)

### New page

If users started filling out a new form, but didn't save the data, if starting to fill out a new form later, they will be asked whether they wish to start from scratch, or from one of the drafts saved earlier. In this case, the prompt will be different whether there is just one draft for new data, or multiple drafts available, as in the latter case, users will need to select which draft they want to use.

![New page, single draft](../images/autosave-new-single.png)

![New page, single draft](../images/autosave-new-multiple.png)

When multiple drafts are available, choosing the "View autosaved drafts" button takes you to the form's Summary page in a special mode where only the relevant drafts are visible:

![Summary page with drafts only](../images/autosave-summary-drafts-only.png)

## Configuration

### With Orbeon Forms 4.4 and newer

You don't need to do anything special to use this feature.

### Properties

```xml
<property
  as="xs:integer"
  name="oxf.fr.detail.autosave-delay.*.*"
  value="5000"/>
```

The value of `oxf.fr.detail.autosave-delay` sets the maximum time, in milliseconds, after which a change done by the user will be autosaved. If 0 or negative, autosaving is disabled. Out of the box, this property is set to `5000`, that is 5 seconds. If your server experiences a lot of load, you might want to make that value higher to reduce the number of requests made by Orbeon Forms to your database.

The following property enables or disable autosave for a given persistence provider, as autosave requires support from the persistence provider. For database support, see [Database Support](../../form-runner/persistence/db-support.md).

```xml
<property
  as="xs:boolean"
  name="oxf.fr.persistence.*.autosave"
  value="false"/>
```

### With Orbeon Forms 4.3

With Orbeon Forms 4.3 specifically, you need to:

- If using MySQL, update your database by running [this  DDL](https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/apps/fr/persistence/relational/ddl/mysql-4_3-to-4_4.sql). (The tables for DB2 on 4.3 already contain the required changes out-of-the-box.)
- Set the following properties:

```xml
<property as="xs:boolean" name="oxf.fr.support-owner-group" value="true"/>
<property as="xs:boolean" name="oxf.fr.support-autosave"    value="true"/>
```

## See also

- [Blog post](https://blog.orbeon.com/2013/10/autosave.html).