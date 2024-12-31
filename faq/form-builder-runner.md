# FAQ - Form Builder and Form Runner

## General questions

### What is Orbeon Form Builder?

Orbeon Form Builder is a visual form designer which allows you to build and deploy forms in minutes right from your web browser.

For a list of Form Builder features, see [Orbeon Forms Features](/form-runner/feature/responsive-design.md).

### What is Orbeon Form Runner?

Orbeon Form Runner is the Orbeon Forms runtime environment which usually runs forms created with Form Builder.
Form Runner manages form definitions and form data, handles search, validation, and takes care of the plumbing
necessary to capture, save, import and export form data.

For a list of Form Runner features, see [Orbeon Forms Features](/form-runner/feature/responsive-design.md).

### What is the difference between Orbeon Forms, Form Runner and Form Builder?

- Orbeon Forms is the name for the whole forms solution developed by Orbeon.
- Form Runner and Form Builder are components part of the Orbeon Forms solution.

### What are the hardware requirements for Orbeon Forms?

Specific requirements will depend on how much load Orbeon Forms needs to handle, but as a starting point:

- **Memory**
    - We recommend you have enough memory on the server to be able to comfortably allocate at least 4 GB to the JVM.
- **CPU**
     - As of 2019, we recommend you use a server with a modern Intel Xeon or Core i5/i7/i9 CPU with a **Geekbench 5 single-core** score of at least 800.
     - We strongly recommend you avoid servers with older AMD CPUs (prior to the Ryzen line), as they have significantly slower single-core performance, which isn't fit for running Orbeon Forms.
     - Similarly, we strongly recommend you avoid servers with older Sparc CPUs.

### How much load can Orbeon Forms handle?

Our testing using real forms used by customers in production show that Orbeon Forms can sustain, on a laptop-grade 2013 4-core i7 CPU, 400 concurrent active users filling out a field every 10 seconds, or 40 requests per second. This means that if you have a forms with 75 fields filled by users on average, with 1/3 load during a given day, Orbeon Forms can support 40\*3600\*24/3/75 ~= 15,000 form submissions per day, per processor.

We don't recommend you deploy Orbeon Forms on a server less powerful than the aforementioned 2013 4-core i7 CPU. This means that the server has to run on a recent i7 or Xeon processors (not AMD processors), and on [Geekbench](http://www.primatelabs.com/geekbench/) have a score of more than 3,000 on single-core and 12,000 on multi-core.

If you're looking to run Orbeon Forms in the cloud, make sure to pick a configuration that matches the above requirement. For instance, the table below provides a summary of Amazon's AWS offering, with [EC2 pricing](http://aws.amazon.com/ec2/pricing/). On EC2, we recommend you start with a c4.2xlarge.

|            | vCPU | Single-core | Multi-core | Price /year | <!---->            |
|------------|-----:|------------:|-----------:|------------:|--------------------|
| 2013 i7    |      |       3,380 |     12,903 |             | <!---->            |
| c4.xlarge  |    4 |       3,397 |      7,804 |      $1,276 | <!---->            |
| c4.2xlarge |    8 |       3,381 |     14,185 |      $2,552 | ←&nbsp;Recommended |
| c4.4xlarge |   16 |       3,381 |     26,238 |      $5,104 | <!---->            |
| c4.8xlarge |   32 |       3,528 |     53,208 |     $10,208 | <!---->            |

### Where is the Form Builder documentation?

See the [Form Builder](/form-builder/README.md).

## Questions about specific features

### Are forms created with Form Builder accessible?

In short, yes. For details, see [Accessible Forms](https://www.orbeon.com/accessible-forms).

### How can I pass a token a new page and have it saved with the data?

Say you've created a form, deployed it, and would like to take users to the `/new` page for that form, but would like to pass along some information to the form (let's call that piece of information "token"), and have that token saved with the data, so you can then find back the data based on the token.

1. You can do this by passing the token to the form:
	- On the URL, as a request parameter, if the token doesn't need to be private;
	- As a request header, set in a reverse proxy, if the token needs to remain private, and shouldn't be exposed to users.
2. In the form, add a hidden field to store the value of the token. You do this in Form Builder: create a section, which you can name "Internal" for your own reference, and in the Section Settings dialog, under Visibility put `false()`, so the section is never visible to end users. In that section add a text field, name it `token`, and in the field Control Settings, in Formulas, set its Initial Value to:
	- [`xxf:get-request-parameter('token')`](/xforms/xpath/extension-http.md#xxf-get-request-parameter), if you're passing the value with a request parameter named `token`.
	- [`xxf:get-request-header('token')`](/xforms/xpath/extension-http.md#xxf-get-request-header), if you're passing the value with a request header named `token`.
3. When users enter data in the form and save it, the value of the token will be saved with the data, just as if the field was visible to users, and they had entered the value of the token.

### How can I create forms which users can fill out offline?

Orbeon Forms doesn't support filling forms offline. Forms created with Orbeon Forms are filled through a browser, whether on a mobile device or laptop/desktop, and users need to have a connection to the server to load the form, fill out the form, and submit the form.

Note however that the server on which Orbeon Forms runs doesn't itself need to be connected to the Internet, so some situations can be handled by deploying Orbeon Forms close to the end users. For instance, if you have users who need to fill out forms with measurements on a boat in an Arctic expedition, then you could have Orbeon Forms deployed on that server, and users will be able to access the local server, even without an Internet connection.

See this [issue](https://github.com/orbeon/orbeon-forms/issues/1221) for discussion.

### When running Form Builder, I get an "out of memory" error. How can I fix it?

Make sure your Java virtual machine (JVM) is configured with enough heap, as the default is sometimes too low.

This is the `-Xmx` option of Java. Set it to at least 500 MB of heap for local testing (e.g. `-Xmx=500m`), and more for production.

### Can the forms designed with Form Builder run in other XForms engines?

No, for multiple reasons:

- The Orbeon XForms engine relies on advanced features, including [XPath 2.0](/xforms/xpath/README.md), [XBL](/xforms/xbl/faq.md), and [extension functions](/xforms/xpath/README.md) not available in other XForms engines.
- Forms designed with Form Builder assume some standard components provided by Form Runner, like sections and grids.
- A lot of the functionality of the form is handled by the Form Runner runtime, including: saving and retrieving data from a database, autosave, permissions, services and actions, and more. The form itself mainly contains the data model, controls, and descriptions of actions and services.

This said, Form Builder forms are probably one XSLT transformation away from being runnable within some other XForms processors.

### Can I import my existing XForms documents into Form Builder?

Form Builder produces XHTML+XForms files as output, but it follows a number of convention when creating forms. It is only able to read forms that follow those conventions, which means that in general, you can't just import your existing forms into Form Builder.

### Where is my Form Builder form stored?

When you save or publish a form definition, it is stored through the [Form Runner persistence API](/form-runner/api/persistence/README.md).

The API has a number of implementations. The default implementation is the embedded eXist XML database, but you can also use relational or your own implementation of the API.

### Do you integrate with my favorite workflow engine / CMS?

There is no built-in integration with  CMS or workflow engines. However you can integrate with systems in a few ways:

- Form Runner is built around a [REST API for persistence](/form-runner/api/persistence/README.md), which allows you to integrate yourself with any system by providing an implementation of that API.
- [Simple processes](/form-runner/advanced/buttons-and-processes.md) allow you to send data to external systems.

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
- Noscript mode: HTML and CSS [UNTIL Orbeon Forms 2017.2]

### Does JavaScript need to be enabled in the browser to use Orbeon Forms?

Yes. Orbeon Forms is designed to work with JavaScript enabled.

*NOTE: Orbeon Forms used to support a mode called *noscript*, which is removed starting with Orbeon Forms 2018.1. With earlier versions of Orbeon Forms, and with the "noscript" mode enabled, it was possible to run Orbeon Forms without JavaScript in the browser.*

### What format is used to archive data?

- XML
- PDF

### Can Form Runner automatically create tables in my database?

Form Runner by default stores data in XML into eXist and relational databases. This does not require creating new tables, as we use generic tables.

Some relational persistence layers support a "flat view", which is created at form publication time. See [Flat view](/form-runner/persistence/flat-view.md).

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

### Can I use Form Builder to create a form, and put the form on my website?

You can't just put the files produced by Form Builder or the HTML produced by the Form Runner runtime on a website. The main reason is that forms produced by Form Builder need the server-side Form Runner runtime to function.

If you have installed the Form Runner runtime on a server, then you can run Form Runner alongside your other web pages or applications. Please note that Form Runner requires a Java servlet container.

Orbeon Forms 4.7 and newer supports [server side embedding](/form-runner/link-embed/java-api.md).

### Can I use Form Builder to create a form, paste the form in a JSP, and use separate deployment?

Not reliably, sorry.

### Can I use Form Builder to create a form, then paste the form into a plain XForms file, and expect it to work?

No, this is not supported, sorry. Forms created with Form Builder require Form Runner to run. Form Runner is enabled only for forms published with Form Builder.

### Can I customize the appearance of forms I create with Form Builder?

1. You can change fonts, colors, and other styling by creating your own CSS stylesheet, to [supplement or override the default CSS](../form-runner/styling/css.md).
1. For changes that you can't do with CSS and that require modifications to the HTML sent by Orbeon Forms to browser, you can change the Form Runner XBL and XSLT stylesheets. But this is hard work and we discourage it.
