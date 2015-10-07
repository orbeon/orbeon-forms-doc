> [[Home]] ▸ [[Installation]]

## Types of environments

Orbeon Forms is often installed in different environments, with one or more servers dedicated to each environment. For instance:

1. A development environment — On which form authors create and test the forms they are working on.
2. A staging environment — On which testing is performed before deployment.
3. A production environment — Accessed by end users to fill out forms.

The above scenario is typical, but there is nothing in Orbeon Forms that dictates you have those 3 environments. Your setup could include more or less different environments depending on your needs.

## Database setup

So you get the full benefit of having different environment, you should setup the instances of Orbeon Forms in different environments to use different databases, or at least different database schemas, so you can see each environment as a silo, and never have, say, form authors accessing the development environment change any data related to the staging or production environment.

## Migration of form definitions

At some point, you might want to graduate data, typically forms, from one environment to the next. For instance, when form authors are done, moving forms from the development to staging. Or once testing is done, from staging to production. This can be achieve with the [[Form Runner ~ Home Page|Form Runner ~ Home Page]].
