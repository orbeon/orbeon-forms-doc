# Datasets

## Introduction

\[SINCE Orbeon Forms 2017.1]

A dataset is a mutable set of data, usually returned by a service and stored in the form in XML format.

A dataset is _transient_ at this point:

* It is not saved alongside the data in the database.
* It is not passed when navigating between the "edit" and "view" modes.

Instead, a dataset lives as long as the user stays on the given form page.

## Creating a dataset

You create a dataset using the [Simple Actions Editor](/broken/pages/-LEkBr90Nq9hbV8krZWy). The data returned by the service is stored into the dataset in XML format.

A dataset is identified by a name. Multiple actions calling services can store data into the same dataset. The action which last updates the dataset overwrites the entire content of the dataset.

## Using data from the dataset

Storing data into the dataset only makes sense if you make use of that data at a later point.

You do so using the [`fr:dataset()`](../../xforms/xpath/extension-form-runner.md#frdataset) function. You can use this function from [formulas](../../form-builder/formulas.md), including validations, visibility, etc. For example:

```xpath
fr:dataset('activity-dataset')/foo/bar
```

## Limitations

Datasets do not work in section templates yet, see [issue #3132](https://github.com/orbeon/orbeon-forms/issues/3132).

## See also

* Blog post: [Datasets](https://blog.orbeon.com/2017/01/datasets.html)
* [Simple Actions](/broken/pages/-LEkBr90Nq9hbV8krZWy)
* [HTTP Services](../../form-builder/http-services.md)
* [Database Services](../../form-builder/database-services.md)
* [`fr:dataset()`](../../xforms/xpath/extension-form-runner.md#frdataset) function
