# Orbeon Forms 2023.1

__Saturday, December 31, 2023__

Today we released Orbeon Forms 2023.1! This release is packed with new features and bug-fixes!

## New features

TODO

## Compatibility notes

TODO

### Visiting of visible fields

In Orbeon Forms, a form control can be *visited* or not. Visited controls have typically been visited by the user, which means that the user navigated through the form control, possibly without changing its value. One way to visit form controls is to navigate using the "Tab" key, or to click on the form control and then click outside of it. Another way is to use the default "Save" or "Send" buttons, which by default visit all the form controls before proceeding. The notion is used to determine whether to show validation errors associated with that form control.

With Orbeon Forms 2023.1, form controls are also marked as visited when they are calculated, visible, and their value changes. This is useful to immediately show validation errors associated with such form controls, which are typically implemented with the Calculated Value form control.

If you don't wish this behavior, you can turn it off globally with the following property:

```xml
<property
    as="xs:boolean"
    name="oxf.xforms.xbl.fr.error-summary.visit-value-changed"
    value="false"/>
```

### Password strength checker

A [password strength checker](/configuration/properties/general.md#oxf.crypto.check-password-strength) will cause an error if one of the passwords configured in your `properties-local.xml` is too weak. Ideally, use randomly-generated strong passwords.

If you are using field-level encryption, and if you already have data in your database that contains encrypted fields, and if you are getting an error upon starting Orbeon Forms telling you that the encryption password is too weak, you can disable the password strength checker:

```xml
<property
    as="xs:boolean"
    name="oxf.crypto.check-password-strength"
    value="false"/>
```

We do not recommend disabling this in general. Instead, always use strong randomly-generated passwords. 

### Encryption passwords

If set, the `oxf.fr.field-encryption.password` property controls a separate encryption password for field-level encryption. If not set, the `oxf.crypto.password` property is used instead for backward compatibility.

```xml
<property
	as="xs:string"
	name="oxf.fr.field-encryption.password"
	value="CHANGE THIS PASSWORD"/>
```

- If you are upgrading from an earlier version of Orbeon Forms, and you already have data in your database that contains encrypted fields:
    - Set `oxf.fr.field-encryption.password` anyway, to the same value as `oxf.crypto.password`.
- If you are not in the above case, set `oxf.fr.field-encryption.password` to a value different from `oxf.crypto.password`. 

### CRUD API

When calling the [CRUD API](/form-runner/api/persistence/crud), you can `PUT` data as well as form definitions and their attachments.

When `PUT`ting data, Form Runner does a number of checks, including a check for permissions. In the past, in some cases, `PUT`ting data for a non-existent form definition could succeed. This is no longer the case, and you should make sure that the form definition exists before `PUT`ting data.

### eXist DB removal

This version of Orbeon Forms removes support for the eXist DB database. use of this database has been deprecated for a long time, and we have not been able to maintain it for a while. If you are using eXist DB, please migrate to a [relational database](/form-runner/persistence/relational-db.md).

### Uploading empty files

Starting with 2023.1, uploaded files that are empty are rejected out-of-the-box. Should you need to accept such files in your forms, you can [set a property to allow them](/xforms/controls/upload.md#empty-files).
