# Introduction

<!-- toc -->

## What is this?

This is the tutorial for Orbeon Forms's XForms features. It is aimed at programmers who either:
 
- want to make changes to the Orbeon Forms source code, which relies in part on XForms
- use [Form Builder][3] but want to add some custom processing to those forms 
- want to write their own XForms applications, as opposed to users who want to use [Form Builder][3]

If you have questions, issues or suggestions related to this tutorial, please send a message to the [Orbeon forum][2].

## Prerequisites

To go through this tutorial, you don't need much: any reasonably modern computer on which you can install Java (7 or 8 are current versions as of November 2016). You should be comfortable with installing new software on your computer, including uncompressing zip or gzip archives. You will also have to edit XML files. If you are familiar with HTML, this should not be a problem.

You also need a reasonably recent web browser. We recommend one of the following browsers:

* Google Chrome
* Safari
* Mozilla Firefox
* Internet Explorer 10, 11, or Microsoft Edge

You will _not_ have to:

* Write any Java code or any scripting language code.
* Use a compiler or other complicated build tool.
* Install browser plugins or any other client-side software, besides your regular web browser!

## Principles of Orbeon Forms

Orbeon Forms follows a few principles:

* **More standards.** You use standards whenever possible. For example, Orbeon Forms applications are created using XForms and XHTML, which are W3C standards.
* **Less scripting.** You write most applications without writing Java, JavaScript, or other scripting code. (But you can if you really want.)
* **Instant gratification.** You get instant gratification by making changes to your application and just reloading your page in your web browser. (You don't need to "compile" or "build".)

[1]: https://www.orbeon.com/
[2]: http://discuss.orbeon.com/
[3]: http://doc.orbeon.com/form-builder/index.html
