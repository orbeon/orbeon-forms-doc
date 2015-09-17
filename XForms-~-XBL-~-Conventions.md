> [[Home]] ▸ [[XForms]] ▸ [[XBL|XForms ~ XBL]]

## Related pages

- [[Introduction|XForms ~ XBL ~ Introduction]]
- [[FAQ|XForms ~ XBL ~ FAQ]]
- [[Learning from Existing Components|XForms ~ XBL ~ Learning from Existing Components]]
- [[Tutorial|XForms ~ XBL ~ Tutorial]]
- [[XBL Bindings| XForms ~ XBL ~ Bindings]]
- [[XForms Models|XForms ~ XBL ~ XForms Models]]
- [[Including Content|XForms ~ XBL ~ Including Content]]
- [[XBL Event Handling|XForms ~ XBL ~ Event Handling]]

## Following XForms

Whenever it is possible, XBL components should follow patterns found in XForms controls. For instance, if it makes sense to think that the component is bound to a node, then the component should support single node binding attributes on the component element, just like an XForms control would.

## Parameters

Some components take "parameters" that can be specified by users of the component. Consider the existing date picker component. You bind it to a node which contains the date entered by the user, but can also provide a minimum and maximum date. We call those min/max dates _parameters_. Parameters can be:

* **Read-only** – they only provide a value to the component, as in the above case of the min/max dates.
* **Read/write** – the component can update a value stored in a an instance.

The convention dealing with parameters is as follows:

* For **read-only** parameters, users can provide:
    * A **static value** through an attribute:
        * the attribute name is the parameter name;
        * the attribute value is the parameter value.

            ```xml
            <fr:date ref="..." mindate="1970-01-01"/>
            ```

    * A **dynamic value** through a nested element:
        * the element local name is the parameter name;
        * the element namespace is the namespace of the component;
        * the element supports single node binding attributes;
        * if the parameter is read-only, the element supports the `value` attribute like `<xf:output>` does.

            ```xml
            <fr:date ref="...">
                <fr:mindate ref="/parameters/mindate"/>
            </fr:date>
            ```

* For **read/write** parameters, users bind the component to the node from which to read/write the value with:

    * An **attribute**:
        * the attribute name starts with `ref-` followed by the parameter name;
        * the attribute value is a binding expression.

            ```xml
            <fr:map selected-longitude-ref="/coordinates/longitude"/>
            ```

    * An **element**:
        * which works like the element for dynamic values in the read-only case (see above).

            ```xml
            <fr:map>
                <fr:selected-longitude ref="/coordinates/longitude"/>
            </fr:map>
            ```