

## Compatibility notes

### When using a combination of custom persistence and built-in relational persistence

If you are using the following unlikely combination:

- The built-in implementation of persistence API for relational databases to store forms.
- Your own implementation of the persistence API to store data.

Then, starting with Orbeon Forms 2021.1, your implementation of the persistence API needs to support the `HEAD` method, in addition to `GET`.

### "Save Draft" button

xxx the `save-draft` button is now called `save-progress`. The button label is also renamed to say "Save Progress" instead of "Save Draft" by default.

The `save-draft` button remains for backward compatibility. By default, it calls the `process("save-progress")` process.

The reason for this renaming is that it reflects the intention better, and reduces confusion with the word "draft" also used for autosave drafts.
