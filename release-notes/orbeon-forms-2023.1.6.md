# Orbeon Forms 2023.1.6

__Sunday, December 15, 2024__

Today we released Orbeon Forms 2023.1.6 PE! This maintenance release contains bug-fixes and new features and is recommended for all users of:

- [Orbeon Forms 2023.1.5 PE](orbeon-forms-2023.1.5.md)
- [Orbeon Forms 2023.1.4 PE](orbeon-forms-2023.1.4.md)
- [Orbeon Forms 2023.1.3 PE](orbeon-forms-2023.1.3.md)
- [Orbeon Forms 2023.1.2 PE](orbeon-forms-2023.1.2.md)
- [Orbeon Forms 2023.1.2 PE](orbeon-forms-2023.1.2.md)
- [Orbeon Forms 2023.1.1 PE](orbeon-forms-2023.1.1.md)
- [Orbeon Forms 2023.1 PE](orbeon-forms-2023.1.md)

## Issues addressed

This release addresses the following issues since [Orbeon Forms 2023.1.5 PE](orbeon-forms-2023.1.5.md):

- Form Builder
    - FB: Revision History says that data was "saved with no changes detected." ([\#6618](https://github.com/orbeon/orbeon-forms/issues/6618))
    - Merge section template fails ([\#6541](https://github.com/orbeon/orbeon-forms/issues/6541))
    - Property to disable the keyboard shortcuts hints ([\#6605](https://github.com/orbeon/orbeon-forms/issues/6605))
    - Bookshelf form: "Control Settings" don't open on "Language" control ([\#6615](https://github.com/orbeon/orbeon-forms/issues/6615))
    - Form Builder Export: consider not saving before ([\#6562](https://github.com/orbeon/orbeon-forms/issues/6562))
    - Allow custom model variables references in formulas ([\#6145](https://github.com/orbeon/orbeon-forms/issues/6145))
    - Using a variable in Custom CSS classes AVT crashes Form Builder ([\#6648](https://github.com/orbeon/orbeon-forms/issues/6648))
    - Can't send the focus inside the fields of the Insert/Edit Link opened from the Messages dialog ([\#6665](https://github.com/orbeon/orbeon-forms/issues/6665))
    - Inserting section template doesn't immediately show content of the section ([\#5856](https://github.com/orbeon/orbeon-forms/issues/5856))
- Form Runner
    - Dynamic Dropdown (`fr:databound-select1`, `databound-select1.xbl`) shows empty ([\#6667](https://github.com/orbeon/orbeon-forms/issues/6667))
    - File upload progress bar not showing progress with large files ([\#6666](https://github.com/orbeon/orbeon-forms/issues/6666))
    - "Process failed with error for property: `oxf.fr.detail.process.after-controls.background.new`" ([\#6669](https://github.com/orbeon/orbeon-forms/issues/6669))
    - No success or error message when encountering issue saving with custom provider ([\#6639](https://github.com/orbeon/orbeon-forms/issues/6639))
    - Lost "+" (Add another line to the repeated grid) for multiline repeats ([\#6622](https://github.com/orbeon/orbeon-forms/issues/6622))
    - `ConnectionContextProvider` not called in some cases ([\#6523](https://github.com/orbeon/orbeon-forms/issues/6523))
    - Possible NPE with legacy actions ([\#6528](https://github.com/orbeon/orbeon-forms/issues/6528))
    - List form data attachments API to return control name instead of `_` ([\#6530](https://github.com/orbeon/orbeon-forms/issues/6530))
    - Can't add row to empty repeated grid with multiple lines ([\#6535](https://github.com/orbeon/orbeon-forms/issues/6535))
    - `oxf.fr.detail.button.$button.visible` doesn't support `fr:control-string-value()` ([\#6542](https://github.com/orbeon/orbeon-forms/issues/6542))
    - Legacy action causes error with `$fr-mode` variable ([\#6544](https://github.com/orbeon/orbeon-forms/issues/6544))
    - `fr-internal-authorized-operations` appearing twice ([\#6549](https://github.com/orbeon/orbeon-forms/issues/6549))
    - Summary: can no longer click on columns other than Created/Modified ([\#6550](https://github.com/orbeon/orbeon-forms/issues/6550))
    - JCache implementation to create a cache if one doesn't already exist ([\#6553](https://github.com/orbeon/orbeon-forms/issues/6553))
    - Datepicker missing resources for Czech and Turkish ([\#6560](https://github.com/orbeon/orbeon-forms/issues/6560))
    - Hint as tooltip not showing for datetime control ([\#6563](https://github.com/orbeon/orbeon-forms/issues/6563))
    - `xxf:get-session-attribute()` to support returning a Java object ([\#6564](https://github.com/orbeon/orbeon-forms/issues/6564))
    - Modal loading indicator not showing anymore for process buttons ([\#6578](https://github.com/orbeon/orbeon-forms/issues/6578))
    - `newToEdit()` can fail in the JavaScript environment ([\#6593](https://github.com/orbeon/orbeon-forms/issues/6593))
    - Form Runner summary page reset the language to English ([\#6582](https://github.com/orbeon/orbeon-forms/issues/6582))
    - Exception "form not found for target id" with `dispatchEvent` and no form provided ([\#6595](https://github.com/orbeon/orbeon-forms/issues/6595))
    - "Required item type of first operand of '/' is node(); supplied value has item type xs:string" when dynamic dropdown in section template, used in form, loading the view page ([\#6596](https://github.com/orbeon/orbeon-forms/issues/6596))
    - Dynamic Dropdown with Search: usability issues ([\#6597](https://github.com/orbeon/orbeon-forms/issues/6597))
    - Landing: fix book.jpg image ([\#6585](https://github.com/orbeon/orbeon-forms/issues/6585))
    - Max aggregate file size not enforced when uploading multiple files ([\#6606](https://github.com/orbeon/orbeon-forms/issues/6606))
    - PDF: long filename can overflow over other form controls ([\#6598](https://github.com/orbeon/orbeon-forms/issues/6598))
    - Summary: crashes when some fields are marked as "Show on Summary" ([\#6604](https://github.com/orbeon/orbeon-forms/issues/6604))
    - Plain textarea can be resized to overlap other controls ([\#6612](https://github.com/orbeon/orbeon-forms/issues/6612))
    - Remove section bottom padding when following section has no title ([\#6616](https://github.com/orbeon/orbeon-forms/issues/6616))
    - Revision History: button to add older revisions ([\#6623](https://github.com/orbeon/orbeon-forms/issues/6623))
    - Screen readers announce Font Awesome's Unicode characters ([\#6624](https://github.com/orbeon/orbeon-forms/issues/6624))
    - Help message shows in place of help icon for buttons ([\#6626](https://github.com/orbeon/orbeon-forms/issues/6626))
    - Button label can overflow grid cell ([\#6627](https://github.com/orbeon/orbeon-forms/issues/6627))
    - Better `Accept` headers in requests by the proxy to implementations of the persistence API ([\#6651](https://github.com/orbeon/orbeon-forms/issues/6651))
    - Summary: `fr-table-no-form-data` CSS class remains on table ([\#6653](https://github.com/orbeon/orbeon-forms/issues/6653))
    - List permission should not show for owner/group member ([\#5864](https://github.com/orbeon/orbeon-forms/issues/5864))
    - Formatted Text Area toolbar shows over the text when page zoomed in or zoomed out ([\#6662](https://github.com/orbeon/orbeon-forms/issues/6662))
    - Offline: element `lang` is not correctly serialized/deserialized ([\#6670](https://github.com/orbeon/orbeon-forms/issues/6670))
    - Bulk apply: no user error message if process fails ([\#6672](https://github.com/orbeon/orbeon-forms/issues/6672))
    - Controls form: repeated grid "+" doesn't work ([\#6677](https://github.com/orbeon/orbeon-forms/issues/6677))
    - Save operation completes before all files are uploaded in Multiple File Attachments control ([\#6548](https://github.com/orbeon/orbeon-forms/issues/6548))

You can download the latest version of Orbeon Forms from the [downloads](https://www.orbeon.com/download) page.

Don't forget to [grab a trial license](https://prod.orbeon.com/prod/fr/orbeon/register/new) for the PE version.

Please send feedback via [Twitter](https://twitter.com/orbeon), [Mastodon](https://mastodon.social/@orbeon), or the [forum](https://www.orbeon.com/community).

We hope you enjoy this release!
 