# Adding an Atom feed

## What is it?

Remember, the name of this application is Book_cast_, which lets suggest that we can expose the list of books as a feed of some sort. Here, you will use the Atom Syndication Format (or Atom in short). Atom is a format very much like RSS but it has been standardized by IETF and is much cleaner than RSS (note that there are at least 6 different versions of RSS). Atom is now supported by most feed readers.

An atom feed looks like this (example from the Atom specification):

```xml
<feed xmlns="http://www.w3.org/2005/Atom">

    <title>Example Feed</title>
    <link href="http://example.org/"/>
    <updated>2003-12-13T18:30:02Z</updated>
    <author>
        <name>John Doe</name>
    </author>
    <id>urn:uuid:60a76c80-d399-11d9-b93C-0003939e0af6</id>

    <entry>
        <title>Atom-Powered Robots Run Amok</title>
        <link href="http://example.org/2003/12/13/atom03"/>
        <id>urn:uuid:1225c695-cfb8-4ebb-aaaa-80da344efa6a</id>
        <updated>2003-12-13T18:30:02Z</updated>
        <summary>Some text.</summary>
    </entry>

</feed>
```

It would be nice if you could use XForms to produce such a format, and in fact in theory this is possible, but Orbeon Forms currently only supports XForms embedded within XHTML. So here you will use XSLT instead. XSLT is an XML transformation language, which can also be used as an XML template language.

But first, it's time to introduce the Model-View-Controller (MVC) support in the page flow. Consider the following page flow declaration:

```xml
<page path="/my-bookcast/atom" model="atom.xpl" view="atom.xsl">
```

Notice how, instead of an XHTML page view (`view.xhtml`), you now use:

* A page _model_, called `atom.xpl`. This page model has an `*.xpl` extension, which tells you that it contains an _XML pipeline_.
* A page _view_, called `view.xsl`. This page view has an `*.xsl` extension, which tells you that it contains an _XSLT stylesheet_.

So what's the idea page models and page view? The idea is to separate the _production_ of the data to display, from the _visual formatting_ of that data. The page model is in charge of the former, and the page view of the latter. In the case of the production of the Atom feed:

* The page model is in charge of fetching the data (the `books.xml` document) from the database.
* The page view formats that data to produce a valid Atom document.

This separation means that you can change how the data is retrieved without changing the formatting part, and the other way around. The Orbeon Forms Page Flow Controller (PFC) automatically connects page model and page view.

Consider the page model:

```xml
<p:config
    xmlns:p="http://www.orbeon.com/oxf/pipeline"
    xmlns:oxf="http://www.orbeon.com/oxf/processors">

    <p:param name="data" type="output"/>

    <!-- Execute REST submission -->
    <p:processor name="oxf:xforms-submission">
        <p:input name="submission">
            <xf:submission
                xmlns:xforms="http://www.w3.org/2002/xforms"
                serialization="none"
                method="get"
                resource="/exist/rest/db/orbeon/my-bookcast/books.xml"/>
        </p:input>
        <p:input name="request"><dummy/></p:input>
        <p:output name="response" ref="data"/>
    </p:processor>

</p:config>
```

This document contains an XML pipeline described in a language called XPL (XML Pipeline Language). An XML pipeline language is simply a language for describing operations to be performed on XML documents. Orbeon Forms comes with an implementation of [XPL][23]. (Orbeon is currently working at W3C on the standardization of a pipeline language called [XProc][24].)

So what does this pipeline do? It runs a _processor_ called `oxf:xforms-submission`, which is handy component that allows you to perform XForms submissions from XPL. That submission retrieves `books.xml` and returns it on the processor's `response` output. That output in turn is sent to the `data` output of the page model pipeline.

Now look at the page view:

```xml
<feed
    xmlns="http://www.w3.org/2005/Atom"
    xmlns:xs="http://www.w3.org/2001/XMLSchema"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xsl:version="2.0">

    <title>Orbeon Forms Bookcast</title>
    <subtitle>An Orbeon Forms tutorial example</subtitle>
    <updated><xsl:value-of select="current-dateTime()"/></updated>
    <id>http://www.orbeon.com/ops/my-bookcast/</id>
    <link href="http://www.orbeon.com/"/>
    <generator uri="http://www.orbeon.com/ops/my-bookcast/" version="1.0">Orbeon Forms Bookcast</generator>

    <xsl:for-each select="/books/book">
        <entry>
            <title><xsl:value-of select="concat(author, ' - ', title)"/></title>
            <id>http://www.orbeon.com/ops/my-bookcast/<xsl:value-of select="concat(author, ' - ', title)"/>"/&gt;</id>
            <updated><xsl:value-of select="current-dateTime()"/></updated>
            <content type="xhtml" xml:lang="en">
                <div xmlns="http://www.w3.org/1999/xhtml">
                    <p>
                        Book information:
                    </p>
                    <table>
                        <tr>
                            <th>Title</th>
                            <td><xsl:value-of select="title"/></td>
                        </tr>
                        <tr>
                            <th>Author</th>
                            <td><xsl:value-of select="author"/></td>
                        </tr>
                        <tr>
                            <th>Language</th>
                            <td><xsl:value-of select="language"/></td>
                        </tr>
                        <tr>
                            <th>Link</th>
                            <td><a href="{link}"><xsl:value-of select="link"/></a></td>
                        </tr>
                        <tr>
                            <th>Rating</th>
                            <xsl:variable name="rating" select="if (rating castable as xs:integer) then xs:integer(rating) else 0" as="xs:integer"/>
                            <td><xsl:value-of select="string-join(for $i in (1 to $rating) return '*', '')"/></td>
                        </tr>
                        <tr>
                            <th>Notes</th>
                            <td>
                                <xsl:for-each select="tokenize(notes, '&amp;#x0a;')">
                                    <xsl:value-of select="."/>
                                    <xsl:if test="position() lt last()">
                                        <br/>
                                    </xsl:if>
                                </xsl:for-each>
                            </td>
                        </tr>
                    </table>
                </div>
            </content>
        </entry>
    </xsl:for-each>
</feed>
```

This page view is an XSLT document (notice the `xsl:version="2.0"` attribute on the root element). It automatically receives on its main input the document produced by the page model. So if you were to write:

```xml
<xsl:value-of select="/books/book[1]/title">
```

You would get the title of the first book from `books.xsl`.

Now this XSLT document does not use many XSLT constructs:

* XSLT relies on XPath, like XForms. So you can reuse your knowledge of XPath when writing XSLT.
* `<xsl:value-of>` outputs the text value returned by the XPath expression on the `select` attribute. It is very similar to `<xf:output>`.
* `<xsl:for-each>` iterates over the nodes returned by the XPath expression on the `select` attribute. It is very similar to `<xf:repeat>`.
* The brackets in `<a href="{link}">` mean that the XPath expression `link` has to be evaluated to produce the `href` attribute.

And that's it! You can now add the entry in `page-flow.xml`, add the two files `atom.xpl` and `atom.xsl`, and point your browser to:

```xml
http://localhost:8080/orbeon/my-bookcast/atom
```

You should see something similar to this, depending on your browser:

![][25]

To make things even better, add the following to `view.xhtml` under the `<head>` element:

```xml
<link
    rel="alternate"
    type="application/atom+xml"
    title="Orbeon XForms Bookcast Tutorial Feed"
    href="atom">
```

With this addition, most modern browsers will display a feed icon or an RSS icon, making the feed directly accessible from the main Bookcast page.

You can now try to load the feed into your favorite feed reader! Here is how the Bookcast feed looks in the Mozilla Thunderbird feed reader:

![][26]

## What's next

So far you have seen:

* How to setup Orbeon Forms.
* How the basic Hello application is organized.
* How to build your own application that allows editing and persisting a form.
* How to create an Atom feed from form data.

You have now covered a good part of the basics of Orbeon Forms. You can now look at the [Orbeon Forms example applications][27] and the [rest of the Orbeon Forms documentation](http://doc.orbeon.com/)!


[23]: http://www.w3.org/Submission/xpl/
[24]: http://www.w3.org/TR/xproc/
[25]: https://raw.github.com/wiki/orbeon/orbeon-forms/images/tutorial/19.png
[26]: https://raw.github.com/wiki/orbeon/orbeon-forms/images/tutorial/20.png
[27]: http://demo.orbeon.com/orbeon/home/
