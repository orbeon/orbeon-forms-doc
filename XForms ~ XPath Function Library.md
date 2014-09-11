> [[Home]] ▸ [[XForms]]

## Main function library documentation

The function library is [documented here](http://wiki.orbeon.com/forms/doc/developer-guide/xforms-xpath-functions). New functions are documented below.

## XForms 2.0 functions

### xf:valid()

[SINCE Orbeon Forms 4.3]

```ruby
xf:valid() as xs:boolean
xf:valid($items as item()*) as xs:boolean
xf:valid($items as item()*, $relevant as xs:boolean) as xs:boolean
xf:valid($items as item()*, $relevant as xs:boolean, $recurse as xs:boolean) as xs:boolean
```

The `valid()` function returns the validity of XPath items, including instance data nodes.

### xf:bind()

[SINCE Orbeon Forms 4.5]

```ruby
xf:bind($id as xs:string) as node()*
```

This function returns the sequence of nodes associated with the bind specified by the `id` parameter.

## Extension XForms functions

### xxf:r()

The purpose of this function is to automatically resolve resources by name given the current language and an XForms instance containing localized resources.

```ruby
xxf:r($resource-name as xs:string) as xs:string
xxf:r($resource-name as xs:string, $instance-name as xs:string) as xs:string
```

- `$resource-name`: resource path of the form `foo.bar.baz`. The path is relative to the `resource` element corresponding to the current language in the resources instance.
- `$instance-name`: name of the resources instance. If omitted, search `orbeon-resources` and then `fr-form-resources`.

The function:

- determines the current language based on `xml:lang`attribute in scope where the function is in used
-  resolves the closest relevant resources instance
  - specified instance name if present
  - `orbeon-resources` or `fr-form-resources` (for Form Runner compatibility) if absent
- uses the resource name specified to find a resource located in the resources instance, relative to the `resource` element with the matching language

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

### xxf:get-request-method()

[SINCE: Orbeon Forms 4.2]

Return the current HTTP method.

```ruby
xxf:get-request-method() as xs:string
```

Return the HTTP method of the current request, such as `GET`, `POST`, etc.

### xxf:get-portlet-mode()

[SINCE: 2013-05-29 / Orbeon Forms 4.2]

Return the portlet mode.

```ruby
xxf:get-portlet-mode() as xs:string
```

If running within a portlet context, return the portlet mode (e.g. `view`, `edit`), otherwise return the empty sequence.

*NOTE: This function only works with the full portlet. The proxy portlet is not supported.*

### xxf:get-window-state()

[SINCE: 2013-05-29 / Orbeon Forms 4.2]

Return the portlet window state.

```ruby
xxf:get-window-state() as xs:string
```

If running within a portlet context, return the window state (e.g. `normal`, `minimized`, `maximized`), otherwise return the empty sequence.

*NOTE: This function only works with the full portlet. The proxy portlet is not supported.*

### xxf:forall()

[SINCE: 2013-07-18 / Orbeon Forms 4.3]

```ruby
xxf:forall(
  $items as item()*,
  $expr as jt:org.orbeon.saxon.functions.Evaluate-PreparedExpression
) as xs:boolean
```

Return true if `$expr` returns `true()` for all items in `$items`. If `$items` is the empty sequence, return `true()`.

`$expr` is a Saxon stored expression which:

- takes an `item()` as context item
- must return an `xs:boolean`

```xml
<xf:var
  name="is-available"
  value="saxon:expression('xxf:split(., ''/'')[3] = ''true''')"/>

<xf:bind
  ref="unpublish-button"
  readonly="not(normalize-space(../selection)
            and xxf:forall(xxf:split(../selection), $is-available))"/>
```

### xxf:exists()

[SINCE: 2013-07-18 / Orbeon Forms 4.3]

```ruby
xxf:exists(
  $items as item()*,
  $expr as jt:org.orbeon.saxon.functions.Evaluate-PreparedExpression
) as xs:boolean
```

Return true if `$expr` returns `true()` for at least one item in `$items`. If `$items` is the empty sequence, return `false()`.

`$expr` is a Saxon stored expression which:

- takes an `item()` as context item
- must return an `xs:boolean`

```xml
<xf:var
  name="is-available"
  value="saxon:expression('xxf:split(., ''/'')[3] = ''true''')"/>

<xf:bind
  ref="publish-button"
  readonly="not(normalize-space(../selection)
            and not(xxf:exists(xxf:split(../selection), $is-available)))"/>
```

### xxf:split()

[SINCE: Orbeon Forms 4.3]

```ruby
xxf:split() as xs:string*
xxf:split($value as xs:string) as xs:string*
xxf:split($value as xs:string, $separators as xs:string) as xs:string*
```

Split a string with one or more separator characters.

If no argument is passed, split the context item on white space.

If `$separator` is specified, each character is allowed as separator.

```ruby
xxf:split(' foo  bar   baz ')
xxf:split('foo$bar_baz', '$_')
element/xxf:split()
element/@value/xxf:split()
```

### xxf:client-id()

[SINCE: Orbeon Forms 4.3]

```ruby
xxf:client-id($static-or-absolute-id as xs:string) as xs:string?
```

Resolve the XForms object with the id specified, and return the id as used on the client.

Return the empty sequence if the resolution fails.

```xml
<xh:a href="#{xxf:client-id('my-element')}"/>

<xh:div id="my-element" xxf:control="true">...</xh:div>
```

### xxf:image-metadata()

[SINCE: Orbeon Forms 4.4]

```ruby
xxf:image-metadata($content as xs:anyURI, $name as xs:string) as xs:item?
```

Access basic image metadata.

The function returns the empty sequence if the URL is empty or the metadata requested is not found.

- `$content`: URL pointing to an image
- `$name`: metadata property name
    - `width`: image width in pixels, returns an `xs:integer` if found
    - `height`: image height in pixels, returns an `xs:integer` if found
    - `mediatype`: image mediatype based on the content, returns an `xs:string` of `image/jpeg`, `image/png`, `image/gif` or `image/bmp` (the formats universally supported by browsers) if found

*NOTE: The function dereferences the content of the URL when called. Accesses to local files are likely to be faster than remote files.*

The following example validates that the image is within 10% of a 1x1 aspect ratio:

```xml
<xf:bind
  ref="uploaded-image"
  constraint="abs(xs:decimal(xxf:image-metadata(., 'width')) div xs:decimal(xxf:image-metadata(., 'height')) - 1.0) le 0.1"/>
```

