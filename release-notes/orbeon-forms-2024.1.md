# Orbeon Forms 2024.1

__Sunday, December xx, 2024__  

Today we released Orbeon Forms 2024.1! This release is absolutely packed with new features and bug-fixes!

## Major new features

xxx

## Other new features and enhancements

xxx

## Compatibility and upgrade notes

###

oxf.fr.summary.buttons: `excel-export-with-search` added by default

- captcha configuration: deprecation of tokens, use `fr:*`
- recaptacha properties v2 vs v3
- https://doc.orbeon.com/form-runner/component/captcha

### Simple data migration

The default is changed from `disabled` to `enabled` for new forms:

```xml
<property
    as="xs:string"  
    name="oxf.fr.detail.data-migration.*.*"                              
    value="{if (fr:created-with-or-newer('2024.1')) then 'enabled' else 'disabled'}"/>
```

### Index names

We previously had a typo in the names of some indexes for SQL Server, PostgreSQL, Oracle, and MySQL: some indices began with `orbeon_from` instead of `orbeon_form`. Although this typo doesn't cause any issues, you may want to check for it during your upgrade and rename the affected indices for consistency. We corrected this typo in the DDL we provided in November 2024, so if you created your indices before that date, you likely have this typo.