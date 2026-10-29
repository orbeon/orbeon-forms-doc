# CSV functions

## xxf:csv-to-xml()

[SINCE Orbeon Forms 2026.1]

The `xxf:csv-to-xml()` function takes a CSV string and returns an XML document following the [XForms 2.0 CSV conversion scheme](https://www.w3.org/community/xformsusers/wiki/XForms_2.0#External_CSV_values).

```xpath
xxf:csv-to-xml() as document-node()?
xxf:csv-to-xml($csv as xs:string?) as document-node()?
xxf:csv-to-xml($csv as xs:string?, $header as xs:boolean?) as document-node()?
```

### Parameters

- `$csv`:
    - The CSV string to parse.
    - If omitted, the context item is converted to a string using the `string()` function.
    - If `$csv` is the empty sequence, the function returns the empty sequence.
- `$header`:
    - Optional parameter indicating whether the first row of the CSV input is a header row.
    - Defaults to `true()`.
    - Accepts a boolean (`true()` or `false()`).

### XML structure

The returned document has a `<csv>` root element:

- **With headers (`$header` is `true()`, the default):**
    - The first row of the CSV input is used as a header row.
    - Each header value is converted into a valid XML `NCName` to be used as an element name:
        - Leading and trailing whitespace is trimmed.
        - An empty or blank header becomes `_`.
        - Characters that are not valid in an `NCName` (such as spaces or punctuation) are replaced with `_`.
        - If the first character is a valid `NCName` character but not a valid `NCName` start character (such as a digit or `-`), `_` is prepended (for example, `123` becomes `_123`).
    - An `<h>` element is created as the first child of `<csv>`, containing one child element per header column. Each child element is named using the derived `NCName` and has the original header string as its text content.
    - Each subsequent CSV row is represented as an `<r>` element under `<csv>`. Within each `<r>`, each field is represented by an element named after the corresponding column's derived `NCName` (or `<v>` if the row has more fields than the header row).
- **Without headers (`$header` is `false()`):**
    - No `<h>` element is created.
    - Every CSV row is represented as an `<r>` element under `<csv>`.
    - Each field within an `<r>` element is represented as a `<v>` element.

### CSV parsing rules

- Fields are separated by commas (`,`), and rows are separated by `CRLF` (`\r\n`) or `LF` (`\n`).
- Unquoted fields have leading and trailing whitespace trimmed.
- Fields enclosed in double quotes (`"..."`) preserve whitespace, commas, and line breaks.
- Double quotes inside a quoted field are escaped by doubling them (`""`).

### Examples

With a header row (default):

```csv
Name, Hex value
Red, #f00
Green, #0f0
Blue, #00f
```

`xxf:csv-to-xml($csv)` returns:

```xml
<csv>
    <h>
        <Name>Name</Name>
        <Hex_value>Hex value</Hex_value>
    </h>
    <r>
        <Name>Red</Name>
        <Hex_value>#f00</Hex_value>
    </r>
    <r>
        <Name>Green</Name>
        <Hex_value>#0f0</Hex_value>
    </r>
    <r>
        <Name>Blue</Name>
        <Hex_value>#00f</Hex_value>
    </r>
</csv>
```

Without a header row:

```csv
2011, "Jan, Mar"
2012, "Feb, Apr"
```

`xxf:csv-to-xml($csv, false())` (or `xxf:csv-to-xml($csv, false())`) returns:

```xml
<csv>
    <r>
        <v>2011</v>
        <v>Jan, Mar</v>
    </r>
    <r>
        <v>2012</v>
        <v>Feb, Apr</v>
    </r>
</csv>
```

## xxf:xml-to-csv()

[SINCE Orbeon Forms 2026.1]

The `xxf:xml-to-csv()` function takes an XML document or element following the [XForms 2.0 CSV conversion scheme](https://www.w3.org/community/xformsusers/wiki/XForms_2.0#External_CSV_values) and converts it to a CSV string.

```xpath
xxf:xml-to-csv() as xs:string?
xxf:xml-to-csv($xml as node()?) as xs:string?
xxf:xml-to-csv($xml as node()?, $header as item()?) as xs:string?
```

### Parameters

- `$xml`:
    - A document node or element node (typically the document returned by `xxf:csv-to-xml()`, its `<csv>` root element, or a single `<r>` row element).
    - If omitted, the context item is used.
    - If `$xml` is the empty sequence, the function returns the empty sequence.
- `$header`:
    - Optional parameter indicating whether a header row should be included in the CSV output.
    - Defaults to `true()`.
    - Accepts a boolean (`true()` or `false()`).

### Conversion rules

- **Header row:**
    - When `$header` is `true()` (the default):
        - If an `<h>` child element is present, the header row is built from the children of `<h>` (using each child's trimmed text value if non-blank, or its local element name otherwise).
        - If no `<h>` child element is present and the first `<r>` element has child elements whose names are not all `<v>`, the local names of the first `<r>` element's children are used as the header row.
        - Otherwise, no header row is emitted.
    - When `$header` is `false()`, no header row is emitted, even if an `<h>` element is present in `$xml`.
- **Data rows:**
    - Each `<r>` element produces a CSV row with the string values of its child elements.
- **Formatting and escaping:**
    - Fields are separated by commas (`,`), and rows are separated by `CRLF` (`\r\n`).
    - Fields containing a comma (`,`), double quote (`"`), carriage return (`\r`), line feed (`\n`), or leading/trailing spaces are enclosed in double quotes (`"..."`), with inner double quotes escaped as `""`.
