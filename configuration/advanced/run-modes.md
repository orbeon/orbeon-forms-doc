

## Rationale

It is often necessary to have slightly different settings between development and production. Run modes provide a way to create two separate sets of settings targeting the two environments.

For a quick introduction, see also this [blog post](http://blog.orbeon.com/2012/05/run-modes.html).

## Availability

This feature is available in builds starting Orbeon Forms 4.0.

## Configuration

The run mode is configured in the web application's web.xml file:

```xml
<context-param>
    <param-name>oxf.run-mode</param-name>
    <param-value>prod</param-value>
</context-param>
```

There are two possible modes:

- `prod`: production
- `dev`: development

By default, when you download Orbeon Forms, the mode is set to `prod`, the safest mode.

*NOTE: If you build Orbeon Forms from source and run it from the exploded `orbeon-war`, the default is set to `dev`.*

Orbeon Forms logs the mode used when starting the web app, for example:

```
Using run mode: prod
Using properties file: oxf:/config/properties-prod.xml
```

## Impact of the modes

Selecting a particular mode selects different properties files. By default, the following properties are different in prod vs. dev mode:

- `oxf.http.exceptions`
    -  `prod`: `false`
    -  `dev`: `true`
- `location-mode` for `oxf:builtin-saxon` and `oxf:unsafe-builtin-saxon`
    -  `prod`: `none`
    -  `dev`: `smart`
- `oxf.xforms.minimal-resources`
    -  `prod`: `true`
    -  `dev`: `false`
- `oxf.xforms.combine-resources`
    -  `prod`: `true`
    -  `dev`: `false`
- `oxf.xforms.resources.encode-version`
    -  `prod`: `true`
    -  `dev`: `false`
- `oxf.xforms.show-recoverable-errors`
    -  `prod`: `0`
    -  `dev`: `10`
- `oxf.show-version` [SINCE Orbeon Forms 4.6.1]
    -  `prod`: `false`
    -  `dev`: `true`
- `oxf.fr.version.*.*` [UNTIL Orbeon Forms 4.6]
    -  `prod`: `false`
    -  `dev`: `true`

In addition, depending on the mode, the following local file is used:

- `prod`: `properties-local-prod.xml`
- `dev`: properties-local-dev.xml

## See also

- [[Configuration Properties|Installation ~ Configuration Properties]]
- Blog post: [Run modes](http://blog.orbeon.com/2012/05/run-modes.html)
