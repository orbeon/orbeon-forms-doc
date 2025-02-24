# Product roadmap

## Upcoming releases

### Orbeon Forms 2025.1

Focus:

- Workflow features
    - Form Builder user interface
- Infrastructure
    - Replace Ehcache 2 with Infinispan [#6640](https://github.com/orbeon/orbeon-forms/issues/6640)
- Form Builder user-friendliness:
    - more UI (properties, validations, formulas, etc.)
    - see [#2282](https://github.com/orbeon/orbeon-forms/issues/2282)
- Additional form controls
    - [#6734](https://github.com/orbeon/orbeon-forms/issues/6734) 
- Additional demo forms
- Steps toward a configurable look and feel
    - [#4140](https://github.com/orbeon/orbeon-forms/issues/4140)
- Additional integrations
    - Google Sheets  

[//]: # (- admin console!)
[//]: # (Think about other general lines.)

See the tentative [2025.1 items](https://github.com/users/orbeon/projects/18) for details.

Consider:

- [low-hanging fruits](https://github.com/orbeon/orbeon-forms/issues?q=is%3Aopen+is%3Aissue+label%3A%22Low-Hanging+Fruit%22)
- [dogfood items](https://github.com/orbeon/orbeon-forms/labels/Area%3A%20Dogfood)
- [maintenance items](https://github.com/orbeon/orbeon-forms/issues?q=is%3Aopen+is%3Aissue+label%3A%22Type%3A+Maintenance%22+)
- [spare time items](https://github.com/orbeon/orbeon-forms/issues?q=is%3Aopen+is%3Aissue++label%3A%22Priority%3A+Spare+Time%22+)
- [top issues](https://github.com/orbeon/orbeon-forms/issues?q=is%3Aopen+is%3Aissue+label%3A%22Top+issue%22)
- [top RFEs](https://github.com/orbeon/orbeon-forms/issues?q=is%3Aopen+is%3Aissue+label%3A%22Top+RFE%22)

__Please remember that the following schedule is a plan, not a promise:__

- 2025-12: tentative release date

## Past releases

See the [Orbeon Forms release history](release-history.md).

## Release strategy

- __How often do we release?__ Since 2020 we have switched to one major release per year with point releases as needed.
- __What do releases contain?__ Releases address both new features and bug-fixes. Point-releases (2020.1.1, 2020.1.2, etc.) essentially contain bug-fixes and sometimes very small features. They are done only when necessary, and only for the PE version of the product. Bigger features are left to the major releases (2019.1, 2019.2, 2020.1, 2021.1, etc.), and we strive to do those on a regular schedule.

## Planning strategy

- __How far ahead do we plan?__ Once we've published a major release, we do the planning for the following one. You can see the future releases listed on the [open projects page](https://github.com/orbeon/orbeon-forms/projects), and from there, see what has been planned for the next release. We don't plan feature or bugs to be included in releases beyond the very next release, so we can react faster to our customers needs, deciding for every release what will best serve our customers.
- __How do we decide what improvements to include in a release?__ A fair amount of the new features we develop are designed in collaboration with our customers, and sponsored by customers through a [Dev Support plan](https://www.orbeon.com/services). Other features are added to better serve existing and future users of the product. And last, but not least, for every release we strive to do work in each one of the development tracks outlined below.

## Development tracks

These are general development tracks which we consider from release to release: 

- __Mobile__ – Better support [responsive design](https://github.com/orbeon/orbeon-forms/issues/1181), and the ability to [run our form engine on the client](https://github.com/orbeon/orbeon-forms/issues/1221) to enable offline support and native apps.
- __Performance__ – While Orbeon Forms has been proven to be able to sustain a [fair amount of load](faq/form-builder-runner.md#how-much-load-can-orbeon-forms-handle), we 're always striving to improve the product in that regard, including [reducing memory usage](https://github.com/orbeon/orbeon-forms/issues/1606), [reducing load time](https://github.com/orbeon/orbeon-forms/issues/1239), and [improving performance on MySQL](https://github.com/orbeon/orbeon-forms/issues/649).
- __Architecture and maintenance__ – Like any project, Orbeon Forms has some technical debt. It is not possible to remove all of it at once, but, like garbage collection, we want improve the code base incrementally. In addition, we want to look to the future to use the best possible languages and frameworks. This includes: [modularizing of the code](https://github.com/orbeon/orbeon-forms/issues/1585), taking care of [issues flagged as "maintenance"](https://github.com/orbeon/orbeon-forms/issues?q=is%3Aopen+is%3Aissue+label%3AMaintenance), drop old frameworks and adopt new ones ([get rid of YUI](https://github.com/orbeon/orbeon-forms/issues/1599), [use Scala.js](https://github.com/orbeon/orbeon-forms/issues/1600)), and do more automated testing (we have almost 800 tests, but need more).
- __Integration and workflow__ – We want to make it easier to [embed forms](https://github.com/orbeon/orbeon-forms/issues/1235), make it easier to use [HTTP/DB services for validation](https://github.com/orbeon/orbeon-forms/issues/1304), allow [forms to be imported/exported with dependencies](https://github.com/orbeon/orbeon-forms/issues/779), better document [standard use cases / workflows](https://github.com/orbeon/orbeon-forms/issues/228), make [integration with external systems/workflows simpler](http://wiki.orbeon.com/forms/projects/form-runner-builder/form-runner-data-envelope), include built-in [Basic workflow](https://github.com/orbeon/orbeon-forms/issues/2256), and more.
- __Form Builder modernization__ – We want to push the envelope and keep improving the [usability of Form Builder](https://github.com/orbeon/orbeon-forms/issues/1675).

## What's in a release

For each release we would like to:

- have bug-fixes
- have at least one new feature (which can be small)
- book one day of work on [documentation tasks](https://github.com/orbeon/orbeon-forms/issues?direction=desc&labels=Doc&milestone=&page=1&sort=updated&state=open)
- pick one [maintenance task](https://github.com/orbeon/orbeon-forms/issues?direction=desc&labels=Refactoring&milestone=&page=1&sort=updated&state=open) (build system, refactoring, …)

The idea is, as some of those tasks are large, to do it incrementally when possible, so that some progress is made.
