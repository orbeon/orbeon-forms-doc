# Grids CSS

## HTML tables or CSS grids?

The HTML markup for grids has changed over time. The following table summarizes the changes.

| Orbeon Forms Version | Not repeated (no spanning) | Not repeated (spanning) | Repeated  | Design-Time     |
|----------------------|----------------------------|-------------------------|-----------|-----------------|
| Until 2016.1         | `<table>`                  | `<table>`               | `<table>` | `<table>`       |
| 2016.2 until 2017.1  | `<div>`                    | `<table>`               | `<table>` | Same as runtime |
| 2017.2 to 2021.1     | `<div>`                    | `<table>`               | `<table>` | CSS grids       |
| From 2022.1          | CSS grids                  | CSS grids               | CSS grids | CSS grids       |

## Starting Orbeon Forms 2022.1

### Introduction

Starting with Orbeon Forms 2022.1, by default, Orbeon Forms uses [CSS grids](https://developer.mozilla.org/en-US/docs/Web/CSS/CSS_Grid_Layout) for all grids, repeated or not repeated, at runtime as well as at design-time. Previously, the default was to use HTML tables at runtime except for non-repeated grids without rowspans. This is made possible with the drop of Internet Explorer support.

It is possible to change the default back to using HTML tables at runtime with the following property:

```xml
<property
    as="xs:string"
    name="oxf.xforms.xbl.fr.grid.markup.*.*"
    value="html-table"/>
```

### Non-repeated grids without column or row spanning

```html
<div id="my-section-section≡my-grid-grid" class="xbl-component xbl-fr-grid-single xbl-fr-grid">
    <div class="fr-grid fr-grid-12 fr-grid-css fr-grid-grid-1 fr-norepeat">
        <div class="fr-grid-body">
            <div id="my-section-section≡my-grid-grid≡xf-633" class="fr-grid-td xforms-group" data-fr-w="6" data-fr-x="1" data-fr-y="1">
                <!-- Control -->
            </div>
            <div id="my-section-section≡my-grid-grid≡xf-638" class="fr-grid-td xforms-group" data-fr-w="6" data-fr-x="7" data-fr-y="1">
                <!-- Control -->
            </div>
            <div id="my-section-section≡my-grid-grid≡xf-640" class="fr-grid-td xforms-group" data-fr-w="6" data-fr-x="1" data-fr-y="2">
                <!-- Control -->
            </div>
            <div id="my-section-section≡my-grid-grid≡xf-642" class="fr-grid-td xforms-group" data-fr-w="6" data-fr-x="7" data-fr-y="2">
                <!-- Control -->
            </div>
            <div id="my-section-section≡my-grid-grid≡xf-644" class="fr-grid-td xforms-group" data-fr-w="6" data-fr-x="1" data-fr-y="3">
                <!-- Control -->
            </div>
            <div id="my-section-section≡my-grid-grid≡xf-646" class="fr-grid-td xforms-group" data-fr-w="6" data-fr-x="7" data-fr-y="3">
                <!-- Control -->
            </div>
        </div>
    </div>
    <div class="fr-grid-non-empty" id="my-section-section≡my-grid-grid≡xf-648"></div>
</div>
```

### Non-repeated grids with column or row spanning

```html
<div id="my-section-section≡my-grid-grid" class="xbl-component xbl-fr-grid-single xbl-fr-grid">
    <div class="fr-grid fr-grid-12 fr-grid-css fr-grid-grid-1 fr-norepeat">
        <div class="fr-grid-body">
            <div id="my-section-section≡my-grid-grid≡xf-633" class="fr-grid-td xforms-group" data-fr-w="6" data-fr-x="1" data-fr-y="1">
                <!-- Control -->
            </div>
            <div id="my-section-section≡my-grid-grid≡xf-638" class="fr-grid-td xforms-group" data-fr-w="6" data-fr-x="7" data-fr-y="1" data-fr-h="3">
                <!-- Control -->
            </div>
            <div id="my-section-section≡my-grid-grid≡xf-640" class="fr-grid-td xforms-group" data-fr-w="6" data-fr-x="1" data-fr-y="2">
                <!-- Control -->
            </div>
            <div id="my-section-section≡my-grid-grid≡xf-642" class="fr-grid-td xforms-group" data-fr-w="6" data-fr-y="3" data-fr-x="1">
                <!-- Control -->
            </div>
        </div>
    </div>
    <div class="fr-grid-non-empty" id="my-section-section≡my-grid-grid≡xf-644"></div>
</div>
```

### Repeated grids over a single row

```html
<div class="fr-grid fr-grid-12 fr-grid-css fr-grid-grid-2 fr-repeat fr-repeat-single-row">
    <div class="fr-grid-repeat-top-row">
        <div id="section-1-section≡my-grid-grid≡xf-739" class="fr-repeat-column-left xforms-group xforms-readonly">
            <!-- Button to add a new row -->
        </div>
        <div class="fr-grid-head">
            <div class="fr-grid-th" data-fr-w="6" data-fr-x="1" data-fr-y="1">
                <label
                    id="section-1-section≡my-grid-grid≡xf-744"
                    class="xforms-control xforms-label xforms-readonly">Foo</label>
                <span id="section-1-section≡my-grid-grid≡xf-745" class="xforms-control xforms-hint xforms-readonly"></span>
            </div>
            <div class="fr-grid-th" data-fr-w="6" data-fr-x="7" data-fr-y="1">
                <label
                    id="section-1-section≡my-grid-grid≡xf-748"
                    class="xforms-control xforms-label xforms-readonly">Bar</label>
                <span id="section-1-section≡my-grid-grid≡xf-749" class="xforms-control xforms-hint xforms-readonly"></span>
            </div>
        </div>
    </div>
    <div id="repeat-begin-section-1-section≡my-grid-grid≡my-grid-grid-repeat" class="xforms-repeat-begin-end xforms-update-full"></div>
    <div class="xforms-repeat-delimiter"></div>
    <span class="xforms-repeat-selected-item-1 xforms-update-full"></span>
    <div class="fr-grid-repeat-iteration can-insert-above can-insert-below can-clear xforms-repeat-selected-item-1 xforms-update-full" id="section-1-section≡my-grid-grid≡xf-756⊙1">
        <div id="section-1-section≡my-grid-grid≡xf-782⊙1" class="fr-repeat-column-left xforms-group xforms-readonly">
            <div class="dropdown"><!-- Dropdown menu --></div>
        </div>
        <div class="fr-grid-body">
            <div id="section-1-section≡my-grid-grid≡xf-785⊙1" class="fr-grid-td xforms-group" data-fr-w="6" data-fr-x="1" data-fr-y="1">
                <!-- Control -->
            </div>
            <div id="section-1-section≡my-grid-grid≡xf-788⊙1" class="fr-grid-td xforms-group" data-fr-w="6" data-fr-x="7" data-fr-y="1">
                <!-- Control -->
            </div>
        </div>
    </div>
    <span class="xforms-repeat-selected-item-1 xforms-update-full"></span>
    <div id="repeat-end-section-1-section≡my-grid-grid≡my-grid-grid-repeat" class="xforms-repeat-begin-end"></div>
</div>
```

### Repeated grids over two or more rows

```html
<div id="my-section-section≡my-grid-grid" class="xbl-component xbl-fr-grid-multiple xbl-javascript-lifecycle xbl-fr-grid">
    <div class="fr-grid fr-grid-12 fr-grid-css fr-grid-grid-1 fr-repeat fr-repeat-multiple-rows">
        <div class="fr-grid-repeat-top-row">
            <div id="my-section-section≡my-grid-grid≡xf-653" class="fr-repeat-column-left xforms-group xforms-readonly">
                <!-- Button to add a new row -->
            </div>
            <div class="fr-grid-head"></div>
        </div>
        <div id="repeat-begin-my-section-section≡my-grid-grid≡my-grid-grid-repeat" class="xforms-repeat-begin-end xforms-update-full"></div>
        <div class="xforms-repeat-delimiter"></div>
        <span class="xforms-repeat-selected-item-1 xforms-update-full"></span>
        <div class="fr-grid-repeat-iteration can-insert-above can-insert-below can-clear xforms-repeat-selected-item-1 xforms-update-full" id="my-section-section≡my-grid-grid≡xf-662⊙1">
            <div id="my-section-section≡my-grid-grid≡xf-688⊙1" class="fr-repeat-column-left xforms-group xforms-readonly">
                <div class="dropdown"><!-- Dropdown menu --></div>
            </div>
            <div class="fr-grid-body">
                <div id="my-section-section≡my-grid-grid≡xf-691⊙1" class="fr-grid-td xforms-group" data-fr-w="6" data-fr-x="1" data-fr-y="1">
                    <!-- Control -->
                </div>
                <div id="my-section-section≡my-grid-grid≡xf-696⊙1" class="fr-grid-td xforms-group" data-fr-w="6" data-fr-x="7" data-fr-y="1" data-fr-h="3">
                    <!-- Control -->
                </div>
                <div id="my-section-section≡my-grid-grid≡xf-698⊙1" class="fr-grid-td xforms-group" data-fr-w="6" data-fr-x="1" data-fr-y="2">
                    <!-- Control -->
                </div>
                <div id="my-section-section≡my-grid-grid≡xf-700⊙1" class="fr-grid-td xforms-group" data-fr-w="6" data-fr-y="3" data-fr-x="1">
                    <!-- Control -->
                </div>
            </div>
        </div>
        <span class="xforms-repeat-selected-item-1 xforms-update-full"></span>
        <div id="repeat-end-my-section-section≡my-grid-grid≡my-grid-grid-repeat" class="xforms-repeat-begin-end"></div>
    </div>
    <div class="fr-grid-non-empty" id="my-section-section≡my-grid-grid≡xf-702"></div>
</div>
```

## HTML markup until Orbeon Forms 2021.1
 
### Non-repeated grids without rowspans

[SINCE Orbeon Forms 2016.2]

Grids which are not repeated and do not include `rowpan`s do not use a `<table>` element but use instead `<div>`s:

```html
<div class="fr-grid fr-grid-2 fr-norepeat">
    <div class="fr-grid-body">
        <div class="fr-grid-tr">
            <div class="fr-grid-td">
                <div class="fr-grid-content"><!-- Control --></div>
            </div>
            <div class="fr-grid-td">
                <div class="fr-grid-content"><!-- Control --></div>
            </div>
        </div>
        <div class="fr-grid-tr">
            <div class="fr-grid-td">
                <div class="fr-grid-content"><!-- Control --></div>
            </div>
            <div class="fr-grid-td">
                <div class="fr-grid-content"><!-- Control --></div>
            </div>
        </div>
        <div class="fr-grid-tr">
            <div class="fr-grid-td">
                <div class="fr-grid-content"><!-- Control --></div>
            </div>
            <div class="fr-grid-td">
                <div class="fr-grid-content"><!-- Control --></div>
            </div>
        </div>
    </div>
</div>
```

### Non-repeated grids with rowspans

Grids which are not repeated and do include `rowpan`s use a `<table>` element with `role="presentation"`:

```html
<table class="fr-grid fr-grid-2 fr-norepeat" role="presentation">
    <tbody class="fr-grid-body">
        <tr class="fr-grid-tr">
            <td class="fr-grid-td">
                <div class="fr-grid-content"><!-- Control --></div>
            </td>
            <td rowspan="3" class="fr-grid-td">
                <div class="fr-grid-content"><!-- Control --></div>
            </td>
        </tr>
        <tr class="fr-grid-tr">
            <td class="fr-grid-td">
                <div class="fr-grid-content"><!-- Control --></div>
            </td>
        </tr>
        <tr class="fr-grid-tr">
            <td class="fr-grid-td">
                <div class="fr-grid-content"><!-- Control --></div>
            </td>
        </tr>
    </tbody>
</table>
```

### Repeated grids

Repeated grids:
  
- include a header row
- include a left or right column  with icons and menus
- produce repeated rows following the Orbeon Forms XForms `<xf:repeat>` output
- use a `<table>` element

Example:

```html
<table class="fr-grid fr-grid-2 fr-grid-image-attachments table table-bordered table-condensed fr-repeat fr-repeat-single-row">
    <colgroup>
        <col id="fr-view-wizard≡attachment-controls-section≡image-attachments-control≡xf-1411" class="fr-repeat-column-left xforms-group xforms-visited">
        <col class="fr-grid-col-1">
        <col class="fr-grid-col-2">
    </colgroup>
    <thead class="fr-grid-head">
        <tr class="fr-grid-master-row">
            <th id="fr-view-wizard≡attachment-controls-section≡image-attachments-control≡xf-1412" class="fr-repeat-column-left xforms-group xforms-visited">
                <!-- + button -->
            </th>
            <th class="fr-grid-th"><!-- Column header --></th>
            <th class="fr-grid-th"><!-- Column header --></th>
        </tr>
    </thead>
    <tbody class="fr-grid-body">
        <tr id="repeat-begin-fr-view-wizard≡attachment-controls-section≡image-attachments-control≡image-attachments-control-repeat" class="xforms-repeat-begin-end"></tr>
        <tr class="xforms-repeat-delimiter"></tr>
        <tr class="fr-grid-tr can-remove can-move-down can-insert-above can-insert-below" id="fr-view-wizard≡attachment-controls-section≡image-attachments-control≡fr-tr⊙1">
            <td id="fr-view-wizard≡attachment-controls-section≡image-attachments-control≡xf-1448⊙1" class="fr-repeat-column-left xforms-group xforms-visited">
                <div class="dropdown"><!-- Dropdown menu --></div>
            </td>
            <td class="fr-grid-td">
                <div class="fr-grid-content"><!-- Control --></div>
            </td>
            <td class="fr-grid-td">
                <div class="fr-grid-content"><!-- Control --></div>
            </td>
        </tr>
        <tr class="xforms-repeat-delimiter"></tr>
        <tr class="fr-grid-tr can-remove can-move-up can-insert-above can-insert-below xforms-repeat-selected-item-1" id="fr-view-wizard≡attachment-controls-section≡image-attachments-control≡fr-tr⊙2">
            <td id="fr-view-wizard≡attachment-controls-section≡image-attachments-control≡xf-1448⊙2" class="fr-repeat-column-left xforms-group xforms-visited">
                <div class="dropdown"><!-- Dropdown menu --></div>
            </td>
            <td class="fr-grid-td">
                <div class="fr-grid-content"><!-- Control --></div>
            </td>
            <td class="fr-grid-td">
                <div class="fr-grid-content"><!-- Control --></div>
            </td>
        </tr>
        <tr class="xforms-repeat-delimiter"></tr>
        <tr class="xforms-repeat-template" id="fr-view-wizard≡attachment-controls-section≡image-attachments-control≡fr-tr">
            <td id="fr-view-wizard≡attachment-controls-section≡image-attachments-control≡xf-1448" class="fr-repeat-column-left xforms-group">
                <div class="dropdown"><!-- Dropdown menu --></div>
            </td>
            <td class="fr-grid-td">
                <div class="fr-grid-content"><!-- Control --></div>
            </td>
            <td class="fr-grid-td">
                <div class="fr-grid-content"><!-- Control --></div>
            </td>
        </tr>
        <tr id="repeat-end-fr-view-wizard≡attachment-controls-section≡image-attachments-control≡image-attachments-control-repeat" class="xforms-repeat-begin-end"></tr>
    </tbody>
</table>
```