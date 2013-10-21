[SINCE Orbeon Forms 4.3]

See also the [blog post](http://blog.orbeon.com/2013/10/autosave.html).

The following property specifies the delay, in ms, after which form data should be automatically saved:

```xml
<property
  as="xs:integer"
  name="oxf.fr.detail.autosave-delay.*.*"
  value="5000"/>
```

If the value is 0 or negative, autosaving is disabled.
