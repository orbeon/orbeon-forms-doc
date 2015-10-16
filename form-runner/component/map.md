> [[Home]] ▸ Form Runner ▸ [[XBL Components|Form Runner ~ XBL Components]]

## What it does

This component renders a Google Maps widget on the page. It is bound to an address, and optionally to a latitude/longitude. In essence:

- An initial address can be provided to the component. When provided, if found by the Google Maps API, a marker will be placed on the map at that location.
- If a latitude/longitude is provided, it takes precedence over the address and the marker will be placed at that latitude/longitude instead of at the address.
- The user can move the pin to choose another location. When that happens, the latitude/longitude for the new position of the marker will be stored.

![Map component in action](../images/xbl-map.png)

## Usage

You use the map component with:

```xml
<fr:map address-ref="address" id="unittest-map"
        longitude-ref="longitude" latitude-ref="latitude"
        style="width: 500px; height: 300px"/>
```

Where:

- `address-ref` points to a node containing the address, as one string.
- `longitude-ref` and `latitude-ref` points to the nodes optionally containing the initial longitude and latitude. The initial value can be empty. If the marker is moved by the user, the longitude/latitude of the new position will be stored in those nodes.
- `style` used as-is on the `<div>` which contains the actual map. You will typically want to use that attribute to set the size of the map.

## Google API configuration

If you wish to deploy a form using the map component (other than accessing your form with a URL such as `http://localhost:8080/orbeon/...`), you need to [obtain a Google API Key](https://developers.google.com/maps/documentation/javascript/tutorial#api_key). After you obtained your key, indicate it in the following property that you add to your config/properties-local.xml:

```xml
<property as="xs:string"  
          name="oxf.xforms.xbl.fr.map.key"
          value="..."/>
```

Since Orbeon Forms 4.8, the map component uses the Google Map v3 API, and this all you need. Orbeon Forms 4.7 and earlier was using the Google Map v2 API, which is now obsolete, on which the following 2 additional properties for [Google Maps Premier](http://code.google.com/apis/maps/documentation/premier/guide.html) customers are supported:

```xml
<property as="xs:string"  
          name="oxf.xforms.xbl.fr.map.clientid"
          value="..."/>
<property as="xs:boolean"
          name="oxf.xforms.xbl.fr.map.ssl"
          value="true"/>
```

The first is used in place of `oxf.xforms.xbl.fr.map.key` (the client id should be set without the leading `gme-` prefix), and the second allows you to tell Google Maps to use HTTPS. (Since Orbeon Forms 4.8, the component always uses HTTPS to talk to Google's servers, whether your forms are served from an HTTPS server or not.)
