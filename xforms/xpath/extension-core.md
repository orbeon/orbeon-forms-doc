# Core functions

<!-- toc -->

## xxf:document-id()

```ruby
xxf:document-id() as xs:string`
```

Each time an XForms document is created, typically when a user requests a page, it is assigned a unique id.

This function returns the current XForms document (or page)'s unique id.

## xxf:evaluate-avt()

```ruby
xxf:evaluate-avt(
    $avt-expression as xs:string
) as item()*
```

This function is similar to `saxon:evaluate()` or `xxf:evaluate()`, but instead of evaluating an XPath expression, it evaluates a string representing an attribute value template.

```xml
<xf:output
    value="xxf:evalute-avt('/xforms-sandbox/service/zip-zips?state-abbreviation={state}&amp;city={city}')"/>
```

## xxf:format-message()

```ruby
xxf:format-message($template as xs:string, $parameters as item()*) as xs:string
```

The `xxf:format-message()` function allows you to format a localized message based on a template and parameters.

* the first parameter is a template string following the syntax of the Java [MessageFormat](http://docs.oracle.com/javase/7/docs/api/java/text/MessageFormat.html) class
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

## xxf:instance()

```ruby
xxf:instance(
    $instance-id as xs:string
) as element()?
```

The `xxf:instance()` function works like the standard `instance()` function except that it searches for instances:

* independently from the current in-scope model specified by the `model` attribute
* across ancestor XBL scopes

The search works as follows:

* first, in a given XBL scope, the function searches all models within that scope
* second, it recursively searches ancestor XBL scopes

For example, if there are no XBL components, and 2 top-level models:

```xml
<xf:model id="main-model">
  <xf:instance id="main-instance">
    ...
  </xf:instance>
</xf:model>
<xf:model id="resources-model">
  <xf:instance id="resources-instance">
    ...
  </xf:instance>
</xf:model>
...
<xf:group model="main-model">
  <xf:output value="xxf:instance('resources-instance')/title"/>
</xf:group>
```

[SINCE Orbeon Forms 4.5] The `xxf:instance()` function can also take an absolute id as parameter.

## xxf:lang()

```ruby
xxf:lang() as xs:string?
```

The `xxf:lang()` function returns the current language as specified with the `xml:lang` attribute.

The `xml:lang` value to use is determined this way:

* if the element containing the `xxf:lang()` function has an `xml:lang` attribute, that attribute is used
* otherwise, the first ancestor element of the element containing the `xxf:lang()` function that has an `xml:lang` attribute is used

`xml:lang` is supported in the following locations:

* for a static `xml:lang` value
    * on any XForms element
    * on the top-level `<xh:html>` element
* for a dynamic xml:lang value (using an AVT)
    * only on the top-level `<xh:html>` element

_NOTE: `xml:lang` attributes on HTML elements other than the top-level `<xh:html>` are ignored for the purpose of the `xxf:lang()` function._

_NOTE: Format of the locale is currently restricted to the form "en" or "en-GB". Support for [BCP 47](http://www.rfc-editor.org/rfc/bcp/bcp47.txt) would be desirable._

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



## xxf:property()

```
xxf:property($property-name as xs:string) as xs:anyAtomicType?
```

The `xxf:property()` function retrieves the value of a property defined in `properties-local.xml`.

This function returns the following:

- empty sequence if the property is not found
- `xs:string`, `xs:integer`, `xs:boolean` or `xs:anyURI` depending on the actual type of the property

```xml
<xf:setvalue ref="my-property" value="xxf:property('my.property.name')"/>
```

## xxf:r()

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

## xxf:rewrite-resource-uri()

```ruby
xxf:rewrite-resource-uri($uri as xs:string) as xs:string
```

Rewrite a URI as an Orbeon Forms resource URI.

## xxf:split()

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
