## Compatibility notes

### When using a combination of custom persistence and built-in relational persistence

If you are using the following unlikely combination:

- The built-in implementation of persistence API for relational databases to store forms.
- Your own implementation of the persistence API to store data.

Then, starting with Orbeon Forms 2021.1, your implementation of the persistence API needs to support the `HEAD` method, in addition to `GET`.
