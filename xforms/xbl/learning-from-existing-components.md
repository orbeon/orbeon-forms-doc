# Learning from existing components

<!-- toc -->

## Finding existing components

A good thing to do is to look at existing components:

* If you are working with the Orbeon Forms sources, they are located under:
    * [src/resources-packaged/xbl](https://github.com/orbeon/orbeon-forms/tree/master/src/resources-packaged/xbl)
* If you are working with a binary distribution:
    * unzip `orbeon-resources-private.jar`
    * the components are under the `xbl` directory

The "meat" of most components is in files ending with the `.xbl` extension.

## Creating your own component

* create a new `xbl` directory under your RESOURCES directory
* create a directory with your company or project name (e.g. `acme`; Orbeon uses `orbeon`)
* create directory with your new component name (e.g. `cool-stuff`)
* create a new XBL file with the same name in that directory, e.g. `cool-stuff.xbl`
* so you should have: `xbl/acme/cool-stuff/cool-stuff.xbl`

Then:

* copy into your XBL file the content of a simple existing component, like `xbl/orbeon/tutorial-simple/tutorial-simple.xbl`
* modify the binding rule (`fr|tutorial-simple`) into something that matches your component name (`fr|cool-stuff`)
* within an XForms page
    * declare `xmlns:fr="http://orbeon.org/oxf/xml/form-runner"`
    * use the control with something like: `<fr:cool-stuff ref="my-node">`
    * when running your XForms page, you should see an upload field appear!

_NOTE: In your own components, you should not use the `fr:` namespace, but instead you should use your own namespace to avoid naming conflicts._
