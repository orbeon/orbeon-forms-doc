# Grids CSS

<!-- toc -->

## HTML markup

[SINCE Orbeon Forms 2016.2]

Form Runner grids produce HTML markup which contains some differences whether a grid is repeated or not, in particular, repeated grids:
  
- include a header row
- include a left or right column  with icons and menus
- produce repeated rows following the Orbeon Forms XForms `<xf:repeat>` output
 
## Non-repeated grids without rowspans

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

## Non-repeated grids with rowspans

Grids which are not repeated and do include `rowpan`s still use a `<table>` element:

```html
<table class="fr-grid fr-grid-2 fr-norepeat">
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

## Repeated grids

Grids which are repeated also use a `<table>` element:

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