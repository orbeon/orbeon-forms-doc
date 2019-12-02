# JavaScript and CSS assets

## Minimal asset resources

Most JavaScript and CSS assets used by the XForms engine are available in two versions:

* A full version, which may contain comments, spaces, longer identifiers, etc.
* A minimal version, which is usually much smaller

Both versions work exactly the same. For development and debugging of the XForms engine itself, the full version is easier to work with. But if you never work directly with these JavaScript and CSS files, as well as for deployment, the minimal versions are recommended as they will load faster in the user's web browser.

You enable minimal resources in `properties-local.xml` as follows:

```xml
<property
  as="xs:boolean"
  name="oxf.xforms.minimal-resources"
  value="true"/>
```

Default:

- `prod` mode: `true`
- `dev` mode: `false`

## Combined asset resources

### Rationale

Serving external CSS and JavaScript assets can have a high performance cost on page loads. This is particularly important with the intensive use of JavaScript in Orbeon Forms. In particular, it can be shown that serving many small files is slower than serving a single large file.

This is why Orbeon Forms supports the option of combining the multiple JavaScript and CSS files required for a given XForms page into one or two JavaScript files and one or two CSS file.

_NOTE: In theory, HTTP pipelining can improve very much on this, but this is (very unfortunately) useless in practice at the time of writing because browsers do not implement it or do not enable it by default. HTTP/2 might help solve this._

### How it works

There are 3 categories of asset resources:

* built-in XForms engine resources, like `xforms.js`
* XBL components resources, like `grid.js`
* user resources (placed in the `<head>` element

Resources are split into 2 groups:

* baseline resources, which include
    * main built-in XForms engine resources
    * XBL resources for components specified with the `oxf.xforms.resources.baseline` property
* other resources, which include
    * other built-in XForms engine resources, such as for the rich text editor or tree selection
    * XBL resources not part of the baseline
    * user resources

The idea is that this way, in an application with multiple pages:

* a large baseline of resources is loaded and cached once and for all
* a smaller incremental set of extra resources is loaded for each individual page

If all the resources belong to the baseline, only one JavaScript and one CSS files are produced. Otherwise, two JavaScript and two CSS files are produced.

The URLs produced identify the resources needed by the page, for example:

```xml
<link rel="stylesheet"
  href="/orbeon/xforms-server/orbeon-8b3d174e93f2d74146c9b2a5356bd5b8b5e196f8.css"
  type="text/css" media="all">

<link rel="stylesheet"
  href="/orbeon/xforms-server/orbeon-e01218f47e6ecd43fb1c2295ccae2e688c443b62.css"
  type="text/css" media="all">

<script type="text/javascript"
  src="/orbeon/xforms-server/orbeon-19e8d10829ccdd0d9aec779c0c89e5d1f57764dd.js">

<script type="text/javascript"
  src="/orbeon/xforms-server/orbeon-3a3469eca94e6df9783e742067f464b57de4e2f3.js"/>
```

When the Orbeon Forms XForms server receives a request for a combined resource, it determine what files need to be combined and outputs them all together. Furthermore, for CSS files, all URLs referred to with `url()` are rewritten, so that links to images, in particular, remain correct.

Some CSS and JavaScript files are never included into aggregated resources:

* resources with `f:url-norewrite="true":  
`<xh:link rel="stylesheet" href="/style.css" f:url-norewrite="true"/>`
* resources with an absolute URL, such as:  
`<xh:link rel="stylesheet" href="http://example.org/style.css"/>`
* CSS resources with a `media` attribute that is present but different from "all":  
`<xh:link rel="stylesheet" href="/style.css" media="print"/>`

### Configuration

#### Basic configuration

You enable this feature in `properties-local.xml` as follows:

```xml
<property
  as="xs:boolean"
  name="oxf.xforms.combine-resources"
  value="true"/>
```

When this is enabled, Orbeon Forms combines JS and CSS resources.

Default:

* `prod` mode: `true`
* `dev` mode: `false`

Mappings between resources URLs and the resources are stored in the `xforms.resources` cache, configured in [`RESOURCES/config/ehcache.xml`](https://github.com/orbeon/orbeon-forms/blob/master/src/main/resources/config/ehcache.xml).

#### Baseline of XForms assets

[SINCE Orbeon Forms 2017.1]

_NOTE: These properties are mainly intended for internal use. Proceed with caution._

The default baseline of XForms assets is configured with the `oxf.xforms.assets.baseline` property. Here is an example:

```xml
<property as="xs:string"  name="oxf.xforms.assets.baseline">
    {
      "css": [
        { "full": "/ops/yui/container/assets/skins/sam/container.css",                   "min": false },
        { "full": "/ops/yui/progressbar/assets/skins/sam/progressbar.css",               "min": false },
        { "full": "/ops/yui/calendar/assets/skins/sam/calendar.css",                     "min": false },
        { "full": "/apps/fr/style/bootstrap/css/bootstrap.css",                          "min": true  },
        { "full": "/apps/fr/style/form-runner-bootstrap-override.css",                   "min": false },
        { "full": "/apps/fr/style/font-awesome/css/font-awesome.css",                    "min": true  },
        { "full": "/config/theme/xforms.css",                                            "min": false },
        { "full": "/config/theme/error.css",                                             "min": false },
        { "full": "/ops/nprogress-0.2.0/nprogress.css",                                  "min": false }
      ],

      "js": [
        { "full": "/ops/jquery/jquery-3.3.1.js",                                         "min": true  },
        { "full": "/ops/jquery/jquery-browser-mobile.js",                                "min": true  },
        { "full": "/apps/fr/style/bootstrap/js/bootstrap.js",                            "min": true  },
        { "full": "/ops/javascript/orbeon/util/jquery-orbeon.js",                        "min": true  },
        { "full": "/ops/nprogress-0.2.0/nprogress.js",                                   "min": true  },
        { "full": "/ops/bowser/bowser.js",                                               "min": true  },
        { "full": "/ops/mousetrap/mousetrap.min.js",                                     "min": false },

        { "full": "/ops/yui/yahoo/yahoo.js",                                             "min": true  },
        { "full": "/ops/yui/event/event.js",                                             "min": true  },
        { "full": "/ops/yui/dom/dom.js",                                                 "min": true  },
        { "full": "/ops/yui/connection/connection.js",                                   "min": true  },
        { "full": "/ops/yui/element/element.js",                                         "min": true  },
        { "full": "/ops/yui/animation/animation.js",                                     "min": true  },
        { "full": "/ops/yui/progressbar/progressbar.js",                                 "min": true  },
        { "full": "/ops/yui/dragdrop/dragdrop.js",                                       "min": true  },
        { "full": "/ops/yui/container/container.js",                                     "min": true  },
        { "full": "/ops/yui/examples/container/assets/containerariaplugin.js",           "min": true  },
        { "full": "/ops/yui/calendar/calendar.js",                                       "min": true  },
        { "full": "/ops/yui/slider/slider.js",                                           "min": true  },

        { "full": "/ops/javascript/underscore/underscore.js",                            "min": true  },

        { "full": "/ops/javascript/xforms.js",                                           "min": true  },
        { "full": "/ops/javascript/orbeon/util/StringOps.js",                            "min": true  },
        { "full": "/ops/javascript/orbeon/xforms/server/AjaxServer.js",                  "min": true  },
        { "full": "/ops/javascript/orbeon/xforms/control/CalendarResources.js",          "min": true  },
        { "full": "/ops/javascript/orbeon/xforms/control/Calendar.js",                   "min": true  },
        { "full": "/ops/javascript/orbeon/xforms/control/Placeholder.js",                "min": true  },
        { "full": "/ops/javascript/orbeon/xforms/controls/Placement.js",                 "min": true  },
        { "full": "/ops/javascript/orbeon/xforms/controls/Help.js",                      "min": true  },
        { "full": "/ops/javascript/orbeon/xforms/controls/Hint.js",                      "min": true  },

        { "full": "/ops/javascript/scalajs/orbeon-xforms.js",                            "min": false }
      ]
    }
</property>
```

Minimal versions:
 
- are enabled when `"min"` is set to `true` and
- assume that an asset named `file.min.css` is the minimal version for `file.css`.

The `oxf.xforms.assets.baseline.excludes` property can be used to exclude assets.

It doesn't make much sense to use this property in a properties file. Instead, it should be used via an attribute on `xf:model`.

```xml
xxf:assets.baseline.excludes="/ops/javascript/scalajs/orbeon-xforms.js /ops/javascript/scalajs/orbeon-xforms-launcher.js"
```

[SINCE Orbeon Forms 2019.2]

The `xxf:assets.baseline.updates` property can be used to exclude and add assets.

It doesn't make much sense to use this property in a properties file. Instead, it should be used via an attribute on `xf:model`.

Each asset must be prefixed with a `+` or a `-` to indicate whether the asset as removed from or added to the baseline. 

```xml
xxf:assets.baseline.updates="-/ops/javascript/scalajs/orbeon-xforms.js +/apps/fr/resources/scalajs/orbeon-form-runner.js"
```

#### Baseline of XBL components assets

The baseline of resources is configured as follows:

```xml
<property
  as="xs:string"
  name="oxf.xforms.resources.baseline"
  value="fr:button fr:tabview fr:autocomplete"/>
```

The value consists of a list of qualified names referring to XBL components. Resources for the components specified are always included in every page, whether the component is used by the page or not.

In addition, you can enable caching on disk of combined resources with:

```xml
<property
  as="xs:boolean"
  name="oxf.xforms.cache-combined-resources"
  value="true"/>
```

This cache works differently from other Orbeon Forms caches, as the result is stored in the resources, typically under:

```xml
WEB-INF/resources/xforms-server/
```

One benefit of this mechanism is that it allows making such combined files to be served by an Apache front-end.

## Versioned asset resources

### Availability

This is an [Orbeon Forms PE](https://www.orbeon.com/download) feature.

This feature is enabled by default in Orbeon Forms PE.  

With the introduction of [run modes](run-modes.md),  the feature is disabled by default in `dev` mode but enabled by default in `prod` mode.

### Rationale

To further improve caching efficiency, Orbeon Forms supports enabling _versioned resources_. Usually, a resource such as a CSS, JavaScript or image file, is served through URLs like this one:

```xml
http://localhost:8080/orbeon/xforms-server/orbeon-a8adf9b6d7d9e9ed23060a279fabed50bd829236.js
```

When configuring caching on the server, for example by using an Apache front-end, you may face a dilemma:

* Caching aggressively (with an expiration date far in the future and no revalidation) so that the client asks for the resource as rarely as possible. Doing so may cause resources on the client to be out of date.
* Caching for a shorter period of time or by forcing revalidation so that your client always has a fresh version of the resources. Doing so may cause longer page loads and more load on the server.

Orbeon Forms solves this by providing the option of using versioned resources, that is inserting automatically a version number within resource URLs.

### Configuration

#### oxf.resources.versioned

You enable versioned resources in `properties-local.xml`:

```xml
<property as="xs:boolean" name="oxf.resources.versioned" value="true"/>
```

With Orbeon Forms PE, versioned resources are enabled by default in `prod` mode.

#### oxf.resources.version-number

You can configure an optional application version number for your own resources:

```xml

<property as="xs:string" name="oxf.resources.version-number" value="1.6.3"/>
```

Note that if this property is commented out or missing, no versioning takes place for your application resources even if `oxf.resources.versioned` is set to `true`.

#### oxf.xforms.resources.encode-version

The Orbeon Forms version number is not exposed by default to users in the `prod` run mode.  You can change this by setting the following property to `false`:

```xml
<property as="xs:boolean" name="oxf.xforms.resources.encode-version" value="false"/>
```

When this is set to `true`, the version number is HMAC-encoded with the configured password. This means the version number is stable, but cannot be guessed.

### Behavior

With versioned resources enabled, resources are served with URLs as follows:

```xml
http://localhost:8080/orbeon/xforms-server/58a8724010cd6fbe3ae7298de0a5c6e9dafc990b/orbeon-aa144f9fcd394054c18536a679b02ad4553e0048.js
```

The XForms Server component, which serves the resource in this case, sets an expiration date far in the future. For example, this is the description of the cache entry in the Firefox `about:cache` page:

```
Key: http://localhost:8080/orbeon/xforms-server/58a8724010cd6fbe3ae7298de0a5c6e9dafc990b/orbeon-aa144f9fcd394054c18536a679b02ad4553e0048.js  
Fetch count: 2
Last-Modified: Fri, 23 Jun 2017 06:23:34 GMT
Expires: Fri, 20 Jul 2018 09:35:03 GMT
```

This means that the resource can effectively be cached "for ever" by a client. In case the client visits Orbeon Forms pages often, the resources will be available from cache, therefore reducing page loading times and server load as well.

When Orbeon Forms is upgraded on the server, the version number changes as well. An XForms page will refer to resources with the new version number, so the cached resource is not used by the browser and a new resource is loaded from the server, before being cached. This "magic" is enabled simply with the inclusion of the Orbeon Forms version number in the URL.

Only CSS and JavaScript resources used by the XForms engine are loaded through the XForms Server component. Other resources like images referred by XForms stylesheets are served by the Page Flow Controller, through URLs like this one:

```xml
http://localhost:8080/orbeon/ops/images/xforms/error.png
```

With resource versioning enabled, the URL becomes:

```xml
http://localhost:8080/orbeon/58a8724010cd6fbe3ae7298de0a5c6e9dafc990b/ops/images/xforms/error.png
```

When resource versioning is enabled, the Page Flow Controller by default serves all the resources defined in `<files>` elements by first checking the `oxf.resources.versioned` property. If versioning is enabled, the PFC removes the version number from the URL before searching for the resource on disk. It is possible to turn this behavior on and off selectively with the `versioned` attribute. Here is how to turn off versioning for PDF files in `page-flow.xml`:

```xml
<config xmlns="http://www.orbeon.com/oxf/controller" xmlns:oxf="http://www.orbeon.com/oxf/processors">
    <!-- GIF images are loaded following oxf.resources.versioned -->
    <files path-info="*.gif"/>
    <!-- More file definitions here -->
    ...
    <!-- PDF files are not versioned -->
    <files path-info="*.pdf" versioned="false"/>
    <!-- More file definitions here -->
    ...
    <!-- More page definitions here -->
    ...
</config>
```

Conversely, resource URLs produced by an XForms page are automatically rewritten following the Page Flow's `<files>` definitions.

The versioning mechanism is made available to your own application resources as well. Any resource whose path doesn't start with `/ops/` or `/config/` is considered part of your application, not of Orbeon Forms. In that case, the Orbeon Forms version number is not used, but you specify instead an application version number in properties-local.xml:

```xml
<property as="xs:string" name="oxf.resources.version-number" value="1.6.3"/>
```

For deployed application, you should upgrade the application version number whenever you modify application resources so that clients retrieve the proper resources.

The following scenario shows the entire lifecycle for application resources:

* You create an image as `RESOURCES/apps/foo/bar.png`
* You refer to it as:
    
    ```xml
    <xhtml:body>
        <xhtml:img src="/apps/foo/bar.png" alt="My Image"/>
    </xhtml:body>
    ```

* With versioning enabled, the image path is rewritten automatically as follows:

    ```xml
    /1.6.3/apps/foo/bar.png
    ```

    Note that the application resource number is used because the resource is not part of Orbeon Forms.

* Your browser sees a URL like:

    ```xml
    http://localhost:8080/orbeon/1.6.3/apps/foo/bar.png
    ```

* When the browser loads the image, the PFC receives back:

    ```xml
    /1.6.3/apps/foo/bar.png
    ```

* The PFC knows that PNG files are versioned, so removes the version number and sends this resource to the browser:

    ```xml
    RESOURCES/apps/foo/bar.png
    ```

From client-side JavaScript, you can access the application version number as follow:

```xml
var version = ORBEON.util.Utils.getProperty(APPLICATION_RESOURCES_VERSION_PROPERTY);
```

Versioned resources served by the PFC (that is all the resources except the XForms engine's CSS and JavaScript resources) also get an aggressive expiration date.

In case you use Apache, you can in addition configure a rewriting rule with [mod_rewrite](https://httpd.apache.org/docs/2.2/mod/mod_rewrite.html) to allow Apache to directly load resources containing a version number, as shown below.

_NOTE: We recommend restarting Orbeon Forms after changing the `oxf.resources.versioned` property, as data in Orbeon Forms caches may not be made aware of the change until the next restart._

## Examples of Apache configurations

_WARNING: As of 2017-03-03, these configuration are likely out of date._

Here is how you can configure Apache to serve Orbeon Forms resources. This assumes the following:

* Orbeon Forms deployed under the `/orbeon` context
* Orbeon Forms exploded WAR file under `/home/orbeon/war/`
* `orbeon-resources-public.jar` unziped under `/home/orbeon/war/WEB-INF/orbeon-resources-public/`
* `oxf.xforms.cache-combined-resources` set to `true` in `properties-local.xml`

Without [resources versioning](#versioned-asset-resources):

```
RewriteEngine on
# Rewrite CSS and JavaScript resources served by the XForms Server
# -> make sure "oxf.xforms.cache-combined-resources" is set to "true" in properties-local.xml
RewriteRule ^/orbeon/(xforms-server/.*\.(css|js))$ /home/orbeon/war/WEB-INF/resources/$1 [L]
# Serve /config/theme resources
# -> make sure orbeon-resources-public.jar is unzipped under "orbeon-resources-public"
RewriteRule ^/orbeon/(config/theme/.*\.(css|png|gif))$ /home/orbeon/war/WEB-INF/orbeon-resources-public/$1 [L]
# Serve /ops resources
# -> make sure orbeon-resources-public.jar is unzipped under "orbeon-resources-public"
RewriteRule ^/orbeon/((ops|config/theme|xbl/orbeon)/.*\.(gif|css|pdf|json|js|png|jpg|xsd|htc|ico|swf))$ /home/orbeon/war/WEB-INF/orbeon-resources-public/$1 [L]
# Serve remaining resources
RewriteRule ^/orbeon/(.*\.(gif|css|pdf|json|js|png|jpg|xsd|htc|ico|swf))$ /home/orbeon/war/WEB-INF/resources/$1 [L]
```

With [resources versioning](#versioned-asset-resources):

```
RewriteEngine on
# Rewrite CSS and JavaScript resources served by the XForms Server
# -> make sure "oxf.xforms.cache-combined-resources" is set to "true" in properties-local.xml
RewriteRule ^/orbeon/xforms-server/[^/]+/(.*\.(css|js))$ /home/orbeon/war/WEB-INF/resources/xforms-server/$1 [L]
# Set far expiration date
<LocationMatch "^/orbeon/xforms-server/([^/]+/.*\.(css|js))$">
  Header set Expires "Wed, 1 Jan 2020 12:00:00 GMT"
</LocationMatch>

# Serve /ops resources
# -> make sure orbeon-resources-public.jar is unzipped under "orbeon-resources-public"
RewriteRule ^/orbeon/[^/]+/((ops|config/theme|xbl/orbeon)/.*\.(gif|css|pdf|json|js|png|jpg|xsd|htc|ico|swf))$ /home/orbeon/war/WEB-INF/orbeon-resources-public/$1 [L]
# Set far expiration date
<LocationMatch "^/orbeon/[^/]+/((ops|config/theme|xbl/orbeon)/.*\.(gif|css|pdf|json|js|png|jpg|xsd|htc|ico|swf))$">
  Header set Expires "Wed, 1 Jan 2020 12:00:00 GMT"
</LocationMatch>
# Serve remaining resources
RewriteRule ^/orbeon/[^/]+/(.*\.(gif|css|pdf|json|js|png|jpg|xsd|htc|ico|swf))$ /home/orbeon/war/WEB-INF/resources/$1 [L]
# Set far expiration date
<LocationMatch "^/orbeon/[^/]+/(.*\.(gif|css|pdf|json|js|png|jpg|xsd|htc|ico|swf))$">
  Header set Expires "Wed, 1 Jan 2020 12:00:00 GMT"
</LocationMatch>
```

All in all, the rules above perform the following:

* Special handling for XForms JavaScript and CSS files (using the cached/combined/minimized resources)
* Serving of `/config/theme` resources originally in `orbeon-resources-public.jar`
* Serving of other `/ops` resources originally in `orbeon-resources-public.jar`
* Serving of other resources under `RESOURCES`
* Marking all versioned resources with an expiration date in the far future

## JavaScript at the bottom of the page

### With Orbeon Forms 2019.1

With Orbeon Forms 2019.1, this feature is removed from Orbeon Forms and the `oxf.xforms.resources.javascript-at-bottom` property doesn't have any effect.

### With Orbeon Forms 2018.1 and 2018.2

With Orbeon Forms 2018.1 and 2018.2, this feature is deprecated and scripts are by default placed within the `<head>` section
with the `defer` attribute. 

### With Orbeon Forms 2017.2 and older

With Orbeon Forms 2017.2 and older, this feature is enabled by default.

The following property, if enabled, places external and inline JavaScript at the bottom of the page for performanc
reasons:

```xml
<property
    as="xs:boolean"
    name="oxf.xforms.resources.javascript-at-bottom"
    value="true"/>
```

See Yahoo’s [Best Practices for Speeding Up Your Website](https://developer.yahoo.com/performance/rules.html#js_bottom)
