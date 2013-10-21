## Support

- [SINCE Orbeon Forms 4.3] This feature is supported with MySQL and DB2 only.
- [SINCE Orbeon Forms 4.4] Oracle support is added as well.
- eXist is not yet supported as of Orbeon Forms 4.4.

See also the [blog post](http://blog.orbeon.com/2013/10/autosave.html).

## How autosave works

When autosave is enabled, form data is automatically saved as *drafts* in the background as you interact with the form. You can access drafts from the Summary page. On the Edit page, you'll also be asked whether you'd like to continue your work from a draft when needed.

## Configuration
The following property specifies the delay, in ms, after which form data should be automatically saved:

```xml
<property
  as="xs:integer"
  name="oxf.fr.detail.autosave-delay.*.*"
  value="5000"/>
```

[SINCE Orbeon Forms 4.4] If the value is 0 or negative, autosaving is disabled.
