### Main function library documentation

The function library is [documented here](http://wiki.orbeon.com/forms/doc/developer-guide/xforms-xpath-functions). New functions are documented below.

### xxf:r()

The purpose of this function is to automatically resolve resources by name given the current language and an XForms instance containing localized resources.

```ruby
xxf:r($resource-name as xs:string) as xs:string
```

- `$resource-name`: resource path of the form `foo/bar/baz` or `foo.bar.baz`. The path is relative to the `resource` element corresponding to the current language in the 

The function:

- determines the current language based on `xml:lang`attribute in scope where the function is in use
-  resolves the closest relevant resources instance
  - `orbeon-resources`
  - `fr-form-resources` (for Form Runner compatibility)
- uses the resource name specified to find a resource located in the resources instance, relative to the ``

Example:

```xml
<xf:instance id="orbeon-resources" xxf:readonly="true">
    <resources>
        <resource xml:lang="en"><buttons><download>Download</download></buttons></resource>
        <resource xml:lang="fr"><buttons><download>Télécharger</download></buttons></resource>
    </resources>
</xf:instance>

<xf:label value="xxf:r('buttons.download')"/>
```