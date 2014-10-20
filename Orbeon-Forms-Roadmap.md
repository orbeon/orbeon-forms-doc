* Our main development tracks are:
    1. [Performance](#performance) (e.g. memory use)
    1. [Mobile](#mobile) (e.g. responsive UI)
    1. [Architecture and maintenance](#architecture-and-maintenance) (e.g. modularization)
    1. [Integration](#integration) (e.g. embedding)
    1. [Form Builder modernization](#form-builder-modernization) (e.g. drag-and-drop)
    1. [Other](#other)
* Upcoming releases
    * [Orbeon Forms 4.8](#orbeon-forms-48)
    * [Orbeon Forms 4.9](#orbeon-forms-49)
* [What's in a release](#whats-in-a-release)
* Past releases
    * [Orbeon Forms 4.6](#orbeon-forms-46)
    * [Orbeon Forms 4.7](#orbeon-forms-47)

## See also

- [Orbeon Forms release history](https://github.com/orbeon/orbeon-forms/wiki/Orbeon-Forms-release-history)

## Tracks

### Performance

- memory use (see umbrella [#1606](https://github.com/orbeon/orbeon-forms/issues/1606))
- load time (see [#1239](https://github.com/orbeon/orbeon-forms/issues/1239))
- MySQL performance (see [#649](https://github.com/orbeon/orbeon-forms/issues/649))

### Mobile

- responsive UI (see umbrella [#1181](https://github.com/orbeon/orbeon-forms/issues/1181))

### Architecture and maintenance

Like any project, Orbeon Forms has some technical debt. It is not possible to remove all of it at once, but, like garbage collection, we would like to improve the code base incrementally.

In addition, we would like to look to the future to use the best possible languages and frameworks.

This includes:

- modularization of the code (see umbrella [#1585](https://github.com/orbeon/orbeon-forms/issues/1585))
- maintenance (see [Maintenance issues](https://github.com/orbeon/orbeon-forms/issues?q=is%3Aopen+is%3Aissue+label%3AMaintenance))
- drop old frameworks and adopt new ones
  - get rid of YUI ([#1599](https://github.com/orbeon/orbeon-forms/issues/1599))
  - evaluate Scala.js (see [#1600](https://github.com/orbeon/orbeon-forms/issues/1600))
- more automated testing (we have almost 800 tests, but need more)

### Integration

- Easy embedding of forms ([#1235](https://github.com/orbeon/orbeon-forms/issues/1235))
- Allow HTTP/DB services to be used for validation ([#1304](https://github.com/orbeon/orbeon-forms/issues/1304))
- Import/export forms with dependencies ([#779](https://github.com/orbeon/orbeon-forms/issues/779))

### Form Builder modernization

- Improve Form Builder usability (see umbrella [#1675](https://github.com/orbeon/orbeon-forms/issues/1675))

### Other

- Document standard use cases / workflows with Orbeon Forms (see [#228](https://github.com/orbeon/orbeon-forms/issues/228))
- Easier integration with external systems/workflows (see [Data Envelope and Metadata](https://sites.google.com/a/orbeon.com/forms/projects/form-runner-builder/form-runner-data-envelope))
- Improved/more out of the box form controls (e.g. SSN, intl phone, etc.)

## Upcoming releases

### Orbeon Forms 4.8

Focus:

- [ ] one performance item:
  - [ ] [Review XForms cache architecture #1718](https://github.com/orbeon/orbeon-forms/issues/1718)
- [ ] one maintenance item from:
  - [ ] [Recalculate/revalidate: deferred event dispatch #1773](https://github.com/orbeon/orbeon-forms/issues/1773)
  - [ ] [Rewrite `ControlTree` in Scala #769](https://github.com/orbeon/orbeon-forms/issues/769)
  - [ ] [Complete move of controls to Scala #715](https://github.com/orbeon/orbeon-forms/issues/715)
- [ ] doc item
  - [ ] [Migrate and update Form Builder doc to reflect current UI #219](https://github.com/orbeon/orbeon-forms/issues/219)

Issues:

- [ ] [all Orbeon Forms 4.8 issues](https://github.com/orbeon/orbeon-forms/milestones/4.8)

Dates (tentative):

- 2014-11-26: tentative release date

### Orbeon Forms 4.9

Focus:

- [ ] TBD
- [ ] one performance item: TBD
- [ ] one maintenance item: TBD
- [ ] doc items: TBD

Issues:

- [ ] Considered for 4.9
- [ ] 4.9

Dates (tentative):

- counting 2 slow weeks around the end of year
- 2015-02-15: tentative release date

## What's in a release

For each release we would like to:

- [ ] have bug-fixes
- [ ] have at least one new feature (which can be small)
- [ ] book one day of work on [documentation tasks](https://github.com/orbeon/orbeon-forms/issues?direction=desc&labels=Doc&milestone=&page=1&sort=updated&state=open)
- [ ] pick one [maintenance task](https://github.com/orbeon/orbeon-forms/issues?direction=desc&labels=Refactoring&milestone=&page=1&sort=updated&state=open) (build system, refactoring, …)
- [ ] pick one [performance task](https://github.com/orbeon/orbeon-forms/issues?direction=desc&labels=Performance&milestone=&page=1&sort=updated&state=open)
- [ ] pick one mobile item if possible

The idea is, as some of those tasks are large, to do it incrementally when possible, so that some progress is made.

---

## Past releases

### Orbeon Forms 4.6

Focus:

- [x] one performance item: [#1143](https://github.com/orbeon/orbeon-forms/issues/1143)
- [x] one maintenance item: [#1710](https://github.com/orbeon/orbeon-forms/issues/1710)
- [ ] doc items: TBD
- [x] [4.6 issues](https://github.com/orbeon/orbeon-forms/issues?direction=desc&milestone=37&page=1&sort=updated&state=open)

Dates:

- ~~2014-05-08: branching and big feature freeze~~
- ~~2014-06-05: tentative release date~~
- ~~2014-06-24: actual release date~~

### Orbeon Forms 4.7

- [x] [4.7 issues](https://github.com/orbeon/orbeon-forms/issues?direction=desc&milestone=41&page=1&sort=updated&state=open)
- [x] one performance item:
  - [x] [Internal requests must not go through HTTP #1363](https://github.com/orbeon/orbeon-forms/issues/1363)
- [x] maintenance items:
  - didn't do the ones planned, but did some others
  - [x] [FB: Itemset editor doesn't correctly distinguish between select/select1 #1084](https://github.com/orbeon/orbeon-forms/issues/1084)
  - [x] [Don't use built-in HTTP client in proxy portlet #1412](https://github.com/orbeon/orbeon-forms/issues/1412)
- [ ] doc items
  - [ ] [Migrate and update Form Builder doc to reflect current UI #219](https://github.com/orbeon/orbeon-forms/issues/219)

Dates:

- ~~counting 1 slow summer month~~
- ~~2014-09-10: tentative release date~~
- ~~2014-09-26: actual release date~~