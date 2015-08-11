> [[Home]] ▸ [[Form Runner|Form Runner]]

## Introduction

PDF files, whether produced automatically or via a [[PDF template|Form Builder ~ PDF Production ~ PDF Templates]], can
be converted to TIFF image files for download or email.

## Availability

[SINCE Orbeon Forms 4.11]

This is an Orbeon Forms PE feature.

## Setup

### Using buttons

TIFF files can be accessed directly from the Form Runner Summary Page and Detail Page using the `tiff` button.

Example for the Detail Page:

```xml
<property as="xs:string" name="oxf.fr.detail.buttons.orbeon.controls">
    summary wizard-prev wizard-next pdf tiff save review
</property>
```

Example for the Summary Page:

```xml
<property as="xs:string"  name="oxf.fr.summary.buttons.orbeon.controls">
    home review pdf tiff delete duplicate new
</property>
```

See also [[Predefined buttons|Form Runner ~ Buttons and Processes#predefined buttons]].

### Configuration

The TIFF compression type can be selected with the `oxf.fr.detail.tiff.compression.type` property:

```xml
<property
    as="xs:string"
    name="oxf.fr.detail.tiff.compression.type.*.*"
    value="LZW"/>
```

- possible value: `CCITT RLE`, `CCITT T.4`, `CCITT T.6`, `LZW`, `JPEG`, `ZLib`, `PackBits`, `Deflate`, `EXIF JPEG` or `none`
- default: `LZW`

The compression quality can be selected with the `oxf.fr.detail.tiff.compression.quality` property:

```xml
<property
    as="xs:string"
    name="oxf.fr.detail.tiff.compression.quality.*.*"
    value="0.8"/>
```

- possible values: `0.0` to `1.0`
- default: `0.8`

The scale factor can be selected with the `oxf.fr.detail.tiff.scale` property:

```xml
<property
    as="xs:string"
    name="oxf.fr.detail.tiff.scale.*.*"
    value="3.0"/>
```

- possible values: positive double-precision number
- default: `3.0`
