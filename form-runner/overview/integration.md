# Integration

## Introduction

Form Builder and Form Runner integrate with other systems through several means, summarized below.

## Plain URLs

See [URLs](../link-embed/linking.md) for more details.

- Through URLs, you access Form Runner and Form Builder's pages.
- The URLs can be produced simply by using hyperlinks or redirects from other applications.

## Configurable persistence API

See [Persistence API](../api/persistence/README.md) for more details.

- The API is based on REST (that is, through HTTP).
- It provides CRUD, search, and metadata operations.

See also [Accessing data captured by forms](accessing-data.md)

## HTTP services

- The [HTTP Service Editor](../../form-builder/http-services.md) allows you to create and bind services from a form.
- With properties, you can tell Form Runner to load initial XML data.
- Configure processes to [submit data](../advanced/buttons-and-processes/actions-form-runner.md#send).

See also [Accessing data captured by forms](accessing-data.md).

## Embedding

Embedding allows you to integrate a form in another application's page.

See [Embedding](../link-embed/README.md) for more details.

## User management

By integrating with a user management system, you make Orbeon Forms aware of users and roles which can be associated with permissions.

See [Access Control](../access-control/README.md) for more details.

## XML representation of form data

See [Form Data Format](../data-format/form-data.md)

## See also

- [Form Builder Integration](../../form-builder/integration.md)
