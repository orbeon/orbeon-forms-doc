# FAQ - Form Builder and Form Runner

<!-- toc -->

## General questions

### What is Orbeon Form Builder?

Orbeon Form Builder is a visual form designer which allows you to build and deploy forms in minutes right from your web browser.

For a list of Form Builder features, see [Orbeon Forms Features](FIXME Orbeon Forms Features#list-of-orbeon-forms-features).

### What is Orbeon Form Runner?

Orbeon Form Runner is the Orbeon Forms runtime environment which usually runs forms created with Form Builder.
Form Runner manages form definitions and form data, handles search, validation, and takes care of the plumbing
necessary to capture, save, import and export form data.

For a list of Form Runner features, see [Orbeon Forms Features](FIXME Orbeon Forms Features#list-of-orbeon-forms-features).

### What is the difference between Orbeon Forms, Form Runner and Form Builder?

- Orbeon Forms is the name for the whole forms solution developed by Orbeon.
- Form Runner and Form Builder are components part of the Orbeon Forms solution.

You can use Orbeon Forms without using the Form Runner or Form Builder components if all you are interested in is the XForms engine or the pipeline engine.

### How much load can Orbeon Forms handle?

Our testing using real forms used by customers in production show that Orbeon Forms can sustain, on a laptop-grade 2013 4-core i7 CPU, 400 concurrent active users filling out a field every 10 seconds, or 40 requests per second. This means that if you have a forms with 75 fields filled by users on average, with 1/3 load during a given day, Orbeon Forms can support 40\*3600\*24/3/75 ~= 15,000 form submissions per day, per processor.

We don't recommend you deploy Orbeon Forms on a server less powerful than the aforementioned 2013 4-core i7 CPU. This means that the server has to run on a recent i7 or Xeon processors (not AMD processors), and on [Geekbench](http://www.primatelabs.com/geekbench/) have a score of more than 3,000 on single-core and 12,000 on multi-core.

If you're looking to run Orbeon Forms in the cloud, make sure to pick a configuration that matches the above requirement. For instance, the table below provides a summary of Amazon's AWS offering, with [EC2 pricing](http://aws.amazon.com/ec2/pricing/). On EC2, we recommend you start with a c4.2xlarge.

|            | vCPU |Single-core | Multi-core | Price /year  | <!---->
| -----------| ----:|-----------:|-----------:|-------------:|----------------
| 2013 i7    |      | 3,380      | 12,903     |              | <!---->
| c4.xlarge  |    4 | 3,397      |  7,804     |  $1,276      | <!---->
| c4.2xlarge |    8 | 3,381      | 14,185     |  $2,552      |  ←&nbsp;Recommended
| c4.4xlarge |   16 | 3,381      | 26,238     |  $5,104      | <!---->
| c4.8xlarge |   32 | 3,528      | 53,208     | $10,208      | <!---->

### Where is the Form Builder documentation?

See the [Form Builder](faq/form-builder-runner.md).

## Questions about specific features

### How can I create forms which users can fill out offline?

Offline support is not a feature of  Orbeon Forms as of December 2014.

See this [issue](https://github.com/orbeon/orbeon-forms/issues/1221) for discussion.

### When running Form Builder, I get an "out of memory" error. How can I fix it?

Make sure your Java virtual machine (JVM) is configured with enough heap, as the default is sometimes too low.

This is the `-Xmx` option of Java. Set it to at least 500 MB of heap for local testing (e.g. `-Xmx=500m`), and [more for production][7].

### Can the forms designed with Form Builder run in other XForms engines?

No, for multiple reasons:

- The Orbeon XForms engine relies on advanced features, including [XPath 2.0][8], [XBL](../xforms/xbl/faq.md), and [extension functions](../xforms/xpath/README.md) not available in other XForms engines.
- Forms designed with Form Builder assume some standard components provided by Form Runner, like sections and grids.
- A lot of the functionality of the form is handled by the Form Runner runtime, including: saving and retrieving data from a database, autosave, permissions, services and actions, and more. The form itself mainly contains the data model, controls, and descriptions of actions and services.

This said, Form Builder forms are probably one XSLT transformation away from being runnable within some other XForms processors.

### Can I import my existing XForms documents into Form Builder?

Form Builder produces XHTML+XForms files as output, but it follows a number of convention when creating forms. It is only able to read forms that follow those conventions, which means that in general, you can't just import your existing forms into Form Builder.

### Where is my Form Builder form stored?

When you save or publish a form definition, it is stored through the [Form Runner persistence API](../form-runner/api/persistence/README.md).

The API has a number of implementations. The default implementation is the embedded eXist XML database, but you can also use relational or your own implementation of the API.

### Do you integrate with my favorite workflow engine / CMS?

There is no built-in integration with  CMS or workflow engines. However you can integrate with systems in a few ways:

- Form Runner is built around a [REST API for persistence](../form-runner/api/persistence/README.md), which allows you to integrate yourself with any system by providing an implementation of that API.
- [Simple processes](https://github.com/orbeon/orbeon-forms/wiki/Form-Runner-~-Buttons-and-Processes) allow you to send data to external systems.

### Is it possible to edit and update a published form?

- If the form has not been published, simply open the form in Form Builder and make your changes.
- The same goes if the form has been published and no data has yet been entered.
- If the form has been published and data has been entered and your changes modify the structure of the data, you might have to manually migrate the existing data.

### How can I deploy a version of Orbeon Forms without Form Builder / Form Runner?

Remove, respectively:

- `WEB-INF/lib/orbeon-form-builder.jar`
- `WEB-INF/lib/orbeon-form-runner.jar`

_NOTE: Running Form Builder also requires `orbeon-form-runner.jar`._

### What technologies are used by Form Runner on the client?

Regular web technologies:

- AJAX mode: HTML, CSS and JavaScript
- Noscript mode: HTML and CSS

### Does JavaScript need to be enabled in the browser to use Orbeon Forms?

The short answer: In general, yes. But if you use the "noscript" mode (which has limitations), you can use clients which don't have JavaScript.

The long answer: Orbeon Forms is designed to work best with JavaScript enabled. This is where you get the most features of the platform. Functionality in noscript mode is limited, in particular there is no dynamic validation, certain events (focus events) are not available, and some widgets are not available (dialogs, etc.).

Form Builder requires JavaScript to work, but forms built with the builder which don't use features that require JavaScript will work in noscript mode.

### What format is used to archive data?

- XML
- PDF

### Can Form Runner automatically create tables in my database?

Form Runner by default stores data in XML into eXist and relational databases. This does not require creating new tables, as we use generic tables.

Some relational persistence layers support a "flat view", which is created at form publication time. See [Database Support](https://github.com/orbeon/orbeon-forms/wiki/Orbeon-Forms-Features-~-Database-Support).

### If I write my own persistence layer, do I need to recompile Orbeon Forms?

No. A persistence layer implementation consists a few REST services that you implement. You simply tell Orbeon Forms, in a configuration file (`properties-local.xml`), what the URL of the service is.

You can implement them within Orbeon (for example using XML pipelines (XPL), or with any technology you like (Java, Ruby, PHP, you name it). In all cases, you won't need to modify Orbeon Forms beyond configuration properties.

### Can you start creating a form, save it, and get back to it later?

Yes, you can save a form definition and get back to it later.

### Does Form Runner has reporting (graphs and charts) capabilities?

No. But you can use third-party tools to analyze the data.

### What is "as-you-type validation"?

Simply data validation that occurs as you type in a form and/or navigate between form fields.

This is as opposed to validation that occurs only when you press a "submit" or "save" button.

One benefit of as-you-type validation is that it allows the user to detect errors faster.

### Can I use Form Builder to create a form, and put the form on my web site?

You can't just put the files produced by Form Builder or the HTML produced by the Form Runner runtime on a web site. The main reason is that forms produced by Form Builder need the server-side Form Runner runtime to function.

If you have installed the Form Runner runtime on a server, then you can run Form Runner alongside your other web pages or applications. Please note that Form Runner requires a Java servlet container.

Orbeon Forms 4.7 and newer supports [server side embedding](https://github.com/orbeon/orbeon-forms/wiki/Form-Runner-~-APIs-~-Server-side-Embedding).

### Can I use Form Builder to create a form, paste the form in a JSP, and use separate deployment?

Not reliably, sorry.

### Can I use Form Builder to create a form, then paste the form into a plain XForms file, and expect it to work?

No, this is not supported, sorry. Forms created with Form Builder require Form Runner to run. Form Runner is enabled only for forms published with Form Builder.

### Can I customize the appearance of forms I create with Form Builder?

1. You can change fonts, colors, and other styling by creating your own CSS stylesheet, to [supplement or override the default CSS](FIXME Form-Runner-~-Configuration-properties#adding-your-own-css).
1. For changes that you can't do with CSS and that require modifications to the HTML sent by Orbeon Forms to browser, you can change the Form Runner XBL and XSLT stylesheets. But this is hard work and we discourage it.

[7]: http://wiki.orbeon.com/forms/doc/developer-guide/admin/performance-tuning
[8]: http://wiki.orbeon.com/forms/doc/developer-guide/xpath-2-0-support
[10]: http://wiki.orbeon.com/forms/doc/developer-guide/xforms-xpath-functions
