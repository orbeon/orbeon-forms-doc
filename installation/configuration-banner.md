# Configuration banner

## Availability

[\[SINCE Orbeon Forms 2023.1\]](/release-notes/orbeon-forms-2023.1.md)

## Rationale

Orbeon Forms requires a number of configuration steps to be performed before it can be used, mainly:

- Setting a password for encryption
- Setting a database provider

If neither of these is configured, Orbeon Forms shows a banner at the top of most application pages. In order to know more about what needs to be configured, [configure logging](/installation/logging.md) and check the Orbeon Forms log file. The file will contain something like this at the `ERROR` level:

```
The following Orbeon Forms configurations are incomplete:

- The password for the `oxf.crypto.password` property is missing or not strong enough.
- The password for the `oxf.fr.access-token.password` property is missing or not strong enough.
- The password for the `oxf.fr.field-encryption.password` property is missing or not strong enough.
- The database configuration is missing or incomplete.
```

In addition, feature-specific passwords must be set for two features which are not enabled by default:

- [Token-based permissions](/form-runner/access-control/tokens.md)
- [Field-level encryption](/form-builder/field-level-encryption.md)

You do not have to configure these features if you do not use them. However, the fact that these features are not configured will be logged at the `INFO` level.

## Configurations

### Database

Out-of-the-box, Orbeon Forms includes an SQLite embedded database with multiple demo forms. This setup is designed for a quick start, but for development or production use, you should configure Orbeon Forms to utilize a proper relational database. For more information, see [Relational Database](/form-runner/persistence/relational-db.md).

The reason for considering this an incomplete configuration is that the SQLite database is not suitable for production use with Orbeon Forms at this point. This is not necessarily due to shortcomings of SQLite, but due to the fact that the SQLite database is embedded in the Orbeon Forms WAR file and more likely to be deleted by mistake. In addition, most users will want to use a more powerful database for production use.

See also [Database setup](/installation/README.md#database-setup).

### Main encryption password

You must set the [`oxf.crypto.password`](/configuration/properties/general.md#oxfcryptopassword) property to something different from the default. This is used for the following:

- internal encryption (admin token, operations, internally-submitted data, uploaded URLs)
- product version in the URL
- `cid:` URLs and XML metadata format

This password is typically used for transient data, but you should still change it.

### Access token password

If you plan to use [Token-based permissions](/form-runner/access-control/tokens.md), also set `oxf.fr.access-token.password`. If you don't set it and try to use access tokens, an error will be produced when the password is required.

### Field-level encryption password

If you plan to use [Field-level encryption](/form-builder/field-level-encryption.md), also set `oxf.fr.field-encryption.password`. If you don't set it and try to use field-level encryption, an error will be produced when the password is required.

## See also

- [Installation](/installation/README.md)
- [Logging](/installation/logging.md)
- [Properties](/configuration/properties/README.md)
