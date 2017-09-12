# Paths and matchers

<!-- toc -->

## Patterns

The value of the `path` attribute can be either a glob pattern or a full regular expression (Java regular expressions, which are very similar to Perl 5 regular expressions).

| Value |  Description |
|---|---|
| Glob |  This is the default but can also be set explicitly with the  `matcher` attribute set to glob (or `oxf:glob-matcher` for backward compatibility) on the `<page>` element. This supports the wildcards "`*`" and "?". as well as character classes, as [documented here][21].|
|Regular expression|This is enabled with the  `matcher` attribute set to regexp (or oxf:perl5-matcher for backward compatibility) on the `<page>` element. This enables full Java/Perl 5 regular expressions.|

Simple examples of glob:

* `/about/company.html `matches exactly this URL
* `about/*` matches any URL that starts with `about/`
* `*.gif` matches any URL that ends with `.gif`
* `a?c` matches `aac`, `abc`, `etc`.
  
A default value for the `matcher` attribute can also be placed on the element:

```xml
<controller xmlns="http://www.orbeon.com/oxf/controller" matcher="regexp">
    ...
</controller>
```

_COMPATIBILITY NOTE: Prior to Orbeon Forms 4.0, expression matchers were fully configurable via XPL processors. Starting Orbeon Forms 4.0, only Java/Perl 5 regular expressions and glob expressions are supported._

## Matching files and pages with regular expressions

Groups of files can be matched using a single `<files>` element with the regexp matcher:

```xml
<files
  path="/doc/[^.]*\.html"
  matcher="regexp"/>
```

A matcher can also be specified on a `<page>` element:

```xml
<page
  path="/forms/([_A-Za-z\-0-9]+)/page/([0-9]{1,3})"
  matcher="regexp"/>
```

When using a matcher that allows for groups, the part of the path matched by those groups can be extracted as documented above with the `<setvalue>` element. This is only supported with the regexp matcher.

## Restricting HTTP methods

[SINCE Orbeon Forms 2017.2]

The `methods` attribute restricts which HTTP methods are allowed on the page or service. It is a space-separated list of HTTP method names. `#all` can be explicitly used to indicate that all HTTP methods are allowed.

By default, all HTTP methods are allowed.

This example requires that the incoming request be an HTTP `POST`, or the controller returns a "404 Not Found" status code:

```xml
<service
    path="/fr/service/([^/^.]+)/([^/^.]+)/(duplicate)"
    methods="POST"
    model="request-parameters.xpl"
    view="services/duplicate.xhtml"/>
 ```

*NOTE: This is independent from page or service authorization.* 

## Parametrizing the `model` and `view` attributes

The result of matches can be referred to directly in the `model` and `view` attributes using the notation `${_group-number_}`:

```xml
<page
  path="/forms/([_A-Za-z\-0-9]+)/page/([0-9]{1,3})"
  matcher="regexp"
  model="oxf:/forms/${1}/model.xpl"
  view="oxf:/forms/${1}/view-${2}.xhtml"/>
```

In this case, if the path contains: `/forms/my-form/page/12`:

* The model file read will be `oxf:/forms/my-form/model.xpl`
* The view file `oxf:/forms/my-form/view-12.xhtml`

Parametrizing `model` and `view` attributes this way often allows greatly reducing the size of page flows that contain many similar pages.

## Navigating to pages that use matchers

When a `result` element directs flow to a page that uses matchers and `<setvalue>` elements, the PFC attemps to rebuild the destination path accordingly. Consider the following example:

```xml
<page id="source" path="/">
    <action>
        <result page="destination" transform="oxf:xslt">
            <form xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xsl:version="2.0">
                <username>orbeon</username>
                <blog-id>12345</blog-id>
            </form>
        </result>
    </action>
</page>
```

```xml
<page
  id="destination"
  path="/user/([^/]+)/blog/([^/]+)"
  matcher="regexp"
  view="blogs/${1}/blog-${2}.xhtml">
    <setvalue ref="/form/username" matcher-group="1"/>
    <setvalue ref="/form/blog-id" matcher-group="2"/>
</page>
```

In this example, accessing the `source` page directly causes navigation to the `destination` page. Using the following information:

* The internal XML submission
* The destination page's matcher groups
* The `<setvalue>` elements

The PFC reconstructs the path to `/user/orbeon/blog/12345`. This path is used to request the `destination` page. In this case, the `view` attribute evaluates to `blogs/orbeon/blog-12345.xhtml`.

_NOTE: Navigation to pages that use matchers but that do not provide an internal XML submission or `<setvalue>` elements will cause the requested path to have its literal value, in the example above `/user/([^/]+)/blog/([^/]+)`. It is not advised to perform navigation this way._

[21]: https://svn.apache.org/repos/asf/jakarta/oro/tags/oro-2.0.9-dev-1/docs/api/org/apache/oro/text/GlobCompiler.html
