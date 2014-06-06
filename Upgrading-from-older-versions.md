## Compatibility policy

- __Compability:__ We strive to remain backward compatible between versions of Orbeon Forms and not to break features, whether on purpose or by accident.

- __Deprecation and removal:__ Over time, we may mark some features as *deprecated*. After being deprecated for a while, these features can even be *removed* from the product. Over years, this means that backward compatibility is not always guaranteed.

- __Compatibility notes:__ Release notes for each version might contain compatibility notes. When upgrading, please make sure to always review compatibility notes.

## Difficulty of upgrades

Since Orbeon Forms 4.0, we have switched to a faster release cycle, with releases every few months.

Here are some general considerations:

- Orbeon Forms 4.0 was a large release with many changes. In general upgrades between pre-4.0 releases and 4.x releases are more difficult than upgrades between two 4.x releases.
- Between any two subsequent releases, upgrades are expected to be fairly straightforward.
- The longer the interval of time between two release, the harder the upgrade. For example, it is harder to upgrade between 3.9.1 and 4.6 than between 4.5 and 4.6.

## Areas of compatibility

- __Form Runner / Form Builder DDL:__ The relational database definitions are subject to change between releases. We provide scripts to upgrade the definitions between versions.
- __Form Runner form format:__ We strive to keep the format, when form definitions are not modified by users, fully backward compatible.
  - Form Builder, upon loading and republishing forms, can upgrade the format of form definitions when needed.
  - Similarly, and since Orbeon Forms 4.6, the Form Runner Home page has an "Upgrade" feature to upgrade published form definitions.
- __XForms support:__ We strive for maximum backward compatibility at the XForms source level. But because the XForms processing model is quite advanced, some subtle details are subject to change, such as the order in which some events are dispatched.
- __Look and feel and CSS:__ Often users adapt the Orbeon Forms look and feel using custom CSS. It is hard to guarantee full backward compatibility here due to the lack of encapsulation provided by CSS. Upgrades can require custom CSS to be adapted.

## Compatibility notes for previous versions

When about to upgrade, we recommend you go through the release notes for all versions between the version you are using and the one you are upgrading to, paying particular attention to the compatibility notes sections. Here are the releases which contain backward compatibility notes:

- 4.6: TBD
- [4.5](http://blog.orbeon.com/2014/04/orbeon-forms-45.html)
- [4.4](http://blog.orbeon.com/2013/11/orbeon-forms-44.html)
- [4.3](http://blog.orbeon.com/2013/08/orbeon-forms-43.html)
- [4.2](http://blog.orbeon.com/2013/05/orbeon-forms-42.html)
- [4.0](http://wiki.orbeon.com/forms/doc/developer-guide/release-notes/40#TOC-Compatibility-notes)
- [3.9](http://wiki.orbeon.com/forms/doc/developer-guide/release-notes/39#TOC-Compatibility-notes)

See also [Orbeon Forms release history](https://github.com/orbeon/orbeon-forms/wiki/Orbeon-Forms-release-history).

## Reliance on Orbeon Forms internals

Our users sometimes customize Orbeon Forms by relying on the internals of Orbeon Forms. This might even be on Orbeon's advice, when no better solution are available at a given time. In such cases, upgrading can be more difficult, because the internals of Orbeon Forms are subject to change, and backward compatibility of look and feel is difficult to achieve with only CSS.

When this happens, we consider the reasons changes relying upon Orbeon Forms internals, and evaluate how this could be improved in the future. Examples include:

- [Form Runner: Custom Model Logic](https://github.com/orbeon/orbeon-forms/wiki/Form-Runner:-Custom-Model-Logic)
- [Stable API for Form Runner](https://github.com/orbeon/orbeon-forms/issues/1095)