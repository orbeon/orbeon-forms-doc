# Map XBL example

## Setup

1. If using Orbeon Forms 2022.1 or newer, the source of this example ships with the product. If using an earlier version, download the following 3 files: [`map.xbl`](https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/resources/xbl/acme/map/map.xbl), [`map.js`](https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/assets/xbl/acme/map/map.js), [`map.css`](https://github.com/orbeon/orbeon-forms/blob/master/form-runner/jvm/src/main/assets/xbl/acme/map/map.css), and place them in the Orbeon Forms `WEB-INF/resources/xbl/acme/map` directory (on an out-of-the-box installation, `WEB-INF/resources` already exists, but you'll need to create the subdirectories `xbl/acme/map`).
2. [Get a Google API Key](https://developers.google.com/maps/documentation/javascript/tutorial#api_key), and add the following 3 properties to your `properties-local.xml`, setting the value of the first property to your key.

```xml
<property as="xs:string" name="oxf.xforms.xbl.acme.map.key"         value=""/>
<property as="xs:string" name="oxf.xforms.xbl.mapping.acme"         value="http://www.acme.com/xbl"/>
<property as="xs:string" name="oxf.fb.toolbox.group.custom.uri.*.*" value="oxf:/xbl/acme/map/map.xbl"/>
```

## What it does

This component renders a Google Maps widget in your form. It is bound to an address, locates the address using the Google Maps geolocation API, and shows a marker at that location on a map.

![Map component in action](../images/xbl-map.png)
