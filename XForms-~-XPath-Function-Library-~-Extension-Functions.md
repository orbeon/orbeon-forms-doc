> [[Home]] ▸ [[XForms]] ▸ [[XForms ~ XPath Function Library]]

## Introduction

The following functions are documented on this page:

- Validation functions
    - `xxf:max-length()`
    - `xxf:min-length()`
- HTTP request functions
    - `xxf:username()`
    - `xxf:user-group()`
    - `xxf:user-roles()`
    - `xxf:get-request-method()`
    - `xxf:get-portlet-mode()`
    - `xxf:get-window-state()`
- Other functions
    - `xxf:r()`
    - `xxf:forall()`
    - `xxf:exists()`
    - `xxf:split()`
    - `xxf:client-id()`
    - `xxf:image-metadata()`
    - `xxf:call-xpl()`
    - `xxf:encode-iso9075-14()`
    - `xxf:decode-iso9075-14()`
    - `xxf:doc-base64()`
    - `xxf:doc-base64-available()`
    - `xxf:lang()`
    - `xxf:format-message()`
    - `xxf:form-urlencode()`
    - `xxf:rewrite-resource-uri()`
    - `xxf:has-class()`
    - `xxf:classes()`

## Validation functions

### xxf:max-length()

[SINCE Orbeon Forms 4.10]

```ruby
xxf:max-length($max as xs:integer?) as xs:boolean
```

Return `true()` if the context item converted to a string via the `string()` function contains at most the number of characters
specified by `$max.` Also return `true()` if `$max` is the empty sequence.

Following [XPath 2.0](http://www.w3.org/TR/xpath-functions/#string-types):

> what is counted is the number of XML characters in the string (or equivalently, the number of Unicode code points). Some implementations may represent a code point above xFFFF using two 16-bit values known as a surrogate. A surrogate counts as one character, not two.

### xxf:min-length()

[SINCE Orbeon Forms 4.10]

```ruby
xxf:min-length($min as xs:integer?) as xs:boolean
```

Return `true()` if the context item converted to a string via the `string()` function contains at least the number of characters
specified by `$min.` Also return `true()` if `$min` is the empty sequence.

Following [XPath 2.0](http://www.w3.org/TR/xpath-functions/#string-types):

> what is counted is the number of XML characters in the string (or equivalently, the number of Unicode code points). Some implementations may represent a code point above xFFFF using two 16-bit values known as a surrogate. A surrogate counts as one character, not two.

## HTTP request functions

### xxf:username()

[SINCE Orbeon Forms 4.9]

```ruby
xxf:username() as xs:string?
```

Return the current user's username if available. This function works with container- and header-driven methods. See [[Form Runner Access Control Setup|Form Runner ~ Access Control ~ Setup]].

### xxf:user-group()

[SINCE Orbeon Forms 4.9]

```ruby
xxf:user-group() as xs:string?
```

Return the current user's group if available. This function works with container- and header-driven methods. See [[Form Runner Access Control Setup|Form Runner ~ Access Control ~ Setup]].

### xxf:user-roles()

[SINCE Orbeon Forms 4.9]

```ruby
xxf:user-roles() as xs:string*
```

Return the current user's groups if available. This function works with container- and header-driven methods. See [[Form Runner Access Control Setup|Form Runner ~ Access Control ~ Setup]].


### xxf:get-request-method()

[SINCE Orbeon Forms 4.2]

Return the current HTTP method.

```ruby
xxf:get-request-method() as xs:string
```

Return the HTTP method of the current request, such as `GET`, `POST`, etc.

### xxf:get-portlet-mode()

[SINCE Orbeon Forms 4.2]

Return the portlet mode.

```ruby
xxf:get-portlet-mode() as xs:string
```

If running within a portlet context, return the portlet mode (e.g. `view`, `edit`), otherwise return the empty sequence.

*NOTE: This function only works with the full portlet. The proxy portlet is not supported.*

### xxf:get-window-state()

[SINCE Orbeon Forms 4.2]

Return the portlet window state.

```ruby
xxf:get-window-state() as xs:string
```

If running within a portlet context, return the window state (e.g. `normal`, `minimized`, `maximized`), otherwise return the empty sequence.

*NOTE: This function only works with the full portlet. The proxy portlet is not supported.*

## Other functions

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

### xxf:forall()

[SINCE Orbeon Forms 4.3]

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
  readonly="
    not(
        normalize-space(../selection) and
        xxf:forall(xxf:split(../selection), $is-available)
    )"/>
```

### xxf:exists()

[SINCE Orbeon Forms 4.3]

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
  readonly="
    not(
        normalize-space(../selection) and
        not(xxf:exists(xxf:split(../selection), $is-available))
    )"/>
```

### xxf:split()

[SINCE Orbeon Forms 4.3]

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

[SINCE Orbeon Forms 4.3]

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

[SINCE Orbeon Forms 4.4]

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
  constraint="
    abs(
        xs:decimal(xxf:image-metadata(., 'width')) div
        xs:decimal(xxf:image-metadata(., 'height')) - 1.0
    ) le 0.1"/>
```

### xxf:call-xpl()

```ruby
xxf:call-xpl(
    $xplURL        as xs:string,
    $inputNames    as xs:string*,
    $inputElements as element()*,
    $outputNames   as xs:string+
) as document-node()*
```

This function lets you call an XPL pipeline.

- `$xplURL` is the URL of the pipeline. It must be an absolute URL.
- `$inputNames` is a sequence of strings, each one representing the name of an input of the pipeline that you want to connect.
- `$inputElements` is a sequence of elements to be used as input for the pipeline. The `$inputNames` and `$inputElements` sequences must have the same length. For each element in `$inputElements`, a document is created and connected to an input of the pipeline. Elements are matched to input name by position, for instance the element at position 3 of `$inputElements` is connected to the input with the name specified at position 3 in `$inputNames`.
- `$outputNames` is a sequence of output names to read.

The function returns a sequence of document nodes corresponding the output of the pipeline. The returned sequence will have the same length as `$outputNames` and will correspond to the pipeline output with the name specified on `$outputNames` based on position.

The example below shows a call to the `xxf:call-xpl` function, calling a pipeline with two inputs and one output :

```ruby
xxf:call-xpl(
    'oxf:/examples/sandbox/xpath/run-xpath.xpl',
    (
        'input',
        'xpath'
    ),
    (
        instance('instance')/input,
        instance('instance')/xpath
    ),
    'html'
)
```

### xxf:encode-iso9075-14()

```ruby
xxf:encode-iso9075-14($value as xs:string) as xs:string
```

The `xxf:encode-iso9075-14()` function encodes a string according to ISO 9075-14:2003. The purpose is to escape any character which is not valid in an XML name.

### xxf:decode-iso9075-14()

```ruby
xxf:decode-iso9075-14($value as xs:string) as xs:string
```

The `xxf:decode-iso9075-14()` function decodes a string according to ISO 9075-14:2003.

### xxf:doc-base64()

```ruby
xxf:doc-base64($href as xs:string) as xs:string
```

The `xxf:doc-base64()` function reads a resource identified by the given URL, and returns the content of the file as a Base64-encoded string. It is a dynamic XPath error if the resource cannot be read.

### xxf:doc-base64-available()

```ruby
xxf:doc-base64-available($href as xs:string) as xs:boolean
```

The `xxf:doc-base64-available()` function reads a resource identified by the given URL. It returns `true()` if the file can be read, `false()` otherwise.

### xxf:lang()

```ruby
xxf:lang() as xs:string?
```

The `xxf:lang()` function returns the current language as specified with the `xml:lang` attribute.

The `xml:lang` value to use is determined this way:

* if the element containing the `xxf:lang()` function has an `xml:lang` attribute, that attribute is used
* otherwise, the first ancestor element of the element containing the `xxf:lang()` function that has an `xml:lang` attribute is used

`xml:lang` is supported in the following locations:

* for a static xml:lang value
    * on any XForms element
    * on the top-level `<xh:html>` element
* for a dynamic xml:lang value (using an AVT)
    * only on the top-level `<xh:html>` element

_NOTE: `xml:lang` attributes on HTML elements other than the top-level `<xh:html>` are ignored for the purpose of the `xxf:lang()` function._

_NOTE: Format of the locale is currently restricted to the form "en" or "en-GB". Support for [BCP 47][4] would be desirable._

Example of static values:

```xml
<xf:group xml:lang="it">
    <!-- This output produces the value "it" -->
    <xf:output value="xxf:lang()"/>
    <!-- This output produces the value "zh" -->
    <xf:output value="xxf:lang()" xml:lang="zh"/>
</xf:group>
```

Example with an AVT:

```xml
<xh:html xml:lang="{instance()}">
    <xh:head>
        <xf:model id="model">
            <xf:instance id="instance">
                <lang>it</lang>
            </xf:instance>
        </xf:model>
    </xh:head>
    <xh:body>
        <xf:group>
            <!-- This output produces "it" based on the top-level AVT, which
                 contains the value stored in the instance -->
            <xf:output value="xxf:lang()"/>
            <!-- This output produces "zh" -->
            <xf:output value="xxf:lang()" xml:lang="zh"/>
        </xf:group>
    </xh:body>
</xh:html>
```

When calling the `xxf:lang()` function from an XBL component:

- `xml:lang` is first searched as described above
- if no `xml:lang` value is found in the XBL component, then the `xml:lang` value of the XBL bound element is searched

Example:

```xml
<xbl:xbl>
    <xbl:binding id="fr-foo" element="fr|foo">
        <xbl:template>
            <xf:group>
                <!-- The xml:lang value of the bound element is used -->
                <xf:output value="xxf:lang()"/>
                <!-- The xml:lang value is "zh" -->
                <xf:output value="xxf:lang()" xml:lang="zh"/>
            </xf:group>
        </xbl:template>
    </xbl:binding>
</xbl:xbl>
```

### xxf:format-message()


```ruby
xxf:format-message($template as xs:string, $parameters as item()*) as xs:string
```

The `xxf:format-message()` function allows you to format a localized message based on a template and parameters.

* the first parameter is a template string following the syntax of the Java [MessageFormat][5] class
* the second parameter is a sequence of parameters that can be referenced from the template string

The following types are supported:

* string (the default)
* number (including currency and percent)
* date
* time

The function uses the current language as would be obtained by the `xxf:lang()` function to determine a locale.

Example with number, date, time, and string:

```xml
<xf:output
    value="
        xxf:format-message(
            'At {2,time,short} on {2,date,long}, we detected {1,number,integer} spaceships on the planet {0}.',
            (
                'Mars',
                3,
                xs:dateTime('2010-07-23T19:25:13-07:00')
            )
        )"/>
```

This produces the following output with an en-US locale:

```
At 7:25 PM on July 23, 2010, we detected 3 spaceships on the planet Mars.
```

Example including a choice:

```xml
<xf:output
    value="
        xxf:format-message(
            'There {0,choice,0#are no files|1#is one file|1&lt;are {0,number,integer} files}.',
            xs:integer(.)
        )"/>
```

This produces the following outputs, depending on the value provided:

```
There are no files.
There is one file.
There are 1,273 files.
```

_NOTE: It is important to pass dates and times as typed values. Use `xs:dateTime()`, `xs:date()`, or `xs:time()` if needed to convert from a string._

### xxf:form-urlencode()

```ruby
xxf:form-urlencode($document as node()) as xs:string
```

Performs `application/x-www-form-urlencoded` encoding on an XML document.

### xxf:rewrite-resource-uri()

```ruby
xxf:rewrite-resource-uri($uri as xs:string) as xs:string
```

Rewrite a URI as an Orbeon Forms resource URI.

### xxf:has-class()

```ruby
xxf:has-class($class-name as xs:string) as xs:boolean
xxf:has-class($class-name as xs:string, $el as node()) as xs:boolean
```

Returns whether the context element, or the given element, has a `class` attribute containing the specified class name.

### xxf:classes()

```ruby
xxf:classes() as xs:boolean
xxf:classes($el as node()) as xs:string*
```

Returns for the context element or the given element if provided, all the classes on the `class` attribute.