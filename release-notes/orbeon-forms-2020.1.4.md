# Orbeon Forms 2020.1.4 PE

__Monday, September 6, 2021__

Today we released Orbeon Forms 2020.1.4 PE. This maintenance release contains bug-fixes and is recommended for all users of:

- [Orbeon Forms 2020.1.3 PE](orbeon-forms-2020.1.3.md)
- [Orbeon Forms 2020.1.2 PE](orbeon-forms-2020.1.2.md)
- [Orbeon Forms 2020.1.1 PE](orbeon-forms-2020.1.1.md) 
- [Orbeon Forms 2020.1 PE](orbeon-forms-2020.1.md)

This release addresses the following issues since [Orbeon Forms 2020.1.3 PE](orbeon-forms-2020.1.3.md):

- XPath errors are logged but do not show the expression and location ([\#3351](https://github.com/orbeon/orbeon-forms/issues/3351))
- Can't use `xxf:property()` in Resource URL AVT ([\#4749](https://github.com/orbeon/orbeon-forms/issues/4749))
- Legacy date field not initialized on iOS ([\#4859](https://github.com/orbeon/orbeon-forms/issues/4859))
- "Required item type of first operand" error in the logs ([\#4877](https://github.com/orbeon/orbeon-forms/issues/4877))
- `IndexOutOfBoundsException` with Open Liberty and XForms filter ([\#4880](https://github.com/orbeon/orbeon-forms/issues/4880))
- Clean-up form definition when it contains inline section templates ([\#4888](https://github.com/orbeon/orbeon-forms/issues/4888))
- Dynamic dropdown with search done by service "queue empty" after become visible again ([\#4891](https://github.com/orbeon/orbeon-forms/issues/4891))
- Properties passed to `dispatchEvent` are lost when called from another window ([\#4893](https://github.com/orbeon/orbeon-forms/issues/4893))
- Clear response from service after it is processed ([\#4895](https://github.com/orbeon/orbeon-forms/issues/4895))
- Don't throw exceptions for methods not listed in `oxf.http.accept-methods` ([\#4902](https://github.com/orbeon/orbeon-forms/issues/4902))
- `java.util.NoSuchElementException: None.get` when clicking on HTML checkbox inside tabview ([\#4908](https://github.com/orbeon/orbeon-forms/issues/4908))
- Form without migrations doesn't set `fr:relevant` attributes ([\#4911](https://github.com/orbeon/orbeon-forms/issues/4911))
- Calculated value appearance property has no effect on fields in section templates ([\#4925](https://github.com/orbeon/orbeon-forms/issues/4925))
- `toString` in `scala.xml.Elem` not producing well-formed XML ([\#4927](https://github.com/orbeon/orbeon-forms/issues/4927))
- Actions in newly added section templates run in Form Builder ([\#4930](https://github.com/orbeon/orbeon-forms/issues/4930))
- Test dialog shows empty on error ([\#4932](https://github.com/orbeon/orbeon-forms/issues/4932))
- Multiple File Attachments to support instance with non-empty default namespace ([\#4933](https://github.com/orbeon/orbeon-forms/issues/4933))
- Db2 tests failing on Travis ([\#4935](https://github.com/orbeon/orbeon-forms/issues/4935))
- Number/currency on mobile and non-English language shows value as invalid ([\#4936](https://github.com/orbeon/orbeon-forms/issues/4936))
- `fr:databound-select1` doesn't initialize in new iteration ([\#4945](https://github.com/orbeon/orbeon-forms/issues/4945))
- For ([\#3814](https://github.com/orbeon/orbeon-forms/issues/3814)): add process to modify published form
- Embedding: better logging/error message when Orbeon Forms doesn't send a mediatype ([\#4917](https://github.com/orbeon/orbeon-forms/issues/4917))
- Simple processes: add `xf:insert` and `xf:delete` actions ([\#4676](https://github.com/orbeon/orbeon-forms/issues/4676))
- File scan error message shows `{0}` ([\#4961](https://github.com/orbeon/orbeon-forms/issues/4961))
- Incorrect section template bind during reindex crash interrupts the process ([\#4972](https://github.com/orbeon/orbeon-forms/issues/4972))
- Section to pass `index` to `fr-iteration-removed` ([\#4973](https://github.com/orbeon/orbeon-forms/issues/4973))
- First field not to show as invalid with separate TOC ([\#4975](https://github.com/orbeon/orbeon-forms/issues/4975))
- User with organization-based permission to be able to access the summary page ([\#4979](https://github.com/orbeon/orbeon-forms/issues/4979))
- Always return 404 when data doesn't exist ([\#4979](https://github.com/orbeon/orbeon-forms/issues/4979))
- Allow user to org-based perms to create data ([\#4978](https://github.com/orbeon/orbeon-forms/issues/4978))

You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page.  
Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon), [Mastodon](https://mastodon.social/@orbeon), or the [forum](https://www.orbeon.com/community).

We hope you enjoy this release!
