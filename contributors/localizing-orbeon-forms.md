# Localizing Orbeon Forms

<!-- toc -->

## Introduction

This document explains how to localize Orbeon Forms
([i18n/L10n](http://en.wikipedia.org/wiki/Internationalization_and_localization)).

See also [[Supported Languages|Form Runner ~ Supported Languages]].

## Files to localize

### Form Runner and Form Builder resources

See these 2 files:

- [Form Runner's `resources.xml`](https://github.com/orbeon/orbeon-forms/blob/master/src/resources/apps/fr/i18n/resources.xml)
- [Form Builder's `resources.xml`](https://github.com/orbeon/orbeon-forms/blob/master/src/resources/forms/orbeon/builder/form/resources.xml)

Each file has as series of `<resource>` elements each with an `xml:lang`
attribute. You need to add your own element. Say you want to localize to
Italian:

```xml
<resource xml:lang="it">
```

You can start by copying an existing `<resource>` element (for example
French).

Starting 2014-03-21, we are marking placeholder resources for existing
languages with `todo="true"`.

*NOTE: You must not translate the `<value>` part of item definitions, for
example:*

```xml
<required>
    <label>Richiesto</label>
    <hint>Se è necessario immettere i dati</hint>
    <item>
        <label>Sì</label>
        <value>true</value>
    </item>
    <item>
        <label>No</label>
        <value>false</value>
    </item>
</required>
```

For Form Builder, starting with Orbeon Forms 4.0, you also need to
update the following property in your `properties-local.xml`:

```xml
<property
    as="xs:string"
    name="oxf.fr.available-languages.orbeon.builder"
    value="en fr it"/>
```

By default, Orbeon Forms 4.0 sets it to `en fr`. The updated property at
least needs to include the new language or languages that you are
adding, in this case, `it`. For more details on this, see this [Stack
Overflow
question](http://stackoverflow.com/questions/11449195/orbeon-4-0-0-m6-how-to-set-default-language-for-form-builder/11565704).

### Calendar resources

See [`CalendarResources.js`](https://github.com/orbeon/orbeon-forms/blob/master/src/resources-packaged/ops/javascript/orbeon/xforms/control/CalendarResources.js).

Similar idea here, but this is in JavaScript, for client-side calendar
support.

### XBL components

Orbeon Forms XBL components are located as subdirectories here:

- https://github.com/orbeon/orbeon-forms/tree/master/src/resources-packaged/xbl/orbeon

Each subdirectory has a .xbl file with some metadata. For example:

- [`us-phone.xbl`](https://github.com/orbeon/orbeon-forms/blob/master/src/resources-packaged/xbl/orbeon/us-phone/us-phone.xbl)

In there, you will notice the localization in English, French, etc. Add your own language.

To get started search for the English version in all files for both:

- `lang="en"`
- `xml:lang="en"`

Then add the corresponding resources for the new language.

*NOTE: Makes sure also to localize
[`dialog-select-resources.xml`](https://github.com/orbeon/orbeon-forms/blob/master/src/resources-packaged/xbl/orbeon/dialog-select/dialog-select-resources.xml),
which is easily missed.*

### Pseudo-XBL components

- https://github.com/orbeon/orbeon-forms/tree/master/src/resources/forms/orbeon/builder/xbl

These files are used by the Form Builder toolbox for built-in XForms
controls.

### Dates and times

For formatting of dates and times, a Java class usually needs to be added. See the example for Norwegian:

- [`Numberer_no.java`](https://github.com/orbeon/orbeon-forms/blob/master/src/main/java/org/orbeon/saxon/number/Numberer_no.java)

## How to localize

You can go about this in various ways.

1. Localize the files
2. Send them to us
   1. with a github pull request
   2. by sending the files to us separately
3. Either way, we need the [CLA](http://wiki.orbeon.com/forms/community/cla) signed.

But you will want to see the results yourself first. Here you can either work with
- the source [from github](https://github.com/orbeon/orbeon-forms)
- or a binary build

If working with the source, build Orbeon Forms, localize the files, and test as you go.

If working with a binary build, you can override built-in files by creating your own files under the WAR file's WEB-INF/resources directory:

- `WEB-INF/resources/apps/fr/i18n/resources.xml`
- `WEB-INF/resources/forms/orbeon/builder/form/resources.xml`
- `WEB-INF/resources/ops/javascript/orbeon/xforms/control/CalendarResources.js`
- `WEB-INF/resources/xbl/orbeon/*/*.xbl`
- `WEB-INF/resources/forms/orbeon/builder/xbl/*/*.xbl`
