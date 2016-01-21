# XForms Logging

<!-- toc -->

## Enabling XForms logging

Sometimes, an error message or stack trace in the Orbeon Forms log file provides enough information to a developer to figure out what went wrong, but not always. In such cases, you can turn to the XForms engine logging facility. To enable it, make sure you uncomment the following logging category under `WEB-INF/resources/config/log4j.xml`:

```xml
<category name="org.orbeon.oxf.xforms.processor.XFormsServer">
    <priority value="debug"/>
</category>
```

_Note: You must restart your Servlet container for those changes to be taken into account._

## Fine-grained configuration

Two properties in `WEB-INF/resources/config/properties-local.xml` control exactly what is logged by the XForms engine:

```xml
    <property as="xs:NMTOKENS" name="oxf.xforms.logging.debug">
        document
        model 
        submission 
        control 
        event 
        action 
        analysis 
        server 
        html
        process
    </property>
    
    <property as="xs:NMTOKENS" name="oxf.xforms.logging.error">
        submission-error-body
    </property>
```

The first property controls what is logged at debug level:  

* Related to a particular XForms document:
    * `model`
        * activity related to `xf:model`, including instance loads, validation, and binds  
    * `submission`
        * timing activity related to `xf:submission`
    * `submission-details`
        * detail activity related to `xf:submission`
        * requires `submission` to be present as well  
    * `control`
        * activity related to controls  
    * `event`
        * activity related to events dispatching and propagation  
    * `action`
        * activity related to XForms actions
    * `document`
        * other activity related to an XForms document
        * this includes the output of the ``<xf:message>`` action with level `xxf:log-debug`  
    * `process`
        * activity related to [processes](../../form-runner/advanced/buttons-and-processes/README.md)
* Not related to a particular XForms document:
    * `analysis`
        * activity related to the static analysis of an XForms document  
    * `server`
        * activity related to handling Ajax requests  
    * `html`
        * activity related to converting XForms to HTML  
    * `resources`
        * activity related to handling XForms CSS and JavaScript resources  
    * `state`
        * activity related to state handling
    * `resolver`
        * activity related to the URI resolver  
    * `utils`
        * miscellaneous activity
    * `cache`
        *  [SINCE Orbeon Forms 4.6]
        * activity of the static state cache during XForms initialization
* Data:
    * `html-static-state`
        * [SINCE: 2010-09-07]
        * requires `html`
        * outputs the static state input
    * `analysis-xbl-tree`
        * requires `analysis`
        * outputs the detail of the XBL shadow trees computed
    * `submission-body`
        * requires `submission` AND `submission-details`
        * outputs the detail of submission request/response bodies
    * `model-serialized-instance`
        * requires `model`
        * outputs the full instances serialized into the dynamic state after an Ajax request completes
    * `server-body`
        * requires `server`
        * outputs the full Ajax request and response bodies

The second property controls what is logged at error level:  

* `submission-error-body`
    * Whether to attempt to output a submission response body when a submission error occurs
    * This is enabled by default, but you can turn it off, e.g. for data sensitivity reasons
    * Binary bodies are not logged, but the logger mentions that the type is a binary type
    * When the response body is NOT logged and is used for `replace="instance|all"`, streaming is taking place. However when the response body IS logged, streaming does not take place:
        * The body is read in memory
        * The body is logged
        * Then the rest of the submission proceeds
* `server-body`
    * output the Ajax request in case of error occurring while processing the request  

## Development configuration

During XForms development, you might want to enable a more aggressive debug configuration.

1. Add (or uncomment) the following in your `config/log4j.xml`:

    ```xml
    <category name="org.orbeon.oxf.xforms.processor.XFormsServer">
        <priority value="debug"/>
    </category>
    ```

2. Configure the `oxf.xforms.logging.debug` property in your `config/properties-local.xml`. You can choose precisely what the XForms engine logs. The following is the most comprehensive configuration. It will log almost everything. In most cases, this is a good configuration during development, and while troubleshooting issues in staging:

    ```xml
    <property as="xs:NMTOKENS" name="oxf.xforms.logging.debug">
        document 
        model 
        submission 
        control 
        event 
        action 
        analysis 
        server 
        server-body 
        html
        submission-details 
        submission-body
    </property>
    ```

## Production configuration 

### No debug output 

In production, you probably don't want any debug information coming out to your logs. So set this in `log4j.xml`:

```xml
<category name="org.orbeon.oxf.xforms.processor.XFormsServer">
    <priority value="info"/>
</category>
```

Alternatively, remove or comment-out the lines above. When this is done, the `oxf.xforms.logging.debug` property is no longer used, so it does not matter what it contains. However, the `oxf.xforms.logging.error` is still relevant. Configure it appropriately, depending on whether you want to see submission responses bodies logged or not.

### Just submission timings   

If you only want to see submission timings, in your `log4j.xml` use:

```xml
<category name="org.orbeon.oxf.xforms.processor.XFormsServer">
    <priority value="debug"/>
</category>
```

And in your `properties-local.xml`:

```xml
<property as="xs:NMTOKENS" name="oxf.xforms.logging.debug">
    submission
</property>
```

## Example output  

The following shows a sample XForms logging session:

```text
XForms server - start handling external events
  XForms server - start handling external event {target id: "age-input-control", event name: "xxforms-value-change-with-focus-change"}
    setvalue - setting instance value {value: "36", changed: "true", instance: "instance"}
    event - start dispatching {name: "xxforms-value-changed", id: "instance"}
    event - end dispatching {time (ms): "0"}
    event - start dispatching {name: "xforms-recalculate", id: "main-model"}
        model - start performing recalculate {model id: "main-model"}
          setvalue - setting instance value {value: "A", changed: "false", instance: "instance"}
          setvalue - setting instance value {value: "A", changed: "false", instance: "countries-instance"}
          setvalue - setting instance value {value: "", changed: "false", instance: "country-details-instance"}
        model - end performing recalculate {time (ms): "9"}
    event - end dispatching {time (ms): "9"}
    event - start dispatching {name: "xforms-revalidate", id: "main-model"}
        model - start performing revalidate {model id: "main-model"}
          event - start dispatching {name: "xxforms-invalid", id: "instance"}
          event - end dispatching {time (ms): "0"}
          event - start dispatching {name: "xxforms-valid", id: "resources-instance"}
          event - end dispatching {time (ms): "0"}
          event - start dispatching {name: "xxforms-valid", id: "flavors-instance"}
          event - end dispatching {time (ms): "0"}
          event - start dispatching {name: "xxforms-valid", id: "carriers-instance"}
          event - end dispatching {time (ms): "0"}
          event - start dispatching {name: "xxforms-valid", id: "countries-names-instance"}
          event - end dispatching {time (ms): "0"}
          event - start dispatching {name: "xxforms-valid", id: "countries-instance"}
          event - end dispatching {time (ms): "0"}
          event - start dispatching {name: "xxforms-valid", id: "country-details-instance"}
          event - end dispatching {time (ms): "0"}
        model - end performing revalidate {time (ms): "4"}
    event - end dispatching {time (ms): "4"}
    event - start dispatching {name: "xforms-refresh", id: "main-model"}
        model - start performing refresh {model id: "main-model"}
          controls - start cloning
          controls - end cloning {time (ms): "0"}
          controls - start updating bindings
          controls - end updating bindings {time (ms): "11", controls updated: "91", repeat iterations: "0"}
          event - start dispatching {name: "xforms-value-changed", id: "age-input-control"}
          event - end dispatching {time (ms): "0"}
          event - start dispatching {name: "xforms-enabled", id: "age-input-control"}
          event - end dispatching {time (ms): "0"}
        model - end performing refresh {time (ms): "17"}
    event - end dispatching {time (ms): "17"}
  XForms server - end handling external event {time (ms): "33"}
XForms server - end handling external events {time (ms): "33"}
```
