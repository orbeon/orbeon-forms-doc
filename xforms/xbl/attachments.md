# Attachment controls

## Encryption

[SINCE Orbeon Forms 2019.1]

If you create your own attachment control, and you provide a link pointing to the persistence API to download the attachment, you need to tell the persistence API that the file is encrypted. You know that a file has been encrypted because the element your control is bound to has the attribute `fr:attachment-encrypted="true"`. If this is the case, you need to add the header `Orbeon-Decrypt: true` so the persistence API knows it needs to decrypt the file after retrieving the attachment from the database. For instance using an `<xf:output appearance="xxf:download">`, you would do:

```xml
<xf:output appearance="xxf:download" ref="xxf:binding('your-component')">
    <xf:label><xh:i class="icon-download"/><xf:output value="xxf:r('download')"/></xf:label>
    <xf:filename  ref="@filename"  xxbl:scope="outer"/>
    <xf:mediatype ref="@mediatype" xxbl:scope="outer"/>
    <xf:header ref="fr:form-version()">
        <xf:name>Orbeon-Form-Definition-Version</xf:name>
        <xf:value value="."/>
    </xf:header>
    <xf:header ref="$binding[@fr:attachment-encrypted = 'true']">
        <xf:name>Orbeon-Decrypt</xf:name>
        <xf:value>true</xf:value>
    </xf:header>
</xf:output>
```

The persistence API could, without being told, know whether an attachment is encrypted since this information is stored in the form data. However this means that it would have to also retrieve the form data from the database every time it is being requested for an attachment. To avoid this step, the burden is instead put on attachment controls to provide this information to the persistence API, which they can do easily since they already have access to the form data.

See also:

- [Field-level encryption](/form-builder/field-level-encryption.md)
- [Encryption in the form data format](/form-runner/data-format/form-data.md#encryption)
