# Other functions

<!-- toc -->

## xxf:decode-iso9075-14()

```ruby
xxf:decode-iso9075-14($value as xs:string) as xs:string
```

The `xxf:decode-iso9075-14()` function decodes a string according to ISO 9075-14:2003.

## xxf:doc-base64()

```ruby
xxf:doc-base64($href as xs:string) as xs:string
```

The `xxf:doc-base64()` function reads a resource identified by the given URL, and returns the content of the file as a Base64-encoded string. It is a dynamic XPath error if the resource cannot be read.

## xxf:doc-base64-available()

```ruby
xxf:doc-base64-available($href as xs:string) as xs:boolean
```

The `xxf:doc-base64-available()` function reads a resource identified by the given URL. It returns `true()` if the file can be read, `false()` otherwise.

## xxf:encode-iso9075-14()

```ruby
xxf:encode-iso9075-14($value as xs:string) as xs:string
```

The `xxf:encode-iso9075-14()` function encodes a string according to ISO 9075-14:2003. The purpose is to escape any character which is not valid in an XML name.

## xxf:image-metadata()

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

