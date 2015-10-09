# Orbeon Forms XForms Tutorial - forms

## What is this?

This is the tutorial for Orbeon Forms's XForms features. It is aimed at programmers who want to program Orbeon Forms, as opposed to analysis who want to use [Form Builder][1].

If you have questions, issues or suggestions related to this tutorial, please send a message to the [Orbeon forum][2].

## A little bit of background

### What Orbeon Forms does for you

Orbeon Forms is an open source solution to build and deploy web forms. For more information, please visit the Orbeon home page at http://www.orbeon.com.

### Prerequisites

To go through this tutorial, you don't need much: any reasonably modern computer on which you can install Java 6 or 7 should do. You should be comfortable with installing new software on your computer, including uncompressing zip or gzip archives. You will also have to edit XML files. If you are familiar with HTML, this should not be a problem.

You also need a reasonably recent web browser. We recommend one of the following browsers:

* Mozilla Firefox
* Safari 4 or greater
* Google Chrome
* Opera 10 or greater
* Internet Explorer 8 or greater

You will _not_ have to:

* Write any Java code or any scripting language code.
* Use a compiler or other complicated build tool.
* Install browser plugins or any other client-side software, besides your regular web browser!

### Principles of Orbeon Forms

Orbeon Forms follows a few principles:

* **More standards.** You use standards whenever possible. For example, Orbeon Forms applications are created using XForms and XHTML, which are W3C standards.
* **Less scripting.** You write most applications without writing Java, JavaScript, or other scripting code. (But you can if you really want.)
* **Instant gratification.** You get instant gratification by making changes to your application and just reloading your page in your web browser. (You don't need to "compile" or "build".)

## Installing and configuring Orbeon Forms

### Downloading and installing Java

Java provides the cross-platform environment in which Orbeon Forms runs.

If you don't have Java installed yet, download it from http://www.oracle.com/technetwork/java/javase/downloads/index.html.

_NOTE: If you use a Mac with Mac OS X, you probably have Java already installed on your machine, but if not visit <http: support.apple.com="" kb="" dl1572="">. Then follow the instructions to install Java._

### Downloading and installing Apache Tomcat

Tomcat is the container application into which Orbeon Forms deploys. Follow these steps to download and install Tomcat if you don't have it installed yet:

1. Download Tomcat 6 from the Apache web site at http://tomcat.apache.org/download-60.cgi..

2. Install Tomcat as per the instructions. If you downloaded the installer version (Windows only), run the installer. If you downloaded a compressed archive, uncompress it to the location of your choice. We call the install location `TOMCAT_HOME` (on windows, this could be `c:/Program Files/Apache/Tomcat`, on a Unix system, `/home/jdoe/tomcat`, etc.).

3. Check that your Tomcat installation is working correctly:
    * Run the Tomcat startup script under `TOMCAT_HOME/bin` (`startup.sh` or `startup.bat` depending on your platform), or start Tomcat with the control application (Windows only).
    * Open a web browser and access the following URL:
    ```xml
    http://localhost:8080/
    ```

    You should see the Tomcat welcome page.

    ![][3]

_NOTE: We recommend using Tomcat for this tutorial, but Orbeon Forms can deploy into containers other than Tomcat._

### Downloading and installing Orbeon Forms

Follow these steps to download and install Orbeon Forms:

1. [Download][4] Orbeon Forms.

2. Uncompress the archive into a directory of your choice. We call that directory `ORBEON_FORMS_HOME`.

3. Under `ORBEON_FORMS_HOME`, you find a file called `orbeon.war`. This is the file to deploy into Tomcat. To do so, just copy it under `TOMCAT_HOME/webapps` (alternatively, if you know what you are doing, you can uncompress it at a location of your choice and configure a context in `TOMCAT_HOME/conf/server.xml`). The `webapps` directory is already present after you have installed Tomcat.

### Testing your setup

Make sure you restart Tomcat (run the shutdown script under `TOMCAT_HOME/bin`, and then the startup script again). Then open up with a web browser the following URL:

```xml
http://localhost:8080/orbeon/
```

You should see the Orbeon Forms welcome page:

![][5]

## The Hello application

### Running the Hello application

The Hello application is about the simplest Orbeon Forms application you can think of. It asks for your name and displays it back. Here is a direct link for running XForms Hello [online on the Orbeon web site][6].

Simply enter your first name in the input field, for example "Joe". You should promptly see a message underneath saying "Hello, Joe!".

![][7]

* `view.xhtml`: this is the XHTML and XForms code for the Hello application.
* `page-flow.xml`: this is the page flow for the Hello application. The main task of a page flow is mapping external URLs (as typed in a web browser) to Orbeon Forms pages.

### The source code

You are now ready to look at the source code of the Hello application. This will give you an idea of what an Orbeon Forms application looks like. First, select `view.xhtml` to make the source code for that file appear on the right:

```xml
<html xmlns="http://www.w3.org/1999/xhtml"
      xmlns:xforms="http://www.w3.org/2002/xforms">
    <head>
        <title>XForms Hello</title>
        <xf:model>
            <xf:instance>
                <first-name xmlns=""/>
            </xf:instance>
        </xf:model>
    </head>
    <body>
        <p>
            <xf:input ref="/first-name" incremental="true">
                <xf:label>Please enter your first name:</xf:label>
            </xf:input>
        </p>
        <p>
            <xf:output
              value="if (normalize-space(/first-name) = '') then ''
                     else concat('Hello, ', /first-name, '!')"/>
        </p>
    </body>
</html>
```

The first thing you notice is that this looks very much like HTML (notice the `<html>` tag). But in fact, this is XHTML, the XML-compatible version of HTML. There are only a few differences that matter between HTML and XHTML, in particular you must close all your tags and use quotes around attributes. Also, you must place your tags in a _namespace_, which is why the `<html>` tag features the `xmlns="http://www.w3.org/1999/xhtml"` _namespace declaration_.

Another difference with plain HTML is that there are tags that start with the string `xf:`. Those are defined by the [XForms specification][8] from W3C. They are at the heart of Orbeon Forms and enable all the cool forms features that you see in Orbeon Forms demos. (In order to use XForms tags that start with `xf:`, you must add another namespace declaration on the `<html>` tag: `xmlns:xforms="http://www.w3.org/2002/xforms"`.)

Using XForms in Orbeon Forms means that you don't have to use HTML forms at all. The benefit is that XForms is much more powerful than HTML forms, as you will see in this tutorial.

You notice a tag called `<xf:model>`. Because XForms follows a Model-View-Controller (MVC) approach, most XForms pages contain one or more _models_ that usually encapsulate other XForms markup. You place these models under the XHTML `<head>` tag.

Note that from now on, we prefer the term _element_ to the term _tag_. An element is an XML term that includes the start tag and end tag, and can have content such as other elements and text.

With XForms, you store the data captured by controls such as input fields, combo boxes, etc. as text contained within XML elements or attributes. Consider the following XML document containing a single element called `first-name`.

```xml
<first-name>
```

The `first-name` element is _empty_. Contrast with:

```xml
<first-name>Joe</first-name>
```

The element now contains the string "Joe". Notice how in `view.xhtml` the XML document is encapsulated within an `<xf:instance>` element:

```xml
<html>
    <xf:instance>
        <first-name xmlns=""/>
    </xf:instance>
</html>
```

Also notice the special `xmlns=""` namespace attribute on `<first-name>`: this is necessary because the root element of the XHTML document defines a default namespace with `xmlns="http://www.w3.org/1999/xhtml"`. Because we want to make it clear that the element `<first-name>` is not an XHTML element, but an element in no namespace, we add `xmlns=""` to that element. If your instance document must be in no particular namespace, you are always safe to add `xmlns=""` on the root element of that instance.

This element defines an XForms _instance_, which is just XForms' way of calling an XML document used to store data.

Now consider the remaining XForms elements in the source file: `<xf:input>` and `<xf:output>`. These two elements are not located under the XHTML ``, but under ``. They are part of the _view_ of your page, in other words these elements directly help define visible controls on the page. Consider `<xf:input>`:

```xml
<xf:input ref="/first-name" incremental="true">
```

You guessed that this element allows the user to _input_ information. `<xf:input>` is usually displayed to the user as an input field. The `ref` attribute is the magic that connects the input field to the XForms instance. It contains an _XPath expression_, which in this case just looks like a file path. In this case, `/first-name` points to the element called `first-name`, which happens to be the only element we have in the XForms instance. Using the `ref` attribute this way is called a _binding_ and means two things:

* When the user enters text in the input field, the text is saved into the element called `first-name`.
* It also goes the other way: if somehow the text content of `first-name` in the XForms instance changes, this is automatically reflected in the input field.

Now consider `<xf:output>`. As you guess from the name of the element, this simply displays a value on screen. If you have tried running the Hello application, you have probably guessed the logic that is being implemented: if the first name entered by the user is not a blank string, then we display the "Hello" message followed by the first name and then an exclamation mark. Otherwise if the first name consists only of spaces, we just display a blank string. The idea is to avoid displaying things like "Hello !" without an actual first name.

With many client-side libraries, you express this type of logic with JavaScript. With XForms, you use XPath instead. This means that you need to learn at least a few bits of the XPath syntax. While XPath may be different from what you already know (it is based on expressions and definitely targeted at XML), it is in fact a smaller language than JavaScript.

So how do you hook-up the logic within `<xf:output>`? Here, instead of a `ref` attribute, we use a `value` attribute. Like `ref`, `value` takes an XPath expression, but it doesn't actually create a binding to instance data: it just returns a string. The XPath is as follows:

```ruby
if (normalize-space(/first-name) = '') then
    ''
else
    concat('Hello, ', /first-name, '!')
```

A few things to point out:

* The main expression has the form `if () then ... else ...`. (This is actually an XPath 2.0 expression, which supports the `if` construct.)
* The `normalize-space()` function removes all leading and trailing space (and simplifies internal spaces as well). This is a little trick to not only test whether the first name is empty, but also to test whether it is an all-blank string.
* Contrary to JavaScript, the equality test is expressed with a single `=` instead of `==`.
* The `concat` function concatenates all its parameters into a resulting string. It is like the JavaScript `+` operator on strings.
* Note the use of the single quote `'` around strings. This is needed because the XPath expression is put within double quotes `"` in the `value` attribute.

### Page flow

If you look at your browser's URL bar when showing the example, you notice that it looks like this:

```xml
http://localhost:8080/orbeon/xforms-hello/
```

* The first part of the URL, `http://localhost:8080/`, is self-explanatory: it depends on what host and port your server is running.
* The next part, `orbeon`, depends on what context you install Orbeon Forms into. By default, because the WAR file is called `orbeon.war`, the context is `orbeon`. (You could as well configure your container to use a different context, or an empty (default) context.)
* The last part, `/xforms-hello/`, automatically matches the name of the directory you store your application into (more on this later when you look at the source code). This causes the application's page flow (`page-flow.xml`) to run.

So now look at `page-flow.xml` for the Hello application. It is very simple:

```xml
<controller xmlns="http://www.orbeon.com/oxf/controller">
    <page path="*" view="view.xhtml"/>
    <epilogue url="oxf:/config/epilogue.xpl"/>
</controller>
```

The important line in this page flow is this one:

```xml
<page path="*" view="view.xhtml">
```

It tells Orbeon Forms that any path (notice the wildcard `*`) sent by the web browser to this application causes the page view stored in `view.xhtml` to be processed. You can check this by entering the following path in your browser:

```xml
http://localhost:8080/orbeon/xforms-hello/my-page
```

The exact same result shows! Of course, page flows make the most sense when you have more than one page in your application, which translates into more than one `<page>` element.

### Orbeon Forms resources

Under `TOMCAT_HOME/webapps/orbeon`, you find a `WEB-INF` directory. That directory, in turn, contains a `resources` directory. We refer to that `resources` directory as `RESOURCES` below.

The `RESOURCES` directory is very important: this is where your application lives. This directory is called "resources" because it contains all the files (or resources) such as XHTML documents, XML schemas, images, CSS, etc. needed by your Orbeon Forms application.

_NOTE: Orbeon Forms supports storing your resources pretty much anywhere you want. For convenience, by default they are available under the WAR file's `WEB-INF/resources` so you can get up and running without configuration._

### A look into the resources directory structure

Under `RESOURCES`, you see the following files and directories:

* `apps` directory: contains one sub-directory for each application currently running in Orbeon Forms. By default, it contains a series of sample applications - the ones that you see on the left side of your screen when you start Orbeon Forms. Notice a directory called `xforms-hello`: this is the directory that contains the Hello application.
* `config` directory: contains several configuration files that you can modify if needed. For now, you don't need to worry about this directory.
* `page-flow.xml` file: the top-level page flow file. You should not worry about this file just yet. Just know that by default, it is in charge of deciding which application to run depending on the application name in the URL that you enter in your web browser.
* `apps-list.xml` file: contains the list of sample applications to display on the left side of the screen. This is not needed by your own application.

### Playing with the hello application source code

You find the application under `RESOURCES/apps/xforms-hello`. That directory contains the two files that you have seen earlier through the Source Code Viewer in your web browser. Again, the name `xforms-hello` is important and matches the `xforms-hello` part of the URL in your web browser: `http://localhost:8080/orbeon/xforms-hello/`.

_NOTE:_

_In this tutorial, you will often have to "reload" pages in your web browser to see the effects of your changes to XHTML or XForms markup. Because of the way Orbeon Forms handles page reloads with XForms, for consistent results we recommend that you don't simply use your browser's reload button, but instead use one of the following two ways to reload a page:_

* _Position your text cursor on your browser's URL bar and press the "Enter" key. Browsers often have keyboard shortcuts such as `CTRL-L` or `CMD-L` to reach the URL bar._
* _Use your browser's "force reload" feature. This is often enabled by pressing the `SHIFT` key and pressing the "reload" button at the same time. Browsers often have keyboard shortcuts such as `CTRL-F5`, `CTRL-SHIFT-R`, or `CMD-SHIFT-R` to perform this operation._

Now, modify `view.xhtml`:

* Open `view.xhtml` in a text editor.
* Modify the string "Please enter your first name:" with "Your name here:".
* Save `view.xhtml`.
* Go back to the Hello application in your web browser and reload the page. You should see the new text appear:

![][9]

* Repeat the experience but add some XForms. For example, add a second `<xf:input>` right after the first one:

```xml
<xf:input ref="/first-name" incremental="true">
```
```xml
<xf:input ref="/first-name" incremental="true">
```

Reload the page, and notice, as you type in an input field, how the other one updates as you type. This happens because the two fields are bound to the same instance data. The `incremental="true"` attribute allows the changes to occur as you type, instead of occurring when you focus in and out of fields:

![][10]

You notice that you get instant gratification with Orbeon forms: just change files on disk, reload your page, and your changes are taken into account with no compilation or other complex deployment.

## The Bookcast application

### What is it?

In this section, you will create a more complete XForms application: the Bookcast application. The Bookcast application allows you to keep track of information about books you have read. For each book, you enter information such as title, author, language, and your own comments. The information is persisted so you can access it again. Then you can do cool things with the available data such create an Atom feed of your entries.

![][11]

You can run the final application [online on the Orbeon web site][12].

### Getting started

But first things first. Start by making a first functional page:

* The first thing to do is to create a new directory for your application. Orbeon Forms already come with the complete `xforms-bookcast` application, so instead let's decide on another name, for example `my-bookcast`. Create a directory with that name as `RESOURCES/apps/my-bookcast`. For convenience, we refer to that new directory as the `BOOKCAST` directory below.
* Create a `page-flow.xml` file under `BOOKCAST`:

```xml
<controller xmlns="http://www.orbeon.com/oxf/controller">
    <page path="/my-bookcast/" view="view.xhtml"/>
    <epilogue url="oxf:/config/epilogue.xpl"/>
</controller>
```

This page flow is automatically called for any path that starts with `/orbeon/my-bookcast/`. Here, it matches on the exact path `/orbeon/my-bookcast/` and calls up the page view called `view.xhtml`.

* Create a skeleton for your `view.xhtml` under `BOOKCAST`:

```xml
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:xhtml="http://www.w3.org/1999/xhtml" xmlns:xforms="http://www.w3.org/2002/xforms" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:ev="http://www.w3.org/2001/xml-events" xmlns:xxforms="http://orbeon.org/oxf/xml/xforms">
    <head>
        <title>XForms Bookcast</title>
    </head>
    <body>
        <p>Hello!</p>
    </body>
</html>
```

This is a very basic XHTML document. It features a title in the `<head>` and a "Hello!" message in the ``/. It also declares a bunch of XML namespaces that you need later in the document.

Now go to:

```xml
http://localhost:8080/orbeon/my-bookcast/
```

You should something like this:

![][13]

_NOTE:_

_If you get lost at some point in this tutorial, feel free to look at the reference source files for the Bookcast application:_

* [view.xhtml][?]
* [page-flow.xml][?]

### XForms model and instance

An XForms document that wants to do something really useful needs at least one model containing one instance. But first it is wise to decide how you would like to represent the information captured by your form. This is an example that shows a possible representation of the data of the Bookcast application (notes are borrowed from Wikipedia and are under the GNU Free Documentation License):

```xml
<books>
    <book>
        <title>Don Quixote de la Mancha</title>
        <author>Miguel de Cervantes Saavedra</author>
        <language>es</language>
        <link>http://en.wikipedia.org/wiki/Don_Quixote</link>
        <rating>5</rating>
        <notes>
            The book tells the story of Alonso Quixano, a man who has read so many stories about
            brave errant knights that, in a half-mad and confused state, he believes himself to be a
            knight, re-names himself Don Quixote de la Mancha, and sets out to fight injustice in
            the name of his beloved maiden Aldonsa, or as he knows her in his mind, Dulcinea del
            Toboso.
        </notes>
    </book>
    <book>
        <title>Jacques le fataliste et son maître</title>
        <author>Denis Diderot</author>
        <language>fr</language>
        <link>http://en.wikipedia.org/wiki/Jacques_le_fataliste_et_son_ma%C3%AEtre</link>
        <rating>5</rating>
        <notes>
            Ce roman complexe et déconcertant — sans doute l'œuvre de Diderot la plus commentée —
            puise pour partie son inspiration dans l'ouvrage de Laurence Sterne, Vie et opinions de
            Tristram Shandy paru quelques années auparavant (1759-1763).
        </notes>
    </book>
    <book>
        <title>Childhood's End</title>
        <author>Arthur C. Clarke</author>
        <language>en</language>
        <link>http://en.wikipedia.org/wiki/Childhood%27s_End</link>
        <rating>5</rating>
        <notes>
            Childhood's End deals with humanity's transformation and integration into an
            interplanetary hive mind. The book also touches on the issues of the Occult, man's
            inability to live in a utopian society, and cruelty to animals, as well as the cliché of
            being the last man on Earth.
        </notes>
    </book>
    <book>
        <title>The Demon-Haunted World</title>
        <author>Carl Sagan</author>
        <language>en</language>
        <link>http://en.wikipedia.org/wiki/The_Demon-Haunted_World</link>
        <rating>5</rating>
        <notes>
            Sagan states that when new ideas are offered for consideration, they should be tested
            by means of skeptical thinking, and should stand up to rigorous questioning.
        </notes>
    </book>
</books>
```

As you can see, the idea is to store the information about all the books in a single XML document. So under a top-level `<books>` element, we put as many `<book>` children elements as needed. You will see later how it is possible with XForms to add and remove children elements. For now, your initial instance declaration is empty and contains a single book, and you place it as usual within an `<xf:model>` element:

```xml
<html>
    <xf:model>
        <xf:instance id="books-instance">
            <books xmlns="">
                <book>
                    <title/>
                    <author/>
                    <language/>
                    <link/>
                    <rating/>
                    <notes/>
                </book>
            </books>
        </xf:instance>
    </xf:model>
</html>
```

Notice the `id` attribute which allows other XForms constructs to refer to that particular instance.

Now do the following:

* Insert the model and instance under the `<head>` element.
* Reload the page.

You should still see a blank page, because so far you haven't added any visual elements!

### First controls

Now it's time to add some visual controls to your page. Start with the following under the `<body>` element:

```xml
<xf:group ref="book">
    <xf:input ref="title">
        <xf:label>Title</xf:label>
    </xf:input>
    <br/>
    <xf:input ref="author">
        <xf:label>Author</xf:label>
    </xf:input>
</xf:group>
```

Reload the page. You should seem something like this:

![][14]

After having looked at the Hello example, this should be clear, with a little novelty: `<xf:group>`: this element allows grouping XForms controls together. The `ref="book"` element changes the current _evaluation context_ for the nested controls, which means that they can use simpler XPath expressions: `ref="title"` instead of `ref="book/title"` and `ref="author"` instead of `ref="book/author"` (groups have other uses but you don't need to learn that now).

Another thing: all XForms controls require a nested `<xf:label>` element, as an effort to help accessibility. In some cases, you won't want an actual label to display next to the control: to achieve this, you can either hide the label with CSS, or use an empty label element (`<xf:label/>`).

### Adding constraints

Now say you want to make the title and author required data. You control this with the `<xf:bind>` element in the XForms model. Add the following under `<xf:model>` after your instance:

```xml
<xf:bind ref="book">
    <xf:bind nodeset="title" required="true()"/>
    <xf:bind nodeset="author" required="true()"/>
</xf:bind>
```

Notice how, as you enter text in the title or author field, the field's background changes color to indicate that the field must be filled out.

The above requires some explanations:

* The `<xf:bind>` element is used to assign so-called _Model Item Properties_ (or _MIPs_) to XForms instance nodes (typically XML elements or attributes). Such properties include whether a field is required, read-only, or visible; whether the field has to satisfy a certain constraint or be of a particular type; and whether the field is a calculated value.
* Here we use the `required` attribute, which determines whether a field is, well, required, that is, whether it has to be filled out by the user.
* Much like `<xf:group>` in the controls, `<xf:bind>` elements can be nested.
* `<xf:bind>` uses a `ref` attribute, which allows pointing at more than one node using a single XPath expression.
* The outer `<xf:bind>` element points to the `<book>` element under the top-level `<books>` element of your instance. This happens because the evaluation context for a top-level XPath expression in an `<xf:bind>` element is the root element of the first XForms instance. You could be more explicit, for example with:

```xml
<xf:bind ref="/books/book">
    ...
</xf:bind>
```

Or with:

```xml
<xf:bind ref="instance('books-instance')/book">
    ...
</xf:bind>
```

The latter makes it clear, with the XForms `instance()` function, that you are addressing the `books-instance` instance and not another instance, so you will probably tend to prefer that notation.

* The inner `<xf:bind>` elements apply the _required_ MIP to the `<title>` and `<author>` elements. The `required` attribute must contain an XPath expression, which is why it contains `true()` (the way to express a Boolean "true" value in XPath) and not simply `true`. Using XPath expressions allows you to make MIPs dynamically change, so that, for example, a form field can be required or not depending on other form fields.
* Note that MIPs are assigned to XML nodes, not directly to controls. But they affect the controls that are bound to those nodes. This is part of XForms's MVC philosophy.

### Single selection controls

XForms is of course not limited to simple input controls. Add the following after the second `<xf:input>` control:

```xml
<xf:select1 ref="language">
    <xf:label>Language</xf:label>
    <xf:item>
        <xf:label>Choose one...</xf:label>
        <xf:value/>
    </xf:item>
    <xf:item>
        <xf:label>English</xf:label>
        <xf:value>en</xf:value>
    </xf:item>
    <xf:item>
        <xf:label>French</xf:label>
        <xf:value>fr</xf:value>
    </xf:item>
    <xf:item>
        <xf:label>Spanish</xf:label>
        <xf:value>es</xf:value>
    </xf:item>
</xf:select1>
```

Reload the page. You should see the following:

![][15]

You have just added a single selection control with `<xf:select1>`. The name means that the user can "select one" item among several items. (XForms tends to call controls using more abstract terms, rather than giving them names such as "combo box" or "menu".) The single selection control usually appears like a drop-down menu or combo box with most XForms implementations (but you can change it's appearance as shown later).

Nested within the control, you find several `<xf:item>` elements. Each one creates an item in the drop-down menu. An item has to sides: the `<xf:label>` element specifies the _label_ that is presented to the user, and the `<xf:value>` element specifies the _value_ that is stored into the XForms instance when the user selects that particular item.

Now XForms encourages you to store data in the model. For a selection control, this means storing the list of labels and values in an XForms instance instead of statically listing the items under the `<xf:select1>` element. So let's do this! Create a new instance in the model:

```xml
<html>
    <xf:instance id="languages-instance">
        <languages xmlns="">
            <language>
                <name>English</name>
                <value>en</value>
            </language>
            <language>
                <name>French</name>
                <value>fr</value>
            </language>
            <language>
                <name>Spanish</name>
                <value>es</value>
            </language>
        </languages>
    </xf:instance>
</html>
```

Then modify the `<xf:select1>` element as follows:

```xml
<xf:select1 ref="language">
    <xf:label>Language</xf:label>
    <xf:item>
        <xf:label>Choose One...</xf:label>
        <xf:value/>
    </xf:item>
    <xf:itemset nodeset="instance('languages-instance')/language">
        <xf:label ref="name"/>
        <xf:value ref="value"/>
    </xf:itemset>
</xf:select1>
```

Notice the new `<xf:itemset>` element in addition to the `<xf:item>` previously used. That element specifies an _item set_, which allows you to point to the list of `<language>` nodes in the `languages-instance` instance, and for each of those to tell the control where to find the label and the value.

You often don't have to use an item set, but using them gives you the flexibility of reusing existing sets of data, dynamically changing the list of items, easing localization, etc.

### Adding a text area

Now add yet another control, a text area:

```xml
<xf:textarea ref="notes" appearance="xxf:autosize">
    <xf:label>Notes</xf:label>
</xf:textarea>
```

The `<xf:textarea>` element acts very much like the HTML `textarea` element. It makes sense to use it to allow entering more than one line of text.

Here there is a little trick: you use the `appearance` attribute to tell Orbeon Forms to use a particular appearance for the text area control. Instead of the standard text area, `appearance="xxf:autosize"` allows the text area to grow vertically as the user enters more text. (This is an appearance which is specific to Orbeon Forms, and you can tell that because of the `xxf:` prefix in the appearance value.)

Note that the application captures the same data without the `appearance` attribute, it's just that the control appears slightly differently and the user experience is changed.

### Finishing-up the controls

To create the ratings input, add this new instance:

```xml
<html>
    <xf:instance id="ratings-instance">
        <ratings xmlns="">
            <rating>
                <name>1</name>
                <value>1</value>
            </rating>
            <rating>
                <name>2</name>
                <value>2</value>
            </rating>
            <rating>
                <name>3</name>
                <value>3</value>
            </rating>
            <rating>
                <name>4</name>
                <value>4</value>
            </rating>
            <rating>
                <name>5</name>
                <value>5</value>
            </rating>
        </ratings>
    </xf:instance>
</html>
```

And then add another `<xf:select1>` control:

```xml
<xf:select1 ref="rating" appearance="full">
    <xf:label>Rating</xf:label>
    <xf:item>
        <xf:label>None</xf:label>
        <xf:value/>
    </xf:item>
    <xf:itemset nodeset="instance('ratings-instance')/rating">
        <xf:label ref="name"/>
        <xf:value ref="value"/>
    </xf:itemset>
</xf:select1>
```

Here again, you store the list of items as a separate instance, but we keep the "empty" item as an `<xf:item>`. There is something new: the use of the `full` appearance, which displays the selection control as a list of radio buttons. This is a standard XForms appearance value, which is likely to be supported by all XForms implementations. (You can tell that it is standard because there is no colon `:` in the appearance value.)

The only missing control now is the input field bound to the `<link>` element. Add this, and you should have something like this in your controls:

```xml
<xf:group ref="book">
    <xf:input ref="title">
        <xf:label>Title</xf:label>
    </xf:input>
    <br/>
    <xf:input ref="author">
        <xf:label>Author</xf:label>
    </xf:input>
    <br/>
    <xf:select1 ref="language">
        <xf:label>Language</xf:label>
        <xf:item>
            <xf:label>Choose One...</xf:label>
            <xf:value/>
        </xf:item>
        <xf:itemset nodeset="instance('languages-instance')/language">
            <xf:label ref="name"/>
            <xf:value ref="value"/>
        </xf:itemset>
    </xf:select1>
    <br/>
    <xf:input ref="link">
        <xf:label>Link</xf:label>
    </xf:input>
    <br/>
    <xf:select1 ref="rating" appearance="full">
        <xf:label>Rating</xf:label>
        <xf:item>
            <xf:label>None</xf:label>
            <xf:value/>
        </xf:item>
        <xf:itemset nodeset="instance('ratings-instance')/rating">
            <xf:label ref="name"/>
            <xf:value ref="value"/>
        </xf:itemset>
    </xf:select1>
    <br/>
    <xf:textarea ref="notes" appearance="xxf:autosize">
        <xf:label>Notes</xf:label>
    </xf:textarea>
</xf:group>
```

And this is how the result should look like (you will see how to add the Save button you see on this screenshot in the next section):

![][16]

By now you probably get the gist of it!

### Adding a "save" button

The Bookcast application now allows you to capture some data. But it is not a very useful application yet, because it doesn't do anything with it! So let's see how you can add a "Save" button that, once pressed, well, saves the data in your form.

Many applications use relational databases as a persistence layer. But because Orbeon Forms and XForms use XML as their native data format, it is very appropriate to use a database that understands XML instead. Orbeon Forms comes with the open source [eXist database][17] that does just that.

So how do you save data from XForms to a database? An important feature of XForms is the XForms _submission_. A submission allows you to read and write XML documents using HTTP and other protocols. Because the eXist database has a REST API (in other words an HTTP-friendly interface), XForms can directly talk to eXist to read and write XML documents and perform all the common CRUD (Create, Retrieve, Update and Delete) operations.

So look at how you create a submission that saves the `books-instance` instance into eXist:

```xml
<xf:submission id="save-submission"
  ref="instance('books-instance')"
  resource="/exist/rest/db/orbeon/my-bookcast/books.xml" method="put" replace="none"/>
```

Let's look at the details:

* The `<xf:submission>` element declares a submission.
* As usual, the `id` attribute allows referring to the submission from other XForms constructs.
* The `ref` attribute specifies what piece of XML must be handled by the submission. It points to an instance node with an XPath expression. Here, we point to the whole `books-instance` instance by using the `instance()` function.
* The `resource` attribute specifies to what URL the submission takes place. Here, you use an absolute path:

```xml
/exist/rest/db/orbeon/my-bookcast/books.xml
```

This path is equivalent to using the absolute URL:

```xml
http://localhost:8080/orbeon/exist/rest/db/orbeon/my-bookcast/books.xml
```

(Because it is inconvenient for you to always write absolute URLs when you want to address an URL handle by Orbeon Forms, Orbeon Forms automatically resolves absolute paths against the base `http://localhost:8080/orbeon/`.)

The paths starts with `/exist/rest/`, which maps to the built-in eXist database. The rest of the path (`/db/orbeon/my-bookcast/books.xml`) specifies the _collection_ and `document` to access. Here, we decide to save the data to a document called `books` within a collection called `/db/orbeon/my-bookcast/`.
* The `method` attribute specifies what HTTP method to use. Here, you use the value `put`, which translates into using the HTTP PUT method. (You may not be very familiar with the PUT method (HTML forms, for example, always use GET and POST), but PUT is getting used more and more with REST interfaces. In just a few words, PUT allows you to store a resource to a particular location on an HTTP server.)
* Finally, the `replace` attribute specifies what to do with the response sent by the server (here the server is the eXist database). Specifying a value of `none` tells the XForms engine to discard the content of the response from the database.

This is great, but specifying a submission does not do anything until you _send_ (execute) that submission. A simple way to do this is to use the submit control:

```xml
<xf:submit submission="save-submission">
    <xf:label>Save</xf:label>
</xf:submit>
```

This control has a `submission` attribute which specifies what submission to send. The control typically looks like a push button, into which the specified label appears. Pressing it automatically sends the submission specified.

So go ahead and:

* Add the submit control just before the `<xf:group>` element.
* Add the submission to the model.
* Reload the page.
* Enter a book title and an author, then press the "Save" button. Your form data has been silently saved to the database. It was that easy!
* Then, let's check that the data is actually in the database. [SINCE 4.0] By default, for security reasons, eXist is setup so you can't directly access it from your browser. However, it is often convenient to do so while in development. For this, comment out the following lines in your `orbeon/EB-INF/web.xml`  noting that you will need to remove the  comment after <url-pattern> to make it well formed XML. (and don't forget to put them back before going to production if necessary):

```xml
<filter-mapping>
    <filter-name>orbeon-exist-filter</filter-name>
    <url-pattern>/exist/*</url-pattern>
    <xsl:comment>Security filter for eXist</xsl:comment>
    <dispatcher>REQUEST</dispatcher>
    <dispatcher>FORWARD</dispatcher>
</filter-mapping>
```

Then, open up a new browser tab or window, and enter the following URL:

```xml
http://localhost:8080/orbeon/exist/rest/db/orbeon/my-bookcast/books.xml
```

This is the exact same URL to which your submission has done an HTTP PUT. By entering it in your browser, you tell it to do an HTTP GET instead, and the eXist database simply sends the XML document to your browser. You should see this:

![][18]

Try changing the book author and pressing "Save" again. Then in your other browser tab or window, reload the eXist URL, and notice that the data has actually changed in the database.

Do you see how persistence is easily implemented with Orbeon Forms? No need for object-relational mapping or for manually marshalling data to and from a database: just send and retrieve XML documents representing your form data!

_NOTE:_

_Of course, you don't have to use eXist or even an XML database with Orbeon Forms: you can in fact interface with any system you can think of with submissions. For systems that don't already have a REST API, you will need to write REST interfaces - and you can do this with your favorite language and platform, including Java, PHP, Ruby, or .NET. You can even write such services with Orbeon Forms XPL (the Orbeon Forms XML pipelines language), which feature built-in components for access to relational databases, web services, and more._

### Loading the initial data

You can now save your data to the database, and read it back using your web browser. The missing part of course consists in allowing the application to automatically load up the data when the page is first displayed.

You guessed it, one way of doing this is to use a submission:

```xml
<xf:submission id="list-submission" serialization="none" method="get" resource="/exist/rest/db/orbeon/my-bookcast/books.xml" replace="instance" instance="books-instance"/>
```

There are a few differences with this `<xf:submission>`:

* The `id` attribute is different: `list-submission`. (All the `id` attributes in a given XForms document must be different.)
* The `serialization="none"` attribute specifies that you don't want to send XML data with this submission.
* The `get` method specifies that you want to do an HTTP GET, like when you pointed your web browser at the URL to read the document from eXist.
* The `replace="instance"` attribute specifies that the result of the submission has to be stored into an instance. The `instance="books-instance"` attribute specifies the identifier of the instance into which the result must be stored.

Add this element after the previous `<xf:submission>` element which has an id value of `save-submission, `i.e. one submission follows the other.

Like with the `<xf:submission`> having  an id of  `save-submission`, the submission needs to be sent to achieve something. You do this by adding the following _event handler_ to the model, just before the end of the model:

```xml
<xf:send ev:event="xforms-ready" submission="list-submission"/>
```

Hence the code now has the appearance of

```xml
<xf:submission id="save-submission" ...

<xf:submission id="list-submission" ...

<xf:send ev:event="xforms-ready" ...

</xf:model>
```

This tells the XForms engine to execute an _action_ called `<xf:send>` when the XForms engine is ready. This action takes an attribute called `submission`, which specifies which submission to send, here `list-submission`.

Note the special attribute called `ev:event`: this attributes marks the `<xf:send>` element as an _event handler_, that is an action that must respond to an event dispatched by the XForms engine. In this case, the name of the event is `xforms-ready`, which is a standard XForms event with the meaning that well, the XForms engine is ready.

After adding the event handler, reload the page, and notice how the page now loads and immediately shows the data that you saved into the database.

The following is an overview of what has just happened:

* You request the page of the Bookcast application with your browser.
* Orbeon Forms receives the request, looks up the page flow file, and finds out that `view.xhtml` must be served.
* `view.xhtml` goes through the XForms engine, which does a few bits of magic: it goes through an initialization phase, where it creates the objects you have defined such as as model, instance, and controls.
* Once this is done, the XForms engine sends the `xforms-ready` event to the model.
* Because you have defined an event handler for `xforms-ready`, that handler is called. This caused the `<xf:send>` action to be run and, therefore, the `list-submission` submission to be sent.
* The submission performs an HTTP GET to the URL you have specified. The connection reaches the built-in eXist database, which returns the document called `books.xml`. The content of that document reaches back the XForms engine, which stores it into the `books-instance` instance.
* The XForms engine updates the XForms controls bound to the instance with the values now contained in the instance. For example, the "title" and "author" input fields are now updated with the values that came from the database.
* The XForms engine sends an HTML page to your web browser. You see the page with all the correct data as saved earlier into the database.

_Actions_ and _events_ are very important in xf: they are the glue that allows you to react to different "things" that happen in an XForms page, whether controlled by the XForms engine or directly by the user. This is very similar to using JavaScript in a regular HTML page. In XForms, they allow you to react to the user pressing a button, entering data, etc. XForms comes with a number of standard events and configurable action that you can combine in many ways, so that in most cases you don't need to use something like JavaScript.

(You may wonder what would happen the first time the `list-submission` is called if no `books.xml` document is available in the database. The answer is that the database would return an error, and the submission would throw an event called `xforms-submit-error`. But because you don't have an event handler for this event, nothing happens: the initial content of the `books-submission` instance is not changed and so you see an empty form.)

### Adding more books

Now let's see how we can enter information about more than a single book!

XForms comes with a really handy construct called `<xf:repeat>`. This allows you to _repeat_ sections of user interface controls, based on instance data. To see how this works, first replace the group you have defined:

```xml
<xf:group ref="book">
    ...
</xf:group>
```

with, instead, this:

```xml
<xf:repeat ref="book" id="book-repeat">
    ...
</xf:repeat>
```

Reload the page, and notice that, well, nothing changes so far!

This tells the XForms engine that the content of the `<xf:repeat>` element must be _repeated_ as many times as `<book>` elements are found in the instance. If you have a single `<book>` element, then the controls are not repeated (which is equivalent to having `<xf:group>`); if you have two `<book>` elements, they are repeated two times, etc.

The trick now is to manage to _add_ a new `<book>` element to the `books-instance` instance. First, create a _template_ for the new `<book>` element to insert by declaring a new instance:

```xml
<html>
    <xf:instance id="book-template">
        <book xmlns="">
            <title/>
            <author/>
            <language/>
            <link/>
            <rating/>
            <notes/>
        </book>
    </xf:instance>
</html>
```

Then you want to copy that template to the right place in `books-instance` when the user presses a button. You do this with a new control called `<xf:trigger>` and a new action called `<xf:insert>`. Add the following to your controls:

```xml
<xf:trigger>
    <xf:label>Add One</xf:label>
    <xf:insert ev:event="DOMActivate" context="instance('books-instance')" nodeset="book" at="1" position="before" origin="instance('book-template')"/>
</xf:trigger>
```

Insert this immediately before the `</xf:repeat>` .

Let's explain what the above does:

* The `<xf:trigger>` element declares a button (remember, XForms likes more abstract names, but this control could have as well been called `<xf:button>`). Like all XForms controls, `<xf:trigger>` takes a label, which is here displayed within the button.
* Once the user presses it, the button sends an event called `DOMActivate`. Don't be scared by this funny name, you will use it all the time. It just means that the user has _activated_ the button, which in most cases means that the user pressed (clicked) on it.
* `<xf:insert>` is declared as an event handler with the `ev:event="DOMActivate"` attribute, so this action runs when the user presses the button.
* Here we have decided that we want to insert a new book always in first position in the page. The trick is to configure the insert action with the appropriate attributes.

With the configuration provided, the action _inserts_ (`<xf:insert>`) the contents of the `book-template` instance (origin="instance('book-template')") _before_ (`position="before"`) the _first_ (`at="1"`) element called `<book>` (`nodeset="book"`) under the `books-instance` instance's root element (`context="instance('books-instance')"`).

This may sound a little confusing at first, but that's because `<xf:insert>` is in fact very powerful and you can combine its attributes in many different ways.

Make the changes above, press on the "Add One" button, and you see a new row of controls created.

![][19]

Again the XForms engine does its magic and takes care of updating the web page automatically. You also notice that the web page does not reload as it updates. This is because Orbeon Forms uses Ajax technology to perform updates to the page. With Ajax, client-side JavaScript code silently talks to the Orbeon Forms server, which then communicates to the client-side code the updates to perform to the page. These update are directly done to the HTML Document Object Model (DOM) without reload.

### Deleting a book

If you can add books, you probably also want to be able to remove them. This can be done with the `<xf:delete>` action. It doesn't seem to make much sense to always remove the first book, but rather, you probably want to delete a specific book. This can be done in several ways, but what about adding a delete button next to each series of repeated controls:

```xml
<xf:trigger>
    <xf:label>Remove</xf:label>
    <xf:delete ev:event="DOMActivate" context="instance('books-instance')"
                   nodeset="book" at="index('book-repeat')"/>
</xf:trigger>
```

This works in a way very similar to the "Add One" button:

* The `<xf:trigger>` element declares a button, but here with a different label ("Remove"). Once the user presses it, the button sends a `DOMActivate`.
* `<xf:delete>` is declared as an event handler with the `ev:event="DOMActivate"` attribute.
* The difference is in the configuration of `<xf:delete>`.

Here you don't use the `position` and `origin` attributes. What you are telling the action here is to delete (`<xf:delete>`) the element called `<book>` (`nodeset="book"`) under the `books-instance` instance's root element (`context="instance('books-instance')"`) which is at the current index position of the `book-repeat` repetition (`at="index('book-repeat')"`).

To understand the `index()` function, you should know that each repetition in XForms has an associated _current index_, which tells you which current iteration of a repetition is currently active. The current index changes as you navigate through controls. If you type in the title input field of the first book, the index is `1`; if you type in the author input field of the third book, the index is `3`; and so on. The index changes also if you click buttons. Usually, the current index is also visually highlighted.

So here, when you click on the "Remove" button of, say, the second book, the index for the `books-repeat` repetition changes to `2`, and therefore `index('books-repeat')` also returns `2`. This way, you can tell `<xf:delete>` to remove the second `<book>` element.

Now add the new trigger within `<xf:repeat>` and reload the page. Try adding books, then removing them by pressing the "Remove" button.

![][20]

### Adding a "revert" button

For fun, let's also add a new button to cancel your unsaved edits and reload the original data from the database. It's as easy as this:

```xml
<xf:submit submission="list-submission">
    <xf:label>Revert</xf:label>
</xf:submit>
```

When you press the "Revert" button, the `list-submission` submission is called, which causes the latest saved `books.xml` document to be reloaded. The XForms engine makes sure that all the controls on the page, including repeats, automatically update to reflect the changes to the `books-instance` instance.

### Making things look nicer

You are probably not very happy with the look of your application. But let's see how you can improve this with CSS.

First, start with a nicer "action bar" at the top of the page:

```xml
<table class="books-action-table">
    <tr>
        <td>
            <xf:submit submission="save-submission" appearance="minimal">
                <xf:label><img src="../apps/my-bookcast/images/save.gif" alt="Save"/> Save</xf:label>
            </xf:submit>
        </td>
        <td>
            <xf:submit submission="list-submission" appearance="minimal">
                <xf:label><img src="../apps/my-bookcast/images/recycle-green.png" alt="Revert"/> Revert</xf:label>
            </xf:submit>
        </td>
    </tr>
</table>
```

You notice that:

* The triggers are also improved with the `minimal` appearance, which renders the trigger as a hyperlink instead of a button.
* You put an HTML image (`<img>`) element within the trigger's label. Yes, this is allowed and allows you to make a clickable icon!

Then encapsulate the main XForms controls within a table:

```xml
<table class="books-table">
    <tr>
        <td>
            <xf:trigger appearance="minimal">
                <xf:label><img src="../apps/my-bookcast/images/add.gif"/></xf:label>
                <xf:insert ev:event="DOMActivate" context="instance('books-instance')" nodeset="book" at="1" position="before" origin="instance('book-template')"/>
            </xf:trigger>
        </td>
        <td class="add-td">
            <xf:trigger appearance="minimal">
                <xf:label>Add One</xf:label>
                <xf:insert ev:event="DOMActivate" context="instance('books-instance')" nodeset="book" at="1" position="before" origin="instance('book-template')"/>
            </xf:trigger>
        </td>
    </tr>
    <xf:repeat nodeset="book" id="book-repeat">
        <tr>
            <td>
                <xf:trigger appearance="minimal">
                    <xf:label><img src="../apps/my-bookcast/images/remove.gif"/></xf:label>
                    <xf:delete ev:event="DOMActivate" context="instance('books-instance')" nodeset="book" at="index('book-repeat')"/>
                </xf:trigger>
            </td>
            <td class="form-td">
                <!--Put the remaining form controls here!-->
                ...
            </td>
        </tr>
    </xf:repeat>
</table>
```

You notice a few more things:

* `<xf:repeat>` is put around an XHTML `<tr>` element, which means that the repetition repeats table rows.
* You add class names on the table and on a table cell, in order to facilitate CSS styling.

Finally, add a `books-label` class to the controls related to book data, for example:

```xml
<xf:label class="books-label">Title</xf:label>
```

Now remember that Orbeon Forms does not send the XForms code directly to the web browser, but instead it transforms it into HTML. You realize that this is done because Orbeon Forms cannot assume that your web browser to support XForms at all. Consider the following examples:

| ----- |
| XForms Source |  HTML Result in Web Browser |
|

```xml
<xf:submit id="my-submit" submission="save-submission">
    <xf:label>Save</xf:label>
</xf:submit>
```

 |

```xml
<button id="my-submit" class="xforms-control xforms-submit" type="button">Save</button>
```

 |
|

```xml
<xf:input id="my-input" ref="title">
    <xf:label>Title</xf:label>
</xf:input>
```

 |

```xml
<label class="xforms-label" for="my-input">Title</label>
```

```xml
<span id="my-input" class="xforms-control xforms-input">
    <span class="xforms-date-display"/>
    <input id="input-my-input" type="text" name="my-input" value="" class="xforms-input-input xforms-type-string"/>
    <span class="xforms-showcalendar xforms-type-string" id="showcalendar-my-input"/>
</span>
```

```xml
<label class="xforms-alert xforms-alert-inactive" for="my-input"/>
```

 |
|

```xml
<xf:textarea ref="notes">
    <xf:label class="books-label">Notes</xf:label>
</xf:textarea>
```

 |

```xml
<label class="books-label xforms-label" for="my-textarea">Notes</label>
```

```xml
<textarea id="my-textarea" class="xforms-control xforms-textarea" name="my-textarea"/>
```

```xml
<label class="xforms-alert xforms-alert-inactive" for="my-textarea"/>
```

 |

And so on for each XForms construct. You notice that Orbeon Forms produces HTML code, including HTML form elements. Ideally, you wouldn't have to know this, and often you don't, but when it comes to styling with CSS having an idea of what the resulting HTML looks like will help you a lot.

So now look at the following CSS declaration for the Bookcast application:

| ----- |
| CSS |  Description |
|  ` .xforms-label { font-weight: bold } `  |  Display all labels in bold.  |
|  ` .books-label { display: -moz-inline-box; display: inline-block; width: expression('9em'); min-width: 9em; } `  |  Display all labels with the `books-label` class to have a minimum width. This allows aligning all the labels on the left. Note the mozilla- and IE-specific CSS.  |
|  ` .xforms-textarea-appearance-xxforms-autosize { width: 20em; margin-bottom: 2px } `  |  Set width and margin to all text area controls with appearance `xxf:autosize`.  |
|  ` .xforms-input input { width: 20em; margin-bottom: 2px } `  |  Set width and margin to all input controls.  |
|  ` .xforms-select1 { margin-bottom: 2px }
.xforms-select1 input { margin-bottom: 2px } `  |  Set margin to all single selection controls.  |
|  ` .books-table { background-color: #fce5b6 }
.books-table .add-td { width: 33em }
.books-table .form-td { width: 33em; background: white; padding: .5em } `  |  Format the main table.  |
|  ` .xforms-repeat-selected-item-1 .form-td { background: #ffc } `  |  Change the background color of the currently selected repeat index.  |
|  ` .books-action-table { margin-bottom: 1em }
.books-action-table td { white-space: nowrap; vertical-align: middle; padding-right: 1em }
.books-action-table .xforms-submit img { vertical-align: middle }
.books-action-table .xforms-trigger-appearance-minimal img { margin-right: 1em; vertical-align: middle } `  |  Set margins and alignment for the action table at the top of the page.  |

Now just add all the CSS declaration under the page's `<head>` element, encapsulated within an HTML `<style>` element:

```xml
<style type="text/css">
    <!--Your CSS declaration here!-->
    ...
</style>
```

Reload the page, and you should see something like this:

![][21]

A little bit of CSS does make things look a little better, doesn't it?

### Adding validation

You already made the title and author mandatory fields, but you may want to validate the data more thoroughly. With XForms, validation has two sides:

* A user-facing side, which tells the user which form fields are invalid. With Orbeon Forms, invalid fields are marked by default with an exclamation point icon. In the Bookcast application, you may want to make sure that the link field contains an HTTP URI with a correct syntax.
* A hidden side, which prevents submission from happening if the data to submit is invalid. With the Bookcast application, for example, it is important to make sure that the data you store into the database follows a certain set of rules. This protects you against invalid data entered by the user, or against programming mistakes.

XForms supports two ways of performing validation:

* With Model Item Properties (MIPs) in the model, called `constraint` and `type`.
* With an _XML Schema_. XML Schema is a W3C standard to specify constraints on XML documents, including constraints on the structure of the document or on the data types contained.

Look at the following XML Schema for the Bookcast application:

```xml
<xs:schema xmlns:xs="http://www.w3.org/2001/XMLSchema" elementFormDefault="qualified" attributeFormDefault="unqualified">

    <!-- Top-level element -->
    <xs:element name="books">
        <xs:complexType>
            <!-- Allow 0 to n books -->
            <xs:sequence minOccurs="0" maxOccurs="unbounded">
                <xs:element name="book" type="book"/>
            </xs:sequence>
        </xs:complexType>
    </xs:element>

    <!-- Individual book element -->
    <xs:complexType name="book">
        <xs:sequence>
            <xs:element name="title" type="xs:string"/>
            <xs:element name="author" type="xs:string"/>
            <xs:element name="language" type="language"/>
            <xs:element name="link" type="link"/>
            <xs:element name="rating" type="rating"/>
            <xs:element name="notes" type="xs:string"/>
        </xs:sequence>
    </xs:complexType>

    <!-- Type for rating -->
    <xs:simpleType name="rating">
        <xs:restriction base="xs:string">
            <xs:enumeration value=""/>
            <xs:enumeration value="1"/>
            <xs:enumeration value="2"/>
            <xs:enumeration value="3"/>
            <xs:enumeration value="4"/>
            <xs:enumeration value="5"/>
        </xs:restriction>
    </xs:simpleType>

    <!-- Type for language -->
    <xs:simpleType name="language">
        <xs:restriction base="xs:string">
            <xs:enumeration value=""/>
            <xs:enumeration value="en"/>
            <xs:enumeration value="fr"/>
            <xs:enumeration value="es"/>
        </xs:restriction>
    </xs:simpleType>

    <!-- Type for link -->
    <xs:simpleType name="link">
        <xs:restriction base="xs:string">
            <!-- Aproximative regexp for HTTP URLs -->
            <xs:pattern value="(https?\://([^/?#]+)/([^?#]*)(\?([^?#]+))?(#(.*))?)?"/>
        </xs:restriction>
    </xs:simpleType>

</xs:schema>
```

XML Schema requires some learning, but for now just consider the following:

* This schema constrains the structure of the `books-instance` document, i.e. it makes sure that the correct elements are encapsulated. If by mistake you create a `<book>` element under `<books>`, this error will be caught.
* This schema also checks the data types for the rating, language and link. If by mistake you allow a rating with value `6`, the schema will catch this.
* A schema is rarely perfect! You can often work more on it and constrain your data in a better way.

Now create the schema as `schema.xsd` in the same directory as `view.xhtml` and `page-flow.xml`. Then link it to the XForms page as follows:

```xml
<xf:model schema="/apps/my-bookcast/schema.xsd">
    <!-- Rest of the XForms model -->
    ...
</xf:model>
```

Alternatively, you can place the schema inline within the XForms model:

```xml
<xf:model >
    <xs:schema elementFormDefault="qualified" attributeFormDefault="unqualified">
        <!-- Rest of the schema -->
    </xs:schema>
    <!-- Rest of the XForms model -->
</xf:model>
```

Also add `<xf:alert>` elements to the controls which might be invalid. This allows you to specify a meaningful validation message:

```xml
<xf:input ref="title">
    <xf:label class="books-label">Title</xf:label>
    <xf:alert>The title is required</xf:alert>
</xf:input>
```

```xml
<xf:input ref="author">
    <xf:label class="books-label">Author</xf:label>
    <xf:alert>The author is required</xf:alert>
</xf:input>
```

```xml
<xf:input ref="link">
    <xf:label class="books-label">Link</xf:label>
    <xf:alert>The link is incorrect</xf:alert>
</xf:input>
```

Reload the page, and try to enter an invalid link, for example "ftp://ftp.example.com/". An alert icon will show up as you leave the link field with your cursor.

_NOTE:_

_The URL of the schema, "/apps/my-bookcast/schema.xsd", is resolved relatively to the external URL of the Bookcast page, so the schema is actually loaded though:_

```xml
http://localhost:8080/orbeon/apps/my-bookcast/schema.xsd
```

Because retrieving documents through HTTP takes some time, you can also use the Orbeon Forms protocol, `oxf:`, to load the schema:

```xml
<xf:model xschema="oxf:/apps/my-bookcast/schema.xsd">
    <!-- Rest of the XForms model -->
    ...
</xf:model>
```

This protocol allows loading files stored as Orbeon Forms resources.

Still with an invalid link, press the "Save" link and check the data in the database. Notice that the invalid data didn't save! This happens because the XForms engine automatically ensures that the data sent by a submission is valid before going on with the actual submission.

It would be nice to tell the user that saving didn't work. You can do this very easily: if a submission error occurs, the `<xf:submission>` element dispaches the `xforms-submit-error` event. So let's see how you catch that event and display a message to the user:

```xml
<xf:submission id="save-submission" ref="instance('books-instance')"
  resource="/exist/rest/db/orbeon/my-bookcast/books.xml" method="put" replace="none">
    <xf:message ev:event="xforms-submit-error" level="modal">An error occurred while saving!</xf:message>
</xf:submission>
```

The `<xf:submission>` element hasn't changed, except we added a nested `<xf:message>` element. Besides the `ev:event` attribute, which you start to be familiar with, this element takes a `level` attribute (use "modal" in general for alerts) and message for the user.

Try now making this change, enter an invalid link, and press the "Save" link: an alert message should show up!

![][22]

### The Atom feed

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
<p:config xmlns:p="http://www.orbeon.com/oxf/pipeline" xmlns:oxf="http://www.orbeon.com/oxf/processors">

    <p:param name="data" type="output"/>

    <!-- Execute REST submission -->
    <p:processor name="oxf:xforms-submission">
        <p:input name="submission">
            <xf:submission xmlns:xforms="http://www.w3.org/2002/xforms" serialization="none" method="get" resource="/exist/rest/db/orbeon/my-bookcast/books.xml"/>
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
<feed xmlns="http://www.w3.org/2005/Atom" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xsl:version="2.0">

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
<link rel="alternate" type="application/atom+xml" title="Orbeon XForms Bookcast Tutorial Feed" href="atom">
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

[1]: http://www.orbeon.com/
[2]: http://www.orbeon.com/community
[3]: https://raw.github.com/wiki/orbeon/orbeon-forms/images/tutorial/01.png
[4]: http://www.orbeon.com/download
[5]: https://raw.github.com/wiki/orbeon/orbeon-forms/images/tutorial/02.png
[6]: http://demo.orbeon.com/orbeon/xforms-hello/
[7]: https://raw.github.com/wiki/orbeon/orbeon-forms/images/tutorial/03.png
[8]: http://www.w3.org/TR/xforms/
[9]: https://raw.github.com/wiki/orbeon/orbeon-forms/images/tutorial/05.png
[10]: https://raw.github.com/wiki/orbeon/orbeon-forms/images/tutorial/06.png
[11]: https://raw.github.com/wiki/orbeon/orbeon-forms/images/tutorial/07.png
[12]: http://demo.orbeon.com/orbeon/xforms-bookcast/
[13]: https://raw.github.com/wiki/orbeon/orbeon-forms/images/tutorial/08.png
[14]: https://raw.github.com/wiki/orbeon/orbeon-forms/images/tutorial/09.png
[15]: https://raw.github.com/wiki/orbeon/orbeon-forms/images/tutorial/11.png
[16]: https://raw.github.com/wiki/orbeon/orbeon-forms/images/tutorial/12.png
[17]: http://exist.sourceforge.net/
[18]: https://raw.github.com/wiki/orbeon/orbeon-forms/images/tutorial/13.png
[19]: https://raw.github.com/wiki/orbeon/orbeon-forms/images/tutorial/14.png
[20]: https://raw.github.com/wiki/orbeon/orbeon-forms/images/tutorial/15.png
[21]: https://raw.github.com/wiki/orbeon/orbeon-forms/images/tutorial/16.png
[22]: https://raw.github.com/wiki/orbeon/orbeon-forms/images/tutorial/17.png
[23]: http://www.w3.org/Submission/xpl/
[24]: http://www.w3.org/TR/xproc/
[25]: https://raw.github.com/wiki/orbeon/orbeon-forms/images/tutorial/19.png
[26]: https://raw.github.com/wiki/orbeon/orbeon-forms/images/tutorial/20.png
[27]: http://demo.orbeon.com/orbeon/home/
