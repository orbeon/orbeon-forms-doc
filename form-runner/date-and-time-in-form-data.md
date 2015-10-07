> [[Home]] ▸ [[Form Runner|Form Runner]]

## Introduction

There are two perspectives on dates and times with Orbeon Forms:

* the perspective of the user of the form, where those are human-readable
* the perspective of the data storage, where those are stored in a standard, human-language-independent format

In this page we discuss dates and times from the perspective of the data.

## Basic format

When using XML, date and time formats usually follow the formats defined in XML Schema, which are a subset of ISO 8601 formats. Specifically, the formats for date, time, and dateTime are specified here:

* [date](http://www.w3.org/TR/xmlschema-2/#date)
* [time](http://www.w3.org/TR/xmlschema-2/#time)
* [dateTime](http://www.w3.org/TR/xmlschema-2/#dateTime)

In practice, the base format is pretty simple, for example:

* `date`: `2012-09-17`
* `time`: `17:24:00`
* `dateTime`: `2012-09-17T17:24:00`

All fields have a fixed number of digits.

## Fractional seconds

In addition, you can add any fractional seconds to the time:

* `2012-09-17T17:24:00.1234`

## Timezones

Things get a bit more complicated if you add optional timezone information. You can have things like:

- `2012-09-17T17:24:00Z` (which means UTC time)
- `2012-09-17T17:24:00+05:00` (timezone indicated with an offset)

For a full reference, the XML Schema documentation linked above is the reference, as Orbeon Forms follows that specification.
