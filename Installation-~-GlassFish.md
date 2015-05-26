> [[Home]] ▸ [[Installation]]

## GlassFish 3.1

On GlassFish, you need to do the following setup to avoid a `java.security.UnrecoverableKeyException` with the message _Password must not be null_:

1. Edit your domain's `domain.xml` (e.g. in `domains/domain1/config/domain.xml`).
2. Search for the section of the file that contains `<jvm-options>` elements, and there, add: `<jvm-options>-Djavax.net.ssl.keyStorePassword=changeit</jvm-options>`. If you changed your Glassfish [master password][8], set this property to your new password.