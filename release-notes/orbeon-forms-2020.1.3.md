# Orbeon Forms 2020.1.3 PE

__Friday, April 30, 2021__

Today we released Orbeon Forms 2020.1.3 PE. This update to [Orbeon Forms 2020.1.2 PE](orbeon-forms-2020.1.2.md), [Orbeon Forms 2020.1.1 PE](orbeon-forms-2020.1.1.md) and [Orbeon Forms 2020.1 PE](orbeon-forms-2020.1.md) contains bug-fixes and is recommended for all [Orbeon Forms 2020.1.2 PE](orbeon-forms-2020.1.2.md), [Orbeon Forms 2020.1.1 PE](orbeon-forms-2020.1.1.md) and [Orbeon Forms 2020.1 PE](orbeon-forms-2020.1.md) users.

This release addresses the following issues since [Orbeon Forms 2020.1.2 PE](orbeon-forms-2020.1.2.md):

- When sessions are destroyed, we get: `java.lang.ClassCastException: class scala.Some cannot be cast to class org.orbeon.oxf.webapp.SessionListeners` ([\#4830](https://github.com/orbeon/orbeon-forms/issues/4830))
- Support for spaces and other non-ASCII characters in filename when serving files ([\#4818](https://github.com/orbeon/orbeon-forms/issues/4818))
- Click on the same line as a button activates it ([\#4834](https://github.com/orbeon/orbeon-forms/issues/4834))
- `<fr:navigate>` action has no effect ([\#4799](https://github.com/orbeon/orbeon-forms/issues/4799))
- Responsive mode no longer shows one column layout ([\#4805](https://github.com/orbeon/orbeon-forms/issues/4805))
- TinyMCE not to convert absolute URL to relative paths ([\#4812](https://github.com/orbeon/orbeon-forms/issues/4812))
- Providing initial instance with `fr-form-data` in `multipart/form-data` POST fails ([\#4820](https://github.com/orbeon/orbeon-forms/issues/4820))
- Error when form has custom buttons + wizard + inner buttons ([\#4806](https://github.com/orbeon/orbeon-forms/issues/4806))
- Clear not reseting the checkboxes set by clicking on the label ([\#4826](https://github.com/orbeon/orbeon-forms/issues/4826))
- Unable to set focus on content of Explanatory Text control ([\#4773](https://github.com/orbeon/orbeon-forms/issues/4773))
- Tell user if the wizard "next" or "submit" failed ([\#4819](https://github.com/orbeon/orbeon-forms/issues/4819))
- Grid/section menu doesn't show with JavaScript embedding ([\#4817](https://github.com/orbeon/orbeon-forms/issues/4817))
- With SQL Server, getting error "The data types ntext and nvarchar are incompatible in the equal to operator" ([\#4835](https://github.com/orbeon/orbeon-forms/issues/4835))
- Currency unit isn't read ([\#4833](https://github.com/orbeon/orbeon-forms/issues/4833))
- `xxf:client-id()` to take namespace into account ([\#4836](https://github.com/orbeon/orbeon-forms/issues/4836))
- Currency label not pointing to input with embedding ([\#4785](https://github.com/orbeon/orbeon-forms/issues/4785))
- XML Schema produced doesn't include XForms types import ([\#4837](https://github.com/orbeon/orbeon-forms/issues/4837))
- Never autosave when running forms in the background ([\#4857](https://github.com/orbeon/orbeon-forms/issues/4857))
- `data-format-version` parameter should be optional when database is not in 4.0.0 format ([\#4861](https://github.com/orbeon/orbeon-forms/issues/4861))
- XML Schema produced doesn't validate correctly against data ([\#4838](https://github.com/orbeon/orbeon-forms/issues/4838))
- NPE in `LHHAElementVisitorListener` when called by `getStaticChildElementValue` ([\#4870](https://github.com/orbeon/orbeon-forms/issues/4870))
- Form Builder library version doesn't stick ([\#4875](https://github.com/orbeon/orbeon-forms/issues/4875))
- Update bcmail-jdk15on to 1.68 ([\#4737](https://github.com/orbeon/orbeon-forms/issues/4737))
- Update shapeless to 2.3.4 ([\#4851](https://github.com/orbeon/orbeon-forms/issues/4851))
- Update mysql-connector-java to 8.0.24 ([\#4858](https://github.com/orbeon/orbeon-forms/issues/4858))
- Update scala-logging to 3.9.3 ([\#4815](https://github.com/orbeon/orbeon-forms/issues/4815))

You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page.  
Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon) or the [forum](https://www.orbeon.com/community).

We hope you enjoy this release!
