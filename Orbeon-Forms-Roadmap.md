* Our main development tracks are:
    1. [Performance](#performance) (e.g. memory use)
    1. [Mobile](#mobile) (e.g. responsive UI)
    1. [Architecture and maintenance](#architecture-and-maintenance) (e.g. modularization)
    1. [Integration](#integration) (e.g. embedding)
    1. [Form Builder modernization](#form-builder-modernization) (e.g. drag-and-drop)
    1. [Other](#other)
* Future releases
    * [Orbeon Forms 4.6](#orbeon-forms-46)
    * [Orbeon Forms 4.7](#orbeon-forms-47)
* [What's in a release](#whats-in-a-release)

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
- refactoring (see [Refactoring issues](https://github.com/orbeon/orbeon-forms/issues?direction=desc&labels=Refactoring&milestone=&page=1&sort=updated&state=open))
- evaluate Scala.js (see [#1600](https://github.com/orbeon/orbeon-forms/issues/1600))
- drop old frameworks and adopt new ones
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

## Future releases

### Orbeon Forms 4.6

Focus:

- one performance item
    - suggested: [#1143](https://github.com/orbeon/orbeon-forms/issues/1143)
- one maintenance item
    - suggested: TODO
- [4.6 issues (in progress)](https://github.com/orbeon/orbeon-forms/issues?direction=desc&milestone=37&page=1&sort=updated&state=open)

Dates (tentative):

- 2014-05-08: branching and big feature freeze
- 2014-06-05: tentative release date

### Orbeon Forms 4.7

Focus:

- TBD
- [Issues considered for 4.7 (in progress)](https://github.com/orbeon/orbeon-forms/issues?direction=desc&milestone=38&page=1&sort=updated&state=open)

Dates (tentative):

- counting 1 slow summer month
- 2014-08-12: branching and big feature freeze
- 2014-09-09: tentative release date

## What's in a release

For each release we would like to:

- have bug-fixes
- have at least one new feature (which can be small)
- book one day of work on [documentation tasks](https://github.com/orbeon/orbeon-forms/issues?direction=desc&labels=Doc&milestone=&page=1&sort=updated&state=open)
- pick one maintenance (build system, refactoring, …) item
- pick one performance item
- pick one mobile item if possible

The idea is, as some of those tasks are large, to do it incrementally when possible, so that some progress is made.