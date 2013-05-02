## Other functions

The function library is [documented here](http://wiki.orbeon.com/forms/doc/developer-guide/xforms-xpath-functions).

### xxf:r()

```xpath
xxf:r($resource-name as xs:string) as xs:string
```

- `$resource-name`: resource path of the form `foo/bar/baz` or `foo.bar.baz`. The path is relative to the `resource` element corresponding to the current language in the 

The function:

- determines the current language based on `xml:lang`attribute in scope where the function is in use
-  resolves the closest relevant resources instance
  - `orbeon-resources`
  - `fr-form-resources` (for Form Runner compatibility)
- uses the resource name specified to find a resource located in the resources instance, relative to the ``

The `xxf:r()` function:

- takes a resource path of the form  following the XML structure in the resources instance; `foo.bar.baz` is also supported 

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