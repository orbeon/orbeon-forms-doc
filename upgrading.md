# Upgrading

## Why upgrade to newer versions of Orbeon Forms?

### Minor releases

Orbeon Forms minor releases (or "dot releases", or "point releases"), for example Orbeon Forms 2018.2.1, 2018.2.2, etc., contain important bug-fixes but no new features (in some rare cases, minor features can be introduced). We recommend upgrading to newer minor releases in all cases. 

### Major releases

Major releases contain new features and bug-fixes. In some cases, bug-fixes cannot be backported to earlier versions of Orbeon Forms, for example in the case where the fix required an important change in the internal architecture of the software. 

The only sure way to keep up to date is to eventually update to a newer major version of Orbeon Forms. We acknowledge that upgrading, while it should always be smooth, comes with some risks, as with any software upgrade.

You can reduce that risk by *not upgrading* to the first published major release, and wait until a subsequent dot release. For example, instead of immediately upgrading to Orbeon Forms 2018.2, wait until Orbeon Forms 2018.2.1 or 2018.2.2.  

## Where do bug-fixes go?

The way we handle fixes, generally, is as follows:

- All new fixes go into the branch for the next major version of Orbeon Forms (for example the future Orbeon Forms 2019.1).
- Most fixes (as opposed to new features) go into the previous major version of Orbeon Forms as well (for example Orbeon Forms 2018.2), and are released as a dot release at a later time (for example Orbeon Forms 2018.2.3).
- Some important fixes are backported to earlier versions of Orbeon Forms (for example 2018.1.x) at Orbeon's discretion.
- We backport certain features to customer branches on demand. But the more time passes between versions, the harder and riskier it becomes to backport fixes. This is because the codebase is more likely to change over time.

## Upgrade steps

To upgrade to a new version of Orbeon Forms you'll want to:

1. Review the compatibility notes (see [Compatibility notes for previous versions](#compatibility-notes-for-previous-versions)).
1. Make sure you have a backup of your current environments, including:
    - databases
    - containers
    - Orbeon Forms WARs
    - all Orbeon Forms configurations
1. Make sure to first upgrade in a safe test environment.
1. Do a "clean Install" of the new version of Orbeon Forms.
    - Don't install it "over" the old version, or try to just update some `jar` files or any other files in the old version (unless directed to do so by Orbeon support). 
    - Check that that your clean install works as expected out of the box.
    - Often, the only file you need to modify is `WEB-INF/resources/config/properties-local.xml`, but you might have created or made changes to other files, such as `WEB-INF/resources/config/form-builder-permissions.xml`, `WEB-INF/resources/config/log4j.xml` or even `WEB-INF/web.xml`.
1. Migrate your database if needed.
    - If you're using the internal eXist and want to keep the forms you created, and corresponding data captured with those forms, you'll want to move the `WEB-INF/exist-data` directory to the new web app.
    - If using a relational database, you might have to run some upgrade DDL at the database level. Search for "upgrade" in the [relational database configuration documentation](form-runner/persistence/relational-db.md), and check if there is some DDL you need to run to upgrade your database schema. This means running the `*-to-*.sql` DDL files from the older version to the new version.
1. Thoroughly test that everything is working as expected with the new version of Orbeon Forms.

Finally, let us know if you encounter issues while upgrading, via the [community](https://www.orbeon.com/community), by email, or via professional support.

## Compatibility policy

- __Compatibility:__ We strive to remain backward compatible between versions of Orbeon Forms and not to break features, whether on purpose or by accident.

- __Deprecation and removal:__ Over time, we may mark some features as *deprecated*. After being deprecated for a while, these features can even be *removed* from the product. Over years, this means that backward compatibility is not always guaranteed.

- __Compatibility notes:__ Release notes for each version might contain compatibility notes. When upgrading, please make sure to always review compatibility notes.

## Difficulty of upgrades

*NOTE: As a reminder, starting with Orbeon Forms 2016.1, we are using a versioning scheme with the number of the year first followed by the number of the major release during that year. See [Release History](release-history.md) for details. *

- Between any two subsequent 4.x releases, or 20xx.x releases, upgrades are expected to be fairly straightforward.
- The longer the interval of time between two release, the harder the upgrade might be. For example, it can be harder to upgrade between 2016.1 and 2018.2 than between 2018.1 and 2018.2.
- Orbeon Forms 4.0 was a large release with many changes. In general upgrading between pre-4.0 releases and 4.x releases is more difficult than upgrades between two 4.x or 20xx.x releases.
- Since Orbeon Forms 4.0, we have switched to a faster release cycle, with releases every few months. So there are typically more changes between, say, 3.8 and 3.9, and especially 3.9 and 4.0, than between two subsequent 4.x or 20xx.x releases.

## Areas of compatibility

- __Form Runner / Form Builder DDL:__ The relational database definitions are subject to change between releases. We provide scripts to upgrade the definitions between versions.
- __Form Runner form format:__ We strive to keep the format, when form definitions are not modified by users, fully backward compatible.
  - Form Builder, upon loading and republishing forms, can upgrade the format of form definitions when needed.
  - Similarly, and since Orbeon Forms 4.6, the Form Runner Home page has an "Upgrade" feature to upgrade published form definitions.
- __XForms support:__ We strive for maximum backward compatibility at the XForms source level. But because the XForms processing model is quite advanced, some subtle details are subject to change, such as the order in which some events are dispatched.
- __Look and feel and CSS:__ Often users adapt the Orbeon Forms look and feel using custom CSS. It is hard to guarantee full backward compatibility here due to the lack of encapsulation provided by CSS. Upgrades can require custom CSS to be adapted. 4.0 in particular introduced the Twitter Bootstrap library for the user interface, and that was a major change from previous versions.
- __Configuration properties:__ We strive to keep properties backward compatible. On rare occasions, configuration properties have changed in incompatible ways, in particular in 4.0 the Form Runner persistence providers configuration have changed.

## Compatibility notes for previous versions

When about to upgrade, we recommend you go through the release notes for all versions between the version you are using and the one you are upgrading to, paying particular attention to the compatibility notes sections. Here are the releases which contain backward compatibility notes:

- [2019.1](/release-notes/orbeon-forms-2019.1.md)
- [2018.2](https://blog.orbeon.com/2018/12/orbeon-forms-20182.html)
- [2018.1](https://blog.orbeon.com/2018/09/orbeon-forms-20181.html)
- [2017.1](https://blog.orbeon.com/2017/06/orbeon-forms-20171.html)
- [2016.2](https://blog.orbeon.com/2016/08/orbeon-forms-20162.html)
- [2016.1](https://blog.orbeon.com/2016/04/orbeon-forms-20161.html)
- [4.10](https://blog.orbeon.com/2015/08/orbeon-forms-410.html)
- [4.9](https://blog.orbeon.com/2015/05/orbeon-forms-49.html)
- [4.8](https://blog.orbeon.com/2015/01/orbeon-forms-48.html)
- [4.7](https://blog.orbeon.com/2014/09/orbeon-forms-47.html)
- [4.6.2](https://blog.orbeon.com/2014/08/orbeon-forms-462.html)
- [4.6.1](https://blog.orbeon.com/2014/07/orbeon-forms-461.html)
- [4.6](https://blog.orbeon.com/2014/06/orbeon-forms-46.html)
- [4.5](https://blog.orbeon.com/2014/04/orbeon-forms-45.html)
- [4.4](https://blog.orbeon.com/2013/11/orbeon-forms-44.html)
- [4.3](https://blog.orbeon.com/2013/08/orbeon-forms-43.html)
- [4.2](https://blog.orbeon.com/2013/05/orbeon-forms-42.html)
- [4.0](http://wiki.orbeon.com/forms/doc/developer-guide/release-notes/40#TOC-Compatibility-notes)
- [3.9](http://wiki.orbeon.com/forms/doc/developer-guide/release-notes/39#TOC-Compatibility-notes)

See also [Orbeon Forms release history](release-history.md).

## Reliance on Orbeon Forms internals

### Recommendation 

In general we *strongly recommend* that you do not rely on Orbeon Forms internals, but only on published APIs.

This includes not modifying the content of any JAR files present in Orbeon Forms.  

### When it happens

Our users sometimes customize Orbeon Forms by relying on the internals of Orbeon Forms. This might even be on Orbeon's advice, when no better solution are available at a given time. In such cases, upgrading can be more difficult, because the internals of Orbeon Forms are subject to change, and backward compatibility of look and feel is difficult to achieve with only CSS.

When this happens, we consider the reasons changes relying upon Orbeon Forms internals, and evaluate how this could be improved in the future. Examples include:

- Available
  - [Form Runner: Buttons and Processes](form-runner/advanced/buttons-and-processes/README.md)
  - [Form Runner: Custom dialogs and model logic](form-runner/advanced/custom.md)
- Considered
  - [Stable API for Form Runner](https://github.com/orbeon/orbeon-forms/issues/1095)
  
## What are the benefits of upgrading Orbeon Forms versions?

Each new version brings:

- stability, security and other bug-fixes
- new features

In addition, we can support newer version of Orbeon Forms much better than older versions.

See the [release notes of all the versions](release-history.md) for more details.

In addition, if you are on the 3.x series of Orbeon Forms, the 4.x series brings:

- an improved look and feel
- a rewritten Form Builder relying on a better foundation
- more configurable features
- many new features
