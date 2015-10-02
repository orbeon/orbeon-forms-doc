## Availability

Orbeon Forms 4.11 and newer.

## Introduction

This processor converts a PDF document into a single image document.

When the output format is TIFF, the processor creates a multi-page TIFF containing one page for each page in the PDF
document. For other output formats, only the first page of the PDF document is used and a single image is created.

When using the TIFF format, if the destination format is bitonal (black and white), dithering is applied.

## Basic usage example

```xml
<p:processor name="oxf:pdf-to-image">
    <p:input name="data" href="#pdf-data"/>
    <p:input name="config">
        <config>
            <format>tiff</format>
            <compression>
                <type>LZW</type>
                <quality>0.8</quality>
            </compression>
            <scale>2.0</scale>
        </config>
    </p:input>
    <p:output name="data" id="image-data"/>
</p:processor>

```

## Inputs and outputs

- `data` input: receives the PDF document to convert as a binary document.
- `config` input: receives the configuration of the transformation.
- `data` output: produces the resulting image as a binary document.

The `config` input supports the following elements:

- `format`:
    - one of `gif`, `png`, `jpeg`, or `tiff`
    - mandatory
- `scale`:
    - scaling factor
    - optional
    - defaults to `1.0`
- `compression`
    - compression settings
    - optional
    - `type` child element
        - only meaningful for `tiff` format
        - one of `CCITT RLE`, `CCITT T.4`, `CCITT T.6`, `LZW`, `JPEG`, `ZLib`, `PackBits`, `Deflate`, `EXIF JPEG` or `none`
        - defaults to `none`
    - `quality` child element
        - only meaningful for `gif`, `jpeg` and `tiff` formats
        - value is a double-precision number from `0.0` to `1.0`

### Examples of configurations


GIF output:

```xml
<config>
    <format>gif</format>
    <scale>3.0</scale>
    <compression>
        <quality>0.5</quality>
    </compression>
</config>
```

JPEG output:

```xml
<config>
    <format>jpeg</format>
    <scale>3.0</scale>
    <compression>
        <quality>1.0</quality>
    </compression>
</config>
```

PNG output:

```xml
<config>
    <format>png</format>
    <scale>3.0</scale>
</config>
```

Black and white TIFF output with "CCITT T.6" compression:

```xml
<config>
    <scale>3.0</scale>
    <format>tiff</format>
    <compression>
        <type>CCITT T.6</type>
        <quality>0.5</quality>
    </compression>
</config>
```

Uncompressed TIFF output:

```xml
<config>
    <scale>3.0</scale>
    <format>tiff</format>
    <compression>
        <type>none</type>
    </compression>
</config>
```

Compressed TIFF output with LZW compression:

```xml
<config>
    <scale>3.0</scale>
    <format>tiff</format>
    <compression>
        <type>LZW</type>
        <quality>1.0</quality>
    </compression>
</config>
```